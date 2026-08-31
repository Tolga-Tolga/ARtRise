#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using KeyPoint = OpenCVForUnity.UnityIntegration.Worker.DnnModule.MediaPipeHandPoseEstimator.KeyPoint;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Referring to https://github.com/digital-standard/ThreeDPoseUnityBarracuda/blob/master/Assets/Scripts/VNectModel.cs
    /// </summary>
    public class MediaPipeHandPoseSkeletonVisualizer : MonoBehaviour
    {
        public class Skeleton
        {
            public GameObject LineObject;
            public LineRenderer Line;
        }

        protected const int NUM_SKELETONS = 20;


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
        /// Updates the hand pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">ReadOnlySpan of 21 world landmarks representing hand keypoints.</param>
        public virtual void UpdatePose(ReadOnlySpan<Vec3f> landmarksWorld)
#else
        /// <summary>
        /// Updates the hand pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">Array of 21 world landmarks representing hand keypoints.</param>
        public virtual void UpdatePose(Vec3f[] landmarksWorld)
#endif
        {
            if (landmarksWorld == null)
                throw new ArgumentNullException(nameof(landmarksWorld));
            if (landmarksWorld.Length < 21)
                throw new ArgumentException("Invalid landmarksWorld array. It must have at least 21 elements.");

            if (_skeletons.Count == 0)
            {
                for (int i = 0; i < NUM_SKELETONS; ++i)
                {
                    AddSkeleton();
                }
            }

            SetLinePosition(0, (int)KeyPoint.Wrist, (int)KeyPoint.Thumb1, landmarksWorld);
            SetLinePosition(1, (int)KeyPoint.Thumb1, (int)KeyPoint.Thumb2, landmarksWorld);
            SetLinePosition(2, (int)KeyPoint.Thumb2, (int)KeyPoint.Thumb3, landmarksWorld);
            SetLinePosition(3, (int)KeyPoint.Thumb3, (int)KeyPoint.Thumb4, landmarksWorld);

            SetLinePosition(4, (int)KeyPoint.Wrist, (int)KeyPoint.Index1, landmarksWorld);
            SetLinePosition(5, (int)KeyPoint.Index1, (int)KeyPoint.Index2, landmarksWorld);
            SetLinePosition(6, (int)KeyPoint.Index2, (int)KeyPoint.Index3, landmarksWorld);
            SetLinePosition(7, (int)KeyPoint.Index3, (int)KeyPoint.Index4, landmarksWorld);

            SetLinePosition(8, (int)KeyPoint.Wrist, (int)KeyPoint.Middle1, landmarksWorld);
            SetLinePosition(9, (int)KeyPoint.Middle1, (int)KeyPoint.Middle2, landmarksWorld);
            SetLinePosition(10, (int)KeyPoint.Middle2, (int)KeyPoint.Middle3, landmarksWorld);
            SetLinePosition(11, (int)KeyPoint.Middle3, (int)KeyPoint.Middle4, landmarksWorld);

            SetLinePosition(12, (int)KeyPoint.Wrist, (int)KeyPoint.Ring1, landmarksWorld);
            SetLinePosition(13, (int)KeyPoint.Ring1, (int)KeyPoint.Ring2, landmarksWorld);
            SetLinePosition(14, (int)KeyPoint.Ring2, (int)KeyPoint.Ring3, landmarksWorld);
            SetLinePosition(15, (int)KeyPoint.Ring3, (int)KeyPoint.Ring4, landmarksWorld);

            SetLinePosition(16, (int)KeyPoint.Wrist, (int)KeyPoint.Pinky1, landmarksWorld);
            SetLinePosition(17, (int)KeyPoint.Pinky1, (int)KeyPoint.Pinky2, landmarksWorld);
            SetLinePosition(18, (int)KeyPoint.Pinky2, (int)KeyPoint.Pinky3, landmarksWorld);
            SetLinePosition(19, (int)KeyPoint.Pinky3, (int)KeyPoint.Pinky4, landmarksWorld);
        }

#if NET_STANDARD_2_1
        /// <summary>
        /// Updates the hand pose skeleton visualization with the provided world landmarks.
        /// </summary>
        /// <param name="landmarksWorld">Array of 21 world landmarks representing hand keypoints.</param>
        public virtual void UpdatePose(Vec3f[] landmarksWorld)
        {
            if (landmarksWorld == null)
                throw new ArgumentNullException(nameof(landmarksWorld));

            UpdatePose(landmarksWorld.AsSpan());
        }
#endif

        /// <summary>
        /// Updates the hand pose skeleton visualization with the provided result matrix.
        /// </summary>
        /// <param name="result">Hand pose estimation results matrix from MediaPipeHandPoseEstimator.Infer method.</param>
        public virtual void UpdatePose(Mat result)
        {
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.rows() < 132)
                throw new ArgumentException("Invalid results matrix. It must have at least 132 rows.");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<Vec3f> landmarksWorld = result.AsSpanRowRange<Vec3f>(67, 67 + 63);
            UpdatePose(landmarksWorld);
#else
            if (_landmarksWorldBuffer == null)
                _landmarksWorldBuffer = new Vec3f[21];

            // Copy only world landmarks data from pose data.
            OpenCVMatUtils.CopyFromMat<Vec3f>(result.rowRange(67, 67 + 63), _landmarksWorldBuffer);

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
            sk.Line.startWidth = 0.004f * SkeletonScale;
            sk.Line.endWidth = 0.001f * SkeletonScale;

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
