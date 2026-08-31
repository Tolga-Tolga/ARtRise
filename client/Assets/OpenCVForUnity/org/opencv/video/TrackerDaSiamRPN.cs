
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

    // C++: class TrackerDaSiamRPN


    public class TrackerDaSiamRPN : Tracker
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
                        video_TrackerDaSiamRPN_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerDaSiamRPN(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerDaSiamRPN __fromPtr__(IntPtr addr) { return new TrackerDaSiamRPN(addr); }

        //
        // C++: static Ptr_TrackerDaSiamRPN cv::TrackerDaSiamRPN::create(TrackerDaSiamRPN_Params parameters = TrackerDaSiamRPN::Params())
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// DaSiamRPN parameters TrackerDaSiamRPN::Params
        /// </param>
        public static TrackerDaSiamRPN create(TrackerDaSiamRPN_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerDaSiamRPN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerDaSiamRPN_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// DaSiamRPN parameters TrackerDaSiamRPN::Params
        /// </param>
        public static TrackerDaSiamRPN create()
        {


            return TrackerDaSiamRPN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerDaSiamRPN_create_11()));


        }

#if !UNITY_WSA_10_0


        //
        // C++: static Ptr_TrackerDaSiamRPN cv::TrackerDaSiamRPN::create(Net siam_rpn, Net kernel_cls1, Net kernel_r1)
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="siam_rpn">
        /// pre-loaded SiamRPN model
        /// </param>
        /// <param name="kernel_cls1">
        /// pre-loaded CLS model
        /// </param>
        /// <param name="kernel_r1">
        /// pre-loaded R1 model
        /// </param>
        public static TrackerDaSiamRPN create(Net siam_rpn, Net kernel_cls1, Net kernel_r1)
        {
            if (siam_rpn != null) siam_rpn.ThrowIfDisposed();
            if (kernel_cls1 != null) kernel_cls1.ThrowIfDisposed();
            if (kernel_r1 != null) kernel_r1.ThrowIfDisposed();

            return TrackerDaSiamRPN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerDaSiamRPN_create_12(siam_rpn.getNativeObjAddr(), kernel_cls1.getNativeObjAddr(), kernel_r1.getNativeObjAddr())));


        }

#endif



        //
        // C++:  float cv::TrackerDaSiamRPN::getTrackingScore()
        //

        /// <summary>
        ///  Return tracking score
        /// </summary>
        public float getTrackingScore()
        {
            ThrowIfDisposed();

            return video_TrackerDaSiamRPN_getTrackingScore_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerDaSiamRPN cv::TrackerDaSiamRPN::create(TrackerDaSiamRPN_Params parameters = TrackerDaSiamRPN::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerDaSiamRPN_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerDaSiamRPN_create_11();

        // C++: static Ptr_TrackerDaSiamRPN cv::TrackerDaSiamRPN::create(Net siam_rpn, Net kernel_cls1, Net kernel_r1)
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerDaSiamRPN_create_12(IntPtr siam_rpn_nativeObj, IntPtr kernel_cls1_nativeObj, IntPtr kernel_r1_nativeObj);

        // C++:  float cv::TrackerDaSiamRPN::getTrackingScore()
        [DllImport(LIBNAME)]
        private static extern float video_TrackerDaSiamRPN_getTrackingScore_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_TrackerDaSiamRPN_delete(IntPtr nativeObj);

    }
}
