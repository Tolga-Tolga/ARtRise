#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    /// MediaPipe Hand Pose Stream Estimator for continuous hand tracking in video streams.
    ///
    /// This class implements a stream-mode hand pose estimation pipeline that:
    /// - Uses MediaPipePalmDetector for initial palm detection and tracking loss recovery
    /// - Uses MediaPipeHandPoseEstimator for 21-point hand landmark estimation
    /// - Applies temporal smoothing and filtering to reduce jitter and outliers
    /// - Maintains tracking state to optimize performance across frames
    /// - Supports both single hand and dual hand tracking modes
    /// - Implements overlap detection to prevent duplicate tracking
    ///
    /// The processing flow:
    /// 1. Check if palm detection is needed based on tracking state (NotInitialized, Lost with interval)
    /// 2. If needed: Run palm detection and process all detected palms with overlap detection
    /// 3. For each valid palm: Run hand pose estimation and check hand type match
    /// 4. Select best candidates for each hand (right/left) based on confidence
    /// 5. For tracking hands: Run hand pose estimation with reused palm detection if not executed
    /// 6. Apply temporal smoothing (One Euro Filter, Moving Average) to landmarks
    /// 7. Recalculate bounding box from smoothed landmarks for coordinate consistency
    /// 8. Generate palm detection from smoothed results for next frame tracking
    /// </summary>
    public class MediaPipeHandPoseStreamEstimator : ProcessingWorkerBase
    {

        public enum HandType : byte
        {
            Both,   // Both hands
            Right,  // Right hand only
            Left    // Left hand only
        }

        /// <summary>
        /// Represents the current tracking state of the hand.
        /// </summary>
        public enum TrackingHandState : byte
        {
            NotInitialized,
            Tracking,
            Lost
        }


        // Tracking parameters
        protected const int PALM_DETECTION_INTERVAL_LOST = 3; // Run palm detection every 3 frames when lost (MediaPipe standard)

        protected float _handPoseConfThreshold;
        protected float _palmScoreThreshold;
        protected int _backend;
        protected int _target;
        protected HandType _handType;
        protected MediaPipePalmDetector _palmDetector;
        protected MediaPipeHandPoseEstimator _handPoseEstimator;

        // Tracking state
        protected int _lostStateFrameCounter; // Counter for palm detection interval in lost state
        private TrakingHand[] _trackingHand; // [Right hand, Left hand]

        /// <summary>
        /// Initializes a new instance of the MediaPipeHandPoseStreamEstimator class.
        /// </summary>
        /// <param name="handType">Type of hand to detect (Both, Right, or Left).</param>
        /// <param name="palmModelFilepath">Path to the palm detection model file.</param>
        /// <param name="handPoseModelFilepath">Path to the hand pose estimation model file.</param>
        /// <param name="palmScoreThreshold">Score threshold for palm detection.</param>
        /// <param name="handPoseConfThreshold">Confidence threshold for filtering hand pose estimations.</param>
        /// <param name="maxTrackingLossFrames">Maximum number of consecutive frames to allow tracking loss before considering hand as lost.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipeHandPoseStreamEstimator(HandType handType, string palmModelFilepath, string handPoseModelFilepath,
                                               float palmScoreThreshold = 0.5f, float handPoseConfThreshold = 0.8f,
                                               int maxTrackingLossFrames = 5, int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(palmModelFilepath))
                throw new ArgumentException("Palm model filepath cannot be empty.", nameof(palmModelFilepath));
            if (string.IsNullOrEmpty(handPoseModelFilepath))
                throw new ArgumentException("Hand pose model filepath cannot be empty.", nameof(handPoseModelFilepath));

            _palmScoreThreshold = Mathf.Clamp01(palmScoreThreshold);
            _handPoseConfThreshold = Mathf.Clamp01(handPoseConfThreshold);
            _backend = backend;
            _target = target;
            _handType = handType;

            try
            {
                _palmDetector = new MediaPipePalmDetector(palmModelFilepath, scoreThreshold: _palmScoreThreshold, backend: _backend, target: _target);
                _handPoseEstimator = new MediaPipeHandPoseEstimator(handPoseModelFilepath, confThreshold: _handPoseConfThreshold, backend: _backend, target: _target);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN models. Invalid model file paths or corrupted model files.", e);
            }


            // Initialize tracking state
            _lostStateFrameCounter = 0;

            // Initialize tracking hands based on HandType
            _trackingHand = new TrakingHand[2];

            switch (_handType)
            {
                case HandType.Right:
                    _trackingHand[0] = new TrakingHand(_handPoseEstimator, 1.0f, maxTrackingLossFrames); // Right hand only (handedness > 0.5f)
                    _trackingHand[1] = null; // Left hand not used
                    break;
                case HandType.Left:
                    _trackingHand[0] = null; // Right hand not used
                    _trackingHand[1] = new TrakingHand(_handPoseEstimator, 0.0f, maxTrackingLossFrames); // Left hand only (handedness <= 0.5f)
                    break;
                case HandType.Both:
                    _trackingHand[0] = new TrakingHand(_handPoseEstimator, 1.0f, maxTrackingLossFrames); // Right hand (handedness > 0.5f)
                    _trackingHand[1] = new TrakingHand(_handPoseEstimator, 0.0f, maxTrackingLossFrames); // Left hand (handedness <= 0.5f)
                    break;
            }
        }


        /// <summary>
        /// Visualizes hand pose estimation result on the input image using Mat[].
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="results">The result Mat[] returned by Estimate method [right hand, left hand].</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public void Visualize(Mat image, Mat[] results, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (results == null || results.Length == 0)
                return;

            // Visualize all results (both right and left hands)
            for (int i = 0; i < results.Length; i++)
            {
                Mat result = results[i];
                if (result == null || result.empty())
                    continue;

                // Use the hand pose estimator's visualization method directly
                // The result Mat should have the same structure as the original MediaPipeHandPoseEstimator output
                _handPoseEstimator.Visualize(image, result, printResult, isRGB);
            }
        }

        /// <summary>
        /// Estimates hand pose in the input stream frame with temporal smoothing.
        /// </summary>
        /// <remarks>
        /// This method implements the stream-mode hand pose estimation pipeline:
        /// - First frame or tracking loss: Runs palm detection
        /// - Uses detected palm region for hand pose estimation
        /// - Applies temporal smoothing to reduce jitter
        /// - Maintains tracking state for optimization
        /// - Supports overlap detection to prevent duplicate tracking
        /// - Generates palm detection from smoothed results for next frame
        ///
        /// The returned Mat[] format:
        /// - Mat[0]: Right hand pose estimation result (132 rows, 1 column)
        /// - Mat[1]: Left hand pose estimation result (132 rows, 1 column)
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
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat[] containing estimation results [right hand, left hand]. The caller is responsible for disposing these Mats.</returns>
        public virtual Mat[] Estimate(Mat image, bool useCopyOutput = false)
        {
            Execute(image);

            // Assume _outputs array size is always 2
            Mat[] resultArray = new Mat[2];

            if (useCopyOutput)
            {
                // Create copies of both outputs
                resultArray[0] = CopyOutput(0); // Right hand pose result
                resultArray[1] = CopyOutput(1); // Left hand pose result
            }
            else
            {
                // Return references to internal buffers
                resultArray[0] = PeekOutput(0); // Right hand pose result
                resultArray[1] = PeekOutput(1); // Left hand pose result
            }

            return resultArray;
        }

        /// <summary>
        /// Estimates hand pose in the input stream frame asynchronously with temporal smoothing.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for stream-mode hand pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat[] containing estimation results (2 elements)
        /// - Applies temporal smoothing and tracking optimization
        /// - Supports overlap detection to prevent duplicate tracking
        /// - Generates palm detection from smoothed results for next frame
        ///
        /// The returned Mat[] format:
        /// - Mat[0]: Right hand pose estimation result (132 rows, 1 column)
        /// - Mat[1]: Left hand pose estimation result (132 rows, 1 column)
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, handedness, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Only one estimation operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat[] with estimation results [right hand, left hand]. The caller is responsible for disposing these Mats.</returns>
        public virtual async Task<Mat[]> EstimateAsync(Mat image, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(image, cancellationToken);

            // Assume _outputs array size is always 2
            // Always create copies for async safety
            Mat[] resultArray = new Mat[2];
            resultArray[0] = CopyOutput(0); // Right hand pose result
            resultArray[1] = CopyOutput(1); // Left hand pose result

            return resultArray;
        }

        /// <summary>
        /// Converts the estimation result matrix to a HandPoseEstimationData structure.
        /// </summary>
        /// <param name="result">Estimation result matrix from Execute method.</param>
        /// <returns>HandPoseEstimationData structure containing hand pose information.</returns>
        public virtual MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData ToStructuredData(Mat result)
        {
            // Use the hand pose estimator's structured data conversion method directly
            // All necessary validation is performed in _handPoseEstimator.ToStructuredData
            return _handPoseEstimator.ToStructuredData(result);
        }

        /// <summary>
        /// Gets the current tracking state for both hands.
        /// </summary>
        /// <returns>Array containing tracking states [right hand, left hand].</returns>
        public virtual TrackingHandState[] GetTrackingState()
        {
            return new TrackingHandState[]
            {
                _trackingHand[0]?.TrackingState ?? TrackingHandState.NotInitialized, // Right hand
                _trackingHand[1]?.TrackingState ?? TrackingHandState.NotInitialized  // Left hand
            };
        }

        /// <summary>
        /// Resets the tracking state, forcing palm detection on the next frame.
        /// </summary>
        public virtual void ResetTracking()
        {
            _lostStateFrameCounter = 0;

            // Reset tracking hands for active hands
            if (_trackingHand[0] != null) _trackingHand[0].Reset(); // Right hand
            if (_trackingHand[1] != null) _trackingHand[1].Reset(); // Left hand
        }

        /// <summary>
        /// Core processing method that implements the stream-mode hand pose estimation pipeline.
        /// This method handles palm detection, hand pose estimation, temporal smoothing, and tracking state management.
        /// </summary>
        /// <param name="inputs">Input array containing the image Mat.</param>
        /// <returns>Mat array containing hand pose estimation results [right hand, left hand].</returns>
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

            try
            {
                // Clear hand pose estimation executed flag for new frame based on HandType
                if (_trackingHand[0] != null) _trackingHand[0].ClearHandPoseEstimationExecuted(); // Right hand
                if (_trackingHand[1] != null) _trackingHand[1].ClearHandPoseEstimationExecuted(); // Left hand

                // Increment lost state frame counter when in lost state (check active hands)
                bool anyHandLost = false;
                if (_trackingHand[0] != null && _trackingHand[0].TrackingState == TrackingHandState.Lost)
                    anyHandLost = true;
                if (_trackingHand[1] != null && _trackingHand[1].TrackingState == TrackingHandState.Lost)
                    anyHandLost = true;

                if (anyHandLost)
                {
                    _lostStateFrameCounter++;
                }

                // Determine if we need palm detection (check both hands)
                bool needsPalmDetection = ShouldRunPalmDetection();

                if (needsPalmDetection)
                {
                    // Run palm detection directly using _palmDetector
                    Mat palmDetection = _palmDetector.Detect(image, useCopyOutput: false);

                    if (!palmDetection.empty())
                    {
                        // Visualize palmDetection
                        // _palmDetector.Visualize(image, palmDetection, printResult: false, isRGB: true);

                        // Determine which hands need detection based on tracking state
                        bool needsRightHand = _trackingHand[0] != null && _trackingHand[0].TrackingState != TrackingHandState.Tracking;
                        bool needsLeftHand = _trackingHand[1] != null && _trackingHand[1].TrackingState != TrackingHandState.Tracking;

                        Mat[] handPoseResults = new Mat[2] { null, null };

                        // Process all palms with unified logic (single or multiple)
                        // Create list to store palm candidates with confidence scores
                        var palmCandidates = new List<(int index, float confidence, Mat handPoseResult, float handedness)>();

                        // Evaluate all palms and collect candidates that match HandType
                        for (int i = 0; i < palmDetection.rows(); i++)
                        {
                            // Check if this palm overlaps significantly with any currently tracking hand
                            // Skip this palm if it overlaps with existing tracking hands to prevent duplicate tracking
                            if (IsPalmOverlappingWithTrackingHands(palmDetection.row(i)))
                            {
                                continue; // Skip this palm as it overlaps with existing tracking
                            }

                            // Use the first available tracking hand for hand pose estimation
                            TrakingHand activeTrackingHand = _trackingHand[0] ?? _trackingHand[1];
                            Mat tempHandPoseResult = _handPoseEstimator.Estimate(image, palmDetection.row(i), useCopyOutput: false);

                            if (tempHandPoseResult.empty())
                            {
                                // Note: tempHandPoseResult is a reference to internal buffer
                                // when useCopyOutput: false, so it should not be disposed here
                                // tempHandPoseResult?.Dispose();
                                continue;
                            }

                            // Check if this palm matches the specified hand type
                            bool isHandTypeMatch = IsHandTypeMatch(tempHandPoseResult);

                            if (isHandTypeMatch)
                            {
                                var poseData = _handPoseEstimator.ToStructuredData(tempHandPoseResult);
                                float handedness = poseData.Handedness;

                                // Only add candidates for enabled hand types and non-tracking hands
                                bool isRightHand = handedness > 0.5f; // Right hand only when greater than 0.5
                                bool isLeftHand = handedness <= 0.5f; // Left hand when 0.5 or less (consistent with visualization)

                                // Check if this hand matches the needs criteria
                                bool rightHandNeedsDetection = isRightHand && needsRightHand;
                                bool leftHandNeedsDetection = isLeftHand && needsLeftHand;

                                if (rightHandNeedsDetection || leftHandNeedsDetection)
                                {
                                    // Only calculate confidence when we actually need it for candidates
                                    float currentConfidence = GetPalmConfidence(palmDetection.row(i));
                                    palmCandidates.Add((i, currentConfidence, tempHandPoseResult.clone(), handedness));
                                }
                            }

                            // Note: tempHandPoseResult is a reference to internal buffer
                            // when useCopyOutput: false, so it should not be disposed here
                            // tempHandPoseResult?.Dispose();
                        }

                        // Sort candidates by confidence (highest first)
                        palmCandidates.Sort((a, b) => b.confidence.CompareTo(a.confidence));

                        // Select best candidates for each hand (already sorted by confidence)
                        int bestRightPalmIndex = -1;
                        int bestLeftPalmIndex = -1;

                        foreach (var candidate in palmCandidates)
                        {
                            bool isRightHand = candidate.handedness > 0.5f; // Right hand only when greater than 0.5
                            bool isLeftHand = candidate.handedness <= 0.5f; // Left hand when 0.5 or less (consistent with visualization)

                            // Select first (highest confidence) right hand candidate
                            // Note: palmCandidates already filtered by needsRightHand and needsLeftHand and sorted by confidence
                            if (isRightHand && bestRightPalmIndex == -1)
                            {
                                bestRightPalmIndex = candidate.index;
                                handPoseResults[0]?.Dispose();
                                handPoseResults[0] = candidate.handPoseResult.clone();
                            }
                            // Select first (highest confidence) left hand candidate
                            // Note: palmCandidates already filtered by needsRightHand and needsLeftHand and sorted by confidence
                            else if (isLeftHand && bestLeftPalmIndex == -1)
                            {
                                bestLeftPalmIndex = candidate.index;
                                handPoseResults[1]?.Dispose();
                                handPoseResults[1] = candidate.handPoseResult.clone();
                            }
                        }

                        // Dispose unused candidates
                        foreach (var candidate in palmCandidates)
                        {
                            candidate.handPoseResult?.Dispose();
                        }

                        // Check which hands were actually detected
                        bool hasRightHand = bestRightPalmIndex >= 0 && _trackingHand[0] != null;
                        bool hasLeftHand = bestLeftPalmIndex >= 0 && _trackingHand[1] != null;

                        // Create palm detection results for each hand
                        Mat rightPalmDetection = bestRightPalmIndex >= 0 ? palmDetection.row(bestRightPalmIndex).clone() : new Mat();
                        Mat leftPalmDetection = bestLeftPalmIndex >= 0 ? palmDetection.row(bestLeftPalmIndex).clone() : new Mat();

                        // Note: palmDetection is a reference to internal buffer
                        // when useCopyOutput: false, so it should not be disposed here
                        // palmDetection?.Dispose();

                        // Store palm detection results and hand pose results only for detected hands
                        // For HandType.Both, only update hands that are not already in tracking state and were detected
                        if (hasRightHand) // Right hand
                        {
                            // Only update if this hand is not in tracking state
                            _trackingHand[0].SetLastPalmDetectionResult(rightPalmDetection);
                            _trackingHand[0].SetLastHandPoseResult(handPoseResults[0]);
                            _trackingHand[0].SetHandPoseEstimationExecuted();
                            _trackingHand[0].UpdateTrackingState(true);
                        }

                        if (hasLeftHand) // Left hand
                        {
                            // Only update if this hand is not in tracking state
                            _trackingHand[1].SetLastPalmDetectionResult(leftPalmDetection);
                            _trackingHand[1].SetLastHandPoseResult(handPoseResults[1]);
                            _trackingHand[1].SetHandPoseEstimationExecuted();
                            _trackingHand[1].UpdateTrackingState(true);
                        }

                        // Note: rightPalmDetection and leftPalmDetection are now managed by TrakingHand class
                        // after being passed to SetLastPalmDetectionResult, so they should not be disposed here
                        // rightPalmDetection?.Dispose();
                        // leftPalmDetection?.Dispose();

                        // Note: handPoseResults are references to internal buffers
                        // when useCopyOutput: false, so they should not be disposed here
                        // handPoseResults[0]?.Dispose();
                        // handPoseResults[1]?.Dispose();

                        _lostStateFrameCounter = 0;
                    }
                }

                // Get image size from the input image
                var imgSize = (inputs[0].width(), inputs[0].height());

                // Process both hands in a single loop: hand pose estimation and temporal smoothing
                for (int handIndex = 0; handIndex < 2; handIndex++)
                {
                    if (_trackingHand[handIndex] != null && _trackingHand[handIndex].TrackingState == TrackingHandState.Tracking)
                    {
                        // Run hand pose estimation if not already executed this frame
                        if (!_trackingHand[handIndex].HandPoseExecutedThisFrame)
                        {
                            // Run hand pose estimation with reused palm detection
                            Mat alwaysHandPoseResult = _handPoseEstimator.Estimate(image, _trackingHand[handIndex].LastPalmDetectionResult, useCopyOutput: false);

                            if (alwaysHandPoseResult.empty())
                            {
                                // Hand pose estimation failed, update tracking state
                                _trackingHand[handIndex].UpdateTrackingState(false);
                            }
                            else
                            {
                                // Store handPoseResult for next frame
                                _trackingHand[handIndex].SetLastHandPoseResult(alwaysHandPoseResult);
                            }

                            // Note: alwaysHandPoseResult is a reference to internal buffer
                            // when useCopyOutput: false, so it should not be disposed here
                            // alwaysHandPoseResult?.Dispose();

                            // Set hand pose estimation executed flag
                            _trackingHand[handIndex].SetHandPoseEstimationExecuted();
                        }

                        // Apply temporal smoothing and update tracking state for next frame
                        _trackingHand[handIndex].ApplySmoothingAndUpdateTracking(imgSize);

                        // Visualize palmDetection
                        // _palmDetector.Visualize(image, _trackingHand[handIndex].LastPalmDetectionResult, printResult: false, isRGB: true);
                    }
                }
            }
            catch (Exception e)
            {
                // On error, reset tracking for active hands
                if (_trackingHand[0] != null) _trackingHand[0].UpdateTrackingState(false);
                if (_trackingHand[1] != null) _trackingHand[1].UpdateTrackingState(false);
                throw new ArgumentException("Error during hand pose estimation: " + e.Message, e);
            }

            // Return the final HandPose results [right hand, left hand]
            // Return LastHandPoseResult if in tracking state, otherwise return empty Mat
            Mat rightHandPoseResult = (_trackingHand[0]?.TrackingState == TrackingHandState.Tracking)
                ? _trackingHand[0].LastHandPoseResult.clone()
                : new Mat();
            Mat leftHandPoseResult = (_trackingHand[1]?.TrackingState == TrackingHandState.Tracking)
                ? _trackingHand[1].LastHandPoseResult.clone()
                : new Mat();
            return new Mat[] { rightHandPoseResult, leftHandPoseResult };
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual bool ShouldRunPalmDetection()
        {
            // Check if any active hand needs palm detection
            bool rightHandNeedsDetection = false;
            bool leftHandNeedsDetection = false;

            if (_trackingHand[0] != null) // Right hand
            {
                switch (_trackingHand[0].TrackingState)
                {
                    case TrackingHandState.NotInitialized:
                        rightHandNeedsDetection = true;
                        break;
                    case TrackingHandState.Tracking:
                        rightHandNeedsDetection = false; // Reuse previous detection (MediaPipe standard approach)
                        break;
                    case TrackingHandState.Lost:
                        // Run palm detection at regular intervals when lost (MediaPipe standard approach)
                        rightHandNeedsDetection = (_lostStateFrameCounter % PALM_DETECTION_INTERVAL_LOST) == 0;
                        break;
                    default:
                        rightHandNeedsDetection = true;
                        break;
                }
            }

            if (_trackingHand[1] != null) // Left hand
            {
                switch (_trackingHand[1].TrackingState)
                {
                    case TrackingHandState.NotInitialized:
                        leftHandNeedsDetection = true;
                        break;
                    case TrackingHandState.Tracking:
                        leftHandNeedsDetection = false; // Reuse previous detection (MediaPipe standard approach)
                        break;
                    case TrackingHandState.Lost:
                        // Run palm detection at regular intervals when lost (MediaPipe standard approach)
                        leftHandNeedsDetection = (_lostStateFrameCounter % PALM_DETECTION_INTERVAL_LOST) == 0;
                        break;
                    default:
                        leftHandNeedsDetection = true;
                        break;
                }
            }

            return rightHandNeedsDetection || leftHandNeedsDetection;
        }


        protected virtual float GetPalmConfidence(Mat palmDetection)
        {
            if (palmDetection.empty() || palmDetection.rows() == 0)
                return 0.0f;

            // Get confidence from the last column (score)
            int scoreColumn = palmDetection.cols() - 1;
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            return palmDetection.at<float>(0, scoreColumn)[0];
#else
            float[] score = new float[1];
            palmDetection.get(0, scoreColumn, score);
            return score[0];
#endif
        }

        /// <summary>
        /// Checks if the detected hand matches the specified hand type.
        /// </summary>
        /// <param name="handPoseResult">The hand pose estimation result.</param>
        /// <returns>True if the hand matches the specified type, false otherwise.</returns>
        protected virtual bool IsHandTypeMatch(Mat handPoseResult)
        {
            if (handPoseResult.empty())
                return false;

            if (_handType == HandType.Both)
                return true;

            // Get handedness from the result using the hand pose estimator directly
            var poseData = _handPoseEstimator.ToStructuredData(handPoseResult);
            float handedness = poseData.Handedness;

            // MediaPipe handedness: > 0.5 for right hand, <= 0.5 for left hand
            switch (_handType)
            {
                case HandType.Right:
                    return handedness > 0.5f; // Right hand only when greater than 0.5
                case HandType.Left:
                    return handedness <= 0.5f; // Left hand when 0.5 or less (consistent with visualization)
                default:
                    return true;
            }
        }

        /// <summary>
        /// Calculates the overlap ratio between two palm detection bounding boxes.
        /// </summary>
        /// <param name="palmDetection1">First palm detection result.</param>
        /// <param name="palmDetection2">Second palm detection result.</param>
        /// <returns>Overlap ratio (0.0 to 1.0). Returns 0.0 if either detection is empty.</returns>
        protected virtual float CalculatePalmOverlapRatio(Mat palmDetection1, Mat palmDetection2)
        {
            if (palmDetection1.empty() || palmDetection2.empty())
                return 0.0f;

            // Extract bounding box coordinates from palm detection results
            // Palm detection format: [x1, y1, x2, y2, landmarks..., score]
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            // Use Mat.AsSpan for direct memory access (faster)
            Span<float> palmDetection1Span = palmDetection1.AsSpan<float>();
            Span<float> palmDetection2Span = palmDetection2.AsSpan<float>();

            // Extract bounding box coordinates directly from span
            float bbox1_x1 = palmDetection1Span[0];
            float bbox1_y1 = palmDetection1Span[1];
            float bbox1_x2 = palmDetection1Span[2];
            float bbox1_y2 = palmDetection1Span[3];

            float bbox2_x1 = palmDetection2Span[0];
            float bbox2_y1 = palmDetection2Span[1];
            float bbox2_x2 = palmDetection2Span[2];
            float bbox2_y2 = palmDetection2Span[3];
#else
            // Fallback to traditional Mat.get method
            float[] bbox1 = new float[4];
            float[] bbox2 = new float[4];

            palmDetection1.get(0, 0, bbox1);
            palmDetection2.get(0, 0, bbox2);

            float bbox1_x1 = bbox1[0];
            float bbox1_y1 = bbox1[1];
            float bbox1_x2 = bbox1[2];
            float bbox1_y2 = bbox1[3];

            float bbox2_x1 = bbox2[0];
            float bbox2_y1 = bbox2[1];
            float bbox2_x2 = bbox2[2];
            float bbox2_y2 = bbox2[3];
#endif

            // Calculate intersection area
            float intersectionX1 = Mathf.Max(bbox1_x1, bbox2_x1);
            float intersectionY1 = Mathf.Max(bbox1_y1, bbox2_y1);
            float intersectionX2 = Mathf.Min(bbox1_x2, bbox2_x2);
            float intersectionY2 = Mathf.Min(bbox1_y2, bbox2_y2);

            // Check if there's no intersection
            if (intersectionX1 >= intersectionX2 || intersectionY1 >= intersectionY2)
                return 0.0f;

            float intersectionArea = (intersectionX2 - intersectionX1) * (intersectionY2 - intersectionY1);

            // Calculate union area
            float area1 = (bbox1_x2 - bbox1_x1) * (bbox1_y2 - bbox1_y1);
            float area2 = (bbox2_x2 - bbox2_x1) * (bbox2_y2 - bbox2_y1);
            float unionArea = area1 + area2 - intersectionArea;

            // Return overlap ratio (intersection over union)
            return unionArea > 0 ? intersectionArea / unionArea : 0.0f;
        }

        /// <summary>
        /// Checks if a palm detection overlaps significantly with any currently tracking hand.
        /// </summary>
        /// <param name="palmDetection">The palm detection to check.</param>
        /// <param name="overlapThreshold">Threshold for considering overlap significant (default: 0.3).</param>
        /// <returns>True if the palm overlaps significantly with any tracking hand, false otherwise.</returns>
        protected virtual bool IsPalmOverlappingWithTrackingHands(Mat palmDetection, float overlapThreshold = 0.2f)
        {
            if (palmDetection.empty())
                return false;

            // Check overlap with right hand if it's tracking
            if (_trackingHand[0] != null && _trackingHand[0].TrackingState == TrackingHandState.Tracking)
            {
                Mat rightHandPalm = _trackingHand[0].LastPalmDetectionResult;
                if (!rightHandPalm.empty())
                {
                    float overlapRatio = CalculatePalmOverlapRatio(palmDetection, rightHandPalm);
                    // Debug.Log("rightHandPalm: " + overlapRatio);
                    if (overlapRatio > overlapThreshold)
                        return true;
                }
            }

            // Check overlap with left hand if it's tracking
            if (_trackingHand[1] != null && _trackingHand[1].TrackingState == TrackingHandState.Tracking)
            {
                Mat leftHandPalm = _trackingHand[1].LastPalmDetectionResult;
                if (!leftHandPalm.empty())
                {
                    float overlapRatio = CalculatePalmOverlapRatio(palmDetection, leftHandPalm);
                    // Debug.Log("leftHandPalm: " + overlapRatio);
                    if (overlapRatio > overlapThreshold)
                        return true;
                }
            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _palmDetector?.Dispose(); _palmDetector = null;
                _handPoseEstimator?.Dispose(); _handPoseEstimator = null;

                // Dispose tracking hands array
                if (_trackingHand != null)
                {
                    for (int i = 0; i < _trackingHand.Length; i++)
                    {
                        _trackingHand[i]?.Dispose();
                    }
                    _trackingHand = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// One Euro Filter implementation for temporal smoothing.
        /// </summary>
        private class OneEuroFilter
        {
            private float _freq;
            private float _minCutoff;
            private float _beta;
            private float _dCutoff;
            private float _x;
            private float _dx;
            private float _lastTime;
            private LowPassFilter _xFilter;
            private LowPassFilter _dxFilter;

            internal OneEuroFilter(float freq, float minCutoff = 1.0f, float beta = 0.007f, float dCutoff = 1.0f)
            {
                _freq = freq;
                _minCutoff = minCutoff;
                _beta = beta;
                _dCutoff = dCutoff;
                _x = 0.0f;
                _dx = 0.0f;
                _lastTime = 0.0f;
                _xFilter = new LowPassFilter(Alpha(_minCutoff));
                _dxFilter = new LowPassFilter(Alpha(_dCutoff));
            }

            internal float Filter(float x, float timestamp = 0.0f)
            {
                if (timestamp == 0.0f)
                {
                    // Fallback to estimated timestamp based on frequency
                    // This should rarely be used as timestamp is typically provided by caller
                    timestamp = _lastTime + (1.0f / _freq);
                }

                if (_lastTime != 0.0f && timestamp != _lastTime)
                    _freq = 1.0f / (timestamp - _lastTime);

                _lastTime = timestamp;

                // Estimate velocity
                float dx = _lastTime == 0.0f ? 0.0f : (x - _x) * _freq;
                _dx = _dxFilter.Filter(dx);

                // Calculate adaptive cutoff
                float cutoff = _minCutoff + _beta * Math.Abs(_dx);

                // Update x filter
                _xFilter.SetAlpha(Alpha(cutoff));
                _x = _xFilter.Filter(x);

                return _x;
            }

            /// <summary>
            /// Filters the input value with confidence-based adaptive smoothing.
            /// Lower confidence values result in stronger smoothing (more past value retention).
            /// Higher confidence values result in weaker smoothing (more current value following).
            /// </summary>
            /// <param name="x">Input value to filter.</param>
            /// <param name="confidence">Confidence value (0.0 to 1.0). Lower values = stronger smoothing.</param>
            /// <param name="timestamp">Timestamp for the input value.</param>
            /// <returns>Filtered output value.</returns>
            internal float Filter(float x, float confidence, float timestamp = 0.0f)
            {
                if (timestamp == 0.0f)
                {
                    // Fallback to estimated timestamp based on frequency
                    // This should rarely be used as timestamp is typically provided by caller
                    timestamp = _lastTime + (1.0f / _freq);
                }

                if (_lastTime != 0.0f && timestamp != _lastTime)
                    _freq = 1.0f / (timestamp - _lastTime);

                _lastTime = timestamp;

                // Estimate velocity
                float dx = _lastTime == 0.0f ? 0.0f : (x - _x) * _freq;
                _dx = _dxFilter.Filter(dx);

                // Calculate adaptive cutoff with confidence-based adjustment
                // Lower confidence (0.0) -> higher smoothing factor (stronger past value retention)
                // Higher confidence (1.0) -> lower smoothing factor (stronger current value following)
                float confidenceFactor = Math.Max(0.1f, confidence); // Minimum factor to prevent division by zero
                float adjustedBeta = _beta / confidenceFactor;
                float cutoff = _minCutoff + adjustedBeta * Math.Abs(_dx);

                // Update x filter
                _xFilter.SetAlpha(Alpha(cutoff));
                _x = _xFilter.Filter(x);

                return _x;
            }

            /// <summary>
            /// Filters the input value with visibility and presence-based adaptive smoothing.
            /// Lower visibility/presence values result in stronger smoothing (more past value retention).
            /// Higher visibility/presence values result in weaker smoothing (more current value following).
            /// </summary>
            /// <param name="x">Input value to filter.</param>
            /// <param name="visibility">Visibility value (0.0 to 1.0). Lower values = stronger smoothing.</param>
            /// <param name="presence">Presence value (0.0 to 1.0). Lower values = stronger smoothing.</param>
            /// <param name="timestamp">Timestamp for the input value.</param>
            /// <returns>Filtered output value.</returns>
            internal float Filter(float x, float visibility, float presence, float timestamp = 0.0f)
            {
                // Combine visibility and presence to calculate overall confidence
                // Use multiplication to provide smoother confidence transitions
                // Both values need to be high for high confidence
                float combinedConfidence = presence * visibility;

                return Filter(x, combinedConfidence, timestamp);
            }

            internal void Reset()
            {
                _x = 0.0f;
                _dx = 0.0f;
                _lastTime = 0.0f;
                _xFilter.Reset();
                _dxFilter.Reset();
            }

            /// <summary>
            /// Warms up the filter with initial values to prevent initial output instability.
            /// </summary>
            /// <param name="x">Initial input value.</param>
            /// <param name="timestamp">Initial timestamp.</param>
            internal void Warmup(float x, float timestamp)
            {
                // Set initial values directly without filtering to prevent initial instability
                _x = x;
                _dx = 0.0f; // Initialize velocity to zero
                _lastTime = timestamp;

                // Reset internal filters to clean state
                _xFilter.Reset();
                _dxFilter.Reset();

                // Set the low-pass filters to the initial value to prevent initial drift
                _xFilter.Output = x; // Directly set the filter output to initial value
                _dxFilter.Output = 0.0f; // Set velocity filter to zero
            }

            private float Alpha(float cutoff)
            {
                float te = 1.0f / _freq;
                float tau = 1.0f / (2.0f * Mathf.PI * cutoff);
                return 1.0f / (1.0f + tau / te);
            }
        }

        /// <summary>
        /// Low Pass Filter implementation.
        /// </summary>
        private class LowPassFilter
        {
            private float _alpha;
            private float _y;

            internal LowPassFilter(float alpha)
            {
                _alpha = alpha;
                _y = 0.0f;
            }

            internal float Filter(float x)
            {
                _y = _alpha * x + (1.0f - _alpha) * _y;
                return _y;
            }

            internal void SetAlpha(float alpha)
            {
                _alpha = alpha;
            }

            internal void Reset()
            {
                _y = 0.0f;
            }

            /// <summary>
            /// Gets or sets the current filter output value for warmup purposes.
            /// </summary>
            internal float Output
            {
                get { return _y; }
                set { _y = value; }
            }
        }

        /// <summary>
        /// Moving Average Filter implementation.
        /// </summary>
        private class MovingAverageFilter
        {
            private Queue<float> _buffer;
            private int _windowSize;
            private float _sum;

            internal MovingAverageFilter(int windowSize)
            {
                _windowSize = windowSize;
                _buffer = new Queue<float>();
                _sum = 0.0f;
            }

            internal float Filter(float x)
            {
                _buffer.Enqueue(x);
                _sum += x;

                if (_buffer.Count > _windowSize)
                {
                    _sum -= _buffer.Dequeue();
                }

                return _sum / _buffer.Count;
            }

            internal void Reset()
            {
                _buffer.Clear();
                _sum = 0.0f;
            }

            /// <summary>
            /// Warms up the moving average filter with initial values to prevent initial output instability.
            /// </summary>
            /// <param name="x">Initial input value.</param>
            internal void Warmup(float x)
            {
                // Clear buffer and reset sum
                _buffer.Clear();
                _sum = 0.0f;

                // Fill buffer with initial value
                for (int i = 0; i < _windowSize; i++)
                {
                    _buffer.Enqueue(x);
                    _sum += x;
                }
            }
        }

        /// <summary>
        /// Private class to manage hand tracking state and temporal smoothing buffers.
        /// This class encapsulates all tracking-related data and provides methods for state management.
        /// It handles palm detection result storage, hand pose result storage, temporal smoothing,
        /// and tracking state transitions for individual hands.
        /// </summary>
        private class TrakingHand
        {

            // Detectors
            private MediaPipeHandPoseEstimator _handPoseEstimator;

            // Tracking state
            private TrackingHandState _trackingState;
            private int _trackingLossFrames;

            // Frame execution tracking
            private bool _isHandPoseExecutedThisFrame = false;

            // Tracking results
            private Mat _lastPalmDetectionResult;
            private Mat _lastHandPoseResult;
            private float _trackedHandedness;

            // Filter control flags
            private bool _enableOneEuroFilter;
            private bool _enableMovingAverageFilter;

            // Temporal smoothing buffers
            private Queue<MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData> _poseHistory;
            private OneEuroFilter[] _landmarkOneEuroFilters;
            private MovingAverageFilter[] _landmarkMovingAverageFilters;

            // Temporal smoothing constants
            private const float ONE_EURO_FREQUENCY = 30.0f;
            private const float ONE_EURO_MIN_CUTOFF = 1.0f;
            private const float ONE_EURO_BETA = 0.05f;
            private const float ONE_EURO_D_CUTOFF = 1.0f;
            private const int MOVING_AVERAGE_WINDOW = 3;

            // Bounding box calculation constants
            private static readonly Point HAND_BOX_SHIFT_VECTOR = new Point(0, -0.1);
            private const double HAND_BOX_ENLARGE_FACTOR = 1.65;

            // Tracking parameters
            private int _maxTrackingLossFrames;

            /// <summary>
            /// Initializes a new instance of the TrakingHand class.
            /// </summary>
            /// <param name="handPoseEstimator">The hand pose estimator instance.</param>
            /// <param name="handedness">The handedness value for this tracking hand. -1.0f for auto-detection, > 0.5f for right hand, <= 0.5f for left hand.</param>
            /// <param name="maxTrackingLossFrames">Maximum number of consecutive frames to allow tracking loss before considering hand as lost.</param>
            /// <param name="enableOneEuroFilter">Whether to enable One Euro Filter for temporal smoothing.</param>
            /// <param name="enableMovingAverageFilter">Whether to enable Moving Average Filter for temporal smoothing.</param>
            internal TrakingHand(MediaPipeHandPoseEstimator handPoseEstimator, float handedness = -1.0f,
                                int maxTrackingLossFrames = 5, bool enableOneEuroFilter = true, bool enableMovingAverageFilter = false)
            {
                if (handPoseEstimator == null)
                    throw new ArgumentNullException(nameof(handPoseEstimator), "Hand pose estimator cannot be null.");

                _handPoseEstimator = handPoseEstimator;

                // Initialize tracking state
                _trackingState = TrackingHandState.NotInitialized;
                _trackingLossFrames = 0;
                _trackedHandedness = handedness;
                _maxTrackingLossFrames = Math.Max(1, maxTrackingLossFrames);

                // Initialize filter control flags
                _enableOneEuroFilter = enableOneEuroFilter;
                _enableMovingAverageFilter = enableMovingAverageFilter;

                // Initialize smoothing buffers
                _poseHistory = new Queue<MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData>();
                _landmarkOneEuroFilters = new OneEuroFilter[MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3 * 2]; // Screen + World
                _landmarkMovingAverageFilters = new MovingAverageFilter[MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3 * 2]; // Screen + World

                for (int i = 0; i < _landmarkOneEuroFilters.Length; i++)
                {
                    _landmarkOneEuroFilters[i] = new OneEuroFilter(ONE_EURO_FREQUENCY, ONE_EURO_MIN_CUTOFF, ONE_EURO_BETA, ONE_EURO_D_CUTOFF);
                    _landmarkMovingAverageFilters[i] = new MovingAverageFilter(MOVING_AVERAGE_WINDOW);
                }
            }

            /// <summary>
            /// Gets the current tracking state.
            /// </summary>
            internal TrackingHandState TrackingState
            {
                get { return _trackingState; }
            }

            /// <summary>
            /// Gets the tracking loss frames count.
            /// </summary>
            internal int TrackingLossFrames
            {
                get { return _trackingLossFrames; }
            }

            /// <summary>
            /// Gets whether hand pose estimation was executed this frame.
            /// </summary>
            internal bool HandPoseExecutedThisFrame => _isHandPoseExecutedThisFrame;

            /// <summary>
            /// Gets the last palm detection result.
            /// </summary>
            internal Mat LastPalmDetectionResult
            {
                get { return _lastPalmDetectionResult; }
            }

            /// <summary>
            /// Gets the last hand pose result.
            /// </summary>
            internal Mat LastHandPoseResult
            {
                get { return _lastHandPoseResult; }
            }

            /// <summary>
            /// Gets the tracked handedness.
            /// </summary>
            internal float TrackedHandedness
            {
                get { return _trackedHandedness; }
            }

            /// <summary>
            /// Gets the pose history queue.
            /// </summary>
            private Queue<MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData> PoseHistory
            {
                get { return _poseHistory; }
            }

            /// <summary>
            /// Gets the landmark One Euro filters array.
            /// </summary>
            private OneEuroFilter[] LandmarkOneEuroFilters
            {
                get { return _landmarkOneEuroFilters; }
            }

            /// <summary>
            /// Gets the landmark moving average filters array.
            /// </summary>
            private MovingAverageFilter[] LandmarkMovingAverageFilters
            {
                get { return _landmarkMovingAverageFilters; }
            }

            /// <summary>
            /// Sets the last palm detection result, disposing the previous one if it exists.
            /// </summary>
            /// <param name="palmDetectionResult">The new palm detection result.</param>
            internal void SetLastPalmDetectionResult(Mat palmDetectionResult)
            {
                _lastPalmDetectionResult?.Dispose();
                _lastPalmDetectionResult = palmDetectionResult;

                // Note: _lastPalmDetectionResult are references to internal buffers
                // when useCopyOutput: false, so they should not be disposed here
                // _lastPalmDetectionResult?.Dispose();
                // _lastPalmDetectionResult = palmDetectionResult?.clone();
            }

            /// <summary>
            /// Sets the last hand pose result, disposing the previous one if it exists.
            /// </summary>
            /// <param name="handPoseResult">The new hand pose result.</param>
            internal void SetLastHandPoseResult(Mat handPoseResult)
            {
                _lastHandPoseResult?.Dispose();
                _lastHandPoseResult = handPoseResult;

                // Note: _lastHandPoseResult are references to internal buffers
                // when useCopyOutput: false, so they should not be disposed here
                // _lastHandPoseResult?.Dispose();
                // _lastHandPoseResult = handPoseResult?.clone();
            }

            /// <summary>
            /// Resets all tracking data and smoothing buffers.
            /// </summary>
            internal void Reset()
            {
                // Reset tracking state
                _trackingState = TrackingHandState.NotInitialized;
                _trackingLossFrames = 0;

                _lastPalmDetectionResult?.Dispose();
                _lastPalmDetectionResult = null;
                _lastHandPoseResult?.Dispose();
                _lastHandPoseResult = null;

                // Reset smoothing buffers
                ResetSmoothingBuffers();
            }

            /// <summary>
            /// Resets smoothing buffers when tracking is resumed to prevent artifacts from previous tracking session.
            /// </summary>
            private void ResetSmoothingBuffers()
            {
                // Clear pose history
                _poseHistory.Clear();

                // Reset all One Euro Filters
                for (int i = 0; i < _landmarkOneEuroFilters.Length; i++)
                {
                    _landmarkOneEuroFilters[i].Reset();
                }

                // Reset all Moving Average Filters
                for (int i = 0; i < _landmarkMovingAverageFilters.Length; i++)
                {
                    _landmarkMovingAverageFilters[i].Reset();
                }
            }

            /// <summary>
            /// Called when hand tracking starts successfully.
            /// </summary>
            private void OnTrackingStart()
            {
                // Default implementation does nothing
                // Override this method in derived classes to handle tracking start events

                // Also display handedness
                // Debug.Log("OnTrackingStart: " + _trackedHandedness);
            }

            /// <summary>
            /// Called when hand tracking is lost.
            /// </summary>
            private void OnTrackingLost()
            {
                // Default implementation does nothing
                // Override this method in derived classes to handle tracking loss events

                // Debug.Log("OnTrackingLost: " + _trackedHandedness);

                // Reset smoothing buffers when tracking is lost to prevent artifacts from previous tracking session
                // ResetSmoothingBuffers();
            }

            /// <summary>
            /// Updates the tracking state based on success or failure.
            /// </summary>
            /// <param name="success">Whether the tracking was successful.</param>
            internal void UpdateTrackingState(bool success)
            {
                if (success)
                {
                    // Check if we're transitioning from Lost to Tracking (tracking resumed)
                    bool trackingResumed = (_trackingState == TrackingHandState.Lost);
                    bool trackingStarted = (_trackingState == TrackingHandState.NotInitialized);

                    _trackingState = TrackingHandState.Tracking;
                    _trackingLossFrames = 0;

                    // Call OnTrackingStart when tracking starts or resumes
                    if (trackingStarted || trackingResumed)
                    {
                        OnTrackingStart();
                    }
                    // Note: For tracking started from NotInitialized, we'll warmup filters after getting the first pose data
                }
                else
                {
                    // Only increment tracking loss frames if we're currently in Tracking state
                    if (_trackingState == TrackingHandState.Tracking)
                    {
                        _trackingLossFrames++;
                        if (_trackingLossFrames >= _maxTrackingLossFrames)
                        {
                            // Call OnTrackingLost when transitioning to Lost state
                            OnTrackingLost();
                            _trackingState = TrackingHandState.Lost;
                        }
                    }
                }
            }

            /// <summary>
            /// Sets the hand pose estimation executed flag to true.
            /// </summary>
            internal void SetHandPoseEstimationExecuted()
            {
                _isHandPoseExecutedThisFrame = true;
            }

            /// <summary>
            /// Clears the hand pose estimation executed flag.
            /// </summary>
            internal void ClearHandPoseEstimationExecuted()
            {
                _isHandPoseExecutedThisFrame = false;
            }

            /// <summary>
            /// Applies temporal smoothing and updates tracking state for the next frame.
            /// This method applies One Euro Filter and Moving Average Filter to smooth landmarks,
            /// recalculates bounding box from smoothed landmarks, and generates palm detection
            /// for the next frame to maintain tracking continuity.
            /// </summary>
            /// <param name="imgSize">Image size (width, height).</param>
            internal void ApplySmoothingAndUpdateTracking((double width, double height) imgSize)
            {
                // Apply temporal smoothing using direct Mat operation approach
                Mat smoothedHandPoseResult = ApplyTemporalSmoothing(_lastHandPoseResult, imgSize);

                // Create palm detection from smoothed result for next frame
                Mat nextFramePalmDetectionResult = CreatePalmDetectionFromSmoothedResult(smoothedHandPoseResult, imgSize);

                // Store for next frame
                SetLastHandPoseResult(smoothedHandPoseResult);

                // Store the generated palm detection for next frame
                SetLastPalmDetectionResult(nextFramePalmDetectionResult);

                // Note: nextFramePalmDetectionResult is now stored in _lastPalmDetectionResult
                // and will be managed by the TrakingHand class, so it should not be disposed here
                // nextFramePalmDetectionResult.Dispose(); // Dispose the generated palm detection copy
            }

            /// <summary>
            /// Applies temporal smoothing to hand pose estimation results.
            /// This method converts the result to structured data, applies One Euro Filter and Moving Average Filter
            /// to landmarks, recalculates bounding box from smoothed landmarks, and converts back to Mat format.
            /// </summary>
            /// <param name="handPoseResult">The hand pose estimation result.</param>
            /// <param name="imgSize">Image size (width, height).</param>
            /// <returns>Smoothed hand pose result.</returns>
            private Mat ApplyTemporalSmoothing(Mat handPoseResult, (double width, double height) imgSize)
            {
                if (handPoseResult.empty())
                    return new Mat();

                // Convert to structured data for easier processing
                var poseData = _handPoseEstimator.ToStructuredData(handPoseResult);

                // Check if this is the first successful tracking frame and warmup filters
                if (_trackingState == TrackingHandState.Tracking && _poseHistory.Count == 0)
                {
                    WarmupFilters(poseData);
                }

                // Add to history
                _poseHistory.Enqueue(poseData);
                if (_poseHistory.Count > MOVING_AVERAGE_WINDOW)
                {
                    _poseHistory.Dequeue();
                }

                // Always apply smoothing filters, even for the first frame after warmup
                // This ensures consistent behavior and prevents initial instability

                // Step 3: Test bounding box recalculation from smoothed landmarks
                // Apply smoothing filters to structured data
                var smoothedPoseData = ApplySmoothingFilters(poseData, imgSize);

                // Convert back to Mat while preserving coordinate system
                return ConvertHandPoseEstimationBlazeDataToMat(smoothedPoseData, handPoseResult);
            }

            /// <summary>
            /// Creates a palm detection Mat from smoothed hand pose result for use in the next frame.
            /// This method extracts the bounding box and landmarks from the smoothed result and formats them
            /// in the same format as MediaPipePalmDetector output. It uses 7 key palm landmarks to calculate
            /// the bounding box and stores them in the palm detection format for tracking continuity.
            /// </summary>
            /// <param name="smoothedHandPoseResult">The smoothed hand pose estimation result.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>A Mat in palm detection format (19 columns: bbox + landmarks + score).</returns>
            private Mat CreatePalmDetectionFromSmoothedResult(Mat smoothedHandPoseResult, (double width, double height) imgSize)
            {
                if (smoothedHandPoseResult.empty())
                    return new Mat();

                // Convert smoothed result to structured data
                var poseData = _handPoseEstimator.ToStructuredData(smoothedHandPoseResult);

                // Get landmarks from the smoothed result
#if NET_STANDARD_2_1
                ReadOnlySpan<Vec3f> landmarksScreen = poseData.GetLandmarksScreen();
#else
                Vec3f[] landmarksScreen = poseData.GetLandmarksScreenArray();
#endif

                // Create palm detection Mat with 19 columns (4 bbox + 14 landmarks + 1 score)
                Mat palmDetection = new Mat(1, 19, CvType.CV_32FC1);

                // Calculate bounding box from 7 palm keypoints
                // Extract 7 key landmarks for palm detection (using specific landmark indices)
                // These correspond to the palm landmarks used by MediaPipePalmDetector
                int[] palmLandmarkIndices = { 0, 5, 9, 13, 17, 1, 2 }; // Wrist, finger bases, and palm points
                float[] palm_landmarks = new float[14]; // 7 points * 2 coordinates

                for (int i = 0; i < palmLandmarkIndices.Length; i++)
                {
                    int landmarkIndex = palmLandmarkIndices[i];
                    if (landmarkIndex < landmarksScreen.Length)
                    {
                        palm_landmarks[i * 2] = landmarksScreen[landmarkIndex].Item1;     // x
                        palm_landmarks[i * 2 + 1] = landmarksScreen[landmarkIndex].Item2; // y
                    }
                }

                // Calculate bounding box from palm landmarks
                float minX = float.MaxValue, minY = float.MaxValue;
                float maxX = float.MinValue, maxY = float.MinValue;

                for (int i = 0; i < palm_landmarks.Length; i += 2)
                {
                    float x = palm_landmarks[i];
                    float y = palm_landmarks[i + 1];

                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }

                // Clamp bounding box to image boundaries
                float imgWidth = (float)imgSize.width;
                float imgHeight = (float)imgSize.height;

                minX = Mathf.Clamp(minX, 0, imgWidth);
                minY = Mathf.Clamp(minY, 0, imgHeight);
                maxX = Mathf.Clamp(maxX, 0, imgWidth);
                maxY = Mathf.Clamp(maxY, 0, imgHeight);

                // Ensure bounding box has valid dimensions
                if (maxX <= minX) maxX = minX + 1;
                if (maxY <= minY) maxY = minY + 1;

                // Convert bounding box to palm detection format
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                // Use Mat.at for direct memory access (faster)
                // Get span for the entire row and access elements safely
                Span<float> palmDetectionSpan = palmDetection.AsSpan<float>();

                // Set bounding box values (first 4 elements)
                palmDetectionSpan[0] = minX;
                palmDetectionSpan[1] = minY;
                palmDetectionSpan[2] = maxX;
                palmDetectionSpan[3] = maxY;

                // Store the palm landmarks (elements 4-17)
                for (int i = 0; i < palm_landmarks.Length; i++)
                {
                    palmDetectionSpan[4 + i] = palm_landmarks[i];
                }

                // Use the confidence from the smoothed result as the score (element 18)
                palmDetectionSpan[18] = poseData.Confidence;
#else
                // Fallback to traditional Mat.put method
                float[] palm_box = new float[] { minX, minY, maxX, maxY };
                palmDetection.put(0, 0, palm_box);

                // Store the palm landmarks (already calculated above)
                palmDetection.put(0, 4, palm_landmarks);

                // Use the confidence from the smoothed result as the score
                float[] score = new float[] { poseData.Confidence };
                palmDetection.put(0, 18, score);
#endif

                return palmDetection;
            }

            /// <summary>
            /// Applies smoothing filters to landmarks and recalculates bounding box from smoothed landmarks.
            /// This is the correct MediaPipe approach for maintaining coordinate system consistency.
            /// The method applies One Euro Filter and Moving Average Filter to both screen and world coordinates,
            /// then recalculates the bounding box from the smoothed landmarks to maintain coordinate consistency.
            /// </summary>
            /// <param name="poseData">The pose data to smooth.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>Pose data with smoothed landmarks and recalculated bounding box.</returns>
            private MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData ApplySmoothingFilters(
                MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData poseData, (double width, double height) imgSize)
            {
                // Calculate absolute timestamp for OneEuroFilter using Environment.TickCount (worker thread compatible)
                int currentTimeMs = Environment.TickCount;
                float currentTimestamp = currentTimeMs / 1000.0f; // Convert to seconds

                // Get landmarks
#if NET_STANDARD_2_1
                ReadOnlySpan<Vec3f> landmarksScreen = poseData.GetLandmarksScreen();
                ReadOnlySpan<Vec3f> landmarksWorld = poseData.GetLandmarksWorld();
#else
                Vec3f[] landmarksScreen = poseData.GetLandmarksScreenArray();
                Vec3f[] landmarksWorld = poseData.GetLandmarksWorldArray();
#endif

                // Apply One Euro Filter and Moving Average to landmarks based on enabled flags
                var smoothedLandmarksScreen = new Vec3f[landmarksScreen.Length];
                var smoothedLandmarksWorld = new Vec3f[landmarksWorld.Length];

                // Initialize with original values
                for (int i = 0; i < landmarksScreen.Length; i++)
                {
                    smoothedLandmarksScreen[i] = landmarksScreen[i];
                    smoothedLandmarksWorld[i] = landmarksWorld[i];
                }

                // Step 1: Apply One Euro Filter to landmarks (MediaPipe standard order) if enabled
                // This provides adaptive smoothing based on movement speed
                if (_enableOneEuroFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates use first half of the array
                        int screenBaseIndex = i * 3;
                        smoothedLandmarksScreen[i] = new Vec3f(
                            _landmarkOneEuroFilters[screenBaseIndex].Filter(landmarksScreen[i].Item1, poseData.Confidence, currentTimestamp),
                            _landmarkOneEuroFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Item2, poseData.Confidence, currentTimestamp),
                            _landmarkOneEuroFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Item3, poseData.Confidence, currentTimestamp)
                        );

                        // World coordinates use second half of the array
                        int worldBaseIndex = (MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3) + (i * 3);
                        smoothedLandmarksWorld[i] = new Vec3f(
                            _landmarkOneEuroFilters[worldBaseIndex].Filter(landmarksWorld[i].Item1, poseData.Confidence, currentTimestamp),
                            _landmarkOneEuroFilters[worldBaseIndex + 1].Filter(landmarksWorld[i].Item2, poseData.Confidence, currentTimestamp),
                            _landmarkOneEuroFilters[worldBaseIndex + 2].Filter(landmarksWorld[i].Item3, poseData.Confidence, currentTimestamp)
                        );
                    }
                }

                // Step 2: Apply Moving Average Filter after One Euro Filter if enabled
                // Use separate MovingAverageFilter instances for Screen and World coordinates
                if (_enableMovingAverageFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates use first half of the array
                        int screenBaseIndex = i * 3;
                        smoothedLandmarksScreen[i] = new Vec3f(
                            _landmarkMovingAverageFilters[screenBaseIndex].Filter(smoothedLandmarksScreen[i].Item1),
                            _landmarkMovingAverageFilters[screenBaseIndex + 1].Filter(smoothedLandmarksScreen[i].Item2),
                            _landmarkMovingAverageFilters[screenBaseIndex + 2].Filter(smoothedLandmarksScreen[i].Item3)
                        );

                        // World coordinates use second half of the array
                        int worldBaseIndex = (MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3) + (i * 3);
                        smoothedLandmarksWorld[i] = new Vec3f(
                            _landmarkMovingAverageFilters[worldBaseIndex].Filter(smoothedLandmarksWorld[i].Item1),
                            _landmarkMovingAverageFilters[worldBaseIndex + 1].Filter(smoothedLandmarksWorld[i].Item2),
                            _landmarkMovingAverageFilters[worldBaseIndex + 2].Filter(smoothedLandmarksWorld[i].Item3)
                        );
                    }
                }

                // Step 3: Test bounding box recalculation from manually smoothed landmarks
                // Calculate new bounding box from smoothed landmarks (MediaPipe approach)
                var (newX1, newY1, newX2, newY2) = CalculateBoundingBoxFromLandmarks(smoothedLandmarksScreen, imgSize);

                // Create new pose data with smoothed landmarks and recalculated bounding box
                return new MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData(
                    newX1, newY1, newX2, newY2,
                    smoothedLandmarksScreen, smoothedLandmarksWorld,
                    _trackedHandedness, poseData.Confidence);
            }

            /// <summary>
            /// Warms up the smoothing filters with initial pose data.
            /// </summary>
            /// <param name="poseData">The initial pose data to warm up filters.</param>
            private void WarmupFilters(MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData poseData)
            {
                // Get landmarks
#if NET_STANDARD_2_1
                ReadOnlySpan<Vec3f> landmarksScreen = poseData.GetLandmarksScreen();
                ReadOnlySpan<Vec3f> landmarksWorld = poseData.GetLandmarksWorld();
#else
                Vec3f[] landmarksScreen = poseData.GetLandmarksScreenArray();
                Vec3f[] landmarksWorld = poseData.GetLandmarksWorldArray();
#endif

                // Warm up One Euro Filters if enabled
                if (_enableOneEuroFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates use first half of the array
                        int screenBaseIndex = i * 3;
                        _landmarkOneEuroFilters[screenBaseIndex].Filter(landmarksScreen[i].Item1, 0.0f);
                        _landmarkOneEuroFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Item2, 0.0f);
                        _landmarkOneEuroFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Item3, 0.0f);

                        // World coordinates use second half of the array
                        int worldBaseIndex = (MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3) + (i * 3);
                        _landmarkOneEuroFilters[worldBaseIndex].Filter(landmarksWorld[i].Item1, 0.0f);
                        _landmarkOneEuroFilters[worldBaseIndex + 1].Filter(landmarksWorld[i].Item2, 0.0f);
                        _landmarkOneEuroFilters[worldBaseIndex + 2].Filter(landmarksWorld[i].Item3, 0.0f);
                    }
                }

                // Warm up Moving Average Filters if enabled
                if (_enableMovingAverageFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates use first half of the array
                        int screenBaseIndex = i * 3;
                        _landmarkMovingAverageFilters[screenBaseIndex].Filter(landmarksScreen[i].Item1);
                        _landmarkMovingAverageFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Item2);
                        _landmarkMovingAverageFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Item3);

                        // World coordinates use second half of the array
                        int worldBaseIndex = (MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData.LANDMARK_VEC3F_COUNT * 3) + (i * 3);
                        _landmarkMovingAverageFilters[worldBaseIndex].Filter(landmarksWorld[i].Item1);
                        _landmarkMovingAverageFilters[worldBaseIndex + 1].Filter(landmarksWorld[i].Item2);
                        _landmarkMovingAverageFilters[worldBaseIndex + 2].Filter(landmarksWorld[i].Item3);
                    }
                }
            }

            /// <summary>
            /// Converts structured data back to Mat while preserving the original coordinate system.
            /// This method uses Marshal.StructureToPtr to ensure exact byte-level compatibility with the original Mat structure.
            /// </summary>
            /// <param name="poseData">The smoothed pose data.</param>
            /// <param name="originalMat">The original Mat for reference structure.</param>
            /// <returns>Mat with smoothed data and preserved coordinate system.</returns>
            private Mat ConvertHandPoseEstimationBlazeDataToMat(MediaPipeHandPoseEstimator.HandPoseEstimationBlazeData poseData, Mat originalMat)
            {
                // Create result Mat with the same structure as original
                Mat result = new Mat(originalMat.rows(), originalMat.cols(), originalMat.type());

                // Use Marshal.StructureToPtr to convert the structure directly to Mat data
                // This ensures exact byte-level compatibility with the original Mat structure
                Marshal.StructureToPtr(poseData, (IntPtr)result.dataAddr(), false);

                return result;
            }

            /// <summary>
            /// Calculates bounding box from smoothed landmarks using the same approach as MediaPipeHandPoseEstimator.
            /// This follows the exact same logic as the PostProcess method in MediaPipeHandPoseEstimator.
            /// The method applies the same shift vector and enlargement factor to maintain consistency
            /// with the original MediaPipe implementation.
            /// </summary>
            /// <param name="landmarks">The smoothed landmarks.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>Bounding box coordinates (x1, y1, x2, y2).</returns>
            private (float x1, float y1, float x2, float y2) CalculateBoundingBoxFromLandmarks(Vec3f[] landmarks, (double width, double height) imgSize)
            {
                if (landmarks == null || landmarks.Length == 0)
                    return (0, 0, 0, 0);

                // Convert landmarks to points for bounding box calculation (same as MediaPipeHandPoseEstimator)
                var landmarks_points = new (float x, float y)[landmarks.Length];
                for (int i = 0; i < landmarks.Length; i++)
                {
                    landmarks_points[i] = (landmarks[i].Item1, landmarks[i].Item2);
                }

                // Use OpenCV's boundingRect (same as MediaPipeHandPoseEstimator)
                using (var points = new MatOfPoint(landmarks_points))
                {
                    var bbox = Imgproc.boundingRect(points);

                    // Apply the same shift and enlargement as MediaPipeHandPoseEstimator
                    // shift bounding box
                    var wh_bbox = bbox.br() - bbox.tl();
                    var shift_vector = new Point(HAND_BOX_SHIFT_VECTOR.x * wh_bbox.x, HAND_BOX_SHIFT_VECTOR.y * wh_bbox.y);
                    bbox = bbox + shift_vector;

                    // enlarge bounding box
                    var center_bbox = new Point((bbox.tl().x + bbox.br().x) / 2, (bbox.tl().y + bbox.br().y) / 2);
                    wh_bbox = bbox.br() - bbox.tl();
                    var new_half_size = new Point(wh_bbox.x * HAND_BOX_ENLARGE_FACTOR / 2.0, wh_bbox.y * HAND_BOX_ENLARGE_FACTOR / 2.0);
                    bbox = new OpenCVRect(new Point(center_bbox.x - new_half_size.x, center_bbox.y - new_half_size.y),
                                             new Point(center_bbox.x + new_half_size.x, center_bbox.y + new_half_size.y));

                    // Return coordinates normalized by image size (same as MediaPipeHandPoseEstimator)
                    float img_size_w = (float)imgSize.width;
                    float img_size_h = (float)imgSize.height;

                    return (
                        Mathf.Clamp((float)bbox.tl().x, 0, img_size_w),
                        Mathf.Clamp((float)bbox.tl().y, 0, img_size_h),
                        Mathf.Clamp((float)bbox.br().x, 0, img_size_w),
                        Mathf.Clamp((float)bbox.br().y, 0, img_size_h)
                    );
                }
            }

            /// <summary>
            /// Disposes all resources held by this TrakingHand instance.
            /// </summary>
            internal void Dispose()
            {
                _lastPalmDetectionResult?.Dispose();
                _lastPalmDetectionResult = null;
                _lastHandPoseResult?.Dispose();
                _lastHandPoseResult = null;

                // Dispose filter arrays are not needed as they don't hold unmanaged resources
                // but we clear them for consistency
                _landmarkOneEuroFilters = null;
                _landmarkMovingAverageFilters = null;
                _poseHistory = null;
            }
        }
    }
}
#endif
