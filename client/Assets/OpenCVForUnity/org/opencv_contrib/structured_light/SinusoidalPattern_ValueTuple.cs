#if !UNITY_WEBGL


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Structured_lightModule
{
    public partial class SinusoidalPattern : StructuredLightPattern
    {


        //
        // C++:  void cv::structured_light::SinusoidalPattern::unwrapPhaseMap(Mat wrappedPhaseMap, Mat& unwrappedPhaseMap, Size camSize, Mat shadowMask = Mat())
        //

        /// <summary>
        ///  Unwrap the wrapped phase map to remove phase ambiguities.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map computed from the pattern.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map used to find correspondences between the two devices.
        /// </param>
        /// <param name="camSize">
        /// Resolution of the camera.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap, in (double width, double height) camSize, Mat shadowMask)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();
            if (shadowMask != null) shadowMask.ThrowIfDisposed();

            structured_1light_SinusoidalPattern_unwrapPhaseMap_10(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj, camSize.width, camSize.height, shadowMask.nativeObj);


        }

        /// <summary>
        ///  Unwrap the wrapped phase map to remove phase ambiguities.
        /// </summary>
        /// <param name="wrappedPhaseMap">
        /// The wrapped phase map computed from the pattern.
        /// </param>
        /// <param name="unwrappedPhaseMap">
        /// The unwrapped phase map used to find correspondences between the two devices.
        /// </param>
        /// <param name="camSize">
        /// Resolution of the camera.
        /// </param>
        /// <param name="shadowMask">
        /// Mask used to discard shadow regions.
        /// </param>
        public void unwrapPhaseMap(Mat wrappedPhaseMap, Mat unwrappedPhaseMap, in (double width, double height) camSize)
        {
            ThrowIfDisposed();
            if (wrappedPhaseMap != null) wrappedPhaseMap.ThrowIfDisposed();
            if (unwrappedPhaseMap != null) unwrappedPhaseMap.ThrowIfDisposed();

            structured_1light_SinusoidalPattern_unwrapPhaseMap_11(nativeObj, wrappedPhaseMap.nativeObj, unwrappedPhaseMap.nativeObj, camSize.width, camSize.height);


        }

    }
}


#endif