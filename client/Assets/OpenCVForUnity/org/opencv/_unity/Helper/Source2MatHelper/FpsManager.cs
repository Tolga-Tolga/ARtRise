using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.FpsManager class instead.")]
    public class FpsManager
    {
        private readonly float interval; // Interval based on FPS (in seconds)
        private float timer = 0f;        // Internal timer
        private bool isPaused = false;  // Flag to indicate pause state

        /// <summary>
        /// Gets or sets whether the timer is paused.
        /// </summary>
        public bool IsPaused
        {
            get => isPaused;
            set
            {
                if (isPaused != value)
                {
                    isPaused = value;

                    // Reset the timer when resuming from pause (optional, remove if unnecessary)
                    if (!isPaused)
                    {
                        timer = 0f;
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
            interval = targetFPS > 0 ? 1f / targetFPS : 0f;
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void Reset()
        {
            timer = 0f;
        }

        /// <summary>
        /// Determines whether to execute certain processing at the FPS interval, and executes it if necessary.
        /// </summary>
        /// <param name="deltaTime">Elapsed time (usually pass Time.deltaTime).</param>
        /// <param name="onTick">Callback invoked when the interval is reached.</param>
        /// <returns>Returns true if the callback is invoked, otherwise false.</returns>
        public bool Update(float deltaTime, Action onTick)
        {
            if (isPaused) return false; // Skip update when paused

            if (interval == 0f)
            {
                // Execute every frame
                onTick?.Invoke();
                return true;
            }
            else
            {
                // Update the timer
                timer += deltaTime;

                if (timer >= interval)
                {
                    timer -= interval;
                    onTick?.Invoke();
                    return true;
                }
            }

            return false; // Callback was not invoked
        }
    }
}
