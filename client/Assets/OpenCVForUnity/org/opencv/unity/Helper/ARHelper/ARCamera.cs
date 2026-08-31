using OpenCVForUnity.CoreModule;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using OpenCVForUnity.UnityIntegration.Helper.AR.Editor;
#endif

namespace OpenCVForUnity.UnityIntegration.Helper.AR
{
    /// <summary>
    /// A class that manages AR camera parameters and updates the projection matrix.
    /// This class utilizes camera calibration data to correct object positioning in AR space
    /// and provides functionalities for integrating with an OpenCV-based AR system.
    /// </summary>
    /// <remarks>
    /// - Configures screen and image dimensions and calculates the appropriate projection matrix.
    /// - Manages the intrinsic camera parameters (camera matrix) and lens distortion coefficients.
    /// - Supports object position estimation using OpenCV's camera model.
    /// - Updates the camera's projection matrix to apply the correct perspective transformation.
    /// - Works with `ARHelper` to provide data for correcting the position and orientation of `ARGameObject`.
    /// </remarks>
    [RequireComponent(typeof(Camera))]
    public class ARCamera : MonoBehaviour
    {
        protected const int CAMMATRIXVALUES_MIN_SIZE = 9;
        protected const int CAMMATRIXVALUES_MAX_SIZE = 9;
        protected const int DISTCOEFFSVALUES_MIN_SIZE = 5;
        protected const int DISTCOEFFSVALUES_MAX_SIZE = 14;

        [Header("Camera Parameters")]

        [SerializeField, Tooltip("Set the width of screen.")]
        protected int _screenWidth = 640;

        /// <summary>
        /// Set the width of screen.
        /// </summary>
        public virtual int ScreenWidth
        {
            get => _screenWidth;
            set
            {
                int _value = Mathf.Clamp(value, 0, int.MaxValue);
                if (_screenWidth != _value)
                {
                    _screenWidth = _value;
                    UpdateProjectionMatrix();
                }
            }
        }

        [SerializeField, Tooltip("Set the height of screen.")]
        protected int _screenHeight = 480;

        /// <summary>
        /// Set the height of screen.
        /// </summary>
        public virtual int ScreenHeight
        {
            get => _screenHeight;
            set
            {
                int _value = Mathf.Clamp(value, 0, int.MaxValue);
                if (_screenHeight != _value)
                {
                    _screenHeight = _value;
                    UpdateProjectionMatrix();
                }
            }
        }

        [SerializeField, Tooltip("Set the width of image.")]
        protected int _imageWidth = 640;

        /// <summary>
        /// Set the width of image.
        /// </summary>
        public virtual int ImageWidth
        {
            get => _imageWidth;
            set
            {
                int _value = Mathf.Clamp(value, 0, int.MaxValue);
                if (_imageWidth != _value)
                {
                    _imageWidth = _value;
                    UpdateProjectionMatrix();
                }
            }
        }

        [SerializeField, Tooltip("Set the height of image.")]
        protected int _imageHeight = 480;

        /// <summary>
        /// Set the height of image.
        /// </summary>
        public virtual int ImageHeight
        {
            get => _imageHeight;
            set
            {
                int _value = Mathf.Clamp(value, 0, int.MaxValue);
                if (_imageHeight != _value)
                {
                    _imageHeight = _value;
                    UpdateProjectionMatrix();
                }
            }
        }

        [SerializeField, Tooltip("Set the translation applied to the projection matrix. The x and y components set offsets in normalized screen coordinates.")]
        protected Vector2 _translation = Vector2.zero;

        /// <summary>
        /// Set the translation vector.
        /// </summary>
        public virtual Vector2 Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                UpdateProjectionMatrix();
            }
        }

        [SerializeField, Tooltip("Set the scaling factors for the projection matrix. The x and y components set scaling in the horizontal and vertical directions.")]
        protected Vector2 _scale = Vector2.one;

        /// <summary>
        /// Set the scale vector.
        /// </summary>
        public virtual Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                UpdateProjectionMatrix();
            }
        }

        [SerializeField, Tooltip("Specifies the initial value of camMatrix used for camera calibration. If the number of elements in this array is less than 9, it is automatically calculated from the Screen's Width and Height and the Image's Width and Height.")]
#if UNITY_EDITOR
        [LabeledArray("f_x", "01", "c_x", "10", "f_y", "c_y", "20", "21", "22")]
#endif
        protected double[] _camMatrixValues = null;

        /// <summary>
        /// Specifies the initial value of camMatrix used for camera calibration.  If the number of elements in this array is less than 9, it is automatically calculated from the Screen's Width and Height and the Image's Width and Height.
        /// </summary>
        public virtual double[] CamMatrixValues
        {
            get => _camMatrixValues;
            set
            {
                _camMatrixValues = value;
                _camMatrixValues = ValidateArraySize(_camMatrixValues, CAMMATRIXVALUES_MAX_SIZE);
                UpdateProjectionMatrix();
            }
        }

        [SerializeField, Tooltip("Specifies the initial value of distCoeffs used for camera calibration. If the number of elements in this array is less than 5, all elements are set to 0.")]
#if UNITY_EDITOR
        [LabeledArray("k_1", "k_2", "k_3", "p_1", "p_2", "k_4", "k_5", "k_6", "s_1", "s_2", "s_3", "s_4", "τ_x", "τ_y")]
#endif
        protected double[] _distCoeffsValues = null;

        /// <summary>
        /// Specifies the initial value of distCoeffs used for camera calibration. If the number of elements in this array is less than 5, all elements are set to 0.
        /// </summary>
        public virtual double[] DistCoeffsValues
        {
            get => _distCoeffsValues;
            set
            {
                _distCoeffsValues = value;
                _distCoeffsValues = ValidateArraySize(_distCoeffsValues, DISTCOEFFSVALUES_MAX_SIZE);
                UpdateProjectionMatrix();
            }
        }

        internal Matrix4x4 _opencvCameraProjectionMatrix;

        protected Mat _camMatrix;
        protected MatOfDouble _distCoeffs;
        protected Camera _cachedCamera;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            //Debug.Log("ARCamera Awake()");
            Initialize();
        }

        /// <summary>
        /// Called when the MonoBehaviour will be destroyed.
        /// This method is used to release resources and perform cleanup operations.
        /// </summary>
        protected virtual void OnDestroy()
        {
            //Debug.Log("ARCamera OnDestroy()");
            Dispose();
        }

        /// <summary>
        /// Called when the script is loaded or a value is changed in the Inspector.
        /// Ensures that the screen and image dimensions are within valid ranges,
        /// validates the camera matrix and distortion coefficients arrays, and updates the projection matrix.
        /// </summary>
        protected virtual void OnValidate()
        {
            //Debug.Log("OnValidate");

            _screenWidth = Mathf.Clamp(_screenWidth, 0, int.MaxValue);
            _screenHeight = Mathf.Clamp(_screenHeight, 0, int.MaxValue);
            _imageWidth = Mathf.Clamp(_imageWidth, 0, int.MaxValue);
            _imageHeight = Mathf.Clamp(_imageHeight, 0, int.MaxValue);

            _camMatrixValues = ValidateArraySize(_camMatrixValues, CAMMATRIXVALUES_MAX_SIZE);
            _distCoeffsValues = ValidateArraySize(_distCoeffsValues, DISTCOEFFSVALUES_MAX_SIZE);

            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Initializes resources and sets the initial values.
        /// This method is called when Awake() is called.
        /// </summary>
        public virtual void Initialize()
        {
            //Debug.Log("ARCamera Initialize()");
            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Disposes resources.
        /// This method is called when OnDestroy() is called.
        /// </summary>
        public virtual void Dispose()
        {
            //Debug.Log("ARCamera Dispose");
            _camMatrix?.Dispose(); _camMatrix = null;
            _distCoeffs?.Dispose(); _distCoeffs = null;
        }

        /// <summary>
        /// Sets the AR camera parameters and updates the projection matrix.
        /// </summary>
        /// <param name="screenWidth">The width of the screen.</param>
        /// <param name="screenHeight">The height of the screen.</param>
        /// <param name="imageWidth">The width of the image.</param>
        /// <param name="imageHeight">The height of the image.</param>
        /// <param name="translation">The translation vector applied to the projection matrix.</param>
        /// <param name="scale">The scaling factors for the projection matrix.</param>
        /// <param name="camMatrixValues">Optional initial values for the camera matrix used for calibration. If camMatrixValues.Length is less than CAMMATRIXVALUES_MIN_SIZE then camMatrixValues is calculated and set based on the values of imageWidth and imageWidth.</param>
        /// <param name="distCoeffsValues">Optional initial values for the distortion coefficients used for calibration. If distCoeffsValues.Length is less than DISTCOEFFSVALUES_MIN_SIZE then distCoeffsValues is calculated and set based on the values of imageWidth and imageWidth.</param>
        public virtual void SetARCameraParameters(int screenWidth, int screenHeight, int imageWidth, int imageHeight, Vector2 translation, Vector2 scale, double[] camMatrixValues = null, double[] distCoeffsValues = null)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _imageWidth = imageWidth;
            _imageHeight = imageHeight;

            _translation = translation;
            _scale = scale;

            if (camMatrixValues != null)
                _camMatrixValues = ValidateArraySize(camMatrixValues, 9);
            if (distCoeffsValues != null)
                _distCoeffsValues = ValidateArraySize(distCoeffsValues, 14);

            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Set normalized CamMatrixValues calculated from image size.
        /// The calculated values are a simplified camera matrix with a unit aspect ratio and normalized parameters.
        /// </summary>
        public virtual void SetCamMatrixValuesFromImageSize()
        {
            _camMatrixValues = new double[0];

            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Updates the projection matrix of the AR camera based on the current parameters.
        /// </summary>
        public virtual void UpdateProjectionMatrix()
        {
            //Debug.Log("UpdateProjectionMatrix");

            // Initialise camMatrix.
            if (_camMatrixValues.Length < CAMMATRIXVALUES_MIN_SIZE)
            {
                _camMatrixValues = new double[CAMMATRIXVALUES_MIN_SIZE];

                // Set camera parameters
                int max_d = (int)Mathf.Max(_imageWidth, _imageHeight);
                double _fx = max_d;
                double _fy = max_d;
                double _cx = _imageWidth / 2.0f;
                double _cy = _imageHeight / 2.0f;

                _camMatrixValues[0] = _fx;
                _camMatrixValues[1] = 0;
                _camMatrixValues[2] = _cx;
                _camMatrixValues[3] = 0;
                _camMatrixValues[4] = _fy;
                _camMatrixValues[5] = _cy;
                _camMatrixValues[6] = 0;
                _camMatrixValues[7] = 0;
                _camMatrixValues[8] = 1.0f;
            }

            // Initialise distCoeff.
            if (_distCoeffsValues.Length < DISTCOEFFSVALUES_MIN_SIZE)
            {
                _distCoeffsValues = new double[DISTCOEFFSVALUES_MIN_SIZE];
            }

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
#endif
                if (_camMatrix == null)
                    _camMatrix = new Mat(3, 3, CvType.CV_64FC1);
                OpenCVMatUtils.CopyToMat(_camMatrixValues, _camMatrix);
                //Debug.Log("camMatrix " + camMatrix.dump());

                if (_distCoeffs == null)
                {
                    _distCoeffs = new MatOfDouble(_distCoeffsValues);
                }
                else
                {
                    if (_distCoeffs.total() != _distCoeffsValues.Length)
                    {
                        _distCoeffs.Dispose();
                        _distCoeffs = new MatOfDouble(_distCoeffsValues);
                    }
                }
                _distCoeffs.put(0, 0, _distCoeffsValues);
                //Debug.Log("distCoeffs " + distCoeffs.dump());
#if UNITY_EDITOR
            }
#endif

            // Get the camera component if it is not already cached.
            if (_cachedCamera == null) _cachedCamera = GetComponent<Camera>();

            // Calculate and set the projectionMatrix of the ARCamera.
            _cachedCamera.ResetProjectionMatrix();

            double fx = _camMatrixValues[0];
            double fy = _camMatrixValues[4];
            double cx = _camMatrixValues[2];
            double cy = _camMatrixValues[5];

            // Near and far clip planes
            float nearClip = _cachedCamera.nearClipPlane;
            float farClip = _cachedCamera.farClipPlane;

            // Calculate the projection matrix
            Matrix4x4 projectionMatrix = CalculateProjectionMatrixFromCameraMatrixValuesWithTranslationAndScale((float)fx, (float)fy, (float)cx, (float)cy, _imageWidth, _imageHeight, nearClip, farClip, Translation, Scale, _screenWidth, _screenHeight);
            _cachedCamera.projectionMatrix = projectionMatrix;

            // Create opencvCameraProjectionMatrix for calculating isARGameObjectInARCameraViewport
            Matrix4x4 openGLCameraProjectionMatrix = OpenCVARUtils.CalculateProjectionMatrixFromCameraMatrixValues((float)fx, (float)fy, (float)cx, (float)cy, _imageWidth, _imageHeight, nearClip, farClip);
            // Matrix for converting the difference between the positive direction of the Z-axis in OpenGL and the positive direction of the Z-axis in OpenCV.
            Matrix4x4 zaxisInvertionMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));
            _opencvCameraProjectionMatrix = openGLCameraProjectionMatrix * zaxisInvertionMatrix;
        }

        /// <summary>
        /// Get camMatrix.
        /// </summary>
        /// <returns></returns>
        public virtual Mat GetCamMatrix()
        {
            return _camMatrix;
        }

        /// <summary>
        /// Get distCoeffs.
        /// </summary>
        /// <returns></returns>
        public virtual MatOfDouble GetDistCoeffs()
        {
            return _distCoeffs;
        }

        /// <summary>
        /// Set camMatrix.
        /// </summary>
        /// <param name="camMatrix"></param>
        public virtual void SetCamMatrix(Mat camMatrix)
        {
            CopyCamMatrixToCamMatrixValues(camMatrix);

            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Set distCoeffs.
        /// </summary>
        /// <param name="distCoeffs"></param>
        public virtual void SetDistCoeffs(MatOfDouble distCoeffs)
        {
            CopyDistCoeffsToDistCoeffsValues(distCoeffs);
            UpdateProjectionMatrix();
        }

        /// <summary>
        /// Restricts the size of the array.
        /// </summary>
        /// <param name="array">The target array.</param>
        /// <param name="maxSize">The maximum size.</param>
        /// <returns>The array after applying the size restriction.</returns>
        protected virtual double[] ValidateArraySize(double[] array, int maxSize)
        {
            if (array != null && array.Length > maxSize)
            {
                Debug.LogWarning($"The size of the array exceeds the limit of {maxSize}. It will be truncated.");
                System.Array.Resize(ref array, maxSize);
            }
            return array;
        }

        protected virtual void CopyCamMatrixToCamMatrixValues(Mat camMatrix)
        {
            if (camMatrix != null && !camMatrix.empty())
            {
                _camMatrixValues = new double[camMatrix.total()];
                camMatrix.get(0, 0, _camMatrixValues);
                _camMatrixValues = ValidateArraySize(_camMatrixValues, 9);
            }
        }

        protected virtual void CopyDistCoeffsToDistCoeffsValues(MatOfDouble distCoeffs)
        {
            if (distCoeffs != null && !distCoeffs.empty())
            {
                _distCoeffsValues = new double[distCoeffs.total()];
                distCoeffs.get(0, 0, _distCoeffsValues);
                _distCoeffsValues = ValidateArraySize(_distCoeffsValues, 14);
            }
        }

        /// <summary>
        /// Calculate the projection matrix from camera matrix values with additional translation and scaling adjustments.
        /// Converts from the OpenCV coordinate system to the OpenGL coordinate system, accounting for aspect ratio differences
        /// between the image and the screen, as well as custom translation and scaling.
        /// </summary>
        /// <param name="fx">Focal length x.</param>
        /// <param name="fy">Focal length y.</param>
        /// <param name="cx">Image center point x.(principal point x)</param>
        /// <param name="cy">Image center point y.(principal point y)</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="near">The near clipping plane distance.</param>
        /// <param name="far">The far clipping plane distance.</param>
        /// <param name="translation">Translation applied to the projection matrix. The x and y components represent offsets in normalized screen coordinates.</param>
        /// <param name="scale">Scaling factors for the projection matrix. The x and y components represent scaling in the horizontal and vertical directions.</param>
        /// <param name="screenWidth">Screen width in pixels.</param>
        /// <param name="screenHeight">Screen height in pixels.</param>
        /// <returns>
        /// A <see cref="Matrix4x4"/> representing the adjusted projection matrix.
        /// </returns>
        protected virtual Matrix4x4 CalculateProjectionMatrixFromCameraMatrixValuesWithTranslationAndScale(
            float fx,
            float fy,
            float cx,
            float cy,
            float width,
            float height,
            float near,
            float far,
            Vector2 translation,
            Vector2 scale,
            float screenWidth,
            float screenHeight)
        {
            // Screen aspect ratio
            float screenAspect = screenWidth / screenHeight;

            // Image aspect ratio
            float imageAspect = width / height;

            // Aspect ratio correction factor
            float aspectRatioCorrection = screenAspect / imageAspect;

            Matrix4x4 projectionMatrix = new Matrix4x4();

            // Horizontal (x-axis)
            projectionMatrix.m00 = (2 * fx / width) / aspectRatioCorrection * scale.x;
            projectionMatrix.m02 = 1 - 2 * (cx + screenWidth * translation.x / aspectRatioCorrection) / width;

            // Vertical (y-axis)
            projectionMatrix.m11 = (2 * fy / height) * scale.y;
            projectionMatrix.m12 = -1 + 2 * (cy - screenHeight * translation.y) / height;

            // Depth (z-axis)
            projectionMatrix.m22 = -(far + near) / (far - near);
            projectionMatrix.m23 = -2 * far * near / (far - near);

            // Coordinate transformation for projection
            projectionMatrix.m32 = -1;

            return projectionMatrix;
        }
    }
}
