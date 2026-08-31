
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ArucoModule
{
    // C++: class Aruco


    public partial class Aruco
    {

        /// <summary>
        /// C++: enum PatternPositionType (cv.aruco.PatternPositionType)
        /// </summary>
        public const int ARUCO_CCW_CENTER = 0;

        /// <summary>
        /// C++: enum PatternPositionType (cv.aruco.PatternPositionType)
        /// </summary>
        public const int ARUCO_CW_TOP_LEFT_CORNER = 1;


        //
        // C++:  void cv::aruco::detectMarkers(Mat image, Ptr_Dictionary dictionary, vector_Mat& corners, Mat& ids, Ptr_DetectorParameters parameters = makePtr<DetectorParameters>(), vector_Mat& rejectedImgPoints = vector_Mat())
        //

        /// <summary>
        ///  detect markers
        ///  @deprecated Use class ArucoDetector::detectMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void detectMarkers(Mat image, Dictionary dictionary, List<Mat> corners, Mat ids, DetectorParameters parameters, List<Mat> rejectedImgPoints)
        {
            if (image != null) image.ThrowIfDisposed();
            if (dictionary != null) dictionary.ThrowIfDisposed();
            if (ids != null) ids.ThrowIfDisposed();
            if (parameters != null) parameters.ThrowIfDisposed();
            using Mat corners_mat = new Mat();
            using Mat rejectedImgPoints_mat = new Mat();
            aruco_Aruco_detectMarkers_10(image.nativeObj, dictionary.getNativeObjAddr(), corners_mat.nativeObj, ids.nativeObj, parameters.getNativeObjAddr(), rejectedImgPoints_mat.nativeObj);
            Converters.Mat_to_vector_Mat(corners_mat, corners);
            Converters.Mat_to_vector_Mat(rejectedImgPoints_mat, rejectedImgPoints);

        }

        /// <summary>
        ///  detect markers
        ///  @deprecated Use class ArucoDetector::detectMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void detectMarkers(Mat image, Dictionary dictionary, List<Mat> corners, Mat ids, DetectorParameters parameters)
        {
            if (image != null) image.ThrowIfDisposed();
            if (dictionary != null) dictionary.ThrowIfDisposed();
            if (ids != null) ids.ThrowIfDisposed();
            if (parameters != null) parameters.ThrowIfDisposed();
            using Mat corners_mat = new Mat();
            aruco_Aruco_detectMarkers_11(image.nativeObj, dictionary.getNativeObjAddr(), corners_mat.nativeObj, ids.nativeObj, parameters.getNativeObjAddr());
            Converters.Mat_to_vector_Mat(corners_mat, corners);

        }

        /// <summary>
        ///  detect markers
        ///  @deprecated Use class ArucoDetector::detectMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void detectMarkers(Mat image, Dictionary dictionary, List<Mat> corners, Mat ids)
        {
            if (image != null) image.ThrowIfDisposed();
            if (dictionary != null) dictionary.ThrowIfDisposed();
            if (ids != null) ids.ThrowIfDisposed();
            using Mat corners_mat = new Mat();
            aruco_Aruco_detectMarkers_12(image.nativeObj, dictionary.getNativeObjAddr(), corners_mat.nativeObj, ids.nativeObj);
            Converters.Mat_to_vector_Mat(corners_mat, corners);

        }


        //
        // C++:  void cv::aruco::refineDetectedMarkers(Mat image, Ptr_Board board, vector_Mat& detectedCorners, Mat& detectedIds, vector_Mat& rejectedCorners, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), float minRepDistance = 10.f, float errorCorrectionRate = 3.f, bool checkAllOrders = true, Mat& recoveredIdxs = Mat(), Ptr_DetectorParameters parameters = makePtr<DetectorParameters>())
        //

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs, float minRepDistance, float errorCorrectionRate, bool checkAllOrders, Mat recoveredIdxs, DetectorParameters parameters)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (recoveredIdxs != null) recoveredIdxs.ThrowIfDisposed();
            if (parameters != null) parameters.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_10(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minRepDistance, errorCorrectionRate, checkAllOrders, recoveredIdxs.nativeObj, parameters.getNativeObjAddr());
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs, float minRepDistance, float errorCorrectionRate, bool checkAllOrders, Mat recoveredIdxs)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (recoveredIdxs != null) recoveredIdxs.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_11(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minRepDistance, errorCorrectionRate, checkAllOrders, recoveredIdxs.nativeObj);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs, float minRepDistance, float errorCorrectionRate, bool checkAllOrders)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_12(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minRepDistance, errorCorrectionRate, checkAllOrders);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs, float minRepDistance, float errorCorrectionRate)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_13(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minRepDistance, errorCorrectionRate);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs, float minRepDistance)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_14(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minRepDistance);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix, Mat distCoeffs)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_15(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners, Mat cameraMatrix)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_16(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj, cameraMatrix.nativeObj);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }

        /// <summary>
        ///  refine detected markers
        ///  @deprecated Use class ArucoDetector::refineDetectedMarkers
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void refineDetectedMarkers(Mat image, Board board, List<Mat> detectedCorners, Mat detectedIds, List<Mat> rejectedCorners)
        {
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            using Mat rejectedCorners_mat = Converters.vector_Mat_to_Mat(rejectedCorners);
            aruco_Aruco_refineDetectedMarkers_17(image.nativeObj, board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, rejectedCorners_mat.nativeObj);
            Converters.Mat_to_vector_Mat(detectedCorners_mat, detectedCorners);
            Converters.Mat_to_vector_Mat(rejectedCorners_mat, rejectedCorners);

        }


        //
        // C++:  void cv::aruco::drawPlanarBoard(Ptr_Board board, Size outSize, Mat& img, int marginSize, int borderBits)
        //

        /// <summary>
        ///  draw planar board
        ///  @deprecated Use Board::generateImage
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void drawPlanarBoard(Board board, Size outSize, Mat img, int marginSize, int borderBits)
        {
            if (board != null) board.ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            aruco_Aruco_drawPlanarBoard_10(board.getNativeObjAddr(), outSize.width, outSize.height, img.nativeObj, marginSize, borderBits);


        }


        //
        // C++:  void cv::aruco::getBoardObjectAndImagePoints(Ptr_Board board, vector_Mat detectedCorners, Mat detectedIds, Mat& objPoints, Mat& imgPoints)
        //

        /// <summary>
        ///  get board object and image points
        ///  @deprecated Use Board::matchImagePoints
        /// </summary>
        [Obsolete("This method is deprecated.")]
        public static void getBoardObjectAndImagePoints(Board board, List<Mat> detectedCorners, Mat detectedIds, Mat objPoints, Mat imgPoints)
        {
            if (board != null) board.ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (objPoints != null) objPoints.ThrowIfDisposed();
            if (imgPoints != null) imgPoints.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            aruco_Aruco_getBoardObjectAndImagePoints_10(board.getNativeObjAddr(), detectedCorners_mat.nativeObj, detectedIds.nativeObj, objPoints.nativeObj, imgPoints.nativeObj);


        }


        //
        // C++:  int cv::aruco::estimatePoseBoard(vector_Mat corners, Mat ids, Ptr_Board board, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false)
        //

        /// <remarks>
        ///  @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int estimatePoseBoard(List<Mat> corners, Mat ids, Board board, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            return aruco_Aruco_estimatePoseBoard_10(corners_mat.nativeObj, ids.nativeObj, board.getNativeObjAddr(), cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, useExtrinsicGuess);


        }

        /// <remarks>
        ///  @deprecated Use Board::matchImagePoints and cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int estimatePoseBoard(List<Mat> corners, Mat ids, Board board, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            return aruco_Aruco_estimatePoseBoard_11(corners_mat.nativeObj, ids.nativeObj, board.getNativeObjAddr(), cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj);


        }


        //
        // C++:  bool cv::aruco::estimatePoseCharucoBoard(Mat charucoCorners, Mat charucoIds, Ptr_CharucoBoard board, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false)
        //

        /// <summary>
        ///  Pose estimation for a ChArUco board given some of their corners
        /// </summary>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="cameraMatrix">
        /// input 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvec">
        /// Output vector (e.g. cv::Mat) corresponding to the rotation vector of the board
        ///     (see cv::Rodrigues).
        /// </param>
        /// <param name="tvec">
        /// Output vector (e.g. cv::Mat) corresponding to the translation vector of the board.
        /// </param>
        /// <param name="useExtrinsicGuess">
        /// defines whether initial guess for \b rvec and \b tvec will be used or not.
        /// </param>
        /// <remarks>
        ///     This function estimates a Charuco board pose from some detected corners.
        ///     The function checks if the input corners are enough and valid to perform pose estimation.
        ///     If pose estimation is valid, returns true, else returns false.
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        ///     @sa use cv::drawFrameAxes to get world coordinate system axis for object points
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static bool estimatePoseCharucoBoard(Mat charucoCorners, Mat charucoIds, CharucoBoard board, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess)
        {
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            return aruco_Aruco_estimatePoseCharucoBoard_10(charucoCorners.nativeObj, charucoIds.nativeObj, board.getNativeObjAddr(), cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj, useExtrinsicGuess);


        }

        /// <summary>
        ///  Pose estimation for a ChArUco board given some of their corners
        /// </summary>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="cameraMatrix">
        /// input 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="rvec">
        /// Output vector (e.g. cv::Mat) corresponding to the rotation vector of the board
        ///     (see cv::Rodrigues).
        /// </param>
        /// <param name="tvec">
        /// Output vector (e.g. cv::Mat) corresponding to the translation vector of the board.
        /// </param>
        /// <param name="useExtrinsicGuess">
        /// defines whether initial guess for \b rvec and \b tvec will be used or not.
        /// </param>
        /// <remarks>
        ///     This function estimates a Charuco board pose from some detected corners.
        ///     The function checks if the input corners are enough and valid to perform pose estimation.
        ///     If pose estimation is valid, returns true, else returns false.
        ///     @deprecated Use CharucoBoard::matchImagePoints and cv::solvePnP
        ///     @sa use cv::drawFrameAxes to get world coordinate system axis for object points
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static bool estimatePoseCharucoBoard(Mat charucoCorners, Mat charucoIds, CharucoBoard board, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec)
        {
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvec != null) rvec.ThrowIfDisposed();
            if (tvec != null) tvec.ThrowIfDisposed();

            return aruco_Aruco_estimatePoseCharucoBoard_11(charucoCorners.nativeObj, charucoIds.nativeObj, board.getNativeObjAddr(), cameraMatrix.nativeObj, distCoeffs.nativeObj, rvec.nativeObj, tvec.nativeObj);


        }


        //
        // C++:  void cv::aruco::estimatePoseSingleMarkers(vector_Mat corners, float markerLength, Mat cameraMatrix, Mat distCoeffs, Mat& rvecs, Mat& tvecs, Mat& objPoints = Mat(), Ptr_EstimateParameters estimateParameters = makePtr<EstimateParameters>())
        //

        /// <remarks>
        ///  @deprecated Use cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void estimatePoseSingleMarkers(List<Mat> corners, float markerLength, Mat cameraMatrix, Mat distCoeffs, Mat rvecs, Mat tvecs, Mat objPoints, EstimateParameters estimateParameters)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvecs != null) rvecs.ThrowIfDisposed();
            if (tvecs != null) tvecs.ThrowIfDisposed();
            if (objPoints != null) objPoints.ThrowIfDisposed();
            if (estimateParameters != null) estimateParameters.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            aruco_Aruco_estimatePoseSingleMarkers_10(corners_mat.nativeObj, markerLength, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs.nativeObj, tvecs.nativeObj, objPoints.nativeObj, estimateParameters.getNativeObjAddr());


        }

        /// <remarks>
        ///  @deprecated Use cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void estimatePoseSingleMarkers(List<Mat> corners, float markerLength, Mat cameraMatrix, Mat distCoeffs, Mat rvecs, Mat tvecs, Mat objPoints)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvecs != null) rvecs.ThrowIfDisposed();
            if (tvecs != null) tvecs.ThrowIfDisposed();
            if (objPoints != null) objPoints.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            aruco_Aruco_estimatePoseSingleMarkers_11(corners_mat.nativeObj, markerLength, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs.nativeObj, tvecs.nativeObj, objPoints.nativeObj);


        }

        /// <remarks>
        ///  @deprecated Use cv::solvePnP
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void estimatePoseSingleMarkers(List<Mat> corners, float markerLength, Mat cameraMatrix, Mat distCoeffs, Mat rvecs, Mat tvecs)
        {
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (rvecs != null) rvecs.ThrowIfDisposed();
            if (tvecs != null) tvecs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            aruco_Aruco_estimatePoseSingleMarkers_12(corners_mat.nativeObj, markerLength, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs.nativeObj, tvecs.nativeObj);


        }


        //
        // C++:  bool cv::aruco::testCharucoCornersCollinear(Ptr_CharucoBoard board, Mat charucoIds)
        //

        /// <remarks>
        ///  @deprecated Use CharucoBoard::checkCharucoCornersCollinear
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static bool testCharucoCornersCollinear(CharucoBoard board, Mat charucoIds)
        {
            if (board != null) board.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();

            return aruco_Aruco_testCharucoCornersCollinear_10(board.getNativeObjAddr(), charucoIds.nativeObj);


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
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, TermCriteria criteria)
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
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_10(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags, criteria.type, criteria.maxCount, criteria.epsilon);
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
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags)
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
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_11(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags);
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
        public static double calibrateCameraArucoExtended(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors)
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
            double retVal = aruco_Aruco_calibrateCameraArucoExtended_12(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj);
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
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags, TermCriteria criteria)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_10(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, criteria.type, criteria.maxCount, criteria.epsilon);
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
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_11(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
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
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_12(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
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
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            using Mat rvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraAruco_13(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj);
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
        public static double calibrateCameraAruco(List<Mat> corners, Mat ids, Mat counter, Board board, Size imageSize, Mat cameraMatrix, Mat distCoeffs)
        {
            if (ids != null) ids.ThrowIfDisposed();
            if (counter != null) counter.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            return aruco_Aruco_calibrateCameraAruco_14(corners_mat.nativeObj, ids.nativeObj, counter.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj);


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
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, TermCriteria criteria)
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
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_10(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags, criteria.type, criteria.maxCount, criteria.epsilon);
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
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags)
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
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_11(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj, flags);
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
        public static double calibrateCameraCharucoExtended(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors)
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
            double retVal = aruco_Aruco_calibrateCameraCharucoExtended_12(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, stdDeviationsIntrinsics.nativeObj, stdDeviationsExtrinsics.nativeObj, perViewErrors.nativeObj);
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
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags, TermCriteria criteria)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_10(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags, criteria.type, criteria.maxCount, criteria.epsilon);
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
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs, int flags)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_11(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj, flags);
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
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs, List<Mat> tvecs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            using Mat tvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_12(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj, tvecs_mat.nativeObj);
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
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs, List<Mat> rvecs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            using Mat rvecs_mat = new Mat();
            double retVal = aruco_Aruco_calibrateCameraCharuco_13(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj, rvecs_mat.nativeObj);
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
        public static double calibrateCameraCharuco(List<Mat> charucoCorners, List<Mat> charucoIds, CharucoBoard board, Size imageSize, Mat cameraMatrix, Mat distCoeffs)
        {
            if (board != null) board.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat charucoCorners_mat = Converters.vector_Mat_to_Mat(charucoCorners);
            using Mat charucoIds_mat = Converters.vector_Mat_to_Mat(charucoIds);
            return aruco_Aruco_calibrateCameraCharuco_14(charucoCorners_mat.nativeObj, charucoIds_mat.nativeObj, board.getNativeObjAddr(), imageSize.width, imageSize.height, cameraMatrix.nativeObj, distCoeffs.nativeObj);


        }


        //
        // C++:  int cv::aruco::interpolateCornersCharuco(vector_Mat markerCorners, Mat markerIds, Mat image, Ptr_CharucoBoard board, Mat& charucoCorners, Mat& charucoIds, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), int minMarkers = 2)
        //

        /// <summary>
        ///  Interpolate position of ChArUco board corners
        /// </summary>
        /// <param name="markerCorners">
        /// vector of already detected markers corners. For each marker, its four
        ///     corners are provided, (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers, the
        ///     dimensions of this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="markerIds">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="image">
        /// input image necesary for corner refinement. Note that markers are not detected and
        ///     should be sent in corners and ids parameters.
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="charucoCorners">
        /// interpolated chessboard corners
        /// </param>
        /// <param name="charucoIds">
        /// interpolated chessboard corners identifiers
        /// </param>
        /// <param name="cameraMatrix">
        /// optional 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// optional vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="minMarkers">
        /// number of adjacent markers that must be detected to return a charuco corner
        /// </param>
        /// <remarks>
        ///     This function receives the detected markers and returns the 2D position of the chessboard corners
        ///     from a ChArUco board using the detected Aruco markers. If camera parameters are provided,
        ///     the process is based in an approximated pose estimation, else it is based on local homography.
        ///     Only visible corners are returned. For each corner, its corresponding identifier is
        ///     also returned in charucoIds.
        ///     The function returns the number of interpolated corners.
        ///    
        ///     @deprecated Use CharucoDetector::detectBoard
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int interpolateCornersCharuco(List<Mat> markerCorners, Mat markerIds, Mat image, CharucoBoard board, Mat charucoCorners, Mat charucoIds, Mat cameraMatrix, Mat distCoeffs, int minMarkers)
        {
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            return aruco_Aruco_interpolateCornersCharuco_10(markerCorners_mat.nativeObj, markerIds.nativeObj, image.nativeObj, board.getNativeObjAddr(), charucoCorners.nativeObj, charucoIds.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, minMarkers);


        }

        /// <summary>
        ///  Interpolate position of ChArUco board corners
        /// </summary>
        /// <param name="markerCorners">
        /// vector of already detected markers corners. For each marker, its four
        ///     corners are provided, (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers, the
        ///     dimensions of this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="markerIds">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="image">
        /// input image necesary for corner refinement. Note that markers are not detected and
        ///     should be sent in corners and ids parameters.
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="charucoCorners">
        /// interpolated chessboard corners
        /// </param>
        /// <param name="charucoIds">
        /// interpolated chessboard corners identifiers
        /// </param>
        /// <param name="cameraMatrix">
        /// optional 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// optional vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="minMarkers">
        /// number of adjacent markers that must be detected to return a charuco corner
        /// </param>
        /// <remarks>
        ///     This function receives the detected markers and returns the 2D position of the chessboard corners
        ///     from a ChArUco board using the detected Aruco markers. If camera parameters are provided,
        ///     the process is based in an approximated pose estimation, else it is based on local homography.
        ///     Only visible corners are returned. For each corner, its corresponding identifier is
        ///     also returned in charucoIds.
        ///     The function returns the number of interpolated corners.
        ///    
        ///     @deprecated Use CharucoDetector::detectBoard
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int interpolateCornersCharuco(List<Mat> markerCorners, Mat markerIds, Mat image, CharucoBoard board, Mat charucoCorners, Mat charucoIds, Mat cameraMatrix, Mat distCoeffs)
        {
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            return aruco_Aruco_interpolateCornersCharuco_11(markerCorners_mat.nativeObj, markerIds.nativeObj, image.nativeObj, board.getNativeObjAddr(), charucoCorners.nativeObj, charucoIds.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj);


        }

        /// <summary>
        ///  Interpolate position of ChArUco board corners
        /// </summary>
        /// <param name="markerCorners">
        /// vector of already detected markers corners. For each marker, its four
        ///     corners are provided, (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers, the
        ///     dimensions of this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="markerIds">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="image">
        /// input image necesary for corner refinement. Note that markers are not detected and
        ///     should be sent in corners and ids parameters.
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="charucoCorners">
        /// interpolated chessboard corners
        /// </param>
        /// <param name="charucoIds">
        /// interpolated chessboard corners identifiers
        /// </param>
        /// <param name="cameraMatrix">
        /// optional 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// optional vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="minMarkers">
        /// number of adjacent markers that must be detected to return a charuco corner
        /// </param>
        /// <remarks>
        ///     This function receives the detected markers and returns the 2D position of the chessboard corners
        ///     from a ChArUco board using the detected Aruco markers. If camera parameters are provided,
        ///     the process is based in an approximated pose estimation, else it is based on local homography.
        ///     Only visible corners are returned. For each corner, its corresponding identifier is
        ///     also returned in charucoIds.
        ///     The function returns the number of interpolated corners.
        ///    
        ///     @deprecated Use CharucoDetector::detectBoard
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int interpolateCornersCharuco(List<Mat> markerCorners, Mat markerIds, Mat image, CharucoBoard board, Mat charucoCorners, Mat charucoIds, Mat cameraMatrix)
        {
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            return aruco_Aruco_interpolateCornersCharuco_12(markerCorners_mat.nativeObj, markerIds.nativeObj, image.nativeObj, board.getNativeObjAddr(), charucoCorners.nativeObj, charucoIds.nativeObj, cameraMatrix.nativeObj);


        }

        /// <summary>
        ///  Interpolate position of ChArUco board corners
        /// </summary>
        /// <param name="markerCorners">
        /// vector of already detected markers corners. For each marker, its four
        ///     corners are provided, (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers, the
        ///     dimensions of this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="markerIds">
        /// list of identifiers for each marker in corners
        /// </param>
        /// <param name="image">
        /// input image necesary for corner refinement. Note that markers are not detected and
        ///     should be sent in corners and ids parameters.
        /// </param>
        /// <param name="board">
        /// layout of ChArUco board.
        /// </param>
        /// <param name="charucoCorners">
        /// interpolated chessboard corners
        /// </param>
        /// <param name="charucoIds">
        /// interpolated chessboard corners identifiers
        /// </param>
        /// <param name="cameraMatrix">
        /// optional 3x3 floating-point camera matrix
        ///     \f$A = \vecthreethree{f_x}{0}{c_x}{0}{f_y}{c_y}{0}{0}{1}\f$
        /// </param>
        /// <param name="distCoeffs">
        /// optional vector of distortion coefficients
        ///     \f$(k_1, k_2, p_1, p_2[, k_3[, k_4, k_5, k_6],[s_1, s_2, s_3, s_4]])\f$ of 4, 5, 8 or 12 elements
        /// </param>
        /// <param name="minMarkers">
        /// number of adjacent markers that must be detected to return a charuco corner
        /// </param>
        /// <remarks>
        ///     This function receives the detected markers and returns the 2D position of the chessboard corners
        ///     from a ChArUco board using the detected Aruco markers. If camera parameters are provided,
        ///     the process is based in an approximated pose estimation, else it is based on local homography.
        ///     Only visible corners are returned. For each corner, its corresponding identifier is
        ///     also returned in charucoIds.
        ///     The function returns the number of interpolated corners.
        ///    
        ///     @deprecated Use CharucoDetector::detectBoard
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static int interpolateCornersCharuco(List<Mat> markerCorners, Mat markerIds, Mat image, CharucoBoard board, Mat charucoCorners, Mat charucoIds)
        {
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (board != null) board.ThrowIfDisposed();
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            return aruco_Aruco_interpolateCornersCharuco_13(markerCorners_mat.nativeObj, markerIds.nativeObj, image.nativeObj, board.getNativeObjAddr(), charucoCorners.nativeObj, charucoIds.nativeObj);


        }


        //
        // C++:  void cv::aruco::detectCharucoDiamond(Mat image, vector_Mat markerCorners, Mat markerIds, float squareMarkerLengthRate, vector_Mat& diamondCorners, Mat& diamondIds, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), Ptr_Dictionary dictionary = makePtr<Dictionary> (getPredefinedDictionary(PredefinedDictionaryType::DICT_4X4_50)))
        //

        /// <summary>
        ///  Detect ChArUco Diamond markers
        /// </summary>
        /// <param name="image">
        /// input image necessary for corner subpixel.
        /// </param>
        /// <param name="markerCorners">
        /// list of detected marker corners from detectMarkers function.
        /// </param>
        /// <param name="markerIds">
        /// list of marker ids in markerCorners.
        /// </param>
        /// <param name="squareMarkerLengthRate">
        /// rate between square and marker length:
        ///     squareMarkerLengthRate = squareLength/markerLength. The real units are not necessary.
        /// </param>
        /// <param name="diamondCorners">
        /// output list of detected diamond corners (4 corners per diamond). The order
        ///     is the same than in marker corners: top left, top right, bottom right and bottom left. Similar
        ///     format than the corners returned by detectMarkers (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ).
        /// </param>
        /// <param name="diamondIds">
        /// ids of the diamonds in diamondCorners. The id of each diamond is in fact of
        ///     type Vec4i, so each diamond has 4 ids, which are the ids of the aruco markers composing the
        ///     diamond.
        /// </param>
        /// <param name="cameraMatrix">
        /// Optional camera calibration matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Optional camera distortion coefficients.
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers.
        /// </param>
        /// <remarks>
        ///     This function detects Diamond markers from the previous detected ArUco markers. The diamonds
        ///     are returned in the diamondCorners and diamondIds parameters. If camera calibration parameters
        ///     are provided, the diamond search is based on reprojection. If not, diamond search is based on
        ///     homography. Homography is faster than reprojection, but less accurate.
        ///    
        ///     @deprecated Use CharucoDetector::detectDiamonds
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void detectCharucoDiamond(Mat image, List<Mat> markerCorners, Mat markerIds, float squareMarkerLengthRate, List<Mat> diamondCorners, Mat diamondIds, Mat cameraMatrix, Mat distCoeffs, Dictionary dictionary)
        {
            if (image != null) image.ThrowIfDisposed();
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (diamondIds != null) diamondIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            if (dictionary != null) dictionary.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            using Mat diamondCorners_mat = new Mat();
            aruco_Aruco_detectCharucoDiamond_10(image.nativeObj, markerCorners_mat.nativeObj, markerIds.nativeObj, squareMarkerLengthRate, diamondCorners_mat.nativeObj, diamondIds.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj, dictionary.getNativeObjAddr());
            Converters.Mat_to_vector_Mat(diamondCorners_mat, diamondCorners);

        }

        /// <summary>
        ///  Detect ChArUco Diamond markers
        /// </summary>
        /// <param name="image">
        /// input image necessary for corner subpixel.
        /// </param>
        /// <param name="markerCorners">
        /// list of detected marker corners from detectMarkers function.
        /// </param>
        /// <param name="markerIds">
        /// list of marker ids in markerCorners.
        /// </param>
        /// <param name="squareMarkerLengthRate">
        /// rate between square and marker length:
        ///     squareMarkerLengthRate = squareLength/markerLength. The real units are not necessary.
        /// </param>
        /// <param name="diamondCorners">
        /// output list of detected diamond corners (4 corners per diamond). The order
        ///     is the same than in marker corners: top left, top right, bottom right and bottom left. Similar
        ///     format than the corners returned by detectMarkers (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ).
        /// </param>
        /// <param name="diamondIds">
        /// ids of the diamonds in diamondCorners. The id of each diamond is in fact of
        ///     type Vec4i, so each diamond has 4 ids, which are the ids of the aruco markers composing the
        ///     diamond.
        /// </param>
        /// <param name="cameraMatrix">
        /// Optional camera calibration matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Optional camera distortion coefficients.
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers.
        /// </param>
        /// <remarks>
        ///     This function detects Diamond markers from the previous detected ArUco markers. The diamonds
        ///     are returned in the diamondCorners and diamondIds parameters. If camera calibration parameters
        ///     are provided, the diamond search is based on reprojection. If not, diamond search is based on
        ///     homography. Homography is faster than reprojection, but less accurate.
        ///    
        ///     @deprecated Use CharucoDetector::detectDiamonds
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void detectCharucoDiamond(Mat image, List<Mat> markerCorners, Mat markerIds, float squareMarkerLengthRate, List<Mat> diamondCorners, Mat diamondIds, Mat cameraMatrix, Mat distCoeffs)
        {
            if (image != null) image.ThrowIfDisposed();
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (diamondIds != null) diamondIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            if (distCoeffs != null) distCoeffs.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            using Mat diamondCorners_mat = new Mat();
            aruco_Aruco_detectCharucoDiamond_11(image.nativeObj, markerCorners_mat.nativeObj, markerIds.nativeObj, squareMarkerLengthRate, diamondCorners_mat.nativeObj, diamondIds.nativeObj, cameraMatrix.nativeObj, distCoeffs.nativeObj);
            Converters.Mat_to_vector_Mat(diamondCorners_mat, diamondCorners);

        }

        /// <summary>
        ///  Detect ChArUco Diamond markers
        /// </summary>
        /// <param name="image">
        /// input image necessary for corner subpixel.
        /// </param>
        /// <param name="markerCorners">
        /// list of detected marker corners from detectMarkers function.
        /// </param>
        /// <param name="markerIds">
        /// list of marker ids in markerCorners.
        /// </param>
        /// <param name="squareMarkerLengthRate">
        /// rate between square and marker length:
        ///     squareMarkerLengthRate = squareLength/markerLength. The real units are not necessary.
        /// </param>
        /// <param name="diamondCorners">
        /// output list of detected diamond corners (4 corners per diamond). The order
        ///     is the same than in marker corners: top left, top right, bottom right and bottom left. Similar
        ///     format than the corners returned by detectMarkers (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ).
        /// </param>
        /// <param name="diamondIds">
        /// ids of the diamonds in diamondCorners. The id of each diamond is in fact of
        ///     type Vec4i, so each diamond has 4 ids, which are the ids of the aruco markers composing the
        ///     diamond.
        /// </param>
        /// <param name="cameraMatrix">
        /// Optional camera calibration matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Optional camera distortion coefficients.
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers.
        /// </param>
        /// <remarks>
        ///     This function detects Diamond markers from the previous detected ArUco markers. The diamonds
        ///     are returned in the diamondCorners and diamondIds parameters. If camera calibration parameters
        ///     are provided, the diamond search is based on reprojection. If not, diamond search is based on
        ///     homography. Homography is faster than reprojection, but less accurate.
        ///    
        ///     @deprecated Use CharucoDetector::detectDiamonds
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void detectCharucoDiamond(Mat image, List<Mat> markerCorners, Mat markerIds, float squareMarkerLengthRate, List<Mat> diamondCorners, Mat diamondIds, Mat cameraMatrix)
        {
            if (image != null) image.ThrowIfDisposed();
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (diamondIds != null) diamondIds.ThrowIfDisposed();
            if (cameraMatrix != null) cameraMatrix.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            using Mat diamondCorners_mat = new Mat();
            aruco_Aruco_detectCharucoDiamond_12(image.nativeObj, markerCorners_mat.nativeObj, markerIds.nativeObj, squareMarkerLengthRate, diamondCorners_mat.nativeObj, diamondIds.nativeObj, cameraMatrix.nativeObj);
            Converters.Mat_to_vector_Mat(diamondCorners_mat, diamondCorners);

        }

        /// <summary>
        ///  Detect ChArUco Diamond markers
        /// </summary>
        /// <param name="image">
        /// input image necessary for corner subpixel.
        /// </param>
        /// <param name="markerCorners">
        /// list of detected marker corners from detectMarkers function.
        /// </param>
        /// <param name="markerIds">
        /// list of marker ids in markerCorners.
        /// </param>
        /// <param name="squareMarkerLengthRate">
        /// rate between square and marker length:
        ///     squareMarkerLengthRate = squareLength/markerLength. The real units are not necessary.
        /// </param>
        /// <param name="diamondCorners">
        /// output list of detected diamond corners (4 corners per diamond). The order
        ///     is the same than in marker corners: top left, top right, bottom right and bottom left. Similar
        ///     format than the corners returned by detectMarkers (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ).
        /// </param>
        /// <param name="diamondIds">
        /// ids of the diamonds in diamondCorners. The id of each diamond is in fact of
        ///     type Vec4i, so each diamond has 4 ids, which are the ids of the aruco markers composing the
        ///     diamond.
        /// </param>
        /// <param name="cameraMatrix">
        /// Optional camera calibration matrix.
        /// </param>
        /// <param name="distCoeffs">
        /// Optional camera distortion coefficients.
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers.
        /// </param>
        /// <remarks>
        ///     This function detects Diamond markers from the previous detected ArUco markers. The diamonds
        ///     are returned in the diamondCorners and diamondIds parameters. If camera calibration parameters
        ///     are provided, the diamond search is based on reprojection. If not, diamond search is based on
        ///     homography. Homography is faster than reprojection, but less accurate.
        ///    
        ///     @deprecated Use CharucoDetector::detectDiamonds
        /// </remarks>
        [Obsolete("This method is deprecated.")]
        public static void detectCharucoDiamond(Mat image, List<Mat> markerCorners, Mat markerIds, float squareMarkerLengthRate, List<Mat> diamondCorners, Mat diamondIds)
        {
            if (image != null) image.ThrowIfDisposed();
            if (markerIds != null) markerIds.ThrowIfDisposed();
            if (diamondIds != null) diamondIds.ThrowIfDisposed();
            using Mat markerCorners_mat = Converters.vector_Mat_to_Mat(markerCorners);
            using Mat diamondCorners_mat = new Mat();
            aruco_Aruco_detectCharucoDiamond_13(image.nativeObj, markerCorners_mat.nativeObj, markerIds.nativeObj, squareMarkerLengthRate, diamondCorners_mat.nativeObj, diamondIds.nativeObj);
            Converters.Mat_to_vector_Mat(diamondCorners_mat, diamondCorners);

        }


        //
        // C++:  void cv::aruco::drawCharucoDiamond(Ptr_Dictionary dictionary, Vec4i ids, int squareLength, int markerLength, Mat& img, int marginSize = 0, int borderBits = 1)
        //

        // Unknown type 'Vec4i' (I), skipping the function


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::aruco::detectMarkers(Mat image, Ptr_Dictionary dictionary, vector_Mat& corners, Mat& ids, Ptr_DetectorParameters parameters = makePtr<DetectorParameters>(), vector_Mat& rejectedImgPoints = vector_Mat())
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectMarkers_10(IntPtr image_nativeObj, IntPtr dictionary_nativeObj, IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr parameters_nativeObj, IntPtr rejectedImgPoints_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectMarkers_11(IntPtr image_nativeObj, IntPtr dictionary_nativeObj, IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectMarkers_12(IntPtr image_nativeObj, IntPtr dictionary_nativeObj, IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj);

        // C++:  void cv::aruco::refineDetectedMarkers(Mat image, Ptr_Board board, vector_Mat& detectedCorners, Mat& detectedIds, vector_Mat& rejectedCorners, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), float minRepDistance = 10.f, float errorCorrectionRate = 3.f, bool checkAllOrders = true, Mat& recoveredIdxs = Mat(), Ptr_DetectorParameters parameters = makePtr<DetectorParameters>())
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_10(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, float minRepDistance, float errorCorrectionRate, [MarshalAs(UnmanagedType.U1)] bool checkAllOrders, IntPtr recoveredIdxs_nativeObj, IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_11(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, float minRepDistance, float errorCorrectionRate, [MarshalAs(UnmanagedType.U1)] bool checkAllOrders, IntPtr recoveredIdxs_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_12(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, float minRepDistance, float errorCorrectionRate, [MarshalAs(UnmanagedType.U1)] bool checkAllOrders);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_13(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, float minRepDistance, float errorCorrectionRate);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_14(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, float minRepDistance);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_15(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_16(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj, IntPtr cameraMatrix_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_refineDetectedMarkers_17(IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr rejectedCorners_mat_nativeObj);

        // C++:  void cv::aruco::drawPlanarBoard(Ptr_Board board, Size outSize, Mat& img, int marginSize, int borderBits)
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_drawPlanarBoard_10(IntPtr board_nativeObj, double outSize_width, double outSize_height, IntPtr img_nativeObj, int marginSize, int borderBits);

        // C++:  void cv::aruco::getBoardObjectAndImagePoints(Ptr_Board board, vector_Mat detectedCorners, Mat detectedIds, Mat& objPoints, Mat& imgPoints)
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_getBoardObjectAndImagePoints_10(IntPtr board_nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr objPoints_nativeObj, IntPtr imgPoints_nativeObj);

        // C++:  int cv::aruco::estimatePoseBoard(vector_Mat corners, Mat ids, Ptr_Board board, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false)
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_estimatePoseBoard_10(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr board_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvec_nativeObj, IntPtr tvec_nativeObj, [MarshalAs(UnmanagedType.U1)] bool useExtrinsicGuess);
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_estimatePoseBoard_11(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr board_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvec_nativeObj, IntPtr tvec_nativeObj);

        // C++:  bool cv::aruco::estimatePoseCharucoBoard(Mat charucoCorners, Mat charucoIds, Ptr_CharucoBoard board, Mat cameraMatrix, Mat distCoeffs, Mat& rvec, Mat& tvec, bool useExtrinsicGuess = false)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool aruco_Aruco_estimatePoseCharucoBoard_10(IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj, IntPtr board_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvec_nativeObj, IntPtr tvec_nativeObj, [MarshalAs(UnmanagedType.U1)] bool useExtrinsicGuess);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool aruco_Aruco_estimatePoseCharucoBoard_11(IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj, IntPtr board_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvec_nativeObj, IntPtr tvec_nativeObj);

        // C++:  void cv::aruco::estimatePoseSingleMarkers(vector_Mat corners, float markerLength, Mat cameraMatrix, Mat distCoeffs, Mat& rvecs, Mat& tvecs, Mat& objPoints = Mat(), Ptr_EstimateParameters estimateParameters = makePtr<EstimateParameters>())
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_estimatePoseSingleMarkers_10(IntPtr corners_mat_nativeObj, float markerLength, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_nativeObj, IntPtr tvecs_nativeObj, IntPtr objPoints_nativeObj, IntPtr estimateParameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_estimatePoseSingleMarkers_11(IntPtr corners_mat_nativeObj, float markerLength, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_nativeObj, IntPtr tvecs_nativeObj, IntPtr objPoints_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_estimatePoseSingleMarkers_12(IntPtr corners_mat_nativeObj, float markerLength, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_nativeObj, IntPtr tvecs_nativeObj);

        // C++:  bool cv::aruco::testCharucoCornersCollinear(Ptr_CharucoBoard board, Mat charucoIds)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool aruco_Aruco_testCharucoCornersCollinear_10(IntPtr board_nativeObj, IntPtr charucoIds_nativeObj);

        // C++:  double cv::aruco::calibrateCameraAruco(vector_Mat corners, Mat ids, Mat counter, Ptr_Board board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraArucoExtended_10(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj, int flags, int criteria_type, int criteria_maxCount, double criteria_epsilon);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraArucoExtended_11(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraArucoExtended_12(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj);

        // C++:  double cv::aruco::calibrateCameraAruco(vector_Mat corners, Mat ids, Mat counter, Ptr_Board board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs = vector_Mat(), vector_Mat& tvecs = vector_Mat(), int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraAruco_10(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, int flags, int criteria_type, int criteria_maxCount, double criteria_epsilon);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraAruco_11(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraAruco_12(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraAruco_13(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraAruco_14(IntPtr corners_mat_nativeObj, IntPtr ids_nativeObj, IntPtr counter_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj);

        // C++:  double cv::aruco::calibrateCameraCharuco(vector_Mat charucoCorners, vector_Mat charucoIds, Ptr_CharucoBoard board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs, vector_Mat& tvecs, Mat& stdDeviationsIntrinsics, Mat& stdDeviationsExtrinsics, Mat& perViewErrors, int flags = 0, TermCriteria criteria = TermCriteria( TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharucoExtended_10(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj, int flags, int criteria_type, int criteria_maxCount, double criteria_epsilon);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharucoExtended_11(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharucoExtended_12(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, IntPtr stdDeviationsIntrinsics_nativeObj, IntPtr stdDeviationsExtrinsics_nativeObj, IntPtr perViewErrors_nativeObj);

        // C++:  double cv::aruco::calibrateCameraCharuco(vector_Mat charucoCorners, vector_Mat charucoIds, Ptr_CharucoBoard board, Size imageSize, Mat& cameraMatrix, Mat& distCoeffs, vector_Mat& rvecs = vector_Mat(), vector_Mat& tvecs = vector_Mat(), int flags = 0, TermCriteria criteria = TermCriteria(TermCriteria::COUNT + TermCriteria::EPS, 30, DBL_EPSILON))
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharuco_10(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, int flags, int criteria_type, int criteria_maxCount, double criteria_epsilon);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharuco_11(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharuco_12(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj, IntPtr tvecs_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharuco_13(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr rvecs_mat_nativeObj);
        [DllImport(LIBNAME)]
        private static extern double aruco_Aruco_calibrateCameraCharuco_14(IntPtr charucoCorners_mat_nativeObj, IntPtr charucoIds_mat_nativeObj, IntPtr board_nativeObj, double imageSize_width, double imageSize_height, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj);

        // C++:  int cv::aruco::interpolateCornersCharuco(vector_Mat markerCorners, Mat markerIds, Mat image, Ptr_CharucoBoard board, Mat& charucoCorners, Mat& charucoIds, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), int minMarkers = 2)
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_interpolateCornersCharuco_10(IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, int minMarkers);
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_interpolateCornersCharuco_11(IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj);
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_interpolateCornersCharuco_12(IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj, IntPtr cameraMatrix_nativeObj);
        [DllImport(LIBNAME)]
        private static extern int aruco_Aruco_interpolateCornersCharuco_13(IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, IntPtr image_nativeObj, IntPtr board_nativeObj, IntPtr charucoCorners_nativeObj, IntPtr charucoIds_nativeObj);

        // C++:  void cv::aruco::detectCharucoDiamond(Mat image, vector_Mat markerCorners, Mat markerIds, float squareMarkerLengthRate, vector_Mat& diamondCorners, Mat& diamondIds, Mat cameraMatrix = Mat(), Mat distCoeffs = Mat(), Ptr_Dictionary dictionary = makePtr<Dictionary> (getPredefinedDictionary(PredefinedDictionaryType::DICT_4X4_50)))
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectCharucoDiamond_10(IntPtr image_nativeObj, IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, float squareMarkerLengthRate, IntPtr diamondCorners_mat_nativeObj, IntPtr diamondIds_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj, IntPtr dictionary_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectCharucoDiamond_11(IntPtr image_nativeObj, IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, float squareMarkerLengthRate, IntPtr diamondCorners_mat_nativeObj, IntPtr diamondIds_nativeObj, IntPtr cameraMatrix_nativeObj, IntPtr distCoeffs_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectCharucoDiamond_12(IntPtr image_nativeObj, IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, float squareMarkerLengthRate, IntPtr diamondCorners_mat_nativeObj, IntPtr diamondIds_nativeObj, IntPtr cameraMatrix_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void aruco_Aruco_detectCharucoDiamond_13(IntPtr image_nativeObj, IntPtr markerCorners_mat_nativeObj, IntPtr markerIds_nativeObj, float squareMarkerLengthRate, IntPtr diamondCorners_mat_nativeObj, IntPtr diamondIds_nativeObj);

    }
}
