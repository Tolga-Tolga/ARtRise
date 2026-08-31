
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class Core
    {


        //
        // C++:  void cv::copyMakeBorder(Mat src, Mat& dst, int top, int bottom, int left, int right, int borderType, Scalar value = Scalar())
        //

        /// <summary>
        ///  Forms a border around an image.
        /// </summary>
        /// <remarks>
        ///  The function copies the source image into the middle of the destination image. The areas to the
        ///  left, to the right, above and below the copied source image will be filled with extrapolated
        ///  pixels. This is not what filtering functions based on it do (they extrapolate pixels on-fly), but
        ///  what other more complex functions, including your own, may do to simplify image boundary handling.
        ///  
        ///  The function supports the mode when src is already in the middle of dst . In this case, the
        ///  function does not copy src itself but simply constructs the border, for example:
        /// </remarks>
        /// <code language="c++">
        ///      // let border be the same in all directions
        ///      int border=2;
        ///      // constructs a larger image to fit both the image and the border
        ///      Mat gray_buf(rgb.rows + border*2, rgb.cols + border*2, rgb.depth());
        ///      // select the middle part of it w/o copying data
        ///      Mat gray(gray_canvas, Rect(border, border, rgb.cols, rgb.rows));
        ///      // convert image from RGB to grayscale
        ///      cvtColor(rgb, gray, COLOR_RGB2GRAY);
        ///      // form a border in-place
        ///      copyMakeBorder(gray, gray_buf, border, border,
        ///                     border, border, BORDER_REPLICATE);
        ///      // now do some custom filtering ...
        ///      ...
        /// </code>
        /// <remarks>
        ///  @note When the source image is a part (ROI) of a bigger image, the function will try to use the
        ///  pixels outside of the ROI to form a border. To disable this feature and always do extrapolation, as
        ///  if src was not a ROI, use borderType | #BORDER_ISOLATED.
        /// </remarks>
        /// <param name="src">
        /// Source image.
        /// </param>
        /// <param name="dst">
        /// Destination image of the same type as src and the size Size(src.cols+left+right,
        ///  src.rows+top+bottom) .
        /// </param>
        /// <param name="top">
        /// the top pixels
        /// </param>
        /// <param name="bottom">
        /// the bottom pixels
        /// </param>
        /// <param name="left">
        /// the left pixels
        /// </param>
        /// <param name="right">
        /// Parameter specifying how many pixels in each direction from the source image rectangle
        ///  to extrapolate. For example, top=1, bottom=1, left=1, right=1 mean that 1 pixel-wide border needs
        ///  to be built.
        /// </param>
        /// <param name="borderType">
        /// Border type. See borderInterpolate for details.
        /// </param>
        /// <param name="value">
        /// Border value if borderType==BORDER_CONSTANT .
        /// </param>
        /// <remarks>
        ///  @sa  borderInterpolate
        /// </remarks>
        public static void copyMakeBorder(Mat src, Mat dst, int top, int bottom, int left, int right, int borderType, in (double v0, double v1, double v2, double v3) value)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_copyMakeBorder_10(src.nativeObj, dst.nativeObj, top, bottom, left, right, borderType, value.v0, value.v1, value.v2, value.v3);


        }


        //
        // C++:  Scalar cv::sum(Mat src)
        //

        /// <summary>
        ///  Calculates the sum of array elements.
        /// </summary>
        /// <remarks>
        ///  The function cv::sum calculates and returns the sum of array elements,
        ///  independently for each channel.
        /// </remarks>
        /// <param name="src">
        /// input array that must have from 1 to 4 channels.
        ///  @sa  countNonZero, mean, meanStdDev, norm, minMaxLoc, reduce
        /// </param>
        public static (double v0, double v1, double v2, double v3) sumElemsAsValueTuple(Mat src)
        {
            if (src != null) src.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            core_Core_sumElems_10(src.nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++:  Scalar cv::mean(Mat src, Mat mask = Mat())
        //

        /// <summary>
        ///  Calculates an average (mean) of array elements.
        /// </summary>
        /// <remarks>
        ///  The function cv::mean calculates the mean value M of array elements,
        ///  independently for each channel, and return it:
        ///  \f[\begin{array}{l} N =  \sum _{I: \; \texttt{mask} (I) \ne 0} 1 \\ M_c =  \left ( \sum _{I: \; \texttt{mask} (I) \ne 0}{ \texttt{mtx} (I)_c} \right )/N \end{array}\f]
        ///  When all the mask elements are 0's, the function returns Scalar::all(0)
        /// </remarks>
        /// <param name="src">
        /// input array that should have from 1 to 4 channels so that the result can be stored in
        ///  Scalar_ .
        /// </param>
        /// <param name="mask">
        /// optional operation mask.
        ///  @sa  countNonZero, meanStdDev, norm, minMaxLoc
        /// </param>
        public static (double v0, double v1, double v2, double v3) meanAsValueTuple(Mat src, Mat mask)
        {
            if (src != null) src.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            core_Core_mean_10(src.nativeObj, mask.nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }

        /// <summary>
        ///  Calculates an average (mean) of array elements.
        /// </summary>
        /// <remarks>
        ///  The function cv::mean calculates the mean value M of array elements,
        ///  independently for each channel, and return it:
        ///  \f[\begin{array}{l} N =  \sum _{I: \; \texttt{mask} (I) \ne 0} 1 \\ M_c =  \left ( \sum _{I: \; \texttt{mask} (I) \ne 0}{ \texttt{mtx} (I)_c} \right )/N \end{array}\f]
        ///  When all the mask elements are 0's, the function returns Scalar::all(0)
        /// </remarks>
        /// <param name="src">
        /// input array that should have from 1 to 4 channels so that the result can be stored in
        ///  Scalar_ .
        /// </param>
        /// <param name="mask">
        /// optional operation mask.
        ///  @sa  countNonZero, meanStdDev, norm, minMaxLoc
        /// </param>
        public static (double v0, double v1, double v2, double v3) meanAsValueTuple(Mat src)
        {
            if (src != null) src.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            core_Core_mean_11(src.nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++:  void cv::inRange(Mat src, Scalar lowerb, Scalar upperb, Mat& dst)
        //

        /// <summary>
        ///   Checks if array elements lie between the elements of two other arrays.
        /// </summary>
        /// <remarks>
        ///  The function checks the range as follows:
        ///  -   For every element of a single-channel input array:
        ///      \f[\texttt{dst} (I)= \texttt{lowerb} (I)_0  \leq \texttt{src} (I)_0 \leq  \texttt{upperb} (I)_0\f]
        ///  -   For two-channel arrays:
        ///      \f[\texttt{dst} (I)= \texttt{lowerb} (I)_0  \leq \texttt{src} (I)_0 \leq  \texttt{upperb} (I)_0  \land \texttt{lowerb} (I)_1  \leq \texttt{src} (I)_1 \leq  \texttt{upperb} (I)_1\f]
        ///  -   and so forth.
        ///  
        ///  That is, dst (I) is set to 255 (all 1 -bits) if src (I) is within the
        ///  specified 1D, 2D, 3D, ... box and 0 otherwise.
        ///  
        ///  When the lower and/or upper boundary parameters are scalars, the indexes
        ///  (I) at lowerb and upperb in the above formulas should be omitted.
        /// </remarks>
        /// <param name="src">
        /// first input array.
        /// </param>
        /// <param name="lowerb">
        /// inclusive lower boundary array or a scalar.
        /// </param>
        /// <param name="upperb">
        /// inclusive upper boundary array or a scalar.
        /// </param>
        /// <param name="dst">
        /// output array of the same size as src and CV_8U type.
        /// </param>
        public static void inRange(Mat src, in (double v0, double v1, double v2, double v3) lowerb, in (double v0, double v1, double v2, double v3) upperb, Mat dst)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_inRange_10(src.nativeObj, lowerb.v0, lowerb.v1, lowerb.v2, lowerb.v3, upperb.v0, upperb.v1, upperb.v2, upperb.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::setIdentity(Mat& mtx, Scalar s = Scalar(1))
        //

        /// <summary>
        ///  Initializes a scaled identity matrix.
        /// </summary>
        /// <remarks>
        ///  The function cv::setIdentity initializes a scaled identity matrix:
        ///  \f[\texttt{mtx} (i,j)= \fork{\texttt{value}}{ if \(i=j\)}{0}{otherwise}\f]
        ///  
        ///  The function can also be emulated using the matrix initializers and the
        ///  matrix expressions:
        /// </remarks>
        /// <code language="c++">
        ///      Mat A = Mat::eye(4, 3, CV_32F)*5;
        ///      // A will be set to [[5, 0, 0], [0, 5, 0], [0, 0, 5], [0, 0, 0]]
        /// </code>
        /// <param name="mtx">
        /// matrix to initialize (not necessarily square).
        /// </param>
        /// <param name="s">
        /// value to assign to diagonal elements.
        ///  @sa Mat::zeros, Mat::ones, Mat::setTo, Mat::operator=
        /// </param>
        public static void setIdentity(Mat mtx, in (double v0, double v1, double v2, double v3) s)
        {
            if (mtx != null) mtx.ThrowIfDisposed();

            core_Core_setIdentity_10(mtx.nativeObj, s.v0, s.v1, s.v2, s.v3);


        }


        //
        // C++:  Scalar cv::trace(Mat mtx)
        //

        /// <summary>
        ///  Returns the trace of a matrix.
        /// </summary>
        /// <remarks>
        ///  The function cv::trace returns the sum of the diagonal elements of the
        ///  matrix mtx .
        ///  \f[\mathrm{tr} ( \texttt{mtx} ) =  \sum _i  \texttt{mtx} (i,i)\f]
        /// </remarks>
        /// <param name="mtx">
        /// input matrix.
        /// </param>
        public static (double v0, double v1, double v2, double v3) traceAsValueTuple(Mat mtx)
        {
            if (mtx != null) mtx.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            core_Core_trace_10(mtx.nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++:  double cv::kmeans(Mat data, int K, Mat& bestLabels, TermCriteria criteria, int attempts, int flags, Mat& centers = Mat())
        //

        /// <summary>
        ///  Finds centers of clusters and groups input samples around the clusters.
        /// </summary>
        /// <remarks>
        ///  The function kmeans implements a k-means algorithm that finds the centers of cluster_count clusters
        ///  and groups the input samples around the clusters. As an output, \f$\texttt{bestLabels}_i\f$ contains a
        ///  0-based cluster index for the sample stored in the \f$i^{th}\f$ row of the samples matrix.
        ///  
        ///  @note
        ///  -   (Python) An example on k-means clustering can be found at
        ///      opencv_source_code/samples/python/kmeans.py
        /// </remarks>
        /// <param name="data">
        /// Data for clustering. An array of N-Dimensional points with float coordinates is needed.
        ///  Examples of this array can be:
        ///  -   Mat points(count, 2, CV_32F);
        ///  -   Mat points(count, 1, CV_32FC2);
        ///  -   Mat points(1, count, CV_32FC2);
        ///  -   std::vector&lt;cv::Point2f&gt; points(sampleCount);
        /// </param>
        /// <param name="K">
        /// Number of clusters to split the set by.
        /// </param>
        /// <param name="bestLabels">
        /// Input/output integer array that stores the cluster indices for every sample.
        /// </param>
        /// <param name="criteria">
        /// The algorithm termination criteria, that is, the maximum number of iterations and/or
        ///  the desired accuracy. The accuracy is specified as criteria.epsilon. As soon as each of the cluster
        ///  centers moves by less than criteria.epsilon on some iteration, the algorithm stops.
        /// </param>
        /// <param name="attempts">
        /// Flag to specify the number of times the algorithm is executed using different
        ///  initial labellings. The algorithm returns the labels that yield the best compactness (see the last
        ///  function parameter).
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::KmeansFlags
        /// </param>
        /// <param name="centers">
        /// Output matrix of the cluster centers, one row per each cluster center.
        /// </param>
        /// <returns>
        ///  The function returns the compactness measure that is computed as
        ///  \f[\sum _i  \| \texttt{samples} _i -  \texttt{centers} _{ \texttt{labels} _i} \| ^2\f]
        ///  after every attempt. The best (minimum) value is chosen and the corresponding labels and the
        ///  compactness value are returned by the function. Basically, you can use only the core of the
        ///  function, set the number of attempts to 1, initialize labels each time using a custom algorithm,
        ///  pass them with the ( flags = #KMEANS_USE_INITIAL_LABELS ) flag, and then choose the best
        ///  (most-compact) clustering.
        /// </returns>
        public static double kmeans(Mat data, int K, Mat bestLabels, in (double type, double maxCount, double epsilon) criteria, int attempts, int flags, Mat centers)
        {
            if (data != null) data.ThrowIfDisposed();
            if (bestLabels != null) bestLabels.ThrowIfDisposed();
            if (centers != null) centers.ThrowIfDisposed();

            return core_Core_kmeans_10(data.nativeObj, K, bestLabels.nativeObj, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, attempts, flags, centers.nativeObj);


        }

        /// <summary>
        ///  Finds centers of clusters and groups input samples around the clusters.
        /// </summary>
        /// <remarks>
        ///  The function kmeans implements a k-means algorithm that finds the centers of cluster_count clusters
        ///  and groups the input samples around the clusters. As an output, \f$\texttt{bestLabels}_i\f$ contains a
        ///  0-based cluster index for the sample stored in the \f$i^{th}\f$ row of the samples matrix.
        ///  
        ///  @note
        ///  -   (Python) An example on k-means clustering can be found at
        ///      opencv_source_code/samples/python/kmeans.py
        /// </remarks>
        /// <param name="data">
        /// Data for clustering. An array of N-Dimensional points with float coordinates is needed.
        ///  Examples of this array can be:
        ///  -   Mat points(count, 2, CV_32F);
        ///  -   Mat points(count, 1, CV_32FC2);
        ///  -   Mat points(1, count, CV_32FC2);
        ///  -   std::vector&lt;cv::Point2f&gt; points(sampleCount);
        /// </param>
        /// <param name="K">
        /// Number of clusters to split the set by.
        /// </param>
        /// <param name="bestLabels">
        /// Input/output integer array that stores the cluster indices for every sample.
        /// </param>
        /// <param name="criteria">
        /// The algorithm termination criteria, that is, the maximum number of iterations and/or
        ///  the desired accuracy. The accuracy is specified as criteria.epsilon. As soon as each of the cluster
        ///  centers moves by less than criteria.epsilon on some iteration, the algorithm stops.
        /// </param>
        /// <param name="attempts">
        /// Flag to specify the number of times the algorithm is executed using different
        ///  initial labellings. The algorithm returns the labels that yield the best compactness (see the last
        ///  function parameter).
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::KmeansFlags
        /// </param>
        /// <param name="centers">
        /// Output matrix of the cluster centers, one row per each cluster center.
        /// </param>
        /// <returns>
        ///  The function returns the compactness measure that is computed as
        ///  \f[\sum _i  \| \texttt{samples} _i -  \texttt{centers} _{ \texttt{labels} _i} \| ^2\f]
        ///  after every attempt. The best (minimum) value is chosen and the corresponding labels and the
        ///  compactness value are returned by the function. Basically, you can use only the core of the
        ///  function, set the number of attempts to 1, initialize labels each time using a custom algorithm,
        ///  pass them with the ( flags = #KMEANS_USE_INITIAL_LABELS ) flag, and then choose the best
        ///  (most-compact) clustering.
        /// </returns>
        public static double kmeans(Mat data, int K, Mat bestLabels, in (double type, double maxCount, double epsilon) criteria, int attempts, int flags)
        {
            if (data != null) data.ThrowIfDisposed();
            if (bestLabels != null) bestLabels.ThrowIfDisposed();

            return core_Core_kmeans_11(data.nativeObj, K, bestLabels.nativeObj, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, attempts, flags);


        }


        //
        // C++:  void cv::add(Mat src1, Scalar src2, Mat& dst, Mat mask = Mat(), int dtype = -1)
        //

        public static void add(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, Mat mask, int dtype)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            core_Core_add_13(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, mask.nativeObj, dtype);


        }

        public static void add(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, Mat mask)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            core_Core_add_14(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, mask.nativeObj);


        }

        public static void add(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_add_15(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::subtract(Mat src1, Scalar src2, Mat& dst, Mat mask = Mat(), int dtype = -1)
        //

        public static void subtract(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, Mat mask, int dtype)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            core_Core_subtract_13(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, mask.nativeObj, dtype);


        }

        public static void subtract(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, Mat mask)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            core_Core_subtract_14(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, mask.nativeObj);


        }

        public static void subtract(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_subtract_15(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::multiply(Mat src1, Scalar src2, Mat& dst, double scale = 1, int dtype = -1)
        //

        public static void multiply(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, double scale, int dtype)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_multiply_13(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, scale, dtype);


        }

        public static void multiply(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, double scale)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_multiply_14(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, scale);


        }

        public static void multiply(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_multiply_15(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::divide(Mat src1, Scalar src2, Mat& dst, double scale = 1, int dtype = -1)
        //

        public static void divide(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, double scale, int dtype)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_divide_15(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, scale, dtype);


        }

        public static void divide(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, double scale)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_divide_16(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, scale);


        }

        public static void divide(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_divide_17(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::absdiff(Mat src1, Scalar src2, Mat& dst)
        //

        public static void absdiff(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_absdiff_11(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::compare(Mat src1, Scalar src2, Mat& dst, int cmpop)
        //

        public static void compare(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst, int cmpop)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_compare_11(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj, cmpop);


        }


        //
        // C++:  void cv::min(Mat src1, Scalar src2, Mat& dst)
        //

        public static void min(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_min_11(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


        //
        // C++:  void cv::max(Mat src1, Scalar src2, Mat& dst)
        //

        public static void max(Mat src1, in (double v0, double v1, double v2, double v3) src2, Mat dst)
        {
            if (src1 != null) src1.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            core_Core_max_11(src1.nativeObj, src2.v0, src2.v1, src2.v2, src2.v3, dst.nativeObj);


        }


    }
}
