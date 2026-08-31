#if !UNITY_WSA_10_0


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.TextModule
{
    // C++: class Text


    public class Text
    {

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int ERFILTER_NM_RGBLGrad = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int ERFILTER_NM_IHSGrad = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int OCR_LEVEL_WORD = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int OCR_LEVEL_TEXTLINE = 1;


        /// <summary>
        /// C++: enum classifier_type (cv.text.classifier_type)
        /// </summary>
        public const int OCR_KNN_CLASSIFIER = 0;

        /// <summary>
        /// C++: enum classifier_type (cv.text.classifier_type)
        /// </summary>
        public const int OCR_CNN_CLASSIFIER = 1;


        /// <summary>
        /// C++: enum decoder_mode (cv.text.decoder_mode)
        /// </summary>
        public const int OCR_DECODER_VITERBI = 0;


        /// <summary>
        /// C++: enum erGrouping_Modes (cv.text.erGrouping_Modes)
        /// </summary>
        public const int ERGROUPING_ORIENTATION_HORIZ = 0;

        /// <summary>
        /// C++: enum erGrouping_Modes (cv.text.erGrouping_Modes)
        /// </summary>
        public const int ERGROUPING_ORIENTATION_ANY = 1;


        /// <summary>
        /// C++: enum ocr_engine_mode (cv.text.ocr_engine_mode)
        /// </summary>
        public const int OEM_TESSERACT_ONLY = 0;

        /// <summary>
        /// C++: enum ocr_engine_mode (cv.text.ocr_engine_mode)
        /// </summary>
        public const int OEM_CUBE_ONLY = 1;

        /// <summary>
        /// C++: enum ocr_engine_mode (cv.text.ocr_engine_mode)
        /// </summary>
        public const int OEM_TESSERACT_CUBE_COMBINED = 2;

        /// <summary>
        /// C++: enum ocr_engine_mode (cv.text.ocr_engine_mode)
        /// </summary>
        public const int OEM_DEFAULT = 3;


        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_OSD_ONLY = 0;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_AUTO_OSD = 1;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_AUTO_ONLY = 2;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_AUTO = 3;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_COLUMN = 4;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_BLOCK_VERT_TEXT = 5;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_BLOCK = 6;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_LINE = 7;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_WORD = 8;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_CIRCLE_WORD = 9;

        /// <summary>
        /// C++: enum page_seg_mode (cv.text.page_seg_mode)
        /// </summary>
        public const int PSM_SINGLE_CHAR = 10;


        //
        // C++:  Ptr_ERFilter cv::text::createERFilterNM1(Ptr_ERFilter_Callback cb, int thresholdDelta = 1, float minArea = (float)0.00025, float maxArea = (float)0.13, float minProbability = (float)0.4, bool nonMaxSuppression = true, float minProbabilityDiff = (float)0.1)
        //

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta, float minArea, float maxArea, float minProbability, bool nonMaxSuppression, float minProbabilityDiff)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_10(cb.getNativeObjAddr(), thresholdDelta, minArea, maxArea, minProbability, nonMaxSuppression, minProbabilityDiff)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta, float minArea, float maxArea, float minProbability, bool nonMaxSuppression)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_11(cb.getNativeObjAddr(), thresholdDelta, minArea, maxArea, minProbability, nonMaxSuppression)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta, float minArea, float maxArea, float minProbability)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_12(cb.getNativeObjAddr(), thresholdDelta, minArea, maxArea, minProbability)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta, float minArea, float maxArea)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_13(cb.getNativeObjAddr(), thresholdDelta, minArea, maxArea)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta, float minArea)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_14(cb.getNativeObjAddr(), thresholdDelta, minArea)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb, int thresholdDelta)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_15(cb.getNativeObjAddr(), thresholdDelta)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM1, e.g. from file in samples/cpp/trained_classifierNM1.xml
        /// </param>
        /// <param name="thresholdDelta">
        /// :   Threshold step in subsequent thresholds when extracting the component tree
        /// </param>
        /// <param name="minArea">
        /// :   The minimum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="maxArea">
        /// :   The maximum area (% of image size) allowed for retreived ER's
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <param name="nonMaxSuppression">
        /// :   Whenever non-maximum suppression is done over the branch probabilities
        /// </param>
        /// <param name="minProbabilityDiff">
        /// :   The minimum probability difference between local maxima and local minima ERs
        /// </param>
        /// <remarks>
        ///  The component tree of the image is extracted by a threshold increased step by step from 0 to 255,
        ///  incrementally computable descriptors (aspect_ratio, compactness, number of holes, and number of
        ///  horizontal crossings) are computed for each ER and used as features for a classifier which estimates
        ///  the class-conditional probability P(er|character). The value of P(er|character) is tracked using the
        ///  inclusion relation of ER across all thresholds and only the ERs which correspond to local maximum of
        ///  the probability P(er|character) are selected (if the local maximum of the probability is above a
        ///  global limit pmin and the difference between local maximum and local minimum is greater than
        ///  minProbabilityDiff).
        /// </remarks>
        public static ERFilter createERFilterNM1(ERFilter_Callback cb)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_16(cb.getNativeObjAddr())));


        }


        //
        // C++:  Ptr_ERFilter cv::text::createERFilterNM2(Ptr_ERFilter_Callback cb, float minProbability = (float)0.3)
        //

        /// <summary>
        ///  Create an Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM2, e.g. from file in samples/cpp/trained_classifierNM2.xml
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <remarks>
        ///  In the second stage, the ERs that passed the first stage are classified into character and
        ///  non-character classes using more informative but also more computationally expensive features. The
        ///  classifier uses all the features calculated in the first stage and the following additional
        ///  features: hole area ratio, convex hull ratio, and number of outer inflexion points.
        /// </remarks>
        public static ERFilter createERFilterNM2(ERFilter_Callback cb, float minProbability)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM2_10(cb.getNativeObjAddr(), minProbability)));


        }

        /// <summary>
        ///  Create an Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="cb">
        /// :   Callback with the classifier. Default classifier can be implicitly load with function
        ///  loadClassifierNM2, e.g. from file in samples/cpp/trained_classifierNM2.xml
        /// </param>
        /// <param name="minProbability">
        /// :   The minimum probability P(er|character) allowed for retreived ER's
        /// </param>
        /// <remarks>
        ///  In the second stage, the ERs that passed the first stage are classified into character and
        ///  non-character classes using more informative but also more computationally expensive features. The
        ///  classifier uses all the features calculated in the first stage and the following additional
        ///  features: hole area ratio, convex hull ratio, and number of outer inflexion points.
        /// </remarks>
        public static ERFilter createERFilterNM2(ERFilter_Callback cb)
        {
            if (cb != null) cb.ThrowIfDisposed();

            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM2_11(cb.getNativeObjAddr())));


        }


        //
        // C++:  Ptr_ERFilter cv::text::createERFilterNM1(String filename, int thresholdDelta = 1, float minArea = (float)0.00025, float maxArea = (float)0.13, float minProbability = (float)0.4, bool nonMaxSuppression = true, float minProbabilityDiff = (float)0.1)
        //

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability, bool nonMaxSuppression, float minProbabilityDiff)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_17(filename, thresholdDelta, minArea, maxArea, minProbability, nonMaxSuppression, minProbabilityDiff)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability, bool nonMaxSuppression)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_18(filename, thresholdDelta, minArea, maxArea, minProbability, nonMaxSuppression)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_19(filename, thresholdDelta, minArea, maxArea, minProbability)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta, float minArea, float maxArea)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_110(filename, thresholdDelta, minArea, maxArea)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta, float minArea)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_111(filename, thresholdDelta, minArea)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename, int thresholdDelta)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_112(filename, thresholdDelta)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM1.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM1(string filename)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM1_113(filename)));


        }


        //
        // C++:  Ptr_ERFilter cv::text::createERFilterNM2(String filename, float minProbability = (float)0.3)
        //

        /// <summary>
        ///  Reads an Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM2.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM2(string filename, float minProbability)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM2_12(filename, minProbability)));


        }

        /// <summary>
        ///  Reads an Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm
        ///      from the provided path e.g. /path/to/cpp/trained_classifierNM2.xml
        /// </summary>
        /// <remarks>
        ///  @overload
        /// </remarks>
        public static ERFilter createERFilterNM2(string filename)
        {


            return ERFilter.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_createERFilterNM2_13(filename)));


        }


        //
        // C++:  Ptr_ERFilter_Callback cv::text::loadClassifierNM1(String filename)
        //

        /// <summary>
        ///  Allow to implicitly load the default classifier when creating an ERFilter object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. trained_classifierNM1.xml)
        /// </param>
        /// <remarks>
        ///  returns a pointer to ERFilter::Callback.
        /// </remarks>
        public static ERFilter_Callback loadClassifierNM1(string filename)
        {


            return ERFilter_Callback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadClassifierNM1_10(filename)));


        }


        //
        // C++:  Ptr_ERFilter_Callback cv::text::loadClassifierNM2(String filename)
        //

        /// <summary>
        ///  Allow to implicitly load the default classifier when creating an ERFilter object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. trained_classifierNM2.xml)
        /// </param>
        /// <remarks>
        ///  returns a pointer to ERFilter::Callback.
        /// </remarks>
        public static ERFilter_Callback loadClassifierNM2(string filename)
        {


            return ERFilter_Callback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadClassifierNM2_10(filename)));


        }


        //
        // C++:  void cv::text::computeNMChannels(Mat _src, vector_Mat& _channels, int _mode = ERFILTER_NM_RGBLGrad)
        //

        /// <summary>
        ///  Compute the different channels to be processed independently in the N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="_src">
        /// Source image. Must be RGB CV_8UC3.
        /// </param>
        /// <param name="_channels">
        /// Output vector&lt;Mat&gt; where computed channels are stored.
        /// </param>
        /// <param name="_mode">
        /// Mode of operation. Currently the only available options are:
        ///   *ERFILTER_NM_RGBLGrad** (used by default) and **ERFILTER_NM_IHSGrad**.
        /// </param>
        /// <remarks>
        ///  In N&amp;M algorithm, the combination of intensity (I), hue (H), saturation (S), and gradient magnitude
        ///  channels (Grad) are used in order to obtain high localization recall. This implementation also
        ///  provides an alternative combination of red (R), green (G), blue (B), lightness (L), and gradient
        ///  magnitude (Grad).
        /// </remarks>
        public static void computeNMChannels(Mat _src, List<Mat> _channels, int _mode)
        {
            if (_src != null) _src.ThrowIfDisposed();
            using Mat _channels_mat = new Mat();
            text_Text_computeNMChannels_10(_src.nativeObj, _channels_mat.nativeObj, _mode);
            Converters.Mat_to_vector_Mat(_channels_mat, _channels);

        }

        /// <summary>
        ///  Compute the different channels to be processed independently in the N&amp;M algorithm @cite Neumann12.
        /// </summary>
        /// <param name="_src">
        /// Source image. Must be RGB CV_8UC3.
        /// </param>
        /// <param name="_channels">
        /// Output vector&lt;Mat&gt; where computed channels are stored.
        /// </param>
        /// <param name="_mode">
        /// Mode of operation. Currently the only available options are:
        ///   *ERFILTER_NM_RGBLGrad** (used by default) and **ERFILTER_NM_IHSGrad**.
        /// </param>
        /// <remarks>
        ///  In N&amp;M algorithm, the combination of intensity (I), hue (H), saturation (S), and gradient magnitude
        ///  channels (Grad) are used in order to obtain high localization recall. This implementation also
        ///  provides an alternative combination of red (R), green (G), blue (B), lightness (L), and gradient
        ///  magnitude (Grad).
        /// </remarks>
        public static void computeNMChannels(Mat _src, List<Mat> _channels)
        {
            if (_src != null) _src.ThrowIfDisposed();
            using Mat _channels_mat = new Mat();
            text_Text_computeNMChannels_11(_src.nativeObj, _channels_mat.nativeObj);
            Converters.Mat_to_vector_Mat(_channels_mat, _channels);

        }


        //
        // C++:  void cv::text::erGrouping(Mat image, Mat channel, vector_vector_Point regions, vector_Rect& groups_rects, int method = ERGROUPING_ORIENTATION_HORIZ, String filename = String(), float minProbablity = (float)0.5)
        //

        /// <summary>
        ///  Find groups of Extremal Regions that are organized as text blocks.
        /// </summary>
        /// <param name="img">
        /// Original RGB or Greyscale image from wich the regions were extracted.
        /// </param>
        /// <param name="channels">
        /// Vector of single channel images CV_8UC1 from wich the regions were extracted.
        /// </param>
        /// <param name="regions">
        /// Vector of ER's retrieved from the ERFilter algorithm from each channel.
        /// </param>
        /// <param name="groups">
        /// The output of the algorithm is stored in this parameter as set of lists of indexes to
        ///  provided regions.
        /// </param>
        /// <param name="groups_rects">
        /// The output of the algorithm are stored in this parameter as list of rectangles.
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ,
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g.
        ///  samples/trained_classifier_erGrouping.xml). Only to use when grouping method is
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbablity">
        /// The minimum probability for accepting a group. Only to use when grouping
        ///  method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void erGrouping(Mat image, Mat channel, List<MatOfPoint> regions, MatOfRect groups_rects, int method, string filename, float minProbablity)
        {
            if (image != null) image.ThrowIfDisposed();
            if (channel != null) channel.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            using Mat regions_mat = Converters.vector_vector_Point_to_Mat(regions);
            Mat groups_rects_mat = groups_rects;
            text_Text_erGrouping_10(image.nativeObj, channel.nativeObj, regions_mat.nativeObj, groups_rects_mat.nativeObj, method, filename, minProbablity);


        }

        /// <summary>
        ///  Find groups of Extremal Regions that are organized as text blocks.
        /// </summary>
        /// <param name="img">
        /// Original RGB or Greyscale image from wich the regions were extracted.
        /// </param>
        /// <param name="channels">
        /// Vector of single channel images CV_8UC1 from wich the regions were extracted.
        /// </param>
        /// <param name="regions">
        /// Vector of ER's retrieved from the ERFilter algorithm from each channel.
        /// </param>
        /// <param name="groups">
        /// The output of the algorithm is stored in this parameter as set of lists of indexes to
        ///  provided regions.
        /// </param>
        /// <param name="groups_rects">
        /// The output of the algorithm are stored in this parameter as list of rectangles.
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ,
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g.
        ///  samples/trained_classifier_erGrouping.xml). Only to use when grouping method is
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbablity">
        /// The minimum probability for accepting a group. Only to use when grouping
        ///  method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void erGrouping(Mat image, Mat channel, List<MatOfPoint> regions, MatOfRect groups_rects, int method, string filename)
        {
            if (image != null) image.ThrowIfDisposed();
            if (channel != null) channel.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            using Mat regions_mat = Converters.vector_vector_Point_to_Mat(regions);
            Mat groups_rects_mat = groups_rects;
            text_Text_erGrouping_11(image.nativeObj, channel.nativeObj, regions_mat.nativeObj, groups_rects_mat.nativeObj, method, filename);


        }

        /// <summary>
        ///  Find groups of Extremal Regions that are organized as text blocks.
        /// </summary>
        /// <param name="img">
        /// Original RGB or Greyscale image from wich the regions were extracted.
        /// </param>
        /// <param name="channels">
        /// Vector of single channel images CV_8UC1 from wich the regions were extracted.
        /// </param>
        /// <param name="regions">
        /// Vector of ER's retrieved from the ERFilter algorithm from each channel.
        /// </param>
        /// <param name="groups">
        /// The output of the algorithm is stored in this parameter as set of lists of indexes to
        ///  provided regions.
        /// </param>
        /// <param name="groups_rects">
        /// The output of the algorithm are stored in this parameter as list of rectangles.
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ,
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g.
        ///  samples/trained_classifier_erGrouping.xml). Only to use when grouping method is
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbablity">
        /// The minimum probability for accepting a group. Only to use when grouping
        ///  method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void erGrouping(Mat image, Mat channel, List<MatOfPoint> regions, MatOfRect groups_rects, int method)
        {
            if (image != null) image.ThrowIfDisposed();
            if (channel != null) channel.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            using Mat regions_mat = Converters.vector_vector_Point_to_Mat(regions);
            Mat groups_rects_mat = groups_rects;
            text_Text_erGrouping_12(image.nativeObj, channel.nativeObj, regions_mat.nativeObj, groups_rects_mat.nativeObj, method);


        }

        /// <summary>
        ///  Find groups of Extremal Regions that are organized as text blocks.
        /// </summary>
        /// <param name="img">
        /// Original RGB or Greyscale image from wich the regions were extracted.
        /// </param>
        /// <param name="channels">
        /// Vector of single channel images CV_8UC1 from wich the regions were extracted.
        /// </param>
        /// <param name="regions">
        /// Vector of ER's retrieved from the ERFilter algorithm from each channel.
        /// </param>
        /// <param name="groups">
        /// The output of the algorithm is stored in this parameter as set of lists of indexes to
        ///  provided regions.
        /// </param>
        /// <param name="groups_rects">
        /// The output of the algorithm are stored in this parameter as list of rectangles.
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ,
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g.
        ///  samples/trained_classifier_erGrouping.xml). Only to use when grouping method is
        ///  ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbablity">
        /// The minimum probability for accepting a group. Only to use when grouping
        ///  method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void erGrouping(Mat image, Mat channel, List<MatOfPoint> regions, MatOfRect groups_rects)
        {
            if (image != null) image.ThrowIfDisposed();
            if (channel != null) channel.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            using Mat regions_mat = Converters.vector_vector_Point_to_Mat(regions);
            Mat groups_rects_mat = groups_rects;
            text_Text_erGrouping_13(image.nativeObj, channel.nativeObj, regions_mat.nativeObj, groups_rects_mat.nativeObj);


        }


        //
        // C++:  void cv::text::detectRegions(Mat image, Ptr_ERFilter er_filter1, Ptr_ERFilter er_filter2, vector_vector_Point& regions)
        //

        /// <summary>
        ///  Converts MSER contours (vector&lt;Point&gt;) to ERStat regions.
        /// </summary>
        /// <param name="image">
        /// Source image CV_8UC1 from which the MSERs where extracted.
        /// </param>
        /// <param name="contours">
        /// Input vector with all the contours (vector&lt;Point&gt;).
        /// </param>
        /// <param name="regions">
        /// Output where the ERStat regions are stored.
        /// </param>
        /// <remarks>
        ///  It takes as input the contours provided by the OpenCV MSER feature detector and returns as output
        ///  two vectors of ERStats. This is because MSER() output contains both MSER+ and MSER- regions in a
        ///  single vector&lt;Point&gt;, the function separates them in two different vectors (this is as if the
        ///  ERStats where extracted from two different channels).
        ///  
        ///  An example of MSERsToERStats in use can be found in the text detection webcam_demo:
        ///  &lt;https://github.com/opencv/opencv_contrib/blob/master/modules/text/samples/webcam_demo.cpp&gt;
        /// </remarks>
        public static void detectRegions(Mat image, ERFilter er_filter1, ERFilter er_filter2, List<MatOfPoint> regions)
        {
            if (image != null) image.ThrowIfDisposed();
            if (er_filter1 != null) er_filter1.ThrowIfDisposed();
            if (er_filter2 != null) er_filter2.ThrowIfDisposed();
            using Mat regions_mat = new Mat();
            text_Text_detectRegions_10(image.nativeObj, er_filter1.getNativeObjAddr(), er_filter2.getNativeObjAddr(), regions_mat.nativeObj);
            Converters.Mat_to_vector_vector_Point(regions_mat, regions);

        }


        //
        // C++:  void cv::text::detectRegions(Mat image, Ptr_ERFilter er_filter1, Ptr_ERFilter er_filter2, vector_Rect& groups_rects, int method = ERGROUPING_ORIENTATION_HORIZ, String filename = String(), float minProbability = (float)0.5)
        //

        /// <summary>
        ///  Extracts text regions from image.
        /// </summary>
        /// <param name="image">
        /// Source image where text blocks needs to be extracted from.  Should be CV_8UC3 (color).
        /// </param>
        /// <param name="er_filter1">
        /// Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="er_filter2">
        /// Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="groups_rects">
        /// Output list of rectangle blocks with text
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ, ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. samples/trained_classifier_erGrouping.xml). Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbability">
        /// The minimum probability for accepting a group. Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void detectRegions(Mat image, ERFilter er_filter1, ERFilter er_filter2, MatOfRect groups_rects, int method, string filename, float minProbability)
        {
            if (image != null) image.ThrowIfDisposed();
            if (er_filter1 != null) er_filter1.ThrowIfDisposed();
            if (er_filter2 != null) er_filter2.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            Mat groups_rects_mat = groups_rects;
            text_Text_detectRegions_11(image.nativeObj, er_filter1.getNativeObjAddr(), er_filter2.getNativeObjAddr(), groups_rects_mat.nativeObj, method, filename, minProbability);


        }

        /// <summary>
        ///  Extracts text regions from image.
        /// </summary>
        /// <param name="image">
        /// Source image where text blocks needs to be extracted from.  Should be CV_8UC3 (color).
        /// </param>
        /// <param name="er_filter1">
        /// Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="er_filter2">
        /// Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="groups_rects">
        /// Output list of rectangle blocks with text
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ, ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. samples/trained_classifier_erGrouping.xml). Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbability">
        /// The minimum probability for accepting a group. Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void detectRegions(Mat image, ERFilter er_filter1, ERFilter er_filter2, MatOfRect groups_rects, int method, string filename)
        {
            if (image != null) image.ThrowIfDisposed();
            if (er_filter1 != null) er_filter1.ThrowIfDisposed();
            if (er_filter2 != null) er_filter2.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            Mat groups_rects_mat = groups_rects;
            text_Text_detectRegions_12(image.nativeObj, er_filter1.getNativeObjAddr(), er_filter2.getNativeObjAddr(), groups_rects_mat.nativeObj, method, filename);


        }

        /// <summary>
        ///  Extracts text regions from image.
        /// </summary>
        /// <param name="image">
        /// Source image where text blocks needs to be extracted from.  Should be CV_8UC3 (color).
        /// </param>
        /// <param name="er_filter1">
        /// Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="er_filter2">
        /// Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="groups_rects">
        /// Output list of rectangle blocks with text
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ, ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. samples/trained_classifier_erGrouping.xml). Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbability">
        /// The minimum probability for accepting a group. Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void detectRegions(Mat image, ERFilter er_filter1, ERFilter er_filter2, MatOfRect groups_rects, int method)
        {
            if (image != null) image.ThrowIfDisposed();
            if (er_filter1 != null) er_filter1.ThrowIfDisposed();
            if (er_filter2 != null) er_filter2.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            Mat groups_rects_mat = groups_rects;
            text_Text_detectRegions_13(image.nativeObj, er_filter1.getNativeObjAddr(), er_filter2.getNativeObjAddr(), groups_rects_mat.nativeObj, method);


        }

        /// <summary>
        ///  Extracts text regions from image.
        /// </summary>
        /// <param name="image">
        /// Source image where text blocks needs to be extracted from.  Should be CV_8UC3 (color).
        /// </param>
        /// <param name="er_filter1">
        /// Extremal Region Filter for the 1st stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="er_filter2">
        /// Extremal Region Filter for the 2nd stage classifier of N&amp;M algorithm @cite Neumann12
        /// </param>
        /// <param name="groups_rects">
        /// Output list of rectangle blocks with text
        /// </param>
        /// <param name="method">
        /// Grouping method (see text::erGrouping_Modes). Can be one of ERGROUPING_ORIENTATION_HORIZ, ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. samples/trained_classifier_erGrouping.xml). Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        /// <param name="minProbability">
        /// The minimum probability for accepting a group. Only to use when grouping method is ERGROUPING_ORIENTATION_ANY.
        /// </param>
        public static void detectRegions(Mat image, ERFilter er_filter1, ERFilter er_filter2, MatOfRect groups_rects)
        {
            if (image != null) image.ThrowIfDisposed();
            if (er_filter1 != null) er_filter1.ThrowIfDisposed();
            if (er_filter2 != null) er_filter2.ThrowIfDisposed();
            if (groups_rects != null) groups_rects.ThrowIfDisposed();
            Mat groups_rects_mat = groups_rects;
            text_Text_detectRegions_14(image.nativeObj, er_filter1.getNativeObjAddr(), er_filter2.getNativeObjAddr(), groups_rects_mat.nativeObj);


        }


        //
        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifierNM(String filename)
        //

        /// <summary>
        ///  Allow to implicitly load the default character classifier when creating an OCRHMMDecoder object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. OCRHMM_knn_model_data.xml)
        /// </param>
        /// <remarks>
        ///  The KNN default classifier is based in the scene text recognition method proposed by Lukás Neumann &amp;
        ///  Jiri Matas in [Neumann11b]. Basically, the region (contour) in the input image is normalized to a
        ///  fixed size, while retaining the centroid and aspect ratio, in order to extract a feature vector
        ///  based on gradient orientations along the chain-code of its perimeter. Then, the region is classified
        ///  using a KNN model trained with synthetic data of rendered characters with different standard font
        ///  types.
        ///  
        ///  @deprecated loadOCRHMMClassifier instead
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static OCRHMMDecoder_ClassifierCallback loadOCRHMMClassifierNM(string filename)
        {


            return OCRHMMDecoder_ClassifierCallback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadOCRHMMClassifierNM_10(filename)));


        }


        //
        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifierCNN(String filename)
        //

        /// <summary>
        ///  Allow to implicitly load the default character classifier when creating an OCRHMMDecoder object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. OCRBeamSearch_CNN_model_data.xml.gz)
        /// </param>
        /// <remarks>
        ///  The CNN default classifier is based in the scene text recognition method proposed by Adam Coates &amp;
        ///  Andrew NG in [Coates11a]. The character classifier consists in a Single Layer Convolutional Neural Network and
        ///  a linear classifier. It is applied to the input image in a sliding window fashion, providing a set of recognitions
        ///  at each window location.
        ///  
        ///  @deprecated use loadOCRHMMClassifier instead
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static OCRHMMDecoder_ClassifierCallback loadOCRHMMClassifierCNN(string filename)
        {


            return OCRHMMDecoder_ClassifierCallback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadOCRHMMClassifierCNN_10(filename)));


        }


        //
        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifier(String filename, int classifier)
        //

        /// <summary>
        ///  Allow to implicitly load the default character classifier when creating an OCRHMMDecoder object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. OCRBeamSearch_CNN_model_data.xml.gz)
        /// </param>
        /// <param name="classifier">
        /// Can be one of classifier_type enum values.
        /// </param>
        public static OCRHMMDecoder_ClassifierCallback loadOCRHMMClassifier(string filename, int classifier)
        {


            return OCRHMMDecoder_ClassifierCallback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadOCRHMMClassifier_10(filename, classifier)));


        }


        //
        // C++:  Mat cv::text::createOCRHMMTransitionsTable(String vocabulary, vector_String lexicon)
        //

        /// <summary>
        ///  Utility function to create a tailored language model transitions table from a given list of words (lexicon).
        /// </summary>
        /// <param name="vocabulary">
        /// The language vocabulary (chars when ASCII English text).
        /// </param>
        /// <param name="lexicon">
        /// The list of words that are expected to be found in a particular image.
        /// </param>
        /// <param name="transition_probabilities_table">
        /// Output table with transition probabilities between character pairs. cols == rows == vocabulary.size().
        /// </param>
        /// <remarks>
        ///     The function calculate frequency statistics of character pairs from the given lexicon and fills the output transition_probabilities_table with them. The transition_probabilities_table can be used as input in the OCRHMMDecoder::create() and OCRBeamSearchDecoder::create() methods.
        ///     @note
        ///        -   (C++) An alternative would be to load the default generic language transition table provided in the text module samples folder (created from ispell 42869 english words list) :
        ///                &lt;https://github.com/opencv/opencv_contrib/blob/master/modules/text/samples/OCRHMM_transitions_table.xml&gt;
        /// </remarks>
        public static Mat createOCRHMMTransitionsTable(string vocabulary, List<string> lexicon)
        {

            using Mat lexicon_mat = Converters.vector_String_to_Mat(lexicon);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(text_Text_createOCRHMMTransitionsTable_10(vocabulary, lexicon_mat.nativeObj)));


        }


        //
        // C++:  Ptr_OCRBeamSearchDecoder_ClassifierCallback cv::text::loadOCRBeamSearchClassifierCNN(String filename)
        //

        /// <summary>
        ///  Allow to implicitly load the default character classifier when creating an OCRBeamSearchDecoder object.
        /// </summary>
        /// <param name="filename">
        /// The XML or YAML file with the classifier model (e.g. OCRBeamSearch_CNN_model_data.xml.gz)
        /// </param>
        /// <remarks>
        ///  The CNN default classifier is based in the scene text recognition method proposed by Adam Coates &amp;
        ///  Andrew NG in [Coates11a]. The character classifier consists in a Single Layer Convolutional Neural Network and
        ///  a linear classifier. It is applied to the input image in a sliding window fashion, providing a set of recognitions
        ///  at each window location.
        /// </remarks>
        public static OCRBeamSearchDecoder_ClassifierCallback loadOCRBeamSearchClassifierCNN(string filename)
        {


            return OCRBeamSearchDecoder_ClassifierCallback.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(text_Text_loadOCRBeamSearchClassifierCNN_10(filename)));


        }


        //
        // C++:  void cv::text::detectTextSWT(Mat input, vector_Rect& result, bool dark_on_light, Mat& draw = Mat(), Mat& chainBBs = Mat())
        //

        /// <summary>
        ///  Applies the Stroke Width Transform operator followed by filtering of connected components of similar Stroke Widths to return letter candidates. It also chain them by proximity and size, saving the result in chainBBs.
        /// </summary>
        /// <param name="input">
        /// the input image with 3 channels.
        /// </param>
        /// <param name="result">
        /// a vector of resulting bounding boxes where probability of finding text is high
        /// </param>
        /// <param name="dark_on_light">
        /// a boolean value signifying whether the text is darker or lighter than the background, it is observed to reverse the gradient obtained from Scharr operator, and significantly affect the result.
        /// </param>
        /// <param name="draw">
        /// an optional Mat of type CV_8UC3 which visualises the detected letters using bounding boxes.
        /// </param>
        /// <param name="chainBBs">
        /// an optional parameter which chains the letter candidates according to heuristics in the paper and returns all possible regions where text is likely to occur.
        /// </param>
        public static void detectTextSWT(Mat input, MatOfRect result, bool dark_on_light, Mat draw, Mat chainBBs)
        {
            if (input != null) input.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (draw != null) draw.ThrowIfDisposed();
            if (chainBBs != null) chainBBs.ThrowIfDisposed();
            Mat result_mat = result;
            text_Text_detectTextSWT_10(input.nativeObj, result_mat.nativeObj, dark_on_light, draw.nativeObj, chainBBs.nativeObj);


        }

        /// <summary>
        ///  Applies the Stroke Width Transform operator followed by filtering of connected components of similar Stroke Widths to return letter candidates. It also chain them by proximity and size, saving the result in chainBBs.
        /// </summary>
        /// <param name="input">
        /// the input image with 3 channels.
        /// </param>
        /// <param name="result">
        /// a vector of resulting bounding boxes where probability of finding text is high
        /// </param>
        /// <param name="dark_on_light">
        /// a boolean value signifying whether the text is darker or lighter than the background, it is observed to reverse the gradient obtained from Scharr operator, and significantly affect the result.
        /// </param>
        /// <param name="draw">
        /// an optional Mat of type CV_8UC3 which visualises the detected letters using bounding boxes.
        /// </param>
        /// <param name="chainBBs">
        /// an optional parameter which chains the letter candidates according to heuristics in the paper and returns all possible regions where text is likely to occur.
        /// </param>
        public static void detectTextSWT(Mat input, MatOfRect result, bool dark_on_light, Mat draw)
        {
            if (input != null) input.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            if (draw != null) draw.ThrowIfDisposed();
            Mat result_mat = result;
            text_Text_detectTextSWT_11(input.nativeObj, result_mat.nativeObj, dark_on_light, draw.nativeObj);


        }

        /// <summary>
        ///  Applies the Stroke Width Transform operator followed by filtering of connected components of similar Stroke Widths to return letter candidates. It also chain them by proximity and size, saving the result in chainBBs.
        /// </summary>
        /// <param name="input">
        /// the input image with 3 channels.
        /// </param>
        /// <param name="result">
        /// a vector of resulting bounding boxes where probability of finding text is high
        /// </param>
        /// <param name="dark_on_light">
        /// a boolean value signifying whether the text is darker or lighter than the background, it is observed to reverse the gradient obtained from Scharr operator, and significantly affect the result.
        /// </param>
        /// <param name="draw">
        /// an optional Mat of type CV_8UC3 which visualises the detected letters using bounding boxes.
        /// </param>
        /// <param name="chainBBs">
        /// an optional parameter which chains the letter candidates according to heuristics in the paper and returns all possible regions where text is likely to occur.
        /// </param>
        public static void detectTextSWT(Mat input, MatOfRect result, bool dark_on_light)
        {
            if (input != null) input.ThrowIfDisposed();
            if (result != null) result.ThrowIfDisposed();
            Mat result_mat = result;
            text_Text_detectTextSWT_12(input.nativeObj, result_mat.nativeObj, dark_on_light);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  Ptr_ERFilter cv::text::createERFilterNM1(Ptr_ERFilter_Callback cb, int thresholdDelta = 1, float minArea = (float)0.00025, float maxArea = (float)0.13, float minProbability = (float)0.4, bool nonMaxSuppression = true, float minProbabilityDiff = (float)0.1)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_10(IntPtr cb_nativeObj, int thresholdDelta, float minArea, float maxArea, float minProbability, [MarshalAs(UnmanagedType.U1)] bool nonMaxSuppression, float minProbabilityDiff);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_11(IntPtr cb_nativeObj, int thresholdDelta, float minArea, float maxArea, float minProbability, [MarshalAs(UnmanagedType.U1)] bool nonMaxSuppression);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_12(IntPtr cb_nativeObj, int thresholdDelta, float minArea, float maxArea, float minProbability);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_13(IntPtr cb_nativeObj, int thresholdDelta, float minArea, float maxArea);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_14(IntPtr cb_nativeObj, int thresholdDelta, float minArea);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_15(IntPtr cb_nativeObj, int thresholdDelta);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_16(IntPtr cb_nativeObj);

        // C++:  Ptr_ERFilter cv::text::createERFilterNM2(Ptr_ERFilter_Callback cb, float minProbability = (float)0.3)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM2_10(IntPtr cb_nativeObj, float minProbability);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM2_11(IntPtr cb_nativeObj);

        // C++:  Ptr_ERFilter cv::text::createERFilterNM1(String filename, int thresholdDelta = 1, float minArea = (float)0.00025, float maxArea = (float)0.13, float minProbability = (float)0.4, bool nonMaxSuppression = true, float minProbabilityDiff = (float)0.1)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_17(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability, [MarshalAs(UnmanagedType.U1)] bool nonMaxSuppression, float minProbabilityDiff);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_18(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability, [MarshalAs(UnmanagedType.U1)] bool nonMaxSuppression);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_19(string filename, int thresholdDelta, float minArea, float maxArea, float minProbability);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_110(string filename, int thresholdDelta, float minArea, float maxArea);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_111(string filename, int thresholdDelta, float minArea);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_112(string filename, int thresholdDelta);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM1_113(string filename);

        // C++:  Ptr_ERFilter cv::text::createERFilterNM2(String filename, float minProbability = (float)0.3)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM2_12(string filename, float minProbability);
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createERFilterNM2_13(string filename);

        // C++:  Ptr_ERFilter_Callback cv::text::loadClassifierNM1(String filename)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadClassifierNM1_10(string filename);

        // C++:  Ptr_ERFilter_Callback cv::text::loadClassifierNM2(String filename)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadClassifierNM2_10(string filename);

        // C++:  void cv::text::computeNMChannels(Mat _src, vector_Mat& _channels, int _mode = ERFILTER_NM_RGBLGrad)
        [DllImport(LIBNAME)]
        private static extern void text_Text_computeNMChannels_10(IntPtr _src_nativeObj, IntPtr _channels_mat_nativeObj, int _mode);
        [DllImport(LIBNAME)]
        private static extern void text_Text_computeNMChannels_11(IntPtr _src_nativeObj, IntPtr _channels_mat_nativeObj);

        // C++:  void cv::text::erGrouping(Mat image, Mat channel, vector_vector_Point regions, vector_Rect& groups_rects, int method = ERGROUPING_ORIENTATION_HORIZ, String filename = String(), float minProbablity = (float)0.5)
        [DllImport(LIBNAME)]
        private static extern void text_Text_erGrouping_10(IntPtr image_nativeObj, IntPtr channel_nativeObj, IntPtr regions_mat_nativeObj, IntPtr groups_rects_mat_nativeObj, int method, string filename, float minProbablity);
        [DllImport(LIBNAME)]
        private static extern void text_Text_erGrouping_11(IntPtr image_nativeObj, IntPtr channel_nativeObj, IntPtr regions_mat_nativeObj, IntPtr groups_rects_mat_nativeObj, int method, string filename);
        [DllImport(LIBNAME)]
        private static extern void text_Text_erGrouping_12(IntPtr image_nativeObj, IntPtr channel_nativeObj, IntPtr regions_mat_nativeObj, IntPtr groups_rects_mat_nativeObj, int method);
        [DllImport(LIBNAME)]
        private static extern void text_Text_erGrouping_13(IntPtr image_nativeObj, IntPtr channel_nativeObj, IntPtr regions_mat_nativeObj, IntPtr groups_rects_mat_nativeObj);

        // C++:  void cv::text::detectRegions(Mat image, Ptr_ERFilter er_filter1, Ptr_ERFilter er_filter2, vector_vector_Point& regions)
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectRegions_10(IntPtr image_nativeObj, IntPtr er_filter1_nativeObj, IntPtr er_filter2_nativeObj, IntPtr regions_mat_nativeObj);

        // C++:  void cv::text::detectRegions(Mat image, Ptr_ERFilter er_filter1, Ptr_ERFilter er_filter2, vector_Rect& groups_rects, int method = ERGROUPING_ORIENTATION_HORIZ, String filename = String(), float minProbability = (float)0.5)
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectRegions_11(IntPtr image_nativeObj, IntPtr er_filter1_nativeObj, IntPtr er_filter2_nativeObj, IntPtr groups_rects_mat_nativeObj, int method, string filename, float minProbability);
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectRegions_12(IntPtr image_nativeObj, IntPtr er_filter1_nativeObj, IntPtr er_filter2_nativeObj, IntPtr groups_rects_mat_nativeObj, int method, string filename);
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectRegions_13(IntPtr image_nativeObj, IntPtr er_filter1_nativeObj, IntPtr er_filter2_nativeObj, IntPtr groups_rects_mat_nativeObj, int method);
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectRegions_14(IntPtr image_nativeObj, IntPtr er_filter1_nativeObj, IntPtr er_filter2_nativeObj, IntPtr groups_rects_mat_nativeObj);

        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifierNM(String filename)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadOCRHMMClassifierNM_10(string filename);

        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifierCNN(String filename)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadOCRHMMClassifierCNN_10(string filename);

        // C++:  Ptr_OCRHMMDecoder_ClassifierCallback cv::text::loadOCRHMMClassifier(String filename, int classifier)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadOCRHMMClassifier_10(string filename, int classifier);

        // C++:  Mat cv::text::createOCRHMMTransitionsTable(String vocabulary, vector_String lexicon)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_createOCRHMMTransitionsTable_10(string vocabulary, IntPtr lexicon_mat_nativeObj);

        // C++:  Ptr_OCRBeamSearchDecoder_ClassifierCallback cv::text::loadOCRBeamSearchClassifierCNN(String filename)
        [DllImport(LIBNAME)]
        private static extern IntPtr text_Text_loadOCRBeamSearchClassifierCNN_10(string filename);

        // C++:  void cv::text::detectTextSWT(Mat input, vector_Rect& result, bool dark_on_light, Mat& draw = Mat(), Mat& chainBBs = Mat())
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectTextSWT_10(IntPtr input_nativeObj, IntPtr result_mat_nativeObj, [MarshalAs(UnmanagedType.U1)] bool dark_on_light, IntPtr draw_nativeObj, IntPtr chainBBs_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectTextSWT_11(IntPtr input_nativeObj, IntPtr result_mat_nativeObj, [MarshalAs(UnmanagedType.U1)] bool dark_on_light, IntPtr draw_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void text_Text_detectTextSWT_12(IntPtr input_nativeObj, IntPtr result_mat_nativeObj, [MarshalAs(UnmanagedType.U1)] bool dark_on_light);

    }
}


#endif