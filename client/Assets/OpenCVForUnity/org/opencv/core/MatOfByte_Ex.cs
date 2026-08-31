using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfByte : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfByte(ReadOnlySpan<byte> a)
            : base()
        {

            fromSpan(a);
        }

        public void fromSpan(ReadOnlySpan<byte> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<byte>(a, this, num);
        }

        public Span<byte> AsByteSpan()
        {
            return AsSpan<byte>();
        }

#endif

    }
}
