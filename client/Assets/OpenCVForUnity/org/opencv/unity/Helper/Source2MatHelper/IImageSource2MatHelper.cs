namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to provide conversion from image source to Mat.
    /// </summary>
    public interface IImageSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the requested image file path.
        /// </summary>
        string RequestedImageFilePath { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat the image.
        /// </summary>
        bool Repeat { get; set; }
    }
}
