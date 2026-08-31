
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

    // C++: class TrackerVit
    /// <summary>
    ///  the VIT tracker is a super lightweight dnn-based general object tracking.
    /// </summary>
    /// <remarks>
    ///      VIT tracker is much faster and extremely lightweight due to special model structure, the model file is about 767KB.
    ///      Model download link: https://github.com/opencv/opencv_zoo/tree/main/models/object_tracking_vittrack
    ///      Author: PengyuLiu, 1872918507@qq.com
    /// </remarks>
    public partial class TrackerVit : Tracker
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
                        video_TrackerVit_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerVit(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerVit __fromPtr__(IntPtr addr) { return new TrackerVit(addr); }

        //
        // C++: static Ptr_TrackerVit cv::TrackerVit::create(TrackerVit_Params parameters = TrackerVit::Params())
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// vit tracker parameters TrackerVit::Params
        /// </param>
        public static TrackerVit create(TrackerVit_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// vit tracker parameters TrackerVit::Params
        /// </param>
        public static TrackerVit create()
        {


            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_11()));


        }

#if !UNITY_WSA_10_0


        //
        // C++: static Ptr_TrackerVit cv::TrackerVit::create(Net model, Scalar meanvalue = Scalar(0.485, 0.456, 0.406), Scalar stdvalue = Scalar(0.229, 0.224, 0.225), float tracking_score_threshold = 0.20f)
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="model">
        /// pre-loaded DNN model
        /// </param>
        /// <param name="meanvalue">
        /// mean value for image preprocessing
        /// </param>
        /// <param name="stdvalue">
        /// std value for image preprocessing
        /// </param>
        /// <param name="tracking_score_threshold">
        /// threshold for tracking score
        /// </param>
        public static TrackerVit create(Net model, Scalar meanvalue, Scalar stdvalue, float tracking_score_threshold)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_12(model.getNativeObjAddr(), meanvalue.val[0], meanvalue.val[1], meanvalue.val[2], meanvalue.val[3], stdvalue.val[0], stdvalue.val[1], stdvalue.val[2], stdvalue.val[3], tracking_score_threshold)));


        }

#endif


#if !UNITY_WSA_10_0

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="model">
        /// pre-loaded DNN model
        /// </param>
        /// <param name="meanvalue">
        /// mean value for image preprocessing
        /// </param>
        /// <param name="stdvalue">
        /// std value for image preprocessing
        /// </param>
        /// <param name="tracking_score_threshold">
        /// threshold for tracking score
        /// </param>
        public static TrackerVit create(Net model, Scalar meanvalue, Scalar stdvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_13(model.getNativeObjAddr(), meanvalue.val[0], meanvalue.val[1], meanvalue.val[2], meanvalue.val[3], stdvalue.val[0], stdvalue.val[1], stdvalue.val[2], stdvalue.val[3])));


        }

#endif


#if !UNITY_WSA_10_0

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="model">
        /// pre-loaded DNN model
        /// </param>
        /// <param name="meanvalue">
        /// mean value for image preprocessing
        /// </param>
        /// <param name="stdvalue">
        /// std value for image preprocessing
        /// </param>
        /// <param name="tracking_score_threshold">
        /// threshold for tracking score
        /// </param>
        public static TrackerVit create(Net model, Scalar meanvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_14(model.getNativeObjAddr(), meanvalue.val[0], meanvalue.val[1], meanvalue.val[2], meanvalue.val[3])));


        }

#endif


#if !UNITY_WSA_10_0

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="model">
        /// pre-loaded DNN model
        /// </param>
        /// <param name="meanvalue">
        /// mean value for image preprocessing
        /// </param>
        /// <param name="stdvalue">
        /// std value for image preprocessing
        /// </param>
        /// <param name="tracking_score_threshold">
        /// threshold for tracking score
        /// </param>
        public static TrackerVit create(Net model)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_15(model.getNativeObjAddr())));


        }

#endif



        //
        // C++:  float cv::TrackerVit::getTrackingScore()
        //

        /// <summary>
        ///  Return tracking score
        /// </summary>
        public float getTrackingScore()
        {
            ThrowIfDisposed();

            return video_TrackerVit_getTrackingScore_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerVit cv::TrackerVit::create(TrackerVit_Params parameters = TrackerVit::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_11();

        // C++: static Ptr_TrackerVit cv::TrackerVit::create(Net model, Scalar meanvalue = Scalar(0.485, 0.456, 0.406), Scalar stdvalue = Scalar(0.229, 0.224, 0.225), float tracking_score_threshold = 0.20f)
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_12(IntPtr model_nativeObj, double meanvalue_val0, double meanvalue_val1, double meanvalue_val2, double meanvalue_val3, double stdvalue_val0, double stdvalue_val1, double stdvalue_val2, double stdvalue_val3, float tracking_score_threshold);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_13(IntPtr model_nativeObj, double meanvalue_val0, double meanvalue_val1, double meanvalue_val2, double meanvalue_val3, double stdvalue_val0, double stdvalue_val1, double stdvalue_val2, double stdvalue_val3);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_14(IntPtr model_nativeObj, double meanvalue_val0, double meanvalue_val1, double meanvalue_val2, double meanvalue_val3);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerVit_create_15(IntPtr model_nativeObj);

        // C++:  float cv::TrackerVit::getTrackingScore()
        [DllImport(LIBNAME)]
        private static extern float video_TrackerVit_getTrackingScore_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_TrackerVit_delete(IntPtr nativeObj);

    }
}
