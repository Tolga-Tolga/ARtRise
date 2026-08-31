using System;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration.MOT
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct TrackInfo
    {
        public readonly BBox BBox;
        public readonly int TrackId;

        public const int ELEMENT_COUNT = 7;
        public const int DATA_SIZE = ELEMENT_COUNT * 4;

        public TrackInfo(int trackId, BBox bbox)
        {
            TrackId = trackId;
            BBox = bbox;
        }

        public readonly override string ToString()
        {
            return $"TrackInfo(TrackId:{TrackId} {BBox.ToString()})";
        }
    }
}
