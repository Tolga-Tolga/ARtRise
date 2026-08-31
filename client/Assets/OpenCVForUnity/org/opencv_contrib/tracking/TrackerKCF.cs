
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using OpenCVForUnity.VideoModule;

namespace OpenCVForUnity.TrackingModule
{

    // C++: class TrackerKCF
    /// <summary>
    ///  the KCF (Kernelized Correlation Filter) tracker
    /// </summary>
    /// <remarks>
    ///     KCF is a novel tracking framework that utilizes properties of circulant matrix to enhance the processing speed.
    ///     This tracking method is an implementation of @cite KCF_ECCV which is extended to KCF with color-names features (@cite KCF_CN).
    ///     The original paper of KCF is available at &lt;http://www.robots.ox.ac.uk/~joao/publications/henriques_tpami2015.pdf&gt;
    ///     as well as the matlab implementation. For more information about KCF with color-names features, please refer to
    ///     &lt;http://www.cvl.isy.liu.se/research/objrec/visualtracking/colvistrack/index.html&gt;.
    /// </remarks>
    public class TrackerKCF : Tracker
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
                        tracking_TrackerKCF_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TrackerKCF(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TrackerKCF __fromPtr__(IntPtr addr) { return new TrackerKCF(addr); }

        /// <summary>
        /// C++: enum MODE (cv.TrackerKCF.MODE)
        /// </summary>
        public const int GRAY = (1 << 0);

        /// <summary>
        /// C++: enum MODE (cv.TrackerKCF.MODE)
        /// </summary>
        public const int CN = (1 << 1);

        /// <summary>
        /// C++: enum MODE (cv.TrackerKCF.MODE)
        /// </summary>
        public const int CUSTOM = (1 << 2);


        //
        // C++: static Ptr_TrackerKCF cv::TrackerKCF::create(TrackerKCF_Params parameters = TrackerKCF::Params())
        //

        /// <summary>
        ///  Create KCF tracker instance
        /// </summary>
        /// <param name="parameters">
        /// KCF parameters TrackerKCF::Params
        /// </param>
        public static TrackerKCF create(TrackerKCF_Params parameters)
        {
            if (parameters != null) parameters.ThrowIfDisposed();

            return TrackerKCF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(tracking_TrackerKCF_create_10(parameters.getNativeObjAddr())));


        }

        /// <summary>
        ///  Create KCF tracker instance
        /// </summary>
        /// <param name="parameters">
        /// KCF parameters TrackerKCF::Params
        /// </param>
        public static TrackerKCF create()
        {


            return TrackerKCF.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(tracking_TrackerKCF_create_11()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++: static Ptr_TrackerKCF cv::TrackerKCF::create(TrackerKCF_Params parameters = TrackerKCF::Params())
        [DllImport(LIBNAME)]
        private static extern IntPtr tracking_TrackerKCF_create_10(IntPtr parameters_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr tracking_TrackerKCF_create_11();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void tracking_TrackerKCF_delete(IntPtr nativeObj);

    }
}
