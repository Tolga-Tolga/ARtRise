
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Calib3dModule
{

    // C++: class StereoBM
    /// <summary>
    ///  Class for computing stereo correspondence using the block matching algorithm, introduced and contributed to OpenCV by K. Konolige.
    ///     @details This class implements a block matching algorithm for stereo correspondence, which is used to compute disparity maps from stereo image pairs. It provides methods to fine-tune parameters such as pre-filtering, texture thresholds, uniqueness ratios, and regions of interest (ROIs) to optimize performance and accuracy.
    /// </summary>
    public partial class StereoBM : StereoMatcher
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
                        calib3d_StereoBM_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal StereoBM(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new StereoBM __fromPtr__(IntPtr addr) { return new StereoBM(addr); }

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int PREFILTER_NORMALIZED_RESPONSE = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int PREFILTER_XSOBEL = 1;


        //
        // C++:  int cv::StereoBM::getPreFilterType()
        //

        /// <summary>
        ///  Gets the type of pre-filtering currently used in the algorithm.
        /// </summary>
        /// <returns>
        ///  The current pre-filter type: 0 for PREFILTER_NORMALIZED_RESPONSE or 1 for PREFILTER_XSOBEL.
        /// </returns>
        public int getPreFilterType()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getPreFilterType_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setPreFilterType(int preFilterType)
        //

        /// <summary>
        ///  Sets the type of pre-filtering used in the algorithm.
        /// </summary>
        /// <param name="preFilterType">
        /// The type of pre-filter to use. Possible values are:
        ///         - PREFILTER_NORMALIZED_RESPONSE (0): Uses normalized response for pre-filtering.
        ///         - PREFILTER_XSOBEL (1): Uses the X-Sobel operator for pre-filtering.
        ///         @details The pre-filter type affects how the images are prepared before computing the disparity map. Different pre-filtering methods can enhance specific image features or reduce noise, influencing the quality of the disparity map.
        /// </param>
        public void setPreFilterType(int preFilterType)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setPreFilterType_10(nativeObj, preFilterType);


        }


        //
        // C++:  int cv::StereoBM::getPreFilterSize()
        //

        /// <summary>
        ///  Gets the current size of the pre-filter kernel.
        /// </summary>
        /// <returns>
        ///  The current pre-filter size.
        /// </returns>
        public int getPreFilterSize()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getPreFilterSize_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setPreFilterSize(int preFilterSize)
        //

        /// <summary>
        ///  Sets the size of the pre-filter kernel.
        /// </summary>
        /// <param name="preFilterSize">
        /// The size of the pre-filter kernel. Must be an odd integer, typically between 5 and 255.
        ///         @details The pre-filter size determines the spatial extent of the pre-filtering operation, which prepares the images for disparity computation by normalizing brightness and enhancing texture. Larger sizes reduce noise but may blur details, while smaller sizes preserve details but are more susceptible to noise.
        /// </param>
        public void setPreFilterSize(int preFilterSize)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setPreFilterSize_10(nativeObj, preFilterSize);


        }


        //
        // C++:  int cv::StereoBM::getPreFilterCap()
        //

        /// <summary>
        ///  Gets the current truncation value for prefiltered pixels.
        /// </summary>
        /// <returns>
        ///  The current pre-filter cap value.
        /// </returns>
        public int getPreFilterCap()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getPreFilterCap_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setPreFilterCap(int preFilterCap)
        //

        /// <summary>
        ///  Sets the truncation value for prefiltered pixels.
        /// </summary>
        /// <param name="preFilterCap">
        /// The truncation value. Typically in the range [1, 63].
        ///         @details This value caps the output of the pre-filter to [-preFilterCap, preFilterCap], helping to reduce the impact of noise and outliers in the pre-filtered image.
        /// </param>
        public void setPreFilterCap(int preFilterCap)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setPreFilterCap_10(nativeObj, preFilterCap);


        }


        //
        // C++:  int cv::StereoBM::getTextureThreshold()
        //

        /// <summary>
        ///  Gets the current texture threshold value.
        /// </summary>
        /// <returns>
        ///  The current texture threshold.
        /// </returns>
        public int getTextureThreshold()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getTextureThreshold_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setTextureThreshold(int textureThreshold)
        //

        /// <summary>
        ///  Sets the threshold for filtering low-texture regions.
        /// </summary>
        /// <param name="textureThreshold">
        /// The threshold value. Must be non-negative.
        ///         @details This parameter filters out regions with low texture, where establishing correspondences is difficult, thus reducing noise in the disparity map. Higher values filter more aggressively but may discard valid information.
        /// </param>
        public void setTextureThreshold(int textureThreshold)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setTextureThreshold_10(nativeObj, textureThreshold);


        }


        //
        // C++:  int cv::StereoBM::getUniquenessRatio()
        //

        /// <summary>
        ///  Gets the current uniqueness ratio value.
        /// </summary>
        /// <returns>
        ///  The current uniqueness ratio.
        /// </returns>
        public int getUniquenessRatio()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getUniquenessRatio_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setUniquenessRatio(int uniquenessRatio)
        //

        /// <summary>
        ///  Sets the uniqueness ratio for filtering ambiguous matches.
        /// </summary>
        /// <param name="uniquenessRatio">
        /// The uniqueness ratio value. Typically in the range [5, 15], but can be from 0 to 100.
        ///         @details This parameter ensures that the best match is sufficiently better than the next best match, reducing false positives. Higher values are stricter but may filter out valid matches in difficult regions.
        /// </param>
        public void setUniquenessRatio(int uniquenessRatio)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setUniquenessRatio_10(nativeObj, uniquenessRatio);


        }


        //
        // C++:  int cv::StereoBM::getSmallerBlockSize()
        //

        /// <summary>
        ///  Gets the current size of the smaller block used for texture check.
        /// </summary>
        /// <returns>
        ///  The current smaller block size.
        /// </returns>
        public int getSmallerBlockSize()
        {
            ThrowIfDisposed();

            return calib3d_StereoBM_getSmallerBlockSize_10(nativeObj);


        }


        //
        // C++:  void cv::StereoBM::setSmallerBlockSize(int blockSize)
        //

        /// <summary>
        ///  Sets the size of the smaller block used for texture check.
        /// </summary>
        /// <param name="blockSize">
        /// The size of the smaller block. Must be an odd integer between 5 and 255.
        ///         @details This parameter determines the size of the block used to compute texture variance. Smaller blocks capture finer details but are more sensitive to noise, while larger blocks are more robust but may miss fine details.
        /// </param>
        public void setSmallerBlockSize(int blockSize)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setSmallerBlockSize_10(nativeObj, blockSize);


        }


        //
        // C++:  Rect cv::StereoBM::getROI1()
        //

        /// <summary>
        ///  Gets the current Region of Interest (ROI) for the left image.
        /// </summary>
        /// <returns>
        ///  The current ROI for the left image.
        /// </returns>
        public Rect getROI1()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_StereoBM_getROI1_10(nativeObj, tmpArray);
            Rect retVal = new Rect(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::StereoBM::setROI1(Rect roi1)
        //

        /// <summary>
        ///  Sets the Region of Interest (ROI) for the left image.
        /// </summary>
        /// <param name="roi1">
        /// The ROI rectangle for the left image.
        ///         @details By setting the ROI, the stereo matching computation is limited to the specified region, improving performance and potentially accuracy by focusing on relevant parts of the image.
        /// </param>
        public void setROI1(Rect roi1)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setROI1_10(nativeObj, roi1.x, roi1.y, roi1.width, roi1.height);


        }


        //
        // C++:  Rect cv::StereoBM::getROI2()
        //

        /// <summary>
        ///  Gets the current Region of Interest (ROI) for the right image.
        /// </summary>
        /// <returns>
        ///  The current ROI for the right image.
        /// </returns>
        public Rect getROI2()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_StereoBM_getROI2_10(nativeObj, tmpArray);
            Rect retVal = new Rect(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::StereoBM::setROI2(Rect roi2)
        //

        /// <summary>
        ///  Sets the Region of Interest (ROI) for the right image.
        /// </summary>
        /// <param name="roi2">
        /// The ROI rectangle for the right image.
        ///         @details Similar to setROI1, this limits the computation to the specified region in the right image.
        /// </param>
        public void setROI2(Rect roi2)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setROI2_10(nativeObj, roi2.x, roi2.y, roi2.width, roi2.height);


        }


        //
        // C++: static Ptr_StereoBM cv::StereoBM::create(int numDisparities = 0, int blockSize = 21)
        //

        /// <summary>
        ///  Creates StereoBM object
        /// </summary>
        /// <param name="numDisparities">
        /// The disparity search range. For each pixel, the algorithm will find the best disparity from 0 (default minimum disparity) to numDisparities. The search range can be shifted by changing the minimum disparity.
        /// </param>
        /// <param name="blockSize">
        /// The linear size of the blocks compared by the algorithm. The size should be odd (as the block is centered at the current pixel). Larger block size implies smoother, though less accurate disparity map. Smaller block size gives more detailed disparity map, but there is a higher chance for the algorithm to find a wrong correspondence.
        /// </param>
        /// <returns>
        ///  A pointer to the created StereoBM object.
        ///         @details The function creates a StereoBM object. You can then call StereoBM::compute() to compute disparity for a specific stereo pair.
        /// </returns>
        public static StereoBM create(int numDisparities, int blockSize)
        {


            return StereoBM.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(calib3d_StereoBM_create_10(numDisparities, blockSize)));


        }

        /// <summary>
        ///  Creates StereoBM object
        /// </summary>
        /// <param name="numDisparities">
        /// The disparity search range. For each pixel, the algorithm will find the best disparity from 0 (default minimum disparity) to numDisparities. The search range can be shifted by changing the minimum disparity.
        /// </param>
        /// <param name="blockSize">
        /// The linear size of the blocks compared by the algorithm. The size should be odd (as the block is centered at the current pixel). Larger block size implies smoother, though less accurate disparity map. Smaller block size gives more detailed disparity map, but there is a higher chance for the algorithm to find a wrong correspondence.
        /// </param>
        /// <returns>
        ///  A pointer to the created StereoBM object.
        ///         @details The function creates a StereoBM object. You can then call StereoBM::compute() to compute disparity for a specific stereo pair.
        /// </returns>
        public static StereoBM create(int numDisparities)
        {


            return StereoBM.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(calib3d_StereoBM_create_11(numDisparities)));


        }

        /// <summary>
        ///  Creates StereoBM object
        /// </summary>
        /// <param name="numDisparities">
        /// The disparity search range. For each pixel, the algorithm will find the best disparity from 0 (default minimum disparity) to numDisparities. The search range can be shifted by changing the minimum disparity.
        /// </param>
        /// <param name="blockSize">
        /// The linear size of the blocks compared by the algorithm. The size should be odd (as the block is centered at the current pixel). Larger block size implies smoother, though less accurate disparity map. Smaller block size gives more detailed disparity map, but there is a higher chance for the algorithm to find a wrong correspondence.
        /// </param>
        /// <returns>
        ///  A pointer to the created StereoBM object.
        ///         @details The function creates a StereoBM object. You can then call StereoBM::compute() to compute disparity for a specific stereo pair.
        /// </returns>
        public static StereoBM create()
        {


            return StereoBM.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(calib3d_StereoBM_create_12()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  int cv::StereoBM::getPreFilterType()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getPreFilterType_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setPreFilterType(int preFilterType)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setPreFilterType_10(IntPtr nativeObj, int preFilterType);

        // C++:  int cv::StereoBM::getPreFilterSize()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getPreFilterSize_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setPreFilterSize(int preFilterSize)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setPreFilterSize_10(IntPtr nativeObj, int preFilterSize);

        // C++:  int cv::StereoBM::getPreFilterCap()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getPreFilterCap_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setPreFilterCap(int preFilterCap)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setPreFilterCap_10(IntPtr nativeObj, int preFilterCap);

        // C++:  int cv::StereoBM::getTextureThreshold()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getTextureThreshold_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setTextureThreshold(int textureThreshold)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setTextureThreshold_10(IntPtr nativeObj, int textureThreshold);

        // C++:  int cv::StereoBM::getUniquenessRatio()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getUniquenessRatio_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setUniquenessRatio(int uniquenessRatio)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setUniquenessRatio_10(IntPtr nativeObj, int uniquenessRatio);

        // C++:  int cv::StereoBM::getSmallerBlockSize()
        [DllImport(LIBNAME)]
        private static extern int calib3d_StereoBM_getSmallerBlockSize_10(IntPtr nativeObj);

        // C++:  void cv::StereoBM::setSmallerBlockSize(int blockSize)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setSmallerBlockSize_10(IntPtr nativeObj, int blockSize);

        // C++:  Rect cv::StereoBM::getROI1()
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_getROI1_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::StereoBM::setROI1(Rect roi1)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setROI1_10(IntPtr nativeObj, int roi1_x, int roi1_y, int roi1_width, int roi1_height);

        // C++:  Rect cv::StereoBM::getROI2()
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_getROI2_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::StereoBM::setROI2(Rect roi2)
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_setROI2_10(IntPtr nativeObj, int roi2_x, int roi2_y, int roi2_width, int roi2_height);

        // C++: static Ptr_StereoBM cv::StereoBM::create(int numDisparities = 0, int blockSize = 21)
        [DllImport(LIBNAME)]
        private static extern IntPtr calib3d_StereoBM_create_10(int numDisparities, int blockSize);
        [DllImport(LIBNAME)]
        private static extern IntPtr calib3d_StereoBM_create_11(int numDisparities);
        [DllImport(LIBNAME)]
        private static extern IntPtr calib3d_StereoBM_create_12();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void calib3d_StereoBM_delete(IntPtr nativeObj);

    }
}
