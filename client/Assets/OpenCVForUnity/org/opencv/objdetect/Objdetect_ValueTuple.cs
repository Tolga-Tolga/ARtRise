
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class Objdetect
    {


        //
        // C++:  void cv::aruco::drawDetectedMarkers(Mat& image, vector_Mat corners, Mat ids = Mat(), Scalar borderColor = Scalar(0, 255, 0))
        //

        /// <summary>
        ///  Draw detected markers in image
        /// </summary>
        /// <param name="image">
        /// input/output image. It must have 1 or 3 channels. The number of channels is not altered.
        /// </param>
        /// <param name="corners">
        /// positions of marker corners on input image.
        ///     (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers, the dimensions of
        ///     this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="ids">
        /// vector of identifiers for markers in markersCorners .
        ///     Optional, if not provided, ids are not painted.
        /// </param>
        /// <param name="borderColor">
        /// color of marker borders. Rest of colors (text color and first corner color)
        ///     are calculated based on this one to improve visualization.
        /// </param>
        /// <remarks>
        ///     Given an array of detected marker corners and its corresponding ids, this functions draws
        ///     the markers in the image. The marker borders are painted and the markers identifiers if provided.
        ///     Useful for debugging purposes.
        /// </remarks>
        public static void drawDetectedMarkers(Mat image, List<Mat> corners, Mat ids, in (double v0, double v1, double v2, double v3) borderColor)
        {
            if (image != null) image.ThrowIfDisposed();
            if (ids != null) ids.ThrowIfDisposed();
            using Mat corners_mat = Converters.vector_Mat_to_Mat(corners);
            objdetect_Objdetect_drawDetectedMarkers_10(image.nativeObj, corners_mat.nativeObj, ids.nativeObj, borderColor.v0, borderColor.v1, borderColor.v2, borderColor.v3);


        }


        //
        // C++:  void cv::aruco::drawDetectedCornersCharuco(Mat& image, Mat charucoCorners, Mat charucoIds = Mat(), Scalar cornerColor = Scalar(255, 0, 0))
        //

        /// <summary>
        ///  Draws a set of Charuco corners
        /// </summary>
        /// <param name="image">
        /// input/output image. It must have 1 or 3 channels. The number of channels is not
        ///     altered.
        /// </param>
        /// <param name="charucoCorners">
        /// vector of detected charuco corners
        /// </param>
        /// <param name="charucoIds">
        /// list of identifiers for each corner in charucoCorners
        /// </param>
        /// <param name="cornerColor">
        /// color of the square surrounding each corner
        /// </param>
        /// <remarks>
        ///     This function draws a set of detected Charuco corners. If identifiers vector is provided, it also
        ///     draws the id of each corner.
        /// </remarks>
        public static void drawDetectedCornersCharuco(Mat image, Mat charucoCorners, Mat charucoIds, in (double v0, double v1, double v2, double v3) cornerColor)
        {
            if (image != null) image.ThrowIfDisposed();
            if (charucoCorners != null) charucoCorners.ThrowIfDisposed();
            if (charucoIds != null) charucoIds.ThrowIfDisposed();

            objdetect_Objdetect_drawDetectedCornersCharuco_10(image.nativeObj, charucoCorners.nativeObj, charucoIds.nativeObj, cornerColor.v0, cornerColor.v1, cornerColor.v2, cornerColor.v3);


        }


        //
        // C++:  void cv::aruco::drawDetectedDiamonds(Mat& image, vector_Mat diamondCorners, Mat diamondIds = Mat(), Scalar borderColor = Scalar(0, 0, 255))
        //

        /// <summary>
        ///  Draw a set of detected ChArUco Diamond markers
        /// </summary>
        /// <param name="image">
        /// input/output image. It must have 1 or 3 channels. The number of channels is not
        ///     altered.
        /// </param>
        /// <param name="diamondCorners">
        /// positions of diamond corners in the same format returned by
        ///     detectCharucoDiamond(). (e.g std::vector&lt;std::vector&lt;cv::Point2f&gt; &gt; ). For N detected markers,
        ///     the dimensions of this array should be Nx4. The order of the corners should be clockwise.
        /// </param>
        /// <param name="diamondIds">
        /// vector of identifiers for diamonds in diamondCorners, in the same format
        ///     returned by detectCharucoDiamond() (e.g. std::vector<Vec4i>).
        ///     Optional, if not provided, ids are not painted.
        /// </param>
        /// <param name="borderColor">
        /// color of marker borders. Rest of colors (text color and first corner color)
        ///     are calculated based on this one.
        /// </param>
        /// <remarks>
        ///     Given an array of detected diamonds, this functions draws them in the image. The marker borders
        ///     are painted and the markers identifiers if provided.
        ///     Useful for debugging purposes.
        /// </remarks>
        public static void drawDetectedDiamonds(Mat image, List<Mat> diamondCorners, Mat diamondIds, in (double v0, double v1, double v2, double v3) borderColor)
        {
            if (image != null) image.ThrowIfDisposed();
            if (diamondIds != null) diamondIds.ThrowIfDisposed();
            using Mat diamondCorners_mat = Converters.vector_Mat_to_Mat(diamondCorners);
            objdetect_Objdetect_drawDetectedDiamonds_10(image.nativeObj, diamondCorners_mat.nativeObj, diamondIds.nativeObj, borderColor.v0, borderColor.v1, borderColor.v2, borderColor.v3);


        }

    }
}
