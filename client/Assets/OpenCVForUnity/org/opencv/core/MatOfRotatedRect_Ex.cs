using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfRotatedRect : Mat
    {
        public MatOfRotatedRect(params Vec5f[] a)
           : base()
        {
            fromVec5fArray(a);
        }

        public void fromVec5fArray(params Vec5f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec5f>(a, this, num);
        }

        public Vec5f[] toVec5fArray()
        {
            int num = (int)total();
            Vec5f[] ap = new Vec5f[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec5f>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfRotatedRect(ReadOnlySpan<Vec5f> a)
            : base()
        {
            fromVec5fSpan(a);
        }

        public void fromVec5fSpan(ReadOnlySpan<Vec5f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec5f>(a, this, num);
        }

#endif

        public MatOfRotatedRect(params Vec5d[] a)
            : base()
        {
            fromVec5dArray(a);
        }

        public void fromVec5dArray(params Vec5d[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec5f vec_span = ref span[i];
                    ref readonly Vec5d vec_a = ref a[i];
                    vec_span.Item1 = (float)vec_a.Item1;
                    vec_span.Item2 = (float)vec_a.Item2;
                    vec_span.Item3 = (float)vec_a.Item3;
                    vec_span.Item4 = (float)vec_a.Item4;
                    vec_span.Item5 = (float)vec_a.Item5;
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
                        ref readonly Vec5d vec_a = ref a[i];
                        ptr[i * 5] = (float)vec_a.Item1;
                        ptr[i * 5 + 1] = (float)vec_a.Item2;
                        ptr[i * 5 + 2] = (float)vec_a.Item3;
                        ptr[i * 5 + 3] = (float)vec_a.Item4;
                        ptr[i * 5 + 4] = (float)vec_a.Item5;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec_span = ref buffSpan[i];
                ref readonly Vec5d vec_a = ref a[i];
                vec_span.Item1 = (float)vec_a.Item1;
                vec_span.Item2 = (float)vec_a.Item2;
                vec_span.Item3 = (float)vec_a.Item3;
                vec_span.Item4 = (float)vec_a.Item4;
                vec_span.Item5 = (float)vec_a.Item5;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 5] = (float)tuple_a.Item1;
                buff[i * 5 + 1] = (float)tuple_a.Item2;
                buff[i * 5 + 2] = (float)tuple_a.Item3;
                buff[i * 5 + 3] = (float)tuple_a.Item4;
                buff[i * 5 + 4] = (float)tuple_a.Item5;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);
#endif
        }

        public Vec5d[] toVec5dArray()
        {
            int num = (int)total();
            Vec5d[] ap = new Vec5d[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec5f vec_span = ref span[i];
                    tuple_ap.Item1 = vec_span.Item1;
                    tuple_ap.Item2 = vec_span.Item2;
                    tuple_ap.Item3 = vec_span.Item3;
                    tuple_ap.Item4 = vec_span.Item4;
                    tuple_ap.Item5 = vec_span.Item5;
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
                        tuple_ap.Item1 = ptr[i * 5];
                        tuple_ap.Item2 = ptr[i * 5 + 1];
                        tuple_ap.Item3 = ptr[i * 5 + 2];
                        tuple_ap.Item4 = ptr[i * 5 + 3];
                        tuple_ap.Item5 = ptr[i * 5 + 4];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Converters.copyMatToArray<float>(this, buff, num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec5f vec_span = ref buffSpan[i];
                tuple_ap.Item1 = vec_span.Item1;
                tuple_ap.Item2 = vec_span.Item2;
                tuple_ap.Item3 = vec_span.Item3;
                tuple_ap.Item4 = vec_span.Item4;
                tuple_ap.Item5 = vec_span.Item5;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];

            Converters.copyMatToArray<float>(this, buff, num * 5);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.Item1 = buff[i * 5];
                tuple_ap.Item2 = buff[i * 5 + 1];
                tuple_ap.Item3 = buff[i * 5 + 2];
                tuple_ap.Item4 = buff[i * 5 + 3];
                tuple_ap.Item5 = buff[i * 5 + 4];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfRotatedRect(ReadOnlySpan<Vec5d> a)
            : base()
        {
            fromVec5dSpan(a);
        }

        public void fromVec5dSpan(ReadOnlySpan<Vec5d> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec5f vec_span = ref span[i];
                    ref readonly Vec5d vec_a = ref a[i];
                    vec_span.Item1 = (float)vec_a.Item1;
                    vec_span.Item2 = (float)vec_a.Item2;
                    vec_span.Item3 = (float)vec_a.Item3;
                    vec_span.Item4 = (float)vec_a.Item4;
                    vec_span.Item5 = (float)vec_a.Item5;
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
                        ref readonly Vec5d vec_a = ref a[i];
                        ptr[i * 5] = (float)vec_a.Item1;
                        ptr[i * 5 + 1] = (float)vec_a.Item2;
                        ptr[i * 5 + 2] = (float)vec_a.Item3;
                        ptr[i * 5 + 3] = (float)vec_a.Item4;
                        ptr[i * 5 + 4] = (float)vec_a.Item5;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec_span = ref buffSpan[i];
                ref readonly Vec5d vec_a = ref a[i];
                vec_span.Item1 = (float)vec_a.Item1;
                vec_span.Item2 = (float)vec_a.Item2;
                vec_span.Item3 = (float)vec_a.Item3;
                vec_span.Item4 = (float)vec_a.Item4;
                vec_span.Item5 = (float)vec_a.Item5;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 5] = (float)tuple_a.Item1;
                buff[i * 5 + 1] = (float)tuple_a.Item2;
                buff[i * 5 + 2] = (float)tuple_a.Item3;
                buff[i * 5 + 3] = (float)tuple_a.Item4;
                buff[i * 5 + 4] = (float)tuple_a.Item5;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);
#endif
        }

#endif

        public MatOfRotatedRect(params (float x, float y, float width, float height, float angle)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float x, float y, float width, float height, float angle)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec5f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
                    vec_span.Item3 = tuple_a.width;
                    vec_span.Item4 = tuple_a.height;
                    vec_span.Item5 = tuple_a.angle;
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
                        ptr[i * 5] = tuple_a.x;
                        ptr[i * 5 + 1] = tuple_a.y;
                        ptr[i * 5 + 2] = tuple_a.width;
                        ptr[i * 5 + 3] = tuple_a.height;
                        ptr[i * 5 + 4] = tuple_a.angle;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
                vec_span.Item3 = tuple_a.width;
                vec_span.Item4 = tuple_a.height;
                vec_span.Item5 = tuple_a.angle;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 5] = tuple_a.x;
                buff[i * 5 + 1] = tuple_a.y;
                buff[i * 5 + 2] = tuple_a.width;
                buff[i * 5 + 3] = tuple_a.height;
                buff[i * 5 + 4] = tuple_a.angle;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);
#endif
        }

        public (float x, float y, float width, float height, float angle)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float x, float y, float width, float height, float angle)[] ap = new (float x, float y, float width, float height, float angle)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec5f vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.width = vec_span.Item3;
                    tuple_ap.height = vec_span.Item4;
                    tuple_ap.angle = vec_span.Item5;
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
                        tuple_ap.x = ptr[i * 5];
                        tuple_ap.y = ptr[i * 5 + 1];
                        tuple_ap.width = ptr[i * 5 + 2];
                        tuple_ap.height = ptr[i * 5 + 3];
                        tuple_ap.angle = ptr[i * 5 + 4];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Converters.copyMatToArray<float>(this, buff, num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec5f vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.width = vec_span.Item3;
                tuple_ap.height = vec_span.Item4;
                tuple_ap.angle = vec_span.Item5;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];

            Converters.copyMatToArray<float>(this, buff, num * 5);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 5];
                tuple_ap.y = buff[i * 5 + 1];
                tuple_ap.width = buff[i * 5 + 2];
                tuple_ap.height = buff[i * 5 + 3];
                tuple_ap.angle = buff[i * 5 + 4];
            }
#endif

            return ap;
        }

        public MatOfRotatedRect(params (double x, double y, double width, double height, double angle)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (double x, double y, double width, double height, double angle)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec5f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = (float)tuple_a.x;
                    vec_span.Item2 = (float)tuple_a.y;
                    vec_span.Item3 = (float)tuple_a.width;
                    vec_span.Item4 = (float)tuple_a.height;
                    vec_span.Item5 = (float)tuple_a.angle;
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
                        ptr[i * 5] = (float)tuple_a.x;
                        ptr[i * 5 + 1] = (float)tuple_a.y;
                        ptr[i * 5 + 2] = (float)tuple_a.width;
                        ptr[i * 5 + 3] = (float)tuple_a.height;
                        ptr[i * 5 + 4] = (float)tuple_a.angle;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = (float)tuple_a.x;
                vec_span.Item2 = (float)tuple_a.y;
                vec_span.Item3 = (float)tuple_a.width;
                vec_span.Item4 = (float)tuple_a.height;
                vec_span.Item5 = (float)tuple_a.angle;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 5] = (float)tuple_a.x;
                buff[i * 5 + 1] = (float)tuple_a.y;
                buff[i * 5 + 2] = (float)tuple_a.width;
                buff[i * 5 + 3] = (float)tuple_a.height;
                buff[i * 5 + 4] = (float)tuple_a.angle;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 5);
#endif
        }

        public (double x, double y, double width, double height, double angle)[] toValueTupleArrayAsDouble()
        {
            int num = (int)total();
            (double x, double y, double width, double height, double angle)[] ap = new (double x, double y, double width, double height, double angle)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec5f> span = this.AsSpan<Vec5f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec5f vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.width = vec_span.Item3;
                    tuple_ap.height = vec_span.Item4;
                    tuple_ap.angle = vec_span.Item5;
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
                        tuple_ap.x = ptr[i * 5];
                        tuple_ap.y = ptr[i * 5 + 1];
                        tuple_ap.width = ptr[i * 5 + 2];
                        tuple_ap.height = ptr[i * 5 + 3];
                        tuple_ap.angle = ptr[i * 5 + 4];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 5);

            Converters.copyMatToArray<float>(this, buff, num * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec5f vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.width = vec_span.Item3;
                tuple_ap.height = vec_span.Item4;
                tuple_ap.angle = vec_span.Item5;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 5];

            Converters.copyMatToArray<float>(this, buff, num * 5);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 5];
                tuple_ap.y = buff[i * 5 + 1];
                tuple_ap.width = buff[i * 5 + 2];
                tuple_ap.height = buff[i * 5 + 3];
                tuple_ap.angle = buff[i * 5 + 4];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec5f> AsVec5fSpan()
        {
            return AsSpan<Vec5f>();
        }

#endif

    }
}
