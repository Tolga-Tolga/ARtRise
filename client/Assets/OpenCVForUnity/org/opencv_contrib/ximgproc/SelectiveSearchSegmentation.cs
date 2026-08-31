
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class SelectiveSearchSegmentation
    /// <summary>
    ///  Selective search segmentation algorithm
    ///                          The class implements the algorithm described in @cite uijlings2013selective.
    /// </summary>
    public class SelectiveSearchSegmentation : Algorithm
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
                        ximgproc_SelectiveSearchSegmentation_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal SelectiveSearchSegmentation(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new SelectiveSearchSegmentation __fromPtr__(IntPtr addr) { return new SelectiveSearchSegmentation(addr); }

        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::setBaseImage(Mat img)
        //

        /// <summary>
        ///  Set a image used by switch* functions to initialize the class
        /// </summary>
        /// <param name="img">
        /// The image
        /// </param>
        public void setBaseImage(Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_setBaseImage_10(nativeObj, img.nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSingleStrategy(int k = 200, float sigma = 0.8f)
        //

        /// <summary>
        ///  Initialize the class with the 'Single stragegy' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="k">
        /// The k parameter for the graph segmentation
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSingleStrategy(int k, float sigma)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_10(nativeObj, k, sigma);


        }

        /// <summary>
        ///  Initialize the class with the 'Single stragegy' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="k">
        /// The k parameter for the graph segmentation
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSingleStrategy(int k)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_11(nativeObj, k);


        }

        /// <summary>
        ///  Initialize the class with the 'Single stragegy' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="k">
        /// The k parameter for the graph segmentation
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSingleStrategy()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_12(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSelectiveSearchFast(int base_k = 150, int inc_k = 150, float sigma = 0.8f)
        //

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchFast(int base_k, int inc_k, float sigma)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_10(nativeObj, base_k, inc_k, sigma);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchFast(int base_k, int inc_k)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_11(nativeObj, base_k, inc_k);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchFast(int base_k)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_12(nativeObj, base_k);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchFast()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_13(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSelectiveSearchQuality(int base_k = 150, int inc_k = 150, float sigma = 0.8f)
        //

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchQuality(int base_k, int inc_k, float sigma)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_10(nativeObj, base_k, inc_k, sigma);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchQuality(int base_k, int inc_k)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_11(nativeObj, base_k, inc_k);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchQuality(int base_k)
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_12(nativeObj, base_k);


        }

        /// <summary>
        ///  Initialize the class with the 'Selective search fast' parameters describled in @cite uijlings2013selective.
        /// </summary>
        /// <param name="base_k">
        /// The k parameter for the first graph segmentation
        /// </param>
        /// <param name="inc_k">
        /// The increment of the k parameter for all graph segmentations
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter for the graph segmentation
        /// </param>
        public void switchToSelectiveSearchQuality()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_13(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addImage(Mat img)
        //

        /// <summary>
        ///  Add a new image in the list of images to process.
        /// </summary>
        /// <param name="img">
        /// The image
        /// </param>
        public void addImage(Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_addImage_10(nativeObj, img.nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearImages()
        //

        /// <summary>
        ///  Clear the list of images to process
        /// </summary>
        public void clearImages()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_clearImages_10(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addGraphSegmentation(Ptr_GraphSegmentation g)
        //

        /// <summary>
        ///  Add a new graph segmentation in the list of graph segementations to process.
        /// </summary>
        /// <param name="g">
        /// The graph segmentation
        /// </param>
        public void addGraphSegmentation(GraphSegmentation g)
        {
            ThrowIfDisposed();
            if (g != null) g.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_addGraphSegmentation_10(nativeObj, g.getNativeObjAddr());


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearGraphSegmentations()
        //

        /// <summary>
        ///  Clear the list of graph segmentations to process;
        /// </summary>
        public void clearGraphSegmentations()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_clearGraphSegmentations_10(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addStrategy(Ptr_SelectiveSearchSegmentationStrategy s)
        //

        /// <summary>
        ///  Add a new strategy in the list of strategy to process.
        /// </summary>
        /// <param name="s">
        /// The strategy
        /// </param>
        public void addStrategy(SelectiveSearchSegmentationStrategy s)
        {
            ThrowIfDisposed();
            if (s != null) s.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_addStrategy_10(nativeObj, s.getNativeObjAddr());


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearStrategies()
        //

        /// <summary>
        ///  Clear the list of strategy to process;
        /// </summary>
        public void clearStrategies()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentation_clearStrategies_10(nativeObj);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::process(vector_Rect& rects)
        //

        /// <summary>
        ///  Based on all images, graph segmentations and stragies, computes all possible rects and return them
        /// </summary>
        /// <param name="rects">
        /// The list of rects. The first ones are more relevents than the lasts ones.
        /// </param>
        public void process(MatOfRect rects)
        {
            ThrowIfDisposed();
            if (rects != null) rects.ThrowIfDisposed();
            Mat rects_mat = rects;
            ximgproc_SelectiveSearchSegmentation_process_10(nativeObj, rects_mat.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::setBaseImage(Mat img)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_setBaseImage_10(IntPtr nativeObj, IntPtr img_nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSingleStrategy(int k = 200, float sigma = 0.8f)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_10(IntPtr nativeObj, int k, float sigma);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_11(IntPtr nativeObj, int k);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSingleStrategy_12(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSelectiveSearchFast(int base_k = 150, int inc_k = 150, float sigma = 0.8f)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_10(IntPtr nativeObj, int base_k, int inc_k, float sigma);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_11(IntPtr nativeObj, int base_k, int inc_k);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_12(IntPtr nativeObj, int base_k);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchFast_13(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::switchToSelectiveSearchQuality(int base_k = 150, int inc_k = 150, float sigma = 0.8f)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_10(IntPtr nativeObj, int base_k, int inc_k, float sigma);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_11(IntPtr nativeObj, int base_k, int inc_k);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_12(IntPtr nativeObj, int base_k);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_switchToSelectiveSearchQuality_13(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addImage(Mat img)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_addImage_10(IntPtr nativeObj, IntPtr img_nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearImages()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_clearImages_10(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addGraphSegmentation(Ptr_GraphSegmentation g)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_addGraphSegmentation_10(IntPtr nativeObj, IntPtr g_nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearGraphSegmentations()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_clearGraphSegmentations_10(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::addStrategy(Ptr_SelectiveSearchSegmentationStrategy s)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_addStrategy_10(IntPtr nativeObj, IntPtr s_nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::clearStrategies()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_clearStrategies_10(IntPtr nativeObj);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentation::process(vector_Rect& rects)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_process_10(IntPtr nativeObj, IntPtr rects_mat_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentation_delete(IntPtr nativeObj);

    }
}
