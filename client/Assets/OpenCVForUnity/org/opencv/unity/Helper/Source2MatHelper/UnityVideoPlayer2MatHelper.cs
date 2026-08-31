using System;
using System.Collections;
using System.IO;
using System.Threading;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Video;

#if UNITY_EDITOR
using OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor;
#endif

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// A helper component class for obtaining video frames from a file using Unity's <c>VideoPlayer</c> and converting them to OpenCV <c>Mat</c> format.
    /// </summary>
    /// <remarks>
    /// The <c>UnityVideoPlayer2MatHelper</c> class uses Unity's <c>VideoPlayer</c> to read frames from a specified video file and converts each frame to an OpenCV <c>Mat</c> object.
    /// This component automatically retrieves frames based on the video's playback rate, ensuring smooth frame updates and accurate timing.
    ///
    /// To ensure compatibility within Unity, it is recommended to use the MP4 format for video files, as it is widely supported by the <c>VideoPlayer</c> class.
    ///
    /// This component is particularly useful for applications requiring frame-by-frame video processing in Unity, allowing seamless integration with OpenCV-based image processing routines while using Unity's native video playback features.
    ///
    /// <strong>Note:</strong> By setting outputColorFormat to RGBA processing that does not include extra color conversion is performed.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the current video frame in <c>Mat</c> format.
    /// The helper class manages video file loading, playback control, and frame updates based on the video's playback rate.
    /// </example>
    public class UnityVideoPlayer2MatHelper : MonoBehaviour, IVideoSource2MatHelper, IMatTransformationProvider
    {

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the video file path, relative to the starting point of the \"StreamingAssets\" folder, or absolute path.")]
        protected string _requestedVideoFilePath = string.Empty;

        /// <summary>
        /// Set the video file path, relative to the starting point of the "StreamingAssets" folder, or absolute path.
        /// </summary>
        public virtual string RequestedVideoFilePath
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
        [SerializeField, Tooltip("Set whether to rotate video frame 90 degrees. (clockwise)")]
        protected bool _rotate90Degree = false;

        /// <summary>
        /// Set whether to rotate video frame 90 degrees. (clockwise)
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


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Indicate whether to play this video in a loop.")]
        protected bool _loop = true;

        /// <summary>
        /// Indicate whether to play this video in a loop.
        /// </summary>
        public virtual bool Loop
        {
            get { return _loop; }
            set
            {
                if (videoPlayer != null) videoPlayer.isLooping = value;
                _loop = value;
            }
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
        /// The videoPlayer
        /// </summary>
        protected VideoPlayer videoPlayer;

        /// <summary>
        /// Internal playing state flag to track video playback status immediately.
        /// </summary>
        protected bool isPlaying = false;

        /// <summary>
        /// Internal paused state flag to track video pause status immediately.
        /// </summary>
        protected bool isPaused = false;

        /// <summary>
        /// Whether the mat that can be obtained with the GetMat method has been updated in this frame.
        /// This flag is changed after waiting until WaitForEndOfFrame in the coroutine.
        /// </summary>
        protected bool didUpdateThisFrame = false;

        /// <summary>
        /// texture2DBuffer.
        /// </summary>
        protected Texture2D texture2DBuffer;

        /// <summary>
        /// Current AsyncGPUReadbackRequest for direct data access.
        /// </summary>
        protected AsyncGPUReadbackRequest asyncGPUReadbackRequestBuffer;

        /// <summary>
        /// frameIndexBuffer
        /// </summary>
        protected long frameIndexBuffer;

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

        /// <summary>
        /// The frame index
        /// </summary>
        protected long frameIndex;

        /// <summary>
        /// The useAsyncGPUReadback
        /// </summary>
        protected bool useAsyncGPUReadback;

        /// <summary>
        /// The RenderTexture for video rendering when using AsyncGPUReadback.
        /// </summary>
        protected RenderTexture renderTexture;

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
        /// The get file path coroutine.
        /// </summary>
        protected IEnumerator getFilePathCoroutine;

        /// <summary>
        /// If set to true play after completion of initialization.
        /// </summary>
        protected bool autoPlayAfterInitialize;

        /// <summary>
        /// Event to signal when frame data is ready for processing
        /// </summary>
        private ManualResetEventSlim frameDataReadyEvent;

        /// <summary>
        /// The waitForEndOfFrameCoroutine
        /// </summary>
        protected IEnumerator waitForEndOfFrameCoroutine;

        protected virtual void OnValidate()
        {
            _timeoutFrameCount = (int)Mathf.Clamp(_timeoutFrameCount, 0f, float.MaxValue);
        }

        protected virtual void PrepareCompleted(VideoPlayer vp)
        {
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "PrepareCompleted");
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "Video Url: " + vp.url);
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "width: " + vp.width + " height: " + vp.height);
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "canSetSkipOnDrop: " + vp.canSetSkipOnDrop);
        }

        protected virtual void ErrorReceived(VideoPlayer source, string message)
        {
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "ErrorReceived: " + message);

            if (OnErrorOccurred != null)
                OnErrorOccurred.Invoke(Source2MatHelperErrorCode.UNKNOWN, "ErrorReceived: " + message);
        }

        protected virtual void LoopPointReached(VideoPlayer vp)
        {
            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "LoopPointReached");

            if (!Loop)
            {
                Stop();
            }
        }

        protected virtual void FrameReady(VideoPlayer vp, long frameIndex)
        {
            // Debug.Log("UnityVideoPlayer2MatHelper:: " + "FrameReady " + frameIndex);

            if (videoPlayer != null && (videoPlayer.isPaused || !videoPlayer.isPlaying))
            {
                return;
            }

            if (!useAsyncGPUReadback)
            {
                OpenCVMatUtils.TextureToTexture2D(renderTexture, texture2DBuffer);
                frameIndexBuffer = frameIndex;

                // Data update
                frameDataReadyEvent.Reset();
            }
            else
            {
                AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, (request) => { OnCompleteReadback(request, frameIndex); });
            }
        }

        /// <summary>
        /// Thread-safe callback method for completed AsyncGPUReadbackRequest.
        /// This method may be called from a background thread, so it stores the request
        /// for direct data access in the main thread.
        /// </summary>
        /// <param name="request">The completed AsyncGPUReadbackRequest.</param>
        /// <param name="frameIndex">The frame index.</param>
        protected virtual void OnCompleteReadback(AsyncGPUReadbackRequest request, long frameIndex)
        {
            // Debug.Log("UnityVideoPlayer2MatHelper:: " + "OnCompleteReadback");

            // Store the request for direct data access
            asyncGPUReadbackRequestBuffer = request;
            frameIndexBuffer = frameIndex;

            // Signal data availability to the main thread by resetting the event
            if (frameDataReadyEvent != null)
            {
                frameDataReadyEvent.Reset();
            }
        }

        protected virtual IEnumerator _WaitForFrameEndCoroutine()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                if (!frameDataReadyEvent.IsSet)
                {
                    // If the data has been updated during this frame, the data is copied to baseMat.

                    if (!useAsyncGPUReadback)
                    {
                        OpenCVMatUtils.Texture2DToMatRaw(texture2DBuffer, baseMat);
                        this.frameIndex = frameIndexBuffer;
                    }
                    else
                    {
                        if (asyncGPUReadbackRequestBuffer.hasError)
                        {
                            frameDataReadyEvent.Set();
                            continue;
                        }

#if !OPENCV_DONT_USE_UNSAFE_CODE
                        // Use the stored AsyncGPUReadbackRequest data directly
                        OpenCVMatUtils.CopyToMat(asyncGPUReadbackRequestBuffer.GetData<byte>(), baseMat);
#endif

                        Core.flip(baseMat, baseMat, 0);

                        this.frameIndex = frameIndexBuffer;
                    }

                    // Data copying to baseMat completed.
                    frameDataReadyEvent.Set();

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
        /// Get the first frame after starting video playback.
        /// </summary>
        /// <returns>IEnumerator for coroutine</returns>
        protected virtual IEnumerator _GetFirstFrameCoroutine()
        {
            // Debug.Log("UnityVideoPlayer2MatHelper:: " + "GetFirstFrame");

            // Wait until the first frame is ready
            while (frameDataReadyEvent.IsSet)
            {
                yield return null;
            }

            //Debug.Log("UnityVideoPlayer2MatHelper:: " + "frameDataReadyEvent.IsSet: " + frameDataReadyEvent.IsSet);

            if (!useAsyncGPUReadback)
            {
                OpenCVMatUtils.Texture2DToMatRaw(texture2DBuffer, baseMat);
                this.frameIndex = frameIndexBuffer;
            }
            else
            {
                if (asyncGPUReadbackRequestBuffer.hasError)
                {
                    frameDataReadyEvent.Set();
                    yield break;
                }

#if !OPENCV_DONT_USE_UNSAFE_CODE
                // Use the stored AsyncGPUReadbackRequest data directly
                OpenCVMatUtils.CopyToMat(asyncGPUReadbackRequestBuffer.GetData<byte>(), baseMat);
#endif

                Core.flip(baseMat, baseMat, 0);

                this.frameIndex = frameIndexBuffer;
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
            if (OnInitialized == null)
                OnInitialized = new UnityEvent();
            if (OnDisposed == null)
                OnDisposed = new UnityEvent();
            if (OnErrorOccurred == null)
                OnErrorOccurred = new Source2MatHelperErrorUnityEvent();

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
            //useAsyncGPUReadback = false;

            if (hasInitDone)
            {
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();

                if (OnDisposed != null)
                    OnDisposed.Invoke();
            }

#if !OPENCV_DONT_USE_UNSAFE_CODE
            useAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
#else
            useAsyncGPUReadback = false;
#endif
            Debug.Log("UnityVideoPlayer2MatHelper:: " + "useAsyncGPUReadback: " + useAsyncGPUReadback);


            isInitWaiting = true;

            // Wait one frame before starting initialization process
            yield return null;


            int initFrameCount = 0;
            bool isTimeout = false;

            string fullPath = string.Empty;
            Uri uri;
            if (Uri.TryCreate(_requestedVideoFilePath, UriKind.Absolute, out uri))
            {
                fullPath = uri.OriginalString;
                //Debug.Log("fullPath " + fullPath);
            }
            else
            {
                fullPath = Path.Combine(Application.streamingAssetsPath, _requestedVideoFilePath);
                //Debug.Log("Not Absolute fullPath " + fullPath);
            }

            if (string.IsNullOrEmpty(fullPath))
            {
                isInitWaiting = false;
                initCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.VIDEO_FILE_NOT_EXIST, _requestedVideoFilePath);

                yield break;
            }

            videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                videoPlayer = gameObject.AddComponent(typeof(VideoPlayer)) as VideoPlayer;
            }
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = fullPath;
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = _loop;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.prepareCompleted += PrepareCompleted;
            videoPlayer.errorReceived += ErrorReceived;
            videoPlayer.loopPointReached += LoopPointReached;
            videoPlayer.Prepare();

            // Wait until the PrepareCompleted method is called
            isTimeout = false;
            while (true)
            {
                if (initFrameCount > _timeoutFrameCount)
                {
                    isTimeout = true;
                    break;
                }
                else if (videoPlayer.isPrepared)
                {
                    int frameWidth = (int)videoPlayer.width;
                    int frameHeight = (int)videoPlayer.height;

                    // Create RenderTexture for both AsyncGPUReadback and TextureToTexture2D
                    renderTexture = new RenderTexture(frameWidth, frameHeight, 0, RenderTextureFormat.ARGB32);
                    renderTexture.Create();
                    videoPlayer.targetTexture = renderTexture;

                    if (!useAsyncGPUReadback)
                    {
                        texture2DBuffer = new Texture2D(frameWidth, frameHeight, TextureFormat.RGBA32, false);
                    }
                    baseMat = new Mat(frameHeight, frameWidth, CvType.CV_8UC4, new Scalar(0, 0, 0, 255));

                    if (baseColorFormat == OutputColorFormat)
                    {
                        frameMat = baseMat.clone();
                    }
                    else
                    {
                        frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)), new Scalar(0, 0, 0, 255));
                    }

                    // Create the rotated frame mat if needed
                    if (Rotate90Degree)
                    {
                        rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
                    }

                    frameIndex = 0;
                    frameIndexBuffer = 0;

                    // Clear the current request
                    asyncGPUReadbackRequestBuffer = default(AsyncGPUReadbackRequest);

                    frameDataReadyEvent = new ManualResetEventSlim(false);

                    Debug.Log("UnityVideoPlayer2MatHelper:: " + " fileUrl:" + videoPlayer.url + " width:" + frameMat.width() + " height:" + frameMat.height() + " fps:" + videoPlayer.frameRate);

                    isInitWaiting = false;
                    hasInitDone = true;
                    initCoroutine = null;

                    frameDataReadyEvent.Set();

#if UNITY_WEBGL
                    // For WebGL, converting the RenderTexture immediately after the first frame results in incomplete images,
                    // so wait for several frames after the first frame is ready before converting.
                    videoPlayer.Play();
                    isPlaying = true;
                    isPaused = false;

                    while (videoPlayer.frame < 0)
                    {
                        yield return null;
                    }
                    videoPlayer.Pause();
                    yield return null;
                    yield return null;
                    yield return null;
                    if (!useAsyncGPUReadback)
                    {
                        OpenCVMatUtils.TextureToTexture2D(renderTexture, texture2DBuffer);
                        frameIndexBuffer = 0;
                        // Data update
                        frameDataReadyEvent.Reset();
                        // Get the first frame
                        yield return StartCoroutine(_GetFirstFrameCoroutine());
                    }
                    else
                    {
                        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, (request) => { OnCompleteReadback(request, 0); });
                        // Get the first frame
                        yield return StartCoroutine(_GetFirstFrameCoroutine());
                    }

                    videoPlayer.sendFrameReadyEvents = true;
                    videoPlayer.frameReady += FrameReady;
                    videoPlayer.Play();
#else
                    videoPlayer.sendFrameReadyEvents = true;
                    videoPlayer.frameReady += FrameReady;

                    videoPlayer.Play();
                    isPlaying = true;
                    isPaused = false;

                    // Get the first frame
                    yield return StartCoroutine(_GetFirstFrameCoroutine());
#endif

                    if (!autoPlayAfterInitialize)
                    {
                        videoPlayer.Stop();
                        isPlaying = false;
                        isPaused = false;
                    }

                    // Data copying to baseMat completed.
                    frameDataReadyEvent.Set();

                    didUpdateThisFrame = true;
                    //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame);

                    // Start a coroutine and wait for WaitForEndOfFrame. If already started, stop and then start.
                    if (waitForEndOfFrameCoroutine != null) StopCoroutine(waitForEndOfFrameCoroutine);
                    waitForEndOfFrameCoroutine = _WaitForFrameEndCoroutine();
                    StartCoroutine(waitForEndOfFrameCoroutine);

                    if (OnInitialized != null)
                        OnInitialized.Invoke();

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
                videoPlayer.sendFrameReadyEvents = false;
                videoPlayer.frameReady -= FrameReady;
                videoPlayer.prepareCompleted -= PrepareCompleted;
                videoPlayer.errorReceived -= ErrorReceived;
                videoPlayer.loopPointReached -= LoopPointReached;
                videoPlayer.Stop();
                videoPlayer = null;
                isInitWaiting = false;
                initCoroutine = null;

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
                videoPlayer.Play();
                isPlaying = true;
                isPaused = false; // Resume from paused state or start from stopped state
            }
        }

        /// <summary>
        /// Pause the video.
        /// </summary>
        public virtual void Pause()
        {
            if (hasInitDone)
            {
                videoPlayer.Pause();
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
        /// Stop the video.
        /// </summary>
        public virtual void Stop()
        {
            if (hasInitDone)
            {
                videoPlayer.Stop();
                isPlaying = false;
                isPaused = false; // Reset to stopped state
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
            return hasInitDone ? isPaused : false;
        }

        /// <summary>
        /// Return the active video device name.
        /// </summary>
        /// <returns>The active video device name.</returns>
        public virtual string GetDeviceName()
        {
            return "Unity_VideoPlayer";
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
            return hasInitDone ? videoPlayer.frameRate : -1f;
        }

        /// <summary>
        /// Return the relative position of the video file: 0=start of the film, 1=end of the film.
        /// </summary>
        /// <returns>The relative position of the video file: 0=start of the film, 1=end of the film.</returns>
        public virtual float GetFramePosRatio()
        {
            if (hasInitDone)
            {
                return (float)videoPlayer.frame / videoPlayer.frameCount;
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
                videoPlayer.frame = (long)(videoPlayer.frameCount * ratio);
            }
        }

        /// <summary>
        /// Return the video frame index.
        /// </summary>
        /// <returns>The video frame index.</returns>
        public virtual int GetFrameIndex()
        {
            return hasInitDone ? (int)frameIndex : 0;
        }

        /// <summary>
        /// Set the video frame index.
        /// </summary>
        /// <param name="index">The video frame index.</param>
        public virtual void SetFrameIndex(int index)
        {
            if (hasInitDone)
            {
                videoPlayer.frame = index;
            }
        }

        /// <summary>
        /// Return the number of frames in the current video content.
        /// </summary>
        /// <returns>The number of frames in the current video content.</returns>
        public virtual int GetFrameCount()
        {
            return hasInitDone ? (int)videoPlayer.frameCount : 0;
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
        public virtual VideoPlayer GetVideoPlayer()
        {
            return hasInitDone ? videoPlayer : null;
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
                Debug.LogWarning("UnityVideoPlayer2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
            }
            if (rotatedFrameMat != null && rotatedFrameMat.IsDisposed)
            {
                Debug.LogWarning("UnityVideoPlayer2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
            }

            if (baseColorFormat == OutputColorFormat)
            {
                baseMat.copyTo(frameMat);
            }
            else
            {
                Imgproc.cvtColor(baseMat, frameMat, Source2MatHelperUtils.ColorConversionCodes(baseColorFormat, OutputColorFormat));
            }

            return Source2MatHelperUtils.ApplyMatTransformations(frameMat, rotatedFrameMat, FlipVertical, FlipHorizontal);
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

            didUpdateThisFrame = false;
            isPlaying = false;
            isPaused = false;

            frameIndex = 0;

            if (frameDataReadyEvent != null)
            {
                frameDataReadyEvent.Dispose();
                frameDataReadyEvent = null;
            }

            // Clear the current request
            asyncGPUReadbackRequestBuffer = default(AsyncGPUReadbackRequest);


            if (videoPlayer != null)
            {
                videoPlayer.sendFrameReadyEvents = false;
                videoPlayer.frameReady -= FrameReady;
                videoPlayer.prepareCompleted -= PrepareCompleted;
                videoPlayer.errorReceived -= ErrorReceived;
                videoPlayer.loopPointReached -= LoopPointReached;

                videoPlayer.Stop();
                videoPlayer = null;
            }
            if (renderTexture != null)
            {
                renderTexture.Release();
                RenderTexture.Destroy(renderTexture);
                renderTexture = null;
            }
            if (texture2DBuffer != null)
            {
                Texture2D.Destroy(texture2DBuffer);
                texture2DBuffer = null;
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

        }

        /// <summary>
        /// Releases all resource used by the <see cref="UnityVideoPlayer2MatHelper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="UnityVideoPlayer2MatHelper"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="UnityVideoPlayer2MatHelper"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="UnityVideoPlayer2MatHelper"/> so
        /// the garbage collector can reclaim the memory that the <see cref="UnityVideoPlayer2MatHelper"/> was occupying.</remarks>
        public virtual void Dispose()
        {
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

                if (OnDisposed != null)
                    OnDisposed.Invoke();
            }
        }
    }
}
