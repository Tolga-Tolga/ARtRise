using System;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    public class FpsManager
    {
        private readonly float _interval; // Interval based on FPS (in seconds)
        private float _timer = 0f;        // Internal timer
        private bool _isPaused = false;  // Flag to indicate pause state

        /// <summary>
        /// Gets or sets whether the timer is paused.
        /// </summary>
        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                if (_isPaused != value)
                {
                    _isPaused = value;

                    // Reset the timer when resuming from pause (optional, remove if unnecessary)
                    if (!_isPaused)
                    {
                        _timer = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a timer to manage timing at the specified FPS.
        /// </summary>
        /// <param name="targetFPS">Target FPS. If 0 or less is specified, the update will occur every frame.</param>
        public FpsManager(float targetFPS)
        {
            Debug.Log("targetFPS " + targetFPS);
            _interval = targetFPS > 0 ? 1f / targetFPS : 0f;
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void Reset()
        {
            _timer = 0f;
        }

        /// <summary>
        /// Determines whether to execute certain processing at the FPS interval, and executes it if necessary.
        /// </summary>
        /// <param name="deltaTime">Elapsed time (usually pass Time.deltaTime).</param>
        /// <param name="onTick">Callback invoked when the interval is reached.</param>
        /// <returns>Returns true if the callback is invoked, otherwise false.</returns>
        public bool Update(float deltaTime, Action onTick)
        {
            if (_isPaused) return false; // Skip update when paused

            if (_interval == 0f)
            {
                // Execute every frame
                onTick?.Invoke();
                return true;
            }
            else
            {
                // Update the timer
                _timer += deltaTime;

                if (_timer >= _interval)
                {
                    _timer -= _interval;
                    onTick?.Invoke();
                    return true;
                }
            }

            return false; // Callback was not invoked
        }
    }
}
