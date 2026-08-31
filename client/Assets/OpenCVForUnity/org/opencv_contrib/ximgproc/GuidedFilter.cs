
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class GuidedFilter
    /// <summary>
    ///  Interface for realizations of (Fast) Guided Filter.
    /// </summary>
    /// <remarks>
    ///  For more details about this filter see @cite Kaiming10 @cite Kaiming15 .
    /// </remarks>
    public class GuidedFilter : Algorithm
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
                        ximgproc_GuidedFilter_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal GuidedFilter(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new GuidedFilter __fromPtr__(IntPtr addr) { return new GuidedFilter(addr); }

        //
        // C++:  void cv::ximgproc::GuidedFilter::filter(Mat src, Mat& dst, int dDepth = -1)
        //

        /// <summary>
        ///  Apply (Fast) Guided Filter to the filtering image.
        /// </summary>
        /// <param name="src">
        /// filtering image with any numbers of channels.
        /// </param>
        /// <param name="dst">
        /// output image.
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

            ximgproc_GuidedFilter_filter_10(nativeObj, src.nativeObj, dst.nativeObj, dDepth);


        }

        /// <summary>
        ///  Apply (Fast) Guided Filter to the filtering image.
        /// </summary>
        /// <param name="src">
        /// filtering image with any numbers of channels.
        /// </param>
        /// <param name="dst">
        /// output image.
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

            ximgproc_GuidedFilter_filter_11(nativeObj, src.nativeObj, dst.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::GuidedFilter::filter(Mat src, Mat& dst, int dDepth = -1)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_GuidedFilter_filter_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj, int dDepth);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_GuidedFilter_filter_11(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_GuidedFilter_delete(IntPtr nativeObj);

    }
}
