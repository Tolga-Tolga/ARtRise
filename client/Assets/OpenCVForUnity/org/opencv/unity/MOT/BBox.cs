using System;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration.MOT
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct BBox
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;
        public readonly float Score;
        public readonly int ClassId;

        public const int ELEMENT_COUNT = 6;
        public const int DATA_SIZE = ELEMENT_COUNT * 4;

        public BBox(float x, float y, float width, float height, float score, int classId = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Score = score;
            ClassId = classId;
        }

        public BBox(UnityEngine.Rect rect, float score, int classId = 0)
        {
            X = rect.x;
            Y = rect.y;
            Width = rect.width;
            Height = rect.height;
            Score = score;
            ClassId = classId;
        }

        public BBox(OpenCVForUnity.CoreModule.Rect rect, float score, int classId = 0)
        {
            X = rect.x;
            Y = rect.y;
            Width = rect.width;
            Height = rect.height;
            Score = score;
            ClassId = classId;
        }

        public BBox(OpenCVForUnity.CoreModule.Rect2d rect, float score, int classId = 0)
        {
            X = (float)rect.x;
            Y = (float)rect.y;
            Width = (float)rect.width;
            Height = (float)rect.height;
            Score = score;
            ClassId = classId;
        }

        public BBox(in Worker.DataStruct.ObjectDetectionData data)
        {
            X = data.X1;
            Y = data.Y1;
            Width = data.X2 - data.X1;
            Height = data.Y2 - data.Y1;
            Score = data.Confidence;
            ClassId = data.ClassId;
        }

        public readonly override string ToString()
        {
            return $"BBox(X:{X} Y:{Y} Width:{Width} Height:{Height} Score:{Score} ClassId:{ClassId})";
        }
    }
}
