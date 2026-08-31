
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.BioinspiredModule
{
    public partial class TransientAreasSegmentationModule : Algorithm
    {

        //
        // C++:  Size cv::bioinspired::TransientAreasSegmentationModule::getSize()
        //

        /// <summary>
        ///  return the sze of the manage input and output images
        /// </summary>
        public (double width, double height) getSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            bioinspired_TransientAreasSegmentationModule_getSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: static Ptr_TransientAreasSegmentationModule cv::bioinspired::TransientAreasSegmentationModule::create(Size inputSize)
        //

        /// <summary>
        ///  allocator
        /// </summary>
        /// <param name="inputSize">
        /// : size of the images input to segment (output will be the same size)
        /// </param>
        public static TransientAreasSegmentationModule create(in (double width, double height) inputSize)
        {


            return TransientAreasSegmentationModule.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_TransientAreasSegmentationModule_create_10(inputSize.width, inputSize.height)));


        }

    }
}
