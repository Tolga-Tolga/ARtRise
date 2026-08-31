using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfPoint : Mat
    {
        public MatOfPoint(params Vec2i[] a)
            : base()
        {
            fromVec2iArray(a);
        }

        public void fromVec2iArray(params Vec2i[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec2i>(a, this, num);
        }

        public Vec2i[] toVec2iArray()
        {
            int num = (int)total();
            Vec2i[] ap = new Vec2i[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec2i>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint(ReadOnlySpan<Vec2i> a)
            : base()
        {
            fromVec2iSpan(a);
        }

        public void fromVec2iSpan(ReadOnlySpan<Vec2i> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec2i>(a, this, num);
        }

#endif

        public MatOfPoint(params Vec2f[] a)
            : base()
        {
            fromVec2fArray(a);
        }

        public void fromVec2fArray(params Vec2f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly Vec2f vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
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
                        ref readonly Vec2f vec_a = ref a[i];
                        ptr[i * 2] = (int)vec_a.Item1;
                        ptr[i * 2 + 1] = (int)vec_a.Item2;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly Vec2f vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec2f vec_a = ref a[i];
                buff[i * 2] = (int)vec_a.Item1;
                buff[i * 2 + 1] = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

        public Vec2f[] toVec2fArray()
        {
            int num = (int)total();
            Vec2f[] ap = new Vec2f[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2f vec_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    vec_ap.Item1 = vec_span.Item1;
                    vec_ap.Item2 = vec_span.Item2;
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
                        ref Vec2f vec_ap = ref ap[i];
                        vec_ap.Item1 = ptr[i * 2];
                        vec_ap.Item2 = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2f vec_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                vec_ap.Item1 = vec_span.Item1;
                vec_ap.Item2 = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec2f vec_ap = ref ap[i];
                vec_ap.Item1 = buff[i * 2];
                vec_ap.Item2 = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint(ReadOnlySpan<Vec2f> a)
            : base()
        {
            fromVec2fSpan(a);
        }

        public void fromVec2fSpan(ReadOnlySpan<Vec2f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly Vec2f vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
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
                        ref readonly Vec2f vec_a = ref a[i];
                        ptr[i * 2] = (int)vec_a.Item1;
                        ptr[i * 2 + 1] = (int)vec_a.Item2;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly Vec2f vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec2f vec_a = ref a[i];
                buff[i * 2] = (int)vec_a.Item1;
                buff[i * 2 + 1] = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

#endif

        public MatOfPoint(params Vec2d[] a)
            : base()
        {

            fromVec2dArray(a);
        }

        public void fromVec2dArray(params Vec2d[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly Vec2d vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
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
                        ref readonly Vec2d vec_a = ref a[i];
                        ptr[i * 2] = (int)vec_a.Item1;
                        ptr[i * 2 + 1] = (int)vec_a.Item2;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly Vec2d vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec2d vec_a = ref a[i];
                buff[i * 2] = (int)vec_a.Item1;
                buff[i * 2 + 1] = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

        public Vec2d[] toVec2dArray()
        {
            int num = (int)total();
            Vec2d[] ap = new Vec2d[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2d vec_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    vec_ap.Item1 = vec_span.Item1;
                    vec_ap.Item2 = vec_span.Item2;
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
                        ref Vec2d vec_ap = ref ap[i];
                        vec_ap.Item1 = ptr[i * 2];
                        vec_ap.Item2 = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2d vec_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                vec_ap.Item1 = vec_span.Item1;
                vec_ap.Item2 = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec2d vec_ap = ref ap[i];
                vec_ap.Item1 = buff[i * 2];
                vec_ap.Item2 = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint(ReadOnlySpan<Vec2d> a)
            : base()
        {
            fromVec2dSpan(a);
        }

        public void fromVec2dSpan(ReadOnlySpan<Vec2d> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly Vec2d vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
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
                        ref readonly Vec2d vec_a = ref a[i];
                        ptr[i * 2] = (int)vec_a.Item1;
                        ptr[i * 2 + 1] = (int)vec_a.Item2;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly Vec2d vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec2d vec_a = ref a[i];
                buff[i * 2] = (int)vec_a.Item1;
                buff[i * 2 + 1] = (int)vec_a.Item2;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

#endif

        public MatOfPoint(params (int x, int y)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (int x, int y)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
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
                        ptr[i * 2] = tuple_a.x;
                        ptr[i * 2 + 1] = tuple_a.y;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 2] = tuple_a.x;
                buff[i * 2 + 1] = tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif

        }

        public (int x, int y)[] toValueTupleArrayAsInt()
        {
            int num = (int)total();
            (int x, int y)[] ap = new (int x, int y)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
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
                        tuple_ap.x = ptr[i * 2];
                        tuple_ap.y = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 2];
                tuple_ap.y = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

        public MatOfPoint(params (float x, float y)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float x, float y)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = (int)tuple_a.x;
                    vec_span.Item2 = (int)tuple_a.y;
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
                        ptr[i * 2] = (int)tuple_a.x;
                        ptr[i * 2 + 1] = (int)tuple_a.y;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = (int)tuple_a.x;
                vec_span.Item2 = (int)tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 2] = (int)tuple_a.x;
                buff[i * 2 + 1] = (int)tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

        public (float x, float y)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float x, float y)[] ap = new (float x, float y)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
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
                        tuple_ap.x = ptr[i * 2];
                        tuple_ap.y = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 2];
                tuple_ap.y = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

        public MatOfPoint(params (double x, double y)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (double x, double y)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = (int)tuple_a.x;
                    vec_span.Item2 = (int)tuple_a.y;
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
                        ptr[i * 2] = (int)tuple_a.x;
                        ptr[i * 2 + 1] = (int)tuple_a.y;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = (int)tuple_a.x;
                vec_span.Item2 = (int)tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 2] = (int)tuple_a.x;
                buff[i * 2 + 1] = (int)tuple_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

        public (double x, double y)[] toValueTupleArrayAsDouble()
        {
            int num = (int)total();
            (double x, double y)[] ap = new (double x, double y)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
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
                        tuple_ap.x = ptr[i * 2];
                        tuple_ap.y = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 2];
                tuple_ap.y = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

        public MatOfPoint(params UnityEngine.Vector2Int[] a)
            : base()
        {
            fromVector2IntArray(a);
        }

        public void fromVector2IntArray(params UnityEngine.Vector2Int[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<UnityEngine.Vector2Int>(a, this, num);
        }

        public UnityEngine.Vector2Int[] toVector2IntArray()
        {
            int num = (int)total();
            UnityEngine.Vector2Int[] ap = new UnityEngine.Vector2Int[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<UnityEngine.Vector2Int>(this, ap, num);

            return ap;
        }

        public MatOfPoint(params UnityEngine.Vector2[] a)
            : base()
        {
            fromVector2Array(a);
        }

        public void fromVector2Array(params UnityEngine.Vector2[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec2i vec_span = ref span[i];
                    ref readonly var vector_a = ref a[i];
                    vec_span.Item1 = (int)vector_a.x;
                    vec_span.Item2 = (int)vector_a.y;
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
                        ptr[i * 2] = (int)vector_a.x;
                        ptr[i * 2 + 1] = (int)vector_a.y;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec_span = ref buffSpan[i];
                ref readonly var vector_a = ref a[i];
                vec_span.Item1 = (int)vector_a.x;
                vec_span.Item2 = (int)vector_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var vector_a = ref a[i];
                buff[i * 2] = (int)vector_a.x;
                buff[i * 2 + 1] = (int)vector_a.y;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 2);
#endif
        }

        public UnityEngine.Vector2[] toVector2Array()
        {
            int num = (int)total();
            UnityEngine.Vector2[] ap = new UnityEngine.Vector2[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec2i> span = this.AsSpan<Vec2i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var vector_ap = ref ap[i];
                    ref readonly Vec2i vec_span = ref span[i];
                    vector_ap.x = vec_span.Item1;
                    vector_ap.y = vec_span.Item2;
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
                        ref var vector_ap = ref ap[i];
                        vector_ap.x = ptr[i * 2];
                        vector_ap.y = ptr[i * 2 + 1];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(num * 2);

            Converters.copyMatToArray<int>(this, buff, num * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var vector_ap = ref ap[i];
                ref readonly Vec2i vec_span = ref buffSpan[i];
                vector_ap.x = vec_span.Item1;
                vector_ap.y = vec_span.Item2;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 2];

            Converters.copyMatToArray<int>(this, buff, num * 2);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var vector_ap = ref ap[i];
                vector_ap.x = buff[i * 2];
                vector_ap.y = buff[i * 2 + 1];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec2i> AsVec2iSpan()
        {
            return AsSpan<Vec2i>();
        }

        public Span<UnityEngine.Vector2Int> AsVector2IntSpan()
        {
            return AsSpan<UnityEngine.Vector2Int>();
        }

#endif

    }
}
