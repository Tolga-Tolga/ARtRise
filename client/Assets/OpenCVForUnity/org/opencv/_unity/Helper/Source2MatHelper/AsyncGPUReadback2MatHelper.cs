using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
#if UNITY_EDITOR
using OpenCVForUnity.UnityUtils.Helper.Editor;
#endif
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// A helper component class for efficiently converting Unity <c>Texture</c> objects, such as <c>RenderTexture</c> and external texture format <c>Texture2D</c>, to OpenCV <c>Mat</c> format using <c>AsyncGPUReadback</c>.
    /// </summary>
    /// <remarks>
    /// The <c>AsyncGPUReadback2MatHelper</c> class leverages <c>AsyncGPUReadback</c> to efficiently process texture data from Unity <c>Texture</c> classes that do not support direct pixel access through methods like <c>GetPixels</c>.
    /// This includes <c>RenderTexture</c> and certain externally provided <c>Texture2D</c> formats. The class transfers the texture data asynchronously to the CPU and converts it to an OpenCV <c>Mat</c> object for further processing.
    ///
    /// By utilizing <c>AsyncGPUReadback</c>, this component optimizes resource usage and reduces CPU load, making it ideal for real-time image processing applications that involve complex textures.
    ///
    /// <strong>Note:</strong> By setting outputColorFormat to RGBA processing that does not include extra color conversion is performed.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the latest frame as a <c>Mat</c> object.
    /// The helper class manages asynchronous readback operations and ensures efficient texture data conversion suitable for image processing in Unity.
    /// </example>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.AsyncGPUReadback2MatHelper class instead.")]
    public class AsyncGPUReadback2MatHelper : MonoBehaviour, ITextureSource2MatHelper, IMatUpdateFPSProvider, IMatTransformationProvider
    {

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("sourceTexture"), TooltipAttribute("Set the source texture.")]
        protected Texture _sourceTexture;

        /// <summary>
        /// Set the source texture.
        /// </summary>
        public virtual Texture sourceTexture
        {
            get { return _sourceTexture; }
            set
            {
                if (_sourceTexture != value)
                {
                    _sourceTexture = value;
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
        [SerializeField, FormerlySerializedAs("requestedMatUpdateFPS"), TooltipAttribute("Set the frame rate of readback.")]
        protected float _requestedMatUpdateFPS = 30f;

        /// <summary>
        /// Sets the frame rate at which the Mat is updated. (interval at which the DidUpdateThisFrame() method becomes true).
        /// </summary>
        public virtual float requestedMatUpdateFPS
        {
            get { return _requestedMatUpdateFPS; }
            set
            {
                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
                if (_requestedMatUpdateFPS != _value)
                {
                    _requestedMatUpdateFPS = _value;
                    fpsManager = new FpsManager(_requestedMatUpdateFPS);
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("rotate90Degree"), TooltipAttribute("Set whether to rotate readback frame 90 degrees. (clockwise)")]
        protected bool _rotate90Degree = false;

        /// <summary>
        /// Set whether to rotate readback frame 90 degrees. (clockwise)
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
        protected Source2MatHelperColorFormat _outputColorFormat = Source2MatHelperColorFormat.RGBA;

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
        /// asyncGPUReadbackRequestBuffer
        /// </summary>
        protected AsyncGPUReadbackRequest asyncGPUReadbackRequestBuffer;

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
        /// The useAsyncGPUReadback
        /// </summary>
        protected bool useAsyncGPUReadback;

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
        /// Indicates whether this instance has been initialized.
        /// </summary>
        protected bool isPlaying = false;

        /// <summary>
        /// If set to true play after completion of initialization.
        /// </summary>
        protected bool autoPlayAfterInitialize;

        /// <summary>
        /// FPS Manager
        /// </summary>
        private FpsManager fpsManager;

        /// <summary>
        /// resetEvent
        /// </summary>
        private ManualResetEventSlim resetEvent;

        /// <summary>
        /// The waitForEndOfFrameCoroutine.
        /// </summary>
        protected IEnumerator waitForEndOfFrameCoroutine;

        protected virtual void OnValidate()
        {
            _requestedMatUpdateFPS = Mathf.Clamp(_requestedMatUpdateFPS, -1f, float.MaxValue);
            _timeoutFrameCount = (int)Mathf.Clamp(_timeoutFrameCount, 0f, float.MaxValue);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (hasInitDone)
            {
                if (_sourceTexture == null) return;

                if (_sourceTexture.width != baseMat.width() || _sourceTexture.height != baseMat.height())
                {

                    Debug.Log("AsyncGPUReadback2MatHelper:: " + "SOURCE_TEXTURE_SIZE_IS_CHANGED");

                    if (hasInitDone)
                        Initialize(IsPlaying());

                    return;
                }

                if (!isPlaying) return;

                // Update FPSManager and execute process at regular intervals.
                fpsManager.Update(Time.deltaTime, CallReadback);
            }
        }

        protected virtual void CallReadback()
        {
            if (!useAsyncGPUReadback)
            {
                Utils.textureToTexture2D(_sourceTexture, texture2DBuffer);

                // Data update
                resetEvent.Reset();
            }
            else
            {
                AsyncGPUReadback.Request(_sourceTexture, 0, TextureFormat.RGBA32, (request) => { OnCompleteReadback(request); });
            }
        }

        protected virtual void OnCompleteReadback(AsyncGPUReadbackRequest request)
        {
            //Debug.Log("AsyncGPUReadback2MatHelper:: " + "OnCompleteReadback" + " " + Time.frameCount);

            if (!gameObject.activeInHierarchy) return;

            if (request.hasError)
            {
                Debug.Log("AsyncGPUReadback2MatHelper:: " + "GPU readback error detected. ");

            }
            else if (request.done)
            {
                //Debug.Log("AsyncGPUReadback2MatHelper:: " + "Start GPU readback done.);

                //Debug.Log("AsyncGPUReadback2MatHelper:: " + "Thread.CurrentThread.ManagedThreadId " + Thread.CurrentThread.ManagedThreadId);

                // Data is retained until the end of the frame.
                asyncGPUReadbackRequestBuffer = request;

                // Data update
                resetEvent.Reset();

                //Debug.Log("AsyncGPUReadback2MatHelper:: " + "End GPU readback done. ");
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

                    if (!useAsyncGPUReadback)
                    {
                        Utils.texture2DToMat(texture2DBuffer, baseMat);

                        // Data copying to baseMat completed.
                        resetEvent.Set();

                        // Set the didUpdateThisFrame flag to true only during the frame following the frame in which the baseMat is updated.
                        didUpdateThisFrame = true;
                        //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame + " " + Time.frameCount);
                    }
                    else
                    {
                        if (asyncGPUReadbackRequestBuffer.hasError)
                        {
                            resetEvent.Set();
                            continue;
                        }

#if !OPENCV_DONT_USE_UNSAFE_CODE
                        MatUtils.copyToMat(asyncGPUReadbackRequestBuffer.GetData<byte>(), baseMat);
#endif

                        Core.flip(baseMat, baseMat, 0);

                        // Data copying to baseMat completed.
                        resetEvent.Set();

                        // Set the didUpdateThisFrame flag to true only during the frame following the frame in which the baseMat is updated.
                        didUpdateThisFrame = true;
                        //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame + " " + Time.frameCount);
                    }

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
        /// Initializes this instance.
        /// </summary>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(bool autoPlay = true)
        {
#if !OPENCV_DONT_USE_UNSAFE_CODE
            useAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
#else
            useAsyncGPUReadback = false;
#endif
            //Debug.Log("AsyncGPUReadback2MatHelper:: " + "useAsyncGPUReadback: " + useAsyncGPUReadback);

            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            autoPlayAfterInitialize = autoPlay;
            if (onInitialized == null)
                onInitialized = new UnityEvent();
            if (onDisposed == null)
                onDisposed = new UnityEvent();
            if (onErrorOccurred == null)
                onErrorOccurred = new Source2MatHelperErrorUnityEvent();

            initCoroutine = _Initialize();
            StartCoroutine(initCoroutine);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="sourceTexture">Source Texture.</param>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(Texture sourceTexture, bool autoPlay = true)
        {

#if !OPENCV_DONT_USE_UNSAFE_CODE
            useAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
#else
            useAsyncGPUReadback = false;
#endif
            //Debug.Log("AsyncGPUReadback2MatHelper:: " + "useAsyncGPUReadback: " + useAsyncGPUReadback);

            if (isInitWaiting)
            {
                CancelInitCoroutine();
                ReleaseResources();
            }

            _sourceTexture = sourceTexture;
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
        /// Initializes this instance by coroutine.
        /// </summary>
        protected virtual IEnumerator _Initialize()
        {
            if (hasInitDone)
            {
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();

                if (onDisposed != null)
                    onDisposed.Invoke();
            }

            if (_sourceTexture == null)
            {
                isInitWaiting = false;
                initCoroutine = null;

                if (onErrorOccurred != null)
                    onErrorOccurred.Invoke(Source2MatHelperErrorCode.SOURCE_TEXTURE_IS_NULL, string.Empty);

                yield break;
            }

            isInitWaiting = true;

            // Wait one frame before starting initialization process
            yield return null;


#if UNITY_6000_0_OR_NEWER
            if (!SystemInfo.IsFormatSupported(_sourceTexture.graphicsFormat, GraphicsFormatUsage.ReadPixels))
#else
            if (!SystemInfo.IsFormatSupported(_sourceTexture.graphicsFormat, FormatUsage.ReadPixels))
#endif
            {
                Debug.Log("AsyncGPUReadback2MatHelper:: " + "The format of the set source texture is unsupported by AsyncGPUReadback, the conversion method has been changed to an inefficient method.");

                useAsyncGPUReadback = false;
            }
#if OPENCV_DONT_USE_UNSAFE_CODE
            useAsyncGPUReadback = false;
#endif
            //useAsyncGPUReadback = false;

            int frameWidth = (int)_sourceTexture.width;
            int frameHeight = (int)_sourceTexture.height;


            texture2DBuffer = new Texture2D(frameWidth, frameHeight, TextureFormat.RGBA32, false);

            baseMat = new Mat(frameHeight, frameWidth, CvType.CV_8UC4, new Scalar(0, 0, 0, 255));

            if (baseColorFormat == outputColorFormat)
            {
                frameMat = baseMat.clone();
            }
            else
            {
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
            }

            // Create the rotated frame mat if needed
            if (rotate90Degree)
            {
                rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
            }

            // Initialize FpsManager
            fpsManager = new FpsManager(_requestedMatUpdateFPS);

            resetEvent = new ManualResetEventSlim(true);

            Debug.Log("AsyncGPUReadback2MatHelper:: " + " width:" + frameMat.width() + " height:" + frameMat.height() + " useAsyncGPUReadback:" + useAsyncGPUReadback);

            isInitWaiting = false;
            hasInitDone = true;
            initCoroutine = null;

            isPlaying = autoPlayAfterInitialize;


            // Get first frame synchronously
            Utils.textureToTexture2D(_sourceTexture, texture2DBuffer);
            Utils.texture2DToMat(texture2DBuffer, baseMat);

            // Data copying to baseMat completed.
            resetEvent.Set();

            didUpdateThisFrame = true;
            //Debug.Log("initialized start didUpdateThisFrame " + didUpdateThisFrame);

            // Start a coroutine and wait for WaitForEndOfFrame. If already started, stop and then start.
            if (waitForEndOfFrameCoroutine != null) StopCoroutine(waitForEndOfFrameCoroutine);
            waitForEndOfFrameCoroutine = _WaitForEndOfFrameCoroutine();
            StartCoroutine(waitForEndOfFrameCoroutine);


            if (onInitialized != null)
                onInitialized.Invoke();
        }


        /// <summary>
        /// Indicates whether this instance has been initialized.
        /// </summary>
        /// <returns><c>true</c>, if this instance has been initialized, <c>false</c> otherwise.</returns>
        public virtual bool IsInitialized()
        {
            return hasInitDone;
        }

        /// <summary>
        /// Starts the readback.
        /// </summary>
        public virtual void Play()
        {
            if (hasInitDone)
            {
                isPlaying = true;
                fpsManager.IsPaused = false;
            }
        }

        /// <summary>
        /// Pauses the readback.
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
        /// Stops the readback.
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
        /// Indicates whether the readback is currently playing.
        /// </summary>
        /// <returns><c>true</c>, if the readback is playing, <c>false</c> otherwise.</returns>
        public virtual bool IsPlaying()
        {
            return hasInitDone ? isPlaying : false;
        }

        /// <summary>
        /// Indicates whether the readback is paused.
        /// </summary>
        /// <returns><c>true</c>, if the readback is playing, <c>false</c> otherwise.</returns>
        public virtual bool IsPaused()
        {
            return hasInitDone ? isPlaying : false;
        }

        /// <summary>
        /// Return the active readback device name.
        /// </summary>
        /// <returns>The active readback device name.</returns>
        public virtual string GetDeviceName()
        {
            return "Unity_AsyncGPUReadback";
        }

        /// <summary>
        /// Returns the readback buffer width.
        /// </summary>
        /// <returns>The readback buffer width.</returns>
        public virtual int GetWidth()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.height() : frameMat.width();
        }

        /// <summary>
        /// Returns the readback buffer height.
        /// </summary>
        /// <returns>The readback buffer height.</returns>
        public virtual int GetHeight()
        {
            if (!hasInitDone)
                return -1;
            return (rotatedFrameMat != null) ? frameMat.width() : frameMat.height();
        }

        /// <summary>
        /// Returns the readback buffer base color format.
        /// </summary>
        /// <returns>The readback buffer base color format.</returns>
        public virtual Source2MatHelperColorFormat GetBaseColorFormat()
        {
            return baseColorFormat;
        }

        /// <summary>
        /// Return the frame rate at which the Mat is updated. (interval at which the DidUpdateThisFrame() method becomes true).
        /// </summary>
        /// <returns>The active readback framerate.</returns>
        public virtual float GetMatUpdateFPS()
        {
            return hasInitDone ? _requestedMatUpdateFPS : -1f;
        }

        /// <summary>
        /// Returns the Source Texture.
        /// </summary>
        /// <returns>The Source Texture.</returns>
        public virtual Texture GetSourceTexture()
        {
            return hasInitDone ? _sourceTexture : null;
        }

        /// <summary>
        /// Use this to check if the Mat has changed since the last frame. Since it would not make sense to do expensive image processing in each Update call, check this value before doing any processing.
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
                Debug.LogWarning("AsyncGPUReadback2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)));
            }
            if (rotatedFrameMat != null && rotatedFrameMat.IsDisposed)
            {
                Debug.LogWarning("AsyncGPUReadback2MatHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
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

            if (useAsyncGPUReadback)
            {
                AsyncGPUReadback.WaitAllRequests();
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
        /// Releases all resource used by the <see cref="AsyncGPUReadback2MatHelper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="AsyncGPUReadback2MatHelper"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="AsyncGPUReadback2MatHelper"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="AsyncGPUReadback2MatHelper"/> so
        /// the garbage collector can reclaim the memory that the <see cref="AsyncGPUReadback2MatHelper"/> was occupying.</remarks>
        public virtual void Dispose()
        {
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

                if (onDisposed != null)
                    onDisposed.Invoke();
            }
        }
    }
}
