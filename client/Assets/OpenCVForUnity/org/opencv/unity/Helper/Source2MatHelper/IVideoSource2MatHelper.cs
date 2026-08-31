namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to provide conversion from video source to Mat.
    /// </summary>
    public interface IVideoSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the requested video file path.
        /// </summary>
        string RequestedVideoFilePath { get; set; }

        /// <summary>
        /// Gets or sets whether to loop the video.
        /// </summary>
        bool Loop { get; set; }

        /// <summary>
        /// Gets the current frame rate (FPS).
        /// </summary>
        /// <returns>The current FPS</returns>
        float GetFPS();

        /// <summary>
        /// Gets the current frame position ratio.
        /// </summary>
        /// <returns>The frame position ratio (0.0 to 1.0)</returns>
        float GetFramePosRatio();

        /// <summary>
        /// Sets the frame position ratio.
        /// </summary>
        /// <param name="ratio">The frame position ratio (0.0 to 1.0)</param>
        void SetFramePosRatio(float ratio);

        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        /// <returns>The frame index</returns>
        int GetFrameIndex();

        /// <summary>
        /// Sets the frame index.
        /// </summary>
        /// <param name="index">The frame index</param>
        void SetFrameIndex(int index);

        /// <summary>
        /// Gets the total frame count.
        /// </summary>
        /// <returns>The total frame count</returns>
        int GetFrameCount();
    }
}
