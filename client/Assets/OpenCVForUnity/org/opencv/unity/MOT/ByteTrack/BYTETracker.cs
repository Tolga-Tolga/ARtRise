using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    /// <summary>
    /// BYTETracker
    /// C# implementation of ByteTrack that does not include an object detection algorithm.
    /// The implementation is based on "ByteTrack-cpp". (https://github.com/Vertical-Beach/ByteTrack-cpp)
    /// Only tracking algorithm are implemented.
    /// Any object detection algorithm can be easily combined.
    /// Some code has been modified to obtain the same processing results as the original code below.
    /// https://github.com/ifzhang/ByteTrack/tree/main/deploy/ncnn/cpp
    /// https://github.com/ifzhang/ByteTrack/tree/main/yolox/tracker
    /// </summary>
    public class BYTETracker : IMultiObjectTracker<BYTETrackInfo>
    {
        private readonly float _trackThresh;
        private readonly float _highThresh;
        private readonly float _matchThresh;
        private bool _mot20;
        private readonly int _maxTimeLost;
        private int _frameId;
        private int _trackIdCount;
        private List<Track> _trackedTracks;
        private List<Track> _lostTracks;
        private List<Track> _removedTracks;
        private TrackPool _trackPool;
        private TrackListPool _trackListPool;

        // Store detection array and matching results from the last Update call
        private BBox[] _lastDetections;
        private int[] _lastMatchingTrackIds; // Matched TrackId (including newly created ones)
        private int _lastDetectionCount;

        private bool _disposed = false;

        /// <summary>
        /// Constructor for BYTETracker
        /// </summary>
        /// <param name="frameRate">Frame rate in fps.</param>
        /// <param name="trackBuffer">Number of frames to keep lost tracks.</param>
        /// <param name="trackThresh">Confidence threshold for tracking.</param>
        /// <param name="highThresh">Confidence threshold for adding new tracks (trackThresh + 0.1).</param>
        /// <param name="matchThresh">Matching threshold for tracking.</param>
        /// <param name="mot20">Options specific to MOT20.
        /// true: For MOT20 - Very high density scenes, high difficulty static cameras, dense crowds, lots of occlusion.
        /// false: For MOT17 - Standard diverse scenes, urban areas, sidewalks, moving cameras.</param>
        public BYTETracker(
            int frameRate = 30,
            int trackBuffer = 30,
            float trackThresh = 0.5f,
            float highThresh = 0.6f,
            float matchThresh = 0.8f,
            bool mot20 = false)
        {
            _trackThresh = trackThresh;
            _highThresh = highThresh;
            _matchThresh = matchThresh;
            _mot20 = mot20;
            _maxTimeLost = (int)(frameRate / 30.0f * trackBuffer);
            _frameId = 0;
            _trackIdCount = 0;

            _trackedTracks = new List<Track>();
            _lostTracks = new List<Track>();
            _removedTracks = new List<Track>();

            _trackPool = new TrackPool();
            _trackListPool = new TrackListPool();
        }

#if NET_STANDARD_2_1
        /// <summary>
        /// Update the tracker with a list of detections
        /// </summary>
        /// <param name="detections">ReadOnlySpan of BBoxes</param>
        public void Update(ReadOnlySpan<BBox> detections)
#else
        /// <summary>
        /// Update the tracker with a list of detections
        /// </summary>
        /// <param name="detections">Array of BBoxes</param>
        public void Update(BBox[] detections)
#endif
        {
            ThrowIfDisposed();

            if (detections == null)
                throw new ArgumentNullException(nameof(detections));

            // Clear previous results
            if (_lastDetections != null)
            {
                Array.Clear(_lastMatchingTrackIds, 0, _lastDetectionCount);
            }

            // Store new detection array
#if NET_STANDARD_2_1
            _lastDetections = detections.ToArray();
#else
            _lastDetections = new BBox[detections.Length];
            Array.Copy(detections, _lastDetections, detections.Length);
#endif
            _lastDetectionCount = detections.Length;

            // Initialize matching results array (-1 indicates unprocessed)
            if (_lastMatchingTrackIds == null || _lastMatchingTrackIds.Length < _lastDetectionCount)
            {
                _lastMatchingTrackIds = new int[_lastDetectionCount];
            }
            Array.Fill(_lastMatchingTrackIds, -1, 0, _lastDetectionCount);

            // Reset frame ID processing
            // If the frame ID exceeds 1,000,000,000, reset the frame ID of all tracks
            if (_frameId >= 1_000_000_000)
            {
                foreach (var track in _trackedTracks)
                {
                    int diff = track.FrameId - track.StartFrameId;
                    track.ResetFrameIds(0, diff);
                }
                foreach (var track in _lostTracks)
                {
                    int diff = track.FrameId - track.StartFrameId;
                    track.ResetFrameIds(0, diff);
                }
                foreach (var track in _removedTracks)
                {
                    int diff = track.FrameId - track.StartFrameId;
                    track.ResetFrameIds(0, diff);
                }
                _frameId = 0;
            }

            _frameId++;

            var detTracks = _trackListPool.Get();
            var detLowTracks = _trackListPool.Get();
            var activeTracks = _trackListPool.Get();
            var nonActiveTracks = _trackListPool.Get();
            var currentTrackedTracks = _trackListPool.Get();
            var remainTrackedTracks = _trackListPool.Get();
            var remainDetTracks = _trackListPool.Get();
            var refindTracks = _trackListPool.Get();
            var currentLostTracks = _trackListPool.Get();
            var currentRemovedTracks = _trackListPool.Get();

            try
            {
                ////////////////// Step 1: Get detections //////////////////

                // Create new STracks using the result of object detection
                for (int i = 0; i < detections.Length; i++)
                {
                    ref readonly BBox detection = ref detections[i];
                    var track = _trackPool.Get(detection);

                    // Record original detection index
                    track.SetOriginalDetectionIndex(i);

                    if (detection.Score >= _trackThresh)
                    {
                        detTracks.Add(track);
                    }
                    else
                    {
                        detLowTracks.Add(track);
                    }
                }

                // Create lists of existing STrack
                foreach (var trackedTrack in _trackedTracks)
                {
                    if (!trackedTrack.IsActivated)
                    {
                        nonActiveTracks.Add(trackedTrack);
                    }
                    else
                    {
                        activeTracks.Add(trackedTrack);
                    }
                }

                List<Track> currentTracks = JointTracks(activeTracks, _lostTracks, _trackListPool);

                // Predict current pose by KF
                foreach (var track in currentTracks)
                {
                    track.Predict();
                }

                ////////////////// Step 2: First association, with IoU //////////////////
                (List<(Track, Track)> matches, List<Track> unmatchedTracks, List<Track> unmatchedDetTracks)
                     = LinearAssignment(currentTracks, detTracks, _matchThresh, !_mot20, _trackListPool);

                _trackListPool.Return(currentTracks);

                foreach (var match in matches)
                {
                    var track = match.Item1;
                    var detection = match.Item2;

                    // Record matched detection index
                    int detectionIndex = detection.OriginalDetectionIndex;
                    if (detectionIndex >= 0)
                    {
                        _lastMatchingTrackIds[detectionIndex] = track.TrackId;
                    }

                    if (track.State == TrackState.Tracked)
                    {
                        track.Update(detection, _frameId);
                        currentTrackedTracks.Add(track);
                    }
                    else
                    {
                        track.ReActivate(detection, _frameId);
                        refindTracks.Add(track);
                    }
                    _trackPool.Return(detection);
                }

                remainDetTracks.AddRange(unmatchedDetTracks);

                foreach (var unmatchedTrack in unmatchedTracks)
                {
                    if (unmatchedTrack.State == TrackState.Tracked)
                    {
                        remainTrackedTracks.Add(unmatchedTrack);
                    }
                }

                matches.Clear();
                _trackListPool.Return(unmatchedTracks);
                _trackListPool.Return(unmatchedDetTracks);

                ////////////////// Step 3: Second association, using low score dets //////////////////
                (List<(Track, Track)> rematchedTracks, List<Track> unmatchedRemainTracks, List<Track> unmatchedDetLowTracks)
                     = LinearAssignment(remainTrackedTracks, detLowTracks, 0.5f, false, _trackListPool);

                foreach (var match in rematchedTracks)
                {
                    var track = match.Item1;
                    var detection = match.Item2;

                    // Record matched detection index
                    int detectionIndex = detection.OriginalDetectionIndex;
                    if (detectionIndex >= 0)
                    {
                        _lastMatchingTrackIds[detectionIndex] = track.TrackId;
                    }

                    if (track.State == TrackState.Tracked)
                    {
                        track.Update(detection, _frameId);
                        currentTrackedTracks.Add(track);
                    }
                    else
                    {
                        track.ReActivate(detection, _frameId);
                        refindTracks.Add(track);
                    }
                    _trackPool.Return(detection);
                }

                foreach (var unmatchedTrack in unmatchedRemainTracks)
                {
                    if (unmatchedTrack.State != TrackState.Lost)
                    {
                        unmatchedTrack.MarkAsLost();
                        currentLostTracks.Add(unmatchedTrack);
                    }
                }

                rematchedTracks.Clear();
                _trackListPool.Return(unmatchedRemainTracks);

                foreach (var unmatchedDetLowTrack in unmatchedDetLowTracks)
                {
                    _trackPool.Return(unmatchedDetLowTrack);
                }
                _trackListPool.Return(unmatchedDetLowTracks);

                ////////////////// Step 4: Init new stracks //////////////////
                (List<(Track, Track)> confirmedMatches, List<Track> unmatchedNonActiveTracks, List<Track> unmatchedRemainDetTracks)
                     = LinearAssignment(nonActiveTracks, remainDetTracks, 0.7f, !_mot20, _trackListPool);

                foreach (var match in confirmedMatches)
                {
                    var track = match.Item1;
                    var detection = match.Item2;

                    // Record matched detection index
                    int detectionIndex = detection.OriginalDetectionIndex;
                    if (detectionIndex >= 0)
                    {
                        _lastMatchingTrackIds[detectionIndex] = track.TrackId;
                    }

                    track.Update(detection, _frameId);
                    currentTrackedTracks.Add(track);

                    _trackPool.Return(detection);
                }

                foreach (var unmatchedNonActiveTrack in unmatchedNonActiveTracks)
                {
                    unmatchedNonActiveTrack.MarkAsRemoved();
                    currentRemovedTracks.Add(unmatchedNonActiveTrack);
                }

                // Add new tracks
                foreach (var unmatchedRemainDetTrack in unmatchedRemainDetTracks)
                {
                    if (unmatchedRemainDetTrack.Score < _highThresh)
                    {
                        _trackPool.Return(unmatchedRemainDetTrack);
                        continue;
                    }

                    _trackIdCount++;
                    unmatchedRemainDetTrack.Activate(_frameId, _trackIdCount);
                    currentTrackedTracks.Add(unmatchedRemainDetTrack);

                    // Newly created Track is always in New state, so set -1
                    int detectionIndex = unmatchedRemainDetTrack.OriginalDetectionIndex;
                    if (detectionIndex >= 0)
                    {
                        _lastMatchingTrackIds[detectionIndex] = -1; // Always -1 for New state
                    }
                }

                confirmedMatches.Clear();
                _trackListPool.Return(unmatchedNonActiveTracks);
                _trackListPool.Return(unmatchedRemainDetTracks);

                ////////////////// Step 5: Update state //////////////////
                List<Track> _lostTracks_Removed = new List<Track>();
                foreach (var lostTrack in _lostTracks)
                {
                    if (_frameId - lostTrack.FrameId > _maxTimeLost)
                    {
                        lostTrack.MarkAsRemoved();
                        currentRemovedTracks.Add(lostTrack);

                        _lostTracks_Removed.Add(lostTrack);
                    }
                }
                foreach (var lostTrack in _lostTracks_Removed)
                {
                    _lostTracks.Remove(lostTrack);
                }
                _lostTracks_Removed.Clear();


                _trackListPool.Return(_trackedTracks);
                _trackedTracks = JointTracks(currentTrackedTracks, refindTracks, _trackListPool);

                List<Track> _lost_tracked_Sub = SubTracks(_lostTracks, _trackedTracks, _trackListPool);
                List<Track> _lost_sub_currentLost_Joint = JointTracks(_lost_tracked_Sub, currentLostTracks, _trackListPool);
                _trackListPool.Return(_lost_tracked_Sub);
                _trackListPool.Return(_lostTracks);
                _lostTracks = SubTracks(_lost_sub_currentLost_Joint, _removedTracks, _trackListPool);
                _trackListPool.Return(_lost_sub_currentLost_Joint);

                // Dispose tracks that were removed in the previous frame
                foreach (var track in _removedTracks)
                {
                    _trackPool.Return(track);
                }
                _trackListPool.Return(_removedTracks);
                _removedTracks = currentRemovedTracks;

                (List<Track> trackedTracksOut, List<Track> lostTracksOut, List<Track> removedDuplicateTracks) = RemoveDuplicateTracks(_trackedTracks, _lostTracks, _trackListPool);

                foreach (var track in removedDuplicateTracks)
                {
                    _trackPool.Return(track);
                }
                _trackListPool.Return(removedDuplicateTracks);

                _trackListPool.Return(_trackedTracks);
                _trackedTracks = trackedTracksOut;
                _trackListPool.Return(_lostTracks);
                _lostTracks = lostTracksOut;
            }
            finally
            {
                // Return all temporary lists to the pool
                _trackListPool.Return(detTracks);
                _trackListPool.Return(detLowTracks);
                _trackListPool.Return(activeTracks);
                _trackListPool.Return(nonActiveTracks);
                _trackListPool.Return(currentTrackedTracks);
                _trackListPool.Return(remainTrackedTracks);
                _trackListPool.Return(remainDetTracks);
                _trackListPool.Return(refindTracks);
                _trackListPool.Return(currentLostTracks);
            }
        }

#if NET_STANDARD_2_1
        /// <summary>
        /// Update the tracker with a list of detections
        /// </summary>
        /// <param name="detections">Array of BBoxes</param>
        public void Update(BBox[] detections)
        {
            ThrowIfDisposed();

            if (detections == null)
                throw new ArgumentNullException(nameof(detections));

            Update(detections.AsSpan());
        }
#endif

        /// <summary>
        /// Resets the tracker to its initial state.
        /// This method clears all tracks and resets counters.
        /// </summary>
        public void Reset()
        {
            _frameId = 0;
            _trackIdCount = 0;
            if (_trackedTracks != null) foreach (var track in _trackedTracks) _trackPool.Return(track);
            if (_lostTracks != null) foreach (var track in _lostTracks) _trackPool.Return(track);
            if (_removedTracks != null) foreach (var track in _removedTracks) _trackPool.Return(track);

            _trackedTracks.Clear();
            _lostTracks.Clear();
            _removedTracks.Clear();
        }

        /// <summary>
        /// Get information about currently active tracks
        /// </summary>
        /// <returns>Array of active tracks information</returns>
        public BYTETrackInfo[] GetActiveTrackInfos()
        {
            ThrowIfDisposed();

            int count = 0;
            for (int i = 0; i < _trackedTracks.Count; i++)
            {
                if (_trackedTracks[i].IsActivated)
                {
                    count++;
                }
            }

            var result = new BYTETrackInfo[count];
            int index = 0;
            for (int i = 0; i < _trackedTracks.Count; i++)
            {
                var track = _trackedTracks[i];
                if (track.IsActivated)
                {
                    result[index++] = new BYTETrackInfo(track);
                }
            }

            return result;
        }

        /// <summary>
        /// Get information about currently lost tracks
        /// </summary>
        /// <returns>Array of lost tracks information</returns>
        public BYTETrackInfo[] GetLostTrackInfos()
        {
            ThrowIfDisposed();

            var result = new BYTETrackInfo[_lostTracks.Count];
            for (int i = 0; i < _lostTracks.Count; i++)
            {
                result[i] = new BYTETrackInfo(_lostTracks[i]);
            }

            return result;
        }

        /// <summary>
        /// Get information about tracks that were removed in the last update
        /// Note: These tracks will be disposed in the next update
        /// </summary>
        /// <returns>Array of removed tracks information</returns>
        public BYTETrackInfo[] GetRemovedTrackInfos()
        {
            ThrowIfDisposed();

            var result = new BYTETrackInfo[_removedTracks.Count];
            for (int i = 0; i < _removedTracks.Count; i++)
            {
                result[i] = new BYTETrackInfo(_removedTracks[i]);
            }

            return result;
        }

        /// <summary>
        /// Get information about currently active tracks
        /// </summary>
        /// <returns>Array of active tracks basic information</returns>
        public TrackInfo[] GetActiveBasicTrackInfos()
        {
            ThrowIfDisposed();

            int count = 0;
            for (int i = 0; i < _trackedTracks.Count; i++)
            {
                if (_trackedTracks[i].IsActivated)
                {
                    count++;
                }
            }

            var result = new TrackInfo[count];
            int index = 0;
            for (int i = 0; i < _trackedTracks.Count; i++)
            {
                var track = _trackedTracks[i];
                if (track.IsActivated)
                {
                    ref readonly var rect = ref track.Rect;
                    result[index++] = new TrackInfo(track.TrackId, new BBox(rect.X, rect.Y, rect.Width, rect.Height, track.Score, track.ClassId));
                }
            }

            return result;
        }

        /// <summary>
        /// Get information about currently lost tracks
        /// </summary>
        /// <returns>Array of lost tracks basic information</returns>
        public TrackInfo[] GetLostBasicTrackInfos()
        {
            ThrowIfDisposed();

            var result = new TrackInfo[_lostTracks.Count];
            for (int i = 0; i < _lostTracks.Count; i++)
            {
                ref readonly var rect = ref _lostTracks[i].Rect;
                result[i] = new TrackInfo(_lostTracks[i].TrackId, new BBox(rect.X, rect.Y, rect.Width, rect.Height, _lostTracks[i].Score, _lostTracks[i].ClassId));
            }

            return result;
        }

        /// <summary>
        /// Get information about tracks that were removed in the last update
        /// Note: These tracks will be disposed in the next update
        /// </summary>
        /// <returns>Array of removed tracks basic information</returns>
        public TrackInfo[] GetRemovedBasicTrackInfos()
        {
            ThrowIfDisposed();

            var result = new TrackInfo[_removedTracks.Count];
            for (int i = 0; i < _removedTracks.Count; i++)
            {
                ref readonly var rect = ref _removedTracks[i].Rect;
                result[i] = new TrackInfo(_removedTracks[i].TrackId, new BBox(rect.X, rect.Y, rect.Width, rect.Height, _removedTracks[i].Score, _removedTracks[i].ClassId));
            }

            return result;
        }

        /// <summary>
        /// Log tracking status
        /// </summary>
        public void LogTrackingStatus()
        {
            ThrowIfDisposed();

            Debug.Log($"[BYTETracker] Tracking Status [FrameId={_frameId}]:\n" +
                     $"  Tracked Tracks: {_trackedTracks.Count}\n" +
                     $"  Lost Tracks: {_lostTracks.Count}\n" +
                     $"  Removed Tracks: {_removedTracks.Count}");
        }

        /// <summary>
        /// Log tracking status detail
        /// </summary>
        public void LogTrackingStatusDetail()
        {
            ThrowIfDisposed();

            StringBuilder trackingStatusStr = new StringBuilder();

            trackingStatusStr.AppendLine($"[BYTETracker] Tracking Status Detail [FrameId={_frameId}]:");
            trackingStatusStr.Append($"  Tracked Tracks : {_trackedTracks.Count}");
            foreach (var track in _trackedTracks)
            {
                trackingStatusStr.AppendLine();
                trackingStatusStr.Append($"    Track[{track.TrackId}]: State={track.State}, FrameId={track.FrameId}, StartFrameId={track.StartFrameId}, TrackletLength={track.TrackletLength}");
            }
            trackingStatusStr.AppendLine();
            trackingStatusStr.Append($"  Lost Tracks : {_lostTracks.Count}");
            foreach (var track in _lostTracks)
            {
                trackingStatusStr.AppendLine();
                trackingStatusStr.Append($"    Track[{track.TrackId}]: State={track.State}, FrameId={track.FrameId}, StartFrameId={track.StartFrameId}, TrackletLength={track.TrackletLength}");
            }
            trackingStatusStr.AppendLine();
            trackingStatusStr.Append($"  Removed Tracks : {_removedTracks.Count}");
            foreach (var track in _removedTracks)
            {
                trackingStatusStr.AppendLine();
                trackingStatusStr.Append($"    Track[{track.TrackId}]: State={track.State}, FrameId={track.FrameId}, StartFrameId={track.StartFrameId}, TrackletLength={track.TrackletLength}");
            }
            trackingStatusStr.AppendLine();
            trackingStatusStr.Append(_trackPool.GetStatsString());
            trackingStatusStr.AppendLine();
            trackingStatusStr.Append(_trackListPool.GetStatsString());

            Debug.Log(trackingStatusStr.ToString());
        }

        /// <summary>
        /// Get the matching results from the last Update call
        /// </summary>
        /// <returns>Array of TrackIds corresponding to the last detection array. -1 indicates no match or low score.</returns>
        public int[] GetLastMatchingTrackIds()
        {
            ThrowIfDisposed();

            if (_lastMatchingTrackIds == null || _lastDetectionCount == 0)
            {
                return Array.Empty<int>();
            }

            var result = new int[_lastDetectionCount];
            Array.Copy(_lastMatchingTrackIds, result, _lastDetectionCount);
            return result;
        }

        /// <summary>
        /// Get the matching results from the last Update call with detection information
        /// </summary>
        /// <returns>Array of tuples containing (detection, trackId). TrackId is -1 if no match or low score.</returns>
        public (BBox detection, int trackId)[] GetLastMatchingResults()
        {
            ThrowIfDisposed();

            if (_lastDetections == null || _lastDetectionCount == 0)
            {
                return Array.Empty<(BBox, int)>();
            }

            var result = new (BBox, int)[_lastDetectionCount];
            for (int i = 0; i < _lastDetectionCount; i++)
            {
                result[i] = (_lastDetections[i], _lastMatchingTrackIds[i]);
            }
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_trackedTracks != null) foreach (var track in _trackedTracks) _trackPool.Return(track);
                if (_lostTracks != null) foreach (var track in _lostTracks) _trackPool.Return(track);
                if (_removedTracks != null) foreach (var track in _removedTracks) _trackPool.Return(track);

                _trackedTracks.Clear(); _trackedTracks = null;
                _lostTracks.Clear(); _lostTracks = null;
                _removedTracks.Clear(); _removedTracks = null;

                _trackListPool.Clear(); _trackListPool = null;
                _trackPool.Clear(); _trackPool = null;
            }

            _disposed = true;
        }

        ~BYTETracker()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        /// <summary>
        /// Combines two track lists into one, ensuring no duplicate tracks by reference equality
        /// </summary>
        /// <param name="aTracks">First list of tracks</param>
        /// <param name="bTracks">Second list of tracks</param>
        /// <param name="pool">TrackListPool instance for memory management</param>
        /// <returns>Combined list of tracks with no duplicates</returns>
        private List<Track> JointTracks(List<Track> aTracks, List<Track> bTracks, TrackListPool pool)
        {
            var res = pool.Get();
            var exists = new HashSet<Track>();

            foreach (var track in aTracks)
            {
                exists.Add(track);
                res.Add(track);
            }

            foreach (var track in bTracks)
            {
                if (!exists.Contains(track))
                {
                    exists.Add(track);
                    res.Add(track);
                }
            }

            return res;
        }

        /// <summary>
        /// Subtracts tracks from list B that exist in list A based on reference equality
        /// </summary>
        /// <param name="aTracks">List of tracks to subtract from</param>
        /// <param name="bTracks">List of tracks to subtract</param>
        /// <param name="pool">TrackListPool instance for memory management</param>
        /// <returns>List of tracks from A that don't exist in B</returns>
        private List<Track> SubTracks(List<Track> aTracks, List<Track> bTracks, TrackListPool pool)
        {
            var res = pool.Get();
            var exists = new HashSet<Track>(bTracks);

            foreach (var track in aTracks)
            {
                if (!exists.Contains(track))
                {
                    res.Add(track);
                }
            }

            return res;
        }

        /// <summary>
        /// Removes duplicate tracks between two lists based on IoU and tracking duration
        /// When tracks overlap significantly (IoU > 0.85), the track with longer tracking duration is kept
        /// </summary>
        /// <param name="aTracks">First list of tracks</param>
        /// <param name="bTracks">Second list of tracks</param>
        /// <param name="pool">TrackListPool instance for memory management</param>
        /// <returns>Tuple containing three lists: (non-duplicate tracks from A, non-duplicate tracks from B, removed duplicate tracks)</returns>
        private (List<Track> nonDuplicateTracksA, List<Track> nonDuplicateTracksB, List<Track> removedDuplicateTracks)
             RemoveDuplicateTracks(List<Track> aTracks, List<Track> bTracks, TrackListPool pool)
        {
            var (ious, rows, cols) = CalcIouDistance(aTracks, bTracks);

            try
            {
#if NET_STANDARD_2_1
                bool[] aOverlapping = ArrayPool<bool>.Shared.Rent(aTracks.Count);
                bool[] bOverlapping = ArrayPool<bool>.Shared.Rent(bTracks.Count);
#else
                bool[] aOverlapping = new bool[aTracks.Count];
                bool[] bOverlapping = new bool[bTracks.Count];
#endif
                try
                {
                    Array.Clear(aOverlapping, 0, aTracks.Count);
                    Array.Clear(bOverlapping, 0, bTracks.Count);

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            int index = i * cols + j;
                            if (ious[index] < 0.15f)
                            {
                                var timeP = aTracks[i].FrameId - aTracks[i].StartFrameId;
                                var timeQ = bTracks[j].FrameId - bTracks[j].StartFrameId;
                                if (timeP > timeQ)
                                {
                                    bOverlapping[j] = true;
                                }
                                else
                                {
                                    aOverlapping[i] = true;
                                }
                            }
                        }
                    }

                    var resA = pool.Get();
                    var resB = pool.Get();
                    var removedTracks = pool.Get();

                    resA.Capacity = aTracks.Count;
                    resB.Capacity = bTracks.Count;

                    for (int ai = 0; ai < aTracks.Count; ai++)
                    {
                        if (!aOverlapping[ai])
                        {
                            resA.Add(aTracks[ai]);
                        }
                        else
                        {
                            removedTracks.Add(aTracks[ai]);
                        }
                    }

                    for (int bi = 0; bi < bTracks.Count; bi++)
                    {
                        if (!bOverlapping[bi])
                        {
                            resB.Add(bTracks[bi]);
                        }
                        else
                        {
                            removedTracks.Add(bTracks[bi]);
                        }
                    }

                    return (resA, resB, removedTracks);
                }
                finally
                {
#if NET_STANDARD_2_1
                    ArrayPool<bool>.Shared.Return(aOverlapping);
                    ArrayPool<bool>.Shared.Return(bOverlapping);
#endif
                }
            }
            finally
            {
#if NET_STANDARD_2_1
                if (ious.Length > 0)
                    ArrayPool<float>.Shared.Return(ious);
#endif
            }
        }

        /// <summary>
        /// Performs linear assignment between two lists of tracks using Hungarian algorithm
        /// </summary>
        /// <param name="a_tracks">First list of tracks</param>
        /// <param name="b_tracks">Second list of tracks</param>
        /// <param name="thresh">Cost threshold for matching (higher values allow more matches)</param>
        /// <param name="use_fuse_score">Whether to fuse detection score with IoU cost</param>
        /// <param name="pool">TrackListPool instance for memory management</param>
        /// <returns>Tuple containing:
        /// - List of matched track pairs (a_track, b_track)
        /// - List of unmatched tracks from a_tracks
        /// - List of unmatched tracks from b_tracks</returns>
        private (List<(Track a_matchedTrack, Track b_matchedTrack)>, List<Track> a_unmatchedTracks, List<Track> b_unmatchedTracks)
             LinearAssignment(List<Track> a_tracks, List<Track> b_tracks, float thresh, bool use_fuse_score, TrackListPool pool)
        {
            if (a_tracks.Count == 0 || b_tracks.Count == 0)
            {
                var emptyMatches = new List<(Track, Track)>();
                var a_tracksClone = pool.Get();
                var b_tracksClone = pool.Get();
                a_tracksClone.AddRange(a_tracks);
                b_tracksClone.AddRange(b_tracks);
                return (emptyMatches, a_tracksClone, b_tracksClone);
            }

            var (cost_matrix, rows, cols) = CalcIouDistance(a_tracks, b_tracks);

            try
            {
                if (use_fuse_score)
                {
                    var fused = FuseScore(cost_matrix, rows, cols, b_tracks);
                    cost_matrix = fused.cost_matrix;
                    rows = fused.rows;
                    cols = fused.cols;
                }

                var matches = new List<(Track, Track)>();
                var a_unmatched = pool.Get();
                var b_unmatched = pool.Get();

                var (rowsol, colsol, _) = ExecLapjv(cost_matrix, rows, cols, true, thresh, false);
                try
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (rowsol[i] >= 0 && rowsol[i] < b_tracks.Count)
                        {
                            matches.Add((a_tracks[i], b_tracks[rowsol[i]]));
                        }
                        else
                        {
                            a_unmatched.Add(a_tracks[i]);
                        }
                    }

                    for (int i = 0; i < cols; i++)
                    {
                        if (colsol[i] < 0) b_unmatched.Add(b_tracks[i]);
                    }

                    return (matches, a_unmatched, b_unmatched);
                }
                finally
                {
#if NET_STANDARD_2_1
                    ArrayPool<int>.Shared.Return(rowsol);
                    ArrayPool<int>.Shared.Return(colsol);
#endif
                }
            }
            finally
            {
#if NET_STANDARD_2_1
                if (cost_matrix.Length > 0)
                    ArrayPool<float>.Shared.Return(cost_matrix);
#endif
            }
        }

        /// <summary>
        /// Calculates IoU-based distance matrix between two lists of tracks
        /// For tracks of different classes, the cost is set to prevent matching
        /// </summary>
        /// <param name="a_tracks">First list of tracks</param>
        /// <param name="b_tracks">Second list of tracks</param>
        /// <returns>Tuple containing:
        /// - 1D array of costs (1 - IoU) between each pair of tracks (Note: Caller is responsible for returning this array to ArrayPool)
        /// - Number of rows (a_tracks.Count)
        /// - Number of columns (b_tracks.Count)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (float[] cost_matrix, int rows, int cols) CalcIouDistance(List<Track> a_tracks, List<Track> b_tracks)
        {
            int rows = a_tracks.Count;
            int cols = b_tracks.Count;

            if (rows * cols == 0)
            {
                return (Array.Empty<float>(), 0, 0);
            }
#if NET_STANDARD_2_1
            float[] cost_matrix = ArrayPool<float>.Shared.Rent(rows * cols);
#else
            float[] cost_matrix = new float[rows * cols];
#endif
            try
            {
                for (int ai = 0; ai < rows; ai++)
                {
                    int baseIndex = ai * cols;
                    for (int bi = 0; bi < cols; bi++)
                    {
                        int index = baseIndex + bi;
                        // If the STracks are of different classes, they should never be matched
                        if (a_tracks[ai].ClassId != b_tracks[bi].ClassId)
                        {
                            cost_matrix[index] = float.MaxValue;
                        }
                        else
                        {
                            cost_matrix[index] = 1 - Track.CalcIoU(a_tracks[ai], b_tracks[bi]);
                        }
                    }
                }

                return (cost_matrix, rows, cols);
            }
            catch
            {
#if NET_STANDARD_2_1
                ArrayPool<float>.Shared.Return(cost_matrix);
#endif
                throw;
            }
        }

        /// <summary>
        /// Fuses detection scores with IoU costs to create a combined cost matrix
        /// The fused cost is calculated as: 1 - (IoU * detection_score)
        /// </summary>
        /// <param name="cost_matrix">Original IoU-based cost matrix</param>
        /// <param name="rows">Number of rows in the cost matrix</param>
        /// <param name="cols">Number of columns in the cost matrix</param>
        /// <param name="detections">List of detections containing scores</param>
        /// <returns>Fused cost matrix incorporating both IoU and detection scores</returns>
        private (float[] cost_matrix, int rows, int cols) FuseScore(float[] cost_matrix, int rows, int cols, List<Track> detections)
        {
            if (cost_matrix.Length == 0)
                return (cost_matrix, rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int index = i * cols + j;
                    float det_score = detections[j].Score;
                    float iou_sim = 1.0f - cost_matrix[index];
                    float fuse_sim = iou_sim * det_score;
                    cost_matrix[index] = 1.0f - fuse_sim;
                }
            }

            return (cost_matrix, rows, cols);
        }

        /// <summary>
        /// Executes the Hungarian algorithm (LAPJV) for optimal assignment
        /// </summary>
        /// <param name="cost">Cost matrix for assignment</param>
        /// <param name="rows">Number of rows in the cost matrix</param>
        /// <param name="cols">Number of columns in the cost matrix</param>
        /// <param name="extend_cost">Whether to extend the cost matrix for rectangular assignment</param>
        /// <param name="cost_limit">Maximum cost threshold for valid assignments</param>
        /// <param name="return_cost">Whether to calculate and return the total assignment cost</param>
        /// <returns>Tuple containing:
        /// - List of row assignments (track indices) (Note: Caller is responsible for returning this array to ArrayPool)
        /// - List of column assignments (detection indices) (Note: Caller is responsible for returning this array to ArrayPool)
        /// - Total assignment cost (if return_cost is true)</returns>
        private (int[] rowsol, int[] colsol, double opt) ExecLapjv(
            float[] cost,
            int rows,
            int cols,
            bool extend_cost = false,
            float cost_limit = float.MaxValue,
            bool return_cost = true)
        {
#if NET_STANDARD_2_1
            int[] rowsol = ArrayPool<int>.Shared.Rent(rows);
            int[] colsol = ArrayPool<int>.Shared.Rent(cols);
#else
            int[] rowsol = new int[rows];
            int[] colsol = new int[cols];
#endif
            try
            {
                if (rows != cols && !extend_cost)
                {
                    throw new InvalidOperationException("The `extendCost` variable should be set to true.");
                }

                int n = 0;
                float[] cost_c;

                if (extend_cost || cost_limit < float.MaxValue)
                {
                    n = rows + cols;

#if NET_STANDARD_2_1
                    cost_c = ArrayPool<float>.Shared.Rent(n * n);
#else
                    cost_c = new float[n * n];
#endif
                    try
                    {
                        if (cost_limit < float.MaxValue)
                        {
                            Array.Fill(cost_c, cost_limit / 2.0f, 0, n * n);
                        }
                        else
                        {
                            float cost_max = -1;
                            for (int i = 0; i < rows; i++)
                            {
                                for (int j = 0; j < cols; j++)
                                {
                                    if (cost[i * cols + j] > cost_max)
                                        cost_max = cost[i * cols + j];
                                }
                            }
                            Array.Fill(cost_c, cost_max + 1, 0, n * n);
                        }

                        // Initialize the extended part
                        for (int i = rows; i < n; i++)
                        {
                            for (int j = cols; j < n; j++)
                            {
                                cost_c[i * n + j] = 0;
                            }
                        }

                        // Copy the original cost matrix
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < cols; j++)
                            {
                                cost_c[i * n + j] = cost[i * cols + j];
                            }
                        }
                    }
                    catch
                    {
#if NET_STANDARD_2_1
                        ArrayPool<float>.Shared.Return(cost_c);
#endif
                        throw;
                    }
                }
                else
                {
                    n = rows;
                    cost_c = cost;
                }
#if NET_STANDARD_2_1
                int[] x_c = ArrayPool<int>.Shared.Rent(n);
                int[] y_c = ArrayPool<int>.Shared.Rent(n);
#else
                int[] x_c = new int[n];
                int[] y_c = new int[n];
#endif
                try
                {
                    Array.Clear(x_c, 0, n);
                    Array.Clear(y_c, 0, n);

                    int ret = Lapjv.LapjvInternal(n, cost_c, x_c, y_c);
                    if (ret != 0)
                    {
                        throw new InvalidOperationException("The result of LapjvInternal() is invalid.");
                    }

                    double opt = 0.0;

                    if (n != rows)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (x_c[i] >= cols) x_c[i] = -1;
                            if (y_c[i] >= rows) y_c[i] = -1;
                        }
                        Array.Copy(x_c, 0, rowsol, 0, rows);
                        Array.Copy(y_c, 0, colsol, 0, cols);

                        if (return_cost)
                        {
                            for (int i = 0; i < rows; i++)
                            {
                                if (rowsol[i] != -1)
                                {
                                    opt += cost_c[i * n + rowsol[i]];
                                }
                            }
                        }

                        if (cost_c != cost)
                        {
#if NET_STANDARD_2_1
                            ArrayPool<float>.Shared.Return(cost_c);
#endif
                        }
                    }
                    else if (return_cost)
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            opt += cost_c[i * cols + rowsol[i]];
                        }
                    }

                    return (rowsol, colsol, opt);
                }
                finally
                {
#if NET_STANDARD_2_1
                    ArrayPool<int>.Shared.Return(x_c);
                    ArrayPool<int>.Shared.Return(y_c);
#endif
                }
            }
            catch
            {
#if NET_STANDARD_2_1
                ArrayPool<int>.Shared.Return(rowsol);
                ArrayPool<int>.Shared.Return(colsol);
#endif
                throw;
            }
        }
    }
}
