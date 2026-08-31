
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

    // C++: class TrackerGOTURN
    /// <summary>
    ///  the GOTURN (Generic Object Tracking Using Regression Networks) tracker
    /// </summary>
    /// <remarks>
    ///      GOTURN (@cite GOTURN) is kind of trackers based on Convolutional Neural Networks (CNN). While taking all advantages of CNN trackers,
    ///      GOTURN is much faster due to offline training without online fine-tuning nature.
    ///      GOTURN tracker addresses the problem of single target tracking: given a bounding box label of an object in the first frame of the video,
    ///      we track that object through the rest of the video. NOTE: Current method of GOTURN does not handle occlusions; however, it is fairly
    ///      robust to viewpoint changes, lighting changes, and deformations.
    ///      Inputs of GOTURN are two RGB patches representing Target and Search patches resized to 227x227.
    ///      Outputs of GOTURN are predicted bounding box coordinates, relative to Search patch coordinate system, in format X1,Y1,X2,Y2.
    ///      Original paper is here: &lt;http://davheld.github.io/GOTURN/GOTURN.pdf&gt;
    ///      As long as original authors implementation: &lt;https://github.com/davheld/GOTURN#train-the-tracker&gt;
    ///      Implementation of training algorithm is placed in separately here due to 3d-party dependencies:
    ///      &lt;https://github.com/Auron-X/GOTURN_Training_Toolkit&gt;
    ///      GOTURN architecture goturn.prototxt and trained model goturn.caffemodel are accessible on opencv_extra GitHub repository.
    /// </remarks>
    public class TrackerGOTURN : Tracker
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
                        video_TrackerGOTURN_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerGOTURN(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerGOTURN __fromPtr__(IntPtr addr) { return new TrackerGOTURN(addr); }

        //
        // C++: static Ptr_TrackerGOTURN cv::TrackerGOTURN::create(TrackerGOTURN_Params parameters = TrackerGOTURN::Params())
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// GOTURN parameters TrackerGOTURN::Params
        /// </param>
        public static TrackerGOTURN create(TrackerGOTURN_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerGOTURN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerGOTURN_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="parameters">
        /// GOTURN parameters TrackerGOTURN::Params
        /// </param>
        public static TrackerGOTURN create()
        {


            return TrackerGOTURN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerGOTURN_create_11()));


        }

#if !UNITY_WSA_10_0


        //
        // C++: static Ptr_TrackerGOTURN cv::TrackerGOTURN::create(Net model)
        //

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="model">
        /// pre-loaded GOTURN model
        /// </param>
        public static TrackerGOTURN create(Net model)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerGOTURN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerGOTURN_create_12(model.getNativeObjAddr())));


        }

#endif



#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerGOTURN cv::TrackerGOTURN::create(TrackerGOTURN_Params parameters = TrackerGOTURN::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerGOTURN_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerGOTURN_create_11();

        // C++: static Ptr_TrackerGOTURN cv::TrackerGOTURN::create(Net model)
        [DllImport(LIBNAME)]
        private static extern IntPtr video_TrackerGOTURN_create_12(IntPtr model_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_TrackerGOTURN_delete(IntPtr nativeObj);

    }
}
