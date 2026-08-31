using System;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration.MOT
{
    public interface ITrackInfoVisualizer<T> : IDisposable where T : struct
    {
#if NET_STANDARD_2_1
        void Visualize(Mat image, ReadOnlySpan<T> trackInfos, bool printResult = false, bool isRGB = false);
#endif
        void Visualize(Mat image, T[] trackInfos, bool printResult = false, bool isRGB = false);
    }
}
