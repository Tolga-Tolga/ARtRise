namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// Interface to provide frame update rate functionality.
    /// </summary>
    /// <remarks>
    /// This interface provides functionality to control the update rate of Mat objects.
    /// Implementing classes must provide the ability to set the requested update rate and retrieve the current update rate.
    /// </remarks>
    public interface IMatUpdateFPSProvider
    {
        /// <summary>
        /// Gets or sets the requested frame update rate (FPS).
        /// </summary>
        /// <remarks>
        /// This property controls the update interval of the Mat object.
        /// </remarks>
        float RequestedMatUpdateFPS { get; set; }

        /// <summary>
        /// Gets the current frame update rate (FPS).
        /// </summary>
        /// <returns>The current FPS.</returns>
        float GetMatUpdateFPS();
    }
}
