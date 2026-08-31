
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class FastGlobalSmootherFilter
    /// <summary>
    ///  Interface for implementations of Fast Global Smoother filter.
    /// </summary>
    /// <remarks>
    ///  For more details about this filter see @cite Min2014 and @cite Farbman2008 .
    /// </remarks>
    public class FastGlobalSmootherFilter : Algorithm
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
                        ximgproc_FastGlobalSmootherFilter_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal FastGlobalSmootherFilter(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new FastGlobalSmootherFilter __fromPtr__(IntPtr addr) { return new FastGlobalSmootherFilter(addr); }

        //
        // C++:  void cv::ximgproc::FastGlobalSmootherFilter::filter(Mat src, Mat& dst)
        //

        /// <summary>
        ///  Apply smoothing operation to the source image.
        /// </summary>
        /// <param name="src">
        /// source image for filtering with unsigned 8-bit or signed 16-bit or floating-point 32-bit depth and up to 4 channels.
        /// </param>
        /// <param name="dst">
        /// destination image.
        /// </param>
        public void filter(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_FastGlobalSmootherFilter_filter_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::FastGlobalSmootherFilter::filter(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastGlobalSmootherFilter_filter_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastGlobalSmootherFilter_delete(IntPtr nativeObj);

    }
}
