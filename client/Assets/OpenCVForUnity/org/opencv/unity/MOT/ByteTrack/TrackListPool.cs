using System.Collections.Generic;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    /// <summary>
    /// Class for managing a pool of Track object lists
    /// </summary>
    internal class TrackListPool
    {
        private readonly Stack<List<Track>> _pool;
        private readonly int _maxPoolSize;
        private readonly int _initialCapacity;

        // Statistics
        private long _totalGets;
        private long _totalReturns;
        private long _totalNewAllocations;
        private long _maxPoolSizeReached;

        /// <summary>
        /// Constructor for TrackListPool
        /// </summary>
        /// <param name="maxPoolSize">Maximum size of the pool (0 for unlimited)</param>
        /// <param name="initialCapacity">Initial capacity for newly created lists</param>
        public TrackListPool(int maxPoolSize = 0, int initialCapacity = 16)
        {
            _pool = new Stack<List<Track>>();
            _maxPoolSize = maxPoolSize;
            _initialCapacity = initialCapacity;
        }

        /// <summary>
        /// Get a list from the pool
        /// </summary>
        /// <returns>Available List<Track></returns>
        public List<Track> Get()
        {
            _totalGets++;

            if (_pool.Count > 0)
            {
                var list = _pool.Pop();
                list.Clear();
                return list;
            }

            _totalNewAllocations++;
            return new List<Track>(_initialCapacity);
        }

        /// <summary>
        /// Return a list to the pool
        /// </summary>
        /// <param name="list">List<Track> to return</param>
        public void Return(List<Track> list)
        {
            if (list == null)
                return;

            _totalReturns++;

            list.Clear();

            if (_maxPoolSize > 0 && _pool.Count >= _maxPoolSize)
            {
                _maxPoolSizeReached++;
                return;
            }

            _pool.Push(list);
        }

        /// <summary>
        /// Get pool statistics as string
        /// </summary>
        public string GetStatsString()
        {
            return $"[TrackListPool] Stats:\n" +
                   $"  Total Gets: {_totalGets}\n" +
                   $"  Total Returns: {_totalReturns}\n" +
                   $"  Total New Allocations: {_totalNewAllocations}\n" +
                   $"  Current Pool Size: {_pool.Count}\n" +
                   $"  Max Pool Size Reached: {_maxPoolSizeReached}\n" +
                   $"  Max Pool Size: {_maxPoolSize}\n" +
                   $"  Initial Capacity: {_initialCapacity}";
        }

        /// <summary>
        /// Output pool statistics to log
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
            _pool.Clear();
        }
    }
}
