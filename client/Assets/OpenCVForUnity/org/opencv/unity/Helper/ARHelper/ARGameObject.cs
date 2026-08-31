using System;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.Events;
using static OpenCVForUnity.UnityIntegration.OpenCVARUtils;

#if UNITY_EDITOR
using UnityEditor;
using OpenCVForUnity.UnityIntegration.Helper.AR.Editor;
#endif

namespace OpenCVForUnity.UnityIntegration.Helper.AR
{
    /// <summary>
    /// Represents an augmented reality (AR) object that integrates with the ARCamera.
    /// This class manages solving Perspective-n-Point (PnP) problems, handling AR matrix transformations,
    /// and detecting whether the object is within the AR camera's viewport.
    /// </summary>
    /// <remarks>
    /// - Utilizes OpenCV's solvePnP methods to estimate the object's 3D position from 2D image points.
    /// - Supports multiple solvePnP flags and optional refinement processes using solvePnPRefineLM or solvePnPRefineVVS.
    /// - Provides RANSAC filtering to remove outliers before solvePnP computation.
    /// - Supports coordinate system transformations and noise reduction via low-pass and smoothing filters.
    /// - Configurable NDC (Normalized Device Coordinates) range settings for viewport detection.
    /// - Supports axis inversion transformations for AR matrix adjustments.
    /// - Triggers events when the object enters or exits the AR camera viewport with configurable delay frames.
    /// - Updates the object's transform based on computed AR matrices.
    /// </remarks>
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
            SOLVEPNP_SQPNP = Calib3d.SOLVEPNP_SQPNP
        }

        /// <summary>
        /// Enum to set refinement method for solvePnP optimization.
        /// </summary>
        public enum SolvePnPRefinementMethod
        {
            None,
            solvePnPRefineLM,
            solvePnPRefineVVS
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
        protected Calib3dSolvePnPFlagsMode _solvePnPFlagsMode = Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE;

        /// <summary>
        /// Specify the flags argument to the Calib3d.solvePnP() method.
        /// </summary>
        public virtual Calib3dSolvePnPFlagsMode SolvePnPFlagsMode
        {
            get => _solvePnPFlagsMode;
            set => _solvePnPFlagsMode = value;
        }

        [Space(10)]

        [SerializeField, Tooltip("If this flag is set to true, Calib3d.SOLVEPNP_ITERATIVE is used for Calib3d.solvePnP() after the initial Calib3d.solvePnP() call.")]
        protected bool _useSOLVEPNP_ITERATIVE = false;

        /// <summary>
        /// If this flag is set to true, Calib3d.SOLVEPNP_ITERATIVE is used for solvePnP when the AR object is already detected (not first detection).
        /// </summary>
        public virtual bool UseSOLVEPNP_ITERATIVE
        {
            get => _useSOLVEPNP_ITERATIVE;
            set => _useSOLVEPNP_ITERATIVE = value;
        }

        [Space(10)]

        [SerializeField, Tooltip("If this flag is set to true, Calib3d.solvePnPRansac() is called before Calib3d.solvePnP() to filter out outliers.")]
        protected bool _useSolvePnPRansac = false;

        /// <summary>
        /// If this flag is set to true, Calib3d.solvePnPRansac() is called before Calib3d.solvePnP() to filter out outliers.
        /// </summary>
        public virtual bool UseSolvePnPRansac
        {
            get => _useSolvePnPRansac;
            set => _useSolvePnPRansac = value;
        }

        [SerializeField, Tooltip("Maximum number of iterations for RANSAC.")]
        [Range(1, 10000)]
        protected int _iterationsCount = 100;

        /// <summary>
        /// Maximum number of iterations for RANSAC.
        /// </summary>
        public virtual int IterationsCount
        {
            get => _iterationsCount;
            set => _iterationsCount = value;
        }

        [SerializeField, Tooltip("Maximum distance from a point to an epipolar line in pixels for RANSAC.")]
        [Range(0.1f, 10.0f)]
        protected float _reprojectionError = 8.0f;

        /// <summary>
        /// Maximum distance from a point to an epipolar line in pixels for RANSAC.
        /// </summary>
        public virtual float ReprojectionError
        {
            get => _reprojectionError;
            set => _reprojectionError = value;
        }

        [SerializeField, Tooltip("Confidence level for RANSAC.")]
        [Range(0.0f, 1.0f)]
        protected float _confidence = 0.99f;

        /// <summary>
        /// Confidence level for RANSAC.
        /// </summary>
        public virtual float Confidence
        {
            get => _confidence;
            set => _confidence = value;
        }

        [Space(10)]

        [SerializeField, Tooltip("Select the refinement method for solvePnP optimization.")]
        protected SolvePnPRefinementMethod _solvePnPRefinementMethod = SolvePnPRefinementMethod.None;

        /// <summary>
        /// Select the refinement method for solvePnP optimization.
        /// </summary>
        public virtual SolvePnPRefinementMethod RefinementMethod
        {
            get => _solvePnPRefinementMethod;
            set => _solvePnPRefinementMethod = value;
        }

        [SerializeField, Tooltip("If refinement is enabled, the refinement method is called if the reprojection error RMS is greater than the specified threshold.")]
        [Range(0.0f, 10.0f)]
        protected float _solvePnPRefinementRMSThreshold = 1.0f;

        /// <summary>
        /// If refinement is enabled, the refinement method is called if the reprojection error RMS is greater than the specified threshold.
        /// </summary>
        public virtual float SolvePnPRefinementRMSThreshold
        {
            get => _solvePnPRefinementRMSThreshold;
            set => _solvePnPRefinementRMSThreshold = value;
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
        [Range(0.0f, 1.0f)]
        protected float _positionLowPassParam = 0.01f;

        /// <summary>
        /// Position parameter of LowPassFilter (Value in meters)
        /// </summary>
        public virtual float PositionLowPassParam
        {
            get => _positionLowPassParam;
            set => _positionLowPassParam = value;
        }

        [SerializeField, Tooltip("Rotation parameter of LowPassFilter (Value in degrees)")]
        [Range(0.0f, 15.0f)]
        protected float _rotationLowPassParam = 4f;

        /// <summary>
        /// Rotation parameter of LowPassFilter (Value in degrees)
        /// </summary>
        public virtual float RotationLowPassParam
        {
            get => _rotationLowPassParam;
            set => _rotationLowPassParam = value;
        }

        [Header("SmoothingFilter")]

        [SerializeField, Tooltip("When enabled, smoothing filter suppresses noise.")]
        protected bool _useSmoothingFilter = false;

        /// <summary>
        /// When enabled, smoothing filter suppresses noise.
        /// </summary>
        public virtual bool UseSmoothingFilter
        {
            get => _useSmoothingFilter;
            set => _useSmoothingFilter = value;
        }

        [SerializeField, Tooltip("Smoothing factor for position (0.0 = no filtering, 1.0 = maximum filtering). Higher values result in more smoothing and slower response to changes.")]
        [Range(0.0f, 1.0f)]
        protected float _positionSmoothingFactor = 0.2f;

        /// <summary>
        /// Smoothing factor for position (0.0 = no filtering, 1.0 = maximum filtering). Higher values result in more smoothing and slower response to changes.
        /// </summary>
        public virtual float PositionSmoothingFactor
        {
            get => _positionSmoothingFactor;
            set => _positionSmoothingFactor = Mathf.Clamp01(value);
        }

        [SerializeField, Tooltip("Smoothing factor for rotation (0.0 = no filtering, 1.0 = maximum filtering). Higher values result in more smoothing and slower response to changes.")]
        [Range(0.0f, 1.0f)]
        protected float _rotationSmoothingFactor = 0.3f;

        /// <summary>
        /// Smoothing factor for rotation (0.0 = no filtering, 1.0 = maximum filtering). Higher values result in more smoothing and slower response to changes.
        /// </summary>
        public virtual float RotationSmoothingFactor
        {
            get => _rotationSmoothingFactor;
            set => _rotationSmoothingFactor = Mathf.Clamp01(value);
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
        /// Invoke UnityEvent onExitARCameraViewport only when the ARGameObject is outside the ARCamera viewport for the specified number of consecutive frames. Prevents frequent notifications due to noise.
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

        [Header("Debug Settings")]

        [SerializeField, Tooltip("Enable debug output to console.")]
        protected bool _enableDebugOutput = false;

        /// <summary>
        /// Enable debug output to console.
        /// </summary>
        public virtual bool EnableDebugOutput
        {
            get => _enableDebugOutput;
            set => _enableDebugOutput = value;
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
        /// Flag to track if old pose data has been initialized with valid data.
        /// </summary>
        protected bool _isOldPoseDataInitialized = false;

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
            _isOldPoseDataInitialized = false;
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
        /// This method updates the ARMatrix that is used by the UpdateTransform() method.
        /// This method should be called before UpdateTransform().
        /// </summary>
        public virtual void CalculateARMatrix(ARHelper arHelper)
        {
            //Debug.Log("CalculateARMatrix");

            if (!isValidArgs())
            {
                checkARCameraEvent(false);
                _isARGameObjectInARCameraViewport = false;

                return;
            }


            // Validates if the input arguments are valid for AR matrix calculation.
            bool isValidArgs()
            {
                // validate input arguments
                if (_imagePoints == null || _imagePoints.Length == 0)
                    return false;
                if (_objectPoints == null || _objectPoints.Length == 0)
                    return false;

                if (!IsValidPointCountForSolvePnPFlag(_objectPoints.Length, _imagePoints.Length, _solvePnPFlagsMode))
                    return false;

                return true;
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

            Mat camMatrix = arHelper.ARCamera.GetCamMatrix();
            MatOfDouble distCoeffs = arHelper.ARCamera.GetDistCoeffs();

            SolvePnPWithRansacAndRefinement(imagePoints, objectPoints, camMatrix, distCoeffs, _rvec, _tvec, _isARGameObjectInARCameraViewport);

            newIsARGameObjectInARCameraViewport = IsARGameObjectInARCameraViewport(_tvec, arHelper.ARCamera._opencvCameraProjectionMatrix);
            //Debug.Log("newIsARGameObjectInARCameraViewport " + newIsARGameObjectInARCameraViewport);

            if (newIsARGameObjectInARCameraViewport)
            {
                // Convert to unity pose data.
                PoseData poseData = OpenCVARUtils.ConvertRvecTvecToPoseData(_rvec, _tvec);

                //Debug.Log("poseData.pos " + poseData.pos);
                //Debug.Log("poseData.rot " + poseData.rot);

                // Initialize old pose data with first valid pose data
                if (!_isOldPoseDataInitialized)
                {
                    _oldPoseData = poseData;
                    _isOldPoseDataInitialized = true;
                    // Debug.Log("Initialized old pose data with first valid pose");
                }

                //Changes in pos / rot below these thresholds are ignored.
                if (_useLowPassFilter)
                {
                    OpenCVARUtils.LowpassPoseData(ref _oldPoseData, ref poseData, _positionLowPassParam, _rotationLowPassParam);
                }

                // Apply smoothing filter using exponential moving average
                if (_useSmoothingFilter)
                {
                    OpenCVARUtils.SmoothingFilterPoseData(ref _oldPoseData, ref poseData, _positionSmoothingFactor, _rotationSmoothingFactor);
                }

                // Update old pose data after filtering
                _oldPoseData = poseData;

                _transformMatrix = OpenCVARUtils.ConvertPoseDataToMatrix(ref _oldPoseData, true);
            }

            _arMatrix = _transformMatrix;

            if (_applyXaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertXMatrix;
            if (_applyYaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertYMatrix;
            if (_applyZaxisInversionToARMatrix) _arMatrix = _arMatrix * _invertZMatrix;

            _changedARMatrix = true;
            //Debug.Log("changedARMatrix " + changedARMatrix);

            checkARCameraEvent(newIsARGameObjectInARCameraViewport);
            _isARGameObjectInARCameraViewport = newIsARGameObjectInARCameraViewport;

            if (_enableDebugOutput){
                if(_isARGameObjectInARCameraViewport){
                    Debug.Log("ARGameObject is in ARCameraViewport");
                }else{
                    Debug.Log("ARGameObject is not in ARCameraViewport");
                }
            }
        }

        /// <summary>
        /// Update the Transform of UpdateTarget using ARMatrix.
        /// This method uses the ARMatrix calculated by the CalculateARMatrix() method.
        /// This method assumes that the CalculateARMatrix() method has been called before this method.
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
                OpenCVARUtils.SetTransformFromMatrix(arHelper.ARCamera.transform, ref matrix);

            }
            else
            {
                Matrix4x4 matrix = arHelper.ARCamera.transform.localToWorldMatrix * _arMatrix;
                OpenCVARUtils.SetTransformFromMatrix(transform, ref matrix);
            }

        }

        /// <summary>
        /// Get rvec.
        /// This value is updated by the CalculateARMatrix() method.
        /// </summary>
        /// <returns></returns>
        public virtual Mat GetRvec()
        {
            return _rvec;
        }

        /// <summary>
        /// Get tvec.
        /// This value is updated by the CalculateARMatrix() method.
        /// </summary>
        /// <returns></returns>
        public virtual Mat GetTvec()
        {
            return _tvec;
        }

        /// <summary>
        /// Get ARMatrix.
        /// This value is updated by the CalculateARMatrix() method.
        /// </summary>
        /// <returns>AR Matrix</returns>
        public virtual Matrix4x4 GetARMatrix()
        {
            return _arMatrix;
        }

        /// <summary>
        /// Is the ARGameObject in the ARCameraViewport? This flag is updated by the CalculateARMatrix() method.
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
        /// Validates if the point count is valid for the selected solvePnP flag.
        /// </summary>
        /// <param name="objectPointCount">Number of 3D object points</param>
        /// <param name="imagePointCount">Number of 2D image points</param>
        /// <param name="solvePnPFlagsMode">The solvePnP flags mode to validate against</param>
        /// <returns>True if the point count is valid for the selected solvePnP flag; otherwise, false.</returns>
        private bool IsValidPointCountForSolvePnPFlag(int objectPointCount, int imagePointCount, Calib3dSolvePnPFlagsMode solvePnPFlagsMode)
        {
            if (objectPointCount != imagePointCount)
            {
                if (_enableDebugOutput)
                    Debug.LogWarning($"SolvePnP input is invalid: objectPoints ({objectPointCount}) and imagePoints ({imagePointCount}) count mismatch.");
                return false;
            }

            switch (solvePnPFlagsMode)
            {
                case Calib3dSolvePnPFlagsMode.SOLVEPNP_ITERATIVE:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"SolvePnP input is invalid: {solvePnPFlagsMode} requires at least 6 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                case Calib3dSolvePnPFlagsMode.SOLVEPNP_EPNP:
                case Calib3dSolvePnPFlagsMode.SOLVEPNP_UPNP:
                case Calib3dSolvePnPFlagsMode.SOLVEPNP_SQPNP:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"SolvePnP input is invalid: {solvePnPFlagsMode} requires at least 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                case Calib3dSolvePnPFlagsMode.SOLVEPNP_P3P:
                case Calib3dSolvePnPFlagsMode.SOLVEPNP_AP3P:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"SolvePnP input is invalid: {solvePnPFlagsMode} requires at least 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                case Calib3dSolvePnPFlagsMode.SOLVEPNP_DLS:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"SolvePnP input is invalid: {solvePnPFlagsMode} requires at least 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                case Calib3dSolvePnPFlagsMode.SOLVEPNP_IPPE:
                case Calib3dSolvePnPFlagsMode.SOLVEPNP_IPPE_SQUARE:
                    if (objectPointCount != 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"SolvePnP input is invalid: {solvePnPFlagsMode} requires exactly 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                default:
                    if (_enableDebugOutput)
                        Debug.LogWarning($"SolvePnP input is invalid: Unknown solvePnPFlagsMode ({solvePnPFlagsMode}).");
                    return false;
            }
        }

        /// <summary>
        /// Validates if the point count is valid for the selected refinement method.
        /// </summary>
        /// <param name="objectPointCount">Number of 3D object points</param>
        /// <param name="imagePointCount">Number of 2D image points</param>
        /// <param name="refinementMethod">The refinement method to validate against</param>
        /// <returns>True if the point count is valid for the selected refinement method; otherwise, false.</returns>
        private bool IsValidPointCountForRefinementMethod(int objectPointCount, int imagePointCount, SolvePnPRefinementMethod refinementMethod)
        {
            if (objectPointCount != imagePointCount)
            {
                if (_enableDebugOutput)
                    Debug.LogWarning($"Refinement input is invalid: objectPoints ({objectPointCount}) and imagePoints ({imagePointCount}) count mismatch.");
                return false;
            }

            switch (refinementMethod)
            {
                case SolvePnPRefinementMethod.solvePnPRefineLM:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"Refinement input is invalid: {refinementMethod} requires at least 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                case SolvePnPRefinementMethod.solvePnPRefineVVS:
                    if (objectPointCount < 4)
                    {
                        if (_enableDebugOutput)
                            Debug.LogWarning($"Refinement input is invalid: {refinementMethod} requires at least 4 points, but got {objectPointCount}.");
                        return false;
                    }
                    return true;

                default:
                    if (_enableDebugOutput)
                        Debug.LogWarning($"Refinement input is invalid: Unknown refinement method ({refinementMethod}).");
                    return false;
            }
        }

        /// <summary>
        /// Performs solvePnP with optional RANSAC filtering and refinement.
        /// Uses different solvePnP flags based on detection status and applies refinement if enabled.
        /// </summary>
        /// <param name="imagePoints">2D image points as Vector2 array</param>
        /// <param name="objectPoints">3D object points as Vector3 array</param>
        /// <param name="camMatrix">Camera matrix</param>
        /// <param name="distCoeffs">Distortion coefficients</param>
        /// <param name="rvec">Output rotation vector</param>
        /// <param name="tvec">Output translation vector</param>
        /// <param name="isARGameObjectInARCameraViewport">Whether the AR game object is in the AR camera viewport</param>
        private void SolvePnPWithRansacAndRefinement(Vector2[] imagePoints, Vector3[] objectPoints, Mat camMatrix, MatOfDouble distCoeffs, Mat rvec, Mat tvec, bool isARGameObjectInARCameraViewport)
        {
            MatOfPoint2f markerCorners2d = new MatOfPoint2f(imagePoints);
            MatOfPoint3f markerCorners3d = new MatOfPoint3f(objectPoints);

            // Determine whether to use extrinsic guess based on AR object viewport status
            bool isFirstDetection = !isARGameObjectInARCameraViewport;
            bool useExtrinsicGuess = !isFirstDetection;

            // Determine the solvePnP flags to use
            int solvePnPFlags = (int)_solvePnPFlagsMode;
            if (!isFirstDetection && _useSOLVEPNP_ITERATIVE)
            {
                solvePnPFlags = Calib3d.SOLVEPNP_ITERATIVE;
            }

            // Try solvePnPRansac first if enabled
            bool ransacSuccess = false;
            MatOfInt inliers = null;
            if (_useSolvePnPRansac)
            {
                inliers = new MatOfInt();
                ransacSuccess = Calib3d.solvePnPRansac(markerCorners3d, markerCorners2d, camMatrix, distCoeffs,
                    rvec, tvec, ransacSuccess, _iterationsCount, _reprojectionError, _confidence, inliers);
            }

            // Use solvePnP with filtered points if RANSAC succeeded, otherwise use all points
            int inlierCount = inliers != null ? (int)inliers.total() : 0;
            if (ransacSuccess && IsValidPointCountForSolvePnPFlag(inlierCount, inlierCount, (Calib3dSolvePnPFlagsMode)solvePnPFlags))
            {
                // Use inliers for solvePnP
                if (_enableDebugOutput)
                    Debug.Log("RANSAC succeeded with " + inlierCount + " inliers");

                MatOfPoint3f filteredObjectPoints = null;
                MatOfPoint2f filteredImagePoints = null;

                // Optimized version using stackalloc for better performance
                Vector3[] filteredObjectPointsArray = new Vector3[inlierCount];
                Vector2[] filteredImagePointsArray = new Vector2[inlierCount];

                // Filter points based on inliers
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                // Optimized version using ReadOnlySpan for inliers access
                ReadOnlySpan<int> inliersArray = inliers.AsSpan<int>();
#else
                // Get all inliers at once for better performance
                int[] inliersArray = new int[inlierCount];
                inliers.get(0, 0, inliersArray);
#endif
                for (int i = 0; i < inlierCount; i++)
                {
                    int idx = inliersArray[i];
                    filteredObjectPointsArray[i] = objectPoints[idx];
                    filteredImagePointsArray[i] = imagePoints[idx];
                }

                filteredObjectPoints = new MatOfPoint3f(filteredObjectPointsArray);
                filteredImagePoints = new MatOfPoint2f(filteredImagePointsArray);

                // Dispose existing markerCorners3d and markerCorners2d before assignment
                if (markerCorners3d != null)
                    markerCorners3d.Dispose();
                if (markerCorners2d != null)
                    markerCorners2d.Dispose();

                // Update markerCorners3d and markerCorners2d with filtered points
                markerCorners3d = filteredObjectPoints;
                markerCorners2d = filteredImagePoints;

                Calib3d.solvePnP(markerCorners3d, markerCorners2d, camMatrix, distCoeffs,
                        rvec, tvec, useExtrinsicGuess, solvePnPFlags);
            }
            else
            {
                // Use all points for solvePnP
                Calib3d.solvePnP(markerCorners3d, markerCorners2d, camMatrix, distCoeffs,
                        rvec, tvec, useExtrinsicGuess, solvePnPFlags);
            }

            if (inliers != null)
                inliers.Dispose();

            // Apply solvePnP refinement based on detection status
            if (_solvePnPRefinementMethod != SolvePnPRefinementMethod.None)
            {
                // Calculate reprojection error RMS for all detections (including first detection)
                float reprojectionErrorRMS = CalculateReprojectionErrorRMS(markerCorners3d, markerCorners2d, rvec, tvec, camMatrix, distCoeffs);
                if (_enableDebugOutput)
                    Debug.Log("reprojectionErrorRMS before refinement " + reprojectionErrorRMS);

                // Determine if refinement should be performed based on RMS threshold and minimum point count
                int pointCount = (int)markerCorners3d.total();
                bool shouldRefine = reprojectionErrorRMS > _solvePnPRefinementRMSThreshold &&
                                   IsValidPointCountForRefinementMethod(pointCount, pointCount, _solvePnPRefinementMethod);

                if (shouldRefine)
                {
                    ApplySolvePnPRefinement(markerCorners3d, markerCorners2d, camMatrix, distCoeffs, rvec, tvec);
                }
            }

            if (_enableDebugOutput)
                Debug.Log("reprojectionErrorRMS " + CalculateReprojectionErrorRMS(markerCorners3d, markerCorners2d, rvec, tvec, camMatrix, distCoeffs));

            if (markerCorners2d != null)
                markerCorners2d.Dispose();
            if (markerCorners3d != null)
                markerCorners3d.Dispose();
        }

        /// <summary>
        /// Applies the selected solvePnP refinement method.
        /// </summary>
        /// <param name="objectPoints">3D object points</param>
        /// <param name="imagePoints">2D image points</param>
        /// <param name="camMatrix">Camera matrix</param>
        /// <param name="distCoeffs">Distortion coefficients</param>
        /// <param name="rvec">Rotation vector</param>
        /// <param name="tvec">Translation vector</param>
        private void ApplySolvePnPRefinement(MatOfPoint3f objectPoints, MatOfPoint2f imagePoints, Mat camMatrix, MatOfDouble distCoeffs, Mat rvec, Mat tvec)
        {
            switch (_solvePnPRefinementMethod)
            {
                case SolvePnPRefinementMethod.solvePnPRefineLM:
                    Calib3d.solvePnPRefineLM(objectPoints, imagePoints, camMatrix, distCoeffs, rvec, tvec);
                    break;
                case SolvePnPRefinementMethod.solvePnPRefineVVS:
                    Calib3d.solvePnPRefineVVS(objectPoints, imagePoints, camMatrix, distCoeffs, rvec, tvec);
                    break;
                default:
                    // No refinement
                    break;
            }
        }

        /// <summary>
        /// Calculates the reprojection error RMS for the given 3D and 2D points.
        /// </summary>
        /// <param name="objectPoints">3D object points</param>
        /// <param name="imagePoints">2D image points</param>
        /// <param name="rvec">Rotation vector</param>
        /// <param name="tvec">Translation vector</param>
        /// <param name="camMatrix">Camera matrix</param>
        /// <param name="distCoeffs">Distortion coefficients</param>
        /// <returns>Reprojection error RMS</returns>
        private float CalculateReprojectionErrorRMS(MatOfPoint3f objectPoints, MatOfPoint2f imagePoints, Mat rvec, Mat tvec, Mat camMatrix, MatOfDouble distCoeffs)
        {
            using (MatOfPoint2f projectedPoints = new MatOfPoint2f())
            {
                // Project 3D points to 2D using the current pose
                Calib3d.projectPoints(objectPoints, rvec, tvec, camMatrix, distCoeffs, projectedPoints);

                // Debug.Log("objectPoints.dump() " + objectPoints.dump());
                // Debug.Log("imagePoints.dump() " + imagePoints.dump());
                // Debug.Log("projectedPoints.dump() " + projectedPoints.dump());

                // Copy Mat data to Vector2 arrays using OpenCVMatUtils
                int pointCount = (int)objectPoints.total();
                Vector2[] imagePointsArray = new Vector2[pointCount];
                Vector2[] projectedPointsArray = new Vector2[pointCount];

                OpenCVMatUtils.CopyFromMat<Vector2>(imagePoints, imagePointsArray);
                OpenCVMatUtils.CopyFromMat<Vector2>(projectedPoints, projectedPointsArray);

                // Calculate reprojection errors using array access
                float totalError = 0.0f;

                for (int i = 0; i < pointCount; i++)
                {
                    float error = Vector2.Distance(imagePointsArray[i], projectedPointsArray[i]);
                    totalError += error * error; // Square of the error
                }

                // Calculate RMS (Root Mean Square)
                return Mathf.Sqrt(totalError / pointCount);
            }
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
