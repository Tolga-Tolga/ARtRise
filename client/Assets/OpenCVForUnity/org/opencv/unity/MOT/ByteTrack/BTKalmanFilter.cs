using System;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.VideoModule;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    internal class BTKalmanFilter : IDisposable
    {
        private KalmanFilter _kf;
        private float _stdWeightPosition;
        private float _stdWeightVelocity;
        private Mat _measurementMat;
        private Mat _statePre;
        private Mat _statePost;
        private Mat _errorCovPre;
        private Mat _errorCovPost;
        private Mat _processNoiseCov;
        private Mat _measurementNoiseCov;
        private bool _resetRequested = false;
        private bool _disposed = false;

        public BTKalmanFilter(float stdWeightPosition = 1f / 20, float stdWeightVelocity = 1f / 160)
        {
            _stdWeightPosition = stdWeightPosition;
            _stdWeightVelocity = stdWeightVelocity;

            // 8-dimensional state vector (position, aspect ratio, height, and their velocities)
            // 4-dimensional measurement vector (position, aspect ratio, height)
            _kf = new KalmanFilter(8, 4, 0, CvType.CV_32F);

            // Get references to the Kalman filter's properties
            _statePre = _kf.get_statePre();
            // To make the result same as the original code, set the reference of statePost to statePre
            _kf.set_statePost(_statePre);
            _statePost = _statePre;
            _errorCovPre = _kf.get_errorCovPre();
            _errorCovPost = _kf.get_errorCovPost();
            _measurementNoiseCov = _kf.get_measurementNoiseCov();
            _processNoiseCov = _kf.get_processNoiseCov();

            // Set transition matrix
            using (Mat transitionMatrix = _kf.get_transitionMatrix())
            {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                Span<float> allTransitionMatrix = transitionMatrix.AsSpan<float>();
#else
                var allTransitionMatrix = new float[transitionMatrix.total() * transitionMatrix.channels()];
                transitionMatrix.get(0, 0, allTransitionMatrix);
#endif

                allTransitionMatrix[0] = 1f; allTransitionMatrix[4] = 1f;
                allTransitionMatrix[1 * 8 + 1] = 1f; allTransitionMatrix[1 * 8 + 5] = 1f;
                allTransitionMatrix[2 * 8 + 2] = 1f; allTransitionMatrix[2 * 8 + 6] = 1f;
                allTransitionMatrix[3 * 8 + 3] = 1f; allTransitionMatrix[3 * 8 + 7] = 1f;
                allTransitionMatrix[4 * 8 + 4] = 1f;
                allTransitionMatrix[5 * 8 + 5] = 1f;
                allTransitionMatrix[6 * 8 + 6] = 1f;
                allTransitionMatrix[7 * 8 + 7] = 1f;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                transitionMatrix.put(0, 0, allTransitionMatrix);
#endif

                // 1, 0, 0, 0, 1, 0, 0, 0,
                // 0, 1, 0, 0, 0, 1, 0, 0,
                // 0, 0, 1, 0, 0, 0, 1, 0,
                // 0, 0, 0, 1, 0, 0, 0, 1,
                // 0, 0, 0, 0, 1, 0, 0, 0,
                // 0, 0, 0, 0, 0, 1, 0, 0,
                // 0, 0, 0, 0, 0, 0, 1, 0,
                // 0, 0, 0, 0, 0, 0, 0, 1
            }

            // Set measurement matrix
            using (Mat measurementMatrix = _kf.get_measurementMatrix())
            {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                Span<float> allMeasurementMatrix = measurementMatrix.AsSpan<float>();
#else
                var allMeasurementMatrix = new float[measurementMatrix.total() * measurementMatrix.channels()];
                measurementMatrix.get(0, 0, allMeasurementMatrix);
#endif

                allMeasurementMatrix[0] = 1f;
                allMeasurementMatrix[1 * 8 + 1] = 1f;
                allMeasurementMatrix[2 * 8 + 2] = 1f;
                allMeasurementMatrix[3 * 8 + 3] = 1f;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
                measurementMatrix.put(0, 0, allMeasurementMatrix);
#endif

                // 1, 0, 0, 0, 0, 0, 0, 0,
                // 0, 1, 0, 0, 0, 0, 0, 0,
                // 0, 0, 1, 0, 0, 0, 0, 0,
                // 0, 0, 0, 1, 0, 0, 0, 0
            }

            _measurementMat = new Mat(4, 1, CvType.CV_32FC1);
        }

        public BTRect Initiate(in BTRect measurement)
        {
            ThrowIfDisposed();

            if (_resetRequested)
                PerformReset();

            float x = measurement.X;
            float y = measurement.Y;
            float width = measurement.Width;
            float height = measurement.Height;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allStatePre = _statePre.AsSpan<float>();
#else
            var allStatePre = new float[8];
            _statePre.get(0, 0, allStatePre);
#endif

            allStatePre[0] = x + width / 2;
            allStatePre[1] = y + height / 2;
            allStatePre[2] = width / height;
            allStatePre[3] = height;
            allStatePre[4] = 0;
            allStatePre[5] = 0;
            allStatePre[6] = 0;
            allStatePre[7] = 0;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _statePre.put(0, 0, allStatePre);
#endif

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allErrorCovPre = _errorCovPre.AsSpan<float>();
#else
            var allErrorCovPre = new float[_errorCovPre.total() * _errorCovPre.channels()];
            _errorCovPre.get(0, 0, allErrorCovPre);
#endif

            float heightSquared = 2 * _stdWeightPosition * height;
            heightSquared *= heightSquared;

            allErrorCovPre[0] = heightSquared;
            allErrorCovPre[1 * 8 + 1] = heightSquared;
            allErrorCovPre[2 * 8 + 2] = 1e-2f * 1e-2f;
            allErrorCovPre[3 * 8 + 3] = heightSquared;

            float velocitySquared = 10 * _stdWeightVelocity * height;
            velocitySquared *= velocitySquared;

            allErrorCovPre[4 * 8 + 4] = velocitySquared;
            allErrorCovPre[5 * 8 + 5] = velocitySquared;
            allErrorCovPre[6 * 8 + 6] = 1e-5f * 1e-5f;
            allErrorCovPre[7 * 8 + 7] = velocitySquared;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _errorCovPre.put(0, 0, allErrorCovPre);
#endif

            return XyAhToRect(_statePost);
        }

        public BTRect Predict(bool meanEightToZero = false)
        {
            ThrowIfDisposed();

            if (_resetRequested)
                PerformReset();

            if (meanEightToZero)
            {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                _statePre.at<float>(7, 0)[0] = 0f;
#else
                _statePre.put(7, 0, 0f);
#endif

            }

            // Update process noise covariance
            float statePre_3_0 = 0f;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            statePre_3_0 = _statePre.at<float>(3, 0)[0];
#else
            statePre_3_0 = (float)_statePre.get(3, 0)[0];
#endif

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allProcessNoiseCov = _processNoiseCov.AsSpan<float>();
#else
            var allProcessNoiseCov = new float[_processNoiseCov.total() * _processNoiseCov.channels()];
            _processNoiseCov.get(0, 0, allProcessNoiseCov);
#endif

            float positionSquared = _stdWeightPosition * statePre_3_0;
            positionSquared *= positionSquared;
            float velocitySquared = _stdWeightVelocity * statePre_3_0;
            velocitySquared *= velocitySquared;

            allProcessNoiseCov[0] = positionSquared;
            allProcessNoiseCov[1 * 8 + 1] = positionSquared;
            allProcessNoiseCov[2 * 8 + 2] = 1e-2f * 1e-2f;
            allProcessNoiseCov[3 * 8 + 3] = positionSquared;
            allProcessNoiseCov[4 * 8 + 4] = velocitySquared;
            allProcessNoiseCov[5 * 8 + 5] = velocitySquared;
            allProcessNoiseCov[6 * 8 + 6] = 1e-5f * 1e-5f;
            allProcessNoiseCov[7 * 8 + 7] = velocitySquared;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _processNoiseCov.put(0, 0, allProcessNoiseCov);
#endif

            // Predict
            using (var prediction = _kf.predict())
            {
                return XyAhToRect(prediction);
            }
        }

        public BTRect Update(in BTRect measurement)
        {
            ThrowIfDisposed();

            if (_resetRequested)
                PerformReset();

            // Update measurement noise covariance
            float statePre_3_0 = 0f;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            statePre_3_0 = _statePre.at<float>(3, 0)[0];
#else
            statePre_3_0 = (float)_statePre.get(3, 0)[0];
#endif

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allMeasurementNoiseCov = _measurementNoiseCov.AsSpan<float>();
#else
            var allMeasurementNoiseCov = new float[_measurementNoiseCov.total() * _measurementNoiseCov.channels()];
            _measurementNoiseCov.get(0, 0, allMeasurementNoiseCov);
#endif

            float positionSquared = _stdWeightPosition * statePre_3_0;
            positionSquared *= positionSquared;

            allMeasurementNoiseCov[0] = positionSquared;
            allMeasurementNoiseCov[1 * 4 + 1] = positionSquared;
            allMeasurementNoiseCov[2 * 4 + 2] = 1e-1f * 1e-1f;
            allMeasurementNoiseCov[3 * 4 + 3] = positionSquared;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _measurementNoiseCov.put(0, 0, allMeasurementNoiseCov);
#endif

            RectToXyAh(measurement, _measurementMat);

            // Update
            using (var corrected = _kf.correct(_measurementMat))
            {
                return XyAhToRect(corrected);
            }
        }

        public void Reset()
        {
            ThrowIfDisposed();

            _resetRequested = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _kf?.Dispose(); _kf = null;
                _measurementMat?.Dispose(); _measurementMat = null;
            }

            _disposed = true;
        }

        ~BTKalmanFilter()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private Mat RectToXyAh(in BTRect rect, Mat dst)
        {
            var xyah = dst;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allXyah = xyah.AsSpan<float>();
#else
            var allXyah = new float[4];
#endif

            allXyah[0] = rect.X + rect.Width / 2;
            allXyah[1] = rect.Y + rect.Height / 2;
            allXyah[2] = rect.Width / rect.Height;
            allXyah[3] = rect.Height;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            xyah.put(0, 0, allXyah);
#endif

            return xyah;
        }

        private BTRect XyAhToRect(Mat xyah)
        {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allXyah = xyah.AsSpan<float>();
#else
            var allXyah = new float[4];
            xyah.get(0, 0, allXyah);
#endif

            var xyahWidth = allXyah[2] * allXyah[3];
            return new BTRect(
                allXyah[0] - xyahWidth / 2,
                allXyah[1] - allXyah[3] / 2,
                xyahWidth,
                allXyah[3]
            );
        }

        private void PerformReset()
        {
            // Set error covariance to zero
            _errorCovPre.setTo((0, 0, 0, 0));
            _errorCovPost.setTo((0, 0, 0, 0));

            // Set process noise covariance to 1
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allProcessNoiseCov = _processNoiseCov.AsSpan<float>();
#else
            var allProcessNoiseCov = new float[_processNoiseCov.total() * _processNoiseCov.channels()];
            _processNoiseCov.get(0, 0, allProcessNoiseCov);
#endif

            allProcessNoiseCov[0] = 1f;
            allProcessNoiseCov[1 * 8 + 1] = 1f;
            allProcessNoiseCov[2 * 8 + 2] = 1f;
            allProcessNoiseCov[3 * 8 + 3] = 1f;
            allProcessNoiseCov[4 * 8 + 4] = 1f;
            allProcessNoiseCov[5 * 8 + 5] = 1f;
            allProcessNoiseCov[6 * 8 + 6] = 1f;
            allProcessNoiseCov[7 * 8 + 7] = 1f;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _processNoiseCov.put(0, 0, allProcessNoiseCov);
#endif

            // 1, 0, 0, 0, 0, 0, 0, 0,
            // 0, 1, 0, 0, 0, 0, 0, 0,
            // 0, 0, 1, 0, 0, 0, 0, 0,
            // 0, 0, 0, 1, 0, 0, 0, 0,
            // 0, 0, 0, 0, 1, 0, 0, 0,
            // 0, 0, 0, 0, 0, 1, 0, 0,
            // 0, 0, 0, 0, 0, 0, 1, 0,
            // 0, 0, 0, 0, 0, 0, 0, 1

            // Set measurement noise covariance to 1
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            Span<float> allMeasurementNoiseCov = _measurementNoiseCov.AsSpan<float>();
#else
            var allMeasurementNoiseCov = new float[_measurementNoiseCov.total() * _measurementNoiseCov.channels()];
            _measurementNoiseCov.get(0, 0, allMeasurementNoiseCov);
#endif

            allMeasurementNoiseCov[0] = 1f;
            allMeasurementNoiseCov[1 * 4 + 1] = 1f;
            allMeasurementNoiseCov[2 * 4 + 2] = 1f;
            allMeasurementNoiseCov[3 * 4 + 3] = 1f;

#if !NET_STANDARD_2_1 || OPENCV_DONT_USE_UNSAFE_CODE
            _measurementNoiseCov.put(0, 0, allMeasurementNoiseCov);
#endif

            // 1, 0, 0, 0,
            // 0, 1, 0, 0,
            // 0, 0, 1, 0,
            // 0, 0, 0, 1

            _resetRequested = false;
        }
    }
}
