
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class Imgproc
    {


        //
        // C++:  Mat cv::getGaborKernel(Size ksize, double sigma, double theta, double lambd, double gamma, double psi = CV_PI*0.5, int ktype = CV_64F)
        //

        /// <summary>
        ///  Returns Gabor filter coefficients.
        /// </summary>
        /// <remarks>
        ///  For more details about gabor filter equations and parameters, see: [Gabor
        ///  Filter](https://en.wikipedia.org/wiki/Gabor_filter).
        /// </remarks>
        /// <param name="ksize">
        /// Size of the filter returned.
        /// </param>
        /// <param name="sigma">
        /// Standard deviation of the gaussian envelope.
        /// </param>
        /// <param name="theta">
        /// Orientation of the normal to the parallel stripes of a Gabor function.
        /// </param>
        /// <param name="lambd">
        /// Wavelength of the sinusoidal factor.
        /// </param>
        /// <param name="gamma">
        /// Spatial aspect ratio.
        /// </param>
        /// <param name="psi">
        /// Phase offset.
        /// </param>
        /// <param name="ktype">
        /// Type of filter coefficients. It can be CV_32F or CV_64F .
        /// </param>
        public static Mat getGaborKernel(in Vec2d ksize, double sigma, double theta, double lambd, double gamma, double psi, int ktype)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getGaborKernel_10(ksize.Item1, ksize.Item2, sigma, theta, lambd, gamma, psi, ktype)));


        }

        /// <summary>
        ///  Returns Gabor filter coefficients.
        /// </summary>
        /// <remarks>
        ///  For more details about gabor filter equations and parameters, see: [Gabor
        ///  Filter](https://en.wikipedia.org/wiki/Gabor_filter).
        /// </remarks>
        /// <param name="ksize">
        /// Size of the filter returned.
        /// </param>
        /// <param name="sigma">
        /// Standard deviation of the gaussian envelope.
        /// </param>
        /// <param name="theta">
        /// Orientation of the normal to the parallel stripes of a Gabor function.
        /// </param>
        /// <param name="lambd">
        /// Wavelength of the sinusoidal factor.
        /// </param>
        /// <param name="gamma">
        /// Spatial aspect ratio.
        /// </param>
        /// <param name="psi">
        /// Phase offset.
        /// </param>
        /// <param name="ktype">
        /// Type of filter coefficients. It can be CV_32F or CV_64F .
        /// </param>
        public static Mat getGaborKernel(in Vec2d ksize, double sigma, double theta, double lambd, double gamma, double psi)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getGaborKernel_11(ksize.Item1, ksize.Item2, sigma, theta, lambd, gamma, psi)));


        }

        /// <summary>
        ///  Returns Gabor filter coefficients.
        /// </summary>
        /// <remarks>
        ///  For more details about gabor filter equations and parameters, see: [Gabor
        ///  Filter](https://en.wikipedia.org/wiki/Gabor_filter).
        /// </remarks>
        /// <param name="ksize">
        /// Size of the filter returned.
        /// </param>
        /// <param name="sigma">
        /// Standard deviation of the gaussian envelope.
        /// </param>
        /// <param name="theta">
        /// Orientation of the normal to the parallel stripes of a Gabor function.
        /// </param>
        /// <param name="lambd">
        /// Wavelength of the sinusoidal factor.
        /// </param>
        /// <param name="gamma">
        /// Spatial aspect ratio.
        /// </param>
        /// <param name="psi">
        /// Phase offset.
        /// </param>
        /// <param name="ktype">
        /// Type of filter coefficients. It can be CV_32F or CV_64F .
        /// </param>
        public static Mat getGaborKernel(in Vec2d ksize, double sigma, double theta, double lambd, double gamma)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getGaborKernel_12(ksize.Item1, ksize.Item2, sigma, theta, lambd, gamma)));


        }


        //
        // C++:  Mat cv::getStructuringElement(int shape, Size ksize, Point anchor = Point(-1,-1))
        //

        /// <summary>
        ///  Returns a structuring element of the specified size and shape for morphological operations.
        /// </summary>
        /// <remarks>
        ///  The function constructs and returns the structuring element that can be further passed to #erode,
        ///  #dilate or #morphologyEx. But you can also construct an arbitrary binary mask yourself and use it as
        ///  the structuring element.
        /// </remarks>
        /// <param name="shape">
        /// Element shape that could be one of #MorphShapes
        /// </param>
        /// <param name="ksize">
        /// Size of the structuring element.
        /// </param>
        /// <param name="anchor">
        /// Anchor position within the element. The default value \f$(-1, -1)\f$ means that the
        ///  anchor is at the center. Note that only the shape of a cross-shaped element depends on the anchor
        ///  position. In other cases the anchor just regulates how much the result of the morphological
        ///  operation is shifted.
        /// </param>
        public static Mat getStructuringElement(int shape, in Vec2d ksize, in Vec2d anchor)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getStructuringElement_10(shape, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2)));


        }

        /// <summary>
        ///  Returns a structuring element of the specified size and shape for morphological operations.
        /// </summary>
        /// <remarks>
        ///  The function constructs and returns the structuring element that can be further passed to #erode,
        ///  #dilate or #morphologyEx. But you can also construct an arbitrary binary mask yourself and use it as
        ///  the structuring element.
        /// </remarks>
        /// <param name="shape">
        /// Element shape that could be one of #MorphShapes
        /// </param>
        /// <param name="ksize">
        /// Size of the structuring element.
        /// </param>
        /// <param name="anchor">
        /// Anchor position within the element. The default value \f$(-1, -1)\f$ means that the
        ///  anchor is at the center. Note that only the shape of a cross-shaped element depends on the anchor
        ///  position. In other cases the anchor just regulates how much the result of the morphological
        ///  operation is shifted.
        /// </param>
        public static Mat getStructuringElement(int shape, in Vec2d ksize)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getStructuringElement_11(shape, ksize.Item1, ksize.Item2)));


        }


        //
        // C++:  void cv::GaussianBlur(Mat src, Mat& dst, Size ksize, double sigmaX, double sigmaY = 0, int borderType = BORDER_DEFAULT, AlgorithmHint hint = cv::ALGO_HINT_DEFAULT)
        //

        /// <summary>
        ///  Blurs an image using a Gaussian filter.
        /// </summary>
        /// <remarks>
        ///  The function convolves the source image with the specified Gaussian kernel. In-place filtering is
        ///  supported.
        /// </remarks>
        /// <param name="src">
        /// input image; the image can have any number of channels, which are processed
        ///  independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be
        ///  positive and odd. Or, they can be zero's and then they are computed from sigma.
        /// </param>
        /// <param name="sigmaX">
        /// Gaussian kernel standard deviation in X direction.
        /// </param>
        /// <param name="sigmaY">
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be
        ///  equal to sigmaX, if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        ///  respectively (see #getGaussianKernel for details); to fully control the result regardless of
        ///  possible future modifications of all this semantics, it is recommended to specify all of ksize,
        ///  sigmaX, and sigmaY.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="hint">
        /// Implementation modfication flags. See #AlgorithmHint
        /// </param>
        /// <remarks>
        ///  @sa  sepFilter2D, filter2D, blur, boxFilter, bilateralFilter, medianBlur
        /// </remarks>
        public static void GaussianBlur(Mat src, Mat dst, in Vec2d ksize, double sigmaX, double sigmaY, int borderType, int hint)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_GaussianBlur_10(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, sigmaX, sigmaY, borderType, hint);


        }

        /// <summary>
        ///  Blurs an image using a Gaussian filter.
        /// </summary>
        /// <remarks>
        ///  The function convolves the source image with the specified Gaussian kernel. In-place filtering is
        ///  supported.
        /// </remarks>
        /// <param name="src">
        /// input image; the image can have any number of channels, which are processed
        ///  independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be
        ///  positive and odd. Or, they can be zero's and then they are computed from sigma.
        /// </param>
        /// <param name="sigmaX">
        /// Gaussian kernel standard deviation in X direction.
        /// </param>
        /// <param name="sigmaY">
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be
        ///  equal to sigmaX, if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        ///  respectively (see #getGaussianKernel for details); to fully control the result regardless of
        ///  possible future modifications of all this semantics, it is recommended to specify all of ksize,
        ///  sigmaX, and sigmaY.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="hint">
        /// Implementation modfication flags. See #AlgorithmHint
        /// </param>
        /// <remarks>
        ///  @sa  sepFilter2D, filter2D, blur, boxFilter, bilateralFilter, medianBlur
        /// </remarks>
        public static void GaussianBlur(Mat src, Mat dst, in Vec2d ksize, double sigmaX, double sigmaY, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_GaussianBlur_11(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, sigmaX, sigmaY, borderType);


        }

        /// <summary>
        ///  Blurs an image using a Gaussian filter.
        /// </summary>
        /// <remarks>
        ///  The function convolves the source image with the specified Gaussian kernel. In-place filtering is
        ///  supported.
        /// </remarks>
        /// <param name="src">
        /// input image; the image can have any number of channels, which are processed
        ///  independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be
        ///  positive and odd. Or, they can be zero's and then they are computed from sigma.
        /// </param>
        /// <param name="sigmaX">
        /// Gaussian kernel standard deviation in X direction.
        /// </param>
        /// <param name="sigmaY">
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be
        ///  equal to sigmaX, if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        ///  respectively (see #getGaussianKernel for details); to fully control the result regardless of
        ///  possible future modifications of all this semantics, it is recommended to specify all of ksize,
        ///  sigmaX, and sigmaY.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="hint">
        /// Implementation modfication flags. See #AlgorithmHint
        /// </param>
        /// <remarks>
        ///  @sa  sepFilter2D, filter2D, blur, boxFilter, bilateralFilter, medianBlur
        /// </remarks>
        public static void GaussianBlur(Mat src, Mat dst, in Vec2d ksize, double sigmaX, double sigmaY)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_GaussianBlur_12(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, sigmaX, sigmaY);


        }

        /// <summary>
        ///  Blurs an image using a Gaussian filter.
        /// </summary>
        /// <remarks>
        ///  The function convolves the source image with the specified Gaussian kernel. In-place filtering is
        ///  supported.
        /// </remarks>
        /// <param name="src">
        /// input image; the image can have any number of channels, which are processed
        ///  independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be
        ///  positive and odd. Or, they can be zero's and then they are computed from sigma.
        /// </param>
        /// <param name="sigmaX">
        /// Gaussian kernel standard deviation in X direction.
        /// </param>
        /// <param name="sigmaY">
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be
        ///  equal to sigmaX, if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        ///  respectively (see #getGaussianKernel for details); to fully control the result regardless of
        ///  possible future modifications of all this semantics, it is recommended to specify all of ksize,
        ///  sigmaX, and sigmaY.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="hint">
        /// Implementation modfication flags. See #AlgorithmHint
        /// </param>
        /// <remarks>
        ///  @sa  sepFilter2D, filter2D, blur, boxFilter, bilateralFilter, medianBlur
        /// </remarks>
        public static void GaussianBlur(Mat src, Mat dst, in Vec2d ksize, double sigmaX)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_GaussianBlur_13(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, sigmaX);


        }


        //
        // C++:  void cv::boxFilter(Mat src, Mat& dst, int ddepth, Size ksize, Point anchor = Point(-1,-1), bool normalize = true, int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Blurs an image using the box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \alpha \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1 \end{bmatrix}\f]
        ///  
        ///  where
        ///  
        ///  \f[\alpha = \begin{cases} \frac{1}{\texttt{ksize.width*ksize.height}} & \texttt{when } \texttt{normalize=true}  \\1 & \texttt{otherwise}\end{cases}\f]
        ///  
        ///  Unnormalized box filter is useful for computing various integral characteristics over each pixel
        ///  neighborhood, such as covariance matrices of image derivatives (used in dense optical flow
        ///  algorithms, and so on). If you need to compute pixel sums over variable-size windows, use #integral.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth()).
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is normalized by its area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  blur, bilateralFilter, GaussianBlur, medianBlur, integral
        /// </param>
        public static void boxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor, bool normalize, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_boxFilter_10(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2, normalize, borderType);


        }

        /// <summary>
        ///  Blurs an image using the box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \alpha \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1 \end{bmatrix}\f]
        ///  
        ///  where
        ///  
        ///  \f[\alpha = \begin{cases} \frac{1}{\texttt{ksize.width*ksize.height}} & \texttt{when } \texttt{normalize=true}  \\1 & \texttt{otherwise}\end{cases}\f]
        ///  
        ///  Unnormalized box filter is useful for computing various integral characteristics over each pixel
        ///  neighborhood, such as covariance matrices of image derivatives (used in dense optical flow
        ///  algorithms, and so on). If you need to compute pixel sums over variable-size windows, use #integral.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth()).
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is normalized by its area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  blur, bilateralFilter, GaussianBlur, medianBlur, integral
        /// </param>
        public static void boxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor, bool normalize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_boxFilter_11(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2, normalize);


        }

        /// <summary>
        ///  Blurs an image using the box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \alpha \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1 \end{bmatrix}\f]
        ///  
        ///  where
        ///  
        ///  \f[\alpha = \begin{cases} \frac{1}{\texttt{ksize.width*ksize.height}} & \texttt{when } \texttt{normalize=true}  \\1 & \texttt{otherwise}\end{cases}\f]
        ///  
        ///  Unnormalized box filter is useful for computing various integral characteristics over each pixel
        ///  neighborhood, such as covariance matrices of image derivatives (used in dense optical flow
        ///  algorithms, and so on). If you need to compute pixel sums over variable-size windows, use #integral.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth()).
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is normalized by its area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  blur, bilateralFilter, GaussianBlur, medianBlur, integral
        /// </param>
        public static void boxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_boxFilter_12(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2);


        }

        /// <summary>
        ///  Blurs an image using the box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \alpha \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1 \end{bmatrix}\f]
        ///  
        ///  where
        ///  
        ///  \f[\alpha = \begin{cases} \frac{1}{\texttt{ksize.width*ksize.height}} & \texttt{when } \texttt{normalize=true}  \\1 & \texttt{otherwise}\end{cases}\f]
        ///  
        ///  Unnormalized box filter is useful for computing various integral characteristics over each pixel
        ///  neighborhood, such as covariance matrices of image derivatives (used in dense optical flow
        ///  algorithms, and so on). If you need to compute pixel sums over variable-size windows, use #integral.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth()).
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is normalized by its area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  blur, bilateralFilter, GaussianBlur, medianBlur, integral
        /// </param>
        public static void boxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_boxFilter_13(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2);


        }


        //
        // C++:  void cv::sqrBoxFilter(Mat src, Mat& dst, int ddepth, Size ksize, Point anchor = Point(-1, -1), bool normalize = true, int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Calculates the normalized sum of squares of the pixel values overlapping the filter.
        /// </summary>
        /// <remarks>
        ///  For every pixel \f$ (x, y) \f$ in the source image, the function calculates the sum of squares of those neighboring
        ///  pixel values which overlap the filter placed over the pixel \f$ (x, y) \f$.
        ///  
        ///  The unnormalized square box filter can be useful in computing local image statistics such as the local
        ///  variance and standard deviation around the neighborhood of a pixel.
        /// </remarks>
        /// <param name="src">
        /// input image
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth())
        /// </param>
        /// <param name="ksize">
        /// kernel size
        /// </param>
        /// <param name="anchor">
        /// kernel anchor point. The default value of Point(-1, -1) denotes that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is to be normalized by it's area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa boxFilter
        /// </param>
        public static void sqrBoxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor, bool normalize, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_sqrBoxFilter_10(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2, normalize, borderType);


        }

        /// <summary>
        ///  Calculates the normalized sum of squares of the pixel values overlapping the filter.
        /// </summary>
        /// <remarks>
        ///  For every pixel \f$ (x, y) \f$ in the source image, the function calculates the sum of squares of those neighboring
        ///  pixel values which overlap the filter placed over the pixel \f$ (x, y) \f$.
        ///  
        ///  The unnormalized square box filter can be useful in computing local image statistics such as the local
        ///  variance and standard deviation around the neighborhood of a pixel.
        /// </remarks>
        /// <param name="src">
        /// input image
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth())
        /// </param>
        /// <param name="ksize">
        /// kernel size
        /// </param>
        /// <param name="anchor">
        /// kernel anchor point. The default value of Point(-1, -1) denotes that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is to be normalized by it's area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa boxFilter
        /// </param>
        public static void sqrBoxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor, bool normalize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_sqrBoxFilter_11(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2, normalize);


        }

        /// <summary>
        ///  Calculates the normalized sum of squares of the pixel values overlapping the filter.
        /// </summary>
        /// <remarks>
        ///  For every pixel \f$ (x, y) \f$ in the source image, the function calculates the sum of squares of those neighboring
        ///  pixel values which overlap the filter placed over the pixel \f$ (x, y) \f$.
        ///  
        ///  The unnormalized square box filter can be useful in computing local image statistics such as the local
        ///  variance and standard deviation around the neighborhood of a pixel.
        /// </remarks>
        /// <param name="src">
        /// input image
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth())
        /// </param>
        /// <param name="ksize">
        /// kernel size
        /// </param>
        /// <param name="anchor">
        /// kernel anchor point. The default value of Point(-1, -1) denotes that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is to be normalized by it's area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa boxFilter
        /// </param>
        public static void sqrBoxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_sqrBoxFilter_12(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2);


        }

        /// <summary>
        ///  Calculates the normalized sum of squares of the pixel values overlapping the filter.
        /// </summary>
        /// <remarks>
        ///  For every pixel \f$ (x, y) \f$ in the source image, the function calculates the sum of squares of those neighboring
        ///  pixel values which overlap the filter placed over the pixel \f$ (x, y) \f$.
        ///  
        ///  The unnormalized square box filter can be useful in computing local image statistics such as the local
        ///  variance and standard deviation around the neighborhood of a pixel.
        /// </remarks>
        /// <param name="src">
        /// input image
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src
        /// </param>
        /// <param name="ddepth">
        /// the output image depth (-1 to use src.depth())
        /// </param>
        /// <param name="ksize">
        /// kernel size
        /// </param>
        /// <param name="anchor">
        /// kernel anchor point. The default value of Point(-1, -1) denotes that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="normalize">
        /// flag, specifying whether the kernel is to be normalized by it's area or not.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa boxFilter
        /// </param>
        public static void sqrBoxFilter(Mat src, Mat dst, int ddepth, in Vec2d ksize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_sqrBoxFilter_13(src.nativeObj, dst.nativeObj, ddepth, ksize.Item1, ksize.Item2);


        }


        //
        // C++:  void cv::blur(Mat src, Mat& dst, Size ksize, Point anchor = Point(-1,-1), int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Blurs an image using the normalized box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \frac{1}{\texttt{ksize.width*ksize.height}} \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \end{bmatrix}\f]
        ///  
        ///  The call `blur(src, dst, ksize, anchor, borderType)` is equivalent to `boxFilter(src, dst, src.type(), ksize,
        ///  anchor, true, borderType)`.
        /// </remarks>
        /// <param name="src">
        /// input image; it can have any number of channels, which are processed independently, but
        ///  the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  boxFilter, bilateralFilter, GaussianBlur, medianBlur
        /// </param>
        public static void blur(Mat src, Mat dst, in Vec2d ksize, in Vec2d anchor, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_blur_10(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2, borderType);


        }

        /// <summary>
        ///  Blurs an image using the normalized box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \frac{1}{\texttt{ksize.width*ksize.height}} \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \end{bmatrix}\f]
        ///  
        ///  The call `blur(src, dst, ksize, anchor, borderType)` is equivalent to `boxFilter(src, dst, src.type(), ksize,
        ///  anchor, true, borderType)`.
        /// </remarks>
        /// <param name="src">
        /// input image; it can have any number of channels, which are processed independently, but
        ///  the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  boxFilter, bilateralFilter, GaussianBlur, medianBlur
        /// </param>
        public static void blur(Mat src, Mat dst, in Vec2d ksize, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_blur_11(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2, anchor.Item1, anchor.Item2);


        }

        /// <summary>
        ///  Blurs an image using the normalized box filter.
        /// </summary>
        /// <remarks>
        ///  The function smooths an image using the kernel:
        ///  
        ///  \f[\texttt{K} =  \frac{1}{\texttt{ksize.width*ksize.height}} \begin{bmatrix} 1 & 1 & 1 &  \cdots & 1 & 1  \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \hdotsfor{6} \\ 1 & 1 & 1 &  \cdots & 1 & 1  \\ \end{bmatrix}\f]
        ///  
        ///  The call `blur(src, dst, ksize, anchor, borderType)` is equivalent to `boxFilter(src, dst, src.type(), ksize,
        ///  anchor, true, borderType)`.
        /// </remarks>
        /// <param name="src">
        /// input image; it can have any number of channels, which are processed independently, but
        ///  the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// blurring kernel size.
        /// </param>
        /// <param name="anchor">
        /// anchor point; default value Point(-1,-1) means that the anchor is at the kernel
        ///  center.
        /// </param>
        /// <param name="borderType">
        /// border mode used to extrapolate pixels outside of the image, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  boxFilter, bilateralFilter, GaussianBlur, medianBlur
        /// </param>
        public static void blur(Mat src, Mat dst, in Vec2d ksize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_blur_12(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2);


        }


        //
        // C++:  void cv::stackBlur(Mat src, Mat& dst, Size ksize)
        //

        /// <summary>
        ///  Blurs an image using the stackBlur.
        /// </summary>
        /// <remarks>
        ///  The function applies and stackBlur to an image.
        ///  stackBlur can generate similar results as Gaussian blur, and the time consumption does not increase with the increase of kernel size.
        ///  It creates a kind of moving stack of colors whilst scanning through the image. Thereby it just has to add one new block of color to the right side
        ///  of the stack and remove the leftmost color. The remaining colors on the topmost layer of the stack are either added on or reduced by one,
        ///  depending on if they are on the right or on the left side of the stack. The only supported borderType is BORDER_REPLICATE.
        ///  Original paper was proposed by Mario Klingemann, which can be found https://underdestruction.com/2004/02/25/stackblur-2004.
        /// </remarks>
        /// <param name="src">
        /// input image. The number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S or CV_32F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="ksize">
        /// stack-blurring kernel size. The ksize.width and ksize.height can differ but they both must be
        ///  positive and odd.
        /// </param>
        public static void stackBlur(Mat src, Mat dst, in Vec2d ksize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_stackBlur_10(src.nativeObj, dst.nativeObj, ksize.Item1, ksize.Item2);


        }


        //
        // C++:  void cv::filter2D(Mat src, Mat& dst, int ddepth, Mat kernel, Point anchor = Point(-1,-1), double delta = 0, int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Convolves an image with the kernel.
        /// </summary>
        /// <remarks>
        ///  The function applies an arbitrary linear filter to an image. In-place operation is supported. When
        ///  the aperture is partially outside the image, the function interpolates outlier pixel values
        ///  according to the specified border mode.
        ///  
        ///  The function does actually compute correlation, not the convolution:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \sum _{ \substack{0\leq x' < \texttt{kernel.cols}\\{0\leq y' < \texttt{kernel.rows}}}}  \texttt{kernel} (x',y')* \texttt{src} (x+x'- \texttt{anchor.x} ,y+y'- \texttt{anchor.y} )\f]
        ///  
        ///  That is, the kernel is not mirrored around the anchor point. If you need a real convolution, flip
        ///  the kernel using #flip and set the new anchor to `(kernel.cols - anchor.x - 1, kernel.rows -
        ///  anchor.y - 1)`.
        ///  
        ///  The function uses the DFT-based algorithm in case of sufficiently large kernels (~`11 x 11` or
        ///  larger) and the direct algorithm for small kernels.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and the same number of channels as src.
        /// </param>
        /// <param name="ddepth">
        /// desired depth of the destination image, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernel">
        /// convolution kernel (or rather a correlation kernel), a single-channel floating point
        ///  matrix; if you want to apply different kernels to different channels, split the image into
        ///  separate color planes using split and process them individually.
        /// </param>
        /// <param name="anchor">
        /// anchor of the kernel that indicates the relative position of a filtered point within
        ///  the kernel; the anchor should lie within the kernel; default value (-1,-1) means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// optional value added to the filtered pixels before storing them in dst.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  sepFilter2D, dft, matchTemplate
        /// </param>
        public static void filter2D(Mat src, Mat dst, int ddepth, Mat kernel, in Vec2d anchor, double delta, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_filter2D_10(src.nativeObj, dst.nativeObj, ddepth, kernel.nativeObj, anchor.Item1, anchor.Item2, delta, borderType);


        }

        /// <summary>
        ///  Convolves an image with the kernel.
        /// </summary>
        /// <remarks>
        ///  The function applies an arbitrary linear filter to an image. In-place operation is supported. When
        ///  the aperture is partially outside the image, the function interpolates outlier pixel values
        ///  according to the specified border mode.
        ///  
        ///  The function does actually compute correlation, not the convolution:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \sum _{ \substack{0\leq x' < \texttt{kernel.cols}\\{0\leq y' < \texttt{kernel.rows}}}}  \texttt{kernel} (x',y')* \texttt{src} (x+x'- \texttt{anchor.x} ,y+y'- \texttt{anchor.y} )\f]
        ///  
        ///  That is, the kernel is not mirrored around the anchor point. If you need a real convolution, flip
        ///  the kernel using #flip and set the new anchor to `(kernel.cols - anchor.x - 1, kernel.rows -
        ///  anchor.y - 1)`.
        ///  
        ///  The function uses the DFT-based algorithm in case of sufficiently large kernels (~`11 x 11` or
        ///  larger) and the direct algorithm for small kernels.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and the same number of channels as src.
        /// </param>
        /// <param name="ddepth">
        /// desired depth of the destination image, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernel">
        /// convolution kernel (or rather a correlation kernel), a single-channel floating point
        ///  matrix; if you want to apply different kernels to different channels, split the image into
        ///  separate color planes using split and process them individually.
        /// </param>
        /// <param name="anchor">
        /// anchor of the kernel that indicates the relative position of a filtered point within
        ///  the kernel; the anchor should lie within the kernel; default value (-1,-1) means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// optional value added to the filtered pixels before storing them in dst.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  sepFilter2D, dft, matchTemplate
        /// </param>
        public static void filter2D(Mat src, Mat dst, int ddepth, Mat kernel, in Vec2d anchor, double delta)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_filter2D_11(src.nativeObj, dst.nativeObj, ddepth, kernel.nativeObj, anchor.Item1, anchor.Item2, delta);


        }

        /// <summary>
        ///  Convolves an image with the kernel.
        /// </summary>
        /// <remarks>
        ///  The function applies an arbitrary linear filter to an image. In-place operation is supported. When
        ///  the aperture is partially outside the image, the function interpolates outlier pixel values
        ///  according to the specified border mode.
        ///  
        ///  The function does actually compute correlation, not the convolution:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \sum _{ \substack{0\leq x' < \texttt{kernel.cols}\\{0\leq y' < \texttt{kernel.rows}}}}  \texttt{kernel} (x',y')* \texttt{src} (x+x'- \texttt{anchor.x} ,y+y'- \texttt{anchor.y} )\f]
        ///  
        ///  That is, the kernel is not mirrored around the anchor point. If you need a real convolution, flip
        ///  the kernel using #flip and set the new anchor to `(kernel.cols - anchor.x - 1, kernel.rows -
        ///  anchor.y - 1)`.
        ///  
        ///  The function uses the DFT-based algorithm in case of sufficiently large kernels (~`11 x 11` or
        ///  larger) and the direct algorithm for small kernels.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and the same number of channels as src.
        /// </param>
        /// <param name="ddepth">
        /// desired depth of the destination image, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernel">
        /// convolution kernel (or rather a correlation kernel), a single-channel floating point
        ///  matrix; if you want to apply different kernels to different channels, split the image into
        ///  separate color planes using split and process them individually.
        /// </param>
        /// <param name="anchor">
        /// anchor of the kernel that indicates the relative position of a filtered point within
        ///  the kernel; the anchor should lie within the kernel; default value (-1,-1) means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// optional value added to the filtered pixels before storing them in dst.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  sepFilter2D, dft, matchTemplate
        /// </param>
        public static void filter2D(Mat src, Mat dst, int ddepth, Mat kernel, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_filter2D_12(src.nativeObj, dst.nativeObj, ddepth, kernel.nativeObj, anchor.Item1, anchor.Item2);


        }


        //
        // C++:  void cv::sepFilter2D(Mat src, Mat& dst, int ddepth, Mat kernelX, Mat kernelY, Point anchor = Point(-1,-1), double delta = 0, int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Applies a separable linear filter to an image.
        /// </summary>
        /// <remarks>
        ///  The function applies a separable linear filter to the image. That is, first, every row of src is
        ///  filtered with the 1D kernel kernelX. Then, every column of the result is filtered with the 1D
        ///  kernel kernelY. The final result shifted by delta is stored in dst .
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and the same number of channels as src .
        /// </param>
        /// <param name="ddepth">
        /// Destination image depth, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernelX">
        /// Coefficients for filtering each row.
        /// </param>
        /// <param name="kernelY">
        /// Coefficients for filtering each column.
        /// </param>
        /// <param name="anchor">
        /// Anchor position within the kernel. The default value \f$(-1,-1)\f$ means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// Value added to the filtered results before storing them.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  filter2D, Sobel, GaussianBlur, boxFilter, blur
        /// </param>
        public static void sepFilter2D(Mat src, Mat dst, int ddepth, Mat kernelX, Mat kernelY, in Vec2d anchor, double delta, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernelX != null) kernelX.ThrowIfDisposed();
            if (kernelY != null) kernelY.ThrowIfDisposed();

            imgproc_Imgproc_sepFilter2D_10(src.nativeObj, dst.nativeObj, ddepth, kernelX.nativeObj, kernelY.nativeObj, anchor.Item1, anchor.Item2, delta, borderType);


        }

        /// <summary>
        ///  Applies a separable linear filter to an image.
        /// </summary>
        /// <remarks>
        ///  The function applies a separable linear filter to the image. That is, first, every row of src is
        ///  filtered with the 1D kernel kernelX. Then, every column of the result is filtered with the 1D
        ///  kernel kernelY. The final result shifted by delta is stored in dst .
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and the same number of channels as src .
        /// </param>
        /// <param name="ddepth">
        /// Destination image depth, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernelX">
        /// Coefficients for filtering each row.
        /// </param>
        /// <param name="kernelY">
        /// Coefficients for filtering each column.
        /// </param>
        /// <param name="anchor">
        /// Anchor position within the kernel. The default value \f$(-1,-1)\f$ means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// Value added to the filtered results before storing them.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  filter2D, Sobel, GaussianBlur, boxFilter, blur
        /// </param>
        public static void sepFilter2D(Mat src, Mat dst, int ddepth, Mat kernelX, Mat kernelY, in Vec2d anchor, double delta)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernelX != null) kernelX.ThrowIfDisposed();
            if (kernelY != null) kernelY.ThrowIfDisposed();

            imgproc_Imgproc_sepFilter2D_11(src.nativeObj, dst.nativeObj, ddepth, kernelX.nativeObj, kernelY.nativeObj, anchor.Item1, anchor.Item2, delta);


        }

        /// <summary>
        ///  Applies a separable linear filter to an image.
        /// </summary>
        /// <remarks>
        ///  The function applies a separable linear filter to the image. That is, first, every row of src is
        ///  filtered with the 1D kernel kernelX. Then, every column of the result is filtered with the 1D
        ///  kernel kernelY. The final result shifted by delta is stored in dst .
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and the same number of channels as src .
        /// </param>
        /// <param name="ddepth">
        /// Destination image depth, see @ref filter_depths "combinations"
        /// </param>
        /// <param name="kernelX">
        /// Coefficients for filtering each row.
        /// </param>
        /// <param name="kernelY">
        /// Coefficients for filtering each column.
        /// </param>
        /// <param name="anchor">
        /// Anchor position within the kernel. The default value \f$(-1,-1)\f$ means that the anchor
        ///  is at the kernel center.
        /// </param>
        /// <param name="delta">
        /// Value added to the filtered results before storing them.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        ///  @sa  filter2D, Sobel, GaussianBlur, boxFilter, blur
        /// </param>
        public static void sepFilter2D(Mat src, Mat dst, int ddepth, Mat kernelX, Mat kernelY, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernelX != null) kernelX.ThrowIfDisposed();
            if (kernelY != null) kernelY.ThrowIfDisposed();

            imgproc_Imgproc_sepFilter2D_12(src.nativeObj, dst.nativeObj, ddepth, kernelX.nativeObj, kernelY.nativeObj, anchor.Item1, anchor.Item2);


        }


        //
        // C++:  void cv::cornerSubPix(Mat image, Mat& corners, Size winSize, Size zeroZone, TermCriteria criteria)
        //

        /// <summary>
        ///  Refines the corner locations.
        /// </summary>
        /// <remarks>
        ///  The function iterates to find the sub-pixel accurate location of corners or radial saddle
        ///  points as described in @cite forstner1987fast, and as shown on the figure below.
        ///  
        ///  ![image](pics/cornersubpix.png)
        ///  
        ///  Sub-pixel accurate corner locator is based on the observation that every vector from the center \f$q\f$
        ///  to a point \f$p\f$ located within a neighborhood of \f$q\f$ is orthogonal to the image gradient at \f$p\f$
        ///  subject to image and measurement noise. Consider the expression:
        ///  
        ///  \f[\epsilon _i = {DI_{p_i}}^T  \cdot (q - p_i)\f]
        ///  
        ///  where \f${DI_{p_i}}\f$ is an image gradient at one of the points \f$p_i\f$ in a neighborhood of \f$q\f$ . The
        ///  value of \f$q\f$ is to be found so that \f$\epsilon_i\f$ is minimized. A system of equations may be set up
        ///  with \f$\epsilon_i\f$ set to zero:
        ///  
        ///  \f[\sum _i(DI_{p_i}  \cdot {DI_{p_i}}^T) \cdot q -  \sum _i(DI_{p_i}  \cdot {DI_{p_i}}^T  \cdot p_i)\f]
        ///  
        ///  where the gradients are summed within a neighborhood ("search window") of \f$q\f$ . Calling the first
        ///  gradient term \f$G\f$ and the second gradient term \f$b\f$ gives:
        ///  
        ///  \f[q = G^{-1}  \cdot b\f]
        ///  
        ///  The algorithm sets the center of the neighborhood window at this new center \f$q\f$ and then iterates
        ///  until the center stays within a set threshold.
        /// </remarks>
        /// <param name="image">
        /// Input single-channel, 8-bit or float image.
        /// </param>
        /// <param name="corners">
        /// Initial coordinates of the input corners and refined coordinates provided for
        ///  output.
        /// </param>
        /// <param name="winSize">
        /// Half of the side length of the search window. For example, if winSize=Size(5,5) ,
        ///  then a \f$(5*2+1) \times (5*2+1) = 11 \times 11\f$ search window is used.
        /// </param>
        /// <param name="zeroZone">
        /// Half of the size of the dead region in the middle of the search zone over which
        ///  the summation in the formula below is not done. It is used sometimes to avoid possible
        ///  singularities of the autocorrelation matrix. The value of (-1,-1) indicates that there is no such
        ///  a size.
        /// </param>
        /// <param name="criteria">
        /// Criteria for termination of the iterative process of corner refinement. That is,
        ///  the process of corner position refinement stops either after criteria.maxCount iterations or when
        ///  the corner position moves by less than criteria.epsilon on some iteration.
        /// </param>
        public static void cornerSubPix(Mat image, Mat corners, in Vec2d winSize, in Vec2d zeroZone, in Vec3d criteria)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            imgproc_Imgproc_cornerSubPix_10(image.nativeObj, corners.nativeObj, winSize.Item1, winSize.Item2, zeroZone.Item1, zeroZone.Item2, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  void cv::erode(Mat src, Mat& dst, Mat kernel, Point anchor = Point(-1,-1), int iterations = 1, int borderType = BORDER_CONSTANT, Scalar borderValue = morphologyDefaultBorderValue())
        //

        /// <summary>
        ///  Erodes an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function erodes the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the minimum is taken:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \min _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Erosion can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for erosion; if `element=Mat()`, a `3 x 3` rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times erosion is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  dilate, morphologyEx, getStructuringElement
        /// </param>
        public static void erode(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations, int borderType, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_erode_10(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

        /// <summary>
        ///  Erodes an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function erodes the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the minimum is taken:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \min _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Erosion can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for erosion; if `element=Mat()`, a `3 x 3` rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times erosion is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  dilate, morphologyEx, getStructuringElement
        /// </param>
        public static void erode(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_erode_11(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType);


        }

        /// <summary>
        ///  Erodes an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function erodes the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the minimum is taken:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \min _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Erosion can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for erosion; if `element=Mat()`, a `3 x 3` rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times erosion is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  dilate, morphologyEx, getStructuringElement
        /// </param>
        public static void erode(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_erode_12(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations);


        }

        /// <summary>
        ///  Erodes an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function erodes the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the minimum is taken:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \min _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Erosion can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for erosion; if `element=Mat()`, a `3 x 3` rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times erosion is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  dilate, morphologyEx, getStructuringElement
        /// </param>
        public static void erode(Mat src, Mat dst, Mat kernel, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_erode_13(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2);


        }


        //
        // C++:  void cv::dilate(Mat src, Mat& dst, Mat kernel, Point anchor = Point(-1,-1), int iterations = 1, int borderType = BORDER_CONSTANT, Scalar borderValue = morphologyDefaultBorderValue())
        //

        /// <summary>
        ///  Dilates an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function dilates the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the maximum is taken:
        ///  \f[\texttt{dst} (x,y) =  \max _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Dilation can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for dilation; if element=Mat(), a 3 x 3 rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times dilation is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not suported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  erode, morphologyEx, getStructuringElement
        /// </param>
        public static void dilate(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations, int borderType, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_dilate_10(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

        /// <summary>
        ///  Dilates an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function dilates the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the maximum is taken:
        ///  \f[\texttt{dst} (x,y) =  \max _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Dilation can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for dilation; if element=Mat(), a 3 x 3 rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times dilation is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not suported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  erode, morphologyEx, getStructuringElement
        /// </param>
        public static void dilate(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_dilate_11(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType);


        }

        /// <summary>
        ///  Dilates an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function dilates the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the maximum is taken:
        ///  \f[\texttt{dst} (x,y) =  \max _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Dilation can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for dilation; if element=Mat(), a 3 x 3 rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times dilation is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not suported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  erode, morphologyEx, getStructuringElement
        /// </param>
        public static void dilate(Mat src, Mat dst, Mat kernel, in Vec2d anchor, int iterations)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_dilate_12(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations);


        }

        /// <summary>
        ///  Dilates an image by using a specific structuring element.
        /// </summary>
        /// <remarks>
        ///  The function dilates the source image using the specified structuring element that determines the
        ///  shape of a pixel neighborhood over which the maximum is taken:
        ///  \f[\texttt{dst} (x,y) =  \max _{(x',y'):  \, \texttt{element} (x',y') \ne0 } \texttt{src} (x+x',y+y')\f]
        ///  
        ///  The function supports the in-place mode. Dilation can be applied several ( iterations ) times. In
        ///  case of multi-channel images, each channel is processed independently.
        /// </remarks>
        /// <param name="src">
        /// input image; the number of channels can be arbitrary, but the depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// output image of the same size and type as src.
        /// </param>
        /// <param name="kernel">
        /// structuring element used for dilation; if element=Mat(), a 3 x 3 rectangular
        ///  structuring element is used. Kernel can be created using #getStructuringElement
        /// </param>
        /// <param name="anchor">
        /// position of the anchor within the element; default value (-1, -1) means that the
        ///  anchor is at the element center.
        /// </param>
        /// <param name="iterations">
        /// number of times dilation is applied.
        /// </param>
        /// <param name="borderType">
        /// pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not suported.
        /// </param>
        /// <param name="borderValue">
        /// border value in case of a constant border
        ///  @sa  erode, morphologyEx, getStructuringElement
        /// </param>
        public static void dilate(Mat src, Mat dst, Mat kernel, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_dilate_13(src.nativeObj, dst.nativeObj, kernel.nativeObj, anchor.Item1, anchor.Item2);


        }


        //
        // C++:  void cv::morphologyEx(Mat src, Mat& dst, int op, Mat kernel, Point anchor = Point(-1,-1), int iterations = 1, int borderType = BORDER_CONSTANT, Scalar borderValue = morphologyDefaultBorderValue())
        //

        /// <summary>
        ///  Performs advanced morphological transformations.
        /// </summary>
        /// <remarks>
        ///  The function cv::morphologyEx can perform advanced morphological transformations using an erosion and dilation as
        ///  basic operations.
        ///  
        ///  Any of the operations can be done in-place. In case of multi-channel images, each channel is
        ///  processed independently.
        /// </remarks>
        /// <param name="src">
        /// Source image. The number of channels can be arbitrary. The depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and type as source image.
        /// </param>
        /// <param name="op">
        /// Type of a morphological operation, see #MorphTypes
        /// </param>
        /// <param name="kernel">
        /// Structuring element. It can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// Anchor position with the kernel. Negative values mean that the anchor is at the
        ///  kernel center.
        /// </param>
        /// <param name="iterations">
        /// Number of times erosion and dilation are applied.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// Border value in case of a constant border. The default value has a special
        ///  meaning.
        ///  @sa  dilate, erode, getStructuringElement
        ///  @note The number of iterations is the number of times erosion or dilatation operation will be applied.
        ///  For instance, an opening operation (#MORPH_OPEN) with two iterations is equivalent to apply
        ///  successively: erode -&gt; erode -&gt; dilate -&gt; dilate (and not erode -&gt; dilate -&gt; erode -&gt; dilate).
        /// </param>
        public static void morphologyEx(Mat src, Mat dst, int op, Mat kernel, in Vec2d anchor, int iterations, int borderType, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_morphologyEx_10(src.nativeObj, dst.nativeObj, op, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

        /// <summary>
        ///  Performs advanced morphological transformations.
        /// </summary>
        /// <remarks>
        ///  The function cv::morphologyEx can perform advanced morphological transformations using an erosion and dilation as
        ///  basic operations.
        ///  
        ///  Any of the operations can be done in-place. In case of multi-channel images, each channel is
        ///  processed independently.
        /// </remarks>
        /// <param name="src">
        /// Source image. The number of channels can be arbitrary. The depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and type as source image.
        /// </param>
        /// <param name="op">
        /// Type of a morphological operation, see #MorphTypes
        /// </param>
        /// <param name="kernel">
        /// Structuring element. It can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// Anchor position with the kernel. Negative values mean that the anchor is at the
        ///  kernel center.
        /// </param>
        /// <param name="iterations">
        /// Number of times erosion and dilation are applied.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// Border value in case of a constant border. The default value has a special
        ///  meaning.
        ///  @sa  dilate, erode, getStructuringElement
        ///  @note The number of iterations is the number of times erosion or dilatation operation will be applied.
        ///  For instance, an opening operation (#MORPH_OPEN) with two iterations is equivalent to apply
        ///  successively: erode -&gt; erode -&gt; dilate -&gt; dilate (and not erode -&gt; dilate -&gt; erode -&gt; dilate).
        /// </param>
        public static void morphologyEx(Mat src, Mat dst, int op, Mat kernel, in Vec2d anchor, int iterations, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_morphologyEx_11(src.nativeObj, dst.nativeObj, op, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations, borderType);


        }

        /// <summary>
        ///  Performs advanced morphological transformations.
        /// </summary>
        /// <remarks>
        ///  The function cv::morphologyEx can perform advanced morphological transformations using an erosion and dilation as
        ///  basic operations.
        ///  
        ///  Any of the operations can be done in-place. In case of multi-channel images, each channel is
        ///  processed independently.
        /// </remarks>
        /// <param name="src">
        /// Source image. The number of channels can be arbitrary. The depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and type as source image.
        /// </param>
        /// <param name="op">
        /// Type of a morphological operation, see #MorphTypes
        /// </param>
        /// <param name="kernel">
        /// Structuring element. It can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// Anchor position with the kernel. Negative values mean that the anchor is at the
        ///  kernel center.
        /// </param>
        /// <param name="iterations">
        /// Number of times erosion and dilation are applied.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// Border value in case of a constant border. The default value has a special
        ///  meaning.
        ///  @sa  dilate, erode, getStructuringElement
        ///  @note The number of iterations is the number of times erosion or dilatation operation will be applied.
        ///  For instance, an opening operation (#MORPH_OPEN) with two iterations is equivalent to apply
        ///  successively: erode -&gt; erode -&gt; dilate -&gt; dilate (and not erode -&gt; dilate -&gt; erode -&gt; dilate).
        /// </param>
        public static void morphologyEx(Mat src, Mat dst, int op, Mat kernel, in Vec2d anchor, int iterations)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_morphologyEx_12(src.nativeObj, dst.nativeObj, op, kernel.nativeObj, anchor.Item1, anchor.Item2, iterations);


        }

        /// <summary>
        ///  Performs advanced morphological transformations.
        /// </summary>
        /// <remarks>
        ///  The function cv::morphologyEx can perform advanced morphological transformations using an erosion and dilation as
        ///  basic operations.
        ///  
        ///  Any of the operations can be done in-place. In case of multi-channel images, each channel is
        ///  processed independently.
        /// </remarks>
        /// <param name="src">
        /// Source image. The number of channels can be arbitrary. The depth should be one of
        ///  CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same size and type as source image.
        /// </param>
        /// <param name="op">
        /// Type of a morphological operation, see #MorphTypes
        /// </param>
        /// <param name="kernel">
        /// Structuring element. It can be created using #getStructuringElement.
        /// </param>
        /// <param name="anchor">
        /// Anchor position with the kernel. Negative values mean that the anchor is at the
        ///  kernel center.
        /// </param>
        /// <param name="iterations">
        /// Number of times erosion and dilation are applied.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes. #BORDER_WRAP is not supported.
        /// </param>
        /// <param name="borderValue">
        /// Border value in case of a constant border. The default value has a special
        ///  meaning.
        ///  @sa  dilate, erode, getStructuringElement
        ///  @note The number of iterations is the number of times erosion or dilatation operation will be applied.
        ///  For instance, an opening operation (#MORPH_OPEN) with two iterations is equivalent to apply
        ///  successively: erode -&gt; erode -&gt; dilate -&gt; dilate (and not erode -&gt; dilate -&gt; erode -&gt; dilate).
        /// </param>
        public static void morphologyEx(Mat src, Mat dst, int op, Mat kernel, in Vec2d anchor)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (kernel != null) kernel.ThrowIfDisposed();

            imgproc_Imgproc_morphologyEx_13(src.nativeObj, dst.nativeObj, op, kernel.nativeObj, anchor.Item1, anchor.Item2);


        }


        //
        // C++:  void cv::resize(Mat src, Mat& dst, Size dsize, double fx = 0, double fy = 0, int interpolation = INTER_LINEAR)
        //

        /// <summary>
        ///  Resizes an image.
        /// </summary>
        /// <remarks>
        ///  The function resize resizes the image src down to or up to the specified size. Note that the
        ///  initial dst type or size are not taken into account. Instead, the size and type are derived from
        ///  the `src`,`dsize`,`fx`, and `fy`. If you want to resize src so that it fits the pre-created dst,
        ///  you may call the function as follows:
        /// </remarks>
        /// <code language="c++">
        ///      // explicitly specify dsize=dst.size(); fx and fy will be computed from that.
        ///      resize(src, dst, dst.size(), 0, 0, interpolation);
        /// </code>
        /// <remarks>
        ///  If you want to decimate the image by factor of 2 in each direction, you can call the function this
        ///  way:
        /// </remarks>
        /// <code language="c++">
        ///      // specify fx and fy and let the function compute the destination image size.
        ///      resize(src, dst, Size(), 0.5, 0.5, interpolation);
        /// </code>
        /// <remarks>
        ///  To shrink an image, it will generally look best with #INTER_AREA interpolation, whereas to
        ///  enlarge an image, it will generally look best with #INTER_CUBIC (slow) or #INTER_LINEAR
        ///  (faster but still looks OK).
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the size dsize (when it is non-zero) or the size computed from
        ///  src.size(), fx, and fy; the type of dst is the same as of src.
        /// </param>
        /// <param name="dsize">
        /// output image size; if it equals zero (`None` in Python), it is computed as:
        ///   \f[\texttt{dsize = Size(round(fx*src.cols), round(fy*src.rows))}\f]
        ///   Either dsize or both fx and fy must be non-zero.
        /// </param>
        /// <param name="fx">
        /// scale factor along the horizontal axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.width/src.cols}\f]
        /// </param>
        /// <param name="fy">
        /// scale factor along the vertical axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.height/src.rows}\f]
        /// </param>
        /// <param name="interpolation">
        /// interpolation method, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective, remap
        /// </remarks>
        public static void resize(Mat src, Mat dst, in Vec2d dsize, double fx, double fy, int interpolation)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_resize_10(src.nativeObj, dst.nativeObj, dsize.Item1, dsize.Item2, fx, fy, interpolation);


        }

        /// <summary>
        ///  Resizes an image.
        /// </summary>
        /// <remarks>
        ///  The function resize resizes the image src down to or up to the specified size. Note that the
        ///  initial dst type or size are not taken into account. Instead, the size and type are derived from
        ///  the `src`,`dsize`,`fx`, and `fy`. If you want to resize src so that it fits the pre-created dst,
        ///  you may call the function as follows:
        /// </remarks>
        /// <code language="c++">
        ///      // explicitly specify dsize=dst.size(); fx and fy will be computed from that.
        ///      resize(src, dst, dst.size(), 0, 0, interpolation);
        /// </code>
        /// <remarks>
        ///  If you want to decimate the image by factor of 2 in each direction, you can call the function this
        ///  way:
        /// </remarks>
        /// <code language="c++">
        ///      // specify fx and fy and let the function compute the destination image size.
        ///      resize(src, dst, Size(), 0.5, 0.5, interpolation);
        /// </code>
        /// <remarks>
        ///  To shrink an image, it will generally look best with #INTER_AREA interpolation, whereas to
        ///  enlarge an image, it will generally look best with #INTER_CUBIC (slow) or #INTER_LINEAR
        ///  (faster but still looks OK).
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the size dsize (when it is non-zero) or the size computed from
        ///  src.size(), fx, and fy; the type of dst is the same as of src.
        /// </param>
        /// <param name="dsize">
        /// output image size; if it equals zero (`None` in Python), it is computed as:
        ///   \f[\texttt{dsize = Size(round(fx*src.cols), round(fy*src.rows))}\f]
        ///   Either dsize or both fx and fy must be non-zero.
        /// </param>
        /// <param name="fx">
        /// scale factor along the horizontal axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.width/src.cols}\f]
        /// </param>
        /// <param name="fy">
        /// scale factor along the vertical axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.height/src.rows}\f]
        /// </param>
        /// <param name="interpolation">
        /// interpolation method, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective, remap
        /// </remarks>
        public static void resize(Mat src, Mat dst, in Vec2d dsize, double fx, double fy)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_resize_11(src.nativeObj, dst.nativeObj, dsize.Item1, dsize.Item2, fx, fy);


        }

        /// <summary>
        ///  Resizes an image.
        /// </summary>
        /// <remarks>
        ///  The function resize resizes the image src down to or up to the specified size. Note that the
        ///  initial dst type or size are not taken into account. Instead, the size and type are derived from
        ///  the `src`,`dsize`,`fx`, and `fy`. If you want to resize src so that it fits the pre-created dst,
        ///  you may call the function as follows:
        /// </remarks>
        /// <code language="c++">
        ///      // explicitly specify dsize=dst.size(); fx and fy will be computed from that.
        ///      resize(src, dst, dst.size(), 0, 0, interpolation);
        /// </code>
        /// <remarks>
        ///  If you want to decimate the image by factor of 2 in each direction, you can call the function this
        ///  way:
        /// </remarks>
        /// <code language="c++">
        ///      // specify fx and fy and let the function compute the destination image size.
        ///      resize(src, dst, Size(), 0.5, 0.5, interpolation);
        /// </code>
        /// <remarks>
        ///  To shrink an image, it will generally look best with #INTER_AREA interpolation, whereas to
        ///  enlarge an image, it will generally look best with #INTER_CUBIC (slow) or #INTER_LINEAR
        ///  (faster but still looks OK).
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the size dsize (when it is non-zero) or the size computed from
        ///  src.size(), fx, and fy; the type of dst is the same as of src.
        /// </param>
        /// <param name="dsize">
        /// output image size; if it equals zero (`None` in Python), it is computed as:
        ///   \f[\texttt{dsize = Size(round(fx*src.cols), round(fy*src.rows))}\f]
        ///   Either dsize or both fx and fy must be non-zero.
        /// </param>
        /// <param name="fx">
        /// scale factor along the horizontal axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.width/src.cols}\f]
        /// </param>
        /// <param name="fy">
        /// scale factor along the vertical axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.height/src.rows}\f]
        /// </param>
        /// <param name="interpolation">
        /// interpolation method, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective, remap
        /// </remarks>
        public static void resize(Mat src, Mat dst, in Vec2d dsize, double fx)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_resize_12(src.nativeObj, dst.nativeObj, dsize.Item1, dsize.Item2, fx);


        }

        /// <summary>
        ///  Resizes an image.
        /// </summary>
        /// <remarks>
        ///  The function resize resizes the image src down to or up to the specified size. Note that the
        ///  initial dst type or size are not taken into account. Instead, the size and type are derived from
        ///  the `src`,`dsize`,`fx`, and `fy`. If you want to resize src so that it fits the pre-created dst,
        ///  you may call the function as follows:
        /// </remarks>
        /// <code language="c++">
        ///      // explicitly specify dsize=dst.size(); fx and fy will be computed from that.
        ///      resize(src, dst, dst.size(), 0, 0, interpolation);
        /// </code>
        /// <remarks>
        ///  If you want to decimate the image by factor of 2 in each direction, you can call the function this
        ///  way:
        /// </remarks>
        /// <code language="c++">
        ///      // specify fx and fy and let the function compute the destination image size.
        ///      resize(src, dst, Size(), 0.5, 0.5, interpolation);
        /// </code>
        /// <remarks>
        ///  To shrink an image, it will generally look best with #INTER_AREA interpolation, whereas to
        ///  enlarge an image, it will generally look best with #INTER_CUBIC (slow) or #INTER_LINEAR
        ///  (faster but still looks OK).
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the size dsize (when it is non-zero) or the size computed from
        ///  src.size(), fx, and fy; the type of dst is the same as of src.
        /// </param>
        /// <param name="dsize">
        /// output image size; if it equals zero (`None` in Python), it is computed as:
        ///   \f[\texttt{dsize = Size(round(fx*src.cols), round(fy*src.rows))}\f]
        ///   Either dsize or both fx and fy must be non-zero.
        /// </param>
        /// <param name="fx">
        /// scale factor along the horizontal axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.width/src.cols}\f]
        /// </param>
        /// <param name="fy">
        /// scale factor along the vertical axis; when it equals 0, it is computed as
        ///  \f[\texttt{(double)dsize.height/src.rows}\f]
        /// </param>
        /// <param name="interpolation">
        /// interpolation method, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective, remap
        /// </remarks>
        public static void resize(Mat src, Mat dst, in Vec2d dsize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_resize_13(src.nativeObj, dst.nativeObj, dsize.Item1, dsize.Item2);


        }


        //
        // C++:  void cv::warpAffine(Mat src, Mat& dst, Mat M, Size dsize, int flags = INTER_LINEAR, int borderMode = BORDER_CONSTANT, Scalar borderValue = Scalar())
        //

        /// <summary>
        ///  Applies an affine transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpAffine transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} ( \texttt{M} _{11} x +  \texttt{M} _{12} y +  \texttt{M} _{13}, \texttt{M} _{21} x +  \texttt{M} _{22} y +  \texttt{M} _{23})\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted
        ///  with #invertAffineTransform and then put in the formula above instead of M. The function cannot
        ///  operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$2\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (see #InterpolationFlags) and the optional
        ///  flag #WARP_INVERSE_MAP that means that M is the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (see #BorderTypes); when
        ///  borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image corresponding to
        ///  the "outliers" in the source image are not modified by the function.
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it is 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpPerspective, resize, remap, getRectSubPix, transform
        /// </remarks>
        public static void warpAffine(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags, int borderMode, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpAffine_10(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags, borderMode, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

        /// <summary>
        ///  Applies an affine transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpAffine transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} ( \texttt{M} _{11} x +  \texttt{M} _{12} y +  \texttt{M} _{13}, \texttt{M} _{21} x +  \texttt{M} _{22} y +  \texttt{M} _{23})\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted
        ///  with #invertAffineTransform and then put in the formula above instead of M. The function cannot
        ///  operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$2\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (see #InterpolationFlags) and the optional
        ///  flag #WARP_INVERSE_MAP that means that M is the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (see #BorderTypes); when
        ///  borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image corresponding to
        ///  the "outliers" in the source image are not modified by the function.
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it is 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpPerspective, resize, remap, getRectSubPix, transform
        /// </remarks>
        public static void warpAffine(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags, int borderMode)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpAffine_11(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags, borderMode);


        }

        /// <summary>
        ///  Applies an affine transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpAffine transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} ( \texttt{M} _{11} x +  \texttt{M} _{12} y +  \texttt{M} _{13}, \texttt{M} _{21} x +  \texttt{M} _{22} y +  \texttt{M} _{23})\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted
        ///  with #invertAffineTransform and then put in the formula above instead of M. The function cannot
        ///  operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$2\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (see #InterpolationFlags) and the optional
        ///  flag #WARP_INVERSE_MAP that means that M is the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (see #BorderTypes); when
        ///  borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image corresponding to
        ///  the "outliers" in the source image are not modified by the function.
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it is 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpPerspective, resize, remap, getRectSubPix, transform
        /// </remarks>
        public static void warpAffine(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpAffine_12(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags);


        }

        /// <summary>
        ///  Applies an affine transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpAffine transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} ( \texttt{M} _{11} x +  \texttt{M} _{12} y +  \texttt{M} _{13}, \texttt{M} _{21} x +  \texttt{M} _{22} y +  \texttt{M} _{23})\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted
        ///  with #invertAffineTransform and then put in the formula above instead of M. The function cannot
        ///  operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$2\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (see #InterpolationFlags) and the optional
        ///  flag #WARP_INVERSE_MAP that means that M is the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (see #BorderTypes); when
        ///  borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image corresponding to
        ///  the "outliers" in the source image are not modified by the function.
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it is 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpPerspective, resize, remap, getRectSubPix, transform
        /// </remarks>
        public static void warpAffine(Mat src, Mat dst, Mat M, in Vec2d dsize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpAffine_13(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2);


        }


        //
        // C++:  void cv::warpPerspective(Mat src, Mat& dst, Mat M, Size dsize, int flags = INTER_LINEAR, int borderMode = BORDER_CONSTANT, Scalar borderValue = Scalar())
        //

        /// <summary>
        ///  Applies a perspective transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpPerspective transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} \left ( \frac{M_{11} x + M_{12} y + M_{13}}{M_{31} x + M_{32} y + M_{33}} ,
        ///       \frac{M_{21} x + M_{22} y + M_{23}}{M_{31} x + M_{32} y + M_{33}} \right )\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted with invert
        ///  and then put in the formula above instead of M. The function cannot operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$3\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (#INTER_LINEAR or #INTER_NEAREST) and the
        ///  optional flag #WARP_INVERSE_MAP, that sets M as the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (#BORDER_CONSTANT or #BORDER_REPLICATE).
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it equals 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, resize, remap, getRectSubPix, perspectiveTransform
        /// </remarks>
        public static void warpPerspective(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags, int borderMode, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpPerspective_10(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags, borderMode, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

        /// <summary>
        ///  Applies a perspective transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpPerspective transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} \left ( \frac{M_{11} x + M_{12} y + M_{13}}{M_{31} x + M_{32} y + M_{33}} ,
        ///       \frac{M_{21} x + M_{22} y + M_{23}}{M_{31} x + M_{32} y + M_{33}} \right )\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted with invert
        ///  and then put in the formula above instead of M. The function cannot operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$3\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (#INTER_LINEAR or #INTER_NEAREST) and the
        ///  optional flag #WARP_INVERSE_MAP, that sets M as the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (#BORDER_CONSTANT or #BORDER_REPLICATE).
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it equals 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, resize, remap, getRectSubPix, perspectiveTransform
        /// </remarks>
        public static void warpPerspective(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags, int borderMode)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpPerspective_11(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags, borderMode);


        }

        /// <summary>
        ///  Applies a perspective transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpPerspective transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} \left ( \frac{M_{11} x + M_{12} y + M_{13}}{M_{31} x + M_{32} y + M_{33}} ,
        ///       \frac{M_{21} x + M_{22} y + M_{23}}{M_{31} x + M_{32} y + M_{33}} \right )\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted with invert
        ///  and then put in the formula above instead of M. The function cannot operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$3\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (#INTER_LINEAR or #INTER_NEAREST) and the
        ///  optional flag #WARP_INVERSE_MAP, that sets M as the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (#BORDER_CONSTANT or #BORDER_REPLICATE).
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it equals 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, resize, remap, getRectSubPix, perspectiveTransform
        /// </remarks>
        public static void warpPerspective(Mat src, Mat dst, Mat M, in Vec2d dsize, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpPerspective_12(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2, flags);


        }

        /// <summary>
        ///  Applies a perspective transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function warpPerspective transforms the source image using the specified matrix:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} \left ( \frac{M_{11} x + M_{12} y + M_{13}}{M_{31} x + M_{32} y + M_{33}} ,
        ///       \frac{M_{21} x + M_{22} y + M_{23}}{M_{31} x + M_{32} y + M_{33}} \right )\f]
        ///  
        ///  when the flag #WARP_INVERSE_MAP is set. Otherwise, the transformation is first inverted with invert
        ///  and then put in the formula above instead of M. The function cannot operate in-place.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image that has the size dsize and the same type as src .
        /// </param>
        /// <param name="M">
        /// \f$3\times 3\f$ transformation matrix.
        /// </param>
        /// <param name="dsize">
        /// size of the output image.
        /// </param>
        /// <param name="flags">
        /// combination of interpolation methods (#INTER_LINEAR or #INTER_NEAREST) and the
        ///  optional flag #WARP_INVERSE_MAP, that sets M as the inverse transformation (
        ///  \f$\texttt{dst}\rightarrow\texttt{src}\f$ ).
        /// </param>
        /// <param name="borderMode">
        /// pixel extrapolation method (#BORDER_CONSTANT or #BORDER_REPLICATE).
        /// </param>
        /// <param name="borderValue">
        /// value used in case of a constant border; by default, it equals 0.
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, resize, remap, getRectSubPix, perspectiveTransform
        /// </remarks>
        public static void warpPerspective(Mat src, Mat dst, Mat M, in Vec2d dsize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (M != null) M.ThrowIfDisposed();

            imgproc_Imgproc_warpPerspective_13(src.nativeObj, dst.nativeObj, M.nativeObj, dsize.Item1, dsize.Item2);


        }


        //
        // C++:  void cv::remap(Mat src, Mat& dst, Mat map1, Mat map2, int interpolation, int borderMode = BORDER_CONSTANT, Scalar borderValue = Scalar())
        //

        /// <summary>
        ///  Applies a generic geometrical transformation to an image.
        /// </summary>
        /// <remarks>
        ///  The function remap transforms the source image using the specified map:
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} (map_x(x,y),map_y(x,y))\f]
        ///  
        ///  with the WARP_RELATIVE_MAP flag :
        ///  
        ///  \f[\texttt{dst} (x,y) =  \texttt{src} (x+map_x(x,y),y+map_y(x,y))\f]
        ///  
        ///  where values of pixels with non-integer coordinates are computed using one of available
        ///  interpolation methods. \f$map_x\f$ and \f$map_y\f$ can be encoded as separate floating-point maps
        ///  in \f$map_1\f$ and \f$map_2\f$ respectively, or interleaved floating-point maps of \f$(x,y)\f$ in
        ///  \f$map_1\f$, or fixed-point maps created by using #convertMaps. The reason you might want to
        ///  convert from floating to fixed-point representations of a map is that they can yield much faster
        ///  (\~2x) remapping operations. In the converted case, \f$map_1\f$ contains pairs (cvFloor(x),
        ///  cvFloor(y)) and \f$map_2\f$ contains indices in a table of interpolation coefficients.
        ///  
        ///  This function cannot operate in-place.
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image. It has the same size as map1 and the same type as src .
        /// </param>
        /// <param name="map1">
        /// The first map of either (x,y) points or just x values having the type CV_16SC2 ,
        ///  CV_32FC1, or CV_32FC2. See #convertMaps for details on converting a floating point
        ///  representation to fixed-point for speed.
        /// </param>
        /// <param name="map2">
        /// The second map of y values having the type CV_16UC1, CV_32FC1, or none (empty map
        ///  if map1 is (x,y) points), respectively.
        /// </param>
        /// <param name="interpolation">
        /// Interpolation method (see #InterpolationFlags). The methods #INTER_AREA
        ///  #INTER_LINEAR_EXACT and #INTER_NEAREST_EXACT are not supported by this function.
        ///  The extra flag WARP_RELATIVE_MAP can be ORed to the interpolation method
        ///  (e.g. INTER_LINEAR | WARP_RELATIVE_MAP)
        /// </param>
        /// <param name="borderMode">
        /// Pixel extrapolation method (see #BorderTypes). When
        ///  borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image that
        ///  corresponds to the "outliers" in the source image are not modified by the function.
        /// </param>
        /// <param name="borderValue">
        /// Value used in case of a constant border. By default, it is 0.
        ///  @note
        ///  Due to current implementation limitations the size of an input and output images should be less than 32767x32767.
        /// </param>
        public static void remap(Mat src, Mat dst, Mat map1, Mat map2, int interpolation, int borderMode, in Vec4d borderValue)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (map1 != null) map1.ThrowIfDisposed();
            if (map2 != null) map2.ThrowIfDisposed();

            imgproc_Imgproc_remap_10(src.nativeObj, dst.nativeObj, map1.nativeObj, map2.nativeObj, interpolation, borderMode, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }


        //
        // C++:  Mat cv::getRotationMatrix2D(Point2f center, double angle, double scale)
        //

        /// <summary>
        ///  Calculates an affine matrix of 2D rotation.
        /// </summary>
        /// <remarks>
        ///  The function calculates the following matrix:
        ///  
        ///  \f[\begin{bmatrix} \alpha &  \beta & (1- \alpha )  \cdot \texttt{center.x} -  \beta \cdot \texttt{center.y} \\ - \beta &  \alpha &  \beta \cdot \texttt{center.x} + (1- \alpha )  \cdot \texttt{center.y} \end{bmatrix}\f]
        ///  
        ///  where
        ///  
        ///  \f[\begin{array}{l} \alpha =  \texttt{scale} \cdot \cos \texttt{angle} , \\ \beta =  \texttt{scale} \cdot \sin \texttt{angle} \end{array}\f]
        ///  
        ///  The transformation maps the rotation center to itself. If this is not the target, adjust the shift.
        /// </remarks>
        /// <param name="center">
        /// Center of the rotation in the source image.
        /// </param>
        /// <param name="angle">
        /// Rotation angle in degrees. Positive values mean counter-clockwise rotation (the
        ///  coordinate origin is assumed to be the top-left corner).
        /// </param>
        /// <param name="scale">
        /// Isotropic scale factor.
        /// </param>
        /// <remarks>
        ///  @sa  getAffineTransform, warpAffine, transform
        /// </remarks>
        public static Mat getRotationMatrix2D(in Vec2d center, double angle, double scale)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_getRotationMatrix2D_10(center.Item1, center.Item2, angle, scale)));


        }


        //
        // C++:  void cv::getRectSubPix(Mat image, Size patchSize, Point2f center, Mat& patch, int patchType = -1)
        //

        /// <summary>
        ///  Retrieves a pixel rectangle from an image with sub-pixel accuracy.
        /// </summary>
        /// <remarks>
        ///  The function getRectSubPix extracts pixels from src:
        ///  
        ///  \f[patch(x, y) = src(x +  \texttt{center.x} - ( \texttt{dst.cols} -1)*0.5, y +  \texttt{center.y} - ( \texttt{dst.rows} -1)*0.5)\f]
        ///  
        ///  where the values of the pixels at non-integer coordinates are retrieved using bilinear
        ///  interpolation. Every channel of multi-channel images is processed independently. Also
        ///  the image should be a single channel or three channel image. While the center of the
        ///  rectangle must be inside the image, parts of the rectangle may be outside.
        /// </remarks>
        /// <param name="image">
        /// Source image.
        /// </param>
        /// <param name="patchSize">
        /// Size of the extracted patch.
        /// </param>
        /// <param name="center">
        /// Floating point coordinates of the center of the extracted rectangle within the
        ///  source image. The center must be inside the image.
        /// </param>
        /// <param name="patch">
        /// Extracted patch that has the size patchSize and the same number of channels as src .
        /// </param>
        /// <param name="patchType">
        /// Depth of the extracted pixels. By default, they have the same depth as src .
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective
        /// </remarks>
        public static void getRectSubPix(Mat image, in Vec2d patchSize, in Vec2d center, Mat patch, int patchType)
        {
            if (image != null) image.ThrowIfDisposed();
            if (patch != null) patch.ThrowIfDisposed();

            imgproc_Imgproc_getRectSubPix_10(image.nativeObj, patchSize.Item1, patchSize.Item2, center.Item1, center.Item2, patch.nativeObj, patchType);


        }

        /// <summary>
        ///  Retrieves a pixel rectangle from an image with sub-pixel accuracy.
        /// </summary>
        /// <remarks>
        ///  The function getRectSubPix extracts pixels from src:
        ///  
        ///  \f[patch(x, y) = src(x +  \texttt{center.x} - ( \texttt{dst.cols} -1)*0.5, y +  \texttt{center.y} - ( \texttt{dst.rows} -1)*0.5)\f]
        ///  
        ///  where the values of the pixels at non-integer coordinates are retrieved using bilinear
        ///  interpolation. Every channel of multi-channel images is processed independently. Also
        ///  the image should be a single channel or three channel image. While the center of the
        ///  rectangle must be inside the image, parts of the rectangle may be outside.
        /// </remarks>
        /// <param name="image">
        /// Source image.
        /// </param>
        /// <param name="patchSize">
        /// Size of the extracted patch.
        /// </param>
        /// <param name="center">
        /// Floating point coordinates of the center of the extracted rectangle within the
        ///  source image. The center must be inside the image.
        /// </param>
        /// <param name="patch">
        /// Extracted patch that has the size patchSize and the same number of channels as src .
        /// </param>
        /// <param name="patchType">
        /// Depth of the extracted pixels. By default, they have the same depth as src .
        /// </param>
        /// <remarks>
        ///  @sa  warpAffine, warpPerspective
        /// </remarks>
        public static void getRectSubPix(Mat image, in Vec2d patchSize, in Vec2d center, Mat patch)
        {
            if (image != null) image.ThrowIfDisposed();
            if (patch != null) patch.ThrowIfDisposed();

            imgproc_Imgproc_getRectSubPix_11(image.nativeObj, patchSize.Item1, patchSize.Item2, center.Item1, center.Item2, patch.nativeObj);


        }


        //
        // C++:  void cv::logPolar(Mat src, Mat& dst, Point2f center, double M, int flags)
        //

        /// <summary>
        ///  Remaps an image to semilog-polar coordinates space.
        /// </summary>
        /// <remarks>
        ///  @deprecated This function produces same result as cv::warpPolar(src, dst, src.size(), center, maxRadius, flags+WARP_POLAR_LOG);
        ///  
        ///  @internal
        ///  Transform the source image using the following transformation (See @ref polar_remaps_reference_image "Polar remaps reference image d)"):
        ///  \f[\begin{array}{l}
        ///    dst( \rho , \phi ) = src(x,y) \\
        ///    dst.size() \leftarrow src.size()
        ///  \end{array}\f]
        ///  
        ///  where
        ///  \f[\begin{array}{l}
        ///    I = (dx,dy) = (x - center.x,y - center.y) \\
        ///    \rho = M \cdot log_e(\texttt{magnitude} (I)) ,\\
        ///    \phi = Kangle \cdot \texttt{angle} (I) \\
        ///  \end{array}\f]
        ///  
        ///  and
        ///  \f[\begin{array}{l}
        ///    M = src.cols / log_e(maxRadius) \\
        ///    Kangle = src.rows / 2\Pi \\
        ///  \end{array}\f]
        ///  
        ///  The function emulates the human "foveal" vision and can be used for fast scale and
        ///  rotation-invariant template matching, for object tracking and so forth.
        /// </remarks>
        /// <param name="src">
        /// Source image
        /// </param>
        /// <param name="dst">
        /// Destination image. It will have same size and type as src.
        /// </param>
        /// <param name="center">
        /// The transformation center; where the output precision is maximal
        /// </param>
        /// <param name="M">
        /// Magnitude scale parameter. It determines the radius of the bounding circle to transform too.
        /// </param>
        /// <param name="flags">
        /// A combination of interpolation methods, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @note
        ///  -   The function can not operate in-place.
        ///  -   To calculate magnitude and angle in degrees #cartToPolar is used internally thus angles are measured from 0 to 360 with accuracy about 0.3 degrees.
        ///  
        ///  @sa cv::linearPolar
        ///  @endinternal
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void logPolar(Mat src, Mat dst, in Vec2d center, double M, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_logPolar_10(src.nativeObj, dst.nativeObj, center.Item1, center.Item2, M, flags);


        }


        //
        // C++:  void cv::linearPolar(Mat src, Mat& dst, Point2f center, double maxRadius, int flags)
        //

        /// <summary>
        ///  Remaps an image to polar coordinates space.
        /// </summary>
        /// <remarks>
        ///  @deprecated This function produces same result as cv::warpPolar(src, dst, src.size(), center, maxRadius, flags)
        ///  
        ///  @internal
        ///  Transform the source image using the following transformation (See @ref polar_remaps_reference_image "Polar remaps reference image c)"):
        ///  \f[\begin{array}{l}
        ///    dst( \rho , \phi ) = src(x,y) \\
        ///    dst.size() \leftarrow src.size()
        ///  \end{array}\f]
        ///  
        ///  where
        ///  \f[\begin{array}{l}
        ///    I = (dx,dy) = (x - center.x,y - center.y) \\
        ///    \rho = Kmag \cdot \texttt{magnitude} (I) ,\\
        ///    \phi = angle \cdot \texttt{angle} (I)
        ///  \end{array}\f]
        ///  
        ///  and
        ///  \f[\begin{array}{l}
        ///    Kx = src.cols / maxRadius \\
        ///    Ky = src.rows / 2\Pi
        ///  \end{array}\f]
        /// </remarks>
        /// <param name="src">
        /// Source image
        /// </param>
        /// <param name="dst">
        /// Destination image. It will have same size and type as src.
        /// </param>
        /// <param name="center">
        /// The transformation center;
        /// </param>
        /// <param name="maxRadius">
        /// The radius of the bounding circle to transform. It determines the inverse magnitude scale parameter too.
        /// </param>
        /// <param name="flags">
        /// A combination of interpolation methods, see #InterpolationFlags
        /// </param>
        /// <remarks>
        ///  @note
        ///  -   The function can not operate in-place.
        ///  -   To calculate magnitude and angle in degrees #cartToPolar is used internally thus angles are measured from 0 to 360 with accuracy about 0.3 degrees.
        ///  
        ///  @sa cv::logPolar
        ///  @endinternal
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void linearPolar(Mat src, Mat dst, in Vec2d center, double maxRadius, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_linearPolar_10(src.nativeObj, dst.nativeObj, center.Item1, center.Item2, maxRadius, flags);


        }


        //
        // C++:  void cv::warpPolar(Mat src, Mat& dst, Size dsize, Point2f center, double maxRadius, int flags)
        //

        /// <remarks>
        ///  \brief Remaps an image to polar or semilog-polar coordinates space
        ///  
        ///  @anchor polar_remaps_reference_image
        ///  ![Polar remaps reference](pics/polar_remap_doc.png)
        ///  
        ///  Transform the source image using the following transformation:
        ///  \f[
        ///  dst(\rho , \phi ) = src(x,y)
        ///  \f]
        ///  
        ///  where
        ///  \f[
        ///  \begin{array}{l}
        ///  \vec{I} = (x - center.x, \;y - center.y) \\
        ///  \phi = Kangle \cdot \texttt{angle} (\vec{I}) \\
        ///  \rho = \left\{\begin{matrix}
        ///  Klin \cdot \texttt{magnitude} (\vec{I}) & default \\
        ///  Klog \cdot log_e(\texttt{magnitude} (\vec{I})) & if \; semilog \\
        ///  \end{matrix}\right.
        ///  \end{array}
        ///  \f]
        ///  
        ///  and
        ///  \f[
        ///  \begin{array}{l}
        ///  Kangle = dsize.height / 2\Pi \\
        ///  Klin = dsize.width / maxRadius \\
        ///  Klog = dsize.width / log_e(maxRadius) \\
        ///  \end{array}
        ///  \f]
        ///  
        ///  
        ///  \par Linear vs semilog mapping
        ///  
        ///  Polar mapping can be linear or semi-log. Add one of #WarpPolarMode to `flags` to specify the polar mapping mode.
        ///  
        ///  Linear is the default mode.
        ///  
        ///  The semilog mapping emulates the human "foveal" vision that permit very high acuity on the line of sight (central vision)
        ///  in contrast to peripheral vision where acuity is minor.
        ///  
        ///  \par Option on `dsize`:
        ///  
        ///  - if both values in `dsize <=0 ` (default),
        ///  the destination image will have (almost) same area of source bounding circle:
        ///  \f[\begin{array}{l}
        ///  dsize.area  \leftarrow (maxRadius^2 \cdot \Pi) \\
        ///  dsize.width = \texttt{cvRound}(maxRadius) \\
        ///  dsize.height = \texttt{cvRound}(maxRadius \cdot \Pi) \\
        ///  \end{array}\f]
        ///  
        ///  
        ///  - if only `dsize.height <= 0`,
        ///  the destination image area will be proportional to the bounding circle area but scaled by `Kx * Kx`:
        ///  \f[\begin{array}{l}
        ///  dsize.height = \texttt{cvRound}(dsize.width \cdot \Pi) \\
        ///  \end{array}
        ///  \f]
        ///  
        ///  - if both values in `dsize > 0 `,
        ///  the destination image will have the given size therefore the area of the bounding circle will be scaled to `dsize`.
        ///  
        ///  
        ///  \par Reverse mapping
        ///  
        ///  You can get reverse mapping adding #WARP_INVERSE_MAP to `flags`
        ///  \snippet polar_transforms.cpp InverseMap
        ///  
        ///  In addiction, to calculate the original coordinate from a polar mapped coordinate \f$(rho, phi)->(x, y)\f$:
        ///  \snippet polar_transforms.cpp InverseCoordinate
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image. It will have same type as src.
        /// </param>
        /// <param name="dsize">
        /// The destination image size (see description for valid options).
        /// </param>
        /// <param name="center">
        /// The transformation center.
        /// </param>
        /// <param name="maxRadius">
        /// The radius of the bounding circle to transform. It determines the inverse magnitude scale parameter too.
        /// </param>
        /// <param name="flags">
        /// A combination of interpolation methods, #InterpolationFlags + #WarpPolarMode.
        ///              - Add #WARP_POLAR_LINEAR to select linear polar mapping (default)
        ///              - Add #WARP_POLAR_LOG to select semilog polar mapping
        ///              - Add #WARP_INVERSE_MAP for reverse mapping.
        ///  @note
        ///  -  The function can not operate in-place.
        ///  -  To calculate magnitude and angle in degrees #cartToPolar is used internally thus angles are measured from 0 to 360 with accuracy about 0.3 degrees.
        ///  -  This function uses #remap. Due to current implementation limitations the size of an input and output images should be less than 32767x32767.
        /// </param>
        /// <remarks>
        ///  @sa cv::remap
        /// </remarks>
        public static void warpPolar(Mat src, Mat dst, in Vec2d dsize, in Vec2d center, double maxRadius, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_warpPolar_10(src.nativeObj, dst.nativeObj, dsize.Item1, dsize.Item2, center.Item1, center.Item2, maxRadius, flags);


        }


        //
        // C++:  Point2d cv::phaseCorrelate(Mat src1, Mat src2, Mat window = Mat(), double* response = 0)
        //

        /// <summary>
        ///  The function is used to detect translational shifts that occur between two images.
        /// </summary>
        /// <remarks>
        ///  The operation takes advantage of the Fourier shift theorem for detecting the translational shift in
        ///  the frequency domain. It can be used for fast image registration as well as motion estimation. For
        ///  more information please see &lt;https://en.wikipedia.org/wiki/Phase_correlation&gt;
        ///  
        ///  Calculates the cross-power spectrum of two supplied source arrays. The arrays are padded if needed
        ///  with getOptimalDFTSize.
        ///  
        ///  The function performs the following equations:
        ///  - First it applies a Hanning window to each image to remove possible edge effects, if it's provided
        ///  by user. See @ref createHanningWindow and &lt;https://en.wikipedia.org/wiki/Hann_function&gt;. This window may
        ///  be cached until the array size changes to speed up processing time.
        ///  - Next it computes the forward DFTs of each source array:
        ///  \f[\mathbf{G}_a = \mathcal{F}\{src_1\}, \; \mathbf{G}_b = \mathcal{F}\{src_2\}\f]
        ///  where \f$\mathcal{F}\f$ is the forward DFT.
        ///  - It then computes the cross-power spectrum of each frequency domain array:
        ///  \f[R = \frac{ \mathbf{G}_a \mathbf{G}_b^*}{|\mathbf{G}_a \mathbf{G}_b^*|}\f]
        ///  - Next the cross-correlation is converted back into the time domain via the inverse DFT:
        ///  \f[r = \mathcal{F}^{-1}\{R\}\f]
        ///  - Finally, it computes the peak location and computes a 5x5 weighted centroid around the peak to
        ///  achieve sub-pixel accuracy.
        ///  \f[(\Delta x, \Delta y) = \texttt{weightedCentroid} \{\arg \max_{(x, y)}\{r\}\}\f]
        ///  - If non-zero, the response parameter is computed as the sum of the elements of r within the 5x5
        ///  centroid around the peak location. It is normalized to a maximum of 1 (meaning there is a single
        ///  peak) and will be smaller when there are multiple peaks.
        /// </remarks>
        /// <param name="src1">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="src2">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="window">
        /// Floating point array with windowing coefficients to reduce edge effects (optional).
        /// </param>
        /// <param name="response">
        /// Signal power within the 5x5 centroid around the peak, between 0 and 1 (optional).
        /// </param>
        /// <returns>
        ///  detected phase shift (sub-pixel) between the two arrays.
        /// </returns>
        /// <remarks>
        ///  @sa dft, getOptimalDFTSize, idft, mulSpectrums createHanningWindow
        /// </remarks>
        public static Vec2d phaseCorrelateAsVec2d(Mat src1, Mat src2, Mat window, double[] response)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (src2 != null) src2.ThrowIfDisposed();
            if (window != null) window.ThrowIfDisposed();
            double[] response_out = new double[1];
            double[] tmpArray = new double[2];
            imgproc_Imgproc_phaseCorrelate_10(src1.nativeObj, src2.nativeObj, window.nativeObj, response_out, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);
            if (response != null) response[0] = (double)response_out[0];
            return retVal;
        }

        /// <summary>
        ///  The function is used to detect translational shifts that occur between two images.
        /// </summary>
        /// <remarks>
        ///  The operation takes advantage of the Fourier shift theorem for detecting the translational shift in
        ///  the frequency domain. It can be used for fast image registration as well as motion estimation. For
        ///  more information please see &lt;https://en.wikipedia.org/wiki/Phase_correlation&gt;
        ///  
        ///  Calculates the cross-power spectrum of two supplied source arrays. The arrays are padded if needed
        ///  with getOptimalDFTSize.
        ///  
        ///  The function performs the following equations:
        ///  - First it applies a Hanning window to each image to remove possible edge effects, if it's provided
        ///  by user. See @ref createHanningWindow and &lt;https://en.wikipedia.org/wiki/Hann_function&gt;. This window may
        ///  be cached until the array size changes to speed up processing time.
        ///  - Next it computes the forward DFTs of each source array:
        ///  \f[\mathbf{G}_a = \mathcal{F}\{src_1\}, \; \mathbf{G}_b = \mathcal{F}\{src_2\}\f]
        ///  where \f$\mathcal{F}\f$ is the forward DFT.
        ///  - It then computes the cross-power spectrum of each frequency domain array:
        ///  \f[R = \frac{ \mathbf{G}_a \mathbf{G}_b^*}{|\mathbf{G}_a \mathbf{G}_b^*|}\f]
        ///  - Next the cross-correlation is converted back into the time domain via the inverse DFT:
        ///  \f[r = \mathcal{F}^{-1}\{R\}\f]
        ///  - Finally, it computes the peak location and computes a 5x5 weighted centroid around the peak to
        ///  achieve sub-pixel accuracy.
        ///  \f[(\Delta x, \Delta y) = \texttt{weightedCentroid} \{\arg \max_{(x, y)}\{r\}\}\f]
        ///  - If non-zero, the response parameter is computed as the sum of the elements of r within the 5x5
        ///  centroid around the peak location. It is normalized to a maximum of 1 (meaning there is a single
        ///  peak) and will be smaller when there are multiple peaks.
        /// </remarks>
        /// <param name="src1">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="src2">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="window">
        /// Floating point array with windowing coefficients to reduce edge effects (optional).
        /// </param>
        /// <param name="response">
        /// Signal power within the 5x5 centroid around the peak, between 0 and 1 (optional).
        /// </param>
        /// <returns>
        ///  detected phase shift (sub-pixel) between the two arrays.
        /// </returns>
        /// <remarks>
        ///  @sa dft, getOptimalDFTSize, idft, mulSpectrums createHanningWindow
        /// </remarks>
        public static Vec2d phaseCorrelateAsVec2d(Mat src1, Mat src2, Mat window)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (src2 != null) src2.ThrowIfDisposed();
            if (window != null) window.ThrowIfDisposed();

            double[] tmpArray = new double[2];
            imgproc_Imgproc_phaseCorrelate_11(src1.nativeObj, src2.nativeObj, window.nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }

        /// <summary>
        ///  The function is used to detect translational shifts that occur between two images.
        /// </summary>
        /// <remarks>
        ///  The operation takes advantage of the Fourier shift theorem for detecting the translational shift in
        ///  the frequency domain. It can be used for fast image registration as well as motion estimation. For
        ///  more information please see &lt;https://en.wikipedia.org/wiki/Phase_correlation&gt;
        ///  
        ///  Calculates the cross-power spectrum of two supplied source arrays. The arrays are padded if needed
        ///  with getOptimalDFTSize.
        ///  
        ///  The function performs the following equations:
        ///  - First it applies a Hanning window to each image to remove possible edge effects, if it's provided
        ///  by user. See @ref createHanningWindow and &lt;https://en.wikipedia.org/wiki/Hann_function&gt;. This window may
        ///  be cached until the array size changes to speed up processing time.
        ///  - Next it computes the forward DFTs of each source array:
        ///  \f[\mathbf{G}_a = \mathcal{F}\{src_1\}, \; \mathbf{G}_b = \mathcal{F}\{src_2\}\f]
        ///  where \f$\mathcal{F}\f$ is the forward DFT.
        ///  - It then computes the cross-power spectrum of each frequency domain array:
        ///  \f[R = \frac{ \mathbf{G}_a \mathbf{G}_b^*}{|\mathbf{G}_a \mathbf{G}_b^*|}\f]
        ///  - Next the cross-correlation is converted back into the time domain via the inverse DFT:
        ///  \f[r = \mathcal{F}^{-1}\{R\}\f]
        ///  - Finally, it computes the peak location and computes a 5x5 weighted centroid around the peak to
        ///  achieve sub-pixel accuracy.
        ///  \f[(\Delta x, \Delta y) = \texttt{weightedCentroid} \{\arg \max_{(x, y)}\{r\}\}\f]
        ///  - If non-zero, the response parameter is computed as the sum of the elements of r within the 5x5
        ///  centroid around the peak location. It is normalized to a maximum of 1 (meaning there is a single
        ///  peak) and will be smaller when there are multiple peaks.
        /// </remarks>
        /// <param name="src1">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="src2">
        /// Source floating point array (CV_32FC1 or CV_64FC1)
        /// </param>
        /// <param name="window">
        /// Floating point array with windowing coefficients to reduce edge effects (optional).
        /// </param>
        /// <param name="response">
        /// Signal power within the 5x5 centroid around the peak, between 0 and 1 (optional).
        /// </param>
        /// <returns>
        ///  detected phase shift (sub-pixel) between the two arrays.
        /// </returns>
        /// <remarks>
        ///  @sa dft, getOptimalDFTSize, idft, mulSpectrums createHanningWindow
        /// </remarks>
        public static Vec2d phaseCorrelateAsVec2d(Mat src1, Mat src2)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (src2 != null) src2.ThrowIfDisposed();

            double[] tmpArray = new double[2];
            imgproc_Imgproc_phaseCorrelate_12(src1.nativeObj, src2.nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++:  void cv::createHanningWindow(Mat& dst, Size winSize, int type)
        //

        /// <summary>
        ///  This function computes a Hanning window coefficients in two dimensions.
        /// </summary>
        /// <remarks>
        ///  See (https://en.wikipedia.org/wiki/Hann_function) and (https://en.wikipedia.org/wiki/Window_function)
        ///  for more information.
        ///  
        ///  An example is shown below:
        /// </remarks>
        /// <code language="c++">
        ///      // create hanning window of size 100x100 and type CV_32F
        ///      Mat hann;
        ///      createHanningWindow(hann, Size(100, 100), CV_32F);
        /// </code>
        /// <param name="dst">
        /// Destination array to place Hann coefficients in
        /// </param>
        /// <param name="winSize">
        /// The window size specifications (both width and height must be &gt; 1)
        /// </param>
        /// <param name="type">
        /// Created array type
        /// </param>
        public static void createHanningWindow(Mat dst, in Vec2d winSize, int type)
        {
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_createHanningWindow_10(dst.nativeObj, winSize.Item1, winSize.Item2, type);


        }


        //
        // C++:  void cv::pyrDown(Mat src, Mat& dst, Size dstsize = Size(), int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Blurs an image and downsamples it.
        /// </summary>
        /// <remarks>
        ///  By default, size of the output image is computed as `Size((src.cols+1)/2, (src.rows+1)/2)`, but in
        ///  any case, the following conditions should be satisfied:
        ///  
        ///  \f[\begin{array}{l} | \texttt{dstsize.width} *2-src.cols| \leq 2 \\ | \texttt{dstsize.height} *2-src.rows| \leq 2 \end{array}\f]
        ///  
        ///  The function performs the downsampling step of the Gaussian pyramid construction. First, it
        ///  convolves the source image with the kernel:
        ///  
        ///  \f[\frac{1}{256} \begin{bmatrix} 1 & 4 & 6 & 4 & 1  \\ 4 & 16 & 24 & 16 & 4  \\ 6 & 24 & 36 & 24 & 6  \\ 4 & 16 & 24 & 16 & 4  \\ 1 & 4 & 6 & 4 & 1 \end{bmatrix}\f]
        ///  
        ///  Then, it downsamples the image by rejecting even rows and columns.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the specified size and the same type as src.
        /// </param>
        /// <param name="dstsize">
        /// size of the output image.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes (#BORDER_CONSTANT isn't supported)
        /// </param>
        public static void pyrDown(Mat src, Mat dst, in Vec2d dstsize, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_pyrDown_10(src.nativeObj, dst.nativeObj, dstsize.Item1, dstsize.Item2, borderType);


        }

        /// <summary>
        ///  Blurs an image and downsamples it.
        /// </summary>
        /// <remarks>
        ///  By default, size of the output image is computed as `Size((src.cols+1)/2, (src.rows+1)/2)`, but in
        ///  any case, the following conditions should be satisfied:
        ///  
        ///  \f[\begin{array}{l} | \texttt{dstsize.width} *2-src.cols| \leq 2 \\ | \texttt{dstsize.height} *2-src.rows| \leq 2 \end{array}\f]
        ///  
        ///  The function performs the downsampling step of the Gaussian pyramid construction. First, it
        ///  convolves the source image with the kernel:
        ///  
        ///  \f[\frac{1}{256} \begin{bmatrix} 1 & 4 & 6 & 4 & 1  \\ 4 & 16 & 24 & 16 & 4  \\ 6 & 24 & 36 & 24 & 6  \\ 4 & 16 & 24 & 16 & 4  \\ 1 & 4 & 6 & 4 & 1 \end{bmatrix}\f]
        ///  
        ///  Then, it downsamples the image by rejecting even rows and columns.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image; it has the specified size and the same type as src.
        /// </param>
        /// <param name="dstsize">
        /// size of the output image.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes (#BORDER_CONSTANT isn't supported)
        /// </param>
        public static void pyrDown(Mat src, Mat dst, in Vec2d dstsize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_pyrDown_11(src.nativeObj, dst.nativeObj, dstsize.Item1, dstsize.Item2);


        }


        //
        // C++:  void cv::pyrUp(Mat src, Mat& dst, Size dstsize = Size(), int borderType = BORDER_DEFAULT)
        //

        /// <summary>
        ///  Upsamples an image and then blurs it.
        /// </summary>
        /// <remarks>
        ///  By default, size of the output image is computed as `Size(src.cols\*2, (src.rows\*2)`, but in any
        ///  case, the following conditions should be satisfied:
        ///  
        ///  \f[\begin{array}{l} | \texttt{dstsize.width} -src.cols*2| \leq  ( \texttt{dstsize.width}   \mod  2)  \\ | \texttt{dstsize.height} -src.rows*2| \leq  ( \texttt{dstsize.height}   \mod  2) \end{array}\f]
        ///  
        ///  The function performs the upsampling step of the Gaussian pyramid construction, though it can
        ///  actually be used to construct the Laplacian pyramid. First, it upsamples the source image by
        ///  injecting even zero rows and columns and then convolves the result with the same kernel as in
        ///  pyrDown multiplied by 4.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image. It has the specified size and the same type as src .
        /// </param>
        /// <param name="dstsize">
        /// size of the output image.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes (only #BORDER_DEFAULT is supported)
        /// </param>
        public static void pyrUp(Mat src, Mat dst, in Vec2d dstsize, int borderType)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_pyrUp_10(src.nativeObj, dst.nativeObj, dstsize.Item1, dstsize.Item2, borderType);


        }

        /// <summary>
        ///  Upsamples an image and then blurs it.
        /// </summary>
        /// <remarks>
        ///  By default, size of the output image is computed as `Size(src.cols\*2, (src.rows\*2)`, but in any
        ///  case, the following conditions should be satisfied:
        ///  
        ///  \f[\begin{array}{l} | \texttt{dstsize.width} -src.cols*2| \leq  ( \texttt{dstsize.width}   \mod  2)  \\ | \texttt{dstsize.height} -src.rows*2| \leq  ( \texttt{dstsize.height}   \mod  2) \end{array}\f]
        ///  
        ///  The function performs the upsampling step of the Gaussian pyramid construction, though it can
        ///  actually be used to construct the Laplacian pyramid. First, it upsamples the source image by
        ///  injecting even zero rows and columns and then convolves the result with the same kernel as in
        ///  pyrDown multiplied by 4.
        /// </remarks>
        /// <param name="src">
        /// input image.
        /// </param>
        /// <param name="dst">
        /// output image. It has the specified size and the same type as src .
        /// </param>
        /// <param name="dstsize">
        /// size of the output image.
        /// </param>
        /// <param name="borderType">
        /// Pixel extrapolation method, see #BorderTypes (only #BORDER_DEFAULT is supported)
        /// </param>
        public static void pyrUp(Mat src, Mat dst, in Vec2d dstsize)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_pyrUp_11(src.nativeObj, dst.nativeObj, dstsize.Item1, dstsize.Item2);


        }


        //
        // C++:  Ptr_CLAHE cv::createCLAHE(double clipLimit = 40.0, Size tileGridSize = Size(8, 8))
        //

        /// <summary>
        ///  Creates a smart pointer to a cv::CLAHE class and initializes it.
        /// </summary>
        /// <param name="clipLimit">
        /// Threshold for contrast limiting.
        /// </param>
        /// <param name="tileGridSize">
        /// Size of grid for histogram equalization. Input image will be divided into
        ///  equally sized rectangular tiles. tileGridSize defines the number of tiles in row and column.
        /// </param>
        public static CLAHE createCLAHE(double clipLimit, in Vec2d tileGridSize)
        {


            return CLAHE.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(imgproc_Imgproc_createCLAHE_10(clipLimit, tileGridSize.Item1, tileGridSize.Item2)));


        }


        //
        // C++:  void cv::pyrMeanShiftFiltering(Mat src, Mat& dst, double sp, double sr, int maxLevel = 1, TermCriteria termcrit = TermCriteria(TermCriteria::MAX_ITER+TermCriteria::EPS,5,1))
        //

        /// <summary>
        ///  Performs initial step of meanshift segmentation of an image.
        /// </summary>
        /// <remarks>
        ///  The function implements the filtering stage of meanshift segmentation, that is, the output of the
        ///  function is the filtered "posterized" image with color gradients and fine-grain texture flattened.
        ///  At every pixel (X,Y) of the input image (or down-sized input image, see below) the function executes
        ///  meanshift iterations, that is, the pixel (X,Y) neighborhood in the joint space-color hyperspace is
        ///  considered:
        ///  
        ///  \f[(x,y): X- \texttt{sp} \le x  \le X+ \texttt{sp} , Y- \texttt{sp} \le y  \le Y+ \texttt{sp} , ||(R,G,B)-(r,g,b)||   \le \texttt{sr}\f]
        ///  
        ///  where (R,G,B) and (r,g,b) are the vectors of color components at (X,Y) and (x,y), respectively
        ///  (though, the algorithm does not depend on the color space used, so any 3-component color space can
        ///  be used instead). Over the neighborhood the average spatial value (X',Y') and average color vector
        ///  (R',G',B') are found and they act as the neighborhood center on the next iteration:
        ///  
        ///  \f[(X,Y)~(X',Y'), (R,G,B)~(R',G',B').\f]
        ///  
        ///  After the iterations over, the color components of the initial pixel (that is, the pixel from where
        ///  the iterations started) are set to the final value (average color at the last iteration):
        ///  
        ///  \f[I(X,Y) <- (R*,G*,B*)\f]
        ///  
        ///  When maxLevel &gt; 0, the gaussian pyramid of maxLevel+1 levels is built, and the above procedure is
        ///  run on the smallest layer first. After that, the results are propagated to the larger layer and the
        ///  iterations are run again only on those pixels where the layer colors differ by more than sr from the
        ///  lower-resolution layer of the pyramid. That makes boundaries of color regions sharper. Note that the
        ///  results will be actually different from the ones obtained by running the meanshift procedure on the
        ///  whole original image (i.e. when maxLevel==0).
        /// </remarks>
        /// <param name="src">
        /// The source 8-bit, 3-channel image.
        /// </param>
        /// <param name="dst">
        /// The destination image of the same format and the same size as the source.
        /// </param>
        /// <param name="sp">
        /// The spatial window radius.
        /// </param>
        /// <param name="sr">
        /// The color window radius.
        /// </param>
        /// <param name="maxLevel">
        /// Maximum level of the pyramid for the segmentation.
        /// </param>
        /// <param name="termcrit">
        /// Termination criteria: when to stop meanshift iterations.
        /// </param>
        public static void pyrMeanShiftFiltering(Mat src, Mat dst, double sp, double sr, int maxLevel, in Vec3d termcrit)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            imgproc_Imgproc_pyrMeanShiftFiltering_10(src.nativeObj, dst.nativeObj, sp, sr, maxLevel, (int)termcrit.Item1, (int)termcrit.Item2, termcrit.Item3);


        }


        //
        // C++:  void cv::grabCut(Mat img, Mat& mask, Rect rect, Mat& bgdModel, Mat& fgdModel, int iterCount, int mode = GC_EVAL)
        //

        /// <summary>
        ///  Runs the GrabCut algorithm.
        /// </summary>
        /// <remarks>
        ///  The function implements the [GrabCut image segmentation algorithm](https://en.wikipedia.org/wiki/GrabCut).
        /// </remarks>
        /// <param name="img">
        /// Input 8-bit 3-channel image.
        /// </param>
        /// <param name="mask">
        /// Input/output 8-bit single-channel mask. The mask is initialized by the function when
        ///  mode is set to #GC_INIT_WITH_RECT. Its elements may have one of the #GrabCutClasses.
        /// </param>
        /// <param name="rect">
        /// ROI containing a segmented object. The pixels outside of the ROI are marked as
        ///  "obvious background". The parameter is only used when mode==#GC_INIT_WITH_RECT .
        /// </param>
        /// <param name="bgdModel">
        /// Temporary array for the background model. Do not modify it while you are
        ///  processing the same image.
        /// </param>
        /// <param name="fgdModel">
        /// Temporary arrays for the foreground model. Do not modify it while you are
        ///  processing the same image.
        /// </param>
        /// <param name="iterCount">
        /// Number of iterations the algorithm should make before returning the result. Note
        ///  that the result can be refined with further calls with mode==#GC_INIT_WITH_MASK or
        ///  mode==GC_EVAL .
        /// </param>
        /// <param name="mode">
        /// Operation mode that could be one of the #GrabCutModes
        /// </param>
        public static void grabCut(Mat img, Mat mask, in Vec4i rect, Mat bgdModel, Mat fgdModel, int iterCount, int mode)
        {
            if (img != null) img.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            if (bgdModel != null) bgdModel.ThrowIfDisposed();
            if (fgdModel != null) fgdModel.ThrowIfDisposed();

            imgproc_Imgproc_grabCut_10(img.nativeObj, mask.nativeObj, rect.Item1, rect.Item2, rect.Item3, rect.Item4, bgdModel.nativeObj, fgdModel.nativeObj, iterCount, mode);


        }

        /// <summary>
        ///  Runs the GrabCut algorithm.
        /// </summary>
        /// <remarks>
        ///  The function implements the [GrabCut image segmentation algorithm](https://en.wikipedia.org/wiki/GrabCut).
        /// </remarks>
        /// <param name="img">
        /// Input 8-bit 3-channel image.
        /// </param>
        /// <param name="mask">
        /// Input/output 8-bit single-channel mask. The mask is initialized by the function when
        ///  mode is set to #GC_INIT_WITH_RECT. Its elements may have one of the #GrabCutClasses.
        /// </param>
        /// <param name="rect">
        /// ROI containing a segmented object. The pixels outside of the ROI are marked as
        ///  "obvious background". The parameter is only used when mode==#GC_INIT_WITH_RECT .
        /// </param>
        /// <param name="bgdModel">
        /// Temporary array for the background model. Do not modify it while you are
        ///  processing the same image.
        /// </param>
        /// <param name="fgdModel">
        /// Temporary arrays for the foreground model. Do not modify it while you are
        ///  processing the same image.
        /// </param>
        /// <param name="iterCount">
        /// Number of iterations the algorithm should make before returning the result. Note
        ///  that the result can be refined with further calls with mode==#GC_INIT_WITH_MASK or
        ///  mode==GC_EVAL .
        /// </param>
        /// <param name="mode">
        /// Operation mode that could be one of the #GrabCutModes
        /// </param>
        public static void grabCut(Mat img, Mat mask, in Vec4i rect, Mat bgdModel, Mat fgdModel, int iterCount)
        {
            if (img != null) img.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            if (bgdModel != null) bgdModel.ThrowIfDisposed();
            if (fgdModel != null) fgdModel.ThrowIfDisposed();

            imgproc_Imgproc_grabCut_11(img.nativeObj, mask.nativeObj, rect.Item1, rect.Item2, rect.Item3, rect.Item4, bgdModel.nativeObj, fgdModel.nativeObj, iterCount);


        }


        //
        // C++:  int cv::floodFill(Mat& image, Mat& mask, Point seedPoint, Scalar newVal, Rect* rect = 0, Scalar loDiff = Scalar(), Scalar upDiff = Scalar(), int flags = 4)
        //

        /// <summary>
        ///  Fills a connected component with the given color.
        /// </summary>
        /// <remarks>
        ///  The function cv::floodFill fills a connected component starting from the seed point with the specified
        ///  color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
        ///  pixel at \f$(x,y)\f$ is considered to belong to the repainted domain if:
        ///  
        ///  - in case of a grayscale image and floating range
        ///  \f[\texttt{src} (x',y')- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} (x',y')+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a grayscale image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a color image and floating range
        ///  \f[\texttt{src} (x',y')_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} (x',y')_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} (x',y')_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} (x',y')_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} (x',y')_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} (x',y')_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  - in case of a color image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  where \f$src(x',y')\f$ is the value of one of pixel neighbors that is already known to belong to the
        ///  component. That is, to be added to the connected component, a color/brightness of the pixel should
        ///  be close enough to:
        ///  - Color/brightness of one of its neighbors that already belong to the connected component in case
        ///  of a floating range.
        ///  - Color/brightness of the seed point in case of a fixed range.
        ///  
        ///  Use these functions to either mark a connected component with the specified color in-place, or build
        ///  a mask and then extract the contour, or copy the region to another image, and so on.
        /// </remarks>
        /// <param name="image">
        /// Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the
        ///  function unless the #FLOODFILL_MASK_ONLY flag is set in the second variant of the function. See
        ///  the details below.
        /// </param>
        /// <param name="mask">
        /// Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels
        ///  taller than image. If an empty Mat is passed it will be created automatically. Since this is both an
        ///  input and output parameter, you must take responsibility of initializing it.
        ///  Flood-filling cannot go across non-zero pixels in the input mask. For example,
        ///  an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the
        ///  mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags
        ///  as described below. Additionally, the function fills the border of the mask with ones to simplify
        ///  internal processing. It is therefore possible to use the same mask in multiple calls to the function
        ///  to make sure the filled areas do not overlap.
        /// </param>
        /// <param name="seedPoint">
        /// Starting point.
        /// </param>
        /// <param name="newVal">
        /// New value of the repainted domain pixels.
        /// </param>
        /// <param name="loDiff">
        /// Maximal lower brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="upDiff">
        /// Maximal upper brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="rect">
        /// Optional output parameter set by the function to the minimum bounding rectangle of the
        ///  repainted domain.
        /// </param>
        /// <param name="flags">
        /// Operation flags. The first 8 bits contain a connectivity value. The default value of
        ///  4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A
        ///  connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner)
        ///  will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill
        ///  the mask (the default value is 1). For example, 4 | ( 255 &lt;&lt; 8 ) will consider 4 nearest
        ///  neighbours and fill the mask with a value of 255. The following additional options occupy higher
        ///  bits and therefore may be further combined with the connectivity and mask fill values using
        ///  bit-wise or (|), see #FloodFillFlags.
        /// </param>
        /// <remarks>
        ///  @note Since the mask is larger than the filled image, a pixel \f$(x, y)\f$ in image corresponds to the
        ///  pixel \f$(x+1, y+1)\f$ in the mask .
        ///  
        ///  @sa findContours
        /// </remarks>
        public static int floodFill(Mat image, Mat mask, in Vec2d seedPoint, in Vec4d newVal, out Vec4i rect, in Vec4d loDiff, in Vec4d upDiff, int flags)
        {
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            double[] rect_out = new double[4];
            int retVal = imgproc_Imgproc_floodFill_10(image.nativeObj, mask.nativeObj, seedPoint.Item1, seedPoint.Item2, newVal.Item1, newVal.Item2, newVal.Item3, newVal.Item4, rect_out, loDiff.Item1, loDiff.Item2, loDiff.Item3, loDiff.Item4, upDiff.Item1, upDiff.Item2, upDiff.Item3, upDiff.Item4, flags);
            { rect.Item1 = (int)rect_out[0]; rect.Item2 = (int)rect_out[1]; rect.Item3 = (int)rect_out[2]; rect.Item4 = (int)rect_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Fills a connected component with the given color.
        /// </summary>
        /// <remarks>
        ///  The function cv::floodFill fills a connected component starting from the seed point with the specified
        ///  color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
        ///  pixel at \f$(x,y)\f$ is considered to belong to the repainted domain if:
        ///  
        ///  - in case of a grayscale image and floating range
        ///  \f[\texttt{src} (x',y')- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} (x',y')+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a grayscale image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a color image and floating range
        ///  \f[\texttt{src} (x',y')_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} (x',y')_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} (x',y')_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} (x',y')_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} (x',y')_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} (x',y')_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  - in case of a color image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  where \f$src(x',y')\f$ is the value of one of pixel neighbors that is already known to belong to the
        ///  component. That is, to be added to the connected component, a color/brightness of the pixel should
        ///  be close enough to:
        ///  - Color/brightness of one of its neighbors that already belong to the connected component in case
        ///  of a floating range.
        ///  - Color/brightness of the seed point in case of a fixed range.
        ///  
        ///  Use these functions to either mark a connected component with the specified color in-place, or build
        ///  a mask and then extract the contour, or copy the region to another image, and so on.
        /// </remarks>
        /// <param name="image">
        /// Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the
        ///  function unless the #FLOODFILL_MASK_ONLY flag is set in the second variant of the function. See
        ///  the details below.
        /// </param>
        /// <param name="mask">
        /// Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels
        ///  taller than image. If an empty Mat is passed it will be created automatically. Since this is both an
        ///  input and output parameter, you must take responsibility of initializing it.
        ///  Flood-filling cannot go across non-zero pixels in the input mask. For example,
        ///  an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the
        ///  mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags
        ///  as described below. Additionally, the function fills the border of the mask with ones to simplify
        ///  internal processing. It is therefore possible to use the same mask in multiple calls to the function
        ///  to make sure the filled areas do not overlap.
        /// </param>
        /// <param name="seedPoint">
        /// Starting point.
        /// </param>
        /// <param name="newVal">
        /// New value of the repainted domain pixels.
        /// </param>
        /// <param name="loDiff">
        /// Maximal lower brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="upDiff">
        /// Maximal upper brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="rect">
        /// Optional output parameter set by the function to the minimum bounding rectangle of the
        ///  repainted domain.
        /// </param>
        /// <param name="flags">
        /// Operation flags. The first 8 bits contain a connectivity value. The default value of
        ///  4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A
        ///  connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner)
        ///  will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill
        ///  the mask (the default value is 1). For example, 4 | ( 255 &lt;&lt; 8 ) will consider 4 nearest
        ///  neighbours and fill the mask with a value of 255. The following additional options occupy higher
        ///  bits and therefore may be further combined with the connectivity and mask fill values using
        ///  bit-wise or (|), see #FloodFillFlags.
        /// </param>
        /// <remarks>
        ///  @note Since the mask is larger than the filled image, a pixel \f$(x, y)\f$ in image corresponds to the
        ///  pixel \f$(x+1, y+1)\f$ in the mask .
        ///  
        ///  @sa findContours
        /// </remarks>
        public static int floodFill(Mat image, Mat mask, in Vec2d seedPoint, in Vec4d newVal, out Vec4i rect, in Vec4d loDiff, in Vec4d upDiff)
        {
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            double[] rect_out = new double[4];
            int retVal = imgproc_Imgproc_floodFill_11(image.nativeObj, mask.nativeObj, seedPoint.Item1, seedPoint.Item2, newVal.Item1, newVal.Item2, newVal.Item3, newVal.Item4, rect_out, loDiff.Item1, loDiff.Item2, loDiff.Item3, loDiff.Item4, upDiff.Item1, upDiff.Item2, upDiff.Item3, upDiff.Item4);
            { rect.Item1 = (int)rect_out[0]; rect.Item2 = (int)rect_out[1]; rect.Item3 = (int)rect_out[2]; rect.Item4 = (int)rect_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Fills a connected component with the given color.
        /// </summary>
        /// <remarks>
        ///  The function cv::floodFill fills a connected component starting from the seed point with the specified
        ///  color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
        ///  pixel at \f$(x,y)\f$ is considered to belong to the repainted domain if:
        ///  
        ///  - in case of a grayscale image and floating range
        ///  \f[\texttt{src} (x',y')- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} (x',y')+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a grayscale image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a color image and floating range
        ///  \f[\texttt{src} (x',y')_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} (x',y')_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} (x',y')_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} (x',y')_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} (x',y')_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} (x',y')_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  - in case of a color image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  where \f$src(x',y')\f$ is the value of one of pixel neighbors that is already known to belong to the
        ///  component. That is, to be added to the connected component, a color/brightness of the pixel should
        ///  be close enough to:
        ///  - Color/brightness of one of its neighbors that already belong to the connected component in case
        ///  of a floating range.
        ///  - Color/brightness of the seed point in case of a fixed range.
        ///  
        ///  Use these functions to either mark a connected component with the specified color in-place, or build
        ///  a mask and then extract the contour, or copy the region to another image, and so on.
        /// </remarks>
        /// <param name="image">
        /// Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the
        ///  function unless the #FLOODFILL_MASK_ONLY flag is set in the second variant of the function. See
        ///  the details below.
        /// </param>
        /// <param name="mask">
        /// Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels
        ///  taller than image. If an empty Mat is passed it will be created automatically. Since this is both an
        ///  input and output parameter, you must take responsibility of initializing it.
        ///  Flood-filling cannot go across non-zero pixels in the input mask. For example,
        ///  an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the
        ///  mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags
        ///  as described below. Additionally, the function fills the border of the mask with ones to simplify
        ///  internal processing. It is therefore possible to use the same mask in multiple calls to the function
        ///  to make sure the filled areas do not overlap.
        /// </param>
        /// <param name="seedPoint">
        /// Starting point.
        /// </param>
        /// <param name="newVal">
        /// New value of the repainted domain pixels.
        /// </param>
        /// <param name="loDiff">
        /// Maximal lower brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="upDiff">
        /// Maximal upper brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="rect">
        /// Optional output parameter set by the function to the minimum bounding rectangle of the
        ///  repainted domain.
        /// </param>
        /// <param name="flags">
        /// Operation flags. The first 8 bits contain a connectivity value. The default value of
        ///  4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A
        ///  connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner)
        ///  will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill
        ///  the mask (the default value is 1). For example, 4 | ( 255 &lt;&lt; 8 ) will consider 4 nearest
        ///  neighbours and fill the mask with a value of 255. The following additional options occupy higher
        ///  bits and therefore may be further combined with the connectivity and mask fill values using
        ///  bit-wise or (|), see #FloodFillFlags.
        /// </param>
        /// <remarks>
        ///  @note Since the mask is larger than the filled image, a pixel \f$(x, y)\f$ in image corresponds to the
        ///  pixel \f$(x+1, y+1)\f$ in the mask .
        ///  
        ///  @sa findContours
        /// </remarks>
        public static int floodFill(Mat image, Mat mask, in Vec2d seedPoint, in Vec4d newVal, out Vec4i rect, in Vec4d loDiff)
        {
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            double[] rect_out = new double[4];
            int retVal = imgproc_Imgproc_floodFill_12(image.nativeObj, mask.nativeObj, seedPoint.Item1, seedPoint.Item2, newVal.Item1, newVal.Item2, newVal.Item3, newVal.Item4, rect_out, loDiff.Item1, loDiff.Item2, loDiff.Item3, loDiff.Item4);
            { rect.Item1 = (int)rect_out[0]; rect.Item2 = (int)rect_out[1]; rect.Item3 = (int)rect_out[2]; rect.Item4 = (int)rect_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Fills a connected component with the given color.
        /// </summary>
        /// <remarks>
        ///  The function cv::floodFill fills a connected component starting from the seed point with the specified
        ///  color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
        ///  pixel at \f$(x,y)\f$ is considered to belong to the repainted domain if:
        ///  
        ///  - in case of a grayscale image and floating range
        ///  \f[\texttt{src} (x',y')- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} (x',y')+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a grayscale image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a color image and floating range
        ///  \f[\texttt{src} (x',y')_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} (x',y')_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} (x',y')_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} (x',y')_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} (x',y')_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} (x',y')_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  - in case of a color image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  where \f$src(x',y')\f$ is the value of one of pixel neighbors that is already known to belong to the
        ///  component. That is, to be added to the connected component, a color/brightness of the pixel should
        ///  be close enough to:
        ///  - Color/brightness of one of its neighbors that already belong to the connected component in case
        ///  of a floating range.
        ///  - Color/brightness of the seed point in case of a fixed range.
        ///  
        ///  Use these functions to either mark a connected component with the specified color in-place, or build
        ///  a mask and then extract the contour, or copy the region to another image, and so on.
        /// </remarks>
        /// <param name="image">
        /// Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the
        ///  function unless the #FLOODFILL_MASK_ONLY flag is set in the second variant of the function. See
        ///  the details below.
        /// </param>
        /// <param name="mask">
        /// Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels
        ///  taller than image. If an empty Mat is passed it will be created automatically. Since this is both an
        ///  input and output parameter, you must take responsibility of initializing it.
        ///  Flood-filling cannot go across non-zero pixels in the input mask. For example,
        ///  an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the
        ///  mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags
        ///  as described below. Additionally, the function fills the border of the mask with ones to simplify
        ///  internal processing. It is therefore possible to use the same mask in multiple calls to the function
        ///  to make sure the filled areas do not overlap.
        /// </param>
        /// <param name="seedPoint">
        /// Starting point.
        /// </param>
        /// <param name="newVal">
        /// New value of the repainted domain pixels.
        /// </param>
        /// <param name="loDiff">
        /// Maximal lower brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="upDiff">
        /// Maximal upper brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="rect">
        /// Optional output parameter set by the function to the minimum bounding rectangle of the
        ///  repainted domain.
        /// </param>
        /// <param name="flags">
        /// Operation flags. The first 8 bits contain a connectivity value. The default value of
        ///  4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A
        ///  connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner)
        ///  will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill
        ///  the mask (the default value is 1). For example, 4 | ( 255 &lt;&lt; 8 ) will consider 4 nearest
        ///  neighbours and fill the mask with a value of 255. The following additional options occupy higher
        ///  bits and therefore may be further combined with the connectivity and mask fill values using
        ///  bit-wise or (|), see #FloodFillFlags.
        /// </param>
        /// <remarks>
        ///  @note Since the mask is larger than the filled image, a pixel \f$(x, y)\f$ in image corresponds to the
        ///  pixel \f$(x+1, y+1)\f$ in the mask .
        ///  
        ///  @sa findContours
        /// </remarks>
        public static int floodFill(Mat image, Mat mask, in Vec2d seedPoint, in Vec4d newVal, out Vec4i rect)
        {
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            double[] rect_out = new double[4];
            int retVal = imgproc_Imgproc_floodFill_13(image.nativeObj, mask.nativeObj, seedPoint.Item1, seedPoint.Item2, newVal.Item1, newVal.Item2, newVal.Item3, newVal.Item4, rect_out);
            { rect.Item1 = (int)rect_out[0]; rect.Item2 = (int)rect_out[1]; rect.Item3 = (int)rect_out[2]; rect.Item4 = (int)rect_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Fills a connected component with the given color.
        /// </summary>
        /// <remarks>
        ///  The function cv::floodFill fills a connected component starting from the seed point with the specified
        ///  color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
        ///  pixel at \f$(x,y)\f$ is considered to belong to the repainted domain if:
        ///  
        ///  - in case of a grayscale image and floating range
        ///  \f[\texttt{src} (x',y')- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} (x',y')+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a grayscale image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)- \texttt{loDiff} \leq \texttt{src} (x,y)  \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)+ \texttt{upDiff}\f]
        ///  
        ///  
        ///  - in case of a color image and floating range
        ///  \f[\texttt{src} (x',y')_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} (x',y')_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} (x',y')_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} (x',y')_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} (x',y')_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} (x',y')_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  - in case of a color image and fixed range
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r- \texttt{loDiff} _r \leq \texttt{src} (x,y)_r \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_r+ \texttt{upDiff} _r,\f]
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g- \texttt{loDiff} _g \leq \texttt{src} (x,y)_g \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_g+ \texttt{upDiff} _g\f]
        ///  and
        ///  \f[\texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b- \texttt{loDiff} _b \leq \texttt{src} (x,y)_b \leq \texttt{src} ( \texttt{seedPoint} .x, \texttt{seedPoint} .y)_b+ \texttt{upDiff} _b\f]
        ///  
        ///  
        ///  where \f$src(x',y')\f$ is the value of one of pixel neighbors that is already known to belong to the
        ///  component. That is, to be added to the connected component, a color/brightness of the pixel should
        ///  be close enough to:
        ///  - Color/brightness of one of its neighbors that already belong to the connected component in case
        ///  of a floating range.
        ///  - Color/brightness of the seed point in case of a fixed range.
        ///  
        ///  Use these functions to either mark a connected component with the specified color in-place, or build
        ///  a mask and then extract the contour, or copy the region to another image, and so on.
        /// </remarks>
        /// <param name="image">
        /// Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the
        ///  function unless the #FLOODFILL_MASK_ONLY flag is set in the second variant of the function. See
        ///  the details below.
        /// </param>
        /// <param name="mask">
        /// Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels
        ///  taller than image. If an empty Mat is passed it will be created automatically. Since this is both an
        ///  input and output parameter, you must take responsibility of initializing it.
        ///  Flood-filling cannot go across non-zero pixels in the input mask. For example,
        ///  an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the
        ///  mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags
        ///  as described below. Additionally, the function fills the border of the mask with ones to simplify
        ///  internal processing. It is therefore possible to use the same mask in multiple calls to the function
        ///  to make sure the filled areas do not overlap.
        /// </param>
        /// <param name="seedPoint">
        /// Starting point.
        /// </param>
        /// <param name="newVal">
        /// New value of the repainted domain pixels.
        /// </param>
        /// <param name="loDiff">
        /// Maximal lower brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="upDiff">
        /// Maximal upper brightness/color difference between the currently observed pixel and
        ///  one of its neighbors belonging to the component, or a seed pixel being added to the component.
        /// </param>
        /// <param name="rect">
        /// Optional output parameter set by the function to the minimum bounding rectangle of the
        ///  repainted domain.
        /// </param>
        /// <param name="flags">
        /// Operation flags. The first 8 bits contain a connectivity value. The default value of
        ///  4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A
        ///  connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner)
        ///  will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill
        ///  the mask (the default value is 1). For example, 4 | ( 255 &lt;&lt; 8 ) will consider 4 nearest
        ///  neighbours and fill the mask with a value of 255. The following additional options occupy higher
        ///  bits and therefore may be further combined with the connectivity and mask fill values using
        ///  bit-wise or (|), see #FloodFillFlags.
        /// </param>
        /// <remarks>
        ///  @note Since the mask is larger than the filled image, a pixel \f$(x, y)\f$ in image corresponds to the
        ///  pixel \f$(x+1, y+1)\f$ in the mask .
        ///  
        ///  @sa findContours
        /// </remarks>
        public static int floodFill(Mat image, Mat mask, in Vec2d seedPoint, in Vec4d newVal)
        {
            if (image != null) image.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            return imgproc_Imgproc_floodFill_14(image.nativeObj, mask.nativeObj, seedPoint.Item1, seedPoint.Item2, newVal.Item1, newVal.Item2, newVal.Item3, newVal.Item4);


        }


        //
        // C++:  Moments cv::moments(Mat array, bool binaryImage = false)
        //

        /// <summary>
        ///  Calculates all of the moments up to the third order of a polygon or rasterized shape.
        /// </summary>
        /// <remarks>
        ///  The function computes moments, up to the 3rd order, of a vector shape or a rasterized shape. The
        ///  results are returned in the structure cv::Moments.
        /// </remarks>
        /// <param name="array">
        /// Single chanel raster image (CV_8U, CV_16U, CV_16S, CV_32F, CV_64F) or an array (
        ///  \f$1 \times N\f$ or \f$N \times 1\f$ ) of 2D points (Point or Point2f).
        /// </param>
        /// <param name="binaryImage">
        /// If it is true, all non-zero image pixels are treated as 1's. The parameter is
        ///  used for images only.
        /// </param>
        /// <returns>
        ///  moments.
        /// </returns>
        /// <remarks>
        ///  @note Only applicable to contour moments calculations from Python bindings: Note that the numpy
        ///  type for the input array should be either np.int32 or np.float32.
        ///  
        ///  @sa  contourArea, arcLength
        /// </remarks>
        public static Vec10d momentsAsVec10d(Mat array, bool binaryImage)
        {
            if (array != null) array.ThrowIfDisposed();

            double[] tmpArray = new double[10];
            imgproc_Imgproc_moments_10(array.nativeObj, binaryImage, tmpArray);
            Vec10d retVal = new Vec10d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4], tmpArray[5], tmpArray[6], tmpArray[7], tmpArray[8], tmpArray[9]);

            return retVal;
        }

        /// <summary>
        ///  Calculates all of the moments up to the third order of a polygon or rasterized shape.
        /// </summary>
        /// <remarks>
        ///  The function computes moments, up to the 3rd order, of a vector shape or a rasterized shape. The
        ///  results are returned in the structure cv::Moments.
        /// </remarks>
        /// <param name="array">
        /// Single chanel raster image (CV_8U, CV_16U, CV_16S, CV_32F, CV_64F) or an array (
        ///  \f$1 \times N\f$ or \f$N \times 1\f$ ) of 2D points (Point or Point2f).
        /// </param>
        /// <param name="binaryImage">
        /// If it is true, all non-zero image pixels are treated as 1's. The parameter is
        ///  used for images only.
        /// </param>
        /// <returns>
        ///  moments.
        /// </returns>
        /// <remarks>
        ///  @note Only applicable to contour moments calculations from Python bindings: Note that the numpy
        ///  type for the input array should be either np.int32 or np.float32.
        ///  
        ///  @sa  contourArea, arcLength
        /// </remarks>
        public static Vec10d momentsAsVec10d(Mat array)
        {
            if (array != null) array.ThrowIfDisposed();

            double[] tmpArray = new double[10];
            imgproc_Imgproc_moments_11(array.nativeObj, tmpArray);
            Vec10d retVal = new Vec10d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4], tmpArray[5], tmpArray[6], tmpArray[7], tmpArray[8], tmpArray[9]);

            return retVal;
        }


        //
        // C++:  void cv::HuMoments(Moments m, Mat& hu)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void HuMoments(in Vec10d m, Mat hu)
        {
            if (hu != null) hu.ThrowIfDisposed();

            imgproc_Imgproc_HuMoments_10(m.Item1, m.Item2, m.Item3, m.Item4, m.Item5, m.Item6, m.Item7, m.Item8, m.Item9, m.Item10, hu.nativeObj);


        }


        //
        // C++:  void cv::findContours(Mat image, vector_vector_Point& contours, Mat& hierarchy, int mode, int method, Point offset = Point())
        //

        /// <summary>
        ///  Finds contours in a binary image.
        /// </summary>
        /// <remarks>
        ///  The function retrieves contours from the binary image using the algorithm @cite Suzuki85 . The contours
        ///  are a useful tool for shape analysis and object detection and recognition. See squares.cpp in the
        ///  OpenCV sample directory.
        ///  @note Since opencv 3.2 source image is not modified by this function.
        /// </remarks>
        /// <param name="image">
        /// Source, an 8-bit single-channel image. Non-zero pixels are treated as 1's. Zero
        ///  pixels remain 0's, so the image is treated as binary . You can use #compare, #inRange, #threshold ,
        ///  #adaptiveThreshold, #Canny, and others to create a binary image out of a grayscale or color one.
        ///  If mode equals to #RETR_CCOMP or #RETR_FLOODFILL, the input can also be a 32-bit integer image of labels (CV_32SC1).
        /// </param>
        /// <param name="contours">
        /// Detected contours. Each contour is stored as a vector of points (e.g.
        ///  std::vector&lt;std::vector&lt;cv::Point&gt; &gt;).
        /// </param>
        /// <param name="hierarchy">
        /// Optional output vector (e.g. std::vector&lt;cv::Vec4i&gt;), containing information about the image topology. It has
        ///  as many elements as the number of contours. For each i-th contour contours[i], the elements
        ///  hierarchy[i][0] , hierarchy[i][1] , hierarchy[i][2] , and hierarchy[i][3] are set to 0-based indices
        ///  in contours of the next and previous contours at the same hierarchical level, the first child
        ///  contour and the parent contour, respectively. If for the contour i there are no next, previous,
        ///  parent, or nested contours, the corresponding elements of hierarchy[i] will be negative.
        ///  @note In Python, hierarchy is nested inside a top level array. Use hierarchy[0][i] to access hierarchical elements of i-th contour.
        /// </param>
        /// <param name="mode">
        /// Contour retrieval mode, see #RetrievalModes
        /// </param>
        /// <param name="method">
        /// Contour approximation method, see #ContourApproximationModes
        /// </param>
        /// <param name="offset">
        /// Optional offset by which every contour point is shifted. This is useful if the
        ///  contours are extracted from the image ROI and then they should be analyzed in the whole image
        ///  context.
        /// </param>
        public static void findContours(Mat image, List<MatOfPoint> contours, Mat hierarchy, int mode, int method, in Vec2d offset)
        {
            if (image != null) image.ThrowIfDisposed();
            if (hierarchy != null) hierarchy.ThrowIfDisposed();
            using Mat contours_mat = new Mat();
            imgproc_Imgproc_findContours_10(image.nativeObj, contours_mat.nativeObj, hierarchy.nativeObj, mode, method, offset.Item1, offset.Item2);
            Converters.Mat_to_vector_vector_Point(contours_mat, contours);

        }


        //
        // C++:  Rect cv::boundingRect(Mat array)
        //

        /// <summary>
        ///  Calculates the up-right bounding rectangle of a point set or non-zero pixels of gray-scale image.
        /// </summary>
        /// <remarks>
        ///  The function calculates and returns the minimal up-right bounding rectangle for the specified point set or
        ///  non-zero pixels of gray-scale image.
        /// </remarks>
        /// <param name="array">
        /// Input gray-scale image or 2D point set, stored in std::vector or Mat.
        /// </param>
        public static Vec4i boundingRectAsVec4i(Mat array)
        {
            if (array != null) array.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            imgproc_Imgproc_boundingRect_10(array.nativeObj, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }


        //
        // C++:  RotatedRect cv::minAreaRect(vector_Point2f points)
        //

        /// <summary>
        ///  Finds a rotated rectangle of the minimum area enclosing the input 2D point set.
        /// </summary>
        /// <remarks>
        ///  The function calculates and returns the minimum-area bounding rectangle (possibly rotated) for a
        ///  specified point set. The angle of rotation represents the angle between the line connecting the starting
        ///  and ending points (based on the clockwise order with greatest index for the corner with greatest \f$y\f$)
        ///  and the horizontal axis. This angle always falls between \f$[-90, 0)\f$ because, if the object
        ///  rotates more than a rect angle, the next edge is used to measure the angle. The starting and ending points change
        ///  as the object rotates.Developer should keep in mind that the returned RotatedRect can contain negative
        ///  indices when data is close to the containing Mat element boundary.
        /// </remarks>
        /// <param name="points">
        /// Input vector of 2D points, stored in std::vector&lt;&gt; or Mat
        /// </param>
        public static Vec5d minAreaRectAsVec5d(MatOfPoint2f points)
        {
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            double[] tmpArray = new double[5];
            imgproc_Imgproc_minAreaRect_10(points_mat.nativeObj, tmpArray);
            Vec5d retVal = new Vec5d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4]);

            return retVal;
        }


        //
        // C++:  void cv::boxPoints(RotatedRect box, Mat& points)
        //

        /// <summary>
        ///  Finds the four vertices of a rotated rect. Useful to draw the rotated rectangle.
        /// </summary>
        /// <remarks>
        ///  The function finds the four vertices of a rotated rectangle. The four vertices are returned
        ///  in clockwise order starting from the point with greatest \f$y\f$. If two points have the
        ///  same \f$y\f$ coordinate the rightmost is the starting point. This function is useful to draw the
        ///  rectangle. In C++, instead of using this function, you can directly use RotatedRect::points method. Please
        ///  visit the @ref tutorial_bounding_rotated_ellipses "tutorial on Creating Bounding rotated boxes and ellipses for contours" for more information.
        /// </remarks>
        /// <param name="box">
        /// The input rotated rectangle. It may be the output of @ref minAreaRect.
        /// </param>
        /// <param name="points">
        /// The output array of four vertices of rectangles.
        /// </param>
        public static void boxPoints(in Vec5d box, Mat points)
        {
            if (points != null) points.ThrowIfDisposed();

            imgproc_Imgproc_boxPoints_10(box.Item1, box.Item2, box.Item3, box.Item4, box.Item5, points.nativeObj);


        }


        //
        // C++:  void cv::minEnclosingCircle(vector_Point2f points, Point2f& center, float& radius)
        //

        /// <summary>
        ///  Finds a circle of the minimum area enclosing a 2D point set.
        /// </summary>
        /// <remarks>
        ///  The function finds the minimal enclosing circle of a 2D point set using an iterative algorithm.
        /// </remarks>
        /// <param name="points">
        /// Input vector of 2D points, stored in std::vector&lt;&gt; or Mat
        /// </param>
        /// <param name="center">
        /// Output center of the circle.
        /// </param>
        /// <param name="radius">
        /// Output radius of the circle.
        /// </param>
        public static void minEnclosingCircle(MatOfPoint2f points, out Vec2d center, float[] radius)
        {
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            double[] center_out = new double[2];
            double[] radius_out = new double[1];
            imgproc_Imgproc_minEnclosingCircle_10(points_mat.nativeObj, center_out, radius_out);
            { center.Item1 = center_out[0]; center.Item2 = center_out[1]; }
            if (radius != null) radius[0] = (float)radius_out[0];

        }


        //
        // C++:  RotatedRect cv::fitEllipse(vector_Point2f points)
        //

        /// <summary>
        ///  Fits an ellipse around a set of 2D points.
        /// </summary>
        /// <remarks>
        ///  The function calculates the ellipse that fits (in a least-squares sense) a set of 2D points best of
        ///  all. It returns the rotated rectangle in which the ellipse is inscribed. The first algorithm described by @cite Fitzgibbon95
        ///  is used. Developer should keep in mind that it is possible that the returned
        ///  ellipse/rotatedRect data contains negative indices, due to the data points being close to the
        ///  border of the containing Mat element.
        /// </remarks>
        /// <param name="points">
        /// Input 2D point set, stored in std::vector&lt;&gt; or Mat
        /// </param>
        /// <remarks>
        ///  @note Input point types are @ref Point2i or @ref Point2f and at least 5 points are required.
        ///  @note @ref getClosestEllipsePoints function can be used to compute the ellipse fitting error.
        /// </remarks>
        public static Vec5d fitEllipseAsVec5d(MatOfPoint2f points)
        {
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            double[] tmpArray = new double[5];
            imgproc_Imgproc_fitEllipse_10(points_mat.nativeObj, tmpArray);
            Vec5d retVal = new Vec5d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4]);

            return retVal;
        }


        //
        // C++:  RotatedRect cv::fitEllipseAMS(Mat points)
        //

        /// <summary>
        ///  Fits an ellipse around a set of 2D points.
        /// </summary>
        /// <remarks>
        ///   The function calculates the ellipse that fits a set of 2D points.
        ///   It returns the rotated rectangle in which the ellipse is inscribed.
        ///   The Approximate Mean Square (AMS) proposed by @cite Taubin1991 is used.
        ///  
        ///   For an ellipse, this basis set is \f$ \chi= \left(x^2, x y, y^2, x, y, 1\right) \f$,
        ///   which is a set of six free coefficients \f$ A^T=\left\{A_{\text{xx}},A_{\text{xy}},A_{\text{yy}},A_x,A_y,A_0\right\} \f$.
        ///   However, to specify an ellipse, all that is needed is five numbers; the major and minor axes lengths \f$ (a,b) \f$,
        ///   the position \f$ (x_0,y_0) \f$, and the orientation \f$ \theta \f$. This is because the basis set includes lines,
        ///   quadratics, parabolic and hyperbolic functions as well as elliptical functions as possible fits.
        ///   If the fit is found to be a parabolic or hyperbolic function then the standard #fitEllipse method is used.
        ///   The AMS method restricts the fit to parabolic, hyperbolic and elliptical curves
        ///   by imposing the condition that \f$ A^T ( D_x^T D_x  +   D_y^T D_y) A = 1 \f$ where
        ///   the matrices \f$ Dx \f$ and \f$ Dy \f$ are the partial derivatives of the design matrix \f$ D \f$ with
        ///   respect to x and y. The matrices are formed row by row applying the following to
        ///   each of the points in the set:
        ///   \f{align*}{
        ///   D(i,:)&amp;=\left\{x_i^2, x_i y_i, y_i^2, x_i, y_i, 1\right\} &amp;
        ///   D_x(i,:)&amp;=\left\{2 x_i,y_i,0,1,0,0\right\} &amp;
        ///   D_y(i,:)&amp;=\left\{0,x_i,2 y_i,0,1,0\right\}
        ///   \f}
        ///   The AMS method minimizes the cost function
        ///   \f{equation*}{
        ///   \epsilon ^2=\frac{ A^T D^T D A }{ A^T (D_x^T D_x +  D_y^T D_y) A^T }
        ///   \f}
        ///  
        ///   The minimum cost is found by solving the generalized eigenvalue problem.
        ///  
        ///   \f{equation*}{
        ///   D^T D A = \lambda  \left( D_x^T D_x +  D_y^T D_y\right) A
        ///   \f}
        /// </remarks>
        /// <param name="points">
        /// Input 2D point set, stored in std::vector&lt;&gt; or Mat
        /// </param>
        /// <remarks>
        ///   @note Input point types are @ref Point2i or @ref Point2f and at least 5 points are required.
        ///   @note @ref getClosestEllipsePoints function can be used to compute the ellipse fitting error.
        /// </remarks>
        public static Vec5d fitEllipseAMSAsVec5d(Mat points)
        {
            if (points != null) points.ThrowIfDisposed();

            double[] tmpArray = new double[5];
            imgproc_Imgproc_fitEllipseAMS_10(points.nativeObj, tmpArray);
            Vec5d retVal = new Vec5d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4]);

            return retVal;
        }


        //
        // C++:  RotatedRect cv::fitEllipseDirect(Mat points)
        //

        /// <summary>
        ///  Fits an ellipse around a set of 2D points.
        /// </summary>
        /// <remarks>
        ///   The function calculates the ellipse that fits a set of 2D points.
        ///   It returns the rotated rectangle in which the ellipse is inscribed.
        ///   The Direct least square (Direct) method by @cite oy1998NumericallySD is used.
        ///  
        ///   For an ellipse, this basis set is \f$ \chi= \left(x^2, x y, y^2, x, y, 1\right) \f$,
        ///   which is a set of six free coefficients \f$ A^T=\left\{A_{\text{xx}},A_{\text{xy}},A_{\text{yy}},A_x,A_y,A_0\right\} \f$.
        ///   However, to specify an ellipse, all that is needed is five numbers; the major and minor axes lengths \f$ (a,b) \f$,
        ///   the position \f$ (x_0,y_0) \f$, and the orientation \f$ \theta \f$. This is because the basis set includes lines,
        ///   quadratics, parabolic and hyperbolic functions as well as elliptical functions as possible fits.
        ///   The Direct method confines the fit to ellipses by ensuring that \f$ 4 A_{xx} A_{yy}- A_{xy}^2 > 0 \f$.
        ///   The condition imposed is that \f$ 4 A_{xx} A_{yy}- A_{xy}^2=1 \f$ which satisfies the inequality
        ///   and as the coefficients can be arbitrarily scaled is not overly restrictive.
        ///  
        ///   \f{equation*}{
        ///   \epsilon ^2= A^T D^T D A \quad \text{with} \quad A^T C A =1 \quad \text{and} \quad C=\left(\begin{matrix}
        ///   0 &amp; 0  &amp; 2  &amp; 0  &amp; 0  &amp;  0  \\
        ///   0 &amp; -1  &amp; 0  &amp; 0  &amp; 0  &amp;  0 \\
        ///   2 &amp; 0  &amp; 0  &amp; 0  &amp; 0  &amp;  0 \\
        ///   0 &amp; 0  &amp; 0  &amp; 0  &amp; 0  &amp;  0 \\
        ///   0 &amp; 0  &amp; 0  &amp; 0  &amp; 0  &amp;  0 \\
        ///   0 &amp; 0  &amp; 0  &amp; 0  &amp; 0  &amp;  0
        ///   \end{matrix} \right)
        ///   \f}
        ///  
        ///   The minimum cost is found by solving the generalized eigenvalue problem.
        ///  
        ///   \f{equation*}{
        ///   D^T D A = \lambda  \left( C\right) A
        ///   \f}
        ///  
        ///   The system produces only one positive eigenvalue \f$ \lambda\f$ which is chosen as the solution
        ///   with its eigenvector \f$\mathbf{u}\f$. These are used to find the coefficients
        ///  
        ///   \f{equation*}{
        ///   A = \sqrt{\frac{1}{\mathbf{u}^T C \mathbf{u}}}  \mathbf{u}
        ///   \f}
        ///   The scaling factor guarantees that  \f$A^T C A =1\f$.
        /// </remarks>
        /// <param name="points">
        /// Input 2D point set, stored in std::vector&lt;&gt; or Mat
        /// </param>
        /// <remarks>
        ///   @note Input point types are @ref Point2i or @ref Point2f and at least 5 points are required.
        ///   @note @ref getClosestEllipsePoints function can be used to compute the ellipse fitting error.
        /// </remarks>
        public static Vec5d fitEllipseDirectAsVec5d(Mat points)
        {
            if (points != null) points.ThrowIfDisposed();

            double[] tmpArray = new double[5];
            imgproc_Imgproc_fitEllipseDirect_10(points.nativeObj, tmpArray);
            Vec5d retVal = new Vec5d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4]);

            return retVal;
        }


        //
        // C++:  void cv::getClosestEllipsePoints(RotatedRect ellipse_params, Mat points, Mat& closest_pts)
        //

        /// <summary>
        ///  Compute for each 2d point the nearest 2d point located on a given ellipse.
        /// </summary>
        /// <remarks>
        ///   The function computes the nearest 2d location on a given ellipse for a vector of 2d points and is based on @cite Chatfield2017 code.
        ///   This function can be used to compute for instance the ellipse fitting error.
        /// </remarks>
        /// <param name="ellipse_params">
        /// Ellipse parameters
        /// </param>
        /// <param name="points">
        /// Input 2d points
        /// </param>
        /// <param name="closest_pts">
        /// For each 2d point, their corresponding closest 2d point located on a given ellipse
        /// </param>
        /// <remarks>
        ///   @note Input point types are @ref Point2i or @ref Point2f
        ///   @see fitEllipse, fitEllipseAMS, fitEllipseDirect
        /// </remarks>
        public static void getClosestEllipsePoints(in Vec5d ellipse_params, Mat points, Mat closest_pts)
        {
            if (points != null) points.ThrowIfDisposed();
            if (closest_pts != null) closest_pts.ThrowIfDisposed();

            imgproc_Imgproc_getClosestEllipsePoints_10(ellipse_params.Item1, ellipse_params.Item2, ellipse_params.Item3, ellipse_params.Item4, ellipse_params.Item5, points.nativeObj, closest_pts.nativeObj);


        }


        //
        // C++:  double cv::pointPolygonTest(vector_Point2f contour, Point2f pt, bool measureDist)
        //

        /// <summary>
        ///  Performs a point-in-contour test.
        /// </summary>
        /// <remarks>
        ///  The function determines whether the point is inside a contour, outside, or lies on an edge (or
        ///  coincides with a vertex). It returns positive (inside), negative (outside), or zero (on an edge)
        ///  value, correspondingly. When measureDist=false , the return value is +1, -1, and 0, respectively.
        ///  Otherwise, the return value is a signed distance between the point and the nearest contour edge.
        ///  
        ///  See below a sample output of the function where each image pixel is tested against the contour:
        ///  
        ///  ![sample output](pics/pointpolygon.png)
        /// </remarks>
        /// <param name="contour">
        /// Input contour.
        /// </param>
        /// <param name="pt">
        /// Point tested against the contour.
        /// </param>
        /// <param name="measureDist">
        /// If true, the function estimates the signed distance from the point to the
        ///  nearest contour edge. Otherwise, the function only checks if the point is inside a contour or not.
        /// </param>
        public static double pointPolygonTest(MatOfPoint2f contour, in Vec2d pt, bool measureDist)
        {
            if (contour != null) contour.ThrowIfDisposed();
            Mat contour_mat = contour;
            return imgproc_Imgproc_pointPolygonTest_10(contour_mat.nativeObj, pt.Item1, pt.Item2, measureDist);


        }


        //
        // C++:  int cv::rotatedRectangleIntersection(RotatedRect rect1, RotatedRect rect2, Mat& intersectingRegion)
        //

        /// <summary>
        ///  Finds out if there is any intersection between two rotated rectangles.
        /// </summary>
        /// <remarks>
        ///  If there is then the vertices of the intersecting region are returned as well.
        ///  
        ///  Below are some examples of intersection configurations. The hatched pattern indicates the
        ///  intersecting region and the red vertices are returned by the function.
        ///  
        ///  ![intersection examples](pics/intersection.png)
        /// </remarks>
        /// <param name="rect1">
        /// First rectangle
        /// </param>
        /// <param name="rect2">
        /// Second rectangle
        /// </param>
        /// <param name="intersectingRegion">
        /// The output array of the vertices of the intersecting region. It returns
        ///  at most 8 vertices. Stored as std::vector&lt;cv::Point2f&gt; or cv::Mat as Mx1 of type CV_32FC2.
        /// </param>
        /// <returns>
        ///  One of #RectanglesIntersectTypes
        /// </returns>
        public static int rotatedRectangleIntersection(in Vec5d rect1, in Vec5d rect2, Mat intersectingRegion)
        {
            if (intersectingRegion != null) intersectingRegion.ThrowIfDisposed();

            return imgproc_Imgproc_rotatedRectangleIntersection_10(rect1.Item1, rect1.Item2, rect1.Item3, rect1.Item4, rect1.Item5, rect2.Item1, rect2.Item2, rect2.Item3, rect2.Item4, rect2.Item5, intersectingRegion.nativeObj);


        }


        //
        // C++:  void cv::line(Mat& img, Point pt1, Point pt2, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Draws a line segment connecting two points.
        /// </summary>
        /// <remarks>
        ///  The function line draws the line segment between pt1 and pt2 points in the image. The line is
        ///  clipped by the image boundaries. For non-antialiased lines with integer coordinates, the 8-connected
        ///  or 4-connected Bresenham algorithm is used. Thick lines are drawn with rounding endings. Antialiased
        ///  lines are drawn using Gaussian filtering.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// First point of the line segment.
        /// </param>
        /// <param name="pt2">
        /// Second point of the line segment.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes.
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void line(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_line_10(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <summary>
        ///  Draws a line segment connecting two points.
        /// </summary>
        /// <remarks>
        ///  The function line draws the line segment between pt1 and pt2 points in the image. The line is
        ///  clipped by the image boundaries. For non-antialiased lines with integer coordinates, the 8-connected
        ///  or 4-connected Bresenham algorithm is used. Thick lines are drawn with rounding endings. Antialiased
        ///  lines are drawn using Gaussian filtering.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// First point of the line segment.
        /// </param>
        /// <param name="pt2">
        /// Second point of the line segment.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes.
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void line(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_line_11(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws a line segment connecting two points.
        /// </summary>
        /// <remarks>
        ///  The function line draws the line segment between pt1 and pt2 points in the image. The line is
        ///  clipped by the image boundaries. For non-antialiased lines with integer coordinates, the 8-connected
        ///  or 4-connected Bresenham algorithm is used. Thick lines are drawn with rounding endings. Antialiased
        ///  lines are drawn using Gaussian filtering.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// First point of the line segment.
        /// </param>
        /// <param name="pt2">
        /// Second point of the line segment.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes.
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void line(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_line_12(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws a line segment connecting two points.
        /// </summary>
        /// <remarks>
        ///  The function line draws the line segment between pt1 and pt2 points in the image. The line is
        ///  clipped by the image boundaries. For non-antialiased lines with integer coordinates, the 8-connected
        ///  or 4-connected Bresenham algorithm is used. Thick lines are drawn with rounding endings. Antialiased
        ///  lines are drawn using Gaussian filtering.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// First point of the line segment.
        /// </param>
        /// <param name="pt2">
        /// Second point of the line segment.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes.
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void line(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_line_13(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::arrowedLine(Mat& img, Point pt1, Point pt2, Scalar color, int thickness = 1, int line_type = 8, int shift = 0, double tipLength = 0.1)
        //

        /// <summary>
        ///  Draws an arrow segment pointing from the first point to the second one.
        /// </summary>
        /// <remarks>
        ///  The function cv::arrowedLine draws an arrow between pt1 and pt2 points in the image. See also #line.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// The point the arrow starts from.
        /// </param>
        /// <param name="pt2">
        /// The point the arrow points to.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        /// <param name="tipLength">
        /// The length of the arrow tip in relation to the arrow length
        /// </param>
        public static void arrowedLine(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int line_type, int shift, double tipLength)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_arrowedLine_10(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, line_type, shift, tipLength);


        }

        /// <summary>
        ///  Draws an arrow segment pointing from the first point to the second one.
        /// </summary>
        /// <remarks>
        ///  The function cv::arrowedLine draws an arrow between pt1 and pt2 points in the image. See also #line.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// The point the arrow starts from.
        /// </param>
        /// <param name="pt2">
        /// The point the arrow points to.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        /// <param name="tipLength">
        /// The length of the arrow tip in relation to the arrow length
        /// </param>
        public static void arrowedLine(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int line_type, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_arrowedLine_11(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, line_type, shift);


        }

        /// <summary>
        ///  Draws an arrow segment pointing from the first point to the second one.
        /// </summary>
        /// <remarks>
        ///  The function cv::arrowedLine draws an arrow between pt1 and pt2 points in the image. See also #line.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// The point the arrow starts from.
        /// </param>
        /// <param name="pt2">
        /// The point the arrow points to.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        /// <param name="tipLength">
        /// The length of the arrow tip in relation to the arrow length
        /// </param>
        public static void arrowedLine(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int line_type)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_arrowedLine_12(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, line_type);


        }

        /// <summary>
        ///  Draws an arrow segment pointing from the first point to the second one.
        /// </summary>
        /// <remarks>
        ///  The function cv::arrowedLine draws an arrow between pt1 and pt2 points in the image. See also #line.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// The point the arrow starts from.
        /// </param>
        /// <param name="pt2">
        /// The point the arrow points to.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        /// <param name="tipLength">
        /// The length of the arrow tip in relation to the arrow length
        /// </param>
        public static void arrowedLine(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_arrowedLine_13(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws an arrow segment pointing from the first point to the second one.
        /// </summary>
        /// <remarks>
        ///  The function cv::arrowedLine draws an arrow between pt1 and pt2 points in the image. See also #line.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// The point the arrow starts from.
        /// </param>
        /// <param name="pt2">
        /// The point the arrow points to.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        /// <param name="tipLength">
        /// The length of the arrow tip in relation to the arrow length
        /// </param>
        public static void arrowedLine(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_arrowedLine_14(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::rectangle(Mat& img, Point pt1, Point pt2, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Draws a simple, thick, or filled up-right rectangle.
        /// </summary>
        /// <remarks>
        ///  The function cv::rectangle draws a rectangle outline or a filled rectangle whose two opposite corners
        ///  are pt1 and pt2.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// Vertex of the rectangle.
        /// </param>
        /// <param name="pt2">
        /// Vertex of the rectangle opposite to pt1 .
        /// </param>
        /// <param name="color">
        /// Rectangle color or brightness (grayscale image).
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines that make up the rectangle. Negative values, like #FILLED,
        ///  mean that the function has to draw a filled rectangle.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void rectangle(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_10(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <summary>
        ///  Draws a simple, thick, or filled up-right rectangle.
        /// </summary>
        /// <remarks>
        ///  The function cv::rectangle draws a rectangle outline or a filled rectangle whose two opposite corners
        ///  are pt1 and pt2.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// Vertex of the rectangle.
        /// </param>
        /// <param name="pt2">
        /// Vertex of the rectangle opposite to pt1 .
        /// </param>
        /// <param name="color">
        /// Rectangle color or brightness (grayscale image).
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines that make up the rectangle. Negative values, like #FILLED,
        ///  mean that the function has to draw a filled rectangle.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void rectangle(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_11(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws a simple, thick, or filled up-right rectangle.
        /// </summary>
        /// <remarks>
        ///  The function cv::rectangle draws a rectangle outline or a filled rectangle whose two opposite corners
        ///  are pt1 and pt2.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// Vertex of the rectangle.
        /// </param>
        /// <param name="pt2">
        /// Vertex of the rectangle opposite to pt1 .
        /// </param>
        /// <param name="color">
        /// Rectangle color or brightness (grayscale image).
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines that make up the rectangle. Negative values, like #FILLED,
        ///  mean that the function has to draw a filled rectangle.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void rectangle(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_12(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws a simple, thick, or filled up-right rectangle.
        /// </summary>
        /// <remarks>
        ///  The function cv::rectangle draws a rectangle outline or a filled rectangle whose two opposite corners
        ///  are pt1 and pt2.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pt1">
        /// Vertex of the rectangle.
        /// </param>
        /// <param name="pt2">
        /// Vertex of the rectangle opposite to pt1 .
        /// </param>
        /// <param name="color">
        /// Rectangle color or brightness (grayscale image).
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines that make up the rectangle. Negative values, like #FILLED,
        ///  mean that the function has to draw a filled rectangle.
        /// </param>
        /// <param name="lineType">
        /// Type of the line. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the point coordinates.
        /// </param>
        public static void rectangle(Mat img, in Vec2d pt1, in Vec2d pt2, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_13(img.nativeObj, pt1.Item1, pt1.Item2, pt2.Item1, pt2.Item2, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::rectangle(Mat& img, Rect rec, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <remarks>
        ///  @overload
        ///  
        ///  use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
        ///  r.br()-Point(1,1)` are opposite corners
        /// </remarks>
        public static void rectangle(Mat img, in Vec4i rec, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_14(img.nativeObj, rec.Item1, rec.Item2, rec.Item3, rec.Item4, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <remarks>
        ///  @overload
        ///  
        ///  use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
        ///  r.br()-Point(1,1)` are opposite corners
        /// </remarks>
        public static void rectangle(Mat img, in Vec4i rec, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_15(img.nativeObj, rec.Item1, rec.Item2, rec.Item3, rec.Item4, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <remarks>
        ///  @overload
        ///  
        ///  use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
        ///  r.br()-Point(1,1)` are opposite corners
        /// </remarks>
        public static void rectangle(Mat img, in Vec4i rec, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_16(img.nativeObj, rec.Item1, rec.Item2, rec.Item3, rec.Item4, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <remarks>
        ///  @overload
        ///  
        ///  use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
        ///  r.br()-Point(1,1)` are opposite corners
        /// </remarks>
        public static void rectangle(Mat img, in Vec4i rec, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_rectangle_17(img.nativeObj, rec.Item1, rec.Item2, rec.Item3, rec.Item4, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::circle(Mat& img, Point center, int radius, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Draws a circle.
        /// </summary>
        /// <remarks>
        ///  The function cv::circle draws a simple or filled circle with a given center and radius.
        /// </remarks>
        /// <param name="img">
        /// Image where the circle is drawn.
        /// </param>
        /// <param name="center">
        /// Center of the circle.
        /// </param>
        /// <param name="radius">
        /// Radius of the circle.
        /// </param>
        /// <param name="color">
        /// Circle color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the circle outline, if positive. Negative values, like #FILLED,
        ///  mean that a filled circle is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the circle boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and in the radius value.
        /// </param>
        public static void circle(Mat img, in Vec2d center, int radius, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_circle_10(img.nativeObj, center.Item1, center.Item2, radius, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <summary>
        ///  Draws a circle.
        /// </summary>
        /// <remarks>
        ///  The function cv::circle draws a simple or filled circle with a given center and radius.
        /// </remarks>
        /// <param name="img">
        /// Image where the circle is drawn.
        /// </param>
        /// <param name="center">
        /// Center of the circle.
        /// </param>
        /// <param name="radius">
        /// Radius of the circle.
        /// </param>
        /// <param name="color">
        /// Circle color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the circle outline, if positive. Negative values, like #FILLED,
        ///  mean that a filled circle is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the circle boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and in the radius value.
        /// </param>
        public static void circle(Mat img, in Vec2d center, int radius, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_circle_11(img.nativeObj, center.Item1, center.Item2, radius, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws a circle.
        /// </summary>
        /// <remarks>
        ///  The function cv::circle draws a simple or filled circle with a given center and radius.
        /// </remarks>
        /// <param name="img">
        /// Image where the circle is drawn.
        /// </param>
        /// <param name="center">
        /// Center of the circle.
        /// </param>
        /// <param name="radius">
        /// Radius of the circle.
        /// </param>
        /// <param name="color">
        /// Circle color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the circle outline, if positive. Negative values, like #FILLED,
        ///  mean that a filled circle is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the circle boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and in the radius value.
        /// </param>
        public static void circle(Mat img, in Vec2d center, int radius, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_circle_12(img.nativeObj, center.Item1, center.Item2, radius, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws a circle.
        /// </summary>
        /// <remarks>
        ///  The function cv::circle draws a simple or filled circle with a given center and radius.
        /// </remarks>
        /// <param name="img">
        /// Image where the circle is drawn.
        /// </param>
        /// <param name="center">
        /// Center of the circle.
        /// </param>
        /// <param name="radius">
        /// Radius of the circle.
        /// </param>
        /// <param name="color">
        /// Circle color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the circle outline, if positive. Negative values, like #FILLED,
        ///  mean that a filled circle is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the circle boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and in the radius value.
        /// </param>
        public static void circle(Mat img, in Vec2d center, int radius, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_circle_13(img.nativeObj, center.Item1, center.Item2, radius, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::ellipse(Mat& img, Point center, Size axes, double angle, double startAngle, double endAngle, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Draws a simple or thick elliptic arc or fills an ellipse sector.
        /// </summary>
        /// <remarks>
        ///  The function cv::ellipse with more parameters draws an ellipse outline, a filled ellipse, an elliptic
        ///  arc, or a filled ellipse sector. The drawing code uses general parametric form.
        ///  A piecewise-linear curve is used to approximate the elliptic arc
        ///  boundary. If you need more control of the ellipse rendering, you can retrieve the curve using
        ///  #ellipse2Poly and then render it with #polylines or fill it with #fillPoly. If you use the first
        ///  variant of the function and want to draw the whole ellipse, not an arc, pass `startAngle=0` and
        ///  `endAngle=360`. If `startAngle` is greater than `endAngle`, they are swapped. The figure below explains
        ///  the meaning of the parameters to draw the blue arc.
        ///  
        ///  ![Parameters of Elliptic Arc](pics/ellipse.svg)
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="center">
        /// Center of the ellipse.
        /// </param>
        /// <param name="axes">
        /// Half of the size of the ellipse main axes.
        /// </param>
        /// <param name="angle">
        /// Ellipse rotation angle in degrees.
        /// </param>
        /// <param name="startAngle">
        /// Starting angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="endAngle">
        /// Ending angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and values of axes.
        /// </param>
        public static void ellipse(Mat img, in Vec2d center, in Vec2d axes, double angle, double startAngle, double endAngle, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_10(img.nativeObj, center.Item1, center.Item2, axes.Item1, axes.Item2, angle, startAngle, endAngle, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <summary>
        ///  Draws a simple or thick elliptic arc or fills an ellipse sector.
        /// </summary>
        /// <remarks>
        ///  The function cv::ellipse with more parameters draws an ellipse outline, a filled ellipse, an elliptic
        ///  arc, or a filled ellipse sector. The drawing code uses general parametric form.
        ///  A piecewise-linear curve is used to approximate the elliptic arc
        ///  boundary. If you need more control of the ellipse rendering, you can retrieve the curve using
        ///  #ellipse2Poly and then render it with #polylines or fill it with #fillPoly. If you use the first
        ///  variant of the function and want to draw the whole ellipse, not an arc, pass `startAngle=0` and
        ///  `endAngle=360`. If `startAngle` is greater than `endAngle`, they are swapped. The figure below explains
        ///  the meaning of the parameters to draw the blue arc.
        ///  
        ///  ![Parameters of Elliptic Arc](pics/ellipse.svg)
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="center">
        /// Center of the ellipse.
        /// </param>
        /// <param name="axes">
        /// Half of the size of the ellipse main axes.
        /// </param>
        /// <param name="angle">
        /// Ellipse rotation angle in degrees.
        /// </param>
        /// <param name="startAngle">
        /// Starting angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="endAngle">
        /// Ending angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and values of axes.
        /// </param>
        public static void ellipse(Mat img, in Vec2d center, in Vec2d axes, double angle, double startAngle, double endAngle, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_11(img.nativeObj, center.Item1, center.Item2, axes.Item1, axes.Item2, angle, startAngle, endAngle, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws a simple or thick elliptic arc or fills an ellipse sector.
        /// </summary>
        /// <remarks>
        ///  The function cv::ellipse with more parameters draws an ellipse outline, a filled ellipse, an elliptic
        ///  arc, or a filled ellipse sector. The drawing code uses general parametric form.
        ///  A piecewise-linear curve is used to approximate the elliptic arc
        ///  boundary. If you need more control of the ellipse rendering, you can retrieve the curve using
        ///  #ellipse2Poly and then render it with #polylines or fill it with #fillPoly. If you use the first
        ///  variant of the function and want to draw the whole ellipse, not an arc, pass `startAngle=0` and
        ///  `endAngle=360`. If `startAngle` is greater than `endAngle`, they are swapped. The figure below explains
        ///  the meaning of the parameters to draw the blue arc.
        ///  
        ///  ![Parameters of Elliptic Arc](pics/ellipse.svg)
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="center">
        /// Center of the ellipse.
        /// </param>
        /// <param name="axes">
        /// Half of the size of the ellipse main axes.
        /// </param>
        /// <param name="angle">
        /// Ellipse rotation angle in degrees.
        /// </param>
        /// <param name="startAngle">
        /// Starting angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="endAngle">
        /// Ending angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and values of axes.
        /// </param>
        public static void ellipse(Mat img, in Vec2d center, in Vec2d axes, double angle, double startAngle, double endAngle, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_12(img.nativeObj, center.Item1, center.Item2, axes.Item1, axes.Item2, angle, startAngle, endAngle, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws a simple or thick elliptic arc or fills an ellipse sector.
        /// </summary>
        /// <remarks>
        ///  The function cv::ellipse with more parameters draws an ellipse outline, a filled ellipse, an elliptic
        ///  arc, or a filled ellipse sector. The drawing code uses general parametric form.
        ///  A piecewise-linear curve is used to approximate the elliptic arc
        ///  boundary. If you need more control of the ellipse rendering, you can retrieve the curve using
        ///  #ellipse2Poly and then render it with #polylines or fill it with #fillPoly. If you use the first
        ///  variant of the function and want to draw the whole ellipse, not an arc, pass `startAngle=0` and
        ///  `endAngle=360`. If `startAngle` is greater than `endAngle`, they are swapped. The figure below explains
        ///  the meaning of the parameters to draw the blue arc.
        ///  
        ///  ![Parameters of Elliptic Arc](pics/ellipse.svg)
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="center">
        /// Center of the ellipse.
        /// </param>
        /// <param name="axes">
        /// Half of the size of the ellipse main axes.
        /// </param>
        /// <param name="angle">
        /// Ellipse rotation angle in degrees.
        /// </param>
        /// <param name="startAngle">
        /// Starting angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="endAngle">
        /// Ending angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the coordinates of the center and values of axes.
        /// </param>
        public static void ellipse(Mat img, in Vec2d center, in Vec2d axes, double angle, double startAngle, double endAngle, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_13(img.nativeObj, center.Item1, center.Item2, axes.Item1, axes.Item2, angle, startAngle, endAngle, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::ellipse(Mat& img, RotatedRect box, Scalar color, int thickness = 1, int lineType = LINE_8)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="box">
        /// Alternative ellipse representation via RotatedRect. This means that the function draws
        ///  an ellipse inscribed in the rotated rectangle.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        public static void ellipse(Mat img, in Vec5d box, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_14(img.nativeObj, box.Item1, box.Item2, box.Item3, box.Item4, box.Item5, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="box">
        /// Alternative ellipse representation via RotatedRect. This means that the function draws
        ///  an ellipse inscribed in the rotated rectangle.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        public static void ellipse(Mat img, in Vec5d box, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_15(img.nativeObj, box.Item1, box.Item2, box.Item3, box.Item4, box.Item5, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="box">
        /// Alternative ellipse representation via RotatedRect. This means that the function draws
        ///  an ellipse inscribed in the rotated rectangle.
        /// </param>
        /// <param name="color">
        /// Ellipse color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that
        ///  a filled ellipse sector is to be drawn.
        /// </param>
        /// <param name="lineType">
        /// Type of the ellipse boundary. See #LineTypes
        /// </param>
        public static void ellipse(Mat img, in Vec5d box, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_ellipse_16(img.nativeObj, box.Item1, box.Item2, box.Item3, box.Item4, box.Item5, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::drawMarker(Mat& img, Point position, Scalar color, int markerType = MARKER_CROSS, int markerSize = 20, int thickness = 1, int line_type = 8)
        //

        /// <summary>
        ///  Draws a marker on a predefined position in an image.
        /// </summary>
        /// <remarks>
        ///  The function cv::drawMarker draws a marker on a given position in the image. For the moment several
        ///  marker types are supported, see #MarkerTypes for more information.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="position">
        /// The point where the crosshair is positioned.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="markerType">
        /// The specific type of marker you want to use, see #MarkerTypes
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line, See #LineTypes
        /// </param>
        /// <param name="markerSize">
        /// The length of the marker axis [default = 20 pixels]
        /// </param>
        public static void drawMarker(Mat img, in Vec2d position, in Vec4d color, int markerType, int markerSize, int thickness, int line_type)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_drawMarker_10(img.nativeObj, position.Item1, position.Item2, color.Item1, color.Item2, color.Item3, color.Item4, markerType, markerSize, thickness, line_type);


        }

        /// <summary>
        ///  Draws a marker on a predefined position in an image.
        /// </summary>
        /// <remarks>
        ///  The function cv::drawMarker draws a marker on a given position in the image. For the moment several
        ///  marker types are supported, see #MarkerTypes for more information.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="position">
        /// The point where the crosshair is positioned.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="markerType">
        /// The specific type of marker you want to use, see #MarkerTypes
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line, See #LineTypes
        /// </param>
        /// <param name="markerSize">
        /// The length of the marker axis [default = 20 pixels]
        /// </param>
        public static void drawMarker(Mat img, in Vec2d position, in Vec4d color, int markerType, int markerSize, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_drawMarker_11(img.nativeObj, position.Item1, position.Item2, color.Item1, color.Item2, color.Item3, color.Item4, markerType, markerSize, thickness);


        }

        /// <summary>
        ///  Draws a marker on a predefined position in an image.
        /// </summary>
        /// <remarks>
        ///  The function cv::drawMarker draws a marker on a given position in the image. For the moment several
        ///  marker types are supported, see #MarkerTypes for more information.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="position">
        /// The point where the crosshair is positioned.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="markerType">
        /// The specific type of marker you want to use, see #MarkerTypes
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line, See #LineTypes
        /// </param>
        /// <param name="markerSize">
        /// The length of the marker axis [default = 20 pixels]
        /// </param>
        public static void drawMarker(Mat img, in Vec2d position, in Vec4d color, int markerType, int markerSize)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_drawMarker_12(img.nativeObj, position.Item1, position.Item2, color.Item1, color.Item2, color.Item3, color.Item4, markerType, markerSize);


        }

        /// <summary>
        ///  Draws a marker on a predefined position in an image.
        /// </summary>
        /// <remarks>
        ///  The function cv::drawMarker draws a marker on a given position in the image. For the moment several
        ///  marker types are supported, see #MarkerTypes for more information.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="position">
        /// The point where the crosshair is positioned.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="markerType">
        /// The specific type of marker you want to use, see #MarkerTypes
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line, See #LineTypes
        /// </param>
        /// <param name="markerSize">
        /// The length of the marker axis [default = 20 pixels]
        /// </param>
        public static void drawMarker(Mat img, in Vec2d position, in Vec4d color, int markerType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_drawMarker_13(img.nativeObj, position.Item1, position.Item2, color.Item1, color.Item2, color.Item3, color.Item4, markerType);


        }

        /// <summary>
        ///  Draws a marker on a predefined position in an image.
        /// </summary>
        /// <remarks>
        ///  The function cv::drawMarker draws a marker on a given position in the image. For the moment several
        ///  marker types are supported, see #MarkerTypes for more information.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="position">
        /// The point where the crosshair is positioned.
        /// </param>
        /// <param name="color">
        /// Line color.
        /// </param>
        /// <param name="markerType">
        /// The specific type of marker you want to use, see #MarkerTypes
        /// </param>
        /// <param name="thickness">
        /// Line thickness.
        /// </param>
        /// <param name="line_type">
        /// Type of the line, See #LineTypes
        /// </param>
        /// <param name="markerSize">
        /// The length of the marker axis [default = 20 pixels]
        /// </param>
        public static void drawMarker(Mat img, in Vec2d position, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_drawMarker_14(img.nativeObj, position.Item1, position.Item2, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::fillConvexPoly(Mat& img, vector_Point points, Scalar color, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Fills a convex polygon.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillConvexPoly draws a filled convex polygon. This function is much faster than the
        ///  function #fillPoly . It can fill not only convex polygons but any monotonic polygon without
        ///  self-intersections, that is, a polygon whose contour intersects every horizontal line (scan line)
        ///  twice at the most (though, its top-most and/or the bottom edge could be horizontal).
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="points">
        /// Polygon vertices.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        public static void fillConvexPoly(Mat img, MatOfPoint points, in Vec4d color, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            imgproc_Imgproc_fillConvexPoly_10(img.nativeObj, points_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, lineType, shift);


        }

        /// <summary>
        ///  Fills a convex polygon.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillConvexPoly draws a filled convex polygon. This function is much faster than the
        ///  function #fillPoly . It can fill not only convex polygons but any monotonic polygon without
        ///  self-intersections, that is, a polygon whose contour intersects every horizontal line (scan line)
        ///  twice at the most (though, its top-most and/or the bottom edge could be horizontal).
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="points">
        /// Polygon vertices.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        public static void fillConvexPoly(Mat img, MatOfPoint points, in Vec4d color, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            imgproc_Imgproc_fillConvexPoly_11(img.nativeObj, points_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, lineType);


        }

        /// <summary>
        ///  Fills a convex polygon.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillConvexPoly draws a filled convex polygon. This function is much faster than the
        ///  function #fillPoly . It can fill not only convex polygons but any monotonic polygon without
        ///  self-intersections, that is, a polygon whose contour intersects every horizontal line (scan line)
        ///  twice at the most (though, its top-most and/or the bottom edge could be horizontal).
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="points">
        /// Polygon vertices.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        public static void fillConvexPoly(Mat img, MatOfPoint points, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            Mat points_mat = points;
            imgproc_Imgproc_fillConvexPoly_12(img.nativeObj, points_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::fillPoly(Mat& img, vector_vector_Point pts, Scalar color, int lineType = LINE_8, int shift = 0, Point offset = Point())
        //

        /// <summary>
        ///  Fills the area bounded by one or more polygons.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillPoly fills an area bounded by several polygonal contours. The function can fill
        ///  complex areas, for example, areas with holes, contours with self-intersections (some of their
        ///  parts), and so forth.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygons where each polygon is represented as an array of points.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <param name="offset">
        /// Optional offset of all points of the contours.
        /// </param>
        public static void fillPoly(Mat img, List<MatOfPoint> pts, in Vec4d color, int lineType, int shift, in Vec2d offset)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_fillPoly_10(img.nativeObj, pts_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, lineType, shift, offset.Item1, offset.Item2);


        }

        /// <summary>
        ///  Fills the area bounded by one or more polygons.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillPoly fills an area bounded by several polygonal contours. The function can fill
        ///  complex areas, for example, areas with holes, contours with self-intersections (some of their
        ///  parts), and so forth.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygons where each polygon is represented as an array of points.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <param name="offset">
        /// Optional offset of all points of the contours.
        /// </param>
        public static void fillPoly(Mat img, List<MatOfPoint> pts, in Vec4d color, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_fillPoly_11(img.nativeObj, pts_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, lineType, shift);


        }

        /// <summary>
        ///  Fills the area bounded by one or more polygons.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillPoly fills an area bounded by several polygonal contours. The function can fill
        ///  complex areas, for example, areas with holes, contours with self-intersections (some of their
        ///  parts), and so forth.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygons where each polygon is represented as an array of points.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <param name="offset">
        /// Optional offset of all points of the contours.
        /// </param>
        public static void fillPoly(Mat img, List<MatOfPoint> pts, in Vec4d color, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_fillPoly_12(img.nativeObj, pts_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, lineType);


        }

        /// <summary>
        ///  Fills the area bounded by one or more polygons.
        /// </summary>
        /// <remarks>
        ///  The function cv::fillPoly fills an area bounded by several polygonal contours. The function can fill
        ///  complex areas, for example, areas with holes, contours with self-intersections (some of their
        ///  parts), and so forth.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygons where each polygon is represented as an array of points.
        /// </param>
        /// <param name="color">
        /// Polygon color.
        /// </param>
        /// <param name="lineType">
        /// Type of the polygon boundaries. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <param name="offset">
        /// Optional offset of all points of the contours.
        /// </param>
        public static void fillPoly(Mat img, List<MatOfPoint> pts, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_fillPoly_13(img.nativeObj, pts_mat.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::polylines(Mat& img, vector_vector_Point pts, bool isClosed, Scalar color, int thickness = 1, int lineType = LINE_8, int shift = 0)
        //

        /// <summary>
        ///  Draws several polygonal curves.
        /// </summary>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygonal curves.
        /// </param>
        /// <param name="isClosed">
        /// Flag indicating whether the drawn polylines are closed or not. If they are closed,
        ///  the function draws a line from the last vertex of each curve to its first vertex.
        /// </param>
        /// <param name="color">
        /// Polyline color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the polyline edges.
        /// </param>
        /// <param name="lineType">
        /// Type of the line segments. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <remarks>
        ///  The function cv::polylines draws one or more polygonal curves.
        /// </remarks>
        public static void polylines(Mat img, List<MatOfPoint> pts, bool isClosed, in Vec4d color, int thickness, int lineType, int shift)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_polylines_10(img.nativeObj, pts_mat.nativeObj, isClosed, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, shift);


        }

        /// <summary>
        ///  Draws several polygonal curves.
        /// </summary>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygonal curves.
        /// </param>
        /// <param name="isClosed">
        /// Flag indicating whether the drawn polylines are closed or not. If they are closed,
        ///  the function draws a line from the last vertex of each curve to its first vertex.
        /// </param>
        /// <param name="color">
        /// Polyline color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the polyline edges.
        /// </param>
        /// <param name="lineType">
        /// Type of the line segments. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <remarks>
        ///  The function cv::polylines draws one or more polygonal curves.
        /// </remarks>
        public static void polylines(Mat img, List<MatOfPoint> pts, bool isClosed, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_polylines_11(img.nativeObj, pts_mat.nativeObj, isClosed, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws several polygonal curves.
        /// </summary>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygonal curves.
        /// </param>
        /// <param name="isClosed">
        /// Flag indicating whether the drawn polylines are closed or not. If they are closed,
        ///  the function draws a line from the last vertex of each curve to its first vertex.
        /// </param>
        /// <param name="color">
        /// Polyline color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the polyline edges.
        /// </param>
        /// <param name="lineType">
        /// Type of the line segments. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <remarks>
        ///  The function cv::polylines draws one or more polygonal curves.
        /// </remarks>
        public static void polylines(Mat img, List<MatOfPoint> pts, bool isClosed, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_polylines_12(img.nativeObj, pts_mat.nativeObj, isClosed, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws several polygonal curves.
        /// </summary>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="pts">
        /// Array of polygonal curves.
        /// </param>
        /// <param name="isClosed">
        /// Flag indicating whether the drawn polylines are closed or not. If they are closed,
        ///  the function draws a line from the last vertex of each curve to its first vertex.
        /// </param>
        /// <param name="color">
        /// Polyline color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the polyline edges.
        /// </param>
        /// <param name="lineType">
        /// Type of the line segments. See #LineTypes
        /// </param>
        /// <param name="shift">
        /// Number of fractional bits in the vertex coordinates.
        /// </param>
        /// <remarks>
        ///  The function cv::polylines draws one or more polygonal curves.
        /// </remarks>
        public static void polylines(Mat img, List<MatOfPoint> pts, bool isClosed, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pts_mat = Converters.vector_vector_Point_to_Mat(pts);
            imgproc_Imgproc_polylines_13(img.nativeObj, pts_mat.nativeObj, isClosed, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::drawContours(Mat& image, vector_vector_Point contours, int contourIdx, Scalar color, int thickness = 1, int lineType = LINE_8, Mat hierarchy = Mat(), int maxLevel = INT_MAX, Point offset = Point())
        //

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color, int thickness, int lineType, Mat hierarchy, int maxLevel, in Vec2d offset)
        {
            if (image != null) image.ThrowIfDisposed();
            if (hierarchy != null) hierarchy.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_10(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, hierarchy.nativeObj, maxLevel, offset.Item1, offset.Item2);


        }

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color, int thickness, int lineType, Mat hierarchy, int maxLevel)
        {
            if (image != null) image.ThrowIfDisposed();
            if (hierarchy != null) hierarchy.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_11(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, hierarchy.nativeObj, maxLevel);


        }

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color, int thickness, int lineType, Mat hierarchy)
        {
            if (image != null) image.ThrowIfDisposed();
            if (hierarchy != null) hierarchy.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_12(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, hierarchy.nativeObj);


        }

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color, int thickness, int lineType)
        {
            if (image != null) image.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_13(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color, int thickness)
        {
            if (image != null) image.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_14(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws contours outlines or filled contours.
        /// </summary>
        /// <remarks>
        ///  The function draws contour outlines in the image if \f$\texttt{thickness} \ge 0\f$ or fills the area
        ///  bounded by the contours if \f$\texttt{thickness}<0\f$ . The example below shows how to retrieve
        ///  connected components from the binary image and label them: :
        ///  @include snippets/imgproc_drawContours.cpp
        /// </remarks>
        /// <param name="image">
        /// Destination image.
        /// </param>
        /// <param name="contours">
        /// All the input contours. Each contour is stored as a point vector.
        /// </param>
        /// <param name="contourIdx">
        /// Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
        /// </param>
        /// <param name="color">
        /// Color of the contours.
        /// </param>
        /// <param name="thickness">
        /// Thickness of lines the contours are drawn with. If it is negative (for example,
        ///  thickness=#FILLED ), the contour interiors are drawn.
        /// </param>
        /// <param name="lineType">
        /// Line connectivity. See #LineTypes
        /// </param>
        /// <param name="hierarchy">
        /// Optional information about hierarchy. It is only needed if you want to draw only
        ///  some of the contours (see maxLevel ).
        /// </param>
        /// <param name="maxLevel">
        /// Maximal level for drawn contours. If it is 0, only the specified contour is drawn.
        ///  If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function
        ///  draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This
        ///  parameter is only taken into account when there is hierarchy available.
        /// </param>
        /// <param name="offset">
        /// Optional contour shift parameter. Shift all the drawn contours by the specified
        ///  \f$\texttt{offset}=(dx,dy)\f$ .
        ///  @note When thickness=#FILLED, the function is designed to handle connected components with holes correctly
        ///  even when no hierarchy data is provided. This is done by analyzing all the outlines together
        ///  using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
        ///  contours. In order to solve this problem, you need to call #drawContours separately for each sub-group
        ///  of contours, or iterate over the collection using contourIdx parameter.
        /// </param>
        public static void drawContours(Mat image, List<MatOfPoint> contours, int contourIdx, in Vec4d color)
        {
            if (image != null) image.ThrowIfDisposed();
            using Mat contours_mat = Converters.vector_vector_Point_to_Mat(contours);
            imgproc_Imgproc_drawContours_15(image.nativeObj, contours_mat.nativeObj, contourIdx, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  bool cv::clipLine(Rect imgRect, Point& pt1, Point& pt2)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="imgRect">
        /// Image rectangle.
        /// </param>
        /// <param name="pt1">
        /// First line point.
        /// </param>
        /// <param name="pt2">
        /// Second line point.
        /// </param>
        public static bool clipLine(in Vec4i imgRect, ref Vec2d pt1, ref Vec2d pt2)
        {

            double[] pt1_out = new double[2];
            double[] pt2_out = new double[2];
            bool retVal = imgproc_Imgproc_clipLine_10(imgRect.Item1, imgRect.Item2, imgRect.Item3, imgRect.Item4, pt1.Item1, pt1.Item2, pt1_out, pt2.Item1, pt2.Item2, pt2_out);
            { pt1.Item1 = pt1_out[0]; pt1.Item2 = pt1_out[1]; }
            { pt2.Item1 = pt2_out[0]; pt2.Item2 = pt2_out[1]; }
            return retVal;
        }


        //
        // C++:  void cv::ellipse2Poly(Point center, Size axes, int angle, int arcStart, int arcEnd, int delta, vector_Point& pts)
        //

        /// <summary>
        ///  Approximates an elliptic arc with a polyline.
        /// </summary>
        /// <remarks>
        ///  The function ellipse2Poly computes the vertices of a polyline that approximates the specified
        ///  elliptic arc. It is used by #ellipse. If `arcStart` is greater than `arcEnd`, they are swapped.
        /// </remarks>
        /// <param name="center">
        /// Center of the arc.
        /// </param>
        /// <param name="axes">
        /// Half of the size of the ellipse main axes. See #ellipse for details.
        /// </param>
        /// <param name="angle">
        /// Rotation angle of the ellipse in degrees. See #ellipse for details.
        /// </param>
        /// <param name="arcStart">
        /// Starting angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="arcEnd">
        /// Ending angle of the elliptic arc in degrees.
        /// </param>
        /// <param name="delta">
        /// Angle between the subsequent polyline vertices. It defines the approximation
        ///  accuracy.
        /// </param>
        /// <param name="pts">
        /// Output vector of polyline vertices.
        /// </param>
        public static void ellipse2Poly(in Vec2d center, in Vec2d axes, int angle, int arcStart, int arcEnd, int delta, MatOfPoint pts)
        {
            if (pts != null) pts.ThrowIfDisposed();
            Mat pts_mat = pts;
            imgproc_Imgproc_ellipse2Poly_10(center.Item1, center.Item2, axes.Item1, axes.Item2, angle, arcStart, arcEnd, delta, pts_mat.nativeObj);


        }


        //
        // C++:  void cv::putText(Mat& img, String text, Point org, int fontFace, double fontScale, Scalar color, int thickness = 1, int lineType = LINE_8, bool bottomLeftOrigin = false)
        //

        /// <summary>
        ///  Draws a text string.
        /// </summary>
        /// <remarks>
        ///  The function cv::putText renders the specified text string in the image. Symbols that cannot be rendered
        ///  using the specified font are replaced by question marks. See #getTextSize for a text rendering code
        ///  example.
        ///  
        ///  The `fontScale` parameter is a scale factor that is multiplied by the base font size:
        ///  - When scale &gt; 1, the text is magnified.
        ///  - When 0 &lt; scale &lt; 1, the text is minimized.
        ///  - When scale &lt; 0, the text is mirrored or reversed.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="text">
        /// Text string to be drawn.
        /// </param>
        /// <param name="org">
        /// Bottom-left corner of the text string in the image.
        /// </param>
        /// <param name="fontFace">
        /// Font type, see #HersheyFonts.
        /// </param>
        /// <param name="fontScale">
        /// Font scale factor that is multiplied by the font-specific base size.
        /// </param>
        /// <param name="color">
        /// Text color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the lines used to draw a text.
        /// </param>
        /// <param name="lineType">
        /// Line type. See #LineTypes
        /// </param>
        /// <param name="bottomLeftOrigin">
        /// When true, the image data origin is at the bottom-left corner. Otherwise,
        ///  it is at the top-left corner.
        /// </param>
        public static void putText(Mat img, string text, in Vec2d org, int fontFace, double fontScale, in Vec4d color, int thickness, int lineType, bool bottomLeftOrigin)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_putText_10(img.nativeObj, text, org.Item1, org.Item2, fontFace, fontScale, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType, bottomLeftOrigin);


        }

        /// <summary>
        ///  Draws a text string.
        /// </summary>
        /// <remarks>
        ///  The function cv::putText renders the specified text string in the image. Symbols that cannot be rendered
        ///  using the specified font are replaced by question marks. See #getTextSize for a text rendering code
        ///  example.
        ///  
        ///  The `fontScale` parameter is a scale factor that is multiplied by the base font size:
        ///  - When scale &gt; 1, the text is magnified.
        ///  - When 0 &lt; scale &lt; 1, the text is minimized.
        ///  - When scale &lt; 0, the text is mirrored or reversed.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="text">
        /// Text string to be drawn.
        /// </param>
        /// <param name="org">
        /// Bottom-left corner of the text string in the image.
        /// </param>
        /// <param name="fontFace">
        /// Font type, see #HersheyFonts.
        /// </param>
        /// <param name="fontScale">
        /// Font scale factor that is multiplied by the font-specific base size.
        /// </param>
        /// <param name="color">
        /// Text color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the lines used to draw a text.
        /// </param>
        /// <param name="lineType">
        /// Line type. See #LineTypes
        /// </param>
        /// <param name="bottomLeftOrigin">
        /// When true, the image data origin is at the bottom-left corner. Otherwise,
        ///  it is at the top-left corner.
        /// </param>
        public static void putText(Mat img, string text, in Vec2d org, int fontFace, double fontScale, in Vec4d color, int thickness, int lineType)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_putText_11(img.nativeObj, text, org.Item1, org.Item2, fontFace, fontScale, color.Item1, color.Item2, color.Item3, color.Item4, thickness, lineType);


        }

        /// <summary>
        ///  Draws a text string.
        /// </summary>
        /// <remarks>
        ///  The function cv::putText renders the specified text string in the image. Symbols that cannot be rendered
        ///  using the specified font are replaced by question marks. See #getTextSize for a text rendering code
        ///  example.
        ///  
        ///  The `fontScale` parameter is a scale factor that is multiplied by the base font size:
        ///  - When scale &gt; 1, the text is magnified.
        ///  - When 0 &lt; scale &lt; 1, the text is minimized.
        ///  - When scale &lt; 0, the text is mirrored or reversed.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="text">
        /// Text string to be drawn.
        /// </param>
        /// <param name="org">
        /// Bottom-left corner of the text string in the image.
        /// </param>
        /// <param name="fontFace">
        /// Font type, see #HersheyFonts.
        /// </param>
        /// <param name="fontScale">
        /// Font scale factor that is multiplied by the font-specific base size.
        /// </param>
        /// <param name="color">
        /// Text color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the lines used to draw a text.
        /// </param>
        /// <param name="lineType">
        /// Line type. See #LineTypes
        /// </param>
        /// <param name="bottomLeftOrigin">
        /// When true, the image data origin is at the bottom-left corner. Otherwise,
        ///  it is at the top-left corner.
        /// </param>
        public static void putText(Mat img, string text, in Vec2d org, int fontFace, double fontScale, in Vec4d color, int thickness)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_putText_12(img.nativeObj, text, org.Item1, org.Item2, fontFace, fontScale, color.Item1, color.Item2, color.Item3, color.Item4, thickness);


        }

        /// <summary>
        ///  Draws a text string.
        /// </summary>
        /// <remarks>
        ///  The function cv::putText renders the specified text string in the image. Symbols that cannot be rendered
        ///  using the specified font are replaced by question marks. See #getTextSize for a text rendering code
        ///  example.
        ///  
        ///  The `fontScale` parameter is a scale factor that is multiplied by the base font size:
        ///  - When scale &gt; 1, the text is magnified.
        ///  - When 0 &lt; scale &lt; 1, the text is minimized.
        ///  - When scale &lt; 0, the text is mirrored or reversed.
        /// </remarks>
        /// <param name="img">
        /// Image.
        /// </param>
        /// <param name="text">
        /// Text string to be drawn.
        /// </param>
        /// <param name="org">
        /// Bottom-left corner of the text string in the image.
        /// </param>
        /// <param name="fontFace">
        /// Font type, see #HersheyFonts.
        /// </param>
        /// <param name="fontScale">
        /// Font scale factor that is multiplied by the font-specific base size.
        /// </param>
        /// <param name="color">
        /// Text color.
        /// </param>
        /// <param name="thickness">
        /// Thickness of the lines used to draw a text.
        /// </param>
        /// <param name="lineType">
        /// Line type. See #LineTypes
        /// </param>
        /// <param name="bottomLeftOrigin">
        /// When true, the image data origin is at the bottom-left corner. Otherwise,
        ///  it is at the top-left corner.
        /// </param>
        public static void putText(Mat img, string text, in Vec2d org, int fontFace, double fontScale, in Vec4d color)
        {
            if (img != null) img.ThrowIfDisposed();

            imgproc_Imgproc_putText_13(img.nativeObj, text, org.Item1, org.Item2, fontFace, fontScale, color.Item1, color.Item2, color.Item3, color.Item4);


        }



        // C++: Size getTextSize(const String& text, int fontFace, double fontScale, int thickness, int* baseLine);
        //javadoc:getTextSize(text, fontFace, fontScale, thickness, baseLine)
        public static Vec2d getTextSizeAsVec2d(string text, int fontFace, double fontScale, int thickness, int[] baseLine)
        {
            if (baseLine != null && baseLine.Length != 1)
                throw new CvException("'baseLine' must be 'int[1]' or 'null'.");
            double[] tmpArray = new double[2];
            imgproc_Imgproc_n_1getTextSize(text, fontFace, fontScale, thickness, baseLine, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);
            return retVal;
        }
    }
}
