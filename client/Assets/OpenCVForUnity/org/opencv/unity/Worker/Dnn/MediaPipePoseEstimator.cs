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
using OpenCVRange = OpenCVForUnity.CoreModule.Range;
using OpenCVRect = OpenCVForUnity.CoreModule.Rect;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Referring to:
    /// https://github.com/opencv/opencv_zoo/tree/main/models/pose_estimation_mediapipe
    /// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
    ///
    /// [Tested Models]
    /// https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/pose_estimation_mediapipe/pose_estimation_mediapipe_2023mar.onnx
    /// </summary>
    public class MediaPipePoseEstimator : ProcessingWorkerBase
    {
        public enum KeyPoint : byte
        {
            Nose, LeftEyeInner, LeftEye, LeftEyeOuter, RightEyeInner, RightEye, RightEyeOuter, LeftEar, RightEar,
            MouthLeft, MouthRight,
            LeftShoulder, RightShoulder, LeftElbow, RightElbow, LeftWrist, RightWrist, LeftPinky, RightPinky, LeftIndex, RightIndex, LeftThumb, RightThumb,
            LeftHip, RightHip, LeftKnee, RightKnee, LeftAnkle, RightAnkle, LeftHeel, RightHeel, LeftFootIndex, RightFootIndex
        }

        protected static readonly Size INPUT_SIZE = new Size(256, 256);
        protected static readonly int[] HEATMAT_SHAPE = new int[] { 64, 64, 39 };
        protected static readonly OpenCVRange[] HEATMAT_SHAPE_RANGE = new OpenCVRange[] {
            new OpenCVRange(0, HEATMAT_SHAPE[0]),
            new OpenCVRange(0, HEATMAT_SHAPE[1]),
            new OpenCVRange(0, HEATMAT_SHAPE[2])
        };
        protected static readonly Scalar SCALAR_WHITE = new Scalar(255, 255, 255, 255);
        protected static readonly Scalar SCALAR_RED = new Scalar(0, 0, 255, 255);
        protected static readonly Scalar SCALAR_GREEN = new Scalar(0, 255, 0, 255);
        protected static readonly Scalar SCALAR_BLUE = new Scalar(255, 0, 0, 255);
        protected static readonly Scalar SCALAR_0 = new Scalar(0, 0, 0, 0);

        // # RoI will be larger so the performance will be better, but preprocess will be slower. Default to 1.
        protected const double PERSON_BOX_PRE_ENLARGE_FACTOR = 1.0;
        protected const double PERSON_BOX_ENLARGE_FACTOR = 1.25;

        protected float _confThreshold;
        protected int _backend;
        protected int _target;
        protected Net _poseEstimationNet;
        protected List<string> _cachedUnconnectedOutLayersNames;
        protected Mat _tmpImage;
        protected Mat _tmpRotatedImage;
        protected Mat _inputSizeMat;
        protected Mat _mask_warp;
        protected Mat _invert_rotation_mask_32F;
        protected Mat _colorMat;
        protected Mat _output0Buffer;
        protected Mat _output1Buffer;
        protected Mat _output2Buffer;
        protected Mat[] _cachedEstimatinMethodInputsFlags = new Mat[] { new Mat(), new Mat() };

        /// <summary>
        /// Initializes a new instance of the MediaPipePoseEstimator class.
        /// </summary>
        /// <param name="modelFilepath">Path to the model file.</param>
        /// <param name="confThreshold">Confidence threshold for filtering estimations.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipePoseEstimator(string modelFilepath, float confThreshold = 0.5f,
                                         int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(modelFilepath))
                throw new ArgumentException("Model filepath cannot be empty.", nameof(modelFilepath));

            _confThreshold = Mathf.Clamp01(confThreshold);
            _backend = backend;
            _target = target;

            try
            {
                _poseEstimationNet = Dnn.readNet(modelFilepath);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN model. Invalid model file path or corrupted model file.", e);
            }
            _poseEstimationNet.setPreferableBackend(_backend);
            _poseEstimationNet.setPreferableTarget(_target);
            _cachedUnconnectedOutLayersNames = _poseEstimationNet.getUnconnectedOutLayersNames();

            _output0Buffer = new Mat(317, 1, CvType.CV_32FC1);
            _output1Buffer = new Mat();
            _output2Buffer = new Mat(HEATMAT_SHAPE, CvType.CV_32FC1);
        }

        /// <summary>
        /// Visualizes pose estimation result (bbox and landmarks) on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">The result (bbox and landmarks) Mat returned by Estimate method.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return;
            if (result.rows() < 317)
                throw new ArgumentException("Invalid result matrix. It must have at least 317 rows.");

            const int auxiliary_points_num = 6;
            PoseEstimationBlazeData data = ToStructuredData(result);

            float left = data.X1;
            float top = data.Y1;
            float right = data.X2;
            float bottom = data.Y2;

#if NET_STANDARD_2_1
            ReadOnlySpan<ScreenLandmark> landmarksScreen = data.GetLandmarksScreen();
            ReadOnlySpan<Vec3f> landmarksWorld = data.GetLandmarksWorld();
#else
            ScreenLandmark[] landmarksScreen = data.GetLandmarksScreenArray();
            Vec3f[] landmarksWorld = data.GetLandmarksWorldArray();
#endif

            float confidence = data.Confidence;

            var lineColor = SCALAR_WHITE.ToValueTuple();
            var pointColor = (isRGB) ? SCALAR_BLUE.ToValueTuple() : SCALAR_RED.ToValueTuple();

            // # draw box
            Imgproc.rectangle(image, (left, top), (right, bottom), SCALAR_GREEN.ToValueTuple(), 2);
            Imgproc.putText(image, confidence.ToString("F3"), (left, top + 12), Imgproc.FONT_HERSHEY_DUPLEX, 0.5, pointColor);

            // # Draw line between each key points
            draw_lines(landmarksScreen, true);

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(1536);
                sb.Append("-----------pose-----------");
                sb.AppendLine();
                sb.AppendFormat("Confidence: {0:F4}", confidence);
                sb.AppendLine();
                sb.AppendFormat("Person Box: ({0:F3}, {1:F3}, {2:F3}, {3:F3})", left, top, right, bottom);
                sb.AppendLine();
                sb.Append("Pose LandmarksScreen: ");
                sb.Append("{");
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    ref readonly var p = ref landmarksScreen[i];
                    sb.AppendFormat("({0:F3}, {1:F3}, {2:F3}, {3:F3}, {4:F3})", p.X, p.Y, p.Z, p.Visibility, p.Presence);
                    if (i < landmarksScreen.Length - 1)
                        sb.Append(", ");
                }
                sb.Append("}");
                sb.AppendLine();
                sb.Append("Pose LandmarksWorld: ");
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
            void draw_lines(ReadOnlySpan<ScreenLandmark> landmarks, bool is_draw_point = true)
#else
            void draw_lines(ScreenLandmark[] landmarks, bool is_draw_point = true)
#endif
            {
                float presenceThreshold = 0.8f;

                void draw_by_presence(in ScreenLandmark p1, in ScreenLandmark p2)
                {
                    if (p1.Presence >= presenceThreshold && p2.Presence >= presenceThreshold)
                    {
                        Imgproc.line(image, (p1.X, p1.Y), (p2.X, p2.Y), lineColor, 2);
                    }
                }

                // Draw line between each key points
                ref readonly var nose = ref landmarks[(int)KeyPoint.Nose];
                ref readonly var leftEyeInner = ref landmarks[(int)KeyPoint.LeftEyeInner];
                ref readonly var leftEye = ref landmarks[(int)KeyPoint.LeftEye];
                ref readonly var leftEyeOuter = ref landmarks[(int)KeyPoint.LeftEyeOuter];
                ref readonly var leftEar = ref landmarks[(int)KeyPoint.LeftEar];
                ref readonly var rightEyeInner = ref landmarks[(int)KeyPoint.RightEyeInner];
                ref readonly var rightEye = ref landmarks[(int)KeyPoint.RightEye];
                ref readonly var rightEyeOuter = ref landmarks[(int)KeyPoint.RightEyeOuter];
                ref readonly var rightEar = ref landmarks[(int)KeyPoint.RightEar];
                ref readonly var mouthLeft = ref landmarks[(int)KeyPoint.MouthLeft];
                ref readonly var mouthRight = ref landmarks[(int)KeyPoint.MouthRight];
                ref readonly var rightShoulder = ref landmarks[(int)KeyPoint.RightShoulder];
                ref readonly var rightElbow = ref landmarks[(int)KeyPoint.RightElbow];
                ref readonly var rightWrist = ref landmarks[(int)KeyPoint.RightWrist];
                ref readonly var rightThumb = ref landmarks[(int)KeyPoint.RightThumb];
                ref readonly var rightPinky = ref landmarks[(int)KeyPoint.RightPinky];
                ref readonly var rightIndex = ref landmarks[(int)KeyPoint.RightIndex];
                ref readonly var leftShoulder = ref landmarks[(int)KeyPoint.LeftShoulder];
                ref readonly var leftElbow = ref landmarks[(int)KeyPoint.LeftElbow];
                ref readonly var leftWrist = ref landmarks[(int)KeyPoint.LeftWrist];
                ref readonly var leftThumb = ref landmarks[(int)KeyPoint.LeftThumb];
                ref readonly var leftIndex = ref landmarks[(int)KeyPoint.LeftIndex];
                ref readonly var leftPinky = ref landmarks[(int)KeyPoint.LeftPinky];
                ref readonly var leftHip = ref landmarks[(int)KeyPoint.LeftHip];
                ref readonly var rightHip = ref landmarks[(int)KeyPoint.RightHip];
                ref readonly var rightKnee = ref landmarks[(int)KeyPoint.RightKnee];
                ref readonly var rightAnkle = ref landmarks[(int)KeyPoint.RightAnkle];
                ref readonly var rightHeel = ref landmarks[(int)KeyPoint.RightHeel];
                ref readonly var rightFootIndex = ref landmarks[(int)KeyPoint.RightFootIndex];
                ref readonly var leftKnee = ref landmarks[(int)KeyPoint.LeftKnee];
                ref readonly var leftAnkle = ref landmarks[(int)KeyPoint.LeftAnkle];
                ref readonly var leftFootIndex = ref landmarks[(int)KeyPoint.LeftFootIndex];
                ref readonly var leftHeel = ref landmarks[(int)KeyPoint.LeftHeel];

                draw_by_presence(nose, leftEyeInner);
                draw_by_presence(leftEyeInner, leftEye);
                draw_by_presence(leftEye, leftEyeOuter);
                draw_by_presence(leftEyeOuter, leftEar);
                draw_by_presence(nose, rightEyeInner);
                draw_by_presence(rightEyeInner, rightEye);
                draw_by_presence(rightEye, rightEyeOuter);
                draw_by_presence(rightEyeOuter, rightEar);

                draw_by_presence(mouthLeft, mouthRight);

                draw_by_presence(rightShoulder, rightElbow);
                draw_by_presence(rightElbow, rightWrist);
                draw_by_presence(rightWrist, rightThumb);
                draw_by_presence(rightWrist, rightPinky);
                draw_by_presence(rightWrist, rightIndex);
                draw_by_presence(rightPinky, rightIndex);

                draw_by_presence(leftShoulder, leftElbow);
                draw_by_presence(leftElbow, leftWrist);
                draw_by_presence(leftWrist, leftThumb);
                draw_by_presence(leftWrist, leftIndex);
                draw_by_presence(leftWrist, leftPinky);

                draw_by_presence(leftShoulder, rightShoulder);
                draw_by_presence(leftShoulder, leftHip);
                draw_by_presence(leftHip, rightHip);
                draw_by_presence(rightHip, rightShoulder);

                draw_by_presence(rightHip, rightKnee);
                draw_by_presence(rightKnee, rightAnkle);
                draw_by_presence(rightAnkle, rightHeel);
                draw_by_presence(rightAnkle, rightFootIndex);
                draw_by_presence(rightHeel, rightFootIndex);

                draw_by_presence(leftHip, leftKnee);
                draw_by_presence(leftKnee, leftAnkle);
                draw_by_presence(leftAnkle, leftFootIndex);
                draw_by_presence(leftAnkle, leftHeel);
                draw_by_presence(leftHeel, leftFootIndex);

                if (is_draw_point)
                {
                    // # z value is relative to HIP, but we use constant to instead
                    for (int i = 0; i < landmarks.Length - auxiliary_points_num; ++i)
                    {
                        ref readonly var landmark = ref landmarks[i];
                        if (landmark.Presence > presenceThreshold)
                            Imgproc.circle(image, (landmark.X, landmark.Y), 2, pointColor, -1);
                    }
                }
            }
        }

        /// <summary>
        /// Visualizes pose estimation result (mask) on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="mask">The result (mask) Mat returned by PeekOutput(1) or CopyOutput(1) method.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public virtual void VisualizeMask(Mat image, Mat mask, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            if (mask.empty())
                return;
            if ((image.width(), image.height()) != (mask.width(), mask.height()) && mask.type() == CvType.CV_8UC1)
                throw new ArgumentException("The input image and mask must have the same size.");

            var color = SCALAR_GREEN.ToValueTuple();

            Imgproc.Canny(mask, mask, 100, 200);
            using (Mat kernel = Mat.ones(2, 2, CvType.CV_8UC1))// # expansion edge to 2 pixels
            {
                Imgproc.dilate(mask, mask, kernel, (0, 0), 1);
            }

            if (_colorMat == null || _colorMat.width() != image.width() || _colorMat.height() != image.height())
            {
                if (_colorMat == null)
                    _colorMat = new Mat();
                _colorMat.create(image.height(), image.width(), image.type());
                _colorMat.setTo(color);
            }

            _colorMat.copyTo(image, mask);
        }

        /// <summary>
        /// Estimates pose in the input image.
        /// </summary>
        /// <remarks>
        /// This is a specialized method for pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing estimation result (317 rows)
        ///
        /// The returned Mat format:
        /// - Single column containing 317 rows
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Output options:
        /// - useCopyOutput = false (default): Returns a reference to internal buffer (faster but unsafe across executions)
        /// - useCopyOutput = true: Returns a new copy of the result (thread-safe but slightly slower)
        ///
        /// For better performance in async scenarios, use EstimateAsync instead.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="persons">Person detection result from MediaPipePersonDetector.</param>
        /// <param name="useMask">Whether to use mask. Use PeekOutput(1) or CopyOutput(1) to get the mask result.</param>
        /// <param name="useHeatmap">Whether to use heatmap. Use PeekOutput(2) or CopyOutput(2) to get the heatmap result.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Estimate(Mat image, Mat persons, bool useMask = false, bool useHeatmap = false, bool useCopyOutput = false)
        {
            Execute(new Mat[] { image, persons,
                                useMask ? _cachedEstimatinMethodInputsFlags[0] : null, useHeatmap ? _cachedEstimatinMethodInputsFlags[1] : null });
            return useCopyOutput ? CopyOutput() : PeekOutput();
        }

        /// <summary>
        /// Estimates pose in the input image asynchronously.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing estimation result (317 rows)
        ///
        /// The returned Mat format:
        /// - Single column containing 317 rows
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Only one estimation operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="persons">Person detection result from MediaPipePersonDetector.</param>
        /// <param name="useMask">Whether to use mask. Use PeekOutput(1) or CopyOutput(1) to get the mask result.</param>
        /// <param name="useHeatmap">Whether to use heatmap. Use PeekOutput(2) or CopyOutput(2) to get the heatmap result.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual async Task<Mat> EstimateAsync(Mat image, Mat persons, bool useMask = false, bool useHeatmap = false, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(new Mat[] { image, persons,
                                            useMask ? _cachedEstimatinMethodInputsFlags[0] : null,
                                            useHeatmap ? _cachedEstimatinMethodInputsFlags[1] : null },
                                            cancellationToken);
            return CopyOutput();
        }

        /// <summary>
        /// Converts the estimation result matrix to a PoseEstimationData structure.
        /// </summary>
        /// <param name="result">Estimation result matrix from Execute method.</param>
        /// <returns>PoseEstimationData structure containing pose estimation information.</returns>
        public virtual PoseEstimationBlazeData ToStructuredData(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new PoseEstimationBlazeData();
            if (result.rows() < 317)
                throw new ArgumentException("Invalid result matrix. It must have at least 317 rows.");
            if (result.cols() != 1)
                throw new ArgumentException("Invalid result matrix. It must have 1 column.");
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            PoseEstimationBlazeData dst = Marshal.PtrToStructure<PoseEstimationBlazeData>((IntPtr)(result.dataAddr()));

            return dst;
        }

        /// <summary>
        /// Gets screen landmarks from landmarks array by removing auxiliary points.
        /// The last 6 points in the raw landmark array are auxiliary points used internally
        /// by the model and are not part of the actual pose landmarks, so they are removed.
        /// </summary>
        /// <param name="landmarks">Landmarks array containing pose landmarks and auxiliary points.</param>
        /// <returns>ScreenLandmark array containing only the actual pose landmarks (without auxiliary points).</returns>
        public virtual ScreenLandmark[] GetScreenLandmarks(ScreenLandmark[] landmarks)
        {
            ThrowIfDisposed();

            if (landmarks == null)
                throw new ArgumentNullException(nameof(landmarks));
            if (landmarks.Length < 39)
                throw new ArgumentException("Invalid landmarks array. It must have at least 39 elements.");

            // Remove 6 unneeded auxiliary points at the end of the raw landmark array.
            ScreenLandmark[] newArr = new ScreenLandmark[landmarks.Length - 6];
            Array.Copy(landmarks, 0, newArr, 0, newArr.Length);

            return newArr;
        }

        /// <summary>
        /// Gets world landmarks from landmarks array by removing auxiliary points.
        /// The last 6 points in the raw landmark array are auxiliary points used internally
        /// by the model and are not part of the actual pose landmarks, so they are removed.
        /// </summary>
        /// <param name="landmarks">Landmarks array containing pose landmarks and auxiliary points.</param>
        /// <returns>Vector3 array containing only the actual pose landmarks (without auxiliary points).</returns>
        public virtual Vector3[] GetWorldLandmarks(Vector3[] landmarks)
        {
            ThrowIfDisposed();

            if (landmarks == null)
                throw new ArgumentNullException(nameof(landmarks));
            if (landmarks.Length < 39)
                throw new ArgumentException("Invalid landmarks array. It must have at least 39 elements.");

            // Remove 6 unneeded auxiliary points at the end of the raw landmark array.
            Vector3[] newArr = new Vector3[landmarks.Length - 6];
            Array.Copy(landmarks, 0, newArr, 0, newArr.Length);

            return newArr;
        }

        /// <summary>
        /// Gets keep landmarks from landmarks array.
        /// </summary>
        /// <param name="landmarks">Landmarks array.</param>
        /// <param name="presence">Presence threshold.</param>
        /// <returns>Boolean array containing keep landmarks.</returns>
        public virtual bool[] GetKeepLandmarks(ScreenLandmark[] landmarks, float presence = 0.8f)
        {
            ThrowIfDisposed();

            if (landmarks == null)
                throw new ArgumentNullException(nameof(landmarks));

            bool[] keep = new bool[landmarks.Length];

            for (int i = 0; i < landmarks.Length; ++i)
            {
                keep[i] = landmarks[i].Presence > presence;
            }

            return keep;
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
            Mat persons = inputs[1];
            bool useMask = inputs.Length > 2 && inputs[2] != null;
            bool useHeatmap = inputs.Length > 3 && inputs[3] != null;

            if (image != null) image.ThrowIfDisposed();
            if (image.channels() != 3)
                throw new ArgumentException("The input image must be in BGR format.");
            if (persons != null) persons.ThrowIfDisposed();
            if (persons != null && persons.rows() != 1)
                throw new ArgumentException("Invalid persons matrix. It must have 1 row.");
            if (persons != null && persons.cols() < 13)
                throw new ArgumentException("Invalid persons matrix. It must have at least 13 columns.");

            // Preprocess
            Mat rotated_person_bbox;
            double angle;
            Mat rotation_matrix;
            Mat pad_bias;
            Mat inputBlob = PreProcess(image, persons, out rotated_person_bbox, out angle, out rotation_matrix, out pad_bias);

            // Forward
            _poseEstimationNet.setInput(inputBlob);
            List<Mat> outputBlobs = new List<Mat>();
            try
            {
                _poseEstimationNet.forward(outputBlobs, _cachedUnconnectedOutLayersNames);
            }
            catch (Exception e)
            {
                inputBlob.Dispose();
                throw new ArgumentException(
                    "The input size specified in the constructor may not match the model's expected input size. " +
                    "Please verify the correct input size for your model and update the constructor parameters accordingly.", e);
            }

            // Postprocess
            Mat box_landmark_conf = PostProcess(outputBlobs, rotated_person_bbox, angle, rotation_matrix,
                                                     pad_bias, image.sizeAsValueTuple()); // submat of _output0Buffer is returned

            Mat mask = null;
            if (useMask)
            {
                mask = PostProcessMask(outputBlobs, rotated_person_bbox, angle, rotation_matrix,
                                             pad_bias, image.sizeAsValueTuple()); // submat of _output1Buffer is returned
            }
            else
            {
                mask = new Mat();
            }

            Mat heatmap = null;
            if (useHeatmap)
            {
                // # 64*64*39 heatmap: currently only used for refining landmarks, requires sigmod processing before use
                // # TODO: refine landmarks with heatmap. reference: https://github.com/tensorflow/tfjs-models/blob/master/pose-detection/src/blazepose_tfjs/detector.ts#L577-L582
                using (Mat outputBlobs_3_reshape_64_64_39 = outputBlobs[3].reshape(1, HEATMAT_SHAPE))
                {
                    lock (_lockObject)
                    {
                        heatmap = _output2Buffer.submat(HEATMAT_SHAPE_RANGE);
                        outputBlobs_3_reshape_64_64_39.copyTo(heatmap); // shape: (1, 64, 64, 39) -> (64, 64, 39)
                    }
                }
            }
            else
            {
                heatmap = new Mat();
            }

            // Any rewriting of buffer data must be done within the lock statement
            // Do not return the buffer itself because it will be destroyed,
            // but return a submat of the same size as the result extracted using rowRange

            rotated_person_bbox.Dispose();
            rotation_matrix.Dispose();
            pad_bias.Dispose();

            inputBlob.Dispose();
            for (int i = 0; i < outputBlobs.Count; i++)
            {
                outputBlobs[i].Dispose();
            }

            // box_landmark_conf = [bbox_coords, landmarks_coords, landmarks_coords_world, conf]
            // mask = (optional) [invert_rotation_mask]
            // heatmap = (optional) [heatmap]
            return new Mat[] { box_landmark_conf, mask, heatmap };
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual Mat PreProcess(Mat image, Mat person, out Mat rotated_person_bbox, out double angle,
                                             out Mat rotation_matrix, out Mat pad_bias)
        {
            // '''
            // Rotate input for inference.
            // Parameters:
            //   image - input image of BGR channel order
            //   face_bbox - human face bounding box found in image of format[[x1, y1], [x2, y2]] (top - left and bottom - right points)
            //   person_landmarks - 4 landmarks(2 full body points, 2 upper body points) of shape[4, 2]
            // Returns:
            //   rotated_person - rotated person image for inference
            //   rotate_person_bbox - person box of interest range
            //   angle - rotate angle for person
            //   rotation_matrix - matrix for rotation and de - rotation
            //   pad_bias - pad pixels of interest range
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

            // Apply the pad_bias to person_bbox and person_landmarks.
            Mat new_person = person.clone();
            Mat new_person_colRange_0_12 = new_person.colRange((0, 12));
            Mat person_bbox_and_landmark = new_person_colRange_0_12.reshape(2, 6);
            Core.add(person_bbox_and_landmark, (pad, pad, 0, 0), person_bbox_and_landmark);

            // # crop and pad image to interest range
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> person_keypoints = stackalloc float[8];
#else
            float[] person_keypoints = new float[8];
#endif
            person_bbox_and_landmark.get(2, 0, person_keypoints);

            Point mid_hip_point = new Point(person_keypoints[0], person_keypoints[1]);
            Point full_body_point = new Point(person_keypoints[2], person_keypoints[3]);

            // # get RoI
            Mat full_body_vector = new Mat(1, 1, CvType.CV_32FC2, (mid_hip_point.x - full_body_point.x,
                                                                     mid_hip_point.y - full_body_point.y, 0, 0));
            double full_dist = Core.norm(full_body_vector);
            OpenCVRect full_bbox_rect = new OpenCVRect(
                new Point((float)(mid_hip_point.x - full_dist), (float)(mid_hip_point.y - full_dist)),
                new Point((float)(mid_hip_point.x + full_dist), (float)(mid_hip_point.y + full_dist)));

            // # enlarge to make sure full body can be cover
            Point center_bbox = mid_hip_point;
            Point wh_bbox = full_bbox_rect.br() - full_bbox_rect.tl();
            Point new_half_size = wh_bbox * PERSON_BOX_PRE_ENLARGE_FACTOR / 2;
            full_bbox_rect = new OpenCVRect(
                center_bbox - new_half_size,
                center_bbox + new_half_size);

            // Rotate input to have vertically oriented person image
            // compute rotation
            using (Mat p1 = person_bbox_and_landmark.row(2)) // mid_hip_point
            using (Mat p2 = person_bbox_and_landmark.row(3)) // full_body_point
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

            // get rotation matrix
            rotation_matrix = Imgproc.getRotationMatrix2D(center_bbox, angle, 1.0);

            // # get landmark bounding box
            Point _rotated_person_bbox_tl = full_bbox_rect.tl();
            Point _rotated_person_bbox_br = full_bbox_rect.br();
            rotated_person_bbox = new Mat(2, 2, CvType.CV_64FC1);
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<double> rotated_person_bbox_arr = rotated_person_bbox.AsSpan<double>();
            rotated_person_bbox_arr[0] = _rotated_person_bbox_tl.x;
            rotated_person_bbox_arr[1] = _rotated_person_bbox_tl.y;
            rotated_person_bbox_arr[2] = _rotated_person_bbox_br.x;
            rotated_person_bbox_arr[3] = _rotated_person_bbox_br.y;
#else
            rotated_person_bbox.put(0, 0, new double[] { _rotated_person_bbox_tl.x, _rotated_person_bbox_tl.y,
                                                             _rotated_person_bbox_br.x, _rotated_person_bbox_br.y });
#endif

            // crop bounding box
#if NET_STANDARD_2_1
            ReadOnlySpan<int> diff = stackalloc int[] {
#else
            int[] diff = new int[] {
#endif
                    Math.Max((int)-_rotated_person_bbox_tl.x, 0),
                    Math.Max((int)-_rotated_person_bbox_tl.y, 0),
                    Math.Max((int)_rotated_person_bbox_br.x - _tmpRotatedImage.width(), 0),
                    Math.Max((int)_rotated_person_bbox_br.y - _tmpRotatedImage.height(), 0)
                };
            Point tl = new Point(_rotated_person_bbox_tl.x + diff[0], _rotated_person_bbox_tl.y + diff[1]);
            Point br = new Point(_rotated_person_bbox_br.x + diff[2], _rotated_person_bbox_br.y + diff[3]);
            OpenCVRect rotated_person_bbox_rect = new OpenCVRect(tl, br);
            OpenCVRect rotated_image_rect = new OpenCVRect(0, 0, _tmpRotatedImage.width(), _tmpRotatedImage.height());


            Mat blob = null;

            // get rotated image
            OpenCVRect warp_roi_rect = rotated_image_rect.intersect(rotated_person_bbox_rect);
            using (Mat _tmpImage_warp_roi = new Mat(_tmpImage, warp_roi_rect))
            using (Mat _tmpRotatedImage_warp_roi = new Mat(_tmpRotatedImage, warp_roi_rect))
            {
                Point warp_roi_center_palm_bbox = center_bbox - warp_roi_rect.tl();
                using (Mat warp_roi_rotation_matrix = Imgproc.getRotationMatrix2D(warp_roi_center_palm_bbox, angle, 1.0))
                {
                    Imgproc.warpAffine(_tmpImage_warp_roi, _tmpRotatedImage_warp_roi, warp_roi_rotation_matrix,
                                         (_tmpImage_warp_roi.width(), _tmpImage_warp_roi.height()));
                }

                // get rotated_person_bbox-size rotated image
                OpenCVRect crop_rect = rotated_image_rect.intersect(
                    new OpenCVRect(0, 0, (int)_rotated_person_bbox_br.x - (int)_rotated_person_bbox_tl.x,
                                     (int)_rotated_person_bbox_br.y - (int)_rotated_person_bbox_tl.y));
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

            full_body_vector.Dispose();
            new_person_colRange_0_12.Dispose();
            person_bbox_and_landmark.Dispose();
            new_person.Dispose();

            return blob;
        }

        protected virtual Mat PostProcess(List<Mat> outputBlobs, Mat rotated_person_bbox, double angle,
                                             Mat rotation_matrix, Mat pad_bias, (double width, double height) img_size)
        {
            Mat landmarks = outputBlobs[0];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            float conf = outputBlobs[1].at<float>(0, 0)[0];
#else
            float conf = (float)outputBlobs[1].get(0, 0)[0];
#endif
            Mat landmarks_world = outputBlobs[4];

            if (conf < _confThreshold)
                return new Mat();

            landmarks = landmarks.reshape(1, 39); // shape: (1, 195) -> (39, 5)
            landmarks_world = landmarks_world.reshape(1, 39); // shape: (1, 117) -> (39, 3)

            // # recover sigmoid score
            using (Mat _ladmarls_col3_5 = landmarks.colRange((3, 5)))
            {
                Sigmoid(_ladmarls_col3_5);
            }

            Mat landmarks_colRange_0_3 = landmarks.colRange((0, 3));
            Mat _ladmarks_col0_3 = landmarks_colRange_0_3.clone();

            // transform coords back to the input coords
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> rotated_person_bbox_arr = rotated_person_bbox.AsSpan<double>();
#else
            double[] rotated_person_bbox_arr = new double[4];
            rotated_person_bbox.get(0, 0, rotated_person_bbox_arr);
#endif
            Point _rotated_palm_bbox_tl = new Point(rotated_person_bbox_arr[0], rotated_person_bbox_arr[1]);
            Point _rotated_palm_bbox_br = new Point(rotated_person_bbox_arr[2], rotated_person_bbox_arr[3]);
            Point wh_rotated_person_bbox = _rotated_palm_bbox_br - _rotated_palm_bbox_tl;
            Point scale_factor = new Point(wh_rotated_person_bbox.x / INPUT_SIZE.width, wh_rotated_person_bbox.y / INPUT_SIZE.height);

            using (Mat _landmarks_39x1_c3 = _ladmarks_col0_3.reshape(3, 39))
            {
                Core.subtract(_landmarks_39x1_c3, (INPUT_SIZE.width / 2.0, INPUT_SIZE.height / 2.0, 0, 0), _landmarks_39x1_c3);
                double max_scale_factor = Math.Max(scale_factor.x, scale_factor.y);
                Core.multiply(_landmarks_39x1_c3, (scale_factor.x, scale_factor.y, max_scale_factor, 0), _landmarks_39x1_c3); //  # depth scaling
            }

            using (Mat _ladmarks_colRange_0_3 = landmarks.colRange((0, 3)))
            {
                _ladmarks_col0_3.copyTo(_ladmarks_colRange_0_3);
            }

            Mat coords_rotation_matrix = Imgproc.getRotationMatrix2D((0, 0), angle, 1.0);

            static float DotProduct(double a0, double a1, double b0, double b1)
            {
                return (float)(a0 * b0 + a1 * b1);
            }

            Mat rotated_landmarks = landmarks.clone();
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> coords_rotation_matrix_data = coords_rotation_matrix.AsSpan<double>();
            ReadOnlySpan<float> landmarks_data = landmarks.AsSpan<float>();
            Span<float> rotated_landmarks_data = rotated_landmarks.AsSpan<float>();
#else
            double[] coords_rotation_matrix_buffer = new double[6];
            coords_rotation_matrix.get(0, 0, coords_rotation_matrix_buffer);
            double[] coords_rotation_matrix_data = coords_rotation_matrix_buffer;
            float[] landmarks_buffer = new float[39 * 5];
            landmarks.get(0, 0, landmarks_buffer);
            float[] landmarks_data = landmarks_buffer;
            float[] rotated_landmarks_buffer = new float[39 * 5];
            rotated_landmarks.get(0, 0, rotated_landmarks_buffer);
            float[] rotated_landmarks_data = rotated_landmarks_buffer;
#endif
            for (int i = 0; i < 39; ++i)
            {
                int index = i * 5;

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
            ReadOnlySpan<float> landmarks_world_data = landmarks_world.AsSpan<float>();
            Span<float> rotated_landmarks_world_data = rotated_landmarks_world.AsSpan<float>();
#else
            float[] landmarks_world_buffer = new float[39 * 3];
            landmarks_world.get(0, 0, landmarks_world_buffer);
            float[] landmarks_world_data = landmarks_world_buffer;
            float[] rotated_landmarks_world_buffer = new float[39 * 3];
            rotated_landmarks_world.get(0, 0, rotated_landmarks_world_buffer);
            float[] rotated_landmarks_world_data = rotated_landmarks_world_buffer;
#endif
            for (int i = 0; i < 39; ++i)
            {
                int index = i * 3;

                double a_0 = landmarks_world_data[index];
                double a_1 = landmarks_world_data[index + 1];
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

            Mat inverse_rotation_matrix = new Mat(2, 3, CvType.CV_64FC1);
            rotation_component.copyTo(inverse_rotation_matrix.colRange((0, 2)));
            inverted_translation.copyTo(inverse_rotation_matrix.colRange((2, 3)));

            // get box center
            using (Mat center = new Mat(3, 1, CvType.CV_64FC1))
            {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                Span<double> center_arr = center.AsSpan<double>();
                center_arr[0] = (rotated_person_bbox_arr[0] + rotated_person_bbox_arr[2]) / 2.0;
                center_arr[1] = (rotated_person_bbox_arr[1] + rotated_person_bbox_arr[3]) / 2.0;
                center_arr[2] = 1.0;
#else
                center.put(0, 0, new double[] { (rotated_person_bbox_arr[0] + rotated_person_bbox_arr[2]) / 2.0,
                                                     (rotated_person_bbox_arr[1] + rotated_person_bbox_arr[3]) / 2.0, 1.0 });
#endif
                using (Mat original_center = new Mat(2, 1, CvType.CV_64FC1))
                {
                    using (Mat inverse_rotation_matrix_row_0 = inverse_rotation_matrix.row(0))
                    using (Mat center_reshape_1_1 = center.reshape(1, 1))
                    using (Mat inverse_rotation_matrix_row_1 = inverse_rotation_matrix.row(1))
                    {
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                        Span<double> original_center_arr = original_center.AsSpan<double>();
                        original_center_arr[0] = inverse_rotation_matrix_row_0.dot(center_reshape_1_1);
                        original_center_arr[1] = inverse_rotation_matrix_row_1.dot(center_reshape_1_1);
#else
                        original_center.put(0, 0, new double[] { inverse_rotation_matrix_row_0.dot(center_reshape_1_1),
                                                                 inverse_rotation_matrix_row_1.dot(center_reshape_1_1) });
#endif
                    }

                    using (Mat rotated_landmarks_colRange_0_3 = rotated_landmarks.colRange((0, 3)))
                    using (Mat _rotated_landmarks_col0_3 = rotated_landmarks_colRange_0_3.clone())
                    using (Mat _rotated_landmarks_col0_3_reshape_3_39 = _rotated_landmarks_col0_3.reshape(3, 39))
                    using (Mat _ladmarks_col0_3_reshape_3_39 = _ladmarks_col0_3.reshape(3, 39))
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
                        Core.add(_rotated_landmarks_col0_3_reshape_3_39
                            , (original_center_0_0 + pad_bias_0_0, original_center_1_0 + pad_bias_1_0, 0, 0)
                            , _ladmarks_col0_3_reshape_3_39);

                        _rotated_landmarks_col0_3.copyTo(rotated_landmarks_colRange_0_3);
                        _rotated_landmarks_col0_3.Dispose();
                        _ladmarks_col0_3.copyTo(landmarks_colRange_0_3);
                    }
                }
            }

            // get bounding box from rotated_landmarks
            var landmarks_points = new (float x, float y)[39];
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<float> landmarks_data2 = landmarks.AsSpan<float>();
#else
            float[] landmarks_buffer2 = new float[39 * 5];
            landmarks.get(0, 0, landmarks_buffer2);
            float[] landmarks_data2 = landmarks_buffer2;
#endif
            for (int i = 0; i < 39; ++i)
            {
                landmarks_points[i] = (landmarks_data2[i * 5], landmarks_data2[i * 5 + 1]);
            }
            MatOfPoint points = new MatOfPoint(landmarks_points);
            OpenCVRect bbox = Imgproc.boundingRect(points);
            Point center_bbox = (bbox.tl() + bbox.br()) / 2;
            Point wh_bbox = bbox.br() - bbox.tl();
            Point new_half_size = wh_bbox * PERSON_BOX_ENLARGE_FACTOR / 2;
            bbox = new OpenCVRect(
                center_bbox - new_half_size,
                center_bbox + new_half_size);


            // # [0: 4]: person bounding box found in image of format [x1, y1, x2, y2] (top-left and bottom-right points)
            // # [4: 199]: screen landmarks with format [x1, y1, z1, v1, p1, x2, y2 ... x39, y39, z39, v39, p39], z value is relative to HIP
            // # [199: 316]: world landmarks with format [x1, y1, z1, x2, y2 ... x39, y39, z39], 3D metric x, y, z coordinate
            // # [316]: confidence

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
                using (Mat result_rowRange_4_199 = result.rowRange((4, 199)))
                using (Mat result_col4_199_39x5 = result_rowRange_4_199.reshape(1, 39))
                {
                    landmarks.colRange((0, 5)).copyTo(result_col4_199_39x5);
                }
                using (Mat result_rowRange_199_316 = result.rowRange((199, 316)))
                using (Mat result_col199_316_39x3 = result_rowRange_199_316.reshape(1, 39))
                {
                    rotated_landmarks_world.colRange((0, 3)).copyTo(result_col199_316_39x3);
                }
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                result.at<float>(316, 0)[0] = conf;
#else
                result.put(316, 0, new float[] { conf });
#endif

            }

            landmarks.Dispose();
            landmarks_world.Dispose();
            landmarks_colRange_0_3.Dispose();
            _ladmarks_col0_3.Dispose();
            coords_rotation_matrix.Dispose();
            rotated_landmarks.Dispose();
            rotated_landmarks_world.Dispose();
            rotation_component.Dispose();
            translation_component.Dispose();
            inverted_translation.Dispose();
            inverse_rotation_matrix.Dispose();
            points.Dispose();

            // # 2*2 person bbox: [[x1, y1], [x2, y2]]
            // # 39*5 screen landmarks: 33 keypoints and 6 auxiliary points with [x, y, z, visibility, presence], z value is relative to HIP
            // #    Visibility is probability that a keypoint is located within the frame and not occluded by another bigger body part or another object
            // #    Presence is probability that a keypoint is located within the frame
            // # 39*3 world landmarks: 33 keypoints and 6 auxiliary points with [x, y, z] 3D metric x, y, z coordinate
            // # conf: confidence of prediction
            return result;
        }

        protected virtual Mat PostProcessMask(List<Mat> outputBlob, Mat rotated_person_bbox, double angle,
                                                 Mat rotation_matrix, Mat pad_bias, (double width, double height) img_size)
        {
            Mat mask = outputBlob[2].reshape(1, 256); // shape: (1, 256, 256, 1) -> (256, 256)

            if (_mask_warp == null)
                _mask_warp = new Mat(mask.rows(), mask.cols(), CvType.CV_32FC1);

            if (_invert_rotation_mask_32F == null || _invert_rotation_mask_32F.width()
                 != img_size.width || _invert_rotation_mask_32F.height() != img_size.height)
            {
                if (_invert_rotation_mask_32F == null)
                    _invert_rotation_mask_32F = new Mat();
                _invert_rotation_mask_32F.create(img_size, CvType.CV_32FC1);
                _invert_rotation_mask_32F.setTo(SCALAR_0);
            }

            // # invert rotation for mask
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> rotated_person_bbox_arr = rotated_person_bbox.AsSpan<double>();
#else
            double[] rotated_person_bbox_arr = new double[4];
            rotated_person_bbox.get(0, 0, rotated_person_bbox_arr);
#endif
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            float pad_bias_0_0 = pad_bias.at<float>(0, 0)[0];
            float pad_bias_1_0 = pad_bias.at<float>(1, 0)[0];
#else
            float pad_bias_0_0 = (float)pad_bias.get(0, 0)[0];
            float pad_bias_1_0 = (float)pad_bias.get(1, 0)[0];
#endif
            Point _rotated_person_bbox_tl = new Point(rotated_person_bbox_arr[0] + pad_bias_0_0,
                                                         rotated_person_bbox_arr[1] + pad_bias_1_0);
            Point _rotated_person_bbox_br = new Point(rotated_person_bbox_arr[2] + pad_bias_0_0,
                                                         rotated_person_bbox_arr[3] + pad_bias_1_0);
            using (Mat invert_rotation_matrix = Imgproc.getRotationMatrix2D((mask.width() / 2, mask.height() / 2), -angle, 1.0))
            {
                Imgproc.warpAffine(mask, _mask_warp, invert_rotation_matrix, (mask.width(), mask.height()));
            }

            // create invert_rotation_mask (32F)
            // crop bounding box
#if NET_STANDARD_2_1
            ReadOnlySpan<int> diff = stackalloc int[] {
#else
            int[] diff = new int[] {
#endif
                    Math.Max((int)-_rotated_person_bbox_tl.x, 0),
                    Math.Max((int)-_rotated_person_bbox_tl.y, 0),
                    Math.Max((int)_rotated_person_bbox_br.x - _invert_rotation_mask_32F.width(), 0),
                    Math.Max((int)_rotated_person_bbox_br.y - _invert_rotation_mask_32F.height(), 0)
                };
            Point wh_rotated_person_bbox = _rotated_person_bbox_br - _rotated_person_bbox_tl;
            var scale_factor = (wh_rotated_person_bbox.x / INPUT_SIZE.width, wh_rotated_person_bbox.y / INPUT_SIZE.height);
            int x = (int)Math.Round(diff[0] / scale_factor.Item1);
            int y = (int)Math.Round(diff[1] / scale_factor.Item2);
            int w = Math.Min((int)Math.Round((wh_rotated_person_bbox.x - diff[0] - diff[2]) / scale_factor.Item1), _mask_warp.width());
            int h = Math.Min((int)Math.Round((wh_rotated_person_bbox.y - diff[1] - diff[3]) / scale_factor.Item2), _mask_warp.height());

            var mask_warp_crop_rect = (x, y, w, h);
            Mat _mask_warp_crop_roi = new Mat(_mask_warp, mask_warp_crop_rect);
            OpenCVRect rotated_person_bbox_rect = new OpenCVRect(_rotated_person_bbox_tl, _rotated_person_bbox_br);
            OpenCVRect invert_rotation_mask_32F_rect = new OpenCVRect(0, 0, _invert_rotation_mask_32F.width(), _invert_rotation_mask_32F.height());
            OpenCVRect invert_rotation_mask_32F_crop_rect = invert_rotation_mask_32F_rect.intersect(rotated_person_bbox_rect);
            Mat _invert_rotation_mask_32F_crop_roi = new Mat(_invert_rotation_mask_32F, invert_rotation_mask_32F_crop_rect);
            Imgproc.resize(_mask_warp_crop_roi, _invert_rotation_mask_32F_crop_roi,
                (_invert_rotation_mask_32F_crop_roi.width(), _invert_rotation_mask_32F_crop_roi.height()));

            // # binarize mask
            Imgproc.threshold(_invert_rotation_mask_32F_crop_roi, _invert_rotation_mask_32F_crop_roi, 0, 255, Imgproc.THRESH_BINARY);

            // create invert_rotation_mask (8U)
            Mat invert_rotation_mask = null;
            lock (_lockObject)
            {
                if (_output1Buffer.width() != img_size.width || _output1Buffer.height() != img_size.height)
                {
                    _output1Buffer.create(img_size, CvType.CV_8UC1);
                }
                _output1Buffer.setTo(SCALAR_0);
                invert_rotation_mask = _output1Buffer.rowRange(0, _output1Buffer.rows());

                using (Mat _invert_rotation_mask_crop_roi = new Mat(invert_rotation_mask, invert_rotation_mask_32F_crop_rect))
                {
                    _invert_rotation_mask_32F_crop_roi.convertTo(_invert_rotation_mask_crop_roi, CvType.CV_8U);
                }
            }

            mask.Dispose();
            _mask_warp_crop_roi.Dispose();
            _invert_rotation_mask_32F_crop_roi.Dispose();

            // # img_size.width*img_size.height img_height*img_width mask: gray mask, where 255 indicates the full body of a person and 0 means background
            return invert_rotation_mask;
        }

        protected virtual void Sigmoid(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            //python: 1 / (1 + np.exp(-x))

            Core.multiply(mat, (-1, -1, -1, -1), mat);  // -x
            Core.exp(mat, mat);                         // exp(-x)
            Core.add(mat, (1, 1, 1, 1), mat);           // 1 + exp(-x)
            Core.divide(1.0, mat, mat);                 // 1 / (1 + exp(-x))
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _poseEstimationNet?.Dispose(); _poseEstimationNet = null;
                _tmpImage?.Dispose(); _tmpImage = null;
                _tmpRotatedImage?.Dispose(); _tmpRotatedImage = null;
                _inputSizeMat?.Dispose(); _inputSizeMat = null;
                _mask_warp?.Dispose(); _mask_warp = null;
                _invert_rotation_mask_32F?.Dispose(); _invert_rotation_mask_32F = null;
                _colorMat?.Dispose(); _colorMat = null;
                _output0Buffer?.Dispose(); _output0Buffer = null;
                _output1Buffer?.Dispose(); _output1Buffer = null;
                _output2Buffer?.Dispose(); _output2Buffer = null;
                foreach (var flag in _cachedEstimatinMethodInputsFlags)
                {
                    flag?.Dispose();
                }
                _cachedEstimatinMethodInputsFlags = null;
            }

            base.Dispose(disposing);
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct ScreenLandmark
        {
            public readonly float X;
            public readonly float Y;
            public readonly float Z;
            public readonly float Visibility;
            public readonly float Presence;

            public const int ELEMENT_COUNT = 5;
            public const int DATA_SIZE = ELEMENT_COUNT * 4;

            public ScreenLandmark(float x, float y, float z, float visibility, float presence)
            {
                X = x;
                Y = y;
                Z = z;
                Visibility = visibility;
                Presence = presence;
            }

            public readonly override string ToString()
            {
                return $"ScreenLandmark(X:{X} Y:{Y} Z:{Z} Visibility:{Visibility} Presence:{Presence})";
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct PoseEstimationBlazeData
        {
            public readonly float X1;
            public readonly float Y1;
            public readonly float X2;
            public readonly float Y2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LANDMARK_SCREEN_ELEMENT_COUNT)]
            public readonly float[] _rawLandmarksScreen;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LANDMARK_WORLD_ELEMENT_COUNT)]
            public readonly float[] _rawLandmarksWorld;

            public readonly float Confidence;

            public const int LANDMARK_SCREEN_COUNT = 39;
            public const int LANDMARK_WORLD_COUNT = 39;
            public const int LANDMARK_SCREEN_ELEMENT_COUNT = 5 * LANDMARK_SCREEN_COUNT;
            public const int LANDMARK_WORLD_ELEMENT_COUNT = 3 * LANDMARK_WORLD_COUNT;
            public const int ELEMENT_COUNT = 4 + LANDMARK_SCREEN_ELEMENT_COUNT + LANDMARK_WORLD_ELEMENT_COUNT + 1;
            public const int DATA_SIZE = ELEMENT_COUNT * 4;

            public PoseEstimationBlazeData(float x1, float y1, float x2, float y2,
                                            ScreenLandmark[] landmarksScreen, Vec3f[] landmarksWorld,
                                            float confidence)
            {
                if (landmarksScreen == null || landmarksScreen.Length != LANDMARK_SCREEN_COUNT)
                    throw new ArgumentException("landmarksScreen must be a ScreenLandmark[" + LANDMARK_SCREEN_COUNT + "]");
                if (landmarksWorld == null || landmarksWorld.Length != LANDMARK_WORLD_COUNT)
                    throw new ArgumentException("landmarksWorld must be a Vec3f[" + LANDMARK_WORLD_COUNT + "]");

                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
                _rawLandmarksScreen = new float[LANDMARK_SCREEN_ELEMENT_COUNT];
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    int offset = i * 5;
                    ref readonly var landmark = ref landmarksScreen[i];
                    _rawLandmarksScreen[offset + 0] = landmark.X;
                    _rawLandmarksScreen[offset + 1] = landmark.Y;
                    _rawLandmarksScreen[offset + 2] = landmark.Z;
                    _rawLandmarksScreen[offset + 3] = landmark.Visibility;
                    _rawLandmarksScreen[offset + 4] = landmark.Presence;
                }
                _rawLandmarksWorld = new float[LANDMARK_WORLD_ELEMENT_COUNT];
                for (int i = 0; i < landmarksWorld.Length; i++)
                {
                    int offset = i * 3;
                    ref readonly var landmark = ref landmarksWorld[i];
                    _rawLandmarksWorld[offset + 0] = landmark.Item1;
                    _rawLandmarksWorld[offset + 1] = landmark.Item2;
                    _rawLandmarksWorld[offset + 2] = landmark.Item3;
                }
                Confidence = confidence;
            }

#if NET_STANDARD_2_1

            public readonly ReadOnlySpan<ScreenLandmark> GetLandmarksScreen()
            {
                return MemoryMarshal.Cast<float, ScreenLandmark>(_rawLandmarksScreen.AsSpan());
            }

            public readonly ReadOnlySpan<Vec3f> GetLandmarksWorld()
            {
                return MemoryMarshal.Cast<float, Vec3f>(_rawLandmarksWorld.AsSpan());
            }

#endif

            public readonly ScreenLandmark[] GetLandmarksScreenArray()
            {
                ScreenLandmark[] landmarks = new ScreenLandmark[LANDMARK_SCREEN_COUNT];
                for (int i = 0; i < landmarks.Length; i++)
                {
                    int offset = i * 5;
                    landmarks[i] = new ScreenLandmark(_rawLandmarksScreen[offset + 0],
                                                         _rawLandmarksScreen[offset + 1],
                                                         _rawLandmarksScreen[offset + 2],
                                                         _rawLandmarksScreen[offset + 3],
                                                         _rawLandmarksScreen[offset + 4]);
                }
                return landmarks;
            }

            public readonly Vec3f[] GetLandmarksWorldArray()
            {
                Vec3f[] landmarks = new Vec3f[LANDMARK_WORLD_COUNT];
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
                StringBuilder sb = new StringBuilder(2260);

                sb.Append("PoseEstimationData(");
                sb.AppendFormat("X1:{0} Y1:{1} X2:{2} Y2:{3} ", X1, Y1, X2, Y2);

                sb.Append("LandmarksScreen:");
#if NET_STANDARD_2_1
                ReadOnlySpan<ScreenLandmark> landmarksScreen = GetLandmarksScreen();
#else
                ScreenLandmark[] landmarksScreen = GetLandmarksScreenArray();
#endif
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    ref readonly var p = ref landmarksScreen[i];
                    sb.AppendFormat("({0}, {1}, {2}, {3}, {4}) ", p.X, p.Y, p.Z, p.Visibility, p.Presence);
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

                sb.AppendFormat("Confidence:{0}", Confidence);
                sb.Append(")");

                return sb.ToString();
            }
        }
    }
}
#endif
