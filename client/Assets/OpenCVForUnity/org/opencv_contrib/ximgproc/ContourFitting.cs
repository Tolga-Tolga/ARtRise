
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class ContourFitting
    /// <summary>
    ///  Class for ContourFitting algorithms.
    ///      ContourFitting match two contours \f$$ z_a \f$$ and \f$$ z_b \f$$ minimizing distance
    ///      \f[ d(z_a,z_b)=\sum (a_n - s  b_n e^{j(n \alpha +\phi )})^2 \f] where \f$$ a_n \f$$ and \f$$ b_n \f$$ are Fourier descriptors of \f$$ z_a \f$$ and \f$$ z_b \f$$ and s is a scaling factor and  \f$$ \phi \f$$ is angle rotation and \f$$ \alpha \f$$ is starting point factor adjustement
    /// </summary>
    public class ContourFitting : Algorithm
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
                        ximgproc_ContourFitting_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal ContourFitting(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new ContourFitting __fromPtr__(IntPtr addr) { return new ContourFitting(addr); }

        //
        // C++:  void cv::ximgproc::ContourFitting::estimateTransformation(Mat src, Mat dst, Mat& alphaPhiST, double& dist, bool fdContour = false)
        //

        /// <summary>
        ///  Fit two closed curves using fourier descriptors. More details in @cite PersoonFu1977 and @cite BergerRaghunathan1998
        /// </summary>
        /// <param name="src">
        /// Contour defining first shape.
        /// </param>
        /// <param name="dst">
        /// Contour defining second shape (Target).
        /// </param>
        /// <param name="alphaPhiST">
        /// : \f$ \alpha \f$=alphaPhiST(0,0), \f$ \phi \f$=alphaPhiST(0,1) (in radian), s=alphaPhiST(0,2), Tx=alphaPhiST(0,3), Ty=alphaPhiST(0,4) rotation center
        /// </param>
        /// <param name="dist">
        /// distance between src and dst after matching.
        /// </param>
        /// <param name="fdContour">
        /// false then src and dst are contours and true src and dst are fourier descriptors.
        /// </param>
        public void estimateTransformation(Mat src, Mat dst, Mat alphaPhiST, double[] dist, bool fdContour)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (alphaPhiST != null) alphaPhiST.ThrowIfDisposed();
            double[] dist_out = new double[1];
            ximgproc_ContourFitting_estimateTransformation_10(nativeObj, src.nativeObj, dst.nativeObj, alphaPhiST.nativeObj, dist_out, fdContour);
            if (dist != null) dist[0] = (double)dist_out[0];

        }

        /// <summary>
        ///  Fit two closed curves using fourier descriptors. More details in @cite PersoonFu1977 and @cite BergerRaghunathan1998
        /// </summary>
        /// <param name="src">
        /// Contour defining first shape.
        /// </param>
        /// <param name="dst">
        /// Contour defining second shape (Target).
        /// </param>
        /// <param name="alphaPhiST">
        /// : \f$ \alpha \f$=alphaPhiST(0,0), \f$ \phi \f$=alphaPhiST(0,1) (in radian), s=alphaPhiST(0,2), Tx=alphaPhiST(0,3), Ty=alphaPhiST(0,4) rotation center
        /// </param>
        /// <param name="dist">
        /// distance between src and dst after matching.
        /// </param>
        /// <param name="fdContour">
        /// false then src and dst are contours and true src and dst are fourier descriptors.
        /// </param>
        public void estimateTransformation(Mat src, Mat dst, Mat alphaPhiST, double[] dist)
        {
            ThrowIfDisposed();
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (alphaPhiST != null) alphaPhiST.ThrowIfDisposed();
            double[] dist_out = new double[1];
            ximgproc_ContourFitting_estimateTransformation_11(nativeObj, src.nativeObj, dst.nativeObj, alphaPhiST.nativeObj, dist_out);
            if (dist != null) dist[0] = (double)dist_out[0];

        }


        //
        // C++:  void cv::ximgproc::ContourFitting::setCtrSize(int n)
        //

        /// <summary>
        ///  set number of Fourier descriptors used in estimateTransformation
        /// </summary>
        /// <param name="n">
        /// number of Fourier descriptors equal to number of contour points after resampling.
        /// </param>
        public void setCtrSize(int n)
        {
            ThrowIfDisposed();

            ximgproc_ContourFitting_setCtrSize_10(nativeObj, n);


        }


        //
        // C++:  void cv::ximgproc::ContourFitting::setFDSize(int n)
        //

        /// <summary>
        ///  set number of Fourier descriptors when estimateTransformation used vector<Point>
        /// </summary>
        /// <param name="n">
        /// number of fourier descriptors used for optimal curve matching.
        /// </param>
        public void setFDSize(int n)
        {
            ThrowIfDisposed();

            ximgproc_ContourFitting_setFDSize_10(nativeObj, n);


        }


        //
        // C++:  int cv::ximgproc::ContourFitting::getCtrSize()
        //

        /// <returns>
        ///  number of fourier descriptors
        /// </returns>
        public int getCtrSize()
        {
            ThrowIfDisposed();

            return ximgproc_ContourFitting_getCtrSize_10(nativeObj);


        }


        //
        // C++:  int cv::ximgproc::ContourFitting::getFDSize()
        //

        /// <returns>
        ///  number of fourier descriptors used for optimal curve matching
        /// </returns>
        public int getFDSize()
        {
            ThrowIfDisposed();

            return ximgproc_ContourFitting_getFDSize_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::ContourFitting::estimateTransformation(Mat src, Mat dst, Mat& alphaPhiST, double& dist, bool fdContour = false)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_ContourFitting_estimateTransformation_10(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj, IntPtr alphaPhiST_nativeObj, double[] dist_out, [MarshalAs(UnmanagedType.U1)] bool fdContour);
        [DllImport(LIBNAME)]
        private static extern void ximgproc_ContourFitting_estimateTransformation_11(IntPtr nativeObj, IntPtr src_nativeObj, IntPtr dst_nativeObj, IntPtr alphaPhiST_nativeObj, double[] dist_out);

        // C++:  void cv::ximgproc::ContourFitting::setCtrSize(int n)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_ContourFitting_setCtrSize_10(IntPtr nativeObj, int n);

        // C++:  void cv::ximgproc::ContourFitting::setFDSize(int n)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_ContourFitting_setFDSize_10(IntPtr nativeObj, int n);

        // C++:  int cv::ximgproc::ContourFitting::getCtrSize()
        [DllImport(LIBNAME)]
        private static extern int ximgproc_ContourFitting_getCtrSize_10(IntPtr nativeObj);

        // C++:  int cv::ximgproc::ContourFitting::getFDSize()
        [DllImport(LIBNAME)]
        private static extern int ximgproc_ContourFitting_getFDSize_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_ContourFitting_delete(IntPtr nativeObj);

    }
}
