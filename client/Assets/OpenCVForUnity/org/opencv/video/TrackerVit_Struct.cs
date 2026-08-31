
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UnityIntegration;
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
        public static TrackerVit create(Net model, in Vec4d meanvalue, in Vec4d stdvalue, float tracking_score_threshold)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_12(model.getNativeObjAddr(), meanvalue.Item1, meanvalue.Item2, meanvalue.Item3, meanvalue.Item4, stdvalue.Item1, stdvalue.Item2, stdvalue.Item3, stdvalue.Item4, tracking_score_threshold)));


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
        public static TrackerVit create(Net model, in Vec4d meanvalue, in Vec4d stdvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_13(model.getNativeObjAddr(), meanvalue.Item1, meanvalue.Item2, meanvalue.Item3, meanvalue.Item4, stdvalue.Item1, stdvalue.Item2, stdvalue.Item3, stdvalue.Item4)));


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
        public static TrackerVit create(Net model, in Vec4d meanvalue)
        {
            if (model != null) model.ThrowIfDisposed();

            return TrackerVit.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_TrackerVit_create_14(model.getNativeObjAddr(), meanvalue.Item1, meanvalue.Item2, meanvalue.Item3, meanvalue.Item4)));


        }

#endif


    }
}
