
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{
    public partial class FastLineDetector : Algorithm
    {


        //
        // C++:  void cv::ximgproc::FastLineDetector::drawSegments(Mat& image, Mat lines, bool draw_arrow = false, Scalar linecolor = Scalar(0, 0, 255), int linethickness = 1)
        //

        /// <summary>
        ///  Draws the line segments on a given image.
        /// </summary>
        /// <param name="image">
        /// The image, where the lines will be drawn. Should be bigger
        ///        or equal to the image, where the lines were found.
        /// </param>
        /// <param name="lines">
        /// A vector of the lines that needed to be drawn.
        /// </param>
        /// <param name="draw_arrow">
        /// If true, arrow heads will be drawn.
        /// </param>
        /// <param name="linecolor">
        /// Line color.
        /// </param>
        /// <param name="linethickness">
        /// Line thickness.
        /// </param>
        public void drawSegments(Mat image, Mat lines, bool draw_arrow, in (double v0, double v1, double v2, double v3) linecolor, int linethickness)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_10(nativeObj, image.nativeObj, lines.nativeObj, draw_arrow, linecolor.v0, linecolor.v1, linecolor.v2, linecolor.v3, linethickness);


        }

        /// <summary>
        ///  Draws the line segments on a given image.
        /// </summary>
        /// <param name="image">
        /// The image, where the lines will be drawn. Should be bigger
        ///        or equal to the image, where the lines were found.
        /// </param>
        /// <param name="lines">
        /// A vector of the lines that needed to be drawn.
        /// </param>
        /// <param name="draw_arrow">
        /// If true, arrow heads will be drawn.
        /// </param>
        /// <param name="linecolor">
        /// Line color.
        /// </param>
        /// <param name="linethickness">
        /// Line thickness.
        /// </param>
        public void drawSegments(Mat image, Mat lines, bool draw_arrow, in (double v0, double v1, double v2, double v3) linecolor)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_11(nativeObj, image.nativeObj, lines.nativeObj, draw_arrow, linecolor.v0, linecolor.v1, linecolor.v2, linecolor.v3);


        }

    }
}
