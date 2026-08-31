
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class StructuredEdgeDetection
    /// <summary>
    ///  Class implementing edge detection algorithm from @cite Dollar2013 :
    /// </summary>
    public class StructuredEdgeDetection : Algorithm
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
                        ximgproc_StructuredEdgeDetection_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal StructuredEdgeDetection(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new StructuredEdgeDetection __fromPtr__(IntPtr addr) { return new StructuredEdgeDetection(addr); }

        //
        // C++:  void cv::ximgproc::StructuredEdgeDetection::detectEdges(Mat src, Mat& dst)
        //

        /// <summary>
        ///  The function detects edges in src and draw them to dst.
        /// </summary>
        /// <remarks>
        ///      The algorithm underlies this function is much more robust to texture presence, than common
        ///      approaches, e.g. Sobel
        /// </remarks>
        /// <param name="src">
        /// source image (RGB, float, in [0;1]) to detect edges
        /// </param>
        /// <param name="dst">
        /// destination image (grayscale, float, in [0;1]) where edges are drawn
        ///      @sa Sobel, Canny
        /// </param>
        public void detectEdges(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_detectEdges_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


        //
        // C++:  void cv::ximgproc::StructuredEdgeDetection::computeOrientation(Mat src, Mat& dst)
        //

        /// <summary>
        ///  The function computes orientation from edge image.
        /// </summary>
        /// <param name="src">
        /// edge image.
        /// </param>
        /// <param name="dst">
        /// orientation image.
        /// </param>
        public void computeOrientation(Mat src, Mat dst)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_computeOrientation_10(nativeObj, src.nativeObj, dst.nativeObj);


        }


        //
        // C++:  void cv::ximgproc::StructuredEdgeDetection::edgesNms(Mat edge_image, Mat orientation_image, Mat& dst, int r = 2, int s = 0, float m = 1, bool isParallel = true)
        //

        /// <summary>
        ///  The function edgenms in edge image and suppress edges where edge is stronger in orthogonal direction.
        /// </summary>
        /// <param name="edge_image">
        /// edge image from detectEdges function.
        /// </param>
        /// <param name="orientation_image">
        /// orientation image from computeOrientation function.
        /// </param>
        /// <param name="dst">
        /// suppressed image (grayscale, float, in [0;1])
        /// </param>
        /// <param name="r">
        /// radius for NMS suppression.
        /// </param>
        /// <param name="s">
        /// radius for boundary suppression.
        /// </param>
        /// <param name="m">
        /// multiplier for conservative suppression.
        /// </param>
        /// <param name="isParallel">
        /// enables/disables parallel computing.
        /// </param>
        public void edgesNms(Mat edge_image, Mat orientation_image, Mat dst, int r, int s, float m, bool isParallel)
        {
            ThrowIfDisposed();
            if (edge_image != null) edge_image.ThrowIfDisposed();
            if (orientation_image != null) orientation_image.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_edgesNms_10(nativeObj, edge_image.nativeObj, orientation_image.nativeObj, dst.nativeObj, r, s, m, isParallel);


        }

        /// <summary>
        ///  The function edgenms in edge image and suppress edges where edge is stronger in orthogonal direction.
        /// </summary>
        /// <param name="edge_image">
        /// edge image from detectEdges function.
        /// </param>
        /// <param name="orientation_image">
        /// orientation image from computeOrientation function.
        /// </param>
        /// <param name="dst">
        /// suppressed image (grayscale, float, in [0;1])
        /// </param>
        /// <param name="r">
        /// radius for NMS suppression.
        /// </param>
        /// <param name="s">
        /// radius for boundary suppression.
        /// </param>
        /// <param name="m">
        /// multiplier for conservative suppression.
        /// </param>
        /// <param name="isParallel">
        /// enables/disables parallel computing.
        /// </param>
        public void edgesNms(Mat edge_image, Mat orientation_image, Mat dst, int r, int s, float m)
        {
            ThrowIfDisposed();
            if (edge_image != null) edge_image.ThrowIfDisposed();
            if (orientation_image != null) orientation_image.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_edgesNms_11(nativeObj, edge_image.nativeObj, orientation_image.nativeObj, dst.nativeObj, r, s, m);


        }

        /// <summary>
        ///  The function edgenms in edge image and suppress edges where edge is stronger in orthogonal direction.
        /// </summary>
        /// <param name="edge_image">
        /// edge image from detectEdges function.
        /// </param>
        /// <param name="orientation_image">
        /// orientation image from computeOrientation function.
        /// </param>
        /// <param name="dst">
        /// suppressed image (grayscale, float, in [0;1])
        /// </param>
        /// <param name="r">
        /// radius for NMS suppression.
        /// </param>
        /// <param name="s">
        /// radius for boundary suppression.
        /// </param>
        /// <param name="m">
        /// multiplier for conservative suppression.
        /// </param>
        /// <param name="isParallel">
        /// enables/disables parallel computing.
        /// </param>
        public void edgesNms(Mat edge_image, Mat orientation_image, Mat dst, int r, int s)
        {
            ThrowIfDisposed();
            if (edge_image != null) edge_image.ThrowIfDisposed();
            if (orientation_image != null) orientation_image.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_edgesNms_12(nativeObj, edge_image.nativeObj, orientation_image.nativeObj, dst.nativeObj, r, s);


        }

        /// <summary>
        ///  The function edgenms in edge image and suppress edges where edge is stronger in orthogonal direction.
        /// </summary>
        /// <param name="edge_image">
        /// edge image from detectEdges function.
        /// </param>
        /// <param name="orientation_image">
        /// orientation image from computeOrientation function.
        /// </param>
        /// <param name="dst">
        /// suppressed image (grayscale, float, in [0;1])
        /// </param>
        /// <param name="r">
        /// radius for NMS suppression.
        /// </param>
        /// <param name="s">
        /// radius for boundary suppression.
        /// </param>
        /// <param name="m">
        /// multiplier for conservative suppression.
        /// </param>
        /// <param name="isParallel">
        /// enables/disables parallel computing.
        /// </param>
        public void edgesNms(Mat edge_image, Mat orientation_image, Mat dst, int r)
        {
            ThrowIfDisposed();
            if (edge_image != null) edge_image.ThrowIfDisposed();
            if (orientation_image != null) orientation_image.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_edgesNms_13(nativeObj, edge_image.nativeObj, orientation_image.nativeObj, dst.nativeObj, r);


        }

        /// <summary>
        ///  The function edgenms in edge image and suppress edges where edge is stronger in orthogonal direction.
        /// </summary>
        /// <param name="edge_image">
        /// edge image from detectEdges function.
        /// </param>
        /// <param name="orientation_image">
        /// orientation image from computeOrientation function.
        /// </param>
        /// <param name="dst">
        /// suppressed image (grayscale, float, in [0;1])
        /// </param>
        /// <param name="r">
        /// radius for NMS suppression.
        /// </param>
        /// <param name="s">
        /// radius for boundary suppression.
        /// </param>
        /// <param name="m">
        /// multiplier for conservative suppression.
        /// </param>
        /// <param name="isParallel">
        /// enables/disables parallel computing.
        /// </param>
        public void edgesNms(Mat edge_image, Mat orientation_image, Mat dst)
        {
            ThrowIfDisposed();
            if (edge_image != null) edge_image.ThrowIfDisposed();
            if (orientation_image != null) orientation_image.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();

            ximgproc_StructuredEdgeDetection_edgesNms_14(nativeObj, edge_image.nativeObj, orientation_image.nativeObj, dst.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::StructuredEdgeDetection::detectEdges(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_detectEdges_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // C++:  void cv::ximgproc::StructuredEdgeDetection::computeOrientation(Mat src, Mat& dst)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_computeOrientation_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj);

        // C++:  void cv::ximgproc::StructuredEdgeDetection::edgesNms(Mat edge_image, Mat orientation_image, Mat& dst, int r = 2, int s = 0, float m = 1, bool isParallel = true)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_edgesNms_10(IntPtr nativeObj, IntPtr edge_image_nativeObj, IntPtr orientation_image_nativeObj, IntPtr dst_nativeObj, int r, int s, float m, [MarshalAs(UnmanagedType.U1)] bool isParallel);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_edgesNms_11(IntPtr nativeObj, IntPtr edge_image_nativeObj, IntPtr orientation_image_nativeObj, IntPtr dst_nativeObj, int r, int s, float m);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_edgesNms_12(IntPtr nativeObj, IntPtr edge_image_nativeObj, IntPtr orientation_image_nativeObj, IntPtr dst_nativeObj, int r, int s);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_edgesNms_13(IntPtr nativeObj, IntPtr edge_image_nativeObj, IntPtr orientation_image_nativeObj, IntPtr dst_nativeObj, int r);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_edgesNms_14(IntPtr nativeObj, IntPtr edge_image_nativeObj, IntPtr orientation_image_nativeObj, IntPtr dst_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_StructuredEdgeDetection_delete(IntPtr nativeObj);

    }
}
