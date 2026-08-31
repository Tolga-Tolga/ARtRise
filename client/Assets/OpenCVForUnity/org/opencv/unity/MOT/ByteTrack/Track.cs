using System;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    internal class Track : IDisposable
    {
        private BTKalmanFilter _kalmanFilter;
        private BTRect _rect;
        private TrackState _state;
        private bool _isActivated;
        private int _classId;
        private float _score;
        private int _trackId;
        private int _frameId;
        private int _startFrameId;
        private int _trackletLength;
        private int _originalDetectionIndex;
        private bool _disposed = false;

        public Track(in BBox bbox)
        {
            _kalmanFilter = new BTKalmanFilter();
            _rect = new BTRect(bbox.X, bbox.Y, bbox.Width, bbox.Height);
            _state = TrackState.New;
            _isActivated = false;
            _classId = bbox.ClassId;
            _score = bbox.Score;
            _trackId = 0;
            _frameId = 0;
            _startFrameId = 0;
            _trackletLength = 0;
            _originalDetectionIndex = -1;
        }

        public void Activate(int frameId, int trackId)
        {
            ThrowIfDisposed();

            _rect = _kalmanFilter.Initiate(_rect);

            _state = TrackState.Tracked;
            if (frameId == 1)
            {
                _isActivated = true;
            }
            _trackId = trackId;
            _frameId = frameId;
            _startFrameId = frameId;
            _trackletLength = 0;
        }

        public void ReActivate(Track newTrack, int frameId, int newTrackId = -1)
        {
            ThrowIfDisposed();

            _rect = _kalmanFilter.Update(newTrack._rect);

            _state = TrackState.Tracked;
            _isActivated = true;
            _score = newTrack.Score;
            if (newTrackId != -1)
            {
                _trackId = newTrackId;
            }
            _frameId = frameId;
            _trackletLength = 0;
        }

        public void Predict()
        {
            ThrowIfDisposed();

            _rect = _kalmanFilter.Predict(_state != TrackState.Tracked);
        }

        public void Update(Track newTrack, int frameId)
        {
            ThrowIfDisposed();

            _rect = _kalmanFilter.Update(newTrack._rect);

            _state = TrackState.Tracked;
            _isActivated = true;
            _score = newTrack.Score;
            _frameId = frameId;
            if (_trackletLength < int.MaxValue)
            {
                _trackletLength++;
            }
        }

        public void MarkAsLost()
        {
            ThrowIfDisposed();

            _state = TrackState.Lost;
        }

        public void MarkAsRemoved()
        {
            ThrowIfDisposed();

            _state = TrackState.Removed;
        }

        public void Reset(in BBox bbox)
        {
            ThrowIfDisposed();

            _kalmanFilter.Reset();
            _rect = new BTRect(bbox.X, bbox.Y, bbox.Width, bbox.Height);

            _state = TrackState.New;
            _isActivated = false;
            _classId = bbox.ClassId;
            _score = bbox.Score;
            _trackId = 0;
            _frameId = 0;
            _startFrameId = 0;
            _trackletLength = 0;
            _originalDetectionIndex = -1;
        }

        public void ResetFrameIds(int newStartFrameId, int newFrameId)
        {
            _startFrameId = newStartFrameId;
            _frameId = newFrameId;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ref readonly BTRect Rect => ref _rect;
        public int ClassId => _classId;
        public float Score => _score;
        public TrackState State => _state;
        public bool IsActivated => _isActivated;
        public int TrackId => _trackId;
        public int FrameId => _frameId;
        public int StartFrameId => _startFrameId;
        public int TrackletLength => _trackletLength;
        internal int OriginalDetectionIndex => _originalDetectionIndex;

        internal void SetOriginalDetectionIndex(int index)
        {
            ThrowIfDisposed();
            _originalDetectionIndex = index;
        }

        public override string ToString()
        {
            ThrowIfDisposed();

            return $"OT_{_trackId}_({_startFrameId}-{_frameId})";
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _kalmanFilter?.Dispose(); _kalmanFilter = null;
            }

            _disposed = true;
        }

        ~Track()
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

        internal static float CalcIoU(Track a, Track b)
        {
            ref readonly var rectA = ref a._rect;
            ref readonly var rectB = ref b._rect;

            float aX2 = rectA.X + rectA.Width;
            float bX2 = rectB.X + rectB.Width;
            float aY2 = rectA.Y + rectA.Height;
            float bY2 = rectB.Y + rectB.Height;

            float iw = Math.Min(aX2, bX2) - Math.Max(rectA.X, rectB.X) + 1;
            if (iw <= 0f)
                return 0f;

            float ih = Math.Min(aY2, bY2) - Math.Max(rectA.Y, rectB.Y) + 1;
            if (ih <= 0f)
                return 0f;

            float interArea = iw * ih;

            float areaA_w1 = rectA.Width + 1;
            float areaA_h1 = rectA.Height + 1;
            float areaA = areaA_w1 * areaA_h1;

            float areaB_w1 = rectB.Width + 1;
            float areaB_h1 = rectB.Height + 1;
            float areaB = areaB_w1 * areaB_h1;

            float unionArea = areaA + areaB - interArea;

            // Consider the possibility that unionArea may be 0 (avoid divide by zero)
            if (unionArea <= 0f) return 0f;

            return interArea / unionArea;
        }
    }
}
