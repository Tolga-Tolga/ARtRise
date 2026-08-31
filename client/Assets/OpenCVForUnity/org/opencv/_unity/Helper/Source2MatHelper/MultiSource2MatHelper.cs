using System;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using OpenCVForUnity.UnityUtils.Helper.Editor;
#endif

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// A versatile helper component class for obtaining frames as OpenCV <c>Mat</c> objects from multiple sources, allowing dynamic switching between different <c>ISource2MatHelper</c> classes.
    /// </summary>
    /// <remarks>
    /// The <c>MultiSource2MatHelper</c> class integrates various <c>ISource2MatHelper</c> implementations, such as <c>WebCamTexture2MatHelper</c>, <c>VideoCapture2MatHelper</c>, <c>UnityVideoPlayer2MatHelper</c>, and <c>Image2MatHelper</c>,
    /// providing a unified interface for accessing frames as <c>Mat</c> objects. Through settings in the Inspector or during runtime, users can select or switch between these helper classes to obtain frames from different sources, such as webcams, video files, images, or textures.
    ///
    /// This component is particularly useful for applications requiring flexible frame input sources, making it easy to integrate various media types into OpenCV-based processing workflows within Unity.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject and select the desired source in the Inspector. Use <c>GetMat()</c> to retrieve the current frame as a <c>Mat</c> object.
    /// The helper class manages source initialization, switching, and frame updates as needed.
    /// </example>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.MultiSource2MatHelper class instead.")]
    public class MultiSource2MatHelper : MonoBehaviour, ISource2MatHelper
    {
        public enum MultiSource2MatHelperClassName : int
        {
            WebCamTexture2MatHelper = 0,
            //VideoCaptureCameraInput2MatHelper,
            VideoCapture2MatHelper,
            UnityVideoPlayer2MatHelper,
            Image2MatHelper,
            AsyncGPUReadback2MatHelper,
            WebCamTexture2MatAsyncGPUHelper,
            CustomSource2MatHelper,
        }

        [Flags]
        protected enum MultiSource2MatHelperClassInterfaces : int
        {
            None = 0,
            Camera = 1 << 0,
            Video = 1 << 1,
            Image = 1 << 2,
            Texture = 1 << 3,
            Custom = 1 << 4,
            MatUpdateFPS = 1 << 5,
            MatTransformation = 1 << 6,
        }

        /// <summary>
        /// If set to true play after completion of initialization.
        /// </summary>
        protected bool autoPlayAfterInitialize;

        protected MultiSource2MatHelperClassName _currentSource2MatHelperClassName = MultiSource2MatHelperClassName.WebCamTexture2MatHelper;

        [SerializeField]
        [HideInInspector]
        protected MultiSource2MatHelperClassInterfaces _currentSource2MatHelperClassInterfaces = MultiSource2MatHelperClassInterfaces.Camera;

        protected ISource2MatHelper _source2MatHelper;
        public ISource2MatHelper source2MatHelper
        {
            get => _source2MatHelper;
        }

        protected virtual MultiSource2MatHelperClassInterfaces ClassNameToClassInterfaces(MultiSource2MatHelperClassName className)
        {
            switch (_requestedSource2MatHelperClassName)
            {
                case MultiSource2MatHelperClassName.WebCamTexture2MatHelper:
                    return MultiSource2MatHelperClassInterfaces.Camera | MultiSource2MatHelperClassInterfaces.MatTransformation;
                case MultiSource2MatHelperClassName.WebCamTexture2MatAsyncGPUHelper:
                    //case MultiSource2MatHelperClassName.VideoCaptureCameraInput2MatHelper:
                    return MultiSource2MatHelperClassInterfaces.Camera | MultiSource2MatHelperClassInterfaces.MatTransformation | MultiSource2MatHelperClassInterfaces.MatUpdateFPS;
                case MultiSource2MatHelperClassName.VideoCapture2MatHelper:
                case MultiSource2MatHelperClassName.UnityVideoPlayer2MatHelper:
                    return MultiSource2MatHelperClassInterfaces.Video | MultiSource2MatHelperClassInterfaces.MatTransformation;
                case MultiSource2MatHelperClassName.Image2MatHelper:
                    return MultiSource2MatHelperClassInterfaces.Image | MultiSource2MatHelperClassInterfaces.MatUpdateFPS | MultiSource2MatHelperClassInterfaces.MatTransformation;
                case MultiSource2MatHelperClassName.AsyncGPUReadback2MatHelper:
                    return MultiSource2MatHelperClassInterfaces.Texture | MultiSource2MatHelperClassInterfaces.MatUpdateFPS | MultiSource2MatHelperClassInterfaces.MatTransformation;
                case MultiSource2MatHelperClassName.CustomSource2MatHelper:
                default:
                    return MultiSource2MatHelperClassInterfaces.Custom;
            }
        }

        public virtual MultiSource2MatHelperClassName GetCurrentSource2MatHelperClassName()
        {
            return _currentSource2MatHelperClassName;
        }

#if UNITY_EDITOR
        [OpenCVForUnityRuntimeDisable]
#endif
        [SerializeField, FormerlySerializedAs("requestedSource2MatHelperClassName"), TooltipAttribute("Select the source to mat helper class name. If CustomSource2MatHelper is selected, only the UnityEvent inspector is overridden for the custom helper class component.")]
        protected MultiSource2MatHelperClassName _requestedSource2MatHelperClassName = MultiSource2MatHelperClassName.WebCamTexture2MatHelper;

        /// <summary>
        /// Select the source to mat helper class name. If CustomSource2MatHelper is selected, only the UnityEvent inspector is overridden for the custom helper class component.
        /// </summary>
        public MultiSource2MatHelperClassName requestedSource2MatHelperClassName
        {
            get => _requestedSource2MatHelperClassName;
            set
            {
                if (_requestedSource2MatHelperClassName != value)
                {
                    _requestedSource2MatHelperClassName = value;

                    _currentSource2MatHelperClassInterfaces = ClassNameToClassInterfaces(_requestedSource2MatHelperClassName);

                    bool autoPlay = true;
                    if (_source2MatHelper != null)
                    {
                        autoPlay = _source2MatHelper.IsInitialized() ? _source2MatHelper.IsPlaying() : autoPlayAfterInitialize;
                        _source2MatHelper.Dispose();
                        Destroy(_source2MatHelper as Component);
                        _source2MatHelper = null;
                    }

                    Initialize(autoPlay);
                }
            }
        }


        #region MonoBehaviour Implementation

        protected virtual void OnValidate()
        {
            _currentSource2MatHelperClassInterfaces = ClassNameToClassInterfaces(_requestedSource2MatHelperClassName);

            _requestedWidth = (int)Mathf.Clamp(_requestedWidth, 0f, float.MaxValue);
            _requestedHeight = (int)Mathf.Clamp(_requestedHeight, 0f, float.MaxValue);
            _requestedFPS = Mathf.Clamp(_requestedFPS, -1f, float.MaxValue);
            _timeoutFrameCount = (int)Mathf.Clamp(_timeoutFrameCount, 0f, float.MaxValue);
        }

        #endregion


        #region ICameraSource2MatHelper Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Camera, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedDeviceName"), TooltipAttribute("Set the name of the device to use. (or device index number)")]
        protected string _requestedDeviceName = null;

        /// <summary>
        /// Set the name of the camera device to use. (or device index number)
        /// </summary>
        public virtual string requestedDeviceName
        {
            get => _requestedDeviceName;
            set
            {
                if (_requestedDeviceName != value)
                {
                    _requestedDeviceName = value;
                    if (_source2MatHelper != null && _source2MatHelper is ICameraSource2MatHelper helper)
                        helper.requestedDeviceName = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Camera, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedWidth"), TooltipAttribute("Set the width of camera.")]
        protected int _requestedWidth = 640;

        /// <summary>
        /// Set the width of camera.
        /// </summary>
        public virtual int requestedWidth
        {
            get => _requestedWidth;
            set
            {
                int _value = (int)Mathf.Clamp(value, 0f, float.MaxValue);
                if (_requestedWidth != _value)
                {
                    _requestedWidth = _value;
                    if (_source2MatHelper != null && _source2MatHelper is ICameraSource2MatHelper helper)
                        helper.requestedWidth = _value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Camera, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedHeight"), TooltipAttribute("Set the height of camera.")]
        protected int _requestedHeight = 480;

        /// <summary>
        /// Set the height of camera.
        /// </summary>
        public virtual int requestedHeight
        {
            get => _requestedHeight;
            set
            {
                int _value = (int)Mathf.Clamp(value, 0f, float.MaxValue);
                if (_requestedHeight != _value)
                {
                    _requestedHeight = _value;
                    if (_source2MatHelper != null && _source2MatHelper is ICameraSource2MatHelper helper)
                        helper.requestedHeight = _value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Camera, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedIsFrontFacing"), TooltipAttribute("Set whether to use the front facing camera.")]
        protected bool _requestedIsFrontFacing = false;

        /// <summary>
        /// Set whether to use the front facing camera.
        /// </summary>
        public virtual bool requestedIsFrontFacing
        {
            get => _requestedIsFrontFacing;
            set
            {
                if (_requestedIsFrontFacing != value)
                {
                    _requestedIsFrontFacing = value;
                    if (_source2MatHelper != null && _source2MatHelper is ICameraSource2MatHelper helper)
                        helper.requestedIsFrontFacing = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Camera, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedFPS"), TooltipAttribute("Set the frame rate of camera.")]
        protected float _requestedFPS = 30f;

        /// <summary>
        /// Set the frame rate of camera.
        /// </summary>
        public virtual float requestedFPS
        {
            get => _requestedFPS;
            set
            {
                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
                if (_requestedFPS != _value)
                {
                    _requestedFPS = _value;
                    if (_source2MatHelper != null && _source2MatHelper is ICameraSource2MatHelper helper)
                        helper.requestedFPS = _value;
                }
            }
        }

        #endregion

        #region IVideoSource2MatHelper Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Video, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedVideoFilePath"), TooltipAttribute("Set the video file path, relative to the starting point of the \"StreamingAssets\" folder, or absolute path.")]
        protected string _requestedVideoFilePath = string.Empty;

        /// <summary>
        /// Set the video file path, relative to the starting point of the "StreamingAssets" folder, or absolute path.
        /// </summary>
        public virtual string requestedVideoFilePath
        {
            get => _requestedVideoFilePath;
            set
            {
                if (_requestedVideoFilePath != value)
                {
                    _requestedVideoFilePath = value;
                    if (_source2MatHelper != null && _source2MatHelper is IVideoSource2MatHelper helper)
                        helper.requestedVideoFilePath = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Video, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("loop"), TooltipAttribute("Indicate whether to play this video in a loop.")]
        protected bool _loop = true;

        /// <summary>
        /// Indicate whether to play this video in a loop.
        /// </summary>
        public virtual bool loop
        {
            get => _loop;
            set
            {
                if (_loop != value)
                {
                    _loop = value;
                    if (_source2MatHelper != null && _source2MatHelper is IVideoSource2MatHelper helper)
                        helper.loop = value;
                }
            }
        }

        #endregion

        #region IImageSource2MatHelper Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Image, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedImageFilePath"), TooltipAttribute("Set the image file path, relative to the starting point of the \"StreamingAssets\" folder, or absolute path.")]
        protected string _requestedImageFilePath = string.Empty;

        /// <summary>
        /// Set the image file path, relative to the starting point of the "StreamingAssets" folder, or absolute path.
        /// </summary>
        public virtual string requestedImageFilePath
        {
            get => _requestedImageFilePath;
            set
            {
                if (_requestedImageFilePath != value)
                {
                    _requestedImageFilePath = value;
                    if (_source2MatHelper != null && _source2MatHelper is IImageSource2MatHelper helper)
                        helper.requestedImageFilePath = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Image, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("repeat"), TooltipAttribute("Indicate whether to play this image in a repeat.")]
        protected bool _repeat = true;

        /// <summary>
        /// Indicate whether to play this image in a repeat.
        /// </summary>
        public virtual bool repeat
        {
            get => _repeat;
            set
            {
                if (_repeat != value)
                {
                    _repeat = value;
                    if (_source2MatHelper != null && _source2MatHelper is IImageSource2MatHelper helper)
                        helper.repeat = value;
                }
            }
        }
        #endregion

        #region ITextureSource2MatHelper Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Texture, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("sourceTexture"), TooltipAttribute("Set the source texture.")]
        protected Texture _sourceTexture;

        /// <summary>
        /// Set the source texture.
        /// </summary>
        public virtual Texture sourceTexture
        {
            get => _sourceTexture;
            set
            {
                if (_sourceTexture != value)
                {
                    _sourceTexture = value;
                    if (_source2MatHelper != null && _source2MatHelper is ITextureSource2MatHelper helper)
                        helper.sourceTexture = value;
                }
            }
        }

        #endregion

        #region IMatUpdateFPSProvider Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.MatUpdateFPS, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("requestedMatUpdateFPS"), TooltipAttribute("Set the frame rate of camera.")]
        protected float _requestedMatUpdateFPS = 30f;

        /// <summary>
        /// Set the frame rate of camera.
        /// </summary>
        public virtual float requestedMatUpdateFPS
        {
            get => _requestedMatUpdateFPS;
            set
            {
                float _value = Mathf.Clamp(value, -1f, float.MaxValue);
                if (_requestedMatUpdateFPS != _value)
                {
                    _requestedMatUpdateFPS = _value;
                    if (_source2MatHelper != null && _source2MatHelper is IMatUpdateFPSProvider helper)
                        helper.requestedMatUpdateFPS = _value;
                }
            }
        }

        #endregion

        #region IMatTextureSource2MatHelper Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.MatTransformation, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("rotate90Degree"), TooltipAttribute("Set whether to rotate camera frame 90 degrees. (clockwise)")]
        protected bool _rotate90Degree = false;

        /// <summary>
        /// Set whether to rotate camera frame 90 degrees. (clockwise)
        /// </summary>
        public virtual bool rotate90Degree
        {
            get { return _rotate90Degree; }
            set
            {
                if (_rotate90Degree != value)
                {
                    _rotate90Degree = value;
                    if (_source2MatHelper != null && _source2MatHelper is IMatTransformationProvider helper)
                        helper.rotate90Degree = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.MatTransformation, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("flipVertical"), TooltipAttribute("Set whether to flip vertically.")]
        protected bool _flipVertical = false;

        /// <summary>
        /// Set whether to flip vertically.
        /// </summary>
        public virtual bool flipVertical
        {
            get { return _flipVertical; }
            set
            {
                if (_flipVertical != value)
                {
                    _flipVertical = value;
                    if (_source2MatHelper != null && _source2MatHelper is IMatTransformationProvider helper)
                        helper.flipVertical = value;
                }
            }
        }


#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.MatTransformation, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("flipHorizontal"), TooltipAttribute("Set whether to flip horizontal.")]
        protected bool _flipHorizontal = false;

        /// <summary>
        /// Set whether to flip horizontal.
        /// </summary>
        public virtual bool flipHorizontal
        {
            get { return _flipHorizontal; }
            set
            {
                if (_flipHorizontal != value)
                {
                    _flipHorizontal = value;
                    if (_source2MatHelper != null && _source2MatHelper is IMatTransformationProvider helper)
                        helper.flipHorizontal = value;
                }
            }
        }

        #endregion

        #region CustomClass Properties Implementation

#if UNITY_EDITOR
        [OpenCVForUnityConditionalDisableInInspector("_currentSource2MatHelperClassInterfaces", (int)MultiSource2MatHelperClassInterfaces.Custom, conditionalInvisible: true, runtimeDisable: true, comparisonType: OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)]
#endif
        [SerializeField, FormerlySerializedAs("customClassComponent"), TooltipAttribute("Set the custom class <ISource2MatHelper> component.")]
        protected Component _customClassComponent = null;

        /// <summary>
        /// Set the custom class <ISource2MatHelper> component.
        /// </summary>
        public virtual Component customClassComponent
        {
            get => _customClassComponent;
            set => _customClassComponent = value;
        }

        #endregion

        [Space(10)]

        #region ISource2MatHelper Properties Implementation

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
                    if (_source2MatHelper != null)
                        _source2MatHelper.outputColorFormat = _outputColorFormat;
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
            set
            {
                _timeoutFrameCount = (int)Mathf.Clamp(value, 0f, float.MaxValue);
                if (_source2MatHelper != null)
                    _source2MatHelper.timeoutFrameCount = _timeoutFrameCount;
            }
        }


        [SerializeField, FormerlySerializedAs("onInitialized"), TooltipAttribute("UnityEvent that is triggered when this instance is initialized.")]
        protected UnityEvent _onInitialized;

        /// <summary>
        /// UnityEvent that is triggered when this instance is initialized.
        /// </summary>
        public UnityEvent onInitialized
        {
            get => _onInitialized;
            set
            {
                _onInitialized = value;
                if (_source2MatHelper != null)
                    _source2MatHelper.onInitialized = _onInitialized;
            }
        }


        [SerializeField, FormerlySerializedAs("onDisposed"), TooltipAttribute("UnityEvent that is triggered when this instance is disposed.")]
        protected UnityEvent _onDisposed;

        /// <summary>
        /// UnityEvent that is triggered when this instance is disposed.
        /// </summary>
        public UnityEvent onDisposed
        {
            get => _onDisposed;
            set
            {
                _onDisposed = value;
                if (_source2MatHelper != null)
                    _source2MatHelper.onDisposed = _onDisposed;
            }
        }


        [SerializeField, FormerlySerializedAs("onErrorOccurred"), TooltipAttribute("UnityEvent that is triggered when this instance is error Occurred.")]
        protected Source2MatHelperErrorUnityEvent _onErrorOccurred;

        /// <summary>
        /// UnityEvent that is triggered when this instance is error Occurred.
        /// </summary>
        public Source2MatHelperErrorUnityEvent onErrorOccurred
        {
            get => _onErrorOccurred;
            set
            {
                _onErrorOccurred = value;
                if (_source2MatHelper != null)
                    _source2MatHelper.onErrorOccurred = _onErrorOccurred;
            }
        }

        #endregion

        #region ISource2MatHelper Methods Implementation

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="autoPlay">If set to <c>true</c> play after completion of initialization.</param>
        public virtual void Initialize(bool autoPlay = true)
        {
            autoPlayAfterInitialize = autoPlay;

            if (_source2MatHelper == null)
            {
                switch (_requestedSource2MatHelperClassName)
                {
#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API
                    case MultiSource2MatHelperClassName.WebCamTexture2MatHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(WebCamTexture2MatHelper)) as ISource2MatHelper;
                        break;
                    //case MultiSource2MatHelperClassName.VideoCaptureCameraInput2MatHelper:
                    //    _source2MatHelper = gameObject.AddComponent(typeof(VideoCaptureCameraInput2MatHelper)) as ISource2MatHelper;
                    //    break;
#endif
                    case MultiSource2MatHelperClassName.VideoCapture2MatHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(VideoCapture2MatHelper)) as ISource2MatHelper;
                        break;
                    case MultiSource2MatHelperClassName.UnityVideoPlayer2MatHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(UnityVideoPlayer2MatHelper)) as ISource2MatHelper;
                        break;
                    case MultiSource2MatHelperClassName.Image2MatHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(Image2MatHelper)) as ISource2MatHelper;
                        break;
                    case MultiSource2MatHelperClassName.AsyncGPUReadback2MatHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(AsyncGPUReadback2MatHelper)) as ISource2MatHelper;
                        break;
#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API
                    case MultiSource2MatHelperClassName.WebCamTexture2MatAsyncGPUHelper:
                        _source2MatHelper = gameObject.AddComponent(typeof(WebCamTexture2MatAsyncGPUHelper)) as ISource2MatHelper;
                        break;
#endif
                    case MultiSource2MatHelperClassName.CustomSource2MatHelper:
                        _source2MatHelper = _customClassComponent as ISource2MatHelper;
                        break;
                }

                _currentSource2MatHelperClassName = _requestedSource2MatHelperClassName;
            }

            if (_source2MatHelper == null)
            {
                Debug.LogError("MultiSource2MatHelper:: " + "requestedCustomClass == null, <ISource2MatHelper> component must be set.");
                return;
            }
            else
            {
                _source2MatHelper.outputColorFormat = _outputColorFormat;
                _source2MatHelper.timeoutFrameCount = _timeoutFrameCount;
                _source2MatHelper.onInitialized = _onInitialized;
                _source2MatHelper.onDisposed = _onDisposed;
                _source2MatHelper.onErrorOccurred = _onErrorOccurred;

                if (_requestedSource2MatHelperClassName != MultiSource2MatHelperClassName.CustomSource2MatHelper)
                {
                    if (_source2MatHelper is ICameraSource2MatHelper cameraHelper)
                    {
                        cameraHelper.requestedDeviceName = _requestedDeviceName;
                        cameraHelper.requestedWidth = _requestedWidth;
                        cameraHelper.requestedHeight = _requestedHeight;
                        cameraHelper.requestedIsFrontFacing = _requestedIsFrontFacing;
                        cameraHelper.requestedFPS = _requestedFPS;
                    }

                    if (_source2MatHelper is IVideoSource2MatHelper videoHelper)
                    {
                        videoHelper.requestedVideoFilePath = _requestedVideoFilePath;
                        videoHelper.loop = _loop;
                    }

                    if (_source2MatHelper is IImageSource2MatHelper imageHelper)
                    {
                        imageHelper.requestedImageFilePath = _requestedImageFilePath;
                        imageHelper.repeat = _repeat;
                    }

                    if (_source2MatHelper is ITextureSource2MatHelper textureHelper)
                    {
                        textureHelper.sourceTexture = _sourceTexture;
                    }

                    if (_source2MatHelper is IMatUpdateFPSProvider matUpdateFPSProvider)
                    {
                        matUpdateFPSProvider.requestedMatUpdateFPS = _requestedMatUpdateFPS;
                    }

                    if (_source2MatHelper is IMatTransformationProvider matTransformationProvider)
                    {
                        matTransformationProvider.rotate90Degree = _rotate90Degree;
                        matTransformationProvider.flipVertical = _flipVertical;
                        matTransformationProvider.flipHorizontal = _flipHorizontal;
                    }
                }

                _source2MatHelper.Initialize(autoPlay);
            }
        }

        /// <summary>
        /// Indicate whether this instance has been initialized.
        /// </summary>
        /// <returns><c>true</c>, if this instance has been initialized, <c>false</c> otherwise.</returns>
        public virtual bool IsInitialized()
        {
            return _source2MatHelper != null ? _source2MatHelper.IsInitialized() : false;
        }

        /// <summary>
        /// Start the source device.
        /// </summary>
        public virtual void Play()
        {
            if (IsInitialized())
                _source2MatHelper.Play();
        }

        /// <summary>
        /// Pause the source device.
        /// </summary>
        public virtual void Pause()
        {
            if (IsInitialized())
                _source2MatHelper.Pause();
        }

        /// <summary>
        /// Stop the source device.
        /// </summary>
        public virtual void Stop()
        {
            if (IsInitialized())
                _source2MatHelper.Stop();
        }

        /// <summary>
        /// Indicate whether the source device is currently playing.
        /// </summary>
        /// <returns><c>true</c>, if the source device is playing, <c>false</c> otherwise.</returns>
        public virtual bool IsPlaying()
        {
            return IsInitialized() ? _source2MatHelper.IsPlaying() : false;
        }

        /// <summary>
        /// Indicate whether the device is paused.
        /// </summary>
        /// <returns><c>true</c>, if the device is paused, <c>false</c> otherwise.</returns>
        public virtual bool IsPaused()
        {
            return IsInitialized() ? _source2MatHelper.IsPaused() : false;
        }

        /// <summary>
        /// Return the source device name.
        /// </summary>
        /// <returns>The source device name.</returns>
        public virtual string GetDeviceName()
        {
            return IsInitialized() ? _source2MatHelper.GetDeviceName() : "";
        }

        /// <summary>
        /// Return the source width.
        /// </summary>
        /// <returns>The source width.</returns>
        public virtual int GetWidth()
        {
            return IsInitialized() ? _source2MatHelper.GetWidth() : -1;
        }

        /// <summary>
        /// Return the source height.
        /// </summary>
        /// <returns>The source height.</returns>
        public virtual int GetHeight()
        {
            return IsInitialized() ? _source2MatHelper.GetHeight() : -1;
        }

        /// <summary>
        /// Return the source base color format.
        /// </summary>
        /// <returns>The source base color format.</returns>
        public virtual Source2MatHelperColorFormat GetBaseColorFormat()
        {
            return _source2MatHelper != null ? _source2MatHelper.GetBaseColorFormat() : Source2MatHelperColorFormat.RGBA;
        }

        /// <summary>
        /// Indicate whether the source buffer of the frame has been updated.
        /// </summary>
        /// <returns><c>true</c>, if the source buffer has been updated <c>false</c> otherwise.</returns>
        public virtual bool DidUpdateThisFrame()
        {
            return IsInitialized() ? _source2MatHelper.DidUpdateThisFrame() : false;
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
            return IsInitialized() ? _source2MatHelper.GetMat() : null;
        }

        public virtual void Dispose()
        {
            if (IsInitialized())
                _source2MatHelper.Dispose();
        }

        #endregion

    }
}
