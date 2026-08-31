using System;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    public enum TrackState : int
    {
        New = 0,
        Tracked = 1,
        Lost = 2,
        Removed = 3
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct BYTETrackInfo
    {
        public readonly BBox BBox;
        public readonly int TrackId;
        public readonly TrackState State;
        public readonly int TrackletLength;

        public const int ELEMENT_COUNT = 9;
        public const int DATA_SIZE = ELEMENT_COUNT * 4;

        internal BYTETrackInfo(Track track)
        {
            State = track.State;
            TrackId = track.TrackId;
            TrackletLength = track.TrackletLength;

            ref readonly var rect = ref track.Rect;
            BBox = new BBox(
                rect.X,
                rect.Y,
                rect.Width,
                rect.Height,
                track.Score,
                track.ClassId
            );
        }

        public BYTETrackInfo(TrackState state, int trackId, int trackletLength, BBox bbox)
        {
            State = state;
            TrackId = trackId;
            TrackletLength = trackletLength;
            BBox = bbox;
        }

        public readonly override string ToString()
        {
            return $"BYTETrackInfo(State:{State} TrackId:{TrackId} TrackletLength:{TrackletLength} {BBox.ToString()})";
        }
    }
}
