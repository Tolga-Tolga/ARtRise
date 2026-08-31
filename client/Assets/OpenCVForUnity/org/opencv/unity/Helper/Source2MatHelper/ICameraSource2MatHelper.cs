using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to provide conversion from camera source to Mat.
    /// </summary>
    public interface ICameraSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the requested device name.
        /// </summary>
        string RequestedDeviceName { get; set; }

        /// <summary>
        /// Gets or sets the requested width.
        /// </summary>
        int RequestedWidth { get; set; }

        /// <summary>
        /// Gets or sets the requested height.
        /// </summary>
        int RequestedHeight { get; set; }

        /// <summary>
        /// Gets or sets whether to use the front-facing camera.
        /// </summary>
        bool RequestedIsFrontFacing { get; set; }

        /// <summary>
        /// Gets or sets the requested frame rate (FPS).
        /// </summary>
        float RequestedFPS { get; set; }

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
