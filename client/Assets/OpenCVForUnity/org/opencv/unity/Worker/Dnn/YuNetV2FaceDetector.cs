#if !UNITY_WSA_10_0

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.DnnModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityIntegration.Worker.DataStruct;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// YuNetV2 face detector implementation.
    /// This class provides functionality for face detection using the YuNetV2 model implemented with OpenCV's DNN module.
    /// Referring to https://github.com/opencv/opencv_zoo/tree/main/models/face_detection_yunet
    ///
    /// [Tested Models]
    /// face_detection_yunet_2023mar.onnx https://github.com/opencv/opencv_zoo/blob/main/models/face_detection_yunet/face_detection_yunet_2023mar.onnx
    /// yunet_n_320_320.onnx https://github.com/ShiqiYu/libfacedetection.train/blob/master/onnx/yunet_n_320_320.onnx
    /// yunet_n_640_640.onnx https://github.com/ShiqiYu/libfacedetection.train/blob/master/onnx/yunet_n_640_640.onnx
    /// yunet_s_320_320.onnx https://github.com/ShiqiYu/libfacedetection.train/blob/master/onnx/yunet_s_320_320.onnx
    /// yunet_s_640_640.onnx https://github.com/ShiqiYu/libfacedetection.train/blob/master/onnx/yunet_s_640_640.onnx
    /// </summary>
    public class YuNetV2FaceDetector : ProcessingWorkerBase
    {
        protected static readonly Scalar SCALAR_BLACK = new Scalar(0, 0, 0, 255);
        protected static readonly Scalar BBOX_COLOR = new Scalar(0, 255, 0, 255);
        protected static readonly Scalar[] KEY_POINTS_COLORS = new Scalar[]
        {
            new(0, 0, 255, 255), // # right eye
            new(255, 0, 0, 255), // # left eye
            new(255, 255, 0, 255), // # nose tip
            new(0, 255, 255, 255), // # mouth right
            new(0, 255, 0, 255), // # mouth left
            new(255, 255, 255, 255)
        };

        protected Size _inputSize;
        protected float _confThreshold;
        protected float _nmsThreshold;
        protected int _topK;
        protected int _backend;
        protected int _target;
        protected FaceDetectorYN _detectionModel;
        protected Mat _inputMat;
        protected Mat _output0Buffer;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
        private float[] _allResultBuffer;
#endif

        /// <summary>
        /// Initializes a new instance of the YuNetV2FaceDetector class.
        /// </summary>
        /// <param name="modelFilepath">Path to the model file.</param>
        /// <param name="configFilepath">Path to the config file.</param>
        /// <param name="inputSize">Input size for the network (default: 320x320).</param>
        /// <param name="confThreshold">Confidence threshold for filtering detections.</param>
        /// <param name="nmsThreshold">Non-maximum suppression threshold.</param>
        /// <param name="topK">Maximum number of output detections.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public YuNetV2FaceDetector(string modelFilepath, string configFilepath, Size inputSize, float confThreshold = 0.6f,
                                    float nmsThreshold = 0.3f, int topK = 100,
                                    int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));
            if (inputSize == null)
                throw new ArgumentNullException(nameof(inputSize), "Input size cannot be null.");

            _inputSize = new Size(inputSize.width > 0 ? inputSize.width : 320, inputSize.height > 0 ? inputSize.height : 320);
            _confThreshold = Mathf.Clamp01(confThreshold);
            _nmsThreshold = Mathf.Clamp01(nmsThreshold);
            _topK = Math.Max(1, topK);
            _backend = backend;
            _target = target;

            try
            {
                _detectionModel = FaceDetectorYN.create(modelFilepath, configFilepath, _inputSize, _confThreshold, _nmsThreshold, _topK, _backend, _target);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _output0Buffer = new Mat(_topK, 15, CvType.CV_32FC1);

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _allResultBuffer = new float[_topK * 15];
#endif
        }

        /// <summary>
        /// Visualizes the detection result on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">The result Mat returned by Detect method.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.cols() < 15)
                throw new ArgumentException("Invalid result matrix. It must have at least 15 columns.");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<FaceDetection5LandmarkData> data = ToStructuredDataAsSpan(result);
#else
            FaceDetection5LandmarkData[] data = ToStructuredData(result);
#endif

            for (int i = 0; i < data.Length; i++)
            {
                ref readonly var d = ref data[i];
                float left = d.X;
                float top = d.Y;
                float right = d.X + d.Width;
                float bottom = d.Y + d.Height;
                float score = d.Score;

                var bbc = BBOX_COLOR.ToValueTuple();
                var bbcolor = isRGB ? bbc : (bbc.v2, bbc.v1, bbc.v0, bbc.v3);

                Imgproc.rectangle(image, (left, top), (right, bottom), bbcolor, 2);

                string label = score.ToString("F4");

                int[] baseLine = new int[1];
                var labelSize = Imgproc.getTextSizeAsValueTuple(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                top = Mathf.Max((float)top, (float)labelSize.height);
                Imgproc.rectangle(image, (left, top - labelSize.height),
                    (left + labelSize.width, top + baseLine[0]), bbcolor, Core.FILLED);
                Imgproc.putText(image, label, (left, top), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, SCALAR_BLACK.ToValueTuple(), 1, Imgproc.LINE_AA);

                // draw landmark points
                Imgproc.circle(image, (d.RightEye.Item1, d.RightEye.Item2), 2,
                    isRGB ? KEY_POINTS_COLORS[0].ToValueTuple() : (KEY_POINTS_COLORS[0].val[2], KEY_POINTS_COLORS[0].val[1], KEY_POINTS_COLORS[0].val[0], KEY_POINTS_COLORS[0].val[3]), 2);
                Imgproc.circle(image, (d.LeftEye.Item1, d.LeftEye.Item2), 2,
                    isRGB ? KEY_POINTS_COLORS[1].ToValueTuple() : (KEY_POINTS_COLORS[1].val[2], KEY_POINTS_COLORS[1].val[1], KEY_POINTS_COLORS[1].val[0], KEY_POINTS_COLORS[1].val[3]), 2);
                Imgproc.circle(image, (d.Nose.Item1, d.Nose.Item2), 2,
                    isRGB ? KEY_POINTS_COLORS[2].ToValueTuple() : (KEY_POINTS_COLORS[2].val[2], KEY_POINTS_COLORS[2].val[1], KEY_POINTS_COLORS[2].val[0], KEY_POINTS_COLORS[2].val[3]), 2);
                Imgproc.circle(image, (d.RightMouth.Item1, d.RightMouth.Item2), 2,
                    isRGB ? KEY_POINTS_COLORS[3].ToValueTuple() : (KEY_POINTS_COLORS[3].val[2], KEY_POINTS_COLORS[3].val[1], KEY_POINTS_COLORS[3].val[0], KEY_POINTS_COLORS[3].val[3]), 2);
                Imgproc.circle(image, (d.LeftMouth.Item1, d.LeftMouth.Item2), 2,
                    isRGB ? KEY_POINTS_COLORS[4].ToValueTuple() : (KEY_POINTS_COLORS[4].val[2], KEY_POINTS_COLORS[4].val[1], KEY_POINTS_COLORS[4].val[0], KEY_POINTS_COLORS[4].val[3]), 2);
            }

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(128);

                for (int i = 0; i < data.Length; ++i)
                {
                    ref readonly var d = ref data[i];
                    float left = d.X;
                    float top = d.Y;
                    float right = d.X + d.Width;
                    float bottom = d.Y + d.Height;
                    float score = d.Score;

                    sb.AppendFormat("-----------face {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.AppendFormat("Score: {0:F4}", score);
                    sb.AppendLine();
                    sb.AppendFormat("Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", left, top, right, bottom);
                    sb.AppendLine();
                    sb.Append("Landmarks: ");
                    sb.Append("{");
                    sb.AppendFormat("({0:F3}, {1:F3}), ", d.RightEye.Item1, d.RightEye.Item2);
                    sb.AppendFormat("({0:F3}, {1:F3}), ", d.LeftEye.Item1, d.LeftEye.Item2);
                    sb.AppendFormat("({0:F3}, {1:F3}), ", d.Nose.Item1, d.Nose.Item2);
                    sb.AppendFormat("({0:F3}, {1:F3}), ", d.RightMouth.Item1, d.RightMouth.Item2);
                    sb.AppendFormat("({0:F3}, {1:F3})", d.LeftMouth.Item1, d.LeftMouth.Item2);
                    sb.Append("}");
                    sb.AppendLine();
                }

                Debug.Log(sb.ToString());
            }
        }

        /// <summary>
        /// Detects faces in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for face detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (15 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [x, y, w, h, score, right_eye_x, right_eye_y, left_eye_x, left_eye_y,
        ///            nose_x, nose_y, right_mouth_x, right_mouth_y, left_mouth_x, left_mouth_y]
        /// - Use ToStructuredData() or ToStructuredDataAsSpan() to convert to a more convenient format
        ///
        /// Output options:
        /// - useCopyOutput = false (default): Returns a reference to internal buffer (faster but unsafe across executions)
        /// - useCopyOutput = true: Returns a new copy of the result (thread-safe but slightly slower)
        ///
        /// For better performance in async scenarios, use DetectAsync instead.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing detection result. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Detect(Mat image, bool useCopyOutput = false)
        {
            Execute(image);
            return useCopyOutput ? CopyOutput() : PeekOutput();
        }

        /// <summary>
        /// Detects faces in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for face detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (15 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [x, y, w, h, score, right_eye_x, right_eye_y, left_eye_x, left_eye_y,
        ///            nose_x, nose_y, right_mouth_x, right_mouth_y, left_mouth_x, left_mouth_y]
        /// - Use ToStructuredData() or ToStructuredDataAsSpan() to convert to a more convenient format
        ///
        /// Only one detection operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with detection result. The caller is responsible for disposing this Mat.</returns>
        public virtual async Task<Mat> DetectAsync(Mat image, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(image, cancellationToken);
            return CopyOutput();
        }

        /// <summary>
        /// Converts the detection result matrix to an array of FaceDetection5LandmarkData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Execute method.</param>
        /// <returns>Array of FaceDetection5LandmarkData structures containing face detection information.</returns>
        public virtual FaceDetection5LandmarkData[] ToStructuredData(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new FaceDetection5LandmarkData[0];
            if (result.cols() < 15)
                throw new ArgumentException("Invalid result matrix. It must have at least 15 columns.");

            var dst = new FaceDetection5LandmarkData[result.rows()];
            OpenCVMatUtils.CopyFromMat(result, dst);

            return dst;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Converts the detection result matrix to a span of FaceDetection5LandmarkData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Execute method.</param>
        /// <returns>Span of FaceDetection5LandmarkData structures containing face detection information.</returns>
        public virtual Span<FaceDetection5LandmarkData> ToStructuredDataAsSpan(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return Span<FaceDetection5LandmarkData>.Empty;
            if (result.cols() < 15)
                throw new ArgumentException("Invalid result matrix. It must have at least 15 columns.");
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            return result.AsSpan<FaceDetection5LandmarkData>();
        }
#endif

        protected override Mat[] RunCoreProcessing(Mat[] inputs)
        {
            ThrowIfDisposed();

            if (inputs == null || inputs.Length < 1)
                throw new ArgumentNullException(nameof(inputs), "Inputs cannot be null or have less than 1 elements.");

            if (inputs[0] == null)
                throw new ArgumentNullException(nameof(inputs), "inputs[0] cannot be null.");

            Mat image = inputs[0];

            if (image != null) image.ThrowIfDisposed();
            if (image.channels() != 3)
                throw new ArgumentException("The input image must be in BGR format.");

            // Preprocess
            Mat inputMat = PreProcess(image); // inputMat will be reused, so don't dispose it

            // Forward
            Mat faces = new Mat();
            _detectionModel.detect(inputMat, faces);

            // Postprocess
            faces = PostProcess(faces, inputMat.sizeAsValueTuple(), image.sizeAsValueTuple());

            if (faces.empty())
            {
                return new Mat[] { faces }; // empty mat
            }

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange
            int numDetections = faces.rows();
            Mat submat = _output0Buffer.rowRange(0, numDetections);
            lock (_lockObject)
            {
                faces.copyTo(submat);
            }
            faces.Dispose();

            return new Mat[] { submat }; // [n, 15] (xywh, score, landmarks)
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image)
        {
            // Resize the input image to fit within _inputSize dimensions while preserving aspect ratio

            int h = (int)_inputSize.height;
            int w = (int)_inputSize.width;

            // Calculate aspect ratios
            double imageAspect = (double)image.width() / image.height();
            double targetAspect = (double)w / h;

            int newWidth, newHeight;
            if (imageAspect > targetAspect)
            {
                newWidth = w;
                newHeight = (int)(w / imageAspect);
            }
            else
            {
                newHeight = h;
                newWidth = (int)(h * imageAspect);
            }

            if (_inputMat == null || _inputMat.width() != newWidth || _inputMat.height() != newHeight)
            {
                if (_inputMat == null) _inputMat = new Mat();
                _inputMat.create(newHeight, newWidth, image.type());
                _detectionModel.setInputSize((newWidth, newHeight));
            }

            Imgproc.resize(image, _inputMat, (newWidth, newHeight));

            return _inputMat; // [newWidth, newHeight, 3]
        }

        protected virtual Mat PostProcess(Mat faces, (double width, double height) inputSize, (double width, double height) originalSize)
        {
            if (faces.empty())
                return faces;

            Mat result = faces;

            float input_w = (float)inputSize.width;
            float input_h = (float)inputSize.height;
            float original_w = (float)originalSize.width;
            float original_h = (float)originalSize.height;

            // Scale boxes
            float xFactor = original_w / input_w;
            float yFactor = original_h / input_h;

            int num = result.rows();
            int requiredResultLen = num * 15;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allResult = result.AsSpan<float>();
#else
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            result.get(0, 0, _allResultBuffer);
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int resultOffset = i * 15;

                for (int j = 0; j < 4; ++j)
                {
                    if (j % 2 == 0)
                    {
                        float p = allResult[resultOffset + j] * xFactor;
                        p = Mathf.Clamp(p, 0, original_w);
                        allResult[resultOffset + j] = p;
                    }
                    else
                    {
                        float p = allResult[resultOffset + j] * yFactor;
                        p = Mathf.Clamp(p, 0, original_h);
                        allResult[resultOffset + j] = p;
                    }
                }

                for (int j = 4; j < 14; ++j)
                {
                    if (j % 2 == 0)
                    {
                        allResult[resultOffset + j] *= xFactor;
                    }
                    else
                    {
                        allResult[resultOffset + j] *= yFactor;
                    }
                }
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _detectionModel?.Dispose(); _detectionModel = null;
                _inputMat?.Dispose(); _inputMat = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                _allResultBuffer = null;
#endif
            }

            base.Dispose(disposing);
        }
    }
}
#endif
