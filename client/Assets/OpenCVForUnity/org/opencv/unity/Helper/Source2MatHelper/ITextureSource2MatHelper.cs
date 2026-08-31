using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to provide conversion from texture source to Mat.
    /// </summary>
    public interface ITextureSource2MatHelper : ISource2MatHelper
    {
        /// <summary>
        /// Gets or sets the source texture.
        /// </summary>
        Texture SourceTexture { get; set; }
    }
}
