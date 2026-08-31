
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.BioinspiredModule
{
    public partial class RetinaFastToneMapping : Algorithm
    {


        //
        // C++: static Ptr_RetinaFastToneMapping cv::bioinspired::RetinaFastToneMapping::create(Size inputSize)
        //

        public static RetinaFastToneMapping create(in (double width, double height) inputSize)
        {


            return RetinaFastToneMapping.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_RetinaFastToneMapping_create_10(inputSize.width, inputSize.height)));


        }

    }
}
