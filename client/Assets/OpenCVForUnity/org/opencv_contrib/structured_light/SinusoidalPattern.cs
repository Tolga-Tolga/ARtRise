#if !UNITY_WEBGL


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Structured_lightModule
{

    // C++: class SinusoidalPattern
    /// <summary>
    ///  Class implementing Fourier transform profilometry (FTP) , phase-shifting profilometry (PSP)
    ///     and Fourier-assisted phase-shifting profilometry (FAPS) based on @cite faps.
    /// </summary>
    /// <remarks>
    ///     This class generates sinusoidal patterns that can be used with FTP, PSP and FAPS.
    /// </remarks>
    public partial class SinusoidalPattern : StructuredLightPattern
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
                        structured_1light_SinusoidalPattern_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal SinusoidalPattern(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new SinusoidalPattern __fromPtr__(IntPtr addr) { return new SinusoidalPattern(addr); }

        //
        // C++: static Ptr_SinusoidalPattern cv::structured_light::SinusoidalPattern::create(Ptr_SinusoidalPattern_Params parameters = makePtr<SinusoidalPattern::Params>())
        //

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="parameters">
        /// SinusoidalPattern parameters SinusoidalPattern::Params: width, height of the projector and patterns parameters.
        /// </param>
        public static SinusoidalPattern create(SinusoidalPattern_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return SinusoidalPattern.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(structured_1light_SinusoidalPattern_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="parameters">
        /// SinusoidalPattern parameters SinusoidalPattern::Params: width, height of the projector and patterns parameters.
        /// </param>
        public static SinusoidalPattern create()
        {


            return SinusoidalPattern.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(structured_1light_SinusoidalPattern_create_11()));


        }


        //
        // C++:  void cv::structured_light::SinusoidalPattern::computePhaseMap(vector_Mat patternImages, Mat& wrappedPhaseMap, Mat& shadowMask = Mat(), Mat fundamental = Mat())
        //

        /// <summary>
        ///  Compute a wrapped phase map from sinusoidal patterns.
        /// </summary>
        /// <param name="patternImages">
        /// Input data to compute the wrapped phase map.
        /// </param>
        /// <param name="wrappedPhaseMap">
        /// Wrapped phase map obtained through one of the three methods.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        /// <param name="fundamental">
        /// Fundamental matrix used to compute epipolar lines and ease the matching step.
        /// </param>
        public void computePhaseMap(List<Mat> patternImages, Mat wrappedPhaseMap, Mat shadowMask, Mat fundamental)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();
            if (fundamental != null) fundamental.ThrowIfDisposed();
            using Mat patternImages_mat = Converters.vector_Mat_to_Mat(patternImages);
            structured_1light_SinusoidalPattern_computePhaseMap_10(nativeObj, patternImages_mat.nativeObj, wrappedPhaseMap.nativeObj, shadowMask.nativeObj, fundamental.nativeObj);


        }

        /// <summary>
        ///  Compute a wrapped phase map from sinusoidal patterns.
        /// </summary>
        /// <param name="patternImages">
        /// Input data to compute the wrapped phase map.
        /// </param>
        /// <param name="wrappedPhaseMap">
        /// Wrapped phase map obtained through one of the three methods.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        /// <param name="fundamental">
        /// Fundamental matrix used to compute epipolar lines and ease the matching step.
        /// </param>
        public void computePhaseMap(List<Mat> patternImages, Mat wrappedPhaseMap, Mat shadowMask)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();
            using Mat patternImages_mat = Converters.vector_Mat_to_Mat(patternImages);
            structured_1light_SinusoidalPattern_computePhaseMap_11(nativeObj, patternImages_mat.nativeObj, wrappedPhaseMap.nativeObj, shadowMask.nativeObj);


        }

        /// <summary>
        ///  Compute a wrapped phase map from sinusoidal patterns.
        /// </summary>
        /// <param name="patternImages">
        /// Input data to compute the wrapped phase map.
        /// </param>
        /// <param name="wrappedPhaseMap">
        /// Wrapped phase map obtained through one of the three methods.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        /// <param name="fundamental">
        /// Fundamental matrix used to compute epipolar lines and ease the matching step.
        /// </param>
        public void computePhaseMap(List<Mat> patternImages, Mat wrappedPhaseMap)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            using Mat patternImages_mat = Converters.vector_Mat_to_Mat(patternImages);
            structured_1light_SinusoidalPattern_computePhaseMap_12(nativeObj, patternImages_mat.nativeObj, wrappedPhaseMap.nativeObj);


        }


        //
        // C++:  void cv::structured_light::SinusoidalPattern::unwrapPhaseMap(Mat wrappedPhaseMap, Mat& unwrappedPhaseMap, Size camSize, Mat shadowMask = Mat())
        //

        /// <summary>
        ///  Unwrap the wrapped phase map to remove phase ambiguities.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map computed from the pattern.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map used to find correspondences between the two devices.
        /// </param>
        /// <param name="camSize">
        /// Resolution of the camera.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap, Size camSize, Mat shadowMask)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();

            structured_1light_SinusoidalPattern_unwrapPhaseMap_10(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj, camSize.width, camSize.height, shadowMask.nativeObj);


        }

        /// <summary>
        ///  Unwrap the wrapped phase map to remove phase ambiguities.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map computed from the pattern.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map used to find correspondences between the two devices.
        /// </param>
        /// <param name="camSize">
        /// Resolution of the camera.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap, Size camSize)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();

            structured_1light_SinusoidalPattern_unwrapPhaseMap_11(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj, camSize.width, camSize.height);


        }


        //
        // C++:  void cv::structured_light::SinusoidalPattern::findProCamMatches(Mat projUnwrappedPhaseMap, Mat camUnwrappedPhaseMap, vector_Mat& matches)
        //

        /// <summary>
        ///  Find correspondences between the two devices thanks to unwrapped phase maps.
        /// </summary>
        /// <param name="projUnwrappedPhaseMap">
        /// Projector's unwrapped phase map.
        /// </param>
        /// <param name="camUnwrappedPhaseMap">
        /// Camera's unwrapped phase map.
        /// </param>
        /// <param name="matches">
        /// Images used to display correspondences map.
        /// </param>
        public void findProCamMatches(Mat projUnwrappedPhaseMap, Mat camUnwrappedPhaseMap, List<Mat> matches)
        {
            ThrowIfDisposed();
            if (projUnwrappedPhaseMap != null) projUnwrappedPhaseMap.ThrowIfDisposed();
            if (camUnwrappedPhaseMap != null) camUnwrappedPhaseMap.ThrowIfDisposed();
            using Mat matches_mat = new Mat();
            structured_1light_SinusoidalPattern_findProCamMatches_10(nativeObj, projUnwrappedPhaseMap.nativeObj, camUnwrappedPhaseMap.nativeObj, matches_mat.nativeObj);
            Converters.Mat_to_vector_Mat(matches_mat, matches);

        }


        //
        // C++:  void cv::structured_light::SinusoidalPattern::computeDataModulationTerm(vector_Mat patternImages, Mat& dataModulationTerm, Mat shadowMask)
        //

        /// <summary>
        ///  compute the data modulation term.
        /// </summary>
        /// <param name="patternImages">
        /// captured images with projected patterns.
        /// </param>
        /// <param name="dataModulationTerm">
        /// Mat where the data modulation term is saved.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        public void computeDataModulationTerm(List<Mat> patternImages, Mat dataModulationTerm, Mat shadowMask)
        {
            ThrowIfDisposed();
            if (dataModulationTerm != null) dataModulationTerm.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();
            using Mat patternImages_mat = Converters.vector_Mat_to_Mat(patternImages);
            structured_1light_SinusoidalPattern_computeDataModulationTerm_10(nativeObj, patternImages_mat.nativeObj, dataModulationTerm.nativeObj, shadowMask.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_SinusoidalPattern cv::structured_light::SinusoidalPattern::create(Ptr_SinusoidalPattern_Params parameters = makePtr<SinusoidalPattern::Params>())
        [DllImport(LIBNAME)]
        private static extern IntPtr structured_1light_SinusoidalPattern_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr structured_1light_SinusoidalPattern_create_11();

        // C++:  void cv::structured_light::SinusoidalPattern::computePhaseMap(vector_Mat patternImages, Mat& wrappedPhaseMap, Mat& shadowMask = Mat(), Mat fundamental = Mat())
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_computePhaseMap_10(IntPtr nativeObj, IntPtr patternImages_mat_nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr shadowMask_nativeObj, IntPtr fundamental_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_computePhaseMap_11(IntPtr nativeObj, IntPtr patternImages_mat_nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr shadowMask_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_computePhaseMap_12(IntPtr nativeObj, IntPtr patternImages_mat_nativeObj, IntPtr wrappedPhaseMap_nativeObj);

        // C++:  void cv::structured_light::SinusoidalPattern::unwrapPhaseMap(Mat wrappedPhaseMap, Mat& unwrappedPhaseMap, Size camSize, Mat shadowMask = Mat())
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_unwrapPhaseMap_10(IntPtr nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr unwrappedPhaseMap_nativeObj, double camSize_width, double camSize_height, IntPtr shadowMask_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_unwrapPhaseMap_11(IntPtr nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr unwrappedPhaseMap_nativeObj, double camSize_width, double camSize_height);

        // C++:  void cv::structured_light::SinusoidalPattern::findProCamMatches(Mat projUnwrappedPhaseMap, Mat camUnwrappedPhaseMap, vector_Mat& matches)
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_findProCamMatches_10(IntPtr nativeObj, IntPtr projUnwrappedPhaseMap_nativeObj, IntPtr camUnwrappedPhaseMap_nativeObj, IntPtr matches_mat_nativeObj);

        // C++:  void cv::structured_light::SinusoidalPattern::computeDataModulationTerm(vector_Mat patternImages, Mat& dataModulationTerm, Mat shadowMask)
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_computeDataModulationTerm_10(IntPtr nativeObj, IntPtr patternImages_mat_nativeObj, IntPtr dataModulationTerm_nativeObj, IntPtr shadowMask_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void structured_1light_SinusoidalPattern_delete(IntPtr nativeObj);

    }
}


#endif