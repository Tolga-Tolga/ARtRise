
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
    public partial class TrackerVit : Tracker
    {

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
        public static TrackerVit create(Net model, in (double v0, double v1, double v2, double v3) meanvalue, in (double v0, double v1, double v2, double v3) stdvalue, float tracking_score_threshold)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_12(model.getNativeObjAddr(), meanvalue.v0, meanvalue.v1, meanvalue.v2, meanvalue.v3, stdvalue.v0, stdvalue.v1, stdvalue.v2, stdvalue.v3, tracking_score_threshold)));


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
        public static TrackerVit create(Net model, in (double v0, double v1, double v2, double v3) meanvalue, in (double v0, double v1, double v2, double v3) stdvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_13(model.getNativeObjAddr(), meanvalue.v0, meanvalue.v1, meanvalue.v2, meanvalue.v3, stdvalue.v0, stdvalue.v1, stdvalue.v2, stdvalue.v3)));


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
        public static TrackerVit create(Net model, in (double v0, double v1, double v2, double v3) meanvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_14(model.getNativeObjAddr(), meanvalue.v0, meanvalue.v1, meanvalue.v2, meanvalue.v3)));


        }

#endif


    }
}
