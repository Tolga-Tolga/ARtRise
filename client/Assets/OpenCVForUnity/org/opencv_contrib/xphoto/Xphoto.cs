
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.PhotoModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XphotoModule
{
    // C++: class Xphoto


    public class Xphoto
    {

        /// <summary>
        /// C++: enum Bm3dSteps (cv.xphoto.Bm3dSteps)
        /// </summary>
        public const int BM3D_STEPALL = 0;

        /// <summary>
        /// C++: enum Bm3dSteps (cv.xphoto.Bm3dSteps)
        /// </summary>
        public const int BM3D_STEP1 = 1;

        /// <summary>
        /// C++: enum Bm3dSteps (cv.xphoto.Bm3dSteps)
        /// </summary>
        public const int BM3D_STEP2 = 2;


        /// <summary>
        /// C++: enum InpaintTypes (cv.xphoto.InpaintTypes)
        /// </summary>
        public const int INPAINT_SHIFTMAP = 0;

        /// <summary>
        /// C++: enum InpaintTypes (cv.xphoto.InpaintTypes)
        /// </summary>
        public const int INPAINT_FSR_BEST = 1;

        /// <summary>
        /// C++: enum InpaintTypes (cv.xphoto.InpaintTypes)
        /// </summary>
        public const int INPAINT_FSR_FAST = 2;


        /// <summary>
        /// C++: enum TransformTypes (cv.xphoto.TransformTypes)
        /// </summary>
        public const int HAAR = 0;


        //
        // C++:  void cv::xphoto::bm3dDenoising(Mat src, Mat& dstStep1, Mat& dstStep2, float h = 1, int templateWindowSize = 4, int searchWindowSize = 16, int blockMatchingStep1 = 2500, int blockMatchingStep2 = 400, int groupSize = 8, int slidingStep = 1, float beta = 2.0f, int normType = cv::NORM_L2, int step = cv::xphoto::BM3D_STEPALL, int transformType = cv::xphoto::HAAR)
        //

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step, int transformType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_10(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType, step, transformType);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_11(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType, step);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_12(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_13(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_14(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_15(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_16(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_17(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize, int searchWindowSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_18(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize, searchWindowSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h, int templateWindowSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_19(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h, templateWindowSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2, float h)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_110(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj, h);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dstStep1">
        /// Output image of the first step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="dstStep2">
        /// Output image of the second step of BM3D with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Possible variants are: step 1, step 2, both steps.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dstStep1, Mat dstStep2)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dstStep1 != null) dstStep1.ThrowIfDisposed();
            if (dstStep2 != null) dstStep2.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_111(src.nativeObj, dstStep1.nativeObj, dstStep2.nativeObj);


        }


        //
        // C++:  void cv::xphoto::bm3dDenoising(Mat src, Mat& dst, float h = 1, int templateWindowSize = 4, int searchWindowSize = 16, int blockMatchingStep1 = 2500, int blockMatchingStep2 = 400, int groupSize = 8, int slidingStep = 1, float beta = 2.0f, int normType = cv::NORM_L2, int step = cv::xphoto::BM3D_STEPALL, int transformType = cv::xphoto::HAAR)
        //

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step, int transformType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_112(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType, step, transformType);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_113(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType, step);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_114(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta, normType);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_115(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep, beta);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_116(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize, slidingStep);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_117(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2, groupSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_118(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1, blockMatchingStep2);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_119(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize, blockMatchingStep1);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_120(src.nativeObj, dst.nativeObj, h, templateWindowSize, searchWindowSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h, int templateWindowSize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_121(src.nativeObj, dst.nativeObj, h, templateWindowSize);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst, float h)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_122(src.nativeObj, dst.nativeObj, h);


        }

        /// <summary>
        ///  Performs image denoising using the Block-Matching and 3D-filtering algorithm
        ///          &lt;http://www.cs.tut.fi/~foi/GCF-BM3D/BM3D_TIP_2007.pdf&gt; with several computational
        ///          optimizations. Noise expected to be a gaussian white noise.
        /// </summary>
        /// <param name="src">
        /// Input 8-bit or 16-bit 1-channel image.
        /// </param>
        /// <param name="dst">
        /// Output image with the same size and type as src.
        /// </param>
        /// <param name="h">
        /// Parameter regulating filter strength. Big h value perfectly removes noise but also
        ///          removes image details, smaller h value preserves details but also preserves some noise.
        /// </param>
        /// <param name="templateWindowSize">
        /// Size in pixels of the template patch that is used for block-matching.
        ///          Should be power of 2.
        /// </param>
        /// <param name="searchWindowSize">
        /// Size in pixels of the window that is used to perform block-matching.
        ///          Affect performance linearly: greater searchWindowsSize - greater denoising time.
        ///          Must be larger than templateWindowSize.
        /// </param>
        /// <param name="blockMatchingStep1">
        /// Block matching threshold for the first step of BM3D (hard thresholding),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="blockMatchingStep2">
        /// Block matching threshold for the second step of BM3D (Wiener filtering),
        ///          i.e. maximum distance for which two blocks are considered similar.
        ///          Value expressed in euclidean distance.
        /// </param>
        /// <param name="groupSize">
        /// Maximum size of the 3D group for collaborative filtering.
        /// </param>
        /// <param name="slidingStep">
        /// Sliding step to process every next reference block.
        /// </param>
        /// <param name="beta">
        /// Kaiser window parameter that affects the sidelobe attenuation of the transform of the
        ///          window. Kaiser window is used in order to reduce border effects. To prevent usage of the window,
        ///          set beta to zero.
        /// </param>
        /// <param name="normType">
        /// Norm used to calculate distance between blocks. L2 is slower than L1
        ///          but yields more accurate results.
        /// </param>
        /// <param name="step">
        /// Step of BM3D to be executed. Allowed are only BM3D_STEP1 and BM3D_STEPALL.
        ///          BM3D_STEP2 is not allowed as it requires basic estimate to be present.
        /// </param>
        /// <param name="transformType">
        /// Type of the orthogonal transform used in collaborative filtering step.
        ///          Currently only Haar transform is supported.
        /// </param>
        /// <remarks>
        ///          This function expected to be applied to grayscale images. Advanced usage of this function
        ///          can be manual denoising of colored image in different colorspaces.
        ///  
        ///          @sa
        ///          fastNlMeansDenoising
        /// </remarks>
        public static void bm3dDenoising(Mat src, Mat dst)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_bm3dDenoising_123(src.nativeObj, dst.nativeObj);


        }


        //
        // C++:  void cv::xphoto::dctDenoising(Mat src, Mat dst, double sigma, int psize = 16)
        //

        /// <summary>
        ///  The function implements simple dct-based denoising
        /// </summary>
        /// <remarks>
        ///      &lt;http://www.ipol.im/pub/art/2011/ys-dct/&gt;.
        /// </remarks>
        /// <param name="src">
        /// source image
        /// </param>
        /// <param name="dst">
        /// destination image
        /// </param>
        /// <param name="sigma">
        /// expected noise standard deviation
        /// </param>
        /// <param name="psize">
        /// size of block side where dct is computed
        /// </param>
        /// <remarks>
        ///      @sa
        ///         fastNlMeansDenoising
        /// </remarks>
        public static void dctDenoising(Mat src, Mat dst, double sigma, int psize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_dctDenoising_10(src.nativeObj, dst.nativeObj, sigma, psize);


        }

        /// <summary>
        ///  The function implements simple dct-based denoising
        /// </summary>
        /// <remarks>
        ///      &lt;http://www.ipol.im/pub/art/2011/ys-dct/&gt;.
        /// </remarks>
        /// <param name="src">
        /// source image
        /// </param>
        /// <param name="dst">
        /// destination image
        /// </param>
        /// <param name="sigma">
        /// expected noise standard deviation
        /// </param>
        /// <param name="psize">
        /// size of block side where dct is computed
        /// </param>
        /// <remarks>
        ///      @sa
        ///         fastNlMeansDenoising
        /// </remarks>
        public static void dctDenoising(Mat src, Mat dst, double sigma)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_dctDenoising_11(src.nativeObj, dst.nativeObj, sigma);


        }


        //
        // C++:  void cv::xphoto::inpaint(Mat src, Mat mask, Mat dst, int algorithmType)
        //

        /// <summary>
        ///  The function implements different single-image inpainting algorithms.
        /// </summary>
        /// <remarks>
        ///      See the original papers @cite He2012 (Shiftmap) or @cite GenserPCS2018 and @cite SeilerTIP2015 (FSR) for details.
        /// </remarks>
        /// <param name="src">
        /// source image
        ///      - #INPAINT_SHIFTMAP: it could be of any type and any number of channels from 1 to 4. In case of
        ///      3- and 4-channels images the function expect them in CIELab colorspace or similar one, where first
        ///      color component shows intensity, while second and third shows colors. Nonetheless you can try any
        ///      colorspaces.
        ///      - #INPAINT_FSR_BEST or #INPAINT_FSR_FAST: 1-channel grayscale or 3-channel BGR image.
        /// </param>
        /// <param name="mask">
        /// mask (#CV_8UC1), where non-zero pixels indicate valid image area, while zero pixels
        ///      indicate area to be inpainted
        /// </param>
        /// <param name="dst">
        /// destination image
        /// </param>
        /// <param name="algorithmType">
        /// see xphoto::InpaintTypes
        /// </param>
        public static void inpaint(Mat src, Mat mask, Mat dst, int algorithmType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_inpaint_10(src.nativeObj, mask.nativeObj, dst.nativeObj, algorithmType);


        }


        //
        // C++:  void cv::xphoto::oilPainting(Mat src, Mat& dst, int size, int dynRatio, int code)
        //

        /// <summary>
        ///  oilPainting
        ///  See the book @cite Holzmann1988 for details.
        /// </summary>
        /// <param name="src">
        /// Input three-channel or one channel image (either CV_8UC3 or CV_8UC1)
        /// </param>
        /// <param name="dst">
        /// Output image of the same size and type as src.
        /// </param>
        /// <param name="size">
        /// neighbouring size is 2-size+1
        /// </param>
        /// <param name="dynRatio">
        /// image is divided by dynRatio before histogram processing
        /// </param>
        /// <param name="code">
        /// color space conversion code(see ColorConversionCodes). Histogram will used only first plane
        /// </param>
        public static void oilPainting(Mat src, Mat dst, int size, int dynRatio, int code)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_oilPainting_10(src.nativeObj, dst.nativeObj, size, dynRatio, code);


        }


        //
        // C++:  void cv::xphoto::oilPainting(Mat src, Mat& dst, int size, int dynRatio)
        //

        /// <summary>
        ///  oilPainting
        ///  See the book @cite Holzmann1988 for details.
        /// </summary>
        /// <param name="src">
        /// Input three-channel or one channel image (either CV_8UC3 or CV_8UC1)
        /// </param>
        /// <param name="dst">
        /// Output image of the same size and type as src.
        /// </param>
        /// <param name="size">
        /// neighbouring size is 2-size+1
        /// </param>
        /// <param name="dynRatio">
        /// image is divided by dynRatio before histogram processing
        /// </param>
        public static void oilPainting(Mat src, Mat dst, int size, int dynRatio)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_oilPainting_11(src.nativeObj, dst.nativeObj, size, dynRatio);


        }


        //
        // C++:  Ptr_TonemapDurand cv::xphoto::createTonemapDurand(float gamma = 1.0f, float contrast = 4.0f, float saturation = 1.0f, float sigma_color = 2.0f, float sigma_space = 2.0f)
        //

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand(float gamma, float contrast, float saturation, float sigma_color, float sigma_space)
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_10(gamma, contrast, saturation, sigma_color, sigma_space)));


        }

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand(float gamma, float contrast, float saturation, float sigma_color)
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_11(gamma, contrast, saturation, sigma_color)));


        }

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand(float gamma, float contrast, float saturation)
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_12(gamma, contrast, saturation)));


        }

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand(float gamma, float contrast)
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_13(gamma, contrast)));


        }

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand(float gamma)
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_14(gamma)));


        }

        /// <summary>
        ///  Creates TonemapDurand object
        /// </summary>
        /// <remarks>
        ///  You need to set the OPENCV_ENABLE_NONFREE option in cmake to use those. Use them at your own risk.
        /// </remarks>
        /// <param name="gamma">
        /// gamma value for gamma correction. See createTonemap
        /// </param>
        /// <param name="contrast">
        /// resulting contrast on logarithmic scale, i. e. log(max / min), where max and min
        ///  are maximum and minimum luminance values of the resulting image.
        /// </param>
        /// <param name="saturation">
        /// saturation enhancement value. See createTonemapDrago
        /// </param>
        /// <param name="sigma_color">
        /// bilateral filter sigma in color space
        /// </param>
        /// <param name="sigma_space">
        /// bilateral filter sigma in coordinate space
        /// </param>
        public static TonemapDurand createTonemapDurand()
        {


            return TonemapDurand.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createTonemapDurand_15()));


        }


        //
        // C++:  Ptr_SimpleWB cv::xphoto::createSimpleWB()
        //

        /// <summary>
        ///  Creates an instance of SimpleWB
        /// </summary>
        public static SimpleWB createSimpleWB()
        {


            return SimpleWB.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createSimpleWB_10()));


        }


        //
        // C++:  Ptr_GrayworldWB cv::xphoto::createGrayworldWB()
        //

        /// <summary>
        ///  Creates an instance of GrayworldWB
        /// </summary>
        public static GrayworldWB createGrayworldWB()
        {


            return GrayworldWB.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createGrayworldWB_10()));


        }


        //
        // C++:  Ptr_LearningBasedWB cv::xphoto::createLearningBasedWB(String path_to_model = String())
        //

        /// <summary>
        ///  Creates an instance of LearningBasedWB
        /// </summary>
        /// <param name="path_to_model">
        /// Path to a .yml file with the model. If not specified, the default model is used
        /// </param>
        public static LearningBasedWB createLearningBasedWB(string path_to_model)
        {


            return LearningBasedWB.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createLearningBasedWB_10(path_to_model)));


        }

        /// <summary>
        ///  Creates an instance of LearningBasedWB
        /// </summary>
        /// <param name="path_to_model">
        /// Path to a .yml file with the model. If not specified, the default model is used
        /// </param>
        public static LearningBasedWB createLearningBasedWB()
        {


            return LearningBasedWB.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(xphoto_Xphoto_createLearningBasedWB_11()));


        }


        //
        // C++:  void cv::xphoto::applyChannelGains(Mat src, Mat& dst, float gainB, float gainG, float gainR)
        //

        /// <summary>
        ///  Implements an efficient fixed-point approximation for applying channel gains, which is
        ///      the last step of multiple white balance algorithms.
        /// </summary>
        /// <param name="src">
        /// Input three-channel image in the BGR color space (either CV_8UC3 or CV_16UC3)
        /// </param>
        /// <param name="dst">
        /// Output image of the same size and type as src.
        /// </param>
        /// <param name="gainB">
        /// gain for the B channel
        /// </param>
        /// <param name="gainG">
        /// gain for the G channel
        /// </param>
        /// <param name="gainR">
        /// gain for the R channel
        /// </param>
        public static void applyChannelGains(Mat src, Mat dst, float gainB, float gainG, float gainR)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            xphoto_Xphoto_applyChannelGains_10(src.nativeObj, dst.nativeObj, gainB, gainG, gainR);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::xphoto::bm3dDenoising(Mat src, Mat& dstStep1, Mat& dstStep2, float h = 1, int templateWindowSize = 4, int searchWindowSize = 16, int blockMatchingStep1 = 2500, int blockMatchingStep2 = 400, int groupSize = 8, int slidingStep = 1, float beta = 2.0f, int normType = cv::NORM_L2, int step = cv::xphoto::BM3D_STEPALL, int transformType = cv::xphoto::HAAR)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_10(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step, int transformType);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_11(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_12(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_13(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_14(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_15(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_16(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_17(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_18(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize, int searchWindowSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_19(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h, int templateWindowSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_110(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj, float h);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_111(IntPtr src_nativeObj, IntPtr dstStep1_nativeObj, IntPtr dstStep2_nativeObj);

        // C++:  void cv::xphoto::bm3dDenoising(Mat src, Mat& dst, float h = 1, int templateWindowSize = 4, int searchWindowSize = 16, int blockMatchingStep1 = 2500, int blockMatchingStep2 = 400, int groupSize = 8, int slidingStep = 1, float beta = 2.0f, int normType = cv::NORM_L2, int step = cv::xphoto::BM3D_STEPALL, int transformType = cv::xphoto::HAAR)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_112(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step, int transformType);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_113(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType, int step);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_114(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta, int normType);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_115(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep, float beta);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_116(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize, int slidingStep);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_117(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2, int groupSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_118(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1, int blockMatchingStep2);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_119(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize, int blockMatchingStep1);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_120(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize, int searchWindowSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_121(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h, int templateWindowSize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_122(IntPtr src_nativeObj, IntPtr dst_nativeObj, float h);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_bm3dDenoising_123(IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // C++:  void cv::xphoto::dctDenoising(Mat src, Mat dst, double sigma, int psize = 16)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_dctDenoising_10(IntPtr src_nativeObj, IntPtr dst_nativeObj, double sigma, int psize);
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_dctDenoising_11(IntPtr src_nativeObj, IntPtr dst_nativeObj, double sigma);

        // C++:  void cv::xphoto::inpaint(Mat src, Mat mask, Mat dst, int algorithmType)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_inpaint_10(IntPtr src_nativeObj, IntPtr mask_nativeObj, IntPtr dst_nativeObj, int algorithmType);

        // C++:  void cv::xphoto::oilPainting(Mat src, Mat& dst, int size, int dynRatio, int code)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_oilPainting_10(IntPtr src_nativeObj, IntPtr dst_nativeObj, int size, int dynRatio, int code);

        // C++:  void cv::xphoto::oilPainting(Mat src, Mat& dst, int size, int dynRatio)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_oilPainting_11(IntPtr src_nativeObj, IntPtr dst_nativeObj, int size, int dynRatio);

        // C++:  Ptr_TonemapDurand cv::xphoto::createTonemapDurand(float gamma = 1.0f, float contrast = 4.0f, float saturation = 1.0f, float sigma_color = 2.0f, float sigma_space = 2.0f)
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_10(float gamma, float contrast, float saturation, float sigma_color, float sigma_space);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_11(float gamma, float contrast, float saturation, float sigma_color);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_12(float gamma, float contrast, float saturation);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_13(float gamma, float contrast);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_14(float gamma);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createTonemapDurand_15();

        // C++:  Ptr_SimpleWB cv::xphoto::createSimpleWB()
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createSimpleWB_10();

        // C++:  Ptr_GrayworldWB cv::xphoto::createGrayworldWB()
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createGrayworldWB_10();

        // C++:  Ptr_LearningBasedWB cv::xphoto::createLearningBasedWB(String path_to_model = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createLearningBasedWB_10(string path_to_model);
        [DllImport(LIBNAME)]
        private static extern IntPtr xphoto_Xphoto_createLearningBasedWB_11();

        // C++:  void cv::xphoto::applyChannelGains(Mat src, Mat& dst, float gainB, float gainG, float gainR)
        [DllImport(LIBNAME)]
        private static extern void xphoto_Xphoto_applyChannelGains_10(IntPtr src_nativeObj, IntPtr dst_nativeObj, float gainB, float gainG, float gainR);

    }
}
