
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class CLAHE : Algorithm
    {


        //
        // C++:  void cv::CLAHE::setTilesGridSize(Size tileGridSize)
        //

        /// <summary>
        ///  Sets size of grid for histogram equalization. Input image will be divided into
        ///      equally sized rectangular tiles.
        /// </summary>
        /// <param name="tileGridSize">
        /// defines the number of tiles in row and column.
        /// </param>
        public void setTilesGridSize(in (double width, double height) tileGridSize)
        {
            ThrowIfDisposed();

            imgproc_CLAHE_setTilesGridSize_10(nativeObj, tileGridSize.width, tileGridSize.height);


        }


        //
        // C++:  Size cv::CLAHE::getTilesGridSize()
        //

        public (double width, double height) getTilesGridSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            imgproc_CLAHE_getTilesGridSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }

    }
}
