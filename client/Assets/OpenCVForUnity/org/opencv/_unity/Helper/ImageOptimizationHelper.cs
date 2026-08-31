using System;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using UnityEngine.Serialization;
using Rect = OpenCVForUnity.CoreModule.Rect;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// A helper component for optimizing image processing in Unity by managing frame skipping
    /// and downscaling operations.
    /// </summary>
    /// <remarks>
    /// The <see cref="ImageOptimizationHelper"/> class provides utilities to enhance performance
    /// during image processing tasks. It includes features such as:
    /// <list type="bullet">
    ///     <item><description>Downscaling images to reduce computational load</description></item>
    ///     <item><description>Skipping frames to optimize processing frequency</description></item>
    /// </list>
    /// This class is particularly useful when working with high-resolution images or real-time
    /// processing where performance is a priority.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject to enable image optimization:
    /// <code>
    /// // Example usage of the ImageOptimizationHelper component
    /// ImageOptimizationHelper imageHelper = gameObject.AddComponent&lt;ImageOptimizationHelper&gt;();
    /// imageHelper.downscaleRatio = 2.0f;
    /// imageHelper.frameSkippingRatio = 3;
    ///
    /// // Check if the current frame should be skipped
    /// if (!imageHelper.IsCurrentFrameSkipped())
    /// {
    ///     // Perform operations on the downscaled image
    ///     Mat optimizedMat = imageHelper.GetDownScaleMat(originalMat);
    /// }
    /// </code>
    /// </example>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Optimization.ImageOptimizationHelper class instead.")]
    public class ImageOptimizationHelper : MonoBehaviour
    {
        [SerializeField, Tooltip("Set the ratio of down scaling.")]
        protected float _downscaleRatio = 2f;

        /// <summary>
        /// The downscale ratio.
        /// </summary>
        public virtual float downscaleRatio
        {
            get { return _downscaleRatio; }
            set { _downscaleRatio = Mathf.Clamp(value, 1f, float.MaxValue); }
        }

        [SerializeField, Tooltip("Set the ratio of frame skipping.")]
        protected int _frameSkippingRatio = 2;

        /// <summary>
        /// The frame skipping ratio.
        /// </summary>
        public virtual int frameSkippingRatio
        {
            get { return _frameSkippingRatio; }
            set { _frameSkippingRatio = (int)Mathf.Clamp(value, 1f, float.MaxValue); }
        }

        /// <summary>
        /// The frame count.
        /// </summary>
        protected int _frameCount = 0;

        /// <summary>
        /// The downscale frame mat.
        /// </summary>
        protected Mat _downScaleFrameMat;

        protected virtual void OnDestroy()
        {
            Dispose();
        }

        protected virtual void OnValidate()
        {
            _downscaleRatio = Mathf.Clamp(_downscaleRatio, 1f, float.MaxValue);
            _frameSkippingRatio = (int)Mathf.Clamp(_frameSkippingRatio, 1f, float.MaxValue);
        }

        /// <summary>
        /// Indicates whether the current frame is skipped.
        /// </summary>
        /// <returns><c>true</c>, if the current frame is skipped, <c>false</c> otherwise.</returns>
        public virtual bool IsCurrentFrameSkipped()
        {
            _frameCount++;

            if (_frameCount == int.MaxValue)
                _frameCount = 0;

            return (_frameCount % frameSkippingRatio != 0) ? true : false;
        }

        /// <summary>
        /// Gets the mat that downscaled the original mat.
        /// if downscaleRatio == 1 , return originalMat.
        /// </summary>
        /// <remarks>
        /// Please do not dispose of the returned mat as it will be reused.
        /// </remarks>
        /// <returns>The downscale mat.</returns>
        /// <param name="originalMat">Original mat.</param>
        public virtual Mat GetDownScaleMat(Mat originalMat)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return originalMat;

            if (_downScaleFrameMat == null)
                _downScaleFrameMat = new Mat();

            if (_downScaleFrameMat.IsDisposed)
            {
                Debug.LogWarning("ImageOptimizationHelper:: " + "Please do not dispose of the Mat returned by GetDownScaleMat as it will be reused");
                _downScaleFrameMat = new Mat();
            }

            Imgproc.resize(originalMat, _downScaleFrameMat, new Size(), 1.0 / _downscaleRatio, 1.0 / _downscaleRatio, Imgproc.INTER_LINEAR);

            return _downScaleFrameMat;
        }

        /// <summary>
        /// Restores the original size of the Rect based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRect">The detected rectangle in the downscaled image.</param>
        /// <returns>A new Rect adjusted to the original image size.</returns>
        public virtual Rect RestoreOriginalSizeRect(Rect downscaledRect)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return downscaledRect.clone();

            return new Rect(
                (int)(downscaledRect.x * _downscaleRatio),
                (int)(downscaledRect.y * _downscaleRatio),
                (int)(downscaledRect.width * _downscaleRatio),
                (int)(downscaledRect.height * _downscaleRatio)
            );
        }

        /// <summary>
        /// Restores the original size of the Rect2d based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRect">The detected rectangle in the downscaled image.</param>
        /// <returns>A new Rect2d adjusted to the original image size.</returns>
        public virtual Rect2d RestoreOriginalSizeRect2d(Rect2d downscaledRect)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return downscaledRect.clone();

            return new Rect2d(
                downscaledRect.x * _downscaleRatio,
                downscaledRect.y * _downscaleRatio,
                downscaledRect.width * _downscaleRatio,
                downscaledRect.height * _downscaleRatio
            );
        }

        /// <summary>
        /// Restores the original size of the RotatedRect based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRect">The detected rectangle in the downscaled image.</param>
        /// <returns>A new RotatedRect adjusted to the original image size.</returns>
        public virtual RotatedRect RestoreOriginalSizeRotatedRect(RotatedRect downscaledRect)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return downscaledRect.clone();

            return new RotatedRect(
                new Point(downscaledRect.center.x * _downscaleRatio, downscaledRect.center.y * _downscaleRatio),
                new Size(downscaledRect.size.width * _downscaleRatio, downscaledRect.size.height * _downscaleRatio),
                downscaledRect.angle
            );
        }

        /// <summary>
        /// Restores the original size of the Point based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoint">The detected point in the downscaled image.</param>
        /// <returns>A new Point adjusted to the original image size.</returns>
        public virtual Point RestoreOriginalSizePoint(Point downscaledPoint)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return downscaledPoint.clone();

            return new Point(
                downscaledPoint.x * _downscaleRatio,
                downscaledPoint.y * _downscaleRatio
            );
        }

        /// <summary>
        /// Restores the original size of the Point3 based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoint">The detected point in the downscaled image.</param>
        /// <returns>A new Point3 adjusted to the original image size.</returns>
        public virtual Point3 RestoreOriginalSizePoint3(Point3 downscaledPoint)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return downscaledPoint.clone();

            return new Point3(
                downscaledPoint.x * _downscaleRatio,
                downscaledPoint.y * _downscaleRatio,
                downscaledPoint.z * _downscaleRatio
            );
        }

        /// <summary>
        /// Restores the original size of the KeyPoint based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoint">The detected keyooint in the downscaled image.</param>
        /// <returns>A new KeyPoint adjusted to the original image size.</returns>
        public virtual KeyPoint RestoreOriginalSizeKeyPoint(KeyPoint downscaledKeyPoint)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new KeyPoint(
                    (float)downscaledKeyPoint.pt.x,
                    (float)downscaledKeyPoint.pt.y,
                    downscaledKeyPoint.size,
                    downscaledKeyPoint.angle,
                    downscaledKeyPoint.response,
                    downscaledKeyPoint.octave,
                    downscaledKeyPoint.class_id
                );

            return new KeyPoint(
                (float)(downscaledKeyPoint.pt.x * _downscaleRatio),
                (float)(downscaledKeyPoint.pt.y * _downscaleRatio),
                downscaledKeyPoint.size * _downscaleRatio,
                downscaledKeyPoint.angle,
                downscaledKeyPoint.response,
                downscaledKeyPoint.octave,
                downscaledKeyPoint.class_id
            );
        }

        /// <summary>
        /// Restores the original size of the MatOfRect based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRects">The MatOfRect containing the detected rectangles in the downscaled image.</param>
        /// <returns>A new MatOfRect with the rectangles adjusted to the original image size.</returns>
        public virtual MatOfRect RestoreOriginalSizeMatOfRect(MatOfRect downscaledRects)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfRect(downscaledRects.clone());

            Rect[] downscaledArray = downscaledRects.toArray();
            Rect[] originalArray = new Rect[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Rect downscaledRect = downscaledArray[i];
                Rect originalRect = new Rect(
                    (int)(downscaledRect.x * _downscaleRatio),
                    (int)(downscaledRect.y * _downscaleRatio),
                    (int)(downscaledRect.width * _downscaleRatio),
                    (int)(downscaledRect.height * _downscaleRatio)
                );
                originalArray[i] = originalRect;
            }

            return new MatOfRect(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfRect2d based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRects">The MatOfRect2d containing the detected rectangles in the downscaled image.</param>
        /// <returns>A new MatOfRect2d with the rectangles adjusted to the original image size.</returns>
        public virtual MatOfRect2d RestoreOriginalSizeMatOfRect2d(MatOfRect2d downscaledRects)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfRect2d(downscaledRects.clone());

            Rect2d[] downscaledArray = downscaledRects.toArray();
            Rect2d[] originalArray = new Rect2d[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Rect2d downscaledRect = downscaledArray[i];
                Rect2d originalRect = new Rect2d(
                    downscaledRect.x * _downscaleRatio,
                    downscaledRect.y * _downscaleRatio,
                    downscaledRect.width * _downscaleRatio,
                    downscaledRect.height * _downscaleRatio
                );
                originalArray[i] = originalRect;
            }

            return new MatOfRect2d(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfRotatedRect based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledRects">The MatOfRotatedRect containing the detected rectangles in the downscaled image.</param>
        /// <returns>A new MatOfRotatedRect with the rectangles adjusted to the original image size.</returns>
        public virtual MatOfRotatedRect RestoreOriginalSizeMatOfRotatedRect(MatOfRotatedRect downscaledRects)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfRotatedRect(downscaledRects.clone());

            RotatedRect[] downscaledArray = downscaledRects.toArray();
            RotatedRect[] originalArray = new RotatedRect[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                RotatedRect downscaledRect = downscaledArray[i];
                RotatedRect originalRect = new RotatedRect(
                    new Point(downscaledRect.center.x * _downscaleRatio, downscaledRect.center.y * _downscaleRatio),
                    new Size(downscaledRect.size.width * _downscaleRatio, downscaledRect.size.height * _downscaleRatio),
                    downscaledRect.angle
                );
                originalArray[i] = originalRect;
            }

            return new MatOfRotatedRect(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfPoint based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoints">The MatOfPoint containing the detected points in the downscaled image.</param>
        /// <returns>A new MatOfPoint with the points adjusted to the original image size.</returns>
        public virtual MatOfPoint RestoreOriginalSizeMatOfPoint(MatOfPoint downscaledPoints)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfPoint(downscaledPoints.clone());

            Point[] downscaledArray = downscaledPoints.toArray();
            Point[] originalArray = new Point[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Point downscaledPoint = downscaledArray[i];
                Point originalPoint = new Point(
                    downscaledPoint.x * _downscaleRatio,
                    downscaledPoint.y * _downscaleRatio
                );
                originalArray[i] = originalPoint;
            }

            return new MatOfPoint(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfPoint2f based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoints">The MatOfPoint2f containing the detected points in the downscaled image.</param>
        /// <returns>A new MatOfPoint2f with the points adjusted to the original image size.</returns>
        public virtual MatOfPoint2f RestoreOriginalSizeMatOfPoint2f(MatOfPoint2f downscaledPoints)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfPoint2f(downscaledPoints.clone());

            Point[] downscaledArray = downscaledPoints.toArray();
            Point[] originalArray = new Point[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Point downscaledPoint = downscaledArray[i];
                Point originalPoint = new Point(
                    (float)(downscaledPoint.x * _downscaleRatio),
                    (float)(downscaledPoint.y * _downscaleRatio)
                );
                originalArray[i] = originalPoint;
            }

            return new MatOfPoint2f(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfPoint3 based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoints">The MatOfPoint3 containing the detected points in the downscaled image.</param>
        /// <returns>A new MatOfPoint3 with the points adjusted to the original image size.</returns>
        public virtual MatOfPoint3 RestoreOriginalSizeMatOfPoint3(MatOfPoint3 downscaledPoints)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfPoint3(downscaledPoints.clone());

            Point3[] downscaledArray = downscaledPoints.toArray();
            Point3[] originalArray = new Point3[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Point3 downscaledPoint = downscaledArray[i];
                Point3 originalPoint = new Point3(
                    downscaledPoint.x * _downscaleRatio,
                    downscaledPoint.y * _downscaleRatio,
                    downscaledPoint.z * _downscaleRatio
                );
                originalArray[i] = originalPoint;
            }

            return new MatOfPoint3(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfPoint3f based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledPoints">The MatOfPoint3f containing the detected points in the downscaled image.</param>
        /// <returns>A new MatOfPoint3f with the points adjusted to the original image size.</returns>
        public virtual MatOfPoint3f RestoreOriginalSizeMatOfPoint3f(MatOfPoint3f downscaledPoints)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfPoint3f(downscaledPoints.clone());

            Point3[] downscaledArray = downscaledPoints.toArray();
            Point3[] originalArray = new Point3[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                Point3 downscaledPoint = downscaledArray[i];
                Point3 originalPoint = new Point3(
                    downscaledPoint.x * _downscaleRatio,
                    downscaledPoint.y * _downscaleRatio,
                    downscaledPoint.z * _downscaleRatio
                );
                originalArray[i] = originalPoint;
            }

            return new MatOfPoint3f(originalArray);
        }

        /// <summary>
        /// Restores the original size of the MatOfKeyPoint based on the downscale ratio.
        /// </summary>
        /// <param name="downscaledKeyPoints">The MatOfKeyPoint containing the detected keypoints in the downscaled image.</param>
        /// <returns>A new MatOfKeyPoint with the keypoints adjusted to the original image size.</returns>
        public virtual MatOfKeyPoint RestoreOriginalSizeMatOfKeyPoint(MatOfKeyPoint downscaledKeyPoints)
        {
            if (Mathf.Approximately(_downscaleRatio, 1f))
                return new MatOfKeyPoint(downscaledKeyPoints.clone());

            KeyPoint[] downscaledArray = downscaledKeyPoints.toArray();
            KeyPoint[] originalArray = new KeyPoint[downscaledArray.Length];

            for (int i = 0; i < downscaledArray.Length; i++)
            {
                KeyPoint downscaledKeyPoint = downscaledArray[i];
                KeyPoint originalKeyPoint = new KeyPoint(
                    (float)(downscaledKeyPoint.pt.x * _downscaleRatio),
                    (float)(downscaledKeyPoint.pt.y * _downscaleRatio),
                    downscaledKeyPoint.size * _downscaleRatio,
                    downscaledKeyPoint.angle,
                    downscaledKeyPoint.response,
                    downscaledKeyPoint.octave,
                    downscaledKeyPoint.class_id
                );
                originalArray[i] = originalKeyPoint;
            }

            return new MatOfKeyPoint(originalArray);
        }

        public virtual void Dispose()
        {
            _frameCount = 0;

            _downScaleFrameMat?.Dispose(); _downScaleFrameMat = null;
        }
    }
}
