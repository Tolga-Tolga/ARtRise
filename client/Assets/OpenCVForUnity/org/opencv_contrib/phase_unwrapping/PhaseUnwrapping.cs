#if !UNITY_WEBGL


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Phase_unwrappingModule
{

    // C++: class PhaseUnwrapping
    /// <summary>
    ///  Abstract base class for phase unwrapping.
    /// </summary>
    public class PhaseUnwrapping : Algorithm
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
                        phase_1unwrapping_PhaseUnwrapping_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal PhaseUnwrapping(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new PhaseUnwrapping __fromPtr__(IntPtr addr) { return new PhaseUnwrapping(addr); }

        //
        // C++:  void cv::phase_unwrapping::PhaseUnwrapping::unwrapPhaseMap(Mat wrappedPhaseMap, Mat& unwrappedPhaseMap, Mat shadowMask = Mat())
        //

        /// <summary>
        ///  Unwraps a 2D phase map.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map of type CV_32FC1 that needs to be unwrapped.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map.
        /// </param>
        /// <param name="shadowMask">
        /// Optional CV_8UC1 mask image used when some pixels do not hold any phase information in the wrapped phase map.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap, Mat shadowMask)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();

            phase_1unwrapping_PhaseUnwrapping_unwrapPhaseMap_10(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj, shadowMask.nativeObj);


        }

        /// <summary>
        ///  Unwraps a 2D phase map.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map of type CV_32FC1 that needs to be unwrapped.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map.
        /// </param>
        /// <param name="shadowMask">
        /// Optional CV_8UC1 mask image used when some pixels do not hold any phase information in the wrapped phase map.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();

            phase_1unwrapping_PhaseUnwrapping_unwrapPhaseMap_11(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::phase_unwrapping::PhaseUnwrapping::unwrapPhaseMap(Mat wrappedPhaseMap, Mat& unwrappedPhaseMap, Mat shadowMask = Mat())
        [DllImport(LIBNAME)]
        private static extern void phase_1unwrapping_PhaseUnwrapping_unwrapPhaseMap_10(IntPtr nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr unwrappedPhaseMap_nativeObj, IntPtr shadowMask_nativeObj);
        [DllImport(LIBNAME)]
        private static extern void phase_1unwrapping_PhaseUnwrapping_unwrapPhaseMap_11(IntPtr nativeObj, IntPtr wrappedPhaseMap_nativeObj, IntPtr unwrappedPhaseMap_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void phase_1unwrapping_PhaseUnwrapping_delete(IntPtr nativeObj);

    }
}


#endif