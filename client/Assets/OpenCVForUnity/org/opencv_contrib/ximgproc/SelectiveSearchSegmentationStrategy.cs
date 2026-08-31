
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class SelectiveSearchSegmentationStrategy
    /// <summary>
    ///  Strategie for the selective search segmentation algorithm
    ///                          The class implements a generic stragery for the algorithm described in @cite uijlings2013selective.
    /// </summary>
    public class SelectiveSearchSegmentationStrategy : Algorithm
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
                        ximgproc_SelectiveSearchSegmentationStrategy_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal SelectiveSearchSegmentationStrategy(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new SelectiveSearchSegmentationStrategy __fromPtr__(IntPtr addr) { return new SelectiveSearchSegmentationStrategy(addr); }

        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::setImage(Mat img, Mat regions, Mat sizes, int image_id = -1)
        //

        /// <summary>
        ///  Set a initial image, with a segmentation.
        /// </summary>
        /// <param name="img">
        /// The input image. Any number of channel can be provided
        /// </param>
        /// <param name="regions">
        /// A segmentation of the image. The parameter must be the same size of img.
        /// </param>
        /// <param name="sizes">
        /// The sizes of different regions
        /// </param>
        /// <param name="image_id">
        /// If not set to -1, try to cache pre-computations. If the same set og (img, regions, size) is used, the image_id need to be the same.
        /// </param>
        public void setImage(Mat img, Mat regions, Mat sizes, int image_id)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (regions != null) regions.ThrowIfDisposed();
            if (sizes != null) sizes.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentationStrategy_setImage_10(nativeObj, img.nativeObj, regions.nativeObj, sizes.nativeObj, image_id);


        }

        /// <summary>
        ///  Set a initial image, with a segmentation.
        /// </summary>
        /// <param name="img">
        /// The input image. Any number of channel can be provided
        /// </param>
        /// <param name="regions">
        /// A segmentation of the image. The parameter must be the same size of img.
        /// </param>
        /// <param name="sizes">
        /// The sizes of different regions
        /// </param>
        /// <param name="image_id">
        /// If not set to -1, try to cache pre-computations. If the same set og (img, regions, size) is used, the image_id need to be the same.
        /// </param>
        public void setImage(Mat img, Mat regions, Mat sizes)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (regions != null) regions.ThrowIfDisposed();
            if (sizes != null) sizes.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentationStrategy_setImage_11(nativeObj, img.nativeObj, regions.nativeObj, sizes.nativeObj);


        }


        //
        // C++:  float cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::get(int r1, int r2)
        //

        /// <summary>
        ///  Return the score between two regions (between 0 and 1)
        /// </summary>
        /// <param name="r1">
        /// The first region
        /// </param>
        /// <param name="r2">
        /// The second region
        /// </param>
        public float get(int r1, int r2)
        {
            ThrowIfDisposed();

            return ximgproc_SelectiveSearchSegmentationStrategy_get_10(nativeObj, r1, r2);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::merge(int r1, int r2)
        //

        /// <summary>
        ///  Inform the strategy that two regions will be merged
        /// </summary>
        /// <param name="r1">
        /// The first region
        /// </param>
        /// <param name="r2">
        /// The second region
        /// </param>
        public void merge(int r1, int r2)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentationStrategy_merge_10(nativeObj, r1, r2);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::setImage(Mat img, Mat regions, Mat sizes, int image_id = -1)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategy_setImage_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr regions_nativeObj, IntPtr sizes_nativeObj, int image_id);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategy_setImage_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr regions_nativeObj, IntPtr sizes_nativeObj);

        // C++:  float cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::get(int r1, int r2)
        [DllImport(LIBNAME)]
        private static extern float ximgproc_SelectiveSearchSegmentationStrategy_get_10(IntPtr nativeObj, int r1, int r2);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategy::merge(int r1, int r2)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategy_merge_10(IntPtr nativeObj, int r1, int r2);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategy_delete(IntPtr nativeObj);

    }
}
