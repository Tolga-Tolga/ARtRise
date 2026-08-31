
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class LineSegmentDetector : Algorithm
    {


        //
        // C++:  int cv::LineSegmentDetector::compareSegments(Size size, Mat lines1, Mat lines2, Mat& image = Mat())
        //

        /// <summary>
        ///  Draws two groups of lines in blue and red, counting the non overlapping (mismatching) pixels.
        /// </summary>
        /// <param name="size">
        /// The size of the image, where lines1 and lines2 were found.
        /// </param>
        /// <param name="lines1">
        /// The first group of lines that needs to be drawn. It is visualized in blue color.
        /// </param>
        /// <param name="lines2">
        /// The second group of lines. They visualized in red color.
        /// </param>
        /// <param name="image">
        /// Optional image, where the lines will be drawn. The image should be color(3-channel)
        ///      in order for lines1 and lines2 to be drawn in the above mentioned colors.
        /// </param>
        public int compareSegments(in Vec2d size, Mat lines1, Mat lines2, Mat image)
        {
            ThrowIfDisposed();
            if (lines1 != null) lines1.ThrowIfDisposed();
            if (lines2 != null) lines2.ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            return imgproc_LineSegmentDetector_compareSegments_10(nativeObj, size.Item1, size.Item2, lines1.nativeObj, lines2.nativeObj, image.nativeObj);


        }

        /// <summary>
        ///  Draws two groups of lines in blue and red, counting the non overlapping (mismatching) pixels.
        /// </summary>
        /// <param name="size">
        /// The size of the image, where lines1 and lines2 were found.
        /// </param>
        /// <param name="lines1">
        /// The first group of lines that needs to be drawn. It is visualized in blue color.
        /// </param>
        /// <param name="lines2">
        /// The second group of lines. They visualized in red color.
        /// </param>
        /// <param name="image">
        /// Optional image, where the lines will be drawn. The image should be color(3-channel)
        ///      in order for lines1 and lines2 to be drawn in the above mentioned colors.
        /// </param>
        public int compareSegments(in Vec2d size, Mat lines1, Mat lines2)
        {
            ThrowIfDisposed();
            if (lines1 != null) lines1.ThrowIfDisposed();
            if (lines2 != null) lines2.ThrowIfDisposed();

            return imgproc_LineSegmentDetector_compareSegments_11(nativeObj, size.Item1, size.Item2, lines1.nativeObj, lines2.nativeObj);


        }

    }
}
