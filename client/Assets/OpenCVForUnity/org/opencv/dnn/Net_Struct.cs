#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    public partial class Net : DisposableOpenCVObject
    {


        //
        // C++:  void cv::dnn::Net::setInput(Mat blob, String name = "", double scalefactor = 1.0, Scalar mean = Scalar())
        //

        /// <summary>
        ///  Sets the new input value for the network
        /// </summary>
        /// <param name="blob">
        /// A new blob. Should have CV_32F or CV_8U depth.
        /// </param>
        /// <param name="name">
        /// A name of input layer.
        /// </param>
        /// <param name="scalefactor">
        /// An optional normalization scale.
        /// </param>
        /// <param name="mean">
        /// An optional mean subtraction values.
        ///              @see connect(String, String) to know format of the descriptor.
        /// </param>
        /// <remarks>
        ///              If scale or mean values are specified, a final input blob is computed
        ///              as:
        ///             \f[input(n,c,h,w) = scalefactor \times (blob(n,c,h,w) - mean_c)\f]
        /// </remarks>
        public void setInput(Mat blob, string name, double scalefactor, in Vec4d mean)
        {
            ThrowIfDisposed();
            if (blob != null) blob.ThrowIfDisposed();

            dnn_Net_setInput_10(nativeObj, blob.nativeObj, name, scalefactor, mean.Item1, mean.Item2, mean.Item3, mean.Item4);


        }

    }
}


#endif