#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.DnnModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Referring to https://github.com/opencv/opencv_zoo/tree/main/models/person_detection_mediapipe
    ///
    /// [Tested Models]
    /// https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/person_detection_mediapipe/person_detection_mediapipe_2023mar.onnx
    /// </summary>
    public class MediaPipePersonDetector : ProcessingWorkerBase
    {
        protected static readonly Size INPUT_SIZE = new Size(224, 224);
        protected static readonly Scalar SCALAR_RED = new Scalar(0, 0, 255, 255);
        protected static readonly Scalar SCALAR_GREEN = new Scalar(0, 255, 0, 255);
        protected static readonly Scalar SCALAR_BLUE = new Scalar(255, 0, 0, 255);
        protected static readonly Scalar SCALAR_CYAN = new Scalar(255, 255, 0, 255);
        protected static readonly Scalar SCALAR_YELLOW = new Scalar(0, 255, 255, 255);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);

        protected float _nmsThreshold;
        protected float _scoreThreshold;
        protected int _topK;
        protected int _backend;
        protected int _target;
        protected Net _personDetectionNet;
        protected List<string> _cachedUnconnectedOutLayersNames;
        protected Mat _anchors;
        protected Mat _anchors_Nx8;
        protected Mat _inputSizeMat;
        protected Mat _maxSizeImg;
        protected Mat _box_xywh;
        protected MatOfRect2d _NMS_boxes;
        protected MatOfFloat _NMS_confidences;
        protected Mat _output0Buffer;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
        protected float[] _allScoresBuffer;
        protected float[] _allBoxAndLandmarksBuffer;
        protected int[] _allIndicesBuffer;
        protected float[] _allResultBuffer;
#endif

        /// <summary>
        /// Initializes a new instance of the MediaPipePersonDetector class.
        /// </summary>
        /// <param name="modelFilepath">Path to the model file.</param>
        /// <param name="nmsThreshold">Non-maximum suppression threshold.</param>
        /// <param name="scoreThreshold">Confidence threshold for filtering detections.</param>
        /// <param name="topK">Maximum number of output detections.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipePersonDetector(string modelFilepath, float nmsThreshold = 0.3f, float scoreThreshold = 0.5f, int topK = 10,
                                         int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));

            _nmsThreshold = Mathf.Clamp01(nmsThreshold);
            _scoreThreshold = Mathf.Clamp01(scoreThreshold);
            _topK = Math.Max(1, topK);
            _backend = backend;
            _target = target;

            try
            {
                _personDetectionNet = Dnn.readNet(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _personDetectionNet.setPreferableBackend(_backend);
            _personDetectionNet.setPreferableTarget(_target);
            _cachedUnconnectedOutLayersNames = _personDetectionNet.getUnconnectedOutLayersNames();

            _output0Buffer = new Mat(_topK, 13, CvType.CV_32FC1);

            _anchors = LoadAnchors();
            _anchors_Nx8 = new Mat();
            Core.repeat(_anchors, 1, 4, _anchors_Nx8);
        }


        /// <summary>
        /// Visualizes person detection result on the input image.
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
            if (result.cols() < 13)
                throw new ArgumentException("Invalid result matrix. It must have at least 13 columns.");

            StringBuilder sb = null;

            if (printResult)
                sb = new StringBuilder(256);

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            float[] score = new float[1];
            float[] faceBox = new float[4];
            float[] personLandmarks = new float[8];
#endif

            for (int i = 0; i < result.rows(); ++i)
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ReadOnlySpan<float> score = result.AsSpan<float>().Slice(12, 1);
                ReadOnlySpan<float> faceBox = result.AsSpan<float>().Slice(0, 4);
                ReadOnlySpan<float> personLandmarks = result.AsSpan<float>().Slice(4, 8);

#else
                result.get(i, 12, score);
                result.get(i, 0, faceBox);
                result.get(i, 4, personLandmarks);
#endif

                float hipPointX = personLandmarks[0];
                float hipPointY = personLandmarks[1];
                float fullBodyX = personLandmarks[2];
                float fullBodyY = personLandmarks[3];
                float shoulderPointX = personLandmarks[4];
                float shoulderPointY = personLandmarks[5];
                float upperBodyX = personLandmarks[6];
                float upperBodyY = personLandmarks[7];

                // put score
                Imgproc.putText(image, score[0].ToString("F4"), (faceBox[0], faceBox[1] + 12), Imgproc.FONT_HERSHEY_DUPLEX, 0.5,
                                     (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple());

                // draw box
                Imgproc.rectangle(image, (faceBox[0], faceBox[1]), (faceBox[2], faceBox[3]), SCALAR_GREEN.ToValueTuple(), 2);

                // draw circle for full body
                // # radius = np.linalg.norm(hip_point - full_body).astype(np.int32)
                using (Mat fullBodyVector = new Mat(1, 1, CvType.CV_32FC2, (hipPointX - fullBodyX, hipPointY - fullBodyY, 0, 0)))
                {
                    int radius = (int)Core.norm(fullBodyVector);
                    Imgproc.circle(image, (hipPointX, hipPointY), radius,
                                     (isRGB) ? SCALAR_RED.ToValueTuple() : SCALAR_BLUE.ToValueTuple(), 2);
                }
                // draw circle for upper body
                // # radius = np.linalg.norm(shoulder_point - upper_body).astype(np.int32)
                using (Mat upperBodyVector = new Mat(1, 1, CvType.CV_32FC2, (shoulderPointX - upperBodyX, shoulderPointY - upperBodyY, 0, 0)))
                {
                    int radius = (int)Core.norm(upperBodyVector);
                    Imgproc.circle(image, (shoulderPointX, shoulderPointY), radius,
                                     (isRGB) ? SCALAR_CYAN.ToValueTuple() : SCALAR_YELLOW.ToValueTuple(), 2);
                }

                // draw points
                for (int j = 0; j < personLandmarks.Length; j += 2)
                {
                    Imgproc.circle(image, (personLandmarks[j], personLandmarks[j + 1]), 2,
                                     (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple(), 2);
                }

                if (printResult)
                {
                    sb.AppendFormat("-----------person {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.AppendFormat("Score: {0:F4}", score[0]);
                    sb.AppendLine();
                    sb.AppendFormat("Person Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", faceBox[0], faceBox[1], faceBox[2], faceBox[3]);
                    sb.AppendLine();
                    sb.Append("Person Landmarks: ");
                    sb.Append("{");
                    for (int j = 0; j < personLandmarks.Length; j++)
                    {
                        sb.AppendFormat("{0:F3}", personLandmarks[j]);
                        if (j < personLandmarks.Length - 1)
                            sb.Append(", ");
                    }
                    sb.Append("}");
                    sb.AppendLine();
                }
            }

            if (printResult)
                Debug.Log(sb.ToString());
        }

        /// <summary>
        /// Detects persons in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for person detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (13 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [bbox_coords, landmarks_coords, score]
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
        /// Detects persons in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for person detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (13 columns per detection)
        ///
        /// The returned Mat format:
        /// - Each row represents one detection
        /// - Columns: [bbox_coords, landmarks_coords, score]
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
            _personDetectionNet.setInput(inputBlob);
            List<Mat> outputBlobs = new List<Mat>();
            try
            {
                _personDetectionNet.forward(outputBlobs, _cachedUnconnectedOutLayersNames);
            }
            catch (Exception e)
            {
                inputBlob.Dispose();
                throw new ArgumentException(
                    "The input size specified in the constructor may not match the model's expected input size. " +
                    "Please verify the correct input size for your model and update the constructor parameters accordingly.", e);
            }

            // Postprocess
            Mat submat = PostProcess(outputBlobs, image.sizeAsValueTuple()); // submat of _output0Buffer is returned

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange

            inputBlob.Dispose();
            for (int i = 0; i < outputBlobs.Count; i++)
            {
                outputBlobs[i].Dispose();
            }

            return new Mat[] { submat };
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image)
        {
            // Add padding to make it square.
            int maxSize = Mathf.Max(image.cols(), image.rows());

            if (_maxSizeImg == null || _maxSizeImg.width() != maxSize || _maxSizeImg.height() != maxSize)
            {
                if (_maxSizeImg == null)
                    _maxSizeImg = new Mat();
                _maxSizeImg.create(maxSize, maxSize, image.type());
                _maxSizeImg.setTo(SCALAR_0);
            }

            using (Mat _maxSizeImg_roi = new Mat(_maxSizeImg, ((maxSize - image.cols()) / 2,
                                                     (maxSize - image.rows()) / 2, image.cols(), image.rows())))
            {
                image.copyTo(_maxSizeImg_roi);
            }

            // Create a 4D blob from a frame.
            Mat blob = Dnn.blobFromImage(_maxSizeImg, 1.0 / 127.5, INPUT_SIZE.ToValueTuple(), (127.5, 127.5, 127.5, 0),
                                             true, false, CvType.CV_32F); // HWC to NCHW, BGR to RGB [0, 1] -> [-1, 1]

            return blob; // [1, 3, 224, 224]
        }

        protected virtual Mat PostProcess(List<Mat> outputBlobs, (double width, double height) originalSize)
        {
            Mat outputBlob_0 = outputBlobs[0]; // 1*N*4+8
            Mat outputBlob_1 = outputBlobs[1]; // 1*N*1

            int num = outputBlob_0.size(1);
            Mat score = outputBlob_1.reshape(1, num);
            Mat boxAndLandmark = outputBlob_0.reshape(1, num);

            // Decode scores
            // # score = np.clip(score, -100, 100)
            NumpyClip(score, -100, 100);
            // # score = 1 / (1 + np.exp(-score))
            Core.multiply(score, (-1.0, -1.0, -1.0, -1.0), score);
            Core.exp(score, score);
            Core.add(score, (1.0, 1.0, 1.0, 1.0), score);
            Core.divide(1.0, score, score);

            // Decode boxes and landmarks
            // Divide by input size
            using (Mat boxAndLandmark_Nx1_c2 = boxAndLandmark.reshape(2, num))
            {
                Core.divide(boxAndLandmark_Nx1_c2, (INPUT_SIZE.width, INPUT_SIZE.height, 0, 0), boxAndLandmark_Nx1_c2);
            }

            // Add grid offsets to cxy
            using (Mat boxAndLandmark_cxy = boxAndLandmark.colRange((0, 2)))
            {
                Core.add(boxAndLandmark_cxy, _anchors, boxAndLandmark_cxy);
            }

            // Add grid offsets to landmarks
            using (Mat boxAndLandmark_landmark = boxAndLandmark.colRange((4, 12)))
            {
                Core.add(boxAndLandmark_landmark, _anchors_Nx8, boxAndLandmark_landmark);
            }

            if (_box_xywh == null || _box_xywh.rows() != num)
            {
                if (_box_xywh == null)
                    _box_xywh = new Mat();
                _box_xywh.create(num, 4, CvType.CV_32FC1);
            }

            // Convert boxes ([cx, cy, w, h] to [x1, y1, x2, y2])
            using (Mat boxAndLandmark_cxy = boxAndLandmark.colRange((0, 2)))
            using (Mat boxAndLandmark_wh = boxAndLandmark.colRange((2, 4)))
            {
                using (Mat _box_xywh_xy = _box_xywh.colRange((0, 2)))
                using (Mat _box_xywh_wh = _box_xywh.colRange((2, 4)))
                {
                    var tmp1 = _box_xywh_xy;
                    var tmp2 = _box_xywh_wh;

                    boxAndLandmark_cxy.copyTo(tmp2);

                    Core.divide(boxAndLandmark_wh, (2.0, 0, 0, 0), tmp1);
                    Core.subtract(tmp2, tmp1, boxAndLandmark_cxy);
                    Core.add(tmp2, tmp1, boxAndLandmark_wh);

                    // Convert boxes from [x1, y1, x2, y2] to [x, y, w, h] where Rect2d data style.
                    var boxAndLandmark_xy1 = boxAndLandmark_cxy;
                    var boxAndLandmark_xy2 = boxAndLandmark_wh;
                    boxAndLandmark_xy1.copyTo(_box_xywh_xy);
                    Core.subtract(boxAndLandmark_xy2, boxAndLandmark_xy1, _box_xywh_wh);
                }
            }

            Mat result = null;

            // NMS
            using (MatOfInt indices = new MatOfInt())
            {
                if (_NMS_boxes == null || _NMS_boxes.rows() != num)
                {
                    if (_NMS_boxes == null)
                        _NMS_boxes = new MatOfRect2d();
                    _NMS_boxes.create(num, 1, CvType.CV_64FC4);
                }
                if (_NMS_confidences == null || _NMS_confidences.rows() != num)
                {
                    if (_NMS_confidences == null)
                        _NMS_confidences = new MatOfFloat();
                    _NMS_confidences.create(num, 1, CvType.CV_32FC1);
                }

                using (Mat boxes_m_c1 = _NMS_boxes.reshape(1, num))
                {
                    _box_xywh.convertTo(boxes_m_c1, CvType.CV_64F);
                }
                score.copyTo(_NMS_confidences);

                Dnn.NMSBoxes(_NMS_boxes, _NMS_confidences, _scoreThreshold, _nmsThreshold, indices, 1f, _topK);

                lock (_lockObject)
                {
                    // Create result
                    result = CreateResultFromBuffer_Person(score, boxAndLandmark, indices, _output0Buffer);

                    // Scale boxes
                    ScaleBoxesAndLandmarks_Person(result, originalSize);
                }
            }

            score.Dispose();
            boxAndLandmark.Dispose();

            // each landmark: hip center point; full body point; shoulder center point; upper body point;
            //
            // [
            //   [bbox_coords, landmarks_coords, score]
            //   ...
            //   [bbox_coords, landmarks_coords, score]
            // ]
            return result;
        }

        protected virtual Mat CreateResultFromBuffer_Person(Mat score_Nx1, Mat boxAndLandmark_Nx12, MatOfInt indices, Mat buffer)
        {
            if (!score_Nx1.isContinuous())
                throw new ArgumentException("score_Nx1 is not continuous.");
            if (!boxAndLandmark_Nx12.isContinuous())
                throw new ArgumentException("boxAndLandmark_Nx12 is not continuous.");
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
            ReadOnlySpan<float> allScores = score_Nx1.AsSpan<float>();
            ReadOnlySpan<float> allBoxAndLandmarks = boxAndLandmark_Nx12.AsSpan<float>();
            ReadOnlySpan<int> allIndices = indices.AsSpan<int>();
            Span<float> allResult = result.AsSpan<float>();
#else
            int requiredScoresLen = score_Nx1.rows();
            int requiredBoxAndLandmarksLen = boxAndLandmark_Nx12.rows() * 12;
            int requiredIndicesLen = buffer.rows();
            int requiredResultLen = buffer.rows() * 13;
            if (_allScoresBuffer == null || _allScoresBuffer.Length < requiredScoresLen)
                _allScoresBuffer = new float[requiredScoresLen];
            if (_allBoxAndLandmarksBuffer == null || _allBoxAndLandmarksBuffer.Length < requiredBoxAndLandmarksLen)
                _allBoxAndLandmarksBuffer = new float[requiredBoxAndLandmarksLen];
            if (_allIndicesBuffer == null || _allIndicesBuffer.Length < requiredIndicesLen)
                _allIndicesBuffer = new int[requiredIndicesLen];
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            score_Nx1.get(0, 0, _allScoresBuffer);
            boxAndLandmark_Nx12.get(0, 0, _allBoxAndLandmarksBuffer);
            indices.get(0, 0, _allIndicesBuffer);
            float[] allScores = _allScoresBuffer;
            float[] allBoxAndLandmarks = _allBoxAndLandmarksBuffer;
            int[] allIndices = _allIndicesBuffer;
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int idx = allIndices[i];
                int resultOffset = i * 13;
                int boxAndLandmarksOffset = idx * 12;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                allBoxAndLandmarks.Slice(boxAndLandmarksOffset, 12).CopyTo(allResult.Slice(resultOffset, 12));
                allResult[resultOffset + 12] = allScores[idx];
#else
                Buffer.BlockCopy(allBoxAndLandmarks, boxAndLandmarksOffset * 4, allResult, resultOffset * 4, 12 * 4);
                allResult[resultOffset + 12] = allScores[idx];
#endif
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif

            return result;
        }

        protected virtual void ScaleBoxesAndLandmarks_Person(Mat result, (double width, double height) originalSize)
        {
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            int num = result.rows();
            if (num == 0)
                return;

            float original_w = (float)originalSize.width;
            float original_h = (float)originalSize.height;

            float maxSize = Mathf.Max(original_w, original_h);
            float xFactor = maxSize;
            float yFactor = maxSize;
            float xShift = (maxSize - original_w) / 2f;
            float yShift = (maxSize - original_h) / 2f;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allResult = result.AsSpan<float>();
#else
            int requiredResultLen = num * 13;
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            result.get(0, 0, _allResultBuffer);
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int resultOffset = i * 13;
                float x1 = allResult[resultOffset] * xFactor - xShift;
                float y1 = allResult[resultOffset + 1] * yFactor - yShift;
                float x2 = allResult[resultOffset + 2] * xFactor - xShift;
                float y2 = allResult[resultOffset + 3] * yFactor - yShift;

                x1 = Mathf.Clamp(x1, 0, original_w);
                y1 = Mathf.Clamp(y1, 0, original_h);
                x2 = Mathf.Clamp(x2, 0, original_w);
                y2 = Mathf.Clamp(y2, 0, original_h);

                allResult[resultOffset] = x1;
                allResult[resultOffset + 1] = y1;
                allResult[resultOffset + 2] = x2;
                allResult[resultOffset + 3] = y2;

                for (int j = 0; j < 8; ++j)
                {
                    int landmarkOffset = resultOffset + 4 + j;
                    if (j % 2 == 0)
                    {
                        allResult[landmarkOffset] = allResult[landmarkOffset] * xFactor - xShift;
                    }
                    else
                    {
                        allResult[landmarkOffset] = allResult[landmarkOffset] * yFactor - yShift;
                    }
                }
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif
        }

        // OpenCV port of numpy.clip(a, a_min, a_max) in Python.
        // Restrict the range to [a_min, a_max].
        protected virtual void NumpyClip(Mat a, double a_min, double a_max)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (a != null)
                a.ThrowIfDisposed();

            Core.min(a, (a_max, 0, 0, 0), a); // a = min(a, a_max)
            Core.max(a, (a_min, 0, 0, 0), a); // a = max(a, a_min)
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _personDetectionNet?.Dispose(); _personDetectionNet = null;
                _anchors?.Dispose(); _anchors = null;
                _anchors_Nx8?.Dispose(); _anchors_Nx8 = null;
                _inputSizeMat?.Dispose(); _inputSizeMat = null;
                _maxSizeImg?.Dispose(); _maxSizeImg = null;
                _box_xywh?.Dispose(); _box_xywh = null;
                _NMS_boxes?.Dispose(); _NMS_boxes = null;
                _NMS_confidences?.Dispose(); _NMS_confidences = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                _allScoresBuffer = null;
                _allBoxAndLandmarksBuffer = null;
                _allIndicesBuffer = null;
                _allResultBuffer = null;
#endif
            }

            base.Dispose(disposing);
        }

        protected virtual Mat LoadAnchors()
        {
            Mat anchors = new Mat(2254, 2, CvType.CV_32FC1);

            float[] anchors_arr = new float[] {
                         0.017857142857142856f, 0.017857142857142856f,
                         0.017857142857142856f, 0.017857142857142856f,
                         0.05357142857142857f, 0.017857142857142856f,
                         0.05357142857142857f, 0.017857142857142856f,
                         0.08928571428571429f, 0.017857142857142856f,
                         0.08928571428571429f, 0.017857142857142856f,
                         0.125f, 0.017857142857142856f,
                         0.125f, 0.017857142857142856f,
                         0.16071428571428573f, 0.017857142857142856f,
                         0.16071428571428573f, 0.017857142857142856f,
                         0.19642857142857142f, 0.017857142857142856f,
                         0.19642857142857142f, 0.017857142857142856f,
                         0.23214285714285715f, 0.017857142857142856f,
                         0.23214285714285715f, 0.017857142857142856f,
                         0.26785714285714285f, 0.017857142857142856f,
                         0.26785714285714285f, 0.017857142857142856f,
                         0.30357142857142855f, 0.017857142857142856f,
                         0.30357142857142855f, 0.017857142857142856f,
                         0.3392857142857143f, 0.017857142857142856f,
                         0.3392857142857143f, 0.017857142857142856f,
                         0.375f, 0.017857142857142856f,
                         0.375f, 0.017857142857142856f,
                         0.4107142857142857f, 0.017857142857142856f,
                         0.4107142857142857f, 0.017857142857142856f,
                         0.44642857142857145f, 0.017857142857142856f,
                         0.44642857142857145f, 0.017857142857142856f,
                         0.48214285714285715f, 0.017857142857142856f,
                         0.48214285714285715f, 0.017857142857142856f,
                         0.5178571428571429f, 0.017857142857142856f,
                         0.5178571428571429f, 0.017857142857142856f,
                         0.5535714285714286f, 0.017857142857142856f,
                         0.5535714285714286f, 0.017857142857142856f,
                         0.5892857142857143f, 0.017857142857142856f,
                         0.5892857142857143f, 0.017857142857142856f,
                         0.625f, 0.017857142857142856f,
                         0.625f, 0.017857142857142856f,
                         0.6607142857142857f, 0.017857142857142856f,
                         0.6607142857142857f, 0.017857142857142856f,
                         0.6964285714285714f, 0.017857142857142856f,
                         0.6964285714285714f, 0.017857142857142856f,
                         0.7321428571428571f, 0.017857142857142856f,
                         0.7321428571428571f, 0.017857142857142856f,
                         0.7678571428571429f, 0.017857142857142856f,
                         0.7678571428571429f, 0.017857142857142856f,
                         0.8035714285714286f, 0.017857142857142856f,
                         0.8035714285714286f, 0.017857142857142856f,
                         0.8392857142857143f, 0.017857142857142856f,
                         0.8392857142857143f, 0.017857142857142856f,
                         0.875f, 0.017857142857142856f,
                         0.875f, 0.017857142857142856f,
                         0.9107142857142857f, 0.017857142857142856f,
                         0.9107142857142857f, 0.017857142857142856f,
                         0.9464285714285714f, 0.017857142857142856f,
                         0.9464285714285714f, 0.017857142857142856f,
                         0.9821428571428571f, 0.017857142857142856f,
                         0.9821428571428571f, 0.017857142857142856f,
                         0.017857142857142856f, 0.05357142857142857f,
                         0.017857142857142856f, 0.05357142857142857f,
                         0.05357142857142857f, 0.05357142857142857f,
                         0.05357142857142857f, 0.05357142857142857f,
                         0.08928571428571429f, 0.05357142857142857f,
                         0.08928571428571429f, 0.05357142857142857f,
                         0.125f, 0.05357142857142857f,
                         0.125f, 0.05357142857142857f,
                         0.16071428571428573f, 0.05357142857142857f,
                         0.16071428571428573f, 0.05357142857142857f,
                         0.19642857142857142f, 0.05357142857142857f,
                         0.19642857142857142f, 0.05357142857142857f,
                         0.23214285714285715f, 0.05357142857142857f,
                         0.23214285714285715f, 0.05357142857142857f,
                         0.26785714285714285f, 0.05357142857142857f,
                         0.26785714285714285f, 0.05357142857142857f,
                         0.30357142857142855f, 0.05357142857142857f,
                         0.30357142857142855f, 0.05357142857142857f,
                         0.3392857142857143f, 0.05357142857142857f,
                         0.3392857142857143f, 0.05357142857142857f,
                         0.375f, 0.05357142857142857f,
                         0.375f, 0.05357142857142857f,
                         0.4107142857142857f, 0.05357142857142857f,
                         0.4107142857142857f, 0.05357142857142857f,
                         0.44642857142857145f, 0.05357142857142857f,
                         0.44642857142857145f, 0.05357142857142857f,
                         0.48214285714285715f, 0.05357142857142857f,
                         0.48214285714285715f, 0.05357142857142857f,
                         0.5178571428571429f, 0.05357142857142857f,
                         0.5178571428571429f, 0.05357142857142857f,
                         0.5535714285714286f, 0.05357142857142857f,
                         0.5535714285714286f, 0.05357142857142857f,
                         0.5892857142857143f, 0.05357142857142857f,
                         0.5892857142857143f, 0.05357142857142857f,
                         0.625f, 0.05357142857142857f,
                         0.625f, 0.05357142857142857f,
                         0.6607142857142857f, 0.05357142857142857f,
                         0.6607142857142857f, 0.05357142857142857f,
                         0.6964285714285714f, 0.05357142857142857f,
                         0.6964285714285714f, 0.05357142857142857f,
                         0.7321428571428571f, 0.05357142857142857f,
                         0.7321428571428571f, 0.05357142857142857f,
                         0.7678571428571429f, 0.05357142857142857f,
                         0.7678571428571429f, 0.05357142857142857f,
                         0.8035714285714286f, 0.05357142857142857f,
                         0.8035714285714286f, 0.05357142857142857f,
                         0.8392857142857143f, 0.05357142857142857f,
                         0.8392857142857143f, 0.05357142857142857f,
                         0.875f, 0.05357142857142857f,
                         0.875f, 0.05357142857142857f,
                         0.9107142857142857f, 0.05357142857142857f,
                         0.9107142857142857f, 0.05357142857142857f,
                         0.9464285714285714f, 0.05357142857142857f,
                         0.9464285714285714f, 0.05357142857142857f,
                         0.9821428571428571f, 0.05357142857142857f,
                         0.9821428571428571f, 0.05357142857142857f,
                         0.017857142857142856f, 0.08928571428571429f,
                         0.017857142857142856f, 0.08928571428571429f,
                         0.05357142857142857f, 0.08928571428571429f,
                         0.05357142857142857f, 0.08928571428571429f,
                         0.08928571428571429f, 0.08928571428571429f,
                         0.08928571428571429f, 0.08928571428571429f,
                         0.125f, 0.08928571428571429f,
                         0.125f, 0.08928571428571429f,
                         0.16071428571428573f, 0.08928571428571429f,
                         0.16071428571428573f, 0.08928571428571429f,
                         0.19642857142857142f, 0.08928571428571429f,
                         0.19642857142857142f, 0.08928571428571429f,
                         0.23214285714285715f, 0.08928571428571429f,
                         0.23214285714285715f, 0.08928571428571429f,
                         0.26785714285714285f, 0.08928571428571429f,
                         0.26785714285714285f, 0.08928571428571429f,
                         0.30357142857142855f, 0.08928571428571429f,
                         0.30357142857142855f, 0.08928571428571429f,
                         0.3392857142857143f, 0.08928571428571429f,
                         0.3392857142857143f, 0.08928571428571429f,
                         0.375f, 0.08928571428571429f,
                         0.375f, 0.08928571428571429f,
                         0.4107142857142857f, 0.08928571428571429f,
                         0.4107142857142857f, 0.08928571428571429f,
                         0.44642857142857145f, 0.08928571428571429f,
                         0.44642857142857145f, 0.08928571428571429f,
                         0.48214285714285715f, 0.08928571428571429f,
                         0.48214285714285715f, 0.08928571428571429f,
                         0.5178571428571429f, 0.08928571428571429f,
                         0.5178571428571429f, 0.08928571428571429f,
                         0.5535714285714286f, 0.08928571428571429f,
                         0.5535714285714286f, 0.08928571428571429f,
                         0.5892857142857143f, 0.08928571428571429f,
                         0.5892857142857143f, 0.08928571428571429f,
                         0.625f, 0.08928571428571429f,
                         0.625f, 0.08928571428571429f,
                         0.6607142857142857f, 0.08928571428571429f,
                         0.6607142857142857f, 0.08928571428571429f,
                         0.6964285714285714f, 0.08928571428571429f,
                         0.6964285714285714f, 0.08928571428571429f,
                         0.7321428571428571f, 0.08928571428571429f,
                         0.7321428571428571f, 0.08928571428571429f,
                         0.7678571428571429f, 0.08928571428571429f,
                         0.7678571428571429f, 0.08928571428571429f,
                         0.8035714285714286f, 0.08928571428571429f,
                         0.8035714285714286f, 0.08928571428571429f,
                         0.8392857142857143f, 0.08928571428571429f,
                         0.8392857142857143f, 0.08928571428571429f,
                         0.875f, 0.08928571428571429f,
                         0.875f, 0.08928571428571429f,
                         0.9107142857142857f, 0.08928571428571429f,
                         0.9107142857142857f, 0.08928571428571429f,
                         0.9464285714285714f, 0.08928571428571429f,
                         0.9464285714285714f, 0.08928571428571429f,
                         0.9821428571428571f, 0.08928571428571429f,
                         0.9821428571428571f, 0.08928571428571429f,
                         0.017857142857142856f, 0.125f,
                         0.017857142857142856f, 0.125f,
                         0.05357142857142857f, 0.125f,
                         0.05357142857142857f, 0.125f,
                         0.08928571428571429f, 0.125f,
                         0.08928571428571429f, 0.125f,
                         0.125f, 0.125f,
                         0.125f, 0.125f,
                         0.16071428571428573f, 0.125f,
                         0.16071428571428573f, 0.125f,
                         0.19642857142857142f, 0.125f,
                         0.19642857142857142f, 0.125f,
                         0.23214285714285715f, 0.125f,
                         0.23214285714285715f, 0.125f,
                         0.26785714285714285f, 0.125f,
                         0.26785714285714285f, 0.125f,
                         0.30357142857142855f, 0.125f,
                         0.30357142857142855f, 0.125f,
                         0.3392857142857143f, 0.125f,
                         0.3392857142857143f, 0.125f,
                         0.375f, 0.125f,
                         0.375f, 0.125f,
                         0.4107142857142857f, 0.125f,
                         0.4107142857142857f, 0.125f,
                         0.44642857142857145f, 0.125f,
                         0.44642857142857145f, 0.125f,
                         0.48214285714285715f, 0.125f,
                         0.48214285714285715f, 0.125f,
                         0.5178571428571429f, 0.125f,
                         0.5178571428571429f, 0.125f,
                         0.5535714285714286f, 0.125f,
                         0.5535714285714286f, 0.125f,
                         0.5892857142857143f, 0.125f,
                         0.5892857142857143f, 0.125f,
                         0.625f, 0.125f,
                         0.625f, 0.125f,
                         0.6607142857142857f, 0.125f,
                         0.6607142857142857f, 0.125f,
                         0.6964285714285714f, 0.125f,
                         0.6964285714285714f, 0.125f,
                         0.7321428571428571f, 0.125f,
                         0.7321428571428571f, 0.125f,
                         0.7678571428571429f, 0.125f,
                         0.7678571428571429f, 0.125f,
                         0.8035714285714286f, 0.125f,
                         0.8035714285714286f, 0.125f,
                         0.8392857142857143f, 0.125f,
                         0.8392857142857143f, 0.125f,
                         0.875f, 0.125f,
                         0.875f, 0.125f,
                         0.9107142857142857f, 0.125f,
                         0.9107142857142857f, 0.125f,
                         0.9464285714285714f, 0.125f,
                         0.9464285714285714f, 0.125f,
                         0.9821428571428571f, 0.125f,
                         0.9821428571428571f, 0.125f,
                         0.017857142857142856f, 0.16071428571428573f,
                         0.017857142857142856f, 0.16071428571428573f,
                         0.05357142857142857f, 0.16071428571428573f,
                         0.05357142857142857f, 0.16071428571428573f,
                         0.08928571428571429f, 0.16071428571428573f,
                         0.08928571428571429f, 0.16071428571428573f,
                         0.125f, 0.16071428571428573f,
                         0.125f, 0.16071428571428573f,
                         0.16071428571428573f, 0.16071428571428573f,
                         0.16071428571428573f, 0.16071428571428573f,
                         0.19642857142857142f, 0.16071428571428573f,
                         0.19642857142857142f, 0.16071428571428573f,
                         0.23214285714285715f, 0.16071428571428573f,
                         0.23214285714285715f, 0.16071428571428573f,
                         0.26785714285714285f, 0.16071428571428573f,
                         0.26785714285714285f, 0.16071428571428573f,
                         0.30357142857142855f, 0.16071428571428573f,
                         0.30357142857142855f, 0.16071428571428573f,
                         0.3392857142857143f, 0.16071428571428573f,
                         0.3392857142857143f, 0.16071428571428573f,
                         0.375f, 0.16071428571428573f,
                         0.375f, 0.16071428571428573f,
                         0.4107142857142857f, 0.16071428571428573f,
                         0.4107142857142857f, 0.16071428571428573f,
                         0.44642857142857145f, 0.16071428571428573f,
                         0.44642857142857145f, 0.16071428571428573f,
                         0.48214285714285715f, 0.16071428571428573f,
                         0.48214285714285715f, 0.16071428571428573f,
                         0.5178571428571429f, 0.16071428571428573f,
                         0.5178571428571429f, 0.16071428571428573f,
                         0.5535714285714286f, 0.16071428571428573f,
                         0.5535714285714286f, 0.16071428571428573f,
                         0.5892857142857143f, 0.16071428571428573f,
                         0.5892857142857143f, 0.16071428571428573f,
                         0.625f, 0.16071428571428573f,
                         0.625f, 0.16071428571428573f,
                         0.6607142857142857f, 0.16071428571428573f,
                         0.6607142857142857f, 0.16071428571428573f,
                         0.6964285714285714f, 0.16071428571428573f,
                         0.6964285714285714f, 0.16071428571428573f,
                         0.7321428571428571f, 0.16071428571428573f,
                         0.7321428571428571f, 0.16071428571428573f,
                         0.7678571428571429f, 0.16071428571428573f,
                         0.7678571428571429f, 0.16071428571428573f,
                         0.8035714285714286f, 0.16071428571428573f,
                         0.8035714285714286f, 0.16071428571428573f,
                         0.8392857142857143f, 0.16071428571428573f,
                         0.8392857142857143f, 0.16071428571428573f,
                         0.875f, 0.16071428571428573f,
                         0.875f, 0.16071428571428573f,
                         0.9107142857142857f, 0.16071428571428573f,
                         0.9107142857142857f, 0.16071428571428573f,
                         0.9464285714285714f, 0.16071428571428573f,
                         0.9464285714285714f, 0.16071428571428573f,
                         0.9821428571428571f, 0.16071428571428573f,
                         0.9821428571428571f, 0.16071428571428573f,
                         0.017857142857142856f, 0.19642857142857142f,
                         0.017857142857142856f, 0.19642857142857142f,
                         0.05357142857142857f, 0.19642857142857142f,
                         0.05357142857142857f, 0.19642857142857142f,
                         0.08928571428571429f, 0.19642857142857142f,
                         0.08928571428571429f, 0.19642857142857142f,
                         0.125f, 0.19642857142857142f,
                         0.125f, 0.19642857142857142f,
                         0.16071428571428573f, 0.19642857142857142f,
                         0.16071428571428573f, 0.19642857142857142f,
                         0.19642857142857142f, 0.19642857142857142f,
                         0.19642857142857142f, 0.19642857142857142f,
                         0.23214285714285715f, 0.19642857142857142f,
                         0.23214285714285715f, 0.19642857142857142f,
                         0.26785714285714285f, 0.19642857142857142f,
                         0.26785714285714285f, 0.19642857142857142f,
                         0.30357142857142855f, 0.19642857142857142f,
                         0.30357142857142855f, 0.19642857142857142f,
                         0.3392857142857143f, 0.19642857142857142f,
                         0.3392857142857143f, 0.19642857142857142f,
                         0.375f, 0.19642857142857142f,
                         0.375f, 0.19642857142857142f,
                         0.4107142857142857f, 0.19642857142857142f,
                         0.4107142857142857f, 0.19642857142857142f,
                         0.44642857142857145f, 0.19642857142857142f,
                         0.44642857142857145f, 0.19642857142857142f,
                         0.48214285714285715f, 0.19642857142857142f,
                         0.48214285714285715f, 0.19642857142857142f,
                         0.5178571428571429f, 0.19642857142857142f,
                         0.5178571428571429f, 0.19642857142857142f,
                         0.5535714285714286f, 0.19642857142857142f,
                         0.5535714285714286f, 0.19642857142857142f,
                         0.5892857142857143f, 0.19642857142857142f,
                         0.5892857142857143f, 0.19642857142857142f,
                         0.625f, 0.19642857142857142f,
                         0.625f, 0.19642857142857142f,
                         0.6607142857142857f, 0.19642857142857142f,
                         0.6607142857142857f, 0.19642857142857142f,
                         0.6964285714285714f, 0.19642857142857142f,
                         0.6964285714285714f, 0.19642857142857142f,
                         0.7321428571428571f, 0.19642857142857142f,
                         0.7321428571428571f, 0.19642857142857142f,
                         0.7678571428571429f, 0.19642857142857142f,
                         0.7678571428571429f, 0.19642857142857142f,
                         0.8035714285714286f, 0.19642857142857142f,
                         0.8035714285714286f, 0.19642857142857142f,
                         0.8392857142857143f, 0.19642857142857142f,
                         0.8392857142857143f, 0.19642857142857142f,
                         0.875f, 0.19642857142857142f,
                         0.875f, 0.19642857142857142f,
                         0.9107142857142857f, 0.19642857142857142f,
                         0.9107142857142857f, 0.19642857142857142f,
                         0.9464285714285714f, 0.19642857142857142f,
                         0.9464285714285714f, 0.19642857142857142f,
                         0.9821428571428571f, 0.19642857142857142f,
                         0.9821428571428571f, 0.19642857142857142f,
                         0.017857142857142856f, 0.23214285714285715f,
                         0.017857142857142856f, 0.23214285714285715f,
                         0.05357142857142857f, 0.23214285714285715f,
                         0.05357142857142857f, 0.23214285714285715f,
                         0.08928571428571429f, 0.23214285714285715f,
                         0.08928571428571429f, 0.23214285714285715f,
                         0.125f, 0.23214285714285715f,
                         0.125f, 0.23214285714285715f,
                         0.16071428571428573f, 0.23214285714285715f,
                         0.16071428571428573f, 0.23214285714285715f,
                         0.19642857142857142f, 0.23214285714285715f,
                         0.19642857142857142f, 0.23214285714285715f,
                         0.23214285714285715f, 0.23214285714285715f,
                         0.23214285714285715f, 0.23214285714285715f,
                         0.26785714285714285f, 0.23214285714285715f,
                         0.26785714285714285f, 0.23214285714285715f,
                         0.30357142857142855f, 0.23214285714285715f,
                         0.30357142857142855f, 0.23214285714285715f,
                         0.3392857142857143f, 0.23214285714285715f,
                         0.3392857142857143f, 0.23214285714285715f,
                         0.375f, 0.23214285714285715f,
                         0.375f, 0.23214285714285715f,
                         0.4107142857142857f, 0.23214285714285715f,
                         0.4107142857142857f, 0.23214285714285715f,
                         0.44642857142857145f, 0.23214285714285715f,
                         0.44642857142857145f, 0.23214285714285715f,
                         0.48214285714285715f, 0.23214285714285715f,
                         0.48214285714285715f, 0.23214285714285715f,
                         0.5178571428571429f, 0.23214285714285715f,
                         0.5178571428571429f, 0.23214285714285715f,
                         0.5535714285714286f, 0.23214285714285715f,
                         0.5535714285714286f, 0.23214285714285715f,
                         0.5892857142857143f, 0.23214285714285715f,
                         0.5892857142857143f, 0.23214285714285715f,
                         0.625f, 0.23214285714285715f,
                         0.625f, 0.23214285714285715f,
                         0.6607142857142857f, 0.23214285714285715f,
                         0.6607142857142857f, 0.23214285714285715f,
                         0.6964285714285714f, 0.23214285714285715f,
                         0.6964285714285714f, 0.23214285714285715f,
                         0.7321428571428571f, 0.23214285714285715f,
                         0.7321428571428571f, 0.23214285714285715f,
                         0.7678571428571429f, 0.23214285714285715f,
                         0.7678571428571429f, 0.23214285714285715f,
                         0.8035714285714286f, 0.23214285714285715f,
                         0.8035714285714286f, 0.23214285714285715f,
                         0.8392857142857143f, 0.23214285714285715f,
                         0.8392857142857143f, 0.23214285714285715f,
                         0.875f, 0.23214285714285715f,
                         0.875f, 0.23214285714285715f,
                         0.9107142857142857f, 0.23214285714285715f,
                         0.9107142857142857f, 0.23214285714285715f,
                         0.9464285714285714f, 0.23214285714285715f,
                         0.9464285714285714f, 0.23214285714285715f,
                         0.9821428571428571f, 0.23214285714285715f,
                         0.9821428571428571f, 0.23214285714285715f,
                         0.017857142857142856f, 0.26785714285714285f,
                         0.017857142857142856f, 0.26785714285714285f,
                         0.05357142857142857f, 0.26785714285714285f,
                         0.05357142857142857f, 0.26785714285714285f,
                         0.08928571428571429f, 0.26785714285714285f,
                         0.08928571428571429f, 0.26785714285714285f,
                         0.125f, 0.26785714285714285f,
                         0.125f, 0.26785714285714285f,
                         0.16071428571428573f, 0.26785714285714285f,
                         0.16071428571428573f, 0.26785714285714285f,
                         0.19642857142857142f, 0.26785714285714285f,
                         0.19642857142857142f, 0.26785714285714285f,
                         0.23214285714285715f, 0.26785714285714285f,
                         0.23214285714285715f, 0.26785714285714285f,
                         0.26785714285714285f, 0.26785714285714285f,
                         0.26785714285714285f, 0.26785714285714285f,
                         0.30357142857142855f, 0.26785714285714285f,
                         0.30357142857142855f, 0.26785714285714285f,
                         0.3392857142857143f, 0.26785714285714285f,
                         0.3392857142857143f, 0.26785714285714285f,
                         0.375f, 0.26785714285714285f,
                         0.375f, 0.26785714285714285f,
                         0.4107142857142857f, 0.26785714285714285f,
                         0.4107142857142857f, 0.26785714285714285f,
                         0.44642857142857145f, 0.26785714285714285f,
                         0.44642857142857145f, 0.26785714285714285f,
                         0.48214285714285715f, 0.26785714285714285f,
                         0.48214285714285715f, 0.26785714285714285f,
                         0.5178571428571429f, 0.26785714285714285f,
                         0.5178571428571429f, 0.26785714285714285f,
                         0.5535714285714286f, 0.26785714285714285f,
                         0.5535714285714286f, 0.26785714285714285f,
                         0.5892857142857143f, 0.26785714285714285f,
                         0.5892857142857143f, 0.26785714285714285f,
                         0.625f, 0.26785714285714285f,
                         0.625f, 0.26785714285714285f,
                         0.6607142857142857f, 0.26785714285714285f,
                         0.6607142857142857f, 0.26785714285714285f,
                         0.6964285714285714f, 0.26785714285714285f,
                         0.6964285714285714f, 0.26785714285714285f,
                         0.7321428571428571f, 0.26785714285714285f,
                         0.7321428571428571f, 0.26785714285714285f,
                         0.7678571428571429f, 0.26785714285714285f,
                         0.7678571428571429f, 0.26785714285714285f,
                         0.8035714285714286f, 0.26785714285714285f,
                         0.8035714285714286f, 0.26785714285714285f,
                         0.8392857142857143f, 0.26785714285714285f,
                         0.8392857142857143f, 0.26785714285714285f,
                         0.875f, 0.26785714285714285f,
                         0.875f, 0.26785714285714285f,
                         0.9107142857142857f, 0.26785714285714285f,
                         0.9107142857142857f, 0.26785714285714285f,
                         0.9464285714285714f, 0.26785714285714285f,
                         0.9464285714285714f, 0.26785714285714285f,
                         0.9821428571428571f, 0.26785714285714285f,
                         0.9821428571428571f, 0.26785714285714285f,
                         0.017857142857142856f, 0.30357142857142855f,
                         0.017857142857142856f, 0.30357142857142855f,
                         0.05357142857142857f, 0.30357142857142855f,
                         0.05357142857142857f, 0.30357142857142855f,
                         0.08928571428571429f, 0.30357142857142855f,
                         0.08928571428571429f, 0.30357142857142855f,
                         0.125f, 0.30357142857142855f,
                         0.125f, 0.30357142857142855f,
                         0.16071428571428573f, 0.30357142857142855f,
                         0.16071428571428573f, 0.30357142857142855f,
                         0.19642857142857142f, 0.30357142857142855f,
                         0.19642857142857142f, 0.30357142857142855f,
                         0.23214285714285715f, 0.30357142857142855f,
                         0.23214285714285715f, 0.30357142857142855f,
                         0.26785714285714285f, 0.30357142857142855f,
                         0.26785714285714285f, 0.30357142857142855f,
                         0.30357142857142855f, 0.30357142857142855f,
                         0.30357142857142855f, 0.30357142857142855f,
                         0.3392857142857143f, 0.30357142857142855f,
                         0.3392857142857143f, 0.30357142857142855f,
                         0.375f, 0.30357142857142855f,
                         0.375f, 0.30357142857142855f,
                         0.4107142857142857f, 0.30357142857142855f,
                         0.4107142857142857f, 0.30357142857142855f,
                         0.44642857142857145f, 0.30357142857142855f,
                         0.44642857142857145f, 0.30357142857142855f,
                         0.48214285714285715f, 0.30357142857142855f,
                         0.48214285714285715f, 0.30357142857142855f,
                         0.5178571428571429f, 0.30357142857142855f,
                         0.5178571428571429f, 0.30357142857142855f,
                         0.5535714285714286f, 0.30357142857142855f,
                         0.5535714285714286f, 0.30357142857142855f,
                         0.5892857142857143f, 0.30357142857142855f,
                         0.5892857142857143f, 0.30357142857142855f,
                         0.625f, 0.30357142857142855f,
                         0.625f, 0.30357142857142855f,
                         0.6607142857142857f, 0.30357142857142855f,
                         0.6607142857142857f, 0.30357142857142855f,
                         0.6964285714285714f, 0.30357142857142855f,
                         0.6964285714285714f, 0.30357142857142855f,
                         0.7321428571428571f, 0.30357142857142855f,
                         0.7321428571428571f, 0.30357142857142855f,
                         0.7678571428571429f, 0.30357142857142855f,
                         0.7678571428571429f, 0.30357142857142855f,
                         0.8035714285714286f, 0.30357142857142855f,
                         0.8035714285714286f, 0.30357142857142855f,
                         0.8392857142857143f, 0.30357142857142855f,
                         0.8392857142857143f, 0.30357142857142855f,
                         0.875f, 0.30357142857142855f,
                         0.875f, 0.30357142857142855f,
                         0.9107142857142857f, 0.30357142857142855f,
                         0.9107142857142857f, 0.30357142857142855f,
                         0.9464285714285714f, 0.30357142857142855f,
                         0.9464285714285714f, 0.30357142857142855f,
                         0.9821428571428571f, 0.30357142857142855f,
                         0.9821428571428571f, 0.30357142857142855f,
                         0.017857142857142856f, 0.3392857142857143f,
                         0.017857142857142856f, 0.3392857142857143f,
                         0.05357142857142857f, 0.3392857142857143f,
                         0.05357142857142857f, 0.3392857142857143f,
                         0.08928571428571429f, 0.3392857142857143f,
                         0.08928571428571429f, 0.3392857142857143f,
                         0.125f, 0.3392857142857143f,
                         0.125f, 0.3392857142857143f,
                         0.16071428571428573f, 0.3392857142857143f,
                         0.16071428571428573f, 0.3392857142857143f,
                         0.19642857142857142f, 0.3392857142857143f,
                         0.19642857142857142f, 0.3392857142857143f,
                         0.23214285714285715f, 0.3392857142857143f,
                         0.23214285714285715f, 0.3392857142857143f,
                         0.26785714285714285f, 0.3392857142857143f,
                         0.26785714285714285f, 0.3392857142857143f,
                         0.30357142857142855f, 0.3392857142857143f,
                         0.30357142857142855f, 0.3392857142857143f,
                         0.3392857142857143f, 0.3392857142857143f,
                         0.3392857142857143f, 0.3392857142857143f,
                         0.375f, 0.3392857142857143f,
                         0.375f, 0.3392857142857143f,
                         0.4107142857142857f, 0.3392857142857143f,
                         0.4107142857142857f, 0.3392857142857143f,
                         0.44642857142857145f, 0.3392857142857143f,
                         0.44642857142857145f, 0.3392857142857143f,
                         0.48214285714285715f, 0.3392857142857143f,
                         0.48214285714285715f, 0.3392857142857143f,
                         0.5178571428571429f, 0.3392857142857143f,
                         0.5178571428571429f, 0.3392857142857143f,
                         0.5535714285714286f, 0.3392857142857143f,
                         0.5535714285714286f, 0.3392857142857143f,
                         0.5892857142857143f, 0.3392857142857143f,
                         0.5892857142857143f, 0.3392857142857143f,
                         0.625f, 0.3392857142857143f,
                         0.625f, 0.3392857142857143f,
                         0.6607142857142857f, 0.3392857142857143f,
                         0.6607142857142857f, 0.3392857142857143f,
                         0.6964285714285714f, 0.3392857142857143f,
                         0.6964285714285714f, 0.3392857142857143f,
                         0.7321428571428571f, 0.3392857142857143f,
                         0.7321428571428571f, 0.3392857142857143f,
                         0.7678571428571429f, 0.3392857142857143f,
                         0.7678571428571429f, 0.3392857142857143f,
                         0.8035714285714286f, 0.3392857142857143f,
                         0.8035714285714286f, 0.3392857142857143f,
                         0.8392857142857143f, 0.3392857142857143f,
                         0.8392857142857143f, 0.3392857142857143f,
                         0.875f, 0.3392857142857143f,
                         0.875f, 0.3392857142857143f,
                         0.9107142857142857f, 0.3392857142857143f,
                         0.9107142857142857f, 0.3392857142857143f,
                         0.9464285714285714f, 0.3392857142857143f,
                         0.9464285714285714f, 0.3392857142857143f,
                         0.9821428571428571f, 0.3392857142857143f,
                         0.9821428571428571f, 0.3392857142857143f,
                         0.017857142857142856f, 0.375f,
                         0.017857142857142856f, 0.375f,
                         0.05357142857142857f, 0.375f,
                         0.05357142857142857f, 0.375f,
                         0.08928571428571429f, 0.375f,
                         0.08928571428571429f, 0.375f,
                         0.125f, 0.375f,
                         0.125f, 0.375f,
                         0.16071428571428573f, 0.375f,
                         0.16071428571428573f, 0.375f,
                         0.19642857142857142f, 0.375f,
                         0.19642857142857142f, 0.375f,
                         0.23214285714285715f, 0.375f,
                         0.23214285714285715f, 0.375f,
                         0.26785714285714285f, 0.375f,
                         0.26785714285714285f, 0.375f,
                         0.30357142857142855f, 0.375f,
                         0.30357142857142855f, 0.375f,
                         0.3392857142857143f, 0.375f,
                         0.3392857142857143f, 0.375f,
                         0.375f, 0.375f,
                         0.375f, 0.375f,
                         0.4107142857142857f, 0.375f,
                         0.4107142857142857f, 0.375f,
                         0.44642857142857145f, 0.375f,
                         0.44642857142857145f, 0.375f,
                         0.48214285714285715f, 0.375f,
                         0.48214285714285715f, 0.375f,
                         0.5178571428571429f, 0.375f,
                         0.5178571428571429f, 0.375f,
                         0.5535714285714286f, 0.375f,
                         0.5535714285714286f, 0.375f,
                         0.5892857142857143f, 0.375f,
                         0.5892857142857143f, 0.375f,
                         0.625f, 0.375f,
                         0.625f, 0.375f,
                         0.6607142857142857f, 0.375f,
                         0.6607142857142857f, 0.375f,
                         0.6964285714285714f, 0.375f,
                         0.6964285714285714f, 0.375f,
                         0.7321428571428571f, 0.375f,
                         0.7321428571428571f, 0.375f,
                         0.7678571428571429f, 0.375f,
                         0.7678571428571429f, 0.375f,
                         0.8035714285714286f, 0.375f,
                         0.8035714285714286f, 0.375f,
                         0.8392857142857143f, 0.375f,
                         0.8392857142857143f, 0.375f,
                         0.875f, 0.375f,
                         0.875f, 0.375f,
                         0.9107142857142857f, 0.375f,
                         0.9107142857142857f, 0.375f,
                         0.9464285714285714f, 0.375f,
                         0.9464285714285714f, 0.375f,
                         0.9821428571428571f, 0.375f,
                         0.9821428571428571f, 0.375f,
                         0.017857142857142856f, 0.4107142857142857f,
                         0.017857142857142856f, 0.4107142857142857f,
                         0.05357142857142857f, 0.4107142857142857f,
                         0.05357142857142857f, 0.4107142857142857f,
                         0.08928571428571429f, 0.4107142857142857f,
                         0.08928571428571429f, 0.4107142857142857f,
                         0.125f, 0.4107142857142857f,
                         0.125f, 0.4107142857142857f,
                         0.16071428571428573f, 0.4107142857142857f,
                         0.16071428571428573f, 0.4107142857142857f,
                         0.19642857142857142f, 0.4107142857142857f,
                         0.19642857142857142f, 0.4107142857142857f,
                         0.23214285714285715f, 0.4107142857142857f,
                         0.23214285714285715f, 0.4107142857142857f,
                         0.26785714285714285f, 0.4107142857142857f,
                         0.26785714285714285f, 0.4107142857142857f,
                         0.30357142857142855f, 0.4107142857142857f,
                         0.30357142857142855f, 0.4107142857142857f,
                         0.3392857142857143f, 0.4107142857142857f,
                         0.3392857142857143f, 0.4107142857142857f,
                         0.375f, 0.4107142857142857f,
                         0.375f, 0.4107142857142857f,
                         0.4107142857142857f, 0.4107142857142857f,
                         0.4107142857142857f, 0.4107142857142857f,
                         0.44642857142857145f, 0.4107142857142857f,
                         0.44642857142857145f, 0.4107142857142857f,
                         0.48214285714285715f, 0.4107142857142857f,
                         0.48214285714285715f, 0.4107142857142857f,
                         0.5178571428571429f, 0.4107142857142857f,
                         0.5178571428571429f, 0.4107142857142857f,
                         0.5535714285714286f, 0.4107142857142857f,
                         0.5535714285714286f, 0.4107142857142857f,
                         0.5892857142857143f, 0.4107142857142857f,
                         0.5892857142857143f, 0.4107142857142857f,
                         0.625f, 0.4107142857142857f,
                         0.625f, 0.4107142857142857f,
                         0.6607142857142857f, 0.4107142857142857f,
                         0.6607142857142857f, 0.4107142857142857f,
                         0.6964285714285714f, 0.4107142857142857f,
                         0.6964285714285714f, 0.4107142857142857f,
                         0.7321428571428571f, 0.4107142857142857f,
                         0.7321428571428571f, 0.4107142857142857f,
                         0.7678571428571429f, 0.4107142857142857f,
                         0.7678571428571429f, 0.4107142857142857f,
                         0.8035714285714286f, 0.4107142857142857f,
                         0.8035714285714286f, 0.4107142857142857f,
                         0.8392857142857143f, 0.4107142857142857f,
                         0.8392857142857143f, 0.4107142857142857f,
                         0.875f, 0.4107142857142857f,
                         0.875f, 0.4107142857142857f,
                         0.9107142857142857f, 0.4107142857142857f,
                         0.9107142857142857f, 0.4107142857142857f,
                         0.9464285714285714f, 0.4107142857142857f,
                         0.9464285714285714f, 0.4107142857142857f,
                         0.9821428571428571f, 0.4107142857142857f,
                         0.9821428571428571f, 0.4107142857142857f,
                         0.017857142857142856f, 0.44642857142857145f,
                         0.017857142857142856f, 0.44642857142857145f,
                         0.05357142857142857f, 0.44642857142857145f,
                         0.05357142857142857f, 0.44642857142857145f,
                         0.08928571428571429f, 0.44642857142857145f,
                         0.08928571428571429f, 0.44642857142857145f,
                         0.125f, 0.44642857142857145f,
                         0.125f, 0.44642857142857145f,
                         0.16071428571428573f, 0.44642857142857145f,
                         0.16071428571428573f, 0.44642857142857145f,
                         0.19642857142857142f, 0.44642857142857145f,
                         0.19642857142857142f, 0.44642857142857145f,
                         0.23214285714285715f, 0.44642857142857145f,
                         0.23214285714285715f, 0.44642857142857145f,
                         0.26785714285714285f, 0.44642857142857145f,
                         0.26785714285714285f, 0.44642857142857145f,
                         0.30357142857142855f, 0.44642857142857145f,
                         0.30357142857142855f, 0.44642857142857145f,
                         0.3392857142857143f, 0.44642857142857145f,
                         0.3392857142857143f, 0.44642857142857145f,
                         0.375f, 0.44642857142857145f,
                         0.375f, 0.44642857142857145f,
                         0.4107142857142857f, 0.44642857142857145f,
                         0.4107142857142857f, 0.44642857142857145f,
                         0.44642857142857145f, 0.44642857142857145f,
                         0.44642857142857145f, 0.44642857142857145f,
                         0.48214285714285715f, 0.44642857142857145f,
                         0.48214285714285715f, 0.44642857142857145f,
                         0.5178571428571429f, 0.44642857142857145f,
                         0.5178571428571429f, 0.44642857142857145f,
                         0.5535714285714286f, 0.44642857142857145f,
                         0.5535714285714286f, 0.44642857142857145f,
                         0.5892857142857143f, 0.44642857142857145f,
                         0.5892857142857143f, 0.44642857142857145f,
                         0.625f, 0.44642857142857145f,
                         0.625f, 0.44642857142857145f,
                         0.6607142857142857f, 0.44642857142857145f,
                         0.6607142857142857f, 0.44642857142857145f,
                         0.6964285714285714f, 0.44642857142857145f,
                         0.6964285714285714f, 0.44642857142857145f,
                         0.7321428571428571f, 0.44642857142857145f,
                         0.7321428571428571f, 0.44642857142857145f,
                         0.7678571428571429f, 0.44642857142857145f,
                         0.7678571428571429f, 0.44642857142857145f,
                         0.8035714285714286f, 0.44642857142857145f,
                         0.8035714285714286f, 0.44642857142857145f,
                         0.8392857142857143f, 0.44642857142857145f,
                         0.8392857142857143f, 0.44642857142857145f,
                         0.875f, 0.44642857142857145f,
                         0.875f, 0.44642857142857145f,
                         0.9107142857142857f, 0.44642857142857145f,
                         0.9107142857142857f, 0.44642857142857145f,
                         0.9464285714285714f, 0.44642857142857145f,
                         0.9464285714285714f, 0.44642857142857145f,
                         0.9821428571428571f, 0.44642857142857145f,
                         0.9821428571428571f, 0.44642857142857145f,
                         0.017857142857142856f, 0.48214285714285715f,
                         0.017857142857142856f, 0.48214285714285715f,
                         0.05357142857142857f, 0.48214285714285715f,
                         0.05357142857142857f, 0.48214285714285715f,
                         0.08928571428571429f, 0.48214285714285715f,
                         0.08928571428571429f, 0.48214285714285715f,
                         0.125f, 0.48214285714285715f,
                         0.125f, 0.48214285714285715f,
                         0.16071428571428573f, 0.48214285714285715f,
                         0.16071428571428573f, 0.48214285714285715f,
                         0.19642857142857142f, 0.48214285714285715f,
                         0.19642857142857142f, 0.48214285714285715f,
                         0.23214285714285715f, 0.48214285714285715f,
                         0.23214285714285715f, 0.48214285714285715f,
                         0.26785714285714285f, 0.48214285714285715f,
                         0.26785714285714285f, 0.48214285714285715f,
                         0.30357142857142855f, 0.48214285714285715f,
                         0.30357142857142855f, 0.48214285714285715f,
                         0.3392857142857143f, 0.48214285714285715f,
                         0.3392857142857143f, 0.48214285714285715f,
                         0.375f, 0.48214285714285715f,
                         0.375f, 0.48214285714285715f,
                         0.4107142857142857f, 0.48214285714285715f,
                         0.4107142857142857f, 0.48214285714285715f,
                         0.44642857142857145f, 0.48214285714285715f,
                         0.44642857142857145f, 0.48214285714285715f,
                         0.48214285714285715f, 0.48214285714285715f,
                         0.48214285714285715f, 0.48214285714285715f,
                         0.5178571428571429f, 0.48214285714285715f,
                         0.5178571428571429f, 0.48214285714285715f,
                         0.5535714285714286f, 0.48214285714285715f,
                         0.5535714285714286f, 0.48214285714285715f,
                         0.5892857142857143f, 0.48214285714285715f,
                         0.5892857142857143f, 0.48214285714285715f,
                         0.625f, 0.48214285714285715f,
                         0.625f, 0.48214285714285715f,
                         0.6607142857142857f, 0.48214285714285715f,
                         0.6607142857142857f, 0.48214285714285715f,
                         0.6964285714285714f, 0.48214285714285715f,
                         0.6964285714285714f, 0.48214285714285715f,
                         0.7321428571428571f, 0.48214285714285715f,
                         0.7321428571428571f, 0.48214285714285715f,
                         0.7678571428571429f, 0.48214285714285715f,
                         0.7678571428571429f, 0.48214285714285715f,
                         0.8035714285714286f, 0.48214285714285715f,
                         0.8035714285714286f, 0.48214285714285715f,
                         0.8392857142857143f, 0.48214285714285715f,
                         0.8392857142857143f, 0.48214285714285715f,
                         0.875f, 0.48214285714285715f,
                         0.875f, 0.48214285714285715f,
                         0.9107142857142857f, 0.48214285714285715f,
                         0.9107142857142857f, 0.48214285714285715f,
                         0.9464285714285714f, 0.48214285714285715f,
                         0.9464285714285714f, 0.48214285714285715f,
                         0.9821428571428571f, 0.48214285714285715f,
                         0.9821428571428571f, 0.48214285714285715f,
                         0.017857142857142856f, 0.5178571428571429f,
                         0.017857142857142856f, 0.5178571428571429f,
                         0.05357142857142857f, 0.5178571428571429f,
                         0.05357142857142857f, 0.5178571428571429f,
                         0.08928571428571429f, 0.5178571428571429f,
                         0.08928571428571429f, 0.5178571428571429f,
                         0.125f, 0.5178571428571429f,
                         0.125f, 0.5178571428571429f,
                         0.16071428571428573f, 0.5178571428571429f,
                         0.16071428571428573f, 0.5178571428571429f,
                         0.19642857142857142f, 0.5178571428571429f,
                         0.19642857142857142f, 0.5178571428571429f,
                         0.23214285714285715f, 0.5178571428571429f,
                         0.23214285714285715f, 0.5178571428571429f,
                         0.26785714285714285f, 0.5178571428571429f,
                         0.26785714285714285f, 0.5178571428571429f,
                         0.30357142857142855f, 0.5178571428571429f,
                         0.30357142857142855f, 0.5178571428571429f,
                         0.3392857142857143f, 0.5178571428571429f,
                         0.3392857142857143f, 0.5178571428571429f,
                         0.375f, 0.5178571428571429f,
                         0.375f, 0.5178571428571429f,
                         0.4107142857142857f, 0.5178571428571429f,
                         0.4107142857142857f, 0.5178571428571429f,
                         0.44642857142857145f, 0.5178571428571429f,
                         0.44642857142857145f, 0.5178571428571429f,
                         0.48214285714285715f, 0.5178571428571429f,
                         0.48214285714285715f, 0.5178571428571429f,
                         0.5178571428571429f, 0.5178571428571429f,
                         0.5178571428571429f, 0.5178571428571429f,
                         0.5535714285714286f, 0.5178571428571429f,
                         0.5535714285714286f, 0.5178571428571429f,
                         0.5892857142857143f, 0.5178571428571429f,
                         0.5892857142857143f, 0.5178571428571429f,
                         0.625f, 0.5178571428571429f,
                         0.625f, 0.5178571428571429f,
                         0.6607142857142857f, 0.5178571428571429f,
                         0.6607142857142857f, 0.5178571428571429f,
                         0.6964285714285714f, 0.5178571428571429f,
                         0.6964285714285714f, 0.5178571428571429f,
                         0.7321428571428571f, 0.5178571428571429f,
                         0.7321428571428571f, 0.5178571428571429f,
                         0.7678571428571429f, 0.5178571428571429f,
                         0.7678571428571429f, 0.5178571428571429f,
                         0.8035714285714286f, 0.5178571428571429f,
                         0.8035714285714286f, 0.5178571428571429f,
                         0.8392857142857143f, 0.5178571428571429f,
                         0.8392857142857143f, 0.5178571428571429f,
                         0.875f, 0.5178571428571429f,
                         0.875f, 0.5178571428571429f,
                         0.9107142857142857f, 0.5178571428571429f,
                         0.9107142857142857f, 0.5178571428571429f,
                         0.9464285714285714f, 0.5178571428571429f,
                         0.9464285714285714f, 0.5178571428571429f,
                         0.9821428571428571f, 0.5178571428571429f,
                         0.9821428571428571f, 0.5178571428571429f,
                         0.017857142857142856f, 0.5535714285714286f,
                         0.017857142857142856f, 0.5535714285714286f,
                         0.05357142857142857f, 0.5535714285714286f,
                         0.05357142857142857f, 0.5535714285714286f,
                         0.08928571428571429f, 0.5535714285714286f,
                         0.08928571428571429f, 0.5535714285714286f,
                         0.125f, 0.5535714285714286f,
                         0.125f, 0.5535714285714286f,
                         0.16071428571428573f, 0.5535714285714286f,
                         0.16071428571428573f, 0.5535714285714286f,
                         0.19642857142857142f, 0.5535714285714286f,
                         0.19642857142857142f, 0.5535714285714286f,
                         0.23214285714285715f, 0.5535714285714286f,
                         0.23214285714285715f, 0.5535714285714286f,
                         0.26785714285714285f, 0.5535714285714286f,
                         0.26785714285714285f, 0.5535714285714286f,
                         0.30357142857142855f, 0.5535714285714286f,
                         0.30357142857142855f, 0.5535714285714286f,
                         0.3392857142857143f, 0.5535714285714286f,
                         0.3392857142857143f, 0.5535714285714286f,
                         0.375f, 0.5535714285714286f,
                         0.375f, 0.5535714285714286f,
                         0.4107142857142857f, 0.5535714285714286f,
                         0.4107142857142857f, 0.5535714285714286f,
                         0.44642857142857145f, 0.5535714285714286f,
                         0.44642857142857145f, 0.5535714285714286f,
                         0.48214285714285715f, 0.5535714285714286f,
                         0.48214285714285715f, 0.5535714285714286f,
                         0.5178571428571429f, 0.5535714285714286f,
                         0.5178571428571429f, 0.5535714285714286f,
                         0.5535714285714286f, 0.5535714285714286f,
                         0.5535714285714286f, 0.5535714285714286f,
                         0.5892857142857143f, 0.5535714285714286f,
                         0.5892857142857143f, 0.5535714285714286f,
                         0.625f, 0.5535714285714286f,
                         0.625f, 0.5535714285714286f,
                         0.6607142857142857f, 0.5535714285714286f,
                         0.6607142857142857f, 0.5535714285714286f,
                         0.6964285714285714f, 0.5535714285714286f,
                         0.6964285714285714f, 0.5535714285714286f,
                         0.7321428571428571f, 0.5535714285714286f,
                         0.7321428571428571f, 0.5535714285714286f,
                         0.7678571428571429f, 0.5535714285714286f,
                         0.7678571428571429f, 0.5535714285714286f,
                         0.8035714285714286f, 0.5535714285714286f,
                         0.8035714285714286f, 0.5535714285714286f,
                         0.8392857142857143f, 0.5535714285714286f,
                         0.8392857142857143f, 0.5535714285714286f,
                         0.875f, 0.5535714285714286f,
                         0.875f, 0.5535714285714286f,
                         0.9107142857142857f, 0.5535714285714286f,
                         0.9107142857142857f, 0.5535714285714286f,
                         0.9464285714285714f, 0.5535714285714286f,
                         0.9464285714285714f, 0.5535714285714286f,
                         0.9821428571428571f, 0.5535714285714286f,
                         0.9821428571428571f, 0.5535714285714286f,
                         0.017857142857142856f, 0.5892857142857143f,
                         0.017857142857142856f, 0.5892857142857143f,
                         0.05357142857142857f, 0.5892857142857143f,
                         0.05357142857142857f, 0.5892857142857143f,
                         0.08928571428571429f, 0.5892857142857143f,
                         0.08928571428571429f, 0.5892857142857143f,
                         0.125f, 0.5892857142857143f,
                         0.125f, 0.5892857142857143f,
                         0.16071428571428573f, 0.5892857142857143f,
                         0.16071428571428573f, 0.5892857142857143f,
                         0.19642857142857142f, 0.5892857142857143f,
                         0.19642857142857142f, 0.5892857142857143f,
                         0.23214285714285715f, 0.5892857142857143f,
                         0.23214285714285715f, 0.5892857142857143f,
                         0.26785714285714285f, 0.5892857142857143f,
                         0.26785714285714285f, 0.5892857142857143f,
                         0.30357142857142855f, 0.5892857142857143f,
                         0.30357142857142855f, 0.5892857142857143f,
                         0.3392857142857143f, 0.5892857142857143f,
                         0.3392857142857143f, 0.5892857142857143f,
                         0.375f, 0.5892857142857143f,
                         0.375f, 0.5892857142857143f,
                         0.4107142857142857f, 0.5892857142857143f,
                         0.4107142857142857f, 0.5892857142857143f,
                         0.44642857142857145f, 0.5892857142857143f,
                         0.44642857142857145f, 0.5892857142857143f,
                         0.48214285714285715f, 0.5892857142857143f,
                         0.48214285714285715f, 0.5892857142857143f,
                         0.5178571428571429f, 0.5892857142857143f,
                         0.5178571428571429f, 0.5892857142857143f,
                         0.5535714285714286f, 0.5892857142857143f,
                         0.5535714285714286f, 0.5892857142857143f,
                         0.5892857142857143f, 0.5892857142857143f,
                         0.5892857142857143f, 0.5892857142857143f,
                         0.625f, 0.5892857142857143f,
                         0.625f, 0.5892857142857143f,
                         0.6607142857142857f, 0.5892857142857143f,
                         0.6607142857142857f, 0.5892857142857143f,
                         0.6964285714285714f, 0.5892857142857143f,
                         0.6964285714285714f, 0.5892857142857143f,
                         0.7321428571428571f, 0.5892857142857143f,
                         0.7321428571428571f, 0.5892857142857143f,
                         0.7678571428571429f, 0.5892857142857143f,
                         0.7678571428571429f, 0.5892857142857143f,
                         0.8035714285714286f, 0.5892857142857143f,
                         0.8035714285714286f, 0.5892857142857143f,
                         0.8392857142857143f, 0.5892857142857143f,
                         0.8392857142857143f, 0.5892857142857143f,
                         0.875f, 0.5892857142857143f,
                         0.875f, 0.5892857142857143f,
                         0.9107142857142857f, 0.5892857142857143f,
                         0.9107142857142857f, 0.5892857142857143f,
                         0.9464285714285714f, 0.5892857142857143f,
                         0.9464285714285714f, 0.5892857142857143f,
                         0.9821428571428571f, 0.5892857142857143f,
                         0.9821428571428571f, 0.5892857142857143f,
                         0.017857142857142856f, 0.625f,
                         0.017857142857142856f, 0.625f,
                         0.05357142857142857f, 0.625f,
                         0.05357142857142857f, 0.625f,
                         0.08928571428571429f, 0.625f,
                         0.08928571428571429f, 0.625f,
                         0.125f, 0.625f,
                         0.125f, 0.625f,
                         0.16071428571428573f, 0.625f,
                         0.16071428571428573f, 0.625f,
                         0.19642857142857142f, 0.625f,
                         0.19642857142857142f, 0.625f,
                         0.23214285714285715f, 0.625f,
                         0.23214285714285715f, 0.625f,
                         0.26785714285714285f, 0.625f,
                         0.26785714285714285f, 0.625f,
                         0.30357142857142855f, 0.625f,
                         0.30357142857142855f, 0.625f,
                         0.3392857142857143f, 0.625f,
                         0.3392857142857143f, 0.625f,
                         0.375f, 0.625f,
                         0.375f, 0.625f,
                         0.4107142857142857f, 0.625f,
                         0.4107142857142857f, 0.625f,
                         0.44642857142857145f, 0.625f,
                         0.44642857142857145f, 0.625f,
                         0.48214285714285715f, 0.625f,
                         0.48214285714285715f, 0.625f,
                         0.5178571428571429f, 0.625f,
                         0.5178571428571429f, 0.625f,
                         0.5535714285714286f, 0.625f,
                         0.5535714285714286f, 0.625f,
                         0.5892857142857143f, 0.625f,
                         0.5892857142857143f, 0.625f,
                         0.625f, 0.625f,
                         0.625f, 0.625f,
                         0.6607142857142857f, 0.625f,
                         0.6607142857142857f, 0.625f,
                         0.6964285714285714f, 0.625f,
                         0.6964285714285714f, 0.625f,
                         0.7321428571428571f, 0.625f,
                         0.7321428571428571f, 0.625f,
                         0.7678571428571429f, 0.625f,
                         0.7678571428571429f, 0.625f,
                         0.8035714285714286f, 0.625f,
                         0.8035714285714286f, 0.625f,
                         0.8392857142857143f, 0.625f,
                         0.8392857142857143f, 0.625f,
                         0.875f, 0.625f,
                         0.875f, 0.625f,
                         0.9107142857142857f, 0.625f,
                         0.9107142857142857f, 0.625f,
                         0.9464285714285714f, 0.625f,
                         0.9464285714285714f, 0.625f,
                         0.9821428571428571f, 0.625f,
                         0.9821428571428571f, 0.625f,
                         0.017857142857142856f, 0.6607142857142857f,
                         0.017857142857142856f, 0.6607142857142857f,
                         0.05357142857142857f, 0.6607142857142857f,
                         0.05357142857142857f, 0.6607142857142857f,
                         0.08928571428571429f, 0.6607142857142857f,
                         0.08928571428571429f, 0.6607142857142857f,
                         0.125f, 0.6607142857142857f,
                         0.125f, 0.6607142857142857f,
                         0.16071428571428573f, 0.6607142857142857f,
                         0.16071428571428573f, 0.6607142857142857f,
                         0.19642857142857142f, 0.6607142857142857f,
                         0.19642857142857142f, 0.6607142857142857f,
                         0.23214285714285715f, 0.6607142857142857f,
                         0.23214285714285715f, 0.6607142857142857f,
                         0.26785714285714285f, 0.6607142857142857f,
                         0.26785714285714285f, 0.6607142857142857f,
                         0.30357142857142855f, 0.6607142857142857f,
                         0.30357142857142855f, 0.6607142857142857f,
                         0.3392857142857143f, 0.6607142857142857f,
                         0.3392857142857143f, 0.6607142857142857f,
                         0.375f, 0.6607142857142857f,
                         0.375f, 0.6607142857142857f,
                         0.4107142857142857f, 0.6607142857142857f,
                         0.4107142857142857f, 0.6607142857142857f,
                         0.44642857142857145f, 0.6607142857142857f,
                         0.44642857142857145f, 0.6607142857142857f,
                         0.48214285714285715f, 0.6607142857142857f,
                         0.48214285714285715f, 0.6607142857142857f,
                         0.5178571428571429f, 0.6607142857142857f,
                         0.5178571428571429f, 0.6607142857142857f,
                         0.5535714285714286f, 0.6607142857142857f,
                         0.5535714285714286f, 0.6607142857142857f,
                         0.5892857142857143f, 0.6607142857142857f,
                         0.5892857142857143f, 0.6607142857142857f,
                         0.625f, 0.6607142857142857f,
                         0.625f, 0.6607142857142857f,
                         0.6607142857142857f, 0.6607142857142857f,
                         0.6607142857142857f, 0.6607142857142857f,
                         0.6964285714285714f, 0.6607142857142857f,
                         0.6964285714285714f, 0.6607142857142857f,
                         0.7321428571428571f, 0.6607142857142857f,
                         0.7321428571428571f, 0.6607142857142857f,
                         0.7678571428571429f, 0.6607142857142857f,
                         0.7678571428571429f, 0.6607142857142857f,
                         0.8035714285714286f, 0.6607142857142857f,
                         0.8035714285714286f, 0.6607142857142857f,
                         0.8392857142857143f, 0.6607142857142857f,
                         0.8392857142857143f, 0.6607142857142857f,
                         0.875f, 0.6607142857142857f,
                         0.875f, 0.6607142857142857f,
                         0.9107142857142857f, 0.6607142857142857f,
                         0.9107142857142857f, 0.6607142857142857f,
                         0.9464285714285714f, 0.6607142857142857f,
                         0.9464285714285714f, 0.6607142857142857f,
                         0.9821428571428571f, 0.6607142857142857f,
                         0.9821428571428571f, 0.6607142857142857f,
                         0.017857142857142856f, 0.6964285714285714f,
                         0.017857142857142856f, 0.6964285714285714f,
                         0.05357142857142857f, 0.6964285714285714f,
                         0.05357142857142857f, 0.6964285714285714f,
                         0.08928571428571429f, 0.6964285714285714f,
                         0.08928571428571429f, 0.6964285714285714f,
                         0.125f, 0.6964285714285714f,
                         0.125f, 0.6964285714285714f,
                         0.16071428571428573f, 0.6964285714285714f,
                         0.16071428571428573f, 0.6964285714285714f,
                         0.19642857142857142f, 0.6964285714285714f,
                         0.19642857142857142f, 0.6964285714285714f,
                         0.23214285714285715f, 0.6964285714285714f,
                         0.23214285714285715f, 0.6964285714285714f,
                         0.26785714285714285f, 0.6964285714285714f,
                         0.26785714285714285f, 0.6964285714285714f,
                         0.30357142857142855f, 0.6964285714285714f,
                         0.30357142857142855f, 0.6964285714285714f,
                         0.3392857142857143f, 0.6964285714285714f,
                         0.3392857142857143f, 0.6964285714285714f,
                         0.375f, 0.6964285714285714f,
                         0.375f, 0.6964285714285714f,
                         0.4107142857142857f, 0.6964285714285714f,
                         0.4107142857142857f, 0.6964285714285714f,
                         0.44642857142857145f, 0.6964285714285714f,
                         0.44642857142857145f, 0.6964285714285714f,
                         0.48214285714285715f, 0.6964285714285714f,
                         0.48214285714285715f, 0.6964285714285714f,
                         0.5178571428571429f, 0.6964285714285714f,
                         0.5178571428571429f, 0.6964285714285714f,
                         0.5535714285714286f, 0.6964285714285714f,
                         0.5535714285714286f, 0.6964285714285714f,
                         0.5892857142857143f, 0.6964285714285714f,
                         0.5892857142857143f, 0.6964285714285714f,
                         0.625f, 0.6964285714285714f,
                         0.625f, 0.6964285714285714f,
                         0.6607142857142857f, 0.6964285714285714f,
                         0.6607142857142857f, 0.6964285714285714f,
                         0.6964285714285714f, 0.6964285714285714f,
                         0.6964285714285714f, 0.6964285714285714f,
                         0.7321428571428571f, 0.6964285714285714f,
                         0.7321428571428571f, 0.6964285714285714f,
                         0.7678571428571429f, 0.6964285714285714f,
                         0.7678571428571429f, 0.6964285714285714f,
                         0.8035714285714286f, 0.6964285714285714f,
                         0.8035714285714286f, 0.6964285714285714f,
                         0.8392857142857143f, 0.6964285714285714f,
                         0.8392857142857143f, 0.6964285714285714f,
                         0.875f, 0.6964285714285714f,
                         0.875f, 0.6964285714285714f,
                         0.9107142857142857f, 0.6964285714285714f,
                         0.9107142857142857f, 0.6964285714285714f,
                         0.9464285714285714f, 0.6964285714285714f,
                         0.9464285714285714f, 0.6964285714285714f,
                         0.9821428571428571f, 0.6964285714285714f,
                         0.9821428571428571f, 0.6964285714285714f,
                         0.017857142857142856f, 0.7321428571428571f,
                         0.017857142857142856f, 0.7321428571428571f,
                         0.05357142857142857f, 0.7321428571428571f,
                         0.05357142857142857f, 0.7321428571428571f,
                         0.08928571428571429f, 0.7321428571428571f,
                         0.08928571428571429f, 0.7321428571428571f,
                         0.125f, 0.7321428571428571f,
                         0.125f, 0.7321428571428571f,
                         0.16071428571428573f, 0.7321428571428571f,
                         0.16071428571428573f, 0.7321428571428571f,
                         0.19642857142857142f, 0.7321428571428571f,
                         0.19642857142857142f, 0.7321428571428571f,
                         0.23214285714285715f, 0.7321428571428571f,
                         0.23214285714285715f, 0.7321428571428571f,
                         0.26785714285714285f, 0.7321428571428571f,
                         0.26785714285714285f, 0.7321428571428571f,
                         0.30357142857142855f, 0.7321428571428571f,
                         0.30357142857142855f, 0.7321428571428571f,
                         0.3392857142857143f, 0.7321428571428571f,
                         0.3392857142857143f, 0.7321428571428571f,
                         0.375f, 0.7321428571428571f,
                         0.375f, 0.7321428571428571f,
                         0.4107142857142857f, 0.7321428571428571f,
                         0.4107142857142857f, 0.7321428571428571f,
                         0.44642857142857145f, 0.7321428571428571f,
                         0.44642857142857145f, 0.7321428571428571f,
                         0.48214285714285715f, 0.7321428571428571f,
                         0.48214285714285715f, 0.7321428571428571f,
                         0.5178571428571429f, 0.7321428571428571f,
                         0.5178571428571429f, 0.7321428571428571f,
                         0.5535714285714286f, 0.7321428571428571f,
                         0.5535714285714286f, 0.7321428571428571f,
                         0.5892857142857143f, 0.7321428571428571f,
                         0.5892857142857143f, 0.7321428571428571f,
                         0.625f, 0.7321428571428571f,
                         0.625f, 0.7321428571428571f,
                         0.6607142857142857f, 0.7321428571428571f,
                         0.6607142857142857f, 0.7321428571428571f,
                         0.6964285714285714f, 0.7321428571428571f,
                         0.6964285714285714f, 0.7321428571428571f,
                         0.7321428571428571f, 0.7321428571428571f,
                         0.7321428571428571f, 0.7321428571428571f,
                         0.7678571428571429f, 0.7321428571428571f,
                         0.7678571428571429f, 0.7321428571428571f,
                         0.8035714285714286f, 0.7321428571428571f,
                         0.8035714285714286f, 0.7321428571428571f,
                         0.8392857142857143f, 0.7321428571428571f,
                         0.8392857142857143f, 0.7321428571428571f,
                         0.875f, 0.7321428571428571f,
                         0.875f, 0.7321428571428571f,
                         0.9107142857142857f, 0.7321428571428571f,
                         0.9107142857142857f, 0.7321428571428571f,
                         0.9464285714285714f, 0.7321428571428571f,
                         0.9464285714285714f, 0.7321428571428571f,
                         0.9821428571428571f, 0.7321428571428571f,
                         0.9821428571428571f, 0.7321428571428571f,
                         0.017857142857142856f, 0.7678571428571429f,
                         0.017857142857142856f, 0.7678571428571429f,
                         0.05357142857142857f, 0.7678571428571429f,
                         0.05357142857142857f, 0.7678571428571429f,
                         0.08928571428571429f, 0.7678571428571429f,
                         0.08928571428571429f, 0.7678571428571429f,
                         0.125f, 0.7678571428571429f,
                         0.125f, 0.7678571428571429f,
                         0.16071428571428573f, 0.7678571428571429f,
                         0.16071428571428573f, 0.7678571428571429f,
                         0.19642857142857142f, 0.7678571428571429f,
                         0.19642857142857142f, 0.7678571428571429f,
                         0.23214285714285715f, 0.7678571428571429f,
                         0.23214285714285715f, 0.7678571428571429f,
                         0.26785714285714285f, 0.7678571428571429f,
                         0.26785714285714285f, 0.7678571428571429f,
                         0.30357142857142855f, 0.7678571428571429f,
                         0.30357142857142855f, 0.7678571428571429f,
                         0.3392857142857143f, 0.7678571428571429f,
                         0.3392857142857143f, 0.7678571428571429f,
                         0.375f, 0.7678571428571429f,
                         0.375f, 0.7678571428571429f,
                         0.4107142857142857f, 0.7678571428571429f,
                         0.4107142857142857f, 0.7678571428571429f,
                         0.44642857142857145f, 0.7678571428571429f,
                         0.44642857142857145f, 0.7678571428571429f,
                         0.48214285714285715f, 0.7678571428571429f,
                         0.48214285714285715f, 0.7678571428571429f,
                         0.5178571428571429f, 0.7678571428571429f,
                         0.5178571428571429f, 0.7678571428571429f,
                         0.5535714285714286f, 0.7678571428571429f,
                         0.5535714285714286f, 0.7678571428571429f,
                         0.5892857142857143f, 0.7678571428571429f,
                         0.5892857142857143f, 0.7678571428571429f,
                         0.625f, 0.7678571428571429f,
                         0.625f, 0.7678571428571429f,
                         0.6607142857142857f, 0.7678571428571429f,
                         0.6607142857142857f, 0.7678571428571429f,
                         0.6964285714285714f, 0.7678571428571429f,
                         0.6964285714285714f, 0.7678571428571429f,
                         0.7321428571428571f, 0.7678571428571429f,
                         0.7321428571428571f, 0.7678571428571429f,
                         0.7678571428571429f, 0.7678571428571429f,
                         0.7678571428571429f, 0.7678571428571429f,
                         0.8035714285714286f, 0.7678571428571429f,
                         0.8035714285714286f, 0.7678571428571429f,
                         0.8392857142857143f, 0.7678571428571429f,
                         0.8392857142857143f, 0.7678571428571429f,
                         0.875f, 0.7678571428571429f,
                         0.875f, 0.7678571428571429f,
                         0.9107142857142857f, 0.7678571428571429f,
                         0.9107142857142857f, 0.7678571428571429f,
                         0.9464285714285714f, 0.7678571428571429f,
                         0.9464285714285714f, 0.7678571428571429f,
                         0.9821428571428571f, 0.7678571428571429f,
                         0.9821428571428571f, 0.7678571428571429f,
                         0.017857142857142856f, 0.8035714285714286f,
                         0.017857142857142856f, 0.8035714285714286f,
                         0.05357142857142857f, 0.8035714285714286f,
                         0.05357142857142857f, 0.8035714285714286f,
                         0.08928571428571429f, 0.8035714285714286f,
                         0.08928571428571429f, 0.8035714285714286f,
                         0.125f, 0.8035714285714286f,
                         0.125f, 0.8035714285714286f,
                         0.16071428571428573f, 0.8035714285714286f,
                         0.16071428571428573f, 0.8035714285714286f,
                         0.19642857142857142f, 0.8035714285714286f,
                         0.19642857142857142f, 0.8035714285714286f,
                         0.23214285714285715f, 0.8035714285714286f,
                         0.23214285714285715f, 0.8035714285714286f,
                         0.26785714285714285f, 0.8035714285714286f,
                         0.26785714285714285f, 0.8035714285714286f,
                         0.30357142857142855f, 0.8035714285714286f,
                         0.30357142857142855f, 0.8035714285714286f,
                         0.3392857142857143f, 0.8035714285714286f,
                         0.3392857142857143f, 0.8035714285714286f,
                         0.375f, 0.8035714285714286f,
                         0.375f, 0.8035714285714286f,
                         0.4107142857142857f, 0.8035714285714286f,
                         0.4107142857142857f, 0.8035714285714286f,
                         0.44642857142857145f, 0.8035714285714286f,
                         0.44642857142857145f, 0.8035714285714286f,
                         0.48214285714285715f, 0.8035714285714286f,
                         0.48214285714285715f, 0.8035714285714286f,
                         0.5178571428571429f, 0.8035714285714286f,
                         0.5178571428571429f, 0.8035714285714286f,
                         0.5535714285714286f, 0.8035714285714286f,
                         0.5535714285714286f, 0.8035714285714286f,
                         0.5892857142857143f, 0.8035714285714286f,
                         0.5892857142857143f, 0.8035714285714286f,
                         0.625f, 0.8035714285714286f,
                         0.625f, 0.8035714285714286f,
                         0.6607142857142857f, 0.8035714285714286f,
                         0.6607142857142857f, 0.8035714285714286f,
                         0.6964285714285714f, 0.8035714285714286f,
                         0.6964285714285714f, 0.8035714285714286f,
                         0.7321428571428571f, 0.8035714285714286f,
                         0.7321428571428571f, 0.8035714285714286f,
                         0.7678571428571429f, 0.8035714285714286f,
                         0.7678571428571429f, 0.8035714285714286f,
                         0.8035714285714286f, 0.8035714285714286f,
                         0.8035714285714286f, 0.8035714285714286f,
                         0.8392857142857143f, 0.8035714285714286f,
                         0.8392857142857143f, 0.8035714285714286f,
                         0.875f, 0.8035714285714286f,
                         0.875f, 0.8035714285714286f,
                         0.9107142857142857f, 0.8035714285714286f,
                         0.9107142857142857f, 0.8035714285714286f,
                         0.9464285714285714f, 0.8035714285714286f,
                         0.9464285714285714f, 0.8035714285714286f,
                         0.9821428571428571f, 0.8035714285714286f,
                         0.9821428571428571f, 0.8035714285714286f,
                         0.017857142857142856f, 0.8392857142857143f,
                         0.017857142857142856f, 0.8392857142857143f,
                         0.05357142857142857f, 0.8392857142857143f,
                         0.05357142857142857f, 0.8392857142857143f,
                         0.08928571428571429f, 0.8392857142857143f,
                         0.08928571428571429f, 0.8392857142857143f,
                         0.125f, 0.8392857142857143f,
                         0.125f, 0.8392857142857143f,
                         0.16071428571428573f, 0.8392857142857143f,
                         0.16071428571428573f, 0.8392857142857143f,
                         0.19642857142857142f, 0.8392857142857143f,
                         0.19642857142857142f, 0.8392857142857143f,
                         0.23214285714285715f, 0.8392857142857143f,
                         0.23214285714285715f, 0.8392857142857143f,
                         0.26785714285714285f, 0.8392857142857143f,
                         0.26785714285714285f, 0.8392857142857143f,
                         0.30357142857142855f, 0.8392857142857143f,
                         0.30357142857142855f, 0.8392857142857143f,
                         0.3392857142857143f, 0.8392857142857143f,
                         0.3392857142857143f, 0.8392857142857143f,
                         0.375f, 0.8392857142857143f,
                         0.375f, 0.8392857142857143f,
                         0.4107142857142857f, 0.8392857142857143f,
                         0.4107142857142857f, 0.8392857142857143f,
                         0.44642857142857145f, 0.8392857142857143f,
                         0.44642857142857145f, 0.8392857142857143f,
                         0.48214285714285715f, 0.8392857142857143f,
                         0.48214285714285715f, 0.8392857142857143f,
                         0.5178571428571429f, 0.8392857142857143f,
                         0.5178571428571429f, 0.8392857142857143f,
                         0.5535714285714286f, 0.8392857142857143f,
                         0.5535714285714286f, 0.8392857142857143f,
                         0.5892857142857143f, 0.8392857142857143f,
                         0.5892857142857143f, 0.8392857142857143f,
                         0.625f, 0.8392857142857143f,
                         0.625f, 0.8392857142857143f,
                         0.6607142857142857f, 0.8392857142857143f,
                         0.6607142857142857f, 0.8392857142857143f,
                         0.6964285714285714f, 0.8392857142857143f,
                         0.6964285714285714f, 0.8392857142857143f,
                         0.7321428571428571f, 0.8392857142857143f,
                         0.7321428571428571f, 0.8392857142857143f,
                         0.7678571428571429f, 0.8392857142857143f,
                         0.7678571428571429f, 0.8392857142857143f,
                         0.8035714285714286f, 0.8392857142857143f,
                         0.8035714285714286f, 0.8392857142857143f,
                         0.8392857142857143f, 0.8392857142857143f,
                         0.8392857142857143f, 0.8392857142857143f,
                         0.875f, 0.8392857142857143f,
                         0.875f, 0.8392857142857143f,
                         0.9107142857142857f, 0.8392857142857143f,
                         0.9107142857142857f, 0.8392857142857143f,
                         0.9464285714285714f, 0.8392857142857143f,
                         0.9464285714285714f, 0.8392857142857143f,
                         0.9821428571428571f, 0.8392857142857143f,
                         0.9821428571428571f, 0.8392857142857143f,
                         0.017857142857142856f, 0.875f,
                         0.017857142857142856f, 0.875f,
                         0.05357142857142857f, 0.875f,
                         0.05357142857142857f, 0.875f,
                         0.08928571428571429f, 0.875f,
                         0.08928571428571429f, 0.875f,
                         0.125f, 0.875f,
                         0.125f, 0.875f,
                         0.16071428571428573f, 0.875f,
                         0.16071428571428573f, 0.875f,
                         0.19642857142857142f, 0.875f,
                         0.19642857142857142f, 0.875f,
                         0.23214285714285715f, 0.875f,
                         0.23214285714285715f, 0.875f,
                         0.26785714285714285f, 0.875f,
                         0.26785714285714285f, 0.875f,
                         0.30357142857142855f, 0.875f,
                         0.30357142857142855f, 0.875f,
                         0.3392857142857143f, 0.875f,
                         0.3392857142857143f, 0.875f,
                         0.375f, 0.875f,
                         0.375f, 0.875f,
                         0.4107142857142857f, 0.875f,
                         0.4107142857142857f, 0.875f,
                         0.44642857142857145f, 0.875f,
                         0.44642857142857145f, 0.875f,
                         0.48214285714285715f, 0.875f,
                         0.48214285714285715f, 0.875f,
                         0.5178571428571429f, 0.875f,
                         0.5178571428571429f, 0.875f,
                         0.5535714285714286f, 0.875f,
                         0.5535714285714286f, 0.875f,
                         0.5892857142857143f, 0.875f,
                         0.5892857142857143f, 0.875f,
                         0.625f, 0.875f,
                         0.625f, 0.875f,
                         0.6607142857142857f, 0.875f,
                         0.6607142857142857f, 0.875f,
                         0.6964285714285714f, 0.875f,
                         0.6964285714285714f, 0.875f,
                         0.7321428571428571f, 0.875f,
                         0.7321428571428571f, 0.875f,
                         0.7678571428571429f, 0.875f,
                         0.7678571428571429f, 0.875f,
                         0.8035714285714286f, 0.875f,
                         0.8035714285714286f, 0.875f,
                         0.8392857142857143f, 0.875f,
                         0.8392857142857143f, 0.875f,
                         0.875f, 0.875f,
                         0.875f, 0.875f,
                         0.9107142857142857f, 0.875f,
                         0.9107142857142857f, 0.875f,
                         0.9464285714285714f, 0.875f,
                         0.9464285714285714f, 0.875f,
                         0.9821428571428571f, 0.875f,
                         0.9821428571428571f, 0.875f,
                         0.017857142857142856f, 0.9107142857142857f,
                         0.017857142857142856f, 0.9107142857142857f,
                         0.05357142857142857f, 0.9107142857142857f,
                         0.05357142857142857f, 0.9107142857142857f,
                         0.08928571428571429f, 0.9107142857142857f,
                         0.08928571428571429f, 0.9107142857142857f,
                         0.125f, 0.9107142857142857f,
                         0.125f, 0.9107142857142857f,
                         0.16071428571428573f, 0.9107142857142857f,
                         0.16071428571428573f, 0.9107142857142857f,
                         0.19642857142857142f, 0.9107142857142857f,
                         0.19642857142857142f, 0.9107142857142857f,
                         0.23214285714285715f, 0.9107142857142857f,
                         0.23214285714285715f, 0.9107142857142857f,
                         0.26785714285714285f, 0.9107142857142857f,
                         0.26785714285714285f, 0.9107142857142857f,
                         0.30357142857142855f, 0.9107142857142857f,
                         0.30357142857142855f, 0.9107142857142857f,
                         0.3392857142857143f, 0.9107142857142857f,
                         0.3392857142857143f, 0.9107142857142857f,
                         0.375f, 0.9107142857142857f,
                         0.375f, 0.9107142857142857f,
                         0.4107142857142857f, 0.9107142857142857f,
                         0.4107142857142857f, 0.9107142857142857f,
                         0.44642857142857145f, 0.9107142857142857f,
                         0.44642857142857145f, 0.9107142857142857f,
                         0.48214285714285715f, 0.9107142857142857f,
                         0.48214285714285715f, 0.9107142857142857f,
                         0.5178571428571429f, 0.9107142857142857f,
                         0.5178571428571429f, 0.9107142857142857f,
                         0.5535714285714286f, 0.9107142857142857f,
                         0.5535714285714286f, 0.9107142857142857f,
                         0.5892857142857143f, 0.9107142857142857f,
                         0.5892857142857143f, 0.9107142857142857f,
                         0.625f, 0.9107142857142857f,
                         0.625f, 0.9107142857142857f,
                         0.6607142857142857f, 0.9107142857142857f,
                         0.6607142857142857f, 0.9107142857142857f,
                         0.6964285714285714f, 0.9107142857142857f,
                         0.6964285714285714f, 0.9107142857142857f,
                         0.7321428571428571f, 0.9107142857142857f,
                         0.7321428571428571f, 0.9107142857142857f,
                         0.7678571428571429f, 0.9107142857142857f,
                         0.7678571428571429f, 0.9107142857142857f,
                         0.8035714285714286f, 0.9107142857142857f,
                         0.8035714285714286f, 0.9107142857142857f,
                         0.8392857142857143f, 0.9107142857142857f,
                         0.8392857142857143f, 0.9107142857142857f,
                         0.875f, 0.9107142857142857f,
                         0.875f, 0.9107142857142857f,
                         0.9107142857142857f, 0.9107142857142857f,
                         0.9107142857142857f, 0.9107142857142857f,
                         0.9464285714285714f, 0.9107142857142857f,
                         0.9464285714285714f, 0.9107142857142857f,
                         0.9821428571428571f, 0.9107142857142857f,
                         0.9821428571428571f, 0.9107142857142857f,
                         0.017857142857142856f, 0.9464285714285714f,
                         0.017857142857142856f, 0.9464285714285714f,
                         0.05357142857142857f, 0.9464285714285714f,
                         0.05357142857142857f, 0.9464285714285714f,
                         0.08928571428571429f, 0.9464285714285714f,
                         0.08928571428571429f, 0.9464285714285714f,
                         0.125f, 0.9464285714285714f,
                         0.125f, 0.9464285714285714f,
                         0.16071428571428573f, 0.9464285714285714f,
                         0.16071428571428573f, 0.9464285714285714f,
                         0.19642857142857142f, 0.9464285714285714f,
                         0.19642857142857142f, 0.9464285714285714f,
                         0.23214285714285715f, 0.9464285714285714f,
                         0.23214285714285715f, 0.9464285714285714f,
                         0.26785714285714285f, 0.9464285714285714f,
                         0.26785714285714285f, 0.9464285714285714f,
                         0.30357142857142855f, 0.9464285714285714f,
                         0.30357142857142855f, 0.9464285714285714f,
                         0.3392857142857143f, 0.9464285714285714f,
                         0.3392857142857143f, 0.9464285714285714f,
                         0.375f, 0.9464285714285714f,
                         0.375f, 0.9464285714285714f,
                         0.4107142857142857f, 0.9464285714285714f,
                         0.4107142857142857f, 0.9464285714285714f,
                         0.44642857142857145f, 0.9464285714285714f,
                         0.44642857142857145f, 0.9464285714285714f,
                         0.48214285714285715f, 0.9464285714285714f,
                         0.48214285714285715f, 0.9464285714285714f,
                         0.5178571428571429f, 0.9464285714285714f,
                         0.5178571428571429f, 0.9464285714285714f,
                         0.5535714285714286f, 0.9464285714285714f,
                         0.5535714285714286f, 0.9464285714285714f,
                         0.5892857142857143f, 0.9464285714285714f,
                         0.5892857142857143f, 0.9464285714285714f,
                         0.625f, 0.9464285714285714f,
                         0.625f, 0.9464285714285714f,
                         0.6607142857142857f, 0.9464285714285714f,
                         0.6607142857142857f, 0.9464285714285714f,
                         0.6964285714285714f, 0.9464285714285714f,
                         0.6964285714285714f, 0.9464285714285714f,
                         0.7321428571428571f, 0.9464285714285714f,
                         0.7321428571428571f, 0.9464285714285714f,
                         0.7678571428571429f, 0.9464285714285714f,
                         0.7678571428571429f, 0.9464285714285714f,
                         0.8035714285714286f, 0.9464285714285714f,
                         0.8035714285714286f, 0.9464285714285714f,
                         0.8392857142857143f, 0.9464285714285714f,
                         0.8392857142857143f, 0.9464285714285714f,
                         0.875f, 0.9464285714285714f,
                         0.875f, 0.9464285714285714f,
                         0.9107142857142857f, 0.9464285714285714f,
                         0.9107142857142857f, 0.9464285714285714f,
                         0.9464285714285714f, 0.9464285714285714f,
                         0.9464285714285714f, 0.9464285714285714f,
                         0.9821428571428571f, 0.9464285714285714f,
                         0.9821428571428571f, 0.9464285714285714f,
                         0.017857142857142856f, 0.9821428571428571f,
                         0.017857142857142856f, 0.9821428571428571f,
                         0.05357142857142857f, 0.9821428571428571f,
                         0.05357142857142857f, 0.9821428571428571f,
                         0.08928571428571429f, 0.9821428571428571f,
                         0.08928571428571429f, 0.9821428571428571f,
                         0.125f, 0.9821428571428571f,
                         0.125f, 0.9821428571428571f,
                         0.16071428571428573f, 0.9821428571428571f,
                         0.16071428571428573f, 0.9821428571428571f,
                         0.19642857142857142f, 0.9821428571428571f,
                         0.19642857142857142f, 0.9821428571428571f,
                         0.23214285714285715f, 0.9821428571428571f,
                         0.23214285714285715f, 0.9821428571428571f,
                         0.26785714285714285f, 0.9821428571428571f,
                         0.26785714285714285f, 0.9821428571428571f,
                         0.30357142857142855f, 0.9821428571428571f,
                         0.30357142857142855f, 0.9821428571428571f,
                         0.3392857142857143f, 0.9821428571428571f,
                         0.3392857142857143f, 0.9821428571428571f,
                         0.375f, 0.9821428571428571f,
                         0.375f, 0.9821428571428571f,
                         0.4107142857142857f, 0.9821428571428571f,
                         0.4107142857142857f, 0.9821428571428571f,
                         0.44642857142857145f, 0.9821428571428571f,
                         0.44642857142857145f, 0.9821428571428571f,
                         0.48214285714285715f, 0.9821428571428571f,
                         0.48214285714285715f, 0.9821428571428571f,
                         0.5178571428571429f, 0.9821428571428571f,
                         0.5178571428571429f, 0.9821428571428571f,
                         0.5535714285714286f, 0.9821428571428571f,
                         0.5535714285714286f, 0.9821428571428571f,
                         0.5892857142857143f, 0.9821428571428571f,
                         0.5892857142857143f, 0.9821428571428571f,
                         0.625f, 0.9821428571428571f,
                         0.625f, 0.9821428571428571f,
                         0.6607142857142857f, 0.9821428571428571f,
                         0.6607142857142857f, 0.9821428571428571f,
                         0.6964285714285714f, 0.9821428571428571f,
                         0.6964285714285714f, 0.9821428571428571f,
                         0.7321428571428571f, 0.9821428571428571f,
                         0.7321428571428571f, 0.9821428571428571f,
                         0.7678571428571429f, 0.9821428571428571f,
                         0.7678571428571429f, 0.9821428571428571f,
                         0.8035714285714286f, 0.9821428571428571f,
                         0.8035714285714286f, 0.9821428571428571f,
                         0.8392857142857143f, 0.9821428571428571f,
                         0.8392857142857143f, 0.9821428571428571f,
                         0.875f, 0.9821428571428571f,
                         0.875f, 0.9821428571428571f,
                         0.9107142857142857f, 0.9821428571428571f,
                         0.9107142857142857f, 0.9821428571428571f,
                         0.9464285714285714f, 0.9821428571428571f,
                         0.9464285714285714f, 0.9821428571428571f,
                         0.9821428571428571f, 0.9821428571428571f,
                         0.9821428571428571f, 0.9821428571428571f,
                         0.03571428571428571f, 0.03571428571428571f,
                         0.03571428571428571f, 0.03571428571428571f,
                         0.10714285714285714f, 0.03571428571428571f,
                         0.10714285714285714f, 0.03571428571428571f,
                         0.17857142857142858f, 0.03571428571428571f,
                         0.17857142857142858f, 0.03571428571428571f,
                         0.25f, 0.03571428571428571f,
                         0.25f, 0.03571428571428571f,
                         0.32142857142857145f, 0.03571428571428571f,
                         0.32142857142857145f, 0.03571428571428571f,
                         0.39285714285714285f, 0.03571428571428571f,
                         0.39285714285714285f, 0.03571428571428571f,
                         0.4642857142857143f, 0.03571428571428571f,
                         0.4642857142857143f, 0.03571428571428571f,
                         0.5357142857142857f, 0.03571428571428571f,
                         0.5357142857142857f, 0.03571428571428571f,
                         0.6071428571428571f, 0.03571428571428571f,
                         0.6071428571428571f, 0.03571428571428571f,
                         0.6785714285714286f, 0.03571428571428571f,
                         0.6785714285714286f, 0.03571428571428571f,
                         0.75f, 0.03571428571428571f,
                         0.75f, 0.03571428571428571f,
                         0.8214285714285714f, 0.03571428571428571f,
                         0.8214285714285714f, 0.03571428571428571f,
                         0.8928571428571429f, 0.03571428571428571f,
                         0.8928571428571429f, 0.03571428571428571f,
                         0.9642857142857143f, 0.03571428571428571f,
                         0.9642857142857143f, 0.03571428571428571f,
                         0.03571428571428571f, 0.10714285714285714f,
                         0.03571428571428571f, 0.10714285714285714f,
                         0.10714285714285714f, 0.10714285714285714f,
                         0.10714285714285714f, 0.10714285714285714f,
                         0.17857142857142858f, 0.10714285714285714f,
                         0.17857142857142858f, 0.10714285714285714f,
                         0.25f, 0.10714285714285714f,
                         0.25f, 0.10714285714285714f,
                         0.32142857142857145f, 0.10714285714285714f,
                         0.32142857142857145f, 0.10714285714285714f,
                         0.39285714285714285f, 0.10714285714285714f,
                         0.39285714285714285f, 0.10714285714285714f,
                         0.4642857142857143f, 0.10714285714285714f,
                         0.4642857142857143f, 0.10714285714285714f,
                         0.5357142857142857f, 0.10714285714285714f,
                         0.5357142857142857f, 0.10714285714285714f,
                         0.6071428571428571f, 0.10714285714285714f,
                         0.6071428571428571f, 0.10714285714285714f,
                         0.6785714285714286f, 0.10714285714285714f,
                         0.6785714285714286f, 0.10714285714285714f,
                         0.75f, 0.10714285714285714f,
                         0.75f, 0.10714285714285714f,
                         0.8214285714285714f, 0.10714285714285714f,
                         0.8214285714285714f, 0.10714285714285714f,
                         0.8928571428571429f, 0.10714285714285714f,
                         0.8928571428571429f, 0.10714285714285714f,
                         0.9642857142857143f, 0.10714285714285714f,
                         0.9642857142857143f, 0.10714285714285714f,
                         0.03571428571428571f, 0.17857142857142858f,
                         0.03571428571428571f, 0.17857142857142858f,
                         0.10714285714285714f, 0.17857142857142858f,
                         0.10714285714285714f, 0.17857142857142858f,
                         0.17857142857142858f, 0.17857142857142858f,
                         0.17857142857142858f, 0.17857142857142858f,
                         0.25f, 0.17857142857142858f,
                         0.25f, 0.17857142857142858f,
                         0.32142857142857145f, 0.17857142857142858f,
                         0.32142857142857145f, 0.17857142857142858f,
                         0.39285714285714285f, 0.17857142857142858f,
                         0.39285714285714285f, 0.17857142857142858f,
                         0.4642857142857143f, 0.17857142857142858f,
                         0.4642857142857143f, 0.17857142857142858f,
                         0.5357142857142857f, 0.17857142857142858f,
                         0.5357142857142857f, 0.17857142857142858f,
                         0.6071428571428571f, 0.17857142857142858f,
                         0.6071428571428571f, 0.17857142857142858f,
                         0.6785714285714286f, 0.17857142857142858f,
                         0.6785714285714286f, 0.17857142857142858f,
                         0.75f, 0.17857142857142858f,
                         0.75f, 0.17857142857142858f,
                         0.8214285714285714f, 0.17857142857142858f,
                         0.8214285714285714f, 0.17857142857142858f,
                         0.8928571428571429f, 0.17857142857142858f,
                         0.8928571428571429f, 0.17857142857142858f,
                         0.9642857142857143f, 0.17857142857142858f,
                         0.9642857142857143f, 0.17857142857142858f,
                         0.03571428571428571f, 0.25f,
                         0.03571428571428571f, 0.25f,
                         0.10714285714285714f, 0.25f,
                         0.10714285714285714f, 0.25f,
                         0.17857142857142858f, 0.25f,
                         0.17857142857142858f, 0.25f,
                         0.25f, 0.25f,
                         0.25f, 0.25f,
                         0.32142857142857145f, 0.25f,
                         0.32142857142857145f, 0.25f,
                         0.39285714285714285f, 0.25f,
                         0.39285714285714285f, 0.25f,
                         0.4642857142857143f, 0.25f,
                         0.4642857142857143f, 0.25f,
                         0.5357142857142857f, 0.25f,
                         0.5357142857142857f, 0.25f,
                         0.6071428571428571f, 0.25f,
                         0.6071428571428571f, 0.25f,
                         0.6785714285714286f, 0.25f,
                         0.6785714285714286f, 0.25f,
                         0.75f, 0.25f,
                         0.75f, 0.25f,
                         0.8214285714285714f, 0.25f,
                         0.8214285714285714f, 0.25f,
                         0.8928571428571429f, 0.25f,
                         0.8928571428571429f, 0.25f,
                         0.9642857142857143f, 0.25f,
                         0.9642857142857143f, 0.25f,
                         0.03571428571428571f, 0.32142857142857145f,
                         0.03571428571428571f, 0.32142857142857145f,
                         0.10714285714285714f, 0.32142857142857145f,
                         0.10714285714285714f, 0.32142857142857145f,
                         0.17857142857142858f, 0.32142857142857145f,
                         0.17857142857142858f, 0.32142857142857145f,
                         0.25f, 0.32142857142857145f,
                         0.25f, 0.32142857142857145f,
                         0.32142857142857145f, 0.32142857142857145f,
                         0.32142857142857145f, 0.32142857142857145f,
                         0.39285714285714285f, 0.32142857142857145f,
                         0.39285714285714285f, 0.32142857142857145f,
                         0.4642857142857143f, 0.32142857142857145f,
                         0.4642857142857143f, 0.32142857142857145f,
                         0.5357142857142857f, 0.32142857142857145f,
                         0.5357142857142857f, 0.32142857142857145f,
                         0.6071428571428571f, 0.32142857142857145f,
                         0.6071428571428571f, 0.32142857142857145f,
                         0.6785714285714286f, 0.32142857142857145f,
                         0.6785714285714286f, 0.32142857142857145f,
                         0.75f, 0.32142857142857145f,
                         0.75f, 0.32142857142857145f,
                         0.8214285714285714f, 0.32142857142857145f,
                         0.8214285714285714f, 0.32142857142857145f,
                         0.8928571428571429f, 0.32142857142857145f,
                         0.8928571428571429f, 0.32142857142857145f,
                         0.9642857142857143f, 0.32142857142857145f,
                         0.9642857142857143f, 0.32142857142857145f,
                         0.03571428571428571f, 0.39285714285714285f,
                         0.03571428571428571f, 0.39285714285714285f,
                         0.10714285714285714f, 0.39285714285714285f,
                         0.10714285714285714f, 0.39285714285714285f,
                         0.17857142857142858f, 0.39285714285714285f,
                         0.17857142857142858f, 0.39285714285714285f,
                         0.25f, 0.39285714285714285f,
                         0.25f, 0.39285714285714285f,
                         0.32142857142857145f, 0.39285714285714285f,
                         0.32142857142857145f, 0.39285714285714285f,
                         0.39285714285714285f, 0.39285714285714285f,
                         0.39285714285714285f, 0.39285714285714285f,
                         0.4642857142857143f, 0.39285714285714285f,
                         0.4642857142857143f, 0.39285714285714285f,
                         0.5357142857142857f, 0.39285714285714285f,
                         0.5357142857142857f, 0.39285714285714285f,
                         0.6071428571428571f, 0.39285714285714285f,
                         0.6071428571428571f, 0.39285714285714285f,
                         0.6785714285714286f, 0.39285714285714285f,
                         0.6785714285714286f, 0.39285714285714285f,
                         0.75f, 0.39285714285714285f,
                         0.75f, 0.39285714285714285f,
                         0.8214285714285714f, 0.39285714285714285f,
                         0.8214285714285714f, 0.39285714285714285f,
                         0.8928571428571429f, 0.39285714285714285f,
                         0.8928571428571429f, 0.39285714285714285f,
                         0.9642857142857143f, 0.39285714285714285f,
                         0.9642857142857143f, 0.39285714285714285f,
                         0.03571428571428571f, 0.4642857142857143f,
                         0.03571428571428571f, 0.4642857142857143f,
                         0.10714285714285714f, 0.4642857142857143f,
                         0.10714285714285714f, 0.4642857142857143f,
                         0.17857142857142858f, 0.4642857142857143f,
                         0.17857142857142858f, 0.4642857142857143f,
                         0.25f, 0.4642857142857143f,
                         0.25f, 0.4642857142857143f,
                         0.32142857142857145f, 0.4642857142857143f,
                         0.32142857142857145f, 0.4642857142857143f,
                         0.39285714285714285f, 0.4642857142857143f,
                         0.39285714285714285f, 0.4642857142857143f,
                         0.4642857142857143f, 0.4642857142857143f,
                         0.4642857142857143f, 0.4642857142857143f,
                         0.5357142857142857f, 0.4642857142857143f,
                         0.5357142857142857f, 0.4642857142857143f,
                         0.6071428571428571f, 0.4642857142857143f,
                         0.6071428571428571f, 0.4642857142857143f,
                         0.6785714285714286f, 0.4642857142857143f,
                         0.6785714285714286f, 0.4642857142857143f,
                         0.75f, 0.4642857142857143f,
                         0.75f, 0.4642857142857143f,
                         0.8214285714285714f, 0.4642857142857143f,
                         0.8214285714285714f, 0.4642857142857143f,
                         0.8928571428571429f, 0.4642857142857143f,
                         0.8928571428571429f, 0.4642857142857143f,
                         0.9642857142857143f, 0.4642857142857143f,
                         0.9642857142857143f, 0.4642857142857143f,
                         0.03571428571428571f, 0.5357142857142857f,
                         0.03571428571428571f, 0.5357142857142857f,
                         0.10714285714285714f, 0.5357142857142857f,
                         0.10714285714285714f, 0.5357142857142857f,
                         0.17857142857142858f, 0.5357142857142857f,
                         0.17857142857142858f, 0.5357142857142857f,
                         0.25f, 0.5357142857142857f,
                         0.25f, 0.5357142857142857f,
                         0.32142857142857145f, 0.5357142857142857f,
                         0.32142857142857145f, 0.5357142857142857f,
                         0.39285714285714285f, 0.5357142857142857f,
                         0.39285714285714285f, 0.5357142857142857f,
                         0.4642857142857143f, 0.5357142857142857f,
                         0.4642857142857143f, 0.5357142857142857f,
                         0.5357142857142857f, 0.5357142857142857f,
                         0.5357142857142857f, 0.5357142857142857f,
                         0.6071428571428571f, 0.5357142857142857f,
                         0.6071428571428571f, 0.5357142857142857f,
                         0.6785714285714286f, 0.5357142857142857f,
                         0.6785714285714286f, 0.5357142857142857f,
                         0.75f, 0.5357142857142857f,
                         0.75f, 0.5357142857142857f,
                         0.8214285714285714f, 0.5357142857142857f,
                         0.8214285714285714f, 0.5357142857142857f,
                         0.8928571428571429f, 0.5357142857142857f,
                         0.8928571428571429f, 0.5357142857142857f,
                         0.9642857142857143f, 0.5357142857142857f,
                         0.9642857142857143f, 0.5357142857142857f,
                         0.03571428571428571f, 0.6071428571428571f,
                         0.03571428571428571f, 0.6071428571428571f,
                         0.10714285714285714f, 0.6071428571428571f,
                         0.10714285714285714f, 0.6071428571428571f,
                         0.17857142857142858f, 0.6071428571428571f,
                         0.17857142857142858f, 0.6071428571428571f,
                         0.25f, 0.6071428571428571f,
                         0.25f, 0.6071428571428571f,
                         0.32142857142857145f, 0.6071428571428571f,
                         0.32142857142857145f, 0.6071428571428571f,
                         0.39285714285714285f, 0.6071428571428571f,
                         0.39285714285714285f, 0.6071428571428571f,
                         0.4642857142857143f, 0.6071428571428571f,
                         0.4642857142857143f, 0.6071428571428571f,
                         0.5357142857142857f, 0.6071428571428571f,
                         0.5357142857142857f, 0.6071428571428571f,
                         0.6071428571428571f, 0.6071428571428571f,
                         0.6071428571428571f, 0.6071428571428571f,
                         0.6785714285714286f, 0.6071428571428571f,
                         0.6785714285714286f, 0.6071428571428571f,
                         0.75f, 0.6071428571428571f,
                         0.75f, 0.6071428571428571f,
                         0.8214285714285714f, 0.6071428571428571f,
                         0.8214285714285714f, 0.6071428571428571f,
                         0.8928571428571429f, 0.6071428571428571f,
                         0.8928571428571429f, 0.6071428571428571f,
                         0.9642857142857143f, 0.6071428571428571f,
                         0.9642857142857143f, 0.6071428571428571f,
                         0.03571428571428571f, 0.6785714285714286f,
                         0.03571428571428571f, 0.6785714285714286f,
                         0.10714285714285714f, 0.6785714285714286f,
                         0.10714285714285714f, 0.6785714285714286f,
                         0.17857142857142858f, 0.6785714285714286f,
                         0.17857142857142858f, 0.6785714285714286f,
                         0.25f, 0.6785714285714286f,
                         0.25f, 0.6785714285714286f,
                         0.32142857142857145f, 0.6785714285714286f,
                         0.32142857142857145f, 0.6785714285714286f,
                         0.39285714285714285f, 0.6785714285714286f,
                         0.39285714285714285f, 0.6785714285714286f,
                         0.4642857142857143f, 0.6785714285714286f,
                         0.4642857142857143f, 0.6785714285714286f,
                         0.5357142857142857f, 0.6785714285714286f,
                         0.5357142857142857f, 0.6785714285714286f,
                         0.6071428571428571f, 0.6785714285714286f,
                         0.6071428571428571f, 0.6785714285714286f,
                         0.6785714285714286f, 0.6785714285714286f,
                         0.6785714285714286f, 0.6785714285714286f,
                         0.75f, 0.6785714285714286f,
                         0.75f, 0.6785714285714286f,
                         0.8214285714285714f, 0.6785714285714286f,
                         0.8214285714285714f, 0.6785714285714286f,
                         0.8928571428571429f, 0.6785714285714286f,
                         0.8928571428571429f, 0.6785714285714286f,
                         0.9642857142857143f, 0.6785714285714286f,
                         0.9642857142857143f, 0.6785714285714286f,
                         0.03571428571428571f, 0.75f,
                         0.03571428571428571f, 0.75f,
                         0.10714285714285714f, 0.75f,
                         0.10714285714285714f, 0.75f,
                         0.17857142857142858f, 0.75f,
                         0.17857142857142858f, 0.75f,
                         0.25f, 0.75f,
                         0.25f, 0.75f,
                         0.32142857142857145f, 0.75f,
                         0.32142857142857145f, 0.75f,
                         0.39285714285714285f, 0.75f,
                         0.39285714285714285f, 0.75f,
                         0.4642857142857143f, 0.75f,
                         0.4642857142857143f, 0.75f,
                         0.5357142857142857f, 0.75f,
                         0.5357142857142857f, 0.75f,
                         0.6071428571428571f, 0.75f,
                         0.6071428571428571f, 0.75f,
                         0.6785714285714286f, 0.75f,
                         0.6785714285714286f, 0.75f,
                         0.75f, 0.75f,
                         0.75f, 0.75f,
                         0.8214285714285714f, 0.75f,
                         0.8214285714285714f, 0.75f,
                         0.8928571428571429f, 0.75f,
                         0.8928571428571429f, 0.75f,
                         0.9642857142857143f, 0.75f,
                         0.9642857142857143f, 0.75f,
                         0.03571428571428571f, 0.8214285714285714f,
                         0.03571428571428571f, 0.8214285714285714f,
                         0.10714285714285714f, 0.8214285714285714f,
                         0.10714285714285714f, 0.8214285714285714f,
                         0.17857142857142858f, 0.8214285714285714f,
                         0.17857142857142858f, 0.8214285714285714f,
                         0.25f, 0.8214285714285714f,
                         0.25f, 0.8214285714285714f,
                         0.32142857142857145f, 0.8214285714285714f,
                         0.32142857142857145f, 0.8214285714285714f,
                         0.39285714285714285f, 0.8214285714285714f,
                         0.39285714285714285f, 0.8214285714285714f,
                         0.4642857142857143f, 0.8214285714285714f,
                         0.4642857142857143f, 0.8214285714285714f,
                         0.5357142857142857f, 0.8214285714285714f,
                         0.5357142857142857f, 0.8214285714285714f,
                         0.6071428571428571f, 0.8214285714285714f,
                         0.6071428571428571f, 0.8214285714285714f,
                         0.6785714285714286f, 0.8214285714285714f,
                         0.6785714285714286f, 0.8214285714285714f,
                         0.75f, 0.8214285714285714f,
                         0.75f, 0.8214285714285714f,
                         0.8214285714285714f, 0.8214285714285714f,
                         0.8214285714285714f, 0.8214285714285714f,
                         0.8928571428571429f, 0.8214285714285714f,
                         0.8928571428571429f, 0.8214285714285714f,
                         0.9642857142857143f, 0.8214285714285714f,
                         0.9642857142857143f, 0.8214285714285714f,
                         0.03571428571428571f, 0.8928571428571429f,
                         0.03571428571428571f, 0.8928571428571429f,
                         0.10714285714285714f, 0.8928571428571429f,
                         0.10714285714285714f, 0.8928571428571429f,
                         0.17857142857142858f, 0.8928571428571429f,
                         0.17857142857142858f, 0.8928571428571429f,
                         0.25f, 0.8928571428571429f,
                         0.25f, 0.8928571428571429f,
                         0.32142857142857145f, 0.8928571428571429f,
                         0.32142857142857145f, 0.8928571428571429f,
                         0.39285714285714285f, 0.8928571428571429f,
                         0.39285714285714285f, 0.8928571428571429f,
                         0.4642857142857143f, 0.8928571428571429f,
                         0.4642857142857143f, 0.8928571428571429f,
                         0.5357142857142857f, 0.8928571428571429f,
                         0.5357142857142857f, 0.8928571428571429f,
                         0.6071428571428571f, 0.8928571428571429f,
                         0.6071428571428571f, 0.8928571428571429f,
                         0.6785714285714286f, 0.8928571428571429f,
                         0.6785714285714286f, 0.8928571428571429f,
                         0.75f, 0.8928571428571429f,
                         0.75f, 0.8928571428571429f,
                         0.8214285714285714f, 0.8928571428571429f,
                         0.8214285714285714f, 0.8928571428571429f,
                         0.8928571428571429f, 0.8928571428571429f,
                         0.8928571428571429f, 0.8928571428571429f,
                         0.9642857142857143f, 0.8928571428571429f,
                         0.9642857142857143f, 0.8928571428571429f,
                         0.03571428571428571f, 0.9642857142857143f,
                         0.03571428571428571f, 0.9642857142857143f,
                         0.10714285714285714f, 0.9642857142857143f,
                         0.10714285714285714f, 0.9642857142857143f,
                         0.17857142857142858f, 0.9642857142857143f,
                         0.17857142857142858f, 0.9642857142857143f,
                         0.25f, 0.9642857142857143f,
                         0.25f, 0.9642857142857143f,
                         0.32142857142857145f, 0.9642857142857143f,
                         0.32142857142857145f, 0.9642857142857143f,
                         0.39285714285714285f, 0.9642857142857143f,
                         0.39285714285714285f, 0.9642857142857143f,
                         0.4642857142857143f, 0.9642857142857143f,
                         0.4642857142857143f, 0.9642857142857143f,
                         0.5357142857142857f, 0.9642857142857143f,
                         0.5357142857142857f, 0.9642857142857143f,
                         0.6071428571428571f, 0.9642857142857143f,
                         0.6071428571428571f, 0.9642857142857143f,
                         0.6785714285714286f, 0.9642857142857143f,
                         0.6785714285714286f, 0.9642857142857143f,
                         0.75f, 0.9642857142857143f,
                         0.75f, 0.9642857142857143f,
                         0.8214285714285714f, 0.9642857142857143f,
                         0.8214285714285714f, 0.9642857142857143f,
                         0.8928571428571429f, 0.9642857142857143f,
                         0.8928571428571429f, 0.9642857142857143f,
                         0.9642857142857143f, 0.9642857142857143f,
                         0.9642857142857143f, 0.9642857142857143f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.07142857142857142f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.21428571428571427f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.35714285714285715f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.5f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.6428571428571429f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.7857142857142857f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.9285714285714286f, 0.07142857142857142f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.07142857142857142f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.21428571428571427f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.35714285714285715f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.5f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.6428571428571429f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.7857142857142857f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.9285714285714286f, 0.21428571428571427f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.07142857142857142f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.21428571428571427f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.35714285714285715f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.5f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.6428571428571429f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.7857142857142857f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.9285714285714286f, 0.35714285714285715f,
                         0.07142857142857142f, 0.5f,
                         0.07142857142857142f, 0.5f,
                         0.07142857142857142f, 0.5f,
                         0.07142857142857142f, 0.5f,
                         0.07142857142857142f, 0.5f,
                         0.07142857142857142f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.21428571428571427f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.35714285714285715f, 0.5f,
                         0.5f, 0.5f,
                         0.5f, 0.5f,
                         0.5f, 0.5f,
                         0.5f, 0.5f,
                         0.5f, 0.5f,
                         0.5f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.6428571428571429f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.7857142857142857f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.9285714285714286f, 0.5f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.07142857142857142f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.21428571428571427f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.35714285714285715f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.5f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.6428571428571429f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.7857142857142857f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.9285714285714286f, 0.6428571428571429f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.07142857142857142f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.21428571428571427f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.35714285714285715f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.5f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.6428571428571429f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.7857142857142857f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.9285714285714286f, 0.7857142857142857f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.07142857142857142f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.21428571428571427f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.35714285714285715f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.5f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.6428571428571429f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.7857142857142857f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f,
                         0.9285714285714286f, 0.9285714285714286f
                 };

            anchors.put(0, 0, anchors_arr);

            return anchors;
        }
    }
}
#endif
