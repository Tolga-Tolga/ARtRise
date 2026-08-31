
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using OpenCVForUnity.VideoModule;

namespace OpenCVForUnity.BgsegmModule
{

    // C++: class BackgroundSubtractorGMG
    /// <summary>
    ///  Background Subtractor module based on the algorithm given in @cite Gold2012 .
    /// </summary>
    /// <remarks>
    ///   Takes a series of images and returns a sequence of mask (8UC1)
    ///   images of the same size, where 255 indicates Foreground and 0 represents Background.
    ///   This class implements an algorithm described in "Visual Tracking of Human Visitors under
    ///   Variable-Lighting Conditions for a Responsive Audio Art Installation," A. Godbehere,
    ///   A. Matsukawa, K. Goldberg, American Control Conference, Montreal, June 2012.
    /// </remarks>
    public class BackgroundSubtractorGMG : BackgroundSubtractor
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
                        bgsegm_BackgroundSubtractorGMG_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal BackgroundSubtractorGMG(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new BackgroundSubtractorGMG __fromPtr__(IntPtr addr) { return new BackgroundSubtractorGMG(addr); }

        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::apply(Mat image, Mat& fgmask, double learningRate = -1)
        //

        /// <summary>
        ///  Computes a foreground mask.
        /// </summary>
        /// <param name="image">
        /// Next video frame of type CV_8UC(n),CV_8SC(n),CV_16UC(n),CV_16SC(n),CV_32SC(n),CV_32FC(n),CV_64FC(n), where n is 1,2,3,4.
        /// </param>
        /// <param name="fgmask">
        /// The output foreground mask as an 8-bit binary image.
        /// </param>
        /// <param name="learningRate">
        /// The value between 0 and 1 that indicates how fast the background model is
        ///      learnt. Negative parameter value makes the algorithm to use some automatically chosen learning
        ///      rate. 0 means that the background model is not updated at all, 1 means that the background model
        ///      is completely reinitialized from the last frame.
        /// </param>
        public override void apply(Mat image, Mat fgmask, double learningRate)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (fgmask != null) fgmask.ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_apply_10(nativeObj, image.nativeObj, fgmask.nativeObj, learningRate);


        }

        /// <summary>
        ///  Computes a foreground mask.
        /// </summary>
        /// <param name="image">
        /// Next video frame of type CV_8UC(n),CV_8SC(n),CV_16UC(n),CV_16SC(n),CV_32SC(n),CV_32FC(n),CV_64FC(n), where n is 1,2,3,4.
        /// </param>
        /// <param name="fgmask">
        /// The output foreground mask as an 8-bit binary image.
        /// </param>
        /// <param name="learningRate">
        /// The value between 0 and 1 that indicates how fast the background model is
        ///      learnt. Negative parameter value makes the algorithm to use some automatically chosen learning
        ///      rate. 0 means that the background model is not updated at all, 1 means that the background model
        ///      is completely reinitialized from the last frame.
        /// </param>
        public override void apply(Mat image, Mat fgmask)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (fgmask != null) fgmask.ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_apply_11(nativeObj, image.nativeObj, fgmask.nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::getBackgroundImage(Mat& backgroundImage)
        //

        public override void getBackgroundImage(Mat backgroundImage)
        {
            ThrowIfDisposed();
            if (backgroundImage != null) backgroundImage.ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_getBackgroundImage_10(nativeObj, backgroundImage.nativeObj);


        }


        //
        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getMaxFeatures()
        //

        /// <summary>
        ///  Returns total number of distinct colors to maintain in histogram.
        /// </summary>
        public int getMaxFeatures()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getMaxFeatures_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMaxFeatures(int maxFeatures)
        //

        /// <summary>
        ///  Sets total number of distinct colors to maintain in histogram.
        /// </summary>
        public void setMaxFeatures(int maxFeatures)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setMaxFeatures_10(nativeObj, maxFeatures);


        }


        //
        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getDefaultLearningRate()
        //

        /// <summary>
        ///  Returns the learning rate of the algorithm.
        /// </summary>
        /// <remarks>
        ///      It lies between 0.0 and 1.0. It determines how quickly features are "forgotten" from
        ///      histograms.
        /// </remarks>
        public double getDefaultLearningRate()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getDefaultLearningRate_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setDefaultLearningRate(double lr)
        //

        /// <summary>
        ///  Sets the learning rate of the algorithm.
        /// </summary>
        public void setDefaultLearningRate(double lr)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setDefaultLearningRate_10(nativeObj, lr);


        }


        //
        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getNumFrames()
        //

        /// <summary>
        ///  Returns the number of frames used to initialize background model.
        /// </summary>
        public int getNumFrames()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getNumFrames_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setNumFrames(int nframes)
        //

        /// <summary>
        ///  Sets the number of frames used to initialize background model.
        /// </summary>
        public void setNumFrames(int nframes)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setNumFrames_10(nativeObj, nframes);


        }


        //
        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getQuantizationLevels()
        //

        /// <summary>
        ///  Returns the parameter used for quantization of color-space.
        /// </summary>
        /// <remarks>
        ///      It is the number of discrete levels in each channel to be used in histograms.
        /// </remarks>
        public int getQuantizationLevels()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getQuantizationLevels_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setQuantizationLevels(int nlevels)
        //

        /// <summary>
        ///  Sets the parameter used for quantization of color-space
        /// </summary>
        public void setQuantizationLevels(int nlevels)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setQuantizationLevels_10(nativeObj, nlevels);


        }


        //
        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getBackgroundPrior()
        //

        /// <summary>
        ///  Returns the prior probability that each individual pixel is a background pixel.
        /// </summary>
        public double getBackgroundPrior()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getBackgroundPrior_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setBackgroundPrior(double bgprior)
        //

        /// <summary>
        ///  Sets the prior probability that each individual pixel is a background pixel.
        /// </summary>
        public void setBackgroundPrior(double bgprior)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setBackgroundPrior_10(nativeObj, bgprior);


        }


        //
        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getSmoothingRadius()
        //

        /// <summary>
        ///  Returns the kernel radius used for morphological operations
        /// </summary>
        public int getSmoothingRadius()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getSmoothingRadius_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setSmoothingRadius(int radius)
        //

        /// <summary>
        ///  Sets the kernel radius used for morphological operations
        /// </summary>
        public void setSmoothingRadius(int radius)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setSmoothingRadius_10(nativeObj, radius);


        }


        //
        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getDecisionThreshold()
        //

        /// <summary>
        ///  Returns the value of decision threshold.
        /// </summary>
        /// <remarks>
        ///      Decision value is the value above which pixel is determined to be FG.
        /// </remarks>
        public double getDecisionThreshold()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getDecisionThreshold_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setDecisionThreshold(double thresh)
        //

        /// <summary>
        ///  Sets the value of decision threshold.
        /// </summary>
        public void setDecisionThreshold(double thresh)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setDecisionThreshold_10(nativeObj, thresh);


        }


        //
        // C++:  bool cv::bgsegm::BackgroundSubtractorGMG::getUpdateBackgroundModel()
        //

        /// <summary>
        ///  Returns the status of background model update
        /// </summary>
        public bool getUpdateBackgroundModel()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getUpdateBackgroundModel_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setUpdateBackgroundModel(bool update)
        //

        /// <summary>
        ///  Sets the status of background model update
        /// </summary>
        public void setUpdateBackgroundModel(bool update)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setUpdateBackgroundModel_10(nativeObj, update);


        }


        //
        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getMinVal()
        //

        /// <summary>
        ///  Returns the minimum value taken on by pixels in image sequence. Usually 0.
        /// </summary>
        public double getMinVal()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getMinVal_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMinVal(double val)
        //

        /// <summary>
        ///  Sets the minimum value taken on by pixels in image sequence.
        /// </summary>
        public void setMinVal(double val)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setMinVal_10(nativeObj, val);


        }


        //
        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getMaxVal()
        //

        /// <summary>
        ///  Returns the maximum value taken on by pixels in image sequence. e.g. 1.0 or 255.
        /// </summary>
        public double getMaxVal()
        {
            ThrowIfDisposed();

            return bgsegm_BackgroundSubtractorGMG_getMaxVal_10(nativeObj);


        }


        //
        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMaxVal(double val)
        //

        /// <summary>
        ///  Sets the maximum value taken on by pixels in image sequence.
        /// </summary>
        public void setMaxVal(double val)
        {
            ThrowIfDisposed();

            bgsegm_BackgroundSubtractorGMG_setMaxVal_10(nativeObj, val);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::apply(Mat image, Mat& fgmask, double learningRate = -1)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_apply_10(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr fgmask_nativeObj, double learningRate);
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_apply_11(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr fgmask_nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::getBackgroundImage(Mat& backgroundImage)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_getBackgroundImage_10(IntPtr nativeObj, IntPtr backgroundImage_nativeObj);

        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getMaxFeatures()
        [DllImport(LIBNAME)]
        private static extern int bgsegm_BackgroundSubtractorGMG_getMaxFeatures_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMaxFeatures(int maxFeatures)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setMaxFeatures_10(IntPtr nativeObj, int maxFeatures);

        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getDefaultLearningRate()
        [DllImport(LIBNAME)]
        private static extern double bgsegm_BackgroundSubtractorGMG_getDefaultLearningRate_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setDefaultLearningRate(double lr)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setDefaultLearningRate_10(IntPtr nativeObj, double lr);

        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getNumFrames()
        [DllImport(LIBNAME)]
        private static extern int bgsegm_BackgroundSubtractorGMG_getNumFrames_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setNumFrames(int nframes)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setNumFrames_10(IntPtr nativeObj, int nframes);

        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getQuantizationLevels()
        [DllImport(LIBNAME)]
        private static extern int bgsegm_BackgroundSubtractorGMG_getQuantizationLevels_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setQuantizationLevels(int nlevels)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setQuantizationLevels_10(IntPtr nativeObj, int nlevels);

        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getBackgroundPrior()
        [DllImport(LIBNAME)]
        private static extern double bgsegm_BackgroundSubtractorGMG_getBackgroundPrior_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setBackgroundPrior(double bgprior)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setBackgroundPrior_10(IntPtr nativeObj, double bgprior);

        // C++:  int cv::bgsegm::BackgroundSubtractorGMG::getSmoothingRadius()
        [DllImport(LIBNAME)]
        private static extern int bgsegm_BackgroundSubtractorGMG_getSmoothingRadius_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setSmoothingRadius(int radius)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setSmoothingRadius_10(IntPtr nativeObj, int radius);

        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getDecisionThreshold()
        [DllImport(LIBNAME)]
        private static extern double bgsegm_BackgroundSubtractorGMG_getDecisionThreshold_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setDecisionThreshold(double thresh)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setDecisionThreshold_10(IntPtr nativeObj, double thresh);

        // C++:  bool cv::bgsegm::BackgroundSubtractorGMG::getUpdateBackgroundModel()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool bgsegm_BackgroundSubtractorGMG_getUpdateBackgroundModel_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setUpdateBackgroundModel(bool update)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setUpdateBackgroundModel_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool update);

        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getMinVal()
        [DllImport(LIBNAME)]
        private static extern double bgsegm_BackgroundSubtractorGMG_getMinVal_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMinVal(double val)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setMinVal_10(IntPtr nativeObj, double val);

        // C++:  double cv::bgsegm::BackgroundSubtractorGMG::getMaxVal()
        [DllImport(LIBNAME)]
        private static extern double bgsegm_BackgroundSubtractorGMG_getMaxVal_10(IntPtr nativeObj);

        // C++:  void cv::bgsegm::BackgroundSubtractorGMG::setMaxVal(double val)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_setMaxVal_10(IntPtr nativeObj, double val);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void bgsegm_BackgroundSubtractorGMG_delete(IntPtr nativeObj);

    }
}
