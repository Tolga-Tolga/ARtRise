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
    /// Referring to https://github.com/opencv/opencv_zoo/tree/main/models/palm_detection_mediapipe
    ///
    /// [Tested Models]
    /// https://github.com/opencv/opencv_zoo/raw/6c68bc48c6f96042b29b3425174e431ccac38376/models/palm_detection_mediapipe/palm_detection_mediapipe_2023feb.onnx
    /// </summary>
    public class MediaPipePalmDetector : ProcessingWorkerBase
    {
        protected static readonly Size INPUT_SIZE = new Size(192, 192);
        protected static readonly Scalar SCALAR_RED = new Scalar(0, 0, 255, 255);
        protected static readonly Scalar SCALAR_GREEN = new Scalar(0, 255, 0, 255);
        protected static readonly Scalar SCALAR_BLUE = new Scalar(255, 0, 0, 255);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);

        protected float _nmsThreshold;
        protected float _scoreThreshold;
        protected int _topK;
        protected int _backend;
        protected int _target;
        protected Net _palmDetectionNet;
        protected List<string> _cachedUnconnectedOutLayersNames;
        protected Mat _anchors;
        protected Mat _anchors_Nx14;
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
        /// Initializes a new instance of the MediaPipePalmDetector class.
        /// </summary>
        /// <param name="modelFilepath">Path to the model file.</param>
        /// <param name="nmsThreshold">Non-maximum suppression threshold.</param>
        /// <param name="scoreThreshold">Confidence threshold for filtering detections.</param>
        /// <param name="topK">Maximum number of output detections.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipePalmDetector(string modelFilepath, float nmsThreshold = 0.3f, float scoreThreshold = 0.5f, int topK = 20,
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
                _palmDetectionNet = Dnn.readNet(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _palmDetectionNet.setPreferableBackend(_backend);
            _palmDetectionNet.setPreferableTarget(_target);
            _cachedUnconnectedOutLayersNames = _palmDetectionNet.getUnconnectedOutLayersNames();

            _output0Buffer = new Mat(_topK, 19, CvType.CV_32FC1);

            _anchors = LoadAnchors();
            _anchors_Nx14 = new Mat();
            Core.repeat(_anchors, 1, 7, _anchors_Nx14);
        }


        /// <summary>
        /// Visualizes palm detection result on the input image.
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
            if (result.cols() < 19)
                throw new ArgumentException("Invalid result matrix. It must have at least 19 columns.");

            StringBuilder sb = null;

            if (printResult)
                sb = new StringBuilder(256);

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            float[] score = new float[1];
            float[] palm_box = new float[4];
            float[] palm_landmarks = new float[14];
#endif

            for (int i = 0; i < result.rows(); ++i)
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ReadOnlySpan<float> score = result.AsSpan<float>().Slice(18, 1);
                ReadOnlySpan<float> palm_box = result.AsSpan<float>().Slice(0, 4);
                ReadOnlySpan<float> palm_landmarks = result.AsSpan<float>().Slice(4, 14);

#else
                result.get(i, 18, score);
                result.get(i, 0, palm_box);
                result.get(i, 4, palm_landmarks);
#endif

                // put score
                Imgproc.putText(image, score[0].ToString("F4"), (palm_box[0], palm_box[1] + 12), Imgproc.FONT_HERSHEY_DUPLEX, 0.5,
                                     (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple());

                // draw box
                Imgproc.rectangle(image, (palm_box[0], palm_box[1]), (palm_box[2], palm_box[3]), SCALAR_GREEN.ToValueTuple(), 2);

                // draw points
                for (int j = 0; j < palm_landmarks.Length; j += 2)
                {
                    Imgproc.circle(image, (palm_landmarks[j], palm_landmarks[j + 1]), 2,
                                     (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple(), 2);
                }

                if (printResult)
                {
                    sb.AppendFormat("-----------palm {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.AppendFormat("Score: {0:F4}", score[0]);
                    sb.AppendLine();
                    sb.AppendFormat("Palm Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", palm_box[0], palm_box[1], palm_box[2], palm_box[3]);
                    sb.AppendLine();
                    sb.Append("Palm Landmarks: ");
                    sb.Append("{");
                    for (int j = 0; j < palm_landmarks.Length; j++)
                    {
                        sb.AppendFormat("{0:F3}", palm_landmarks[j]);
                        if (j < palm_landmarks.Length - 1)
                            sb.Append(", ");
                    }
                    sb.Append("}");
                    sb.AppendLine();
                }
            }

            if (printResult)
                Debug.Log(sb.ToString() + sb.Length);
        }

        /// <summary>
        /// Detects palms in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for palm detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (19 columns per detection)
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
        /// Detects palms in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for palm detection that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing detection result (19 columns per detection)
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
            _palmDetectionNet.setInput(inputBlob);
            List<Mat> outputBlobs = new List<Mat>();
            try
            {
                _palmDetectionNet.forward(outputBlobs, _cachedUnconnectedOutLayersNames);
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
            int c = image.channels();
            int h = (int)INPUT_SIZE.height;
            int w = (int)INPUT_SIZE.width;

            // HWC to NHWC, BGR to RGB
            Mat blob = new Mat(new int[] { 1, h, w, c }, CvType.CV_32FC1);
            using (Mat blob_HxW = blob.reshape(c, new int[] { h, w })) // [h, w]
            {
                if (_inputSizeMat == null)
                    _inputSizeMat = new Mat((w, h), CvType.CV_8UC3); // [h, w]

                Imgproc.resize(_maxSizeImg, _inputSizeMat, (w, h));
                Imgproc.cvtColor(_inputSizeMat, _inputSizeMat, Imgproc.COLOR_BGR2RGB);

                _inputSizeMat.convertTo(blob_HxW, CvType.CV_32F, 1.0 / 255.0);
            }

            return blob; // [1, 192, 192, 3]
        }

        protected virtual Mat PostProcess(List<Mat> outputBlobs, (double width, double height) originalSize)
        {
            Mat outputBlob_0 = outputBlobs[0]; // 1*N*4+14
            Mat outputBlob_1 = outputBlobs[1]; // 1*N*1

            int num = outputBlob_0.size(1);
            Mat score = outputBlob_1.reshape(1, num);
            Mat boxAndLandmark = outputBlob_0.reshape(1, num);

            // Decode scores
            // score = 1 / (1 + np.exp(-score))
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
            using (Mat boxAndLandmark_landmark = boxAndLandmark.colRange((4, 18)))
            {
                Core.add(boxAndLandmark_landmark, _anchors_Nx14, boxAndLandmark_landmark);
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
                    result = CreateResultFromBuffer_Palm(score, boxAndLandmark, indices, _output0Buffer);

                    // Scale boxes
                    ScaleBoxesAndLandmarks_Palm(result, originalSize);
                }
            }

            score.Dispose();
            boxAndLandmark.Dispose();

            // [
            //   [bbox_coords, landmarks_coords, score]
            //   ...
            //   [bbox_coords, landmarks_coords, score]
            // ]
            return result;
        }

        protected virtual Mat CreateResultFromBuffer_Palm(Mat score_Nx1, Mat boxAndLandmark_Nx18, MatOfInt indices, Mat buffer)
        {
            if (!score_Nx1.isContinuous())
                throw new ArgumentException("score_Nx1 is not continuous.");
            if (!boxAndLandmark_Nx18.isContinuous())
                throw new ArgumentException("boxAndLandmark_Nx18 is not continuous.");
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
            ReadOnlySpan<float> allBoxAndLandmarks = boxAndLandmark_Nx18.AsSpan<float>();
            ReadOnlySpan<int> allIndices = indices.AsSpan<int>();
            Span<float> allResult = result.AsSpan<float>();
#else
            int requiredScoresLen = score_Nx1.rows();
            int requiredBoxAndLandmarksLen = boxAndLandmark_Nx18.rows() * 18;
            int requiredIndicesLen = buffer.rows();
            int requiredResultLen = buffer.rows() * 19;
            if (_allScoresBuffer == null || _allScoresBuffer.Length < requiredScoresLen)
                _allScoresBuffer = new float[requiredScoresLen];
            if (_allBoxAndLandmarksBuffer == null || _allBoxAndLandmarksBuffer.Length < requiredBoxAndLandmarksLen)
                _allBoxAndLandmarksBuffer = new float[requiredBoxAndLandmarksLen];
            if (_allIndicesBuffer == null || _allIndicesBuffer.Length < requiredIndicesLen)
                _allIndicesBuffer = new int[requiredIndicesLen];
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            score_Nx1.get(0, 0, _allScoresBuffer);
            boxAndLandmark_Nx18.get(0, 0, _allBoxAndLandmarksBuffer);
            indices.get(0, 0, _allIndicesBuffer);
            float[] allScores = _allScoresBuffer;
            float[] allBoxAndLandmarks = _allBoxAndLandmarksBuffer;
            int[] allIndices = _allIndicesBuffer;
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int idx = allIndices[i];
                int resultOffset = i * 19;
                int boxAndLandmarksOffset = idx * 18;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                allBoxAndLandmarks.Slice(boxAndLandmarksOffset, 18).CopyTo(allResult.Slice(resultOffset, 18));
                allResult[resultOffset + 18] = allScores[idx];
#else
                Buffer.BlockCopy(allBoxAndLandmarks, boxAndLandmarksOffset * 4, allResult, resultOffset * 4, 18 * 4);
                allResult[resultOffset + 18] = allScores[idx];
#endif
            }

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            result.put(0, 0, _allResultBuffer);
#endif

            return result;
        }

        protected virtual void ScaleBoxesAndLandmarks_Palm(Mat result, (double width, double height) originalSize)
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
            int requiredResultLen = num * 19;
            if (_allResultBuffer == null || _allResultBuffer.Length < requiredResultLen)
                _allResultBuffer = new float[requiredResultLen];

            result.get(0, 0, _allResultBuffer);
            float[] allResult = _allResultBuffer;
#endif

            for (int i = 0; i < num; ++i)
            {
                int resultOffset = i * 19;
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

                for (int j = 0; j < 14; ++j)
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

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _palmDetectionNet?.Dispose(); _palmDetectionNet = null;
                _anchors?.Dispose(); _anchors = null;
                _anchors_Nx14?.Dispose(); _anchors_Nx14 = null;
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
            Mat anchors = new Mat(2016, 2, CvType.CV_32FC1);

            float[] anchors_arr = new float[] {
                  0.02083333f, 0.02083333f,
                      0.02083333f, 0.02083333f,
                      0.0625f, 0.02083333f,
                      0.0625f, 0.02083333f,
                      0.10416666f, 0.02083333f,
                      0.10416666f, 0.02083333f,
                      0.14583333f, 0.02083333f,
                      0.14583333f, 0.02083333f,
                      0.1875f, 0.02083333f,
                      0.1875f, 0.02083333f,
                      0.22916667f, 0.02083333f,
                      0.22916667f, 0.02083333f,
                      0.27083334f, 0.02083333f,
                      0.27083334f, 0.02083333f,
                      0.3125f, 0.02083333f,
                      0.3125f, 0.02083333f,
                      0.35416666f, 0.02083333f,
                      0.35416666f, 0.02083333f,
                      0.39583334f, 0.02083333f,
                      0.39583334f, 0.02083333f,
                      0.4375f, 0.02083333f,
                      0.4375f, 0.02083333f,
                      0.47916666f, 0.02083333f,
                      0.47916666f, 0.02083333f,
                      0.5208333f, 0.02083333f,
                      0.5208333f, 0.02083333f,
                      0.5625f, 0.02083333f,
                      0.5625f, 0.02083333f,
                      0.6041667f, 0.02083333f,
                      0.6041667f, 0.02083333f,
                      0.6458333f, 0.02083333f,
                      0.6458333f, 0.02083333f,
                      0.6875f, 0.02083333f,
                      0.6875f, 0.02083333f,
                      0.7291667f, 0.02083333f,
                      0.7291667f, 0.02083333f,
                      0.7708333f, 0.02083333f,
                      0.7708333f, 0.02083333f,
                      0.8125f, 0.02083333f,
                      0.8125f, 0.02083333f,
                      0.8541667f, 0.02083333f,
                      0.8541667f, 0.02083333f,
                      0.8958333f, 0.02083333f,
                      0.8958333f, 0.02083333f,
                      0.9375f, 0.02083333f,
                      0.9375f, 0.02083333f,
                      0.9791667f, 0.02083333f,
                      0.9791667f, 0.02083333f,
                      0.02083333f, 0.0625f,
                      0.02083333f, 0.0625f,
                      0.0625f, 0.0625f,
                      0.0625f, 0.0625f,
                      0.10416666f, 0.0625f,
                      0.10416666f, 0.0625f,
                      0.14583333f, 0.0625f,
                      0.14583333f, 0.0625f,
                      0.1875f, 0.0625f,
                      0.1875f, 0.0625f,
                      0.22916667f, 0.0625f,
                      0.22916667f, 0.0625f,
                      0.27083334f, 0.0625f,
                      0.27083334f, 0.0625f,
                      0.3125f, 0.0625f,
                      0.3125f, 0.0625f,
                      0.35416666f, 0.0625f,
                      0.35416666f, 0.0625f,
                      0.39583334f, 0.0625f,
                      0.39583334f, 0.0625f,
                      0.4375f, 0.0625f,
                      0.4375f, 0.0625f,
                      0.47916666f, 0.0625f,
                      0.47916666f, 0.0625f,
                      0.5208333f, 0.0625f,
                      0.5208333f, 0.0625f,
                      0.5625f, 0.0625f,
                      0.5625f, 0.0625f,
                      0.6041667f, 0.0625f,
                      0.6041667f, 0.0625f,
                      0.6458333f, 0.0625f,
                      0.6458333f, 0.0625f,
                      0.6875f, 0.0625f,
                      0.6875f, 0.0625f,
                      0.7291667f, 0.0625f,
                      0.7291667f, 0.0625f,
                      0.7708333f, 0.0625f,
                      0.7708333f, 0.0625f,
                      0.8125f, 0.0625f,
                      0.8125f, 0.0625f,
                      0.8541667f, 0.0625f,
                      0.8541667f, 0.0625f,
                      0.8958333f, 0.0625f,
                      0.8958333f, 0.0625f,
                      0.9375f, 0.0625f,
                      0.9375f, 0.0625f,
                      0.9791667f, 0.0625f,
                      0.9791667f, 0.0625f,
                      0.02083333f, 0.10416666f,
                      0.02083333f, 0.10416666f,
                      0.0625f, 0.10416666f,
                      0.0625f, 0.10416666f,
                      0.10416666f, 0.10416666f,
                      0.10416666f, 0.10416666f,
                      0.14583333f, 0.10416666f,
                      0.14583333f, 0.10416666f,
                      0.1875f, 0.10416666f,
                      0.1875f, 0.10416666f,
                      0.22916667f, 0.10416666f,
                      0.22916667f, 0.10416666f,
                      0.27083334f, 0.10416666f,
                      0.27083334f, 0.10416666f,
                      0.3125f, 0.10416666f,
                      0.3125f, 0.10416666f,
                      0.35416666f, 0.10416666f,
                      0.35416666f, 0.10416666f,
                      0.39583334f, 0.10416666f,
                      0.39583334f, 0.10416666f,
                      0.4375f, 0.10416666f,
                      0.4375f, 0.10416666f,
                      0.47916666f, 0.10416666f,
                      0.47916666f, 0.10416666f,
                      0.5208333f, 0.10416666f,
                      0.5208333f, 0.10416666f,
                      0.5625f, 0.10416666f,
                      0.5625f, 0.10416666f,
                      0.6041667f, 0.10416666f,
                      0.6041667f, 0.10416666f,
                      0.6458333f, 0.10416666f,
                      0.6458333f, 0.10416666f,
                      0.6875f, 0.10416666f,
                      0.6875f, 0.10416666f,
                      0.7291667f, 0.10416666f,
                      0.7291667f, 0.10416666f,
                      0.7708333f, 0.10416666f,
                      0.7708333f, 0.10416666f,
                      0.8125f, 0.10416666f,
                      0.8125f, 0.10416666f,
                      0.8541667f, 0.10416666f,
                      0.8541667f, 0.10416666f,
                      0.8958333f, 0.10416666f,
                      0.8958333f, 0.10416666f,
                      0.9375f, 0.10416666f,
                      0.9375f, 0.10416666f,
                      0.9791667f, 0.10416666f,
                      0.9791667f, 0.10416666f,
                      0.02083333f, 0.14583333f,
                      0.02083333f, 0.14583333f,
                      0.0625f, 0.14583333f,
                      0.0625f, 0.14583333f,
                      0.10416666f, 0.14583333f,
                      0.10416666f, 0.14583333f,
                      0.14583333f, 0.14583333f,
                      0.14583333f, 0.14583333f,
                      0.1875f, 0.14583333f,
                      0.1875f, 0.14583333f,
                      0.22916667f, 0.14583333f,
                      0.22916667f, 0.14583333f,
                      0.27083334f, 0.14583333f,
                      0.27083334f, 0.14583333f,
                      0.3125f, 0.14583333f,
                      0.3125f, 0.14583333f,
                      0.35416666f, 0.14583333f,
                      0.35416666f, 0.14583333f,
                      0.39583334f, 0.14583333f,
                      0.39583334f, 0.14583333f,
                      0.4375f, 0.14583333f,
                      0.4375f, 0.14583333f,
                      0.47916666f, 0.14583333f,
                      0.47916666f, 0.14583333f,
                      0.5208333f, 0.14583333f,
                      0.5208333f, 0.14583333f,
                      0.5625f, 0.14583333f,
                      0.5625f, 0.14583333f,
                      0.6041667f, 0.14583333f,
                      0.6041667f, 0.14583333f,
                      0.6458333f, 0.14583333f,
                      0.6458333f, 0.14583333f,
                      0.6875f, 0.14583333f,
                      0.6875f, 0.14583333f,
                      0.7291667f, 0.14583333f,
                      0.7291667f, 0.14583333f,
                      0.7708333f, 0.14583333f,
                      0.7708333f, 0.14583333f,
                      0.8125f, 0.14583333f,
                      0.8125f, 0.14583333f,
                      0.8541667f, 0.14583333f,
                      0.8541667f, 0.14583333f,
                      0.8958333f, 0.14583333f,
                      0.8958333f, 0.14583333f,
                      0.9375f, 0.14583333f,
                      0.9375f, 0.14583333f,
                      0.9791667f, 0.14583333f,
                      0.9791667f, 0.14583333f,
                      0.02083333f, 0.1875f,
                      0.02083333f, 0.1875f,
                      0.0625f, 0.1875f,
                      0.0625f, 0.1875f,
                      0.10416666f, 0.1875f,
                      0.10416666f, 0.1875f,
                      0.14583333f, 0.1875f,
                      0.14583333f, 0.1875f,
                      0.1875f, 0.1875f,
                      0.1875f, 0.1875f,
                      0.22916667f, 0.1875f,
                      0.22916667f, 0.1875f,
                      0.27083334f, 0.1875f,
                      0.27083334f, 0.1875f,
                      0.3125f, 0.1875f,
                      0.3125f, 0.1875f,
                      0.35416666f, 0.1875f,
                      0.35416666f, 0.1875f,
                      0.39583334f, 0.1875f,
                      0.39583334f, 0.1875f,
                      0.4375f, 0.1875f,
                      0.4375f, 0.1875f,
                      0.47916666f, 0.1875f,
                      0.47916666f, 0.1875f,
                      0.5208333f, 0.1875f,
                      0.5208333f, 0.1875f,
                      0.5625f, 0.1875f,
                      0.5625f, 0.1875f,
                      0.6041667f, 0.1875f,
                      0.6041667f, 0.1875f,
                      0.6458333f, 0.1875f,
                      0.6458333f, 0.1875f,
                      0.6875f, 0.1875f,
                      0.6875f, 0.1875f,
                      0.7291667f, 0.1875f,
                      0.7291667f, 0.1875f,
                      0.7708333f, 0.1875f,
                      0.7708333f, 0.1875f,
                      0.8125f, 0.1875f,
                      0.8125f, 0.1875f,
                      0.8541667f, 0.1875f,
                      0.8541667f, 0.1875f,
                      0.8958333f, 0.1875f,
                      0.8958333f, 0.1875f,
                      0.9375f, 0.1875f,
                      0.9375f, 0.1875f,
                      0.9791667f, 0.1875f,
                      0.9791667f, 0.1875f,
                      0.02083333f, 0.22916667f,
                      0.02083333f, 0.22916667f,
                      0.0625f, 0.22916667f,
                      0.0625f, 0.22916667f,
                      0.10416666f, 0.22916667f,
                      0.10416666f, 0.22916667f,
                      0.14583333f, 0.22916667f,
                      0.14583333f, 0.22916667f,
                      0.1875f, 0.22916667f,
                      0.1875f, 0.22916667f,
                      0.22916667f, 0.22916667f,
                      0.22916667f, 0.22916667f,
                      0.27083334f, 0.22916667f,
                      0.27083334f, 0.22916667f,
                      0.3125f, 0.22916667f,
                      0.3125f, 0.22916667f,
                      0.35416666f, 0.22916667f,
                      0.35416666f, 0.22916667f,
                      0.39583334f, 0.22916667f,
                      0.39583334f, 0.22916667f,
                      0.4375f, 0.22916667f,
                      0.4375f, 0.22916667f,
                      0.47916666f, 0.22916667f,
                      0.47916666f, 0.22916667f,
                      0.5208333f, 0.22916667f,
                      0.5208333f, 0.22916667f,
                      0.5625f, 0.22916667f,
                      0.5625f, 0.22916667f,
                      0.6041667f, 0.22916667f,
                      0.6041667f, 0.22916667f,
                      0.6458333f, 0.22916667f,
                      0.6458333f, 0.22916667f,
                      0.6875f, 0.22916667f,
                      0.6875f, 0.22916667f,
                      0.7291667f, 0.22916667f,
                      0.7291667f, 0.22916667f,
                      0.7708333f, 0.22916667f,
                      0.7708333f, 0.22916667f,
                      0.8125f, 0.22916667f,
                      0.8125f, 0.22916667f,
                      0.8541667f, 0.22916667f,
                      0.8541667f, 0.22916667f,
                      0.8958333f, 0.22916667f,
                      0.8958333f, 0.22916667f,
                      0.9375f, 0.22916667f,
                      0.9375f, 0.22916667f,
                      0.9791667f, 0.22916667f,
                      0.9791667f, 0.22916667f,
                      0.02083333f, 0.27083334f,
                      0.02083333f, 0.27083334f,
                      0.0625f, 0.27083334f,
                      0.0625f, 0.27083334f,
                      0.10416666f, 0.27083334f,
                      0.10416666f, 0.27083334f,
                      0.14583333f, 0.27083334f,
                      0.14583333f, 0.27083334f,
                      0.1875f, 0.27083334f,
                      0.1875f, 0.27083334f,
                      0.22916667f, 0.27083334f,
                      0.22916667f, 0.27083334f,
                      0.27083334f, 0.27083334f,
                      0.27083334f, 0.27083334f,
                      0.3125f, 0.27083334f,
                      0.3125f, 0.27083334f,
                      0.35416666f, 0.27083334f,
                      0.35416666f, 0.27083334f,
                      0.39583334f, 0.27083334f,
                      0.39583334f, 0.27083334f,
                      0.4375f, 0.27083334f,
                      0.4375f, 0.27083334f,
                      0.47916666f, 0.27083334f,
                      0.47916666f, 0.27083334f,
                      0.5208333f, 0.27083334f,
                      0.5208333f, 0.27083334f,
                      0.5625f, 0.27083334f,
                      0.5625f, 0.27083334f,
                      0.6041667f, 0.27083334f,
                      0.6041667f, 0.27083334f,
                      0.6458333f, 0.27083334f,
                      0.6458333f, 0.27083334f,
                      0.6875f, 0.27083334f,
                      0.6875f, 0.27083334f,
                      0.7291667f, 0.27083334f,
                      0.7291667f, 0.27083334f,
                      0.7708333f, 0.27083334f,
                      0.7708333f, 0.27083334f,
                      0.8125f, 0.27083334f,
                      0.8125f, 0.27083334f,
                      0.8541667f, 0.27083334f,
                      0.8541667f, 0.27083334f,
                      0.8958333f, 0.27083334f,
                      0.8958333f, 0.27083334f,
                      0.9375f, 0.27083334f,
                      0.9375f, 0.27083334f,
                      0.9791667f, 0.27083334f,
                      0.9791667f, 0.27083334f,
                      0.02083333f, 0.3125f,
                      0.02083333f, 0.3125f,
                      0.0625f, 0.3125f,
                      0.0625f, 0.3125f,
                      0.10416666f, 0.3125f,
                      0.10416666f, 0.3125f,
                      0.14583333f, 0.3125f,
                      0.14583333f, 0.3125f,
                      0.1875f, 0.3125f,
                      0.1875f, 0.3125f,
                      0.22916667f, 0.3125f,
                      0.22916667f, 0.3125f,
                      0.27083334f, 0.3125f,
                      0.27083334f, 0.3125f,
                      0.3125f, 0.3125f,
                      0.3125f, 0.3125f,
                      0.35416666f, 0.3125f,
                      0.35416666f, 0.3125f,
                      0.39583334f, 0.3125f,
                      0.39583334f, 0.3125f,
                      0.4375f, 0.3125f,
                      0.4375f, 0.3125f,
                      0.47916666f, 0.3125f,
                      0.47916666f, 0.3125f,
                      0.5208333f, 0.3125f,
                      0.5208333f, 0.3125f,
                      0.5625f, 0.3125f,
                      0.5625f, 0.3125f,
                      0.6041667f, 0.3125f,
                      0.6041667f, 0.3125f,
                      0.6458333f, 0.3125f,
                      0.6458333f, 0.3125f,
                      0.6875f, 0.3125f,
                      0.6875f, 0.3125f,
                      0.7291667f, 0.3125f,
                      0.7291667f, 0.3125f,
                      0.7708333f, 0.3125f,
                      0.7708333f, 0.3125f,
                      0.8125f, 0.3125f,
                      0.8125f, 0.3125f,
                      0.8541667f, 0.3125f,
                      0.8541667f, 0.3125f,
                      0.8958333f, 0.3125f,
                      0.8958333f, 0.3125f,
                      0.9375f, 0.3125f,
                      0.9375f, 0.3125f,
                      0.9791667f, 0.3125f,
                      0.9791667f, 0.3125f,
                      0.02083333f, 0.35416666f,
                      0.02083333f, 0.35416666f,
                      0.0625f, 0.35416666f,
                      0.0625f, 0.35416666f,
                      0.10416666f, 0.35416666f,
                      0.10416666f, 0.35416666f,
                      0.14583333f, 0.35416666f,
                      0.14583333f, 0.35416666f,
                      0.1875f, 0.35416666f,
                      0.1875f, 0.35416666f,
                      0.22916667f, 0.35416666f,
                      0.22916667f, 0.35416666f,
                      0.27083334f, 0.35416666f,
                      0.27083334f, 0.35416666f,
                      0.3125f, 0.35416666f,
                      0.3125f, 0.35416666f,
                      0.35416666f, 0.35416666f,
                      0.35416666f, 0.35416666f,
                      0.39583334f, 0.35416666f,
                      0.39583334f, 0.35416666f,
                      0.4375f, 0.35416666f,
                      0.4375f, 0.35416666f,
                      0.47916666f, 0.35416666f,
                      0.47916666f, 0.35416666f,
                      0.5208333f, 0.35416666f,
                      0.5208333f, 0.35416666f,
                      0.5625f, 0.35416666f,
                      0.5625f, 0.35416666f,
                      0.6041667f, 0.35416666f,
                      0.6041667f, 0.35416666f,
                      0.6458333f, 0.35416666f,
                      0.6458333f, 0.35416666f,
                      0.6875f, 0.35416666f,
                      0.6875f, 0.35416666f,
                      0.7291667f, 0.35416666f,
                      0.7291667f, 0.35416666f,
                      0.7708333f, 0.35416666f,
                      0.7708333f, 0.35416666f,
                      0.8125f, 0.35416666f,
                      0.8125f, 0.35416666f,
                      0.8541667f, 0.35416666f,
                      0.8541667f, 0.35416666f,
                      0.8958333f, 0.35416666f,
                      0.8958333f, 0.35416666f,
                      0.9375f, 0.35416666f,
                      0.9375f, 0.35416666f,
                      0.9791667f, 0.35416666f,
                      0.9791667f, 0.35416666f,
                      0.02083333f, 0.39583334f,
                      0.02083333f, 0.39583334f,
                      0.0625f, 0.39583334f,
                      0.0625f, 0.39583334f,
                      0.10416666f, 0.39583334f,
                      0.10416666f, 0.39583334f,
                      0.14583333f, 0.39583334f,
                      0.14583333f, 0.39583334f,
                      0.1875f, 0.39583334f,
                      0.1875f, 0.39583334f,
                      0.22916667f, 0.39583334f,
                      0.22916667f, 0.39583334f,
                      0.27083334f, 0.39583334f,
                      0.27083334f, 0.39583334f,
                      0.3125f, 0.39583334f,
                      0.3125f, 0.39583334f,
                      0.35416666f, 0.39583334f,
                      0.35416666f, 0.39583334f,
                      0.39583334f, 0.39583334f,
                      0.39583334f, 0.39583334f,
                      0.4375f, 0.39583334f,
                      0.4375f, 0.39583334f,
                      0.47916666f, 0.39583334f,
                      0.47916666f, 0.39583334f,
                      0.5208333f, 0.39583334f,
                      0.5208333f, 0.39583334f,
                      0.5625f, 0.39583334f,
                      0.5625f, 0.39583334f,
                      0.6041667f, 0.39583334f,
                      0.6041667f, 0.39583334f,
                      0.6458333f, 0.39583334f,
                      0.6458333f, 0.39583334f,
                      0.6875f, 0.39583334f,
                      0.6875f, 0.39583334f,
                      0.7291667f, 0.39583334f,
                      0.7291667f, 0.39583334f,
                      0.7708333f, 0.39583334f,
                      0.7708333f, 0.39583334f,
                      0.8125f, 0.39583334f,
                      0.8125f, 0.39583334f,
                      0.8541667f, 0.39583334f,
                      0.8541667f, 0.39583334f,
                      0.8958333f, 0.39583334f,
                      0.8958333f, 0.39583334f,
                      0.9375f, 0.39583334f,
                      0.9375f, 0.39583334f,
                      0.9791667f, 0.39583334f,
                      0.9791667f, 0.39583334f,
                      0.02083333f, 0.4375f,
                      0.02083333f, 0.4375f,
                      0.0625f, 0.4375f,
                      0.0625f, 0.4375f,
                      0.10416666f, 0.4375f,
                      0.10416666f, 0.4375f,
                      0.14583333f, 0.4375f,
                      0.14583333f, 0.4375f,
                      0.1875f, 0.4375f,
                      0.1875f, 0.4375f,
                      0.22916667f, 0.4375f,
                      0.22916667f, 0.4375f,
                      0.27083334f, 0.4375f,
                      0.27083334f, 0.4375f,
                      0.3125f, 0.4375f,
                      0.3125f, 0.4375f,
                      0.35416666f, 0.4375f,
                      0.35416666f, 0.4375f,
                      0.39583334f, 0.4375f,
                      0.39583334f, 0.4375f,
                      0.4375f, 0.4375f,
                      0.4375f, 0.4375f,
                      0.47916666f, 0.4375f,
                      0.47916666f, 0.4375f,
                      0.5208333f, 0.4375f,
                      0.5208333f, 0.4375f,
                      0.5625f, 0.4375f,
                      0.5625f, 0.4375f,
                      0.6041667f, 0.4375f,
                      0.6041667f, 0.4375f,
                      0.6458333f, 0.4375f,
                      0.6458333f, 0.4375f,
                      0.6875f, 0.4375f,
                      0.6875f, 0.4375f,
                      0.7291667f, 0.4375f,
                      0.7291667f, 0.4375f,
                      0.7708333f, 0.4375f,
                      0.7708333f, 0.4375f,
                      0.8125f, 0.4375f,
                      0.8125f, 0.4375f,
                      0.8541667f, 0.4375f,
                      0.8541667f, 0.4375f,
                      0.8958333f, 0.4375f,
                      0.8958333f, 0.4375f,
                      0.9375f, 0.4375f,
                      0.9375f, 0.4375f,
                      0.9791667f, 0.4375f,
                      0.9791667f, 0.4375f,
                      0.02083333f, 0.47916666f,
                      0.02083333f, 0.47916666f,
                      0.0625f, 0.47916666f,
                      0.0625f, 0.47916666f,
                      0.10416666f, 0.47916666f,
                      0.10416666f, 0.47916666f,
                      0.14583333f, 0.47916666f,
                      0.14583333f, 0.47916666f,
                      0.1875f, 0.47916666f,
                      0.1875f, 0.47916666f,
                      0.22916667f, 0.47916666f,
                      0.22916667f, 0.47916666f,
                      0.27083334f, 0.47916666f,
                      0.27083334f, 0.47916666f,
                      0.3125f, 0.47916666f,
                      0.3125f, 0.47916666f,
                      0.35416666f, 0.47916666f,
                      0.35416666f, 0.47916666f,
                      0.39583334f, 0.47916666f,
                      0.39583334f, 0.47916666f,
                      0.4375f, 0.47916666f,
                      0.4375f, 0.47916666f,
                      0.47916666f, 0.47916666f,
                      0.47916666f, 0.47916666f,
                      0.5208333f, 0.47916666f,
                      0.5208333f, 0.47916666f,
                      0.5625f, 0.47916666f,
                      0.5625f, 0.47916666f,
                      0.6041667f, 0.47916666f,
                      0.6041667f, 0.47916666f,
                      0.6458333f, 0.47916666f,
                      0.6458333f, 0.47916666f,
                      0.6875f, 0.47916666f,
                      0.6875f, 0.47916666f,
                      0.7291667f, 0.47916666f,
                      0.7291667f, 0.47916666f,
                      0.7708333f, 0.47916666f,
                      0.7708333f, 0.47916666f,
                      0.8125f, 0.47916666f,
                      0.8125f, 0.47916666f,
                      0.8541667f, 0.47916666f,
                      0.8541667f, 0.47916666f,
                      0.8958333f, 0.47916666f,
                      0.8958333f, 0.47916666f,
                      0.9375f, 0.47916666f,
                      0.9375f, 0.47916666f,
                      0.9791667f, 0.47916666f,
                      0.9791667f, 0.47916666f,
                      0.02083333f, 0.5208333f,
                      0.02083333f, 0.5208333f,
                      0.0625f, 0.5208333f,
                      0.0625f, 0.5208333f,
                      0.10416666f, 0.5208333f,
                      0.10416666f, 0.5208333f,
                      0.14583333f, 0.5208333f,
                      0.14583333f, 0.5208333f,
                      0.1875f, 0.5208333f,
                      0.1875f, 0.5208333f,
                      0.22916667f, 0.5208333f,
                      0.22916667f, 0.5208333f,
                      0.27083334f, 0.5208333f,
                      0.27083334f, 0.5208333f,
                      0.3125f, 0.5208333f,
                      0.3125f, 0.5208333f,
                      0.35416666f, 0.5208333f,
                      0.35416666f, 0.5208333f,
                      0.39583334f, 0.5208333f,
                      0.39583334f, 0.5208333f,
                      0.4375f, 0.5208333f,
                      0.4375f, 0.5208333f,
                      0.47916666f, 0.5208333f,
                      0.47916666f, 0.5208333f,
                      0.5208333f, 0.5208333f,
                      0.5208333f, 0.5208333f,
                      0.5625f, 0.5208333f,
                      0.5625f, 0.5208333f,
                      0.6041667f, 0.5208333f,
                      0.6041667f, 0.5208333f,
                      0.6458333f, 0.5208333f,
                      0.6458333f, 0.5208333f,
                      0.6875f, 0.5208333f,
                      0.6875f, 0.5208333f,
                      0.7291667f, 0.5208333f,
                      0.7291667f, 0.5208333f,
                      0.7708333f, 0.5208333f,
                      0.7708333f, 0.5208333f,
                      0.8125f, 0.5208333f,
                      0.8125f, 0.5208333f,
                      0.8541667f, 0.5208333f,
                      0.8541667f, 0.5208333f,
                      0.8958333f, 0.5208333f,
                      0.8958333f, 0.5208333f,
                      0.9375f, 0.5208333f,
                      0.9375f, 0.5208333f,
                      0.9791667f, 0.5208333f,
                      0.9791667f, 0.5208333f,
                      0.02083333f, 0.5625f,
                      0.02083333f, 0.5625f,
                      0.0625f, 0.5625f,
                      0.0625f, 0.5625f,
                      0.10416666f, 0.5625f,
                      0.10416666f, 0.5625f,
                      0.14583333f, 0.5625f,
                      0.14583333f, 0.5625f,
                      0.1875f, 0.5625f,
                      0.1875f, 0.5625f,
                      0.22916667f, 0.5625f,
                      0.22916667f, 0.5625f,
                      0.27083334f, 0.5625f,
                      0.27083334f, 0.5625f,
                      0.3125f, 0.5625f,
                      0.3125f, 0.5625f,
                      0.35416666f, 0.5625f,
                      0.35416666f, 0.5625f,
                      0.39583334f, 0.5625f,
                      0.39583334f, 0.5625f,
                      0.4375f, 0.5625f,
                      0.4375f, 0.5625f,
                      0.47916666f, 0.5625f,
                      0.47916666f, 0.5625f,
                      0.5208333f, 0.5625f,
                      0.5208333f, 0.5625f,
                      0.5625f, 0.5625f,
                      0.5625f, 0.5625f,
                      0.6041667f, 0.5625f,
                      0.6041667f, 0.5625f,
                      0.6458333f, 0.5625f,
                      0.6458333f, 0.5625f,
                      0.6875f, 0.5625f,
                      0.6875f, 0.5625f,
                      0.7291667f, 0.5625f,
                      0.7291667f, 0.5625f,
                      0.7708333f, 0.5625f,
                      0.7708333f, 0.5625f,
                      0.8125f, 0.5625f,
                      0.8125f, 0.5625f,
                      0.8541667f, 0.5625f,
                      0.8541667f, 0.5625f,
                      0.8958333f, 0.5625f,
                      0.8958333f, 0.5625f,
                      0.9375f, 0.5625f,
                      0.9375f, 0.5625f,
                      0.9791667f, 0.5625f,
                      0.9791667f, 0.5625f,
                      0.02083333f, 0.6041667f,
                      0.02083333f, 0.6041667f,
                      0.0625f, 0.6041667f,
                      0.0625f, 0.6041667f,
                      0.10416666f, 0.6041667f,
                      0.10416666f, 0.6041667f,
                      0.14583333f, 0.6041667f,
                      0.14583333f, 0.6041667f,
                      0.1875f, 0.6041667f,
                      0.1875f, 0.6041667f,
                      0.22916667f, 0.6041667f,
                      0.22916667f, 0.6041667f,
                      0.27083334f, 0.6041667f,
                      0.27083334f, 0.6041667f,
                      0.3125f, 0.6041667f,
                      0.3125f, 0.6041667f,
                      0.35416666f, 0.6041667f,
                      0.35416666f, 0.6041667f,
                      0.39583334f, 0.6041667f,
                      0.39583334f, 0.6041667f,
                      0.4375f, 0.6041667f,
                      0.4375f, 0.6041667f,
                      0.47916666f, 0.6041667f,
                      0.47916666f, 0.6041667f,
                      0.5208333f, 0.6041667f,
                      0.5208333f, 0.6041667f,
                      0.5625f, 0.6041667f,
                      0.5625f, 0.6041667f,
                      0.6041667f, 0.6041667f,
                      0.6041667f, 0.6041667f,
                      0.6458333f, 0.6041667f,
                      0.6458333f, 0.6041667f,
                      0.6875f, 0.6041667f,
                      0.6875f, 0.6041667f,
                      0.7291667f, 0.6041667f,
                      0.7291667f, 0.6041667f,
                      0.7708333f, 0.6041667f,
                      0.7708333f, 0.6041667f,
                      0.8125f, 0.6041667f,
                      0.8125f, 0.6041667f,
                      0.8541667f, 0.6041667f,
                      0.8541667f, 0.6041667f,
                      0.8958333f, 0.6041667f,
                      0.8958333f, 0.6041667f,
                      0.9375f, 0.6041667f,
                      0.9375f, 0.6041667f,
                      0.9791667f, 0.6041667f,
                      0.9791667f, 0.6041667f,
                      0.02083333f, 0.6458333f,
                      0.02083333f, 0.6458333f,
                      0.0625f, 0.6458333f,
                      0.0625f, 0.6458333f,
                      0.10416666f, 0.6458333f,
                      0.10416666f, 0.6458333f,
                      0.14583333f, 0.6458333f,
                      0.14583333f, 0.6458333f,
                      0.1875f, 0.6458333f,
                      0.1875f, 0.6458333f,
                      0.22916667f, 0.6458333f,
                      0.22916667f, 0.6458333f,
                      0.27083334f, 0.6458333f,
                      0.27083334f, 0.6458333f,
                      0.3125f, 0.6458333f,
                      0.3125f, 0.6458333f,
                      0.35416666f, 0.6458333f,
                      0.35416666f, 0.6458333f,
                      0.39583334f, 0.6458333f,
                      0.39583334f, 0.6458333f,
                      0.4375f, 0.6458333f,
                      0.4375f, 0.6458333f,
                      0.47916666f, 0.6458333f,
                      0.47916666f, 0.6458333f,
                      0.5208333f, 0.6458333f,
                      0.5208333f, 0.6458333f,
                      0.5625f, 0.6458333f,
                      0.5625f, 0.6458333f,
                      0.6041667f, 0.6458333f,
                      0.6041667f, 0.6458333f,
                      0.6458333f, 0.6458333f,
                      0.6458333f, 0.6458333f,
                      0.6875f, 0.6458333f,
                      0.6875f, 0.6458333f,
                      0.7291667f, 0.6458333f,
                      0.7291667f, 0.6458333f,
                      0.7708333f, 0.6458333f,
                      0.7708333f, 0.6458333f,
                      0.8125f, 0.6458333f,
                      0.8125f, 0.6458333f,
                      0.8541667f, 0.6458333f,
                      0.8541667f, 0.6458333f,
                      0.8958333f, 0.6458333f,
                      0.8958333f, 0.6458333f,
                      0.9375f, 0.6458333f,
                      0.9375f, 0.6458333f,
                      0.9791667f, 0.6458333f,
                      0.9791667f, 0.6458333f,
                      0.02083333f, 0.6875f,
                      0.02083333f, 0.6875f,
                      0.0625f, 0.6875f,
                      0.0625f, 0.6875f,
                      0.10416666f, 0.6875f,
                      0.10416666f, 0.6875f,
                      0.14583333f, 0.6875f,
                      0.14583333f, 0.6875f,
                      0.1875f, 0.6875f,
                      0.1875f, 0.6875f,
                      0.22916667f, 0.6875f,
                      0.22916667f, 0.6875f,
                      0.27083334f, 0.6875f,
                      0.27083334f, 0.6875f,
                      0.3125f, 0.6875f,
                      0.3125f, 0.6875f,
                      0.35416666f, 0.6875f,
                      0.35416666f, 0.6875f,
                      0.39583334f, 0.6875f,
                      0.39583334f, 0.6875f,
                      0.4375f, 0.6875f,
                      0.4375f, 0.6875f,
                      0.47916666f, 0.6875f,
                      0.47916666f, 0.6875f,
                      0.5208333f, 0.6875f,
                      0.5208333f, 0.6875f,
                      0.5625f, 0.6875f,
                      0.5625f, 0.6875f,
                      0.6041667f, 0.6875f,
                      0.6041667f, 0.6875f,
                      0.6458333f, 0.6875f,
                      0.6458333f, 0.6875f,
                      0.6875f, 0.6875f,
                      0.6875f, 0.6875f,
                      0.7291667f, 0.6875f,
                      0.7291667f, 0.6875f,
                      0.7708333f, 0.6875f,
                      0.7708333f, 0.6875f,
                      0.8125f, 0.6875f,
                      0.8125f, 0.6875f,
                      0.8541667f, 0.6875f,
                      0.8541667f, 0.6875f,
                      0.8958333f, 0.6875f,
                      0.8958333f, 0.6875f,
                      0.9375f, 0.6875f,
                      0.9375f, 0.6875f,
                      0.9791667f, 0.6875f,
                      0.9791667f, 0.6875f,
                      0.02083333f, 0.7291667f,
                      0.02083333f, 0.7291667f,
                      0.0625f, 0.7291667f,
                      0.0625f, 0.7291667f,
                      0.10416666f, 0.7291667f,
                      0.10416666f, 0.7291667f,
                      0.14583333f, 0.7291667f,
                      0.14583333f, 0.7291667f,
                      0.1875f, 0.7291667f,
                      0.1875f, 0.7291667f,
                      0.22916667f, 0.7291667f,
                      0.22916667f, 0.7291667f,
                      0.27083334f, 0.7291667f,
                      0.27083334f, 0.7291667f,
                      0.3125f, 0.7291667f,
                      0.3125f, 0.7291667f,
                      0.35416666f, 0.7291667f,
                      0.35416666f, 0.7291667f,
                      0.39583334f, 0.7291667f,
                      0.39583334f, 0.7291667f,
                      0.4375f, 0.7291667f,
                      0.4375f, 0.7291667f,
                      0.47916666f, 0.7291667f,
                      0.47916666f, 0.7291667f,
                      0.5208333f, 0.7291667f,
                      0.5208333f, 0.7291667f,
                      0.5625f, 0.7291667f,
                      0.5625f, 0.7291667f,
                      0.6041667f, 0.7291667f,
                      0.6041667f, 0.7291667f,
                      0.6458333f, 0.7291667f,
                      0.6458333f, 0.7291667f,
                      0.6875f, 0.7291667f,
                      0.6875f, 0.7291667f,
                      0.7291667f, 0.7291667f,
                      0.7291667f, 0.7291667f,
                      0.7708333f, 0.7291667f,
                      0.7708333f, 0.7291667f,
                      0.8125f, 0.7291667f,
                      0.8125f, 0.7291667f,
                      0.8541667f, 0.7291667f,
                      0.8541667f, 0.7291667f,
                      0.8958333f, 0.7291667f,
                      0.8958333f, 0.7291667f,
                      0.9375f, 0.7291667f,
                      0.9375f, 0.7291667f,
                      0.9791667f, 0.7291667f,
                      0.9791667f, 0.7291667f,
                      0.02083333f, 0.7708333f,
                      0.02083333f, 0.7708333f,
                      0.0625f, 0.7708333f,
                      0.0625f, 0.7708333f,
                      0.10416666f, 0.7708333f,
                      0.10416666f, 0.7708333f,
                      0.14583333f, 0.7708333f,
                      0.14583333f, 0.7708333f,
                      0.1875f, 0.7708333f,
                      0.1875f, 0.7708333f,
                      0.22916667f, 0.7708333f,
                      0.22916667f, 0.7708333f,
                      0.27083334f, 0.7708333f,
                      0.27083334f, 0.7708333f,
                      0.3125f, 0.7708333f,
                      0.3125f, 0.7708333f,
                      0.35416666f, 0.7708333f,
                      0.35416666f, 0.7708333f,
                      0.39583334f, 0.7708333f,
                      0.39583334f, 0.7708333f,
                      0.4375f, 0.7708333f,
                      0.4375f, 0.7708333f,
                      0.47916666f, 0.7708333f,
                      0.47916666f, 0.7708333f,
                      0.5208333f, 0.7708333f,
                      0.5208333f, 0.7708333f,
                      0.5625f, 0.7708333f,
                      0.5625f, 0.7708333f,
                      0.6041667f, 0.7708333f,
                      0.6041667f, 0.7708333f,
                      0.6458333f, 0.7708333f,
                      0.6458333f, 0.7708333f,
                      0.6875f, 0.7708333f,
                      0.6875f, 0.7708333f,
                      0.7291667f, 0.7708333f,
                      0.7291667f, 0.7708333f,
                      0.7708333f, 0.7708333f,
                      0.7708333f, 0.7708333f,
                      0.8125f, 0.7708333f,
                      0.8125f, 0.7708333f,
                      0.8541667f, 0.7708333f,
                      0.8541667f, 0.7708333f,
                      0.8958333f, 0.7708333f,
                      0.8958333f, 0.7708333f,
                      0.9375f, 0.7708333f,
                      0.9375f, 0.7708333f,
                      0.9791667f, 0.7708333f,
                      0.9791667f, 0.7708333f,
                      0.02083333f, 0.8125f,
                      0.02083333f, 0.8125f,
                      0.0625f, 0.8125f,
                      0.0625f, 0.8125f,
                      0.10416666f, 0.8125f,
                      0.10416666f, 0.8125f,
                      0.14583333f, 0.8125f,
                      0.14583333f, 0.8125f,
                      0.1875f, 0.8125f,
                      0.1875f, 0.8125f,
                      0.22916667f, 0.8125f,
                      0.22916667f, 0.8125f,
                      0.27083334f, 0.8125f,
                      0.27083334f, 0.8125f,
                      0.3125f, 0.8125f,
                      0.3125f, 0.8125f,
                      0.35416666f, 0.8125f,
                      0.35416666f, 0.8125f,
                      0.39583334f, 0.8125f,
                      0.39583334f, 0.8125f,
                      0.4375f, 0.8125f,
                      0.4375f, 0.8125f,
                      0.47916666f, 0.8125f,
                      0.47916666f, 0.8125f,
                      0.5208333f, 0.8125f,
                      0.5208333f, 0.8125f,
                      0.5625f, 0.8125f,
                      0.5625f, 0.8125f,
                      0.6041667f, 0.8125f,
                      0.6041667f, 0.8125f,
                      0.6458333f, 0.8125f,
                      0.6458333f, 0.8125f,
                      0.6875f, 0.8125f,
                      0.6875f, 0.8125f,
                      0.7291667f, 0.8125f,
                      0.7291667f, 0.8125f,
                      0.7708333f, 0.8125f,
                      0.7708333f, 0.8125f,
                      0.8125f, 0.8125f,
                      0.8125f, 0.8125f,
                      0.8541667f, 0.8125f,
                      0.8541667f, 0.8125f,
                      0.8958333f, 0.8125f,
                      0.8958333f, 0.8125f,
                      0.9375f, 0.8125f,
                      0.9375f, 0.8125f,
                      0.9791667f, 0.8125f,
                      0.9791667f, 0.8125f,
                      0.02083333f, 0.8541667f,
                      0.02083333f, 0.8541667f,
                      0.0625f, 0.8541667f,
                      0.0625f, 0.8541667f,
                      0.10416666f, 0.8541667f,
                      0.10416666f, 0.8541667f,
                      0.14583333f, 0.8541667f,
                      0.14583333f, 0.8541667f,
                      0.1875f, 0.8541667f,
                      0.1875f, 0.8541667f,
                      0.22916667f, 0.8541667f,
                      0.22916667f, 0.8541667f,
                      0.27083334f, 0.8541667f,
                      0.27083334f, 0.8541667f,
                      0.3125f, 0.8541667f,
                      0.3125f, 0.8541667f,
                      0.35416666f, 0.8541667f,
                      0.35416666f, 0.8541667f,
                      0.39583334f, 0.8541667f,
                      0.39583334f, 0.8541667f,
                      0.4375f, 0.8541667f,
                      0.4375f, 0.8541667f,
                      0.47916666f, 0.8541667f,
                      0.47916666f, 0.8541667f,
                      0.5208333f, 0.8541667f,
                      0.5208333f, 0.8541667f,
                      0.5625f, 0.8541667f,
                      0.5625f, 0.8541667f,
                      0.6041667f, 0.8541667f,
                      0.6041667f, 0.8541667f,
                      0.6458333f, 0.8541667f,
                      0.6458333f, 0.8541667f,
                      0.6875f, 0.8541667f,
                      0.6875f, 0.8541667f,
                      0.7291667f, 0.8541667f,
                      0.7291667f, 0.8541667f,
                      0.7708333f, 0.8541667f,
                      0.7708333f, 0.8541667f,
                      0.8125f, 0.8541667f,
                      0.8125f, 0.8541667f,
                      0.8541667f, 0.8541667f,
                      0.8541667f, 0.8541667f,
                      0.8958333f, 0.8541667f,
                      0.8958333f, 0.8541667f,
                      0.9375f, 0.8541667f,
                      0.9375f, 0.8541667f,
                      0.9791667f, 0.8541667f,
                      0.9791667f, 0.8541667f,
                      0.02083333f, 0.8958333f,
                      0.02083333f, 0.8958333f,
                      0.0625f, 0.8958333f,
                      0.0625f, 0.8958333f,
                      0.10416666f, 0.8958333f,
                      0.10416666f, 0.8958333f,
                      0.14583333f, 0.8958333f,
                      0.14583333f, 0.8958333f,
                      0.1875f, 0.8958333f,
                      0.1875f, 0.8958333f,
                      0.22916667f, 0.8958333f,
                      0.22916667f, 0.8958333f,
                      0.27083334f, 0.8958333f,
                      0.27083334f, 0.8958333f,
                      0.3125f, 0.8958333f,
                      0.3125f, 0.8958333f,
                      0.35416666f, 0.8958333f,
                      0.35416666f, 0.8958333f,
                      0.39583334f, 0.8958333f,
                      0.39583334f, 0.8958333f,
                      0.4375f, 0.8958333f,
                      0.4375f, 0.8958333f,
                      0.47916666f, 0.8958333f,
                      0.47916666f, 0.8958333f,
                      0.5208333f, 0.8958333f,
                      0.5208333f, 0.8958333f,
                      0.5625f, 0.8958333f,
                      0.5625f, 0.8958333f,
                      0.6041667f, 0.8958333f,
                      0.6041667f, 0.8958333f,
                      0.6458333f, 0.8958333f,
                      0.6458333f, 0.8958333f,
                      0.6875f, 0.8958333f,
                      0.6875f, 0.8958333f,
                      0.7291667f, 0.8958333f,
                      0.7291667f, 0.8958333f,
                      0.7708333f, 0.8958333f,
                      0.7708333f, 0.8958333f,
                      0.8125f, 0.8958333f,
                      0.8125f, 0.8958333f,
                      0.8541667f, 0.8958333f,
                      0.8541667f, 0.8958333f,
                      0.8958333f, 0.8958333f,
                      0.8958333f, 0.8958333f,
                      0.9375f, 0.8958333f,
                      0.9375f, 0.8958333f,
                      0.9791667f, 0.8958333f,
                      0.9791667f, 0.8958333f,
                      0.02083333f, 0.9375f,
                      0.02083333f, 0.9375f,
                      0.0625f, 0.9375f,
                      0.0625f, 0.9375f,
                      0.10416666f, 0.9375f,
                      0.10416666f, 0.9375f,
                      0.14583333f, 0.9375f,
                      0.14583333f, 0.9375f,
                      0.1875f, 0.9375f,
                      0.1875f, 0.9375f,
                      0.22916667f, 0.9375f,
                      0.22916667f, 0.9375f,
                      0.27083334f, 0.9375f,
                      0.27083334f, 0.9375f,
                      0.3125f, 0.9375f,
                      0.3125f, 0.9375f,
                      0.35416666f, 0.9375f,
                      0.35416666f, 0.9375f,
                      0.39583334f, 0.9375f,
                      0.39583334f, 0.9375f,
                      0.4375f, 0.9375f,
                      0.4375f, 0.9375f,
                      0.47916666f, 0.9375f,
                      0.47916666f, 0.9375f,
                      0.5208333f, 0.9375f,
                      0.5208333f, 0.9375f,
                      0.5625f, 0.9375f,
                      0.5625f, 0.9375f,
                      0.6041667f, 0.9375f,
                      0.6041667f, 0.9375f,
                      0.6458333f, 0.9375f,
                      0.6458333f, 0.9375f,
                      0.6875f, 0.9375f,
                      0.6875f, 0.9375f,
                      0.7291667f, 0.9375f,
                      0.7291667f, 0.9375f,
                      0.7708333f, 0.9375f,
                      0.7708333f, 0.9375f,
                      0.8125f, 0.9375f,
                      0.8125f, 0.9375f,
                      0.8541667f, 0.9375f,
                      0.8541667f, 0.9375f,
                      0.8958333f, 0.9375f,
                      0.8958333f, 0.9375f,
                      0.9375f, 0.9375f,
                      0.9375f, 0.9375f,
                      0.9791667f, 0.9375f,
                      0.9791667f, 0.9375f,
                      0.02083333f, 0.9791667f,
                      0.02083333f, 0.9791667f,
                      0.0625f, 0.9791667f,
                      0.0625f, 0.9791667f,
                      0.10416666f, 0.9791667f,
                      0.10416666f, 0.9791667f,
                      0.14583333f, 0.9791667f,
                      0.14583333f, 0.9791667f,
                      0.1875f, 0.9791667f,
                      0.1875f, 0.9791667f,
                      0.22916667f, 0.9791667f,
                      0.22916667f, 0.9791667f,
                      0.27083334f, 0.9791667f,
                      0.27083334f, 0.9791667f,
                      0.3125f, 0.9791667f,
                      0.3125f, 0.9791667f,
                      0.35416666f, 0.9791667f,
                      0.35416666f, 0.9791667f,
                      0.39583334f, 0.9791667f,
                      0.39583334f, 0.9791667f,
                      0.4375f, 0.9791667f,
                      0.4375f, 0.9791667f,
                      0.47916666f, 0.9791667f,
                      0.47916666f, 0.9791667f,
                      0.5208333f, 0.9791667f,
                      0.5208333f, 0.9791667f,
                      0.5625f, 0.9791667f,
                      0.5625f, 0.9791667f,
                      0.6041667f, 0.9791667f,
                      0.6041667f, 0.9791667f,
                      0.6458333f, 0.9791667f,
                      0.6458333f, 0.9791667f,
                      0.6875f, 0.9791667f,
                      0.6875f, 0.9791667f,
                      0.7291667f, 0.9791667f,
                      0.7291667f, 0.9791667f,
                      0.7708333f, 0.9791667f,
                      0.7708333f, 0.9791667f,
                      0.8125f, 0.9791667f,
                      0.8125f, 0.9791667f,
                      0.8541667f, 0.9791667f,
                      0.8541667f, 0.9791667f,
                      0.8958333f, 0.9791667f,
                      0.8958333f, 0.9791667f,
                      0.9375f, 0.9791667f,
                      0.9375f, 0.9791667f,
                      0.9791667f, 0.9791667f,
                      0.9791667f, 0.9791667f,
                      0.04166667f, 0.04166667f,
                      0.04166667f, 0.04166667f,
                      0.04166667f, 0.04166667f,
                      0.04166667f, 0.04166667f,
                      0.04166667f, 0.04166667f,
                      0.04166667f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.125f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.20833333f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.29166666f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.375f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.45833334f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.5416667f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.625f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7083333f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.7916667f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.875f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.9583333f, 0.04166667f,
                      0.04166667f, 0.125f,
                      0.04166667f, 0.125f,
                      0.04166667f, 0.125f,
                      0.04166667f, 0.125f,
                      0.04166667f, 0.125f,
                      0.04166667f, 0.125f,
                      0.125f, 0.125f,
                      0.125f, 0.125f,
                      0.125f, 0.125f,
                      0.125f, 0.125f,
                      0.125f, 0.125f,
                      0.125f, 0.125f,
                      0.20833333f, 0.125f,
                      0.20833333f, 0.125f,
                      0.20833333f, 0.125f,
                      0.20833333f, 0.125f,
                      0.20833333f, 0.125f,
                      0.20833333f, 0.125f,
                      0.29166666f, 0.125f,
                      0.29166666f, 0.125f,
                      0.29166666f, 0.125f,
                      0.29166666f, 0.125f,
                      0.29166666f, 0.125f,
                      0.29166666f, 0.125f,
                      0.375f, 0.125f,
                      0.375f, 0.125f,
                      0.375f, 0.125f,
                      0.375f, 0.125f,
                      0.375f, 0.125f,
                      0.375f, 0.125f,
                      0.45833334f, 0.125f,
                      0.45833334f, 0.125f,
                      0.45833334f, 0.125f,
                      0.45833334f, 0.125f,
                      0.45833334f, 0.125f,
                      0.45833334f, 0.125f,
                      0.5416667f, 0.125f,
                      0.5416667f, 0.125f,
                      0.5416667f, 0.125f,
                      0.5416667f, 0.125f,
                      0.5416667f, 0.125f,
                      0.5416667f, 0.125f,
                      0.625f, 0.125f,
                      0.625f, 0.125f,
                      0.625f, 0.125f,
                      0.625f, 0.125f,
                      0.625f, 0.125f,
                      0.625f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7083333f, 0.125f,
                      0.7916667f, 0.125f,
                      0.7916667f, 0.125f,
                      0.7916667f, 0.125f,
                      0.7916667f, 0.125f,
                      0.7916667f, 0.125f,
                      0.7916667f, 0.125f,
                      0.875f, 0.125f,
                      0.875f, 0.125f,
                      0.875f, 0.125f,
                      0.875f, 0.125f,
                      0.875f, 0.125f,
                      0.875f, 0.125f,
                      0.9583333f, 0.125f,
                      0.9583333f, 0.125f,
                      0.9583333f, 0.125f,
                      0.9583333f, 0.125f,
                      0.9583333f, 0.125f,
                      0.9583333f, 0.125f,
                      0.04166667f, 0.20833333f,
                      0.04166667f, 0.20833333f,
                      0.04166667f, 0.20833333f,
                      0.04166667f, 0.20833333f,
                      0.04166667f, 0.20833333f,
                      0.04166667f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.125f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.20833333f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.29166666f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.375f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.45833334f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.5416667f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.625f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7083333f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.7916667f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.875f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.9583333f, 0.20833333f,
                      0.04166667f, 0.29166666f,
                      0.04166667f, 0.29166666f,
                      0.04166667f, 0.29166666f,
                      0.04166667f, 0.29166666f,
                      0.04166667f, 0.29166666f,
                      0.04166667f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.125f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.20833333f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.29166666f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.375f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.45833334f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.5416667f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.625f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7083333f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.7916667f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.875f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.9583333f, 0.29166666f,
                      0.04166667f, 0.375f,
                      0.04166667f, 0.375f,
                      0.04166667f, 0.375f,
                      0.04166667f, 0.375f,
                      0.04166667f, 0.375f,
                      0.04166667f, 0.375f,
                      0.125f, 0.375f,
                      0.125f, 0.375f,
                      0.125f, 0.375f,
                      0.125f, 0.375f,
                      0.125f, 0.375f,
                      0.125f, 0.375f,
                      0.20833333f, 0.375f,
                      0.20833333f, 0.375f,
                      0.20833333f, 0.375f,
                      0.20833333f, 0.375f,
                      0.20833333f, 0.375f,
                      0.20833333f, 0.375f,
                      0.29166666f, 0.375f,
                      0.29166666f, 0.375f,
                      0.29166666f, 0.375f,
                      0.29166666f, 0.375f,
                      0.29166666f, 0.375f,
                      0.29166666f, 0.375f,
                      0.375f, 0.375f,
                      0.375f, 0.375f,
                      0.375f, 0.375f,
                      0.375f, 0.375f,
                      0.375f, 0.375f,
                      0.375f, 0.375f,
                      0.45833334f, 0.375f,
                      0.45833334f, 0.375f,
                      0.45833334f, 0.375f,
                      0.45833334f, 0.375f,
                      0.45833334f, 0.375f,
                      0.45833334f, 0.375f,
                      0.5416667f, 0.375f,
                      0.5416667f, 0.375f,
                      0.5416667f, 0.375f,
                      0.5416667f, 0.375f,
                      0.5416667f, 0.375f,
                      0.5416667f, 0.375f,
                      0.625f, 0.375f,
                      0.625f, 0.375f,
                      0.625f, 0.375f,
                      0.625f, 0.375f,
                      0.625f, 0.375f,
                      0.625f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7083333f, 0.375f,
                      0.7916667f, 0.375f,
                      0.7916667f, 0.375f,
                      0.7916667f, 0.375f,
                      0.7916667f, 0.375f,
                      0.7916667f, 0.375f,
                      0.7916667f, 0.375f,
                      0.875f, 0.375f,
                      0.875f, 0.375f,
                      0.875f, 0.375f,
                      0.875f, 0.375f,
                      0.875f, 0.375f,
                      0.875f, 0.375f,
                      0.9583333f, 0.375f,
                      0.9583333f, 0.375f,
                      0.9583333f, 0.375f,
                      0.9583333f, 0.375f,
                      0.9583333f, 0.375f,
                      0.9583333f, 0.375f,
                      0.04166667f, 0.45833334f,
                      0.04166667f, 0.45833334f,
                      0.04166667f, 0.45833334f,
                      0.04166667f, 0.45833334f,
                      0.04166667f, 0.45833334f,
                      0.04166667f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.125f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.20833333f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.29166666f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.375f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.45833334f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.5416667f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.625f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7083333f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.7916667f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.875f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.9583333f, 0.45833334f,
                      0.04166667f, 0.5416667f,
                      0.04166667f, 0.5416667f,
                      0.04166667f, 0.5416667f,
                      0.04166667f, 0.5416667f,
                      0.04166667f, 0.5416667f,
                      0.04166667f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.125f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.20833333f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.29166666f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.375f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.45833334f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.5416667f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.625f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7083333f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.7916667f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.875f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.9583333f, 0.5416667f,
                      0.04166667f, 0.625f,
                      0.04166667f, 0.625f,
                      0.04166667f, 0.625f,
                      0.04166667f, 0.625f,
                      0.04166667f, 0.625f,
                      0.04166667f, 0.625f,
                      0.125f, 0.625f,
                      0.125f, 0.625f,
                      0.125f, 0.625f,
                      0.125f, 0.625f,
                      0.125f, 0.625f,
                      0.125f, 0.625f,
                      0.20833333f, 0.625f,
                      0.20833333f, 0.625f,
                      0.20833333f, 0.625f,
                      0.20833333f, 0.625f,
                      0.20833333f, 0.625f,
                      0.20833333f, 0.625f,
                      0.29166666f, 0.625f,
                      0.29166666f, 0.625f,
                      0.29166666f, 0.625f,
                      0.29166666f, 0.625f,
                      0.29166666f, 0.625f,
                      0.29166666f, 0.625f,
                      0.375f, 0.625f,
                      0.375f, 0.625f,
                      0.375f, 0.625f,
                      0.375f, 0.625f,
                      0.375f, 0.625f,
                      0.375f, 0.625f,
                      0.45833334f, 0.625f,
                      0.45833334f, 0.625f,
                      0.45833334f, 0.625f,
                      0.45833334f, 0.625f,
                      0.45833334f, 0.625f,
                      0.45833334f, 0.625f,
                      0.5416667f, 0.625f,
                      0.5416667f, 0.625f,
                      0.5416667f, 0.625f,
                      0.5416667f, 0.625f,
                      0.5416667f, 0.625f,
                      0.5416667f, 0.625f,
                      0.625f, 0.625f,
                      0.625f, 0.625f,
                      0.625f, 0.625f,
                      0.625f, 0.625f,
                      0.625f, 0.625f,
                      0.625f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7083333f, 0.625f,
                      0.7916667f, 0.625f,
                      0.7916667f, 0.625f,
                      0.7916667f, 0.625f,
                      0.7916667f, 0.625f,
                      0.7916667f, 0.625f,
                      0.7916667f, 0.625f,
                      0.875f, 0.625f,
                      0.875f, 0.625f,
                      0.875f, 0.625f,
                      0.875f, 0.625f,
                      0.875f, 0.625f,
                      0.875f, 0.625f,
                      0.9583333f, 0.625f,
                      0.9583333f, 0.625f,
                      0.9583333f, 0.625f,
                      0.9583333f, 0.625f,
                      0.9583333f, 0.625f,
                      0.9583333f, 0.625f,
                      0.04166667f, 0.7083333f,
                      0.04166667f, 0.7083333f,
                      0.04166667f, 0.7083333f,
                      0.04166667f, 0.7083333f,
                      0.04166667f, 0.7083333f,
                      0.04166667f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.125f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.20833333f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.29166666f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.375f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.45833334f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.5416667f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.625f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7083333f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.7916667f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.875f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.9583333f, 0.7083333f,
                      0.04166667f, 0.7916667f,
                      0.04166667f, 0.7916667f,
                      0.04166667f, 0.7916667f,
                      0.04166667f, 0.7916667f,
                      0.04166667f, 0.7916667f,
                      0.04166667f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.125f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.20833333f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.29166666f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.375f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.45833334f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.5416667f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.625f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7083333f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.7916667f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.875f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.9583333f, 0.7916667f,
                      0.04166667f, 0.875f,
                      0.04166667f, 0.875f,
                      0.04166667f, 0.875f,
                      0.04166667f, 0.875f,
                      0.04166667f, 0.875f,
                      0.04166667f, 0.875f,
                      0.125f, 0.875f,
                      0.125f, 0.875f,
                      0.125f, 0.875f,
                      0.125f, 0.875f,
                      0.125f, 0.875f,
                      0.125f, 0.875f,
                      0.20833333f, 0.875f,
                      0.20833333f, 0.875f,
                      0.20833333f, 0.875f,
                      0.20833333f, 0.875f,
                      0.20833333f, 0.875f,
                      0.20833333f, 0.875f,
                      0.29166666f, 0.875f,
                      0.29166666f, 0.875f,
                      0.29166666f, 0.875f,
                      0.29166666f, 0.875f,
                      0.29166666f, 0.875f,
                      0.29166666f, 0.875f,
                      0.375f, 0.875f,
                      0.375f, 0.875f,
                      0.375f, 0.875f,
                      0.375f, 0.875f,
                      0.375f, 0.875f,
                      0.375f, 0.875f,
                      0.45833334f, 0.875f,
                      0.45833334f, 0.875f,
                      0.45833334f, 0.875f,
                      0.45833334f, 0.875f,
                      0.45833334f, 0.875f,
                      0.45833334f, 0.875f,
                      0.5416667f, 0.875f,
                      0.5416667f, 0.875f,
                      0.5416667f, 0.875f,
                      0.5416667f, 0.875f,
                      0.5416667f, 0.875f,
                      0.5416667f, 0.875f,
                      0.625f, 0.875f,
                      0.625f, 0.875f,
                      0.625f, 0.875f,
                      0.625f, 0.875f,
                      0.625f, 0.875f,
                      0.625f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7083333f, 0.875f,
                      0.7916667f, 0.875f,
                      0.7916667f, 0.875f,
                      0.7916667f, 0.875f,
                      0.7916667f, 0.875f,
                      0.7916667f, 0.875f,
                      0.7916667f, 0.875f,
                      0.875f, 0.875f,
                      0.875f, 0.875f,
                      0.875f, 0.875f,
                      0.875f, 0.875f,
                      0.875f, 0.875f,
                      0.875f, 0.875f,
                      0.9583333f, 0.875f,
                      0.9583333f, 0.875f,
                      0.9583333f, 0.875f,
                      0.9583333f, 0.875f,
                      0.9583333f, 0.875f,
                      0.9583333f, 0.875f,
                      0.04166667f, 0.9583333f,
                      0.04166667f, 0.9583333f,
                      0.04166667f, 0.9583333f,
                      0.04166667f, 0.9583333f,
                      0.04166667f, 0.9583333f,
                      0.04166667f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.125f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.20833333f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.29166666f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.375f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.45833334f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.5416667f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.625f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7083333f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.7916667f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.875f, 0.9583333f,
                      0.9583333f, 0.9583333f,
                      0.9583333f, 0.9583333f,
                      0.9583333f, 0.9583333f,
                      0.9583333f, 0.9583333f,
                      0.9583333f, 0.9583333f,
                      0.9583333f, 0.9583333f
                 };

            anchors.put(0, 0, anchors_arr);

            return anchors;
        }
    }
}
#endif
