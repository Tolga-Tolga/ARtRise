using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfFloat4 : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfFloat4(ReadOnlySpan<float> a)
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

        public MatOfFloat4(params Vec4f[] a)
            : base()
        {
            fromVec4fArray(a);
        }

        public void fromVec4fArray(params Vec4f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec4f>(a, this, num);
        }

        public Vec4f[] toVec4fArray()
        {
            int num = (int)total();
            Vec4f[] ap = new Vec4f[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec4f>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfFloat4(ReadOnlySpan<Vec4f> a)
            : base()
        {
            fromVec4fSpan(a);
        }

        public void fromVec4fSpan(ReadOnlySpan<Vec4f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec4f>(a, this, num);
        }

#endif

        public MatOfFloat4(params (float v0, float v1, float v2, float v3)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float v0, float v1, float v2, float v3)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4f> span = this.AsSpan<Vec4f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.v0;
                    vec_span.Item2 = tuple_a.v1;
                    vec_span.Item3 = tuple_a.v2;
                    vec_span.Item4 = tuple_a.v3;
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
                        ptr[i * 4] = tuple_a.v0;
                        ptr[i * 4 + 1] = tuple_a.v1;
                        ptr[i * 4 + 2] = tuple_a.v2;
                        ptr[i * 4 + 3] = tuple_a.v3;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 4);

            Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.v0;
                vec_span.Item2 = tuple_a.v1;
                vec_span.Item3 = tuple_a.v2;
                vec_span.Item4 = tuple_a.v3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 4);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref var tuple_a = ref a[i];
                buff[i * 4] = tuple_a.v0;
                buff[i * 4 + 1] = tuple_a.v1;
                buff[i * 4 + 2] = tuple_a.v2;
                buff[i * 4 + 3] = tuple_a.v3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 4);
#endif
        }

        public (float v0, float v1, float v2, float v3)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float v0, float v1, float v2, float v3)[] ap = new (float v0, float v1, float v2, float v3)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4f> span = this.AsSpan<Vec4f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec4f vec_span = ref span[i];
                    tuple_ap.v0 = vec_span.Item1;
                    tuple_ap.v1 = vec_span.Item2;
                    tuple_ap.v2 = vec_span.Item3;
                    tuple_ap.v3 = vec_span.Item4;
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
                        tuple_ap.v0 = ptr[i * 4];
                        tuple_ap.v1 = ptr[i * 4 + 1];
                        tuple_ap.v2 = ptr[i * 4 + 2];
                        tuple_ap.v3 = ptr[i * 4 + 3];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 4);

            Converters.copyMatToArray<float>(this, buff, num * 4);

            Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec4f vec_span = ref buffSpan[i];
                tuple_ap.v0 = vec_span.Item1;
                tuple_ap.v1 = vec_span.Item2;
                tuple_ap.v2 = vec_span.Item3;
                tuple_ap.v3 = vec_span.Item4;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 4];

            Converters.copyMatToArray<float>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.v0 = buff[i * 4];
                tuple_ap.v1 = buff[i * 4 + 1];
                tuple_ap.v2 = buff[i * 4 + 2];
                tuple_ap.v3 = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

        public MatOfFloat4(params UnityEngine.Vector4[] a)
            : base()
        {
            fromVector4Array(a);
        }

        public void fromVector4Array(params UnityEngine.Vector4[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<UnityEngine.Vector4>(a, this, num);
        }

        public UnityEngine.Vector4[] toVector4Array()
        {
            int num = (int)total();
            UnityEngine.Vector4[] ap = new UnityEngine.Vector4[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<UnityEngine.Vector4>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec4f> AsVec4fSpan()
        {
            return AsSpan<Vec4f>();
        }

        public Span<UnityEngine.Vector4> AsVector4Span()
        {
            return AsSpan<UnityEngine.Vector4>();
        }

#endif

    }
}
