using System;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration.Worker.DataStruct
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct FaceDetection5LandmarkData
    {
        // Bounding box
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;

        // Key points
        public readonly Vec2f RightEye;
        public readonly Vec2f LeftEye;
        public readonly Vec2f Nose;
        public readonly Vec2f RightMouth;
        public readonly Vec2f LeftMouth;

        // Confidence score [0, 1]
        public readonly float Score;

        public const int LANDMARK_VEC2F_COUNT = 5;
        public const int LANDMARK_ELEMENT_COUNT = 2 * LANDMARK_VEC2F_COUNT;
        public const int ELEMENT_COUNT = 4 + LANDMARK_ELEMENT_COUNT + 1;
        public const int DATA_SIZE = ELEMENT_COUNT * 4;

        public FaceDetection5LandmarkData(float x, float y, float width, float height, Vec2f rightEye, Vec2f leftEye, Vec2f nose, Vec2f rightMouth, Vec2f leftMouth, float score)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RightEye = rightEye;
            LeftEye = leftEye;
            Nose = nose;
            RightMouth = rightMouth;
            LeftMouth = leftMouth;
            Score = score;
        }

        public readonly override string ToString()
        {
            return $"FaceDetection5LandmarkData(X:{X} Y:{Y} Width:{Width} Height:{Height} RightEye:{RightEye.ToString()} LeftEye:{LeftEye.ToString()} Nose:{Nose.ToString()} RightMouth:{RightMouth.ToString()} LeftMouth:{LeftMouth.ToString()} Score:{Score})";
        }
    }
}
