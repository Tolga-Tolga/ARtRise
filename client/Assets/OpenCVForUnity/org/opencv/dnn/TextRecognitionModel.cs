#if !UNITY_WSA_10_0


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{

    // C++: class TextRecognitionModel
    /// <summary>
    ///  This class represents high-level API for text recognition networks.
    /// </summary>
    /// <remarks>
    ///     TextRecognitionModel allows to set params for preprocessing input image.
    ///     TextRecognitionModel creates net from file with trained weights and config,
    ///     sets preprocessing input, runs forward pass and return recognition result.
    ///     For TextRecognitionModel, CRNN-CTC is supported.
    /// </remarks>
    public class TextRecognitionModel : Model
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
                        dnn_TextRecognitionModel_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TextRecognitionModel(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TextRecognitionModel __fromPtr__(IntPtr addr) { return new TextRecognitionModel(addr); }

        //
        // C++:   cv::dnn::TextRecognitionModel::TextRecognitionModel(Net network)
        //

        /// <summary>
        ///  Create Text Recognition model from deep learning network
        ///         Call setDecodeType() and setVocabulary() after constructor to initialize the decoding method
        /// </summary>
        /// <param name="network">
        /// Net object
        /// </param>
        public TextRecognitionModel(Net network) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_TextRecognitionModel_10(network.getNativeObjAddr())))
        {



        }


        //
        // C++:   cv::dnn::TextRecognitionModel::TextRecognitionModel(string model, string config = "")
        //

        /// <summary>
        ///  Create text recognition model from network represented in one of the supported formats
        ///         Call setDecodeType() and setVocabulary() after constructor to initialize the decoding method
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration
        /// </param>
        public TextRecognitionModel(string model, string config) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_TextRecognitionModel_11(model, config)))
        {



        }

        /// <summary>
        ///  Create text recognition model from network represented in one of the supported formats
        ///         Call setDecodeType() and setVocabulary() after constructor to initialize the decoding method
        /// </summary>
        /// <param name="model">
        /// Binary file contains trained weights
        /// </param>
        /// <param name="config">
        /// Text file contains network configuration
        /// </param>
        public TextRecognitionModel(string model) :
                    base(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_TextRecognitionModel_12(model)))
        {



        }


        //
        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setDecodeType(string decodeType)
        //

        /// <summary>
        ///  Set the decoding method of translating the network output into string
        /// </summary>
        /// <param name="decodeType">
        /// The decoding method of translating the network output into string, currently supported type:
        ///            - `"CTC-greedy"` greedy decoding for the output of CTC-based methods
        ///            - `"CTC-prefix-beam-search"` Prefix beam search decoding for the output of CTC-based methods
        /// </param>
        public TextRecognitionModel setDecodeType(string decodeType)
        {
            ThrowIfDisposed();

            return new TextRecognitionModel(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_setDecodeType_10(nativeObj, decodeType)));


        }


        //
        // C++:  string cv::dnn::TextRecognitionModel::getDecodeType()
        //

        /// <summary>
        ///  Get the decoding method
        /// </summary>
        /// <returns>
        ///  the decoding method
        /// </returns>
        public string getDecodeType()
        {
            ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_getDecodeType_10(nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setDecodeOptsCTCPrefixBeamSearch(int beamSize, int vocPruneSize = 0)
        //

        /// <summary>
        ///  Set the decoding method options for `"CTC-prefix-beam-search"` decode usage
        /// </summary>
        /// <param name="beamSize">
        /// Beam size for search
        /// </param>
        /// <param name="vocPruneSize">
        /// Parameter to optimize big vocabulary search,
        ///         only take top @p vocPruneSize tokens in each search step, @p vocPruneSize &lt;= 0 stands for disable this prune.
        /// </param>
        public TextRecognitionModel setDecodeOptsCTCPrefixBeamSearch(int beamSize, int vocPruneSize)
        {
            ThrowIfDisposed();

            return new TextRecognitionModel(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_setDecodeOptsCTCPrefixBeamSearch_10(nativeObj, beamSize, vocPruneSize)));


        }

        /// <summary>
        ///  Set the decoding method options for `"CTC-prefix-beam-search"` decode usage
        /// </summary>
        /// <param name="beamSize">
        /// Beam size for search
        /// </param>
        /// <param name="vocPruneSize">
        /// Parameter to optimize big vocabulary search,
        ///         only take top @p vocPruneSize tokens in each search step, @p vocPruneSize &lt;= 0 stands for disable this prune.
        /// </param>
        public TextRecognitionModel setDecodeOptsCTCPrefixBeamSearch(int beamSize)
        {
            ThrowIfDisposed();

            return new TextRecognitionModel(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_setDecodeOptsCTCPrefixBeamSearch_11(nativeObj, beamSize)));


        }


        //
        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setVocabulary(vector_string vocabulary)
        //

        /// <summary>
        ///  Set the vocabulary for recognition.
        /// </summary>
        /// <param name="vocabulary">
        /// the associated vocabulary of the network.
        /// </param>
        public TextRecognitionModel setVocabulary(List<string> vocabulary)
        {
            ThrowIfDisposed();
            using Mat vocabulary_mat = Converters.vector_string_to_Mat(vocabulary);
            return new TextRecognitionModel(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_setVocabulary_10(nativeObj, vocabulary_mat.nativeObj)));


        }


        //
        // C++:  vector_string cv::dnn::TextRecognitionModel::getVocabulary()
        //

        /// <summary>
        ///  Get the vocabulary for recognition.
        /// </summary>
        /// <returns>
        ///  vocabulary the associated vocabulary
        /// </returns>
        public List<string> getVocabulary()
        {
            ThrowIfDisposed();
            List<string> retVal = new List<string>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_getVocabulary_10(nativeObj)));
            Converters.Mat_to_vector_string(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  string cv::dnn::TextRecognitionModel::recognize(Mat frame)
        //

        /// <summary>
        ///  Given the @p input frame, create input blob, run net and return recognition result
        /// </summary>
        /// <param name="frame">
        /// The input image
        /// </param>
        /// <returns>
        ///  The text recognition result
        /// </returns>
        public string recognize(Mat frame)
        {
            ThrowIfDisposed();
            if (frame != null) frame.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(dnn_TextRecognitionModel_recognize_10(nativeObj, frame.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  void cv::dnn::TextRecognitionModel::recognize(Mat frame, vector_Mat roiRects, vector_string& results)
        //

        /// <summary>
        ///  Given the @p input frame, create input blob, run net and return recognition result
        /// </summary>
        /// <param name="frame">
        /// The input image
        /// </param>
        /// <param name="roiRects">
        /// List of text detection regions of interest (cv::Rect, CV_32SC4). ROIs is be cropped as the network inputs
        /// </param>
        /// <param name="results">
        /// A set of text recognition results.
        /// </param>
        public void recognize(Mat frame, List<Mat> roiRects, List<string> results)
        {
            ThrowIfDisposed();
            if (frame != null) frame.ThrowIfDisposed();
            using Mat roiRects_mat = Converters.vector_Mat_to_Mat(roiRects);
            using Mat results_mat = new Mat();
            dnn_TextRecognitionModel_recognize_11(nativeObj, frame.nativeObj, roiRects_mat.nativeObj, results_mat.nativeObj);
            Converters.Mat_to_vector_string(results_mat, results);

        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::dnn::TextRecognitionModel::TextRecognitionModel(Net network)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_TextRecognitionModel_10(IntPtr network_nativeObj);

        // C++:   cv::dnn::TextRecognitionModel::TextRecognitionModel(string model, string config = "")
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_TextRecognitionModel_11(string model, string config);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_TextRecognitionModel_12(string model);

        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setDecodeType(string decodeType)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_setDecodeType_10(IntPtr nativeObj, string decodeType);

        // C++:  string cv::dnn::TextRecognitionModel::getDecodeType()
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_getDecodeType_10(IntPtr nativeObj);

        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setDecodeOptsCTCPrefixBeamSearch(int beamSize, int vocPruneSize = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_setDecodeOptsCTCPrefixBeamSearch_10(IntPtr nativeObj, int beamSize, int vocPruneSize);
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_setDecodeOptsCTCPrefixBeamSearch_11(IntPtr nativeObj, int beamSize);

        // C++:  TextRecognitionModel cv::dnn::TextRecognitionModel::setVocabulary(vector_string vocabulary)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_setVocabulary_10(IntPtr nativeObj, IntPtr vocabulary_mat_nativeObj);

        // C++:  vector_string cv::dnn::TextRecognitionModel::getVocabulary()
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_getVocabulary_10(IntPtr nativeObj);

        // C++:  string cv::dnn::TextRecognitionModel::recognize(Mat frame)
        [DllImport(LIBNAME)]
        private static extern IntPtr dnn_TextRecognitionModel_recognize_10(IntPtr nativeObj, IntPtr frame_nativeObj);

        // C++:  void cv::dnn::TextRecognitionModel::recognize(Mat frame, vector_Mat roiRects, vector_string& results)
        [DllImport(LIBNAME)]
        private static extern void dnn_TextRecognitionModel_recognize_11(IntPtr nativeObj, IntPtr frame_nativeObj, IntPtr roiRects_mat_nativeObj, IntPtr results_mat_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void dnn_TextRecognitionModel_delete(IntPtr nativeObj);

    }
}


#endif