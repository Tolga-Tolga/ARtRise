
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class FastLineDetector
    /// <summary>
    ///  Class implementing the FLD (Fast Line Detector) algorithm described
    ///  in @cite Lee14 .
    /// </summary>
    public partial class FastLineDetector : Algorithm
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        ximgproc_FastLineDetector_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal FastLineDetector(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new FastLineDetector __fromPtr__(IntPtr addr) { return new FastLineDetector(addr); }

        //
        // C++:  void cv::ximgproc::FastLineDetector::detect(Mat image, Mat& lines)
        //

        /// <summary>
        ///  Finds lines in the input image.
        ///        This is the output of the default parameters of the algorithm on the above
        ///        shown image.
        /// </summary>
        /// <remarks>
        ///        ![image](pics/corridor_fld.jpg)
        /// </remarks>
        /// <param name="image">
        /// A grayscale (CV_8UC1) input image. If only a roi needs to be
        ///        selected, use: `fld_ptr-\>detect(image(roi), lines, ...);
        ///        lines += Scalar(roi.x, roi.y, roi.x, roi.y);`
        /// </param>
        /// <param name="lines">
        /// A vector of Vec4f elements specifying the beginning
        ///        and ending point of a line.  Where Vec4f is (x1, y1, x2, y2), point
        ///        1 is the start, point 2 - end. Returned lines are directed so that the
        ///        brighter side is on their left.
        /// </param>
        public void detect(Mat image, Mat lines)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_detect_10(nativeObj, image.nativeObj, lines.nativeObj);


        }


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
        public void drawSegments(Mat image, Mat lines, bool draw_arrow, Scalar linecolor, int linethickness)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_10(nativeObj, image.nativeObj, lines.nativeObj, draw_arrow, linecolor.val[0], linecolor.val[1], linecolor.val[2], linecolor.val[3], linethickness);


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
        public void drawSegments(Mat image, Mat lines, bool draw_arrow, Scalar linecolor)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_11(nativeObj, image.nativeObj, lines.nativeObj, draw_arrow, linecolor.val[0], linecolor.val[1], linecolor.val[2], linecolor.val[3]);


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
        public void drawSegments(Mat image, Mat lines, bool draw_arrow)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_12(nativeObj, image.nativeObj, lines.nativeObj, draw_arrow);


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
        public void drawSegments(Mat image, Mat lines)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (lines != null) lines.ThrowIfDisposed();

            ximgproc_FastLineDetector_drawSegments_13(nativeObj, image.nativeObj, lines.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::FastLineDetector::detect(Mat image, Mat& lines)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_detect_10(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr lines_nativeObj);

        // C++:  void cv::ximgproc::FastLineDetector::drawSegments(Mat& image, Mat lines, bool draw_arrow = false, Scalar linecolor = Scalar(0, 0, 255), int linethickness = 1)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_drawSegments_10(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr lines_nativeObj, [MarshalAs(UnmanagedType.U1)] bool draw_arrow, double linecolor_val0, double linecolor_val1, double linecolor_val2, double linecolor_val3, int linethickness);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_drawSegments_11(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr lines_nativeObj, [MarshalAs(UnmanagedType.U1)] bool draw_arrow, double linecolor_val0, double linecolor_val1, double linecolor_val2, double linecolor_val3);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_drawSegments_12(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr lines_nativeObj, [MarshalAs(UnmanagedType.U1)] bool draw_arrow);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_drawSegments_13(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr lines_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_FastLineDetector_delete(IntPtr nativeObj);

    }
}
