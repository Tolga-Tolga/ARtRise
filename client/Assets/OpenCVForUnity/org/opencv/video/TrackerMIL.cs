
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

    // C++: class TrackerMIL
    /// <summary>
    ///  The MIL algorithm trains a classifier in an online manner to separate the object from the
    ///  background.
    /// </summary>
    /// <remarks>
    ///  Multiple Instance Learning avoids the drift problem for a robust tracking. The implementation is
    ///  based on @cite MIL .
    ///  
    ///  Original code can be found here &lt;http://vision.ucsd.edu/~bbabenko/project_miltrack.shtml&gt;
    /// </remarks>
    public class TrackerMIL : Tracker
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
                        video_TrackerMIL_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerMIL(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerMIL __fromPtr__(IntPtr addr) { return new TrackerMIL(addr); }

        //
        // C++: static Ptr_TrackerMIL cv::TrackerMIL::create(TrackerMIL_Params parameters = TrackerMIL::Params())
        //

        /// <summary>
        ///  Create MIL tracker instance
        /// </summary>
        /// <param name="parameters">
        /// MIL parameters TrackerMIL::Params
        /// </param>
        public static TrackerMIL create(TrackerMIL_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerMIL.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerMIL_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Create MIL tracker instance
        /// </summary>
        /// <param name="parameters">
        /// MIL parameters TrackerMIL::Params
        /// </param>
        public static TrackerMIL create()
        {


            return TrackerMIL.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerMIL_create_11()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerMIL cv::TrackerMIL::create(TrackerMIL_Params parameters = TrackerMIL::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerMIL_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerMIL_create_11();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_TrackerMIL_delete(IntPtr nativeObj);

    }
}
