#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API

using System;
using System.Collections;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor;
#endif

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// A helper component class for obtaining camera frames from <see cref="WebCamTexture"/> and converting them to OpenCV <c>Mat</c> format in real-time.
    /// </summary>
    /// <remarks>
    /// The <c>WebCamTexture2MatHelper</c> class captures video frames from a device's camera using <see cref="WebCamTexture"/>
    /// and converts each frame to an OpenCV <c>Mat</c> object every frame.
    /// This component handles camera orientation, rotation, and necessary transformations to ensure the <c>Mat</c> output
    /// aligns correctly with the device's display orientation.
    ///
    /// This component is particularly useful for image processing tasks in Unity, such as computer vision applications,
    /// where real-time camera input in <c>Mat</c> format is required. It enables seamless integration of OpenCV-based
    /// image processing algorithms with Unity's camera input.
    ///
    /// <strong>Note:</strong> By setting outputColorFormat to RGBA, processing that does not include extra color conversion is performed.
    ///
    /// <strong>WebGPU Limitation:</strong> This helper class is not compatible with WebGPU due to synchronous pixel readback limitations.
    /// Please use <see cref="WebCamTexture2MatAsyncGPUHelper"/> for WebGPU projects.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the latest camera frame in <c>Mat</c> format.
    /// The helper class manages camera start/stop operations and frame updates internally.
    /// </example>
    public class WebCamTexture2MatHelper : MonoBehaviour, ICameraSource2MatHelper, IMatTransformationProvider
    {

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the name of the device to use. (or device index number)")]
        protected string _requestedDeviceName = null;

        /// <summary>
        /// Set the name of the camera device to use. (or device index number)
        /// </summary>
        public virtual string RequestedDeviceName
        {
            get { return _requestedDeviceName; }
            set
            {
                if (_requestedDeviceName != value)
                {
                    _requestedDeviceName = value;
                    if (hasInitDone)
                        Initialize(IsPlaying());
                    else if (isInitWaiting)
                        Initialize(autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the width of camera.")]
        protected int _requestedWidth = 640;

        /// <summary>
        /// Set the width of camera.
        /// </summary>
        public virtual int RequestedWidth
        {
            get { return _requestedWidth; }
            set
            {
                int _value = (int)Mathf.Clamp(value, 0f, float.MaxValue);
                if (_requestedWidth != _value)
                {
                    _requestedWidth = _value;
                    if (hasInitDone)
                        Initialize(IsPlaying());
                    else if (isInitWaiting)
                        Initialize(autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the height of camera.")]
        protected int _requestedHeight = 480;

        /// <summary>
        /// Set the height of camera.
        /// </summary>
        public virtual int RequestedHeight
        {
            get { return _requestedHeight; }
            set
            {
                int _value = (int)Mathf.Clamp(value, 0f, float.MaxValue);
                if (_requestedHeight != _value)
                {
                    _requestedHeight = _value;
                    if (hasInitDone)
                        Initialize(IsPlaying());
                    else if (isInitWaiting)
                        Initialize(autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set whether to use the front facing camera.")]
        protected bool _requestedIsFrontFacing = false;

        /// <summary>
        /// Set whether to use the front facing camera.
        /// </summary>
        public virtual bool RequestedIsFrontFacing
        {
            get { return _requestedIsFrontFacing; }
            set
            {
                if (_requestedIsFrontFacing != value)
                {
                    _requestedIsFrontFacing = value;
                    if (hasInitDone)
                        Initialize(RequestedIsFrontFacing, RequestedFPS, Rotate90Degree, IsPlaying());
                    else if (isInitWaiting)
                        Initialize(RequestedIsFrontFacing, RequestedFPS, Rotate90Degree, autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the frame rate of camera.")]
        protected float _requestedFPS = 30f;

        /// <summary>
        /// Set the frame rate of camera.
        /// </summary>
        public virtual float RequestedFPS
        {
            get { return _requestedFPS; }
            set
            {
                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
                if (_requestedFPS != _value)
                {
                    _requestedFPS = _value;
                    if (hasInitDone)
                    {
                        bool isPlaying = IsPlaying();
                        Stop();
                        webCamTexture.requestedFPS = _requestedFPS;
                        if (isPlaying)
                            Play();
                    }
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set whether to rotate camera frame 90 degrees. (clockwise)")]
        protected bool _rotate90Degree = false;

        /// <summary>
        /// Set whether to rotate camera frame 90 degrees. (clockwise)
        /// </summary>
        public virtual bool Rotate90Degree
        {
            get { return _rotate90Degree; }
            set
            {
                if (_rotate90Degree != value)
                {
                    _rotate90Degree = value;
                    if (hasInitDone)
                        Initialize(IsPlaying());
                    else if (isInitWaiting)
                        Initialize(autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set whether to flip vertically.")]
        protected bool _flipVertical = false;

        /// <summary>
        /// Set whether to flip vertically.
        /// </summary>
        public virtual bool FlipVertical
        {
            get { return _flipVertical; }
            set { _flipVertical = value; }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set whether to flip horizontal.")]
        protected bool _flipHorizontal = false;

        /// <summary>
        /// Set whether to flip horizontal.
        /// </summary>
        public virtual bool FlipHorizontal
        {
            get { return _flipHorizontal; }
            set { _flipHorizontal = value; }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Select the output color format.")]
        protected Source2MatHelperColorFormat _outputColorFormat = Source2MatHelperColorFormat.RGBA;

        /// <summary>
        /// Select the output color format.
        /// </summary>
        public virtual Source2MatHelperColorFormat OutputColorFormat
        {
            get { return _outputColorFormat; }
            set
            {
                if (_outputColorFormat != value)
                {
                    _outputColorFormat = value;
                    if (hasInitDone)
                        Initialize(IsPlaying());
                    else if (isInitWaiting)
                        Initialize(autoPlayAfterInitialize);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("The number of frames before the initialization process times out.")]
        protected int _timeoutFrameCount = 1500;

        /// <summary>
        /// The number of frames before the initialization process times out.
        /// </summary>
        public virtual int TimeoutFrameCount
        {
            get { return _timeoutFrameCount; }
            set { _timeoutFrameCount = (int)Mathf.Clamp(value, 0f, float.MaxValue); }
        }


        [SerializeField, Tooltip("UnityEvent that is triggered when this instance is initialized.")]
        protected UnityEvent _onInitialized;

        /// <summary>
        /// UnityEvent that is triggered when this instance is initialized.
        /// </summary>
        public UnityEvent OnInitialized
        {
            get => _onInitialized;
            set => _onInitialized = value;
        }


        [SerializeField, Tooltip("UnityEvent that is triggered when this instance is disposed.")]
        protected UnityEvent _onDisposed;

        /// <summary>
        /// UnityEvent that is triggered when this instance is disposed.
        /// </summary>
        public UnityEvent OnDisposed
        {
            get => _onDisposed;
            set => _onDisposed = value;
        }


        [SerializeField, Tooltip("UnityEvent that is triggered when this instance is error Occurred.")]
        protected Source2MatHelperErrorUnityEvent _onErrorOccurred;

        /// <summary>
        /// UnityEvent that is triggered when this instance is error Occurred.
        /// </summary>
        public Source2MatHelperErrorUnityEvent OnErrorOccurred
        {
            get => _onErrorOccurred;
            set => _onErrorOccurred = value;
        }

        /// <summary>
        /// The active WebcamTexture.
        /// </summary>
        protected WebCamTexture webCamTexture;

        /// <summary>
        /// Internal playing state flag to track camera playback status immediately.
        /// </summary>
        protected bool isPlaying = false;

        /// <summary>
        /// Internal paused state flag to track camera pause status immediately.
        /// </summary>
        protected bool isPaused = false;

        /// <summary>
        /// The active WebcamDevice.
        /// </summary>
        protected WebCamDevice webCamDevice;

        /// <summary>
        /// The frame mat.
        /// </summary>
        protected Mat frameMat;

        /// <summary>
        /// The base mat.
        /// </summary>
        protected Mat baseMat;

        /// <summary>
        /// The rotated frame mat
        /// </summary>
        protected Mat rotatedFrameMat;

        /// <summary>
        /// The buffer colors.
        /// </summary>
        protected Color32[] colors;

        /// <summary>
        /// The base color format.
        /// </summary>
        protected Source2MatHelperColorFormat baseColorFormat = Source2MatHelperColorFormat.RGBA;

        /// <summary>
        /// Indicates whether this instance is waiting for initialization to complete.
        /// </summary>
        protected bool isInitWaiting = false;

        /// <summary>
        /// Indicates whether this instance has been initialized.
        /// </summary>
        protected bool hasInitDone = false;

        /// <summary>
        /// The initialization coroutine.
        /// </summary>
        protected IEnumerator initCoroutine;

        /// <summary>
        /// The orientation of the screen.
        /// </summary>
        protected ScreenOrientation screenOrientation;

        /// <summary>
        /// Is ScreenOrientation the correct value?
        /// </summary>
        protected bool isScreenOrientationCorrect;

        /// <summary>
        /// Is VideoRotationAngle the correct value?
        /// </summary>
        protected bool isVideoRotationAngleCorrect;

        /// <summary>
        /// The width of the screen.
        /// </summary>
        protected int screenWidth;

        /// <summary>
        /// The height of the screen.
        /// </summary>
        protected int screenHeight;

        /// <summary>
        /// If set to true play after completion of initialization.
        /// </summary>
        protected bool autoPlayAfterInitialize;

        /// <summary>
        /// isScreenSizeChangeWaiting
        /// </summary>
        protected bool isScreenSizeChangeWaiting = false;

        protected virtual void OnValidate()
        {
            _requestedWidth = (int)Mathf.Clamp(_requestedWidth, 0f, float.MaxValue);
            _requestedHeight = (int)Mathf.Clamp(_requestedHeight, 0f, float.MaxValue);
            _requestedFPS = Mathf.Clamp(_requestedFPS, -1f, float.MaxValue);
            _timeoutFrameCount = (int)Mathf.Clamp(_timeoutFrameCount, 0f, float.MaxValue);
        }

        /// <summary>
        /// Is ScreenOrientation the correct value?
        /// </summary>
        /// <returns>True if ScreenOrientation is the correct value; otherwise, false.</returns>
        protected virtual bool IsScreenOrientationCorrect()
        {
#if UNITY_EDITOR
            if (DeviceSimulatorUtils.IsDeviceSimulatorActive())
                return true;

            return false;
#elif UNITY_STANDALONE
            return false;
#else
            return true;
#endif
        }

        /// <summary>
        /// Is VideoRotationAngle the correct value?
        /// </summary>
        /// <returns>True if VideoRotationAngle is the correct value; otherwise, false.</returns>
        protected virtual bool IsVideoRotationAngleCorrect()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            return false;
#else
            return true;
#endif
        }

        /// <summary>
        /// Determines whether the RotatedFrameMat is needed.
        /// </summary>
        /// <returns>True if RotatedFrameMat is needed; otherwise, false.</returns>
        protected virtual bool NeedsRotatedFrameMat(bool isScreenOrientationCorrect)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
            {
                return true;
            }
            else if (Rotate90Degree)
            {
                return true;
            }
            return false;
#else
            if (isScreenOrientationCorrect)
            {
                if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
                {
                    if (!Rotate90Degree)
                        return true;
                }
                else if (Rotate90Degree)
                {
                    return true;
                }
            }
            else
            {
                if (Rotate90Degree)
                    return true;
            }

            return false;
#endif
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (hasInitDone)
            {
                // Catch the orientation change of the screen and correct the mat image to the correct direction.
                if (screenOrientation != Screen.orientation)
                {
                    // Wait one frame until the Screen.width/Screen.height property changes.
                    if (!isScreenSizeChangeWaiting)
                    {
                        isScreenSizeChangeWaiting = true;
                        return;
                    }
                    isScreenSizeChangeWaiting = false;

                    if (_onDisposed != null)
                        _onDisposed.Invoke();

                    if (rotatedFrameMat != null)
                    {
                        rotatedFrameMat.Dispose();
                        rotatedFrameMat = null;
                    }

                    screenOrientation = Screen.orientation;
                    screenWidth = Screen.width;
                    screenHeight = Screen.height;

                    isScreenOrientationCorrect = IsScreenOrientationCorrect();
                    isVideoRotationAngleCorrect = IsVideoRotationAngleCorrect();

                    if (NeedsRotatedFrameMat(isScreenOrientationCorrect))
                        rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)), new Scalar(0, 0, 0, 255));

                    if (_onInitialized != null)
                        _onInitialized.Invoke();
                }
            }
        }

        protected virtual IEnumerator OnApplicationFocus(bool hasFocus)
        {
#if ((UNITY_IOS || UNITY_WEBGL) && UNITY_2018_1_OR_NEWER) || (UNITY_ANDROID && UNITY_2018_3_OR_NEWER)
            yield return null;

            if (isUserRequestingPermission && hasFocus)
                isUserRequestingPermission = false;
#endif
            yield break;
        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        protected virtual void OnDestroy()
        {
            Dispose();
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(bool autoPlay = true)
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            autoPlayAfterInitialize = autoPlay;
            if (_onInitialized == null)
                _onInitialized = new UnityEvent();
            if (_onDisposed == null)
                _onDisposed = new UnityEvent();
            if (_onErrorOccurred == null)
                _onErrorOccurred = new Source2MatHelperErrorUnityEvent();

            initCoroutine = _Initialize();
            StartCoroutine(initCoroutine);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="requestedWidth">Requested width.</param>
        /// <param name="requestedHeight">Requested height.</param>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(int requestedWidth, int requestedHeight, bool autoPlay = true)
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            _requestedWidth = requestedWidth;
            _requestedHeight = requestedHeight;
            autoPlayAfterInitialize = autoPlay;
            if (_onInitialized == null)
                _onInitialized = new UnityEvent();
            if (_onDisposed == null)
                _onDisposed = new UnityEvent();
            if (_onErrorOccurred == null)
                _onErrorOccurred = new Source2MatHelperErrorUnityEvent();

            initCoroutine = _Initialize();
            StartCoroutine(initCoroutine);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="requestedIsFrontFacing">If set to <c>true</c> requested to using the front camera.</param>
        /// <param name="requestedFPS">Requested FPS.</param>
        /// <param name="rotate90Degree">If set to <c>true</c> requested to rotate camera frame 90 degrees. (clockwise)</param>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(bool requestedIsFrontFacing, float requestedFPS = 30f, bool rotate90Degree = false, bool autoPlay = true)
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            _requestedDeviceName = null;
            _requestedIsFrontFacing = requestedIsFrontFacing;
            _requestedFPS = requestedFPS;
            _rotate90Degree = rotate90Degree;
            autoPlayAfterInitialize = autoPlay;
            if (_onInitialized == null)
                _onInitialized = new UnityEvent();
            if (_onDisposed == null)
                _onDisposed = new UnityEvent();
            if (_onErrorOccurred == null)
                _onErrorOccurred = new Source2MatHelperErrorUnityEvent();

            initCoroutine = _Initialize();
            StartCoroutine(initCoroutine);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="deviceName">Device name.</param>
        /// <param name="requestedWidth">Requested width.</param>
        /// <param name="requestedHeight">Requested height.</param>
        /// <param name="requestedIsFrontFacing">If set to <c>true</c> requested to using the front camera.</param>
        /// <param name="requestedFPS">Requested FPS.</param>
        /// <param name="rotate90Degree">If set to <c>true</c> requested to rotate camera frame 90 degrees. (clockwise)</param>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(string deviceName, int requestedWidth, int requestedHeight, bool requestedIsFrontFacing = false, float requestedFPS = 30f, bool rotate90Degree = false, bool autoPlay = true)
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            _requestedDeviceName = deviceName;
            _requestedWidth = requestedWidth;
            _requestedHeight = requestedHeight;
            _requestedIsFrontFacing = requestedIsFrontFacing;
            _requestedFPS = requestedFPS;
            _rotate90Degree = rotate90Degree;
            autoPlayAfterInitialize = autoPlay;
            if (_onInitialized == null)
                _onInitialized = new UnityEvent();
            if (_onDisposed == null)
                _onDisposed = new UnityEvent();
            if (_onErrorOccurred == null)
                _onErrorOccurred = new Source2MatHelperErrorUnityEvent();

            initCoroutine = _Initialize();
            StartCoroutine(initCoroutine);
        }

        /// <summary>
        /// Initialize this instance by coroutine.
        /// </summary>
        protected virtual IEnumerator _Initialize()
        {
            if (hasInitDone)
            {
                ReleaseResources();

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }

            isInitWaiting = true;

            // Wait one frame before starting initialization process
            yield return null;


#if (UNITY_IOS || UNITY_WEBGL || UNITY_ANDROID) && !UNITY_EDITOR
            // Checks camera permission state.
            IEnumerator coroutine = hasUserAuthorizedCameraPermission();
            yield return coroutine;

            if (!(bool)coroutine.Current)
            {
                isInitWaiting = false;
                initCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.CAMERA_PERMISSION_DENIED, string.Empty);

                yield break;
            }
#endif


            // Creates a WebCamTexture with settings closest to the requested name, resolution, and frame rate.
            var devices = WebCamTexture.devices;
            if (devices.Length == 0)
            {
                isInitWaiting = false;
                initCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.CAMERA_DEVICE_NOT_EXIST, RequestedDeviceName);

                yield break;
            }

            if (!String.IsNullOrEmpty(RequestedDeviceName))
            {
                // Try to parse requestedDeviceName as an index
                int requestedDeviceIndex = -1;
                if (Int32.TryParse(RequestedDeviceName, out requestedDeviceIndex))
                {
                    if (requestedDeviceIndex >= 0 && requestedDeviceIndex < devices.Length)
                    {
                        webCamDevice = devices[requestedDeviceIndex];
                        if (RequestedFPS < 0)
                            webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight);
                        else
                            webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight, (int)RequestedFPS);
                    }
                }
                else
                {
                    // Search for a device with a matching name
                    for (int cameraIndex = 0; cameraIndex < devices.Length; cameraIndex++)
                    {
                        if (devices[cameraIndex].name == RequestedDeviceName)
                        {
                            webCamDevice = devices[cameraIndex];
                            if (RequestedFPS < 0)
                                webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight);
                            else
                                webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight, (int)RequestedFPS);
                            break;
                        }
                    }
                }
                if (webCamTexture == null)
                    Debug.Log("WebCamTexture2MatHelper:: " + "Cannot find camera device " + RequestedDeviceName + ".");
            }

            if (webCamTexture == null)
            {
                var prioritizedKinds = new WebCamKind[]
                {
                    WebCamKind.WideAngle,
                    WebCamKind.Telephoto,
                    WebCamKind.UltraWideAngle,
                    WebCamKind.ColorAndDepth
                };

                // Checks how many and which cameras are available on the device
                foreach (var kind in prioritizedKinds)
                {
                    foreach (var device in devices)
                    {
                        if (device.kind == kind && device.isFrontFacing == RequestedIsFrontFacing)
                        {
                            webCamDevice = device;
                            if (RequestedFPS < 0)
                                webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight);
                            else
                                webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight, (int)RequestedFPS);
                            break;
                        }
                    }
                    if (webCamTexture != null) break;
                }
            }

            if (webCamTexture == null)
            {
                webCamDevice = devices[0];
                if (RequestedFPS < 0)
                    webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight);
                else
                    webCamTexture = new WebCamTexture(webCamDevice.name, RequestedWidth, RequestedHeight, (int)RequestedFPS);
            }


            // Starts the camera
            webCamTexture.Play();

            int initFrameCount = 0;
            bool isTimeout = false;

            while (true)
            {
                if (initFrameCount > TimeoutFrameCount)
                {
                    isTimeout = true;
                    break;
                }
                else if (webCamTexture.didUpdateThisFrame)
                {
                    Debug.Log("WebCamTexture2MatHelper:: " + "devicename:" + webCamTexture.deviceName + " name:" + webCamTexture.name + " width:" + webCamTexture.width + " height:" + webCamTexture.height + " fps:" + webCamTexture.requestedFPS
                    + " videoRotationAngle:" + webCamTexture.videoRotationAngle + " videoVerticallyMirrored:" + webCamTexture.videoVerticallyMirrored + " isFrongFacing:" + webCamDevice.isFrontFacing);

                    if (colors == null || colors.Length != webCamTexture.width * webCamTexture.height)
                        colors = new Color32[webCamTexture.width * webCamTexture.height];

                    baseMat = new Mat(webCamTexture.height, webCamTexture.width, CvType.CV_8UC4, new Scalar(0, 0, 0, 255));

                    if (baseColorFormat == OutputColorFormat)
                    {
                        //frameMat = baseMat;
                        frameMat = baseMat.clone();
                    }
                    else
                    {
                        frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)), new Scalar(0, 0, 0, 255));
                    }

                    screenOrientation = Screen.orientation;
                    screenWidth = Screen.width;
                    screenHeight = Screen.height;

                    isScreenOrientationCorrect = IsScreenOrientationCorrect();
                    isVideoRotationAngleCorrect = IsVideoRotationAngleCorrect();

                    if (NeedsRotatedFrameMat(isScreenOrientationCorrect))
                        rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)), new Scalar(0, 0, 0, 255));

                    isInitWaiting = false;
                    hasInitDone = true;
                    initCoroutine = null;

                    if (!autoPlayAfterInitialize)
                    {
                        webCamTexture.Stop();
                        isPlaying = false;
                        isPaused = false;
                    }
                    else
                    {
                        isPlaying = true;
                        isPaused = false;
                    }

                    if (_onInitialized != null)
                        _onInitialized.Invoke();

                    break;
                }
                else
                {
                    initFrameCount++;
                    yield return null;
                }
            }

            if (isTimeout)
            {
                webCamTexture.Stop();
                webCamTexture = null;
                isInitWaiting = false;
                initCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.TIMEOUT, string.Empty);
            }
        }

        /// <summary>
        /// Check camera permission state by coroutine.
        /// </summary>
        protected virtual IEnumerator hasUserAuthorizedCameraPermission()
        {
#if (UNITY_IOS || UNITY_WEBGL) && UNITY_2018_1_OR_NEWER
            UserAuthorization mode = UserAuthorization.WebCam;
            if (!Application.HasUserAuthorization(mode))
            {
                yield return RequestUserAuthorization(mode);
            }
            yield return Application.HasUserAuthorization(mode);
#elif UNITY_ANDROID && UNITY_2018_3_OR_NEWER
            string permission = UnityEngine.Android.Permission.Camera;
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission))
            {
                yield return RequestUserPermission(permission);
            }
            yield return UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission);
#else
            yield return true;
#endif
        }

#if ((UNITY_IOS || UNITY_WEBGL) && UNITY_2018_1_OR_NEWER) || (UNITY_ANDROID && UNITY_2018_3_OR_NEWER)
        protected bool isUserRequestingPermission;
#endif

#if (UNITY_IOS || UNITY_WEBGL) && UNITY_2018_1_OR_NEWER
        protected virtual IEnumerator RequestUserAuthorization(UserAuthorization mode)
        {
            isUserRequestingPermission = true;
            yield return Application.RequestUserAuthorization(mode);

            float timeElapsed = 0;
            while (isUserRequestingPermission)
            {
                if (timeElapsed > 0.25f)
                {
                    isUserRequestingPermission = false;
                    yield break;
                }
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            yield break;
        }
#elif UNITY_ANDROID && UNITY_2018_3_OR_NEWER
        protected virtual IEnumerator RequestUserPermission(string permission)
        {
            isUserRequestingPermission = true;
            UnityEngine.Android.Permission.RequestUserPermission(permission);

            float timeElapsed = 0;
            while (isUserRequestingPermission)
            {
                if (timeElapsed > 0.25f)
                {
                    isUserRequestingPermission = false;
                    yield break;
                }
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            yield break;
        }
#endif

        /// <summary>
        /// Indicate whether this instance has been initialized.
        /// </summary>
        /// <returns><c>true</c>, if this instance has been initialized, <c>false</c> otherwise.</returns>
        public virtual bool IsInitialized()
        {
            return hasInitDone;
        }

        /// <summary>
        /// Start the active camera.
        /// </summary>
        public virtual void Play()
        {
            if (hasInitDone)
            {
                webCamTexture.Play();
                isPlaying = true;
                isPaused = false; // Resume from paused state or start from stopped state
            }
        }

        /// <summary>
        /// Pause the active camera.
        /// </summary>
        public virtual void Pause()
        {
            if (hasInitDone)
            {
                webCamTexture.Pause();
                // Only change state if currently playing
                if (isPlaying)
                {
                    isPlaying = true;
                    isPaused = true;
                }
                // If stopped, no state change
            }
        }

        /// <summary>
        /// Stop the active camera.
        /// </summary>
        public virtual void Stop()
        {
            if (hasInitDone)
            {
                webCamTexture.Stop();
                isPlaying = false;
                isPaused = false; // Reset to stopped state
            }
        }

        /// <summary>
        /// Indicate whether the active camera is currently playing.
        /// </summary>
        /// <returns><c>true</c>, if the active camera is playing, <c>false</c> otherwise.</returns>
        public virtual bool IsPlaying()
        {
            return hasInitDone ? isPlaying : false;
        }

        /// <summary>
        /// Indicate whether the camera is paused.
        /// </summary>
        /// <returns><c>true</c>, if the active camera is paused, <c>false</c> otherwise.</returns>
        public virtual bool IsPaused()
        {
            return hasInitDone ? isPaused : false;
        }

        /// <summary>
        /// Indicate whether the active camera device is currently front facng.
        /// </summary>
        /// <returns><c>true</c>, if the active camera device is front facng, <c>false</c> otherwise.</returns>
        public virtual bool IsFrontFacing()
        {
            return hasInitDone ? webCamDevice.isFrontFacing : false;
        }

        /// <summary>
        /// Return the active camera device name.
        /// </summary>
        /// <returns>The active camera device name.</returns>
        public virtual string GetDeviceName()
        {
            return hasInitDone ? webCamTexture.deviceName : "";
        }

        /// <summary>
        /// Return the active camera width.
        /// </summary>
        /// <returns>The active camera width.</returns>
        public virtual int GetWidth()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.height() : frameMat.width();
        }

        /// <summary>
        /// Return the active camera height.
        /// </summary>
        /// <returns>The active camera height.</returns>
        public virtual int GetHeight()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.width() : frameMat.height();
        }

        /// <summary>
        /// Return the active camera framerate.
        /// </summary>
        /// <returns>The active camera framerate.</returns>
        public virtual float GetFPS()
        {
            return hasInitDone ? webCamTexture.requestedFPS : -1f;
        }

        /// <summary>
        /// Return the active WebcamTexture.
        /// </summary>
        /// <returns>The active WebcamTexture.</returns>
        public virtual WebCamTexture GetWebCamTexture()
        {
            return hasInitDone ? webCamTexture : null;
        }

        /// <summary>
        /// Return the active WebcamDevice.
        /// </summary>
        /// <returns>The active WebcamDevice.</returns>
        public virtual WebCamDevice GetWebCamDevice()
        {
            return webCamDevice;
        }

        /// <summary>
        /// Return the camera to world matrix.
        /// </summary>
        /// <returns>The camera to world matrix.</returns>
        public virtual Matrix4x4 GetCameraToWorldMatrix()
        {
            return Camera.main.cameraToWorldMatrix;
        }

        /// <summary>
        /// Return the projection matrix matrix.
        /// </summary>
        /// <returns>The projection matrix.</returns>
        public virtual Matrix4x4 GetProjectionMatrix()
        {
            return Camera.main.projectionMatrix;
        }

        /// <summary>
        /// Return the video base color format.
        /// </summary>
        /// <returns>The video base color format.</returns>
        public virtual Source2MatHelperColorFormat GetBaseColorFormat()
        {
            return baseColorFormat;
        }

        /// <summary>
        /// Use this to check if the Mat has changed since the last frame. Since it would not make sense to do expensive video processing in each Update call, check this value before doing any processing.
        /// </summary>
        /// <returns><c>true</c>, if the Mat has been updated <c>false</c> otherwise.</returns>
        public virtual bool DidUpdateThisFrame()
        {
            if (!hasInitDone)
                return false;

            return webCamTexture.didUpdateThisFrame;
        }

        /// <summary>
        /// Get the mat of the current frame.
        /// </summary>
        /// <remarks>
        /// The Mat object's type is 'CV_8UC4' or 'CV_8UC3' or 'CV_8UC1' (ColorFormat is determined by the outputColorFormat setting).
        /// Please do not dispose of the returned mat as it will be reused.
        /// </remarks>
        /// <returns>The mat of the current frame.</returns>
        public virtual Mat GetMat()
        {
            if (!hasInitDone)
            {
                return null;
            }

            if (frameMat != null && frameMat.IsDisposed)
            {
                Debug.LogWarning("WebCamTexture2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
            }
            if (rotatedFrameMat != null && rotatedFrameMat.IsDisposed)
            {
                Debug.LogWarning("WebCamTexture2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
            }

            if (baseColorFormat == OutputColorFormat)
            {
                if (webCamTexture.isPlaying)
                    OpenCVMatUtils.WebCamTextureToMat(webCamTexture, baseMat, colors, false);
                baseMat.copyTo(frameMat);
            }
            else
            {
                if (webCamTexture.isPlaying)
                    OpenCVMatUtils.WebCamTextureToMat(webCamTexture, baseMat, colors, false);
                Imgproc.cvtColor(baseMat, frameMat, Source2MatHelperUtils.ColorConversionCodes(baseColorFormat, OutputColorFormat));
            }

            return FlipAndRotateMat(frameMat, rotatedFrameMat, FlipVertical, FlipHorizontal, webCamDevice.isFrontFacing, webCamTexture.videoRotationAngle, screenOrientation, isScreenOrientationCorrect, isVideoRotationAngleCorrect);
        }

        /// <summary>
        /// Flips and rotates the Mat according to the specified parameters.
        /// </summary>
        /// <param name="frameMat">The source Mat to flip and rotate</param>
        /// <param name="rotatedFrameMat">The rotated Mat output</param>
        /// <param name="flipVertical">Whether to flip vertically</param>
        /// <param name="flipHorizontal">Whether to flip horizontally</param>
        /// <param name="isFrontFacing">Whether the camera is front-facing</param>
        /// <param name="videoRotationAngle">The video rotation angle</param>
        /// <param name="screenOrientation">The screen orientation</param>
        /// <param name="isScreenOrientationCorrect">Whether the screen orientation is correct</param>
        /// <param name="isVidevideoRotationAngleCorrect">Whether the video rotation angle is correct</param>
        /// <returns>The flipped and rotated Mat</returns>
        protected virtual Mat FlipAndRotateMat(Mat frameMat, Mat rotatedFrameMat, bool flipVertical, bool flipHorizontal, bool isFrontFacing, int videoRotationAngle, ScreenOrientation screenOrientation, bool isScreenOrientationCorrect, bool isVidevideoRotationAngleCorrect)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            // For WebGL, use Imgproc.resize() to copy data to rotatedFrameMat as a special case
            FlipMat(frameMat, flipVertical, flipHorizontal, isFrontFacing, videoRotationAngle);

            // Early return for landscape orientation when no rotation needed
            bool isPortrait = (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown);
            if (!isPortrait)
            {
                if (rotatedFrameMat != null)
                {
                    Core.rotate(frameMat, rotatedFrameMat, Core.ROTATE_90_CLOCKWISE);
                    return rotatedFrameMat;
                }
                return frameMat;
            }

            // Portrait orientation handling
            if (Rotate90Degree)
            {
                Imgproc.resize(frameMat, rotatedFrameMat, rotatedFrameMat.size());
                Core.rotate(rotatedFrameMat, frameMat, Core.ROTATE_90_CLOCKWISE);
                return frameMat;
            }
            else
            {
                Imgproc.resize(frameMat, rotatedFrameMat, rotatedFrameMat.size());
                return rotatedFrameMat;
            }
#else
            if (isScreenOrientationCorrect && isVidevideoRotationAngleCorrect)
            {
                if (rotatedFrameMat != null)
                {
                    if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
                    {
                        // (Orientation is Portrait, rotate90Degree is false)
                        if (webCamDevice.isFrontFacing)
                        {
                            FlipMat(frameMat, !flipHorizontal, !flipVertical, isFrontFacing, videoRotationAngle);
                        }
                        else
                        {
                            FlipMat(frameMat, flipHorizontal, flipVertical, isFrontFacing, videoRotationAngle);
                        }
                    }
                    else
                    {
                        // (Orientation is Landscape, rotate90Degrees=true)
                        FlipMat(frameMat, flipVertical, flipHorizontal, isFrontFacing, videoRotationAngle);
                    }
                    Core.rotate(frameMat, rotatedFrameMat, Core.ROTATE_90_CLOCKWISE);
                    return rotatedFrameMat;
                }
                else
                {
                    if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
                    {
                        // (Orientation is Portrait, rotate90Degree is true)
                        if (webCamDevice.isFrontFacing)
                        {
                            FlipMat(frameMat, flipHorizontal, flipVertical, isFrontFacing, videoRotationAngle);
                        }
                        else
                        {
                            FlipMat(frameMat, !flipHorizontal, !flipVertical, isFrontFacing, videoRotationAngle);
                        }
                    }
                    else
                    {
                        // (Orientation is Landscape, rotate90Degree is false)
                        FlipMat(frameMat, flipVertical, flipHorizontal, isFrontFacing, videoRotationAngle);
                    }
                    return frameMat;
                }
            }
            else
            {
                FlipMat(frameMat, flipVertical, flipHorizontal, isFrontFacing, videoRotationAngle);
                if (rotatedFrameMat != null)
                {
                    Core.rotate(frameMat, rotatedFrameMat, Core.ROTATE_90_CLOCKWISE);
                    return rotatedFrameMat;
                }
                else
                {
                    return frameMat;
                }
            }
#endif
        }

        /// <summary>
        /// Flips the input Mat according to the specified parameters.
        /// The initial flipCode is set to 0 (vertical flip) because the pixel order of WebCamTexture and Mat is reversed.
        /// </summary>
        /// <param name="mat">The Mat to be flipped</param>
        /// <param name="flipVertical">Whether to flip vertically</param>
        /// <param name="flipHorizontal">Whether to flip horizontally</param>
        /// <param name="isFrontFacing">Whether the camera is front-facing</param>
        /// <param name="videoRotationAngle">Video rotation angle (0, 90, 180, or 270 degrees)</param>
        protected virtual void FlipMat(Mat mat, bool flipVertical, bool flipHorizontal, bool isFrontFacing, int videoRotationAngle)
        {
            // Flip code constants for OpenCV Core.flip method
            const int FLIP_NONE = 0;           // No flip
            const int FLIP_VERTICAL = 1;       // Flip vertically
            const int FLIP_HORIZONTAL = -1;    // Flip horizontally
            const int FLIP_BOTH = int.MinValue; // Flip both vertically and horizontally

            // Since the order of pixels of WebCamTexture and Mat is opposite, the initial value of flipCode is set to 0 (flipVertical).
            int flipCode = FLIP_NONE;

            if (isFrontFacing)
            {
                if (videoRotationAngle == 0 || videoRotationAngle == 90)
                {
                    flipCode = FLIP_HORIZONTAL;
                }
                else if (videoRotationAngle == 180 || videoRotationAngle == 270)
                {
                    flipCode = FLIP_BOTH;
                }
            }
            else
            {
                if (videoRotationAngle == 180 || videoRotationAngle == 270)
                {
                    flipCode = FLIP_VERTICAL;
                }
            }

            if (flipVertical)
            {
                if (flipCode == FLIP_BOTH)
                {
                    flipCode = FLIP_NONE;
                }
                else if (flipCode == FLIP_NONE)
                {
                    flipCode = FLIP_BOTH;
                }
                else if (flipCode == FLIP_VERTICAL)
                {
                    flipCode = FLIP_HORIZONTAL;
                }
                else if (flipCode == FLIP_HORIZONTAL)
                {
                    flipCode = FLIP_VERTICAL;
                }
            }

            if (flipHorizontal)
            {
                if (flipCode == FLIP_BOTH)
                {
                    flipCode = FLIP_VERTICAL;
                }
                else if (flipCode == FLIP_NONE)
                {
                    flipCode = FLIP_HORIZONTAL;
                }
                else if (flipCode == FLIP_VERTICAL)
                {
                    flipCode = FLIP_BOTH;
                }
                else if (flipCode == FLIP_HORIZONTAL)
                {
                    flipCode = FLIP_NONE;
                }
            }

            if (flipCode > FLIP_BOTH)
            {
                Core.flip(mat, mat, flipCode);
            }
        }

        /// <summary>
        /// Get the buffer colors.
        /// </summary>
        /// <returns>The buffer colors.</returns>
        public virtual Color32[] GetBufferColors()
        {
            return colors;
        }

        /// <summary>
        /// Cancel Init Coroutine.
        /// </summary>
        protected virtual void CancelInitCoroutine()
        {
            if (initCoroutine != null)
            {
                StopCoroutine(initCoroutine);
                ((IDisposable)initCoroutine).Dispose();
                initCoroutine = null;
            }
        }

        /// <summary>
        /// To release the resources.
        /// </summary>
        protected virtual void ReleaseResources()
        {
            isInitWaiting = false;
            hasInitDone = false;

            isPlaying = false;
            isPaused = false;

            if (webCamTexture != null)
            {
                webCamTexture.Stop();
                WebCamTexture.Destroy(webCamTexture);
                webCamTexture = null;
            }
            if (frameMat != null)
            {
                frameMat.Dispose();
                frameMat = null;
            }
            if (baseMat != null)
            {
                baseMat.Dispose();
                baseMat = null;
            }
            if (rotatedFrameMat != null)
            {
                rotatedFrameMat.Dispose();
                rotatedFrameMat = null;
            }
            if (colors != null)
                colors = null;
        }

        /// <summary>
        /// Releases all resource used by the <see cref="WebCamTexture2MatHelper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="WebCamTexture2MatHelper"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="WebCamTexture2MatHelper"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="WebCamTexture2MatHelper"/> so
        /// the garbage collector can reclaim the memory that the <see cref="WebCamTexture2MatHelper"/> was occupying.</remarks>
        public virtual void Dispose()
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }
            else if (hasInitDone)
            {
                ReleaseResources();

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }
        }
    }
}

#endif
