#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using KeyPoint = OpenCVForUnity.UnityIntegration.Worker.DnnModule.MediaPipePoseEstimator.KeyPoint;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Referring to https://github.com/digital-standard/ThreeDPoseUnityBarracuda/blob/master/Assets/Scripts/VNectModel.cs
    /// </summary>
    public class MediaPipePoseSkeletonVisualizer : MonoBehaviour
    {
        public class Skeleton
        {
            public GameObject LineObject;
            public LineRenderer Line;
        }

        protected const int NUM_SKELETONS = 35;

        [SerializeField]
        protected Material _skeletonMaterial;
        public virtual Material SkeletonMaterial
        {
            get => _skeletonMaterial;
            set => _skeletonMaterial = value;
        }

        [SerializeField]
        protected float _skeletonX;
        public virtual float SkeletonX
        {
            get => _skeletonX;
            set => _skeletonX = value;
        }

        [SerializeField]
        protected float _skeletonY;
        public virtual float SkeletonY
        {
            get => _skeletonY;
            set => _skeletonY = value;
        }

        [SerializeField]
        protected float _skeletonZ;
        public virtual float SkeletonZ
        {
            get => _skeletonZ;
            set => _skeletonZ = value;
        }

        [SerializeField]
        protected float _skeletonScale = 1f;
        public virtual float SkeletonScale
        {
            get => _skeletonScale;
            set => _skeletonScale = value;
        }

        [SerializeField]
        protected bool _showSkeleton = true;
        public virtual bool ShowSkeleton
        {
            get => _showSkeleton;
            set
            {
                _showSkeleton = value;
                ClearLine();
            }
        }

        protected List<Skeleton> _skeletons = new List<Skeleton>();

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
        protected Vec3f[] _landmarksWorldBuffer;
#endif

        protected virtual void OnDestroy()
        {
            if (_skeletons != null)
            {
                foreach (var skeleton in _skeletons)
                {
                    if (skeleton.LineObject != null)
                    {
                        UnityEngine.Object.Destroy(skeleton.LineObject);
                    }
                }
                _skeletons.Clear();
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _landmarksWorldBuffer = null;
#endif
        }

#if NET_STANDARD_2_1
        /// <summary>
        /// Updates the pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">ReadOnlySpan of 33 world landmarks representing pose keypoints.</param>
        public virtual void UpdatePose(ReadOnlySpan<Vec3f> landmarksWorld)
#else
        /// <summary>
        /// Updates the pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">Array of 33 world landmarks representing pose keypoints.</param>
        public virtual void UpdatePose(Vec3f[] landmarksWorld)
#endif
        {
            if (landmarksWorld == null)
                throw new ArgumentNullException(nameof(landmarksWorld));
            if (landmarksWorld.Length < 33)
                throw new ArgumentException("Invalid landmarks_world array. It must have at least 33 elements.");

            if (_skeletons.Count == 0)
            {
                for (int i = 0; i < NUM_SKELETONS; ++i)
                {
                    AddSkeleton();
                }
            }

            SetLinePosition(0, (int)KeyPoint.Nose, (int)KeyPoint.LeftEyeInner, landmarksWorld);
            SetLinePosition(1, (int)KeyPoint.LeftEyeInner, (int)KeyPoint.LeftEye, landmarksWorld);
            SetLinePosition(2, (int)KeyPoint.LeftEye, (int)KeyPoint.LeftEyeOuter, landmarksWorld);
            SetLinePosition(3, (int)KeyPoint.LeftEyeOuter, (int)KeyPoint.LeftEar, landmarksWorld);
            SetLinePosition(4, (int)KeyPoint.Nose, (int)KeyPoint.RightEyeInner, landmarksWorld);
            SetLinePosition(5, (int)KeyPoint.RightEyeInner, (int)KeyPoint.RightEye, landmarksWorld);
            SetLinePosition(6, (int)KeyPoint.RightEye, (int)KeyPoint.RightEyeOuter, landmarksWorld);
            SetLinePosition(7, (int)KeyPoint.RightEyeOuter, (int)KeyPoint.RightEar, landmarksWorld);

            SetLinePosition(8, (int)KeyPoint.MouthLeft, (int)KeyPoint.MouthRight, landmarksWorld);

            SetLinePosition(9, (int)KeyPoint.RightShoulder, (int)KeyPoint.RightElbow, landmarksWorld);
            SetLinePosition(10, (int)KeyPoint.RightElbow, (int)KeyPoint.RightWrist, landmarksWorld);
            SetLinePosition(11, (int)KeyPoint.RightWrist, (int)KeyPoint.RightThumb, landmarksWorld);
            SetLinePosition(12, (int)KeyPoint.RightWrist, (int)KeyPoint.RightPinky, landmarksWorld);
            SetLinePosition(13, (int)KeyPoint.RightWrist, (int)KeyPoint.RightIndex, landmarksWorld);
            SetLinePosition(14, (int)KeyPoint.RightPinky, (int)KeyPoint.RightIndex, landmarksWorld);

            SetLinePosition(15, (int)KeyPoint.LeftShoulder, (int)KeyPoint.LeftElbow, landmarksWorld);
            SetLinePosition(16, (int)KeyPoint.LeftElbow, (int)KeyPoint.LeftWrist, landmarksWorld);
            SetLinePosition(17, (int)KeyPoint.LeftWrist, (int)KeyPoint.LeftThumb, landmarksWorld);
            SetLinePosition(18, (int)KeyPoint.LeftWrist, (int)KeyPoint.LeftIndex, landmarksWorld);
            SetLinePosition(19, (int)KeyPoint.LeftWrist, (int)KeyPoint.LeftPinky, landmarksWorld);
            SetLinePosition(20, (int)KeyPoint.LeftPinky, (int)KeyPoint.LeftIndex, landmarksWorld);

            SetLinePosition(21, (int)KeyPoint.LeftShoulder, (int)KeyPoint.RightShoulder, landmarksWorld);
            SetLinePosition(22, (int)KeyPoint.LeftShoulder, (int)KeyPoint.LeftHip, landmarksWorld);
            SetLinePosition(23, (int)KeyPoint.LeftHip, (int)KeyPoint.RightHip, landmarksWorld);
            SetLinePosition(24, (int)KeyPoint.RightHip, (int)KeyPoint.RightShoulder, landmarksWorld);

            SetLinePosition(25, (int)KeyPoint.RightHip, (int)KeyPoint.RightKnee, landmarksWorld);
            SetLinePosition(26, (int)KeyPoint.RightKnee, (int)KeyPoint.RightAnkle, landmarksWorld);
            SetLinePosition(27, (int)KeyPoint.RightAnkle, (int)KeyPoint.RightHeel, landmarksWorld);
            SetLinePosition(28, (int)KeyPoint.RightAnkle, (int)KeyPoint.RightFootIndex, landmarksWorld);
            SetLinePosition(29, (int)KeyPoint.RightHeel, (int)KeyPoint.RightFootIndex, landmarksWorld);

            SetLinePosition(30, (int)KeyPoint.LeftHip, (int)KeyPoint.LeftKnee, landmarksWorld);
            SetLinePosition(31, (int)KeyPoint.LeftKnee, (int)KeyPoint.LeftAnkle, landmarksWorld);
            SetLinePosition(32, (int)KeyPoint.LeftAnkle, (int)KeyPoint.LeftFootIndex, landmarksWorld);
            SetLinePosition(33, (int)KeyPoint.LeftAnkle, (int)KeyPoint.LeftHeel, landmarksWorld);
            SetLinePosition(34, (int)KeyPoint.LeftHeel, (int)KeyPoint.LeftFootIndex, landmarksWorld);
        }

#if NET_STANDARD_2_1
        /// <summary>
        /// Updates the pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">Array of 33 world landmarks representing pose keypoints.</param>
        public virtual void UpdatePose(Vec3f[] landmarksWorld)
        {
            if (landmarksWorld == null)
                throw new ArgumentNullException(nameof(landmarksWorld));

            UpdatePose(landmarksWorld.AsSpan());
        }
#endif

        /// <summary>
        /// Updates the pose skeleton visualization with the provided pose estimation results.
        /// </summary>
        /// <param name="result">Pose estimation results matrix from MediaPipePoseEstimator.Infer method.</param>
        public virtual void UpdatePose(Mat result)
        {
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.rows() < 317)
                throw new ArgumentException("Invalid results matrix. It must have at least 317 rows.");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<Vec3f> landmarksWorld = result.AsSpanRowRange<Vec3f>(199, 199 + 99);
            UpdatePose(landmarksWorld);
#else

            if (_landmarksWorldBuffer == null)
                _landmarksWorldBuffer = new Vec3f[33];

            // Copy only world landmarks data from pose data.
            OpenCVMatUtils.CopyFromMat<Vec3f>(result.rowRange(199, 199 + 99), _landmarksWorldBuffer);

            UpdatePose(_landmarksWorldBuffer);
#endif
        }

#if NET_STANDARD_2_1
        protected virtual void SetLinePosition(int index, int idx1, int idx2, ReadOnlySpan<Vec3f> landmarksWorld)
#else
        protected virtual void SetLinePosition(int index, int idx1, int idx2, Vec3f[] landmarksWorld)
#endif
        {
            // Add scaling and shifting to world landmark coordinates, and convert from a right-handed coordinate system to a left-handed coordinate system (Unity).
            ref readonly var landmark1 = ref landmarksWorld[idx1];
            _skeletons[index].Line.SetPosition(0, new Vector3(
                landmark1.Item1 * SkeletonScale * 1 + SkeletonX,
                landmark1.Item2 * SkeletonScale * -1 + SkeletonY,
                landmark1.Item3 * SkeletonScale * 1 + SkeletonZ
                ));
            ref readonly var landmark2 = ref landmarksWorld[idx2];
            _skeletons[index].Line.SetPosition(1, new Vector3(
                landmark2.Item1 * SkeletonScale * 1 + SkeletonX,
                landmark2.Item2 * SkeletonScale * -1 + SkeletonY,
                landmark2.Item3 * SkeletonScale * 1 + SkeletonZ
                ));
        }

        protected virtual void AddSkeleton()
        {
            var lineObject = new GameObject("Line");
            lineObject.transform.parent = gameObject.transform;

            lineObject.layer = gameObject.layer;

            var sk = new Skeleton()
            {
                LineObject = lineObject
            };

            sk.Line = sk.LineObject.AddComponent<LineRenderer>();
            sk.Line.startWidth = 0.032f * SkeletonScale;
            sk.Line.endWidth = 0.008f * SkeletonScale;

            // define the number of vertex
            sk.Line.positionCount = 2;
            sk.Line.material = SkeletonMaterial;

            _skeletons.Add(sk);
        }

        protected virtual void ClearLine()
        {
            if (_skeletons.Count != NUM_SKELETONS)
                return;

            for (int i = 0; i < NUM_SKELETONS; ++i)
            {
                var skeleton = _skeletons[i];
                skeleton.Line.positionCount = 0;
                skeleton.Line.positionCount = 2;
            }
        }
    }
}
#endif
