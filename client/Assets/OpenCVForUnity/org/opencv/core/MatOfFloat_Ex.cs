using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfFloat : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfFloat(ReadOnlySpan<float> a)
            : base()
        {

            fromSpan(a);
        }

        public void fromSpan(ReadOnlySpan<float> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<float>(a, this, num);

        }

        public Span<float> AsFloatSpan()
        {
            return AsSpan<float>();
        }

#endif

    }
}
