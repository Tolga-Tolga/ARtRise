#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API

using System;
using System.Collections;
using System.Threading;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor;
#endif

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    /// <summary>
    /// A helper component class for efficiently obtaining camera frames from <see cref="WebCamTexture"/> and converting them to OpenCV <c>Mat</c> format in real-time using <c>AsyncGPUReadback</c>.
    /// </summary>
    /// <remarks>
    /// The <c>WebCamTexture2MatAsyncGPUHelper</c> class captures video frames from a device's camera using <see cref="WebCamTexture"/>
    /// and utilizes <c>AsyncGPUReadback</c> to efficiently transfer pixel data to the CPU. Each frame is then converted to an OpenCV <c>Mat</c> object.
    /// This component handles camera orientation, rotation, and necessary transformations to ensure the <c>Mat</c> output aligns correctly with the device's display orientation.
    ///
    /// By leveraging <c>AsyncGPUReadback</c>, this component reduces CPU load and enhances performance, making it particularly suitable for applications requiring real-time image processing with high frame rates.
    /// It enables seamless integration of OpenCV-based image processing algorithms with Unity's camera input while optimizing resource usage.
    ///
    /// <strong>Note:</strong> By setting outputColorFormat to RGBA, processing that does not include extra color conversion is performed.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the latest camera frame in <c>Mat</c> format.
    /// The helper class manages camera start/stop operations, <c>AsyncGPUReadback</c> requests, and frame updates internally.
    /// </example>
    public class WebCamTexture2MatAsyncGPUHelper : WebCamTexture2MatHelper, IMatUpdateFPSProvider
    {

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, Tooltip("Set the frame rate of camera.")]
        protected float _requestedMatUpdateFPS = 30f;

        /// <summary>
        /// Sets the frame rate at which the Mat is updated. (interval at which the DidUpdateThisFrame() method becomes true).
        /// </summary>
        public virtual float RequestedMatUpdateFPS
        {
            get { return _requestedMatUpdateFPS; }
            set
            {
                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
                if (_requestedMatUpdateFPS != _value)
                {
                    _requestedMatUpdateFPS = _value;
                    fpsManager = new FpsManager(RequestedMatUpdateFPS);
                }
            }
        }

        /// <summary>
        /// Whether the mat that can be obtained with the GetMat method has been updated in this frame.
        /// This flag is changed after waiting until WaitForEndOfFrame in the coroutine.
        /// </summary>
        protected bool didUpdateThisFrame = false;

        /// <summary>
        /// The RenderTexture.
        /// </summary>
        protected RenderTexture renderTexture;

        /// <summary>
        /// Current AsyncGPUReadbackRequest for direct data access.
        /// </summary>
        protected AsyncGPUReadbackRequest asyncGPUReadbackRequestBuffer;

        /// <summary>
        /// FPS Manager
        /// </summary>
        private FpsManager fpsManager;

        /// <summary>
        /// Event to signal when frame data is ready for processing
        /// </summary>
        private ManualResetEventSlim frameDataReadyEvent;

        /// <summary>
        /// The waitForEndOfFrameCoroutine
        /// </summary>
        protected IEnumerator waitForEndOfFrameCoroutine;

        protected override void OnValidate()
        {
            base.OnValidate();
            _requestedMatUpdateFPS = Mathf.Clamp(_requestedMatUpdateFPS, -1f, float.MaxValue);
        }

        // Update is called once per frame
        protected override void Update()
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

                if (!webCamTexture.isPlaying) return;

                // If RequestedMatUpdateFPS is greater than webCamTexture.requestedFPS, match the update of webCamTexture.didUpdateThisFrame.
                if (RequestedMatUpdateFPS >= webCamTexture.requestedFPS)
                {
                    if (!webCamTexture.didUpdateThisFrame) return;

                    CallReadback();
                }
                else
                {
                    fpsManager.Update(Time.deltaTime, CallReadback);
                }
            }
        }

        protected virtual void CallReadback()
        {
#if UNITY_IOS && !UNITY_EDITOR
            Graphics.Blit(webCamTexture, renderTexture);
#else
            Graphics.Blit(webCamTexture, renderTexture, new Vector2(1, -1), new Vector2(0, 1));
#endif

            // Create the AsyncGPUReadbackRequest
            AsyncGPUReadback.Request(renderTexture, 0, (completedRequest) => { OnCompleteReadback(completedRequest); });

            //AsyncGPUReadback.Request(renderTexture, 0, GraphicsFormat.R8G8B8A8_UNorm, (request) => { OnCompleteReadback(request); });
            //AsyncGPUReadback.Request(renderTexture, 0, GraphicsFormat.R8G8B8A8_SRGB, (request) => { OnCompleteReadback(request); });

            //AsyncGPUReadback.Request(webCamTexture, 0, (request) => { OnCompleteReadback(request); });
            //AsyncGPUReadback.Request(webCamTexture, 0, GraphicsFormat.R8G8B8A8_SRGB, (request) => { OnCompleteReadback(request); });
        }

        /// <summary>
        /// Thread-safe callback method for completed AsyncGPUReadbackRequest.
        /// This method may be called from a background thread, so it stores the request
        /// for direct data access in the main thread.
        /// </summary>
        /// <param name="request">The completed AsyncGPUReadbackRequest.</param>
        protected virtual void OnCompleteReadback(AsyncGPUReadbackRequest request)
        {
            //Debug.Log("WebCamTexture2MatAsyncGPUHelper:: " + "OnCompleteReadback");

            // Store the request for direct data access
            asyncGPUReadbackRequestBuffer = request;

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

                    if (asyncGPUReadbackRequestBuffer.hasError)
                    {
                        frameDataReadyEvent.Set();
                        continue;
                    }

#if !OPENCV_DONT_USE_UNSAFE_CODE
                    // Use the stored AsyncGPUReadbackRequest data directly
                    OpenCVMatUtils.CopyToMat(asyncGPUReadbackRequestBuffer.GetData<byte>(), baseMat);
#endif

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
        /// Coroutine to get first frame asynchronously using AsyncGPUReadback.
        /// </summary>
        /// <returns>IEnumerator for coroutine.</returns>
        protected virtual IEnumerator _GetFirstFrameCoroutine()
        {
            bool isCompleted = false;
            AsyncGPUReadbackRequest request = default(AsyncGPUReadbackRequest);

            // Blit webCamTexture to renderTexture
#if UNITY_IOS && !UNITY_EDITOR
            Graphics.Blit(webCamTexture, renderTexture);
#else
            Graphics.Blit(webCamTexture, renderTexture, new Vector2(1, -1), new Vector2(0, 1));
#endif

            // Start AsyncGPUReadback request
            AsyncGPUReadback.Request(renderTexture, 0, (completedRequest) =>
            {
                request = completedRequest;
                isCompleted = true;
            });

            // Wait for completion
            while (!isCompleted)
            {
                yield return null;
            }

            // Convert to baseMat
            if (request.hasError)
            {
                Debug.LogError("WebCamTexture2MatAsyncGPUHelper:: " + "GPU readback error detected during initialization.");
            }
            else
            {
#if !OPENCV_DONT_USE_UNSAFE_CODE
                // Use the AsyncGPUReadbackRequest data directly
                OpenCVMatUtils.CopyToMat(request.GetData<byte>(), baseMat);
#endif
            }
        }

        /// <summary>
        /// Initialize this instance by coroutine.
        /// </summary>
        protected override IEnumerator _Initialize()
        {
            if (hasInitDone)
            {
                CancelWaitForEndOfFrameCoroutine();
                ReleaseResources();

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }

            bool useAsyncGPUReadback = SystemInfo.supportsAsyncGPUReadback;
#if OPENCV_DONT_USE_UNSAFE_CODE
            Debug.Log("WebCamTexture2MatAsyncGPUHelper:: " + "OPENCV_DONT_USE_UNSAFE_CODE is defined, so AsyncGPUReadback is not supported");
            useAsyncGPUReadback = false;
#endif

            if (!useAsyncGPUReadback)
            {
                isInitWaiting = false;
                initCoroutine = null;

                if (_onErrorOccurred != null)
                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.ASYNC_GPU_READBACK_IS_NOT_SPPORTED, string.Empty);

                yield break;
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
                    Debug.Log("WebCamTexture2MatAsyncGPUHelper:: " + "Cannot find camera device " + RequestedDeviceName + ".");
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
                if (initFrameCount > _timeoutFrameCount)
                {
                    isTimeout = true;
                    break;
                }
                else if (webCamTexture.didUpdateThisFrame)
                {
                    Debug.Log("WebCamTexture2MatAsyncGPUHelper:: " + "devicename:" + webCamTexture.deviceName + " name:" + webCamTexture.name + " width:" + webCamTexture.width + " height:" + webCamTexture.height + " fps:" + webCamTexture.requestedFPS
                    + " videoRotationAngle:" + webCamTexture.videoRotationAngle + " videoVerticallyMirrored:" + webCamTexture.videoVerticallyMirrored + " isFrongFacing:" + webCamDevice.isFrontFacing);

                    renderTexture = new RenderTexture(webCamTexture.width, webCamTexture.height, 0, GraphicsFormat.R8G8B8A8_SRGB);
                    Debug.Log("renderTexture.graphicsFormat " + renderTexture.graphicsFormat);
#if UNITY_6000_0_OR_NEWER
                    if (!SystemInfo.IsFormatSupported(renderTexture.graphicsFormat, GraphicsFormatUsage.ReadPixels))
#else
                    if (!SystemInfo.IsFormatSupported(renderTexture.graphicsFormat, FormatUsage.ReadPixels))
#endif
                    {
                        Debug.Log("WebCamTexture2MatAsyncGPUHelper:: " + "The format (" + renderTexture.graphicsFormat + ") of the set source texture is unsupported by AsyncGPUReadback.");

                        isInitWaiting = false;
                        initCoroutine = null;

                        if (_onErrorOccurred != null)
                            _onErrorOccurred.Invoke(Source2MatHelperErrorCode.RENDERTEXTURE_GRAPHICS_FORMAT_IS_NOT_SPPORTED, "graphicsFormat " + renderTexture.graphicsFormat);

                        yield break;
                    }

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

                    // Initialize FpsManager
                    fpsManager = new FpsManager(RequestedMatUpdateFPS);

                    // Clear the current request
                    asyncGPUReadbackRequestBuffer = default(AsyncGPUReadbackRequest);

                    frameDataReadyEvent = new ManualResetEventSlim(false);

                    isInitWaiting = false;
                    hasInitDone = true;
                    initCoroutine = null;

                    // Get first frame
                    if (useAsyncGPUReadback)
                    {
                        // Get first frame asynchronously
                        yield return StartCoroutine(_GetFirstFrameCoroutine());
                    }
                    else
                    {
                        // Get first frame synchronously
                        OpenCVMatUtils.WebCamTextureToMat(webCamTexture, baseMat, colors);
                    }

                    // Data copying to baseMat completed.
                    frameDataReadyEvent.Set();

                    didUpdateThisFrame = true;
                    //Debug.Log("start didUpdateThisFrame " + didUpdateThisFrame);

                    // Start a coroutine and wait for WaitForEndOfFrame. If already started, stop and then start.
                    if (waitForEndOfFrameCoroutine != null) StopCoroutine(waitForEndOfFrameCoroutine);
                    waitForEndOfFrameCoroutine = _WaitForFrameEndCoroutine();
                    StartCoroutine(waitForEndOfFrameCoroutine);

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
        /// Start the active camera.
        /// </summary>
        public override void Play()
        {
            if (hasInitDone)
            {
                webCamTexture.Play();
                isPlaying = true;
                isPaused = false; // Resume from paused state or start from stopped state
                fpsManager.IsPaused = false;
            }
        }

        /// <summary>
        /// Pause the active camera.
        /// </summary>
        public override void Pause()
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
                fpsManager.IsPaused = true;
            }
        }

        /// <summary>
        /// Stop the active camera.
        /// </summary>
        public override void Stop()
        {
            if (hasInitDone)
            {
                webCamTexture.Stop();
                isPlaying = false;
                isPaused = false; // Reset to stopped state
                fpsManager.IsPaused = true;
            }
        }

        /// <summary>
        /// Return the frame rate at which the Mat is updated. (interval at which the DidUpdateThisFrame() method becomes true).
        /// </summary>
        /// <returns>The active camera framerate.</returns>
        public virtual float GetMatUpdateFPS()
        {
            if (hasInitDone)
            {
                // If RequestedMatUpdateFPS is greater than webCamTexture.requestedFPS, match the update of webCamTexture.didUpdateThisFrame.
                if (RequestedMatUpdateFPS >= webCamTexture.requestedFPS)
                {
                    return webCamTexture.requestedFPS;
                }
                else
                {
                    return RequestedMatUpdateFPS;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Use this to check if the Mat has changed since the last frame. Since it would not make sense to do expensive video processing in each Update call, check this value before doing any processing.
        /// </summary>
        /// <returns><c>true</c>, if the Mat has been updated <c>false</c> otherwise.</returns>
        public override bool DidUpdateThisFrame()
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
        public override Mat GetMat()
        {
            if (!hasInitDone)
            {
                return null;
            }

            if (frameMat != null && frameMat.IsDisposed)
            {
                Debug.LogWarning("WebCamTexture2MatAsyncGPUHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
                frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(OutputColorFormat)));
            }
            if (rotatedFrameMat != null && rotatedFrameMat.IsDisposed)
            {
                Debug.LogWarning("WebCamTexture2MatAsyncGPUHelper:: " + "Please do not dispose of the Mat returned by GetMat as it will be reused");
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

            // For WebCamTexture2MatAsyncGPUHelper, the flipVertical flag is flipped because the frameMat has already been flipped vertically.
            // On Android and iOS, if rotatedFrameMat is not null, flip FlipHorizontal instead of FlipVertical.
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            if (rotatedFrameMat != null)
            {
                return FlipAndRotateMat(frameMat, rotatedFrameMat, FlipVertical, !FlipHorizontal, webCamDevice.isFrontFacing, webCamTexture.videoRotationAngle, screenOrientation, isScreenOrientationCorrect, isVideoRotationAngleCorrect);
            }
            else
            {
                return FlipAndRotateMat(frameMat, rotatedFrameMat, !FlipVertical, FlipHorizontal, webCamDevice.isFrontFacing, webCamTexture.videoRotationAngle, screenOrientation, isScreenOrientationCorrect, isVideoRotationAngleCorrect);
            }
#else
            return FlipAndRotateMat(frameMat, rotatedFrameMat, !FlipVertical, FlipHorizontal, webCamDevice.isFrontFacing, webCamTexture.videoRotationAngle, screenOrientation, isScreenOrientationCorrect, isVideoRotationAngleCorrect);
#endif
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
        protected override void ReleaseResources()
        {
            isInitWaiting = false;
            hasInitDone = false;

            didUpdateThisFrame = false;
            isPlaying = false;
            isPaused = false;

            if (frameDataReadyEvent != null)
            {
                frameDataReadyEvent.Dispose();
                frameDataReadyEvent = null;
            }

            // Clear the current request
            asyncGPUReadbackRequestBuffer = default(AsyncGPUReadbackRequest);

            if (webCamTexture != null)
            {
                webCamTexture.Stop();
                WebCamTexture.Destroy(webCamTexture);
                webCamTexture = null;
            }
            if (renderTexture != null)
            {
                RenderTexture.Destroy(renderTexture);
                renderTexture = null;
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
        /// Releases all resource used by the <see cref="WebCamTexture2MatAsyncGPUHelper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="WebCamTexture2MatAsyncGPUHelper"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="WebCamTexture2MatAsyncGPUHelper"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="WebCamTexture2MatAsyncGPUHelper"/> so
        /// the garbage collector can reclaim the memory that the <see cref="WebCamTexture2MatAsyncGPUHelper"/> was occupying.</remarks>
        public override void Dispose()
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

                if (_onDisposed != null)
                    _onDisposed.Invoke();
            }
        }
    }
}

#endif
