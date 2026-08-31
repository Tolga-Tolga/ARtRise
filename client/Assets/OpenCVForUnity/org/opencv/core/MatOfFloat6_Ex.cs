using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfFloat6 : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfFloat6(ReadOnlySpan<float> a)
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

            Converters.copyArrayToMat<float>(a, this, a.Length);

        }

#endif

        public MatOfFloat6(params Vec6f[] a)
            : base()
        {
            fromVec6fArray(a);
        }

        public void fromVec6fArray(params Vec6f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec6f>(a, this, num);
        }

        public Vec6f[] toVec6fArray()
        {
            int num = (int)total();
            Vec6f[] ap = new Vec6f[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec6f>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfFloat6(ReadOnlySpan<Vec6f> a)
            : base()
        {
            fromVec6fSpan(a);
        }

        public void fromVec6fSpan(ReadOnlySpan<Vec6f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec6f>(a, this, num);
        }

#endif

        public MatOfFloat6(params (float v0, float v1, float v2, float v3, float v4, float v5)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float v0, float v1, float v2, float v3, float v4, float v5)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec6f> span = this.AsSpan<Vec6f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec6f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.v0;
                    vec_span.Item2 = tuple_a.v1;
                    vec_span.Item3 = tuple_a.v2;
                    vec_span.Item4 = tuple_a.v3;
                    vec_span.Item5 = tuple_a.v4;
                    vec_span.Item6 = tuple_a.v5;
                }

                return;
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)this.dataAddr();
                    for (int i = 0; i < a.Length; i++)
                    {
                        ref readonly var tuple_a = ref a[i];
                        ptr[i * 6] = tuple_a.v0;
                        ptr[i * 6 + 1] = tuple_a.v1;
                        ptr[i * 6 + 2] = tuple_a.v2;
                        ptr[i * 6 + 3] = tuple_a.v3;
                        ptr[i * 6 + 4] = tuple_a.v4;
                        ptr[i * 6 + 5] = tuple_a.v5;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 6);

            Span<Vec6f> buffSpan = MemoryMarshal.Cast<float, Vec6f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec6f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.v0;
                vec_span.Item2 = tuple_a.v1;
                vec_span.Item3 = tuple_a.v2;
                vec_span.Item4 = tuple_a.v3;
                vec_span.Item5 = tuple_a.v4;
                vec_span.Item6 = tuple_a.v5;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 6);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 6];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 6] = tuple_a.v0;
                buff[i * 6 + 1] = tuple_a.v1;
                buff[i * 6 + 2] = tuple_a.v2;
                buff[i * 6 + 3] = tuple_a.v3;
                buff[i * 6 + 4] = tuple_a.v4;
                buff[i * 6 + 5] = tuple_a.v5;

            }

            Converters.copyArrayToMat<float>(buff, this, num * 6);
#endif
        }

        public (float v0, float v1, float v2, float v3, float v4, float v5)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float v0, float v1, float v2, float v3, float v4, float v5)[] ap = new (float v0, float v1, float v2, float v3, float v4, float v5)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec6f> span = this.AsSpan<Vec6f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec6f vec_span = ref span[i];
                    tuple_ap.v0 = vec_span.Item1;
                    tuple_ap.v1 = vec_span.Item2;
                    tuple_ap.v2 = vec_span.Item3;
                    tuple_ap.v3 = vec_span.Item4;
                    tuple_ap.v4 = vec_span.Item5;
                    tuple_ap.v5 = vec_span.Item6;
                }

                return ap;
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)this.dataAddr();
                    for (int i = 0; i < ap.Length; i++)
                    {
                        ref var tuple_ap = ref ap[i];
                        tuple_ap.v0 = ptr[i * 6];
                        tuple_ap.v1 = ptr[i * 6 + 1];
                        tuple_ap.v2 = ptr[i * 6 + 2];
                        tuple_ap.v3 = ptr[i * 6 + 3];
                        tuple_ap.v4 = ptr[i * 6 + 4];
                        tuple_ap.v5 = ptr[i * 6 + 5];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 6);

            Converters.copyMatToArray<float>(this, buff, num * 6);

            Span<Vec6f> buffSpan = MemoryMarshal.Cast<float, Vec6f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec6f vec_span = ref buffSpan[i];
                tuple_ap.v0 = vec_span.Item1;
                tuple_ap.v1 = vec_span.Item2;
                tuple_ap.v2 = vec_span.Item3;
                tuple_ap.v3 = vec_span.Item4;
                tuple_ap.v4 = vec_span.Item5;
                tuple_ap.v5 = vec_span.Item6;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 6];

            Converters.copyMatToArray<float>(this, buff, num * 6);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.v0 = buff[i * 6];
                tuple_ap.v1 = buff[i * 6 + 1];
                tuple_ap.v2 = buff[i * 6 + 2];
                tuple_ap.v3 = buff[i * 6 + 3];
                tuple_ap.v4 = buff[i * 6 + 4];
                tuple_ap.v5 = buff[i * 6 + 5];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec6f> AsVec6fSpan()
        {
            return AsSpan<Vec6f>();
        }

#endif

    }
}
