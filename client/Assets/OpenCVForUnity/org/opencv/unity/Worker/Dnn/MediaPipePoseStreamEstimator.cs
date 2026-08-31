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
    /// MediaPipe Pose Stream Estimator for continuous pose tracking in video streams.
    ///
    /// This class implements a video-mode pose estimation pipeline that:
    /// - Uses MediaPipePersonDetector for initial person detection and tracking loss recovery
    /// - Uses MediaPipePoseEstimator for 33-point pose landmark estimation
    /// - Applies temporal smoothing and filtering to reduce jitter and outliers
    /// - Maintains tracking state to optimize performance across frames
    /// - Supports mask and heatmap output options
    /// - Implements body part-specific smoothing (torso vs extremity landmarks)
    ///
    /// The processing flow:
    /// 1. Check if person detection is needed based on tracking state (NotInitialized, Lost with interval)
    /// 2. If needed: Run person detection and find best person by confidence
    /// 3. Run pose estimation with detected person region and store results
    /// 4. For tracking person: Run pose estimation with reused person detection if not executed
    /// 5. Apply temporal smoothing (One Euro Filter, Moving Average) with body part-specific parameters
    /// 6. Recalculate bounding box from smoothed landmarks for coordinate consistency
    /// 7. Generate person detection from smoothed results for next frame tracking
    /// 8. Support mask and heatmap output options based on flags
    /// </summary>
    public class MediaPipePoseStreamEstimator : ProcessingWorkerBase
    {
        /// <summary>
        /// Represents the current tracking state of the person.
        /// </summary>
        public enum TrackingPersonState : byte
        {
            NotInitialized,
            Tracking,
            Lost
        }

        // Tracking parameters
        protected const int PERSON_DETECTION_INTERVAL_LOST = 3; // Run person detection every 3 frames when lost (MediaPipe standard)

        protected float _poseConfThreshold;
        protected float _personScoreThreshold;
        protected int _backend;
        protected int _target;
        protected bool _useMask;
        protected bool _useHeatmap;
        protected MediaPipePersonDetector _personDetector;
        protected MediaPipePoseEstimator _poseEstimator;

        // Tracking state
        protected int _lostStateFrameCounter; // Counter for person detection interval in lost state
        private TrackingPerson _trackingPerson; // Single person tracking

        /// <summary>
        /// Initializes a new instance of the MediaPipePoseStreamEstimator class.
        /// </summary>
        /// <param name="personModelFilepath">Path to the person detection model file.</param>
        /// <param name="poseModelFilepath">Path to the pose estimation model file.</param>
        /// <param name="personScoreThreshold">Score threshold for person detection.</param>
        /// <param name="poseConfThreshold">Confidence threshold for filtering pose estimations.</param>
        /// <param name="maxTrackingLossFrames">Maximum number of consecutive frames to allow tracking loss before considering person as lost.</param>
        /// <param name="backend">Preferred DNN backend.</param>
        /// <param name="target">Preferred DNN target device.</param>
        public MediaPipePoseStreamEstimator(string personModelFilepath, string poseModelFilepath,
                                          float personScoreThreshold = 0.5f, float poseConfThreshold = 0.5f,
                                          int maxTrackingLossFrames = 5, int backend = Dnn.DNN_BACKEND_OPENCV, int target = Dnn.DNN_TARGET_CPU)
        {
            if (string.IsNullOrEmpty(personModelFilepath))
                throw new ArgumentException("Person model filepath cannot be empty.", nameof(personModelFilepath));
            if (string.IsNullOrEmpty(poseModelFilepath))
                throw new ArgumentException("Pose model filepath cannot be empty.", nameof(poseModelFilepath));

            _personScoreThreshold = Mathf.Clamp01(personScoreThreshold);
            _poseConfThreshold = Mathf.Clamp01(poseConfThreshold);
            _backend = backend;
            _target = target;

            try
            {
                _personDetector = new MediaPipePersonDetector(personModelFilepath, scoreThreshold: _personScoreThreshold, backend: _backend, target: _target);
                _poseEstimator = new MediaPipePoseEstimator(poseModelFilepath, confThreshold: _poseConfThreshold, backend: _backend, target: _target);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN models. Invalid model file paths or corrupted model files.", e);
            }

            // Initialize tracking state
            _lostStateFrameCounter = 0;

            // Initialize tracking person (single person tracking)
            _trackingPerson = new TrackingPerson(_poseEstimator, maxTrackingLossFrames);
        }

        /// <summary>
        /// Visualizes pose estimation result on the input image using Mat.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="result">The result Mat returned by Estimate method.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result == null || result.empty())
                return;

            // Use the pose estimator's visualization method directly
            // The result Mat should have the same structure as the original MediaPipePoseEstimator output
            _poseEstimator.Visualize(image, result, printResult, isRGB);
        }

        /// <summary>
        /// Visualizes pose estimation mask on the input image using Mat.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="mask">The mask Mat returned by Estimate method.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public virtual void VisualizeMask(Mat image, Mat mask, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (mask == null || mask.empty())
                return;

            // Use the pose estimator's mask visualization method directly
            _poseEstimator.VisualizeMask(image, mask, isRGB);
        }

        /// <summary>
        /// Estimates pose in the input video frame with temporal smoothing.
        /// </summary>
        /// <remarks>
        /// This method implements the video-mode pose estimation pipeline:
        /// - First frame or tracking loss: Runs person detection
        /// - Uses detected person region for pose estimation
        /// - Applies temporal smoothing to reduce jitter
        /// - Maintains tracking state for optimization
        /// - Supports mask and heatmap output options
        /// - Generates person detection from smoothed results for next frame
        ///
        /// The returned Mat format:
        /// - Mat: Pose estimation result (317 rows, 1 column)
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
        /// <param name="useMask">Whether to enable mask output.</param>
        /// <param name="useHeatmap">Whether to enable heatmap output.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Estimate(Mat image, bool useMask = false, bool useHeatmap = false, bool useCopyOutput = false)
        {
            // Set mask and heatmap flags for this estimation
            _useMask = useMask;
            _useHeatmap = useHeatmap;

            Execute(image);

            if (useCopyOutput)
            {
                // Create copy of output
                return CopyOutput(0); // Pose result
            }
            else
            {
                // Return reference to internal buffer
                return PeekOutput(0); // Pose result
            }
        }

        /// <summary>
        /// Estimates pose in the input video frame asynchronously with temporal smoothing.
        /// </summary>
        /// <remarks>
        /// This is a specialized async method for video-mode pose estimation that:
        /// - Takes a single BGR image as input
        /// - Returns a Mat containing estimation result
        /// - Applies temporal smoothing and tracking optimization
        /// - Supports mask and heatmap output options
        /// - Generates person detection from smoothed results for next frame
        ///
        /// The returned Mat format:
        /// - Mat: Pose estimation result (317 rows, 1 column)
        /// - Rows: [bbox_coords, landmarks_coords, landmarks_coords_world, conf]
        /// - Use ToStructuredData() to convert to a more convenient format
        ///
        /// Only one estimation operation can run at a time.
        /// </remarks>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="useMask">Whether to enable mask output.</param>
        /// <param name="useHeatmap">Whether to enable heatmap output.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with estimation result. The caller is responsible for disposing this Mat.</returns>
        public virtual async Task<Mat> EstimateAsync(Mat image, bool useMask = false, bool useHeatmap = false, CancellationToken cancellationToken = default)
        {
            // Set mask and heatmap flags for this estimation
            _useMask = useMask;
            _useHeatmap = useHeatmap;

            await ExecuteAsync(image, cancellationToken);

            // Always create copy for async safety
            return CopyOutput(0); // Pose result
        }

        /// <summary>
        /// Converts the estimation result matrix to a PoseEstimationBlazeData structure.
        /// </summary>
        /// <param name="result">Estimation result matrix from Execute method.</param>
        /// <returns>PoseEstimationBlazeData structure containing pose information.</returns>
        public virtual MediaPipePoseEstimator.PoseEstimationBlazeData ToStructuredData(Mat result)
        {
            // Use the pose estimator's structured data conversion method directly
            // All necessary validation is performed in _poseEstimator.ToStructuredData
            return _poseEstimator.ToStructuredData(result);
        }

        /// <summary>
        /// Gets the current tracking state for the person.
        /// </summary>
        /// <returns>Current tracking state.</returns>
        public virtual TrackingPersonState GetTrackingState()
        {
            return _trackingPerson?.TrackingState ?? TrackingPersonState.NotInitialized;
        }

        /// <summary>
        /// Resets the tracking state, forcing person detection on the next frame.
        /// </summary>
        public virtual void ResetTracking()
        {
            _lostStateFrameCounter = 0;

            // Reset tracking person
            if (_trackingPerson != null) _trackingPerson.Reset();
        }

        /// <summary>
        /// Core processing method that implements the video-mode pose estimation pipeline.
        /// This method handles person detection, pose estimation, temporal smoothing, and tracking state management.
        /// </summary>
        /// <param name="inputs">Input array containing the image Mat.</param>
        /// <returns>Mat array containing pose estimation results [pose, mask, heatmap].</returns>
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
                // Clear pose estimation executed flag for new frame
                if (_trackingPerson != null) _trackingPerson.ClearPoseEstimationExecuted();

                // Increment lost state frame counter when in lost state
                bool personLost = _trackingPerson != null && _trackingPerson.TrackingState == TrackingPersonState.Lost;
                if (personLost)
                {
                    _lostStateFrameCounter++;
                }

                // Visualize normal person detection
                // Mat personDetection0 = _personDetector.Detect(image, useCopyOutput: false);
                // _personDetector.Visualize(image, personDetection0, false, true);

                // Determine if we need person detection
                bool needsPersonDetection = ShouldRunPersonDetection();

                if (needsPersonDetection)
                {

                    // Run person detection directly using _personDetector
                    Mat personDetection = _personDetector.Detect(image, useCopyOutput: false);

                    //_personDetector.Visualize(image, personDetection, false, true);

                    if (!personDetection.empty())
                    {
                        // Find the person with highest confidence
                        int bestPersonIndex = FindBestPersonByConfidence(personDetection);

                        if (bestPersonIndex >= 0)
                        {
                            // Get the best person detection
                            Mat bestPersonDetection = personDetection.row(bestPersonIndex).clone();

                            // Run pose estimation with the best person detection
                            Mat initialPoseResult = _poseEstimator.Estimate(image, bestPersonDetection, useCopyOutput: false, useMask: _useMask, useHeatmap: _useHeatmap);

                            if (!initialPoseResult.empty())
                            {
                                // Store results and update tracking state
                                _trackingPerson.SetLastPersonDetectionResult(bestPersonDetection);
                                _trackingPerson.SetLastPoseResult(initialPoseResult);
                                _trackingPerson.SetPoseEstimationExecuted();
                                _trackingPerson.UpdateTrackingState(true);
                            }
                            else
                            {
                                // Pose estimation failed, update tracking state
                                _trackingPerson.UpdateTrackingState(false);
                                bestPersonDetection?.Dispose();
                            }
                        }

                        // Note: personDetection is a reference to internal buffer
                        // when useCopyOutput: false, so it should not be disposed here
                        // personDetection?.Dispose();

                        _lostStateFrameCounter = 0;
                    }
                }

                // Get image size from the input image
                var imgSize = (inputs[0].width(), inputs[0].height());

                // Process person: pose estimation and temporal smoothing
                if (_trackingPerson != null && _trackingPerson.TrackingState == TrackingPersonState.Tracking)
                {
                    // Run pose estimation if not already executed this frame
                    if (!_trackingPerson.PoseExecutedThisFrame)
                    {

                        // Run pose estimation with reused person detection
                        Mat trackingPoseResult = _poseEstimator.Estimate(image, _trackingPerson.LastPersonDetectionResult, useCopyOutput: false, useMask: _useMask, useHeatmap: _useHeatmap);

                        if (trackingPoseResult.empty())
                        {
                            // Pose estimation failed, update tracking state
                            _trackingPerson.UpdateTrackingState(false);
                        }
                        else
                        {
                            // Store trackingPoseResult for next frame
                            _trackingPerson.SetLastPoseResult(trackingPoseResult);
                        }

                        // Note: trackingPoseResult is a reference to internal buffer
                        // when useCopyOutput: false, so it should not be disposed here
                        // trackingPoseResult?.Dispose();

                        // Set pose estimation executed flag
                        _trackingPerson.SetPoseEstimationExecuted();
                    }

                    // Apply temporal smoothing and update tracking state for next frame
                    _trackingPerson.ApplySmoothingAndUpdateTracking(imgSize);

                    // Visualize _trackingPerson.LastPersonDetectionResult
                    // _personDetector.Visualize(image, _trackingPerson.LastPersonDetectionResult, false, false);
                }
            }
            catch (Exception e)
            {
                // On error, reset tracking
                if (_trackingPerson != null) _trackingPerson.UpdateTrackingState(false);
                throw new ArgumentException("Error during pose estimation: " + e.Message, e);
            }

            // Return the final Pose result
            // Return LastPoseResult if in tracking state, otherwise return empty Mat
            Mat finalPoseResult = (_trackingPerson?.TrackingState == TrackingPersonState.Tracking && _trackingPerson.LastPoseResult != null && !_trackingPerson.LastPoseResult.empty())
                ? _trackingPerson.LastPoseResult.clone() // Create a copy to avoid disposal issues
                : new Mat();

            // Get mask and heatmap results based on flags
            Mat maskResult = null;
            Mat heatmapResult = null;

            if (_useMask)
            {
                maskResult = _poseEstimator.CopyOutput(1); // Get mask from pose estimator
            }

            if (_useHeatmap)
            {
                heatmapResult = _poseEstimator.CopyOutput(2); // Get heatmap from pose estimator
            }

            return new Mat[] { finalPoseResult, maskResult, heatmapResult };
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected virtual bool ShouldRunPersonDetection()
        {
            // Check if person needs detection
            if (_trackingPerson == null)
                return true;

            switch (_trackingPerson.TrackingState)
            {
                case TrackingPersonState.NotInitialized:
                    return true;
                case TrackingPersonState.Tracking:
                    return false; // Reuse previous detection (MediaPipe standard approach)
                case TrackingPersonState.Lost:
                    // Run person detection at regular intervals when lost (MediaPipe standard approach)
                    return (_lostStateFrameCounter % PERSON_DETECTION_INTERVAL_LOST) == 0;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Finds the person with the highest confidence score from person detection results.
        /// </summary>
        /// <param name="personDetection">Person detection results.</param>
        /// <returns>Index of the person with highest confidence, or -1 if no valid person found.</returns>
        protected virtual int FindBestPersonByConfidence(Mat personDetection)
        {
            if (personDetection.empty() || personDetection.rows() == 0)
                return -1;

            int bestIndex = -1;
            float bestConfidence = 0.0f;

            // Iterate through all detected persons and find the one with highest confidence
            for (int i = 0; i < personDetection.rows(); i++)
            {
                float confidence = GetPersonConfidence(personDetection.row(i));
                if (confidence > bestConfidence)
                {
                    bestConfidence = confidence;
                    bestIndex = i;
                }
            }

            return bestIndex;
        }

        protected virtual float GetPersonConfidence(Mat personDetection)
        {
            if (personDetection.empty() || personDetection.rows() == 0)
                return 0.0f;

            // Get confidence from the last column (score)
            int scoreColumn = personDetection.cols() - 1;
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            return personDetection.at<float>(0, scoreColumn)[0];
#else
            float[] score = new float[1];
            personDetection.get(0, scoreColumn, score);
            return score[0];
#endif
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _personDetector?.Dispose(); _personDetector = null;
                _poseEstimator?.Dispose(); _poseEstimator = null;

                // Dispose tracking person
                _trackingPerson?.Dispose();
                _trackingPerson = null;
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
        /// Private class to manage person tracking state and temporal smoothing buffers.
        /// This class encapsulates all tracking-related data and provides methods for state management.
        /// It handles person detection result storage, pose result storage, temporal smoothing,
        /// and tracking state transitions for person pose estimation.
        /// </summary>
        private class TrackingPerson
        {
            // Detectors
            private MediaPipePoseEstimator _poseEstimator;

            // Tracking state
            private TrackingPersonState _trackingState;
            private int _trackingLossFrames;

            // Frame execution tracking
            private bool _isPoseExecutedThisFrame = false;

            // Tracking results
            private Mat _lastPersonDetectionResult;
            private Mat _lastPoseResult;

            // Filter control flags
            private bool _enableOneEuroFilter;
            private bool _enableMovingAverageFilter;

            // Temporal smoothing buffers
            private Queue<MediaPipePoseEstimator.PoseEstimationBlazeData> _poseHistory;
            private OneEuroFilter[] _landmarkOneEuroFilters;
            private MovingAverageFilter[] _landmarkMovingAverageFilters;

            // Temporal smoothing constants for person pose (MediaPipe standard values)
            private const float ONE_EURO_FREQUENCY = 30.0f;
            private const float ONE_EURO_D_CUTOFF = 1.0f;
            private const int MOVING_AVERAGE_WINDOW = 3;

            // OneEuroFilter parameters for different body parts
            // Torso parts (face, shoulders, hips) - slower movement, stronger smoothing
            private const float ONE_EURO_MIN_CUTOFF_TORSO = 0.5f;
            private const float ONE_EURO_BETA_TORSO = 0.1f;

            // Extremity parts (arms, hands, legs, feet) - faster movement, lighter smoothing
            private const float ONE_EURO_MIN_CUTOFF_EXTREMITY = 1.5f;
            private const float ONE_EURO_BETA_EXTREMITY = 0.3f;

            /// <summary>
            /// Determines if a landmark index belongs to torso parts (face, shoulders, hips) or extremity parts (arms, hands, legs, feet).
            /// </summary>
            /// <param name="landmarkIndex">The landmark index (0-38 for MediaPipe pose landmarks).</param>
            /// <returns>True if the landmark is a torso part, false if it's an extremity part.</returns>
            private static bool IsTorsoLandmark(int landmarkIndex)
            {
                // MediaPipe pose landmarks mapping:
                // 0-10: Face (nose, eyes, ears, mouth) - torso
                // 11-12: Shoulders - torso
                // 13-22: Arms and hands (elbows, wrists, fingers) - extremity
                // 23-24: Hips - torso
                // 25-32: Legs and feet (knees, ankles, heels, toes) - extremity
                // 33-38: Additional landmarks (if any) - treat as extremity for safety

                return (landmarkIndex >= 0 && landmarkIndex <= 12) || // Face and shoulders
                       (landmarkIndex >= 23 && landmarkIndex <= 24);   // Hips
            }

            // Bounding box calculation constants (same as MediaPipePoseEstimator)
            private static readonly Point PERSON_BOX_SHIFT_VECTOR = new Point(0, -0.1);
            private const double PERSON_BOX_ENLARGE_FACTOR = 1.25;

            // Tracking parameters
            private int _maxTrackingLossFrames;

            /// <summary>
            /// Initializes a new instance of the TrackingPerson class.
            /// </summary>
            /// <param name="poseEstimator">The pose estimator instance.</param>
            /// <param name="maxTrackingLossFrames">Maximum number of consecutive frames to allow tracking loss before considering person as lost.</param>
            /// <param name="enableOneEuroFilter">Whether to enable One Euro Filter for temporal smoothing.</param>
            /// <param name="enableMovingAverageFilter">Whether to enable Moving Average Filter for temporal smoothing.</param>
            internal TrackingPerson(MediaPipePoseEstimator poseEstimator, int maxTrackingLossFrames = 5, bool enableOneEuroFilter = true, bool enableMovingAverageFilter = false)
            {
                if (poseEstimator == null)
                    throw new ArgumentNullException(nameof(poseEstimator), "Pose estimator cannot be null.");

                _poseEstimator = poseEstimator;

                // Initialize tracking state
                _trackingState = TrackingPersonState.NotInitialized;
                _trackingLossFrames = 0;
                _maxTrackingLossFrames = Math.Max(1, maxTrackingLossFrames);

                // Initialize filter control flags
                _enableOneEuroFilter = enableOneEuroFilter;
                _enableMovingAverageFilter = enableMovingAverageFilter;

                // Initialize smoothing buffers
                _poseHistory = new Queue<MediaPipePoseEstimator.PoseEstimationBlazeData>();
                _landmarkOneEuroFilters = new OneEuroFilter[MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3 + MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_WORLD_COUNT * 3]; // Screen (3 elements: X,Y,Z) + World (3 elements)
                _landmarkMovingAverageFilters = new MovingAverageFilter[MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3 + MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_WORLD_COUNT * 3]; // Screen + World

                for (int i = 0; i < _landmarkOneEuroFilters.Length; i++)
                {
                    // Calculate landmark index from filter index
                    // Each landmark has 3 components (X, Y, Z) for screen coordinates
                    // and 3 components (X, Y, Z) for world coordinates
                    int landmarkIndex = i / 6; // 6 components per landmark (3 screen + 3 world)

                    // Determine filter parameters based on landmark type
                    float minCutoff, beta;
                    if (IsTorsoLandmark(landmarkIndex))
                    {
                        // Torso parts (face, shoulders, hips) - stronger smoothing
                        minCutoff = ONE_EURO_MIN_CUTOFF_TORSO;
                        beta = ONE_EURO_BETA_TORSO;
                    }
                    else
                    {
                        // Extremity parts (arms, hands, legs, feet) - lighter smoothing
                        minCutoff = ONE_EURO_MIN_CUTOFF_EXTREMITY;
                        beta = ONE_EURO_BETA_EXTREMITY;
                    }

                    _landmarkOneEuroFilters[i] = new OneEuroFilter(ONE_EURO_FREQUENCY, minCutoff, beta, ONE_EURO_D_CUTOFF);
                    _landmarkMovingAverageFilters[i] = new MovingAverageFilter(MOVING_AVERAGE_WINDOW);
                }
            }

            /// <summary>
            /// Gets the current tracking state.
            /// </summary>
            internal TrackingPersonState TrackingState
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
            /// Gets whether pose estimation was executed this frame.
            /// </summary>
            internal bool PoseExecutedThisFrame => _isPoseExecutedThisFrame;

            /// <summary>
            /// Gets the last person detection result.
            /// </summary>
            internal Mat LastPersonDetectionResult
            {
                get { return _lastPersonDetectionResult; }
            }

            /// <summary>
            /// Gets the last pose result.
            /// </summary>
            internal Mat LastPoseResult
            {
                get { return _lastPoseResult; }
            }

            /// <summary>
            /// Sets the last person detection result, disposing the previous one if it exists.
            /// </summary>
            /// <param name="personDetectionResult">The new person detection result.</param>
            internal void SetLastPersonDetectionResult(Mat personDetectionResult)
            {
                _lastPersonDetectionResult?.Dispose();
                _lastPersonDetectionResult = personDetectionResult;
            }

            /// <summary>
            /// Sets the last pose result, disposing the previous one if it exists.
            /// </summary>
            /// <param name="poseResult">The new pose result.</param>
            internal void SetLastPoseResult(Mat poseResult)
            {
                _lastPoseResult?.Dispose();
                _lastPoseResult = poseResult;
            }

            /// <summary>
            /// Resets all tracking data and smoothing buffers.
            /// </summary>
            internal void Reset()
            {
                // Reset tracking state
                _trackingState = TrackingPersonState.NotInitialized;
                _trackingLossFrames = 0;

                _lastPersonDetectionResult?.Dispose();
                _lastPersonDetectionResult = null;
                _lastPoseResult?.Dispose();
                _lastPoseResult = null;

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
            /// Called when person tracking starts successfully.
            /// </summary>
            private void OnTrackingStart()
            {
                // Default implementation does nothing
                // Override this method in derived classes to handle tracking start events
                // Debug.Log("OnTrackingStart: Person tracking started");
            }

            /// <summary>
            /// Called when person tracking is lost.
            /// </summary>
            private void OnTrackingLost()
            {
                // Default implementation does nothing
                // Override this method in derived classes to handle tracking loss events
                // Debug.Log("OnTrackingLost: Person tracking lost");

                // Reset smoothing buffers when tracking is lost to prevent artifacts from previous tracking session
                ResetSmoothingBuffers();
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
                    bool trackingResumed = (_trackingState == TrackingPersonState.Lost);
                    bool trackingStarted = (_trackingState == TrackingPersonState.NotInitialized);

                    _trackingState = TrackingPersonState.Tracking;
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
                    if (_trackingState == TrackingPersonState.Tracking)
                    {
                        _trackingLossFrames++;
                        if (_trackingLossFrames >= _maxTrackingLossFrames)
                        {
                            // Call OnTrackingLost when transitioning to Lost state
                            OnTrackingLost();
                            _trackingState = TrackingPersonState.Lost;
                        }
                    }
                }
            }

            /// <summary>
            /// Sets the pose estimation executed flag to true.
            /// </summary>
            internal void SetPoseEstimationExecuted()
            {
                _isPoseExecutedThisFrame = true;
            }

            /// <summary>
            /// Clears the pose estimation executed flag.
            /// </summary>
            internal void ClearPoseEstimationExecuted()
            {
                _isPoseExecutedThisFrame = false;
            }

            /// <summary>
            /// Applies temporal smoothing and updates tracking state for the next frame.
            /// This method applies One Euro Filter and Moving Average Filter to smooth landmarks,
            /// recalculates bounding box from smoothed landmarks, and generates person detection
            /// for the next frame to maintain tracking continuity.
            /// </summary>
            /// <param name="imgSize">Image size (width, height).</param>
            internal void ApplySmoothingAndUpdateTracking((double width, double height) imgSize)
            {
                // Apply temporal smoothing using direct Mat operation approach
                Mat smoothedPoseResult = ApplyTemporalSmoothing(_lastPoseResult, imgSize);

                // Create person detection from smoothed result for next frame
                Mat nextFramePersonDetectionResult = CreatePersonDetectionFromSmoothedResult(smoothedPoseResult, imgSize);

                // Store for next frame
                SetLastPoseResult(smoothedPoseResult);

                // Store the generated person detection for next frame
                SetLastPersonDetectionResult(nextFramePersonDetectionResult);
            }

            /// <summary>
            /// Applies temporal smoothing to pose estimation results.
            /// This method converts the result to structured data, applies One Euro Filter and Moving Average Filter
            /// to landmarks with body part-specific parameters, recalculates bounding box from smoothed landmarks,
            /// and converts back to Mat format.
            /// </summary>
            /// <param name="poseResult">The pose estimation result.</param>
            /// <param name="imgSize">Image size (width, height).</param>
            /// <returns>Smoothed pose result.</returns>
            private Mat ApplyTemporalSmoothing(Mat poseResult, (double width, double height) imgSize)
            {
                if (poseResult.empty())
                    return new Mat();

                // Convert to structured data for easier processing
                var poseData = _poseEstimator.ToStructuredData(poseResult);

                // Check if this is the first successful tracking frame and warmup filters
                if (_trackingState == TrackingPersonState.Tracking && _poseHistory.Count == 0)
                {
                    WarmupFilters(poseData);
                }

                // Add to history
                _poseHistory.Enqueue(poseData);
                if (_poseHistory.Count > MOVING_AVERAGE_WINDOW)
                {
                    _poseHistory.Dequeue();
                }

                // Apply smoothing filters to structured data
                var smoothedPoseData = ApplySmoothingFilters(poseData, imgSize);

                // Convert back to Mat while preserving coordinate system
                return ConvertPoseEstimationBlazeDataToMat(smoothedPoseData, poseResult);
            }

            /// <summary>
            /// Creates a person detection Mat from smoothed pose result for use in the next frame.
            /// This method extracts the bounding box and landmarks from the smoothed result and formats them
            /// in the same format as MediaPipePersonDetector output. It calculates MediaPipePersonDetector-style
            /// landmarks (hip center, full body, shoulder center, upper body) and face bounding box.
            /// </summary>
            /// <param name="smoothedPoseResult">The smoothed pose estimation result.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>A Mat in person detection format (13 columns: faceBox + landmarks + score).</returns>
            private Mat CreatePersonDetectionFromSmoothedResult(Mat smoothedPoseResult, (double width, double height) imgSize)
            {
                if (smoothedPoseResult.empty())
                    return new Mat();

                // Convert smoothed result to structured data
                var poseData = _poseEstimator.ToStructuredData(smoothedPoseResult);

                // Create person detection Mat with same format as MediaPipePersonDetector
                // Format: [faceBox_x1, faceBox_y1, faceBox_x2, faceBox_y2, hip_center_x, hip_center_y, full_body_x, full_body_y, shoulder_center_x, shoulder_center_y, upper_body_x, upper_body_y, score]
                Mat personDetection = new Mat(1, 13, CvType.CV_32FC1); // 4 faceBox + 4 landmarks (8 coords) + 1 score

                // Calculate MediaPipePersonDetector-style landmarks from pose landmarks
                var landmarksScreen = poseData.GetLandmarksScreenArray();
                var (hipCenter, fullBody, shoulderCenter, upperBody) = CalculatePersonDetectorLandmarks(landmarksScreen);

                // Calculate face bounding box from face landmarks (MediaPipePersonDetector uses faceBox)
                var (faceX1, faceY1, faceX2, faceY2) = CalculateFaceBoundingBox(landmarksScreen);

                // Convert bounding box to person detection format
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                // Use Mat.at for direct memory access (faster)
                Span<float> personDetectionSpan = personDetection.AsSpan<float>();

                // Set face bounding box values (first 4 elements) - MediaPipePersonDetector uses faceBox
                personDetectionSpan[0] = faceX1;
                personDetectionSpan[1] = faceY1;
                personDetectionSpan[2] = faceX2;
                personDetectionSpan[3] = faceY2;

                // Set landmarks (elements 4-11) - MediaPipePersonDetector format
                personDetectionSpan[4] = hipCenter.x;     // Hip center X
                personDetectionSpan[5] = hipCenter.y;     // Hip center Y
                personDetectionSpan[6] = fullBody.x;      // Full body X
                personDetectionSpan[7] = fullBody.y;      // Full body Y
                personDetectionSpan[8] = shoulderCenter.x; // Shoulder center X
                personDetectionSpan[9] = shoulderCenter.y; // Shoulder center Y
                personDetectionSpan[10] = upperBody.x;    // Upper body X
                personDetectionSpan[11] = upperBody.y;    // Upper body Y

                // Use the confidence from the smoothed result as the score (element 12)
                personDetectionSpan[12] = poseData.Confidence;
#else
                // Fallback to traditional Mat.put method
                float[] faceBox = new float[] { faceX1, faceY1, faceX2, faceY2 };
                personDetection.put(0, 0, faceBox);

                // Set landmarks (4 landmarks with X,Y coordinates) - MediaPipePersonDetector format
                float[] landmarks = new float[8]; // 4 landmarks * 2 coordinates each
                landmarks[0] = hipCenter.x;     // Hip center X
                landmarks[1] = hipCenter.y;     // Hip center Y
                landmarks[2] = fullBody.x;      // Full body X
                landmarks[3] = fullBody.y;      // Full body Y
                landmarks[4] = shoulderCenter.x; // Shoulder center X
                landmarks[5] = shoulderCenter.y; // Shoulder center Y
                landmarks[6] = upperBody.x;     // Upper body X
                landmarks[7] = upperBody.y;     // Upper body Y
                personDetection.put(0, 4, landmarks);

                // Use the confidence from the smoothed result as the score
                float[] score = new float[] { poseData.Confidence };
                personDetection.put(0, 12, score);
#endif

                return personDetection;
            }

            /// <summary>
            /// Calculates MediaPipePersonDetector-style landmarks from pose landmarks.
            /// This method computes the same landmark types as MediaPipePersonDetector:
            /// - Hip center point: Center of left and right hip landmarks
            /// - Full body point: Center of all visible landmarks
            /// - Shoulder center point: Center of left and right shoulder landmarks
            /// - Upper body point: Center of upper body landmarks (shoulders, elbows, wrists)
            /// </summary>
            /// <param name="landmarksScreen">Array of screen landmarks from pose estimation.</param>
            /// <returns>Tuple containing the four landmark points in MediaPipePersonDetector format.</returns>
            private ((float x, float y) hipCenter, (float x, float y) fullBody, (float x, float y) shoulderCenter, (float x, float y) upperBody)
                CalculatePersonDetectorLandmarks(MediaPipePoseEstimator.ScreenLandmark[] landmarksScreen)
            {
                if (landmarksScreen == null || landmarksScreen.Length == 0)
                {
                    return ((0, 0), (0, 0), (0, 0), (0, 0));
                }

                // MediaPipe pose landmark indices (33-point model)
                const int LEFT_HIP = 23;
                const int RIGHT_HIP = 24;
                const int LEFT_SHOULDER = 11;
                const int RIGHT_SHOULDER = 12;

                // Extension factors for landmark calculations
                const float FULL_BODY_EXTENSION_FACTOR = 1.40f;
                const float UPPER_BODY_EXTENSION_FACTOR = 3.0f;

                // 1. Hip center point: Center of left and right hip landmarks
                (float x, float y) hipCenter = (0, 0);
                if (LEFT_HIP < landmarksScreen.Length && RIGHT_HIP < landmarksScreen.Length)
                {
                    hipCenter.x = (landmarksScreen[LEFT_HIP].X + landmarksScreen[RIGHT_HIP].X) / 2.0f;
                    hipCenter.y = (landmarksScreen[LEFT_HIP].Y + landmarksScreen[RIGHT_HIP].Y) / 2.0f;
                }

                // 2. Full body point: Point extended 1.3x from hip center towards eye center
                (float x, float y) fullBody = (0, 0);
                if (LEFT_HIP < landmarksScreen.Length && RIGHT_HIP < landmarksScreen.Length)
                {
                    // Calculate hip center
                    float hipX = (landmarksScreen[LEFT_HIP].X + landmarksScreen[RIGHT_HIP].X) / 2.0f;
                    float hipY = (landmarksScreen[LEFT_HIP].Y + landmarksScreen[RIGHT_HIP].Y) / 2.0f;

                    // Calculate eye center (left and right eyes)
                    const int LEFT_EYE = 2;
                    const int RIGHT_EYE = 5;
                    float eyeCenterX = 0, eyeCenterY = 0;
                    int eyeCount = 0;

                    if (LEFT_EYE < landmarksScreen.Length)
                    {
                        eyeCenterX += landmarksScreen[LEFT_EYE].X;
                        eyeCenterY += landmarksScreen[LEFT_EYE].Y;
                        eyeCount++;
                    }
                    if (RIGHT_EYE < landmarksScreen.Length)
                    {
                        eyeCenterX += landmarksScreen[RIGHT_EYE].X;
                        eyeCenterY += landmarksScreen[RIGHT_EYE].Y;
                        eyeCount++;
                    }

                    eyeCenterX /= eyeCount;
                    eyeCenterY /= eyeCount;

                    // Calculate vector from hip to eye center
                    float vectorX = eyeCenterX - hipX;
                    float vectorY = eyeCenterY - hipY;

                    // Extend the vector by FULL_BODY_EXTENSION_FACTOR from hip center
                    fullBody.x = hipX + vectorX * FULL_BODY_EXTENSION_FACTOR;
                    fullBody.y = hipY + vectorY * FULL_BODY_EXTENSION_FACTOR;
                }

                // 3. Shoulder center point: Center of left and right shoulder landmarks
                (float x, float y) shoulderCenter = (0, 0);
                if (LEFT_SHOULDER < landmarksScreen.Length && RIGHT_SHOULDER < landmarksScreen.Length)
                {
                    shoulderCenter.x = (landmarksScreen[LEFT_SHOULDER].X + landmarksScreen[RIGHT_SHOULDER].X) / 2.0f;
                    shoulderCenter.y = (landmarksScreen[LEFT_SHOULDER].Y + landmarksScreen[RIGHT_SHOULDER].Y) / 2.0f;
                }

                // 4. Upper body point: Point extended 3.0x from shoulder center towards eye center
                (float x, float y) upperBody = (0, 0);
                if (LEFT_SHOULDER < landmarksScreen.Length && RIGHT_SHOULDER < landmarksScreen.Length)
                {
                    // Calculate shoulder center
                    float shoulderX = (landmarksScreen[LEFT_SHOULDER].X + landmarksScreen[RIGHT_SHOULDER].X) / 2.0f;
                    float shoulderY = (landmarksScreen[LEFT_SHOULDER].Y + landmarksScreen[RIGHT_SHOULDER].Y) / 2.0f;

                    // Calculate eye center (left and right eyes)
                    const int LEFT_EYE = 2;
                    const int RIGHT_EYE = 5;
                    float eyeCenterX = 0, eyeCenterY = 0;
                    int eyeCount = 0;

                    if (LEFT_EYE < landmarksScreen.Length)
                    {
                        eyeCenterX += landmarksScreen[LEFT_EYE].X;
                        eyeCenterY += landmarksScreen[LEFT_EYE].Y;
                        eyeCount++;
                    }
                    if (RIGHT_EYE < landmarksScreen.Length)
                    {
                        eyeCenterX += landmarksScreen[RIGHT_EYE].X;
                        eyeCenterY += landmarksScreen[RIGHT_EYE].Y;
                        eyeCount++;
                    }

                    eyeCenterX /= eyeCount;
                    eyeCenterY /= eyeCount;

                    // Calculate vector from shoulder center to eye center
                    float vectorX = eyeCenterX - shoulderX;
                    float vectorY = eyeCenterY - shoulderY;

                    // Extend the vector by UPPER_BODY_EXTENSION_FACTOR from shoulder center
                    upperBody.x = shoulderX + vectorX * UPPER_BODY_EXTENSION_FACTOR;
                    upperBody.y = shoulderY + vectorY * UPPER_BODY_EXTENSION_FACTOR;
                }

                return (hipCenter, fullBody, shoulderCenter, upperBody);
            }

            /// <summary>
            /// Calculates face bounding box from pose landmarks.
            /// This method computes the bounding box around face landmarks to match MediaPipePersonDetector's faceBox format.
            /// </summary>
            /// <param name="landmarksScreen">Array of screen landmarks from pose estimation.</param>
            /// <returns>Face bounding box coordinates (x1, y1, x2, y2).</returns>
            private (float x1, float y1, float x2, float y2) CalculateFaceBoundingBox(MediaPipePoseEstimator.ScreenLandmark[] landmarksScreen)
            {
                if (landmarksScreen == null || landmarksScreen.Length == 0)
                {
                    return (0, 0, 0, 0);
                }

                // MediaPipe pose landmark indices for face landmarks (33-point model)
                const int NOSE = 0;
                const int LEFT_EYE_INNER = 1;
                const int LEFT_EYE = 2;
                const int LEFT_EYE_OUTER = 3;
                const int RIGHT_EYE_INNER = 4;
                const int RIGHT_EYE = 5;
                const int RIGHT_EYE_OUTER = 6;
                const int LEFT_EAR = 7;
                const int RIGHT_EAR = 8;
                const int MOUTH_LEFT = 9;
                const int MOUTH_RIGHT = 10;

                // Collect all visible face landmarks
                var faceLandmarks = new List<(float x, float y)>();
                int[] faceIndices = { NOSE, LEFT_EYE_INNER, LEFT_EYE, LEFT_EYE_OUTER, RIGHT_EYE_INNER, RIGHT_EYE, RIGHT_EYE_OUTER, LEFT_EAR, RIGHT_EAR, MOUTH_LEFT, MOUTH_RIGHT };

                foreach (int index in faceIndices)
                {
                    if (index < landmarksScreen.Length)
                    {
                        faceLandmarks.Add((landmarksScreen[index].X, landmarksScreen[index].Y));
                    }
                }

                if (faceLandmarks.Count == 0)
                {
                    return (0, 0, 0, 0);
                }

                // Calculate bounding box from face landmarks
                float minX = faceLandmarks[0].x;
                float maxX = faceLandmarks[0].x;
                float minY = faceLandmarks[0].y;
                float maxY = faceLandmarks[0].y;

                for (int i = 1; i < faceLandmarks.Count; i++)
                {
                    minX = Mathf.Min(minX, faceLandmarks[i].x);
                    maxX = Mathf.Max(maxX, faceLandmarks[i].x);
                    minY = Mathf.Min(minY, faceLandmarks[i].y);
                    maxY = Mathf.Max(maxY, faceLandmarks[i].y);
                }

                // Calculate center point of the bounding box
                float centerX = (minX + maxX) / 2f;
                float centerY = (minY + maxY) / 2f;

                // Calculate current width and height
                float currentWidth = maxX - minX;
                float currentHeight = maxY - minY;

                // Calculate new dimensions by doubling the larger dimension
                float maxDimension = Mathf.Max(currentWidth, currentHeight);
                float newWidth = maxDimension * 2f;
                float newHeight = maxDimension * 2f;

                // Calculate new bounding box coordinates centered on the original center
                minX = centerX - newWidth / 2f;
                maxX = centerX + newWidth / 2f;
                minY = centerY - newHeight / 2f;
                maxY = centerY + newHeight / 2f;

                return (minX, minY, maxX, maxY);
            }

            /// <summary>
            /// Applies smoothing filters to landmarks and recalculates bounding box from smoothed landmarks.
            /// This is the correct MediaPipe approach for maintaining coordinate system consistency.
            /// The method applies One Euro Filter and Moving Average Filter to both screen and world coordinates
            /// with body part-specific parameters (torso vs extremity), then recalculates the bounding box
            /// from the smoothed landmarks to maintain coordinate consistency.
            /// </summary>
            /// <param name="poseData">The pose data to smooth.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>Pose data with smoothed landmarks and recalculated bounding box.</returns>
            private MediaPipePoseEstimator.PoseEstimationBlazeData ApplySmoothingFilters(
                MediaPipePoseEstimator.PoseEstimationBlazeData poseData, (double width, double height) imgSize)
            {
                // Calculate absolute timestamp for OneEuroFilter using Environment.TickCount (worker thread compatible)
                int currentTimeMs = Environment.TickCount;
                float currentTimestamp = currentTimeMs / 1000.0f; // Convert to seconds

                // Get landmarks
                var landmarksScreen = poseData.GetLandmarksScreenArray();
                var landmarksWorld = poseData.GetLandmarksWorldArray();

                // Apply One Euro Filter and Moving Average to landmarks based on enabled flags
                var smoothedLandmarksScreen = new MediaPipePoseEstimator.ScreenLandmark[landmarksScreen.Length];
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
                        // Screen coordinates use first part of the array (3 elements per landmark: X,Y,Z)
                        int screenBaseIndex = i * 3;
                        smoothedLandmarksScreen[i] = new MediaPipePoseEstimator.ScreenLandmark(
                            _landmarkOneEuroFilters[screenBaseIndex].Filter(landmarksScreen[i].X, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp),
                            _landmarkOneEuroFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Y, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp),
                            _landmarkOneEuroFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Z, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp),
                            landmarksScreen[i].Visibility, // No smoothing for visibility
                            landmarksScreen[i].Presence    // No smoothing for presence
                        );

                        // World coordinates use second part of the array (3 elements per landmark)
                        int worldBaseIndex = (MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3) + (i * 3);
                        smoothedLandmarksWorld[i] = new Vec3f(
                            _landmarkOneEuroFilters[worldBaseIndex].Filter(landmarksWorld[i].Item1, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp),
                            _landmarkOneEuroFilters[worldBaseIndex + 1].Filter(landmarksWorld[i].Item2, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp),
                            _landmarkOneEuroFilters[worldBaseIndex + 2].Filter(landmarksWorld[i].Item3, landmarksScreen[i].Visibility, landmarksScreen[i].Presence, currentTimestamp)
                        );
                    }
                }

                // Step 2: Apply Moving Average Filter after One Euro Filter if enabled
                if (_enableMovingAverageFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates
                        int screenBaseIndex = i * 3;
                        smoothedLandmarksScreen[i] = new MediaPipePoseEstimator.ScreenLandmark(
                            _landmarkMovingAverageFilters[screenBaseIndex].Filter(smoothedLandmarksScreen[i].X),
                            _landmarkMovingAverageFilters[screenBaseIndex + 1].Filter(smoothedLandmarksScreen[i].Y),
                            _landmarkMovingAverageFilters[screenBaseIndex + 2].Filter(smoothedLandmarksScreen[i].Z),
                            smoothedLandmarksScreen[i].Visibility, // No smoothing for visibility
                            smoothedLandmarksScreen[i].Presence    // No smoothing for presence
                        );

                        // World coordinates
                        int worldBaseIndex = (MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3) + (i * 3);
                        smoothedLandmarksWorld[i] = new Vec3f(
                            _landmarkMovingAverageFilters[worldBaseIndex].Filter(smoothedLandmarksWorld[i].Item1),
                            _landmarkMovingAverageFilters[worldBaseIndex + 1].Filter(smoothedLandmarksWorld[i].Item2),
                            _landmarkMovingAverageFilters[worldBaseIndex + 2].Filter(smoothedLandmarksWorld[i].Item3)
                        );
                    }
                }

                // Step 3: Calculate new bounding box from smoothed landmarks (MediaPipe approach)
                var (newX1, newY1, newX2, newY2) = CalculateBoundingBoxFromLandmarks(smoothedLandmarksScreen, imgSize);

                // Create new pose data with smoothed landmarks and recalculated bounding box
                return new MediaPipePoseEstimator.PoseEstimationBlazeData(
                    newX1, newY1, newX2, newY2,
                    smoothedLandmarksScreen, smoothedLandmarksWorld,
                    poseData.Confidence);
            }

            /// <summary>
            /// Warms up the smoothing filters with initial pose data.
            /// </summary>
            /// <param name="poseData">The initial pose data to warm up filters.</param>
            private void WarmupFilters(MediaPipePoseEstimator.PoseEstimationBlazeData poseData)
            {
                // Get landmarks
                var landmarksScreen = poseData.GetLandmarksScreenArray();
                var landmarksWorld = poseData.GetLandmarksWorldArray();

                // Warm up One Euro Filters if enabled
                if (_enableOneEuroFilter)
                {
                    for (int i = 0; i < landmarksScreen.Length; i++)
                    {
                        // Screen coordinates
                        int screenBaseIndex = i * 3;
                        _landmarkOneEuroFilters[screenBaseIndex].Filter(landmarksScreen[i].X, 0.0f);
                        _landmarkOneEuroFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Y, 0.0f);
                        _landmarkOneEuroFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Z, 0.0f);
                        // Skip visibility and presence warmup as they are not smoothed

                        // World coordinates
                        int worldBaseIndex = (MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3) + (i * 3);
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
                        // Screen coordinates
                        int screenBaseIndex = i * 3;
                        _landmarkMovingAverageFilters[screenBaseIndex].Filter(landmarksScreen[i].X);
                        _landmarkMovingAverageFilters[screenBaseIndex + 1].Filter(landmarksScreen[i].Y);
                        _landmarkMovingAverageFilters[screenBaseIndex + 2].Filter(landmarksScreen[i].Z);
                        // Skip visibility and presence warmup as they are not smoothed

                        // World coordinates
                        int worldBaseIndex = (MediaPipePoseEstimator.PoseEstimationBlazeData.LANDMARK_SCREEN_COUNT * 3) + (i * 3);
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
            private Mat ConvertPoseEstimationBlazeDataToMat(MediaPipePoseEstimator.PoseEstimationBlazeData poseData, Mat originalMat)
            {
                // Create result Mat with the same structure as original
                Mat result = new Mat(originalMat.rows(), originalMat.cols(), originalMat.type());

                // Use Marshal.StructureToPtr to convert the structure directly to Mat data
                // This ensures exact byte-level compatibility with the original Mat structure
                Marshal.StructureToPtr(poseData, (IntPtr)result.dataAddr(), false);

                return result;
            }

            /// <summary>
            /// Calculates bounding box from smoothed landmarks using the same approach as MediaPipePoseEstimator.
            /// This follows the exact same logic as the PostProcess method in MediaPipePoseEstimator.
            /// The method applies the same shift vector and enlargement factor to maintain consistency
            /// with the original MediaPipe implementation.
            /// </summary>
            /// <param name="landmarks">The smoothed landmarks.</param>
            /// <param name="imgSize">The image size for coordinate normalization.</param>
            /// <returns>Bounding box coordinates (x1, y1, x2, y2).</returns>
            private (float x1, float y1, float x2, float y2) CalculateBoundingBoxFromLandmarks(MediaPipePoseEstimator.ScreenLandmark[] landmarks, (double width, double height) imgSize)
            {
                if (landmarks == null || landmarks.Length == 0)
                    return (0, 0, 0, 0);

                // Convert landmarks to points for bounding box calculation (same as MediaPipePoseEstimator)
                var landmarks_points = new (float x, float y)[landmarks.Length];
                for (int i = 0; i < landmarks.Length; i++)
                {
                    landmarks_points[i] = (landmarks[i].X, landmarks[i].Y);
                }

                // Use OpenCV's boundingRect (same as MediaPipePoseEstimator)
                using (var points = new MatOfPoint(landmarks_points))
                {
                    var bbox = Imgproc.boundingRect(points);

                    // Apply the same shift and enlargement as MediaPipePoseEstimator
                    // shift bounding box
                    var wh_bbox = bbox.br() - bbox.tl();
                    var shift_vector = new Point(PERSON_BOX_SHIFT_VECTOR.x * wh_bbox.x, PERSON_BOX_SHIFT_VECTOR.y * wh_bbox.y);
                    bbox = bbox + shift_vector;

                    // enlarge bounding box
                    var center_bbox = new Point((bbox.tl().x + bbox.br().x) / 2, (bbox.tl().y + bbox.br().y) / 2);
                    wh_bbox = bbox.br() - bbox.tl();
                    var new_half_size = new Point(wh_bbox.x * PERSON_BOX_ENLARGE_FACTOR / 2.0, wh_bbox.y * PERSON_BOX_ENLARGE_FACTOR / 2.0);
                    bbox = new OpenCVRect(new Point(center_bbox.x - new_half_size.x, center_bbox.y - new_half_size.y),
                                             new Point(center_bbox.x + new_half_size.x, center_bbox.y + new_half_size.y));

                    // Return coordinates normalized by image size (same as MediaPipePoseEstimator)
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
            /// Disposes all resources held by this TrackingPerson instance.
            /// </summary>
            internal void Dispose()
            {
                _lastPersonDetectionResult?.Dispose();
                _lastPersonDetectionResult = null;
                _lastPoseResult?.Dispose();
                _lastPoseResult = null;

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
