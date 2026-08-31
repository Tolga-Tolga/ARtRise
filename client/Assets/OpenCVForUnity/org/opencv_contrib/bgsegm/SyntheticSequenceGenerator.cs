
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using OpenCVForUnity.VideoModule;

namespace OpenCVForUnity.BgsegmModule
{

    // C++: class SyntheticSequenceGenerator
    /// <summary>
    ///  Synthetic frame sequence generator for testing background subtraction algorithms.
    /// </summary>
    /// <remarks>
    ///   It will generate the moving object on top of the background.
    ///   It will apply some distortion to the background to make the test more complex.
    /// </remarks>
    public class SyntheticSequenceGenerator : Algorithm
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
                        bgsegm_SyntheticSequenceGenerator_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal SyntheticSequenceGenerator(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new SyntheticSequenceGenerator __fromPtr__(IntPtr addr) { return new SyntheticSequenceGenerator(addr); }

        //
        // C++:   cv::bgsegm::SyntheticSequenceGenerator::SyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength, double wavespeed, double objspeed)
        //

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public SyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength, double wavespeed, double objspeed) :
                    base(DisposableObject.ThrowIfNullIntPtr(bgsegm_SyntheticSequenceGenerator_SyntheticSequenceGenerator_10(background.nativeObj, _object.nativeObj, amplitude, wavelength, wavespeed, objspeed)))
        {



        }


        //
        // C++:  void cv::bgsegm::SyntheticSequenceGenerator::getNextFrame(Mat& frame, Mat& gtMask)
        //

        /// <summary>
        ///  Obtain the next frame in the sequence.
        /// </summary>
        /// <param name="frame">
        /// Output frame.
        /// </param>
        /// <param name="gtMask">
        /// Output ground-truth (reference) segmentation mask object/background.
        /// </param>
        public void getNextFrame(Mat frame, Mat gtMask)
        {
            ThrowIfDisposed();
            if (frame != null) frame.ThrowIfDisposed();
            if (gtMask != null) gtMask.ThrowIfDisposed();

            bgsegm_SyntheticSequenceGenerator_getNextFrame_10(nativeObj, frame.nativeObj, gtMask.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::bgsegm::SyntheticSequenceGenerator::SyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength, double wavespeed, double objspeed)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_SyntheticSequenceGenerator_SyntheticSequenceGenerator_10(IntPtr background_nativeObj, IntPtr _object_nativeObj, double amplitude, double wavelength, double wavespeed, double objspeed);

        // C++:  void cv::bgsegm::SyntheticSequenceGenerator::getNextFrame(Mat& frame, Mat& gtMask)
        [DllImport(LIBNAME)]
        private static extern void bgsegm_SyntheticSequenceGenerator_getNextFrame_10(IntPtr nativeObj, IntPtr frame_nativeObj, IntPtr gtMask_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void bgsegm_SyntheticSequenceGenerator_delete(IntPtr nativeObj);

    }
}
