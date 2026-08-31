
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class DTFilter
    /// <summary>
    ///  Interface for realizations of Domain Transform filter.
    /// </summary>
    /// <remarks>
    ///  For more details about this filter see @cite Gastal11 .
    /// </remarks>
    public class DTFilter : Algorithm
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
                        ximgproc_DTFilter_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal DTFilter(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new DTFilter __fromPtr__(IntPtr addr) { return new DTFilter(addr); }

        //
        // C++:  void cv::ximgproc::DTFilter::filter(Mat src, Mat& dst, int dDepth = -1)
        //

        /// <summary>
        ///  Produce domain transform filtering operation on source image.
        /// </summary>
        /// <param name="src">
        /// filtering image with unsigned 8-bit or floating-point 32-bit depth and up to 4 channels.
        /// </param>
        /// <param name="dst">
        /// destination image.
        /// </param>
        /// <param name="dDepth">
        /// optional depth of the output image. dDepth can be set to -1, which will be equivalent
        ///      to src.depth().
        /// </param>
        public void filter(Mat src, Mat dst, int dDepth)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_DTFilter_filter_10(nativeObj, src.nativeObj, dst.nativeObj, dDepth);


        }

        /// <summary>
        ///  Produce domain transform filtering operation on source image.
        /// </summary>
        /// <param name="src">
        /// filtering image with unsigned 8-bit or floating-point 32-bit depth and up to 4 channels.
        /// </param>
        /// <param name="dst">
        /// destination image.
        /// </param>
        /// <param name="dDepth">
        /// optional depth of the output image. dDepth can be set to -1, which will be equivalent
        ///      to src.depth().
        /// </param>
        public void filter(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_DTFilter_filter_11(nativeObj, src.nativeObj, dst.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::DTFilter::filter(Mat src, Mat& dst, int dDepth = -1)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_DTFilter_filter_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj, int dDepth);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_DTFilter_filter_11(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_DTFilter_delete(IntPtr nativeObj);

    }
}
