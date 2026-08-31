using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{

    public partial class MatOfDouble : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfDouble(ReadOnlySpan<double> a)
            : base()
        {

            fromSpan(a);
        }

        public void fromSpan(ReadOnlySpan<double> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<double>(a, this, num);

        }

        public Span<double> AsDoubleSpan()
        {
            return AsSpan<double>();
        }

#endif

    }
}
