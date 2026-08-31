using System;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct BTRect
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;

        public const int ELEMENT_COUNT = 4;
        public const int DATA_SIZE = ELEMENT_COUNT * 4;

        public BTRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public readonly override string ToString()
        {
            return $"BTRect(X:{X} Y:{Y} Width:{Width} Height:{Height})";
        }
    }
}
