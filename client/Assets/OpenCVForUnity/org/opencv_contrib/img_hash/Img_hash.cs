
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Img_hashModule
{
    // C++: class Img_hash


    public class Img_hash
    {

        /// <summary>
        /// C++: enum BlockMeanHashMode (cv.img_hash.BlockMeanHashMode)
        /// </summary>
        public const int BLOCK_MEAN_HASH_MODE_0 = 0;

        /// <summary>
        /// C++: enum BlockMeanHashMode (cv.img_hash.BlockMeanHashMode)
        /// </summary>
        public const int BLOCK_MEAN_HASH_MODE_1 = 1;


        //
        // C++:  void cv::img_hash::averageHash(Mat inputArr, Mat& outputArr)
        //

        /// <summary>
        ///  Calculates img_hash::AverageHash in one call
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value, type should be CV_8UC4, CV_8UC3 or CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex decimal number, return type is CV_8U
        /// </param>
        public static void averageHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_averageHash_10(inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  void cv::img_hash::blockMeanHash(Mat inputArr, Mat& outputArr, int mode = BLOCK_MEAN_HASH_MODE_0)
        //

        /// <summary>
        ///  Computes block mean hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value, type should be CV_8UC4, CV_8UC3 or CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex decimal number, return type is CV_8U
        /// </param>
        /// <param name="mode">
        /// the mode
        /// </param>
        public static void blockMeanHash(Mat inputArr, Mat outputArr, int mode)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_blockMeanHash_10(inputArr.nativeObj, outputArr.nativeObj, mode);


        }

        /// <summary>
        ///  Computes block mean hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value, type should be CV_8UC4, CV_8UC3 or CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex decimal number, return type is CV_8U
        /// </param>
        /// <param name="mode">
        /// the mode
        /// </param>
        public static void blockMeanHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_blockMeanHash_11(inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  void cv::img_hash::colorMomentHash(Mat inputArr, Mat& outputArr)
        //

        /// <summary>
        ///  Computes color moment hash of the input, the algorithm
        ///      is come from the paper "Perceptual  Hashing  for  Color  Images
        ///      Using  Invariant Moments"
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3 or CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// 42 hash values with type CV_64F(double)
        /// </param>
        public static void colorMomentHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_colorMomentHash_10(inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  void cv::img_hash::marrHildrethHash(Mat inputArr, Mat& outputArr, float alpha = 2.0f, float scale = 1.0f)
        //

        /// <summary>
        ///  Computes average hash value of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex
        ///      decimal number, return type is CV_8U
        /// </param>
        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static void marrHildrethHash(Mat inputArr, Mat outputArr, float alpha, float scale)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_marrHildrethHash_10(inputArr.nativeObj, outputArr.nativeObj, alpha, scale);


        }

        /// <summary>
        ///  Computes average hash value of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex
        ///      decimal number, return type is CV_8U
        /// </param>
        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static void marrHildrethHash(Mat inputArr, Mat outputArr, float alpha)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_marrHildrethHash_11(inputArr.nativeObj, outputArr.nativeObj, alpha);


        }

        /// <summary>
        ///  Computes average hash value of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 16 hex
        ///      decimal number, return type is CV_8U
        /// </param>
        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static void marrHildrethHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_marrHildrethHash_12(inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  void cv::img_hash::pHash(Mat inputArr, Mat& outputArr)
        //

        /// <summary>
        ///  Computes pHash value of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///       type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input, it will contain 8 uchar value
        /// </param>
        public static void pHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_pHash_10(inputArr.nativeObj, outputArr.nativeObj);


        }


        //
        // C++:  void cv::img_hash::radialVarianceHash(Mat inputArr, Mat& outputArr, double sigma = 1, int numOfAngleLine = 180)
        //

        /// <summary>
        ///  Computes radial variance hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input
        /// </param>
        /// <param name="sigma">
        /// Gaussian kernel standard deviation
        /// </param>
        /// <param name="numOfAngleLine">
        /// The number of angles to consider
        /// </param>
        public static void radialVarianceHash(Mat inputArr, Mat outputArr, double sigma, int numOfAngleLine)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_radialVarianceHash_10(inputArr.nativeObj, outputArr.nativeObj, sigma, numOfAngleLine);


        }

        /// <summary>
        ///  Computes radial variance hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input
        /// </param>
        /// <param name="sigma">
        /// Gaussian kernel standard deviation
        /// </param>
        /// <param name="numOfAngleLine">
        /// The number of angles to consider
        /// </param>
        public static void radialVarianceHash(Mat inputArr, Mat outputArr, double sigma)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_radialVarianceHash_11(inputArr.nativeObj, outputArr.nativeObj, sigma);


        }

        /// <summary>
        ///  Computes radial variance hash of the input image
        /// </summary>
        /// <param name="inputArr">
        /// input image want to compute hash value,
        ///      type should be CV_8UC4, CV_8UC3, CV_8UC1.
        /// </param>
        /// <param name="outputArr">
        /// Hash value of input
        /// </param>
        /// <param name="sigma">
        /// Gaussian kernel standard deviation
        /// </param>
        /// <param name="numOfAngleLine">
        /// The number of angles to consider
        /// </param>
        public static void radialVarianceHash(Mat inputArr, Mat outputArr)
        {
            if (inputArr != null) inputArr.ThrowIfDisposed();
            if (outputArr != null) outputArr.ThrowIfDisposed();

            img_1hash_Img_1hash_radialVarianceHash_12(inputArr.nativeObj, outputArr.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::img_hash::averageHash(Mat inputArr, Mat& outputArr)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_averageHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  void cv::img_hash::blockMeanHash(Mat inputArr, Mat& outputArr, int mode = BLOCK_MEAN_HASH_MODE_0)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_blockMeanHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj, int mode);
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_blockMeanHash_11(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  void cv::img_hash::colorMomentHash(Mat inputArr, Mat& outputArr)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_colorMomentHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  void cv::img_hash::marrHildrethHash(Mat inputArr, Mat& outputArr, float alpha = 2.0f, float scale = 1.0f)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_marrHildrethHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj, float alpha, float scale);
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_marrHildrethHash_11(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj, float alpha);
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_marrHildrethHash_12(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  void cv::img_hash::pHash(Mat inputArr, Mat& outputArr)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_pHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

        // C++:  void cv::img_hash::radialVarianceHash(Mat inputArr, Mat& outputArr, double sigma = 1, int numOfAngleLine = 180)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_radialVarianceHash_10(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj, double sigma, int numOfAngleLine);
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_radialVarianceHash_11(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj, double sigma);
        [DllImport(LIBNAME)]
        private static extern void img_1hash_Img_1hash_radialVarianceHash_12(IntPtr inputArr_nativeObj, IntPtr outputArr_nativeObj);

    }
}
