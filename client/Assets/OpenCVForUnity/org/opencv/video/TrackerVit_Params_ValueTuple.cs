

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
    public partial class TrackerVit_Params : DisposableOpenCVObject
    {


        //
        // C++: Scalar TrackerVit_Params::meanvalue
        //

        public (double v0, double v1, double v2, double v3) get_meanvalueAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            video_TrackerVit_1Params_get_1meanvalue_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void TrackerVit_Params::meanvalue
        //

        public void set_meanvalue(in (double v0, double v1, double v2, double v3) meanvalue)
        {
            ThrowIfDisposed();

            video_TrackerVit_1Params_set_1meanvalue_10(nativeObj, meanvalue.v0, meanvalue.v1, meanvalue.v2, meanvalue.v3);


        }


        //
        // C++: Scalar TrackerVit_Params::stdvalue
        //

        public (double v0, double v1, double v2, double v3) get_stdvalueAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            video_TrackerVit_1Params_get_1stdvalue_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void TrackerVit_Params::stdvalue
        //

        public void set_stdvalue(in (double v0, double v1, double v2, double v3) stdvalue)
        {
            ThrowIfDisposed();

            video_TrackerVit_1Params_set_1stdvalue_10(nativeObj, stdvalue.v0, stdvalue.v1, stdvalue.v2, stdvalue.v3);


        }

    }
}
