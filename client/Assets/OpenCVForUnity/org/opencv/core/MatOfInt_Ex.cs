using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfInt : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfInt(ReadOnlySpan<int> a)
            : base()
        {

            fromSpan(a);
        }

        public void fromSpan(ReadOnlySpan<int> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<int>(a, this, num);

        }

        public Span<int> AsIntSpan()
        {
            return AsSpan<int>();
        }

#endif

    }
}
