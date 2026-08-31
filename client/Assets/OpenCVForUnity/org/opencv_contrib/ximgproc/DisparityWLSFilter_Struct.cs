
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{
    public partial class DisparityWLSFilter : DisparityFilter
    {


        //
        // C++:  Rect cv::ximgproc::DisparityWLSFilter::getROI()
        //

        /// <summary>
        ///  Get the ROI used in the last filter call
        /// </summary>
        public Vec4i getROIAsVec4i()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            ximgproc_DisparityWLSFilter_getROI_10(nativeObj, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }

    }
}
