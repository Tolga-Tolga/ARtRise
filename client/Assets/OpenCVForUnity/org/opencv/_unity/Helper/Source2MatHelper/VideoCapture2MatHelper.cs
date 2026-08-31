using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.VideoioModule;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using OpenCVForUnity.UnityUtils.Helper.Editor;
#endif

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// A helper component class for obtaining video frames from a file using OpenCV's <c>VideoCapture</c> and converting them to OpenCV <c>Mat</c> format.
    /// </summary>
    /// <remarks>
    /// The <c>VideoCapture2MatHelper</c> class uses OpenCV's <c>VideoCapture</c> to read frames from a specified video file and converts each frame to an OpenCV <c>Mat</c> object.
    /// This component automatically retrieves the appropriate frame based on the video's frame rate, ensuring smooth playback and accurate frame timing.
    ///
    /// To ensure compatibility across platforms, it is recommended to use the MJPEG format for video files, as it is widely supported by the <c>VideoCapture</c> class.
    ///
    /// This component is particularly useful for applications that require frame-by-frame processing of video files in Unity, allowing seamless integration with OpenCV-based image processing routines.
    ///
    /// <strong>Note:</strong> By setting outputColorFormat to BGR, processing that does not include extra color conversion is performed.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the latest video frame in <c>Mat</c> format.
    /// The helper class manages video file loading, frame retrieval, and frame updates internally based on the video's playback rate.
    /// </example>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.VideoCapture2MatHelper class instead.")]
    public class VideoCapture2MatHelper : MonoBehaviour, IVideoSource2MatHelper, IMatTransformationProvider
    {

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("requestedVideoFilePath"), TooltipAttribute("Set the video file path, relative to the starting point of the \"StreamingAssets\" folder, or absolute path.")]
        protected string _requestedVideoFilePath = string.Empty;

        /// <summary>
        /// Set the video file path, relative to the starting point of the "StreamingAssets" folder, or absolute path.
        /// </summary>
        public virtual string requestedVideoFilePath
        {
            get { return _requestedVideoFilePath; }
            set
            {
                if (_requestedVideoFilePath != value)
                {
                    _requestedVideoFilePath = value;
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
        [SerializeField, FormerlySerializedAs("apiPreference"), TooltipAttribute("Set the apiPreference. VideoCapture API backends identifier. (Advanced Option)\n" +
            "0 = CAP_ANY (Auto detect)[default]\n" +
            "1400 = CAP_MSMF (Microsoft Media Foundation (via videoInput))\n" +
            "1900 = CAP_FFMPEG (Open and record video file or stream using the FFMPEG library)\n" +
            "2200 = CAP_OPENCV_MJPEG (Built-in OpenCV MotionJPEG codec)")]
        protected int _apiPreference = Videoio.CAP_ANY;

        /// <summary>
        /// Set the apiPreference. VideoCapture API backends identifier. (Advanced Option)
        /// See ReadMe.pdf for setup instructions for using CAP_FFMPEG on Windows platforms.
        /// </summary>
        public virtual int apiPreference
        {
            get { return _apiPreference; }
            set
            {
                if (_apiPreference != value)
                {
                    _apiPreference = value;
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
        [SerializeField, FormerlySerializedAs("rotate90Degree"), TooltipAttribute("Set whether to rotate video frame 90 degrees. (clockwise)")]
        protected bool _rotate90Degree = false;

        /// <summary>
        /// Set whether to rotate video frame 90 degrees. (clockwise)
        /// </summary>
        public virtual bool rotate90Degree
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
        [SerializeField, FormerlySerializedAs("flipVertical"), TooltipAttribute("Set whether to flip vertically.")]
        protected bool _flipVertical = false;

        /// <summary>
        /// Set whether to flip vertically.
        /// </summary>
        public virtual bool flipVertical
        {
            get { return _flipVertical; }
            set { _flipVertical = value; }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("flipHorizontal"), TooltipAttribute("Set whether to flip horizontal.")]
        protected bool _flipHorizontal = false;

        /// <summary>
        /// Set whether to flip horizontal.
        /// </summary>
        public virtual bool flipHorizontal
        {
            get { return _flipHorizontal; }
            set { _flipHorizontal = value; }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("outputColorFormat"), TooltipAttribute("Select the output color format.")]
        protected Source2MatHelperColorFormat _outputColorFormat = Source2MatHelperColorFormat.BGR;

        /// <summary>
        /// Select the output color format.
        /// </summary>
        public virtual Source2MatHelperColorFormat outputColorFormat
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
        [SerializeField, FormerlySerializedAs("timeoutFrameCount"), TooltipAttribute("The number of frames before the initialization process times out.")]
        protected int _timeoutFrameCount = 1500;

        /// <summary>
        /// The number of frames before the initialization process times out.
        /// </summary>
        public virtual int timeoutFrameCount
        {
            get { return _timeoutFrameCount; }
            set { _timeoutFrameCount = (int)Mathf.Clamp(value, 0f, float.MaxValue); }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("loop"), TooltipAttribute("Indicate whether to play this video in a loop.")]
        protected bool _loop = true;

        /// <summary>
        /// Indicate whether to play this video in a loop.
        /// </summary>
        public virtual bool loop
        {
            get { lock (_sync) return _loop; }
            set { lock (_sync) _loop = value; }
        }


        [SerializeField, FormerlySerializedAs("onInitialized"), TooltipAttribute("UnityEvent that is triggered when this instance is initialized.")]
        protected UnityEvent _onInitialized;

        /// <summary>
        /// UnityEvent that is triggered when this instance is initialized.
        /// </summary>
        public UnityEvent onInitialized
        {
            get => _onInitialized;
            set => _onInitialized = value;
        }


        [SerializeField, FormerlySerializedAs("onDisposed"), TooltipAttribute("UnityEvent that is triggered when this instance is disposed.")]
        protected UnityEvent _onDisposed;

        /// <summary>
        /// UnityEvent that is triggered when this instance is disposed.
        /// </summary>
        public UnityEvent onDisposed
        {
            get => _onDisposed;
            set => _onDisposed = value;
        }


        [SerializeField, FormerlySerializedAs("onErrorOccurred"), TooltipAttribute("UnityEvent that is triggered when this instance is error Occurred.")]
        protected Source2MatHelperErrorUnityEvent _onErrorOccurred;

        /// <summary>
        /// UnityEvent that is triggered when this instance is error Occurred.
        /// </summary>
        public Source2MatHelperErrorUnityEvent onErrorOccurred
        {
            get => _onErrorOccurred;
            set => _onErrorOccurred = value;
        }


        protected System.Object _videoCaptureLockObject = new System.Object();
        protected VideoCapture _videoCapture;

        /// <summary>
        /// videoCapture
        /// </summary>
        protected VideoCapture videoCapture
        {
            get { lock (_videoCaptureLockObject) return _videoCapture; }
            set { lock (_videoCaptureLockObject) _videoCapture = value; }
        }


        protected System.Object _sync = new System.Object();
        protected bool _isPlaying = false;

        /// <summary>
        /// isPlaying
        /// </summary>
        protected virtual bool isPlaying
        {
            get { lock (_sync) return _isPlaying; }
            set { lock (_sync) _isPlaying = value; }
        }

        /// <summary>
        /// Whether the mat that can be obtained with the GetMat method has been updated in this frame.
        /// This flag is changed after waiting until WaitForEndOfFrame in the coroutine.
        /// </summary>
        protected bool didUpdateThisFrame = false;

        /// <summary>
        /// The frame mat.
        /// </summary>
        protected Mat frameMat;

        /// <summary>
        /// The base mat.
        /// </summary>
        protected Mat baseMat;

        /// <summary>
        /// The rotated frame mat.
        /// </summary>
        protected Mat rotatedFrameMat;

        protected System.Object _imageBufferMatLockObject = new System.Object();
        protected Mat _imageBufferMat;

        /// <summary>
        /// The image buffer mat.
        /// </summary>
        protected Mat imageBufferMat
        {
            get { lock (_imageBufferMatLockObject) return _imageBufferMat; }
            set { lock (_imageBufferMatLockObject) _imageBufferMat = value; }
        }

        /// <summary>
        /// The base color format.
        /// </summary>
        protected Source2MatHelperColorFormat baseColorFormat = Source2MatHelperColorFormat.BGR;

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
        /// The get file path coroutine.
        /// </summary>
        protected IEnumerator getFilePathCoroutine;

        /// <summary>
        /// If set to true play after completion of initialization.
        /// </summary>
        protected bool autoPlayAfterInitialize;

        /// <summary>
        /// FPS Manager
        /// </summary>
        private FpsManager fpsManager;

        /// <summary>
        /// cancellationTokenSource
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// cancellationToken
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        /// currentTask
        /// </summary>
        private Task currentTask;

        /// <summary>
        /// resetEvent
        /// </summary>
        private ManualResetEventSlim resetEvent;

        /// <summary>
        /// The waitForEndOfFrameCoroutine
        /// </summary>
        protected IEnumerator waitForEndOfFrameCoroutine;

        protected virtual void OnValidate()
        {
            _timeoutFrameCount = (int)Mathf.Clamp(_timeoutFrameCount, 0f, float.MaxValue);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (hasInitDone)
            {
                if (!isPlaying)
                    return;

                // Update FPSManager and execute process at regular intervals.
                fpsManager.Update(Time.deltaTime, CallReadFrame);
            }
        }

        protected virtual void CallReadFrame()
        {

#if UNITY_WEBGL

            ReadFrame();

#else
            // Skip if Task.Run is already running.
            if (currentTask == null || currentTask.IsCompleted)
            {
                // Execute the process in background thread
                currentTask = Task.Run(() =>
                {
                    //Debug.Log("task start Background thread: " + Thread.CurrentThread.ManagedThreadId);

                    try
                    {
                        resetEvent.Wait();

                        cancellationToken.ThrowIfCancellationRequested();

                        ReadFrame();

                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("Task was canceled.");
                    }

                }, cancellationToken);
            }
#endif
        }

        protected virtual void ReadFrame()
        {
            lock (_videoCaptureLockObject)
            {
                if (_videoCapture != null && !_videoCapture.IsDisposed)
                {
                    if (_videoCapture.get(Videoio.CAP_PROP_POS_FRAMES) >= _videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT))
                    {
                        if (loop)
                        {
                            _videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, 0);
                        }
                        else
                        {
                            isPlaying = false;
                        }
                    }

                    if (isPlaying)
                    {
                        lock (_imageBufferMatLockObject)
                        {
                            if (_imageBufferMat != null && !_imageBufferMat.IsDisposed)
                            {
                                _videoCapture.read(_imageBufferMat);

                                // Data update
                                resetEvent.Reset();
                            }
                        }
                    }
                }
            }
        }

        protected virtual IEnumerator _WaitForEndOfFrameCoroutine()
        {

            while (true)
            {
                yield return new WaitForEndOfFrame();

                if (!resetEvent.IsSet)
                {
                    // If the data has been updated during this frame, the data is copied to baseMat.

                    imageBufferMat.copyTo(baseMat);

                    // Data copying to baseMat completed.
                    resetEvent.Set();

                    // Set the didUpdateThisFrame flag to true only during the frame following the frame in which the baseMat is updated.
                    didUpdateThisFrame = true;
                    //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame + " " + Time.frameCount);
                }
                else
                {
                    // After one frame, the didUpdateThisFrame flag is set to false.
                    didUpdateThisFrame = false;
                    //Debug.Log("end didUpdateThisFrame " + didUpdateThisFrame + " " + Time.frameCount);
                }
            }
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
        /// <param name="requestedVideoFilePath">Requested video file path.</param>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(string requestedVideoFilePath, bool autoPlay = true)
        {
            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            _requestedVideoFilePath = requestedVideoFilePath;
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
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }

            isInitWaiting = true;

            // Wait one frame before starting initialization process
            yield return null;


            bool hasFilePathCoroutineCompleted = false;
            string fullPath = string.Empty;

            Uri uri;
            if (Uri.TryCreate(requestedVideoFilePath, UriKind.Absolute, out uri))
            {
                hasFilePathCoroutineCompleted = true;
                fullPath = uri.OriginalString;
            }
            else
            {
                getFilePathCoroutine = Utils.getFilePathCoroutine(requestedVideoFilePath, (result) =>
            {
                hasFilePathCoroutineCompleted = true;
                fullPath = result;
            });
                StartCoroutine(getFilePathCoroutine);
            }

            int initFrameCount = 0;
            bool isTimeout = false;

            while (true)
            {
                if (initFrameCount > timeoutFrameCount)
                {
                    isTimeout = true;
                    break;
                }
                else if (hasFilePathCoroutineCompleted)
                {
                    if (string.IsNullOrEmpty(fullPath))
                    {
                        videoCapture = null;
                        isInitWaiting = false;
                        initCoroutine = null;
                        getFilePathCoroutine = null;

                        if (_onErrorOccurred != null)
                            _onErrorOccurred.Invoke(Source2MatHelperErrorCode.VIDEO_FILE_NOT_EXIST, requestedVideoFilePath);

                        yield break;
                    }

                    videoCapture = new VideoCapture();
                    videoCapture.open(fullPath, apiPreference);

                    if (!videoCapture.isOpened())
                    {
                        videoCapture.Dispose();
                        videoCapture = null;
                        isInitWaiting = false;
                        initCoroutine = null;
                        getFilePathCoroutine = null;

                        if (_onErrorOccurred != null)
                            _onErrorOccurred.Invoke(Source2MatHelperErrorCode.VIDEO_FILE_CANT_OPEN, fullPath);

                        yield break;
                    }

                    imageBufferMat = new Mat();
                    videoCapture.read(imageBufferMat);
                    videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, 0);
                    videoCapture.grab();

                    baseMat = new Mat(imageBufferMat.rows(), imageBufferMat.cols(), imageBufferMat.type(), new Scalar(0, 0, 0, 255));
                    imageBufferMat.copyTo(baseMat);

                    if (baseColorFormat == outputColorFormat)
                    {
                        frameMat = baseMat;
                    }
                    else
                    {
                        frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)), new Scalar(0, 0, 0, 255));
                    }

                    // Create the rotated frame mat if needed
                    if (rotate90Degree)
                    {
                        rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
                    }

                    // Initialize FpsManager
                    double videoFPS = (videoCapture.get(Videoio.CAP_PROP_FPS) <= 0) ? 10.0 : videoCapture.get(Videoio.CAP_PROP_FPS);
                    fpsManager = new FpsManager((float)videoFPS);

                    // Create token source for cancellation
                    cancellationTokenSource = new CancellationTokenSource();
                    cancellationToken = cancellationTokenSource.Token;

                    resetEvent = new ManualResetEventSlim(true);

                    Debug.Log("VideoCaptrue2MatHelper:: " + " filePath:" + requestedVideoFilePath + " width:" + frameMat.width() + " height:" + frameMat.height() + " fps:" + videoCapture.get(Videoio.CAP_PROP_FPS));

                    isInitWaiting = false;
                    hasInitDone = true;
                    initCoroutine = null;
                    getFilePathCoroutine = null;

                    isPlaying = autoPlayAfterInitialize;

                    // Data copying to baseMat completed.
                    resetEvent.Set();

                    didUpdateThisFrame = true;
                    //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame);

                    // Start a coroutine and wait for WaitForEndOfFrame. If already started, stop and then start.
                    if (waitForEndOfFrameCoroutine != null) StopCoroutine(waitForEndOfFrameCoroutine);
                    waitForEndOfFrameCoroutine = _WaitForEndOfFrameCoroutine();
                    StartCoroutine(waitForEndOfFrameCoroutine);


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
                videoCapture = null;
                isInitWaiting = false;
                initCoroutine = null;
                getFilePathCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.TIMEOUT, string.Empty);
            }
        }

        /// <summary>
        /// Indicate whether this instance has been initialized.
        /// </summary>
        /// <returns><c>true</c>, if this instance has been initialized, <c>false</c> otherwise.</returns>
        public virtual bool IsInitialized()
        {
            return hasInitDone;
        }

        /// <summary>
        /// Start the video.
        /// </summary>
        public virtual void Play()
        {
            if (hasInitDone)
            {
                lock (_videoCaptureLockObject)
                {
                    if (_videoCapture.get(Videoio.CAP_PROP_POS_FRAMES) >= _videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT))
                    {
                        _videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, 0);
                        _videoCapture.read(baseMat);
                    }
                }

                isPlaying = true;
                fpsManager.IsPaused = false;
            }
        }

        /// <summary>
        /// Pause the video.
        /// </summary>
        public virtual void Pause()
        {
            if (hasInitDone)
            {
                isPlaying = false;
                fpsManager.IsPaused = true;
            }
        }

        /// <summary>
        /// Stop the video.
        /// </summary>
        public virtual void Stop()
        {
            if (hasInitDone)
            {
                isPlaying = false;
                fpsManager.IsPaused = true;
            }
        }

        /// <summary>
        /// Indicate whether the video is currently playing.
        /// </summary>
        /// <returns><c>true</c>, if the video is playing, <c>false</c> otherwise.</returns>
        public virtual bool IsPlaying()
        {
            return hasInitDone ? isPlaying : false;
        }

        /// <summary>
        /// Indicate whether the video is paused.
        /// </summary>
        /// <returns><c>true</c>, if the video is paused, <c>false</c> otherwise.</returns>
        public virtual bool IsPaused()
        {
            return hasInitDone ? isPlaying : false;
        }

        /// <summary>
        /// Return the active video device name.
        /// </summary>
        /// <returns>The active video device name.</returns>
        public virtual string GetDeviceName()
        {
            return "OpenCV_VideoCapture";
        }

        /// <summary>
        /// Return the video width.
        /// </summary>
        /// <returns>The video width.</returns>
        public virtual int GetWidth()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.height() : frameMat.width();
        }

        /// <summary>
        /// Return the video height.
        /// </summary>
        /// <returns>The video height.</returns>
        public virtual int GetHeight()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.width() : frameMat.height();
        }

        /// <summary>
        /// Return the video framerate.
        /// </summary>
        /// <returns>The video framerate.</returns>
        public virtual float GetFPS()
        {
            return hasInitDone ? (float)videoCapture.get(Videoio.CAP_PROP_FPS) : -1f;
        }

        /// <summary>
        /// Return the relative position of the video file: 0=start of the film, 1=end of the film.
        /// </summary>
        /// <returns>The relative position of the video file: 0=start of the film, 1=end of the film.</returns>
        public virtual float GetFramePosRatio()
        {
            if (hasInitDone)
            {
                return (float)videoCapture.get(Videoio.CAP_PROP_POS_AVI_RATIO);
            }
            else
            {
                return 0f;
            }
        }

        /// <summary>
        /// Set the relative position of the video file: 0=start of the film, 1=end of the film.
        /// </summary>
        /// <param name="ratio">The relative position of the video file: 0=start of the film, 1=end of the film.</param>
        public virtual void SetFramePosRatio(float ratio)
        {
            if (hasInitDone)
            {
                lock (_videoCaptureLockObject)
                {
                    ratio = Mathf.Clamp01(ratio);

                    bool supported = _videoCapture.set(Videoio.CAP_PROP_POS_AVI_RATIO, ratio);

                    if (!supported)
                    {
                        _videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, (int)(ratio * _videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT)));
                    }

                    if (_videoCapture.get(Videoio.CAP_PROP_POS_FRAMES) >= _videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT))
                    {
                        if (loop)
                        {
                            _videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, 0);
                        }
                        else
                        {
                            _videoCapture.set(Videoio.CAP_PROP_POS_FRAMES, (int)_videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT) - 1);
                            isPlaying = false;
                        }
                    }

                    _videoCapture.read(baseMat);
                }
            }
        }

        /// <summary>
        /// Return the video frame index.
        /// </summary>
        /// <returns>The video frame index.</returns>
        public virtual int GetFrameIndex()
        {
            if (hasInitDone)
            {
                return (int)videoCapture.get(Videoio.CAP_PROP_POS_FRAMES);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Set the video frame index.
        /// </summary>
        /// <param name="index">The video frame index.</param>
        public virtual void SetFrameIndex(int index)
        {
            if (hasInitDone)
            {
                lock (_videoCaptureLockObject)
                {
                    SetFramePosRatio((float)(index / _videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT)));
                }
            }
        }

        /// <summary>
        /// Return the number of frames in the current video content.
        /// </summary>
        /// <returns>The number of frames in the current video content.</returns>
        public virtual int GetFrameCount()
        {
            if (hasInitDone)
            {
                return (int)videoCapture.get(Videoio.CAP_PROP_FRAME_COUNT);
            }
            else
            {
                return 0;
            }
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
        /// Return the VideoCapture instance.
        /// </summary>
        /// <returns>The VideoCapture instance.</returns>
        public virtual VideoCapture GetVideoCapture()
        {
            return hasInitDone ? videoCapture : null;
        }

        /// <summary>
        /// Use this to check if the Mat has changed since the last frame. Since it would not make sense to do expensive video processing in each Update call, check this value before doing any processing.
        /// </summary>
        /// <returns><c>true</c>, if the Mat has been updated <c>false</c> otherwise.</returns>
        public virtual bool DidUpdateThisFrame()
        {
            if (!hasInitDone)
                return false;

            return didUpdateThisFrame;
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
                Debug.LogWarning("VideoCapture2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
            }
            if (rotatedFrameMat != null && rotatedFrameMat.IsDisposed)
            {
                Debug.LogWarning("VideoCapture2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
            }

            if (baseColorFormat == outputColorFormat)
            {
                baseMat.copyTo(frameMat);
            }
            else
            {
                Imgproc.cvtColor(baseMat, frameMat, Source2MatHelperUtils.ColorConversionCodes(baseColorFormat, outputColorFormat));
            }

            return Source2MatHelperUtils.ApplyMatTransformations(frameMat, rotatedFrameMat, flipVertical, flipHorizontal);
        }

        /// <summary>
        /// Cancel Init Coroutine.
        /// </summary>
        protected virtual void CancelInitCoroutine()
        {
            if (getFilePathCoroutine != null)
            {
                StopCoroutine(getFilePathCoroutine);
                ((IDisposable)getFilePathCoroutine).Dispose();
                getFilePathCoroutine = null;
            }

            if (initCoroutine != null)
            {
                StopCoroutine(initCoroutine);
                ((IDisposable)initCoroutine).Dispose();
                initCoroutine = null;
            }
        }

        /// <summary>
        /// Cancel WaitForEndOfFrame Coroutine.
        /// </summary>
        protected virtual void CancelWaitForEndOfFrameCoroutine()
        {
            if (waitForEndOfFrameCoroutine != null)
            {
                StopCoroutine(waitForEndOfFrameCoroutine);
                ((IDisposable)waitForEndOfFrameCoroutine).Dispose();
                waitForEndOfFrameCoroutine = null;
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
            didUpdateThisFrame = false;

            if (videoCapture != null)
            {
                videoCapture.Dispose();
                videoCapture = null;
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

            if (imageBufferMat != null)
            {
                imageBufferMat.Dispose();
                imageBufferMat = null;
            }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="VideoCapture2MatHelper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="VideoCapture2MatHelper"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="VideoCapture2MatHelper"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="VideoCapture2MatHelper"/> so
        /// the garbage collector can reclaim the memory that the <see cref="VideoCapture2MatHelper"/> was occupying.</remarks>
        public virtual void Dispose()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

            if (resetEvent != null)
            {
                resetEvent.Dispose();
            }

            if (isInitWaiting)
            {
                CancelInitCoroutine();
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();
            }
            else if (hasInitDone)
            {
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }
        }
    }
}
