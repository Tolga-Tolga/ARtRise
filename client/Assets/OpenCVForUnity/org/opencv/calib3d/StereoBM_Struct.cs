
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Calib3dModule
{
    public partial class StereoBM : StereoMatcher
    {


        //
        // C++:  Rect cv::StereoBM::getROI1()
        //

        /// <summary>
        ///  Gets the current Region of Interest (ROI) for the left image.
        /// </summary>
        /// <returns>
        ///  The current ROI for the left image.
        /// </returns>
        public Vec4i getROI1AsVec4i()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_StereoBM_getROI1_10(nativeObj, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }


        //
        // C++:  void cv::StereoBM::setROI1(Rect roi1)
        //

        /// <summary>
        ///  Sets the Region of Interest (ROI) for the left image.
        /// </summary>
        /// <param name="roi1">
        /// The ROI rectangle for the left image.
        ///         @details By setting the ROI, the stereo matching computation is limited to the specified region, improving performance and potentially accuracy by focusing on relevant parts of the image.
        /// </param>
        public void setROI1(in Vec4i roi1)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setROI1_10(nativeObj, roi1.Item1, roi1.Item2, roi1.Item3, roi1.Item4);


        }


        //
        // C++:  Rect cv::StereoBM::getROI2()
        //

        /// <summary>
        ///  Gets the current Region of Interest (ROI) for the right image.
        /// </summary>
        /// <returns>
        ///  The current ROI for the right image.
        /// </returns>
        public Vec4i getROI2AsVec4i()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            calib3d_StereoBM_getROI2_10(nativeObj, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }


        //
        // C++:  void cv::StereoBM::setROI2(Rect roi2)
        //

        /// <summary>
        ///  Sets the Region of Interest (ROI) for the right image.
        /// </summary>
        /// <param name="roi2">
        /// The ROI rectangle for the right image.
        ///         @details Similar to setROI1, this limits the computation to the specified region in the right image.
        /// </param>
        public void setROI2(in Vec4i roi2)
        {
            ThrowIfDisposed();

            calib3d_StereoBM_setROI2_10(nativeObj, roi2.Item1, roi2.Item2, roi2.Item3, roi2.Item4);


        }

    }
}
