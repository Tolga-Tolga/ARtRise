
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Img_hashModule
{

    // C++: class MarrHildrethHash
    /// <summary>
    ///  Marr-Hildreth Operator Based Hash, slowest but more discriminative.
    /// </summary>
    /// <remarks>
    ///  See @cite zauner2010implementation for details.
    /// </remarks>
    public class MarrHildrethHash : ImgHashBase
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
                        img_1hash_MarrHildrethHash_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal MarrHildrethHash(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new MarrHildrethHash __fromPtr__(IntPtr addr) { return new MarrHildrethHash(addr); }

        //
        // C++:  float cv::img_hash::MarrHildrethHash::getAlpha()
        //

        /// <summary>
        ///  self explain
        /// </summary>
        public float getAlpha()
        {
            ThrowIfDisposed();

            return img_1hash_MarrHildrethHash_getAlpha_10(nativeObj);


        }


        //
        // C++:  float cv::img_hash::MarrHildrethHash::getScale()
        //

        /// <summary>
        ///  self explain
        /// </summary>
        public float getScale()
        {
            ThrowIfDisposed();

            return img_1hash_MarrHildrethHash_getScale_10(nativeObj);


        }


        //
        // C++:  void cv::img_hash::MarrHildrethHash::setKernelParam(float alpha, float scale)
        //

        /// <summary>
        ///  Set Mh kernel parameters
        /// </summary>
        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public void setKernelParam(float alpha, float scale)
        {
            ThrowIfDisposed();

            img_1hash_MarrHildrethHash_setKernelParam_10(nativeObj, alpha, scale);


        }


        //
        // C++: static Ptr_MarrHildrethHash cv::img_hash::MarrHildrethHash::create(float alpha = 2.0f, float scale = 1.0f)
        //

        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static MarrHildrethHash create(float alpha, float scale)
        {


            return MarrHildrethHash.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(img_1hash_MarrHildrethHash_create_10(alpha, scale)));


        }

        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static MarrHildrethHash create(float alpha)
        {


            return MarrHildrethHash.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(img_1hash_MarrHildrethHash_create_11(alpha)));


        }

        /// <param name="alpha">
        /// int scale factor for marr wavelet (default=2).
        /// </param>
        /// <param name="scale">
        /// int level of scale factor (default = 1)
        /// </param>
        public static MarrHildrethHash create()
        {


            return MarrHildrethHash.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(img_1hash_MarrHildrethHash_create_12()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  float cv::img_hash::MarrHildrethHash::getAlpha()
        [DllImport(LIBNAME)]
        private static extern float img_1hash_MarrHildrethHash_getAlpha_10(IntPtr nativeObj);

        // C++:  float cv::img_hash::MarrHildrethHash::getScale()
        [DllImport(LIBNAME)]
        private static extern float img_1hash_MarrHildrethHash_getScale_10(IntPtr nativeObj);

        // C++:  void cv::img_hash::MarrHildrethHash::setKernelParam(float alpha, float scale)
        [DllImport(LIBNAME)]
        private static extern void img_1hash_MarrHildrethHash_setKernelParam_10(IntPtr nativeObj, float alpha, float scale);

        // C++: static Ptr_MarrHildrethHash cv::img_hash::MarrHildrethHash::create(float alpha = 2.0f, float scale = 1.0f)
        [DllImport(LIBNAME)]
        private static extern IntPtr img_1hash_MarrHildrethHash_create_10(float alpha, float scale);
        [DllImport(LIBNAME)]
        private static extern IntPtr img_1hash_MarrHildrethHash_create_11(float alpha);
        [DllImport(LIBNAME)]
        private static extern IntPtr img_1hash_MarrHildrethHash_create_12();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void img_1hash_MarrHildrethHash_delete(IntPtr nativeObj);

    }
}
