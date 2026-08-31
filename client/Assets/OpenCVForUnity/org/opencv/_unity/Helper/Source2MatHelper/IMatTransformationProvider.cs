using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Interface to define rotation and flip operations for image processing.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.IMatTransformationProvider class instead.")]
    public interface IMatTransformationProvider
    {
        /// <summary>
        /// Gets or sets whether to rotate the image 90 degrees clockwise.
        /// </summary>
        bool rotate90Degree { get; set; }

        /// <summary>
        /// Gets or sets whether to flip the image vertically.
        /// </summary>
        bool flipVertical { get; set; }

        /// <summary>
        /// Gets or sets whether to flip the image horizontally.
        /// </summary>
        bool flipHorizontal { get; set; }
    }
}
