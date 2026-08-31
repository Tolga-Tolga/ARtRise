
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{

    // C++: class CLAHE
    /// <summary>
    ///  Base class for Contrast Limited Adaptive Histogram Equalization.
    /// </summary>
    public partial class CLAHE : Algorithm
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        imgproc_CLAHE_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal CLAHE(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new CLAHE __fromPtr__(IntPtr addr) { return new CLAHE(addr); }

        //
        // C++:  void cv::CLAHE::apply(Mat src, Mat& dst)
        //

        /// <summary>
        ///  Equalizes the histogram of a grayscale image using Contrast Limited Adaptive Histogram Equalization.
        /// </summary>
        /// <param name="src">
        /// Source image of type CV_8UC1 or CV_16UC1.
        /// </param>
        /// <param name="dst">
        /// Destination image.
        /// </param>
        public void apply(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_CLAHE_apply_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


        //
        // C++:  void cv::CLAHE::setClipLimit(double clipLimit)
        //

        /// <summary>
        ///  Sets threshold for contrast limiting.
        /// </summary>
        /// <param name="clipLimit">
        /// threshold value.
        /// </param>
        public void setClipLimit(double clipLimit)
        {
            ThrowIfDisposed();

            imgproc_CLAHE_setClipLimit_10(nativeObj, clipLimit);


        }


        //
        // C++:  double cv::CLAHE::getClipLimit()
        //

        public double getClipLimit()
        {
            ThrowIfDisposed();

            return imgproc_CLAHE_getClipLimit_10(nativeObj);


        }


        //
        // C++:  void cv::CLAHE::setTilesGridSize(Size tileGridSize)
        //

        /// <summary>
        ///  Sets size of grid for histogram equalization. Input image will be divided into
        ///      equally sized rectangular tiles.
        /// </summary>
        /// <param name="tileGridSize">
        /// defines the number of tiles in row and column.
        /// </param>
        public void setTilesGridSize(Size tileGridSize)
        {
            ThrowIfDisposed();

            imgproc_CLAHE_setTilesGridSize_10(nativeObj, tileGridSize.width, tileGridSize.height);


        }


        //
        // C++:  Size cv::CLAHE::getTilesGridSize()
        //

        public Size getTilesGridSize()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            imgproc_CLAHE_getTilesGridSize_10(nativeObj, tmpArray);
            Size retVal = new Size(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::CLAHE::collectGarbage()
        //

        public void collectGarbage()
        {
            ThrowIfDisposed();

            imgproc_CLAHE_collectGarbage_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::CLAHE::apply(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_apply_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // C++:  void cv::CLAHE::setClipLimit(double clipLimit)
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_setClipLimit_10(IntPtr nativeObj, double clipLimit);

        // C++:  double cv::CLAHE::getClipLimit()
        [DllImport(LIBNAME)]
        private static extern double imgproc_CLAHE_getClipLimit_10(IntPtr nativeObj);

        // C++:  void cv::CLAHE::setTilesGridSize(Size tileGridSize)
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_setTilesGridSize_10(IntPtr nativeObj, double tileGridSize_width, double tileGridSize_height);

        // C++:  Size cv::CLAHE::getTilesGridSize()
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_getTilesGridSize_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::CLAHE::collectGarbage()
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_collectGarbage_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void imgproc_CLAHE_delete(IntPtr nativeObj);

    }
}
