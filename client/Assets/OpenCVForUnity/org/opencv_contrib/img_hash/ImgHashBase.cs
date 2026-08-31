
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Img_hashModule
{

    // C++: class ImgHashBase
    /// <summary>
    ///  The base class for image hash algorithms
    /// </summary>
    public class ImgHashBase : Algorithm
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
                        img_1hash_ImgHashBase_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal ImgHashBase(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new ImgHashBase __fromPtr__(IntPtr addr) { return new ImgHashBase(addr); }

        //
        // C++:  void cv::img_hash::ImgHashBase::compute(Mat inputArr, Mat& outputArr)
        //

        /// <summary>
        ///  Computes hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value
        /// </param>
        /// <param name="outputArr">
        /// hash of the image
        /// </param>
        public void compute(Mat inputArr, Mat outputArr)
        {
            ThrowIfDisposed();
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_ImgHashBase_compute_10(nativeObj, inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  double cv::img_hash::ImgHashBase::compare(Mat hashOne, Mat hashTwo)
        //

        /// <summary>
        ///  Compare the hash value between inOne and inTwo
        /// </summary>
        /// <param name="hashOne">
        /// Hash value one
        /// </param>
        /// <param name="hashTwo">
        /// Hash value two
        /// </param>
        /// <returns>
        ///  value indicate similarity between inOne and inTwo, the meaning
        ///          of the value vary from algorithms to algorithms
        /// </returns>
        public double compare(Mat hashOne, Mat hashTwo)
        {
            ThrowIfDisposed();
            if (hashOne != null) hashOne.ThrowIfDisposed();
            if (hashTwo != null) hashTwo.ThrowIfDisposed();

            return img_1hash_ImgHashBase_compare_10(nativeObj, hashOne.nativeObj, hashTwo.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::img_hash::ImgHashBase::compute(Mat inputArr, Mat& outputArr)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_ImgHashBase_compute_10(IntPtr nativeObj, IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  double cv::img_hash::ImgHashBase::compare(Mat hashOne, Mat hashTwo)
        [DllImport(LIBNAME)]
        private static extern double img_1hash_ImgHashBase_compare_10(IntPtr nativeObj, IntPtr hashOne_nativeObj, IntPtr hashTwo_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void img_1hash_ImgHashBase_delete(IntPtr nativeObj);

    }
}
