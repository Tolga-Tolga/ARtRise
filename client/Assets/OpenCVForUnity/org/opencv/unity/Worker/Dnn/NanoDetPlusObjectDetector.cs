#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.DnnModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityIntegration.Worker.DataStruct;
using OpenCVForUnity.UnityIntegration.Worker.Utils;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// NanoDetPlus object detector implementation.
    /// This class provides functionality for object detection using the NanoDetPlus model implemented with OpenCV's DNN module.
    /// Referring to:
    /// https://github.com/RangiLyu/nanodet
    /// https://github.com/RangiLyu/nanodet/blob/main/nanodet/model/head/nanodet_plus_head.py
    /// https://github.com/hpc203/nanodet-plus-opencv
    ///
    /// [Tested Models]
    /// nanodet-plus-m_320.onnx https://github.com/RangiLyu/nanodet/releases/download/v1.0.0-alpha-1/nanodet-plus-m_320.onnx
    /// nanodet-plus-m_416.onnx https://github.com/RangiLyu/nanodet/releases/download/v1.0.0-alpha-1/nanodet-plus-m_416.onnx
    /// nanodet-plus-m-1.5x_320.onnx https://github.com/RangiLyu/nanodet/releases/download/v1.0.0-alpha-1/nanodet-plus-m-1.5x_320.onnx
    /// nanodet-plus-m-1.5x_416.onnx https://github.com/RangiLyu/nanodet/releases/download/v1.0.0-alpha-1/nanodet-plus-m-1.5x_416.onnx
    /// </summary>
    public class NanoDetPlusObjectDetector : ProcessingWorkerBase
    {
        public enum NMSStrategy
        {
            /// <summary>
            /// Performs NMS ignoring class information (class-agnostic).
            /// </summary>
            ClassAgnostic,

            /// <summary>
            /// Performs NMS with OpenCV's NMSBoxesBatched, where multi-class boxes may still suppress each other
            /// if their IoU is high, as suppression is not strictly class-wise.
            /// </summary>
            OpenCVNMSBoxesBatched,

            /// <summary>
            /// Performs NMS separately for each class (Ultralytics YOLO style).
            /// </summary>
            ClassWise
        }

        protected static readonly int[] STRIDES = new int[] { 8, 16, 32, 64 };
        protected const int REG_MAX = 7;
        protected static readonly Scalar MEAN = new Scalar(103.53, 116.28, 123.675); // BGR mean
        protected static readonly Scalar STD = new Scalar(57.375, 57.12, 58.395); // BGR standard deviation
        protected static readonly Scalar SIMPLIFIED_MEAN = new Scalar(103.33, 116.28, 121.96); // BGR mean
        protected static readonly double SIMPLIFIED_STD = 1.0 / 57.12; // BGR standard deviation
        protected static readonly Scalar SCALAR_WHITE = new Scalar(255, 255, 255, 255);
        protected static readonly Scalar SCALAR_114 = new Scalar(114, 114, 114, 114);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);
        protected static readonly Scalar[] SCALAR_PALETTE = new Scalar[]
        {
            new(255, 56, 56, 255),
            new(255, 157, 151, 255),
            new(255, 112, 31, 255),
            new(255, 178, 29, 255),
            new(207, 210, 49, 255),
            new(72, 249, 10, 255),
            new(146, 204, 23, 255),
            new(61, 219, 134, 255),
            new(26, 147, 52, 255),
            new(0, 212, 187, 255),
            new(44, 153, 168, 255),
            new(0, 194, 255, 255),
            new(52, 69, 147, 255),
            new(100, 115, 255, 255),
            new(0, 24, 236, 255),
            new(132, 56, 255, 255),
            new(82, 0, 133, 255),
            new(203, 56, 255, 255),
            new(255, 149, 200, 255),
            new(255, 55, 199, 255)
        };
        protected static readonly int BOUNDING_BOX_COLUMNS = 4;
        protected static readonly int DETECTION_RESULT_COLUMNS = 6;
        protected static readonly int DETECTION_BOX_START_INDEX = 0;
        protected static readonly int DETECTION_CONFIDENCE_INDEX = 4;
        protected static readonly int DETECTION_CLASS_ID_INDEX = 5;
        protected static readonly int BBOX_DIST_COLUMNS = 32;

        protected Size _inputSize;
        protected float _confThreshold;
        protected float _nmsThreshold;
        protected int _topK;
        protected int _backend;
        protected int _target;
        protected int _numClasses = 80;
        protected Net _objectDetectionNet;
        protected List<string> _cachedUnconnectedOutLayersNames;
        protected List<string> _classNames;
        protected Mat _project;
        protected float[] _project_arr;
        protected int[] _hsizes;
        protected int[] _wsizes;
        protected Mat _mlvlAnchors;
        protected Mat _paddedImg;
        protected Mat _preNMS_Nx6;
        protected Mat _preNMS_box_xywh;
        protected MatOfRect2d _NMS_boxes;
        protected MatOfFloat _NMS_confidences;
        protected MatOfInt _NMS_classIds;
        protected Mat _output0Buffer;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
        protected float[] _allClsconfsBBoxdistBuffer;
        protected float[] _allAnchorsBuffer;
        protected float[] _allPreNMSBuffer;
        protected int[] _allIndicesBuffer;
        protected float[] _allResultBuffer;
#endif

        /// <summary>
        /// Gets or sets the NMS strategy to use for object detection.
        /// </summary>
        public NMSStrategy SelectedNMSStrategy { get; set; } = NMSStrategy.ClassWise;

        /// <summary>
        /// Whether to use simplified normalization.
        /// </summary>
        /// <remarks>
        /// In exchange for a slight speedup in PreProcess, there will be a 2-3% error from the originally expected input tensor,
        /// but this is unlikely to make a significant difference in the final object detection result.
        /// (To the extent that there is a pixel difference in the rectangle)
        /// </remarks>
        public bool UseSimplifiedNormalization { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the NanoDetPlusObjectDetector class.
        /// </summary>
        /// <param name="modelFilepath">Path to the ONNX model file.</param>
        /// <param name="classesFilepath">Path to the text file containing class names.</param>
        /// <param name="inputSize">Input size for the network (default: 416x416).</param>
        /// <param name="confThreshold">Confidence threshold for filtering detections.</param>
        /// <param name="nmsThreshold">Non-maximum suppression threshold.</param>
        /// <param name="topK">Maximum number of output detections.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public NanoDetPlusObjectDetector(string modelFilepath, string classesFilepath, Size inputSize,
                                             float confThreshold = 0.35f, float nmsThreshold = 0.6f, int topK = 300,
                                             int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));
            if (inputSize == null)
                throw new ArgumentNullException(nameof(inputSize), "Input size cannot be null.");

            _inputSize = new Size(inputSize.width > 0 ? inputSize.width : 416, inputSize.height > 0 ? inputSize.height : 416);
            _confThreshold = Mathf.Clamp01(confThreshold);
            _nmsThreshold = Mathf.Clamp01(nmsThreshold);
            _topK = Math.Max(1, topK);
            _backend = backend;
            _target = target;

            try
            {
                _objectDetectionNet = Dnn.readNetFromONNX(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _objectDetectionNet.setPreferableBackend(_backend);
            _objectDetectionNet.setPreferableTarget(_target);
            _cachedUnconnectedOutLayersNames = _objectDetectionNet.getUnconnectedOutLayersNames();

            _output0Buffer = new Mat(_topK, DETECTION_RESULT_COLUMNS, CvType.CV_32FC1);

            if (!string.IsNullOrEmpty(classesFilepath))
            {
                _classNames = ReadClassNames(classesFilepath);
                _numClasses = _classNames.Count;
            }
            else
            {
                _classNames = new List<string>();
            }

            GenerateAnchors(out _mlvlAnchors);

            _project = Arange(0, REG_MAX + 1);
            _project_arr = new float[_project.cols()];
            _project.get(0, 0, _project_arr);

            _hsizes = new int[STRIDES.Length];
            _wsizes = new int[STRIDES.Length];
            for (int i = 0; i < STRIDES.Length; i++)
            {
                _hsizes[i] = (int)Mathf.Ceil((float)_inputSize.height / STRIDES[i]);
                _wsizes[i] = (int)Mathf.Ceil((float)_inputSize.width / STRIDES[i]);
            }
        }

        /// <summary>
        /// Visualizes detection result on the input image.
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
            if (result.cols() < DETECTION_RESULT_COLUMNS)
                throw new ArgumentException("Invalid result matrix. It must have at least 6 columns.");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<ObjectDetectionData> data = ToStructuredDataAsSpan(result);
#else
            ObjectDetectionData[] data = ToStructuredData(result);
#endif

            for (int i = 0; i < data.Length; i++)
            {
                ref readonly var d = ref data[i];
                float left = d.X1;
                float top = d.Y1;
                float right = d.X2;
                float bottom = d.Y2;
                float conf = d.Confidence;
                int classId = d.ClassId;

                var c = SCALAR_PALETTE[classId % SCALAR_PALETTE.Length].ToValueTuple();
                var color = isRGB ? c : (c.v2, c.v1, c.v0, c.v3);

                Imgproc.rectangle(image, (left, top), (right, bottom), color, 2);

                string label = $"{GetClassLabel(classId)}, {conf:F2}";

                int[] baseLine = new int[1];
                Size labelSize = Imgproc.getTextSize(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                top = Mathf.Max((float)top, (float)labelSize.height);
                Imgproc.rectangle(image, (left, top - labelSize.height),
                    (left + labelSize.width, top + baseLine[0]), color, Core.FILLED);
                Imgproc.putText(image, label, (left, top), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, SCALAR_WHITE.ToValueTuple(), 1, Imgproc.LINE_AA);
            }

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(512);

                for (int i = 0; i < data.Length; ++i)
                {
                    ref readonly var d = ref data[i];
                    sb.AppendFormat("-----------object {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.Append("Class: ").Append(GetClassLabel(d.ClassId));
                    sb.AppendLine();
                    sb.AppendFormat("Confidence: {0:F4}", d.Confidence);
                    sb.AppendLine();
                    sb.AppendFormat("Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", d.X1, d.Y1, d.X2, d.Y2);
                    sb.AppendLine();
                }

                Debug.Log(sb.ToString());
            }
        }

        /// <summary>
        /// Detects objects in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for object detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (6 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [x1, y1, x2, y2, confidence, classId]
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
        /// Detects objects in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for object detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (6 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [x1, y1, x2, y2, confidence, classId]
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
        /// Converts the detection result matrix to an array of ObjectDetectionData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Execute method.</param>
        /// <returns>Array of ObjectDetectionData structures containing object detection information.</returns>
        public virtual ObjectDetectionData[] ToStructuredData(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new ObjectDetectionData[0];
            if (result.cols() < DETECTION_RESULT_COLUMNS)
                throw new ArgumentException("Invalid result matrix. It must have at least 6 columns.");

            var dst = new ObjectDetectionData[result.rows()];
            OpenCVMatUtils.CopyFromMat(result, dst);

            return dst;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Converts the detection result matrix to a span of ObjectDetectionData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Execute method.</param>
        /// <returns>Span of ObjectDetectionData structures containing object detection information.</returns>
        public virtual Span<ObjectDetectionData> ToStructuredDataAsSpan(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return Span<ObjectDetectionData>.Empty;
            if (result.cols() < DETECTION_RESULT_COLUMNS)
                throw new ArgumentException("Invalid result matrix. It must have at least 6 columns.");
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            return result.AsSpan<ObjectDetectionData>();
        }
#endif

        /// <summary>
        /// Gets the class label for the given class ID.
        /// </summary>
        /// <param name="id">Class ID.</param>
        /// <returns>Class label string. Returns the ID as string if no label is found.</returns>
        public virtual string GetClassLabel(float id)
        {
            ThrowIfDisposed();

            return ClassLabelUtils.GetClassLabel(id, _classNames);
        }

        /// <summary>
        /// Gets all class labels.
        /// </summary>
        /// <returns>Array of class label strings.</returns>
        public virtual string[] GetClassLabels()
        {
            ThrowIfDisposed();

            return _classNames.ToArray();
        }

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
            Mat inputBlob = PreProcess(image);

            // Forward
            _objectDetectionNet.setInput(inputBlob);
            List<Mat> outputBlobs = new List<Mat>();
            try
            {
                _objectDetectionNet.forward(outputBlobs, _cachedUnconnectedOutLayersNames);
            }
            catch (Exception e)
            {
                inputBlob.Dispose();
                throw new ArgumentException(
                    "The input size specified in the constructor may not match the model's expected input size. " +
                    "Please verify the correct input size for your model and update the constructor parameters accordingly.", e);
            }

            // Postprocess
            Mat submat = PostProcess(outputBlobs[0], image.sizeAsValueTuple()); // submat of _output0Buffer is returned

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange

            inputBlob.Dispose();
            for (int i = 0; i < outputBlobs.Count; i++)
            {
                outputBlobs[i].Dispose();
            }

            return new Mat[] { submat }; // [n, 6] (xyxy, conf, cls)
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image)
        {
            // Create a 4D blob from a frame.
            // NanoDetPlus is swapRB = false
            Mat blob;

            if (UseSimplifiedNormalization)
            {
                blob = Dnn.blobFromImage(image, SIMPLIFIED_STD, _inputSize, SIMPLIFIED_MEAN, false, false, CvType.CV_32F); // HWC to NCHW, BGR
            }
            else
            {
                blob = Dnn.blobFromImage(image, 1.0, _inputSize, SCALAR_0, false, false, CvType.CV_32F); // HWC to NCHW, BGR

                int c = image.channels();
                int h = (int)_inputSize.height;
                int w = (int)_inputSize.width;

                using (Mat blob_CxHxW = blob.reshape(1, new int[] { c, h, w })) // [c, h, w]
                {
                    for (int i = 0; i < c; ++i)
                    {
                        using (Mat blob_CxHxW_row = blob_CxHxW.row(i))
                        using (Mat blob_1xHW = blob_CxHxW_row.reshape(1, 1)) // [1, h, w] => [1, h * w]
                        {
                            // Subtract blob by mean.
                            Core.subtract(blob_1xHW, (MEAN.val[i], 0, 0, 0), blob_1xHW);
                            // Divide blob by std.
                            Core.divide(blob_1xHW, (STD.val[i], 0, 0, 0), blob_1xHW);
                        }
                    }
                }
            }

            return blob; // [1, 3, h, w]
        }

        protected virtual Mat PostProcess(Mat outputBlob, (double width, double height) originalSize)
        {
            Mat outputBlob_0 = outputBlob; // 1 * N * (_numClasses + 32)

            if (outputBlob_0.size(2) != BBOX_DIST_COLUMNS + _numClasses)
            {
                Debug.LogWarning("The number of classes and output shapes are different. " +
                "( outputBlob_0.size(2):" + outputBlob_0.size(2) + " != 32 + numClasses:" + _numClasses + " )\n" +
                "When using a custom model, be sure to set the correct number of classes by loading the appropriate custom classesFile.");

                _numClasses = outputBlob_0.size(2) - BBOX_DIST_COLUMNS;
            }

            // pre-NMS
            int num = outputBlob_0.size(1);
            using (Mat outputBlob_0_NxClsconfsBBoxdist = outputBlob_0.reshape(1, num))
            {
                if (_preNMS_Nx6 == null || _preNMS_Nx6.rows() < _topK)
                {
                    if (_preNMS_Nx6 == null)
                        _preNMS_Nx6 = new Mat();
                    _preNMS_Nx6.create(_topK, DETECTION_RESULT_COLUMNS, CvType.CV_32FC1);
                    _preNMS_Nx6.setTo(SCALAR_0);
                }

                // Initialize output data (confidences only)
                using (Mat _preNMS_Nx6_col_4 = _preNMS_Nx6.col(DETECTION_CONFIDENCE_INDEX))
                {
                    _preNMS_Nx6_col_4.setTo(SCALAR_0);
                }

                int preNMS_index = 0;
                int index = 0;

                for (int i = 0; i < STRIDES.Length; i++)
                {
                    int feat_h = _hsizes[i];
                    int feat_w = _wsizes[i];
                    int stride = STRIDES[i];

                    int num_anchors = feat_h * feat_w;

                    int start_row = index;
                    int end_row = index + num_anchors;
                    using (Mat current_clsconfsBBoxdist = outputBlob_0_NxClsconfsBBoxdist.rowRange((start_row, end_row)))
                    using (Mat current_anchors = _mlvlAnchors.rowRange((start_row, end_row)))
                    {
                        PreNMS_NanoDetPlus(current_clsconfsBBoxdist, current_anchors,
                                            ref _preNMS_Nx6, ref preNMS_index, stride, _confThreshold);
                    }

                    index += num_anchors;
                }
            }

            Mat result = null;
            using (MatOfInt indices = new MatOfInt())
            {
                // NMS
                NMS(_preNMS_Nx6, _confThreshold, _nmsThreshold, indices, 1f, _topK, SelectedNMSStrategy);

                lock (_lockObject)
                {
                    // Create result
                    result = CreateResultFromBuffer(_preNMS_Nx6, indices, _output0Buffer);

                    // Scale boxes
                    ScaleBoxes_DIRECT_SCALIING(result, _inputSize.ToValueTuple(), originalSize);
                }
            }

            // [
            //   [xyxy, conf, cls]
            //   ...
            //   [xyxy, conf, cls]
            // ]
            return result;
        }

        protected virtual void PreNMS_NanoDetPlus(Mat clsconfsBBoxdist, Mat anchors, ref Mat preNMS_Nx6,
                                                     ref int preNMS_index, int box_stride, float score_threshold = 0)
        {
            if (!clsconfsBBoxdist.isContinuous())
                throw new ArgumentException("clsconfsBBoxdist is not continuous.");
            if (!anchors.isContinuous())
                throw new ArgumentException("anchors is not continuous.");

            int clsconfsBBoxdist_rows = clsconfsBBoxdist.rows();
            int clsconfsBBoxdist_cols = clsconfsBBoxdist.cols();
            int num_preNMS = preNMS_Nx6.rows();

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> allClsconfsBBoxdist = clsconfsBBoxdist.AsSpan<float>();
            ReadOnlySpan<float> allAnchors = anchors.AsSpan<float>();
            Span<float> allPreNMS = preNMS_Nx6.AsSpan<float>();

            Span<float> p_dot = stackalloc float[4];
            Span<float> bbox_pred_p_arr = stackalloc float[8];
#else
            int requiredClsconfsBBoxdistLen = clsconfsBBoxdist_rows * clsconfsBBoxdist_cols;
            int requiredAnchorsLen = clsconfsBBoxdist_rows * 2;
            int requiredPreNMSLen = num_preNMS * DETECTION_RESULT_COLUMNS;
            if (_allClsconfsBBoxdistBuffer == null || _allClsconfsBBoxdistBuffer.Length < requiredClsconfsBBoxdistLen)
                _allClsconfsBBoxdistBuffer = new float[requiredClsconfsBBoxdistLen];
            if (_allAnchorsBuffer == null || _allAnchorsBuffer.Length < requiredAnchorsLen)
                _allAnchorsBuffer = new float[requiredAnchorsLen];
            if (_allPreNMSBuffer == null || _allPreNMSBuffer.Length < requiredPreNMSLen)
                _allPreNMSBuffer = new float[requiredPreNMSLen];

            clsconfsBBoxdist.get(0, 0, _allClsconfsBBoxdistBuffer);
            anchors.get(0, 0, _allAnchorsBuffer);
            preNMS_Nx6.get(0, 0, _allPreNMSBuffer);
            float[] allClsconfsBBoxdist = _allClsconfsBBoxdistBuffer;
            float[] allAnchors = _allAnchorsBuffer;
            float[] allPreNMS = _allPreNMSBuffer;

            float[] p_dot = new float[4];
            float[] bbox_pred_p_arr = new float[8];
#endif

            for (int i = 0; i < clsconfsBBoxdist_rows; ++i)
            {
                float maxVal = float.MinValue;
                int maxIdx = -1;
                int baseIdx = i * clsconfsBBoxdist_cols;
                for (int j = 0; j < _numClasses; ++j)
                {
                    float conf = allClsconfsBBoxdist[baseIdx + j];
                    if (conf > maxVal)
                    {
                        maxVal = conf;
                        maxIdx = j;
                    }
                }

                if (maxVal > score_threshold)
                {
                    if (preNMS_index >= num_preNMS)
                    {
                        Mat new_preNMS_Nx6 = new Mat(num_preNMS * 2, DETECTION_RESULT_COLUMNS, CvType.CV_32FC1, SCALAR_0.ToValueTuple());
                        using (Mat new_preNMS_Nx6_roi = new_preNMS_Nx6.rowRange(0, num_preNMS))
                        {
                            preNMS_Nx6.copyTo(new_preNMS_Nx6_roi);
                        }
                        preNMS_Nx6.Dispose();
                        preNMS_Nx6 = new_preNMS_Nx6;
                        num_preNMS = preNMS_Nx6.rows();

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                        allPreNMS = preNMS_Nx6.AsSpan<float>();
#else
                        requiredPreNMSLen = num_preNMS * DETECTION_RESULT_COLUMNS;
                        float[] newBuffer = new float[requiredPreNMSLen];
                        Array.Copy(_allPreNMSBuffer, newBuffer, _allPreNMSBuffer.Length);
                        _allPreNMSBuffer = newBuffer;
                        allPreNMS = _allPreNMSBuffer;
#endif
                    }

                    int bboxDistBaseIdx = baseIdx + _numClasses;

                    for (int p = 0; p < 4; p++)
                    {
                        int bboxDistIdx = bboxDistBaseIdx + p * 8;
                        for (int k = 0; k < 8; k++)
                        {
                            bbox_pred_p_arr[k] = allClsconfsBBoxdist[bboxDistIdx + k];
                        }

                        // Softmax
                        float maxValue = bbox_pred_p_arr[0];
                        for (int k = 1; k < 8; k++)
                        {
                            if (bbox_pred_p_arr[k] > maxValue)
                                maxValue = bbox_pred_p_arr[k];
                        }

                        float sum = 0;
                        for (int k = 0; k < 8; k++)
                        {
                            bbox_pred_p_arr[k] = Mathf.Exp(bbox_pred_p_arr[k] - maxValue);
                            sum += bbox_pred_p_arr[k];
                        }

                        float invSum = 1.0f / sum;
                        for (int k = 0; k < 8; k++)
                        {
                            bbox_pred_p_arr[k] *= invSum;
                        }

                        // Inner product calculation with _project
                        float dotProduct = 0;
                        for (int k = 0; k < 8; k++)
                        {
                            dotProduct += bbox_pred_p_arr[k] * _project_arr[k];
                        }
                        p_dot[p] = dotProduct;
                    }

                    p_dot[0] *= box_stride;
                    p_dot[1] *= box_stride;
                    p_dot[2] *= box_stride;
                    p_dot[3] *= box_stride;

                    // distance2bbox
                    int anchorIdx = i * 2;
                    float x1 = allAnchors[anchorIdx] - p_dot[0];
                    float y1 = allAnchors[anchorIdx + 1] - p_dot[1];
                    float x2 = allAnchors[anchorIdx] + p_dot[2];
                    float y2 = allAnchors[anchorIdx + 1] + p_dot[3];

                    x1 = Mathf.Clamp(x1, 0, (float)_inputSize.width);
                    y1 = Mathf.Clamp(y1, 0, (float)_inputSize.height);
                    x2 = Mathf.Clamp(x2, 0, (float)_inputSize.width);
                    y2 = Mathf.Clamp(y2, 0, (float)_inputSize.height);

                    int preNMSIdx = preNMS_index * DETECTION_RESULT_COLUMNS;
                    allPreNMS[preNMSIdx] = x1;
                    allPreNMS[preNMSIdx + 1] = y1;
                    allPreNMS[preNMSIdx + 2] = x2;
                    allPreNMS[preNMSIdx + 3] = y2;
                    allPreNMS[preNMSIdx + DETECTION_CONFIDENCE_INDEX] = maxVal;
                    allPreNMS[preNMSIdx + DETECTION_CLASS_ID_INDEX] = maxIdx;

                    preNMS_index++;
                }
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            preNMS_Nx6.put(0, 0, _allPreNMSBuffer);
#endif
        }

        protected virtual void NMS(Mat preNMS_Nx6, float score_threshold, float nms_threshold, MatOfInt indices,
                                     float eta = 1f, int top_k = 300, NMSStrategy nmsStrategy = NMSStrategy.ClassWise)
        {
            if (indices == null)
                throw new ArgumentNullException("indices");

            int num_preNMS = preNMS_Nx6.rows();
            using (Mat preNMS_box = preNMS_Nx6.colRange((DETECTION_BOX_START_INDEX, BOUNDING_BOX_COLUMNS)))
            using (Mat preNMS_confidence = preNMS_Nx6.colRange((DETECTION_CONFIDENCE_INDEX, DETECTION_CONFIDENCE_INDEX + 1)))
            {
                // Convert boxes from [x1, y1, x2, y2] to [x, y, w, h] where Rect2d data style.
                if (_preNMS_box_xywh == null || _preNMS_box_xywh.rows() != num_preNMS)
                {
                    if (_preNMS_box_xywh == null)
                        _preNMS_box_xywh = new Mat();
                    _preNMS_box_xywh.create(num_preNMS, BOUNDING_BOX_COLUMNS, CvType.CV_32FC1);
                }
                using (Mat preNMS_xy1 = preNMS_box.colRange((0, 2)))
                using (Mat preNMS_xy2 = preNMS_box.colRange((2, 4)))
                using (Mat _preNMS_box_xywh_xy = _preNMS_box_xywh.colRange((0, 2)))
                using (Mat _preNMS_box_xywh_wh = _preNMS_box_xywh.colRange((2, 4)))
                {
                    preNMS_xy1.copyTo(_preNMS_box_xywh_xy);
                    Core.subtract(preNMS_xy2, preNMS_xy1, _preNMS_box_xywh_wh);
                }

                if (_NMS_boxes == null || _NMS_boxes.rows() != num_preNMS)
                {
                    if (_NMS_boxes == null)
                        _NMS_boxes = new MatOfRect2d();
                    _NMS_boxes.create(num_preNMS, 1, CvType.CV_64FC4);
                }
                if (_NMS_confidences == null || _NMS_confidences.rows() != num_preNMS)
                {
                    if (_NMS_confidences == null)
                        _NMS_confidences = new MatOfFloat();
                    _NMS_confidences.create(num_preNMS, 1, CvType.CV_32FC1);
                }

                using (Mat boxes_m_c1 = _NMS_boxes.reshape(1, num_preNMS))
                {
                    _preNMS_box_xywh.convertTo(boxes_m_c1, CvType.CV_64F);
                }
                preNMS_confidence.copyTo(_NMS_confidences);


                if (_NMS_classIds == null || _NMS_classIds.rows() != num_preNMS)
                {
                    if (_NMS_classIds == null)
                        _NMS_classIds = new MatOfInt();
                    _NMS_classIds.create(num_preNMS, 1, CvType.CV_32SC1);
                }

                using (Mat preNMS_classIds = preNMS_Nx6.colRange((DETECTION_CLASS_ID_INDEX, DETECTION_CLASS_ID_INDEX + 1)))
                {
                    preNMS_classIds.convertTo(_NMS_classIds, CvType.CV_32S);
                }

                if (nmsStrategy == NMSStrategy.OpenCVNMSBoxesBatched)
                {
                    Dnn.NMSBoxesBatched(_NMS_boxes, _NMS_confidences, _NMS_classIds, score_threshold, nms_threshold, indices, eta, top_k);
                }
                else if (nmsStrategy == NMSStrategy.ClassWise)
                {
                    DnnProcessingUtils.NMSBoxesClassWise(_NMS_boxes, _NMS_confidences, _NMS_classIds, score_threshold, nms_threshold, indices, eta, top_k);
                }
            }
        }

        protected virtual Mat CreateResultFromBuffer(Mat preNMS_Nx6, MatOfInt indices, Mat buffer)
        {
            if (!preNMS_Nx6.isContinuous())
                throw new ArgumentException("preNMS_Nx6 is not continuous.");
            if (!indices.isContinuous())
                throw new ArgumentException("indices is not continuous.");
            if (!buffer.isContinuous())
                throw new ArgumentException("buffer is not continuous.");
            if (indices.rows() > buffer.rows())
                throw new ArgumentException("indices.rows() > buffer.rows()");

            int num = indices.rows();
            Mat result = buffer.rowRange(0, num);

            if (num == 0)
                return result;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> allPreNMS = preNMS_Nx6.AsSpan<float>();
            ReadOnlySpan<int> allIndices = indices.AsSpan<int>();
            Span<float> allResult = result.AsSpan<float>();
#else
            int requiredPreNMSLen = preNMS_Nx6.rows() * DETECTION_RESULT_COLUMNS;
            int requiredIndicesLen = buffer.rows();
            int requiredResultLen = buffer.rows() * DETECTION_RESULT_COLUMNS;
            if (_allPreNMSBuffer == null || _allPreNMSBuffer.Length < requiredPreNMSLen)
                _allPreNMSBuffer = new float[requiredPreNMSLen];
            if (_allIndicesBuffer == null || _allIndicesBuffer.Length < requiredIndicesLen)
                _allIndicesBuffer = new int[requiredIndicesLen];
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            preNMS_Nx6.get(0, 0, _allPreNMSBuffer);
            indices.get(0, 0, _allIndicesBuffer);
            float[] allPreNMS = _allPreNMSBuffer;
            int[] allIndices = _allIndicesBuffer;
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int idx = allIndices[i];
                int resultOffset = i * DETECTION_RESULT_COLUMNS;
                int preNMSOffset = idx * DETECTION_RESULT_COLUMNS;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                allPreNMS.Slice(preNMSOffset, DETECTION_RESULT_COLUMNS).CopyTo(allResult.Slice(resultOffset, DETECTION_RESULT_COLUMNS));
#else
                Buffer.BlockCopy(allPreNMS, preNMSOffset * 4, allResult, resultOffset * 4, DETECTION_RESULT_COLUMNS * 4);
#endif
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif

            return result;
        }

        protected virtual void ScaleBoxes_DIRECT_SCALIING(Mat result, (double width, double height) inputSize,
                                                             (double width, double height) originalSize)
        {
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            int num = result.rows();
            if (num == 0)
                return;

            float input_w = (float)inputSize.width;
            float input_h = (float)inputSize.height;
            float original_w = (float)originalSize.width;
            float original_h = (float)originalSize.height;

            float gain_x = input_w / original_w;
            float gain_y = input_h / original_h;
            float pad_w = 0;
            float pad_h = 0;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allResult = result.AsSpan<float>();
#else
            int requiredResultLen = num * DETECTION_RESULT_COLUMNS;
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            result.get(0, 0, _allResultBuffer);
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int resultOffset = i * DETECTION_RESULT_COLUMNS;
                float x1 = (allResult[resultOffset] - pad_w) / gain_x;
                float y1 = (allResult[resultOffset + 1] - pad_h) / gain_y;
                float x2 = (allResult[resultOffset + 2] - pad_w) / gain_x;
                float y2 = (allResult[resultOffset + 3] - pad_h) / gain_y;

                x1 = Mathf.Clamp(x1, 0, original_w);
                y1 = Mathf.Clamp(y1, 0, original_h);
                x2 = Mathf.Clamp(x2, 0, original_w);
                y2 = Mathf.Clamp(y2, 0, original_h);

                allResult[resultOffset] = x1;
                allResult[resultOffset + 1] = y1;
                allResult[resultOffset + 2] = x2;
                allResult[resultOffset + 3] = y2;
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif
        }

        protected virtual List<string> ReadClassNames(string filename)
        {
            return ClassLabelUtils.ReadClassNames(filename);
        }

        protected virtual void GenerateAnchors(out Mat mlvlAnchors)
        {
            int num = 0;

            int[] hsizes = new int[STRIDES.Length]; // stride for stride in self.strides
            int[] wsizes = new int[STRIDES.Length]; // stride for stride in self.strides
            for (int i = 0; i < STRIDES.Length; i++)
            {
                hsizes[i] = (int)Mathf.Ceil((float)_inputSize.height / STRIDES[i]);
                wsizes[i] = (int)Mathf.Ceil((float)_inputSize.width / STRIDES[i]);

                num += hsizes[i] * wsizes[i];
            }

            mlvlAnchors = new Mat(num, 2, CvType.CV_32FC1); // num*2*CV_32FC1
            int index = 0;

            for (int i = 0; i < STRIDES.Length; i++)
            {
                int feat_h = hsizes[i];
                int feat_w = wsizes[i];
                int stride = STRIDES[i];

                // #shift_x = np.arange(0, feat_w) * stride
                // #shift_y = np.arange(0, feat_h) * stride
                using (Mat shift_x = Arange(0, feat_w))
                using (Mat shift_y = Arange(0, feat_h).t())
                {
                    Core.multiply(shift_x, (stride, stride, stride, stride), shift_x);
                    Core.multiply(shift_y, (stride, stride, stride, stride), shift_y);

                    // #xv, yv = np.meshgrid(shift_x, shift_y)
                    using (Mat xv = new Mat(feat_h, feat_w, CvType.CV_32FC1))
                    using (Mat yv = new Mat(feat_h, feat_w, CvType.CV_32FC1))
                    {
                        Tile(shift_x, feat_h, 1, xv);
                        Tile(shift_y, 1, feat_w, yv);

                        // #np.stack((xv, yv), axis=-1)
                        using (Mat xv_totalx1 = xv.reshape(1, (int)xv.total())) // total*1*CV_32FC1
                        using (Mat grid_roi = new Mat(mlvlAnchors, (0, index, 1, (int)xv.total()))) // total*1*CV_32FC1
                        {
                            xv_totalx1.copyTo(grid_roi);
                        }
                        using (Mat yv_totalx1 = yv.reshape(1, (int)yv.total())) // total*1*CV_32FC1
                        using (Mat grid_roi = new Mat(mlvlAnchors, (1, index, 1, (int)yv.total()))) // total*1*CV_32FC1
                        {
                            yv_totalx1.copyTo(grid_roi);
                        }
                    }
                }

                index += feat_h * feat_w;
            }
        }

        protected virtual void Distance2bbox(Mat points, Mat distance, Size max_shape = null)
        {
            // #x1 = points[:, 0] - distance[:, 0]
            // #y1 = points[:, 1] - distance[:, 1]
            // #x2 = points[:, 0] + distance[:, 2]
            // #y2 = points[:, 1] + distance[:, 3]
            // #if max_shape is not None:
            // #   x1 = np.clip(x1, 0, max_shape[1])
            // #   y1 = np.clip(y1, 0, max_shape[0])
            // #   x2 = np.clip(x2, 0, max_shape[1])
            // #   y2 = np.clip(y2, 0, max_shape[0])
            // #return np.stack([x1, y1, x2, y2], axis = -1)
            using (Mat xy1 = distance.colRange((0, 2)))
            using (Mat xy2 = distance.colRange((2, 4)))
            {
                Core.subtract(points, xy1, xy1);
                Core.add(points, xy2, xy2);

                if (max_shape != null)
                {
                    using (Mat x1 = distance.colRange((0, 1)))
                    using (Mat y1 = distance.colRange((1, 2)))
                    using (Mat x2 = distance.colRange((2, 3)))
                    using (Mat y2 = distance.colRange((3, 4)))
                    {
                        Imgproc.threshold(distance, distance, 0, -1, Imgproc.THRESH_TOZERO);
                        Imgproc.threshold(x1, x1, max_shape.width, -1, Imgproc.THRESH_TRUNC);
                        Imgproc.threshold(y1, y1, max_shape.height, -1, Imgproc.THRESH_TRUNC);
                        Imgproc.threshold(x2, x2, max_shape.width, -1, Imgproc.THRESH_TRUNC);
                        Imgproc.threshold(y2, y2, max_shape.height, -1, Imgproc.THRESH_TRUNC);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _objectDetectionNet?.Dispose(); _objectDetectionNet = null;
                _project?.Dispose(); _project = null;
                _mlvlAnchors?.Dispose(); _mlvlAnchors = null;
                _paddedImg?.Dispose(); _paddedImg = null;
                _preNMS_Nx6?.Dispose(); _preNMS_Nx6 = null;
                _preNMS_box_xywh?.Dispose(); _preNMS_box_xywh = null;
                _NMS_boxes?.Dispose(); _NMS_boxes = null;
                _NMS_confidences?.Dispose(); _NMS_confidences = null;
                _NMS_classIds?.Dispose(); _NMS_classIds = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                _allClsconfsBBoxdistBuffer = null;
                _allAnchorsBuffer = null;
                _allPreNMSBuffer = null;
                _allIndicesBuffer = null;
                _allResultBuffer = null;
#endif
            }

            base.Dispose(disposing);
        }

        protected virtual Mat Arange(int start, int stop)
        {
            if (start < 0 || stop < 0 || stop < start || stop == start)
                throw new ArgumentException("start < 0 || stop < 0 || stop < start || stop == start");

            float[] data = Enumerable.Range(start, stop).Select(i => (float)i).ToArray();
            Mat dst = new Mat(1, stop - start, CvType.CV_32FC1);
            dst.put(0, 0, data);

            return dst;
        }

        protected virtual void Tile(Mat a, int ny, int nx, Mat dst)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (a != null)
                a.ThrowIfDisposed();

            if (dst == null)
                throw new ArgumentNullException("dst");
            if (dst != null)
                dst.ThrowIfDisposed();
            if (dst.rows() != a.rows() * ny || dst.cols() != a.cols() * nx || dst.type() != a.type())
                throw new ArgumentException("dst.rows() != a.rows() * ny || dst.cols() != a.cols() * nx || dst.type() != a.type()");

            Core.repeat(a, ny, nx, dst);
        }
    }
}
#endif
