
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ArucoModule
{
    public partial class Aruco
    {


        //
        // C++:  void cv::aruco::drawPlanarBoard(Ptr_Board board, Size outSize, Mat& img, int marginSize, int borderBits)
        //

        /// <summary>
        ///  draw planar board
        ///  @deprecated Use Board::generateImage
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void drawPlanarBoard(Board board, in Vec2d outSize, Mat img, int marginSize, int borderBits)
        {
            if (board != null) board.ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            aruco_Aruco_drawPlanarBoard_10(board.getNativeObjAddr(), outSize.Item1, outSize.Item2, img.nativeObj, marginSize, borderBits);


        }


        //
        // C++:  double cv::aruco::calibrateCameraAruco(vector_Mat corners, Mat ids, Mat counter, Ptr_Board board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <summary>
        ///  Calibrate a camera using aruco markers
        /// </summary>
        /// <param name="corners">
        /// vector of detected marker corners in all frames.
        ///     The corners should have the same format returned by detectMarkers (see #detectMarkers).
        /// </param>
        /// <param name="ids">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="counter">
        /// number of markers in each frame so that corners and ids can be split
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using an Aruco Board. The function receives a list of
        ///     detected markers from several views of the Board. The process is similar to the chessboard
        ///     calibration in calibrateCamera(). The function returns the final re-projection error.
        ///    
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_10(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrate a camera using aruco markers
        /// </summary>
        /// <param name="corners">
        /// vector of detected marker corners in all frames.
        ///     The corners should have the same format returned by detectMarkers (see #detectMarkers).
        /// </param>
        /// <param name="ids">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="counter">
        /// number of markers in each frame so that corners and ids can be split
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using an Aruco Board. The function receives a list of
        ///     detected markers from several views of the Board. The process is similar to the chessboard
        ///     calibration in calibrateCamera(). The function returns the final re-projection error.
        ///    
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_11(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrate a camera using aruco markers
        /// </summary>
        /// <param name="corners">
        /// vector of detected marker corners in all frames.
        ///     The corners should have the same format returned by detectMarkers (see #detectMarkers).
        /// </param>
        /// <param name="ids">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="counter">
        /// number of markers in each frame so that corners and ids can be split
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// Size of the image used only to initialize the intrinsic camera matrix.
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using an Aruco Board. The function receives a list of
        ///     detected markers from several views of the Board. The process is similar to the chessboard
        ///     calibration in calibrateCamera(). The function returns the final re-projection error.
        ///    
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_12(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::aruco::calibrateCameraAruco(vector_Mat corners, Mat ids, Mat counter, Ptr_Board board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs = vector_Mat(), vector_Mat& tvecs = vector_Mat(), int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <summary>
        ///  It's the same function as #calibrateCameraAruco but without calibration error estimation.
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags, in Vec3d criteria)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_10(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <summary>
        ///  It's the same function as #calibrateCameraAruco but without calibration error estimation.
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_11(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <summary>
        ///  It's the same function as #calibrateCameraAruco but without calibration error estimation.
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_12(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <summary>
        ///  It's the same function as #calibrateCameraAruco but without calibration error estimation.
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_13(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            return retVal;
        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <summary>
        ///  It's the same function as #calibrateCameraAruco but without calibration error estimation.
        ///     @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            return aruco_Aruco_calibrateCameraAruco_14(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj);


        }


        //
        // C++:  double cv::aruco::calibrateCameraCharuco(vector_Mat charucoCorners, vector_Mat charucoIds, Ptr_CharucoBoard board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <summary>
        ///  Calibrate a camera using Charuco corners
        /// </summary>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners per frame
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners per frame
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// input image size
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using a set of corners of a  Charuco Board. The function
        ///     receives a list of detected corners and its identifiers from several views of the Board.
        ///     The function returns the final re-projection error.
        ///    
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, in Vec3d criteria)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_10(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrate a camera using Charuco corners
        /// </summary>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners per frame
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners per frame
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// input image size
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using a set of corners of a  Charuco Board. The function
        ///     receives a list of detected corners and its identifiers from several views of the Board.
        ///     The function returns the final re-projection error.
        ///    
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_11(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  Calibrate a camera using Charuco corners
        /// </summary>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners per frame
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners per frame
        /// </param>
        /// <param name="board">
        /// Marker Board layout
        /// </param>
        /// <param name="imageSize">
        /// input image size
        /// </param>
        /// <param name="cameraMatrix">
        /// Output 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$ . If CV\_CALIB\_USE\_INTRINSIC\_GUESS
        ///     and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be
        ///     initialized before calling the function.
        /// </param>
        /// <param name="distCoeffs">
        /// Output vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvecs">
        /// Output vector of rotation vectors (see Rodrigues ) estimated for each board view
        ///     (e.g. std::vector&lt;cv::Mat&gt;&gt;). That is, each k-th rotation vector together with the corresponding
        ///     k-th translation vector (see the next output parameter description) brings the board pattern
        ///     from the model coordinate space (in which object points are specified) to the world coordinate
        ///     space, that is, a real position of the board pattern in the k-th pattern view (k=0.. *M* -1).
        /// </param>
        /// <param name="tvecs">
        /// Output vector of translation vectors estimated for each pattern view.
        /// </param>
        /// <param name="stdDeviationsIntrinsics">
        /// Output vector of standard deviations estimated for intrinsic parameters.
        ///     Order of deviations values:
        ///     \f$(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3,
        ///     s_4, \tau_x, \tau_y)\f$ If one of parameters is not estimated, it's deviation is equals to zero.
        /// </param>
        /// <param name="stdDeviationsExtrinsics">
        /// Output vector of standard deviations estimated for extrinsic parameters.
        ///     Order of deviations values: \f$(R_1, T_1, \dotsc , R_M, T_M)\f$ where M is number of pattern views,
        ///     \f$R_i, T_i\f$ are concatenated 1x3 vectors.
        /// </param>
        /// <param name="perViewErrors">
        /// Output vector of average re-projection errors estimated for each pattern view.
        /// </param>
        /// <param name="flags">
        /// flags Different flags  for the calibration process (see #calibrateCamera for details).
        /// </param>
        /// <param name="criteria">
        /// Termination criteria for the iterative optimization algorithm.
        /// </param>
        /// <remarks>
        ///     This function calibrates a camera using a set of corners of a  Charuco Board. The function
        ///     receives a list of detected corners and its identifiers from several views of the Board.
        ///     The function returns the final re-projection error.
        ///    
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (stdDeviationsIntrinsics != null) stdDeviationsIntrinsics.ThrowIfDisposed();
            if (stdDeviationsExtrinsics != null) stdDeviationsExtrinsics.ThrowIfDisposed();
            if (perViewErrors != null) perViewErrors.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_12(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }


        //
        // C++:  double cv::aruco::calibrateCameraCharuco(vector_Mat charucoCorners, vector_Mat charucoIds, Ptr_CharucoBoard board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs = vector_Mat(), vector_Mat& tvecs = vector_Mat(), int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        //

        /// <summary>
        ///  It's the same function as #calibrateCameraCharuco but without calibration error estimation.
        /// </summary>
        /// <remarks>
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags, in Vec3d criteria)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_10(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, (int)criteria.Item1, (int)criteria.Item2, criteria.Item3);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  It's the same function as #calibrateCameraCharuco but without calibration error estimation.
        /// </summary>
        /// <remarks>
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_11(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  It's the same function as #calibrateCameraCharuco but without calibration error estimation.
        /// </summary>
        /// <remarks>
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_12(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            Converters.Mat_to_vector_Mat(tvecs_mat, tvecs);
            return retVal;
        }

        /// <summary>
        ///  It's the same function as #calibrateCameraCharuco but without calibration error estimation.
        /// </summary>
        /// <remarks>
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_13(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj);
            Converters.Mat_to_vector_Mat(rvecs_mat, rvecs);
            return retVal;
        }

        /// <summary>
        ///  It's the same function as #calibrateCameraCharuco but without calibration error estimation.
        /// </summary>
        /// <remarks>
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, in Vec2d imageSize, Mat cameraMatrix, Mat distCoeffs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            return aruco_Aruco_calibrateCameraCharuco_14(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.Item1, imageSize.Item2, cameraMatrix.nativeObj, distCoeffs.nativeObj);


        }

    }
}
