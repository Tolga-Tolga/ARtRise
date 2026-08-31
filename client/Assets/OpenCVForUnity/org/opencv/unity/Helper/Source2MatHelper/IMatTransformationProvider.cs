namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to define rotation and flip operations for image processing.
    /// </summary>
    public interface IMatTransformationProvider
    {
        /// <summary>
        /// Gets or sets whether to rotate the image 90 degrees clockwise.
        /// </summary>
        bool Rotate90Degree { get; set; }

        /// <summary>
        /// Gets or sets whether to flip the image vertically.
        /// </summary>
        bool FlipVertical { get; set; }

        /// <summary>
        /// Gets or sets whether to flip the image horizontally.
        /// </summary>
        bool FlipHorizontal { get; set; }
    }
}
