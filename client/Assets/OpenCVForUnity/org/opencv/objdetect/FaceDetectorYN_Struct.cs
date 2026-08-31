

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class FaceDetectorYN : DisposableOpenCVObject
    {

        //
        // C++:  void cv::FaceDetectorYN::setInputSize(Size input_size)
        //

        /// <summary>
        ///  Set the size for the network input, which overwrites the input size of creating model. Call this method when the size of input image does not match the input size when creating model
        /// </summary>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        public void setInputSize(in Vec2d input_size)
        {
            ThrowIfDisposed();

            objdetect_FaceDetectorYN_setInputSize_10(nativeObj, input_size.Item1, input_size.Item2);


        }


        //
        // C++:  Size cv::FaceDetectorYN::getInputSize()
        //

        public Vec2d getInputSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_FaceDetectorYN_getInputSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: static Ptr_FaceDetectorYN cv::FaceDetectorYN::create(String model, String config, Size input_size, float score_threshold = 0.9f, float nms_threshold = 0.3f, int top_k = 5000, int backend_id = 0, int target_id = 0)
        //

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k, int backend_id, int target_id)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_10(model, config, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k, backend_id, target_id)));


        }

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k, int backend_id)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_11(model, config, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k, backend_id)));


        }

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_12(model, config, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k)));


        }

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size, float score_threshold, float nms_threshold)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_13(model, config, input_size.Item1, input_size.Item2, score_threshold, nms_threshold)));


        }

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size, float score_threshold)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_14(model, config, input_size.Item1, input_size.Item2, score_threshold)));


        }

        /// <summary>
        ///  Creates an instance of face detector class with given parameters
        /// </summary>
        /// <param name="model">
        /// the path to the requested model
        /// </param>
        /// <param name="config">
        /// the path to the config file for compability, which is not requested for ONNX models
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string model, string config, in Vec2d input_size)
        {


            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_15(model, config, input_size.Item1, input_size.Item2)));


        }


        //
        // C++: static Ptr_FaceDetectorYN cv::FaceDetectorYN::create(String framework, vector_uchar bufferModel, vector_uchar bufferConfig, Size input_size, float score_threshold = 0.9f, float nms_threshold = 0.3f, int top_k = 5000, int backend_id = 0, int target_id = 0)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k, int backend_id, int target_id)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_16(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k, backend_id, target_id)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k, int backend_id)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_17(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k, backend_id)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size, float score_threshold, float nms_threshold, int top_k)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_18(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2, score_threshold, nms_threshold, top_k)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size, float score_threshold, float nms_threshold)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_19(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2, score_threshold, nms_threshold)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size, float score_threshold)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_110(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2, score_threshold)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="framework">
        /// Name of origin framework
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration
        /// </param>
        /// <param name="input_size">
        /// the size of the input image
        /// </param>
        /// <param name="score_threshold">
        /// the threshold to filter out bounding boxes of score smaller than the given value
        /// </param>
        /// <param name="nms_threshold">
        /// the threshold to suppress bounding boxes of IoU bigger than the given value
        /// </param>
        /// <param name="top_k">
        /// keep top K bboxes before NMS
        /// </param>
        /// <param name="backend_id">
        /// the id of backend
        /// </param>
        /// <param name="target_id">
        /// the id of target device
        /// </param>
        public static FaceDetectorYN create(string framework, MatOfByte bufferModel, MatOfByte bufferConfig, in Vec2d input_size)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return FaceDetectorYN.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(objdetect_FaceDetectorYN_create_111(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj, input_size.Item1, input_size.Item2)));


        }

    }
}
