
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

    // C++: class VariationalRefinement
    /// <summary>
    ///  Variational optical flow refinement
    /// </summary>
    /// <remarks>
    ///  This class implements variational refinement of the input flow field, i.e.
    ///  it uses input flow to initialize the minimization of the following functional:
    ///  \f$$E(U) = \int_{\Omega} \delta \Psi(E_I) + \gamma \Psi(E_G) + \alpha \Psi(E_S) \f$$,
    ///  where \f$$E_I,E_G,E_S\f$$ are color constancy, gradient constancy and smoothness terms
    ///  respectively. \f$$\Psi(s^2)=\sqrt{s^2+\epsilon^2}\f$$ is a robust penalizer to limit the
    ///  influence of outliers. A complete formulation and a description of the minimization
    ///  procedure can be found in @cite Brox2004
    /// </remarks>
    public class VariationalRefinement : DenseOpticalFlow
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
                        video_VariationalRefinement_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal VariationalRefinement(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new VariationalRefinement __fromPtr__(IntPtr addr) { return new VariationalRefinement(addr); }

        //
        // C++:  void cv::VariationalRefinement::calcUV(Mat I0, Mat I1, Mat& flow_u, Mat& flow_v)
        //

        /// <summary>
        ///  @ref calc function overload to handle separate horizontal (u) and vertical (v) flow components
        ///  (to avoid extra splits/merges)
        /// </summary>
        public void calcUV(Mat I0, Mat I1, Mat flow_u, Mat flow_v)
        {
            ThrowIfDisposed();
            if (I0 != null) I0.ThrowIfDisposed();
            if (I1 != null) I1.ThrowIfDisposed();
            if (flow_u != null) flow_u.ThrowIfDisposed();
            if (flow_v != null) flow_v.ThrowIfDisposed();

            video_VariationalRefinement_calcUV_10(nativeObj, I0.nativeObj, I1.nativeObj, flow_u.nativeObj, flow_v.nativeObj);


        }


        //
        // C++:  int cv::VariationalRefinement::getFixedPointIterations()
        //

        /// <summary>
        ///  Number of outer (fixed-point) iterations in the minimization procedure.
        ///  @see setFixedPointIterations
        /// </summary>
        public int getFixedPointIterations()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getFixedPointIterations_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setFixedPointIterations(int val)
        //

        /// <remarks>
        ///  @copybrief getFixedPointIterations @see getFixedPointIterations
        /// </remarks>
        public void setFixedPointIterations(int val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setFixedPointIterations_10(nativeObj, val);


        }


        //
        // C++:  int cv::VariationalRefinement::getSorIterations()
        //

        /// <summary>
        ///  Number of inner successive over-relaxation (SOR) iterations
        ///          in the minimization procedure to solve the respective linear system.
        ///  @see setSorIterations
        /// </summary>
        public int getSorIterations()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getSorIterations_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setSorIterations(int val)
        //

        /// <remarks>
        ///  @copybrief getSorIterations @see getSorIterations
        /// </remarks>
        public void setSorIterations(int val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setSorIterations_10(nativeObj, val);


        }


        //
        // C++:  float cv::VariationalRefinement::getOmega()
        //

        /// <summary>
        ///  Relaxation factor in SOR
        ///  @see setOmega
        /// </summary>
        public float getOmega()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getOmega_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setOmega(float val)
        //

        /// <remarks>
        ///  @copybrief getOmega @see getOmega
        /// </remarks>
        public void setOmega(float val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setOmega_10(nativeObj, val);


        }


        //
        // C++:  float cv::VariationalRefinement::getAlpha()
        //

        /// <summary>
        ///  Weight of the smoothness term
        ///  @see setAlpha
        /// </summary>
        public float getAlpha()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getAlpha_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setAlpha(float val)
        //

        /// <remarks>
        ///  @copybrief getAlpha @see getAlpha
        /// </remarks>
        public void setAlpha(float val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setAlpha_10(nativeObj, val);


        }


        //
        // C++:  float cv::VariationalRefinement::getDelta()
        //

        /// <summary>
        ///  Weight of the color constancy term
        ///  @see setDelta
        /// </summary>
        public float getDelta()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getDelta_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setDelta(float val)
        //

        /// <remarks>
        ///  @copybrief getDelta @see getDelta
        /// </remarks>
        public void setDelta(float val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setDelta_10(nativeObj, val);


        }


        //
        // C++:  float cv::VariationalRefinement::getGamma()
        //

        /// <summary>
        ///  Weight of the gradient constancy term
        ///  @see setGamma
        /// </summary>
        public float getGamma()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getGamma_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setGamma(float val)
        //

        /// <remarks>
        ///  @copybrief getGamma @see getGamma
        /// </remarks>
        public void setGamma(float val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setGamma_10(nativeObj, val);


        }


        //
        // C++:  float cv::VariationalRefinement::getEpsilon()
        //

        /// <summary>
        ///  Norm value shift for robust penalizer
        ///  @see setEpsilon
        /// </summary>
        public float getEpsilon()
        {
            ThrowIfDisposed();

            return video_VariationalRefinement_getEpsilon_10(nativeObj);


        }


        //
        // C++:  void cv::VariationalRefinement::setEpsilon(float val)
        //

        /// <remarks>
        ///  @copybrief getEpsilon @see getEpsilon
        /// </remarks>
        public void setEpsilon(float val)
        {
            ThrowIfDisposed();

            video_VariationalRefinement_setEpsilon_10(nativeObj, val);


        }


        //
        // C++: static Ptr_VariationalRefinement cv::VariationalRefinement::create()
        //

        /// <summary>
        ///  Creates an instance of VariationalRefinement
        /// </summary>
        public static VariationalRefinement create()
        {


            return VariationalRefinement.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_VariationalRefinement_create_10()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::VariationalRefinement::calcUV(Mat I0, Mat I1, Mat& flow_u, Mat& flow_v)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_calcUV_10(IntPtr nativeObj, IntPtr I0_nativeObj, IntPtr I1_nativeObj, IntPtr flow_u_nativeObj, IntPtr flow_v_nativeObj);

        // C++:  int cv::VariationalRefinement::getFixedPointIterations()
        [DllImport(LIBNAME)]
        private static extern int video_VariationalRefinement_getFixedPointIterations_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setFixedPointIterations(int val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setFixedPointIterations_10(IntPtr nativeObj, int val);

        // C++:  int cv::VariationalRefinement::getSorIterations()
        [DllImport(LIBNAME)]
        private static extern int video_VariationalRefinement_getSorIterations_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setSorIterations(int val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setSorIterations_10(IntPtr nativeObj, int val);

        // C++:  float cv::VariationalRefinement::getOmega()
        [DllImport(LIBNAME)]
        private static extern float video_VariationalRefinement_getOmega_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setOmega(float val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setOmega_10(IntPtr nativeObj, float val);

        // C++:  float cv::VariationalRefinement::getAlpha()
        [DllImport(LIBNAME)]
        private static extern float video_VariationalRefinement_getAlpha_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setAlpha(float val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setAlpha_10(IntPtr nativeObj, float val);

        // C++:  float cv::VariationalRefinement::getDelta()
        [DllImport(LIBNAME)]
        private static extern float video_VariationalRefinement_getDelta_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setDelta(float val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setDelta_10(IntPtr nativeObj, float val);

        // C++:  float cv::VariationalRefinement::getGamma()
        [DllImport(LIBNAME)]
        private static extern float video_VariationalRefinement_getGamma_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setGamma(float val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setGamma_10(IntPtr nativeObj, float val);

        // C++:  float cv::VariationalRefinement::getEpsilon()
        [DllImport(LIBNAME)]
        private static extern float video_VariationalRefinement_getEpsilon_10(IntPtr nativeObj);

        // C++:  void cv::VariationalRefinement::setEpsilon(float val)
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_setEpsilon_10(IntPtr nativeObj, float val);

        // C++: static Ptr_VariationalRefinement cv::VariationalRefinement::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr video_VariationalRefinement_create_10();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void video_VariationalRefinement_delete(IntPtr nativeObj);

    }
}
