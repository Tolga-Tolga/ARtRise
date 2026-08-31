

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    // C++: class FaceRecognizerSF
    /// <summary>
    ///  DNN-based face recognizer
    /// </summary>
    /// <remarks>
    ///  model download link: https://github.com/opencv/opencv_zoo/tree/master/models/face_recognition_sface
    /// </remarks>
    public class FaceRecognizerSF : DisposableOpenCVObject
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
                        objdetect_FaceRecognizerSF_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal FaceRecognizerSF(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static FaceRecognizerSF __fromPtr__(IntPtr addr) { return new FaceRecognizerSF(addr); }

        /// <summary>
        /// C++: enum DisType (cv.FaceRecognizerSF.DisType)
        /// </summary>
        public const int FR_COSINE = 0;

        /// <summary>
        /// C++: enum DisType (cv.FaceRecognizerSF.DisType)
        /// </summary>
        public const int FR_NORM_L2 = 1;


        //
        // C++:  void cv::FaceRecognizerSF::alignCrop(Mat src_img, Mat face_box, Mat& aligned_img)
        //

        /// <summary>
        ///  Aligns detected face with the source input image and crops it
        /// </summary>
        /// <param name="src_img">
        /// input image
        /// </param>
        /// <param name="face_box">
        /// the detected face result from the input image
        /// </param>
        /// <param name="aligned_img">
        /// output aligned image
        /// </param>
        public void alignCrop(Mat src_img, Mat face_box, Mat aligned_img)
        {
            ThrowIfDisposed();
            if (src_img != null) src_img.ThrowIfDisposed();
            if (face_box != null) face_box.ThrowIfDisposed();
            if (aligned_img != null) aligned_img.ThrowIfDisposed();

            objdetect_FaceRecognizerSF_alignCrop_10(nativeObj, src_img.nativeObj, face_box.nativeObj, aligned_img.nativeObj);


        }


        //
        // C++:  void cv::FaceRecognizerSF::feature(Mat aligned_img, Mat& face_feature)
        //

        /// <summary>
        ///  Extracts face feature from aligned image
        /// </summary>
        /// <param name="aligned_img">
        /// input aligned image
        /// </param>
        /// <param name="face_feature">
        /// output face feature
        /// </param>
        public void feature(Mat aligned_img, Mat face_feature)
        {
            ThrowIfDisposed();
            if (aligned_img != null) aligned_img.ThrowIfDisposed();
            if (face_feature != null) face_feature.ThrowIfDisposed();

            objdetect_FaceRecognizerSF_feature_10(nativeObj, aligned_img.nativeObj, face_feature.nativeObj);


        }


        //
        // C++:  double cv::FaceRecognizerSF::match(Mat face_feature1, Mat face_feature2, int dis_type = FaceRecognizerSF::FR_COSINE)
        //

        /// <summary>
        ///  Calculates the distance between two face features
        /// </summary>
        /// <param name="face_feature1">
        /// the first input feature
        /// </param>
        /// <param name="face_feature2">
        /// the second input feature of the same size and the same type as face_feature1
        /// </param>
        /// <param name="dis_type">
        /// defines how to calculate the distance between two face features with optional values "FR_COSINE" or "FR_NORM_L2"
        /// </param>
        public double match(Mat face_feature1, Mat face_feature2, int dis_type)
        {
            ThrowIfDisposed();
            if (face_feature1 != null) face_feature1.ThrowIfDisposed();
            if (face_feature2 != null) face_feature2.ThrowIfDisposed();

            return objdetect_FaceRecognizerSF_match_10(nativeObj, face_feature1.nativeObj, face_feature2.nativeObj, dis_type);


        }

        /// <summary>
        ///  Calculates the distance between two face features
        /// </summary>
        /// <param name="face_feature1">
        /// the first input feature
        /// </param>
        /// <param name="face_feature2">
        /// the second input feature of the same size and the same type as face_feature1
        /// </param>
        /// <param name="dis_type">
        /// defines how to calculate the distance between two face features with optional values "FR_COSINE" or "FR_NORM_L2"
        /// </param>
        public double match(Mat face_feature1, Mat face_feature2)
        {
            ThrowIfDisposed();
            if (face_feature1 != null) face_feature1.ThrowIfDisposed();
            if (face_feature2 != null) face_feature2.ThrowIfDisposed();

            return objdetect_FaceRecognizerSF_match_11(nativeObj, face_feature1.nativeObj, face_feature2.nativeObj);


        }


        //
        // C++: static Ptr_FaceRecognizerSF cv::FaceRecognizerSF::create(String model, String config, int backend_id = 0, int target_id = 0)
        //

        /// <summary>
        ///  Creates an instance of this class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path of the onnx model used for face recognition
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceRecognizerSF create(string model, string config, int backend_id, int target_id)
        {


            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_10(model, config, backend_id, target_id)));


        }

        /// <summary>
        ///  Creates an instance of this class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path of the onnx model used for face recognition
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceRecognizerSF create(string model, string config, int backend_id)
        {


            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_11(model, config, backend_id)));


        }

        /// <summary>
        ///  Creates an instance of this class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path of the onnx model used for face recognition
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceRecognizerSF create(string model, string config)
        {


            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_12(model, config)));


        }


        //
        // C++: static Ptr_FaceRecognizerSF cv::FaceRecognizerSF::create(String framework, vector_uchar bufferModel, vector_uchar bufferConfig, int backend_id = 0, int target_id = 0)
        //

        /// <summary>
        ///  Creates an instance of this class from a buffer containing the model weights and configuration.
        /// </summary>
        /// <param name="framework">
        /// Name of the framework (ONNX, etc.)
        /// </param>
        /// <param name="bufferModel">
        /// A buffer containing the binary model weights.
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer containing the network configuration.
        /// </param>
        /// <param name="backend_id">
        /// The id of the backend.
        /// </param>
        /// <param name="target_id">
        /// The id of the target device.
        /// </param>
        /// <returns>
        ///  A pointer to the created instance of FaceRecognizerSF.
        /// </returns>
        public static FaceRecognizerSF create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, int backend_id, int target_id)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_13(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, backend_id, target_id)));


        }

        /// <summary>
        ///  Creates an instance of this class from a buffer containing the model weights and configuration.
        /// </summary>
        /// <param name="framework">
        /// Name of the framework (ONNX, etc.)
        /// </param>
        /// <param name="bufferModel">
        /// A buffer containing the binary model weights.
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer containing the network configuration.
        /// </param>
        /// <param name="backend_id">
        /// The id of the backend.
        /// </param>
        /// <param name="target_id">
        /// The id of the target device.
        /// </param>
        /// <returns>
        ///  A pointer to the created instance of FaceRecognizerSF.
        /// </returns>
        public static FaceRecognizerSF create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, int backend_id)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_14(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, backend_id)));


        }

        /// <summary>
        ///  Creates an instance of this class from a buffer containing the model weights and configuration.
        /// </summary>
        /// <param name="framework">
        /// Name of the framework (ONNX, etc.)
        /// </param>
        /// <param name="bufferModel">
        /// A buffer containing the binary model weights.
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer containing the network configuration.
        /// </param>
        /// <param name="backend_id">
        /// The id of the backend.
        /// </param>
        /// <param name="target_id">
        /// The id of the target device.
        /// </param>
        /// <returns>
        ///  A pointer to the created instance of FaceRecognizerSF.
        /// </returns>
        public static FaceRecognizerSF create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceRecognizerSF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceRecognizerSF_create_15(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::FaceRecognizerSF::alignCrop(Mat src_img, Mat face_box, Mat& aligned_img)
        [DllImport(LIBNAME)]
        private static extern void objdetect_FaceRecognizerSF_alignCrop_10(IntPtr nativeObj, IntPtr src_img_nativeObj, IntPtr face_box_nativeObj, IntPtr aligned_img_nativeObj);

        // C++:  void cv::FaceRecognizerSF::feature(Mat aligned_img, Mat& face_feature)
        [DllImport(LIBNAME)]
        private static extern void objdetect_FaceRecognizerSF_feature_10(IntPtr nativeObj, IntPtr aligned_img_nativeObj, IntPtr face_feature_nativeObj);

        // C++:  double cv::FaceRecognizerSF::match(Mat face_feature1, Mat face_feature2, int dis_type = FaceRecognizerSF::FR_COSINE)
        [DllImport(LIBNAME)]
        private static extern double objdetect_FaceRecognizerSF_match_10(IntPtr nativeObj, IntPtr face_feature1_nativeObj, IntPtr face_feature2_nativeObj, int dis_type);
        [DllImport(LIBNAME)]
        private static extern double objdetect_FaceRecognizerSF_match_11(IntPtr nativeObj, IntPtr face_feature1_nativeObj, IntPtr face_feature2_nativeObj);

        // C++: static Ptr_FaceRecognizerSF cv::FaceRecognizerSF::create(String model, String config, int backend_id = 0, int target_id = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_10(string model, string config, int backend_id, int target_id);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_11(string model, string config, int backend_id);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_12(string model, string config);

        // C++: static Ptr_FaceRecognizerSF cv::FaceRecognizerSF::create(String framework, vector_uchar bufferModel, vector_uchar bufferConfig, int backend_id = 0, int target_id = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_13(string framework, IntPtr bufferModel_mat_nativeObj, IntPtr bufferConfig_mat_nativeObj, int backend_id, int target_id);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_14(string framework, IntPtr bufferModel_mat_nativeObj, IntPtr bufferConfig_mat_nativeObj, int backend_id);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_FaceRecognizerSF_create_15(string framework, IntPtr bufferModel_mat_nativeObj, IntPtr bufferConfig_mat_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void objdetect_FaceRecognizerSF_delete(IntPtr nativeObj);

    }
}
