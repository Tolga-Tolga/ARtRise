#if !UNITY_WSA_10_0


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.TextModule
{

    // C++: class OCRHMMDecoder
    /// <summary>
    ///  OCRHMMDecoder class provides an interface for OCR using Hidden Markov Models.
    /// </summary>
    /// <remarks>
    ///  @note
    ///     -   (C++) An example on using OCRHMMDecoder recognition combined with scene text detection can
    ///          be found at the webcam_demo sample:
    ///          &lt;https://github.com/opencv/opencv_contrib/blob/master/modules/text/samples/webcam_demo.cpp&gt;
    /// </remarks>
    public class OCRHMMDecoder : BaseOCR
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
                        text_OCRHMMDecoder_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal OCRHMMDecoder(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new OCRHMMDecoder __fromPtr__(IntPtr addr) { return new OCRHMMDecoder(addr); }

        //
        // C++:  String cv::text::OCRHMMDecoder::run(Mat image, int min_confidence, int component_level = 0)
        //

        /// <summary>
        ///  Recognize text using HMM.
        /// </summary>
        /// <remarks>
        ///      Takes an image and a mask (where each connected component corresponds to a segmented character)
        ///      on input and returns recognized text in the output_text parameter. Optionally
        ///      provides also the Rects for individual text elements found (e.g. words), and the list of those
        ///      text elements with their confidence values.
        /// </remarks>
        /// <param name="image">
        /// Input image CV_8UC1 or CV_8UC3 with a single text line (or word).
        /// </param>
        /// <param name="mask">
        /// Input binary image CV_8UC1 same size as input image. Each connected component in mask corresponds to a segmented character in the input image.
        /// </param>
        /// <param name="output_text">
        /// Output text. Most likely character sequence found by the HMM decoder.
        /// </param>
        /// <param name="component_rects">
        /// If provided the method will output a list of Rects for the individual
        ///      text elements found (e.g. words).
        /// </param>
        /// <param name="component_texts">
        /// If provided the method will output a list of text strings for the
        ///      recognition of individual text elements found (e.g. words).
        /// </param>
        /// <param name="component_confidences">
        /// If provided the method will output a list of confidence values
        ///      for the recognition of individual text elements found (e.g. words).
        /// </param>
        /// <param name="component_level">
        /// Only OCR_LEVEL_WORD is supported.
        /// </param>
        public string run(Mat image, int min_confidence, int component_level)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_run_10(nativeObj, image.nativeObj, min_confidence, component_level)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }

        /// <summary>
        ///  Recognize text using HMM.
        /// </summary>
        /// <remarks>
        ///      Takes an image and a mask (where each connected component corresponds to a segmented character)
        ///      on input and returns recognized text in the output_text parameter. Optionally
        ///      provides also the Rects for individual text elements found (e.g. words), and the list of those
        ///      text elements with their confidence values.
        /// </remarks>
        /// <param name="image">
        /// Input image CV_8UC1 or CV_8UC3 with a single text line (or word).
        /// </param>
        /// <param name="mask">
        /// Input binary image CV_8UC1 same size as input image. Each connected component in mask corresponds to a segmented character in the input image.
        /// </param>
        /// <param name="output_text">
        /// Output text. Most likely character sequence found by the HMM decoder.
        /// </param>
        /// <param name="component_rects">
        /// If provided the method will output a list of Rects for the individual
        ///      text elements found (e.g. words).
        /// </param>
        /// <param name="component_texts">
        /// If provided the method will output a list of text strings for the
        ///      recognition of individual text elements found (e.g. words).
        /// </param>
        /// <param name="component_confidences">
        /// If provided the method will output a list of confidence values
        ///      for the recognition of individual text elements found (e.g. words).
        /// </param>
        /// <param name="component_level">
        /// Only OCR_LEVEL_WORD is supported.
        /// </param>
        public string run(Mat image, int min_confidence)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_run_11(nativeObj, image.nativeObj, min_confidence)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  String cv::text::OCRHMMDecoder::run(Mat image, Mat mask, int min_confidence, int component_level = 0)
        //

        public string run(Mat image, Mat mask, int min_confidence, int component_level)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_run_12(nativeObj, image.nativeObj, mask.nativeObj, min_confidence, component_level)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }

        public string run(Mat image, Mat mask, int min_confidence)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_run_13(nativeObj, image.nativeObj, mask.nativeObj, min_confidence)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++: static Ptr_OCRHMMDecoder cv::text::OCRHMMDecoder::create(Ptr_OCRHMMDecoder_ClassifierCallback classifier, String vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode = OCR_DECODER_VITERBI)
        //

        /// <summary>
        ///  Creates an instance of the OCRHMMDecoder class. Initializes HMMDecoder.
        /// </summary>
        /// <param name="classifier">
        /// The character classifier with built in feature extractor.
        /// </param>
        /// <param name="vocabulary">
        /// The language vocabulary (chars when ascii english text). vocabulary.size()
        ///      must be equal to the number of classes of the classifier.
        /// </param>
        /// <param name="transition_probabilities_table">
        /// Table with transition probabilities between character
        ///      pairs. cols == rows == vocabulary.size().
        /// </param>
        /// <param name="emission_probabilities_table">
        /// Table with observation emission probabilities. cols ==
        ///      rows == vocabulary.size().
        /// </param>
        /// <param name="mode">
        /// HMM Decoding algorithm. Only OCR_DECODER_VITERBI is available for the moment
        ///      (&lt;http://en.wikipedia.org/wiki/Viterbi_algorithm&gt;).
        /// </param>
        public static OCRHMMDecoder create(OCRHMMDecoder_ClassifierCallback classifier, string vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode)
        {
            if (classifier != null) classifier.ThrowIfDisposed();
            if (transition_probabilities_table != null) transition_probabilities_table.ThrowIfDisposed();
            if (emission_probabilities_table != null) emission_probabilities_table.ThrowIfDisposed();

            return OCRHMMDecoder.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_create_10(classifier.getNativeObjAddr(), vocabulary, transition_probabilities_table.nativeObj, emission_probabilities_table.nativeObj, mode)));


        }

        /// <summary>
        ///  Creates an instance of the OCRHMMDecoder class. Initializes HMMDecoder.
        /// </summary>
        /// <param name="classifier">
        /// The character classifier with built in feature extractor.
        /// </param>
        /// <param name="vocabulary">
        /// The language vocabulary (chars when ascii english text). vocabulary.size()
        ///      must be equal to the number of classes of the classifier.
        /// </param>
        /// <param name="transition_probabilities_table">
        /// Table with transition probabilities between character
        ///      pairs. cols == rows == vocabulary.size().
        /// </param>
        /// <param name="emission_probabilities_table">
        /// Table with observation emission probabilities. cols ==
        ///      rows == vocabulary.size().
        /// </param>
        /// <param name="mode">
        /// HMM Decoding algorithm. Only OCR_DECODER_VITERBI is available for the moment
        ///      (&lt;http://en.wikipedia.org/wiki/Viterbi_algorithm&gt;).
        /// </param>
        public static OCRHMMDecoder create(OCRHMMDecoder_ClassifierCallback classifier, string vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table)
        {
            if (classifier != null) classifier.ThrowIfDisposed();
            if (transition_probabilities_table != null) transition_probabilities_table.ThrowIfDisposed();
            if (emission_probabilities_table != null) emission_probabilities_table.ThrowIfDisposed();

            return OCRHMMDecoder.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_create_11(classifier.getNativeObjAddr(), vocabulary, transition_probabilities_table.nativeObj, emission_probabilities_table.nativeObj)));


        }


        //
        // C++: static Ptr_OCRHMMDecoder cv::text::OCRHMMDecoder::create(String filename, String vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode = OCR_DECODER_VITERBI, int classifier = OCR_KNN_CLASSIFIER)
        //

        /// <summary>
        ///  Creates an instance of the OCRHMMDecoder class. Loads and initializes HMMDecoder from the specified path
        /// </summary>
        /// <remarks>
        ///       @overload
        /// </remarks>
        public static OCRHMMDecoder create(string filename, string vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode, int classifier)
        {
            if (transition_probabilities_table != null) transition_probabilities_table.ThrowIfDisposed();
            if (emission_probabilities_table != null) emission_probabilities_table.ThrowIfDisposed();

            return OCRHMMDecoder.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_create_12(filename, vocabulary, transition_probabilities_table.nativeObj, emission_probabilities_table.nativeObj, mode, classifier)));


        }

        /// <summary>
        ///  Creates an instance of the OCRHMMDecoder class. Loads and initializes HMMDecoder from the specified path
        /// </summary>
        /// <remarks>
        ///       @overload
        /// </remarks>
        public static OCRHMMDecoder create(string filename, string vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode)
        {
            if (transition_probabilities_table != null) transition_probabilities_table.ThrowIfDisposed();
            if (emission_probabilities_table != null) emission_probabilities_table.ThrowIfDisposed();

            return OCRHMMDecoder.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_create_13(filename, vocabulary, transition_probabilities_table.nativeObj, emission_probabilities_table.nativeObj, mode)));


        }

        /// <summary>
        ///  Creates an instance of the OCRHMMDecoder class. Loads and initializes HMMDecoder from the specified path
        /// </summary>
        /// <remarks>
        ///       @overload
        /// </remarks>
        public static OCRHMMDecoder create(string filename, string vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table)
        {
            if (transition_probabilities_table != null) transition_probabilities_table.ThrowIfDisposed();
            if (emission_probabilities_table != null) emission_probabilities_table.ThrowIfDisposed();

            return OCRHMMDecoder.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_OCRHMMDecoder_create_14(filename, vocabulary, transition_probabilities_table.nativeObj, emission_probabilities_table.nativeObj)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  String cv::text::OCRHMMDecoder::run(Mat image, int min_confidence, int component_level = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_run_10(IntPtr nativeObj, IntPtr image_nativeObj, int min_confidence, int component_level);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_run_11(IntPtr nativeObj, IntPtr image_nativeObj, int min_confidence);

        // C++:  String cv::text::OCRHMMDecoder::run(Mat image, Mat mask, int min_confidence, int component_level = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_run_12(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr mask_nativeObj, int min_confidence, int component_level);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_run_13(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr mask_nativeObj, int min_confidence);

        // C++: static Ptr_OCRHMMDecoder cv::text::OCRHMMDecoder::create(Ptr_OCRHMMDecoder_ClassifierCallback classifier, String vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode = OCR_DECODER_VITERBI)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_create_10(IntPtr classifier_nativeObj, string vocabulary, IntPtr transition_probabilities_table_nativeObj, IntPtr emission_probabilities_table_nativeObj, int mode);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_create_11(IntPtr classifier_nativeObj, string vocabulary, IntPtr transition_probabilities_table_nativeObj, IntPtr emission_probabilities_table_nativeObj);

        // C++: static Ptr_OCRHMMDecoder cv::text::OCRHMMDecoder::create(String filename, String vocabulary, Mat transition_probabilities_table, Mat emission_probabilities_table, int mode = OCR_DECODER_VITERBI, int classifier = OCR_KNN_CLASSIFIER)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_create_12(string filename, string vocabulary, IntPtr transition_probabilities_table_nativeObj, IntPtr emission_probabilities_table_nativeObj, int mode, int classifier);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_create_13(string filename, string vocabulary, IntPtr transition_probabilities_table_nativeObj, IntPtr emission_probabilities_table_nativeObj, int mode);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_OCRHMMDecoder_create_14(string filename, string vocabulary, IntPtr transition_probabilities_table_nativeObj, IntPtr emission_probabilities_table_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void text_OCRHMMDecoder_delete(IntPtr nativeObj);

    }
}


#endif