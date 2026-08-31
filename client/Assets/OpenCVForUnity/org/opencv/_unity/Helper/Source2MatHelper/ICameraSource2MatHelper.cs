using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Interface to provide conversion from camera source to Mat.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.ICameraSource2MatHelper class instead.")]
    public interface ICameraSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the requested device name.
        /// </summary>
        string requestedDeviceName { get; set; }

        /// <summary>
        /// Gets or sets the requested width.
        /// </summary>
        int requestedWidth { get; set; }

        /// <summary>
        /// Gets or sets the requested height.
        /// </summary>
        int requestedHeight { get; set; }

        /// <summary>
        /// Gets or sets whether to use the front-facing camera.
        /// </summary>
        bool requestedIsFrontFacing { get; set; }

        /// <summary>
        /// Gets or sets the requested frame rate (FPS).
        /// </summary>
        float requestedFPS { get; set; }

        /// <summary>
        /// Gets whether the front-facing camera is being used.
        /// </summary>
        /// <returns>True if using the front-facing camera</returns>
        bool IsFrontFacing();

        /// <summary>
        /// Gets the current frame rate (FPS).
        /// </summary>
        /// <returns>The current FPS</returns>
        float GetFPS();

        /// <summary>
        /// Gets the camera to world space transformation matrix.
        /// </summary>
        /// <returns>The camera to world space transformation matrix</returns>
        Matrix4x4 GetCameraToWorldMatrix();

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>The camera projection matrix</returns>
        Matrix4x4 GetProjectionMatrix();
    }
}
