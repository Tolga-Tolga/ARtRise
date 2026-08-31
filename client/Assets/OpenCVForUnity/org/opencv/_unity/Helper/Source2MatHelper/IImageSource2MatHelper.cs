using System;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Interface to provide conversion from image source to Mat.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.IImageSource2MatHelper class instead.")]
    public interface IImageSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the requested image file path.
        /// </summary>
        string requestedImageFilePath { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat the image.
        /// </summary>
        bool repeat { get; set; }
    }
}
