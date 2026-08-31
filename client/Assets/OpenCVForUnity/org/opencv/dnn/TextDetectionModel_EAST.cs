#if !UNITY_WSA_10_0


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{

    // C++: class TextDetectionModel_EAST
    /// <summary>
    ///  This class represents high-level API for text detection DL networks compatible with EAST model.
    /// </summary>
    /// <remarks>
    ///     Configurable parameters:
    ///     - (float) confThreshold - used to filter boxes by confidences, default: 0.5f
    ///     - (float) nmsThreshold - used in non maximum suppression, default: 0.0f
    /// </remarks>
    public class TextDetectionModel_EAST : TextDetectionModel
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
                        dnn_TextDetectionModel_1EAST_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TextDetectionModel_EAST(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TextDetectionModel_EAST __fromPtr__(IntPtr addr) { return new TextDetectionModel_EAST(addr); }

        //
        // C++:   cv::dnn::TextDetectionModel_EAST::TextDetectionModel_EAST(Net network)
        //

        /// <summary>
        ///  Create text detection algorithm from deep learning network
        /// </summary>
        /// <param name="network">
        /// Net object
        /// </param>
        public TextDetectionModel_EAST(Net network) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_10(network.getNativeObjAddr())))
        {



        }


        //
        // C++:   cv::dnn::TextDetectionModel_EAST::TextDetectionModel_EAST(string model, string config = "")
        //

        /// <summary>
        ///  Create text detection model from network represented in one of the supported formats.
        ///         An order of @p model and @p config arguments does not matter.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights.
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration.
        /// </param>
        public TextDetectionModel_EAST(string model, string config) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_11(model, config)))
        {



        }

        /// <summary>
        ///  Create text detection model from network represented in one of the supported formats.
        ///         An order of @p model and @p config arguments does not matter.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights.
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration.
        /// </param>
        public TextDetectionModel_EAST(string model) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_12(model)))
        {



        }


        //
        // C++:  TextDetectionModel_EAST cv::dnn::TextDetectionModel_EAST::setConfidenceThreshold(float confThreshold)
        //

        /// <summary>
        ///  Set the detection confidence threshold
        /// </summary>
        /// <param name="confThreshold">
        /// A threshold used to filter boxes by confidences
        /// </param>
        public TextDetectionModel_EAST setConfidenceThreshold(float confThreshold)
        {
            ThrowIfDisposed();

            return new TextDetectionModel_EAST(DisposableObject.ThrowIfNullIntPtr(dnn_TextDetectionModel_1EAST_setConfidenceThreshold_10(nativeObj, confThreshold)));


        }


        //
        // C++:  float cv::dnn::TextDetectionModel_EAST::getConfidenceThreshold()
        //

        /// <summary>
        ///  Get the detection confidence threshold
        /// </summary>
        public float getConfidenceThreshold()
        {
            ThrowIfDisposed();

            return dnn_TextDetectionModel_1EAST_getConfidenceThreshold_10(nativeObj);


        }


        //
        // C++:  TextDetectionModel_EAST cv::dnn::TextDetectionModel_EAST::setNMSThreshold(float nmsThreshold)
        //

        /// <summary>
        ///  Set the detection NMS filter threshold
        /// </summary>
        /// <param name="nmsThreshold">
        /// A threshold used in non maximum suppression
        /// </param>
        public TextDetectionModel_EAST setNMSThreshold(float nmsThreshold)
        {
            ThrowIfDisposed();

            return new TextDetectionModel_EAST(DisposableObject.ThrowIfNullIntPtr(dnn_TextDetectionModel_1EAST_setNMSThreshold_10(nativeObj, nmsThreshold)));


        }


        //
        // C++:  float cv::dnn::TextDetectionModel_EAST::getNMSThreshold()
        //

        /// <summary>
        ///  Get the detection confidence threshold
        /// </summary>
        public float getNMSThreshold()
        {
            ThrowIfDisposed();

            return dnn_TextDetectionModel_1EAST_getNMSThreshold_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::dnn::TextDetectionModel_EAST::TextDetectionModel_EAST(Net network)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_10(IntPtr network_nativeObj);

        // C++:   cv::dnn::TextDetectionModel_EAST::TextDetectionModel_EAST(string model, string config = "")
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_11(string model, string config);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextDetectionModel_1EAST_TextDetectionModel_1EAST_12(string model);

        // C++:  TextDetectionModel_EAST cv::dnn::TextDetectionModel_EAST::setConfidenceThreshold(float confThreshold)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextDetectionModel_1EAST_setConfidenceThreshold_10(IntPtr nativeObj, float confThreshold);

        // C++:  float cv::dnn::TextDetectionModel_EAST::getConfidenceThreshold()
        [DllImport(LIBNAME)]
        private static extern float dnn_TextDetectionModel_1EAST_getConfidenceThreshold_10(IntPtr nativeObj);

        // C++:  TextDetectionModel_EAST cv::dnn::TextDetectionModel_EAST::setNMSThreshold(float nmsThreshold)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextDetectionModel_1EAST_setNMSThreshold_10(IntPtr nativeObj, float nmsThreshold);

        // C++:  float cv::dnn::TextDetectionModel_EAST::getNMSThreshold()
        [DllImport(LIBNAME)]
        private static extern float dnn_TextDetectionModel_1EAST_getNMSThreshold_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void dnn_TextDetectionModel_1EAST_delete(IntPtr nativeObj);

    }
}


#endif