

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    // C++: class IntelligentScissorsMB
    /// <summary>
    ///  Intelligent Scissors image segmentation
    /// </summary>
    /// <remarks>
    ///     This class is used to find the path (contour) between two points
    ///     which can be used for image segmentation.
    ///    
    ///     Usage example:
    ///     @snippet snippets/imgproc_segmentation.cpp usage_example_intelligent_scissors
    ///    
    ///     Reference: <a href="http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.138.3811&rep=rep1&type=pdf">"Intelligent Scissors for Image Composition"</a>
    ///     algorithm designed by Eric N. Mortensen and William A. Barrett, Brigham Young University
    ///     @cite Mortensen95intelligentscissors
    /// </remarks>
    public partial class IntelligentScissorsMB : DisposableOpenCVObject
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
                        imgproc_IntelligentScissorsMB_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal IntelligentScissorsMB(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static IntelligentScissorsMB __fromPtr__(IntPtr addr) { return new IntelligentScissorsMB(addr); }

        //
        // C++:   cv::segmentation::IntelligentScissorsMB::IntelligentScissorsMB()
        //

        public IntelligentScissorsMB()
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_IntelligentScissorsMB_10());


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setWeights(float weight_non_edge, float weight_gradient_direction, float weight_gradient_magnitude)
        //

        /// <summary>
        ///  Specify weights of feature functions
        /// </summary>
        /// <remarks>
        ///         Consider keeping weights normalized (sum of weights equals to 1.0)
        ///         Discrete dynamic programming (DP) goal is minimization of costs between pixels.
        /// </remarks>
        /// <param name="weight_non_edge">
        /// Specify cost of non-edge pixels (default: 0.43f)
        /// </param>
        /// <param name="weight_gradient_direction">
        /// Specify cost of gradient direction function (default: 0.43f)
        /// </param>
        /// <param name="weight_gradient_magnitude">
        /// Specify cost of gradient magnitude function (default: 0.14f)
        /// </param>
        public IntelligentScissorsMB setWeights(float weight_non_edge, float weight_gradient_direction, float weight_gradient_magnitude)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setWeights_10(nativeObj, weight_non_edge, weight_gradient_direction, weight_gradient_magnitude)));


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setGradientMagnitudeMaxLimit(float gradient_magnitude_threshold_max = 0.0f)
        //

        /// <summary>
        ///  Specify gradient magnitude max value threshold
        /// </summary>
        /// <remarks>
        ///         Zero limit value is used to disable gradient magnitude thresholding (default behavior, as described in original article).
        ///         Otherwize pixels with `gradient magnitude >= threshold` have zero cost.
        ///        
        ///         @note Thresholding should be used for images with irregular regions (to avoid stuck on parameters from high-contract areas, like embedded logos).
        /// </remarks>
        /// <param name="gradient_magnitude_threshold_max">
        /// Specify gradient magnitude max value threshold (default: 0, disabled)
        /// </param>
        public IntelligentScissorsMB setGradientMagnitudeMaxLimit(float gradient_magnitude_threshold_max)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setGradientMagnitudeMaxLimit_10(nativeObj, gradient_magnitude_threshold_max)));


        }

        /// <summary>
        ///  Specify gradient magnitude max value threshold
        /// </summary>
        /// <remarks>
        ///         Zero limit value is used to disable gradient magnitude thresholding (default behavior, as described in original article).
        ///         Otherwize pixels with `gradient magnitude >= threshold` have zero cost.
        ///        
        ///         @note Thresholding should be used for images with irregular regions (to avoid stuck on parameters from high-contract areas, like embedded logos).
        /// </remarks>
        /// <param name="gradient_magnitude_threshold_max">
        /// Specify gradient magnitude max value threshold (default: 0, disabled)
        /// </param>
        public IntelligentScissorsMB setGradientMagnitudeMaxLimit()
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setGradientMagnitudeMaxLimit_11(nativeObj)));


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setEdgeFeatureZeroCrossingParameters(float gradient_magnitude_min_value = 0.0f)
        //

        /// <summary>
        ///  Switch to "Laplacian Zero-Crossing" edge feature extractor and specify its parameters
        /// </summary>
        /// <remarks>
        ///         This feature extractor is used by default according to article.
        ///        
        ///         Implementation has additional filtering for regions with low-amplitude noise.
        ///         This filtering is enabled through parameter of minimal gradient amplitude (use some small value 4, 8, 16).
        ///        
        ///         @note Current implementation of this feature extractor is based on processing of grayscale images (color image is converted to grayscale image first).
        ///        
        ///         @note Canny edge detector is a bit slower, but provides better results (especially on color images): use setEdgeFeatureCannyParameters().
        /// </remarks>
        /// <param name="gradient_magnitude_min_value">
        /// Minimal gradient magnitude value for edge pixels (default: 0, check is disabled)
        /// </param>
        public IntelligentScissorsMB setEdgeFeatureZeroCrossingParameters(float gradient_magnitude_min_value)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setEdgeFeatureZeroCrossingParameters_10(nativeObj, gradient_magnitude_min_value)));


        }

        /// <summary>
        ///  Switch to "Laplacian Zero-Crossing" edge feature extractor and specify its parameters
        /// </summary>
        /// <remarks>
        ///         This feature extractor is used by default according to article.
        ///        
        ///         Implementation has additional filtering for regions with low-amplitude noise.
        ///         This filtering is enabled through parameter of minimal gradient amplitude (use some small value 4, 8, 16).
        ///        
        ///         @note Current implementation of this feature extractor is based on processing of grayscale images (color image is converted to grayscale image first).
        ///        
        ///         @note Canny edge detector is a bit slower, but provides better results (especially on color images): use setEdgeFeatureCannyParameters().
        /// </remarks>
        /// <param name="gradient_magnitude_min_value">
        /// Minimal gradient magnitude value for edge pixels (default: 0, check is disabled)
        /// </param>
        public IntelligentScissorsMB setEdgeFeatureZeroCrossingParameters()
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setEdgeFeatureZeroCrossingParameters_11(nativeObj)));


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setEdgeFeatureCannyParameters(double threshold1, double threshold2, int apertureSize = 3, bool L2gradient = false)
        //

        /// <summary>
        ///  Switch edge feature extractor to use Canny edge detector
        /// </summary>
        /// <remarks>
        ///         @note "Laplacian Zero-Crossing" feature extractor is used by default (following to original article)
        ///        
        ///         @sa Canny
        /// </remarks>
        public IntelligentScissorsMB setEdgeFeatureCannyParameters(double threshold1, double threshold2, int apertureSize, bool L2gradient)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_10(nativeObj, threshold1, threshold2, apertureSize, L2gradient)));


        }

        /// <summary>
        ///  Switch edge feature extractor to use Canny edge detector
        /// </summary>
        /// <remarks>
        ///         @note "Laplacian Zero-Crossing" feature extractor is used by default (following to original article)
        ///        
        ///         @sa Canny
        /// </remarks>
        public IntelligentScissorsMB setEdgeFeatureCannyParameters(double threshold1, double threshold2, int apertureSize)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_11(nativeObj, threshold1, threshold2, apertureSize)));


        }

        /// <summary>
        ///  Switch edge feature extractor to use Canny edge detector
        /// </summary>
        /// <remarks>
        ///         @note "Laplacian Zero-Crossing" feature extractor is used by default (following to original article)
        ///        
        ///         @sa Canny
        /// </remarks>
        public IntelligentScissorsMB setEdgeFeatureCannyParameters(double threshold1, double threshold2)
        {
            ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_12(nativeObj, threshold1, threshold2)));


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::applyImage(Mat image)
        //

        /// <summary>
        ///  Specify input image and extract image features
        /// </summary>
        /// <param name="image">
        /// input image. Type is #CV_8UC1 / #CV_8UC3
        /// </param>
        public IntelligentScissorsMB applyImage(Mat image)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_applyImage_10(nativeObj, image.nativeObj)));


        }


        //
        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::applyImageFeatures(Mat non_edge, Mat gradient_direction, Mat gradient_magnitude, Mat image = Mat())
        //

        /// <summary>
        ///  Specify custom features of input image
        /// </summary>
        /// <remarks>
        ///         Customized advanced variant of applyImage() call.
        /// </remarks>
        /// <param name="non_edge">
        /// Specify cost of non-edge pixels. Type is CV_8UC1. Expected values are `{0, 1}`.
        /// </param>
        /// <param name="gradient_direction">
        /// Specify gradient direction feature. Type is CV_32FC2. Values are expected to be normalized: `x^2 + y^2 == 1`
        /// </param>
        /// <param name="gradient_magnitude">
        /// Specify cost of gradient magnitude function: Type is CV_32FC1. Values should be in range `[0, 1]`.
        /// </param>
        /// <param name="image">
        /// **Optional parameter**. Must be specified if subset of features is specified (non-specified features are calculated internally)
        /// </param>
        public IntelligentScissorsMB applyImageFeatures(Mat non_edge, Mat gradient_direction, Mat gradient_magnitude, Mat image)
        {
            ThrowIfDisposed();
            if (non_edge != null) non_edge.ThrowIfDisposed();
            if (gradient_direction != null) gradient_direction.ThrowIfDisposed();
            if (gradient_magnitude != null) gradient_magnitude.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_applyImageFeatures_10(nativeObj, non_edge.nativeObj, gradient_direction.nativeObj, gradient_magnitude.nativeObj, image.nativeObj)));


        }

        /// <summary>
        ///  Specify custom features of input image
        /// </summary>
        /// <remarks>
        ///         Customized advanced variant of applyImage() call.
        /// </remarks>
        /// <param name="non_edge">
        /// Specify cost of non-edge pixels. Type is CV_8UC1. Expected values are `{0, 1}`.
        /// </param>
        /// <param name="gradient_direction">
        /// Specify gradient direction feature. Type is CV_32FC2. Values are expected to be normalized: `x^2 + y^2 == 1`
        /// </param>
        /// <param name="gradient_magnitude">
        /// Specify cost of gradient magnitude function: Type is CV_32FC1. Values should be in range `[0, 1]`.
        /// </param>
        /// <param name="image">
        /// **Optional parameter**. Must be specified if subset of features is specified (non-specified features are calculated internally)
        /// </param>
        public IntelligentScissorsMB applyImageFeatures(Mat non_edge, Mat gradient_direction, Mat gradient_magnitude)
        {
            ThrowIfDisposed();
            if (non_edge != null) non_edge.ThrowIfDisposed();
            if (gradient_direction != null) gradient_direction.ThrowIfDisposed();
            if (gradient_magnitude != null) gradient_magnitude.ThrowIfDisposed();

            return new IntelligentScissorsMB(DisposableObject.ThrowIfNullIntPtr(imgproc_IntelligentScissorsMB_applyImageFeatures_11(nativeObj, non_edge.nativeObj, gradient_direction.nativeObj, gradient_magnitude.nativeObj)));


        }


        //
        // C++:  void cv::segmentation::IntelligentScissorsMB::buildMap(Point sourcePt)
        //

        /// <summary>
        ///  Prepares a map of optimal paths for the given source point on the image
        /// </summary>
        /// <remarks>
        ///         @note applyImage() / applyImageFeatures() must be called before this call
        /// </remarks>
        /// <param name="sourcePt">
        /// The source point used to find the paths
        /// </param>
        public void buildMap(Point sourcePt)
        {
            ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_buildMap_10(nativeObj, sourcePt.x, sourcePt.y);


        }


        //
        // C++:  void cv::segmentation::IntelligentScissorsMB::getContour(Point targetPt, Mat& contour, bool backward = false)
        //

        /// <summary>
        ///  Extracts optimal contour for the given target point on the image
        /// </summary>
        /// <remarks>
        ///         @note buildMap() must be called before this call
        /// </remarks>
        /// <param name="targetPt">
        /// The target point
        /// </param>
        /// <param name="contour">
        /// The list of pixels which contains optimal path between the source and the target points of the image. Type is CV_32SC2 (compatible with `std::vector<Point>`)
        /// </param>
        /// <param name="backward">
        /// Flag to indicate reverse order of retrieved pixels (use "true" value to fetch points from the target to the source point)
        /// </param>
        public void getContour(Point targetPt, Mat contour, bool backward)
        {
            ThrowIfDisposed();
            if (contour != null) contour.ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_getContour_10(nativeObj, targetPt.x, targetPt.y, contour.nativeObj, backward);


        }

        /// <summary>
        ///  Extracts optimal contour for the given target point on the image
        /// </summary>
        /// <remarks>
        ///         @note buildMap() must be called before this call
        /// </remarks>
        /// <param name="targetPt">
        /// The target point
        /// </param>
        /// <param name="contour">
        /// The list of pixels which contains optimal path between the source and the target points of the image. Type is CV_32SC2 (compatible with `std::vector<Point>`)
        /// </param>
        /// <param name="backward">
        /// Flag to indicate reverse order of retrieved pixels (use "true" value to fetch points from the target to the source point)
        /// </param>
        public void getContour(Point targetPt, Mat contour)
        {
            ThrowIfDisposed();
            if (contour != null) contour.ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_getContour_11(nativeObj, targetPt.x, targetPt.y, contour.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::segmentation::IntelligentScissorsMB::IntelligentScissorsMB()
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_IntelligentScissorsMB_10();

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setWeights(float weight_non_edge, float weight_gradient_direction, float weight_gradient_magnitude)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setWeights_10(IntPtr nativeObj, float weight_non_edge, float weight_gradient_direction, float weight_gradient_magnitude);

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setGradientMagnitudeMaxLimit(float gradient_magnitude_threshold_max = 0.0f)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setGradientMagnitudeMaxLimit_10(IntPtr nativeObj, float gradient_magnitude_threshold_max);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setGradientMagnitudeMaxLimit_11(IntPtr nativeObj);

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setEdgeFeatureZeroCrossingParameters(float gradient_magnitude_min_value = 0.0f)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setEdgeFeatureZeroCrossingParameters_10(IntPtr nativeObj, float gradient_magnitude_min_value);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setEdgeFeatureZeroCrossingParameters_11(IntPtr nativeObj);

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::setEdgeFeatureCannyParameters(double threshold1, double threshold2, int apertureSize = 3, bool L2gradient = false)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_10(IntPtr nativeObj, double threshold1, double threshold2, int apertureSize, [MarshalAs(UnmanagedType.U1)] bool L2gradient);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_11(IntPtr nativeObj, double threshold1, double threshold2, int apertureSize);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_setEdgeFeatureCannyParameters_12(IntPtr nativeObj, double threshold1, double threshold2);

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::applyImage(Mat image)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_applyImage_10(IntPtr nativeObj, IntPtr image_nativeObj);

        // C++:  IntelligentScissorsMB cv::segmentation::IntelligentScissorsMB::applyImageFeatures(Mat non_edge, Mat gradient_direction, Mat gradient_magnitude, Mat image = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_applyImageFeatures_10(IntPtr nativeObj, IntPtr non_edge_nativeObj, IntPtr gradient_direction_nativeObj, IntPtr gradient_magnitude_nativeObj, IntPtr image_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgproc_IntelligentScissorsMB_applyImageFeatures_11(IntPtr nativeObj, IntPtr non_edge_nativeObj, IntPtr gradient_direction_nativeObj, IntPtr gradient_magnitude_nativeObj);

        // C++:  void cv::segmentation::IntelligentScissorsMB::buildMap(Point sourcePt)
        [DllImport(LIBNAME)]
        private static extern void imgproc_IntelligentScissorsMB_buildMap_10(IntPtr nativeObj, double sourcePt_x, double sourcePt_y);

        // C++:  void cv::segmentation::IntelligentScissorsMB::getContour(Point targetPt, Mat& contour, bool backward = false)
        [DllImport(LIBNAME)]
        private static extern void imgproc_IntelligentScissorsMB_getContour_10(IntPtr nativeObj, double targetPt_x, double targetPt_y, IntPtr contour_nativeObj, [MarshalAs(UnmanagedType.U1)] bool backward);
        [DllImport(LIBNAME)]
        private static extern void imgproc_IntelligentScissorsMB_getContour_11(IntPtr nativeObj, double targetPt_x, double targetPt_y, IntPtr contour_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void imgproc_IntelligentScissorsMB_delete(IntPtr nativeObj);

    }
}
