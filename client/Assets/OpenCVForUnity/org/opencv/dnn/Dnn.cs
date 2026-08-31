#if !UNITY_WSA_10_0


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    // C++: class Dnn


    public partial class Dnn
    {

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_DEFAULT = 0;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_HALIDE = 0 + 1;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_INFERENCE_ENGINE = 0 + 2;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_OPENCV = 0 + 3;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_VKCOM = 0 + 4;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_CUDA = 0 + 5;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_WEBNN = 0 + 6;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_TIMVX = 0 + 7;

        /// <summary>
        /// C++: enum Backend (cv.dnn.Backend)
        /// </summary>
        public const int DNN_BACKEND_CANN = 0 + 8;


        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_UNKNOWN = 0;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_ND = 1;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_NCHW = 2;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_NCDHW = 3;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_NHWC = 4;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_NDHWC = 5;

        /// <summary>
        /// C++: enum DataLayout (cv.dnn.DataLayout)
        /// </summary>
        public const int DNN_LAYOUT_PLANAR = 6;


        /// <summary>
        /// C++: enum ImagePaddingMode (cv.dnn.ImagePaddingMode)
        /// </summary>
        public const int DNN_PMODE_NULL = 0;

        /// <summary>
        /// C++: enum ImagePaddingMode (cv.dnn.ImagePaddingMode)
        /// </summary>
        public const int DNN_PMODE_CROP_CENTER = 1;

        /// <summary>
        /// C++: enum ImagePaddingMode (cv.dnn.ImagePaddingMode)
        /// </summary>
        public const int DNN_PMODE_LETTERBOX = 2;


        /// <summary>
        /// C++: enum SoftNMSMethod (cv.dnn.SoftNMSMethod)
        /// </summary>
        public const int SoftNMSMethod_SOFTNMS_LINEAR = 1;

        /// <summary>
        /// C++: enum SoftNMSMethod (cv.dnn.SoftNMSMethod)
        /// </summary>
        public const int SoftNMSMethod_SOFTNMS_GAUSSIAN = 2;


        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_CPU = 0;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_OPENCL = 0 + 1;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_OPENCL_FP16 = 0 + 2;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_MYRIAD = 0 + 3;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_VULKAN = 0 + 4;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_FPGA = 0 + 5;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_CUDA = 0 + 6;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_CUDA_FP16 = 0 + 7;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_HDDL = 0 + 8;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_NPU = 0 + 9;

        /// <summary>
        /// C++: enum Target (cv.dnn.Target)
        /// </summary>
        public const int DNN_TARGET_CPU_FP16 = 0 + 10;


        //
        // C++:  vector_Target cv::dnn::getAvailableTargets(dnn_Backend be)
        //

        public static List<int> getAvailableTargets(int be)
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_getAvailableTargets_10(be)));
            Converters.Mat_to_vector_Target(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  Net cv::dnn::readNetFromDarknet(String cfgFile, String darknetModel = String())
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://pjreddie.com/darknet/">Darknet</a> model files.
        /// </summary>
        /// <param name="cfgFile">
        /// path to the .cfg file with text description of the network architecture.
        /// </param>
        /// <param name="darknetModel">
        /// path to the .weights file with learned network.
        /// </param>
        /// <returns>
        ///  Network object that ready to do forward, throw an exception in failure cases.
        /// </returns>
        public static Net readNetFromDarknet(string cfgFile, string darknetModel)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromDarknet_10(cfgFile, darknetModel)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="https://pjreddie.com/darknet/">Darknet</a> model files.
        /// </summary>
        /// <param name="cfgFile">
        /// path to the .cfg file with text description of the network architecture.
        /// </param>
        /// <param name="darknetModel">
        /// path to the .weights file with learned network.
        /// </param>
        /// <returns>
        ///  Network object that ready to do forward, throw an exception in failure cases.
        /// </returns>
        public static Net readNetFromDarknet(string cfgFile)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromDarknet_11(cfgFile)));


        }


        //
        // C++:  Net cv::dnn::readNetFromDarknet(vector_uchar bufferCfg, vector_uchar bufferModel = std::vector<uchar>())
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://pjreddie.com/darknet/">Darknet</a> model files.
        /// </summary>
        /// <param name="bufferCfg">
        /// A buffer contains a content of .cfg file with text description of the network architecture.
        /// </param>
        /// <param name="bufferModel">
        /// A buffer contains a content of .weights file with learned network.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromDarknet(MatOfByte bufferCfg, MatOfByte bufferModel)
        {
            if (bufferCfg != null) bufferCfg.ThrowIfDisposed();
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            Mat bufferCfg_mat = bufferCfg;
            Mat bufferModel_mat = bufferModel;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromDarknet_12(bufferCfg_mat.nativeObj, bufferModel_mat.nativeObj)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="https://pjreddie.com/darknet/">Darknet</a> model files.
        /// </summary>
        /// <param name="bufferCfg">
        /// A buffer contains a content of .cfg file with text description of the network architecture.
        /// </param>
        /// <param name="bufferModel">
        /// A buffer contains a content of .weights file with learned network.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromDarknet(MatOfByte bufferCfg)
        {
            if (bufferCfg != null) bufferCfg.ThrowIfDisposed();
            Mat bufferCfg_mat = bufferCfg;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromDarknet_13(bufferCfg_mat.nativeObj)));


        }


        //
        // C++:  Net cv::dnn::readNetFromCaffe(String prototxt, String caffeModel = String())
        //

        /// <summary>
        ///  Reads a network model stored in <a href="http://caffe.berkeleyvision.org">Caffe</a> framework's format.
        /// </summary>
        /// <param name="prototxt">
        /// path to the .prototxt file with text description of the network architecture.
        /// </param>
        /// <param name="caffeModel">
        /// path to the .caffemodel file with learned network.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromCaffe(string prototxt, string caffeModel)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromCaffe_10(prototxt, caffeModel)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="http://caffe.berkeleyvision.org">Caffe</a> framework's format.
        /// </summary>
        /// <param name="prototxt">
        /// path to the .prototxt file with text description of the network architecture.
        /// </param>
        /// <param name="caffeModel">
        /// path to the .caffemodel file with learned network.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromCaffe(string prototxt)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromCaffe_11(prototxt)));


        }


        //
        // C++:  Net cv::dnn::readNetFromCaffe(vector_uchar bufferProto, vector_uchar bufferModel = std::vector<uchar>())
        //

        /// <summary>
        ///  Reads a network model stored in Caffe model in memory.
        /// </summary>
        /// <param name="bufferProto">
        /// buffer containing the content of the .prototxt file
        /// </param>
        /// <param name="bufferModel">
        /// buffer containing the content of the .caffemodel file
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromCaffe(MatOfByte bufferProto, MatOfByte bufferModel)
        {
            if (bufferProto != null) bufferProto.ThrowIfDisposed();
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            Mat bufferProto_mat = bufferProto;
            Mat bufferModel_mat = bufferModel;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromCaffe_12(bufferProto_mat.nativeObj, bufferModel_mat.nativeObj)));


        }

        /// <summary>
        ///  Reads a network model stored in Caffe model in memory.
        /// </summary>
        /// <param name="bufferProto">
        /// buffer containing the content of the .prototxt file
        /// </param>
        /// <param name="bufferModel">
        /// buffer containing the content of the .caffemodel file
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromCaffe(MatOfByte bufferProto)
        {
            if (bufferProto != null) bufferProto.ThrowIfDisposed();
            Mat bufferProto_mat = bufferProto;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromCaffe_13(bufferProto_mat.nativeObj)));


        }


        //
        // C++:  Net cv::dnn::readNetFromTensorflow(String model, String config = String())
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the .pb file with binary protobuf description of the network architecture
        /// </param>
        /// <param name="config">
        /// path to the .pbtxt file that contains text graph definition in protobuf format.
        ///                        Resulting Net object is built by text graph using weights from a binary one that
        ///                        let us make it more flexible.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTensorflow(string model, string config)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTensorflow_10(model, config)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the .pb file with binary protobuf description of the network architecture
        /// </param>
        /// <param name="config">
        /// path to the .pbtxt file that contains text graph definition in protobuf format.
        ///                        Resulting Net object is built by text graph using weights from a binary one that
        ///                        let us make it more flexible.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTensorflow(string model)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTensorflow_11(model)));


        }


        //
        // C++:  Net cv::dnn::readNetFromTensorflow(vector_uchar bufferModel, vector_uchar bufferConfig = std::vector<uchar>())
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format.
        /// </summary>
        /// <param name="bufferModel">
        /// buffer containing the content of the pb file
        /// </param>
        /// <param name="bufferConfig">
        /// buffer containing the content of the pbtxt file
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTensorflow(MatOfByte bufferModel, MatOfByte bufferConfig)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTensorflow_12(bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format.
        /// </summary>
        /// <param name="bufferModel">
        /// buffer containing the content of the pb file
        /// </param>
        /// <param name="bufferConfig">
        /// buffer containing the content of the pbtxt file
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTensorflow(MatOfByte bufferModel)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTensorflow_13(bufferModel_mat.nativeObj)));


        }


        //
        // C++:  Net cv::dnn::readNetFromTFLite(String model)
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/lite">TFLite</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the .tflite file with binary flatbuffers description of the network architecture
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTFLite(string model)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTFLite_10(model)));


        }


        //
        // C++:  Net cv::dnn::readNetFromTFLite(vector_uchar bufferModel)
        //

        /// <summary>
        ///  Reads a network model stored in <a href="https://www.tensorflow.org/lite">TFLite</a> framework's format.
        /// </summary>
        /// <param name="bufferModel">
        /// buffer containing the content of the tflite file
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNetFromTFLite(MatOfByte bufferModel)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTFLite_11(bufferModel_mat.nativeObj)));


        }


        //
        // C++:  Net cv::dnn::readNetFromTorch(String model, bool isBinary = true, bool evaluate = true)
        //

        /// <summary>
        ///  Reads a network model stored in <a href="http://torch.ch">Torch7</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the file, dumped from Torch by using torch.save() function.
        /// </param>
        /// <param name="isBinary">
        /// specifies whether the network was serialized in ascii mode or binary.
        /// </param>
        /// <param name="evaluate">
        /// specifies testing phase of network. If true, it's similar to evaluate() method in Torch.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          @note Ascii mode of Torch serializer is more preferable, because binary mode extensively use `long` type of C language,
        ///          which has various bit-length on different systems.
        ///        
        ///         The loading file must contain serialized <a href="https://github.com/torch/nn/blob/master/doc/module.md">nn.Module</a> object
        ///         with importing network. Try to eliminate a custom objects from serialazing data to avoid importing errors.
        ///        
        ///         List of supported layers (i.e. object instances derived from Torch nn.Module class):
        ///         - nn.Sequential
        ///         - nn.Parallel
        ///         - nn.Concat
        ///         - nn.Linear
        ///         - nn.SpatialConvolution
        ///         - nn.SpatialMaxPooling, nn.SpatialAveragePooling
        ///         - nn.ReLU, nn.TanH, nn.Sigmoid
        ///         - nn.Reshape
        ///         - nn.SoftMax, nn.LogSoftMax
        ///        
        ///         Also some equivalents of these classes from cunn, cudnn, and fbcunn may be successfully imported.
        /// </remarks>
        public static Net readNetFromTorch(string model, bool isBinary, bool evaluate)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTorch_10(model, isBinary, evaluate)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="http://torch.ch">Torch7</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the file, dumped from Torch by using torch.save() function.
        /// </param>
        /// <param name="isBinary">
        /// specifies whether the network was serialized in ascii mode or binary.
        /// </param>
        /// <param name="evaluate">
        /// specifies testing phase of network. If true, it's similar to evaluate() method in Torch.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          @note Ascii mode of Torch serializer is more preferable, because binary mode extensively use `long` type of C language,
        ///          which has various bit-length on different systems.
        ///        
        ///         The loading file must contain serialized <a href="https://github.com/torch/nn/blob/master/doc/module.md">nn.Module</a> object
        ///         with importing network. Try to eliminate a custom objects from serialazing data to avoid importing errors.
        ///        
        ///         List of supported layers (i.e. object instances derived from Torch nn.Module class):
        ///         - nn.Sequential
        ///         - nn.Parallel
        ///         - nn.Concat
        ///         - nn.Linear
        ///         - nn.SpatialConvolution
        ///         - nn.SpatialMaxPooling, nn.SpatialAveragePooling
        ///         - nn.ReLU, nn.TanH, nn.Sigmoid
        ///         - nn.Reshape
        ///         - nn.SoftMax, nn.LogSoftMax
        ///        
        ///         Also some equivalents of these classes from cunn, cudnn, and fbcunn may be successfully imported.
        /// </remarks>
        public static Net readNetFromTorch(string model, bool isBinary)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTorch_11(model, isBinary)));


        }

        /// <summary>
        ///  Reads a network model stored in <a href="http://torch.ch">Torch7</a> framework's format.
        /// </summary>
        /// <param name="model">
        /// path to the file, dumped from Torch by using torch.save() function.
        /// </param>
        /// <param name="isBinary">
        /// specifies whether the network was serialized in ascii mode or binary.
        /// </param>
        /// <param name="evaluate">
        /// specifies testing phase of network. If true, it's similar to evaluate() method in Torch.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          @note Ascii mode of Torch serializer is more preferable, because binary mode extensively use `long` type of C language,
        ///          which has various bit-length on different systems.
        ///        
        ///         The loading file must contain serialized <a href="https://github.com/torch/nn/blob/master/doc/module.md">nn.Module</a> object
        ///         with importing network. Try to eliminate a custom objects from serialazing data to avoid importing errors.
        ///        
        ///         List of supported layers (i.e. object instances derived from Torch nn.Module class):
        ///         - nn.Sequential
        ///         - nn.Parallel
        ///         - nn.Concat
        ///         - nn.Linear
        ///         - nn.SpatialConvolution
        ///         - nn.SpatialMaxPooling, nn.SpatialAveragePooling
        ///         - nn.ReLU, nn.TanH, nn.Sigmoid
        ///         - nn.Reshape
        ///         - nn.SoftMax, nn.LogSoftMax
        ///        
        ///         Also some equivalents of these classes from cunn, cudnn, and fbcunn may be successfully imported.
        /// </remarks>
        public static Net readNetFromTorch(string model)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromTorch_12(model)));


        }


        //
        // C++:  Net cv::dnn::readNet(String model, String config = "", String framework = "")
        //

        /// <summary>
        ///  Read deep learning network represented in one of the supported formats.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights. The following file
        ///                           extensions are expected for models from different frameworks:
        ///                           * `*.caffemodel` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pb` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.t7` | `*.net` (Torch, http://torch.ch/)
        ///                           * `*.weights` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.bin` | `*.onnx` (OpenVINO, https://software.intel.com/openvino-toolkit)
        ///                           * `*.onnx` (ONNX, https://onnx.ai/)
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration. It could be a
        ///                            file with the following extensions:
        ///                           * `*.prototxt` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pbtxt` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.cfg` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.xml` (OpenVINO, https://software.intel.com/openvino-toolkit)
        /// </param>
        /// <param name="framework">
        /// Explicit framework name tag to determine a format.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          This function automatically detects an origin framework of trained model
        ///          and calls an appropriate function such @ref readNetFromCaffe, @ref readNetFromTensorflow,
        ///          @ref readNetFromTorch or @ref readNetFromDarknet. An order of @p model and @p config
        ///          arguments does not matter.
        /// </remarks>
        public static Net readNet(string model, string config, string framework)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNet_10(model, config, framework)));


        }

        /// <summary>
        ///  Read deep learning network represented in one of the supported formats.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights. The following file
        ///                           extensions are expected for models from different frameworks:
        ///                           * `*.caffemodel` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pb` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.t7` | `*.net` (Torch, http://torch.ch/)
        ///                           * `*.weights` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.bin` | `*.onnx` (OpenVINO, https://software.intel.com/openvino-toolkit)
        ///                           * `*.onnx` (ONNX, https://onnx.ai/)
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration. It could be a
        ///                            file with the following extensions:
        ///                           * `*.prototxt` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pbtxt` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.cfg` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.xml` (OpenVINO, https://software.intel.com/openvino-toolkit)
        /// </param>
        /// <param name="framework">
        /// Explicit framework name tag to determine a format.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          This function automatically detects an origin framework of trained model
        ///          and calls an appropriate function such @ref readNetFromCaffe, @ref readNetFromTensorflow,
        ///          @ref readNetFromTorch or @ref readNetFromDarknet. An order of @p model and @p config
        ///          arguments does not matter.
        /// </remarks>
        public static Net readNet(string model, string config)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNet_11(model, config)));


        }

        /// <summary>
        ///  Read deep learning network represented in one of the supported formats.
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights. The following file
        ///                           extensions are expected for models from different frameworks:
        ///                           * `*.caffemodel` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pb` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.t7` | `*.net` (Torch, http://torch.ch/)
        ///                           * `*.weights` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.bin` | `*.onnx` (OpenVINO, https://software.intel.com/openvino-toolkit)
        ///                           * `*.onnx` (ONNX, https://onnx.ai/)
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration. It could be a
        ///                            file with the following extensions:
        ///                           * `*.prototxt` (Caffe, http://caffe.berkeleyvision.org/)
        ///                           * `*.pbtxt` (TensorFlow, https://www.tensorflow.org/)
        ///                           * `*.cfg` (Darknet, https://pjreddie.com/darknet/)
        ///                           * `*.xml` (OpenVINO, https://software.intel.com/openvino-toolkit)
        /// </param>
        /// <param name="framework">
        /// Explicit framework name tag to determine a format.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        /// <remarks>
        ///          This function automatically detects an origin framework of trained model
        ///          and calls an appropriate function such @ref readNetFromCaffe, @ref readNetFromTensorflow,
        ///          @ref readNetFromTorch or @ref readNetFromDarknet. An order of @p model and @p config
        ///          arguments does not matter.
        /// </remarks>
        public static Net readNet(string model)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNet_12(model)));


        }


        //
        // C++:  Net cv::dnn::readNet(String framework, vector_uchar bufferModel, vector_uchar bufferConfig = std::vector<uchar>())
        //

        /// <summary>
        ///  Read deep learning network represented in one of the supported formats.
        ///          @details This is an overloaded member function, provided for convenience.
        ///                   It differs from the above function only in what argument(s) it accepts.
        /// </summary>
        /// <param name="framework">
        /// Name of origin framework.
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNet(string framework, MatOfByte bufferModel, MatOfByte bufferConfig)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            if (bufferConfig != null) bufferConfig.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            Mat bufferConfig_mat = bufferConfig;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNet_13(framework, bufferModel_mat.nativeObj, bufferConfig_mat.nativeObj)));


        }

        /// <summary>
        ///  Read deep learning network represented in one of the supported formats.
        ///          @details This is an overloaded member function, provided for convenience.
        ///                   It differs from the above function only in what argument(s) it accepts.
        /// </summary>
        /// <param name="framework">
        /// Name of origin framework.
        /// </param>
        /// <param name="bufferModel">
        /// A buffer with a content of binary file with weights
        /// </param>
        /// <param name="bufferConfig">
        /// A buffer with a content of text file contains network configuration.
        /// </param>
        /// <returns>
        ///  Net object.
        /// </returns>
        public static Net readNet(string framework, MatOfByte bufferModel)
        {
            if (bufferModel != null) bufferModel.ThrowIfDisposed();
            Mat bufferModel_mat = bufferModel;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNet_14(framework, bufferModel_mat.nativeObj)));


        }


        //
        // C++:  Mat cv::dnn::readTorchBlob(String filename, bool isBinary = true)
        //

        /// <summary>
        ///  Loads blob which was serialized as torch.Tensor object of Torch7 framework.
        ///          @warning This function has the same limitations as readNetFromTorch().
        /// </summary>
        public static Mat readTorchBlob(string filename, bool isBinary)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readTorchBlob_10(filename, isBinary)));


        }

        /// <summary>
        ///  Loads blob which was serialized as torch.Tensor object of Torch7 framework.
        ///          @warning This function has the same limitations as readNetFromTorch().
        /// </summary>
        public static Mat readTorchBlob(string filename)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readTorchBlob_11(filename)));


        }


        //
        // C++:  Net cv::dnn::readNetFromModelOptimizer(String xml, String bin = "")
        //

        /// <summary>
        ///  Load a network from Intel's Model Optimizer intermediate representation.
        /// </summary>
        /// <param name="xml">
        /// XML configuration file with network's topology.
        /// </param>
        /// <param name="bin">
        /// Binary file with trained weights.
        /// </param>
        /// <returns>
        ///  Net object.
        ///          Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine
        ///          backend.
        /// </returns>
        public static Net readNetFromModelOptimizer(string xml, string bin)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromModelOptimizer_10(xml, bin)));


        }

        /// <summary>
        ///  Load a network from Intel's Model Optimizer intermediate representation.
        /// </summary>
        /// <param name="xml">
        /// XML configuration file with network's topology.
        /// </param>
        /// <param name="bin">
        /// Binary file with trained weights.
        /// </param>
        /// <returns>
        ///  Net object.
        ///          Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine
        ///          backend.
        /// </returns>
        public static Net readNetFromModelOptimizer(string xml)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromModelOptimizer_11(xml)));


        }


        //
        // C++:  Net cv::dnn::readNetFromModelOptimizer(vector_uchar bufferModelConfig, vector_uchar bufferWeights)
        //

        /// <summary>
        ///  Load a network from Intel's Model Optimizer intermediate representation.
        /// </summary>
        /// <param name="bufferModelConfig">
        /// Buffer contains XML configuration with network's topology.
        /// </param>
        /// <param name="bufferWeights">
        /// Buffer contains binary data with trained weights.
        /// </param>
        /// <returns>
        ///  Net object.
        ///          Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine
        ///          backend.
        /// </returns>
        public static Net readNetFromModelOptimizer(MatOfByte bufferModelConfig, MatOfByte bufferWeights)
        {
            if (bufferModelConfig != null) bufferModelConfig.ThrowIfDisposed();
            if (bufferWeights != null) bufferWeights.ThrowIfDisposed();
            Mat bufferModelConfig_mat = bufferModelConfig;
            Mat bufferWeights_mat = bufferWeights;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromModelOptimizer_12(bufferModelConfig_mat.nativeObj, bufferWeights_mat.nativeObj)));


        }


        //
        // C++:  Net cv::dnn::readNetFromONNX(String onnxFile)
        //

        /// <summary>
        ///  Reads a network model <a href="https://onnx.ai/">ONNX</a>.
        /// </summary>
        /// <param name="onnxFile">
        /// path to the .onnx file with text description of the network architecture.
        /// </param>
        /// <returns>
        ///  Network object that ready to do forward, throw an exception in failure cases.
        /// </returns>
        public static Net readNetFromONNX(string onnxFile)
        {


            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromONNX_10(onnxFile)));


        }


        //
        // C++:  Net cv::dnn::readNetFromONNX(vector_uchar buffer)
        //

        /// <summary>
        ///  Reads a network model from <a href="https://onnx.ai/">ONNX</a>
        ///                 in-memory buffer.
        /// </summary>
        /// <param name="buffer">
        /// in-memory buffer that stores the ONNX model bytes.
        /// </param>
        /// <returns>
        ///  Network object that ready to do forward, throw an exception
        ///                in failure cases.
        /// </returns>
        public static Net readNetFromONNX(MatOfByte buffer)
        {
            if (buffer != null) buffer.ThrowIfDisposed();
            Mat buffer_mat = buffer;
            return new Net(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readNetFromONNX_11(buffer_mat.nativeObj)));


        }


        //
        // C++:  Mat cv::dnn::readTensorFromONNX(String path)
        //

        /// <summary>
        ///  Creates blob from .pb file.
        /// </summary>
        /// <param name="path">
        /// to the .pb file with input tensor.
        /// </param>
        /// <returns>
        ///  Mat.
        /// </returns>
        public static Mat readTensorFromONNX(string path)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_readTensorFromONNX_10(path)));


        }


        //
        // C++:  Mat cv::dnn::blobFromImage(Mat image, double scalefactor = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false, int ddepth = CV_32F)
        //

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop, int ddepth)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_10(image.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB, crop, ddepth)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_11(image.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB, crop)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor, Size size, Scalar mean, bool swapRB)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_12(image.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor, Size size, Scalar mean)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_13(image.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3])));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor, Size size)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_14(image.nativeObj, scalefactor, size.width, size.height)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image, double scalefactor)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_15(image.nativeObj, scalefactor)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image. Optionally resizes and crops @p image from center,
        ///          subtract @p mean values, scales values by @p scalefactor, swap Blue and Red channels.
        /// </summary>
        /// <param name="image">
        /// input image (with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImage(Mat image)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImage_16(image.nativeObj)));


        }


        //
        // C++:  Mat cv::dnn::blobFromImages(vector_Mat images, double scalefactor = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false, int ddepth = CV_32F)
        //

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop, int ddepth)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_10(images_mat.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB, crop, ddepth)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_11(images_mat.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB, crop)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor, Size size, Scalar mean, bool swapRB)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_12(images_mat.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3], swapRB)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor, Size size, Scalar mean)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_13(images_mat.nativeObj, scalefactor, size.width, size.height, mean.val[0], mean.val[1], mean.val[2], mean.val[3])));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor, Size size)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_14(images_mat.nativeObj, scalefactor, size.width, size.height)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images, double scalefactor)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_15(images_mat.nativeObj, scalefactor)));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images. Optionally resizes and
        ///          crops @p images from center, subtract @p mean values, scales values by @p scalefactor,
        ///          swap Blue and Red channels.
        /// </summary>
        /// <param name="images">
        /// input images (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="size">
        /// spatial size for output image
        /// </param>
        /// <param name="mean">
        /// scalar with mean values which are subtracted from channels. Values are intended
        ///          to be in (mean-R, mean-G, mean-B) order if @p image has BGR ordering and @p swapRB is true.
        /// </param>
        /// <param name="scalefactor">
        /// multiplier for @p images values.
        /// </param>
        /// <param name="swapRB">
        /// flag which indicates that swap first and last channels
        ///          in 3-channel image is necessary.
        /// </param>
        /// <param name="crop">
        /// flag which indicates whether image will be cropped after resize or not
        /// </param>
        /// <param name="ddepth">
        /// Depth of output blob. Choose CV_32F or CV_8U.
        ///          @details if @p crop is true, input image is resized so one side after resize is equal to corresponding
        ///          dimension in @p size and another one is equal or larger. Then, crop from the center is performed.
        ///          If @p crop is false, direct resize without cropping and preserving aspect ratio is performed.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat with NCHW dimensions order.
        /// </returns>
        /// <remarks>
        ///         @note
        ///         The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.
        /// </remarks>
        public static Mat blobFromImages(List<Mat> images)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImages_16(images_mat.nativeObj)));


        }


        //
        // C++:  Mat cv::dnn::blobFromImageWithParams(Mat image, Image2BlobParams param = Image2BlobParams())
        //

        /// <summary>
        ///  Creates 4-dimensional blob from image with given params.
        /// </summary>
        /// <remarks>
        ///          @details This function is an extension of @ref blobFromImage to meet more image preprocess needs.
        ///          Given input image and preprocessing parameters, and function outputs the blob.
        /// </remarks>
        /// <param name="image">
        /// input image (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="param">
        /// struct of Image2BlobParams, contains all parameters needed by processing of image to blob.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat.
        /// </returns>
        public static Mat blobFromImageWithParams(Mat image, Image2BlobParams param)
        {
            if (image != null) image.ThrowIfDisposed();
            if (param != null) param.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImageWithParams_10(image.nativeObj, param.getNativeObjAddr())));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from image with given params.
        /// </summary>
        /// <remarks>
        ///          @details This function is an extension of @ref blobFromImage to meet more image preprocess needs.
        ///          Given input image and preprocessing parameters, and function outputs the blob.
        /// </remarks>
        /// <param name="image">
        /// input image (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="param">
        /// struct of Image2BlobParams, contains all parameters needed by processing of image to blob.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat.
        /// </returns>
        public static Mat blobFromImageWithParams(Mat image)
        {
            if (image != null) image.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImageWithParams_11(image.nativeObj)));


        }


        //
        // C++:  void cv::dnn::blobFromImageWithParams(Mat image, Mat& blob, Image2BlobParams param = Image2BlobParams())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void blobFromImageWithParams(Mat image, Mat blob, Image2BlobParams param)
        {
            if (image != null) image.ThrowIfDisposed();
            if (blob != null) blob.ThrowIfDisposed();
            if (param != null) param.ThrowIfDisposed();

            dnn_Dnn_blobFromImageWithParams_12(image.nativeObj, blob.nativeObj, param.getNativeObjAddr());


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void blobFromImageWithParams(Mat image, Mat blob)
        {
            if (image != null) image.ThrowIfDisposed();
            if (blob != null) blob.ThrowIfDisposed();

            dnn_Dnn_blobFromImageWithParams_13(image.nativeObj, blob.nativeObj);


        }


        //
        // C++:  Mat cv::dnn::blobFromImagesWithParams(vector_Mat images, Image2BlobParams param = Image2BlobParams())
        //

        /// <summary>
        ///  Creates 4-dimensional blob from series of images with given params.
        /// </summary>
        /// <remarks>
        ///          @details This function is an extension of @ref blobFromImages to meet more image preprocess needs.
        ///          Given input image and preprocessing parameters, and function outputs the blob.
        /// </remarks>
        /// <param name="images">
        /// input image (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="param">
        /// struct of Image2BlobParams, contains all parameters needed by processing of image to blob.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat.
        /// </returns>
        public static Mat blobFromImagesWithParams(List<Mat> images, Image2BlobParams param)
        {
            if (param != null) param.ThrowIfDisposed();
            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImagesWithParams_10(images_mat.nativeObj, param.getNativeObjAddr())));


        }

        /// <summary>
        ///  Creates 4-dimensional blob from series of images with given params.
        /// </summary>
        /// <remarks>
        ///          @details This function is an extension of @ref blobFromImages to meet more image preprocess needs.
        ///          Given input image and preprocessing parameters, and function outputs the blob.
        /// </remarks>
        /// <param name="images">
        /// input image (all with 1-, 3- or 4-channels).
        /// </param>
        /// <param name="param">
        /// struct of Image2BlobParams, contains all parameters needed by processing of image to blob.
        /// </param>
        /// <returns>
        ///  4-dimensional Mat.
        /// </returns>
        public static Mat blobFromImagesWithParams(List<Mat> images)
        {

            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_blobFromImagesWithParams_11(images_mat.nativeObj)));


        }


        //
        // C++:  void cv::dnn::blobFromImagesWithParams(vector_Mat images, Mat& blob, Image2BlobParams param = Image2BlobParams())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void blobFromImagesWithParams(List<Mat> images, Mat blob, Image2BlobParams param)
        {
            if (blob != null) blob.ThrowIfDisposed();
            if (param != null) param.ThrowIfDisposed();
            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            dnn_Dnn_blobFromImagesWithParams_12(images_mat.nativeObj, blob.nativeObj, param.getNativeObjAddr());


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void blobFromImagesWithParams(List<Mat> images, Mat blob)
        {
            if (blob != null) blob.ThrowIfDisposed();
            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            dnn_Dnn_blobFromImagesWithParams_13(images_mat.nativeObj, blob.nativeObj);


        }


        //
        // C++:  void cv::dnn::imagesFromBlob(Mat blob_, vector_Mat& images_)
        //

        /// <summary>
        ///  Parse a 4D blob and output the images it contains as 2D arrays through a simpler data structure
        ///          (std::vector&lt;cv::Mat&gt;).
        /// </summary>
        /// <param name="blob_">
        /// 4 dimensional array (images, channels, height, width) in floating point precision (CV_32F) from
        ///          which you would like to extract the images.
        /// </param>
        /// <param name="images_">
        /// array of 2D Mat containing the images extracted from the blob in floating point precision
        ///          (CV_32F). They are non normalized neither mean added. The number of returned images equals the first dimension
        ///          of the blob (batch size). Every image has a number of channels equals to the second dimension of the blob (depth).
        /// </param>
        public static void imagesFromBlob(Mat blob_, List<Mat> images_)
        {
            if (blob_ != null) blob_.ThrowIfDisposed();
            using Mat images__mat = new Mat();
            dnn_Dnn_imagesFromBlob_10(blob_.nativeObj, images__mat.nativeObj);
            Converters.Mat_to_vector_Mat(images__mat, images_);

        }


        //
        // C++:  void cv::dnn::shrinkCaffeModel(String src, String dst, vector_String layersTypes = std::vector<String>())
        //

        /// <summary>
        ///  Convert all weights of Caffe network to half precision floating point.
        /// </summary>
        /// <param name="src">
        /// Path to origin model from Caffe framework contains single
        ///                    precision floating point weights (usually has `.caffemodel` extension).
        /// </param>
        /// <param name="dst">
        /// Path to destination model with updated weights.
        /// </param>
        /// <param name="layersTypes">
        /// Set of layers types which parameters will be converted.
        ///                            By default, converts only Convolutional and Fully-Connected layers'
        ///                            weights.
        /// </param>
        /// <remarks>
        ///         @note Shrinked model has no origin float32 weights so it can't be used
        ///               in origin Caffe framework anymore. However the structure of data
        ///               is taken from NVidia's Caffe fork: https://github.com/NVIDIA/caffe.
        ///               So the resulting model may be used there.
        /// </remarks>
        public static void shrinkCaffeModel(string src, string dst, List<string> layersTypes)
        {

            using Mat layersTypes_mat = Converters.vector_String_to_Mat(layersTypes);
            dnn_Dnn_shrinkCaffeModel_10(src, dst, layersTypes_mat.nativeObj);


        }

        /// <summary>
        ///  Convert all weights of Caffe network to half precision floating point.
        /// </summary>
        /// <param name="src">
        /// Path to origin model from Caffe framework contains single
        ///                    precision floating point weights (usually has `.caffemodel` extension).
        /// </param>
        /// <param name="dst">
        /// Path to destination model with updated weights.
        /// </param>
        /// <param name="layersTypes">
        /// Set of layers types which parameters will be converted.
        ///                            By default, converts only Convolutional and Fully-Connected layers'
        ///                            weights.
        /// </param>
        /// <remarks>
        ///         @note Shrinked model has no origin float32 weights so it can't be used
        ///               in origin Caffe framework anymore. However the structure of data
        ///               is taken from NVidia's Caffe fork: https://github.com/NVIDIA/caffe.
        ///               So the resulting model may be used there.
        /// </remarks>
        public static void shrinkCaffeModel(string src, string dst)
        {


            dnn_Dnn_shrinkCaffeModel_11(src, dst);


        }


        //
        // C++:  void cv::dnn::writeTextGraph(String model, String output)
        //

        /// <summary>
        ///  Create a text representation for a binary network stored in protocol buffer format.
        /// </summary>
        /// <param name="model">
        /// A path to binary network.
        /// </param>
        /// <param name="output">
        /// A path to output text file to be created.
        /// </param>
        /// <remarks>
        ///          @note To reduce output file size, trained weights are not included.
        /// </remarks>
        public static void writeTextGraph(string model, string output)
        {


            dnn_Dnn_writeTextGraph_10(model, output);


        }


        //
        // C++:  void cv::dnn::NMSBoxes(vector_Rect2d bboxes, vector_float scores, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        //

        /// <summary>
        ///  Performs non maximum suppression given boxes and corresponding scores.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxes(MatOfRect2d bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices, float eta, int top_k)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxes_10(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta, top_k);


        }

        /// <summary>
        ///  Performs non maximum suppression given boxes and corresponding scores.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxes(MatOfRect2d bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices, float eta)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxes_11(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta);


        }

        /// <summary>
        ///  Performs non maximum suppression given boxes and corresponding scores.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxes(MatOfRect2d bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxes_12(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj);


        }


        //
        // C++:  void cv::dnn::NMSBoxes(vector_RotatedRect bboxes, vector_float scores, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        //

        public static void NMSBoxesRotated(MatOfRotatedRect bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices, float eta, int top_k)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesRotated_10(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta, top_k);


        }

        public static void NMSBoxesRotated(MatOfRotatedRect bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices, float eta)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesRotated_11(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta);


        }

        public static void NMSBoxesRotated(MatOfRotatedRect bboxes, MatOfFloat scores, float score_threshold, float nms_threshold, MatOfInt indices)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesRotated_12(bboxes_mat.nativeObj, scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj);


        }


        //
        // C++:  void cv::dnn::NMSBoxesBatched(vector_Rect2d bboxes, vector_float scores, vector_int class_ids, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        //

        /// <summary>
        ///  Performs batched non maximum suppression on given boxes and corresponding scores across different classes.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="class_ids">
        /// a set of corresponding class ids. Ids are integer and usually start from 0.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxesBatched(MatOfRect2d bboxes, MatOfFloat scores, MatOfInt class_ids, float score_threshold, float nms_threshold, MatOfInt indices, float eta, int top_k)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (class_ids != null) class_ids.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat class_ids_mat = class_ids;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesBatched_10(bboxes_mat.nativeObj, scores_mat.nativeObj, class_ids_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta, top_k);


        }

        /// <summary>
        ///  Performs batched non maximum suppression on given boxes and corresponding scores across different classes.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="class_ids">
        /// a set of corresponding class ids. Ids are integer and usually start from 0.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxesBatched(MatOfRect2d bboxes, MatOfFloat scores, MatOfInt class_ids, float score_threshold, float nms_threshold, MatOfInt indices, float eta)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (class_ids != null) class_ids.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat class_ids_mat = class_ids;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesBatched_11(bboxes_mat.nativeObj, scores_mat.nativeObj, class_ids_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, eta);


        }

        /// <summary>
        ///  Performs batched non maximum suppression on given boxes and corresponding scores across different classes.
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="class_ids">
        /// a set of corresponding class ids. Ids are integer and usually start from 0.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="eta">
        /// a coefficient in adaptive threshold formula: \f$nms\_threshold_{i+1}=eta\cdot nms\_threshold_i\f$.
        /// </param>
        /// <param name="top_k">
        /// if `>0`, keep at most @p top_k picked indices.
        /// </param>
        public static void NMSBoxesBatched(MatOfRect2d bboxes, MatOfFloat scores, MatOfInt class_ids, float score_threshold, float nms_threshold, MatOfInt indices)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (class_ids != null) class_ids.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat class_ids_mat = class_ids;
            Mat indices_mat = indices;
            dnn_Dnn_NMSBoxesBatched_12(bboxes_mat.nativeObj, scores_mat.nativeObj, class_ids_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj);


        }


        //
        // C++:  void cv::dnn::softNMSBoxes(vector_Rect bboxes, vector_float scores, vector_float& updated_scores, float score_threshold, float nms_threshold, vector_int& indices, size_t top_k = 0, float sigma = 0.5, SoftNMSMethod method = SoftNMSMethod::SOFTNMS_GAUSSIAN)
        //

        /// <summary>
        ///  Performs soft non maximum suppression given boxes and corresponding scores.
        ///         Reference: https://arxiv.org/abs/1704.04503
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply Soft NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="updated_scores">
        /// a set of corresponding updated confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="top_k">
        /// keep at most @p top_k picked indices.
        /// </param>
        /// <param name="sigma">
        /// parameter of Gaussian weighting.
        /// </param>
        /// <param name="method">
        /// Gaussian or linear.
        ///         @see SoftNMSMethod
        /// </param>
        public static void softNMSBoxes(MatOfRect bboxes, MatOfFloat scores, MatOfFloat updated_scores, float score_threshold, float nms_threshold, MatOfInt indices, long top_k, float sigma)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (updated_scores != null) updated_scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat updated_scores_mat = updated_scores;
            Mat indices_mat = indices;
            dnn_Dnn_softNMSBoxes_10(bboxes_mat.nativeObj, scores_mat.nativeObj, updated_scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, top_k, sigma);


        }

        /// <summary>
        ///  Performs soft non maximum suppression given boxes and corresponding scores.
        ///         Reference: https://arxiv.org/abs/1704.04503
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply Soft NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="updated_scores">
        /// a set of corresponding updated confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="top_k">
        /// keep at most @p top_k picked indices.
        /// </param>
        /// <param name="sigma">
        /// parameter of Gaussian weighting.
        /// </param>
        /// <param name="method">
        /// Gaussian or linear.
        ///         @see SoftNMSMethod
        /// </param>
        public static void softNMSBoxes(MatOfRect bboxes, MatOfFloat scores, MatOfFloat updated_scores, float score_threshold, float nms_threshold, MatOfInt indices, long top_k)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (updated_scores != null) updated_scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat updated_scores_mat = updated_scores;
            Mat indices_mat = indices;
            dnn_Dnn_softNMSBoxes_12(bboxes_mat.nativeObj, scores_mat.nativeObj, updated_scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj, top_k);


        }

        /// <summary>
        ///  Performs soft non maximum suppression given boxes and corresponding scores.
        ///         Reference: https://arxiv.org/abs/1704.04503
        /// </summary>
        /// <param name="bboxes">
        /// a set of bounding boxes to apply Soft NMS.
        /// </param>
        /// <param name="scores">
        /// a set of corresponding confidences.
        /// </param>
        /// <param name="updated_scores">
        /// a set of corresponding updated confidences.
        /// </param>
        /// <param name="score_threshold">
        /// a threshold used to filter boxes by score.
        /// </param>
        /// <param name="nms_threshold">
        /// a threshold used in non maximum suppression.
        /// </param>
        /// <param name="indices">
        /// the kept indices of bboxes after NMS.
        /// </param>
        /// <param name="top_k">
        /// keep at most @p top_k picked indices.
        /// </param>
        /// <param name="sigma">
        /// parameter of Gaussian weighting.
        /// </param>
        /// <param name="method">
        /// Gaussian or linear.
        ///         @see SoftNMSMethod
        /// </param>
        public static void softNMSBoxes(MatOfRect bboxes, MatOfFloat scores, MatOfFloat updated_scores, float score_threshold, float nms_threshold, MatOfInt indices)
        {
            if (bboxes != null) bboxes.ThrowIfDisposed();
            if (scores != null) scores.ThrowIfDisposed();
            if (updated_scores != null) updated_scores.ThrowIfDisposed();
            if (indices != null) indices.ThrowIfDisposed();
            Mat bboxes_mat = bboxes;
            Mat scores_mat = scores;
            Mat updated_scores_mat = updated_scores;
            Mat indices_mat = indices;
            dnn_Dnn_softNMSBoxes_13(bboxes_mat.nativeObj, scores_mat.nativeObj, updated_scores_mat.nativeObj, score_threshold, nms_threshold, indices_mat.nativeObj);


        }


        //
        // C++:  String cv::dnn::getInferenceEngineBackendType()
        //

        /// <summary>
        ///  Returns Inference Engine internal backend API.
        /// </summary>
        /// <remarks>
        ///     See values of `CV_DNN_BACKEND_INFERENCE_ENGINE_*` macros.
        ///    
        ///     `OPENCV_DNN_BACKEND_INFERENCE_ENGINE_TYPE` runtime parameter (environment variable) is ignored since 4.6.0.
        ///    
        ///     @deprecated
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static string getInferenceEngineBackendType()
        {


            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_getInferenceEngineBackendType_10()));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  String cv::dnn::setInferenceEngineBackendType(String newBackendType)
        //

        /// <summary>
        ///  Specify Inference Engine internal backend API.
        /// </summary>
        /// <remarks>
        ///     See values of `CV_DNN_BACKEND_INFERENCE_ENGINE_*` macros.
        /// </remarks>
        /// <returns>
        ///  previous value of internal backend API
        /// </returns>
        /// <remarks>
        ///     @deprecated
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static string setInferenceEngineBackendType(string newBackendType)
        {


            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_setInferenceEngineBackendType_10(newBackendType)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  void cv::dnn::resetMyriadDevice()
        //

        /// <summary>
        ///  Release a Myriad device (binded by OpenCV).
        /// </summary>
        /// <remarks>
        ///     Single Myriad device cannot be shared across multiple processes which uses
        ///     Inference Engine's Myriad plugin.
        /// </remarks>
        public static void resetMyriadDevice()
        {


            dnn_Dnn_resetMyriadDevice_10();


        }


        //
        // C++:  String cv::dnn::getInferenceEngineVPUType()
        //

        /// <summary>
        ///  Returns Inference Engine VPU type.
        /// </summary>
        /// <remarks>
        ///     See values of `CV_DNN_INFERENCE_ENGINE_VPU_TYPE_*` macros.
        /// </remarks>
        public static string getInferenceEngineVPUType()
        {


            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_getInferenceEngineVPUType_10()));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  String cv::dnn::getInferenceEngineCPUType()
        //

        /// <summary>
        ///  Returns Inference Engine CPU type.
        /// </summary>
        /// <remarks>
        ///     Specify OpenVINO plugin: CPU or ARM.
        /// </remarks>
        public static string getInferenceEngineCPUType()
        {


            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_Dnn_getInferenceEngineCPUType_10()));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  void cv::dnn::releaseHDDLPlugin()
        //

        /// <summary>
        ///  Release a HDDL plugin.
        /// </summary>
        public static void releaseHDDLPlugin()
        {


            dnn_Dnn_releaseHDDLPlugin_10();


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  vector_Target cv::dnn::getAvailableTargets(dnn_Backend be)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_getAvailableTargets_10(int be);

        // C++:  Net cv::dnn::readNetFromDarknet(String cfgFile, String darknetModel = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromDarknet_10(string cfgFile, string darknetModel);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromDarknet_11(string cfgFile);

        // C++:  Net cv::dnn::readNetFromDarknet(vector_uchar bufferCfg, vector_uchar bufferModel = std::vector<uchar>())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromDarknet_12(IntPtr bufferCfg_mat_nativeObj, IntPtr bufferModel_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromDarknet_13(IntPtr bufferCfg_mat_nativeObj);

        // C++:  Net cv::dnn::readNetFromCaffe(String prototxt, String caffeModel = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromCaffe_10(string prototxt, string caffeModel);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromCaffe_11(string prototxt);

        // C++:  Net cv::dnn::readNetFromCaffe(vector_uchar bufferProto, vector_uchar bufferModel = std::vector<uchar>())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromCaffe_12(IntPtr bufferProto_mat_nativeObj, IntPtr bufferModel_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromCaffe_13(IntPtr bufferProto_mat_nativeObj);

        // C++:  Net cv::dnn::readNetFromTensorflow(String model, String config = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTensorflow_10(string model, string config);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTensorflow_11(string model);

        // C++:  Net cv::dnn::readNetFromTensorflow(vector_uchar bufferModel, vector_uchar bufferConfig = std::vector<uchar>())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTensorflow_12(IntPtr bufferModel_mat_nativeObj, IntPtr bufferConfig_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTensorflow_13(IntPtr bufferModel_mat_nativeObj);

        // C++:  Net cv::dnn::readNetFromTFLite(String model)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTFLite_10(string model);

        // C++:  Net cv::dnn::readNetFromTFLite(vector_uchar bufferModel)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTFLite_11(IntPtr bufferModel_mat_nativeObj);

        // C++:  Net cv::dnn::readNetFromTorch(String model, bool isBinary = true, bool evaluate = true)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTorch_10(string model, [MarshalAs(UnmanagedType.U1)] bool isBinary, [MarshalAs(UnmanagedType.U1)] bool evaluate);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTorch_11(string model, [MarshalAs(UnmanagedType.U1)] bool isBinary);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromTorch_12(string model);

        // C++:  Net cv::dnn::readNet(String model, String config = "", String framework = "")
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNet_10(string model, string config, string framework);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNet_11(string model, string config);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNet_12(string model);

        // C++:  Net cv::dnn::readNet(String framework, vector_uchar bufferModel, vector_uchar bufferConfig = std::vector<uchar>())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNet_13(string framework, IntPtr bufferModel_mat_nativeObj, IntPtr bufferConfig_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNet_14(string framework, IntPtr bufferModel_mat_nativeObj);

        // C++:  Mat cv::dnn::readTorchBlob(String filename, bool isBinary = true)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readTorchBlob_10(string filename, [MarshalAs(UnmanagedType.U1)] bool isBinary);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readTorchBlob_11(string filename);

        // C++:  Net cv::dnn::readNetFromModelOptimizer(String xml, String bin = "")
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromModelOptimizer_10(string xml, string bin);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromModelOptimizer_11(string xml);

        // C++:  Net cv::dnn::readNetFromModelOptimizer(vector_uchar bufferModelConfig, vector_uchar bufferWeights)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromModelOptimizer_12(IntPtr bufferModelConfig_mat_nativeObj, IntPtr bufferWeights_mat_nativeObj);

        // C++:  Net cv::dnn::readNetFromONNX(String onnxFile)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromONNX_10(string onnxFile);

        // C++:  Net cv::dnn::readNetFromONNX(vector_uchar buffer)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readNetFromONNX_11(IntPtr buffer_mat_nativeObj);

        // C++:  Mat cv::dnn::readTensorFromONNX(String path)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_readTensorFromONNX_10(string path);

        // C++:  Mat cv::dnn::blobFromImage(Mat image, double scalefactor = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false, int ddepth = CV_32F)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_10(IntPtr image_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB, [MarshalAs(UnmanagedType.U1)] bool crop, int ddepth);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_11(IntPtr image_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB, [MarshalAs(UnmanagedType.U1)] bool crop);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_12(IntPtr image_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_13(IntPtr image_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_14(IntPtr image_nativeObj, double scalefactor, double size_width, double size_height);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_15(IntPtr image_nativeObj, double scalefactor);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImage_16(IntPtr image_nativeObj);

        // C++:  Mat cv::dnn::blobFromImages(vector_Mat images, double scalefactor = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false, int ddepth = CV_32F)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_10(IntPtr images_mat_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB, [MarshalAs(UnmanagedType.U1)] bool crop, int ddepth);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_11(IntPtr images_mat_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB, [MarshalAs(UnmanagedType.U1)] bool crop);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_12(IntPtr images_mat_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3, [MarshalAs(UnmanagedType.U1)] bool swapRB);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_13(IntPtr images_mat_nativeObj, double scalefactor, double size_width, double size_height, double mean_val0, double mean_val1, double mean_val2, double mean_val3);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_14(IntPtr images_mat_nativeObj, double scalefactor, double size_width, double size_height);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_15(IntPtr images_mat_nativeObj, double scalefactor);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImages_16(IntPtr images_mat_nativeObj);

        // C++:  Mat cv::dnn::blobFromImageWithParams(Mat image, Image2BlobParams param = Image2BlobParams())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImageWithParams_10(IntPtr image_nativeObj, IntPtr param_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImageWithParams_11(IntPtr image_nativeObj);

        // C++:  void cv::dnn::blobFromImageWithParams(Mat image, Mat& blob, Image2BlobParams param = Image2BlobParams())
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_blobFromImageWithParams_12(IntPtr image_nativeObj, IntPtr blob_nativeObj, IntPtr param_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_blobFromImageWithParams_13(IntPtr image_nativeObj, IntPtr blob_nativeObj);

        // C++:  Mat cv::dnn::blobFromImagesWithParams(vector_Mat images, Image2BlobParams param = Image2BlobParams())
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImagesWithParams_10(IntPtr images_mat_nativeObj, IntPtr param_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_blobFromImagesWithParams_11(IntPtr images_mat_nativeObj);

        // C++:  void cv::dnn::blobFromImagesWithParams(vector_Mat images, Mat& blob, Image2BlobParams param = Image2BlobParams())
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_blobFromImagesWithParams_12(IntPtr images_mat_nativeObj, IntPtr blob_nativeObj, IntPtr param_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_blobFromImagesWithParams_13(IntPtr images_mat_nativeObj, IntPtr blob_nativeObj);

        // C++:  void cv::dnn::imagesFromBlob(Mat blob_, vector_Mat& images_)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_imagesFromBlob_10(IntPtr blob__nativeObj, IntPtr images__mat_nativeObj);

        // C++:  void cv::dnn::shrinkCaffeModel(String src, String dst, vector_String layersTypes = std::vector<String>())
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_shrinkCaffeModel_10(string src, string dst, IntPtr layersTypes_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_shrinkCaffeModel_11(string src, string dst);

        // C++:  void cv::dnn::writeTextGraph(String model, String output)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_writeTextGraph_10(string model, string output);

        // C++:  void cv::dnn::NMSBoxes(vector_Rect2d bboxes, vector_float scores, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxes_10(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta, int top_k);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxes_11(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxes_12(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj);

        // C++:  void cv::dnn::NMSBoxes(vector_RotatedRect bboxes, vector_float scores, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesRotated_10(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta, int top_k);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesRotated_11(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesRotated_12(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj);

        // C++:  void cv::dnn::NMSBoxesBatched(vector_Rect2d bboxes, vector_float scores, vector_int class_ids, float score_threshold, float nms_threshold, vector_int& indices, float eta = 1.f, int top_k = 0)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesBatched_10(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr class_ids_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta, int top_k);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesBatched_11(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr class_ids_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, float eta);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_NMSBoxesBatched_12(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr class_ids_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj);

        // C++:  void cv::dnn::softNMSBoxes(vector_Rect bboxes, vector_float scores, vector_float& updated_scores, float score_threshold, float nms_threshold, vector_int& indices, size_t top_k = 0, float sigma = 0.5, SoftNMSMethod method = SoftNMSMethod::SOFTNMS_GAUSSIAN)
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_softNMSBoxes_10(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr updated_scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, long top_k, float sigma);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_softNMSBoxes_12(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr updated_scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj, long top_k);
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_softNMSBoxes_13(IntPtr bboxes_mat_nativeObj, IntPtr scores_mat_nativeObj, IntPtr updated_scores_mat_nativeObj, float score_threshold, float nms_threshold, IntPtr indices_mat_nativeObj);

        // C++:  String cv::dnn::getInferenceEngineBackendType()
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_getInferenceEngineBackendType_10();

        // C++:  String cv::dnn::setInferenceEngineBackendType(String newBackendType)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_setInferenceEngineBackendType_10(string newBackendType);

        // C++:  void cv::dnn::resetMyriadDevice()
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_resetMyriadDevice_10();

        // C++:  String cv::dnn::getInferenceEngineVPUType()
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_getInferenceEngineVPUType_10();

        // C++:  String cv::dnn::getInferenceEngineCPUType()
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_Dnn_getInferenceEngineCPUType_10();

        // C++:  void cv::dnn::releaseHDDLPlugin()
        [DllImport(LIBNAME)]
        private static extern void dnn_Dnn_releaseHDDLPlugin_10();

    }
}


#endif