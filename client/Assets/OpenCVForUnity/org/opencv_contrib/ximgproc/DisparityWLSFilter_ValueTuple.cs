
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
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
        public (int x, int y, int width, int height) getROIAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            ximgproc_DisparityWLSFilter_getROI_10(nativeObj, tmpArray);
            (int x, int y, int width, int height) retVal = ((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }

    }
}
