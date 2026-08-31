using System.Collections.Generic;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    /// <summary>
    /// Class that manages a pool of Track objects
    /// </summary>
    internal class TrackPool
    {
        private readonly Stack<Track> _pool;
        private readonly int _maxPoolSize;

        // Statistics
        private long _totalGets;
        private long _totalReturns;
        private long _totalNewAllocations;
        private long _maxPoolSizeReached;

        /// <summary>
        /// Constructor for TrackPool
        /// </summary>
        /// <param name="maxPoolSize">Maximum size of the pool (0 for unlimited)</param>
        public TrackPool(int maxPoolSize = 0)
        {
            _pool = new Stack<Track>();
            _maxPoolSize = maxPoolSize;
        }

        /// <summary>
        /// Get a Track from the pool
        /// </summary>
        /// <param name="rect">Bounding box</param>
        /// <param name="score">Detection score</param>
        /// <param name="classId">Class ID</param>
        /// <returns>Track object</returns>
        public Track Get(in BBox bbox)
        {
            _totalGets++;

            Track track;
            if (_pool.Count > 0)
            {
                track = _pool.Pop();
                track.Reset(bbox);
            }
            else
            {
                _totalNewAllocations++;
                track = new Track(bbox);
            }

            return track;
        }

        /// <summary>
        /// Return a Track to the pool
        /// </summary>
        /// <param name="track">Track object to return</param>
        public void Return(Track track)
        {
            if (track == null)
                return;

            _totalReturns++;

            if (_maxPoolSize > 0 && _pool.Count >= _maxPoolSize)
            {
                _maxPoolSizeReached++;
                track.Dispose();
                return;
            }

            _pool.Push(track);
        }

        /// <summary>
        /// Get pool statistics as string
        /// </summary>
        public string GetStatsString()
        {
            return $"[TrackPool] Stats:\n" +
                   $"  Total Gets: {_totalGets}\n" +
                   $"  Total Returns: {_totalReturns}\n" +
                   $"  Total New Allocations: {_totalNewAllocations}\n" +
                   $"  Current Pool Size: {_pool.Count}\n" +
                   $"  Max Pool Size Reached: {_maxPoolSizeReached}\n" +
                   $"  Max Pool Size: {_maxPoolSize}";
        }

        /// <summary>
        /// Log pool statistics
        /// </summary>
        public void LogStats()
        {
            Debug.Log(GetStatsString());
        }

        /// <summary>
        /// Clear the pool
        /// </summary>
        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var track = _pool.Pop();
                track.Dispose();
            }
        }
    }
}
