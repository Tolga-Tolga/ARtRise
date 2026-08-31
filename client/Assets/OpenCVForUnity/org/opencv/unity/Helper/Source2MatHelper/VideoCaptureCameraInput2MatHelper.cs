//#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API

//using OpenCVForUnity.CoreModule;
//using OpenCVForUnity.ImgprocModule;
//using OpenCVForUnity.VideoioModule;
//using System;
//using System.Collections;
//using UnityEngine;

//namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
//{
//    /// <summary>
//    /// A helper component class for obtaining camera frames from OpenCV's <see cref="VideoCapture"/> and converting them to OpenCV <c>Mat</c> format in real-time.
//    /// </summary>
//    /// <remarks>
//    /// The <c>VideoCaptureCameraInput2MatHelper</c> class captures video frames from a device's camera using OpenCV's <see cref="VideoCapture"/>
//    /// and converts each frame to an OpenCV <c>Mat</c> object every frame.
//    /// This component handles camera orientation, rotation, and necessary transformations to ensure the <c>Mat</c> output
//    /// aligns correctly with the device's display orientation.
//    ///
//    /// This component is particularly useful for image processing tasks in Unity, such as computer vision applications,
//    /// where real-time camera input in <c>Mat</c> format is required. It enables seamless integration of OpenCV-based
//    /// image processing algorithms with VideoCapture's camera input.
//    ///
//    /// <strong>Note:</strong> Use the WebCamDevice.isFrontFacing and WebCamTexture.videoRotationAngle properties to flip the camera input image in VideoCaptue to the correct orientation.
//    /// <strong>Note:</strong> This class is experimental and will not work properly on many platforms. Currently, it falls back to WebCamTexture2MatHelper for all but Windows and MacOS platforms.
//    /// <strong>Note:</strong> By setting outputColorFormat to BGR, processing that does not include extra color conversion is performed.
//    /// </remarks>
//    /// <example>
//    /// Attach this component to a GameObject and call <c>GetMat()</c> to retrieve the latest camera frame in <c>Mat</c> format.
//    /// The helper class manages camera start/stop operations and frame updates internally.
//    /// </example>
//    public class VideoCaptureCameraInput2MatHelper : WebCamTexture2MatHelper
//    {
//        //#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_ANDROID || UNITY_IOS) && !DISABLE_VIDEOCAPTURE_API
//#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !DISABLE_VIDEOCAPTURE_API
//        public override float RequestedFPS
//        {
//            get { return _requestedFPS; }
//            set
//            {
//                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
//                if (_requestedFPS != _value)
//                {
//                    _requestedFPS = _value;
//                    if (hasInitDone)
//                        Initialize(IsPlaying());
//                    else if (isInitWaiting)
//                        Initialize(autoPlayAfterInitialize);
//                }
//            }
//        }

//        protected VideoCapture videoCapture;
//        protected int deviceId = 0;
//        protected bool isPlaying = false;
//        protected int videoRotationAngle = 0;
//        protected bool videoVerticallyMirrored = false;

//        // Update is called once per frame
//        protected override void Update()
//        {
//            if (hasInitDone)
//            {
//                // Catch the orientation change of the screen and correct the mat image to the correct direction.
//                if (screenOrientation != Screen.orientation)
//                {
//                    Initialize(IsPlaying());
//                }
//            }
//        }

//        /// <summary>
//        /// Initialize this instance by coroutine.
//        /// </summary>
//        protected override IEnumerator _Initialize()
//        {
//            baseColorFormat = Source2MatHelperColorFormat.BGR;

//            if (hasInitDone)
//            {
//                ReleaseResources();

//                if (onDisposed != null)
//                    onDisposed.Invoke();
//            }

//            isInitWaiting = true;

//            // Wait one frame before starting initialization process
//            yield return null;


//#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
//            // Checks camera permission state.
//            IEnumerator coroutine = hasUserAuthorizedCameraPermission();
//            yield return coroutine;

//            if (!(bool)coroutine.Current)
//            {
//                isInitWaiting = false;
//                initCoroutine = null;

//                if (onErrorOccurred != null)
//                    onErrorOccurred.Invoke(Source2MatHelperErrorCode.CAMERA_PERMISSION_DENIED, string.Empty);

//                yield break;
//            }
//#endif


//            // Creates a WebCamTexture with settings closest to the requested name, resolution, and frame rate.
//            var devices = WebCamTexture.devices;
//            if (devices.Length == 0)
//            {
//                isInitWaiting = false;
//                initCoroutine = null;

//                if (_onErrorOccurred != null)
//                    _onErrorOccurred.Invoke(Source2MatHelperErrorCode.CAMERA_DEVICE_NOT_EXIST, requestedDeviceName);

//                yield break;
//            }

//            if (!String.IsNullOrEmpty(requestedDeviceName))
//            {
//                // Try to parse requestedDeviceName as an index
//                int requestedDeviceIndex = -1;
//                if (Int32.TryParse(requestedDeviceName, out requestedDeviceIndex))
//                {
//                    if (requestedDeviceIndex >= 0 && requestedDeviceIndex < devices.Length)
//                    {
//                        webCamDevice = devices[requestedDeviceIndex];
//                        if (RequestedFPS < 0)
//                            webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight);
//                        else
//                            webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight, (int)RequestedFPS);

//                        deviceId = requestedDeviceIndex;
//                    }
//                }
//                else
//                {
//                    // Search for a device with a matching name
//                    for (int cameraIndex = 0; cameraIndex < devices.Length; cameraIndex++)
//                    {
//                        if (devices[cameraIndex].name == requestedDeviceName)
//                        {
//                            webCamDevice = devices[cameraIndex];
//                            if (RequestedFPS < 0)
//                                webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight);
//                            else
//                                webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight, (int)RequestedFPS);

//                            deviceId = cameraIndex;
//                            break;
//                        }
//                    }
//                }
//                if (webCamTexture == null)
//                    Debug.Log("WebCamTexture2MatHelper:: " + "Cannot find camera device " + requestedDeviceName + ".");
//            }

//            if (webCamTexture == null)
//            {
//                var prioritizedKinds = new WebCamKind[]
//                {
//                    WebCamKind.WideAngle,
//                    WebCamKind.Telephoto,
//                    WebCamKind.UltraWideAngle,
//                    WebCamKind.ColorAndDepth
//                };

//                // Checks how many and which cameras are available on the device
//                foreach (var kind in prioritizedKinds)
//                {
//                    //foreach (var device in devices)
//                    for (int cameraIndex = 0; cameraIndex < devices.Length; cameraIndex++)
//                    {
//                        if (devices[cameraIndex].kind == kind && devices[cameraIndex].isFrontFacing == requestedIsFrontFacing)
//                        {
//                            webCamDevice = devices[cameraIndex];
//                            if (RequestedFPS < 0)
//                                webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight);
//                            else
//                                webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight, (int)RequestedFPS);

//                            deviceId = cameraIndex;
//                            break;
//                        }
//                    }
//                    if (webCamTexture != null) break;
//                }
//            }

//            if (webCamTexture == null)
//            {
//                webCamDevice = devices[0];
//                if (RequestedFPS < 0)
//                    webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight);
//                else
//                    webCamTexture = new WebCamTexture(webCamDevice.name, requestedWidth, requestedHeight, (int)RequestedFPS);

//                deviceId = 0;
//            }


//            // Starts the camera.
//            videoCapture = new VideoCapture(deviceId);
//            webCamTexture.Play();

//            int initFrameCount = 0;
//            bool isTimeout = false;
//            int step = 0;

//            while (true)
//            {
//                if (initFrameCount > timeoutFrameCount)
//                {
//                    isTimeout = true;
//                    break;
//                }
//                else if (step == 0 && webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame)
//                {
//                    videoRotationAngle = webCamTexture.videoRotationAngle;
//                    videoVerticallyMirrored = webCamTexture.videoVerticallyMirrored;
//                    webCamTexture.Stop();

//                    step = 1;

//                    initFrameCount++;
//                    yield return null;
//                }
//                else if (step == 1 && videoCapture.isOpened())
//                {
//                    videoCapture.set(Videoio.CAP_PROP_FRAME_WIDTH, requestedWidth);
//                    videoCapture.set(Videoio.CAP_PROP_FRAME_HEIGHT, requestedHeight);
//                    videoCapture.set(Videoio.CAP_PROP_FPS, (int)RequestedFPS);
//                    baseMat = new Mat();

//                    step = 2;

//                    initFrameCount++;
//                    yield return null;
//                }
//                else if (step == 2 && videoCapture.grab() && videoCapture.retrieve(baseMat) && (baseMat.width() > 0 && baseMat.height() > 0))
//                {
//                    Debug.Log("VideoCaptureCameraInputToMatHelper:: " + "devicename:" + webCamTexture.deviceName + " name:" + webCamTexture.name + " width:" + baseMat.width() + " height:" + baseMat.height() + " fps:" + videoCapture.get(Videoio.CAP_PROP_FPS)
//                    + " videoRotationAngle:" + videoRotationAngle + " videoVerticallyMirrored:" + videoVerticallyMirrored + " isFrongFacing:" + webCamDevice.isFrontFacing);

//                    if (baseColorFormat == outputColorFormat)
//                    {
//                        frameMat = new Mat(baseMat.rows(), baseMat.cols(), baseMat.type(), new Scalar(0, 0, 0, 255));
//                    }
//                    else
//                    {
//                        frameMat = new Mat(baseMat.rows(), baseMat.cols(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)), new Scalar(0, 0, 0, 255));
//                    }

//                    screenOrientation = Screen.orientation;
//                    screenWidth = Screen.width;
//                    screenHeight = Screen.height;

//                    bool isRotatedFrame = false;
//#if !UNITY_EDITOR && !(UNITY_STANDALONE || UNITY_WEBGL)
//                    if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
//                    {
//                        if (!rotate90Degree)
//                            isRotatedFrame = true;
//                    }
//                    else if (rotate90Degree)
//                    {
//                        isRotatedFrame = true;
//                    }
//#else
//                    if (rotate90Degree)
//                        isRotatedFrame = true;
//#endif
//                    if (isRotatedFrame)
//                        rotatedFrameMat = new Mat(frameMat.cols(), frameMat.rows(), CvType.CV_8UC(Source2MatHelperUtils.Channels(outputColorFormat)), new Scalar(0, 0, 0, 255));

//                    isInitWaiting = false;
//                    hasInitDone = true;
//                    initCoroutine = null;

//                    isPlaying = autoPlayAfterInitialize;

//                    if (onInitialized != null)
//                        onInitialized.Invoke();

//                    break;
//                }
//                else
//                {
//                    initFrameCount++;
//                    yield return null;
//                }
//            }

//            if (isTimeout)
//            {
//                webCamTexture.Stop();
//                WebCamTexture.Destroy(webCamTexture);
//                if (baseMat != null)
//                {
//                    baseMat.Dispose();
//                    baseMat = null;
//                }
//                videoCapture.release();
//                videoCapture.Dispose();
//                videoCapture = null;
//                isInitWaiting = false;
//                initCoroutine = null;

//                if (onErrorOccurred != null)
//                    onErrorOccurred.Invoke(Source2MatHelperErrorCode.TIMEOUT, string.Empty);
//            }
//        }

//        /// <summary>
//        /// Start the active camera.
//        /// </summary>
//        public override void Play()
//        {
//            if (hasInitDone && !isPlaying)
//            {
//                if (!videoCapture.isOpened())
//                    videoCapture.open(deviceId);

//                isPlaying = true;
//            }
//        }

//        /// <summary>
//        /// Pause the active camera.
//        /// </summary>
//        public override void Pause()
//        {
//            if (hasInitDone && isPlaying)
//                isPlaying = false;
//        }

//        /// <summary>
//        /// Stop the active camera.
//        /// </summary>
//        public override void Stop()
//        {
//            if (hasInitDone && isPlaying)
//            {
//                if (videoCapture.isOpened())
//                    videoCapture.release();

//                isPlaying = false;
//            }
//        }

//        /// <summary>
//        /// Indicate whether the active camera is currently playing.
//        /// </summary>
//        /// <returns><c>true</c>, if the active camera is playing, <c>false</c> otherwise.</returns>
//        public override bool IsPlaying()
//        {
//            return hasInitDone ? isPlaying : false;
//        }

//        /// <summary>
//        /// Indicate whether the camera is paused.
//        /// </summary>
//        /// <returns><c>true</c>, if the active camera is paused, <c>false</c> otherwise.</returns>
//        public override bool IsPaused()
//        {
//            return hasInitDone ? isPlaying : false;
//        }

//        /// <summary>
//        /// Indicate whether the active camera device is currently front facng.
//        /// </summary>
//        /// <returns><c>true</c>, if the active camera device is front facng, <c>false</c> otherwise.</returns>
//        public override bool IsFrontFacing()
//        {
//            return hasInitDone ? webCamDevice.isFrontFacing : false;
//        }

//        /// <summary>
//        /// Return the active camera device name.
//        /// </summary>
//        /// <returns>The active camera device name.</returns>
//        public override string GetDeviceName()
//        {
//            return hasInitDone ? webCamTexture.deviceName : "";
//        }

//        /// <summary>
//        /// Return the active camera framerate.
//        /// </summary>
//        /// <returns>The active camera framerate.</returns>
//        public override float GetFPS()
//        {
//            return hasInitDone ? (float)videoCapture.get(Videoio.CAP_PROP_FPS) : -1f;
//        }

//        /// <summary>
//        /// Indicate whether the video buffer of the frame has been updated.
//        /// </summary>
//        /// <returns><c>true</c>, if the video buffer has been updated <c>false</c> otherwise.</returns>
//        public override bool DidUpdateThisFrame()
//        {
//            return hasInitDone ? isPlaying : false;
//        }
//#endif

//        /// <summary>
//        /// Return the VideoCapture instanse.
//        /// </summary>
//        /// <returns>The VideoCapture instanse.</returns>
//        public virtual VideoCapture GetVideoCapture()
//        {
//            //#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_ANDROID || UNITY_IOS) && !DISABLE_VIDEOCAPTURE_API
//#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !DISABLE_VIDEOCAPTURE_API
//            return videoCapture;
//#else
//            return null;
//#endif
//        }

//        //#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_ANDROID || UNITY_IOS) && !DISABLE_VIDEOCAPTURE_API
//#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !DISABLE_VIDEOCAPTURE_API
//        /// <summary>
//        /// Get the mat of the current frame.
//        /// </summary>
//        /// <remarks>
//        /// The Mat object's type is 'CV_8UC4' or 'CV_8UC3' or 'CV_8UC1' (ColorFormat is determined by the outputColorFormat setting).
//        /// Please do not dispose of the returned mat as it will be reused.
//        /// </remarks>
//        /// <returns>The mat of the current frame.</returns>
//        public override Mat GetMat()
//        {
//            if (!hasInitDone || !videoCapture.isOpened() || !isPlaying)
//            {
//                return (rotatedFrameMat != null) ? rotatedFrameMat : frameMat;
//            }

//            if (videoCapture.grab())
//                videoCapture.retrieve(baseMat);

//            if (baseColorFormat == outputColorFormat)
//            {
//                baseMat.copyTo(frameMat);
//            }
//            else
//            {
//                Imgproc.cvtColor(baseMat, frameMat, Source2MatHelperUtils.ColorConversionCodes(baseColorFormat, outputColorFormat));
//            }

//#if !UNITY_EDITOR && !(UNITY_STANDALONE || UNITY_WEBGL)
//            if (rotatedFrameMat != null)
//            {
//                if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
//                {
//                    // (Orientation is Portrait, rotate90Degree is false)
//                    if (webCamDevice.isFrontFacing)
//                    {
//                        FlipMat(frameMat, !flipHorizontal, !flipVertical);
//                    }
//                    else
//                    {
//                        FlipMat(frameMat, flipHorizontal, flipVertical);
//                    }
//                }
//                else
//                {
//                    // (Orientation is Landscape, rotate90Degrees=true)
//                    FlipMat(frameMat, flipVertical, flipHorizontal);
//                }
//                Core.rotate(frameMat, rotatedFrameMat, Core.ROTATE_90_CLOCKWISE);
//                return rotatedFrameMat;
//            }
//            else
//            {
//                if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
//                {
//                    // (Orientation is Portrait, rotate90Degree is ture)
//                    if (webCamDevice.isFrontFacing)
//                    {
//                        FlipMat(frameMat, flipHorizontal, flipVertical);
//                    }
//                    else
//                    {
//                        FlipMat(frameMat, !flipHorizontal, !flipVertical);
//                    }
//                }
//                else
//                {
//                    // (Orientation is Landscape, rotate90Degree is false)
//                    FlipMat(frameMat, flipVertical, flipHorizontal);
//                }
//                return frameMat;
//            }
//#else
//            FlipMat(frameMat, flipVertical, flipHorizontal);

//            if (rotatedFrameMat != null)
//            {
//                Core.rotate(frameMat, rotatedFrameMat, Core.ROTATE_90_CLOCKWISE);
//                return rotatedFrameMat;
//            }
//            else
//            {
//                return frameMat;
//            }
//#endif
//        }

//        /// <summary>
//        /// Flip the mat.
//        /// </summary>
//        /// <param name="mat">Mat.</param>
//        protected void FlipMat(Mat mat, bool flipVertical, bool flipHorizontal)
//        {
//            int flipCode = int.MinValue;

//            if (webCamDevice.isFrontFacing)
//            {
//                if (videoRotationAngle == 0 || videoRotationAngle == 90)
//                {
//                    flipCode = 1;
//                }
//                else if (videoRotationAngle == 180 || videoRotationAngle == 270)
//                {
//                    flipCode = 0;
//                }
//            }
//            else
//            {
//                if (videoRotationAngle == 180 || videoRotationAngle == 270)
//                {
//                    flipCode = -1;
//                }
//            }

//            if (flipVertical)
//            {
//                if (flipCode == int.MinValue)
//                {
//                    flipCode = 0;
//                }
//                else if (flipCode == 0)
//                {
//                    flipCode = int.MinValue;
//                }
//                else if (flipCode == 1)
//                {
//                    flipCode = -1;
//                }
//                else if (flipCode == -1)
//                {
//                    flipCode = 1;
//                }
//            }

//            if (flipHorizontal)
//            {
//                if (flipCode == int.MinValue)
//                {
//                    flipCode = 1;
//                }
//                else if (flipCode == 0)
//                {
//                    flipCode = -1;
//                }
//                else if (flipCode == 1)
//                {
//                    flipCode = int.MinValue;
//                }
//                else if (flipCode == -1)
//                {
//                    flipCode = 0;
//                }
//            }

//            if (flipCode > int.MinValue)
//            {
//                Core.flip(mat, mat, flipCode);
//            }
//        }

//        /// <summary>
//        /// To release the resources.
//        /// </summary>
//        protected override void ReleaseResources()
//        {
//            isInitWaiting = false;
//            hasInitDone = false;
//            isPlaying = false;
//            videoRotationAngle = 0;
//            videoVerticallyMirrored = false;

//            if (webCamTexture != null)
//            {
//                webCamTexture.Stop();
//                WebCamTexture.Destroy(webCamTexture);
//                webCamTexture = null;
//            }
//            if (frameMat != null)
//            {
//                frameMat.Dispose();
//                frameMat = null;
//            }
//            if (baseMat != null)
//            {
//                baseMat.Dispose();
//                baseMat = null;
//            }
//            if (rotatedFrameMat != null)
//            {
//                rotatedFrameMat.Dispose();
//                rotatedFrameMat = null;
//            }
//            if (videoCapture != null)
//            {
//                videoCapture.release();
//                videoCapture.Dispose();
//                videoCapture = null;
//            }
//        }
//#endif
//    }
//}

//#endif
