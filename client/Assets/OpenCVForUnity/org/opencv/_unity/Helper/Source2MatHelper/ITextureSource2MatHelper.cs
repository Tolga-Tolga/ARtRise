using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Interface to provide conversion from texture source to Mat.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.ITextureSource2MatHelper class instead.")]
    public interface ITextureSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the source texture.
        /// </summary>
        Texture sourceTexture { get; set; }
    }
}
