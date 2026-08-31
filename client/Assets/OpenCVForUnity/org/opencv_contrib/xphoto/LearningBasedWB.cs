
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.PhotoModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XphotoModule
{

    // C++: class LearningBasedWB
    /// <summary>
    ///  More sophisticated learning-based automatic white balance algorithm.
    /// </summary>
    /// <remarks>
    ///  As @ref GrayworldWB, this algorithm works by applying different gains to the input
    ///  image channels, but their computation is a bit more involved compared to the
    ///  simple gray-world assumption. More details about the algorithm can be found in
    ///  @cite Cheng2015 .
    ///  
    ///  To mask out saturated pixels this function uses only pixels that satisfy the
    ///  following condition:
    ///  
    ///  \f[ \frac{\textrm{max}(R,G,B)}{\texttt{range_max_val}} < \texttt{saturation_thresh} \f]
    ///  
    ///  Currently supports images of type @ref CV_8UC3 and @ref CV_16UC3.
    /// </remarks>
    public class LearningBasedWB : WhiteBalancer
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
                        xphoto_LearningBasedWB_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal LearningBasedWB(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new LearningBasedWB __fromPtr__(IntPtr addr) { return new LearningBasedWB(addr); }

        //
        // C++:  void cv::xphoto::LearningBasedWB::extractSimpleFeatures(Mat src, Mat& dst)
        //

        /// <summary>
        ///  Implements the feature extraction part of the algorithm.
        /// </summary>
        /// <remarks>
        ///      In accordance with @cite Cheng2015 , computes the following features for the input image:
        ///      1. Chromaticity of an average (R,G,B) tuple
        ///      2. Chromaticity of the brightest (R,G,B) tuple (while ignoring saturated pixels)
        ///      3. Chromaticity of the dominant (R,G,B) tuple (the one that has the highest value in the RGB histogram)
        ///      4. Mode of the chromaticity palette, that is constructed by taking 300 most common colors according to
        ///         the RGB histogram and projecting them on the chromaticity plane. Mode is the most high-density point
        ///         of the palette, which is computed by a straightforward fixed-bandwidth kernel density estimator with
        ///         a Epanechnikov kernel function.
        /// </remarks>
        /// <param name="src">
        /// Input three-channel image (BGR color space is assumed).
        /// </param>
        /// <param name="dst">
        /// An array of four (r,g) chromaticity tuples corresponding to the features listed above.
        /// </param>
        public void extractSimpleFeatures(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_LearningBasedWB_extractSimpleFeatures_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


        //
        // C++:  int cv::xphoto::LearningBasedWB::getRangeMaxVal()
        //

        /// <summary>
        ///  Maximum possible value of the input image (e.g. 255 for 8 bit images,
        ///                 4095 for 12 bit images)
        ///  @see setRangeMaxVal
        /// </summary>
        public int getRangeMaxVal()
        {
            ThrowIfDisposed();

            return xphoto_LearningBasedWB_getRangeMaxVal_10(nativeObj);


        }


        //
        // C++:  void cv::xphoto::LearningBasedWB::setRangeMaxVal(int val)
        //

        /// <remarks>
        ///  @copybrief getRangeMaxVal @see getRangeMaxVal
        /// </remarks>
        public void setRangeMaxVal(int val)
        {
            ThrowIfDisposed();

            xphoto_LearningBasedWB_setRangeMaxVal_10(nativeObj, val);


        }


        //
        // C++:  float cv::xphoto::LearningBasedWB::getSaturationThreshold()
        //

        /// <summary>
        ///  Threshold that is used to determine saturated pixels, i.e. pixels where at least one of the
        ///          channels exceeds \f$\texttt{saturation_threshold}\times\texttt{range_max_val}\f$ are ignored.
        ///  @see setSaturationThreshold
        /// </summary>
        public float getSaturationThreshold()
        {
            ThrowIfDisposed();

            return xphoto_LearningBasedWB_getSaturationThreshold_10(nativeObj);


        }


        //
        // C++:  void cv::xphoto::LearningBasedWB::setSaturationThreshold(float val)
        //

        /// <remarks>
        ///  @copybrief getSaturationThreshold @see getSaturationThreshold
        /// </remarks>
        public void setSaturationThreshold(float val)
        {
            ThrowIfDisposed();

            xphoto_LearningBasedWB_setSaturationThreshold_10(nativeObj, val);


        }


        //
        // C++:  int cv::xphoto::LearningBasedWB::getHistBinNum()
        //

        /// <summary>
        ///  Defines the size of one dimension of a three-dimensional RGB histogram that is used internally
        ///          by the algorithm. It often makes sense to increase the number of bins for images with higher bit depth
        ///          (e.g. 256 bins for a 12 bit image).
        ///  @see setHistBinNum
        /// </summary>
        public int getHistBinNum()
        {
            ThrowIfDisposed();

            return xphoto_LearningBasedWB_getHistBinNum_10(nativeObj);


        }


        //
        // C++:  void cv::xphoto::LearningBasedWB::setHistBinNum(int val)
        //

        /// <remarks>
        ///  @copybrief getHistBinNum @see getHistBinNum
        /// </remarks>
        public void setHistBinNum(int val)
        {
            ThrowIfDisposed();

            xphoto_LearningBasedWB_setHistBinNum_10(nativeObj, val);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::xphoto::LearningBasedWB::extractSimpleFeatures(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void xphoto_LearningBasedWB_extractSimpleFeatures_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // C++:  int cv::xphoto::LearningBasedWB::getRangeMaxVal()
        [DllImport(LIBNAME)]
        private static extern int xphoto_LearningBasedWB_getRangeMaxVal_10(IntPtr nativeObj);

        // C++:  void cv::xphoto::LearningBasedWB::setRangeMaxVal(int val)
        [DllImport(LIBNAME)]
        private static extern void xphoto_LearningBasedWB_setRangeMaxVal_10(IntPtr nativeObj, int val);

        // C++:  float cv::xphoto::LearningBasedWB::getSaturationThreshold()
        [DllImport(LIBNAME)]
        private static extern float xphoto_LearningBasedWB_getSaturationThreshold_10(IntPtr nativeObj);

        // C++:  void cv::xphoto::LearningBasedWB::setSaturationThreshold(float val)
        [DllImport(LIBNAME)]
        private static extern void xphoto_LearningBasedWB_setSaturationThreshold_10(IntPtr nativeObj, float val);

        // C++:  int cv::xphoto::LearningBasedWB::getHistBinNum()
        [DllImport(LIBNAME)]
        private static extern int xphoto_LearningBasedWB_getHistBinNum_10(IntPtr nativeObj);

        // C++:  void cv::xphoto::LearningBasedWB::setHistBinNum(int val)
        [DllImport(LIBNAME)]
        private static extern void xphoto_LearningBasedWB_setHistBinNum_10(IntPtr nativeObj, int val);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void xphoto_LearningBasedWB_delete(IntPtr nativeObj);

    }
}
