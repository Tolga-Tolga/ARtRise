using System;

namespace OpenCVForUnity.UnityIntegration.MOT
{
    public interface IMultiObjectTracker<T> : IDisposable where T : struct
    {
#if NET_STANDARD_2_1
        void Update(ReadOnlySpan<BBox> detections);
#endif
        void Update(BBox[] detections);
        void Reset();
        T[] GetActiveTrackInfos();
        TrackInfo[] GetActiveBasicTrackInfos();
        void LogTrackingStatus();
        void LogTrackingStatusDetail();
    }
}
