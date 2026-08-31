
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{

    // C++: class DenseOpticalFlow
    /// <remarks>
    ///  Base class for dense optical flow algorithms
    /// </remarks>
    public class DenseOpticalFlow : Algorithm
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
                        video_DenseOpticalFlow_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal DenseOpticalFlow(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new DenseOpticalFlow __fromPtr__(IntPtr addr) { return new DenseOpticalFlow(addr); }

        //
        // C++:  void cv::DenseOpticalFlow::calc(Mat I0, Mat I1, Mat& flow)
        //

        /// <summary>
        ///  Calculates an optical flow.
        /// </summary>
        /// <param name="I0">
        /// first 8-bit single-channel input image.
        /// </param>
        /// <param name="I1">
        /// second input image of the same size and the same type as prev.
        /// </param>
        /// <param name="flow">
        /// computed flow image that has the same size as prev and type CV_32FC2.
        /// </param>
        public void calc(Mat I0, Mat I1, Mat flow)
        {
            ThrowIfDisposed();
            if (I0 != null) I0.ThrowIfDisposed();
            if (I1 != null) I1.ThrowIfDisposed();
            if (flow != null) flow.ThrowIfDisposed();

            video_DenseOpticalFlow_calc_10(nativeObj, I0.nativeObj, I1.nativeObj, flow.nativeObj);


        }


        //
        // C++:  void cv::DenseOpticalFlow::collectGarbage()
        //

        /// <summary>
        ///  Releases all inner buffers.
        /// </summary>
        public void collectGarbage()
        {
            ThrowIfDisposed();

            video_DenseOpticalFlow_collectGarbage_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::DenseOpticalFlow::calc(Mat I0, Mat I1, Mat& flow)
        [DllImport(LIBNAME)]
        private static extern void video_DenseOpticalFlow_calc_10(IntPtr nativeObj, IntPtr I0_nativeObj, IntPtr I1_nativeObj, IntPtr flow_nativeObj);

        // C++:  void cv::DenseOpticalFlow::collectGarbage()
        [DllImport(LIBNAME)]
        private static extern void video_DenseOpticalFlow_collectGarbage_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_DenseOpticalFlow_delete(IntPtr nativeObj);

    }
}
