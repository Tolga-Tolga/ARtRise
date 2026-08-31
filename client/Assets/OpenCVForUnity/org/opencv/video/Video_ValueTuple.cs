
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{
    public partial class Video
    {


        //
        // C++:  RotatedRect cv::CamShift(Mat probImage, Rect& window, TermCriteria criteria)
        //

        /// <summary>
        ///  Finds an object center, size, and orientation.
        /// </summary>
        /// <param name="probImage">
        /// Back projection of the object histogram. See calcBackProject.
        /// </param>
        /// <param name="window">
        /// Initial search window.
        /// </param>
        /// <param name="criteria">
        /// Stop criteria for the underlying meanShift.
        ///  returns
        ///  (in old interfaces) Number of iterations CAMSHIFT took to converge
        ///  The function implements the CAMSHIFT object tracking algorithm @cite Bradski98 . First, it finds an
        ///  object center using meanShift and then adjusts the window size and finds the optimal rotation. The
        ///  function returns the rotated rectangle structure that includes the object position, size, and
        ///  orientation. The next position of the search window can be obtained with RotatedRect::boundingRect()
        /// </param>
        /// <remarks>
        ///  See the OpenCV sample camshiftdemo.c that tracks colored objects.
        ///  
        ///  @note
        ///  -   (Python) A sample explaining the camshift tracking algorithm can be found at
        ///      opencv_source_code/samples/python/camshift.py
        /// </remarks>
        public static (double x, double y, double width, double height, double angle) CamShiftAsValueTuple(Mat probImage, ref (int x, int y, int width, int height) window, in (double type, double maxCount, double epsilon) criteria)
        {
            if (probImage != null) probImage.ThrowIfDisposed();
            double[] window_out = new double[4];
            double[] tmpArray = new double[5];
            video_Video_CamShift_10(probImage.nativeObj, window.x, window.y, window.width, window.height, window_out, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, tmpArray);
            (double x, double y, double width, double height, double angle) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3], tmpArray[4]);
            { window.x = (int)window_out[0]; window.y = (int)window_out[1]; window.width = (int)window_out[2]; window.height = (int)window_out[3]; }
            return retVal;
        }


        //
        // C++:  int cv::meanShift(Mat probImage, Rect& window, TermCriteria criteria)
        //

        /// <summary>
        ///  Finds an object on a back projection image.
        /// </summary>
        /// <param name="probImage">
        /// Back projection of the object histogram. See calcBackProject for details.
        /// </param>
        /// <param name="window">
        /// Initial search window.
        /// </param>
        /// <param name="criteria">
        /// Stop criteria for the iterative search algorithm.
        ///  returns
        ///  :   Number of iterations CAMSHIFT took to converge.
        ///  The function implements the iterative object search algorithm. It takes the input back projection of
        ///  an object and the initial position. The mass center in window of the back projection image is
        ///  computed and the search window center shifts to the mass center. The procedure is repeated until the
        ///  specified number of iterations criteria.maxCount is done or until the window center shifts by less
        ///  than criteria.epsilon. The algorithm is used inside CamShift and, unlike CamShift , the search
        ///  window size or orientation do not change during the search. You can simply pass the output of
        ///  calcBackProject to this function. But better results can be obtained if you pre-filter the back
        ///  projection and remove the noise. For example, you can do this by retrieving connected components
        ///  with findContours , throwing away contours with small area ( contourArea ), and rendering the
        ///  remaining contours with drawContours.
        /// </param>
        public static int meanShift(Mat probImage, ref (int x, int y, int width, int height) window, in (double type, double maxCount, double epsilon) criteria)
        {
            if (probImage != null) probImage.ThrowIfDisposed();
            double[] window_out = new double[4];
            int retVal = video_Video_meanShift_10(probImage.nativeObj, window.x, window.y, window.width, window.height, window_out, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon);
            { window.x = (int)window_out[0]; window.y = (int)window_out[1]; window.width = (int)window_out[2]; window.height = (int)window_out[3]; }
            return retVal;
        }


        //
        // C++:  int cv::buildOpticalFlowPyramid(Mat img, vector_Mat& pyramid, Size winSize, int maxLevel, bool withDerivatives = true, int pyrBorder = BORDER_REFLECT_101, int derivBorder = BORDER_CONSTANT, bool tryReuseInputImage = true)
        //

        /// <summary>
        ///  Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.
        /// </summary>
        /// <param name="img">
        /// 8-bit input image.
        /// </param>
        /// <param name="pyramid">
        /// output pyramid.
        /// </param>
        /// <param name="winSize">
        /// window size of optical flow algorithm. Must be not less than winSize argument of
        ///  calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number.
        /// </param>
        /// <param name="withDerivatives">
        /// set to precompute gradients for the every pyramid level. If pyramid is
        ///  constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
        /// </param>
        /// <param name="pyrBorder">
        /// the border mode for pyramid layers.
        /// </param>
        /// <param name="derivBorder">
        /// the border mode for gradients.
        /// </param>
        /// <param name="tryReuseInputImage">
        /// put ROI of input image into the pyramid if possible. You can pass false
        ///  to force data copying.
        /// </param>
        /// <returns>
        ///  number of levels in constructed pyramid. Can be less than maxLevel.
        /// </returns>
        public static int buildOpticalFlowPyramid(Mat img, List<Mat> pyramid, in (double width, double height) winSize, int maxLevel, bool withDerivatives, int pyrBorder, int derivBorder, bool tryReuseInputImage)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pyramid_mat = new Mat();
            int retVal = video_Video_buildOpticalFlowPyramid_10(img.nativeObj, pyramid_mat.nativeObj, winSize.width, winSize.height, maxLevel, withDerivatives, pyrBorder, derivBorder, tryReuseInputImage);
            Converters.Mat_to_vector_Mat(pyramid_mat, pyramid);
            return retVal;
        }

        /// <summary>
        ///  Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.
        /// </summary>
        /// <param name="img">
        /// 8-bit input image.
        /// </param>
        /// <param name="pyramid">
        /// output pyramid.
        /// </param>
        /// <param name="winSize">
        /// window size of optical flow algorithm. Must be not less than winSize argument of
        ///  calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number.
        /// </param>
        /// <param name="withDerivatives">
        /// set to precompute gradients for the every pyramid level. If pyramid is
        ///  constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
        /// </param>
        /// <param name="pyrBorder">
        /// the border mode for pyramid layers.
        /// </param>
        /// <param name="derivBorder">
        /// the border mode for gradients.
        /// </param>
        /// <param name="tryReuseInputImage">
        /// put ROI of input image into the pyramid if possible. You can pass false
        ///  to force data copying.
        /// </param>
        /// <returns>
        ///  number of levels in constructed pyramid. Can be less than maxLevel.
        /// </returns>
        public static int buildOpticalFlowPyramid(Mat img, List<Mat> pyramid, in (double width, double height) winSize, int maxLevel, bool withDerivatives, int pyrBorder, int derivBorder)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pyramid_mat = new Mat();
            int retVal = video_Video_buildOpticalFlowPyramid_11(img.nativeObj, pyramid_mat.nativeObj, winSize.width, winSize.height, maxLevel, withDerivatives, pyrBorder, derivBorder);
            Converters.Mat_to_vector_Mat(pyramid_mat, pyramid);
            return retVal;
        }

        /// <summary>
        ///  Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.
        /// </summary>
        /// <param name="img">
        /// 8-bit input image.
        /// </param>
        /// <param name="pyramid">
        /// output pyramid.
        /// </param>
        /// <param name="winSize">
        /// window size of optical flow algorithm. Must be not less than winSize argument of
        ///  calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number.
        /// </param>
        /// <param name="withDerivatives">
        /// set to precompute gradients for the every pyramid level. If pyramid is
        ///  constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
        /// </param>
        /// <param name="pyrBorder">
        /// the border mode for pyramid layers.
        /// </param>
        /// <param name="derivBorder">
        /// the border mode for gradients.
        /// </param>
        /// <param name="tryReuseInputImage">
        /// put ROI of input image into the pyramid if possible. You can pass false
        ///  to force data copying.
        /// </param>
        /// <returns>
        ///  number of levels in constructed pyramid. Can be less than maxLevel.
        /// </returns>
        public static int buildOpticalFlowPyramid(Mat img, List<Mat> pyramid, in (double width, double height) winSize, int maxLevel, bool withDerivatives, int pyrBorder)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pyramid_mat = new Mat();
            int retVal = video_Video_buildOpticalFlowPyramid_12(img.nativeObj, pyramid_mat.nativeObj, winSize.width, winSize.height, maxLevel, withDerivatives, pyrBorder);
            Converters.Mat_to_vector_Mat(pyramid_mat, pyramid);
            return retVal;
        }

        /// <summary>
        ///  Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.
        /// </summary>
        /// <param name="img">
        /// 8-bit input image.
        /// </param>
        /// <param name="pyramid">
        /// output pyramid.
        /// </param>
        /// <param name="winSize">
        /// window size of optical flow algorithm. Must be not less than winSize argument of
        ///  calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number.
        /// </param>
        /// <param name="withDerivatives">
        /// set to precompute gradients for the every pyramid level. If pyramid is
        ///  constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
        /// </param>
        /// <param name="pyrBorder">
        /// the border mode for pyramid layers.
        /// </param>
        /// <param name="derivBorder">
        /// the border mode for gradients.
        /// </param>
        /// <param name="tryReuseInputImage">
        /// put ROI of input image into the pyramid if possible. You can pass false
        ///  to force data copying.
        /// </param>
        /// <returns>
        ///  number of levels in constructed pyramid. Can be less than maxLevel.
        /// </returns>
        public static int buildOpticalFlowPyramid(Mat img, List<Mat> pyramid, in (double width, double height) winSize, int maxLevel, bool withDerivatives)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pyramid_mat = new Mat();
            int retVal = video_Video_buildOpticalFlowPyramid_13(img.nativeObj, pyramid_mat.nativeObj, winSize.width, winSize.height, maxLevel, withDerivatives);
            Converters.Mat_to_vector_Mat(pyramid_mat, pyramid);
            return retVal;
        }

        /// <summary>
        ///  Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.
        /// </summary>
        /// <param name="img">
        /// 8-bit input image.
        /// </param>
        /// <param name="pyramid">
        /// output pyramid.
        /// </param>
        /// <param name="winSize">
        /// window size of optical flow algorithm. Must be not less than winSize argument of
        ///  calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number.
        /// </param>
        /// <param name="withDerivatives">
        /// set to precompute gradients for the every pyramid level. If pyramid is
        ///  constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
        /// </param>
        /// <param name="pyrBorder">
        /// the border mode for pyramid layers.
        /// </param>
        /// <param name="derivBorder">
        /// the border mode for gradients.
        /// </param>
        /// <param name="tryReuseInputImage">
        /// put ROI of input image into the pyramid if possible. You can pass false
        ///  to force data copying.
        /// </param>
        /// <returns>
        ///  number of levels in constructed pyramid. Can be less than maxLevel.
        /// </returns>
        public static int buildOpticalFlowPyramid(Mat img, List<Mat> pyramid, in (double width, double height) winSize, int maxLevel)
        {
            if (img != null) img.ThrowIfDisposed();
            using Mat pyramid_mat = new Mat();
            int retVal = video_Video_buildOpticalFlowPyramid_14(img.nativeObj, pyramid_mat.nativeObj, winSize.width, winSize.height, maxLevel);
            Converters.Mat_to_vector_Mat(pyramid_mat, pyramid);
            return retVal;
        }


        //
        // C++:  void cv::calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, vector_Point2f prevPts, vector_Point2f& nextPts, vector_uchar& status, vector_float& err, Size winSize = Size(21,21), int maxLevel = 3, TermCriteria criteria = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 0.01), int flags = 0, double minEigThreshold = 1e-4)
        //

        /// <summary>
        ///  Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with
        ///  pyramids.
        /// </summary>
        /// <param name="prevImg">
        /// first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
        /// </param>
        /// <param name="nextImg">
        /// second input image or pyramid of the same size and the same type as prevImg.
        /// </param>
        /// <param name="prevPts">
        /// vector of 2D points for which the flow needs to be found; point coordinates must be
        ///  single-precision floating-point numbers.
        /// </param>
        /// <param name="nextPts">
        /// output vector of 2D points (with single-precision floating-point coordinates)
        ///  containing the calculated new positions of input features in the second image; when
        ///  OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
        /// </param>
        /// <param name="status">
        /// output status vector (of unsigned chars); each element of the vector is set to 1 if
        ///  the flow for the corresponding features has been found, otherwise, it is set to 0.
        /// </param>
        /// <param name="err">
        /// output vector of errors; each element of the vector is set to an error for the
        ///  corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't
        ///  found then the error is not defined (use the status parameter to find such cases).
        /// </param>
        /// <param name="winSize">
        /// size of the search window at each pyramid level.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number; if set to 0, pyramids are not used (single
        ///  level), if set to 1, two levels are used, and so on; if pyramids are passed to input then
        ///  algorithm will use as many levels as pyramids have but no more than maxLevel.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the iterative search algorithm
        ///  (after the specified maximum number of iterations criteria.maxCount or when the search window
        ///  moves by less than criteria.epsilon.
        /// </param>
        /// <param name="flags">
        /// operation flags:
        ///   -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is
        ///       not set, then prevPts is copied to nextPts and is considered the initial estimate.
        ///   -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see
        ///       minEigThreshold description); if the flag is not set, then L1 distance between patches
        ///       around the original and a moved point, divided by number of pixels in a window, is used as a
        ///       error measure.
        /// </param>
        /// <param name="minEigThreshold">
        /// the algorithm calculates the minimum eigen value of a 2x2 normal matrix of
        ///  optical flow equations (this matrix is called a spatial gradient matrix in @cite Bouguet00), divided
        ///  by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding
        ///  feature is filtered out and its flow is not processed, so it allows to remove bad points and get a
        ///  performance boost.
        /// </param>
        /// <remarks>
        ///  The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See
        ///  @cite Bouguet00 . The function is parallelized with the TBB library.
        ///  
        ///  @note Some examples:
        ///  
        ///  -   An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/cpp/lkdemo.cpp
        ///  -   (Python) An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/python/lk_track.py
        ///  -   (Python) An example using the Lucas-Kanade tracker for homography matching can be found at
        ///      opencv_source_code/samples/python/lk_homography.py
        /// </remarks>
        public static void calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, MatOfPoint2f prevPts, MatOfPoint2f nextPts, MatOfByte status, MatOfFloat err, in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) criteria, int flags, double minEigThreshold)
        {
            if (prevImg != null) prevImg.ThrowIfDisposed();
            if (nextImg != null) nextImg.ThrowIfDisposed();
            if (prevPts != null) prevPts.ThrowIfDisposed();
            if (nextPts != null) nextPts.ThrowIfDisposed();
            if (status != null) status.ThrowIfDisposed();
            if (err != null) err.ThrowIfDisposed();
            Mat prevPts_mat = prevPts;
            Mat nextPts_mat = nextPts;
            Mat status_mat = status;
            Mat err_mat = err;
            video_Video_calcOpticalFlowPyrLK_10(prevImg.nativeObj, nextImg.nativeObj, prevPts_mat.nativeObj, nextPts_mat.nativeObj, status_mat.nativeObj, err_mat.nativeObj, winSize.width, winSize.height, maxLevel, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, flags, minEigThreshold);


        }

        /// <summary>
        ///  Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with
        ///  pyramids.
        /// </summary>
        /// <param name="prevImg">
        /// first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
        /// </param>
        /// <param name="nextImg">
        /// second input image or pyramid of the same size and the same type as prevImg.
        /// </param>
        /// <param name="prevPts">
        /// vector of 2D points for which the flow needs to be found; point coordinates must be
        ///  single-precision floating-point numbers.
        /// </param>
        /// <param name="nextPts">
        /// output vector of 2D points (with single-precision floating-point coordinates)
        ///  containing the calculated new positions of input features in the second image; when
        ///  OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
        /// </param>
        /// <param name="status">
        /// output status vector (of unsigned chars); each element of the vector is set to 1 if
        ///  the flow for the corresponding features has been found, otherwise, it is set to 0.
        /// </param>
        /// <param name="err">
        /// output vector of errors; each element of the vector is set to an error for the
        ///  corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't
        ///  found then the error is not defined (use the status parameter to find such cases).
        /// </param>
        /// <param name="winSize">
        /// size of the search window at each pyramid level.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number; if set to 0, pyramids are not used (single
        ///  level), if set to 1, two levels are used, and so on; if pyramids are passed to input then
        ///  algorithm will use as many levels as pyramids have but no more than maxLevel.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the iterative search algorithm
        ///  (after the specified maximum number of iterations criteria.maxCount or when the search window
        ///  moves by less than criteria.epsilon.
        /// </param>
        /// <param name="flags">
        /// operation flags:
        ///   -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is
        ///       not set, then prevPts is copied to nextPts and is considered the initial estimate.
        ///   -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see
        ///       minEigThreshold description); if the flag is not set, then L1 distance between patches
        ///       around the original and a moved point, divided by number of pixels in a window, is used as a
        ///       error measure.
        /// </param>
        /// <param name="minEigThreshold">
        /// the algorithm calculates the minimum eigen value of a 2x2 normal matrix of
        ///  optical flow equations (this matrix is called a spatial gradient matrix in @cite Bouguet00), divided
        ///  by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding
        ///  feature is filtered out and its flow is not processed, so it allows to remove bad points and get a
        ///  performance boost.
        /// </param>
        /// <remarks>
        ///  The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See
        ///  @cite Bouguet00 . The function is parallelized with the TBB library.
        ///  
        ///  @note Some examples:
        ///  
        ///  -   An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/cpp/lkdemo.cpp
        ///  -   (Python) An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/python/lk_track.py
        ///  -   (Python) An example using the Lucas-Kanade tracker for homography matching can be found at
        ///      opencv_source_code/samples/python/lk_homography.py
        /// </remarks>
        public static void calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, MatOfPoint2f prevPts, MatOfPoint2f nextPts, MatOfByte status, MatOfFloat err, in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) criteria, int flags)
        {
            if (prevImg != null) prevImg.ThrowIfDisposed();
            if (nextImg != null) nextImg.ThrowIfDisposed();
            if (prevPts != null) prevPts.ThrowIfDisposed();
            if (nextPts != null) nextPts.ThrowIfDisposed();
            if (status != null) status.ThrowIfDisposed();
            if (err != null) err.ThrowIfDisposed();
            Mat prevPts_mat = prevPts;
            Mat nextPts_mat = nextPts;
            Mat status_mat = status;
            Mat err_mat = err;
            video_Video_calcOpticalFlowPyrLK_11(prevImg.nativeObj, nextImg.nativeObj, prevPts_mat.nativeObj, nextPts_mat.nativeObj, status_mat.nativeObj, err_mat.nativeObj, winSize.width, winSize.height, maxLevel, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, flags);


        }

        /// <summary>
        ///  Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with
        ///  pyramids.
        /// </summary>
        /// <param name="prevImg">
        /// first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
        /// </param>
        /// <param name="nextImg">
        /// second input image or pyramid of the same size and the same type as prevImg.
        /// </param>
        /// <param name="prevPts">
        /// vector of 2D points for which the flow needs to be found; point coordinates must be
        ///  single-precision floating-point numbers.
        /// </param>
        /// <param name="nextPts">
        /// output vector of 2D points (with single-precision floating-point coordinates)
        ///  containing the calculated new positions of input features in the second image; when
        ///  OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
        /// </param>
        /// <param name="status">
        /// output status vector (of unsigned chars); each element of the vector is set to 1 if
        ///  the flow for the corresponding features has been found, otherwise, it is set to 0.
        /// </param>
        /// <param name="err">
        /// output vector of errors; each element of the vector is set to an error for the
        ///  corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't
        ///  found then the error is not defined (use the status parameter to find such cases).
        /// </param>
        /// <param name="winSize">
        /// size of the search window at each pyramid level.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number; if set to 0, pyramids are not used (single
        ///  level), if set to 1, two levels are used, and so on; if pyramids are passed to input then
        ///  algorithm will use as many levels as pyramids have but no more than maxLevel.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the iterative search algorithm
        ///  (after the specified maximum number of iterations criteria.maxCount or when the search window
        ///  moves by less than criteria.epsilon.
        /// </param>
        /// <param name="flags">
        /// operation flags:
        ///   -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is
        ///       not set, then prevPts is copied to nextPts and is considered the initial estimate.
        ///   -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see
        ///       minEigThreshold description); if the flag is not set, then L1 distance between patches
        ///       around the original and a moved point, divided by number of pixels in a window, is used as a
        ///       error measure.
        /// </param>
        /// <param name="minEigThreshold">
        /// the algorithm calculates the minimum eigen value of a 2x2 normal matrix of
        ///  optical flow equations (this matrix is called a spatial gradient matrix in @cite Bouguet00), divided
        ///  by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding
        ///  feature is filtered out and its flow is not processed, so it allows to remove bad points and get a
        ///  performance boost.
        /// </param>
        /// <remarks>
        ///  The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See
        ///  @cite Bouguet00 . The function is parallelized with the TBB library.
        ///  
        ///  @note Some examples:
        ///  
        ///  -   An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/cpp/lkdemo.cpp
        ///  -   (Python) An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/python/lk_track.py
        ///  -   (Python) An example using the Lucas-Kanade tracker for homography matching can be found at
        ///      opencv_source_code/samples/python/lk_homography.py
        /// </remarks>
        public static void calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, MatOfPoint2f prevPts, MatOfPoint2f nextPts, MatOfByte status, MatOfFloat err, in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) criteria)
        {
            if (prevImg != null) prevImg.ThrowIfDisposed();
            if (nextImg != null) nextImg.ThrowIfDisposed();
            if (prevPts != null) prevPts.ThrowIfDisposed();
            if (nextPts != null) nextPts.ThrowIfDisposed();
            if (status != null) status.ThrowIfDisposed();
            if (err != null) err.ThrowIfDisposed();
            Mat prevPts_mat = prevPts;
            Mat nextPts_mat = nextPts;
            Mat status_mat = status;
            Mat err_mat = err;
            video_Video_calcOpticalFlowPyrLK_12(prevImg.nativeObj, nextImg.nativeObj, prevPts_mat.nativeObj, nextPts_mat.nativeObj, status_mat.nativeObj, err_mat.nativeObj, winSize.width, winSize.height, maxLevel, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon);


        }

        /// <summary>
        ///  Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with
        ///  pyramids.
        /// </summary>
        /// <param name="prevImg">
        /// first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
        /// </param>
        /// <param name="nextImg">
        /// second input image or pyramid of the same size and the same type as prevImg.
        /// </param>
        /// <param name="prevPts">
        /// vector of 2D points for which the flow needs to be found; point coordinates must be
        ///  single-precision floating-point numbers.
        /// </param>
        /// <param name="nextPts">
        /// output vector of 2D points (with single-precision floating-point coordinates)
        ///  containing the calculated new positions of input features in the second image; when
        ///  OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
        /// </param>
        /// <param name="status">
        /// output status vector (of unsigned chars); each element of the vector is set to 1 if
        ///  the flow for the corresponding features has been found, otherwise, it is set to 0.
        /// </param>
        /// <param name="err">
        /// output vector of errors; each element of the vector is set to an error for the
        ///  corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't
        ///  found then the error is not defined (use the status parameter to find such cases).
        /// </param>
        /// <param name="winSize">
        /// size of the search window at each pyramid level.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number; if set to 0, pyramids are not used (single
        ///  level), if set to 1, two levels are used, and so on; if pyramids are passed to input then
        ///  algorithm will use as many levels as pyramids have but no more than maxLevel.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the iterative search algorithm
        ///  (after the specified maximum number of iterations criteria.maxCount or when the search window
        ///  moves by less than criteria.epsilon.
        /// </param>
        /// <param name="flags">
        /// operation flags:
        ///   -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is
        ///       not set, then prevPts is copied to nextPts and is considered the initial estimate.
        ///   -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see
        ///       minEigThreshold description); if the flag is not set, then L1 distance between patches
        ///       around the original and a moved point, divided by number of pixels in a window, is used as a
        ///       error measure.
        /// </param>
        /// <param name="minEigThreshold">
        /// the algorithm calculates the minimum eigen value of a 2x2 normal matrix of
        ///  optical flow equations (this matrix is called a spatial gradient matrix in @cite Bouguet00), divided
        ///  by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding
        ///  feature is filtered out and its flow is not processed, so it allows to remove bad points and get a
        ///  performance boost.
        /// </param>
        /// <remarks>
        ///  The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See
        ///  @cite Bouguet00 . The function is parallelized with the TBB library.
        ///  
        ///  @note Some examples:
        ///  
        ///  -   An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/cpp/lkdemo.cpp
        ///  -   (Python) An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/python/lk_track.py
        ///  -   (Python) An example using the Lucas-Kanade tracker for homography matching can be found at
        ///      opencv_source_code/samples/python/lk_homography.py
        /// </remarks>
        public static void calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, MatOfPoint2f prevPts, MatOfPoint2f nextPts, MatOfByte status, MatOfFloat err, in (double width, double height) winSize, int maxLevel)
        {
            if (prevImg != null) prevImg.ThrowIfDisposed();
            if (nextImg != null) nextImg.ThrowIfDisposed();
            if (prevPts != null) prevPts.ThrowIfDisposed();
            if (nextPts != null) nextPts.ThrowIfDisposed();
            if (status != null) status.ThrowIfDisposed();
            if (err != null) err.ThrowIfDisposed();
            Mat prevPts_mat = prevPts;
            Mat nextPts_mat = nextPts;
            Mat status_mat = status;
            Mat err_mat = err;
            video_Video_calcOpticalFlowPyrLK_13(prevImg.nativeObj, nextImg.nativeObj, prevPts_mat.nativeObj, nextPts_mat.nativeObj, status_mat.nativeObj, err_mat.nativeObj, winSize.width, winSize.height, maxLevel);


        }

        /// <summary>
        ///  Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with
        ///  pyramids.
        /// </summary>
        /// <param name="prevImg">
        /// first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
        /// </param>
        /// <param name="nextImg">
        /// second input image or pyramid of the same size and the same type as prevImg.
        /// </param>
        /// <param name="prevPts">
        /// vector of 2D points for which the flow needs to be found; point coordinates must be
        ///  single-precision floating-point numbers.
        /// </param>
        /// <param name="nextPts">
        /// output vector of 2D points (with single-precision floating-point coordinates)
        ///  containing the calculated new positions of input features in the second image; when
        ///  OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
        /// </param>
        /// <param name="status">
        /// output status vector (of unsigned chars); each element of the vector is set to 1 if
        ///  the flow for the corresponding features has been found, otherwise, it is set to 0.
        /// </param>
        /// <param name="err">
        /// output vector of errors; each element of the vector is set to an error for the
        ///  corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't
        ///  found then the error is not defined (use the status parameter to find such cases).
        /// </param>
        /// <param name="winSize">
        /// size of the search window at each pyramid level.
        /// </param>
        /// <param name="maxLevel">
        /// 0-based maximal pyramid level number; if set to 0, pyramids are not used (single
        ///  level), if set to 1, two levels are used, and so on; if pyramids are passed to input then
        ///  algorithm will use as many levels as pyramids have but no more than maxLevel.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the iterative search algorithm
        ///  (after the specified maximum number of iterations criteria.maxCount or when the search window
        ///  moves by less than criteria.epsilon.
        /// </param>
        /// <param name="flags">
        /// operation flags:
        ///   -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is
        ///       not set, then prevPts is copied to nextPts and is considered the initial estimate.
        ///   -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see
        ///       minEigThreshold description); if the flag is not set, then L1 distance between patches
        ///       around the original and a moved point, divided by number of pixels in a window, is used as a
        ///       error measure.
        /// </param>
        /// <param name="minEigThreshold">
        /// the algorithm calculates the minimum eigen value of a 2x2 normal matrix of
        ///  optical flow equations (this matrix is called a spatial gradient matrix in @cite Bouguet00), divided
        ///  by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding
        ///  feature is filtered out and its flow is not processed, so it allows to remove bad points and get a
        ///  performance boost.
        /// </param>
        /// <remarks>
        ///  The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See
        ///  @cite Bouguet00 . The function is parallelized with the TBB library.
        ///  
        ///  @note Some examples:
        ///  
        ///  -   An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/cpp/lkdemo.cpp
        ///  -   (Python) An example using the Lucas-Kanade optical flow algorithm can be found at
        ///      opencv_source_code/samples/python/lk_track.py
        ///  -   (Python) An example using the Lucas-Kanade tracker for homography matching can be found at
        ///      opencv_source_code/samples/python/lk_homography.py
        /// </remarks>
        public static void calcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, MatOfPoint2f prevPts, MatOfPoint2f nextPts, MatOfByte status, MatOfFloat err, in (double width, double height) winSize)
        {
            if (prevImg != null) prevImg.ThrowIfDisposed();
            if (nextImg != null) nextImg.ThrowIfDisposed();
            if (prevPts != null) prevPts.ThrowIfDisposed();
            if (nextPts != null) nextPts.ThrowIfDisposed();
            if (status != null) status.ThrowIfDisposed();
            if (err != null) err.ThrowIfDisposed();
            Mat prevPts_mat = prevPts;
            Mat nextPts_mat = nextPts;
            Mat status_mat = status;
            Mat err_mat = err;
            video_Video_calcOpticalFlowPyrLK_14(prevImg.nativeObj, nextImg.nativeObj, prevPts_mat.nativeObj, nextPts_mat.nativeObj, status_mat.nativeObj, err_mat.nativeObj, winSize.width, winSize.height);


        }


        //
        // C++:  double cv::findTransformECC(Mat templateImage, Mat inputImage, Mat& warpMatrix, int motionType, TermCriteria criteria, Mat inputMask, int gaussFiltSize)
        //

        /// <summary>
        ///  Finds the geometric transform (warp) between two images in terms of the ECC criterion @cite EP08 .
        /// </summary>
        /// <param name="templateImage">
        /// single-channel template image; CV_8U or CV_32F array.
        /// </param>
        /// <param name="inputImage">
        /// single-channel input image which should be warped with the final warpMatrix in
        ///  order to provide an image similar to templateImage, same type as templateImage.
        /// </param>
        /// <param name="warpMatrix">
        /// floating-point \f$2\times 3\f$ or \f$3\times 3\f$ mapping matrix (warp).
        /// </param>
        /// <param name="motionType">
        /// parameter, specifying the type of motion:
        ///   -   **MOTION_TRANSLATION** sets a translational motion model; warpMatrix is \f$2\times 3\f$ with
        ///       the first \f$2\times 2\f$ part being the unity matrix and the rest two parameters being
        ///       estimated.
        ///   -   **MOTION_EUCLIDEAN** sets a Euclidean (rigid) transformation as motion model; three
        ///       parameters are estimated; warpMatrix is \f$2\times 3\f$.
        ///   -   **MOTION_AFFINE** sets an affine motion model (DEFAULT); six parameters are estimated;
        ///       warpMatrix is \f$2\times 3\f$.
        ///   -   **MOTION_HOMOGRAPHY** sets a homography as a motion model; eight parameters are
        ///       estimated;\`warpMatrix\` is \f$3\times 3\f$.
        /// </param>
        /// <param name="criteria">
        /// parameter, specifying the termination criteria of the ECC algorithm;
        ///  criteria.epsilon defines the threshold of the increment in the correlation coefficient between two
        ///  iterations (a negative criteria.epsilon makes criteria.maxcount the only termination criterion).
        ///  Default values are shown in the declaration above.
        /// </param>
        /// <param name="inputMask">
        /// An optional mask to indicate valid values of inputImage.
        /// </param>
        /// <param name="gaussFiltSize">
        /// An optional value indicating size of gaussian blur filter; (DEFAULT: 5)
        /// </param>
        /// <remarks>
        ///  The function estimates the optimum transformation (warpMatrix) with respect to ECC criterion
        ///  (@cite EP08), that is
        ///  
        ///  \f[\texttt{warpMatrix} = \arg\max_{W} \texttt{ECC}(\texttt{templateImage}(x,y),\texttt{inputImage}(x',y'))\f]
        ///  
        ///  where
        ///  
        ///  \f[\begin{bmatrix} x' \\ y' \end{bmatrix} = W \cdot \begin{bmatrix} x \\ y \\ 1 \end{bmatrix}\f]
        ///  
        ///  (the equation holds with homogeneous coordinates for homography). It returns the final enhanced
        ///  correlation coefficient, that is the correlation coefficient between the template image and the
        ///  final warped input image. When a \f$3\times 3\f$ matrix is given with motionType =0, 1 or 2, the third
        ///  row is ignored.
        ///  
        ///  Unlike findHomography and estimateRigidTransform, the function findTransformECC implements an
        ///  area-based alignment that builds on intensity similarities. In essence, the function updates the
        ///  initial transformation that roughly aligns the images. If this information is missing, the identity
        ///  warp (unity matrix) is used as an initialization. Note that if images undergo strong
        ///  displacements/rotations, an initial transformation that roughly aligns the images is necessary
        ///  (e.g., a simple euclidean/similarity transform that allows for the images showing the same image
        ///  content approximately). Use inverse warping in the second image to take an image close to the first
        ///  one, i.e. use the flag WARP_INVERSE_MAP with warpAffine or warpPerspective. See also the OpenCV
        ///  sample image_alignment.cpp that demonstrates the use of the function. Note that the function throws
        ///  an exception if algorithm does not converges.
        ///  
        ///  @sa
        ///  computeECC, estimateAffine2D, estimateAffinePartial2D, findHomography
        /// </remarks>
        public static double findTransformECC(Mat templateImage, Mat inputImage, Mat warpMatrix, int motionType, in (double type, double maxCount, double epsilon) criteria, Mat inputMask, int gaussFiltSize)
        {
            if (templateImage != null) templateImage.ThrowIfDisposed();
            if (inputImage != null) inputImage.ThrowIfDisposed();
            if (warpMatrix != null) warpMatrix.ThrowIfDisposed();
            if (inputMask != null) inputMask.ThrowIfDisposed();

            return video_Video_findTransformECC_10(templateImage.nativeObj, inputImage.nativeObj, warpMatrix.nativeObj, motionType, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, inputMask.nativeObj, gaussFiltSize);


        }


        //
        // C++:  double cv::findTransformECC(Mat templateImage, Mat inputImage, Mat& warpMatrix, int motionType = MOTION_AFFINE, TermCriteria criteria = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 50, 0.001), Mat inputMask = Mat())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double findTransformECC(Mat templateImage, Mat inputImage, Mat warpMatrix, int motionType, in (double type, double maxCount, double epsilon) criteria, Mat inputMask)
        {
            if (templateImage != null) templateImage.ThrowIfDisposed();
            if (inputImage != null) inputImage.ThrowIfDisposed();
            if (warpMatrix != null) warpMatrix.ThrowIfDisposed();
            if (inputMask != null) inputMask.ThrowIfDisposed();

            return video_Video_findTransformECC_11(templateImage.nativeObj, inputImage.nativeObj, warpMatrix.nativeObj, motionType, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon, inputMask.nativeObj);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double findTransformECC(Mat templateImage, Mat inputImage, Mat warpMatrix, int motionType, in (double type, double maxCount, double epsilon) criteria)
        {
            if (templateImage != null) templateImage.ThrowIfDisposed();
            if (inputImage != null) inputImage.ThrowIfDisposed();
            if (warpMatrix != null) warpMatrix.ThrowIfDisposed();

            return video_Video_findTransformECC_12(templateImage.nativeObj, inputImage.nativeObj, warpMatrix.nativeObj, motionType, (int)criteria.type, (int)criteria.maxCount, criteria.epsilon);


        }

    }
}
