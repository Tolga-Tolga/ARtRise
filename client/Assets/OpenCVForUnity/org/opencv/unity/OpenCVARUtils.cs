using System;
using System.Collections.Generic;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// OpenCV AR utilities.
    /// </summary>
    public class OpenCVARUtils
    {
        public struct PoseData
        {
            public Vector3 Pos;
            public Quaternion Rot;
        }

        /// <summary>
        /// Converts rvec value to rotation transform.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <returns>Rotation.</returns>
        public static Quaternion ConvertRvecToRot(double[] rvec)
        {
            if (rvec == null)
                throw new ArgumentNullException("rvec");
            if (rvec.Length < 3)
                throw new ArgumentException("rvec.Count < 3");

            Vector3 _rvec = new Vector3((float)rvec[0], (float)rvec[1], (float)rvec[2]);
            float theta = _rvec.magnitude;
            _rvec.Normalize();

            // http://stackoverflow.com/questions/12933284/rodrigues-into-eulerangles-and-vice-versa
            return Quaternion.AngleAxis(theta * Mathf.Rad2Deg, _rvec);
        }

        /// <summary>
        /// Converts rvec value to rotation transform.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <returns>Rotation.</returns>
        public static Quaternion ConvertRvecToRot(Mat rvec)
        {
            if (rvec != null) rvec.ThrowIfDisposed();
            if (!rvec.isContinuous())
                throw new ArgumentException("rvec is not continuous");
            if (rvec.total() * rvec.channels() < 3)
                throw new ArgumentException("rvec.total() * rvec.channels() < 3");
            if (rvec.type() != CvType.CV_64FC(rvec.channels()))
                throw new ArgumentException("rvec.type() != CvType.CV_64FC(rvec.channels())");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> rvecSpan = rvec.AsSpan<double>();
            Vector3 _rvec = new Vector3((float)rvecSpan[0], (float)rvecSpan[1], (float)rvecSpan[2]);
            float theta = _rvec.magnitude;
            _rvec.Normalize();

            // http://stackoverflow.com/questions/12933284/rodrigues-into-eulerangles-and-vice-versa
            return Quaternion.AngleAxis(theta * Mathf.Rad2Deg, _rvec);
#else
            double[] rvecValues = new double[3];
            rvec.get(0, 0, rvecValues);
            return ConvertRvecToRot(rvecValues);
#endif
        }

        /// <summary>
        /// Converts tvec value to position transform.
        /// </summary>
        /// <param name="tvec">Tvec.</param>
        /// <returns>Position.</returns>
        public static Vector3 ConvertTvecToPos(double[] tvec)
        {
            if (tvec == null)
                throw new ArgumentNullException("tvec");
            if (tvec.Length < 3)
                throw new ArgumentException("tvec.Count < 3");

            return new Vector3((float)tvec[0], (float)tvec[1], (float)tvec[2]);
        }

        /// <summary>
        /// Converts tvec value to position transform.
        /// </summary>
        /// <param name="tvec">Tvec.</param>
        /// <returns>Position.</returns>
        public static Vector3 ConvertTvecToPos(Mat tvec)
        {
            if (tvec != null) tvec.ThrowIfDisposed();
            if (!tvec.isContinuous())
                throw new ArgumentException("tvec is not continuous");
            if (tvec.total() * tvec.channels() < 3)
                throw new ArgumentException("tvec.total() * tvec.channels() < 3");
            if (tvec.type() != CvType.CV_64FC(tvec.channels()))
                throw new ArgumentException("tvec.type() != CvType.CV_64FC(tvec.channels())");

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> tvecSpan = tvec.AsSpan<double>();
            return new Vector3((float)tvecSpan[0], (float)tvecSpan[1], (float)tvecSpan[2]);
#else
            double[] tvecValues = new double[3];
            tvec.get(0, 0, tvecValues);
            return ConvertTvecToPos(tvecValues);
#endif
        }

        /// <summary>
        /// Converts rvec and tvec value to PoseData.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <param name="tvec">Tvec.</param>
        /// <returns>PoseData.</returns>
        public static PoseData ConvertRvecTvecToPoseData(double[] rvec, double[] tvec)
        {
            PoseData data = new PoseData();
            data.Pos = ConvertTvecToPos(tvec);
            data.Rot = ConvertRvecToRot(rvec);

            return data;
        }

        /// <summary>
        /// Converts rvec and tvec value to PoseData.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <param name="tvec">Tvec.</param>
        /// <returns>PoseData.</returns>
        public static PoseData ConvertRvecTvecToPoseData(Mat rvec, Mat tvec)
        {
            PoseData data = new PoseData();
            data.Pos = ConvertTvecToPos(tvec);
            data.Rot = ConvertRvecToRot(rvec);

            return data;
        }

        private static Vector3 vector_one = Vector3.one;
        private static Matrix4x4 invertYMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, -1, 1));

        /// <summary>
        /// Converts PoseData to transform matrix.
        /// </summary>
        /// <param name="poseData">PoseData.</param>
        /// <param name="toLeftHandCoordinateSystem">
        /// When true, converts the transformation matrix from OpenCV's right-handed coordinate system
        /// to Unity's left-handed coordinate system by inverting the Y-axis.
        /// This option must be enabled (set to true) when applying a pose calculated by OpenCV to a Unity object.
        /// When false (default), returns the matrix without coordinate system conversion.
        /// </param>
        /// <returns>Transform matrix.</returns>
        public static Matrix4x4 ConvertPoseDataToMatrix(ref PoseData poseData, bool toLeftHandCoordinateSystem = false)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(poseData.Pos, poseData.Rot, vector_one);

            // right-handed coordinates system (OpenCV) to left-handed one (Unity)
            // https://stackoverflow.com/questions/30234945/change-handedness-of-a-row-major-4x4-transformation-matrix
            if (toLeftHandCoordinateSystem)
                matrix = invertYMatrix * matrix * invertYMatrix;

            return matrix;
        }

        /// <summary>
        /// Converts transform matrix to PoseData.
        /// </summary>
        /// <param name="matrix">Transform matrix.</param>
        /// <returns>PoseData.</returns>
        public static PoseData ConvertMatrixToPoseData(ref Matrix4x4 matrix)
        {
            PoseData data = new PoseData();
            data.Pos = ExtractTranslationFromMatrix(ref matrix);
            data.Rot = ExtractRotationFromMatrix(ref matrix);

            return data;
        }

        /// <summary>
        /// Creates pose data dictionary.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <param name="rvecs">Rvecs.</param>
        /// <param name="tvecs">Tvecs.</param>
        /// <returns>PoseData dictionary.</returns>
        public static Dictionary<int, PoseData> CreatePoseDataDict(int[] ids, double[] rvecs, double[] tvecs)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");
            if (rvecs == null)
                throw new ArgumentNullException("rvecs");
            if (tvecs == null)
                throw new ArgumentNullException("tvecs");

            Dictionary<int, PoseData> dict = new Dictionary<int, PoseData>();
            if (ids.Length == 0 || ids.Length * 3 != rvecs.Length || ids.Length * 3 != tvecs.Length)
                return dict;

            Vector3 rvec = new Vector3();
            for (int i = 0; i < ids.Length; i++)
            {
                PoseData data = new PoseData();
                data.Pos.Set((float)tvecs[i * 3], (float)tvecs[i * 3 + 1], (float)tvecs[i * 3 + 2]);

                rvec.Set((float)rvecs[i * 3], (float)rvecs[i * 3 + 1], (float)rvecs[i * 3 + 2]);
                float theta = rvec.magnitude;
                rvec.Normalize();
                data.Rot = Quaternion.AngleAxis(theta * Mathf.Rad2Deg, rvec);

                dict[ids[i]] = data;
            }
            return dict;
        }

        /// <summary>
        /// Performs a lowpass check on the position and rotation in newPose, comparing them to oldPose.
        /// </summary>
        /// <param name="oldPose">Old PoseData.</param>
        /// <param name="newPose">New PoseData.</param>
        /// <param name="posThreshold">Position threshold.</param>
        /// <param name="rotThreshold">Rotation threshold.</param>
        public static void LowpassPoseData(ref PoseData oldPose, ref PoseData newPose, float posThreshold, float rotThreshold)
        {
            posThreshold *= posThreshold;

            float posDiff = (newPose.Pos - oldPose.Pos).sqrMagnitude;
            float rotDiff = Quaternion.Angle(newPose.Rot, oldPose.Rot);

            if (posDiff < posThreshold)
            {
                newPose.Pos = oldPose.Pos;
            }

            if (rotDiff < rotThreshold)
            {
                newPose.Rot = oldPose.Rot;
            }
        }

        /// <summary>
        /// Performs smoothing filter on the position and rotation in newPose using exponential moving average.
        /// </summary>
        /// <param name="oldPose">Old PoseData.</param>
        /// <param name="newPose">New PoseData.</param>
        /// <param name="positionSmoothingFactor">Position smoothing factor (0.0 = no filtering, 1.0 = maximum filtering).</param>
        /// <param name="rotationSmoothingFactor">Rotation smoothing factor (0.0 = no filtering, 1.0 = maximum filtering).</param>
        public static void SmoothingFilterPoseData(ref PoseData oldPose, ref PoseData newPose, float positionSmoothingFactor, float rotationSmoothingFactor)
        {
            // Apply exponential moving average filtering for position
            // Standard EMA formula: smoothed = alpha * new + (1 - alpha) * previous
            // where alpha = (1 - smoothingFactor)
            // 0.0 = no filtering (immediate response), 1.0 = maximum filtering (no movement)
            newPose.Pos = Vector3.Lerp(newPose.Pos, oldPose.Pos, positionSmoothingFactor);

            // Apply exponential moving average filtering for rotation
            newPose.Rot = Quaternion.Lerp(newPose.Rot, oldPose.Rot, rotationSmoothingFactor);
        }

        /// <summary>
        /// Performs a lowpass check on the position and rotation of each marker in newDict, comparing them to those in oldDict.
        /// </summary>
        /// <param name="oldDict">Old dictionary.</param>
        /// <param name="newDict">New dictionary.</param>
        /// <param name="posThreshold">Position threshold.</param>
        /// <param name="rotThreshold">Rotation threshold.</param>
        public static void LowpassPoseDataDict(Dictionary<int, PoseData> oldDict, Dictionary<int, PoseData> newDict, float posThreshold, float rotThreshold)
        {
            if (oldDict == null)
                throw new ArgumentNullException("oldDict");
            if (newDict == null)
                throw new ArgumentNullException("newDict");

            posThreshold *= posThreshold;

            List<int> keys = new List<int>(newDict.Keys);
            foreach (int key in keys)
            {
                if (!oldDict.ContainsKey(key))
                    continue;

                PoseData oldPose = oldDict[key];
                PoseData newPose = newDict[key];

                float posDiff = (newPose.Pos - oldPose.Pos).sqrMagnitude;
                float rotDiff = Quaternion.Angle(newPose.Rot, oldPose.Rot);

                if (posDiff < posThreshold)
                {
                    newPose.Pos = oldPose.Pos;
                }

                if (rotDiff < rotThreshold)
                {
                    newPose.Rot = oldPose.Rot;
                }

                newDict[key] = newPose;
            }
        }

        /// <summary>
        /// Performs smoothing filter on the position and rotation of each marker in newDict using exponential moving average.
        /// </summary>
        /// <param name="oldDict">Old dictionary.</param>
        /// <param name="newDict">New dictionary.</param>
        /// <param name="positionSmoothingFactor">Position smoothing factor (0.0 = no filtering, 1.0 = maximum filtering).</param>
        /// <param name="rotationSmoothingFactor">Rotation smoothing factor (0.0 = no filtering, 1.0 = maximum filtering).</param>
        public static void SmoothingFilterPoseDataDict(Dictionary<int, PoseData> oldDict, Dictionary<int, PoseData> newDict, float positionSmoothingFactor, float rotationSmoothingFactor)
        {
            if (oldDict == null)
                throw new ArgumentNullException("oldDict");
            if (newDict == null)
                throw new ArgumentNullException("newDict");

            List<int> keys = new List<int>(newDict.Keys);
            foreach (int key in keys)
            {
                if (!oldDict.ContainsKey(key))
                    continue;

                PoseData oldPose = oldDict[key];
                PoseData newPose = newDict[key];

                // Apply exponential moving average filtering for position
                newPose.Pos = Vector3.Lerp(newPose.Pos, oldPose.Pos, positionSmoothingFactor);

                // Apply exponential moving average filtering for rotation
                newPose.Rot = Quaternion.Lerp(newPose.Rot, oldPose.Rot, rotationSmoothingFactor);

                newDict[key] = newPose;
            }
        }

        /// <summary>
        /// Extract translation from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Translation offset.
        /// </returns>
        public static Vector3 ExtractTranslationFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 translate;
            translate.x = matrix.m03;
            translate.y = matrix.m13;
            translate.z = matrix.m23;
            return translate;
        }

        /// <summary>
        /// Extract rotation quaternion from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Quaternion representation of rotation transform.
        /// </returns>
        public static Quaternion ExtractRotationFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        /// <summary>
        /// Extract scale from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Scale vector.
        /// </returns>
        public static Vector3 ExtractScaleFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;

            if (Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized)
            {
                scale.x *= -1;
            }

            return scale;
        }

        /// <summary>
        /// Compose TRS matrix from position, rotation and scale.
        /// </summary>
        /// <param name="localPosition">Position.</param>
        /// <param name="localRotation">Rotation.</param>
        /// <param name="localScale">Scale.</param>
        /// <param name="matrix">Transform matrix.</param>
        public static void ComposeMatrix(Vector3 localPosition, Quaternion localRotation, Vector3 localScale, out Matrix4x4 matrix)
        {
            matrix = Matrix4x4.TRS(localPosition, localRotation, localScale);
        }

        /// <summary>
        /// Extract position, rotation and scale from TRS matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <param name="localPosition">Position.</param>
        /// <param name="localRotation">Rotation.</param>
        /// <param name="localScale">Scale.</param>
        public static void DecomposeMatrix(ref Matrix4x4 matrix, out Vector3 localPosition, out Quaternion localRotation, out Vector3 localScale)
        {
            localPosition = ExtractTranslationFromMatrix(ref matrix);
            localRotation = ExtractRotationFromMatrix(ref matrix);
            localScale = ExtractScaleFromMatrix(ref matrix);
        }

        /// <summary>
        /// Set transform component from TRS matrix.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        public static void SetTransformFromMatrix(Transform transform, ref Matrix4x4 matrix)
        {
            transform.localPosition = ExtractTranslationFromMatrix(ref matrix);
            transform.localRotation = ExtractRotationFromMatrix(ref matrix);
            transform.localScale = ExtractScaleFromMatrix(ref matrix);
        }

        /// <summary>
        /// Calculates a projection matrix from OpenCV-style camera intrinsic parameters,
        /// suitable for use as <see cref="Camera.projectionMatrix"/> in Unity.
        /// <para>
        /// Converts from the right-handed OpenCV camera coordinate system (Z-forward, Y-down)
        /// to Unity's left-handed camera coordinate system (Z-forward, Y-up).
        /// </para>
        /// <para>
        /// The resulting matrix can be directly assigned to <see cref="Camera.projectionMatrix"/>,
        /// regardless of the rendering backend (OpenGL ES, Vulkan, or DirectX).
        /// </para>
        /// <para>
        /// This effectively reverses the operation performed by
        /// <see cref="CalculateCameraMatrixValuesFromProjectionMatrix"/>.
        /// </para>
        /// </summary>
        /// <param name="fx">Focal length in pixels along the X axis.</param>
        /// <param name="fy">Focal length in pixels along the Y axis.</param>
        /// <param name="cx">Principal point (image center) X coordinate in pixels.</param>
        /// <param name="cy">Principal point (image center) Y coordinate in pixels.</param>
        /// <param name="width">Image width in pixels.</param>
        /// <param name="height">Image height in pixels.</param>
        /// <param name="near">Near clipping plane distance.</param>
        /// <param name="far">Far clipping plane distance.</param>
        /// <returns>
        /// A 4×4 projection matrix compatible with Unity’s <see cref="Camera"/> component.
        /// </returns>
        public static Matrix4x4 CalculateProjectionMatrixFromCameraMatrixValues(
            float fx, float fy, float cx, float cy,
            float width, float height,
            float near = 0.01f, float far = 2000f)
        {
            // Construct a projection matrix from OpenCV intrinsics.
            // OpenCV origin: top-left (Y down)
            // Unity origin: bottom-left (Y up)
            // => Flip Y-axis when mapping principal point.
            Matrix4x4 projectionMatrix = new Matrix4x4();

            projectionMatrix.m00 = 2.0f * fx / width;
            projectionMatrix.m02 = 1.0f - 2.0f * cx / width;

            projectionMatrix.m11 = 2.0f * fy / height;
            projectionMatrix.m12 = -1.0f + 2.0f * cy / height; // flip Y

            projectionMatrix.m22 = -(far + near) / (far - near);
            projectionMatrix.m23 = -2.0f * far * near / (far - near);
            projectionMatrix.m32 = -1.0f;
            projectionMatrix.m33 = 0.0f;

            return projectionMatrix;
        }

        /// <summary>
        /// Calculates OpenCV-style intrinsic camera parameters (camera matrix values)
        /// from a Unity-compatible projection matrix.
        /// <para>
        /// Converts from Unity's left-handed camera coordinate system (Z-forward, Y-up)
        /// (used by <see cref="Camera.projectionMatrix"/>) to
        /// OpenCV's right-handed camera coordinate system (Z-forward, Y-down).
        /// </para>
        /// <para>
        /// This effectively reverses the operation performed by
        /// <see cref="CalculateProjectionMatrixFromCameraMatrixValues"/>.
        /// </para>
        /// </summary>
        /// <param name="projectionMatrix">A Unity camera projection matrix.</param>
        /// <param name="width">Image width in pixels.</param>
        /// <param name="height">Image height in pixels.</param>
        /// <returns>
        /// A 3×3-style camera matrix where:
        /// <br/> fx = m00, fy = m11, cx = m02, cy = m12
        /// </returns>
        public static Matrix4x4 CalculateCameraMatrixValuesFromProjectionMatrix(
            Matrix4x4 projectionMatrix,
            float width,
            float height)
        {
            // Construct an OpenCV-style intrinsic matrix (K matrix)
            // OpenCV origin: top-left (Y down)
            // Unity origin: bottom-left (Y up)
            // => Y-axis must be flipped when recovering principal point.
            Matrix4x4 cameraMatrix = new Matrix4x4();

            cameraMatrix.m00 = (projectionMatrix.m00 * width) / 2.0f; // fx
            cameraMatrix.m11 = (projectionMatrix.m11 * height) / 2.0f; // fy
            cameraMatrix.m02 = width * (1.0f - projectionMatrix.m02) / 2.0f; // cx
            cameraMatrix.m12 = height * (1.0f + projectionMatrix.m12) / 2.0f; // cy (flip Y)
            cameraMatrix.m22 = 1.0f;

            return cameraMatrix;
        }

        /// <summary>
        /// Calculate frustum size.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="distance">Distance.</param>
        /// <param name="fov">Field of view. (horizontal or vertical direction)</param>
        /// <returns>
        /// Frustum height.
        /// </returns>
        public static float CalculateFrustumSize(float distance, float fov)
        {
            return 2.0f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate distance.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="frustumHeight">One side size of a frustum.</param>
        /// <param name="fov">Field of view. (horizontal or vertical direction)</param>
        /// <returns>
        /// Distance.
        /// </returns>
        public static float CalculateDistance(float frustumSize, float fov)
        {
            return frustumSize * 0.5f / Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate FOV angle.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="frustumHeight">One side size of a frustum.</param>
        /// <param name="distance">Distance.</param>
        /// <returns>
        /// FOV angle.
        /// </returns>
        public static float CalculateFOVAngle(float frustumSize, float distance)
        {
            return 2.0f * Mathf.Atan(frustumSize * 0.5f / distance) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Safe version of drawFrameAxes that prevents hang or crashes when tvec has zero or near-zero Z component.
        /// Draw axes of the world/object coordinate system from pose estimation. @sa solvePnP
        /// </summary>
        /// <param name="image">
        /// Input/output image. It must have 1 or 3 channels. The number of channels is not altered.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input 3x3 floating-point matrix of camera intrinsic parameters.
        ///  \f$\cameramatrix{A}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is empty, the zero distortion coefficients are assumed.
        /// </param>
        /// <param name="rvec">
        /// Rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///  the model coordinate system to the camera coordinate system.
        /// </param>
        /// <param name="tvec">
        /// Translation vector.
        /// </param>
        /// <param name="length">
        /// Length of the painted axes in the same unit than tvec (usually in meters).
        /// </param>
        /// <param name="thickness">
        /// Line thickness of the painted axes.
        /// </param>
        /// <remarks>
        /// OX is drawn in red, OY in green and OZ in blue.
        /// It includes safety checks to prevent crashes when tvec has invalid Z values.
        /// </remarks>
        public static void SafeDrawFrameAxes(Mat image, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, float length, int thickness = 3)
        {
            if (tvec != null) tvec.ThrowIfDisposed();
            if (!tvec.isContinuous())
                throw new ArgumentException("tvec is not continuous");
            if (tvec.total() * tvec.channels() < 3)
                throw new ArgumentException("tvec.total() * tvec.channels() < 3");
            if (tvec.type() != CvType.CV_64FC(tvec.channels()))
                throw new ArgumentException("tvec.type() != CvType.CV_64FC(tvec.channels())");

            // drawFrameAxes() may hang or crash when the projected points are result in NaN/INF due to zero or near-zero Z component in tvec.
#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            ReadOnlySpan<double> tvecSpan = tvec.AsSpan<double>();
            if (tvecSpan[2] < 1e-4)
                return;
#else
            if (tvec.get(2, 0)[0] < 1e-4)
                return;
#endif

            Calib3d.drawFrameAxes(image, cameraMatrix, distCoeffs, rvec, tvec, length, thickness);
        }
    }
}
