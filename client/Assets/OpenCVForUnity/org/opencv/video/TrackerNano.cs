
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

    // C++: class TrackerNano
    /// <summary>
    ///  the Nano tracker is a super lightweight dnn-based general object tracking.
    /// </summary>
    /// <remarks>
    ///      Nano tracker is much faster and extremely lightweight due to special model structure, the whole model size is about 1.9 MB.
    ///      Nano tracker needs two models: one for feature extraction (backbone) and the another for localization (neckhead).
    ///      Model download link: https://github.com/HonglinChu/SiamTrackers/tree/master/NanoTrack/models/nanotrackv2
    ///      Original repo is here: https://github.com/HonglinChu/NanoTrack
    ///      Author: HongLinChu, 1628464345@qq.com
    /// </remarks>
    public class TrackerNano : Tracker
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
                        video_TrackerNano_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerNano(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerNano __fromPtr__(IntPtr addr) { return new TrackerNano(addr); }

        //
        // C++: static Ptr_TrackerNano cv::TrackerNano::create(TrackerNano_Params parameters = TrackerNano::Params())
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// NanoTrack parameters TrackerNano::Params
        /// </param>
        public static TrackerNano create(TrackerNano_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerNano.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerNano_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// NanoTrack parameters TrackerNano::Params
        /// </param>
        public static TrackerNano create()
        {


            return TrackerNano.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerNano_create_11()));


        }

#if !UNITY_WSA_10_0


        //
        // C++: static Ptr_TrackerNano cv::TrackerNano::create(Net backbone, Net neckhead)
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="backbone">
        /// pre-loaded backbone model
        /// </param>
        /// <param name="neckhead">
        /// pre-loaded neckhead model
        /// </param>
        public static TrackerNano create(Net backbone, Net neckhead)
        {
            if (backbone != null) backbone.ThrowIfDisposed();
            if (neckhead != null) neckhead.ThrowIfDisposed();

            return TrackerNano.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerNano_create_12(backbone.getNativeObjAddr(), neckhead.getNativeObjAddr())));


        }

#endif



        //
        // C++:  float cv::TrackerNano::getTrackingScore()
        //

        /// <summary>
        ///  Return tracking score
        /// </summary>
        public float getTrackingScore()
        {
            ThrowIfDisposed();

            return video_TrackerNano_getTrackingScore_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerNano cv::TrackerNano::create(TrackerNano_Params parameters = TrackerNano::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerNano_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerNano_create_11();

        // C++: static Ptr_TrackerNano cv::TrackerNano::create(Net backbone, Net neckhead)
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerNano_create_12(IntPtr backbone_nativeObj, IntPtr neckhead_nativeObj);

        // C++:  float cv::TrackerNano::getTrackingScore()
        [DllImport(LIBNAME)]
        private static extern float video_TrackerNano_getTrackingScore_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_TrackerNano_delete(IntPtr nativeObj);

    }
}
