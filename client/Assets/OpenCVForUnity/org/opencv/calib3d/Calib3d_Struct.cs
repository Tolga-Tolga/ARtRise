
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Calib3dModule
{
    public partial class Calib3d
    {


        //
        // C++:  void cv::solvePnPRefineLM(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, TermCriteria criteria = TermCriteria(TermCriteria::EPS + TermCriteria::COUNT, 20, FLT_EPSILON))
        //

        /// <summary>
        ///  Refine a pose (the translation and the rotation that transform a 3D point expressed in the object coordinate frame
        ///  to the camera coordinate frame) from a 3D-2D point correspondences and starting from an initial solution.
        /// </summary>
        /// <remarks>
        ///  @see @ref calib3d_solvePnP
        /// </remarks>
        /// <param name="objectPoints">
        /// Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel,
        ///  where N is the number of points. vector&lt;Point3d&gt; can also be passed here.
        /// </param>
        /// <param name="imagePoints">
        /// Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel,
        ///  where N is the number of points. vector&lt;Point2d&gt; can also be passed here.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix \f$\cameramatrix{A}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="rvec">
        /// Input/Output rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///  the model coordinate system to the camera coordinate system. Input values are used as an initial solution.
        /// </param>
        /// <param name="tvec">
        /// Input/Output translation vector. Input values are used as an initial solution.
        /// </param>
        /// <param name="criteria">
        /// Criteria when to stop the Levenberg-Marquard iterative algorithm.
        /// </param>
        /// <remarks>
        ///  The function refines the object pose given at least 3 object points, their corresponding image
        ///  projections, an initial solution for the rotation and translation vector,
        ///  as well as the camera intrinsic matrix and the distortion coefficients.
        ///  The function minimizes the projection error with respect to the rotation and the translation vectors, according
        ///  to a Levenberg-Marquardt iterative minimization @cite Madsen04 @cite Eade13 process.
        /// </remarks>
        public static void solvePnPRefineLM(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, in Vec3d criteria)
        {
            if (objectPoints != null) objectPoints.ThrowIfDisposed();
            if (imagePoints != null) imagePoints.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            calib3d_Calib3d_solvePnPRefineLM_10(objectPoints.nativeObj, imagePoints.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  void cv::solvePnPRefineVVS(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, TermCriteria criteria = TermCriteria(TermCriteria::EPS + TermCriteria::COUNT, 20, FLT_EPSILON), double VVSlambda = 1)
        //

        /// <summary>
        ///  Refine a pose (the translation and the rotation that transform a 3D point expressed in the object coordinate frame
        ///  to the camera coordinate frame) from a 3D-2D point correspondences and starting from an initial solution.
        /// </summary>
        /// <remarks>
        ///  @see @ref calib3d_solvePnP
        /// </remarks>
        /// <param name="objectPoints">
        /// Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel,
        ///  where N is the number of points. vector&lt;Point3d&gt; can also be passed here.
        /// </param>
        /// <param name="imagePoints">
        /// Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel,
        ///  where N is the number of points. vector&lt;Point2d&gt; can also be passed here.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix \f$\cameramatrix{A}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="rvec">
        /// Input/Output rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///  the model coordinate system to the camera coordinate system. Input values are used as an initial solution.
        /// </param>
        /// <param name="tvec">
        /// Input/Output translation vector. Input values are used as an initial solution.
        /// </param>
        /// <param name="criteria">
        /// Criteria when to stop the Levenberg-Marquard iterative algorithm.
        /// </param>
        /// <param name="VVSlambda">
        /// Gain for the virtual visual servoing control law, equivalent to the \f$\alpha\f$
        ///  gain in the Damped Gauss-Newton formulation.
        /// </param>
        /// <remarks>
        ///  The function refines the object pose given at least 3 object points, their corresponding image
        ///  projections, an initial solution for the rotation and translation vector,
        ///  as well as the camera intrinsic matrix and the distortion coefficients.
        ///  The function minimizes the projection error with respect to the rotation and the translation vectors, using a
        ///  virtual visual servoing (VVS) @cite Chaumette06 @cite Marchand16 scheme.
        /// </remarks>
        public static void solvePnPRefineVVS(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, in Vec3d criteria, double VVSlambda)
        {
            if (objectPoints != null) objectPoints.ThrowIfDisposed();
            if (imagePoints != null) imagePoints.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            calib3d_Calib3d_solvePnPRefineVVS_10(objectPoints.nativeObj, imagePoints.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3, VVSlambda);


        }

        /// <summary>
        ///  Refine a pose (the translation and the rotation that transform a 3D point expressed in the object coordinate frame
        ///  to the camera coordinate frame) from a 3D-2D point correspondences and starting from an initial solution.
        /// </summary>
        /// <remarks>
        ///  @see @ref calib3d_solvePnP
        /// </remarks>
        /// <param name="objectPoints">
        /// Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel,
        ///  where N is the number of points. vector&lt;Point3d&gt; can also be passed here.
        /// </param>
        /// <param name="imagePoints">
        /// Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel,
        ///  where N is the number of points. vector&lt;Point2d&gt; can also be passed here.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix \f$\cameramatrix{A}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="rvec">
        /// Input/Output rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///  the model coordinate system to the camera coordinate system. Input values are used as an initial solution.
        /// </param>
        /// <param name="tvec">
        /// Input/Output translation vector. Input values are used as an initial solution.
        /// </param>
        /// <param name="criteria">
        /// Criteria when to stop the Levenberg-Marquard iterative algorithm.
        /// </param>
        /// <param name="VVSlambda">
        /// Gain for the virtual visual servoing control law, equivalent to the \f$\alpha\f$
        ///  gain in the Damped Gauss-Newton formulation.
        /// </param>
        /// <remarks>
        ///  The function refines the object pose given at least 3 object points, their corresponding image
        ///  projections, an initial solution for the rotation and translation vector,
        ///  as well as the camera intrinsic matrix and the distortion coefficients.
        ///  The function minimizes the projection error with respect to the rotation and the translation vectors, using a
        ///  virtual visual servoing (VVS) @cite Chaumette06 @cite Marchand16 scheme.
        /// </remarks>
        public static void solvePnPRefineVVS(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, in Vec3d criteria)
        {
            if (objectPoints != null) objectPoints.ThrowIfDisposed();
            if (imagePoints != null) imagePoints.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            calib3d_Calib3d_solvePnPRefineVVS_11(objectPoints.nativeObj, imagePoints.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  Mat cv::initCameraMatrix2D(vector_vector_Point3f objectPoints, vector_vector_Point2f imagePoints, Size imageSize, double aspectRatio = 1.0)
        //

        /// <summary>
        ///  Finds an initial camera intrinsic matrix from 3D-2D point correspondences.
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points in the calibration pattern
        ///  coordinate space. In the old interface all the per-view vectors are concatenated. See
        ///  #calibrateCamera for details.
        /// </param>
        /// <param name="imagePoints">
        /// Vector of vectors of the projections of the calibration pattern points. In the
        ///  old interface all the per-view vectors are concatenated.
        /// </param>
        /// <param name="imageSize">
        /// Image size in pixels used to initialize the principal point.
        /// </param>
        /// <param name="aspectRatio">
        /// If it is zero or negative, both \f$f_x\f$ and \f$f_y\f$ are estimated independently.
        ///  Otherwise, \f$f_x = f_y \cdot \texttt{aspectRatio}\f$ .
        /// </param>
        /// <remarks>
        ///  The function estimates and returns an initial camera intrinsic matrix for the camera calibration process.
        ///  Currently, the function only supports planar calibration patterns, which are patterns where each
        ///  object point has z-coordinate =0.
        /// </remarks>
        public static Mat initCameraMatrix2D(List<MatOfPoint3f> objectPoints, List<MatOfPoint2f> imagePoints, in Vec2d imageSize, double aspectRatio)
        {

            using Mat objectPoints_mat = Converters.vector_vector_Point3f_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_vector_Point2f_to_Mat(imagePoints);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_initCameraMatrix2D_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, aspectRatio)));


        }

        /// <summary>
        ///  Finds an initial camera intrinsic matrix from 3D-2D point correspondences.
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points in the calibration pattern
        ///  coordinate space. In the old interface all the per-view vectors are concatenated. See
        ///  #calibrateCamera for details.
        /// </param>
        /// <param name="imagePoints">
        /// Vector of vectors of the projections of the calibration pattern points. In the
        ///  old interface all the per-view vectors are concatenated.
        /// </param>
        /// <param name="imageSize">
        /// Image size in pixels used to initialize the principal point.
        /// </param>
        /// <param name="aspectRatio">
        /// If it is zero or negative, both \f$f_x\f$ and \f$f_y\f$ are estimated independently.
        ///  Otherwise, \f$f_x = f_y \cdot \texttt{aspectRatio}\f$ .
        /// </param>
        /// <remarks>
        ///  The function estimates and returns an initial camera intrinsic matrix for the camera calibration process.
        ///  Currently, the function only supports planar calibration patterns, which are patterns where each
        ///  object point has z-coordinate =0.
        /// </remarks>
        public static Mat initCameraMatrix2D(List<MatOfPoint3f> objectPoints, List<MatOfPoint2f> imagePoints, in Vec2d imageSize)
        {

            using Mat objectPoints_mat = Converters.vector_vector_Point3f_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_vector_Point2f_to_Mat(imagePoints);
            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_initCameraMatrix2D_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2)));


        }


        //
        // C++:  bool cv::findChessboardCorners(Mat image, Size patternSize, vector_Point2f& corners, int flags = CALIB_CB_ADAPTIVE_THRESH + CALIB_CB_NORMALIZE_IMAGE)
        //

        /// <summary>
        ///  Finds the positions of internal corners of the chessboard.
        /// </summary>
        /// <param name="image">
        /// Source chessboard view. It must be an 8-bit grayscale or color image.
        /// </param>
        /// <param name="patternSize">
        /// Number of inner corners per a chessboard row and column
        ///  ( patternSize = cv::Size(points_per_row,points_per_colum) = cv::Size(columns,rows) ).
        /// </param>
        /// <param name="corners">
        /// Output array of detected corners.
        /// </param>
        /// <param name="flags">
        /// Various operation flags that can be zero or a combination of the following values:
        ///  -   @ref CALIB_CB_ADAPTIVE_THRESH Use adaptive thresholding to convert the image to black
        ///  and white, rather than a fixed threshold level (computed from the average image brightness).
        ///  -   @ref CALIB_CB_NORMALIZE_IMAGE Normalize the image gamma with #equalizeHist before
        ///  applying fixed or adaptive thresholding.
        ///  -   @ref CALIB_CB_FILTER_QUADS Use additional criteria (like contour area, perimeter,
        ///  square-like shape) to filter out false quads extracted at the contour retrieval stage.
        ///  -   @ref CALIB_CB_FAST_CHECK Run a fast check on the image that looks for chessboard corners,
        ///  and shortcut the call if none is found. This can drastically speed up the call in the
        ///  degenerate condition when no chessboard is observed.
        ///  -   @ref CALIB_CB_PLAIN All other flags are ignored. The input image is taken as is.
        ///  No image processing is done to improve to find the checkerboard. This has the effect of speeding up the
        ///  execution of the function but could lead to not recognizing the checkerboard if the image
        ///  is not previously binarized in the appropriate manner.
        /// </param>
        /// <remarks>
        ///  The function attempts to determine whether the input image is a view of the chessboard pattern and
        ///  locate the internal chessboard corners. The function returns a non-zero value if all of the corners
        ///  are found and they are placed in a certain order (row by row, left to right in every row).
        ///  Otherwise, if the function fails to find all the corners or reorder them, it returns 0. For example,
        ///  a regular chessboard has 8 x 8 squares and 7 x 7 internal corners, that is, points where the black
        ///  squares touch each other. The detected coordinates are approximate, and to determine their positions
        ///  more accurately, the function calls #cornerSubPix. You also may use the function #cornerSubPix with
        ///  different parameters if returned coordinates are not accurate enough.
        ///  
        ///  Sample usage of detecting and drawing chessboard corners: :
        /// </remarks>
        /// <code language="c++">
        ///      Size patternsize(8,6); //interior number of corners
        ///      Mat gray = ....; //source image
        ///      vector<Point2f> corners; //this will be filled by the detected corners
        ///  
        ///      //CALIB_CB_FAST_CHECK saves a lot of time on images
        ///      //that do not contain any chessboard corners
        ///      bool patternfound = findChessboardCorners(gray, patternsize, corners,
        ///              CALIB_CB_ADAPTIVE_THRESH + CALIB_CB_NORMALIZE_IMAGE
        ///              + CALIB_CB_FAST_CHECK);
        ///  
        ///      if(patternfound)
        ///        cornerSubPix(gray, corners, Size(11, 11), Size(-1, -1),
        ///          TermCriteria(CV_TERMCRIT_EPS + CV_TERMCRIT_ITER, 30, 0.1));
        ///  
        ///      drawChessboardCorners(img, patternsize, Mat(corners), patternfound);
        /// </code>
        /// <remarks>
        ///  @note The function requires white space (like a square-thick border, the wider the better) around
        ///  the board to make the detection more robust in various environments. Otherwise, if there is no
        ///  border and the background is dark, the outer black squares cannot be segmented properly and so the
        ///  square grouping and ordering algorithm fails.
        ///  
        ///  Use the `gen_pattern.py` Python script (@ref tutorial_camera_calibration_pattern)
        ///  to create the desired checkerboard pattern.
        /// </remarks>
        public static bool findChessboardCorners(Mat image, in Vec2d patternSize, MatOfPoint2f corners, int flags)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();
            Mat corners_mat = corners;
            return calib3d_Calib3d_findChessboardCorners_10(image.nativeObj, patternSize.Item1, patternSize.Item2, corners_mat.nativeObj, flags);


        }

        /// <summary>
        ///  Finds the positions of internal corners of the chessboard.
        /// </summary>
        /// <param name="image">
        /// Source chessboard view. It must be an 8-bit grayscale or color image.
        /// </param>
        /// <param name="patternSize">
        /// Number of inner corners per a chessboard row and column
        ///  ( patternSize = cv::Size(points_per_row,points_per_colum) = cv::Size(columns,rows) ).
        /// </param>
        /// <param name="corners">
        /// Output array of detected corners.
        /// </param>
        /// <param name="flags">
        /// Various operation flags that can be zero or a combination of the following values:
        ///  -   @ref CALIB_CB_ADAPTIVE_THRESH Use adaptive thresholding to convert the image to black
        ///  and white, rather than a fixed threshold level (computed from the average image brightness).
        ///  -   @ref CALIB_CB_NORMALIZE_IMAGE Normalize the image gamma with #equalizeHist before
        ///  applying fixed or adaptive thresholding.
        ///  -   @ref CALIB_CB_FILTER_QUADS Use additional criteria (like contour area, perimeter,
        ///  square-like shape) to filter out false quads extracted at the contour retrieval stage.
        ///  -   @ref CALIB_CB_FAST_CHECK Run a fast check on the image that looks for chessboard corners,
        ///  and shortcut the call if none is found. This can drastically speed up the call in the
        ///  degenerate condition when no chessboard is observed.
        ///  -   @ref CALIB_CB_PLAIN All other flags are ignored. The input image is taken as is.
        ///  No image processing is done to improve to find the checkerboard. This has the effect of speeding up the
        ///  execution of the function but could lead to not recognizing the checkerboard if the image
        ///  is not previously binarized in the appropriate manner.
        /// </param>
        /// <remarks>
        ///  The function attempts to determine whether the input image is a view of the chessboard pattern and
        ///  locate the internal chessboard corners. The function returns a non-zero value if all of the corners
        ///  are found and they are placed in a certain order (row by row, left to right in every row).
        ///  Otherwise, if the function fails to find all the corners or reorder them, it returns 0. For example,
        ///  a regular chessboard has 8 x 8 squares and 7 x 7 internal corners, that is, points where the black
        ///  squares touch each other. The detected coordinates are approximate, and to determine their positions
        ///  more accurately, the function calls #cornerSubPix. You also may use the function #cornerSubPix with
        ///  different parameters if returned coordinates are not accurate enough.
        ///  
        ///  Sample usage of detecting and drawing chessboard corners: :
        /// </remarks>
        /// <code language="c++">
        ///      Size patternsize(8,6); //interior number of corners
        ///      Mat gray = ....; //source image
        ///      vector<Point2f> corners; //this will be filled by the detected corners
        ///  
        ///      //CALIB_CB_FAST_CHECK saves a lot of time on images
        ///      //that do not contain any chessboard corners
        ///      bool patternfound = findChessboardCorners(gray, patternsize, corners,
        ///              CALIB_CB_ADAPTIVE_THRESH + CALIB_CB_NORMALIZE_IMAGE
        ///              + CALIB_CB_FAST_CHECK);
        ///  
        ///      if(patternfound)
        ///        cornerSubPix(gray, corners, Size(11, 11), Size(-1, -1),
        ///          TermCriteria(CV_TERMCRIT_EPS + CV_TERMCRIT_ITER, 30, 0.1));
        ///  
        ///      drawChessboardCorners(img, patternsize, Mat(corners), patternfound);
        /// </code>
        /// <remarks>
        ///  @note The function requires white space (like a square-thick border, the wider the better) around
        ///  the board to make the detection more robust in various environments. Otherwise, if there is no
        ///  border and the background is dark, the outer black squares cannot be segmented properly and so the
        ///  square grouping and ordering algorithm fails.
        ///  
        ///  Use the `gen_pattern.py` Python script (@ref tutorial_camera_calibration_pattern)
        ///  to create the desired checkerboard pattern.
        /// </remarks>
        public static bool findChessboardCorners(Mat image, in Vec2d patternSize, MatOfPoint2f corners)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();
            Mat corners_mat = corners;
            return calib3d_Calib3d_findChessboardCorners_11(image.nativeObj, patternSize.Item1, patternSize.Item2, corners_mat.nativeObj);


        }


        //
        // C++:  bool cv::checkChessboard(Mat img, Size size)
        //

        public static bool checkChessboard(Mat img, in Vec2d size)
        {
            if (img != null) img.ThrowIfDisposed();

            return calib3d_Calib3d_checkChessboard_10(img.nativeObj, size.Item1, size.Item2);


        }


        //
        // C++:  bool cv::findChessboardCornersSB(Mat image, Size patternSize, Mat& corners, int flags, Mat& meta)
        //

        /// <summary>
        ///  Finds the positions of internal corners of the chessboard using a sector based approach.
        /// </summary>
        /// <param name="image">
        /// Source chessboard view. It must be an 8-bit grayscale or color image.
        /// </param>
        /// <param name="patternSize">
        /// Number of inner corners per a chessboard row and column
        ///  ( patternSize = cv::Size(points_per_row,points_per_colum) = cv::Size(columns,rows) ).
        /// </param>
        /// <param name="corners">
        /// Output array of detected corners.
        /// </param>
        /// <param name="flags">
        /// Various operation flags that can be zero or a combination of the following values:
        ///  -   @ref CALIB_CB_NORMALIZE_IMAGE Normalize the image gamma with equalizeHist before detection.
        ///  -   @ref CALIB_CB_EXHAUSTIVE Run an exhaustive search to improve detection rate.
        ///  -   @ref CALIB_CB_ACCURACY Up sample input image to improve sub-pixel accuracy due to aliasing effects.
        ///  -   @ref CALIB_CB_LARGER The detected pattern is allowed to be larger than patternSize (see description).
        ///  -   @ref CALIB_CB_MARKER The detected pattern must have a marker (see description).
        ///  This should be used if an accurate camera calibration is required.
        /// </param>
        /// <param name="meta">
        /// Optional output arrray of detected corners (CV_8UC1 and size = cv::Size(columns,rows)).
        ///  Each entry stands for one corner of the pattern and can have one of the following values:
        ///  -   0 = no meta data attached
        ///  -   1 = left-top corner of a black cell
        ///  -   2 = left-top corner of a white cell
        ///  -   3 = left-top corner of a black cell with a white marker dot
        ///  -   4 = left-top corner of a white cell with a black marker dot (pattern origin in case of markers otherwise first corner)
        /// </param>
        /// <remarks>
        ///  The function is analog to #findChessboardCorners but uses a localized radon
        ///  transformation approximated by box filters being more robust to all sort of
        ///  noise, faster on larger images and is able to directly return the sub-pixel
        ///  position of the internal chessboard corners. The Method is based on the paper
        ///  @cite duda2018 "Accurate Detection and Localization of Checkerboard Corners for
        ///  Calibration" demonstrating that the returned sub-pixel positions are more
        ///  accurate than the one returned by cornerSubPix allowing a precise camera
        ///  calibration for demanding applications.
        ///  
        ///  In the case, the flags @ref CALIB_CB_LARGER or @ref CALIB_CB_MARKER are given,
        ///  the result can be recovered from the optional meta array. Both flags are
        ///  helpful to use calibration patterns exceeding the field of view of the camera.
        ///  These oversized patterns allow more accurate calibrations as corners can be
        ///  utilized, which are as close as possible to the image borders.  For a
        ///  consistent coordinate system across all images, the optional marker (see image
        ///  below) can be used to move the origin of the board to the location where the
        ///  black circle is located.
        ///  
        ///  @note The function requires a white boarder with roughly the same width as one
        ///  of the checkerboard fields around the whole board to improve the detection in
        ///  various environments. In addition, because of the localized radon
        ///  transformation it is beneficial to use round corners for the field corners
        ///  which are located on the outside of the board. The following figure illustrates
        ///  a sample checkerboard optimized for the detection. However, any other checkerboard
        ///  can be used as well.
        ///  
        ///  Use the `gen_pattern.py` Python script (@ref tutorial_camera_calibration_pattern)
        ///  to create the corresponding checkerboard pattern:
        ///  \image html pics/checkerboard_radon.png width=60%
        /// </remarks>
        public static bool findChessboardCornersSBWithMeta(Mat image, in Vec2d patternSize, Mat corners, int flags, Mat meta)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();
            if (meta != null) meta.ThrowIfDisposed();

            return calib3d_Calib3d_findChessboardCornersSBWithMeta_10(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, flags, meta.nativeObj);


        }


        //
        // C++:  bool cv::findChessboardCornersSB(Mat image, Size patternSize, Mat& corners, int flags = 0)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static bool findChessboardCornersSB(Mat image, in Vec2d patternSize, Mat corners, int flags)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            return calib3d_Calib3d_findChessboardCornersSB_10(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, flags);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static bool findChessboardCornersSB(Mat image, in Vec2d patternSize, Mat corners)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            return calib3d_Calib3d_findChessboardCornersSB_11(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj);


        }


        //
        // C++:  Scalar cv::estimateChessboardSharpness(Mat image, Size patternSize, Mat corners, float rise_distance = 0.8F, bool vertical = false, Mat& sharpness = Mat())
        //

        /// <summary>
        ///  Estimates the sharpness of a detected chessboard.
        /// </summary>
        /// <remarks>
        ///  Image sharpness, as well as brightness, are a critical parameter for accuracte
        ///  camera calibration. For accessing these parameters for filtering out
        ///  problematic calibraiton images, this method calculates edge profiles by traveling from
        ///  black to white chessboard cell centers. Based on this, the number of pixels is
        ///  calculated required to transit from black to white. This width of the
        ///  transition area is a good indication of how sharp the chessboard is imaged
        ///  and should be below ~3.0 pixels.
        /// </remarks>
        /// <param name="image">
        /// Gray image used to find chessboard corners
        /// </param>
        /// <param name="patternSize">
        /// Size of a found chessboard pattern
        /// </param>
        /// <param name="corners">
        /// Corners found by #findChessboardCornersSB
        /// </param>
        /// <param name="rise_distance">
        /// Rise distance 0.8 means 10% ... 90% of the final signal strength
        /// </param>
        /// <param name="vertical">
        /// By default edge responses for horizontal lines are calculated
        /// </param>
        /// <param name="sharpness">
        /// Optional output array with a sharpness value for calculated edge responses (see description)
        /// </param>
        /// <remarks>
        ///  The optional sharpness array is of type CV_32FC1 and has for each calculated
        ///  profile one row with the following five entries:
        ///    0 = x coordinate of the underlying edge in the image
        ///    1 = y coordinate of the underlying edge in the image
        ///    2 = width of the transition area (sharpness)
        ///    3 = signal strength in the black cell (min brightness)
        ///    4 = signal strength in the white cell (max brightness)
        /// </remarks>
        /// <returns>
        ///  Scalar(average sharpness, average min brightness, average max brightness,0)
        /// </returns>
        public static Vec4d estimateChessboardSharpnessAsVec4d(Mat image, in Vec2d patternSize, Mat corners, float rise_distance, bool vertical, Mat sharpness)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();
            if (sharpness != null) sharpness.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_Calib3d_estimateChessboardSharpness_10(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, rise_distance, vertical, sharpness.nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }

        /// <summary>
        ///  Estimates the sharpness of a detected chessboard.
        /// </summary>
        /// <remarks>
        ///  Image sharpness, as well as brightness, are a critical parameter for accuracte
        ///  camera calibration. For accessing these parameters for filtering out
        ///  problematic calibraiton images, this method calculates edge profiles by traveling from
        ///  black to white chessboard cell centers. Based on this, the number of pixels is
        ///  calculated required to transit from black to white. This width of the
        ///  transition area is a good indication of how sharp the chessboard is imaged
        ///  and should be below ~3.0 pixels.
        /// </remarks>
        /// <param name="image">
        /// Gray image used to find chessboard corners
        /// </param>
        /// <param name="patternSize">
        /// Size of a found chessboard pattern
        /// </param>
        /// <param name="corners">
        /// Corners found by #findChessboardCornersSB
        /// </param>
        /// <param name="rise_distance">
        /// Rise distance 0.8 means 10% ... 90% of the final signal strength
        /// </param>
        /// <param name="vertical">
        /// By default edge responses for horizontal lines are calculated
        /// </param>
        /// <param name="sharpness">
        /// Optional output array with a sharpness value for calculated edge responses (see description)
        /// </param>
        /// <remarks>
        ///  The optional sharpness array is of type CV_32FC1 and has for each calculated
        ///  profile one row with the following five entries:
        ///    0 = x coordinate of the underlying edge in the image
        ///    1 = y coordinate of the underlying edge in the image
        ///    2 = width of the transition area (sharpness)
        ///    3 = signal strength in the black cell (min brightness)
        ///    4 = signal strength in the white cell (max brightness)
        /// </remarks>
        /// <returns>
        ///  Scalar(average sharpness, average min brightness, average max brightness,0)
        /// </returns>
        public static Vec4d estimateChessboardSharpnessAsVec4d(Mat image, in Vec2d patternSize, Mat corners, float rise_distance, bool vertical)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_Calib3d_estimateChessboardSharpness_11(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, rise_distance, vertical, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }

        /// <summary>
        ///  Estimates the sharpness of a detected chessboard.
        /// </summary>
        /// <remarks>
        ///  Image sharpness, as well as brightness, are a critical parameter for accuracte
        ///  camera calibration. For accessing these parameters for filtering out
        ///  problematic calibraiton images, this method calculates edge profiles by traveling from
        ///  black to white chessboard cell centers. Based on this, the number of pixels is
        ///  calculated required to transit from black to white. This width of the
        ///  transition area is a good indication of how sharp the chessboard is imaged
        ///  and should be below ~3.0 pixels.
        /// </remarks>
        /// <param name="image">
        /// Gray image used to find chessboard corners
        /// </param>
        /// <param name="patternSize">
        /// Size of a found chessboard pattern
        /// </param>
        /// <param name="corners">
        /// Corners found by #findChessboardCornersSB
        /// </param>
        /// <param name="rise_distance">
        /// Rise distance 0.8 means 10% ... 90% of the final signal strength
        /// </param>
        /// <param name="vertical">
        /// By default edge responses for horizontal lines are calculated
        /// </param>
        /// <param name="sharpness">
        /// Optional output array with a sharpness value for calculated edge responses (see description)
        /// </param>
        /// <remarks>
        ///  The optional sharpness array is of type CV_32FC1 and has for each calculated
        ///  profile one row with the following five entries:
        ///    0 = x coordinate of the underlying edge in the image
        ///    1 = y coordinate of the underlying edge in the image
        ///    2 = width of the transition area (sharpness)
        ///    3 = signal strength in the black cell (min brightness)
        ///    4 = signal strength in the white cell (max brightness)
        /// </remarks>
        /// <returns>
        ///  Scalar(average sharpness, average min brightness, average max brightness,0)
        /// </returns>
        public static Vec4d estimateChessboardSharpnessAsVec4d(Mat image, in Vec2d patternSize, Mat corners, float rise_distance)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_Calib3d_estimateChessboardSharpness_12(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, rise_distance, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }

        /// <summary>
        ///  Estimates the sharpness of a detected chessboard.
        /// </summary>
        /// <remarks>
        ///  Image sharpness, as well as brightness, are a critical parameter for accuracte
        ///  camera calibration. For accessing these parameters for filtering out
        ///  problematic calibraiton images, this method calculates edge profiles by traveling from
        ///  black to white chessboard cell centers. Based on this, the number of pixels is
        ///  calculated required to transit from black to white. This width of the
        ///  transition area is a good indication of how sharp the chessboard is imaged
        ///  and should be below ~3.0 pixels.
        /// </remarks>
        /// <param name="image">
        /// Gray image used to find chessboard corners
        /// </param>
        /// <param name="patternSize">
        /// Size of a found chessboard pattern
        /// </param>
        /// <param name="corners">
        /// Corners found by #findChessboardCornersSB
        /// </param>
        /// <param name="rise_distance">
        /// Rise distance 0.8 means 10% ... 90% of the final signal strength
        /// </param>
        /// <param name="vertical">
        /// By default edge responses for horizontal lines are calculated
        /// </param>
        /// <param name="sharpness">
        /// Optional output array with a sharpness value for calculated edge responses (see description)
        /// </param>
        /// <remarks>
        ///  The optional sharpness array is of type CV_32FC1 and has for each calculated
        ///  profile one row with the following five entries:
        ///    0 = x coordinate of the underlying edge in the image
        ///    1 = y coordinate of the underlying edge in the image
        ///    2 = width of the transition area (sharpness)
        ///    3 = signal strength in the black cell (min brightness)
        ///    4 = signal strength in the white cell (max brightness)
        /// </remarks>
        /// <returns>
        ///  Scalar(average sharpness, average min brightness, average max brightness,0)
        /// </returns>
        public static Vec4d estimateChessboardSharpnessAsVec4d(Mat image, in Vec2d patternSize, Mat corners)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_Calib3d_estimateChessboardSharpness_13(image.nativeObj, patternSize.Item1, patternSize.Item2, corners.nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++:  bool cv::find4QuadCornerSubpix(Mat img, Mat& corners, Size region_size)
        //

        public static bool find4QuadCornerSubpix(Mat img, Mat corners, in Vec2d region_size)
        {
            if (img != null) img.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();

            return calib3d_Calib3d_find4QuadCornerSubpix_10(img.nativeObj, corners.nativeObj, region_size.Item1, region_size.Item2);


        }


        //
        // C++:  void cv::drawChessboardCorners(Mat& image, Size patternSize, vector_Point2f corners, bool patternWasFound)
        //

        /// <summary>
        ///  Renders the detected chessboard corners.
        /// </summary>
        /// <param name="image">
        /// Destination image. It must be an 8-bit color image.
        /// </param>
        /// <param name="patternSize">
        /// Number of inner corners per a chessboard row and column
        ///  (patternSize = cv::Size(points_per_row,points_per_column)).
        /// </param>
        /// <param name="corners">
        /// Array of detected corners, the output of #findChessboardCorners.
        /// </param>
        /// <param name="patternWasFound">
        /// Parameter indicating whether the complete board was found or not. The
        ///  return value of #findChessboardCorners should be passed here.
        /// </param>
        /// <remarks>
        ///  The function draws individual chessboard corners detected either as red circles if the board was not
        ///  found, or as colored corners connected with lines if the board was found.
        /// </remarks>
        public static void drawChessboardCorners(Mat image, in Vec2d patternSize, MatOfPoint2f corners, bool patternWasFound)
        {
            if (image != null) image.ThrowIfDisposed();
            if (corners != null) corners.ThrowIfDisposed();
            Mat corners_mat = corners;
            calib3d_Calib3d_drawChessboardCorners_10(image.nativeObj, patternSize.Item1, patternSize.Item2, corners_mat.nativeObj, patternWasFound);


        }


        //
        // C++:  bool cv::findCirclesGrid(Mat image, Size patternSize, Mat& centers, int flags, Ptr_FeatureDetector blobDetector, CirclesGridFinderParameters parameters)
        //

        // Unknown type 'Ptr_FeatureDetector' (I), skipping the function


        //
        // C++:  bool cv::findCirclesGrid(Mat image, Size patternSize, Mat& centers, int flags = CALIB_CB_SYMMETRIC_GRID, Ptr_FeatureDetector blobDetector = SimpleBlobDetector::create())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static bool findCirclesGrid(Mat image, in Vec2d patternSize, Mat centers, int flags)
        {
            if (image != null) image.ThrowIfDisposed();
            if (centers != null) centers.ThrowIfDisposed();

            return calib3d_Calib3d_findCirclesGrid_10(image.nativeObj, patternSize.Item1, patternSize.Item2, centers.nativeObj, flags);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static bool findCirclesGrid(Mat image, in Vec2d patternSize, Mat centers)
        {
            if (image != null) image.ThrowIfDisposed();
            if (centers != null) centers.ThrowIfDisposed();

            return calib3d_Calib3d_findCirclesGrid_12(image.nativeObj, patternSize.Item1, patternSize.Item2, centers.nativeObj);


        }


        //
        // C++:  double cv::calibrateCamera(vector_Mat objectPoints, vector_Mat imagePoints, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration
        ///  pattern.
        /// </summary>
        /// <param name="objectPoints">
        /// In the new interface it is a vector of vectors of calibration pattern points in
        ///  the calibration pattern coordinate space (e.g. std::vector&lt;std::vector&lt;cv::Vec3f&gt;&gt;). The outer
        ///  vector contains as many elements as the number of pattern views. If the same calibration pattern
        ///  is shown in each view and it is fully visible, all the vectors will be the same. Although, it is
        ///  possible to use partially occluded patterns or even different patterns in different views. Then,
        ///  the vectors will be different. Although the points are 3D, they all lie in the calibration pattern's
        ///  XY coordinate plane (thus 0 in the Z-coordinate), if the used calibration pattern is a planar rig.
        ///  In the old interface all the vectors of object points from different views are concatenated
        ///  together.
        /// </param>
        /// <param name="imagePoints">
        /// In the new interface it is a vector of vectors of the projections of calibration
        ///  pattern points (e.g. std::vector&lt;std::vector&lt;cv::Vec2f&gt;&gt;). imagePoints.size() and
        ///  objectPoints.size(), and imagePoints[i].size() and objectPoints[i].size() for each i, must be equal,
        ///  respectively. In the old interface all the vectors of object points from different views are
        ///  concatenated together.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input/output 3x3 floating-point camera intrinsic matrix
        ///  \f$\cameramatrix{A}\f$ . If @ref CALIB_USE_INTRINSIC_GUESS
        ///  and/or @ref CALIB_FIX_ASPECT_RATIO, @ref CALIB_FIX_PRINCIPAL_POINT or @ref CALIB_FIX_FOCAL_LENGTH
        ///  are specified, some or all of fx, fy, cx, cy must be initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Input/output vector of distortion coefficients
        ///  \f$\distcoeffs\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (@ref Rodrigues ) estimated for each pattern view
        ///  (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each i-th rotation vector together with the corresponding
        ///  i-th translation vector (see the next output parameter description) brings the calibration pattern
        ///  from the object coordinate space (in which object points are specified) to the camera coordinate
        ///  space. In more technical terms, the tuple of the i-th rotation and translation vector performs
        ///  a change of basis from object coordinate space to camera coordinate space. Due to its duality, this
        ///  tuple is equivalent to the position of the calibration pattern with respect to the camera coordinate
        ///  space.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter
        ///  describtion above.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic
        ///  parameters. Order of deviations values:
        ///  \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///   s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic
        ///  parameters. Order of deviations values: \f$(R_0, T_0, \dotsc , R_{M - 1}, T_{M - 1})\f$ where M is
        ///  the number of pattern views. \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS cameraMatrix contains valid initial values of
        ///  fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///  center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///  Note, that if intrinsic parameters are known, there is no need to use this function just to
        ///  estimate extrinsic parameters. Use @ref solvePnP instead.
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set too.
        ///  -   @ref CALIB_FIX_ASPECT_RATIO The functions consider only fy as a free parameter. The
        ///  ratio fx/fy stays the same as in the input cameraMatrix . When
        ///   @ref CALIB_USE_INTRINSIC_GUESS is not set, the actual input values of fx and fy are
        ///  ignored, only their ratio is computed and used further.
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Tangential distortion coefficients \f$(p_1, p_2)\f$ are set
        ///  to zeros and stay zero.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global optimization if
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 The corresponding radial distortion
        ///  coefficient is not changed during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is
        ///  set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Coefficients k4, k5, and k6 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the rational model and return 8 coefficients or more.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients or more.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000 and @cite BouguetMCT . The coordinates of 3D object
        ///  points and their corresponding 2D projections in each view must be specified. That may be achieved
        ///  by using an object with known geometry and easily detectable feature points. Such an object is
        ///  called a calibration rig or calibration pattern, and OpenCV has built-in support for a chessboard as
        ///  a calibration rig (see @ref findChessboardCorners). Currently, initialization of intrinsic
        ///  parameters (when @ref CALIB_USE_INTRINSIC_GUESS is not set) is only implemented for planar calibration
        ///  patterns (where Z-coordinates of the object points must be all zeros). 3D calibration rigs can also
        ///  be used as long as initial cameraMatrix is provided.
        ///  
        ///  The algorithm performs the following steps:
        ///  
        ///  -   Compute the initial intrinsic parameters (the option only available for planar calibration
        ///      patterns) or read them from the input parameters. The distortion coefficients are all set to
        ///      zeros initially unless some of CALIB_FIX_K? are specified.
        ///  
        ///  -   Estimate the initial camera pose as if the intrinsic parameters have been already known. This is
        ///      done using @ref solvePnP .
        ///  
        ///  -   Run the global Levenberg-Marquardt optimization algorithm to minimize the reprojection error,
        ///      that is, the total sum of squared distances between the observed feature points imagePoints and
        ///      the projected (using the current estimates for camera parameters and the poses) object points
        ///      objectPoints. See @ref projectPoints for details.
        ///  
        ///  @note
        ///      If you use a non-square (i.e. non-N-by-N) grid and @ref findChessboardCorners for calibration,
        ///      and @ref calibrateCamera returns bad values (zero distortion coefficients, \f$c_x\f$ and
        ///      \f$c_y\f$ very far from the image center, and/or large differences between \f$f_x\f$ and
        ///      \f$f_y\f$ (ratios of 10:1 or more)), then you are probably using patternSize=cvSize(rows,cols)
        ///      instead of using patternSize=cvSize(cols,rows) in @ref findChessboardCorners.
        ///  
        ///  @note
        ///      The function may throw exceptions, if unsupported combination of parameters is provided or
        ///      the system is underconstrained.
        ///  
        ///  @sa
        ///     calibrateCameraRO, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate,
        ///     undistort
        /// </remarks>
        public static double calibrateCameraExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraExtended_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration
        ///  pattern.
        /// </summary>
        /// <param name="objectPoints">
        /// In the new interface it is a vector of vectors of calibration pattern points in
        ///  the calibration pattern coordinate space (e.g. std::vector&lt;std::vector&lt;cv::Vec3f&gt;&gt;). The outer
        ///  vector contains as many elements as the number of pattern views. If the same calibration pattern
        ///  is shown in each view and it is fully visible, all the vectors will be the same. Although, it is
        ///  possible to use partially occluded patterns or even different patterns in different views. Then,
        ///  the vectors will be different. Although the points are 3D, they all lie in the calibration pattern's
        ///  XY coordinate plane (thus 0 in the Z-coordinate), if the used calibration pattern is a planar rig.
        ///  In the old interface all the vectors of object points from different views are concatenated
        ///  together.
        /// </param>
        /// <param name="imagePoints">
        /// In the new interface it is a vector of vectors of the projections of calibration
        ///  pattern points (e.g. std::vector&lt;std::vector&lt;cv::Vec2f&gt;&gt;). imagePoints.size() and
        ///  objectPoints.size(), and imagePoints[i].size() and objectPoints[i].size() for each i, must be equal,
        ///  respectively. In the old interface all the vectors of object points from different views are
        ///  concatenated together.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input/output 3x3 floating-point camera intrinsic matrix
        ///  \f$\cameramatrix{A}\f$ . If @ref CALIB_USE_INTRINSIC_GUESS
        ///  and/or @ref CALIB_FIX_ASPECT_RATIO, @ref CALIB_FIX_PRINCIPAL_POINT or @ref CALIB_FIX_FOCAL_LENGTH
        ///  are specified, some or all of fx, fy, cx, cy must be initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Input/output vector of distortion coefficients
        ///  \f$\distcoeffs\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (@ref Rodrigues ) estimated for each pattern view
        ///  (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each i-th rotation vector together with the corresponding
        ///  i-th translation vector (see the next output parameter description) brings the calibration pattern
        ///  from the object coordinate space (in which object points are specified) to the camera coordinate
        ///  space. In more technical terms, the tuple of the i-th rotation and translation vector performs
        ///  a change of basis from object coordinate space to camera coordinate space. Due to its duality, this
        ///  tuple is equivalent to the position of the calibration pattern with respect to the camera coordinate
        ///  space.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter
        ///  describtion above.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic
        ///  parameters. Order of deviations values:
        ///  \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///   s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic
        ///  parameters. Order of deviations values: \f$(R_0, T_0, \dotsc , R_{M - 1}, T_{M - 1})\f$ where M is
        ///  the number of pattern views. \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS cameraMatrix contains valid initial values of
        ///  fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///  center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///  Note, that if intrinsic parameters are known, there is no need to use this function just to
        ///  estimate extrinsic parameters. Use @ref solvePnP instead.
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set too.
        ///  -   @ref CALIB_FIX_ASPECT_RATIO The functions consider only fy as a free parameter. The
        ///  ratio fx/fy stays the same as in the input cameraMatrix . When
        ///   @ref CALIB_USE_INTRINSIC_GUESS is not set, the actual input values of fx and fy are
        ///  ignored, only their ratio is computed and used further.
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Tangential distortion coefficients \f$(p_1, p_2)\f$ are set
        ///  to zeros and stay zero.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global optimization if
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 The corresponding radial distortion
        ///  coefficient is not changed during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is
        ///  set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Coefficients k4, k5, and k6 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the rational model and return 8 coefficients or more.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients or more.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000 and @cite BouguetMCT . The coordinates of 3D object
        ///  points and their corresponding 2D projections in each view must be specified. That may be achieved
        ///  by using an object with known geometry and easily detectable feature points. Such an object is
        ///  called a calibration rig or calibration pattern, and OpenCV has built-in support for a chessboard as
        ///  a calibration rig (see @ref findChessboardCorners). Currently, initialization of intrinsic
        ///  parameters (when @ref CALIB_USE_INTRINSIC_GUESS is not set) is only implemented for planar calibration
        ///  patterns (where Z-coordinates of the object points must be all zeros). 3D calibration rigs can also
        ///  be used as long as initial cameraMatrix is provided.
        ///  
        ///  The algorithm performs the following steps:
        ///  
        ///  -   Compute the initial intrinsic parameters (the option only available for planar calibration
        ///      patterns) or read them from the input parameters. The distortion coefficients are all set to
        ///      zeros initially unless some of CALIB_FIX_K? are specified.
        ///  
        ///  -   Estimate the initial camera pose as if the intrinsic parameters have been already known. This is
        ///      done using @ref solvePnP .
        ///  
        ///  -   Run the global Levenberg-Marquardt optimization algorithm to minimize the reprojection error,
        ///      that is, the total sum of squared distances between the observed feature points imagePoints and
        ///      the projected (using the current estimates for camera parameters and the poses) object points
        ///      objectPoints. See @ref projectPoints for details.
        ///  
        ///  @note
        ///      If you use a non-square (i.e. non-N-by-N) grid and @ref findChessboardCorners for calibration,
        ///      and @ref calibrateCamera returns bad values (zero distortion coefficients, \f$c_x\f$ and
        ///      \f$c_y\f$ very far from the image center, and/or large differences between \f$f_x\f$ and
        ///      \f$f_y\f$ (ratios of 10:1 or more)), then you are probably using patternSize=cvSize(rows,cols)
        ///      instead of using patternSize=cvSize(cols,rows) in @ref findChessboardCorners.
        ///  
        ///  @note
        ///      The function may throw exceptions, if unsupported combination of parameters is provided or
        ///      the system is underconstrained.
        ///  
        ///  @sa
        ///     calibrateCameraRO, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate,
        ///     undistort
        /// </remarks>
        public static double calibrateCameraExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraExtended_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration
        ///  pattern.
        /// </summary>
        /// <param name="objectPoints">
        /// In the new interface it is a vector of vectors of calibration pattern points in
        ///  the calibration pattern coordinate space (e.g. std::vector&lt;std::vector&lt;cv::Vec3f&gt;&gt;). The outer
        ///  vector contains as many elements as the number of pattern views. If the same calibration pattern
        ///  is shown in each view and it is fully visible, all the vectors will be the same. Although, it is
        ///  possible to use partially occluded patterns or even different patterns in different views. Then,
        ///  the vectors will be different. Although the points are 3D, they all lie in the calibration pattern's
        ///  XY coordinate plane (thus 0 in the Z-coordinate), if the used calibration pattern is a planar rig.
        ///  In the old interface all the vectors of object points from different views are concatenated
        ///  together.
        /// </param>
        /// <param name="imagePoints">
        /// In the new interface it is a vector of vectors of the projections of calibration
        ///  pattern points (e.g. std::vector&lt;std::vector&lt;cv::Vec2f&gt;&gt;). imagePoints.size() and
        ///  objectPoints.size(), and imagePoints[i].size() and objectPoints[i].size() for each i, must be equal,
        ///  respectively. In the old interface all the vectors of object points from different views are
        ///  concatenated together.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input/output 3x3 floating-point camera intrinsic matrix
        ///  \f$\cameramatrix{A}\f$ . If @ref CALIB_USE_INTRINSIC_GUESS
        ///  and/or @ref CALIB_FIX_ASPECT_RATIO, @ref CALIB_FIX_PRINCIPAL_POINT or @ref CALIB_FIX_FOCAL_LENGTH
        ///  are specified, some or all of fx, fy, cx, cy must be initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Input/output vector of distortion coefficients
        ///  \f$\distcoeffs\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (@ref Rodrigues ) estimated for each pattern view
        ///  (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each i-th rotation vector together with the corresponding
        ///  i-th translation vector (see the next output parameter description) brings the calibration pattern
        ///  from the object coordinate space (in which object points are specified) to the camera coordinate
        ///  space. In more technical terms, the tuple of the i-th rotation and translation vector performs
        ///  a change of basis from object coordinate space to camera coordinate space. Due to its duality, this
        ///  tuple is equivalent to the position of the calibration pattern with respect to the camera coordinate
        ///  space.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter
        ///  describtion above.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic
        ///  parameters. Order of deviations values:
        ///  \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///   s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic
        ///  parameters. Order of deviations values: \f$(R_0, T_0, \dotsc , R_{M - 1}, T_{M - 1})\f$ where M is
        ///  the number of pattern views. \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS cameraMatrix contains valid initial values of
        ///  fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///  center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///  Note, that if intrinsic parameters are known, there is no need to use this function just to
        ///  estimate extrinsic parameters. Use @ref solvePnP instead.
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set too.
        ///  -   @ref CALIB_FIX_ASPECT_RATIO The functions consider only fy as a free parameter. The
        ///  ratio fx/fy stays the same as in the input cameraMatrix . When
        ///   @ref CALIB_USE_INTRINSIC_GUESS is not set, the actual input values of fx and fy are
        ///  ignored, only their ratio is computed and used further.
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Tangential distortion coefficients \f$(p_1, p_2)\f$ are set
        ///  to zeros and stay zero.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global optimization if
        ///   @ref CALIB_USE_INTRINSIC_GUESS is set.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 The corresponding radial distortion
        ///  coefficient is not changed during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is
        ///  set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Coefficients k4, k5, and k6 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the rational model and return 8 coefficients or more.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients or more.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000 and @cite BouguetMCT . The coordinates of 3D object
        ///  points and their corresponding 2D projections in each view must be specified. That may be achieved
        ///  by using an object with known geometry and easily detectable feature points. Such an object is
        ///  called a calibration rig or calibration pattern, and OpenCV has built-in support for a chessboard as
        ///  a calibration rig (see @ref findChessboardCorners). Currently, initialization of intrinsic
        ///  parameters (when @ref CALIB_USE_INTRINSIC_GUESS is not set) is only implemented for planar calibration
        ///  patterns (where Z-coordinates of the object points must be all zeros). 3D calibration rigs can also
        ///  be used as long as initial cameraMatrix is provided.
        ///  
        ///  The algorithm performs the following steps:
        ///  
        ///  -   Compute the initial intrinsic parameters (the option only available for planar calibration
        ///      patterns) or read them from the input parameters. The distortion coefficients are all set to
        ///      zeros initially unless some of CALIB_FIX_K? are specified.
        ///  
        ///  -   Estimate the initial camera pose as if the intrinsic parameters have been already known. This is
        ///      done using @ref solvePnP .
        ///  
        ///  -   Run the global Levenberg-Marquardt optimization algorithm to minimize the reprojection error,
        ///      that is, the total sum of squared distances between the observed feature points imagePoints and
        ///      the projected (using the current estimates for camera parameters and the poses) object points
        ///      objectPoints. See @ref projectPoints for details.
        ///  
        ///  @note
        ///      If you use a non-square (i.e. non-N-by-N) grid and @ref findChessboardCorners for calibration,
        ///      and @ref calibrateCamera returns bad values (zero distortion coefficients, \f$c_x\f$ and
        ///      \f$c_y\f$ very far from the image center, and/or large differences between \f$f_x\f$ and
        ///      \f$f_y\f$ (ratios of 10:1 or more)), then you are probably using patternSize=cvSize(rows,cols)
        ///      instead of using patternSize=cvSize(cols,rows) in @ref findChessboardCorners.
        ///  
        ///  @note
        ///      The function may throw exceptions, if unsupported combination of parameters is provided or
        ///      the system is underconstrained.
        ///  
        ///  @sa
        ///     calibrateCameraRO, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate,
        ///     undistort
        /// </remarks>
        public static double calibrateCameraExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraExtended_12(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::calibrateCamera(vector_Mat objectPoints, vector_Mat imagePoints, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCamera(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags, in Vec3d criteria)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCamera_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCamera(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCamera_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCamera(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCamera_12(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::calibrateCameraRO(vector_Mat objectPoints, vector_Mat imagePoints, Size imageSize, int iFixedPoint, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& newObjPoints, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& stdDeviationsObjPoints, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration pattern.
        /// </summary>
        /// <remarks>
        ///  This function is an extension of #calibrateCamera with the method of releasing object which was
        ///  proposed in @cite strobl2011iccv. In many common cases with inaccurate, unmeasured, roughly planar
        ///  targets (calibration plates), this method can dramatically improve the precision of the estimated
        ///  camera parameters. Both the object-releasing method and standard method are supported by this
        ///  function. Use the parameter **iFixedPoint** for method selection. In the internal implementation,
        ///  #calibrateCamera is a wrapper for this function.
        /// </remarks>
        /// <param name="objectPoints">
        /// Vector of vectors of calibration pattern points in the calibration pattern
        ///  coordinate space. See #calibrateCamera for details. If the method of releasing object to be used,
        ///  the identical calibration board must be used in each view and it must be fully visible, and all
        ///  objectPoints[i] must be the same and all points should be roughly close to a plane. **The calibration
        ///  target has to be rigid, or at least static if the camera (rather than the calibration target) is
        ///  shifted for grabbing images.**
        /// </param>
        /// <param name="imagePoints">
        /// Vector of vectors of the projections of calibration pattern points. See
        ///  #calibrateCamera for details.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="iFixedPoint">
        /// The index of the 3D object point in objectPoints[0] to be fixed. It also acts as
        ///  a switch for calibration method selection. If object-releasing method to be used, pass in the
        ///  parameter in the range of [1, objectPoints[0].size()-2], otherwise a value out of this range will
        ///  make standard calibration method selected. Usually the top-right corner point of the calibration
        ///  board grid is recommended to be fixed when object-releasing method being utilized. According to
        ///  \cite strobl2011iccv, two other points are also fixed. In this implementation, objectPoints[0].front
        ///  and objectPoints[0].back.z are used. With object-releasing method, accurate rvecs, tvecs and
        ///  newObjPoints are only possible if coordinates of these three fixed points are accurate enough.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix. See #calibrateCamera for details.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients. See #calibrateCamera for details.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors estimated for each pattern view. See #calibrateCamera
        ///  for details.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="newObjPoints">
        /// The updated output vector of calibration pattern points. The coordinates might
        ///  be scaled based on three fixed points. The returned coordinates are accurate only if the above
        ///  mentioned three fixed points are accurate. If not needed, noArray() can be passed in. This parameter
        ///  is ignored with standard calibration method.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsObjPoints">
        /// Output vector of standard deviations estimated for refined coordinates
        ///  of calibration pattern points. It has the same size and order as objectPoints[0] vector. This
        ///  parameter is ignored with standard calibration method.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of some predefined values. See
        ///  #calibrateCamera for details. If the method of releasing object is used, the calibration time may
        ///  be much longer. CALIB_USE_QR or CALIB_USE_LU could be used for faster calibration with potentially
        ///  less precise and less stable in some rare cases.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000, @cite BouguetMCT and @cite strobl2011iccv. See
        ///  #calibrateCamera for other detailed explanations.
        ///  @sa
        ///     calibrateCamera, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate, undistort
        /// </remarks>
        public static double calibrateCameraROExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat stdDeviationsObjPoints, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (stdDeviationsObjPoints != null) stdDeviationsObjPoints.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraROExtended_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, stdDeviationsObjPoints.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration pattern.
        /// </summary>
        /// <remarks>
        ///  This function is an extension of #calibrateCamera with the method of releasing object which was
        ///  proposed in @cite strobl2011iccv. In many common cases with inaccurate, unmeasured, roughly planar
        ///  targets (calibration plates), this method can dramatically improve the precision of the estimated
        ///  camera parameters. Both the object-releasing method and standard method are supported by this
        ///  function. Use the parameter **iFixedPoint** for method selection. In the internal implementation,
        ///  #calibrateCamera is a wrapper for this function.
        /// </remarks>
        /// <param name="objectPoints">
        /// Vector of vectors of calibration pattern points in the calibration pattern
        ///  coordinate space. See #calibrateCamera for details. If the method of releasing object to be used,
        ///  the identical calibration board must be used in each view and it must be fully visible, and all
        ///  objectPoints[i] must be the same and all points should be roughly close to a plane. **The calibration
        ///  target has to be rigid, or at least static if the camera (rather than the calibration target) is
        ///  shifted for grabbing images.**
        /// </param>
        /// <param name="imagePoints">
        /// Vector of vectors of the projections of calibration pattern points. See
        ///  #calibrateCamera for details.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="iFixedPoint">
        /// The index of the 3D object point in objectPoints[0] to be fixed. It also acts as
        ///  a switch for calibration method selection. If object-releasing method to be used, pass in the
        ///  parameter in the range of [1, objectPoints[0].size()-2], otherwise a value out of this range will
        ///  make standard calibration method selected. Usually the top-right corner point of the calibration
        ///  board grid is recommended to be fixed when object-releasing method being utilized. According to
        ///  \cite strobl2011iccv, two other points are also fixed. In this implementation, objectPoints[0].front
        ///  and objectPoints[0].back.z are used. With object-releasing method, accurate rvecs, tvecs and
        ///  newObjPoints are only possible if coordinates of these three fixed points are accurate enough.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix. See #calibrateCamera for details.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients. See #calibrateCamera for details.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors estimated for each pattern view. See #calibrateCamera
        ///  for details.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="newObjPoints">
        /// The updated output vector of calibration pattern points. The coordinates might
        ///  be scaled based on three fixed points. The returned coordinates are accurate only if the above
        ///  mentioned three fixed points are accurate. If not needed, noArray() can be passed in. This parameter
        ///  is ignored with standard calibration method.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsObjPoints">
        /// Output vector of standard deviations estimated for refined coordinates
        ///  of calibration pattern points. It has the same size and order as objectPoints[0] vector. This
        ///  parameter is ignored with standard calibration method.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of some predefined values. See
        ///  #calibrateCamera for details. If the method of releasing object is used, the calibration time may
        ///  be much longer. CALIB_USE_QR or CALIB_USE_LU could be used for faster calibration with potentially
        ///  less precise and less stable in some rare cases.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000, @cite BouguetMCT and @cite strobl2011iccv. See
        ///  #calibrateCamera for other detailed explanations.
        ///  @sa
        ///     calibrateCamera, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate, undistort
        /// </remarks>
        public static double calibrateCameraROExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat stdDeviationsObjPoints, Mat perViewErrors, int flags)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (stdDeviationsObjPoints != null) stdDeviationsObjPoints.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraROExtended_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, stdDeviationsObjPoints.nativeObj, perViewErrors.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Finds the camera intrinsic and extrinsic parameters from several views of a calibration pattern.
        /// </summary>
        /// <remarks>
        ///  This function is an extension of #calibrateCamera with the method of releasing object which was
        ///  proposed in @cite strobl2011iccv. In many common cases with inaccurate, unmeasured, roughly planar
        ///  targets (calibration plates), this method can dramatically improve the precision of the estimated
        ///  camera parameters. Both the object-releasing method and standard method are supported by this
        ///  function. Use the parameter **iFixedPoint** for method selection. In the internal implementation,
        ///  #calibrateCamera is a wrapper for this function.
        /// </remarks>
        /// <param name="objectPoints">
        /// Vector of vectors of calibration pattern points in the calibration pattern
        ///  coordinate space. See #calibrateCamera for details. If the method of releasing object to be used,
        ///  the identical calibration board must be used in each view and it must be fully visible, and all
        ///  objectPoints[i] must be the same and all points should be roughly close to a plane. **The calibration
        ///  target has to be rigid, or at least static if the camera (rather than the calibration target) is
        ///  shifted for grabbing images.**
        /// </param>
        /// <param name="imagePoints">
        /// Vector of vectors of the projections of calibration pattern points. See
        ///  #calibrateCamera for details.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="iFixedPoint">
        /// The index of the 3D object point in objectPoints[0] to be fixed. It also acts as
        ///  a switch for calibration method selection. If object-releasing method to be used, pass in the
        ///  parameter in the range of [1, objectPoints[0].size()-2], otherwise a value out of this range will
        ///  make standard calibration method selected. Usually the top-right corner point of the calibration
        ///  board grid is recommended to be fixed when object-releasing method being utilized. According to
        ///  \cite strobl2011iccv, two other points are also fixed. In this implementation, objectPoints[0].front
        ///  and objectPoints[0].back.z are used. With object-releasing method, accurate rvecs, tvecs and
        ///  newObjPoints are only possible if coordinates of these three fixed points are accurate enough.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix. See #calibrateCamera for details.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients. See #calibrateCamera for details.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors estimated for each pattern view. See #calibrateCamera
        ///  for details.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="newObjPoints">
        /// The updated output vector of calibration pattern points. The coordinates might
        ///  be scaled based on three fixed points. The returned coordinates are accurate only if the above
        ///  mentioned three fixed points are accurate. If not needed, noArray() can be passed in. This parameter
        ///  is ignored with standard calibration method.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///  See #calibrateCamera for details.
        /// </param>
        /// <param name="stdDeviationsObjPoints">
        /// Output vector of standard deviations estimated for refined coordinates
        ///  of calibration pattern points. It has the same size and order as objectPoints[0] vector. This
        ///  parameter is ignored with standard calibration method.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of some predefined values. See
        ///  #calibrateCamera for details. If the method of releasing object is used, the calibration time may
        ///  be much longer. CALIB_USE_QR or CALIB_USE_LU could be used for faster calibration with potentially
        ///  less precise and less stable in some rare cases.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <returns>
        ///  the overall RMS re-projection error.
        /// </returns>
        /// <remarks>
        ///  The function estimates the intrinsic camera parameters and extrinsic parameters for each of the
        ///  views. The algorithm is based on @cite Zhang2000, @cite BouguetMCT and @cite strobl2011iccv. See
        ///  #calibrateCamera for other detailed explanations.
        ///  @sa
        ///     calibrateCamera, findChessboardCorners, solvePnP, initCameraMatrix2D, stereoCalibrate, undistort
        /// </remarks>
        public static double calibrateCameraROExtended(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat stdDeviationsObjPoints, Mat perViewErrors)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (stdDeviationsObjPoints != null) stdDeviationsObjPoints.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraROExtended_12(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, stdDeviationsObjPoints.nativeObj, perViewErrors.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::calibrateCameraRO(vector_Mat objectPoints, vector_Mat imagePoints, Size imageSize, int iFixedPoint, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& newObjPoints, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCameraRO(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints, int flags, in Vec3d criteria)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraRO_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCameraRO(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints, int flags)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraRO_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static double calibrateCameraRO(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat newObjPoints)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (newObjPoints != null) newObjPoints.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_calibrateCameraRO_12(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, imageSize.Item1, imageSize.Item2, iFixedPoint, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, newObjPoints.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  void cv::calibrationMatrixValues(Mat cameraMatrix, Size imageSize, double apertureWidth, double apertureHeight, double& fovx, double& fovy, double& focalLength, Point2d& principalPoint, double& aspectRatio)
        //

        /// <summary>
        ///  Computes useful camera characteristics from the camera intrinsic matrix.
        /// </summary>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix that can be estimated by #calibrateCamera or
        ///  #stereoCalibrate .
        /// </param>
        /// <param name="imageSize">
        /// Input image size in pixels.
        /// </param>
        /// <param name="apertureWidth">
        /// Physical width in mm of the sensor.
        /// </param>
        /// <param name="apertureHeight">
        /// Physical height in mm of the sensor.
        /// </param>
        /// <param name="fovx">
        /// Output field of view in degrees along the horizontal sensor axis.
        /// </param>
        /// <param name="fovy">
        /// Output field of view in degrees along the vertical sensor axis.
        /// </param>
        /// <param name="focalLength">
        /// Focal length of the lens in mm.
        /// </param>
        /// <param name="principalPoint">
        /// Principal point in mm.
        /// </param>
        /// <param name="aspectRatio">
        /// \f$f_y/f_x\f$
        /// </param>
        /// <remarks>
        ///  The function computes various useful camera characteristics from the previously estimated camera
        ///  matrix.
        ///  
        ///  @note
        ///     Do keep in mind that the unity measure 'mm' stands for whatever unit of measure one chooses for
        ///      the chessboard pitch (it can thus be any value).
        /// </remarks>
        public static void calibrationMatrixValues(Mat cameraMatrix, in Vec2d imageSize, double apertureWidth, double apertureHeight, double[] fovx, double[] fovy, double[] focalLength, out Vec2d principalPoint, double[] aspectRatio)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            double[] fovx_out = new double[1];
            double[] fovy_out = new double[1];
            double[] focalLength_out = new double[1];
            double[] principalPoint_out = new double[2];
            double[] aspectRatio_out = new double[1];
            calib3d_Calib3d_calibrationMatrixValues_10(cameraMatrix.nativeObj, imageSize.Item1, imageSize.Item2, apertureWidth, apertureHeight, fovx_out, fovy_out, focalLength_out, principalPoint_out, aspectRatio_out);
            if (fovx != null) fovx[0] = (double)fovx_out[0];
            if (fovy != null) fovy[0] = (double)fovy_out[0];
            if (focalLength != null) focalLength[0] = (double)focalLength_out[0];
            { principalPoint.Item1 = principalPoint_out[0]; principalPoint.Item2 = principalPoint_out[1]; }
            if (aspectRatio != null) aspectRatio[0] = (double)aspectRatio_out[0];

        }


        //
        // C++:  double cv::stereoCalibrate(vector_Mat objectPoints, vector_Mat imagePoints1, vector_Mat imagePoints2, Mat& cameraMatrix1, Mat& distCoeffs1, Mat& cameraMatrix2, Mat& distCoeffs2, Size imageSize, Mat& R, Mat& T, Mat& E, Mat& F, vector_Mat& rvecs, vector_Mat& tvecs, Mat& perViewErrors, int flags = CALIB_FIX_INTRINSIC, TermCriteria criteria = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 1e-6))
        //

        /// <summary>
        ///  Calibrates a stereo camera set up. This function finds the intrinsic parameters
        ///  for each of the two cameras and the extrinsic parameters between the two cameras.
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points. The same structure as
        ///  in @ref calibrateCamera. For each pattern view, both cameras need to see the same object
        ///  points. Therefore, objectPoints.size(), imagePoints1.size(), and imagePoints2.size() need to be
        ///  equal as well as objectPoints[i].size(), imagePoints1[i].size(), and imagePoints2[i].size() need to
        ///  be equal for each i.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the first camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the second camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix1">
        /// Input/output camera intrinsic matrix for the first camera, the same as in
        ///  @ref calibrateCamera. Furthermore, for the stereo case, additional flags may be used, see below.
        /// </param>
        /// <param name="distCoeffs1">
        /// Input/output vector of distortion coefficients, the same as in
        ///  @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Input/output second camera intrinsic matrix for the second camera. See description for
        ///  cameraMatrix1.
        /// </param>
        /// <param name="distCoeffs2">
        /// Input/output lens distortion coefficients for the second camera. See
        ///  description for distCoeffs1.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrices.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix. Together with the translation vector T, this matrix brings
        ///  points given in the first camera's coordinate system to points in the second camera's
        ///  coordinate system. In more technical terms, the tuple of R and T performs a change of basis
        ///  from the first camera's coordinate system to the second camera's coordinate system. Due to its
        ///  duality, this tuple is equivalent to the position of the first camera with respect to the
        ///  second camera coordinate system.
        /// </param>
        /// <param name="T">
        /// Output translation vector, see description above.
        /// </param>
        /// <param name="E">
        /// Output essential matrix.
        /// </param>
        /// <param name="F">
        /// Output fundamental matrix.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///  coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///  i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///  description) brings the calibration pattern from the object coordinate space (in which object points are
        ///  specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///  the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///  to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///  of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_FIX_INTRINSIC Fix cameraMatrix? and distCoeffs? so that only R, T, E, and F
        ///  matrices are estimated.
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS Optimize some or all of the intrinsic parameters
        ///  according to the specified flags. Initial values are provided by the user.
        ///  -   @ref CALIB_USE_EXTRINSIC_GUESS R and T contain valid initial values that are optimized further.
        ///  Otherwise R and T are initialized to the median value of the pattern views (each dimension separately).
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT Fix the principal points during the optimization.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH Fix \f$f^{(j)}_x\f$ and \f$f^{(j)}_y\f$ .
        ///  -   @ref CALIB_FIX_ASPECT_RATIO Optimize \f$f^{(j)}_y\f$ . Fix the ratio \f$f^{(j)}_x/f^{(j)}_y\f$
        ///  .
        ///  -   @ref CALIB_SAME_FOCAL_LENGTH Enforce \f$f^{(0)}_x=f^{(1)}_x\f$ and \f$f^{(0)}_y=f^{(1)}_y\f$ .
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Set tangential distortion coefficients for each camera to
        ///  zeros and fix there.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 Do not change the corresponding radial
        ///  distortion coefficient during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set,
        ///  the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Enable coefficients k4, k5, and k6. To provide the backward
        ///  compatibility, this extra flag should be explicitly specified to make the calibration
        ///  function use the rational model and return 8 coefficients. If the flag is not set, the
        ///  function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///  The function estimates the transformation between two cameras making a stereo pair. If one computes
        ///  the poses of an object relative to the first camera and to the second camera,
        ///  ( \f$R_1\f$,\f$T_1\f$ ) and (\f$R_2\f$,\f$T_2\f$), respectively, for a stereo camera where the
        ///  relative position and orientation between the two cameras are fixed, then those poses definitely
        ///  relate to each other. This means, if the relative position and orientation (\f$R\f$,\f$T\f$) of the
        ///  two cameras is known, it is possible to compute (\f$R_2\f$,\f$T_2\f$) when (\f$R_1\f$,\f$T_1\f$) is
        ///  given. This is what the described function does. It computes (\f$R\f$,\f$T\f$) such that:
        ///  
        ///  \f[R_2=R R_1\f]
        ///  \f[T_2=R T_1 + T.\f]
        ///  
        ///  Therefore, one can compute the coordinate representation of a 3D point for the second camera's
        ///  coordinate system when given the point's coordinate representation in the first camera's coordinate
        ///  system:
        ///  
        ///  \f[\begin{bmatrix}
        ///  X_2 \\
        ///  Y_2 \\
        ///  Z_2 \\
        ///  1
        ///  \end{bmatrix} = \begin{bmatrix}
        ///  R & T \\
        ///  0 & 1
        ///  \end{bmatrix} \begin{bmatrix}
        ///  X_1 \\
        ///  Y_1 \\
        ///  Z_1 \\
        ///  1
        ///  \end{bmatrix}.\f]
        ///  
        ///  
        ///  Optionally, it computes the essential matrix E:
        ///  
        ///  \f[E= \vecthreethree{0}{-T_2}{T_1}{T_2}{0}{-T_0}{-T_1}{T_0}{0} R\f]
        ///  
        ///  where \f$T_i\f$ are components of the translation vector \f$T\f$ : \f$T=[T_0, T_1, T_2]^T\f$ .
        ///  And the function can also compute the fundamental matrix F:
        ///  
        ///  \f[F = cameraMatrix2^{-T}\cdot E \cdot cameraMatrix1^{-1}\f]
        ///  
        ///  Besides the stereo-related information, the function can also perform a full calibration of each of
        ///  the two cameras. However, due to the high dimensionality of the parameter space and noise in the
        ///  input data, the function can diverge from the correct solution. If the intrinsic parameters can be
        ///  estimated with high accuracy for each of the cameras individually (for example, using
        ///  #calibrateCamera ), you are recommended to do so and then pass @ref CALIB_FIX_INTRINSIC flag to the
        ///  function along with the computed intrinsic parameters. Otherwise, if all the parameters are
        ///  estimated at once, it makes sense to restrict some parameters, for example, pass
        ///   @ref CALIB_SAME_FOCAL_LENGTH and @ref CALIB_ZERO_TANGENT_DIST flags, which is usually a
        ///  reasonable assumption.
        ///  
        ///  Similarly to #calibrateCamera, the function minimizes the total re-projection error for all the
        ///  points in all the available views from both cameras. The function returns the final value of the
        ///  re-projection error.
        /// </remarks>
        public static double stereoCalibrateExtended(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, List<Mat> rvecs, List<Mat> tvecs, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_stereoCalibrateExtended_10(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrates a stereo camera set up. This function finds the intrinsic parameters
        ///  for each of the two cameras and the extrinsic parameters between the two cameras.
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points. The same structure as
        ///  in @ref calibrateCamera. For each pattern view, both cameras need to see the same object
        ///  points. Therefore, objectPoints.size(), imagePoints1.size(), and imagePoints2.size() need to be
        ///  equal as well as objectPoints[i].size(), imagePoints1[i].size(), and imagePoints2[i].size() need to
        ///  be equal for each i.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the first camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the second camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix1">
        /// Input/output camera intrinsic matrix for the first camera, the same as in
        ///  @ref calibrateCamera. Furthermore, for the stereo case, additional flags may be used, see below.
        /// </param>
        /// <param name="distCoeffs1">
        /// Input/output vector of distortion coefficients, the same as in
        ///  @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Input/output second camera intrinsic matrix for the second camera. See description for
        ///  cameraMatrix1.
        /// </param>
        /// <param name="distCoeffs2">
        /// Input/output lens distortion coefficients for the second camera. See
        ///  description for distCoeffs1.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrices.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix. Together with the translation vector T, this matrix brings
        ///  points given in the first camera's coordinate system to points in the second camera's
        ///  coordinate system. In more technical terms, the tuple of R and T performs a change of basis
        ///  from the first camera's coordinate system to the second camera's coordinate system. Due to its
        ///  duality, this tuple is equivalent to the position of the first camera with respect to the
        ///  second camera coordinate system.
        /// </param>
        /// <param name="T">
        /// Output translation vector, see description above.
        /// </param>
        /// <param name="E">
        /// Output essential matrix.
        /// </param>
        /// <param name="F">
        /// Output fundamental matrix.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///  coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///  i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///  description) brings the calibration pattern from the object coordinate space (in which object points are
        ///  specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///  the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///  to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///  of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_FIX_INTRINSIC Fix cameraMatrix? and distCoeffs? so that only R, T, E, and F
        ///  matrices are estimated.
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS Optimize some or all of the intrinsic parameters
        ///  according to the specified flags. Initial values are provided by the user.
        ///  -   @ref CALIB_USE_EXTRINSIC_GUESS R and T contain valid initial values that are optimized further.
        ///  Otherwise R and T are initialized to the median value of the pattern views (each dimension separately).
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT Fix the principal points during the optimization.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH Fix \f$f^{(j)}_x\f$ and \f$f^{(j)}_y\f$ .
        ///  -   @ref CALIB_FIX_ASPECT_RATIO Optimize \f$f^{(j)}_y\f$ . Fix the ratio \f$f^{(j)}_x/f^{(j)}_y\f$
        ///  .
        ///  -   @ref CALIB_SAME_FOCAL_LENGTH Enforce \f$f^{(0)}_x=f^{(1)}_x\f$ and \f$f^{(0)}_y=f^{(1)}_y\f$ .
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Set tangential distortion coefficients for each camera to
        ///  zeros and fix there.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 Do not change the corresponding radial
        ///  distortion coefficient during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set,
        ///  the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Enable coefficients k4, k5, and k6. To provide the backward
        ///  compatibility, this extra flag should be explicitly specified to make the calibration
        ///  function use the rational model and return 8 coefficients. If the flag is not set, the
        ///  function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///  The function estimates the transformation between two cameras making a stereo pair. If one computes
        ///  the poses of an object relative to the first camera and to the second camera,
        ///  ( \f$R_1\f$,\f$T_1\f$ ) and (\f$R_2\f$,\f$T_2\f$), respectively, for a stereo camera where the
        ///  relative position and orientation between the two cameras are fixed, then those poses definitely
        ///  relate to each other. This means, if the relative position and orientation (\f$R\f$,\f$T\f$) of the
        ///  two cameras is known, it is possible to compute (\f$R_2\f$,\f$T_2\f$) when (\f$R_1\f$,\f$T_1\f$) is
        ///  given. This is what the described function does. It computes (\f$R\f$,\f$T\f$) such that:
        ///  
        ///  \f[R_2=R R_1\f]
        ///  \f[T_2=R T_1 + T.\f]
        ///  
        ///  Therefore, one can compute the coordinate representation of a 3D point for the second camera's
        ///  coordinate system when given the point's coordinate representation in the first camera's coordinate
        ///  system:
        ///  
        ///  \f[\begin{bmatrix}
        ///  X_2 \\
        ///  Y_2 \\
        ///  Z_2 \\
        ///  1
        ///  \end{bmatrix} = \begin{bmatrix}
        ///  R & T \\
        ///  0 & 1
        ///  \end{bmatrix} \begin{bmatrix}
        ///  X_1 \\
        ///  Y_1 \\
        ///  Z_1 \\
        ///  1
        ///  \end{bmatrix}.\f]
        ///  
        ///  
        ///  Optionally, it computes the essential matrix E:
        ///  
        ///  \f[E= \vecthreethree{0}{-T_2}{T_1}{T_2}{0}{-T_0}{-T_1}{T_0}{0} R\f]
        ///  
        ///  where \f$T_i\f$ are components of the translation vector \f$T\f$ : \f$T=[T_0, T_1, T_2]^T\f$ .
        ///  And the function can also compute the fundamental matrix F:
        ///  
        ///  \f[F = cameraMatrix2^{-T}\cdot E \cdot cameraMatrix1^{-1}\f]
        ///  
        ///  Besides the stereo-related information, the function can also perform a full calibration of each of
        ///  the two cameras. However, due to the high dimensionality of the parameter space and noise in the
        ///  input data, the function can diverge from the correct solution. If the intrinsic parameters can be
        ///  estimated with high accuracy for each of the cameras individually (for example, using
        ///  #calibrateCamera ), you are recommended to do so and then pass @ref CALIB_FIX_INTRINSIC flag to the
        ///  function along with the computed intrinsic parameters. Otherwise, if all the parameters are
        ///  estimated at once, it makes sense to restrict some parameters, for example, pass
        ///   @ref CALIB_SAME_FOCAL_LENGTH and @ref CALIB_ZERO_TANGENT_DIST flags, which is usually a
        ///  reasonable assumption.
        ///  
        ///  Similarly to #calibrateCamera, the function minimizes the total re-projection error for all the
        ///  points in all the available views from both cameras. The function returns the final value of the
        ///  re-projection error.
        /// </remarks>
        public static double stereoCalibrateExtended(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, List<Mat> rvecs, List<Mat> tvecs, Mat perViewErrors, int flags)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_stereoCalibrateExtended_11(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, perViewErrors.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrates a stereo camera set up. This function finds the intrinsic parameters
        ///  for each of the two cameras and the extrinsic parameters between the two cameras.
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points. The same structure as
        ///  in @ref calibrateCamera. For each pattern view, both cameras need to see the same object
        ///  points. Therefore, objectPoints.size(), imagePoints1.size(), and imagePoints2.size() need to be
        ///  equal as well as objectPoints[i].size(), imagePoints1[i].size(), and imagePoints2[i].size() need to
        ///  be equal for each i.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the first camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///  observed by the second camera. The same structure as in @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix1">
        /// Input/output camera intrinsic matrix for the first camera, the same as in
        ///  @ref calibrateCamera. Furthermore, for the stereo case, additional flags may be used, see below.
        /// </param>
        /// <param name="distCoeffs1">
        /// Input/output vector of distortion coefficients, the same as in
        ///  @ref calibrateCamera.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Input/output second camera intrinsic matrix for the second camera. See description for
        ///  cameraMatrix1.
        /// </param>
        /// <param name="distCoeffs2">
        /// Input/output lens distortion coefficients for the second camera. See
        ///  description for distCoeffs1.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the camera intrinsic matrices.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix. Together with the translation vector T, this matrix brings
        ///  points given in the first camera's coordinate system to points in the second camera's
        ///  coordinate system. In more technical terms, the tuple of R and T performs a change of basis
        ///  from the first camera's coordinate system to the second camera's coordinate system. Due to its
        ///  duality, this tuple is equivalent to the position of the first camera with respect to the
        ///  second camera coordinate system.
        /// </param>
        /// <param name="T">
        /// Output translation vector, see description above.
        /// </param>
        /// <param name="E">
        /// Output essential matrix.
        /// </param>
        /// <param name="F">
        /// Output fundamental matrix.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///  coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///  i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///  description) brings the calibration pattern from the object coordinate space (in which object points are
        ///  specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///  the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///  to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///  of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of the RMS re-projection error estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///  -   @ref CALIB_FIX_INTRINSIC Fix cameraMatrix? and distCoeffs? so that only R, T, E, and F
        ///  matrices are estimated.
        ///  -   @ref CALIB_USE_INTRINSIC_GUESS Optimize some or all of the intrinsic parameters
        ///  according to the specified flags. Initial values are provided by the user.
        ///  -   @ref CALIB_USE_EXTRINSIC_GUESS R and T contain valid initial values that are optimized further.
        ///  Otherwise R and T are initialized to the median value of the pattern views (each dimension separately).
        ///  -   @ref CALIB_FIX_PRINCIPAL_POINT Fix the principal points during the optimization.
        ///  -   @ref CALIB_FIX_FOCAL_LENGTH Fix \f$f^{(j)}_x\f$ and \f$f^{(j)}_y\f$ .
        ///  -   @ref CALIB_FIX_ASPECT_RATIO Optimize \f$f^{(j)}_y\f$ . Fix the ratio \f$f^{(j)}_x/f^{(j)}_y\f$
        ///  .
        ///  -   @ref CALIB_SAME_FOCAL_LENGTH Enforce \f$f^{(0)}_x=f^{(1)}_x\f$ and \f$f^{(0)}_y=f^{(1)}_y\f$ .
        ///  -   @ref CALIB_ZERO_TANGENT_DIST Set tangential distortion coefficients for each camera to
        ///  zeros and fix there.
        ///  -   @ref CALIB_FIX_K1,..., @ref CALIB_FIX_K6 Do not change the corresponding radial
        ///  distortion coefficient during the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set,
        ///  the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_RATIONAL_MODEL Enable coefficients k4, k5, and k6. To provide the backward
        ///  compatibility, this extra flag should be explicitly specified to make the calibration
        ///  function use the rational model and return 8 coefficients. If the flag is not set, the
        ///  function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_THIN_PRISM_MODEL Coefficients s1, s2, s3 and s4 are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the thin prism model and return 12 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_S1_S2_S3_S4 The thin prism distortion coefficients are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        ///  -   @ref CALIB_TILTED_MODEL Coefficients tauX and tauY are enabled. To provide the
        ///  backward compatibility, this extra flag should be explicitly specified to make the
        ///  calibration function use the tilted sensor model and return 14 coefficients. If the flag is not
        ///  set, the function computes and returns only 5 distortion coefficients.
        ///  -   @ref CALIB_FIX_TAUX_TAUY The coefficients of the tilted sensor model are not changed during
        ///  the optimization. If @ref CALIB_USE_INTRINSIC_GUESS is set, the coefficient from the
        ///  supplied distCoeffs matrix is used. Otherwise, it is set to 0.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///  The function estimates the transformation between two cameras making a stereo pair. If one computes
        ///  the poses of an object relative to the first camera and to the second camera,
        ///  ( \f$R_1\f$,\f$T_1\f$ ) and (\f$R_2\f$,\f$T_2\f$), respectively, for a stereo camera where the
        ///  relative position and orientation between the two cameras are fixed, then those poses definitely
        ///  relate to each other. This means, if the relative position and orientation (\f$R\f$,\f$T\f$) of the
        ///  two cameras is known, it is possible to compute (\f$R_2\f$,\f$T_2\f$) when (\f$R_1\f$,\f$T_1\f$) is
        ///  given. This is what the described function does. It computes (\f$R\f$,\f$T\f$) such that:
        ///  
        ///  \f[R_2=R R_1\f]
        ///  \f[T_2=R T_1 + T.\f]
        ///  
        ///  Therefore, one can compute the coordinate representation of a 3D point for the second camera's
        ///  coordinate system when given the point's coordinate representation in the first camera's coordinate
        ///  system:
        ///  
        ///  \f[\begin{bmatrix}
        ///  X_2 \\
        ///  Y_2 \\
        ///  Z_2 \\
        ///  1
        ///  \end{bmatrix} = \begin{bmatrix}
        ///  R & T \\
        ///  0 & 1
        ///  \end{bmatrix} \begin{bmatrix}
        ///  X_1 \\
        ///  Y_1 \\
        ///  Z_1 \\
        ///  1
        ///  \end{bmatrix}.\f]
        ///  
        ///  
        ///  Optionally, it computes the essential matrix E:
        ///  
        ///  \f[E= \vecthreethree{0}{-T_2}{T_1}{T_2}{0}{-T_0}{-T_1}{T_0}{0} R\f]
        ///  
        ///  where \f$T_i\f$ are components of the translation vector \f$T\f$ : \f$T=[T_0, T_1, T_2]^T\f$ .
        ///  And the function can also compute the fundamental matrix F:
        ///  
        ///  \f[F = cameraMatrix2^{-T}\cdot E \cdot cameraMatrix1^{-1}\f]
        ///  
        ///  Besides the stereo-related information, the function can also perform a full calibration of each of
        ///  the two cameras. However, due to the high dimensionality of the parameter space and noise in the
        ///  input data, the function can diverge from the correct solution. If the intrinsic parameters can be
        ///  estimated with high accuracy for each of the cameras individually (for example, using
        ///  #calibrateCamera ), you are recommended to do so and then pass @ref CALIB_FIX_INTRINSIC flag to the
        ///  function along with the computed intrinsic parameters. Otherwise, if all the parameters are
        ///  estimated at once, it makes sense to restrict some parameters, for example, pass
        ///   @ref CALIB_SAME_FOCAL_LENGTH and @ref CALIB_ZERO_TANGENT_DIST flags, which is usually a
        ///  reasonable assumption.
        ///  
        ///  Similarly to #calibrateCamera, the function minimizes the total re-projection error for all the
        ///  points in all the available views from both cameras. The function returns the final value of the
        ///  re-projection error.
        /// </remarks>
        public static double stereoCalibrateExtended(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, List<Mat> rvecs, List<Mat> tvecs, Mat perViewErrors)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_stereoCalibrateExtended_12(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, perViewErrors.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::stereoCalibrate(vector_Mat objectPoints, vector_Mat imagePoints1, vector_Mat imagePoints2, Mat& cameraMatrix1, Mat& distCoeffs1, Mat& cameraMatrix2, Mat& distCoeffs2, Size imageSize, Mat& R, Mat& T, Mat& E, Mat& F, int flags = CALIB_FIX_INTRINSIC, TermCriteria criteria = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 1e-6))
        //

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, int flags, in Vec3d criteria)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_10(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, int flags)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_11(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, flags);


        }

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_12(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj);


        }


        //
        // C++:  double cv::stereoCalibrate(vector_Mat objectPoints, vector_Mat imagePoints1, vector_Mat imagePoints2, Mat& cameraMatrix1, Mat& distCoeffs1, Mat& cameraMatrix2, Mat& distCoeffs2, Size imageSize, Mat& R, Mat& T, Mat& E, Mat& F, Mat& perViewErrors, int flags = CALIB_FIX_INTRINSIC, TermCriteria criteria = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 1e-6))
        //

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_13(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, Mat perViewErrors, int flags)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_14(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, perViewErrors.nativeObj, flags);


        }

        public static double stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat E, Mat F, Mat perViewErrors)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (E != null) E.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_stereoCalibrate_15(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, E.nativeObj, F.nativeObj, perViewErrors.nativeObj);


        }


        //
        // C++:  void cv::stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Size imageSize, Mat R, Mat T, Mat& R1, Mat& R2, Mat& P1, Mat& P2, Mat& Q, int flags = CALIB_ZERO_DISPARITY, double alpha = -1, Size newImageSize = Size(), Rect* validPixROI1 = 0, Rect* validPixROI2 = 0)
        //

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, double alpha, in Vec2d newImageSize, out Vec4i validPixROI1, out Vec4i validPixROI2)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();
            double[] validPixROI1_out = new double[4];
            double[] validPixROI2_out = new double[4];
            calib3d_Calib3d_stereoRectify_10(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, alpha, newImageSize.Item1, newImageSize.Item2, validPixROI1_out, validPixROI2_out);
            { validPixROI1.Item1 = (int)validPixROI1_out[0]; validPixROI1.Item2 = (int)validPixROI1_out[1]; validPixROI1.Item3 = (int)validPixROI1_out[2]; validPixROI1.Item4 = (int)validPixROI1_out[3]; }
            { validPixROI2.Item1 = (int)validPixROI2_out[0]; validPixROI2.Item2 = (int)validPixROI2_out[1]; validPixROI2.Item3 = (int)validPixROI2_out[2]; validPixROI2.Item4 = (int)validPixROI2_out[3]; }

        }

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, double alpha, in Vec2d newImageSize, out Vec4i validPixROI1)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();
            double[] validPixROI1_out = new double[4];
            calib3d_Calib3d_stereoRectify_11(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, alpha, newImageSize.Item1, newImageSize.Item2, validPixROI1_out);
            { validPixROI1.Item1 = (int)validPixROI1_out[0]; validPixROI1.Item2 = (int)validPixROI1_out[1]; validPixROI1.Item3 = (int)validPixROI1_out[2]; validPixROI1.Item4 = (int)validPixROI1_out[3]; }

        }

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, double alpha, in Vec2d newImageSize)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_stereoRectify_12(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, alpha, newImageSize.Item1, newImageSize.Item2);


        }

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, double alpha)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_stereoRectify_13(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, alpha);


        }

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_stereoRectify_14(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags);


        }

        /// <summary>
        ///  Computes rectification transforms for each head of a calibrated stereo camera.
        /// </summary>
        /// <param name="cameraMatrix1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="cameraMatrix2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="T">
        /// Translation vector from the coordinate system of the first camera to the second camera,
        ///  see @ref stereoCalibrate.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix
        ///  brings points given in the unrectified first camera's coordinate system to points in the rectified
        ///  first camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified first camera's coordinate system to the rectified first camera's coordinate system.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix
        ///  brings points given in the unrectified second camera's coordinate system to points in the rectified
        ///  second camera's coordinate system. In more technical terms, it performs a change of basis from the
        ///  unrectified second camera's coordinate system to the rectified second camera's coordinate system.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified first camera's image.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///  camera, i.e. it projects points given in the rectified first camera coordinate system into the
        ///  rectified second camera's image.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see @ref reprojectImageTo3D).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref CALIB_ZERO_DISPARITY . If the flag is set,
        ///  the function makes the principal points of each camera have the same pixel coordinates in the
        ///  rectified views. And if the flag is not set, the function may still shift the images in the
        ///  horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///  useful image area.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter. If it is -1 or absent, the function performs the default
        ///  scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified
        ///  images are zoomed and shifted so that only valid pixels are visible (no black areas after
        ///  rectification). alpha=1 means that the rectified image is decimated and shifted so that all the
        ///  pixels from the original images from the cameras are retained in the rectified images (no source
        ///  image pixels are lost). Any intermediate value yields an intermediate result between
        ///  those two extreme cases.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///  #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///  is passed (default), it is set to the original imageSize . Setting it to a larger value can help you
        ///  preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="validPixROI1">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <param name="validPixROI2">
        /// Optional output rectangles inside the rectified images where all the pixels
        ///  are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller
        ///  (see the picture below).
        /// </param>
        /// <remarks>
        ///  The function computes the rotation matrices for each camera that (virtually) make both camera image
        ///  planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies
        ///  the dense stereo correspondence problem. The function takes the matrices computed by #stereoCalibrate
        ///  as input. As output, it provides two rotation matrices and also two projection matrices in the new
        ///  coordinates. The function distinguishes the following two cases:
        ///  
        ///  -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly along the x-axis (with possible small vertical shift). In the rectified images, the
        ///      corresponding epipolar lines in the left and right cameras are horizontal and have the same
        ///      y-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx_1 & 0 \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx_2 & T_x \cdot f \\
        ///                          0 & f & cy & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix} ,\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx_1 \\
        ///                          0 & 1 & 0 & -cy \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_x} & \frac{cx_1 - cx_2}{T_x}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_x\f$ is a horizontal shift between the cameras and \f$cx_1=cx_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  -   **Vertical stereo**: the first and the second camera views are shifted relative to each other
        ///      mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar
        ///      lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like:
        ///  
        ///      \f[\texttt{P1} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_1 & 0 \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix}\f]
        ///  
        ///      \f[\texttt{P2} = \begin{bmatrix}
        ///                          f & 0 & cx & 0 \\
        ///                          0 & f & cy_2 & T_y \cdot f \\
        ///                          0 & 0 & 1 & 0
        ///                       \end{bmatrix},\f]
        ///  
        ///      \f[\texttt{Q} = \begin{bmatrix}
        ///                          1 & 0 & 0 & -cx \\
        ///                          0 & 1 & 0 & -cy_1 \\
        ///                          0 & 0 & 0 & f \\
        ///                          0 & 0 & -\frac{1}{T_y} & \frac{cy_1 - cy_2}{T_y}
        ///                      \end{bmatrix} \f]
        ///  
        ///      where \f$T_y\f$ is a vertical shift between the cameras and \f$cy_1=cy_2\f$ if
        ///      @ref CALIB_ZERO_DISPARITY is set.
        ///  
        ///  As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera
        ///  matrices. The matrices, together with R1 and R2 , can then be passed to #initUndistortRectifyMap to
        ///  initialize the rectification map for each camera.
        ///  
        ///  See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through
        ///  the corresponding image regions. This means that the images are well rectified, which is what most
        ///  stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that
        ///  their interiors are all valid pixels.
        ///  
        ///  ![image](pics/stereo_undistort.jpg)
        /// </remarks>
        public static void stereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, in Vec2d imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_stereoRectify_15(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj);


        }


        //
        // C++:  bool cv::stereoRectifyUncalibrated(Mat points1, Mat points2, Mat F, Size imgSize, Mat& H1, Mat& H2, double threshold = 5)
        //

        /// <summary>
        ///  Computes a rectification transform for an uncalibrated stereo camera.
        /// </summary>
        /// <param name="points1">
        /// Array of feature points in the first image.
        /// </param>
        /// <param name="points2">
        /// The corresponding points in the second image. The same formats as in
        ///  #findFundamentalMat are supported.
        /// </param>
        /// <param name="F">
        /// Input fundamental matrix. It can be computed from the same set of point pairs using
        ///  #findFundamentalMat .
        /// </param>
        /// <param name="imgSize">
        /// Size of the image.
        /// </param>
        /// <param name="H1">
        /// Output rectification homography matrix for the first image.
        /// </param>
        /// <param name="H2">
        /// Output rectification homography matrix for the second image.
        /// </param>
        /// <param name="threshold">
        /// Optional threshold used to filter out the outliers. If the parameter is greater
        ///  than zero, all the point pairs that do not comply with the epipolar geometry (that is, the points
        ///  for which \f$|\texttt{points2[i]}^T \cdot \texttt{F} \cdot \texttt{points1[i]}|>\texttt{threshold}\f$ )
        ///  are rejected prior to computing the homographies. Otherwise, all the points are considered inliers.
        /// </param>
        /// <remarks>
        ///  The function computes the rectification transformations without knowing intrinsic parameters of the
        ///  cameras and their relative position in the space, which explains the suffix "uncalibrated". Another
        ///  related difference from #stereoRectify is that the function outputs not the rectification
        ///  transformations in the object (3D) space, but the planar perspective transformations encoded by the
        ///  homography matrices H1 and H2 . The function implements the algorithm @cite Hartley99 .
        ///  
        ///  @note
        ///     While the algorithm does not need to know the intrinsic parameters of the cameras, it heavily
        ///      depends on the epipolar geometry. Therefore, if the camera lenses have a significant distortion,
        ///      it would be better to correct it before computing the fundamental matrix and calling this
        ///      function. For example, distortion coefficients can be estimated for each head of stereo camera
        ///      separately by using #calibrateCamera . Then, the images can be corrected using #undistort , or
        ///      just the point coordinates can be corrected with #undistortPoints .
        /// </remarks>
        public static bool stereoRectifyUncalibrated(Mat points1, Mat points2, Mat F, in Vec2d imgSize, Mat H1, Mat H2, double threshold)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (H1 != null) H1.ThrowIfDisposed();
            if (H2 != null) H2.ThrowIfDisposed();

            return calib3d_Calib3d_stereoRectifyUncalibrated_10(points1.nativeObj, points2.nativeObj, F.nativeObj, imgSize.Item1, imgSize.Item2, H1.nativeObj, H2.nativeObj, threshold);


        }

        /// <summary>
        ///  Computes a rectification transform for an uncalibrated stereo camera.
        /// </summary>
        /// <param name="points1">
        /// Array of feature points in the first image.
        /// </param>
        /// <param name="points2">
        /// The corresponding points in the second image. The same formats as in
        ///  #findFundamentalMat are supported.
        /// </param>
        /// <param name="F">
        /// Input fundamental matrix. It can be computed from the same set of point pairs using
        ///  #findFundamentalMat .
        /// </param>
        /// <param name="imgSize">
        /// Size of the image.
        /// </param>
        /// <param name="H1">
        /// Output rectification homography matrix for the first image.
        /// </param>
        /// <param name="H2">
        /// Output rectification homography matrix for the second image.
        /// </param>
        /// <param name="threshold">
        /// Optional threshold used to filter out the outliers. If the parameter is greater
        ///  than zero, all the point pairs that do not comply with the epipolar geometry (that is, the points
        ///  for which \f$|\texttt{points2[i]}^T \cdot \texttt{F} \cdot \texttt{points1[i]}|>\texttt{threshold}\f$ )
        ///  are rejected prior to computing the homographies. Otherwise, all the points are considered inliers.
        /// </param>
        /// <remarks>
        ///  The function computes the rectification transformations without knowing intrinsic parameters of the
        ///  cameras and their relative position in the space, which explains the suffix "uncalibrated". Another
        ///  related difference from #stereoRectify is that the function outputs not the rectification
        ///  transformations in the object (3D) space, but the planar perspective transformations encoded by the
        ///  homography matrices H1 and H2 . The function implements the algorithm @cite Hartley99 .
        ///  
        ///  @note
        ///     While the algorithm does not need to know the intrinsic parameters of the cameras, it heavily
        ///      depends on the epipolar geometry. Therefore, if the camera lenses have a significant distortion,
        ///      it would be better to correct it before computing the fundamental matrix and calling this
        ///      function. For example, distortion coefficients can be estimated for each head of stereo camera
        ///      separately by using #calibrateCamera . Then, the images can be corrected using #undistort , or
        ///      just the point coordinates can be corrected with #undistortPoints .
        /// </remarks>
        public static bool stereoRectifyUncalibrated(Mat points1, Mat points2, Mat F, in Vec2d imgSize, Mat H1, Mat H2)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();
            if (F != null) F.ThrowIfDisposed();
            if (H1 != null) H1.ThrowIfDisposed();
            if (H2 != null) H2.ThrowIfDisposed();

            return calib3d_Calib3d_stereoRectifyUncalibrated_11(points1.nativeObj, points2.nativeObj, F.nativeObj, imgSize.Item1, imgSize.Item2, H1.nativeObj, H2.nativeObj);


        }


        //
        // C++:  float cv::rectify3Collinear(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Mat cameraMatrix3, Mat distCoeffs3, vector_Mat imgpt1, vector_Mat imgpt3, Size imageSize, Mat R12, Mat T12, Mat R13, Mat T13, Mat& R1, Mat& R2, Mat& R3, Mat& P1, Mat& P2, Mat& P3, Mat& Q, double alpha, Size newImgSize, Rect* roi1, Rect* roi2, int flags)
        //

        public static float rectify3Collinear(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Mat cameraMatrix3, Mat distCoeffs3, List<Mat> imgpt1, List<Mat> imgpt3, in Vec2d imageSize, Mat R12, Mat T12, Mat R13, Mat T13, Mat R1, Mat R2, Mat R3, Mat P1, Mat P2, Mat P3, Mat Q, double alpha, in Vec2d newImgSize, out Vec4i roi1, out Vec4i roi2, int flags)
        {
            if (cameraMatrix1 != null) cameraMatrix1.ThrowIfDisposed();
            if (distCoeffs1 != null) distCoeffs1.ThrowIfDisposed();
            if (cameraMatrix2 != null) cameraMatrix2.ThrowIfDisposed();
            if (distCoeffs2 != null) distCoeffs2.ThrowIfDisposed();
            if (cameraMatrix3 != null) cameraMatrix3.ThrowIfDisposed();
            if (distCoeffs3 != null) distCoeffs3.ThrowIfDisposed();
            if (R12 != null) R12.ThrowIfDisposed();
            if (T12 != null) T12.ThrowIfDisposed();
            if (R13 != null) R13.ThrowIfDisposed();
            if (T13 != null) T13.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (R3 != null) R3.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (P3 != null) P3.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();
            using Mat imgpt1_mat = Converters.vector_Mat_to_Mat(imgpt1);
            using Mat imgpt3_mat = Converters.vector_Mat_to_Mat(imgpt3);
            double[] roi1_out = new double[4];
            double[] roi2_out = new double[4];
            float retVal = calib3d_Calib3d_rectify3Collinear_10(cameraMatrix1.nativeObj, distCoeffs1.nativeObj, cameraMatrix2.nativeObj, distCoeffs2.nativeObj, cameraMatrix3.nativeObj, distCoeffs3.nativeObj, imgpt1_mat.nativeObj, imgpt3_mat.nativeObj, imageSize.Item1, imageSize.Item2, R12.nativeObj, T12.nativeObj, R13.nativeObj, T13.nativeObj, R1.nativeObj, R2.nativeObj, R3.nativeObj, P1.nativeObj, P2.nativeObj, P3.nativeObj, Q.nativeObj, alpha, newImgSize.Item1, newImgSize.Item2, roi1_out, roi2_out, flags);
            { roi1.Item1 = (int)roi1_out[0]; roi1.Item2 = (int)roi1_out[1]; roi1.Item3 = (int)roi1_out[2]; roi1.Item4 = (int)roi1_out[3]; }
            { roi2.Item1 = (int)roi2_out[0]; roi2.Item2 = (int)roi2_out[1]; roi2.Item3 = (int)roi2_out[2]; roi2.Item4 = (int)roi2_out[3]; }
            return retVal;
        }


        //
        // C++:  Mat cv::getOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, Size imageSize, double alpha, Size newImgSize = Size(), Rect* validPixROI = 0, bool centerPrincipalPoint = false)
        //

        /// <summary>
        ///  Returns the new camera intrinsic matrix based on the free scaling parameter.
        /// </summary>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="imageSize">
        /// Original image size.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter between 0 (when all the pixels in the undistorted image are
        ///  valid) and 1 (when all the source image pixels are retained in the undistorted image). See
        ///  #stereoRectify for details.
        /// </param>
        /// <param name="newImgSize">
        /// Image size after rectification. By default, it is set to imageSize .
        /// </param>
        /// <param name="validPixROI">
        /// Optional output rectangle that outlines all-good-pixels region in the
        ///  undistorted image. See roi1, roi2 description in #stereoRectify .
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Optional flag that indicates whether in the new camera intrinsic matrix the
        ///  principal point should be at the image center or not. By default, the principal point is chosen to
        ///  best fit a subset of the source image (determined by alpha) to the corrected image.
        /// </param>
        /// <returns>
        ///  new_camera_matrix Output new camera intrinsic matrix.
        /// </returns>
        /// <remarks>
        ///  The function computes and returns the optimal new camera intrinsic matrix based on the free scaling parameter.
        ///  By varying this parameter, you may retrieve only sensible pixels alpha=0 , keep all the original
        ///  image pixels if there is valuable information in the corners alpha=1 , or get something in between.
        ///  When alpha&gt;0 , the undistorted result is likely to have some black pixels corresponding to
        ///  "virtual" pixels outside of the captured distorted image. The original camera intrinsic matrix, distortion
        ///  coefficients, the computed new camera intrinsic matrix, and newImageSize should be passed to
        ///  #initUndistortRectifyMap to produce the maps for #remap .
        /// </remarks>
        public static Mat getOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, in Vec2d imageSize, double alpha, in Vec2d newImgSize, out Vec4i validPixROI, bool centerPrincipalPoint)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            double[] validPixROI_out = new double[4];
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getOptimalNewCameraMatrix_10(cameraMatrix.nativeObj, distCoeffs.nativeObj, imageSize.Item1, imageSize.Item2, alpha, newImgSize.Item1, newImgSize.Item2, validPixROI_out, centerPrincipalPoint)));
            { validPixROI.Item1 = (int)validPixROI_out[0]; validPixROI.Item2 = (int)validPixROI_out[1]; validPixROI.Item3 = (int)validPixROI_out[2]; validPixROI.Item4 = (int)validPixROI_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Returns the new camera intrinsic matrix based on the free scaling parameter.
        /// </summary>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="imageSize">
        /// Original image size.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter between 0 (when all the pixels in the undistorted image are
        ///  valid) and 1 (when all the source image pixels are retained in the undistorted image). See
        ///  #stereoRectify for details.
        /// </param>
        /// <param name="newImgSize">
        /// Image size after rectification. By default, it is set to imageSize .
        /// </param>
        /// <param name="validPixROI">
        /// Optional output rectangle that outlines all-good-pixels region in the
        ///  undistorted image. See roi1, roi2 description in #stereoRectify .
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Optional flag that indicates whether in the new camera intrinsic matrix the
        ///  principal point should be at the image center or not. By default, the principal point is chosen to
        ///  best fit a subset of the source image (determined by alpha) to the corrected image.
        /// </param>
        /// <returns>
        ///  new_camera_matrix Output new camera intrinsic matrix.
        /// </returns>
        /// <remarks>
        ///  The function computes and returns the optimal new camera intrinsic matrix based on the free scaling parameter.
        ///  By varying this parameter, you may retrieve only sensible pixels alpha=0 , keep all the original
        ///  image pixels if there is valuable information in the corners alpha=1 , or get something in between.
        ///  When alpha&gt;0 , the undistorted result is likely to have some black pixels corresponding to
        ///  "virtual" pixels outside of the captured distorted image. The original camera intrinsic matrix, distortion
        ///  coefficients, the computed new camera intrinsic matrix, and newImageSize should be passed to
        ///  #initUndistortRectifyMap to produce the maps for #remap .
        /// </remarks>
        public static Mat getOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, in Vec2d imageSize, double alpha, in Vec2d newImgSize, out Vec4i validPixROI)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            double[] validPixROI_out = new double[4];
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getOptimalNewCameraMatrix_11(cameraMatrix.nativeObj, distCoeffs.nativeObj, imageSize.Item1, imageSize.Item2, alpha, newImgSize.Item1, newImgSize.Item2, validPixROI_out)));
            { validPixROI.Item1 = (int)validPixROI_out[0]; validPixROI.Item2 = (int)validPixROI_out[1]; validPixROI.Item3 = (int)validPixROI_out[2]; validPixROI.Item4 = (int)validPixROI_out[3]; }
            return retVal;
        }

        /// <summary>
        ///  Returns the new camera intrinsic matrix based on the free scaling parameter.
        /// </summary>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="imageSize">
        /// Original image size.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter between 0 (when all the pixels in the undistorted image are
        ///  valid) and 1 (when all the source image pixels are retained in the undistorted image). See
        ///  #stereoRectify for details.
        /// </param>
        /// <param name="newImgSize">
        /// Image size after rectification. By default, it is set to imageSize .
        /// </param>
        /// <param name="validPixROI">
        /// Optional output rectangle that outlines all-good-pixels region in the
        ///  undistorted image. See roi1, roi2 description in #stereoRectify .
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Optional flag that indicates whether in the new camera intrinsic matrix the
        ///  principal point should be at the image center or not. By default, the principal point is chosen to
        ///  best fit a subset of the source image (determined by alpha) to the corrected image.
        /// </param>
        /// <returns>
        ///  new_camera_matrix Output new camera intrinsic matrix.
        /// </returns>
        /// <remarks>
        ///  The function computes and returns the optimal new camera intrinsic matrix based on the free scaling parameter.
        ///  By varying this parameter, you may retrieve only sensible pixels alpha=0 , keep all the original
        ///  image pixels if there is valuable information in the corners alpha=1 , or get something in between.
        ///  When alpha&gt;0 , the undistorted result is likely to have some black pixels corresponding to
        ///  "virtual" pixels outside of the captured distorted image. The original camera intrinsic matrix, distortion
        ///  coefficients, the computed new camera intrinsic matrix, and newImageSize should be passed to
        ///  #initUndistortRectifyMap to produce the maps for #remap .
        /// </remarks>
        public static Mat getOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, in Vec2d imageSize, double alpha, in Vec2d newImgSize)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getOptimalNewCameraMatrix_12(cameraMatrix.nativeObj, distCoeffs.nativeObj, imageSize.Item1, imageSize.Item2, alpha, newImgSize.Item1, newImgSize.Item2)));


        }

        /// <summary>
        ///  Returns the new camera intrinsic matrix based on the free scaling parameter.
        /// </summary>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$\distcoeffs\f$. If the vector is NULL/empty, the zero distortion coefficients are
        ///  assumed.
        /// </param>
        /// <param name="imageSize">
        /// Original image size.
        /// </param>
        /// <param name="alpha">
        /// Free scaling parameter between 0 (when all the pixels in the undistorted image are
        ///  valid) and 1 (when all the source image pixels are retained in the undistorted image). See
        ///  #stereoRectify for details.
        /// </param>
        /// <param name="newImgSize">
        /// Image size after rectification. By default, it is set to imageSize .
        /// </param>
        /// <param name="validPixROI">
        /// Optional output rectangle that outlines all-good-pixels region in the
        ///  undistorted image. See roi1, roi2 description in #stereoRectify .
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Optional flag that indicates whether in the new camera intrinsic matrix the
        ///  principal point should be at the image center or not. By default, the principal point is chosen to
        ///  best fit a subset of the source image (determined by alpha) to the corrected image.
        /// </param>
        /// <returns>
        ///  new_camera_matrix Output new camera intrinsic matrix.
        /// </returns>
        /// <remarks>
        ///  The function computes and returns the optimal new camera intrinsic matrix based on the free scaling parameter.
        ///  By varying this parameter, you may retrieve only sensible pixels alpha=0 , keep all the original
        ///  image pixels if there is valuable information in the corners alpha=1 , or get something in between.
        ///  When alpha&gt;0 , the undistorted result is likely to have some black pixels corresponding to
        ///  "virtual" pixels outside of the captured distorted image. The original camera intrinsic matrix, distortion
        ///  coefficients, the computed new camera intrinsic matrix, and newImageSize should be passed to
        ///  #initUndistortRectifyMap to produce the maps for #remap .
        /// </remarks>
        public static Mat getOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, in Vec2d imageSize, double alpha)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getOptimalNewCameraMatrix_13(cameraMatrix.nativeObj, distCoeffs.nativeObj, imageSize.Item1, imageSize.Item2, alpha)));


        }


        //
        // C++:  Mat cv::findEssentialMat(Mat points1, Mat points2, double focal = 1.0, Point2d pp = Point2d(0, 0), int method = RANSAC, double prob = 0.999, double threshold = 1.0, int maxIters = 1000, Mat& mask = Mat())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp, int method, double prob, double threshold, int maxIters, Mat mask)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_16(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2, method, prob, threshold, maxIters, mask.nativeObj)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp, int method, double prob, double threshold, int maxIters)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_17(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2, method, prob, threshold, maxIters)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp, int method, double prob, double threshold)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_18(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2, method, prob, threshold)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp, int method, double prob)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_19(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2, method, prob)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp, int method)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_110(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2, method)));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="points1">
        /// Array of N (N &gt;= 5) 2D points from the first image. The point coordinates should
        ///  be floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="focal">
        /// focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="method">
        /// Method for computing a fundamental matrix.
        ///  -   @ref RANSAC for the RANSAC algorithm.
        ///  -   @ref LMEDS for the LMedS algorithm.
        /// </param>
        /// <param name="threshold">
        /// Parameter used for RANSAC. It is the maximum distance from a point to an epipolar
        ///  line in pixels, beyond which the point is considered an outlier and is not used for computing the
        ///  final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the
        ///  point localization, image resolution, and the image noise.
        /// </param>
        /// <param name="prob">
        /// Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of
        ///  confidence (probability) that the estimated matrix is correct.
        /// </param>
        /// <param name="mask">
        /// Output array of N elements, every element of which is set to 0 for outliers and to 1
        ///  for the other points. The array is computed only in the RANSAC and LMedS methods.
        /// </param>
        /// <param name="maxIters">
        /// The maximum number of robust method iterations.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static Mat findEssentialMat(Mat points1, Mat points2, double focal, in Vec2d pp)
        {
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_findEssentialMat_111(points1.nativeObj, points2.nativeObj, focal, pp.Item1, pp.Item2)));


        }


        //
        // C++:  int cv::recoverPose(Mat E, Mat points1, Mat points2, Mat& R, Mat& t, double focal = 1.0, Point2d pp = Point2d(0, 0), Mat& mask = Mat())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="E">
        /// The input essential matrix.
        /// </param>
        /// <param name="points1">
        /// Array of N 2D points from the first image. The point coordinates should be
        ///  floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="R">
        /// Output rotation matrix. Together with the translation vector, this matrix makes up a tuple
        ///  that performs a change of basis from the first camera's coordinate system to the second camera's
        ///  coordinate system. Note that, in general, t can not be used for this tuple, see the parameter
        ///  description below.
        /// </param>
        /// <param name="t">
        /// Output translation vector. This vector is obtained by @ref decomposeEssentialMat and
        ///  therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit
        ///  length.
        /// </param>
        /// <param name="focal">
        /// Focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="mask">
        /// Input/output mask for inliers in points1 and points2. If it is not empty, then it marks
        ///  inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to
        ///  recover pose. In the output mask only inliers which pass the chirality check.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static int recoverPose(Mat E, Mat points1, Mat points2, Mat R, Mat t, double focal, in Vec2d pp, Mat mask)
        {
            if (E != null) E.ThrowIfDisposed();
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (t != null) t.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();

            return calib3d_Calib3d_recoverPose_17(E.nativeObj, points1.nativeObj, points2.nativeObj, R.nativeObj, t.nativeObj, focal, pp.Item1, pp.Item2, mask.nativeObj);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="E">
        /// The input essential matrix.
        /// </param>
        /// <param name="points1">
        /// Array of N 2D points from the first image. The point coordinates should be
        ///  floating-point (single or double precision).
        /// </param>
        /// <param name="points2">
        /// Array of the second image points of the same size and format as points1 .
        /// </param>
        /// <param name="R">
        /// Output rotation matrix. Together with the translation vector, this matrix makes up a tuple
        ///  that performs a change of basis from the first camera's coordinate system to the second camera's
        ///  coordinate system. Note that, in general, t can not be used for this tuple, see the parameter
        ///  description below.
        /// </param>
        /// <param name="t">
        /// Output translation vector. This vector is obtained by @ref decomposeEssentialMat and
        ///  therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit
        ///  length.
        /// </param>
        /// <param name="focal">
        /// Focal length of the camera. Note that this function assumes that points1 and points2
        ///  are feature points from cameras with same focal length and principal point.
        /// </param>
        /// <param name="pp">
        /// principal point of the camera.
        /// </param>
        /// <param name="mask">
        /// Input/output mask for inliers in points1 and points2. If it is not empty, then it marks
        ///  inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to
        ///  recover pose. In the output mask only inliers which pass the chirality check.
        /// </param>
        /// <remarks>
        ///  This function differs from the one above that it computes camera intrinsic matrix from focal length and
        ///  principal point:
        ///  
        ///  \f[A =
        ///  \begin{bmatrix}
        ///  f & 0 & x_{pp}  \\
        ///  0 & f & y_{pp}  \\
        ///  0 & 0 & 1
        ///  \end{bmatrix}\f]
        /// </remarks>
        public static int recoverPose(Mat E, Mat points1, Mat points2, Mat R, Mat t, double focal, in Vec2d pp)
        {
            if (E != null) E.ThrowIfDisposed();
            if (points1 != null) points1.ThrowIfDisposed();
            if (points2 != null) points2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (t != null) t.ThrowIfDisposed();

            return calib3d_Calib3d_recoverPose_18(E.nativeObj, points1.nativeObj, points2.nativeObj, R.nativeObj, t.nativeObj, focal, pp.Item1, pp.Item2);


        }


        //
        // C++:  Rect cv::getValidDisparityROI(Rect roi1, Rect roi2, int minDisparity, int numberOfDisparities, int blockSize)
        //

        public static Vec4i getValidDisparityROIAsVec4i(in Vec4i roi1, Vec4i roi2, int minDisparity, int numberOfDisparities, int blockSize)
        {


            double[] tmpArray = new double[4];
            calib3d_Calib3d_getValidDisparityROI_10(roi1.Item1, roi1.Item2, roi1.Item3, roi1.Item4, roi2.Item1, roi2.Item2, roi2.Item3, roi2.Item4, minDisparity, numberOfDisparities, blockSize, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }


        //
        // C++:  void cv::initUndistortRectifyMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, Size size, int m1type, Mat& map1, Mat& map2)
        //

        /// <summary>
        ///  Computes the undistortion and rectification transformation map.
        /// </summary>
        /// <remarks>
        ///  The function computes the joint undistortion and rectification transformation and represents the
        ///  result in the form of maps for #remap. The undistorted image looks like original, as if it is
        ///  captured with a camera using the camera matrix =newCameraMatrix and zero distortion. In case of a
        ///  monocular camera, newCameraMatrix is usually equal to cameraMatrix, or it can be computed by
        ///  #getOptimalNewCameraMatrix for a better control over scaling. In case of a stereo camera,
        ///  newCameraMatrix is normally set to P1 or P2 computed by #stereoRectify .
        ///  
        ///  Also, this new camera is oriented differently in the coordinate space, according to R. That, for
        ///  example, helps to align two heads of a stereo camera so that the epipolar lines on both images
        ///  become horizontal and have the same y- coordinate (in case of a horizontally aligned stereo camera).
        ///  
        ///  The function actually builds the maps for the inverse mapping algorithm that is used by #remap. That
        ///  is, for each pixel \f$(u, v)\f$ in the destination (corrected and rectified) image, the function
        ///  computes the corresponding coordinates in the source image (that is, in the original image from
        ///  camera). The following process is applied:
        ///  \f[
        ///  \begin{array}{l}
        ///  x  \leftarrow (u - {c'}_x)/{f'}_x  \\
        ///  y  \leftarrow (v - {c'}_y)/{f'}_y  \\
        ///  {[X\,Y\,W]} ^T  \leftarrow R^{-1}*[x \, y \, 1]^T  \\
        ///  x'  \leftarrow X/W  \\
        ///  y'  \leftarrow Y/W  \\
        ///  r^2  \leftarrow x'^2 + y'^2 \\
        ///  x''  \leftarrow x' \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}
        ///  + 2p_1 x' y' + p_2(r^2 + 2 x'^2)  + s_1 r^2 + s_2 r^4\\
        ///  y''  \leftarrow y' \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}
        ///  + p_1 (r^2 + 2 y'^2) + 2 p_2 x' y' + s_3 r^2 + s_4 r^4 \\
        ///  s\vecthree{x'''}{y'''}{1} =
        ///  \vecthreethree{R_{33}(\tau_x, \tau_y)}{0}{-R_{13}((\tau_x, \tau_y)}
        ///  {0}{R_{33}(\tau_x, \tau_y)}{-R_{23}(\tau_x, \tau_y)}
        ///  {0}{0}{1} R(\tau_x, \tau_y) \vecthree{x''}{y''}{1}\\
        ///  map_x(u,v)  \leftarrow x''' f_x + c_x  \\
        ///  map_y(u,v)  \leftarrow y''' f_y + c_y
        ///  \end{array}
        ///  \f]
        ///  where \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6[, s_1, s_2, s_3, s_4[, \tau_x, \tau_y]]]])\f$
        ///  are the distortion coefficients.
        ///  
        ///  In case of a stereo camera, this function is called twice: once for each camera head, after
        ///  #stereoRectify, which in its turn is called after #stereoCalibrate. But if the stereo camera
        ///  was not calibrated, it is still possible to compute the rectification transformations directly from
        ///  the fundamental matrix using #stereoRectifyUncalibrated. For each camera, the function computes
        ///  homography H as the rectification transformation in a pixel domain, not a rotation matrix R in 3D
        ///  space. R can be computed from H as
        ///  \f[\texttt{R} = \texttt{cameraMatrix} ^{-1} \cdot \texttt{H} \cdot \texttt{cameraMatrix}\f]
        ///  where cameraMatrix can be chosen arbitrarily.
        /// </remarks>
        /// <param name="cameraMatrix">
        /// Input camera matrix \f$A=\vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6[, s_1, s_2, s_3, s_4[, \tau_x, \tau_y]]]])\f$
        ///  of 4, 5, 8, 12 or 14 elements. If the vector is NULL/empty, the zero distortion coefficients are assumed.
        /// </param>
        /// <param name="R">
        /// Optional rectification transformation in the object space (3x3 matrix). R1 or R2 ,
        ///  computed by #stereoRectify can be passed here. If the matrix is empty, the identity transformation
        ///  is assumed. In #initUndistortRectifyMap R assumed to be an identity matrix.
        /// </param>
        /// <param name="newCameraMatrix">
        /// New camera matrix \f$A'=\vecthreethree{f_x'}{0}{c_x'}{0}{f_y'}{c_y'}{0}{0}{1}\f$.
        /// </param>
        /// <param name="size">
        /// Undistorted image size.
        /// </param>
        /// <param name="m1type">
        /// Type of the first output map that can be CV_32FC1, CV_32FC2 or CV_16SC2, see #convertMaps
        /// </param>
        /// <param name="map1">
        /// The first output map.
        /// </param>
        /// <param name="map2">
        /// The second output map.
        /// </param>
        public static void initUndistortRectifyMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, in Vec2d size, int m1type, Mat map1, Mat map2)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (newCameraMatrix != null) newCameraMatrix.ThrowIfDisposed();
            if (map1 != null) map1.ThrowIfDisposed();
            if (map2 != null) map2.ThrowIfDisposed();

            calib3d_Calib3d_initUndistortRectifyMap_10(cameraMatrix.nativeObj, distCoeffs.nativeObj, R.nativeObj, newCameraMatrix.nativeObj, size.Item1, size.Item2, m1type, map1.nativeObj, map2.nativeObj);


        }


        //
        // C++:  void cv::initInverseRectificationMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, Size size, int m1type, Mat& map1, Mat& map2)
        //

        /// <summary>
        ///  Computes the projection and inverse-rectification transformation map. In essense, this is the inverse of
        ///  #initUndistortRectifyMap to accomodate stereo-rectification of projectors ('inverse-cameras') in projector-camera pairs.
        /// </summary>
        /// <remarks>
        ///  The function computes the joint projection and inverse rectification transformation and represents the
        ///  result in the form of maps for #remap. The projected image looks like a distorted version of the original which,
        ///  once projected by a projector, should visually match the original. In case of a monocular camera, newCameraMatrix
        ///  is usually equal to cameraMatrix, or it can be computed by
        ///  #getOptimalNewCameraMatrix for a better control over scaling. In case of a projector-camera pair,
        ///  newCameraMatrix is normally set to P1 or P2 computed by #stereoRectify .
        ///  
        ///  The projector is oriented differently in the coordinate space, according to R. In case of projector-camera pairs,
        ///  this helps align the projector (in the same manner as #initUndistortRectifyMap for the camera) to create a stereo-rectified pair. This
        ///  allows epipolar lines on both images to become horizontal and have the same y-coordinate (in case of a horizontally aligned projector-camera pair).
        ///  
        ///  The function builds the maps for the inverse mapping algorithm that is used by #remap. That
        ///  is, for each pixel \f$(u, v)\f$ in the destination (projected and inverse-rectified) image, the function
        ///  computes the corresponding coordinates in the source image (that is, in the original digital image). The following process is applied:
        ///  
        ///  \f[
        ///  \begin{array}{l}
        ///  \text{newCameraMatrix}\\
        ///  x  \leftarrow (u - {c'}_x)/{f'}_x  \\
        ///  y  \leftarrow (v - {c'}_y)/{f'}_y  \\
        ///  
        ///  \\\text{Undistortion}
        ///  \\\scriptsize{\textit{though equation shown is for radial undistortion, function implements cv::undistortPoints()}}\\
        ///  r^2  \leftarrow x^2 + y^2 \\
        ///  \theta \leftarrow \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}\\
        ///  x' \leftarrow \frac{x}{\theta} \\
        ///  y'  \leftarrow \frac{y}{\theta} \\
        ///  
        ///  \\\text{Rectification}\\
        ///  {[X\,Y\,W]} ^T  \leftarrow R*[x' \, y' \, 1]^T  \\
        ///  x''  \leftarrow X/W  \\
        ///  y''  \leftarrow Y/W  \\
        ///  
        ///  \\\text{cameraMatrix}\\
        ///  map_x(u,v)  \leftarrow x'' f_x + c_x  \\
        ///  map_y(u,v)  \leftarrow y'' f_y + c_y
        ///  \end{array}
        ///  \f]
        ///  where \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6[, s_1, s_2, s_3, s_4[, \tau_x, \tau_y]]]])\f$
        ///  are the distortion coefficients vector distCoeffs.
        ///  
        ///  In case of a stereo-rectified projector-camera pair, this function is called for the projector while #initUndistortRectifyMap is called for the camera head.
        ///  This is done after #stereoRectify, which in turn is called after #stereoCalibrate. If the projector-camera pair
        ///  is not calibrated, it is still possible to compute the rectification transformations directly from
        ///  the fundamental matrix using #stereoRectifyUncalibrated. For the projector and camera, the function computes
        ///  homography H as the rectification transformation in a pixel domain, not a rotation matrix R in 3D
        ///  space. R can be computed from H as
        ///  \f[\texttt{R} = \texttt{cameraMatrix} ^{-1} \cdot \texttt{H} \cdot \texttt{cameraMatrix}\f]
        ///  where cameraMatrix can be chosen arbitrarily.
        /// </remarks>
        /// <param name="cameraMatrix">
        /// Input camera matrix \f$A=\vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients
        ///  \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6[, s_1, s_2, s_3, s_4[, \tau_x, \tau_y]]]])\f$
        ///  of 4, 5, 8, 12 or 14 elements. If the vector is NULL/empty, the zero distortion coefficients are assumed.
        /// </param>
        /// <param name="R">
        /// Optional rectification transformation in the object space (3x3 matrix). R1 or R2,
        ///  computed by #stereoRectify can be passed here. If the matrix is empty, the identity transformation
        ///  is assumed.
        /// </param>
        /// <param name="newCameraMatrix">
        /// New camera matrix \f$A'=\vecthreethree{f_x'}{0}{c_x'}{0}{f_y'}{c_y'}{0}{0}{1}\f$.
        /// </param>
        /// <param name="size">
        /// Distorted image size.
        /// </param>
        /// <param name="m1type">
        /// Type of the first output map. Can be CV_32FC1, CV_32FC2 or CV_16SC2, see #convertMaps
        /// </param>
        /// <param name="map1">
        /// The first output map for #remap.
        /// </param>
        /// <param name="map2">
        /// The second output map for #remap.
        /// </param>
        public static void initInverseRectificationMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, in Vec2d size, int m1type, Mat map1, Mat map2)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (newCameraMatrix != null) newCameraMatrix.ThrowIfDisposed();
            if (map1 != null) map1.ThrowIfDisposed();
            if (map2 != null) map2.ThrowIfDisposed();

            calib3d_Calib3d_initInverseRectificationMap_10(cameraMatrix.nativeObj, distCoeffs.nativeObj, R.nativeObj, newCameraMatrix.nativeObj, size.Item1, size.Item2, m1type, map1.nativeObj, map2.nativeObj);


        }


        //
        // C++:  Mat cv::getDefaultNewCameraMatrix(Mat cameraMatrix, Size imgsize = Size(), bool centerPrincipalPoint = false)
        //

        /// <summary>
        ///  Returns the default new camera matrix.
        /// </summary>
        /// <remarks>
        ///  The function returns the camera matrix that is either an exact copy of the input cameraMatrix (when
        ///  centerPrinicipalPoint=false ), or the modified one (when centerPrincipalPoint=true).
        ///  
        ///  In the latter case, the new camera matrix will be:
        ///  
        ///  \f[\begin{bmatrix} f_x && 0 && ( \texttt{imgSize.width} -1)*0.5  \\ 0 && f_y && ( \texttt{imgSize.height} -1)*0.5  \\ 0 && 0 && 1 \end{bmatrix} ,\f]
        ///  
        ///  where \f$f_x\f$ and \f$f_y\f$ are \f$(0,0)\f$ and \f$(1,1)\f$ elements of cameraMatrix, respectively.
        ///  
        ///  By default, the undistortion functions in OpenCV (see #initUndistortRectifyMap, #undistort) do not
        ///  move the principal point. However, when you work with stereo, it is important to move the principal
        ///  points in both views to the same y-coordinate (which is required by most of stereo correspondence
        ///  algorithms), and may be to the same x-coordinate too. So, you can form the new camera matrix for
        ///  each view where the principal points are located at the center.
        /// </remarks>
        /// <param name="cameraMatrix">
        /// Input camera matrix.
        /// </param>
        /// <param name="imgsize">
        /// Camera view image size in pixels.
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Location of the principal point in the new camera matrix. The
        ///  parameter indicates whether this location should be at the image center or not.
        /// </param>
        public static Mat getDefaultNewCameraMatrix(Mat cameraMatrix, in Vec2d imgsize, bool centerPrincipalPoint)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getDefaultNewCameraMatrix_10(cameraMatrix.nativeObj, imgsize.Item1, imgsize.Item2, centerPrincipalPoint)));


        }

        /// <summary>
        ///  Returns the default new camera matrix.
        /// </summary>
        /// <remarks>
        ///  The function returns the camera matrix that is either an exact copy of the input cameraMatrix (when
        ///  centerPrinicipalPoint=false ), or the modified one (when centerPrincipalPoint=true).
        ///  
        ///  In the latter case, the new camera matrix will be:
        ///  
        ///  \f[\begin{bmatrix} f_x && 0 && ( \texttt{imgSize.width} -1)*0.5  \\ 0 && f_y && ( \texttt{imgSize.height} -1)*0.5  \\ 0 && 0 && 1 \end{bmatrix} ,\f]
        ///  
        ///  where \f$f_x\f$ and \f$f_y\f$ are \f$(0,0)\f$ and \f$(1,1)\f$ elements of cameraMatrix, respectively.
        ///  
        ///  By default, the undistortion functions in OpenCV (see #initUndistortRectifyMap, #undistort) do not
        ///  move the principal point. However, when you work with stereo, it is important to move the principal
        ///  points in both views to the same y-coordinate (which is required by most of stereo correspondence
        ///  algorithms), and may be to the same x-coordinate too. So, you can form the new camera matrix for
        ///  each view where the principal points are located at the center.
        /// </remarks>
        /// <param name="cameraMatrix">
        /// Input camera matrix.
        /// </param>
        /// <param name="imgsize">
        /// Camera view image size in pixels.
        /// </param>
        /// <param name="centerPrincipalPoint">
        /// Location of the principal point in the new camera matrix. The
        ///  parameter indicates whether this location should be at the image center or not.
        /// </param>
        public static Mat getDefaultNewCameraMatrix(Mat cameraMatrix, in Vec2d imgsize)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(calib3d_Calib3d_getDefaultNewCameraMatrix_11(cameraMatrix.nativeObj, imgsize.Item1, imgsize.Item2)));


        }


        //
        // C++:  void cv::undistortPoints(Mat src, Mat& dst, Mat cameraMatrix, Mat distCoeffs, Mat R, Mat P, TermCriteria criteria)
        //

        /// <remarks>
        ///  @overload
        ///      @note Default version of #undistortPoints does 5 iterations to compute undistorted points.
        /// </remarks>
        public static void undistortPointsIter(Mat src, Mat dst, Mat cameraMatrix, Mat distCoeffs, Mat R, Mat P, in Vec3d criteria)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_undistortPointsIter_10(src.nativeObj, dst.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, R.nativeObj, P.nativeObj, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  void cv::undistortImagePoints(Mat src, Mat& dst, Mat cameraMatrix, Mat distCoeffs, TermCriteria arg1 = TermCriteria(TermCriteria::MAX_ITER + TermCriteria::EPS, 5, 0.01))
        //

        /// <summary>
        ///  Compute undistorted image points position
        /// </summary>
        /// <param name="src">
        /// Observed points position, 2xN/Nx2 1-channel or 1xN/Nx1 2-channel (CV_32FC2 or
        ///  CV_64FC2) (or vector&lt;Point2f&gt; ).
        /// </param>
        /// <param name="dst">
        /// Output undistorted points position (1xN/Nx1 2-channel or vector&lt;Point2f&gt; ).
        /// </param>
        /// <param name="cameraMatrix">
        /// Camera matrix \f$\vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Distortion coefficients
        /// </param>
        public static void undistortImagePoints(Mat src, Mat dst, Mat cameraMatrix, Mat distCoeffs, in Vec3d arg1)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();

            calib3d_Calib3d_undistortImagePoints_10(src.nativeObj, dst.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, (int)arg1.Item1, (int)arg1.Item2, arg1.Item3);


        }


        //
        // C++:  void cv::fisheye::undistortPoints(Mat distorted, Mat& undistorted, Mat K, Mat D, Mat R = Mat(), Mat P = Mat(), TermCriteria criteria = TermCriteria(TermCriteria::MAX_ITER + TermCriteria::EPS, 10, 1e-8))
        //

        /// <summary>
        ///  Undistorts 2D points using fisheye model
        /// </summary>
        /// <param name="distorted">
        /// Array of object points, 1xN/Nx1 2-channel (or vector&lt;Point2f&gt; ), where N is the
        ///      number of points in the view.
        /// </param>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="criteria">
        /// Termination criteria
        /// </param>
        /// <param name="undistorted">
        /// Output array of image points, 1xN/Nx1 2-channel, or vector&lt;Point2f&gt; .
        /// </param>
        public static void fisheye_undistortPoints(Mat distorted, Mat undistorted, Mat K, Mat D, Mat R, Mat P, in Vec3d criteria)
        {
            if (distorted != null) distorted.ThrowIfDisposed();
            if (undistorted != null) undistorted.ThrowIfDisposed();
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1undistortPoints_10(distorted.nativeObj, undistorted.nativeObj, K.nativeObj, D.nativeObj, R.nativeObj, P.nativeObj, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  void cv::fisheye::initUndistortRectifyMap(Mat K, Mat D, Mat R, Mat P, Size size, int m1type, Mat& map1, Mat& map2)
        //

        /// <summary>
        ///  Computes undistortion and rectification maps for image transform by #remap. If D is empty zero
        ///      distortion is used, if R or P is empty identity matrixes are used.
        /// </summary>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="size">
        /// Undistorted image size.
        /// </param>
        /// <param name="m1type">
        /// Type of the first output map that can be CV_32FC1 or CV_16SC2 . See #convertMaps
        ///      for details.
        /// </param>
        /// <param name="map1">
        /// The first output map.
        /// </param>
        /// <param name="map2">
        /// The second output map.
        /// </param>
        public static void fisheye_initUndistortRectifyMap(Mat K, Mat D, Mat R, Mat P, in Vec2d size, int m1type, Mat map1, Mat map2)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();
            if (map1 != null) map1.ThrowIfDisposed();
            if (map2 != null) map2.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1initUndistortRectifyMap_10(K.nativeObj, D.nativeObj, R.nativeObj, P.nativeObj, size.Item1, size.Item2, m1type, map1.nativeObj, map2.nativeObj);


        }


        //
        // C++:  void cv::fisheye::undistortImage(Mat distorted, Mat& undistorted, Mat K, Mat D, Mat Knew = cv::Mat(), Size new_size = Size())
        //

        /// <summary>
        ///  Transforms an image to compensate for fisheye lens distortion.
        /// </summary>
        /// <param name="distorted">
        /// image with fisheye lens distortion.
        /// </param>
        /// <param name="undistorted">
        /// Output image with compensated fisheye lens distortion.
        /// </param>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="Knew">
        /// Camera intrinsic matrix of the distorted image. By default, it is the identity matrix but you
        ///      may additionally scale and shift the result by using a different matrix.
        /// </param>
        /// <param name="new_size">
        /// the new size
        /// </param>
        /// <remarks>
        ///      The function transforms an image to compensate radial and tangential lens distortion.
        ///  
        ///      The function is simply a combination of #fisheye::initUndistortRectifyMap (with unity R ) and #remap
        ///      (with bilinear interpolation). See the former function for details of the transformation being
        ///      performed.
        ///  
        ///      See below the results of undistortImage.
        ///         -   a\) result of undistort of perspective camera model (all possible coefficients (k_1, k_2, k_3,
        ///              k_4, k_5, k_6) of distortion were optimized under calibration)
        ///          -   b\) result of #fisheye::undistortImage of fisheye camera model (all possible coefficients (k_1, k_2,
        ///              k_3, k_4) of fisheye distortion were optimized under calibration)
        ///          -   c\) original image was captured with fisheye lens
        ///  
        ///      Pictures a) and b) almost the same. But if we consider points of image located far from the center
        ///      of image, we can notice that on image a) these points are distorted.
        ///  
        ///      ![image](pics/fisheye_undistorted.jpg)
        /// </remarks>
        public static void fisheye_undistortImage(Mat distorted, Mat undistorted, Mat K, Mat D, Mat Knew, in Vec2d new_size)
        {
            if (distorted != null) distorted.ThrowIfDisposed();
            if (undistorted != null) undistorted.ThrowIfDisposed();
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (Knew != null) Knew.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1undistortImage_10(distorted.nativeObj, undistorted.nativeObj, K.nativeObj, D.nativeObj, Knew.nativeObj, new_size.Item1, new_size.Item2);


        }


        //
        // C++:  void cv::fisheye::estimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, Size image_size, Mat R, Mat& P, double balance = 0.0, Size new_size = Size(), double fov_scale = 1.0)
        //

        /// <summary>
        ///  Estimates new camera intrinsic matrix for undistortion or rectification.
        /// </summary>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="image_size">
        /// Size of the image
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="new_size">
        /// the new size
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_estimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, in Vec2d image_size, Mat R, Mat P, double balance, in Vec2d new_size, double fov_scale)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1estimateNewCameraMatrixForUndistortRectify_10(K.nativeObj, D.nativeObj, image_size.Item1, image_size.Item2, R.nativeObj, P.nativeObj, balance, new_size.Item1, new_size.Item2, fov_scale);


        }

        /// <summary>
        ///  Estimates new camera intrinsic matrix for undistortion or rectification.
        /// </summary>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="image_size">
        /// Size of the image
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="new_size">
        /// the new size
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_estimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, in Vec2d image_size, Mat R, Mat P, double balance, in Vec2d new_size)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1estimateNewCameraMatrixForUndistortRectify_11(K.nativeObj, D.nativeObj, image_size.Item1, image_size.Item2, R.nativeObj, P.nativeObj, balance, new_size.Item1, new_size.Item2);


        }

        /// <summary>
        ///  Estimates new camera intrinsic matrix for undistortion or rectification.
        /// </summary>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="image_size">
        /// Size of the image
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="new_size">
        /// the new size
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_estimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, in Vec2d image_size, Mat R, Mat P, double balance)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1estimateNewCameraMatrixForUndistortRectify_12(K.nativeObj, D.nativeObj, image_size.Item1, image_size.Item2, R.nativeObj, P.nativeObj, balance);


        }

        /// <summary>
        ///  Estimates new camera intrinsic matrix for undistortion or rectification.
        /// </summary>
        /// <param name="K">
        /// Camera intrinsic matrix \f$\cameramatrix{K}\f$.
        /// </param>
        /// <param name="image_size">
        /// Size of the image
        /// </param>
        /// <param name="D">
        /// Input vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="R">
        /// Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3
        ///      1-channel or 1x1 3-channel
        /// </param>
        /// <param name="P">
        /// New camera intrinsic matrix (3x3) or new projection matrix (3x4)
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="new_size">
        /// the new size
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_estimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, in Vec2d image_size, Mat R, Mat P)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (P != null) P.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1estimateNewCameraMatrixForUndistortRectify_13(K.nativeObj, D.nativeObj, image_size.Item1, image_size.Item2, R.nativeObj, P.nativeObj);


        }


        //
        // C++:  double cv::fisheye::calibrate(vector_Mat objectPoints, vector_Mat imagePoints, Size image_size, Mat& K, Mat& D, vector_Mat& rvecs, vector_Mat& tvecs, int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 100, DBL_EPSILON))
        //

        /// <summary>
        ///  Performs camera calibration
        /// </summary>
        /// <param name="objectPoints">
        /// vector of vectors of calibration pattern points in the calibration pattern
        ///      coordinate space.
        /// </param>
        /// <param name="imagePoints">
        /// vector of vectors of the projections of calibration pattern points.
        ///      imagePoints.size() and objectPoints.size() and imagePoints[i].size() must be equal to
        ///      objectPoints[i].size() for each i.
        /// </param>
        /// <param name="image_size">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="K">
        /// Output 3x3 floating-point camera intrinsic matrix
        ///      \f$\cameramatrix{A}\f$ . If
        ///      @ref fisheye::CALIB_USE_INTRINSIC_GUESS is specified, some or all of fx, fy, cx, cy must be
        ///      initialized before calling the function.
        /// </param>
        /// <param name="D">
        /// Output vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see @ref Rodrigues ) estimated for each pattern view.
        ///      That is, each k-th rotation vector together with the corresponding k-th translation vector (see
        ///      the next output parameter description) brings the calibration pattern from the model coordinate
        ///      space (in which object points are specified) to the world coordinate space, that is, a real
        ///      position of the calibration pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  cameraMatrix contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients
        ///      are set to zeros and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_PRINCIPAL_POINT  The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        ///      -    @ref fisheye::CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global
        ///  optimization. It is the \f$max(width,height)/\pi\f$ or the provided \f$f_x\f$, \f$f_y\f$ when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_calibrate(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d image_size, Mat K, Mat D, List<Mat> rvecs, List<Mat> tvecs, int flags, in Vec3d criteria)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1calibrate_10(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, image_size.Item1, image_size.Item2, K.nativeObj, D.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Performs camera calibration
        /// </summary>
        /// <param name="objectPoints">
        /// vector of vectors of calibration pattern points in the calibration pattern
        ///      coordinate space.
        /// </param>
        /// <param name="imagePoints">
        /// vector of vectors of the projections of calibration pattern points.
        ///      imagePoints.size() and objectPoints.size() and imagePoints[i].size() must be equal to
        ///      objectPoints[i].size() for each i.
        /// </param>
        /// <param name="image_size">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="K">
        /// Output 3x3 floating-point camera intrinsic matrix
        ///      \f$\cameramatrix{A}\f$ . If
        ///      @ref fisheye::CALIB_USE_INTRINSIC_GUESS is specified, some or all of fx, fy, cx, cy must be
        ///      initialized before calling the function.
        /// </param>
        /// <param name="D">
        /// Output vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see @ref Rodrigues ) estimated for each pattern view.
        ///      That is, each k-th rotation vector together with the corresponding k-th translation vector (see
        ///      the next output parameter description) brings the calibration pattern from the model coordinate
        ///      space (in which object points are specified) to the world coordinate space, that is, a real
        ///      position of the calibration pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  cameraMatrix contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients
        ///      are set to zeros and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_PRINCIPAL_POINT  The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        ///      -    @ref fisheye::CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global
        ///  optimization. It is the \f$max(width,height)/\pi\f$ or the provided \f$f_x\f$, \f$f_y\f$ when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_calibrate(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d image_size, Mat K, Mat D, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1calibrate_11(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, image_size.Item1, image_size.Item2, K.nativeObj, D.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Performs camera calibration
        /// </summary>
        /// <param name="objectPoints">
        /// vector of vectors of calibration pattern points in the calibration pattern
        ///      coordinate space.
        /// </param>
        /// <param name="imagePoints">
        /// vector of vectors of the projections of calibration pattern points.
        ///      imagePoints.size() and objectPoints.size() and imagePoints[i].size() must be equal to
        ///      objectPoints[i].size() for each i.
        /// </param>
        /// <param name="image_size">
        /// Size of the image used only to initialize the camera intrinsic matrix.
        /// </param>
        /// <param name="K">
        /// Output 3x3 floating-point camera intrinsic matrix
        ///      \f$\cameramatrix{A}\f$ . If
        ///      @ref fisheye::CALIB_USE_INTRINSIC_GUESS is specified, some or all of fx, fy, cx, cy must be
        ///      initialized before calling the function.
        /// </param>
        /// <param name="D">
        /// Output vector of distortion coefficients \f$\distcoeffsfisheye\f$.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see @ref Rodrigues ) estimated for each pattern view.
        ///      That is, each k-th rotation vector together with the corresponding k-th translation vector (see
        ///      the next output parameter description) brings the calibration pattern from the model coordinate
        ///      space (in which object points are specified) to the world coordinate space, that is, a real
        ///      position of the calibration pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  cameraMatrix contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center ( imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients
        ///      are set to zeros and stay zero.
        ///      -    @ref fisheye::CALIB_FIX_PRINCIPAL_POINT  The principal point is not changed during the global
        ///  optimization. It stays at the center or at a different location specified when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        ///      -    @ref fisheye::CALIB_FIX_FOCAL_LENGTH The focal length is not changed during the global
        ///  optimization. It is the \f$max(width,height)/\pi\f$ or the provided \f$f_x\f$, \f$f_y\f$ when @ref fisheye::CALIB_USE_INTRINSIC_GUESS is set too.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_calibrate(List<Mat> objectPoints, List<Mat> imagePoints, in Vec2d image_size, Mat K, Mat D, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (K != null) K.ThrowIfDisposed();
            if (D != null) D.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints_mat = Converters.vector_Mat_to_Mat(imagePoints);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1calibrate_12(objectPoints_mat.nativeObj, imagePoints_mat.nativeObj, image_size.Item1, image_size.Item2, K.nativeObj, D.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  void cv::fisheye::stereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, Size imageSize, Mat R, Mat tvec, Mat& R1, Mat& R2, Mat& P1, Mat& P2, Mat& Q, int flags, Size newImageSize = Size(), double balance = 0.0, double fov_scale = 1.0)
        //

        /// <summary>
        ///  Stereo rectification for fisheye camera model
        /// </summary>
        /// <param name="K1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="D1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="K2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="D2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix between the coordinate systems of the first and the second
        ///      cameras.
        /// </param>
        /// <param name="tvec">
        /// Translation vector between coordinate systems of the cameras.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///      camera.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///      camera.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see #reprojectImageTo3D ).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref fisheye::CALIB_ZERO_DISPARITY . If the flag is set,
        ///      the function makes the principal points of each camera have the same pixel coordinates in the
        ///      rectified views. And if the flag is not set, the function may still shift the images in the
        ///      horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///      useful image area.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///      #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///      is passed (default), it is set to the original imageSize . Setting it to larger value can help you
        ///      preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_stereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat tvec, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, in Vec2d newImageSize, double balance, double fov_scale)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1stereoRectify_10(K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, tvec.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, newImageSize.Item1, newImageSize.Item2, balance, fov_scale);


        }

        /// <summary>
        ///  Stereo rectification for fisheye camera model
        /// </summary>
        /// <param name="K1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="D1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="K2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="D2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix between the coordinate systems of the first and the second
        ///      cameras.
        /// </param>
        /// <param name="tvec">
        /// Translation vector between coordinate systems of the cameras.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///      camera.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///      camera.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see #reprojectImageTo3D ).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref fisheye::CALIB_ZERO_DISPARITY . If the flag is set,
        ///      the function makes the principal points of each camera have the same pixel coordinates in the
        ///      rectified views. And if the flag is not set, the function may still shift the images in the
        ///      horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///      useful image area.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///      #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///      is passed (default), it is set to the original imageSize . Setting it to larger value can help you
        ///      preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_stereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat tvec, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, in Vec2d newImageSize, double balance)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1stereoRectify_11(K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, tvec.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, newImageSize.Item1, newImageSize.Item2, balance);


        }

        /// <summary>
        ///  Stereo rectification for fisheye camera model
        /// </summary>
        /// <param name="K1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="D1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="K2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="D2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix between the coordinate systems of the first and the second
        ///      cameras.
        /// </param>
        /// <param name="tvec">
        /// Translation vector between coordinate systems of the cameras.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///      camera.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///      camera.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see #reprojectImageTo3D ).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref fisheye::CALIB_ZERO_DISPARITY . If the flag is set,
        ///      the function makes the principal points of each camera have the same pixel coordinates in the
        ///      rectified views. And if the flag is not set, the function may still shift the images in the
        ///      horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///      useful image area.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///      #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///      is passed (default), it is set to the original imageSize . Setting it to larger value can help you
        ///      preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_stereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat tvec, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, in Vec2d newImageSize)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1stereoRectify_12(K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, tvec.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags, newImageSize.Item1, newImageSize.Item2);


        }

        /// <summary>
        ///  Stereo rectification for fisheye camera model
        /// </summary>
        /// <param name="K1">
        /// First camera intrinsic matrix.
        /// </param>
        /// <param name="D1">
        /// First camera distortion parameters.
        /// </param>
        /// <param name="K2">
        /// Second camera intrinsic matrix.
        /// </param>
        /// <param name="D2">
        /// Second camera distortion parameters.
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used for stereo calibration.
        /// </param>
        /// <param name="R">
        /// Rotation matrix between the coordinate systems of the first and the second
        ///      cameras.
        /// </param>
        /// <param name="tvec">
        /// Translation vector between coordinate systems of the cameras.
        /// </param>
        /// <param name="R1">
        /// Output 3x3 rectification transform (rotation matrix) for the first camera.
        /// </param>
        /// <param name="R2">
        /// Output 3x3 rectification transform (rotation matrix) for the second camera.
        /// </param>
        /// <param name="P1">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the first
        ///      camera.
        /// </param>
        /// <param name="P2">
        /// Output 3x4 projection matrix in the new (rectified) coordinate systems for the second
        ///      camera.
        /// </param>
        /// <param name="Q">
        /// Output \f$4 \times 4\f$ disparity-to-depth mapping matrix (see #reprojectImageTo3D ).
        /// </param>
        /// <param name="flags">
        /// Operation flags that may be zero or @ref fisheye::CALIB_ZERO_DISPARITY . If the flag is set,
        ///      the function makes the principal points of each camera have the same pixel coordinates in the
        ///      rectified views. And if the flag is not set, the function may still shift the images in the
        ///      horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the
        ///      useful image area.
        /// </param>
        /// <param name="newImageSize">
        /// New image resolution after rectification. The same size should be passed to
        ///      #initUndistortRectifyMap (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0)
        ///      is passed (default), it is set to the original imageSize . Setting it to larger value can help you
        ///      preserve details in the original image, especially when there is a big radial distortion.
        /// </param>
        /// <param name="balance">
        /// Sets the new focal length in range between the min focal length and the max focal
        ///      length. Balance is in range of [0, 1].
        /// </param>
        /// <param name="fov_scale">
        /// Divisor for new focal length.
        /// </param>
        public static void fisheye_stereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat tvec, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            if (R1 != null) R1.ThrowIfDisposed();
            if (R2 != null) R2.ThrowIfDisposed();
            if (P1 != null) P1.ThrowIfDisposed();
            if (P2 != null) P2.ThrowIfDisposed();
            if (Q != null) Q.ThrowIfDisposed();

            calib3d_Calib3d_fisheye_1stereoRectify_13(K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, tvec.nativeObj, R1.nativeObj, R2.nativeObj, P1.nativeObj, P2.nativeObj, Q.nativeObj, flags);


        }


        //
        // C++:  double cv::fisheye::stereoCalibrate(vector_Mat objectPoints, vector_Mat imagePoints1, vector_Mat imagePoints2, Mat& K1, Mat& D1, Mat& K2, Mat& D2, Size imageSize, Mat& R, Mat& T, vector_Mat& rvecs, vector_Mat& tvecs, int flags = fisheye::CALIB_FIX_INTRINSIC, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 100, DBL_EPSILON))
        //

        /// <summary>
        ///  Performs stereo calibration
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the first camera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the second camera.
        /// </param>
        /// <param name="K1">
        /// Input/output first camera intrinsic matrix:
        ///      \f$\vecthreethree{f_x^{(j)}}{0}{c_x^{(j)}}{0}{f_y^{(j)}}{c_y^{(j)}}{0}{0}{1}\f$ , \f$j = 0,\, 1\f$ . If
        ///      any of @ref fisheye::CALIB_USE_INTRINSIC_GUESS , @ref fisheye::CALIB_FIX_INTRINSIC are specified,
        ///      some or all of the matrix components must be initialized.
        /// </param>
        /// <param name="D1">
        /// Input/output vector of distortion coefficients \f$\distcoeffsfisheye\f$ of 4 elements.
        /// </param>
        /// <param name="K2">
        /// Input/output second camera intrinsic matrix. The parameter is similar to K1 .
        /// </param>
        /// <param name="D2">
        /// Input/output lens distortion coefficients for the second camera. The parameter is
        ///      similar to D1 .
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize camera intrinsic matrix.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix between the 1st and the 2nd camera coordinate systems.
        /// </param>
        /// <param name="T">
        /// Output translation vector between the coordinate systems of the cameras.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///      coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///      i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///      description) brings the calibration pattern from the object coordinate space (in which object points are
        ///      specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///      the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///      to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///      of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_FIX_INTRINSIC  Fix K1, K2? and D1, D2? so that only R, T matrices
        ///      are estimated.
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  K1, K2 contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center (imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -   @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients are set to zeros and stay
        ///      zero.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T, List<Mat> rvecs, List<Mat> tvecs, int flags, in Vec3d criteria)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1stereoCalibrate_10(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Performs stereo calibration
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the first camera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the second camera.
        /// </param>
        /// <param name="K1">
        /// Input/output first camera intrinsic matrix:
        ///      \f$\vecthreethree{f_x^{(j)}}{0}{c_x^{(j)}}{0}{f_y^{(j)}}{c_y^{(j)}}{0}{0}{1}\f$ , \f$j = 0,\, 1\f$ . If
        ///      any of @ref fisheye::CALIB_USE_INTRINSIC_GUESS , @ref fisheye::CALIB_FIX_INTRINSIC are specified,
        ///      some or all of the matrix components must be initialized.
        /// </param>
        /// <param name="D1">
        /// Input/output vector of distortion coefficients \f$\distcoeffsfisheye\f$ of 4 elements.
        /// </param>
        /// <param name="K2">
        /// Input/output second camera intrinsic matrix. The parameter is similar to K1 .
        /// </param>
        /// <param name="D2">
        /// Input/output lens distortion coefficients for the second camera. The parameter is
        ///      similar to D1 .
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize camera intrinsic matrix.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix between the 1st and the 2nd camera coordinate systems.
        /// </param>
        /// <param name="T">
        /// Output translation vector between the coordinate systems of the cameras.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///      coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///      i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///      description) brings the calibration pattern from the object coordinate space (in which object points are
        ///      specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///      the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///      to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///      of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_FIX_INTRINSIC  Fix K1, K2? and D1, D2? so that only R, T matrices
        ///      are estimated.
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  K1, K2 contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center (imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -   @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients are set to zeros and stay
        ///      zero.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1stereoCalibrate_11(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Performs stereo calibration
        /// </summary>
        /// <param name="objectPoints">
        /// Vector of vectors of the calibration pattern points.
        /// </param>
        /// <param name="imagePoints1">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the first camera.
        /// </param>
        /// <param name="imagePoints2">
        /// Vector of vectors of the projections of the calibration pattern points,
        ///      observed by the second camera.
        /// </param>
        /// <param name="K1">
        /// Input/output first camera intrinsic matrix:
        ///      \f$\vecthreethree{f_x^{(j)}}{0}{c_x^{(j)}}{0}{f_y^{(j)}}{c_y^{(j)}}{0}{0}{1}\f$ , \f$j = 0,\, 1\f$ . If
        ///      any of @ref fisheye::CALIB_USE_INTRINSIC_GUESS , @ref fisheye::CALIB_FIX_INTRINSIC are specified,
        ///      some or all of the matrix components must be initialized.
        /// </param>
        /// <param name="D1">
        /// Input/output vector of distortion coefficients \f$\distcoeffsfisheye\f$ of 4 elements.
        /// </param>
        /// <param name="K2">
        /// Input/output second camera intrinsic matrix. The parameter is similar to K1 .
        /// </param>
        /// <param name="D2">
        /// Input/output lens distortion coefficients for the second camera. The parameter is
        ///      similar to D1 .
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize camera intrinsic matrix.
        /// </param>
        /// <param name="R">
        /// Output rotation matrix between the 1st and the 2nd camera coordinate systems.
        /// </param>
        /// <param name="T">
        /// Output translation vector between the coordinate systems of the cameras.
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors ( @ref Rodrigues ) estimated for each pattern view in the
        ///      coordinate system of the first camera of the stereo pair (e.g. std::vector&lt;cv::Mat&gt;). More in detail, each
        ///      i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter
        ///      description) brings the calibration pattern from the object coordinate space (in which object points are
        ///      specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms,
        ///      the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space
        ///      to camera coordinate space of the first camera of the stereo pair.
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view, see parameter description
        ///      of previous output parameter ( rvecs ).
        /// </param>
        /// <param name="flags">
        /// Different flags that may be zero or a combination of the following values:
        ///      -    @ref fisheye::CALIB_FIX_INTRINSIC  Fix K1, K2? and D1, D2? so that only R, T matrices
        ///      are estimated.
        ///      -    @ref fisheye::CALIB_USE_INTRINSIC_GUESS  K1, K2 contains valid initial values of
        ///      fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image
        ///      center (imageSize is used), and focal distances are computed in a least-squares fashion.
        ///      -    @ref fisheye::CALIB_RECOMPUTE_EXTRINSIC  Extrinsic will be recomputed after each iteration
        ///      of intrinsic optimization.
        ///      -    @ref fisheye::CALIB_CHECK_COND  The functions will check validity of condition number.
        ///      -    @ref fisheye::CALIB_FIX_SKEW  Skew coefficient (alpha) is set to zero and stay zero.
        ///      -   @ref fisheye::CALIB_FIX_K1,..., @ref fisheye::CALIB_FIX_K4 Selected distortion coefficients are set to zeros and stay
        ///      zero.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = calib3d_Calib3d_fisheye_1stereoCalibrate_12(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::fisheye::stereoCalibrate(vector_Mat objectPoints, vector_Mat imagePoints1, vector_Mat imagePoints2, Mat& K1, Mat& D1, Mat& K2, Mat& D2, Size imageSize, Mat& R, Mat& T, int flags = fisheye::CALIB_FIX_INTRINSIC, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 100, DBL_EPSILON))
        //

        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T, int flags, in Vec3d criteria)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_fisheye_1stereoCalibrate_13(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }

        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T, int flags)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_fisheye_1stereoCalibrate_14(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj, flags);


        }

        public static double fisheye_stereoCalibrate(List<Mat> objectPoints, List<Mat> imagePoints1, List<Mat> imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, in Vec2d imageSize, Mat R, Mat T)
        {
            if (K1 != null) K1.ThrowIfDisposed();
            if (D1 != null) D1.ThrowIfDisposed();
            if (K2 != null) K2.ThrowIfDisposed();
            if (D2 != null) D2.ThrowIfDisposed();
            if (R != null) R.ThrowIfDisposed();
            if (T != null) T.ThrowIfDisposed();
            using Mat objectPoints_mat = Converters.vector_Mat_to_Mat(objectPoints);
            using Mat imagePoints1_mat = Converters.vector_Mat_to_Mat(imagePoints1);
            using Mat imagePoints2_mat = Converters.vector_Mat_to_Mat(imagePoints2);
            return calib3d_Calib3d_fisheye_1stereoCalibrate_15(objectPoints_mat.nativeObj, imagePoints1_mat.nativeObj, imagePoints2_mat.nativeObj, K1.nativeObj, D1.nativeObj, K2.nativeObj, D2.nativeObj, imageSize.Item1, imageSize.Item2, R.nativeObj, T.nativeObj);


        }


        //
        // C++:  bool cv::fisheye::solvePnP(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false, int flags = SOLVEPNP_ITERATIVE, TermCriteria criteria = TermCriteria(TermCriteria::MAX_ITER + TermCriteria::EPS, 10, 1e-8))
        //

        /// <summary>
        ///  Finds an object pose from 3D-2D point correspondences for fisheye camera moodel.
        /// </summary>
        /// <param name="objectPoints">
        /// Array of object points in the object coordinate space, Nx3 1-channel or
        ///      1xN/Nx1 3-channel, where N is the number of points. vector&lt;Point3d&gt; can also be passed here.
        /// </param>
        /// <param name="imagePoints">
        /// Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel,
        ///      where N is the number of points. vector&lt;Point2d&gt; can also be passed here.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix \f$\cameramatrix{A}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients (4x1/1x4).
        /// </param>
        /// <param name="rvec">
        /// Output rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///      the model coordinate system to the camera coordinate system.
        /// </param>
        /// <param name="tvec">
        /// Output translation vector.
        /// </param>
        /// <param name="useExtrinsicGuess">
        /// Parameter used for #SOLVEPNP_ITERATIVE. If true (1), the function uses
        ///      the provided rvec and tvec values as initial approximations of the rotation and translation
        ///      vectors, respectively, and further optimizes them.
        /// </param>
        /// <param name="flags">
        /// Method for solving a PnP problem: see @ref calib3d_solvePnP_flags
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for internal undistortPoints call.
        ///      The function interally undistorts points with @ref undistortPoints and call @ref cv::solvePnP,
        ///      thus the input are very similar. More information about Perspective-n-Points is described in @ref calib3d_solvePnP
        ///      for more information.
        /// </param>
        public static bool fisheye_solvePnP(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int flags, in Vec3d criteria)
        {
            if (objectPoints != null) objectPoints.ThrowIfDisposed();
            if (imagePoints != null) imagePoints.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            return calib3d_Calib3d_fisheye_1solvePnP_10(objectPoints.nativeObj, imagePoints.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, useExtrinsicGuess, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }


        //
        // C++:  bool cv::fisheye::solvePnPRansac(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false, int iterationsCount = 100, float reprojectionError = 8.0, double confidence = 0.99, Mat& inliers = Mat(), int flags = SOLVEPNP_ITERATIVE, TermCriteria criteria = TermCriteria(TermCriteria::MAX_ITER + TermCriteria::EPS, 10, 1e-8))
        //

        /// <summary>
        ///  Finds an object pose from 3D-2D point correspondences using the RANSAC scheme for fisheye camera moodel.
        /// </summary>
        /// <param name="objectPoints">
        /// Array of object points in the object coordinate space, Nx3 1-channel or
        ///      1xN/Nx1 3-channel, where N is the number of points. vector&lt;Point3d&gt; can be also passed here.
        /// </param>
        /// <param name="imagePoints">
        /// Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel,
        ///      where N is the number of points. vector&lt;Point2d&gt; can be also passed here.
        /// </param>
        /// <param name="cameraMatrix">
        /// Input camera intrinsic matrix \f$\cameramatrix{A}\f$ .
        /// </param>
        /// <param name="distCoeffs">
        /// Input vector of distortion coefficients (4x1/1x4).
        /// </param>
        /// <param name="rvec">
        /// Output rotation vector (see @ref Rodrigues ) that, together with tvec, brings points from
        ///      the model coordinate system to the camera coordinate system.
        /// </param>
        /// <param name="tvec">
        /// Output translation vector.
        /// </param>
        /// <param name="useExtrinsicGuess">
        /// Parameter used for #SOLVEPNP_ITERATIVE. If true (1), the function uses
        ///      the provided rvec and tvec values as initial approximations of the rotation and translation
        ///      vectors, respectively, and further optimizes them.
        /// </param>
        /// <param name="iterationsCount">
        /// Number of iterations.
        /// </param>
        /// <param name="reprojectionError">
        /// Inlier threshold value used by the RANSAC procedure. The parameter value
        ///      is the maximum allowed distance between the observed and computed point projections to consider it
        ///      an inlier.
        /// </param>
        /// <param name="confidence">
        /// The probability that the algorithm produces a useful result.
        /// </param>
        /// <param name="inliers">
        /// Output vector that contains indices of inliers in objectPoints and imagePoints .
        /// </param>
        /// <param name="flags">
        /// Method for solving a PnP problem: see @ref calib3d_solvePnP_flags
        ///      This function returns the rotation and the translation vectors that transform a 3D point expressed in the object
        ///      coordinate frame to the camera coordinate frame, using different methods:
        ///      - P3P methods (@ref SOLVEPNP_P3P, @ref SOLVEPNP_AP3P): need 4 input points to return a unique solution.
        ///      - @ref SOLVEPNP_IPPE Input points must be &gt;= 4 and object points must be coplanar.
        ///      - @ref SOLVEPNP_IPPE_SQUARE Special case suitable for marker pose estimation.
        ///      Number of input points must be 4. Object points must be defined in the following order:
        ///      - point 0: [-squareLength / 2,  squareLength / 2, 0]
        ///      - point 1: [ squareLength / 2,  squareLength / 2, 0]
        ///      - point 2: [ squareLength / 2, -squareLength / 2, 0]
        ///      - point 3: [-squareLength / 2, -squareLength / 2, 0]
        ///      - for all the other flags, number of input points must be &gt;= 4 and object points can be in any configuration.
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for internal undistortPoints call.
        ///      The function interally undistorts points with @ref undistortPoints and call @ref cv::solvePnP,
        ///      thus the input are very similar. More information about Perspective-n-Points is described in @ref calib3d_solvePnP
        ///      for more information.
        /// </param>
        public static bool fisheye_solvePnPRansac(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int iterationsCount, float reprojectionError, double confidence, Mat inliers, int flags, in Vec3d criteria)
        {
            if (objectPoints != null) objectPoints.ThrowIfDisposed();
            if (imagePoints != null) imagePoints.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            if (inliers != null) inliers.ThrowIfDisposed();

            return calib3d_Calib3d_fisheye_1solvePnPRansac_10(objectPoints.nativeObj, imagePoints.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, useExtrinsicGuess, iterationsCount, reprojectionError, confidence, inliers.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);


        }

    }
}
