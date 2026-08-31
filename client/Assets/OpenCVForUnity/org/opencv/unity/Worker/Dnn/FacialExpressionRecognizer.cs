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
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityIntegration.Worker.DataStruct;
using OpenCVForUnity.UnityIntegration.Worker.Utils;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Facial expression recognizer implementation.
    /// This class provides functionality for facial expression recognition using the Facial Expression Recognition model implemented with OpenCV's DNN module.
    /// Referring to https://github.com/opencv/opencv_zoo/tree/main/models/facial_expression_recognition
    ///
    /// [Tested Models]
    /// facial_expression_recognition_mobilefacenet_2022july.onnx https://github.com/opencv/opencv_zoo/blob/main/models/facial_expression_recognition/facial_expression_recognition_mobilefacenet_2022july.onnx
    /// face_recognition_sface_2021dec.onnx https://github.com/opencv/opencv_zoo/blob/main/models/face_recognition/face_recognition_sface_2021dec.onnx
    /// </summary>
    public class FacialExpressionRecognizer : ProcessingWorkerBase
    {
        protected const string INPUT_NAME = "data";
        protected const string OUTPUT_NAME = "label";
        protected static readonly Size INPUT_SIZE = new Size(112, 112);
        protected static readonly Scalar MEAN = new Scalar(0.5, 0.5, 0.5);
        protected static readonly Scalar STD = new Scalar(0.5, 0.5, 0.5);
        protected static readonly Scalar SIMPLIFIED_MEAN = new Scalar(127.5, 127.5, 127.5);
        protected static readonly double SIMPLIFIED_STD = 1.0 / 127.5;
        protected static readonly Scalar SCALAR_WHITE = new Scalar(255, 255, 255, 255);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);
        protected static readonly Scalar[] SCALAR_PALETTE = new Scalar[]
        {
            new(255, 56, 56, 255),
            new(82, 0, 133, 255),
            new(52, 69, 147, 255),
            new(255, 178, 29, 255),
            new(55, 55, 55, 255),
            new(100, 115, 255, 255),
            new(255, 112, 31, 255)
        };

        protected int _backend;
        protected int _target;
        protected int _numClasses = 7;
        protected Net _facialExpressionRecognitionNet;
        protected List<string> _classNames;
        protected MatPool _inputSizeMatPool;
        protected Mat _classificationResultBuffer;
        protected Mat _output0Buffer;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
        protected float[] _allResultRowBuffer;
#endif

        /// <summary>
        /// Whether to use simplified normalization.
        /// </summary>
        /// <remarks>
        /// In exchange for a slight speedup in PreProcess, there will be a 2-3% error from the originally expected input tensor,
        /// but this is unlikely to make a significant difference in the final classification result.
        /// </remarks>
        public bool UseSimplifiedNormalization { get; set; } = true;

        /// <summary>
        /// Creates a new instance of FacialExpressionRecognizer.
        /// </summary>
        /// <param name="modelFilepath">Path to the facial expression recognition model file.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public FacialExpressionRecognizer(string modelFilepath,
                                             int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));

            _backend = backend;
            _target = target;

            try
            {
                _facialExpressionRecognitionNet = Dnn.readNet(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _facialExpressionRecognitionNet.setPreferableBackend(_backend);
            _facialExpressionRecognitionNet.setPreferableTarget(_target);

            _output0Buffer = new Mat();

            _classNames = new List<string>
            {
                "angry",
                "disgust",
                "fearful",
                "happy",
                "neutral",
                "sad",
                "surprised"
            };
            _numClasses = _classNames.Count;

            _inputSizeMatPool = new MatPool(INPUT_SIZE, CvType.CV_8UC3);
        }

        /// <summary>
        /// Visualizes the best matching facial expression recognition result on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">The result Mat returned by Recognize method.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            ClassificationData bmData = GetBestMatchData(result, 0);
            int classId = bmData.ClassId;
            string label = GetClassLabel(bmData.ClassId) + ", " + bmData.Confidence.ToString("F2");

            var c = SCALAR_PALETTE[classId % SCALAR_PALETTE.Length].ToValueTuple();
            var color = isRGB ? c : (c.v2, c.v1, c.v0, c.v3);

            int[] baseLine = new int[1];
            var labelSize = Imgproc.getTextSizeAsValueTuple(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

            float top = 20f + (float)labelSize.height;
            float left = (float)(image.width() / 2 - labelSize.width / 2f);

            top = Mathf.Max((float)top, (float)labelSize.height);
            Imgproc.rectangle(image, (left, top - labelSize.height),
                (left + labelSize.width, top + baseLine[0]), color, Core.FILLED);
            Imgproc.putText(image, label, (left, top), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, SCALAR_WHITE.ToValueTuple(), 1, Imgproc.LINE_AA);

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(64);
                sb.AppendLine("Best match: " + GetClassLabel(bmData.ClassId) + ", " + bmData.ToString());

                Debug.Log(sb.ToString());
            }
        }

        /// <summary>
        /// Visualizes the best matching facial expression recognition result on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">Recognition result matrix from Recognize method.</param>
        /// <param name="faceBox">Matrix containing face detection result from YuNetV2FaceDetector.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public virtual void Visualize(Mat image, Mat result, Mat faceBox, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");
            if (faceBox != null) faceBox.ThrowIfDisposed();
            if (faceBox.empty())
                return;
            if (faceBox.cols() < 15)
                throw new ArgumentException("Invalid faceBox matrix. It must have at least 15 columns.");
            if (result.rows() != faceBox.rows())
                throw new ArgumentException("The number of result must match the number of faceBox.");

            StringBuilder sb = null;

            if (printResult)
                sb = new StringBuilder(256);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> allFaceBox = faceBox.AsSpan<float>();
#else
            float[] allFaceBoxBuffer = new float[faceBox.total() * faceBox.channels()];
            faceBox.get(0, 0, allFaceBoxBuffer);
            float[] allFaceBox = allFaceBoxBuffer;
#endif

            for (int i = 0; i < result.rows(); ++i)
            {
                int faceBoxIndex = i * 15;

                float left = allFaceBox[faceBoxIndex] + 2;
                float top = allFaceBox[faceBoxIndex + 1] + 2;
                float right = allFaceBox[faceBoxIndex] + allFaceBox[faceBoxIndex + 2] - 2;
                float bottom = allFaceBox[faceBoxIndex + 1] + allFaceBox[faceBoxIndex + 3] - 2;

                ClassificationData bmData = GetBestMatchData(result, i);
                int classId = bmData.ClassId;
                string label = $"{GetClassLabel(bmData.ClassId)}, {bmData.Confidence:F4}";

                var c = SCALAR_PALETTE[classId % SCALAR_PALETTE.Length].ToValueTuple();
                var color = isRGB ? c : (c.v2, c.v1, c.v0, c.v3);

                Imgproc.rectangle(image, (left, top), (right, bottom), color, 2);

                int[] baseLine = new int[1];
                var labelSize = Imgproc.getTextSizeAsValueTuple(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                top = Mathf.Max((float)top, (float)labelSize.height);
                Imgproc.rectangle(image, (left, top + 2),
                    (left + labelSize.width, top + labelSize.height + baseLine[0] + 2), color, Core.FILLED);
                Imgproc.putText(image, label, (left, top + labelSize.height + 2), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, SCALAR_WHITE.ToValueTuple(), 1, Imgproc.LINE_AA);

                if (printResult)
                {
                    sb.AppendFormat("---FaceBox {0}--- ", i + 1);
                    sb.Append("Best match: " + GetClassLabel(bmData.ClassId) + ", " + bmData.ToString());
                    sb.AppendLine();
                }
            }

            if (printResult)
                Debug.Log(sb.ToString());
        }

        /// <summary>
        /// Recognize facial expressions in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for facial expression recognition that:
        /// - Takes a single BGR image as input
        /// - Takes a face detection result from YuNetV2FaceDetector as input, which is used to perform face alignment and cropping using face rectangles and keypoints
        /// - Returns a Mat containing recognition result for all faces (7 columns per recognition)
        ///
        /// The returned Mat format:
        /// - Each row contains confidence scores for each expression class of one face
        /// - Columns: [angry, disgust, fearful, happy, neutral, sad, surprised]
        /// - Use ToStructuredData() or GetBestMatchData() to convert to a more convenient format
        ///
        /// Output options:
        /// - useCopyOutput = false (default): Returns a reference to internal buffer (faster but unsafe across executions)
        /// - useCopyOutput = true: Returns a new copy of the result (thread-safe but slightly slower)
        ///
        /// For better performance in async scenarios, use RecognizeAsync instead.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="faceBox">Optional face detection result from YuNetV2FaceDetector. If null, the entire image area will be used for inference.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing recognition result for all faces. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Recognize(Mat image, Mat faceBox = null, bool useCopyOutput = false)
        {
            Execute(image, faceBox);
            return useCopyOutput ? CopyOutput() : PeekOutput();
        }

        /// <summary>
        /// Recognize facial expressions in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for facial expression recognition that:
        /// - Takes a single BGR image as input
        /// - Takes a face detection result from YuNetV2FaceDetector as input, which is used to perform face alignment and cropping using face rectangles and keypoints
        /// - Returns a Mat containing recognition result for all faces (7 columns per recognition)
        ///
        /// The returned Mat format:
        /// -  Each row contains confidence scores for each expression class of one face
        /// - Columns: [angry, disgust, fearful, happy, neutral, sad, surprised]
        /// - Use ToStructuredData() or GetBestMatchData() to convert to a more convenient format
        ///
        /// Only one recognition operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="faceBox">Optional face detection result from YuNetV2FaceDetector. If null, the entire image area will be used for inference.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with recognition result for all faces. The caller is responsible for disposing this Mat.</returns>
        public virtual async Task<Mat> RecognizeAsync(Mat image, Mat faceBox = null, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(image, faceBox, cancellationToken);
            return CopyOutput();
        }

        /// <summary>
        /// Converts the recognition result matrix to an array of ClassificationData structures.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <param name="index">Index of the result row to convert.</param>
        /// <returns>Array of ClassificationData structures containing class IDs and confidence scores.</returns>
        public virtual ClassificationData[] ToStructuredData(Mat result, int index = 0)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new ClassificationData[0];
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            int num = result.cols();

            if (_classificationResultBuffer == null)
            {
                _classificationResultBuffer = new Mat(num, 2, CvType.CV_32FC1);
                float[] arange = Enumerable.Range(0, num).Select(i => (float)i).ToArray();
                using (Mat result_Nx1_col_1 = _classificationResultBuffer.col(1))
                {
                    result_Nx1_col_1.put(0, 0, arange);
                }
            }

            using (Mat result_row = result.row(index))
            using (Mat result_Nx1 = result_row.reshape(1, num))
            using (Mat result_Nx1_col_0 = _classificationResultBuffer.col(0))
            {
                result_Nx1.copyTo(result_Nx1_col_0);
            }

            var dst = new ClassificationData[num];
            OpenCVMatUtils.CopyFromMat(_classificationResultBuffer, dst);

            return dst;
        }

        /// <summary>
        /// Gets the top K sorted recognition result.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <param name="topK">Number of top result to return.</param>
        /// <param name="index">Index of the result row to get sorted data from.</param>
        /// <returns>Array of sorted ClassificationData structures containing top K result.</returns>
        public virtual ClassificationData[] GetSortedData(Mat result, int topK = 5, int index = 0)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new ClassificationData[0];
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            if (index < 0 || index >= result.rows())
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            int num = result.cols();

            if (topK < 1 || topK > num) topK = num;

            // Get raw data
            var data = ToStructuredData(result, index);
            if (data.Length == 0)
                return data;

            // If we need all elements, just sort the entire array
            if (topK == num)
            {
                Array.Sort(data, (a, b) => b.Confidence.CompareTo(a.Confidence));
                return data;
            }

            // Otherwise, use partial sort to get top K elements
            var sortedData = new ClassificationData[topK];
            var indices = new int[data.Length];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            // Partial sort indices based on confidence values
            for (int i = 0; i < topK; i++)
            {
                int maxIndex = i;
                float maxConfidence = data[indices[i]].Confidence;

                for (int j = i + 1; j < indices.Length; j++)
                {
                    float compareConfidence = data[indices[j]].Confidence;
                    if (compareConfidence > maxConfidence)
                    {
                        maxIndex = j;
                        maxConfidence = compareConfidence;
                    }
                }

                if (maxIndex != i)
                {
                    int temp = indices[i];
                    indices[i] = indices[maxIndex];
                    indices[maxIndex] = temp;
                }

                sortedData[i] = data[indices[i]];
            }

            return sortedData;
        }

        /// <summary>
        /// Gets the best matching recognition result.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <returns>ClassificationData structure containing the best match.</returns>
        public virtual ClassificationData GetBestMatchData(Mat result, int index = 0)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new ClassificationData();
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            if (index < 0 || index >= result.rows())
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            float maxVal = float.MinValue;
            int maxLoc = 0;


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> data = result.AsSpan<float>(index);

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > maxVal)
                {
                    maxVal = data[i];
                    maxLoc = i;
                }
            }
#else
            using (Mat result_row = result.row(index))
            {
                int requiredResultRowLen = (int)result_row.total() * result_row.channels();
                if (_allResultRowBuffer == null || _allResultRowBuffer.Length < requiredResultRowLen)
                    _allResultRowBuffer = new float[requiredResultRowLen];
                result_row.get(0, 0, _allResultRowBuffer);
                float[] data = _allResultRowBuffer;

                for (int i = 0; i < requiredResultRowLen; i++)
                {
                    if (data[i] > maxVal)
                    {
                        maxVal = data[i];
                        maxLoc = i;
                    }
                }
            }
#endif

            return new ClassificationData(maxVal, maxLoc);
        }

        /// <summary>
        /// Converts the recognition result matrix to an array of ClassificationData structures for all rows.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <returns>List of arrays of ClassificationData structures containing class IDs and confidence scores.</returns>
        public virtual List<ClassificationData[]> ToStructuredDatas(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new List<ClassificationData[]>();
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            var datas = new List<ClassificationData[]>();
            for (int i = 0; i < result.rows(); i++)
            {
                datas.Add(ToStructuredData(result, i));
            }
            return datas;
        }

        /// <summary>
        /// Gets the top K sorted recognition results for all rows.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <param name="topK">Number of top results to return.</param>
        /// <returns>List of arrays of sorted ClassificationData structures containing top K results.</returns>
        public virtual List<ClassificationData[]> GetSortedDatas(Mat result, int topK = 5)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new List<ClassificationData[]>();
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            var datas = new List<ClassificationData[]>();
            for (int i = 0; i < result.rows(); i++)
            {
                datas.Add(GetSortedData(result, topK, i));
            }
            return datas;
        }

        /// <summary>
        /// Gets the best matching recognition results for all rows.
        /// </summary>
        /// <param name="result">Recognition result matrix from Execute method.</param>
        /// <returns>Array of ClassificationData structures containing the best matches.</returns>
        public virtual ClassificationData[] GetBestMatchDatas(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new ClassificationData[0];
            if (result.cols() < _numClasses)
                throw new ArgumentException("Invalid result matrix. It must have at least " + _numClasses + " columns.");

            var datas = new ClassificationData[result.rows()];
            for (int i = 0; i < result.rows(); i++)
            {
                datas[i] = GetBestMatchData(result, i);
            }
            return datas;
        }

        /// <summary>
        /// Gets the class label for the given class ID.
        /// </summary>
        /// <param name="id">Class ID.</param>
        /// <returns>String representation of the class label.</returns>
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
            Mat faceBox = inputs.Length >= 2 ? inputs[1] : null;

            if (image != null) image.ThrowIfDisposed();
            if (image.channels() != 3)
                throw new ArgumentException("The input image must be in BGR format.");

            if (faceBox != null) faceBox.ThrowIfDisposed();
            if (faceBox != null && faceBox.empty())
                faceBox = null;
            if (faceBox != null && faceBox.rows() < 1)
                throw new ArgumentException("Invalid faceBox matrix. It must have at least 1 row.");
            if (faceBox != null && faceBox.cols() < 15)
                throw new ArgumentException("Invalid faceBox matrix. It must have at least 15 columns.");

            // Preprocess
            Mat inputBlob = PreProcess(image, faceBox);

            // Forward
            _facialExpressionRecognitionNet.setInput(inputBlob, INPUT_NAME);
            Mat outputBlob = null;
            try
            {
                outputBlob = _facialExpressionRecognitionNet.forward(OUTPUT_NAME);
            }
            catch (Exception e)
            {
                inputBlob.Dispose();
                throw new ArgumentException(
                    "The input size specified in the constructor may not match the model's expected input size. " +
                    "Please verify the correct input size for your model and update the constructor parameters accordingly.", e);
            }

            // Postprocess
            outputBlob = PostProcess(outputBlob);

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange
            Mat submat = null;
            lock (_lockObject)
            {
                // Check if _output0Buffer needs to be resized
                if (_output0Buffer == null || _output0Buffer.rows() < outputBlob.rows() || _output0Buffer.cols() < outputBlob.cols())
                {
                    _output0Buffer.create(outputBlob.rows(), outputBlob.cols(), outputBlob.type());
                }

                // If buffer is larger, use rowRange to copy only the needed portion
                submat = _output0Buffer.rowRange(0, outputBlob.rows());
                outputBlob.copyTo(submat);
            }

            inputBlob.Dispose();
            outputBlob.Dispose();

            return new Mat[] { submat }; // [n, num_classes]
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image, Mat bbox = null)
        {
            List<Mat> inputSizeMats = new List<Mat>();

            if (bbox != null)
            {
                for (int i = 0; i < bbox.rows(); i++)
                {
                    Mat inputSizeMat = _inputSizeMatPool.Get();
                    using (Mat bbox_row = bbox.row(i))
                    {
                        AlignCrop(image, bbox_row, inputSizeMat);
                    }
                    inputSizeMats.Add(inputSizeMat);
                }
            }
            else
            {
                Mat inputSizeMat = _inputSizeMatPool.Get();
                Imgproc.resize(image, inputSizeMat, INPUT_SIZE);
                inputSizeMats.Add(inputSizeMat);
            }

            // Create a 4D blob from a frame.
            // Facial Expression Recognition is swapRB = true
            Mat blob;

            if (UseSimplifiedNormalization)
            {
                blob = Dnn.blobFromImages(inputSizeMats, SIMPLIFIED_STD, INPUT_SIZE, SIMPLIFIED_MEAN, true, false, CvType.CV_32F); // HWC to NCHW, BGR to RGB
            }
            else
            {
                blob = Dnn.blobFromImages(inputSizeMats, 1.0 / 255.0, INPUT_SIZE, SCALAR_0, true, false, CvType.CV_32F); // HWC to NCHW, BGR to RGB

                int n = blob.size(0);
                int c = blob.size(1);
                int h = blob.size(2);
                int w = blob.size(3);

                for (int i = 0; i < n; ++i)
                {
                    using (Mat blob_NxCxHxW_row = blob.row(i)) // [1, c, h, w]
                    {
                        using (Mat blob_CxHxW = blob_NxCxHxW_row.reshape(1, new int[] { c, h, w })) // [c, h, w]
                        {
                            for (int j = 0; j < c; ++j)
                            {
                                using (Mat blob_CxHxW_row = blob_CxHxW.row(j))
                                using (Mat blob_1xHW = blob_CxHxW_row.reshape(1, 1)) // [1, h, w] => [1, h * w]
                                {
                                    // Subtract blob by mean.
                                    Core.subtract(blob_1xHW, (MEAN.val[j], 0, 0, 0), blob_1xHW);
                                    // Divide blob by std.
                                    Core.divide(blob_1xHW, (STD.val[j], 0, 0, 0), blob_1xHW);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var inputSizeMat in inputSizeMats)
            {
                _inputSizeMatPool.Return(inputSizeMat);
            }

            return blob; // [n, 3, 112, 112]
        }

        protected virtual Mat PostProcess(Mat outputBlob)
        {
            Mat outputBlob_0 = outputBlob;

            if (outputBlob_0.cols() != _numClasses)
            {
                Debug.LogWarning("The number of classes and output shapes are different. " +
                                "( outputBlob_0.cols():" + outputBlob_0.cols() + " != numClasses:" + _numClasses + " )\n" +
                                "When using a custom model, be sure to set the correct number of classes by loading the appropriate custom classesFile.");

                _numClasses = outputBlob_0.cols();
            }

            Softmax(outputBlob_0);

            return outputBlob_0; // [n, num_classes]
        }

        protected virtual void Softmax(Mat result)
        {
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            for (int i = 0; i < result.rows(); i++)
            {
                using (Mat row = result.row(i))
                {
                    float maxVal = float.MinValue;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                    Span<float> data = row.AsSpan<float>();

                    for (int j = 0; j < data.Length; j++)
                    {
                        if (data[j] > maxVal)
                        {
                            maxVal = data[j];
                        }
                    }
#else
                    int requiredResultRowLen = (int)row.total() * row.channels();
                    if (_allResultRowBuffer == null || _allResultRowBuffer.Length < requiredResultRowLen)
                        _allResultRowBuffer = new float[requiredResultRowLen];
                    row.get(0, 0, _allResultRowBuffer);
                    float[] data = _allResultRowBuffer;

                    for (int j = 0; j < requiredResultRowLen; j++)
                    {
                        if (data[j] > maxVal)
                        {
                            maxVal = data[j];
                        }
                    }
#endif

                    Core.subtract(row, (maxVal, 0, 0, 0), row);
                    Core.exp(row, row);
                    var sum = Core.sumElemsAsValueTuple(row);
                    Core.divide(row, (sum.v0, 0, 0, 0), row);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _facialExpressionRecognitionNet?.Dispose(); _facialExpressionRecognitionNet = null;
                _inputSizeMatPool?.Dispose(); _inputSizeMatPool = null;
                _classificationResultBuffer?.Dispose(); _classificationResultBuffer = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                _allResultRowBuffer = null;
#endif
            }

            base.Dispose(disposing);
        }

        // This method returns an image of 112x112 pixels, the same as the Facial Expression Recognition model input.
        // https://github.com/opencv/opencv/blob/1950c4dbb993c60f11ddc8adf3c4eeab998fc175/modules/objdetect/src/face_recognize.cpp#L42
        protected virtual void AlignCrop(Mat src_img, Mat face_mat, Mat aligned_img)
        {
            float[,] src_point = new float[5, 2];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> allFaceMat = face_mat.AsSpan<float>();
#else
            float[] allFaceMatBuffar = new float[14];
            face_mat.get(0, 0, allFaceMatBuffar);
            float[] allFaceMat = allFaceMatBuffar;
#endif
            for (int row = 0; row < 5; ++row)
            {
                for (int col = 0; col < 2; ++col)
                {
                    src_point[row, col] = allFaceMat[row * 2 + col + 4];
                }
            }

            using (Mat warp_mat = GetSimilarityTransformMatrix(src_point))
            {
                Imgproc.warpAffine(src_img, aligned_img, warp_mat, (112, 112), Imgproc.INTER_LINEAR);
            }
        }

        // https://github.com/opencv/opencv/blob/1950c4dbb993c60f11ddc8adf3c4eeab998fc175/modules/objdetect/src/face_recognize.cpp#L79
        protected virtual Mat GetSimilarityTransformMatrix(float[,] src)
        {
            // dst[5,2] -> dst[10]
#if NET_STANDARD_2_1
            Span<float> dst = stackalloc float[10]
#else
            float[] dst = new float[10]
#endif
            {
                38.2946f, 51.6963f,  // dst[0,0], dst[0,1]
                73.5318f, 51.5014f,  // dst[1,0], dst[1,1]
                56.0252f, 71.7366f,  // dst[2,0], dst[2,1]
                41.5493f, 92.3655f,  // dst[3,0], dst[3,1]
                70.7299f, 92.2041f   // dst[4,0], dst[4,1]
            };

            // Compute mean of src and dst.
#if NET_STANDARD_2_1
            Span<float> src_mean = stackalloc float[2] { 0f, 0f };
#else
            float[] src_mean = new float[2] { 0f, 0f };
#endif
            for (int i = 0; i < 5; i++)
            {
                src_mean[0] += src[i, 0];
                src_mean[1] += src[i, 1];
            }
            src_mean[0] /= 5f;
            src_mean[1] /= 5f;
#if NET_STANDARD_2_1
            Span<float> dst_mean = stackalloc float[2] { 56.0262f, 71.9008f };
#else
            float[] dst_mean = new float[2] { 56.0262f, 71.9008f };
#endif

            //Subtract mean from src and dst.
            // src_demean[5,2] -> src_demean[10], dst_demean[5,2] -> dst_demean[10]
#if NET_STANDARD_2_1
            Span<float> src_demean = stackalloc float[10];
            Span<float> dst_demean = stackalloc float[10];
#else
            float[] src_demean = new float[10];
            float[] dst_demean = new float[10];
#endif
            for (int i = 0; i < 5; i++)
            {
                src_demean[i * 2 + 0] = src[i, 0] - src_mean[0];  // src_demean[i, 0]
                src_demean[i * 2 + 1] = src[i, 1] - src_mean[1];  // src_demean[i, 1]
                dst_demean[i * 2 + 0] = dst[i * 2 + 0] - dst_mean[0];  // dst_demean[i, 0]
                dst_demean[i * 2 + 1] = dst[i * 2 + 1] - dst_mean[1];  // dst_demean[i, 1]
            }

            double A00 = 0, A01 = 0, A10 = 0, A11 = 0;
            for (int i = 0; i < 5; i++)
            {
                A00 += dst_demean[i * 2 + 0] * src_demean[i * 2 + 0];  // dst_demean[i, 0] * src_demean[i, 0]
                A01 += dst_demean[i * 2 + 0] * src_demean[i * 2 + 1];  // dst_demean[i, 0] * src_demean[i, 1]
                A10 += dst_demean[i * 2 + 1] * src_demean[i * 2 + 0];  // dst_demean[i, 1] * src_demean[i, 0]
                A11 += dst_demean[i * 2 + 1] * src_demean[i * 2 + 1];  // dst_demean[i, 1] * src_demean[i, 1]
            }
            A00 /= 5.0;
            A01 /= 5.0;
            A10 /= 5.0;
            A11 /= 5.0;

            Mat A = new Mat(2, 2, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> allA = A.AsSpan<double>();
#else
            double[] allABuffar = new double[4];
            A.get(0, 0, allABuffar);
            double[] allA = allABuffar;
#endif
            allA[0] = (double)A00;
            allA[1] = (double)A01;
            allA[2] = (double)A10;
            allA[3] = (double)A11;
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            A.put(0, 0, allABuffar);
#endif

#if NET_STANDARD_2_1
            Span<double> d = stackalloc double[2] { 1.0, 1.0 };
#else
            double[] d = new double[2] { 1.0, 1.0 };
#endif
            double detA = A00 * A11 - A01 * A10;
            if (detA < 0) d[1] = -1;

            Mat s = new Mat();
            Mat u = new Mat();
            Mat vt = new Mat();
            Core.SVDecomp(A, s, u, vt);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> allS = s.AsSpan<double>();
#else
            double[] allSBuffar = new double[2];
            s.get(0, 0, allSBuffar);
            double[] allS = allSBuffar;
#endif
            double s0 = allS[0];
            double s1 = allS[1];

            double tol = Math.Max(s0, s1) * 2 * float.Epsilon;

            int rank = 0;
            if (s0 > tol) rank++;
            if (s1 > tol) rank++;

            Mat D = Mat.eye(2, 2, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> allD = D.AsSpan<double>();
#else
            double[] allDBuffar = new double[4];
            D.get(0, 0, allDBuffar);
            double[] allD = allDBuffar;
#endif
            allD[0] = d[0];
            allD[3] = d[1];
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            D.put(0, 0, allDBuffar);
#endif

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> allU = u.AsSpan<double>();
#else
            double[] allUBuffar = new double[4];
            u.get(0, 0, allUBuffar);
            double[] allU = allUBuffar;
#endif
            double det_u = allU[0] * allU[3] - allU[1] * allU[2];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> allVt = vt.AsSpan<double>();
#else
            double[] allVtBuffar = new double[4];
            vt.get(0, 0, allVtBuffar);
            double[] allVt = allVtBuffar;
#endif
            double det_vt = allVt[0] * allVt[3] - allVt[1] * allVt[2];

            Mat R;
            if (rank == 1)
            {
                if (det_u * det_vt > 0)
                {
                    R = new Mat();
                    Core.gemm(u, vt, 1, new Mat(), 0, R);  // u * vt
                }
                else
                {
                    allD[3] = -1.0;
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                    D.put(0, 0, allDBuffar);
#endif

                    R = new Mat();
                    Core.gemm(D, vt, 1, new Mat(), 0, R);  // D * vt
                    Core.gemm(u, R, 1, new Mat(), 0, R);  // u * D * vt
                }
            }
            else
            {
                R = new Mat();
                Core.gemm(D, vt, 1, new Mat(), 0, R);  // D * vt
                Core.gemm(u, R, 1, new Mat(), 0, R);  // u * D * vt
            }

            double var1 = 0.0, var2 = 0.0;
            for (int i = 0; i < 5; i++)
            {
                var1 += src_demean[i * 2 + 0] * src_demean[i * 2 + 0];  // src_demean[i, 0] * src_demean[i, 0]
                var2 += src_demean[i * 2 + 1] * src_demean[i * 2 + 1];  // src_demean[i, 1] * src_demean[i, 1]
            }
            var1 /= 5.0;
            var2 /= 5.0;

            double scale = 1.0 / (var1 + var2) * (s0 * d[0] + s1 * d[1]);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> allR = R.AsSpan<double>();
#else
            double[] allRBuffar = new double[4];
            R.get(0, 0, allRBuffar);
            double[] allR = allRBuffar;
#endif
            double ts0 = allR[0] * src_mean[0] + allR[1] * src_mean[1];
            double ts1 = allR[2] * src_mean[0] + allR[3] * src_mean[1];

            // T[2,3] -> T[6]
#if NET_STANDARD_2_1
            Span<double> T = stackalloc double[6];
#else
            double[] T = new double[6];
#endif
            T[0 * 3 + 2] = dst_mean[0] - scale * ts0;  // T[0, 2]
            T[1 * 3 + 2] = dst_mean[1] - scale * ts1;  // T[1, 2]
            T[0 * 3 + 0] = allR[0] * scale;  // T[0, 0]
            T[0 * 3 + 1] = allR[1] * scale;  // T[0, 1]
            T[1 * 3 + 0] = allR[2] * scale;  // T[1, 0]
            T[1 * 3 + 1] = allR[3] * scale;  // T[1, 1]

            Mat transform = new Mat(2, 3, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> allTransform = transform.AsSpan<double>();
#else
            double[] allTransformBuffar = new double[6];
            transform.get(0, 0, allTransformBuffar);
            double[] allTransform = allTransformBuffar;
#endif
            allTransform[0] = T[0 * 3 + 0];  // T[0, 0]
            allTransform[1] = T[0 * 3 + 1];  // T[0, 1]
            allTransform[2] = T[0 * 3 + 2];  // T[0, 2]
            allTransform[3] = T[1 * 3 + 0];  // T[1, 0]
            allTransform[4] = T[1 * 3 + 1];  // T[1, 1]
            allTransform[5] = T[1 * 3 + 2];  // T[1, 2]
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            transform.put(0, 0, allTransformBuffar);
#endif

            A.Dispose();
            s.Dispose();
            u.Dispose();
            vt.Dispose();
            D.Dispose();
            R.Dispose();

            return transform;
        }
    }
}
#endif
