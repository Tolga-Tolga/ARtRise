#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.DnnModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using OpenCVRect = OpenCVForUnity.CoreModule.Rect;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Referring to https://github.com/opencv/opencv_zoo/tree/master/models/handpose_estimation_mediapipe
    ///
    /// [Tested Models]
    /// https://github.com/opencv/opencv_zoo/raw/05a07912a619f3dd491ba22ca489245c7847c9ff/models/handpose_estimation_mediapipe/handpose_estimation_mediapipe_2023feb.onnx
    /// </summary>
    public class MediaPipeHandPoseEstimator : ProcessingWorkerBase
    {
        public enum KeyPoint : byte
        {
            Wrist,
            Thumb1, Thumb2, Thumb3, Thumb4,
            Index1, Index2, Index3, Index4,
            Middle1, Middle2, Middle3, Middle4,
            Ring1, Ring2, Ring3, Ring4,
            Pinky1, Pinky2, Pinky3, Pinky4
        }

        protected static readonly Size INPUT_SIZE = new Size(224, 224);
        protected static readonly Scalar SCALAR_WHITE = new Scalar(255, 255, 255, 255);
        protected static readonly Scalar SCALAR_RED = new Scalar(0, 0, 255, 255);
        protected static readonly Scalar SCALAR_GREEN = new Scalar(0, 255, 0, 255);
        protected static readonly Scalar SCALAR_BLUE = new Scalar(255, 0, 0, 255);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);

        //private int[] PALM_LANDMARK_IDS = new int[] { 0, 5, 9, 13, 17, 1, 2 };
        protected const int PALM_LANDMARKS_INDEX_OF_PALM_BASE = 0;
        protected const int PALM_LANDMARKS_INDEX_OF_MIDDLE_FINGER_BASE = 2;
        protected static readonly Point PALM_BOX_PRE_SHIFT_VECTOR = new Point(0, 0);
        //private double PALM_BOX_PRE_ENLARGE_FACTOR = 4.0;
        protected static readonly Point PALM_BOX_SHIFT_VECTOR = new Point(0, -0.4);
        protected const double PALM_BOX_ENLARGE_FACTOR = 3.0;
        protected static readonly Point HAND_BOX_SHIFT_VECTOR = new Point(0, -0.1);
        protected const double HAND_BOX_ENLARGE_FACTOR = 1.65;

        protected float _confThreshold;
        protected int _backend;
        protected int _target;
        protected Net _handposeEstimationNet;
        protected List<string> _cachedUnconnectedOutLayersNames;
        protected Mat _tmpImage;
        protected Mat _tmpRotatedImage;
        protected Mat _inputSizeMat;
        protected Mat _output0Buffer;

        /// <summary>
        /// Initializes a new instance of the MediaPipeHandPoseEstimator class.
        /// </summary>
        /// <param name="modelFilepath">Path to the model file.</param>
        /// <param name="confThreshold">Confidence threshold for filtering estimations.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipeHandPoseEstimator(string modelFilepath, float confThreshold = 0.8f,
                                             int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));

            _confThreshold = Mathf.Clamp01(confThreshold);
            _backend = backend;
            _target = target;

            try
            {
                _handposeEstimationNet = Dnn.readNet(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _handposeEstimationNet.setPreferableBackend(_backend);
            _handposeEstimationNet.setPreferableTarget(_target);
            _cachedUnconnectedOutLayersNames = _handposeEstimationNet.getUnconnectedOutLayersNames();

            _output0Buffer = new Mat(132, 1, CvType.CV_32FC1);
        }


        /// <summary>
        /// Visualizes hand pose estimation result on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">The result Mat returned by Estimate method.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.rows() < 132)
                throw new ArgumentException("Invalid result matrix. It must have at least 132 rows.");

            HandPoseEstimationBlazeData data = ToStructuredData(result);

            float left = data.X1;
            float top = data.Y1;
            float right = data.X2;
            float bottom = data.Y2;

#if NET_STANDARD_2_1
            ReadOnlySpan<Vec3f> landmarksScreen = data.GetLandmarksScreen();
            ReadOnlySpan<Vec3f> landmarksWorld = data.GetLandmarksWorld();
#else
            Vec3f[] landmarksScreen = data.GetLandmarksScreenArray();
            Vec3f[] landmarksWorld = data.GetLandmarksWorldArray();
#endif

            float handedness = data.Handedness;
            string handednessText = (handedness <= 0.5f) ? "Left" : "Right";
            float confidence = data.Confidence;

            var lineColor = SCALAR_WHITE.ToValueTuple();
            var pointColor = (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple();

            // # draw box
            Imgproc.rectangle(image, (left, top), (right, bottom), SCALAR_GREEN.ToValueTuple(), 2);
            Imgproc.putText(image, confidence.ToString("F3"), (left, top + 12), Imgproc.FONT_HERSHEY_DUPLEX, 0.5, pointColor);
            Imgproc.putText(image, handednessText, (left, top + 24), Imgproc.FONT_HERSHEY_DUPLEX, 0.5, pointColor);

            // # Draw line between each key points
            draw_lines(landmarksScreen, true);

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("-----------hand-----------");
                sb.AppendLine();
                sb.AppendFormat("Confidence: {0:F4}", confidence);
                sb.AppendLine();
                sb.AppendFormat("Handedness: {0} ({1:F3})", handednessText, handedness);
                sb.AppendLine();
                sb.AppendFormat("Hand Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", left, top, right, bottom);
                sb.AppendLine();
                sb.Append("Hand LandmarksScreen: ");
                sb.Append("{");
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    ref readonly var p = ref landmarksScreen[i];
                    sb.AppendFormat("({0:F3}, {1:F3}, {2:F3})", p.Item1, p.Item2, p.Item3);
                    if (i < landmarksScreen.Length - 1)
                        sb.Append(", ");
                }
                sb.Append("}");
                sb.AppendLine();
                sb.Append("Hand LandmarksWorld: ");
                sb.Append("{");
                for (int i = 0; i < landmarksWorld.Length; i++)
                {
                    ref readonly var p = ref landmarksWorld[i];
                    sb.AppendFormat("({0:F3}, {1:F3}, {2:F3})", p.Item1, p.Item2, p.Item3);
                    if (i < landmarksWorld.Length - 1)
                        sb.Append(", ");
                }
                sb.Append("}");
                sb.AppendLine();

                Debug.Log(sb.ToString());
            }

#if NET_STANDARD_2_1
            void draw_lines(ReadOnlySpan<Vec3f> landmarks, bool is_draw_point = true, int thickness = 2)
#else
            void draw_lines(Vec3f[] landmarks, bool is_draw_point = true, int thickness = 2)
#endif
            {
                ref readonly var wrist = ref landmarks[(int)KeyPoint.Wrist];
                ref readonly var thumb1 = ref landmarks[(int)KeyPoint.Thumb1];
                ref readonly var thumb2 = ref landmarks[(int)KeyPoint.Thumb2];
                ref readonly var thumb3 = ref landmarks[(int)KeyPoint.Thumb3];
                ref readonly var thumb4 = ref landmarks[(int)KeyPoint.Thumb4];
                ref readonly var index1 = ref landmarks[(int)KeyPoint.Index1];
                ref readonly var index2 = ref landmarks[(int)KeyPoint.Index2];
                ref readonly var index3 = ref landmarks[(int)KeyPoint.Index3];
                ref readonly var index4 = ref landmarks[(int)KeyPoint.Index4];
                ref readonly var middle1 = ref landmarks[(int)KeyPoint.Middle1];
                ref readonly var middle2 = ref landmarks[(int)KeyPoint.Middle2];
                ref readonly var middle3 = ref landmarks[(int)KeyPoint.Middle3];
                ref readonly var middle4 = ref landmarks[(int)KeyPoint.Middle4];
                ref readonly var ring1 = ref landmarks[(int)KeyPoint.Ring1];
                ref readonly var ring2 = ref landmarks[(int)KeyPoint.Ring2];
                ref readonly var ring3 = ref landmarks[(int)KeyPoint.Ring3];
                ref readonly var ring4 = ref landmarks[(int)KeyPoint.Ring4];
                ref readonly var pinky1 = ref landmarks[(int)KeyPoint.Pinky1];
                ref readonly var pinky2 = ref landmarks[(int)KeyPoint.Pinky2];
                ref readonly var pinky3 = ref landmarks[(int)KeyPoint.Pinky3];
                ref readonly var pinky4 = ref landmarks[(int)KeyPoint.Pinky4];

                Imgproc.line(image, (wrist.Item1, wrist.Item2), (thumb1.Item1, thumb1.Item2), lineColor, thickness);
                Imgproc.line(image, (thumb1.Item1, thumb1.Item2), (thumb2.Item1, thumb2.Item2), lineColor, thickness);
                Imgproc.line(image, (thumb2.Item1, thumb2.Item2), (thumb3.Item1, thumb3.Item2), lineColor, thickness);
                Imgproc.line(image, (thumb3.Item1, thumb3.Item2), (thumb4.Item1, thumb4.Item2), lineColor, thickness);

                Imgproc.line(image, (wrist.Item1, wrist.Item2), (index1.Item1, index1.Item2), lineColor, thickness);
                Imgproc.line(image, (index1.Item1, index1.Item2), (index2.Item1, index2.Item2), lineColor, thickness);
                Imgproc.line(image, (index2.Item1, index2.Item2), (index3.Item1, index3.Item2), lineColor, thickness);
                Imgproc.line(image, (index3.Item1, index3.Item2), (index4.Item1, index4.Item2), lineColor, thickness);

                Imgproc.line(image, (wrist.Item1, wrist.Item2), (middle1.Item1, middle1.Item2), lineColor, thickness);
                Imgproc.line(image, (middle1.Item1, middle1.Item2), (middle2.Item1, middle2.Item2), lineColor, thickness);
                Imgproc.line(image, (middle2.Item1, middle2.Item2), (middle3.Item1, middle3.Item2), lineColor, thickness);
                Imgproc.line(image, (middle3.Item1, middle3.Item2), (middle4.Item1, middle4.Item2), lineColor, thickness);

                Imgproc.line(image, (wrist.Item1, wrist.Item2), (ring1.Item1, ring1.Item2), lineColor, thickness);
                Imgproc.line(image, (ring1.Item1, ring1.Item2), (ring2.Item1, ring2.Item2), lineColor, thickness);
                Imgproc.line(image, (ring2.Item1, ring2.Item2), (ring3.Item1, ring3.Item2), lineColor, thickness);
                Imgproc.line(image, (ring3.Item1, ring3.Item2), (ring4.Item1, ring4.Item2), lineColor, thickness);

                Imgproc.line(image, (wrist.Item1, wrist.Item2), (pinky1.Item1, pinky1.Item2), lineColor, thickness);
                Imgproc.line(image, (pinky1.Item1, pinky1.Item2), (pinky2.Item1, pinky2.Item2), lineColor, thickness);
                Imgproc.line(image, (pinky2.Item1, pinky2.Item2), (pinky3.Item1, pinky3.Item2), lineColor, thickness);
                Imgproc.line(image, (pinky3.Item1, pinky3.Item2), (pinky4.Item1, pinky4.Item2), lineColor, thickness);

                if (is_draw_point)
                {
                    // # z value is relative to WRIST
                    for (int i = 0; i < landmarks.Length; i++)
                    {
                        ref readonly var p = ref landmarks[i];
                        int r = Mathf.Max((int)(5 - p.Item3 / 5), 0);
                        r = Mathf.Min(r, 14);
                        Imgproc.circle(image, (p.Item1, p.Item2), r, pointColor, -1);
                    }
                }
            }
        }

        /// <summary>
        /// Estimates hand pose in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for hand pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing estimation result (132 rows)
        ///
        /// The returned Mat format:
        /// - Single column containing 132 rows
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, handedness, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Output options:
        /// - useCopyOutput = false (default): Returns a reference to internal buffer (faster but unsafe across executions)
        /// - useCopyOutput = true: Returns a new copy of the result (thread-safe but slightly slower)
        ///
        /// For better performance in async scenarios, use EstimateAsync instead.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="palms">Palm detection result from MediaPipePalmDetector.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Estimate(Mat image, Mat palms, bool useCopyOutput = false)
        {
            Execute(image, palms);
            return useCopyOutput ? CopyOutput() : PeekOutput();
        }

        /// <summary>
        /// Estimates hand pose in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for hand pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing estimation result (132 rows)
        ///
        /// The returned Mat format:
        /// - Single column containing 132 rows
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, handedness, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Only one estimation operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="palms">Palm detection result from MediaPipePalmDetector.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual async Task<Mat> EstimateAsync(Mat image, Mat palms, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(image, palms, cancellationToken);
            return CopyOutput();
        }

        /// <summary>
        /// Converts the estimation result matrix to a HandPoseEstimationData structure.
        /// </summary>
        /// <param name="result">Estimation result matrix from Execute method.</param>
        /// <returns>HandPoseEstimationData structure containing hand pose information.</returns>
        public virtual HandPoseEstimationBlazeData ToStructuredData(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new HandPoseEstimationBlazeData();
            if (result.rows() < 132)
                throw new ArgumentException("Invalid result matrix. It must have at least 132 rows.");
            if (result.cols() != 1)
                throw new ArgumentException("Invalid result matrix. It must have 1 column.");
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            HandPoseEstimationBlazeData dst = Marshal.PtrToStructure<HandPoseEstimationBlazeData>((IntPtr)result.dataAddr());

            return dst;
        }

        protected override Mat[] RunCoreProcessing(Mat[] inputs)
        {
            ThrowIfDisposed();

            if (inputs == null || inputs.Length < 2)
                throw new ArgumentNullException(nameof(inputs), "Inputs cannot be null or have less than 2 elements.");

            if (inputs[0] == null)
                throw new ArgumentNullException(nameof(inputs), "inputs[0] cannot be null.");

            if (inputs[1] == null)
                throw new ArgumentNullException(nameof(inputs), "inputs[1] cannot be null.");

            Mat image = inputs[0];
            Mat palms = inputs[1];

            if (image != null) image.ThrowIfDisposed();
            if (image.channels() != 3)
                throw new ArgumentException("The input image must be in BGR format.");
            if (palms != null) palms.ThrowIfDisposed();
            if (palms != null && palms.rows() != 1)
                throw new ArgumentException("Invalid palms matrix. It must have 1 row.");
            if (palms != null && palms.cols() < 19)
                throw new ArgumentException("Invalid palms matrix. It must have at least 19 columns.");

            // Preprocess
            Mat rotated_palm_bbox;
            double angle;
            Mat rotation_matrix;
            Mat pad_bias;
            Mat inputBlob = PreProcess(image, palms, out rotated_palm_bbox, out angle, out rotation_matrix, out pad_bias);

            // Forward
            _handposeEstimationNet.setInput(inputBlob);
            List<Mat> outputBlobs = new List<Mat>();
            try
            {
                _handposeEstimationNet.forward(outputBlobs, _cachedUnconnectedOutLayersNames);
            }
            catch (Exception e)
            {
                inputBlob.Dispose();
                throw new ArgumentException(
                    "The input size specified in the constructor may not match the model's expected input size. " +
                    "Please verify the correct input size for your model and update the constructor parameters accordingly.", e);
            }

            // Postprocess
            Mat submat = PostProcess(outputBlobs, rotated_palm_bbox, angle, rotation_matrix, pad_bias,
                                         image.sizeAsValueTuple()); // submat of _output0Buffer is returned

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange

            rotated_palm_bbox.Dispose();
            rotation_matrix.Dispose();
            pad_bias.Dispose();

            inputBlob.Dispose();
            for (int i = 0; i < outputBlobs.Count; i++)
            {
                outputBlobs[i].Dispose();
            }

            return new Mat[] { submat }; // [bbox_coords, landmarks_coords, landmarks_coords_world, handedness, conf]
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image, Mat palm, out Mat rotated_palm_bbox, out double angle,
                                             out Mat rotation_matrix, out Mat pad_bias)
        {
            // '''
            // Rotate input for inference.
            // Parameters:
            //  image - input image of BGR channel order
            //  palm_bbox - palm bounding box found in image of format[[x1, y1], [x2, y2]] (top - left and bottom - right points)
            //            palm_landmarks - 7 landmarks(5 finger base points, 2 palm base points) of shape[7, 2]
            // Returns:
            //        rotated_hand - rotated hand image for inference
            //        rotate_palm_bbox - palm box of interest range
            //        angle - rotate angle for hand
            //        rotation_matrix - matrix for rotation and de - rotation
            //        pad_bias - pad pixels of interest range
            // '''

            // Generate an image with padding added after the squarify process.
            int maxSize = Math.Max(image.width(), image.height());
            int tmpImageSize = (int)(maxSize * 1.5);
            if (_tmpImage == null || _tmpImage.width() != tmpImageSize || _tmpImage.height() != tmpImageSize)
            {
                if (_tmpImage == null)
                    _tmpImage = new Mat();
                _tmpImage.create(tmpImageSize, tmpImageSize, image.type());
                _tmpImage.setTo(SCALAR_0);
                if (_tmpRotatedImage == null)
                    _tmpRotatedImage = new Mat();
                _tmpRotatedImage.create(tmpImageSize, tmpImageSize, image.type());
                _tmpRotatedImage.setTo(SCALAR_0);
            }

            int pad = (tmpImageSize - maxSize) / 2;
            pad_bias = new Mat(2, 1, CvType.CV_32FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            pad_bias.at<float>(0, 0)[0] = -pad;
            pad_bias.at<float>(1, 0)[0] = -pad;
#else
            pad_bias.put(0, 0, new float[] { -pad, -pad });
#endif
            using (Mat _tmpImage_roi = new Mat(_tmpImage, (pad, pad, image.width(), image.height())))
            {
                image.copyTo(_tmpImage_roi);
            }

            // Apply the pad_bias to palm_bbox and palm_landmarks.
            Mat new_palm = palm.clone();
            using (Mat new_palm_colRange_0_18 = new_palm.colRange((0, 18)))
            using (Mat palm_bbox_and_landmark = new_palm_colRange_0_18.reshape(2, 9))
            {
                Core.add(palm_bbox_and_landmark, (pad, pad, 0, 0), palm_bbox_and_landmark);
            }

            // Rotate input to have vertically oriented hand image
            // compute rotation
            Mat new_palm_colRange_0_4 = new_palm.colRange((0, 4));
            Mat palm_bbox = new_palm_colRange_0_4.reshape(1, 2);
            Mat new_palm_colRange_4_18 = new_palm.colRange((4, 18));
            Mat palm_landmarks = new_palm_colRange_4_18.reshape(1, 7);

            using (Mat p1 = palm_landmarks.row(PALM_LANDMARKS_INDEX_OF_PALM_BASE))
            using (Mat p2 = palm_landmarks.row(PALM_LANDMARKS_INDEX_OF_MIDDLE_FINGER_BASE))
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ReadOnlySpan<float> p1_arr = p1.AsSpan<float>();
#else
                float[] p1_arr = new float[2];
                p1.get(0, 0, p1_arr);
#endif
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ReadOnlySpan<float> p2_arr = p2.AsSpan<float>();
#else
                float[] p2_arr = new float[2];
                p2.get(0, 0, p2_arr);
#endif
                double radians = Math.PI / 2 - Math.Atan2(-(p2_arr[1] - p1_arr[1]), p2_arr[0] - p1_arr[0]);
                radians = radians - 2 * Math.PI * Math.Floor((radians + Math.PI) / (2 * Math.PI));
                angle = Mathf.Rad2Deg * radians;
            }

            // get bbox center
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> palm_bbox_arr = palm_bbox.AsSpan<float>();
#else
            float[] palm_bbox_arr = new float[4];
            palm_bbox.get(0, 0, palm_bbox_arr);
#endif
            Point center_palm_bbox = new Point((palm_bbox_arr[0] + palm_bbox_arr[2]) / 2,
                                                 (palm_bbox_arr[1] + palm_bbox_arr[3]) / 2);

            // get rotation matrix
            rotation_matrix = Imgproc.getRotationMatrix2D(center_palm_bbox, angle, 1.0);

            // get bounding boxes from rotated palm landmarks
            var rotated_palm_landmarks_points = new (float x, float y)[7];

            static float DotProduct(double a0, double a1, double a2, double b0, double b1, double b2)
            {
                return (float)(a0 * b0 + a1 * b1 + a2 * b2);
            }

            using (Mat rotated_palm_landmarks = new Mat(2, 7, CvType.CV_32FC1))
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ReadOnlySpan<float> palm_landmarks_data = palm_landmarks.AsSpan<float>();
                Span<float> rotated_palm_landmarks_data = rotated_palm_landmarks.AsSpan<float>();
                ReadOnlySpan<double> coords_rotation_matrix_data = rotation_matrix.AsSpan<double>();
#else
                float[] palm_landmarks_buffer = new float[7 * 2];
                palm_landmarks.get(0, 0, palm_landmarks_buffer);
                float[] palm_landmarks_data = palm_landmarks_buffer;
                float[] rotated_palm_landmarks_buffer = new float[7 * 2];
                float[] rotated_palm_landmarks_data = rotated_palm_landmarks_buffer;
                double[] coords_rotation_matrix_buffer = new double[6];
                rotation_matrix.get(0, 0, coords_rotation_matrix_buffer);
                double[] coords_rotation_matrix_data = coords_rotation_matrix_buffer;
#endif
                for (int i = 0; i < 7; ++i)
                {
                    double a_0 = palm_landmarks_data[i * 2];
                    double a_1 = palm_landmarks_data[i * 2 + 1];
                    const double a_2 = 1.0;
                    double b_0 = coords_rotation_matrix_data[0];
                    double b_1 = coords_rotation_matrix_data[1];
                    double b_2 = coords_rotation_matrix_data[2];
                    double x = DotProduct(a_0, a_1, a_2, b_0, b_1, b_2);
                    rotated_palm_landmarks_data[i] = (float)x;

                    b_0 = coords_rotation_matrix_data[3];
                    b_1 = coords_rotation_matrix_data[4];
                    b_2 = coords_rotation_matrix_data[5];
                    double y = DotProduct(a_0, a_1, a_2, b_0, b_1, b_2);
                    rotated_palm_landmarks_data[i + 7] = (float)y;

                    rotated_palm_landmarks_points[i] = ((float)x, (float)y);
                }
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                rotated_palm_landmarks.put(0, 0, rotated_palm_landmarks_buffer);
#endif
            }

            // get landmark bounding box
            MatOfPoint points = new MatOfPoint(rotated_palm_landmarks_points);
            OpenCVRect _rotated_palm_bbox = Imgproc.boundingRect(points);
            rotated_palm_bbox = new Mat(2, 2, CvType.CV_64FC1);

            // shift bounding box
            Point _rotated_palm_bbox_tl = _rotated_palm_bbox.tl();
            Point _rotated_palm_bbox_br = _rotated_palm_bbox.br();
            Point wh_rotated_palm_bbox = _rotated_palm_bbox_br - _rotated_palm_bbox_tl;
            Point shift_vector = new Point(PALM_BOX_SHIFT_VECTOR.x * wh_rotated_palm_bbox.x,
                                             PALM_BOX_SHIFT_VECTOR.y * wh_rotated_palm_bbox.y);
            _rotated_palm_bbox_tl = _rotated_palm_bbox_tl + shift_vector;
            _rotated_palm_bbox_br = _rotated_palm_bbox_br + shift_vector;

            // squarify bounding boxx
            Point center_rotated_plam_bbox = new Point((_rotated_palm_bbox_tl.x + _rotated_palm_bbox_br.x) / 2,
                                                         (_rotated_palm_bbox_tl.y + _rotated_palm_bbox_br.y) / 2);
            wh_rotated_palm_bbox = _rotated_palm_bbox_br - _rotated_palm_bbox_tl;
            double new_half_size = Math.Max(wh_rotated_palm_bbox.x, wh_rotated_palm_bbox.y) / 2.0;
            _rotated_palm_bbox_tl = new Point(center_rotated_plam_bbox.x - new_half_size,
                                                 center_rotated_plam_bbox.y - new_half_size);
            _rotated_palm_bbox_br = new Point(center_rotated_plam_bbox.x + new_half_size,
                                                 center_rotated_plam_bbox.y + new_half_size);

            // enlarge bounding box
            center_rotated_plam_bbox = new Point((_rotated_palm_bbox_tl.x + _rotated_palm_bbox_br.x) / 2,
                                                     (_rotated_palm_bbox_tl.y + _rotated_palm_bbox_br.y) / 2);
            wh_rotated_palm_bbox = _rotated_palm_bbox_br - _rotated_palm_bbox_tl;
            Point new_half_size2 = new Point(wh_rotated_palm_bbox.x * PALM_BOX_ENLARGE_FACTOR / 2.0,
                                                 wh_rotated_palm_bbox.y * PALM_BOX_ENLARGE_FACTOR / 2.0);
            OpenCVRect _rotated_palm_bbox_rect = new OpenCVRect((int)(center_rotated_plam_bbox.x - new_half_size2.x),
                                                                 (int)(center_rotated_plam_bbox.y - new_half_size2.y),
                                                                 (int)(new_half_size2.x * 2), (int)(new_half_size2.y * 2));
            _rotated_palm_bbox_tl = _rotated_palm_bbox_rect.tl();
            _rotated_palm_bbox_br = _rotated_palm_bbox_rect.br();
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> rotated_palm_bbox_arr = rotated_palm_bbox.AsSpan<double>();
            rotated_palm_bbox_arr[0] = _rotated_palm_bbox_tl.x;
            rotated_palm_bbox_arr[1] = _rotated_palm_bbox_tl.y;
            rotated_palm_bbox_arr[2] = _rotated_palm_bbox_br.x;
            rotated_palm_bbox_arr[3] = _rotated_palm_bbox_br.y;
#else
            rotated_palm_bbox.put(0, 0, new double[] { _rotated_palm_bbox_tl.x, _rotated_palm_bbox_tl.y,
                                                         _rotated_palm_bbox_br.x, _rotated_palm_bbox_br.y });
#endif

            // crop bounding box
#if NET_STANDARD_2_1
            ReadOnlySpan<int> diff = stackalloc int[] {
#else
            int[] diff = new int[] {
#endif
                    Math.Max((int)-_rotated_palm_bbox_tl.x, 0),
                    Math.Max((int)-_rotated_palm_bbox_tl.y, 0),
                    Math.Max((int)_rotated_palm_bbox_br.x - _tmpRotatedImage.width(), 0),
                    Math.Max((int)_rotated_palm_bbox_br.y - _tmpRotatedImage.height(), 0)
                };
            Point tl = new Point(_rotated_palm_bbox_tl.x + diff[0], _rotated_palm_bbox_tl.y + diff[1]);
            Point br = new Point(_rotated_palm_bbox_br.x + diff[2], _rotated_palm_bbox_br.y + diff[3]);
            OpenCVRect rotated_palm_bbox_rect = new OpenCVRect(tl, br);
            OpenCVRect rotated_image_rect = new OpenCVRect(0, 0, _tmpRotatedImage.width(), _tmpRotatedImage.height());


            Mat blob = null;

            // get rotated image
            OpenCVRect warp_roi_rect = rotated_image_rect.intersect(rotated_palm_bbox_rect);
            using (Mat _tmpImage_warp_roi = new Mat(_tmpImage, warp_roi_rect))
            using (Mat _tmpRotatedImage_warp_roi = new Mat(_tmpRotatedImage, warp_roi_rect))
            {
                Point warp_roi_center_palm_bbox = center_palm_bbox - warp_roi_rect.tl();
                using (Mat warp_roi_rotation_matrix = Imgproc.getRotationMatrix2D(warp_roi_center_palm_bbox, angle, 1.0))
                {
                    Imgproc.warpAffine(_tmpImage_warp_roi, _tmpRotatedImage_warp_roi, warp_roi_rotation_matrix,
                                         (_tmpImage_warp_roi.width(), _tmpImage_warp_roi.height()));
                }

                // get rotated_palm_bbox-size rotated image
                OpenCVRect crop_rect = rotated_image_rect.intersect(
                    new OpenCVRect(0, 0, (int)_rotated_palm_bbox_br.x - (int)_rotated_palm_bbox_tl.x,
                                     (int)_rotated_palm_bbox_br.y - (int)_rotated_palm_bbox_tl.y));
                using (Mat _tmpImage_crop_roi = new Mat(_tmpImage, crop_rect))
                {
                    _tmpImage_crop_roi.setTo(SCALAR_0);
                    OpenCVRect crop2_rect = rotated_image_rect.intersect(new OpenCVRect(diff[0], diff[1],
                                                         _tmpRotatedImage_warp_roi.width(), _tmpRotatedImage_warp_roi.height()));
                    using (Mat _tmpImage_crop2_roi = new Mat(_tmpImage, crop2_rect))
                    {
                        if (_tmpRotatedImage_warp_roi.width() == _tmpImage_crop2_roi.width()
                             && _tmpRotatedImage_warp_roi.height() == _tmpImage_crop2_roi.height())
                            _tmpRotatedImage_warp_roi.copyTo(_tmpImage_crop2_roi);
                    }

                    // Create a 4D blob from a frame.
                    int c = _tmpImage_crop_roi.channels();
                    int h = (int)INPUT_SIZE.height;
                    int w = (int)INPUT_SIZE.width;

                    // HWC to NHWC, BGR to RGB
                    blob = new Mat(new int[] { 1, h, w, c }, CvType.CV_32FC1);
                    using (Mat blob_HxW = blob.reshape(c, new int[] { h, w })) // [h, w]
                    {
                        if (_inputSizeMat == null)
                            _inputSizeMat = new Mat((w, h), CvType.CV_8UC3); // [h, w]

                        Imgproc.resize(_tmpImage_crop_roi, _inputSizeMat, (w, h));
                        Imgproc.cvtColor(_inputSizeMat, _inputSizeMat, Imgproc.COLOR_BGR2RGB);

                        _inputSizeMat.convertTo(blob_HxW, CvType.CV_32F, 1.0 / 255.0);
                    }
                }
            }

            points.Dispose();
            new_palm_colRange_0_4.Dispose();
            palm_bbox.Dispose();
            new_palm_colRange_4_18.Dispose();
            palm_landmarks.Dispose();
            new_palm.Dispose();

            return blob;
        }

        protected virtual Mat PostProcess(List<Mat> outputBlobs, Mat rotated_palm_bbox, double angle, Mat rotation_matrix, Mat pad_bias,
                                             (double width, double height) img_size)
        {
            Mat landmarks = outputBlobs[0];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            float conf = outputBlobs[1].at<float>(0, 0)[0];
            float handedness = outputBlobs[2].at<float>(0, 0)[0];
#else
            float conf = (float)outputBlobs[1].get(0, 0)[0];
            float handedness = (float)outputBlobs[2].get(0, 0)[0];
#endif
            Mat landmarks_world = outputBlobs[3];

            if (conf < _confThreshold)
                return new Mat();

            landmarks = landmarks.reshape(1, 21); // shape: (1, 63) -> (21, 3)
            landmarks_world = landmarks_world.reshape(1, 21); // shape: (1, 63) -> (21, 3)

            // transform coords back to the input coords
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> rotated_palm_bbox_arr = rotated_palm_bbox.AsSpan<double>();
#else
            double[] rotated_palm_bbox_arr = new double[4];
            rotated_palm_bbox.get(0, 0, rotated_palm_bbox_arr);
#endif
            Point _rotated_palm_bbox_tl = new Point(rotated_palm_bbox_arr[0], rotated_palm_bbox_arr[1]);
            Point _rotated_palm_bbox_br = new Point(rotated_palm_bbox_arr[2], rotated_palm_bbox_arr[3]);
            Point wh_rotated_palm_bbox = _rotated_palm_bbox_br - _rotated_palm_bbox_tl;
            Point scale_factor = new Point(wh_rotated_palm_bbox.x / INPUT_SIZE.width, wh_rotated_palm_bbox.y / INPUT_SIZE.height);

            using (Mat _landmarks_21x1_c3 = landmarks.reshape(3, 21))
            {
                Core.subtract(_landmarks_21x1_c3, (INPUT_SIZE.width / 2.0, INPUT_SIZE.height / 2.0, 0, 0), _landmarks_21x1_c3);
                double max_scale_factor = Math.Max(scale_factor.x, scale_factor.y);
                Core.multiply(_landmarks_21x1_c3, (scale_factor.x, scale_factor.y, max_scale_factor, 0), _landmarks_21x1_c3); //  # depth scaling
            }

            Mat coords_rotation_matrix = Imgproc.getRotationMatrix2D((0, 0), angle, 1.0);

            static float DotProduct(double a0, double a1, double b0, double b1) => (float)(a0 * b0 + a1 * b1);

            Mat rotated_landmarks = landmarks.clone();
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> coords_rotation_matrix_data = coords_rotation_matrix.AsSpan<double>();
            ReadOnlySpan<float> landmarks_data = landmarks.AsSpan<float>();
            Span<float> rotated_landmarks_data = rotated_landmarks.AsSpan<float>();
#else
            double[] coords_rotation_matrix_buffer = new double[6];
            coords_rotation_matrix.get(0, 0, coords_rotation_matrix_buffer);
            double[] coords_rotation_matrix_data = coords_rotation_matrix_buffer;
            float[] landmarks_buffer = new float[21 * 3];
            landmarks.get(0, 0, landmarks_buffer);
            float[] landmarks_data = landmarks_buffer;
            float[] rotated_landmarks_buffer = new float[21 * 3];
            rotated_landmarks.get(0, 0, rotated_landmarks_buffer);
            float[] rotated_landmarks_data = rotated_landmarks_buffer;
#endif
            for (int i = 0; i < 21; ++i)
            {
                int index = i * 3;

                double a_0 = landmarks_data[index];
                double a_1 = landmarks_data[index + 1];
                double b_0 = coords_rotation_matrix_data[0];
                double b_1 = coords_rotation_matrix_data[3];
                rotated_landmarks_data[index] = DotProduct(a_0, a_1, b_0, b_1);

                b_0 = coords_rotation_matrix_data[1];
                b_1 = coords_rotation_matrix_data[4];
                rotated_landmarks_data[index + 1] = DotProduct(a_0, a_1, b_0, b_1);
            }
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            rotated_landmarks.put(0, 0, rotated_landmarks_buffer);
#endif

            Mat rotated_landmarks_world = landmarks_world.clone();
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> rotated_landmarks_world_data = rotated_landmarks_world.AsSpan<float>();
#else
            float[] rotated_landmarks_world_buffer = new float[21 * 3];
            rotated_landmarks_world.get(0, 0, rotated_landmarks_world_buffer);
            float[] rotated_landmarks_world_data = rotated_landmarks_world_buffer;
#endif
            for (int i = 0; i < 21; ++i)
            {
                int index = i * 3;

                double a_0 = rotated_landmarks_world_data[index];
                double a_1 = rotated_landmarks_world_data[index + 1];
                double b_0 = coords_rotation_matrix_data[0];
                double b_1 = coords_rotation_matrix_data[3];
                rotated_landmarks_world_data[index] = DotProduct(a_0, a_1, b_0, b_1);

                b_0 = coords_rotation_matrix_data[1];
                b_1 = coords_rotation_matrix_data[4];
                rotated_landmarks_world_data[index + 1] = DotProduct(a_0, a_1, b_0, b_1);
            }
#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            rotated_landmarks_world.put(0, 0, rotated_landmarks_world_buffer);
#endif

            // invert rotation
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> rotation_matrix_arr = rotation_matrix.AsSpan<double>();
#else
            double[] rotation_matrix_arr = new double[6];
            rotation_matrix.get(0, 0, rotation_matrix_arr);
#endif
            Mat rotation_component = new Mat(2, 2, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> rotation_component_arr = rotation_component.AsSpan<double>();
            rotation_component_arr[0] = rotation_matrix_arr[0];
            rotation_component_arr[1] = rotation_matrix_arr[3];
            rotation_component_arr[2] = rotation_matrix_arr[1];
            rotation_component_arr[3] = rotation_matrix_arr[4];
#else
            rotation_component.put(0, 0, new double[] { rotation_matrix_arr[0], rotation_matrix_arr[3],
                                                         rotation_matrix_arr[1], rotation_matrix_arr[4] });
#endif
            Mat translation_component = new Mat(2, 1, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> translation_component_arr = translation_component.AsSpan<double>();
            translation_component_arr[0] = rotation_matrix_arr[2];
            translation_component_arr[1] = rotation_matrix_arr[5];
#else
            translation_component.put(0, 0, new double[] { rotation_matrix_arr[2], rotation_matrix_arr[5] });
#endif
            Mat inverted_translation = new Mat(2, 1, CvType.CV_64FC1);
            using (Mat rotation_component_row_0 = rotation_component.row(0))
            using (Mat translation_component_reshape_1_1 = translation_component.reshape(1, 1))
            using (Mat rotation_component_row_1 = rotation_component.row(1))
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                Span<double> inverted_translation_arr = inverted_translation.AsSpan<double>();
                inverted_translation_arr[0] = -rotation_component_row_0.dot(translation_component_reshape_1_1);
                inverted_translation_arr[1] = -rotation_component_row_1.dot(translation_component_reshape_1_1);
#else
                inverted_translation.put(0, 0, new double[] { -rotation_component_row_0.dot(translation_component_reshape_1_1),
                                                                 -rotation_component_row_1.dot(translation_component_reshape_1_1) });
#endif
            }

            using (Mat inverse_rotation_matrix = new Mat(2, 3, CvType.CV_64FC1))
            {
                using (Mat inverse_rotation_matrix_colRange_0_2 = inverse_rotation_matrix.colRange((0, 2)))
                {
                    rotation_component.copyTo(inverse_rotation_matrix_colRange_0_2);
                }
                using (Mat inverse_rotation_matrix_colRange_2_3 = inverse_rotation_matrix.colRange((2, 3)))
                {
                    inverted_translation.copyTo(inverse_rotation_matrix_colRange_2_3);
                }

                // get box center
                using (Mat center = new Mat(3, 1, CvType.CV_64FC1))
                {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                    Span<double> center_arr = center.AsSpan<double>();
                    center_arr[0] = (rotated_palm_bbox_arr[0] + rotated_palm_bbox_arr[2]) / 2.0;
                    center_arr[1] = (rotated_palm_bbox_arr[1] + rotated_palm_bbox_arr[3]) / 2.0;
                    center_arr[2] = 1.0;
#else
                    center.put(0, 0, new double[] { (rotated_palm_bbox_arr[0] + rotated_palm_bbox_arr[2]) / 2.0,
                                                         (rotated_palm_bbox_arr[1] + rotated_palm_bbox_arr[3]) / 2.0, 1.0 });
#endif
                    using (Mat original_center = new Mat(2, 1, CvType.CV_64FC1))
                    {
                        using (Mat inverse_rotation_matrix_row0 = inverse_rotation_matrix.row(0))
                        using (Mat center_reshape_1_1 = center.reshape(1, 1))
                        using (Mat inverse_rotation_matrix_row1 = inverse_rotation_matrix.row(1))
                        {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                            Span<double> original_center_arr = original_center.AsSpan<double>();
                            original_center_arr[0] = inverse_rotation_matrix_row0.dot(center_reshape_1_1);
                            original_center_arr[1] = inverse_rotation_matrix_row1.dot(center_reshape_1_1);
#else
                            original_center.put(0, 0, new double[] { inverse_rotation_matrix_row0.dot(center_reshape_1_1),
                                                                         inverse_rotation_matrix_row1.dot(center_reshape_1_1) });
#endif
                        }

                        using (Mat rotated_landmark_reshape_3_21 = rotated_landmarks.reshape(3, 21))
                        using (Mat landmarks_reshape_3_21 = landmarks.reshape(3, 21))
                        {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                            double original_center_0_0 = original_center.at<double>(0, 0)[0];
                            float pad_bias_0_0 = pad_bias.at<float>(0, 0)[0];
                            double original_center_1_0 = original_center.at<double>(1, 0)[0];
                            float pad_bias_1_0 = pad_bias.at<float>(1, 0)[0];
#else
                            double original_center_0_0 = original_center.get(0, 0)[0];
                            float pad_bias_0_0 = (float)pad_bias.get(0, 0)[0];
                            double original_center_1_0 = original_center.get(1, 0)[0];
                            float pad_bias_1_0 = (float)pad_bias.get(1, 0)[0];
#endif
                            Core.add(rotated_landmark_reshape_3_21
                                , (original_center_0_0 + pad_bias_0_0, original_center_1_0 + pad_bias_1_0, 0, 0)
                                , landmarks_reshape_3_21);
                        }
                    }
                }
            }

            // get bounding box from rotated_landmarks
            var landmarks_points = new (float x, float y)[21];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> landmarks_data2 = landmarks.AsSpan<float>();
#else
            float[] landmarks_buffer2 = new float[21 * 3];
            landmarks.get(0, 0, landmarks_buffer2);
            float[] landmarks_data2 = landmarks_buffer2;
#endif
            for (int i = 0; i < 21; ++i)
            {
                landmarks_points[i] = (landmarks_data2[i * 3], landmarks_data2[i * 3 + 1]);
            }
            MatOfPoint points = new MatOfPoint(landmarks_points);
            OpenCVRect bbox = Imgproc.boundingRect(points);

            // shift bounding box
            Point wh_bbox = bbox.br() - bbox.tl();
            Point shift_vector = new Point(HAND_BOX_SHIFT_VECTOR.x * wh_bbox.x, HAND_BOX_SHIFT_VECTOR.y * wh_bbox.y);
            bbox = bbox + shift_vector;

            // enlarge bounding box
            Point center_bbox = new Point((bbox.tl().x + bbox.br().x) / 2, (bbox.tl().y + bbox.br().y) / 2);
            wh_bbox = bbox.br() - bbox.tl();
            Point new_half_size = new Point(wh_bbox.x * HAND_BOX_ENLARGE_FACTOR / 2.0, wh_bbox.y * HAND_BOX_ENLARGE_FACTOR / 2.0);
            bbox = new OpenCVRect(new Point(center_bbox.x - new_half_size.x, center_bbox.y - new_half_size.y),
                                     new Point(center_bbox.x + new_half_size.x, center_bbox.y + new_half_size.y));


            Mat result = null;

            lock (_lockObject)
            {
                result = _output0Buffer.rowRange(0, _output0Buffer.rows());

                float img_size_w = (float)img_size.width;
                float img_size_h = (float)img_size.height;

                float x1 = Mathf.Clamp((float)bbox.tl().x, 0, img_size_w);
                float y1 = Mathf.Clamp((float)bbox.tl().y, 0, img_size_h);
                float x2 = Mathf.Clamp((float)bbox.br().x, 0, img_size_w);
                float y2 = Mathf.Clamp((float)bbox.br().y, 0, img_size_h);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                Span<float> result_arr = result.AsSpan<float>();
                result_arr[0] = x1;
                result_arr[1] = y1;
                result_arr[2] = x2;
                result_arr[3] = y2;
#else
                result.put(0, 0, new float[] { x1, y1, x2, y2 });
#endif
                using (Mat result_col4_67_21x3 = result.rowRange((4, 67)))
                using (Mat result_col4_67_21x3_reshape_1_21 = result_col4_67_21x3.reshape(1, 21))
                using (Mat landmarks_colRange_0_3 = landmarks.colRange((0, 3)))
                {
                    landmarks_colRange_0_3.copyTo(result_col4_67_21x3_reshape_1_21);
                }
                using (Mat result_col67_130_21x3 = result.rowRange((67, 130)))
                using (Mat result_col67_130_21x3_reshape_1_21 = result_col67_130_21x3.reshape(1, 21))
                using (Mat rotated_landmarks_world_colRange_0_3 = rotated_landmarks_world.colRange((0, 3)))
                {
                    rotated_landmarks_world_colRange_0_3.copyTo(result_col67_130_21x3_reshape_1_21);
                }
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                result.at<float>(130, 0)[0] = handedness;
                result.at<float>(131, 0)[0] = conf;
#else
                result.put(130, 0, new float[] { handedness });
                result.put(131, 0, new float[] { conf });
#endif

            }

            landmarks.Dispose();
            landmarks_world.Dispose();
            coords_rotation_matrix.Dispose();
            rotated_landmarks.Dispose();
            rotated_landmarks_world.Dispose();
            rotation_component.Dispose();
            translation_component.Dispose();
            inverted_translation.Dispose();
            points.Dispose();

            // # [0: 4]: hand bounding box found in image of format [x1, y1, x2, y2] (top-left and bottom-right points)
            // # [4: 67]: screen landmarks with format [x1, y1, z1, x2, y2 ... x21, y21, z21], z value is relative to WRIST
            // # [67: 130]: world landmarks with format [x1, y1, z1, x2, y2 ... x21, y21, z21], 3D metric x, y, z coordinate
            // # [130]: handedness, (left)[0, 1](right) hand
            // # [131]: confidence
            return result; // np.r_[bbox.reshape(-1), landmarks.reshape(-1), rotated_landmarks_world.reshape(-1), handedness[0][0], conf]
        }


        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _handposeEstimationNet?.Dispose(); _handposeEstimationNet = null;
                _tmpImage?.Dispose(); _tmpImage = null;
                _tmpRotatedImage?.Dispose(); _tmpRotatedImage = null;
                _inputSizeMat?.Dispose(); _inputSizeMat = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;
            }

            base.Dispose(disposing);
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct HandPoseEstimationBlazeData
        {
            public readonly float X1;
            public readonly float Y1;
            public readonly float X2;
            public readonly float Y2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LANDMARK_ELEMENT_COUNT)]
            private readonly float[] _rawLandmarksScreen;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LANDMARK_ELEMENT_COUNT)]
            private readonly float[] _rawLandmarksWorld;

            public readonly float Handedness;
            public readonly float Confidence;

            public const int LANDMARK_VEC3F_COUNT = 21;
            public const int LANDMARK_ELEMENT_COUNT = 3 * LANDMARK_VEC3F_COUNT;
            public const int ELEMENT_COUNT = 4 + LANDMARK_ELEMENT_COUNT + LANDMARK_ELEMENT_COUNT + 2;
            public const int DATA_SIZE = ELEMENT_COUNT * 4;

            public HandPoseEstimationBlazeData(float x1, float y1, float x2, float y2,
                                                 Vec3f[] landmarksScreen, Vec3f[] landmarksWorld,
                                                 float handedness, float confidence)
            {
                if (landmarksScreen == null || landmarksScreen.Length != LANDMARK_VEC3F_COUNT)
                    throw new ArgumentException("landmarksScreen must be a Vec3f[" + LANDMARK_VEC3F_COUNT + "]");
                if (landmarksWorld == null || landmarksWorld.Length != LANDMARK_VEC3F_COUNT)
                    throw new ArgumentException("landmarksWorld must be a Vec3f[" + LANDMARK_VEC3F_COUNT + "]");

                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
                _rawLandmarksScreen = new float[LANDMARK_ELEMENT_COUNT];
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    int offset = i * 3;
                    ref readonly var landmark = ref landmarksScreen[i];
                    _rawLandmarksScreen[offset + 0] = landmark.Item1;
                    _rawLandmarksScreen[offset + 1] = landmark.Item2;
                    _rawLandmarksScreen[offset + 2] = landmark.Item3;
                }
                _rawLandmarksWorld = new float[LANDMARK_ELEMENT_COUNT];
                for (int i = 0; i < landmarksWorld.Length; i++)
                {
                    int offset = i * 3;
                    ref readonly var landmark = ref landmarksWorld[i];
                    _rawLandmarksWorld[offset + 0] = landmark.Item1;
                    _rawLandmarksWorld[offset + 1] = landmark.Item2;
                    _rawLandmarksWorld[offset + 2] = landmark.Item3;
                }
                Handedness = handedness;
                Confidence = confidence;
            }

#if NET_STANDARD_2_1

            public readonly ReadOnlySpan<Vec3f> GetLandmarksScreen()
            {
                return MemoryMarshal.Cast<float, Vec3f>(_rawLandmarksScreen.AsSpan());
            }

            public readonly ReadOnlySpan<Vec3f> GetLandmarksWorld()
            {
                return MemoryMarshal.Cast<float, Vec3f>(_rawLandmarksWorld.AsSpan());
            }

#endif

            public readonly Vec3f[] GetLandmarksScreenArray()
            {
                Vec3f[] landmarks = new Vec3f[LANDMARK_VEC3F_COUNT];
                for (int i = 0; i < landmarks.Length; i++)
                {
                    int offset = i * 3;
                    landmarks[i] = new Vec3f(_rawLandmarksScreen[offset + 0],
                                                 _rawLandmarksScreen[offset + 1],
                                                 _rawLandmarksScreen[offset + 2]);
                }
                return landmarks;
            }

            public readonly Vec3f[] GetLandmarksWorldArray()
            {
                Vec3f[] landmarks = new Vec3f[LANDMARK_VEC3F_COUNT];
                for (int i = 0; i < landmarks.Length; i++)
                {
                    int offset = i * 3;
                    landmarks[i] = new Vec3f(_rawLandmarksWorld[offset + 0],
                                                 _rawLandmarksWorld[offset + 1],
                                                 _rawLandmarksWorld[offset + 2]);
                }
                return landmarks;
            }

            public readonly override string ToString()
            {
                StringBuilder sb = new StringBuilder(1536);

                sb.Append("HandPoseEstimationBlazeData(");
                sb.AppendFormat("X1:{0} Y1:{1} X2:{2} Y2:{3} ", X1, Y1, X2, Y2);

                sb.Append("LandmarksScreen:");
#if NET_STANDARD_2_1
                ReadOnlySpan<Vec3f> landmarksScreen = GetLandmarksScreen();
#else
                Vec3f[] landmarksScreen = GetLandmarksScreenArray();
#endif
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    ref readonly var p = ref landmarksScreen[i];
                    sb.Append(p.ToString());
                }
                sb.Append(" ");

                sb.Append("LandmarksWorld:");
#if NET_STANDARD_2_1
                ReadOnlySpan<Vec3f> landmarksWorld = GetLandmarksWorld();
#else
                Vec3f[] landmarksWorld = GetLandmarksWorldArray();
#endif
                for (int i = 0; i < landmarksWorld.Length; i++)
                {
                    ref readonly var p = ref landmarksWorld[i];
                    sb.Append(p.ToString());
                }
                sb.Append(" ");

                sb.AppendFormat("Handedness:{0},({1}) ", Handedness, Handedness <= 0.5f ? "Left" : "Right");
                sb.AppendFormat("Confidence:{0}", Confidence);
                sb.Append(")");
                return sb.ToString();
            }
        }
    }
}
#endif
