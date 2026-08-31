using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfRect : Mat
    {
        public MatOfRect(params Vec4i[] a)
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

        public MatOfRect(ReadOnlySpan<Vec4i> a)
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

        public MatOfRect(params Vec4f[] a)
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

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4i vec_span = ref span[i];
                    ref readonly Vec4f vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
                    vec_span.Item3 = (int)vec_a.Item3;
                    vec_span.Item4 = (int)vec_a.Item4;
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
                        ref readonly Vec4f vec_a = ref a[i];
                        ptr[i * 4] = (int)vec_a.Item1;
                        ptr[i * 4 + 1] = (int)vec_a.Item2;
                        ptr[i * 4 + 2] = (int)vec_a.Item3;
                        ptr[i * 4 + 3] = (int)vec_a.Item4;
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
                ref readonly Vec4f vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
                vec_span.Item3 = (int)vec_a.Item3;
                vec_span.Item4 = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec4f vec_a = ref a[i];
                buff[i * 4] = (int)vec_a.Item1;
                buff[i * 4 + 1] = (int)vec_a.Item2;
                buff[i * 4 + 2] = (int)vec_a.Item3;
                buff[i * 4 + 3] = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public Vec4f[] toVec4fArray()
        {
            int num = (int)total();
            Vec4f[] ap = new Vec4f[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4f vec_ap = ref ap[i];
                    ref readonly Vec4i vec_span = ref span[i];
                    vec_ap.Item1 = vec_span.Item1;
                    vec_ap.Item2 = vec_span.Item2;
                    vec_ap.Item3 = vec_span.Item3;
                    vec_ap.Item4 = vec_span.Item4;
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
                        ref Vec4f vec_ap = ref ap[i];
                        vec_ap.Item1 = ptr[i * 4];
                        vec_ap.Item2 = ptr[i * 4 + 1];
                        vec_ap.Item3 = ptr[i * 4 + 2];
                        vec_ap.Item4 = ptr[i * 4 + 3];
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
                ref Vec4f vec_ap = ref ap[i];
                ref readonly Vec4i vec_span = ref buffSpan[i];
                vec_ap.Item1 = vec_span.Item1;
                vec_ap.Item2 = vec_span.Item2;
                vec_ap.Item3 = vec_span.Item3;
                vec_ap.Item4 = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec4f vec_ap = ref ap[i];
                vec_ap.Item1 = buff[i * 4];
                vec_ap.Item2 = buff[i * 4 + 1];
                vec_ap.Item3 = buff[i * 4 + 2];
                vec_ap.Item4 = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfRect(ReadOnlySpan<Vec4f> a)
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

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4i vec_span = ref span[i];
                    ref readonly Vec4f vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
                    vec_span.Item3 = (int)vec_a.Item3;
                    vec_span.Item4 = (int)vec_a.Item4;
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
                        ref readonly Vec4f vec_a = ref a[i];
                        ptr[i * 4] = (int)vec_a.Item1;
                        ptr[i * 4 + 1] = (int)vec_a.Item2;
                        ptr[i * 4 + 2] = (int)vec_a.Item3;
                        ptr[i * 4 + 3] = (int)vec_a.Item4;
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
                ref readonly Vec4f vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
                vec_span.Item3 = (int)vec_a.Item3;
                vec_span.Item4 = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec4f vec_a = ref a[i];
                buff[i * 4] = (int)vec_a.Item1;
                buff[i * 4 + 1] = (int)vec_a.Item2;
                buff[i * 4 + 2] = (int)vec_a.Item3;
                buff[i * 4 + 3] = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

#endif

        public MatOfRect(params Vec4d[] a)
            : base()
        {
            fromVec4dArray(a);
        }

        public void fromVec4dArray(params Vec4d[] a)
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
                    ref readonly Vec4d vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
                    vec_span.Item3 = (int)vec_a.Item3;
                    vec_span.Item4 = (int)vec_a.Item4;
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
                        ref readonly Vec4d vec_a = ref a[i];
                        ptr[i * 4] = (int)vec_a.Item1;
                        ptr[i * 4 + 1] = (int)vec_a.Item2;
                        ptr[i * 4 + 2] = (int)vec_a.Item3;
                        ptr[i * 4 + 3] = (int)vec_a.Item4;
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
                ref readonly Vec4d vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
                vec_span.Item3 = (int)vec_a.Item3;
                vec_span.Item4 = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec4d vec_a = ref a[i];
                buff[i * 4] = (int)vec_a.Item1;
                buff[i * 4 + 1] = (int)vec_a.Item2;
                buff[i * 4 + 2] = (int)vec_a.Item3;
                buff[i * 4 + 3] = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public Vec4d[] toVec4dArray()
        {
            int num = (int)total();
            Vec4d[] ap = new Vec4d[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec4d vec_ap = ref ap[i];
                    ref readonly Vec4i vec_span = ref span[i];
                    vec_ap.Item1 = vec_span.Item1;
                    vec_ap.Item2 = vec_span.Item2;
                    vec_ap.Item3 = vec_span.Item3;
                    vec_ap.Item4 = vec_span.Item4;
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
                        ref Vec4d vec_ap = ref ap[i];
                        vec_ap.Item1 = ptr[i * 4];
                        vec_ap.Item2 = ptr[i * 4 + 1];
                        vec_ap.Item3 = ptr[i * 4 + 2];
                        vec_ap.Item4 = ptr[i * 4 + 3];
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
                ref Vec4d vec_ap = ref ap[i];
                ref readonly Vec4i vec_span = ref buffSpan[i];
                vec_ap.Item1 = vec_span.Item1;
                vec_ap.Item2 = vec_span.Item2;
                vec_ap.Item3 = vec_span.Item3;
                vec_ap.Item4 = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec4d vec_ap = ref ap[i];
                vec_ap.Item1 = buff[i * 4];
                vec_ap.Item2 = buff[i * 4 + 1];
                vec_ap.Item3 = buff[i * 4 + 2];
                vec_ap.Item4 = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfRect(ReadOnlySpan<Vec4d> a)
            : base()
        {
            fromVec4dSpan(a);
        }

        public void fromVec4dSpan(ReadOnlySpan<Vec4d> a)
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
                    ref readonly Vec4d vec_a = ref a[i];
                    vec_span.Item1 = (int)vec_a.Item1;
                    vec_span.Item2 = (int)vec_a.Item2;
                    vec_span.Item3 = (int)vec_a.Item3;
                    vec_span.Item4 = (int)vec_a.Item4;
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
                        ref readonly Vec4d vec_a = ref a[i];
                        ptr[i * 4] = (int)vec_a.Item1;
                        ptr[i * 4 + 1] = (int)vec_a.Item2;
                        ptr[i * 4 + 2] = (int)vec_a.Item3;
                        ptr[i * 4 + 3] = (int)vec_a.Item4;
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
                ref readonly Vec4d vec_a = ref a[i];
                vec_span.Item1 = (int)vec_a.Item1;
                vec_span.Item2 = (int)vec_a.Item2;
                vec_span.Item3 = (int)vec_a.Item3;
                vec_span.Item4 = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec4d vec_a = ref a[i];
                buff[i * 4] = (int)vec_a.Item1;
                buff[i * 4 + 1] = (int)vec_a.Item2;
                buff[i * 4 + 2] = (int)vec_a.Item3;
                buff[i * 4 + 3] = (int)vec_a.Item4;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

#endif

        public MatOfRect(params (int x, int y, int width, int height)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (int x, int y, int width, int height)[] a)
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
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
                    vec_span.Item3 = tuple_a.width;
                    vec_span.Item4 = tuple_a.height;
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
                        ptr[i * 4] = tuple_a.x;
                        ptr[i * 4 + 1] = tuple_a.y;
                        ptr[i * 4 + 2] = tuple_a.width;
                        ptr[i * 4 + 3] = tuple_a.height;
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
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
                vec_span.Item3 = tuple_a.width;
                vec_span.Item4 = tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 4] = tuple_a.x;
                buff[i * 4 + 1] = tuple_a.y;
                buff[i * 4 + 2] = tuple_a.width;
                buff[i * 4 + 3] = tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public (int x, int y, int width, int height)[] toValueTupleArrayAsInt()
        {
            int num = (int)total();
            (int x, int y, int width, int height)[] ap = new (int x, int y, int width, int height)[num];
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
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.width = vec_span.Item3;
                    tuple_ap.height = vec_span.Item4;
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
                        tuple_ap.x = ptr[i * 4];
                        tuple_ap.y = ptr[i * 4 + 1];
                        tuple_ap.width = ptr[i * 4 + 2];
                        tuple_ap.height = ptr[i * 4 + 3];
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
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.width = vec_span.Item3;
                tuple_ap.height = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 4];
                tuple_ap.y = buff[i * 4 + 1];
                tuple_ap.width = buff[i * 4 + 2];
                tuple_ap.height = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

        public MatOfRect(params (float x, float y, float width, float height)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float x, float y, float width, float height)[] a)
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
                    vec_span.Item1 = (int)tuple_a.x;
                    vec_span.Item2 = (int)tuple_a.y;
                    vec_span.Item3 = (int)tuple_a.width;
                    vec_span.Item4 = (int)tuple_a.height;
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
                        ptr[i * 4] = (int)tuple_a.x;
                        ptr[i * 4 + 1] = (int)tuple_a.y;
                        ptr[i * 4 + 2] = (int)tuple_a.width;
                        ptr[i * 4 + 3] = (int)tuple_a.height;
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
                vec_span.Item1 = (int)tuple_a.x;
                vec_span.Item2 = (int)tuple_a.y;
                vec_span.Item3 = (int)tuple_a.width;
                vec_span.Item4 = (int)tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 4] = (int)tuple_a.x;
                buff[i * 4 + 1] = (int)tuple_a.y;
                buff[i * 4 + 2] = (int)tuple_a.width;
                buff[i * 4 + 3] = (int)tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public (float x, float y, float width, float height)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float x, float y, float width, float height)[] ap = new (float x, float y, float width, float height)[num];
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
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.width = vec_span.Item3;
                    tuple_ap.height = vec_span.Item4;
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
                        tuple_ap.x = ptr[i * 4];
                        tuple_ap.y = ptr[i * 4 + 1];
                        tuple_ap.width = ptr[i * 4 + 2];
                        tuple_ap.height = ptr[i * 4 + 3];
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
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.width = vec_span.Item3;
                tuple_ap.height = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 4];
                tuple_ap.y = buff[i * 4 + 1];
                tuple_ap.width = buff[i * 4 + 2];
                tuple_ap.height = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

        public MatOfRect(params (double x, double y, double width, double height)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (double x, double y, double width, double height)[] a)
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
                    vec_span.Item1 = (int)tuple_a.x;
                    vec_span.Item2 = (int)tuple_a.y;
                    vec_span.Item3 = (int)tuple_a.width;
                    vec_span.Item4 = (int)tuple_a.height;
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
                        ptr[i * 4] = (int)tuple_a.x;
                        ptr[i * 4 + 1] = (int)tuple_a.y;
                        ptr[i * 4 + 2] = (int)tuple_a.width;
                        ptr[i * 4 + 3] = (int)tuple_a.height;
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
                vec_span.Item1 = (int)tuple_a.x;
                vec_span.Item2 = (int)tuple_a.y;
                vec_span.Item3 = (int)tuple_a.width;
                vec_span.Item4 = (int)tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 4] = (int)tuple_a.x;
                buff[i * 4 + 1] = (int)tuple_a.y;
                buff[i * 4 + 2] = (int)tuple_a.width;
                buff[i * 4 + 3] = (int)tuple_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public (double x, double y, double width, double height)[] toValueTupleArrayAsDouble()
        {
            int num = (int)total();
            (double x, double y, double width, double height)[] ap = new (double x, double y, double width, double height)[num];
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
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.width = vec_span.Item3;
                    tuple_ap.height = vec_span.Item4;
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
                        tuple_ap.x = ptr[i * 4];
                        tuple_ap.y = ptr[i * 4 + 1];
                        tuple_ap.width = ptr[i * 4 + 2];
                        tuple_ap.height = ptr[i * 4 + 3];
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
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.width = vec_span.Item3;
                tuple_ap.height = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 4];
                tuple_ap.y = buff[i * 4 + 1];
                tuple_ap.width = buff[i * 4 + 2];
                tuple_ap.height = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

        public MatOfRect(params UnityEngine.RectInt[] a)
            : base()
        {
            fromRectIntArray(a);
        }

        public void fromRectIntArray(params UnityEngine.RectInt[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<UnityEngine.RectInt>(a, this, num);
        }

        public UnityEngine.RectInt[] toRectIntArray()
        {
            int num = (int)total();
            UnityEngine.RectInt[] ap = new UnityEngine.RectInt[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<UnityEngine.RectInt>(this, ap, num);

            return ap;
        }

        public MatOfRect(params UnityEngine.Rect[] a)
            : base()
        {
            fromRectArray(a);
        }

        public void fromRectArray(params UnityEngine.Rect[] a)
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
                    ref readonly var rect_a = ref a[i];
                    vec_span.Item1 = (int)rect_a.x;
                    vec_span.Item2 = (int)rect_a.y;
                    vec_span.Item3 = (int)rect_a.width;
                    vec_span.Item4 = (int)rect_a.height;
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
                        ref readonly var rect_a = ref a[i];
                        ptr[i * 4] = (int)rect_a.x;
                        ptr[i * 4 + 1] = (int)rect_a.y;
                        ptr[i * 4 + 2] = (int)rect_a.width;
                        ptr[i * 4 + 3] = (int)rect_a.height;
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
                ref readonly var rect_a = ref a[i];
                vec_span.Item1 = (int)rect_a.x;
                vec_span.Item2 = (int)rect_a.y;
                vec_span.Item3 = (int)rect_a.width;
                vec_span.Item4 = (int)rect_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var rect_a = ref a[i];
                buff[i * 4] = (int)rect_a.x;
                buff[i * 4 + 1] = (int)rect_a.y;
                buff[i * 4 + 2] = (int)rect_a.width;
                buff[i * 4 + 3] = (int)rect_a.height;
            }

            Converters.copyArrayToMat<int>(buff, this, num * 4);
#endif
        }

        public UnityEngine.Rect[] toRectArray()
        {
            int num = (int)total();
            UnityEngine.Rect[] ap = new UnityEngine.Rect[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec4i> span = this.AsSpan<Vec4i>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var rect_ap = ref ap[i];
                    ref readonly Vec4i vec_span = ref span[i];
                    rect_ap.x = vec_span.Item1;
                    rect_ap.y = vec_span.Item2;
                    rect_ap.width = vec_span.Item3;
                    rect_ap.height = vec_span.Item4;
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
                        ref var rect_ap = ref ap[i];
                        rect_ap.x = ptr[i * 4];
                        rect_ap.y = ptr[i * 4 + 1];
                        rect_ap.width = ptr[i * 4 + 2];
                        rect_ap.height = ptr[i * 4 + 3];
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
                ref var rect_ap = ref ap[i];
                ref readonly Vec4i vec_span = ref buffSpan[i];
                rect_ap.x = vec_span.Item1;
                rect_ap.y = vec_span.Item2;
                rect_ap.width = vec_span.Item3;
                rect_ap.height = vec_span.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[num * 4];

            Converters.copyMatToArray<int>(this, buff, num * 4);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var rect_ap = ref ap[i];
                rect_ap.x = buff[i * 4];
                rect_ap.y = buff[i * 4 + 1];
                rect_ap.width = buff[i * 4 + 2];
                rect_ap.height = buff[i * 4 + 3];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec4i> AsVec4iSpan()
        {
            return AsSpan<Vec4i>();
        }

        public Span<UnityEngine.RectInt> AsRectIntSpan()
        {
            return AsSpan<UnityEngine.RectInt>();
        }

#endif

    }
}
