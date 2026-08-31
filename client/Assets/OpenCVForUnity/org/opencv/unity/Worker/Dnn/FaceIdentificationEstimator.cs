#if !UNITY_WSA_10_0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityIntegration.Worker.DataStruct;
using OpenCVForUnity.UnityIntegration.MOT.ByteTrack;
using OpenCVForUnity.UnityIntegration.MOT;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Worker.DnnModule
{
    /// <summary>
    /// Face Identification Estimator with BYTETracker integration for robust face detection, tracking, and recognition in video streams.
    ///
    /// This class implements an advanced face identification pipeline that combines:
    /// - FaceDetectorYN for face detection with 5 facial landmarks
    /// - FaceRecognizerSF for face feature extraction and matching
    /// - BYTETracker for robust multi-object tracking across frames
    /// - Hybrid re-recognition logic for improved accuracy
    /// - Registered face database with pre-computed features
    ///
    /// Key Features:
    /// - Multi-face tracking with consistent IDs across frames
    /// - Hybrid re-recognition: periodic + confidence-based re-identification
    /// - Adaptive recognition intervals based on failure patterns
    /// - Thread-safe face registration and management
    /// - Memory-efficient processing with Span<T> support
    ///
    /// The processing flow:
    /// 1. Detect faces using FaceDetectorYN
    /// 2. Update BYTETracker with detections for consistent tracking
    /// 3. For new tracks: perform face recognition using FaceRecognizerSF
    /// 4. For existing tracks: apply hybrid re-recognition logic
    /// 5. Match extracted features against registered faces using dual thresholds
    /// 6. Return tracking results with persistent face IDs
    ///
    /// Usage:
    /// ```csharp
    /// using (var estimator = new FaceIdentificationEstimator(fdModelPath, sfModelPath, inputSize))
    /// {
    ///     // Register faces
    ///     estimator.RegisterFace(alignedFace1, 1, "Person1");
    ///     estimator.RegisterFace(alignedFace2, 2, "Person2");
    ///
    ///     // Detect, track, and identify faces
    ///     var result = estimator.Estimate(image);
    ///     // result format: [N rows x 17 columns]
    ///     // columns 0-14: FaceDetectorYN output (bbox, landmarks, score)
    ///     // column 15: faceId (float, -1 = unknown, >=0 = registered face ID)
    ///     // column 16: trackId (float, BYTETracker track ID)
    /// }
    /// ```
    /// </summary>
    public class FaceIdentificationEstimator : ProcessingWorkerBase
    {
        #region Private Fields

        // Core DNN models for face detection and recognition
        private FaceDetectorYN _faceDetector;        // Face detection with 5 landmarks
        private FaceRecognizerSF _faceRecognizer;   // Face feature extraction and matching

        // BYTETracker integration for robust multi-object tracking
        private BYTETracker _byteTracker;           // Multi-object tracker
        private List<TrackedFace> _trackedFaces;     // Currently tracked faces with recognition state
        private readonly object _trackedFacesLock = new object(); // Thread safety for tracked faces

        // Registered faces database with pre-computed features
        private List<RegisteredFace> _registeredFaces; // Database of known faces
        private readonly object _registeredFacesLock = new object(); // Thread safety for registered faces

        // Face detection configuration parameters
        private float _scoreThreshold;               // Minimum confidence for face detection (0.0-1.0)
        private float _nmsThreshold;                 // Non-maximum suppression threshold (0.0-1.0)
        private int _topK;                          // Maximum detections before NMS
        private double _cosineSimilarThreshold;     // Cosine similarity threshold for face matching
        private double _l2normSimilarThreshold;    // L2 norm threshold for face matching

        // BYTETracker configuration parameters
        private float _trackThresh;                 // Confidence threshold for tracking
        private float _highThresh;                  // Confidence threshold for new track creation
        private float _matchThresh;                 // Matching threshold for track association
        private int _trackBuffer;                   // Frames to keep lost tracks before removal

        // Memory-efficient output buffer (pre-allocated)
        private Mat _outputBuffer;                  // Pre-allocated buffer for result output [N x 17]

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FaceIdentificationEstimator class with BYTETracker integration.
        ///
        /// This constructor sets up the complete face identification pipeline including:
        /// - FaceDetectorYN for face detection with 5 facial landmarks
        /// - FaceRecognizerSF for face feature extraction and matching
        /// - BYTETracker for robust multi-object tracking
        /// - Thread-safe collections for tracked and registered faces
        /// - Pre-allocated output buffer for efficient processing
        /// </summary>
        /// <param name="fdModelFilepath">Path to the face detection model file.</param>
        /// <param name="sfModelFilepath">Path to the face recognition model file.</param>
        /// <param name="inputSize">Size of the input image for face detection.</param>
        /// <param name="scoreThreshold">Score threshold for face detection filtering (0.0-1.0).</param>
        /// <param name="nmsThreshold">NMS threshold for face detection (0.0-1.0).</param>
        /// <param name="topK">Number of top detections to keep before NMS.</param>
        /// <param name="cosineSimilarThreshold">Cosine similarity threshold for face matching (higher = stricter).</param>
        /// <param name="l2normSimilarThreshold">L2 norm similarity threshold for face matching (lower = stricter).</param>
        /// <param name="trackThresh">Confidence threshold for tracking (BYTETracker).</param>
        /// <param name="highThresh">Confidence threshold for adding new tracks (BYTETracker).</param>
        /// <param name="matchThresh">Matching threshold for tracking (BYTETracker).</param>
        /// <param name="trackBuffer">Number of frames to keep lost tracks before removal.</param>
        /// <param name="backend">Preferred DNN backend (0=OpenCV, 1=Intel Inference Engine, etc.).</param>
        /// <param name="target">Preferred DNN target device (0=CPU, 1=OpenCL, etc.).</param>
        public FaceIdentificationEstimator(string fdModelFilepath, string sfModelFilepath, Size inputSize,
                                           float scoreThreshold = 0.9f, float nmsThreshold = 0.3f, int topK = 5000,
                                           double cosineSimilarThreshold = 0.363, double l2normSimilarThreshold = 1.128,
                                           float trackThresh = 0.5f, float highThresh = 0.6f, float matchThresh = 0.8f,
                                           int trackBuffer = 30, int backend = 0, int target = 0)
        {
            if (string.IsNullOrEmpty(fdModelFilepath))
                throw new ArgumentException("Face detection model filepath cannot be empty.", nameof(fdModelFilepath));
            if (string.IsNullOrEmpty(sfModelFilepath))
                throw new ArgumentException("Face recognition model filepath cannot be empty.", nameof(sfModelFilepath));

            _scoreThreshold = Mathf.Clamp01(scoreThreshold);
            _nmsThreshold = Mathf.Clamp01(nmsThreshold);
            _topK = Math.Max(1, topK);
            _cosineSimilarThreshold = cosineSimilarThreshold;
            _l2normSimilarThreshold = l2normSimilarThreshold;

            // BYTETracker configuration
            _trackThresh = trackThresh;
            _highThresh = highThresh;
            _matchThresh = matchThresh;
            _trackBuffer = trackBuffer;

            try
            {
                _faceDetector = FaceDetectorYN.create(fdModelFilepath, "", inputSize, _scoreThreshold, _nmsThreshold, _topK, backend, target);
                _faceRecognizer = FaceRecognizerSF.create(sfModelFilepath, "", backend, target);
                _byteTracker = new BYTETracker(30, _trackBuffer, _trackThresh, _highThresh, _matchThresh);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Failed to initialize DNN models. Invalid model file paths or corrupted model files.", e);
            }

            // Initialize tracking
            _trackedFaces = new List<TrackedFace>();

            // Initialize registered faces storage
            _registeredFaces = new List<RegisteredFace>();

            // Initialize output buffer
            _outputBuffer = new Mat(30, 17, CvType.CV_32FC1); // Pre-allocate for up to 30 faces
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers a face with the given ID and name for future recognition.
        ///
        /// This method extracts face features using FaceRecognizerSF and stores them for efficient matching.
        /// If the faceId already exists, the registration is updated with new data (useful for improving recognition accuracy).
        /// The method is thread-safe and handles resource management automatically.
        /// </summary>
        /// <param name="alignedFace">The aligned face image (already cropped and aligned to standard size).</param>
        /// <param name="faceId">The unique identifier for this face (must be >= 0).</param>
        /// <param name="name">The human-readable name for this face.</param>
        /// <param name="detectionConfidence">The confidence score from face detection (optional, used for quality assessment).</param>
        /// <exception cref="ArgumentException">Thrown when name is null or empty, or faceId is negative.</exception>
        /// <exception cref="ArgumentNullException">Thrown when alignedFace is null.</exception>
        public void RegisterFace(Mat alignedFace, int faceId, string name, float detectionConfidence = 0f)
        {
            ThrowIfDisposed();

            if (alignedFace == null)
                throw new ArgumentNullException(nameof(alignedFace), "Aligned face cannot be null.");
            if (alignedFace != null) alignedFace.ThrowIfDisposed();
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Face name cannot be null or empty.", nameof(name));
            if (faceId < 0)
                throw new ArgumentException("Face ID must be >= 0. Use -1 only for unknown faces (not for registration).", nameof(faceId));

            // Check if faceId already exists
            RegisteredFace existingFace;
            lock (_registeredFacesLock)
            {
                existingFace = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
            }
            if (existingFace != null)
            {
                // FaceId already exists - update with new data
                // Debug.Log($"[RegisterFace] Updating existing face ID {faceId} with new data (confidence: {existingFace.DetectionConfidence:F3} -> {detectionConfidence:F3})");

                // Dispose old data
                existingFace.AlignedFace?.Dispose();
                existingFace.Feature?.Dispose();

                // Extract face features
                Mat newFeature = new Mat();
                _faceRecognizer.feature(alignedFace, newFeature);
                newFeature = newFeature.clone(); // Create a copy for storage

                // Update existing face data
                Mat newAlignedFaceCopy = alignedFace.clone();
                existingFace.AlignedFace = newAlignedFaceCopy;
                existingFace.Feature = newFeature;
                existingFace.DetectionConfidence = detectionConfidence;
                existingFace.Name = name; // Update name as well
                return;
            }

            // New face registration
            // Extract face features
            Mat feature = new Mat();
            _faceRecognizer.feature(alignedFace, feature);
            feature = feature.clone(); // Create a copy for storage

            // Store aligned face image and features
            Mat alignedFaceCopy = alignedFace.clone();
            lock (_registeredFacesLock)
            {
                _registeredFaces.Add(new RegisteredFace(faceId, name, alignedFaceCopy, feature, detectionConfidence));
            }
        }

        /// <summary>
        /// Registers a face from detection results with proper alignment.
        ///
        /// This method aligns and crops the face from the detection result using FaceRecognizerSF,
        /// extracts face features, and stores them for efficient matching.
        /// If the faceId already exists, the registration is updated with new data (useful for improving recognition accuracy).
        /// The detection confidence is automatically extracted from the faceRow parameter.
        /// Note: This method is not thread-safe and should be called from a single thread context.
        /// </summary>
        /// <param name="image">The original image containing the face.</param>
        /// <param name="faceRow">The face detection result row (15 columns).</param>
        /// <param name="faceId">The unique identifier for this face (must be >= 0).</param>
        /// <param name="name">The human-readable name for this face.</param>
        /// <exception cref="ArgumentException">Thrown when name is null/empty, faceId is negative, or alignment fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown when image or faceRow is null.</exception>
        public void RegisterFaceFromDetection(Mat image, Mat faceRow, int faceId, string name)
        {
            ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            if (image != null) image.ThrowIfDisposed();
            if (faceRow == null)
                throw new ArgumentNullException(nameof(faceRow), "Face row cannot be null.");
            if (faceRow != null) faceRow.ThrowIfDisposed();
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Face name cannot be null or empty.", nameof(name));
            if (faceId < 0)
                throw new ArgumentException("Face ID must be >= 0. Use -1 only for unknown faces (not for registration).", nameof(faceId));

            // Extract detection confidence from faceRow using conversion method
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            var faceDetectionSpan = ConvertMatToFaceDetectionDataAsSpan(faceRow);
            float detectionConfidence = faceDetectionSpan[0].Score;
#else
            var faceDetectionData = ConvertMatToFaceDetectionData(faceRow);
            float detectionConfidence = faceDetectionData.Score;
#endif

            // Check if faceId already exists
            var existingFace = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
            if (existingFace != null)
            {
                // FaceId already exists - update with new data
                // Debug.Log($"[RegisterFaceFromDetection] Updating existing face ID {faceId} with new data (confidence: {existingFace.DetectionConfidence:F3} -> {detectionConfidence:F3})");

                // Dispose old data
                existingFace.AlignedFace?.Dispose();
                existingFace.Feature?.Dispose();

                // Align and crop the face using the internal face recognizer
                Mat newAlignedFace = new Mat();
                _faceRecognizer.alignCrop(image, faceRow, newAlignedFace);

                if (newAlignedFace.empty())
                    throw new ArgumentException("Failed to align and crop the face from the detection result.");

                // Extract face features
                Mat newFeature = new Mat();
                _faceRecognizer.feature(newAlignedFace, newFeature);
                newFeature = newFeature.clone(); // Create independent copy to prevent sharing

                // Update existing face data
                existingFace.AlignedFace = newAlignedFace;
                existingFace.Feature = newFeature;
                existingFace.DetectionConfidence = detectionConfidence;
                existingFace.Name = name;
                return;
            }

            // New face registration
            // Align and crop the face using the internal face recognizer
            Mat alignedFace = new Mat();
            _faceRecognizer.alignCrop(image, faceRow, alignedFace);

            if (alignedFace.empty())
                throw new ArgumentException("Failed to align and crop the face from the detection result.");

            // Extract face features
            Mat feature = new Mat();
            _faceRecognizer.feature(alignedFace, feature);
            feature = feature.clone(); // Create independent copy to prevent sharing

            // Store aligned face image and features
            _registeredFaces.Add(new RegisteredFace(faceId, name, alignedFace, feature, detectionConfidence));
        }

        /// <summary>
        /// Unregisters a face with the specified ID.
        /// </summary>
        /// <param name="faceId">The face ID to remove.</param>
        /// <returns>True if the face was found and removed, false otherwise.</returns>
        public bool UnregisterFace(int faceId)
        {
            ThrowIfDisposed();

            RegisteredFace faceToRemove;
            lock (_registeredFacesLock)
            {
                faceToRemove = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
                if (faceToRemove != null)
                {
                    _registeredFaces.Remove(faceToRemove);
                }
            }

            if (faceToRemove != null)
            {
                faceToRemove.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears all registered faces.
        /// </summary>
        public void ClearRegisteredFaces()
        {
            ThrowIfDisposed();

            List<RegisteredFace> facesToDispose;
            lock (_registeredFacesLock)
            {
                facesToDispose = new List<RegisteredFace>(_registeredFaces);
                _registeredFaces.Clear();
            }

            foreach (var face in facesToDispose)
            {
                face.Dispose();
            }
        }

        /// <summary>
        /// Gets the list of registered face IDs.
        /// </summary>
        /// <returns>Array of registered face IDs.</returns>
        public int[] GetRegisteredFaceIds()
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                return _registeredFaces.Select(f => f.FaceId).ToArray();
            }
        }

        /// <summary>
        /// Gets the number of registered faces.
        /// </summary>
        public int RegisteredFaceCount
        {
            get
            {
                lock (_registeredFacesLock)
                {
                    return _registeredFaces.Count;
                }
            }
        }

        /// <summary>
        /// Gets the face name for the specified face ID.
        /// </summary>
        /// <param name="faceId">The face ID to look up.</param>
        /// <returns>The face name, or null if not found.</returns>
        public string GetFaceName(int faceId)
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                var face = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
                return face?.Name;
            }
        }

        /// <summary>
        /// Gets a dictionary mapping face IDs to face names.
        /// </summary>
        /// <returns>Dictionary with face IDs as keys and names as values.</returns>
        public Dictionary<int, string> GetFaceIdNameMap()
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                return _registeredFaces.ToDictionary(f => f.FaceId, f => f.Name);
            }
        }

        /// <summary>
        /// Gets the aligned face image for the specified face ID.
        /// </summary>
        /// <param name="faceId">The face ID to look up.</param>
        /// <returns>The aligned face image, or null if not found.</returns>
        public Mat GetAlignedFace(int faceId)
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                var face = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
                return face?.AlignedFace?.clone(); // Return a copy to prevent external modification
            }
        }

        /// <summary>
        /// Gets the face feature vector for the specified face ID.
        /// </summary>
        /// <param name="faceId">The face ID to look up.</param>
        /// <returns>The face feature vector, or null if not found.</returns>
        public Mat GetFaceFeature(int faceId)
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                var face = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
                return face?.Feature?.clone(); // Return a copy to prevent external modification
            }
        }

        /// <summary>
        /// Gets the detection confidence for the specified face ID.
        /// </summary>
        /// <param name="faceId">The face ID to look up.</param>
        /// <returns>The detection confidence, or -1 if not found.</returns>
        public float GetFaceDetectionConfidence(int faceId)
        {
            ThrowIfDisposed();
            lock (_registeredFacesLock)
            {
                var face = _registeredFaces.FirstOrDefault(f => f.FaceId == faceId);
                return face?.DetectionConfidence ?? -1f;
            }
        }

        /// <summary>
        /// Resets face recognition for all currently tracked faces by setting their FaceId to -1.
        /// This forces all tracked faces to be treated as unknown and will trigger re-recognition
        /// based on the hybrid re-recognition logic.
        /// </summary>
        public void ResetFaceRecognitionForAllTrackedFaces()
        {
            ThrowIfDisposed();

            int resetCount = 0;
            lock (_trackedFacesLock)
            {
                foreach (var trackedFace in _trackedFaces)
                {
                    if (trackedFace.FaceId >= 0)
                    {
                        trackedFace.FaceId = -1;
                        trackedFace.FailedRecognitionCount = 0; // Reset failure count
                        trackedFace.RecognitionInterval = 30; // Reset to default interval
                        resetCount++;
                    }
                }
            }

            // Debug.Log($"[ResetFaceRecognitionForAllTrackedFaces] Reset face recognition for {resetCount} tracked faces. All faces are now treated as unknown.");
        }

        /// <summary>
        /// Updates face recognition for all tracked faces using current registered faces.
        /// This method can be called to re-identify faces when new faces are registered.
        /// </summary>
        /// <param name="image">The input image containing the faces.</param>
        /// <param name="skipExistingFaceIds">If true, skips tracks that already have valid FaceIds (>= 0). If false, re-processes all tracks.</param>
        public void UpdateFaceRecognitionForAllTrackedFaces(Mat image, bool skipExistingFaceIds = true)
        {
            ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            if (image != null) image.ThrowIfDisposed();

            int registeredFacesCount;
            lock (_registeredFacesLock)
            {
                registeredFacesCount = _registeredFaces.Count;
            }

            if (registeredFacesCount == 0)
            {
                // Debug.LogWarning("[UpdateFaceRecognitionForAllTrackedFaces] No registered faces available for recognition.");
                return;
            }

            // Debug.Log($"[UpdateFaceRecognitionForAllTrackedFaces] Updating recognition for {_trackedFaces.Count} tracked faces using {registeredFacesCount} registered faces. SkipExistingFaceIds: {skipExistingFaceIds}");

            lock (_trackedFacesLock)
            {
                foreach (var trackedFace in _trackedFaces)
                {
                    // Skip if already has a valid face ID and skipExistingFaceIds is true
                    if (skipExistingFaceIds && trackedFace.FaceId >= 0)
                    {
                        // Debug.Log($"[UpdateFaceRecognitionForAllTrackedFaces] Track {trackedFace.TrackId} already has FaceId {trackedFace.FaceId}, skipping.");
                        continue;
                    }

                    // Perform face recognition using the image and tracked face's bounding box
                    UpdateFaceRecognitionForTrackedFace(image, trackedFace);
                }
            }
        }

        /// <summary>
        /// Updates face recognition for a specific tracked face using current registered faces.
        /// </summary>
        /// <param name="image">The input image containing the face.</param>
        /// <param name="trackedFace">The tracked face to update recognition for.</param>
        private void UpdateFaceRecognitionForTrackedFace(Mat image, TrackedFace trackedFace)
        {
            ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            if (image != null) image.ThrowIfDisposed();
            if (trackedFace == null)
                throw new ArgumentNullException(nameof(trackedFace), "TrackedFace cannot be null.");

            int registeredFacesCount;
            lock (_registeredFacesLock)
            {
                registeredFacesCount = _registeredFaces.Count;
            }

            if (registeredFacesCount == 0)
            {
                // Debug.LogWarning($"[UpdateFaceRecognitionForTrackedFace] No registered faces available for track {trackedFace.TrackId}.");
                return;
            }

            // Align and crop the face from the image using the tracked face's bounding box
            Mat alignedFace = new Mat();
            Mat faceRowMat = ConvertFaceDetectionDataToMat(trackedFace.FaceDetection);
            _faceRecognizer.alignCrop(image, faceRowMat, alignedFace);
            faceRowMat.Dispose();

            if (!alignedFace.empty())
            {
                // Extract features from the face
                Mat detectedFeature = new Mat();
                _faceRecognizer.feature(alignedFace, detectedFeature);

                // Match against registered faces
                double bestScore = double.MaxValue;
                int bestMatchId = -1;

                List<RegisteredFace> registeredFacesCopy;
                lock (_registeredFacesLock)
                {
                    registeredFacesCopy = new List<RegisteredFace>(_registeredFaces);
                }

                foreach (var registeredFace in registeredFacesCopy)
                {
                    // Calculate cosine similarity
                    double cosineScore = _faceRecognizer.match(detectedFeature, registeredFace.Feature, FaceRecognizerSF.FR_COSINE);

                    // Calculate L2 norm distance
                    double l2Score = _faceRecognizer.match(detectedFeature, registeredFace.Feature, FaceRecognizerSF.FR_NORM_L2);

                    // Check if this face matches (both thresholds must be satisfied)
                    bool cosineMatch = cosineScore >= _cosineSimilarThreshold;
                    bool l2Match = l2Score <= _l2normSimilarThreshold;

                    if (cosineMatch && l2Match)
                    {
                        // Use L2 score as the primary metric (lower is better)
                        if (l2Score < bestScore)
                        {
                            bestScore = l2Score;
                            bestMatchId = registeredFace.FaceId;
                        }
                    }
                }

                if (bestMatchId >= 0)
                {
                    int previousFaceId = trackedFace.FaceId;
                    trackedFace.FaceId = bestMatchId;
                    // Debug.Log($"[UpdateFaceRecognitionForTrackedFace] Track {trackedFace.TrackId} face recognition updated: {previousFaceId} -> {bestMatchId}");
                }
                else
                {
                    // Debug.Log($"[UpdateFaceRecognitionForTrackedFace] Track {trackedFace.TrackId} no match found, keeping FaceId as {trackedFace.FaceId}");
                }

                alignedFace.Dispose();
                detectedFeature.Dispose();
            }
            else
            {
                // Debug.LogWarning($"[UpdateFaceRecognitionForTrackedFace] Failed to align face for track {trackedFace.TrackId}");
            }
        }

        /// <summary>
        /// Detects, tracks, and identifies faces in the input image using the complete pipeline.
        ///
        /// This method performs the full face identification workflow:
        /// 1. Detects faces using FaceDetectorYN
        /// 2. Updates BYTETracker for consistent tracking across frames
        /// 3. Performs face recognition for new tracks
        /// 4. Applies hybrid re-recognition logic for existing tracks
        /// 5. Returns results with persistent face IDs
        /// </summary>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="useCopyOutput">Whether to return a copy of the output (true) or a reference (false).</param>
        /// <returns>A Mat containing detection results with face IDs [N rows x 17 columns]. The caller is responsible for disposing this Mat.</returns>
        public virtual Mat Estimate(Mat image, bool useCopyOutput = false)
        {
            Execute(image);
            return useCopyOutput ? CopyOutput() : PeekOutput();
        }

        /// <summary>
        /// Detects, tracks, and identifies faces in the input image asynchronously.
        ///
        /// This method performs the same workflow as Estimate() but in an asynchronous manner.
        /// Always returns a copy of the output for thread safety.
        /// </summary>
        /// <param name="image">Input image in BGR format.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Mat with detection results and face IDs [N rows x 17 columns].</returns>
        public virtual async Task<Mat> EstimateAsync(Mat image, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(image, cancellationToken);
            return CopyOutput(); // Always create copy for async safety
        }

        /// <summary>
        /// Visualizes face detection, tracking, and identification results on the input image.
        ///
        /// This method draws:
        /// - Bounding boxes with consistent colors based on face IDs
        /// - 5 facial landmarks (eyes, nose, mouth corners)
        /// - Track ID, Face ID, name (if registered), and confidence score
        /// - Uses HSV-based color generation for consistent face ID colors
        /// </summary>
        /// <param name="image">The input image to draw on (modified in-place).</param>
        /// <param name="result">The result Mat returned by Estimate method [N rows x 17 columns].</param>
        /// <param name="printResult">Whether to print detailed results to console.</param>
        /// <param name="isRGB">Whether the image is in RGB format (vs BGR).</param>
        public override void Visualize(Mat image, Mat result, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (result == null || result.empty())
                return;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<FaceIdentificationData> data = ToStructuredDataAsSpan(result);
#else
            FaceIdentificationData[] data = ToStructuredData(result);
#endif

            for (int i = 0; i < data.Length; i++)
            {
                ref readonly var d = ref data[i];
                float x = d.X;
                float y = d.Y;
                float w = d.Width;
                float h = d.Height;
                float score = d.Score;
                int faceId = (int)d.FaceId;
                int trackId = (int)d.TrackId;

                // Draw bounding box with consistent color based on faceId (ValueTuple based)
                Scalar colorScalar = GetColorForFaceId(faceId);
                var c = (B: (double)colorScalar.val[0], G: (double)colorScalar.val[1], R: (double)colorScalar.val[2], A: (double)colorScalar.val[3]);
                var drawColor = isRGB ? (c.R, c.G, c.B, c.A) : (c.B, c.G, c.R, c.A);
                var topLeft = (X: (double)x, Y: (double)y);
                var bottomRight = (X: (double)(x + w), Y: (double)(y + h));
                Imgproc.rectangle(image, topLeft, bottomRight, drawColor, 2);

                // Draw landmarks
                var lc = (B: 255d, G: 255d, R: 0d, A: 255d);
                var landmarkColor = isRGB ? (lc.R, lc.G, lc.B, lc.A) : (lc.B, lc.G, lc.R, lc.A);
                var rightEye = (X: (double)d.RightEye.Item1, Y: (double)d.RightEye.Item2);
                var leftEye = (X: (double)d.LeftEye.Item1, Y: (double)d.LeftEye.Item2);
                var nose = (X: (double)d.Nose.Item1, Y: (double)d.Nose.Item2);
                var rightMouth = (X: (double)d.RightMouth.Item1, Y: (double)d.RightMouth.Item2);
                var leftMouth = (X: (double)d.LeftMouth.Item1, Y: (double)d.LeftMouth.Item2);
                Imgproc.circle(image, rightEye, 2, landmarkColor, 2);
                Imgproc.circle(image, leftEye, 2, landmarkColor, 2);
                Imgproc.circle(image, nose, 2, landmarkColor, 2);
                Imgproc.circle(image, rightMouth, 2, landmarkColor, 2);
                Imgproc.circle(image, leftMouth, 2, landmarkColor, 2);

                // Draw face ID, name, track ID and score with background inside the bbox (BYTETrack style)
                string label;
                label = $"TrackId:{trackId}";
                if (faceId >= 0)
                {
                    string faceName = GetFaceName(faceId);
                    label += $" FaceId:{faceId}";
                    if (!string.IsNullOrEmpty(faceName))
                    {
                        label += $" ({faceName})";
                    }
                }
                else
                {
                    label += " Unknown";
                }
                label += $", {score:F2}";

                int[] baseLine = new int[1];
                var labelSize = Imgproc.getTextSizeAsValueTuple(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                float leftF = x;
                float topF = y;
                topF = Mathf.Max(topF, (float)labelSize.height);

                Imgproc.rectangle(image, (leftF, topF - labelSize.height),
                    (leftF + labelSize.width, topF + baseLine[0]), drawColor, Core.FILLED);
                var textColor = (B: 255d, G: 255d, R: 255d, A: 255d);
                Imgproc.putText(image, label, (leftF, topF), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, textColor, 1, Imgproc.LINE_AA);
            }
            if (printResult)
            {
                StringBuilder sb = new StringBuilder(512);
                for (int i = 0; i < data.Length; ++i)
                {
                    ref readonly var dd = ref data[i];
                    sb.AppendFormat("-----------face {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.Append("TrackId: ").Append((int)dd.TrackId);
                    sb.AppendLine();
                    sb.Append("FaceId: ").Append((int)dd.FaceId);
                    sb.AppendLine();
                    string n = ((int)dd.FaceId) >= 0 ? GetFaceName((int)dd.FaceId) : null;
                    if (!string.IsNullOrEmpty(n))
                    {
                        sb.Append("Name: ").Append(n);
                        sb.AppendLine();
                    }
                    sb.AppendFormat("Score: {0:F4}", dd.Score);
                    sb.AppendLine();
                    sb.AppendFormat("Box: {0:F0} {1:F0} {2:F0} {3:F0}", dd.X, dd.Y, dd.Width, dd.Height);
                    sb.AppendLine();
                    sb.AppendFormat("Landmarks: RE({0:F1},{1:F1}) LE({2:F1},{3:F1}) N({4:F1},{5:F1}) RM({6:F1},{7:F1}) LM({8:F1},{9:F1})",
                        dd.RightEye.Item1, dd.RightEye.Item2,
                        dd.LeftEye.Item1, dd.LeftEye.Item2,
                        dd.Nose.Item1, dd.Nose.Item2,
                        dd.RightMouth.Item1, dd.RightMouth.Item2,
                        dd.LeftMouth.Item1, dd.LeftMouth.Item2);
                    sb.AppendLine();
                }
                Debug.Log(sb.ToString());
            }
        }

        /// <summary>
        /// Gets a consistent color for a given face ID using HSV color space generation.
        ///
        /// This method ensures that the same face ID always gets the same color across frames,
        /// making it easier to visually track faces. Uses golden angle approximation for good color distribution.
        /// Unknown faces (faceId < 0) are assigned red color.
        /// </summary>
        /// <param name="faceId">The face ID to get color for.</param>
        /// <returns>A Scalar representing the BGR color for this face ID.</returns>
        public Scalar GetColorForFaceId(int faceId)
        {
            if (faceId < 0)
            {
                // Unknown face - use red
                return new Scalar(0, 0, 255, 255);
            }

            // Generate consistent color based on faceId using HSV color space
            // Use faceId as hue value to ensure different colors for different IDs
            float hue = (faceId * 137.5f) % 360f; // Golden angle approximation for good distribution
            float saturation = 0.8f; // High saturation for vibrant colors
            float value = 0.9f; // High value for bright colors

            // Convert HSV to RGB
            float c = value * saturation;
            float x = c * (1f - Math.Abs((hue / 60f) % 2f - 1f));
            float m = value - c;

            float r, g, b;
            if (hue < 60f)
            {
                r = c; g = x; b = 0;
            }
            else if (hue < 120f)
            {
                r = x; g = c; b = 0;
            }
            else if (hue < 180f)
            {
                r = 0; g = c; b = x;
            }
            else if (hue < 240f)
            {
                r = 0; g = x; b = c;
            }
            else if (hue < 300f)
            {
                r = x; g = 0; b = c;
            }
            else
            {
                r = c; g = 0; b = x;
            }

            // Convert to 0-255 range and return as BGR (OpenCV format)
            return new Scalar(
                (int)((b + m) * 255), // Blue
                (int)((g + m) * 255), // Green
                (int)((r + m) * 255), // Red
                255 // Alpha
            );
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Core processing method that implements the complete face detection, tracking, and identification pipeline.
        ///
        /// This method orchestrates the entire workflow:
        /// 1. Detects faces using FaceDetectorYN
        /// 2. Converts detections to BBox format for BYTETracker
        /// 3. Updates BYTETracker and gets matching track IDs
        /// 4. Processes tracks (new, existing, lost, removed)
        /// 5. Performs face recognition and hybrid re-recognition
        /// 6. Returns results with persistent face IDs
        /// </summary>
        /// <param name="inputs">Input array containing the image Mat [BGR format, 3 channels].</param>
        /// <returns>Mat containing face detection results with face IDs [N rows x 17 columns].</returns>
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

            // Set input size for face detector
            _faceDetector.setInputSize(image.size());

            // Detect faces
            Mat faces = new Mat();
            _faceDetector.detect(image, faces);

            if (faces.empty())
            {
                // No faces detected, update tracker with empty detections
                _byteTracker.Update(Array.Empty<BBox>());

                // Remove all tracked faces since no detections
                lock (_trackedFacesLock)
                {
                    foreach (var trackedFace in _trackedFaces)
                    {
                        trackedFace.Dispose();
                    }
                    _trackedFaces.Clear();
                }

                return new Mat[] { new Mat() }; // Return empty mat
            }

            // Convert face detections to BBox array for BYTETracker and prepare for ProcessTracks
            int numFaces = faces.rows();
            BBox[] detections = new BBox[numFaces];

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            // Use Span for efficient processing
            Span<FaceDetection5LandmarkData> faceDetections = faces.AsSpan<FaceDetection5LandmarkData>();
            for (int i = 0; i < numFaces; i++)
            {
                ref readonly var faceDetection = ref faceDetections[i];
                // Convert to BBox format (x, y, width, height, score, classId)
                detections[i] = new BBox(faceDetection.X, faceDetection.Y, faceDetection.Width, faceDetection.Height, faceDetection.Score, 0);
            }
#else
            // Use array for compatibility
            FaceDetection5LandmarkData[] faceDetections = new FaceDetection5LandmarkData[numFaces];
            OpenCVMatUtils.CopyFromMat(faces, faceDetections);
            for (int i = 0; i < numFaces; i++)
            {
                var faceDetection = faceDetections[i];
                // Convert to BBox format (x, y, width, height, score, classId)
                detections[i] = new BBox(faceDetection.X, faceDetection.Y, faceDetection.Width, faceDetection.Height, faceDetection.Score, 0);
            }
#endif

            // Update BYTETracker
            _byteTracker.Update(detections);

            // Get matching results
            int[] matchingTrackIds = _byteTracker.GetLastMatchingTrackIds();

            // Process new tracks and update existing ones
            ProcessTracks(image, faceDetections, matchingTrackIds);

            faces.Dispose();

            // Create result from tracked faces
            return CreateResultFromTrackedFaces();
        }

        /// <summary>
        /// Processes tracks: handles new tracks, updates existing ones, and manages track lifecycle.
        ///
        /// This method manages the complete track lifecycle:
        /// - Removes tracks that are actually removed (not just lost)
        /// - Updates existing tracks with new detection data and applies hybrid re-recognition
        /// - Creates new tracks for unmatched detections and performs initial recognition
        /// - Updates TrackInfo for lost tracks (still exist but not detected in current frame)
        /// </summary>
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        private void ProcessTracks(Mat image, Span<FaceDetection5LandmarkData> faceDetections, int[] matchingTrackIds)
#else
        private void ProcessTracks(Mat image, FaceDetection5LandmarkData[] faceDetections, int[] matchingTrackIds)
#endif
        {
            // Get all track information (active + lost)
            BYTETrackInfo[] activeTracks = _byteTracker.GetActiveTrackInfos();
            BYTETrackInfo[] lostTracks = _byteTracker.GetLostTrackInfos();
            BYTETrackInfo[] removedTracks = _byteTracker.GetRemovedTrackInfos();

            // Display matchingTrackIds
            //Debug.Log($"[ProcessTracks] Matching track IDs: {string.Join(", ", matchingTrackIds)}");
            //Debug.Log($"[ProcessTracks] aaa Active tracks: {activeTracks.Length}, Lost tracks: {lostTracks.Length}, Removed tracks: {removedTracks.Length}");

            // Display activeTracks
            //Debug.Log($"[ProcessTracks] aaa Active tracks: {string.Join(", ", activeTracks.Select(t => t.TrackId))}");
            //Debug.Log($"[ProcessTracks] aaa Lost tracks: {string.Join(", ", lostTracks.Select(t => t.TrackId))}");
            //Debug.Log($"[ProcessTracks] aaa Removed tracks: {string.Join(", ", removedTracks.Select(t => t.TrackId))}");

            // Remove tracks that are actually removed (not just lost)
            lock (_trackedFacesLock)
            {
                foreach (var removedTrack in removedTracks)
                {
                    var trackedFace = _trackedFaces.FirstOrDefault(tf => tf.TrackId == removedTrack.TrackId);
                    if (trackedFace != null)
                    {
                        // Debug.Log($"[ProcessTracks] Removing track: {removedTrack.TrackId}, State={removedTrack.State}, FaceId={trackedFace.FaceId}");
                        trackedFace.Dispose();
                        _trackedFaces.Remove(trackedFace);
                    }
                }
            }

            // Process each detection
            for (int i = 0; i < matchingTrackIds.Length; i++)
            {
                int trackId = matchingTrackIds[i];
                if (trackId < 0) continue; // No match or low score

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                ref readonly var faceDetection = ref faceDetections[i];
#else
                var faceDetection = faceDetections[i];
#endif

                TrackedFace existingTrackedFace;
                lock (_trackedFacesLock)
                {
                    existingTrackedFace = _trackedFaces.FirstOrDefault(tf => tf.TrackId == trackId);
                }
                if (existingTrackedFace != null)
                {
                    // Update existing track
                    existingTrackedFace.FrameCount++;
                    existingTrackedFace.IsNewTrack = false;

                    // Update TrackInfo from active tracks
                    foreach (var trackInfo in activeTracks)
                    {
                        if (trackInfo.TrackId == trackId)
                        {
                            existingTrackedFace.TrackInfo = trackInfo;
                            break;
                        }
                    }

                    // Update FaceDetection with new detection data
                    existingTrackedFace.FaceDetection = faceDetection;

                    // Hybrid re-recognition logic for existing tracks
                    PerformHybridReRecognition(image, existingTrackedFace);
                }
                else
                {
                    // New track - perform face recognition
                    // Debug.Log("[New Track] trackId: " + trackId);
                    BYTETrackInfo? trackInfo = null;
                    foreach (var track in activeTracks)
                    {
                        if (track.TrackId == trackId)
                        {
                            trackInfo = track;
                            break;
                        }
                    }

                    if (trackInfo == null)
                    {
                        // Debug.LogWarning($"[New Track] TrackId {trackId} not found in activeTracks, skipping");
                        continue;
                    }

                    // Debug.Log("[New Track] trackInfo: " + trackInfo.Value.TrackId);
                    var newTrackedFace = new TrackedFace(trackInfo.Value, faceDetection, true);

                    // Debug.Log($"[New Track] TrackId={newTrackedFace.TrackId}, State={newTrackedFace.State}");

                    // Initialize recognition score for new track
                    newTrackedFace.LastRecognitionScore = newTrackedFace.FaceDetection.Score;

                    // Perform face recognition for new track
                    UpdateFaceRecognitionForTrackedFace(image, newTrackedFace);

                    lock (_trackedFacesLock)
                    {
                        _trackedFaces.Add(newTrackedFace);
                    }
                }
            }

            // Update TrackInfo for lost tracks (they still exist but are not detected in current frame)
            lock (_trackedFacesLock)
            {
                foreach (var lostTrackInfo in lostTracks)
                {
                    var trackedFace = _trackedFaces.FirstOrDefault(tf => tf.TrackId == lostTrackInfo.TrackId);
                    if (trackedFace != null)
                    {
                        // Update TrackInfo with lost track information
                        trackedFace.TrackInfo = lostTrackInfo;
                    }
                }
            }
        }

        /// <summary>
        /// Performs hybrid re-recognition for existing tracks using dual-condition logic.
        ///
        /// This method implements an intelligent re-recognition strategy:
        /// - Condition 1: Periodic re-recognition based on configurable frame intervals
        /// - Condition 2: Confidence improvement - re-recognize when detection score increases significantly
        /// - Adaptive intervals: increases interval for frequently failing tracks to reduce computational cost
        /// - Only attempts re-recognition for unknown faces (FaceId < 0)
        /// </summary>
        /// <param name="image">The input image containing the face.</param>
        /// <param name="trackedFace">The tracked face to potentially re-recognize.</param>
        private void PerformHybridReRecognition(Mat image, TrackedFace trackedFace)
        {
            if (_registeredFaces.Count == 0)
                return;

            // Only attempt re-recognition for unknown faces
            if (trackedFace.FaceId >= 0)
                return;

            float currentScore = trackedFace.FaceDetection.Score;

            bool shouldReRecognize = false;

            // Condition 1: Periodic re-recognition based on frame interval
            if (trackedFace.FrameCount - trackedFace.LastRecognitionFrame >= trackedFace.RecognitionInterval)
            {
                shouldReRecognize = true;
                // Debug.Log($"[HybridReRecognition] Track {trackedFace.TrackId} periodic re-recognition (interval: {trackedFace.RecognitionInterval})");
            }

            // Condition 2: Confidence improvement - significant score increase
            if (currentScore > trackedFace.LastRecognitionScore + 0.1f)
            {
                shouldReRecognize = true;
                // Debug.Log($"[HybridReRecognition] Track {trackedFace.TrackId} confidence improvement re-recognition (score: {trackedFace.LastRecognitionScore:F2} -> {currentScore:F2})");
            }

            if (shouldReRecognize)
            {
                // Perform face recognition
                UpdateFaceRecognitionForTrackedFace(image, trackedFace);

                // Update tracking variables
                trackedFace.LastRecognitionFrame = trackedFace.FrameCount;
                trackedFace.LastRecognitionScore = currentScore;

                // Adjust recognition interval based on failure count
                if (trackedFace.FaceId < 0)
                {
                    trackedFace.FailedRecognitionCount++;
                    // Increase interval for frequently failing tracks to reduce cost
                    if (trackedFace.FailedRecognitionCount > 3)
                    {
                        trackedFace.RecognitionInterval = Math.Min(60, trackedFace.RecognitionInterval + 10);
                        // Debug.Log($"[HybridReRecognition] Track {trackedFace.TrackId} increased recognition interval to {trackedFace.RecognitionInterval} due to repeated failures");
                    }
                }
                else
                {
                    // Reset failure count on successful recognition
                    trackedFace.FailedRecognitionCount = 0;
                    trackedFace.RecognitionInterval = 30; // Reset to default interval
                    // Debug.Log($"[HybridReRecognition] Track {trackedFace.TrackId} successful recognition, reset interval to default");
                }
            }
        }

        /// <summary>
        /// Creates result Mat from tracked faces, filtering only Tracked state faces.
        ///
        /// This method:
        /// - Filters tracked faces to include only those with Tracked state
        /// - Creates FaceIdentificationData array with detection info and face IDs
        /// - Uses pre-allocated output buffer for memory efficiency
        /// - Returns submat containing only the actual results
        /// </summary>
        private Mat[] CreateResultFromTrackedFaces()
        {
            // Filter out only Tracked state faces
            List<TrackedFace> trackedOnlyFaces;
            lock (_trackedFacesLock)
            {
                trackedOnlyFaces = _trackedFaces.Where(tf => tf.State == TrackState.Tracked).ToList();
            }
            int numTrackedFaces = trackedOnlyFaces.Count;

            // Debug: Display TrackState for all tracked faces
            //Debug.Log($"[CreateResultFromTrackedFaces] Total tracked faces: {_trackedFaces.Count}, Tracked state only: {numTrackedFaces}");
            //for (int i = 0; i < _trackedFaces.Count; i++)
            //{
            //    var trackedFace = _trackedFaces[i];
            //    Debug.Log($"[CreateResultFromTrackedFaces] TrackedFace[{i}]: TrackId={trackedFace.TrackId}, State={trackedFace.State}, FaceId={trackedFace.FaceId}");
            //}

            if (numTrackedFaces == 0)
            {
                return new Mat[] { new Mat() };
            }

            // Ensure output buffer is large enough
            if (numTrackedFaces > _outputBuffer.rows())
            {
                _outputBuffer.Dispose();
                _outputBuffer = new Mat(numTrackedFaces * 2, 17, CvType.CV_32FC1);
            }

            // Create FaceIdentificationData array
            FaceIdentificationData[] faceIdentificationData = new FaceIdentificationData[numTrackedFaces];

            for (int i = 0; i < numTrackedFaces; i++)
            {
                var trackedFace = trackedOnlyFaces[i];

                // Create FaceIdentificationData using trackedFace data directly
                faceIdentificationData[i] = new FaceIdentificationData(trackedFace.FaceDetection, trackedFace.FaceId, (float)trackedFace.TrackId);
            }

            // Copy all data to output buffer at once
            OpenCVMatUtils.CopyToMat(faceIdentificationData, _outputBuffer);

            // Return submat of the actual results
            Mat result = _outputBuffer.rowRange(0, numTrackedFaces);
            return new Mat[] { result };
        }

        protected override Task<Mat[]> RunCoreProcessingAsync(Mat[] inputs, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RunCoreProcessing(inputs));
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _faceDetector?.Dispose();
                _faceDetector = null;
                _faceRecognizer?.Dispose();
                _faceRecognizer = null;
                _byteTracker?.Dispose();
                _byteTracker = null;

                if (_trackedFaces != null)
                {
                    lock (_trackedFacesLock)
                    {
                        foreach (var trackedFace in _trackedFaces)
                        {
                            trackedFace.Dispose();
                        }
                        _trackedFaces.Clear();
                    }
                    _trackedFaces = null;
                }

                if (_registeredFaces != null)
                {
                    lock (_registeredFacesLock)
                    {
                        foreach (var face in _registeredFaces)
                        {
                            face.Dispose();
                        }
                        _registeredFaces.Clear();
                    }
                    _registeredFaces = null;
                }

                _outputBuffer?.Dispose();
                _outputBuffer = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Converts the detection result matrix to an array of FaceIdentificationData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Estimate method.</param>
        /// <returns>Array of FaceIdentificationData structures containing face detection and identification information.</returns>
        public virtual FaceIdentificationData[] ToStructuredData(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return new FaceIdentificationData[0];
            if (result.cols() < 17)
                throw new ArgumentException("Invalid result matrix. It must have at least 17 columns.");

            var dst = new FaceIdentificationData[result.rows()];
            OpenCVMatUtils.CopyFromMat(result, dst);

            return dst;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Converts the detection result matrix to a span of FaceIdentificationData structures.
        /// </summary>
        /// <param name="result">Detection result matrix from Estimate method.</param>
        /// <returns>Span of FaceIdentificationData structures containing face detection and identification information.</returns>
        public virtual Span<FaceIdentificationData> ToStructuredDataAsSpan(Mat result)
        {
            ThrowIfDisposed();

            if (result != null) result.ThrowIfDisposed();
            if (result.empty())
                return Span<FaceIdentificationData>.Empty;
            if (result.cols() < 17)
                throw new ArgumentException("Invalid result matrix. It must have at least 17 columns.");
            if (!result.isContinuous())
                throw new ArgumentException("result is not continuous.");

            return result.AsSpan<FaceIdentificationData>();
        }
#endif

        #endregion

        #region Public Classes

        /// <summary>
        /// Represents face detection and identification data with 5 facial landmarks and tracking information.
        ///
        /// This structure extends FaceDetection5LandmarkData with face identification ID for the complete pipeline.
        /// Contains all necessary information for face detection, tracking, and recognition results.
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct FaceIdentificationData
        {
            // Base face detection data (15 elements)
            public readonly FaceDetection5LandmarkData FaceDetection;

            // Face identification ID (-1.0f = unknown)
            public readonly float FaceId;

            // BYTETracker track ID
            public readonly float TrackId;

            public const int ELEMENT_COUNT = FaceDetection5LandmarkData.ELEMENT_COUNT + 2; // 15 + 1 + 1 = 17
            public const int DATA_SIZE = ELEMENT_COUNT * 4;

            public FaceIdentificationData(FaceDetection5LandmarkData faceDetection, float faceId, float trackId)
            {
                FaceDetection = faceDetection;
                FaceId = faceId;
                TrackId = trackId;
            }

            public FaceIdentificationData(float x, float y, float width, float height, Vec2f rightEye, Vec2f leftEye, Vec2f nose, Vec2f rightMouth, Vec2f leftMouth, float score, float faceId, float trackId)
            {
                FaceDetection = new FaceDetection5LandmarkData(x, y, width, height, rightEye, leftEye, nose, rightMouth, leftMouth, score);
                FaceId = faceId;
                TrackId = trackId;
            }

            // Convenience properties for backward compatibility
            public readonly float X => FaceDetection.X;
            public readonly float Y => FaceDetection.Y;
            public readonly float Width => FaceDetection.Width;
            public readonly float Height => FaceDetection.Height;
            public readonly Vec2f RightEye => FaceDetection.RightEye;
            public readonly Vec2f LeftEye => FaceDetection.LeftEye;
            public readonly Vec2f Nose => FaceDetection.Nose;
            public readonly Vec2f RightMouth => FaceDetection.RightMouth;
            public readonly Vec2f LeftMouth => FaceDetection.LeftMouth;
            public readonly float Score => FaceDetection.Score;

            public readonly override string ToString()
            {
                return $"FaceIdentificationData({FaceDetection.ToString()} FaceId:{FaceId} TrackId:{TrackId})";
            }
        }

        /// <summary>
        /// Converts Mat faceRow to FaceDetection5LandmarkData for memory efficiency.
        /// </summary>
        /// <param name="faceRow">Mat containing face detection data (15 columns).</param>
        /// <returns>FaceDetection5LandmarkData structure.</returns>
        public static FaceDetection5LandmarkData ConvertMatToFaceDetectionData(Mat faceRow)
        {
            if (faceRow == null || faceRow.empty())
                throw new ArgumentException("FaceRow cannot be null or empty.");
            if (faceRow.cols() < 15)
                throw new ArgumentException("Invalid faceRow matrix. It must have at least 15 columns.");

            var dst = new FaceDetection5LandmarkData[1];
            OpenCVMatUtils.CopyFromMat(faceRow, dst);

            return dst[0];
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Converts Mat faceRow to FaceDetection5LandmarkData as Span for memory efficiency.
        /// </summary>
        /// <param name="faceRow">Mat containing face detection data (15 columns).</param>
        /// <returns>Span containing FaceDetection5LandmarkData structure.</returns>
        public static Span<FaceDetection5LandmarkData> ConvertMatToFaceDetectionDataAsSpan(Mat faceRow)
        {
            if (faceRow == null || faceRow.empty())
                throw new ArgumentException("FaceRow cannot be null or empty.");
            if (faceRow.cols() < 15)
                throw new ArgumentException("Invalid faceRow matrix. It must have at least 15 columns.");
            if (!faceRow.isContinuous())
                throw new ArgumentException("faceRow is not continuous.");

            return faceRow.AsSpan<FaceDetection5LandmarkData>();
        }
#endif

        /// <summary>
        /// Converts FaceDetection5LandmarkData to Mat for compatibility with face recognizer.
        /// </summary>
        /// <param name="faceDetection">FaceDetection5LandmarkData structure.</param>
        /// <returns>Mat containing face detection data (15 columns).</returns>
        public static Mat ConvertFaceDetectionDataToMat(FaceDetection5LandmarkData faceDetection)
        {
            Mat faceRow = new Mat(1, 15, CvType.CV_32FC1);
            var src = new FaceDetection5LandmarkData[] { faceDetection };
            OpenCVMatUtils.CopyToMat(src, faceRow);
            return faceRow;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Converts Span of FaceDetection5LandmarkData to Mat for compatibility with face recognizer.
        /// </summary>
        /// <param name="faceDetections">Span containing FaceDetection5LandmarkData structures.</param>
        /// <returns>Mat containing face detection data (N rows x 15 columns).</returns>
        public static Mat ConvertFaceDetectionDataToMatAsSpan(Span<FaceDetection5LandmarkData> faceDetections)
        {
            if (faceDetections.IsEmpty)
                return null;

            Mat faceRows = new Mat(faceDetections.Length, 15, CvType.CV_32FC1);
            ReadOnlySpan<FaceDetection5LandmarkData> readonlySpan = faceDetections;
            OpenCVMatUtils.CopyToMat(readonlySpan, faceRows);
            return faceRows;
        }
#endif

        #endregion

        #region Private Classes

        /// <summary>
        /// Represents a tracked face with BYTETracker integration.
        /// </summary>
        private class TrackedFace
        {
            public int FaceId { get; set; }            // FaceId of RegisteredFace (-1 = unknown)
            public BYTETrackInfo TrackInfo { get; set; } // BYTETracker information
            public FaceDetection5LandmarkData FaceDetection { get; set; } // Face detection data (15 elements)
            public bool IsNewTrack { get; set; }       // Whether this is a newly created track
            public int FrameCount { get; set; }        // Number of frames this track has been active

            // Re-recognition properties for hybrid approach
            public int LastRecognitionFrame { get; set; } = 0;     // Last frame when recognition was attempted
            public int RecognitionInterval { get; set; } = 30;      // Frames between recognition attempts
            public float LastRecognitionScore { get; set; } = 0f;   // Score when last recognition was attempted
            public int FailedRecognitionCount { get; set; } = 0;    // Number of consecutive failed recognitions

            public TrackedFace(BYTETrackInfo trackInfo, Mat faceRow, bool isNewTrack = false)
            {
                FaceId = -1; // Unknown by default
                TrackInfo = trackInfo;
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                var faceDetectionSpan = ConvertMatToFaceDetectionDataAsSpan(faceRow);
                FaceDetection = faceDetectionSpan[0];
#else
                FaceDetection = ConvertMatToFaceDetectionData(faceRow);
#endif
                IsNewTrack = isNewTrack;
                FrameCount = 0;

                // Initialize re-recognition properties
                LastRecognitionFrame = 0;
                RecognitionInterval = 30;
                LastRecognitionScore = 0f;
                FailedRecognitionCount = 0;
            }

            public TrackedFace(BYTETrackInfo trackInfo, FaceDetection5LandmarkData faceDetection, bool isNewTrack = false)
            {
                FaceId = -1; // Unknown by default
                TrackInfo = trackInfo;
                FaceDetection = faceDetection;
                IsNewTrack = isNewTrack;
                FrameCount = 0;

                // Initialize re-recognition properties
                LastRecognitionFrame = 0;
                RecognitionInterval = 30;
                LastRecognitionScore = faceDetection.Score;
                FailedRecognitionCount = 0;
            }

            // Convenience properties for backward compatibility
            public int TrackId => TrackInfo.TrackId;
            public BBox CurrentBBox => TrackInfo.BBox;
            public TrackState State => TrackInfo.State;
            public int TrackletLength => TrackInfo.TrackletLength;

            public void Dispose()
            {
                // No need to dispose FaceDetection5LandmarkData as it's a value type
            }
        }

        /// <summary>
        /// Represents a registered face with its features and metadata.
        /// </summary>
        private class RegisteredFace
        {
            public int FaceId { get; set; }        // Numeric face ID
            public string Name { get; set; }       // Human-readable name
            public Mat AlignedFace { get; set; }   // The aligned face image
            public Mat Feature { get; set; }       // Pre-computed face feature vector
            public float DetectionConfidence { get; set; }  // Confidence score from face detection

            public RegisteredFace(int faceId, string name, Mat alignedFace, Mat feature, float detectionConfidence = 0f)
            {
                FaceId = faceId;
                Name = name;
                AlignedFace = alignedFace;
                Feature = feature;
                DetectionConfidence = detectionConfidence;
            }

            public void Dispose()
            {
                AlignedFace?.Dispose();
                AlignedFace = null;
                Feature?.Dispose();
                Feature = null;
            }
        }

        #endregion
    }
}

#endif
