#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    // C++: class Model
    /// <summary>
    ///  This class is presented high-level API for neural networks.
    /// </summary>
    /// <remarks>
    ///          Model allows to set params for preprocessing input image.
    ///          Model creates net from file with trained weights and config,
    ///          sets preprocessing input and runs forward pass.
    /// </remarks>
    public partial class Model : DisposableOpenCVObject
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
                        dnn_Model_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Model(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static Model __fromPtr__(IntPtr addr) { return new Model(addr); }

        //
        // C++:   cv::dnn::Model::Model(String model, String config = "")
        //

        /// <summary>
        ///  Create model from deep learning network represented in one of the supported formats.
        ///              An order of @p model and @p config arguments does not matter.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights.
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration.
        /// </param>
        public Model(string model, string config)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Model_Model_10(model, config));


        }

        /// <summary>
        ///  Create model from deep learning network represented in one of the supported formats.
        ///              An order of @p model and @p config arguments does not matter.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights.
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration.
        /// </param>
        public Model(string model)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Model_Model_11(model));


        }


        //
        // C++:   cv::dnn::Model::Model(Net network)
        //

        /// <summary>
        ///  Create model from deep learning network.
        /// </summary>
        /// <param name="network">
        /// Net object.
        /// </param>
        public Model(Net network)
        {
            if (network != null) network.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Model_Model_12(network.getNativeObjAddr()));


        }


        //
        // C++:  Model cv::dnn::Model::setInputSize(Size size)
        //

        /// <summary>
        ///  Set input size for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        ///               @note If shape of the new blob less than 0, then frame size not change.
        /// </param>
        public Model setInputSize(Size size)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputSize_10(nativeObj, size.width, size.height)));


        }


        //
        // C++:  Model cv::dnn::Model::setInputSize(int width, int height)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="width">
        /// New input width.
        /// </param>
        /// <param name="height">
        /// New input height.
        /// </param>
        public Model setInputSize(int width, int height)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputSize_11(nativeObj, width, height)));


        }


        //
        // C++:  Model cv::dnn::Model::setInputMean(Scalar mean)
        //

        /// <summary>
        ///  Set mean value for frame.
        /// </summary>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        public Model setInputMean(Scalar mean)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputMean_10(nativeObj, mean.val[0], mean.val[1], mean.val[2], mean.val[3])));


        }


        //
        // C++:  Model cv::dnn::Model::setInputScale(Scalar scale)
        //

        /// <summary>
        ///  Set scalefactor value for frame.
        /// </summary>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        public Model setInputScale(Scalar scale)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputScale_10(nativeObj, scale.val[0], scale.val[1], scale.val[2], scale.val[3])));


        }


        //
        // C++:  Model cv::dnn::Model::setInputCrop(bool crop)
        //

        /// <summary>
        ///  Set flag crop for frame.
        /// </summary>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        /// </param>
        public Model setInputCrop(bool crop)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputCrop_10(nativeObj, crop)));


        }


        //
        // C++:  Model cv::dnn::Model::setInputSwapRB(bool swapRB)
        //

        /// <summary>
        ///  Set flag swapRB for frame.
        /// </summary>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        public Model setInputSwapRB(bool swapRB)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputSwapRB_10(nativeObj, swapRB)));


        }


        //
        // C++:  Model cv::dnn::Model::setOutputNames(vector_String outNames)
        //

        /// <summary>
        ///  Set output names for frame.
        /// </summary>
        /// <param name="outNames">
        /// Names for output layers.
        /// </param>
        public Model setOutputNames(List<string> outNames)
        {
            ThrowIfDisposed();
            using Mat outNames_mat = Converters.vector_String_to_Mat(outNames);
            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setOutputNames_10(nativeObj, outNames_mat.nativeObj)));


        }


        //
        // C++:  void cv::dnn::Model::setInputParams(double scale = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false)
        //

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, Size size, Scalar mean, bool swapRB, bool crop)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_10(nativeObj, scale, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB, crop);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, Size size, Scalar mean, bool swapRB)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_11(nativeObj, scale, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, Size size, Scalar mean)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_12(nativeObj, scale, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3]);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, Size size)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_13(nativeObj, scale, size.width, size.height);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_14(nativeObj, scale);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams()
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_15(nativeObj);


        }


        //
        // C++:  void cv::dnn::Model::predict(Mat frame, vector_Mat& outs)
        //

        /// <summary>
        ///  Given the @p input frame, create input blob, run net and return the output @p blobs.
        /// </summary>
        /// <param name="frame">
        /// The input image.
        /// </param>
        /// <param name="outs">
        /// Allocated output blobs, which will store results of the computation.
        /// </param>
        public void predict(Mat frame, List<Mat> outs)
        {
            ThrowIfDisposed();
            if (frame != null) frame.ThrowIfDisposed();
            using Mat outs_mat = new Mat();
            dnn_Model_predict_10(nativeObj, frame.nativeObj, outs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(outs_mat, outs);

        }


        //
        // C++:  Model cv::dnn::Model::setPreferableBackend(dnn_Backend backendId)
        //

        public Model setPreferableBackend(int backendId)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setPreferableBackend_10(nativeObj, backendId)));


        }


        //
        // C++:  Model cv::dnn::Model::setPreferableTarget(dnn_Target targetId)
        //

        public Model setPreferableTarget(int targetId)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setPreferableTarget_10(nativeObj, targetId)));


        }


        //
        // C++:  Model cv::dnn::Model::enableWinograd(bool useWinograd)
        //

        public Model enableWinograd(bool useWinograd)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_enableWinograd_10(nativeObj, useWinograd)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::dnn::Model::Model(String model, String config = "")
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_Model_10(string model, string config);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_Model_11(string model);

        // C++:   cv::dnn::Model::Model(Net network)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_Model_12(IntPtr network_nativeObj);

        // C++:  Model cv::dnn::Model::setInputSize(Size size)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputSize_10(IntPtr nativeObj, double size_width, double size_height);

        // C++:  Model cv::dnn::Model::setInputSize(int width, int height)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputSize_11(IntPtr nativeObj, int width, int height);

        // C++:  Model cv::dnn::Model::setInputMean(Scalar mean)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputMean_10(IntPtr nativeObj, double mean_val0, double mean_val1, double mean_val2, double mean_val3);

        // C++:  Model cv::dnn::Model::setInputScale(Scalar scale)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputScale_10(IntPtr nativeObj, double scale_val0, double scale_val1, double scale_val2, double scale_val3);

        // C++:  Model cv::dnn::Model::setInputCrop(bool crop)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputCrop_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool crop);

        // C++:  Model cv::dnn::Model::setInputSwapRB(bool swapRB)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setInputSwapRB_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool swapRB);

        // C++:  Model cv::dnn::Model::setOutputNames(vector_String outNames)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setOutputNames_10(IntPtr nativeObj, IntPtr outNames_mat_nativeObj);

        // C++:  void cv::dnn::Model::setInputParams(double scale = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false)
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_10(IntPtr nativeObj, double scale, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB, [MarshalAs(UnmanagedType.U1)] bool crop);
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_11(IntPtr nativeObj, double scale, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB);
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_12(IntPtr nativeObj, double scale, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3);
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_13(IntPtr nativeObj, double scale, double size_width, double size_height);
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_14(IntPtr nativeObj, double scale);
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_setInputParams_15(IntPtr nativeObj);

        // C++:  void cv::dnn::Model::predict(Mat frame, vector_Mat& outs)
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_predict_10(IntPtr nativeObj, IntPtr frame_nativeObj, IntPtr outs_mat_nativeObj);

        // C++:  Model cv::dnn::Model::setPreferableBackend(dnn_Backend backendId)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setPreferableBackend_10(IntPtr nativeObj, int backendId);

        // C++:  Model cv::dnn::Model::setPreferableTarget(dnn_Target targetId)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_setPreferableTarget_10(IntPtr nativeObj, int targetId);

        // C++:  Model cv::dnn::Model::enableWinograd(bool useWinograd)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Model_enableWinograd_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool useWinograd);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void dnn_Model_delete(IntPtr nativeObj);

    }
}


#endif