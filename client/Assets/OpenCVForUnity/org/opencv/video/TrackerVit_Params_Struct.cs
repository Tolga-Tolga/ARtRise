

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{
    public partial class TrackerVit_Params : DisposableOpenCVObject
    {


        //
        // C++: Scalar TrackerVit_Params::meanvalue
        //

        public Vec4d get_meanvalueAsVec4d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            video_TrackerVit_1Params_get_1meanvalue_10(nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void TrackerVit_Params::meanvalue
        //

        public void set_meanvalue(in Vec4d meanvalue)
        {
            ThrowIfDisposed();

            video_TrackerVit_1Params_set_1meanvalue_10(nativeObj, meanvalue.Item1, meanvalue.Item2, meanvalue.Item3, meanvalue.Item4);


        }


        //
        // C++: Scalar TrackerVit_Params::stdvalue
        //

        public Vec4d get_stdvalueAsVec4d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            video_TrackerVit_1Params_get_1stdvalue_10(nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void TrackerVit_Params::stdvalue
        //

        public void set_stdvalue(in Vec4d stdvalue)
        {
            ThrowIfDisposed();

            video_TrackerVit_1Params_set_1stdvalue_10(nativeObj, stdvalue.Item1, stdvalue.Item2, stdvalue.Item3, stdvalue.Item4);


        }

    }
}
