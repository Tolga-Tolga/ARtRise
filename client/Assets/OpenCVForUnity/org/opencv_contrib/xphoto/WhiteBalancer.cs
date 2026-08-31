
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.PhotoModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XphotoModule
{

    // C++: class WhiteBalancer
    /// <summary>
    ///  The base class for auto white balance algorithms.
    /// </summary>
    public class WhiteBalancer : Algorithm
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
                        xphoto_WhiteBalancer_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal WhiteBalancer(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new WhiteBalancer __fromPtr__(IntPtr addr) { return new WhiteBalancer(addr); }

        //
        // C++:  void cv::xphoto::WhiteBalancer::balanceWhite(Mat src, Mat& dst)
        //

        /// <summary>
        ///  Applies white balancing to the input image
        /// </summary>
        /// <param name="src">
        /// Input image
        /// </param>
        /// <param name="dst">
        /// White balancing result
        ///      @sa cvtColor, equalizeHist
        /// </param>
        public void balanceWhite(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_WhiteBalancer_balanceWhite_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::xphoto::WhiteBalancer::balanceWhite(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void xphoto_WhiteBalancer_balanceWhite_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void xphoto_WhiteBalancer_delete(IntPtr nativeObj);

    }
}
