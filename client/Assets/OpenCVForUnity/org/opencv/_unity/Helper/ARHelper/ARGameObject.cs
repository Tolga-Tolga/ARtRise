using System;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using OpenCVForUnity.UnityUtils.Helper.Editor;
#endif

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// Represents an augmented reality (AR) object that integrates with the ARCamera.
    /// This class manages solving Perspective-n-Point (PnP) problems, handling AR matrix transformations,
    /// and detecting whether the object is within the AR camera's viewport.
    /// </summary>
    /// <remarks>
    /// - Utilizes OpenCV's solvePnP methods to estimate the object's 3D position from 2D image points.
    /// - Supports multiple solvePnP flags and an optional refinement process using solvePnPRefineLM.
    /// - Provides options for coordinate system transformations and noise reduction via a low-pass filter.
    /// - Triggers events when the object enters or exits the AR camera viewport.
    /// - Updates the object's transform based on computed AR matrices.
    /// </remarks>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.AR.ARGameObject class instead.")]
    public class ARGameObject : MonoBehaviour
    {
        /// <summary>
        /// Enum to set flags for solvePnP
        /// </summary>
        public enum Calib3dSolvePnPFlagsMode
        {
            SOLVEPNP_ITERATIVE = Calib3d.SOLVEPNP_ITERATIVE,
            SOLVEPNP_EPNP = Calib3d.SOLVEPNP_EPNP,
            SOLVEPNP_P3P = Calib3d.SOLVEPNP_P3P,
            SOLVEPNP_AP3P = Calib3d.SOLVEPNP_AP3P,
            SOLVEPNP_DLS = Calib3d.SOLVEPNP_DLS,
            SOLVEPNP_UPNP = Calib3d.SOLVEPNP_UPNP,
            SOLVEPNP_IPPE = Calib3d.SOLVEPNP_IPPE,
            SOLVEPNP_IPPE_SQUARE = Calib3d.SOLVEPNP_IPPE_SQUARE,
            SOLVEPNP_SQPNP = Calib3d.SOLVEPNP_SQPNP,

            /// <summary>
            /// Iterative method is called first, then EPNP is called.
            /// </summary>
            SOLVEPNP_ITERATIVE_THEN_EPNP
        }

        [Header("SolvePnP Settings")]

        [Header("2D Points")]

        [SerializeField, Tooltip("Specify the imagePoints argument to the Calib3d.solvePnP() method.")]
        protected Vector2[] _imagePoints = null;

        /// <summary>
        /// Specify the imagePoints argument to the Calib3d.solvePnP() method.
        /// </summary>
        public virtual Vector2[] ImagePoints
        {
            get { return _imagePoints; }
            set
            {
                _imagePoints = value;

                _changedSolvePnpPoints = true;
                //Debug.Log("changedSolvePnpPoints " + changedSolvePnpPoints);
            }
        }

        [Header("3D Points")]

        [SerializeField, Tooltip("Enable this flag if the object point is a left-hand coordinate system (Unity).")]
        protected bool _leftHandedCoordinates = false;

        /// <summary>
        /// Enable this flag if the object point is a left-hand coordinate system (Unity).
        /// </summary>
        public virtual bool LeftHandedCoordinates
        {
            get => _leftHandedCoordinates;
            set => _leftHandedCoordinates = value;
        }

        [SerializeField, Tooltip("Specify the objectPoints argument to the Calib3d.solvePnP() method.")]
        protected Vector3[] _objectPoints = null;

        /// <summary>
        /// Specify the objectPoints argument to the Calib3d.solvePnP() method.
        /// </summary>
        public virtual Vector3[] ObjectPoints
        {
            get { return _objectPoints; }
            set
            {
                _objectPoints = value;

                _changedSolvePnpPoints = true;
                //Debug.Log("changedSolvePnpPoints " + changedSolvePnpPoints);
            }
        }

        [Space(10)]

        [SerializeField, Tooltip("Specify the flags argument to the Calib3d.solvePnP() method.")]
        protected Calib3dSolvePnPFlagsMode _solvePnPFlagsMode = Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE_THEN_EPNP;

        /// <summary>
        /// Specify the flags argument to the Calib3d.solvePnP() method.
        /// </summary>
        public virtual Calib3dSolvePnPFlagsMode SolvePnPFlagsMode
        {
            get => _solvePnPFlagsMode;
            set => _solvePnPFlagsMode = value;
        }

        [SerializeField, Tooltip("If this flag is set to true, the Calib3d.solvePnPRefineLM() method is called after the Calib3d.solvePnPnP() method to optimize the results.")]
        protected bool _useSolvePnPRefineLM = false;

        /// <summary>
        /// If this flag is set to true, the Calib3d.solvePnPRefineLM() method is called after the Calib3d.solvePnPnP() method to optimize the results.
        /// </summary>
        public virtual bool UseSolvePnPRefineLM
        {
            get => _useSolvePnPRefineLM;
            set => _useSolvePnPRefineLM = value;
        }

        [SerializeField, Tooltip("If useSolvePnPRefineLM is true, the Calib3d.solvePnPRefineLM() method is called if the difference between the tvec of the current frame and that of the previous frame is greater than the specified threshold.")]
        [Range(0.0f, 1000.0f)]
        protected float _solvePnPRefineLMMovementThreshold = 100;

        /// <summary>
        /// If useSolvePnPRefineLM is true, the Calib3d.solvePnPRefineLM() method is called if the difference between the tvec of the current frame and that of the previous frame is greater than the specified threshold.
        /// </summary>
        public virtual float SolvePnPRefineLMMovementThreshold
        {
            get => _solvePnPRefineLMMovementThreshold;
            set => _solvePnPRefineLMMovementThreshold = value;
        }

        [Header("NDC Range Settings")]

        [SerializeField, Tooltip("Defines the valid range for the X coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.")]
#if UNITY_EDITOR
        [LabeledVector2("Min", "Max")]
#endif
        protected Vector2 _ndcXRange = new Vector2(-1.0f, 1.0f);

        /// <summary>
        /// Defines the valid range for the X coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.
        /// </summary>
        public virtual Vector2 NdcXRange
        {
            get => _ndcXRange;
            set => _ndcXRange = value;
        }

        [SerializeField, Tooltip("Defines the valid range for the Y coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.")]
#if UNITY_EDITOR
        [LabeledVector2("Min", "Max")]
#endif
        protected Vector2 _ndcYRange = new Vector2(-1.0f, 1.0f);

        /// <summary>
        /// Defines the valid range for the Y coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.
        /// </summary>
        public virtual Vector2 NdcYRange
        {
            get => _ndcYRange;
            set => _ndcYRange = value;
        }

        [SerializeField, Tooltip("Defines the valid range for the Z coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.")]
#if UNITY_EDITOR
        [LabeledVector2("Min", "Max")]
#endif
        protected Vector2 _ndcZRange = new Vector2(0.0f, 1.0f);

        /// <summary>
        /// Defines the valid range for the Z coordinate in NDC space. Used in IsARGameObjectInARCameraViewport method for viewport visibility checks.
        /// </summary>
        public virtual Vector2 NdcZRange
        {
            get => _ndcZRange;
            set => _ndcZRange = value;
        }

        [Header("LowPassFilter")]

        [SerializeField, Tooltip("When enabled, LowPassFilter suppresses noise.")]
        protected bool _useLowPassFilter = false;

        /// <summary>
        /// When enabled, LowPassFilter suppresses noise.
        /// </summary>
        public virtual bool UseLowPassFilter
        {
            get => _useLowPassFilter;
            set => _useLowPassFilter = value;
        }

        [SerializeField, Tooltip("Position parameter of LowPassFilter (Value in meters)")]
        [Range(0.0f, 10.0f)]
        protected float _positionLowPassParam = 4f;

        /// <summary>
        /// Position parameter of LowPassFilter (Value in meters)
        /// </summary>
        public virtual float PositionLowPassParam
        {
            get => _positionLowPassParam;
            set => _positionLowPassParam = value;
        }

        [SerializeField, Tooltip("Rotation parameter of LowPassFilter (Value in degrees)")]
        [Range(0.0f, 10.0f)]
        protected float _rotationLowPassParam = 2f;

        /// <summary>
        /// Rotation parameter of LowPassFilter (Value in degrees)
        /// </summary>
        public virtual float RotationLowPassParam
        {
            get => _rotationLowPassParam;
            set => _rotationLowPassParam = value;
        }

        [Header("Apply axis inversion to ARMatrix")]

        [SerializeField, Tooltip("Apply X-axis inversion to ARMatrix.")]
        protected bool _applyXaxisInversionToARMatrix = false;

        /// <summary>
        /// Apply X-axis inversion to ARMatrix.
        /// </summary>
        public virtual bool ApplyXaxisInversionToARMatrix
        {
            get => _applyXaxisInversionToARMatrix;
            set => _applyXaxisInversionToARMatrix = value;
        }

        [SerializeField, Tooltip("Apply Y-axis inversion to ARMatrix.")]
        protected bool _applyYaxisInversionToARMatrix = false;

        /// <summary>
        /// Apply Y-axis inversion to ARMatrix.
        /// </summary>
        public virtual bool ApplyYaxisInversionToARMatrix
        {
            get => _applyYaxisInversionToARMatrix;
            set => _applyYaxisInversionToARMatrix = value;
        }

        [SerializeField, Tooltip("Apply Z-axis inversion to ARMatrix.")]
        protected bool _applyZaxisInversionToARMatrix = false;

        /// <summary>
        /// Apply Z-axis inversion to ARMatrix.
        /// </summary>
        public virtual bool ApplyZaxisInversionToARMatrix
        {
            get => _applyZaxisInversionToARMatrix;
            set => _applyZaxisInversionToARMatrix = value;
        }

        [Header("Event")]

        [SerializeField, Tooltip("Invoke UnityEvent onEnterARCameraViewport only when the ARGameObject is in the ARCamera viewport for the specified number of consecutive frames. Prevents frequent notifications due to noise.")]
        [Range(0.0f, 100.0f)]
        protected int _delayFrameOnEnterARCameraViewportEvent = 0;

        /// <summary>
        /// Invoke UnityEvent onEnterARCameraViewport only when the ARGameObject is in the ARCamera viewport for the specified number of consecutive frames. Prevents frequent notifications due to noise.
        /// </summary>
        public virtual int DelayFrameOnEnterARCameraViewportEvent
        {
            get => _delayFrameOnEnterARCameraViewportEvent;
            set => _delayFrameOnEnterARCameraViewportEvent = value;
        }

        [SerializeField, Tooltip("UnityEvent notified when an ARGameObject enters the viewport of the ARCamera")]
        protected UnityEvent<ARHelper, ARCamera, ARGameObject> _onEnterARCameraViewport;

        /// <summary>
        /// UnityEvent notified when the ARGameObject enters the viewport of the ARCamera
        /// </summary>
        public virtual UnityEvent<ARHelper, ARCamera, ARGameObject> OnEnterARCameraViewport
        {
            get => _onEnterARCameraViewport;
            set => _onEnterARCameraViewport = value;
        }

        [SerializeField, Tooltip("Invoke UnityEvent onEnterARCameraViewport only when the ARGameObject is outside the ARCamera viewport for the specified number of consecutive frames. Prevents frequent notifications due to noise.")]
        [Range(0.0f, 100.0f)]
        protected int _delayFrameOnExitARCameraViewportEvent = 0;

        /// <summary>
        /// Invoke UnityEvent onEnterARCameraViewport only when the ARGameObject is outside the ARCamera viewport for the specified number of consecutive frames. Prevents frequent notifications due to noise.
        /// </summary>
        public virtual int DelayFrameOnExitARCameraViewportEvent
        {
            get => _delayFrameOnExitARCameraViewportEvent;
            set => _delayFrameOnExitARCameraViewportEvent = value;
        }

        [SerializeField, Tooltip("UnityEvent notified when an ARGameObject exits the viewport of the ARCamera")]
        protected UnityEvent<ARHelper, ARCamera, ARGameObject> _onExitARCameraViewport;

        /// <summary>
        /// UnityEvent notified when the ARGameObject exits the viewport of the ARCamera
        /// </summary>
        public virtual UnityEvent<ARHelper, ARCamera, ARGameObject> OnExitARCameraViewport
        {
            get => _onExitARCameraViewport;
            set => _onExitARCameraViewport = value;
        }

        /// <summary>
        /// The number of frames to wait for the ARCamera event to fire.
        /// </summary>
        protected int _arCameraEventCount = 0;

        /// <summary>
        /// The old pose data.
        /// </summary>
        protected PoseData _oldPoseData;

        /// <summary>
        /// The matrix that inverts the X axis.
        /// </summary>
        protected Matrix4x4 _invertXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1, 1, 1));

        /// <summary>
        /// The matrix that inverts the Y axis.
        /// </summary>
        protected Matrix4x4 _invertYMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, -1, 1));

        /// <summary>
        /// The matrix that inverts the Z axis.
        /// </summary>
        protected Matrix4x4 _invertZMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));

        /// <summary>
        /// The transformation matrix from poseData.
        /// </summary>
        protected Matrix4x4 _transformMatrix;

        /// <summary>
        /// The transformation matrix for AR.
        /// </summary>
        protected Matrix4x4 _arMatrix;

        /// <summary>
        /// The rvec Mat for Calib3d.solvePnP().
        /// </summary>
        protected Mat _rvec;

        /// <summary>
        /// The tvec Mat for Calib3d.solvePnP().
        /// </summary>
        protected Mat _tvec;

        /// <summary>
        /// Is the ARGameObject in the ARCameraViewport? This flag is updated when CalculateARMatrix() is called.
        /// </summary>
        protected bool _isARGameObjectInARCameraViewport = false;

        /// <summary>
        /// The flag that indicates whether the SolvePnP points have changed.
        /// </summary>
        protected bool _changedSolvePnpPoints = false;

        /// <summary>
        /// The flag that indicates whether the ARMatrix has changed.
        /// </summary>
        protected bool _changedARMatrix = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            //Debug.Log("ARGameObject Awake()");
            if (_tvec == null || _rvec == null)
            {
                Initialize();
            }
        }

        /// <summary>
        /// Called when the MonoBehaviour will be destroyed.
        /// This method is used to release resources and perform cleanup operations.
        /// </summary>
        protected virtual void OnDestroy()
        {
            //Debug.Log("ARGameObject OnDestroy()");
            Dispose();
        }

        /// <summary>
        /// Called when the script is loaded or a value is changed in the Inspector.
        /// </summary>
        protected virtual void OnValidate()
        {
            //Debug.Log("OnValidate");

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
#endif
                _changedSolvePnpPoints = true;
#if UNITY_EDITOR
            }
#endif
        }

        /// <summary>
        /// Initializes resources and sets the initial values.
        /// This method is called when Awake() is called.
        /// </summary>
        public virtual void Initialize()
        {
            //Debug.Log("ARGameObject Initialize");

            _oldPoseData = new PoseData();
            _transformMatrix = Matrix4x4.identity;
            _arMatrix = Matrix4x4.identity;

            _rvec = new Mat(3, 1, CvType.CV_64FC1);
            _tvec = new Mat(3, 1, CvType.CV_64FC1);

            _isARGameObjectInARCameraViewport = false;
            _arCameraEventCount = -1;

            _changedSolvePnpPoints = false;
            _changedARMatrix = false;
        }

        /// <summary>
        /// Disposes resources.
        /// This method is called when OnDestroy() is called.
        /// </summary>
        public virtual void Dispose()
        {
            //Debug.Log("ARGameObject Dispose");

            _rvec?.Dispose(); _rvec = null;
            _tvec?.Dispose(); _tvec = null;
        }

        /// <summary>
        /// Calculate ARMatrix from set parameters.
        /// </summary>
        public virtual void CalculateARMatrix(ARHelper arHelper)
        {
            //Debug.Log("CalculateARMatrix");

            // validate input arguments
            bool isValidArgs()
            {
                if (_imagePoints == null || _imagePoints.Length == 0)
                    return false;
                if (_objectPoints == null || _objectPoints.Length == 0)
                    return false;

                if (!isValidSolvePnPArgs())
                    return false;

                return true;
            }

            // validate SolvePnP input arguments
            bool isValidSolvePnPArgs()
            {
                int objectCount = _objectPoints.Length;
                int imageCount = _imagePoints.Length;

                if (objectCount != imageCount)
                {
                    Debug.LogWarning($"SolvePnP input is invalid: objectPoints ({objectCount}) and imagePoints ({imageCount}) count mismatch.");
                    return false;
                }

                switch (_solvePnPFlagsMode)
                {
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE_THEN_EPNP:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_EPNP:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_UPNP:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_SQPNP:
                        if (objectCount < 4)
                        {
                            Debug.LogWarning($"SolvePnP input is invalid: {_solvePnPFlagsMode} requires at least 4 points, but got {objectCount}.");
                            return false;
                        }
                        return true;

                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_P3P:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_AP3P:
                        if (objectCount != 3)
                        {
                            Debug.LogWarning($"SolvePnP input is invalid: {_solvePnPFlagsMode} requires exactly 3 points, but got {objectCount}.");
                            return false;
                        }
                        return true;

                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_DLS:
                        if (objectCount < 6)
                        {
                            Debug.LogWarning($"SolvePnP input is invalid: {_solvePnPFlagsMode} requires at least 6 points, but got {objectCount}.");
                            return false;
                        }
                        return true;

                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_IPPE:
                    case Calib3dSolvePnPFlagsMode.SOLVEPNP_IPPE_SQUARE:
                        if (objectCount != 4)
                        {
                            Debug.LogWarning($"SolvePnP input is invalid: {_solvePnPFlagsMode} requires exactly 4 points, but got {objectCount}.");
                            return false;
                        }
                        return true;

                    default:
                        Debug.LogWarning($"SolvePnP input is invalid: Unknown solvePnPFlagsMode ({_solvePnPFlagsMode}).");
                        return false;
                }
            }


            // Manages events when the AR object enters or exits the AR camera viewport. The event trigger is delayed to prevent unnecessary consecutive event invocations.
            void checkARCameraEvent(bool isInViewport)
            {
                // If an event is already waiting to be triggered and the state changes, cancel the pending event.
                if (_arCameraEventCount >= 0 && _isARGameObjectInARCameraViewport != isInViewport)
                {
                    _arCameraEventCount = -1; // Cancel the pending event.
                    return;
                }

                // If no event is currently waiting and the state has changed, set up a new waiting period.
                if (_arCameraEventCount < 0 && _isARGameObjectInARCameraViewport != isInViewport)
                {
                    // If entering the viewport, wait for `delayFrameOnEnterARCameraViewportEvent` frames before triggering the event.
                    // If exiting the viewport, wait for `delayFrameOnExitARCameraViewportEvent` frames before triggering the event.
                    _arCameraEventCount = isInViewport
                        ? _delayFrameOnEnterARCameraViewportEvent
                        : _delayFrameOnExitARCameraViewportEvent;
                }

                // If an event is waiting to be triggered, count down the frames.
                if (_arCameraEventCount >= 0)
                {
                    if (_arCameraEventCount == 0) // When the countdown reaches zero, trigger the event.
                    {
                        if (isInViewport)
                        {
                            // Event triggered when the AR object enters the AR camera viewport.
                            _onEnterARCameraViewport.Invoke(arHelper, arHelper.ARCamera, this);
                        }
                        else
                        {
                            // Event triggered when the AR object exits the AR camera viewport.
                            _onExitARCameraViewport.Invoke(arHelper, arHelper.ARCamera, this);
                        }
                        _arCameraEventCount = -1; // Disable the counter after triggering the event.
                    }
                    else
                    {
                        _arCameraEventCount--; // Decrease the countdown counter.
                    }
                }
            }


            if (!_changedSolvePnpPoints) return;
            _changedSolvePnpPoints = false;
            //Debug.Log("CalculateARMatrix 2");

            // tvec and rvec are null, initialize resources.
            if (_tvec == null || _rvec == null)
            {
                Initialize();
            }


            if (!isValidArgs())
            {
                checkARCameraEvent(false);
                _isARGameObjectInARCameraViewport = false;

                return;
            }


            Vector2[] imagePoints = _imagePoints;
            Vector3[] objectPoints;
            // If the object point is a left-hand coordinate system (Unity), convert it to a right-hand coordinate system (OpenCV).
            if (_leftHandedCoordinates)
            {
                objectPoints = new Vector3[_objectPoints.Length];
                for (int i = 0; i < _objectPoints.Length; i++)
                    objectPoints[i] = new Vector3(_objectPoints[i].x, -_objectPoints[i].y, _objectPoints[i].z);
            }
            else
            {
                objectPoints = _objectPoints;
            }

            bool newIsARGameObjectInARCameraViewport;

            using (MatOfPoint2f m_markerCorners2d = new MatOfPoint2f(imagePoints))
            using (MatOfPoint3f m_markerCorners3d = new MatOfPoint3f(objectPoints))
            {
                Mat camMatrix = arHelper.ARCamera.GetCamMatrix();
                MatOfDouble distCoeffs = arHelper.ARCamera.GetDistCoeffs();

                if (!_isARGameObjectInARCameraViewport)
                {
                    //Debug.Log("first call");

                    if (_solvePnPFlagsMode == Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE_THEN_EPNP)
                    {

                        Calib3d.solvePnP(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs,
                                 _rvec, _tvec, false, Calib3d.SOLVEPNP_ITERATIVE);
                    }
                    else
                    {
                        Calib3d.solvePnP(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs,
                                _rvec, _tvec, false, (int)_solvePnPFlagsMode);
                    }

                    if (_useSolvePnPRefineLM)
                        Calib3d.solvePnPRefineLM(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs, _rvec, _tvec);
                }
                else
                {
                    if (_solvePnPFlagsMode == Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE_THEN_EPNP)
                    {
                        Calib3d.solvePnP(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs,
                             _rvec, _tvec, true, Calib3d.SOLVEPNP_EPNP);
                    }
                    else
                    {
                        Calib3d.solvePnP(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs,
                             _rvec, _tvec, true, (int)_solvePnPFlagsMode);
                    }

                    // If the difference between the tvec of the current frame and that of the previous frame is greater than the specified threshold, call the Calib3d.solvePnPRefineLM() method.
                    if (_useSolvePnPRefineLM)
                    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                        ReadOnlySpan<double> tvecValue = _tvec.AsSpan<double>().Slice(0, 3);
#else
                        double[] tvecValue = new double[3];
                        _tvec.get(0, 0, tvecValue);
#endif
                        //Debug.Log("distance"+ Vector3.Distance(oldPoseData.pos, new Vector3((float)tvecValue[0], (float)tvecValue[1], (float)tvecValue[2])));
                        if (Vector3.Distance(_oldPoseData.pos, new Vector3((float)tvecValue[0], (float)tvecValue[1], (float)tvecValue[2])) > _solvePnPRefineLMMovementThreshold)
                        {
                            //Debug.Log("refine call");
                            Calib3d.solvePnPRefineLM(m_markerCorners3d, m_markerCorners2d, camMatrix, distCoeffs, _rvec, _tvec);
                        }
                    }
                }


                newIsARGameObjectInARCameraViewport = IsARGameObjectInARCameraViewport(_tvec, arHelper.ARCamera._opencvCameraProjectionMatrix);
                //Debug.Log("newIsARGameObjectInARCameraViewport " + newIsARGameObjectInARCameraViewport);


                if (newIsARGameObjectInARCameraViewport)
                {
                    // Convert to unity pose data.
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                    ReadOnlySpan<double> rvecSpan = _rvec.AsSpan<double>().Slice(0, 3);
                    double[] rvecValues = System.Buffers.ArrayPool<double>.Shared.Rent(rvecSpan.Length);
                    rvecSpan.CopyTo(rvecValues.AsSpan(0, rvecSpan.Length));
                    ReadOnlySpan<double> tvecSpan = _tvec.AsSpan<double>().Slice(0, 3);
                    double[] tvecValues = System.Buffers.ArrayPool<double>.Shared.Rent(tvecSpan.Length);
                    tvecSpan.CopyTo(tvecValues.AsSpan(0, tvecSpan.Length));
                    PoseData poseData = ARUtils.ConvertRvecTvecToPoseData(rvecValues, tvecValues);
                    System.Buffers.ArrayPool<double>.Shared.Return(rvecValues);
                    System.Buffers.ArrayPool<double>.Shared.Return(tvecValues);
#else
                    double[] rvecValues = new double[3];
                    _rvec.get(0, 0, rvecValues);
                    double[] tvecValues = new double[3];
                    _tvec.get(0, 0, tvecValues);
                    PoseData poseData = ARUtils.ConvertRvecTvecToPoseData(rvecValues, tvecValues);
#endif
                    //Debug.Log("poseData.pos " + poseData.pos);
                    //Debug.Log("poseData.rot " + poseData.rot);

                    //Changes in pos / rot below these thresholds are ignored.
                    if (_useLowPassFilter)
                    {
                        ARUtils.LowpassPoseData(ref _oldPoseData, ref poseData, _positionLowPassParam, _rotationLowPassParam);
                    }
                    _oldPoseData = poseData;

                    _transformMatrix = ARUtils.ConvertPoseDataToMatrix(ref _oldPoseData, true);
                }
            }

            _arMatrix = _transformMatrix;

            if (_applyXaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertXMatrix;
            if (_applyYaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertYMatrix;
            if (_applyZaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertZMatrix;

            _changedARMatrix = true;
            //Debug.Log("changedARMatrix " + changedARMatrix);

            checkARCameraEvent(newIsARGameObjectInARCameraViewport);
            _isARGameObjectInARCameraViewport = newIsARGameObjectInARCameraViewport;
        }

        /// <summary>
        /// Update the Transform of UpdateTarget using ARMatrix.
        /// </summary>
        public virtual void UpdateTransform(ARHelper arHelper)
        {
            if (!_changedARMatrix) return;
            _changedARMatrix = false;

            //Debug.Log("UpdateTransform");

            if (_arMatrix.isIdentity)
                return;

            if (arHelper.UpdateTarget == ARHelper.ARUpdateTarget.ARCamera)
            {
                Matrix4x4 matrix = this.transform.localToWorldMatrix * _arMatrix.inverse;
                ARUtils.SetTransformFromMatrix(arHelper.ARCamera.transform, ref matrix);

            }
            else
            {
                Matrix4x4 matrix = arHelper.ARCamera.transform.localToWorldMatrix * _arMatrix;
                ARUtils.SetTransformFromMatrix(transform, ref matrix);
            }

        }

        /// <summary>
        /// Get rvec.
        /// </summary>
        /// <returns></returns>
        public virtual Mat GetRvec()
        {
            return _rvec;
        }

        /// <summary>
        /// Get tvec.
        /// </summary>
        /// <returns></returns>
        public virtual Mat GetTvec()
        {
            return _tvec;
        }

        /// <summary>
        /// Get ARMatrix.
        /// </summary>
        /// <returns>AR Matrix</returns>
        public virtual Matrix4x4 GetARMatrix()
        {
            return _arMatrix;
        }

        /// <summary>
        /// Is the ARGameObject in the ARCameraViewport? This flag is updated when CalculateARMatrix() is called.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsARGameObjectInARCameraViewport()
        {
            return _isARGameObjectInARCameraViewport;
        }

        /// <summary>
        /// Reset the imagePoints and objectPoints.
        /// </summary>
        public virtual void ResetImagePointsAndObjectPoints()
        {
            //Debug.Log("ResetImagePointsAndObjectPoints");

            _imagePoints = null;
            _objectPoints = null;

            _changedSolvePnpPoints = true;
        }

        /// <summary>
        /// Determines whether an AR game object is within the AR camera's viewport.
        /// </summary>
        /// <param name="tvec">The translation vector (tvec) representing the object's position in the camera coordinate system.</param>
        /// <param name="projectionMatrix">The projection matrix to transform the position.</param>
        /// <returns>True if the object is in the viewport; otherwise, false.</returns>
        protected virtual bool IsARGameObjectInARCameraViewport(Mat tvec, Matrix4x4 projectionMatrix)
        {
            //Debug.Log("tvec.dump() " + tvec.dump());

            // Retrieve the tvec values as a double array
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> tvecValues = tvec.AsSpan<double>().Slice(0, 3);
#else
            double[] tvecValues = new double[3];
            tvec.get(0, 0, tvecValues);
#endif

            // Check if any value in tvecValue is NaN
            if (double.IsNaN(tvecValues[0]) || double.IsNaN(tvecValues[1]) || double.IsNaN(tvecValues[2]))
            {
                return false;
            }

            // Transform the position using the projection matrix
            Vector4 pos = projectionMatrix * new Vector4((float)tvecValues[0], (float)tvecValues[1], (float)tvecValues[2], 1.0f);

            if (pos.w != 0)
            {
                // Normalize the projected position
                float x = pos.x / pos.w;
                float y = pos.y / pos.w;
                float z = pos.z / pos.w;

                // Check if the position is within the normalized device coordinate (NDC) range
                return x >= _ndcXRange.x && x <= _ndcXRange.y &&
                       y >= _ndcYRange.x && y <= _ndcYRange.y &&
                       z >= _ndcZRange.x && z <= _ndcZRange.y;
            }

            return false;
        }
    }
}
