using System;
using OpenCVForUnity.CoreModule;
using UnityEngine.Events;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Base interface for converting various sources to Mat.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.ISource2MatHelper class instead.")]
    public interface ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the output color format.
        /// </summary>
        Source2MatHelperColorFormat outputColorFormat { get; set; }

        /// <summary>
        /// Gets or sets the timeout frame count.
        /// </summary>
        int timeoutFrameCount { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when initialization is complete.
        /// </summary>
        UnityEvent onInitialized { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when resources are disposed.
        /// </summary>
        UnityEvent onDisposed { get; set; }

        /// <summary>
        /// Gets or sets the event that is invoked when an error occurs.
        /// </summary>
        Source2MatHelperErrorUnityEvent onErrorOccurred { get; set; }

        /// <summary>
        /// Initializes the helper with the specified auto-play setting.
        /// </summary>
        /// <param name="autoPlay">Whether to automatically start playing</param>
        void Initialize(bool autoPlay);

        /// <summary>
        /// Gets whether the helper is initialized.
        /// </summary>
        /// <returns>True if initialized</returns>
        bool IsInitialized();

        /// <summary>
        /// Starts playing.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops playback.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets whether playback is active.
        /// </summary>
        /// <returns>True if playing</returns>
        bool IsPlaying();

        /// <summary>
        /// Gets whether playback is paused.
        /// </summary>
        /// <returns>True if paused</returns>
        bool IsPaused();

        /// <summary>
        /// Gets the device name.
        /// </summary>
        /// <returns>The device name</returns>
        string GetDeviceName();

        /// <summary>
        /// Gets the width of the frame.
        /// </summary>
        /// <returns>The width</returns>
        int GetWidth();

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        /// <returns>The height</returns>
        int GetHeight();

        /// <summary>
        /// Gets the base color format.
        /// </summary>
        /// <returns>The base color format</returns>
        Source2MatHelperColorFormat GetBaseColorFormat();

        /// <summary>
        /// Gets whether the frame was updated in the current frame.
        /// </summary>
        /// <returns>True if updated</returns>
        bool DidUpdateThisFrame();

        /// <summary>
        /// Gets the current Mat.
        /// </summary>
        /// <returns>The current Mat</returns>
        Mat GetMat();

        /// <summary>
        /// Disposes of resources.
        /// </summary>
        void Dispose();
    }
}
