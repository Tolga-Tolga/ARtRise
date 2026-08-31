using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfPoint3f : Mat
    {
        public MatOfPoint3f(params Vec3f[] a)
            : base()
        {
            fromVec3fArray(a);
        }

        public void fromVec3fArray(params Vec3f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec3f>(a, this, num);
        }

        public Vec3f[] toVec3fArray()
        {
            int num = (int)total();
            Vec3f[] ap = new Vec3f[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec3f>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint3f(ReadOnlySpan<Vec3f> a)
            : base()
        {
            fromVec3fSpan(a);
        }

        public void fromVec3fSpan(ReadOnlySpan<Vec3f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec3f>(a, this, num);
        }

#endif

        public MatOfPoint3f(params Vec3i[] a)
            : base()
        {
            fromVec3iArray(a);
        }

        public void fromVec3iArray(params Vec3i[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly Vec3i vec_a = ref a[i];
                    vec_span.Item1 = vec_a.Item1;
                    vec_span.Item2 = vec_a.Item2;
                    vec_span.Item3 = vec_a.Item3;
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
                        ref readonly Vec3i vec_a = ref a[i];
                        ptr[i * 3] = vec_a.Item1;
                        ptr[i * 3 + 1] = vec_a.Item2;
                        ptr[i * 3 + 2] = vec_a.Item3;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly Vec3i vec_a = ref a[i];
                vec_span.Item1 = vec_a.Item1;
                vec_span.Item2 = vec_a.Item2;
                vec_span.Item3 = vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec3i vec_a = ref a[i];
                buff[i * 3] = vec_a.Item1;
                buff[i * 3 + 1] = vec_a.Item2;
                buff[i * 3 + 2] = vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

        public Vec3i[] toVec3iArray()
        {
            int num = (int)total();
            Vec3i[] ap = new Vec3i[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3i vec_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    vec_ap.Item1 = (int)vec_span.Item1;
                    vec_ap.Item2 = (int)vec_span.Item2;
                    vec_ap.Item3 = (int)vec_span.Item3;
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
                        ref Vec3i vec_ap = ref ap[i];
                        vec_ap.Item1 = (int)ptr[i * 3];
                        vec_ap.Item2 = (int)ptr[i * 3 + 1];
                        vec_ap.Item3 = (int)ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3i vec_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                vec_ap.Item1 = (int)vec_span.Item1;
                vec_ap.Item2 = (int)vec_span.Item2;
                vec_ap.Item3 = (int)vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec3i vec_ap = ref ap[i];
                vec_ap.Item1 = (int)buff[i * 3];
                vec_ap.Item2 = (int)buff[i * 3 + 1];
                vec_ap.Item3 = (int)buff[i * 3 + 2];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint3f(ReadOnlySpan<Vec3i> a)
            : base()
        {
            fromVec3iSpan(a);
        }

        public void fromVec3iSpan(ReadOnlySpan<Vec3i> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly Vec3i vec_a = ref a[i];
                    vec_span.Item1 = vec_a.Item1;
                    vec_span.Item2 = vec_a.Item2;
                    vec_span.Item3 = vec_a.Item3;
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
                        ref readonly Vec3i vec_a = ref a[i];
                        ptr[i * 3] = vec_a.Item1;
                        ptr[i * 3 + 1] = vec_a.Item2;
                        ptr[i * 3 + 2] = vec_a.Item3;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly Vec3i vec_a = ref a[i];
                vec_span.Item1 = vec_a.Item1;
                vec_span.Item2 = vec_a.Item2;
                vec_span.Item3 = vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec3i vec_a = ref a[i];
                buff[i * 3] = vec_a.Item1;
                buff[i * 3 + 1] = vec_a.Item2;
                buff[i * 3 + 2] = vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

#endif

        public MatOfPoint3f(params Vec3d[] a)
            : base()
        {
            fromVec3dArray(a);
        }

        public void fromVec3dArray(params Vec3d[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly Vec3d vec_a = ref a[i];
                    vec_span.Item1 = (float)vec_a.Item1;
                    vec_span.Item2 = (float)vec_a.Item2;
                    vec_span.Item3 = (float)vec_a.Item3;
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
                        ref readonly Vec3d vec_a = ref a[i];
                        ptr[i * 3] = (float)vec_a.Item1;
                        ptr[i * 3 + 1] = (float)vec_a.Item2;
                        ptr[i * 3 + 2] = (float)vec_a.Item3;
                    }

                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly Vec3d vec_a = ref a[i];
                vec_span.Item1 = (float)vec_a.Item1;
                vec_span.Item2 = (float)vec_a.Item2;
                vec_span.Item3 = (float)vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec3d vec_a = ref a[i];
                buff[i * 3] = (float)vec_a.Item1;
                buff[i * 3 + 1] = (float)vec_a.Item2;
                buff[i * 3 + 2] = (float)vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

        public Vec3d[] toVec3dArray()
        {
            int num = (int)total();
            Vec3d[] ap = new Vec3d[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3d vec_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    vec_ap.Item1 = vec_span.Item1;
                    vec_ap.Item2 = vec_span.Item2;
                    vec_ap.Item3 = vec_span.Item3;
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
                        ref Vec3d vec_ap = ref ap[i];
                        vec_ap.Item1 = ptr[i * 3];
                        vec_ap.Item2 = ptr[i * 3 + 1];
                        vec_ap.Item3 = ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3d vec_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                vec_ap.Item1 = vec_span.Item1;
                vec_ap.Item2 = vec_span.Item2;
                vec_ap.Item3 = vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref Vec3d vec_ap = ref ap[i];
                vec_ap.Item1 = buff[i * 3];
                vec_ap.Item2 = buff[i * 3 + 1];
                vec_ap.Item3 = buff[i * 3 + 2];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfPoint3f(ReadOnlySpan<Vec3d> a)
            : base()
        {
            fromVec3dSpan(a);
        }

        public void fromVec3dSpan(ReadOnlySpan<Vec3d> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly Vec3d vec_a = ref a[i];
                    vec_span.Item1 = (float)vec_a.Item1;
                    vec_span.Item2 = (float)vec_a.Item2;
                    vec_span.Item3 = (float)vec_a.Item3;
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
                        ref readonly Vec3d vec_a = ref a[i];
                        ptr[i * 3] = (float)vec_a.Item1;
                        ptr[i * 3 + 1] = (float)vec_a.Item2;
                        ptr[i * 3 + 2] = (float)vec_a.Item3;
                    }

                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly Vec3d vec_a = ref a[i];
                vec_span.Item1 = (float)vec_a.Item1;
                vec_span.Item2 = (float)vec_a.Item2;
                vec_span.Item3 = (float)vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly Vec3d vec_a = ref a[i];
                buff[i * 3] = (float)vec_a.Item1;
                buff[i * 3 + 1] = (float)vec_a.Item2;
                buff[i * 3 + 2] = (float)vec_a.Item3;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

#endif

        public MatOfPoint3f(params (float x, float y, float z)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float x, float y, float z)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
                    vec_span.Item3 = tuple_a.z;
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
                        ptr[i * 3] = tuple_a.x;
                        ptr[i * 3 + 1] = tuple_a.y;
                        ptr[i * 3 + 2] = tuple_a.z;
                    }

                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
                vec_span.Item3 = tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 3] = tuple_a.x;
                buff[i * 3 + 1] = tuple_a.y;
                buff[i * 3 + 2] = tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif

        }

        public (float x, float y, float z)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float x, float y, float z)[] ap = new (float x, float y, float z)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.z = vec_span.Item3;
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
                        tuple_ap.x = ptr[i * 3];
                        tuple_ap.y = ptr[i * 3 + 1];
                        tuple_ap.z = ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.z = vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 3];
                tuple_ap.y = buff[i * 3 + 1];
                tuple_ap.z = buff[i * 3 + 2];
            }
#endif

            return ap;
        }

        public MatOfPoint3f(params (int x, int y, int z)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (int x, int y, int z)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
                    vec_span.Item3 = tuple_a.z;
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
                        ptr[i * 3] = tuple_a.x;
                        ptr[i * 3 + 1] = tuple_a.y;
                        ptr[i * 3 + 2] = tuple_a.z;
                    }

                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
                vec_span.Item3 = tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 3] = tuple_a.x;
                buff[i * 3 + 1] = tuple_a.y;
                buff[i * 3 + 2] = tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

        public (int x, int y, int z)[] toValueTupleArrayAsInt()
        {
            int num = (int)total();
            (int x, int y, int z)[] ap = new (int x, int y, int z)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    tuple_ap.x = (int)vec_span.Item1;
                    tuple_ap.y = (int)vec_span.Item2;
                    tuple_ap.z = (int)vec_span.Item3;
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
                        tuple_ap.x = (int)ptr[i * 3];
                        tuple_ap.y = (int)ptr[i * 3 + 1];
                        tuple_ap.z = (int)ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                tuple_ap.x = (int)vec_span.Item1;
                tuple_ap.y = (int)vec_span.Item2;
                tuple_ap.z = (int)vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = (int)buff[i * 3];
                tuple_ap.y = (int)buff[i * 3 + 1];
                tuple_ap.z = (int)buff[i * 3 + 2];
            }
#endif

            return ap;
        }

        public MatOfPoint3f(params (double x, double y, double z)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (double x, double y, double z)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = (float)tuple_a.x;
                    vec_span.Item2 = (float)tuple_a.y;
                    vec_span.Item3 = (float)tuple_a.z;
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
                        ptr[i * 3] = (float)tuple_a.x;
                        ptr[i * 3 + 1] = (float)tuple_a.y;
                        ptr[i * 3 + 2] = (float)tuple_a.z;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = (float)tuple_a.x;
                vec_span.Item2 = (float)tuple_a.y;
                vec_span.Item3 = (float)tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 3] = (float)tuple_a.x;
                buff[i * 3 + 1] = (float)tuple_a.y;
                buff[i * 3 + 2] = (float)tuple_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

        public (double x, double y, double z)[] toValueTupleArrayAsDouble()
        {
            int num = (int)total();
            (double x, double y, double z)[] ap = new (double x, double y, double z)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.z = vec_span.Item3;
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
                        tuple_ap.x = ptr[i * 3];
                        tuple_ap.y = ptr[i * 3 + 1];
                        tuple_ap.z = ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.z = vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 3];
                tuple_ap.y = buff[i * 3 + 1];
                tuple_ap.z = buff[i * 3 + 2];
            }
#endif

            return ap;
        }

        public MatOfPoint3f(params UnityEngine.Vector3Int[] a)
            : base()
        {
            fromVector3IntArray(a);
        }

        public void fromVector3IntArray(params UnityEngine.Vector3Int[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec3f vec_span = ref span[i];
                    ref readonly var vector_a = ref a[i];
                    vec_span.Item1 = vector_a.x;
                    vec_span.Item2 = vector_a.y;
                    vec_span.Item3 = vector_a.z;
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
                        ref readonly var vector_a = ref a[i];
                        ptr[i * 3] = vector_a.x;
                        ptr[i * 3 + 1] = vector_a.y;
                        ptr[i * 3 + 2] = vector_a.z;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec3f vec_span = ref buffSpan[i];
                ref readonly var vector_a = ref a[i];
                vec_span.Item1 = vector_a.x;
                vec_span.Item2 = vector_a.y;
                vec_span.Item3 = vector_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var vector_a = ref a[i];
                buff[i * 3] = vector_a.x;
                buff[i * 3 + 1] = vector_a.y;
                buff[i * 3 + 2] = vector_a.z;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 3);
#endif
        }

        public UnityEngine.Vector3Int[] toVector3IntArray()
        {
            int num = (int)total();
            UnityEngine.Vector3Int[] ap = new UnityEngine.Vector3Int[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec3f> span = this.AsSpan<Vec3f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var vector_ap = ref ap[i];
                    ref readonly Vec3f vec_span = ref span[i];
                    vector_ap.x = (int)vec_span.Item1;
                    vector_ap.y = (int)vec_span.Item2;
                    vector_ap.z = (int)vec_span.Item3;
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
                        ref var vector_ap = ref ap[i];
                        vector_ap.x = (int)ptr[i * 3];
                        vector_ap.y = (int)ptr[i * 3 + 1];
                        vector_ap.z = (int)ptr[i * 3 + 2];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 3);

            Converters.copyMatToArray<float>(this, buff, num * 3);

            Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var vector_ap = ref ap[i];
                ref readonly Vec3f vec_span = ref buffSpan[i];
                vector_ap.x = (int)vec_span.Item1;
                vector_ap.y = (int)vec_span.Item2;
                vector_ap.z = (int)vec_span.Item3;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 3];

            Converters.copyMatToArray<float>(this, buff, num * 3);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var vector_ap = ref ap[i];
                vector_ap.x = (int)buff[i * 3];
                vector_ap.y = (int)buff[i * 3 + 1];
                vector_ap.z = (int)buff[i * 3 + 2];
            }
#endif

            return ap;
        }

        public MatOfPoint3f(params UnityEngine.Vector3[] a)
            : base()
        {
            fromVector3Array(a);
        }

        public void fromVector3Array(params UnityEngine.Vector3[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<UnityEngine.Vector3>(a, this, num);
        }

        public UnityEngine.Vector3[] toVector3Array()
        {
            int num = (int)total();
            UnityEngine.Vector3[] ap = new UnityEngine.Vector3[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<UnityEngine.Vector3>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec3f> AsVec3fSpan()
        {
            return AsSpan<Vec3f>();
        }

        public Span<UnityEngine.Vector3> AsVector3Span()
        {
            return AsSpan<UnityEngine.Vector3>();
        }

#endif

    }
}
