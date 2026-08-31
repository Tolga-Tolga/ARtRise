using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfInt4 : Mat
    {

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfInt4(ReadOnlySpan<int> a)
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

            Converters.copyArrayToMat<int>(a, this, a.Length);

        }

#endif

        public MatOfInt4(params Vec4i[] a)
            : base()
        {
            fromVec4iArray(a);
        }

        public void fromVec4iArray(params Vec4i[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec4i>(a, this, num);
        }

        public Vec4i[] toVec4iArray()
        {
            int num = (int)total();
            Vec4i[] ap = new Vec4i[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec4i>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfInt4(ReadOnlySpan<Vec4i> a)
            : base()
        {
            fromVec4iSpan(a);
        }

        public void fromVec4iSpan(ReadOnlySpan<Vec4i> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec4i>(a, this, num);
        }

#endif

        public MatOfInt4(params (int v0, int v1, int v2, int v3)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (int v0, int v1, int v2, int v3)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4i vec_span = ref span[i];
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
                    int* ptr = (int*)this.dataAddr();
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
            int[] buff = ArrayPool<int>.Shared.Rent(num * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4i vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.v0;
                vec_span.Item2 = tuple_a.v1;
                vec_span.Item3 = tuple_a.v2;
                vec_span.Item4 = tuple_a.v3;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 4] = tuple_a.v0;
                buff[i * 4 + 1] = tuple_a.v1;
                buff[i * 4 + 2] = tuple_a.v2;
                buff[i * 4 + 3] = tuple_a.v3;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public (int v0, int v1, int v2, int v3)[] toValueTupleArrayAsInt()
        {
            int num = (int)total();
            (int v0, int v1, int v2, int v3)[] ap = new (int v0, int v1, int v2, int v3)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec4i vec_span = ref span[i];
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
                    int* ptr = (int*)this.dataAddr();

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
            int[] buff = ArrayPool<int>.Shared.Rent(num * 4);

            Converters.copyMatToArray<int>(this, buff, num * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec4i vec_span = ref buffSpan[i];
                tuple_ap.v0 = vec_span.Item1;
                tuple_ap.v1 = vec_span.Item2;
                tuple_ap.v2 = vec_span.Item3;
                tuple_ap.v3 = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
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

        public MatOfInt4(params UnityEngine.Vector4[] a)
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

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4i vec_span = ref span[i];
                    ref readonly var vector_a = ref a[i];
                    vec_span.Item1 = (int)vector_a.x;
                    vec_span.Item2 = (int)vector_a.y;
                    vec_span.Item3 = (int)vector_a.z;
                    vec_span.Item4 = (int)vector_a.w;
                }

                return;
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)this.dataAddr();
                    for (int i = 0; i < a.Length; i++)
                    {
                        ref readonly var vector_a = ref a[i];
                        ptr[i * 4] = (int)vector_a.x;
                        ptr[i * 4 + 1] = (int)vector_a.y;
                        ptr[i * 4 + 2] = (int)vector_a.z;
                        ptr[i * 4 + 3] = (int)vector_a.w;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4i vec_span = ref buffSpan[i];
                ref readonly var vector_a = ref a[i];
                vec_span.Item1 = (int)vector_a.x;
                vec_span.Item2 = (int)vector_a.y;
                vec_span.Item3 = (int)vector_a.z;
                vec_span.Item4 = (int)vector_a.w;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var vector_a = ref a[i];
                buff[i * 4] = (int)vector_a.x;
                buff[i * 4 + 1] = (int)vector_a.y;
                buff[i * 4 + 2] = (int)vector_a.z;
                buff[i * 4 + 3] = (int)vector_a.w;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public UnityEngine.Vector4[] toVector4Array()
        {
            int num = (int)total();
            UnityEngine.Vector4[] ap = new UnityEngine.Vector4[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var vector_a = ref ap[i];
                    ref readonly Vec4i vec_span = ref span[i];
                    vector_a.x = vec_span.Item1;
                    vector_a.y = vec_span.Item2;
                    vector_a.z = vec_span.Item3;
                    vector_a.w = vec_span.Item4;
                }

                return ap;
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)this.dataAddr();

                    for (int i = 0; i < ap.Length; i++)
                    {
                        ref var vector_a = ref ap[i];
                        vector_a.x = ptr[i * 4];
                        vector_a.y = ptr[i * 4 + 1];
                        vector_a.z = ptr[i * 4 + 2];
                        vector_a.w = ptr[i * 4 + 3];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 4);

            Converters.copyMatToArray<int>(this, buff, num * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var vector_a = ref ap[i];
                ref readonly Vec4i vec_span = ref buffSpan[i];
                vector_a.x = vec_span.Item1;
                vector_a.y = vec_span.Item2;
                vector_a.z = vec_span.Item3;
                vector_a.w = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var vector_a = ref ap[i];
                vector_a.x = buff[i * 4];
                vector_a.y = buff[i * 4 + 1];
                vector_a.z = buff[i * 4 + 2];
                vector_a.w = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec4i> AsVec4iSpan()
        {
            return AsSpan<Vec4i>();
        }

#endif

    }
}
