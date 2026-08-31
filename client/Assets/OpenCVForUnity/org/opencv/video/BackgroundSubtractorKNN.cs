
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{

    // C++: class BackgroundSubtractorKNN
    /// <summary>
    ///  K-nearest neighbours - based Background/Foreground Segmentation Algorithm.
    /// </summary>
    /// <remarks>
    ///  The class implements the K-nearest neighbours background subtraction described in @cite Zivkovic2006 .
    ///  Very efficient if number of foreground pixels is low.
    /// </remarks>
    public class BackgroundSubtractorKNN : BackgroundSubtractor
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
                        video_BackgroundSubtractorKNN_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal BackgroundSubtractorKNN(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new BackgroundSubtractorKNN __fromPtr__(IntPtr addr) { return new BackgroundSubtractorKNN(addr); }

        //
        // C++:  int cv::BackgroundSubtractorKNN::getHistory()
        //

        /// <summary>
        ///  Returns the number of last frames that affect the background model
        /// </summary>
        public int getHistory()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getHistory_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setHistory(int history)
        //

        /// <summary>
        ///  Sets the number of last frames that affect the background model
        /// </summary>
        public void setHistory(int history)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setHistory_10(nativeObj, history);


        }


        //
        // C++:  int cv::BackgroundSubtractorKNN::getNSamples()
        //

        /// <summary>
        ///  Returns the number of data samples in the background model
        /// </summary>
        public int getNSamples()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getNSamples_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setNSamples(int _nN)
        //

        /// <summary>
        ///  Sets the number of data samples in the background model.
        /// </summary>
        /// <remarks>
        ///      The model needs to be reinitalized to reserve memory.
        /// </remarks>
        public void setNSamples(int _nN)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setNSamples_10(nativeObj, _nN);


        }


        //
        // C++:  double cv::BackgroundSubtractorKNN::getDist2Threshold()
        //

        /// <summary>
        ///  Returns the threshold on the squared distance between the pixel and the sample
        /// </summary>
        /// <remarks>
        ///      The threshold on the squared distance between the pixel and the sample to decide whether a pixel is
        ///      close to a data sample.
        /// </remarks>
        public double getDist2Threshold()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getDist2Threshold_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setDist2Threshold(double _dist2Threshold)
        //

        /// <summary>
        ///  Sets the threshold on the squared distance
        /// </summary>
        public void setDist2Threshold(double _dist2Threshold)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setDist2Threshold_10(nativeObj, _dist2Threshold);


        }


        //
        // C++:  int cv::BackgroundSubtractorKNN::getkNNSamples()
        //

        /// <summary>
        ///  Returns the number of neighbours, the k in the kNN.
        /// </summary>
        /// <remarks>
        ///      K is the number of samples that need to be within dist2Threshold in order to decide that that
        ///      pixel is matching the kNN background model.
        /// </remarks>
        public int getkNNSamples()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getkNNSamples_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setkNNSamples(int _nkNN)
        //

        /// <summary>
        ///  Sets the k in the kNN. How many nearest neighbours need to match.
        /// </summary>
        public void setkNNSamples(int _nkNN)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setkNNSamples_10(nativeObj, _nkNN);


        }


        //
        // C++:  bool cv::BackgroundSubtractorKNN::getDetectShadows()
        //

        /// <summary>
        ///  Returns the shadow detection flag
        /// </summary>
        /// <remarks>
        ///      If true, the algorithm detects shadows and marks them. See createBackgroundSubtractorKNN for
        ///      details.
        /// </remarks>
        public bool getDetectShadows()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getDetectShadows_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setDetectShadows(bool detectShadows)
        //

        /// <summary>
        ///  Enables or disables shadow detection
        /// </summary>
        public void setDetectShadows(bool detectShadows)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setDetectShadows_10(nativeObj, detectShadows);


        }


        //
        // C++:  int cv::BackgroundSubtractorKNN::getShadowValue()
        //

        /// <summary>
        ///  Returns the shadow value
        /// </summary>
        /// <remarks>
        ///      Shadow value is the value used to mark shadows in the foreground mask. Default value is 127. Value 0
        ///      in the mask always means background, 255 means foreground.
        /// </remarks>
        public int getShadowValue()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getShadowValue_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setShadowValue(int value)
        //

        /// <summary>
        ///  Sets the shadow value
        /// </summary>
        public void setShadowValue(int value)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setShadowValue_10(nativeObj, value);


        }


        //
        // C++:  double cv::BackgroundSubtractorKNN::getShadowThreshold()
        //

        /// <summary>
        ///  Returns the shadow threshold
        /// </summary>
        /// <remarks>
        ///      A shadow is detected if pixel is a darker version of the background. The shadow threshold (Tau in
        ///      the paper) is a threshold defining how much darker the shadow can be. Tau= 0.5 means that if a pixel
        ///      is more than twice darker then it is not shadow. See Prati, Mikic, Trivedi and Cucchiara,
        ///       Detecting Moving Shadows...*, IEEE PAMI,2003.
        /// </remarks>
        public double getShadowThreshold()
        {
            ThrowIfDisposed();

            return video_BackgroundSubtractorKNN_getShadowThreshold_10(nativeObj);


        }


        //
        // C++:  void cv::BackgroundSubtractorKNN::setShadowThreshold(double threshold)
        //

        /// <summary>
        ///  Sets the shadow threshold
        /// </summary>
        public void setShadowThreshold(double threshold)
        {
            ThrowIfDisposed();

            video_BackgroundSubtractorKNN_setShadowThreshold_10(nativeObj, threshold);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  int cv::BackgroundSubtractorKNN::getHistory()
        [DllImport(LIBNAME)]
        private static extern int video_BackgroundSubtractorKNN_getHistory_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setHistory(int history)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setHistory_10(IntPtr nativeObj, int history);

        // C++:  int cv::BackgroundSubtractorKNN::getNSamples()
        [DllImport(LIBNAME)]
        private static extern int video_BackgroundSubtractorKNN_getNSamples_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setNSamples(int _nN)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setNSamples_10(IntPtr nativeObj, int _nN);

        // C++:  double cv::BackgroundSubtractorKNN::getDist2Threshold()
        [DllImport(LIBNAME)]
        private static extern double video_BackgroundSubtractorKNN_getDist2Threshold_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setDist2Threshold(double _dist2Threshold)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setDist2Threshold_10(IntPtr nativeObj, double _dist2Threshold);

        // C++:  int cv::BackgroundSubtractorKNN::getkNNSamples()
        [DllImport(LIBNAME)]
        private static extern int video_BackgroundSubtractorKNN_getkNNSamples_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setkNNSamples(int _nkNN)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setkNNSamples_10(IntPtr nativeObj, int _nkNN);

        // C++:  bool cv::BackgroundSubtractorKNN::getDetectShadows()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool video_BackgroundSubtractorKNN_getDetectShadows_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setDetectShadows(bool detectShadows)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setDetectShadows_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool detectShadows);

        // C++:  int cv::BackgroundSubtractorKNN::getShadowValue()
        [DllImport(LIBNAME)]
        private static extern int video_BackgroundSubtractorKNN_getShadowValue_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setShadowValue(int value)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setShadowValue_10(IntPtr nativeObj, int value);

        // C++:  double cv::BackgroundSubtractorKNN::getShadowThreshold()
        [DllImport(LIBNAME)]
        private static extern double video_BackgroundSubtractorKNN_getShadowThreshold_10(IntPtr nativeObj);

        // C++:  void cv::BackgroundSubtractorKNN::setShadowThreshold(double threshold)
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_setShadowThreshold_10(IntPtr nativeObj, double threshold);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_BackgroundSubtractorKNN_delete(IntPtr nativeObj);

    }
}
