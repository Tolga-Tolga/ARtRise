using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    public partial class MatOfKeyPoint : Mat
    {
        public MatOfKeyPoint(params Vec7f[] a)
           : base()
        {
            fromVec7fArray(a);
        }

        public void fromVec7fArray(params Vec7f[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec7f>(a, this, num);
        }

        public Vec7f[] toVec7fArray()
        {
            int num = (int)total();
            Vec7f[] ap = new Vec7f[num];
            if (num == 0)
                return ap;

            Converters.copyMatToArray<Vec7f>(this, ap, num);

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public MatOfKeyPoint(ReadOnlySpan<Vec7f> a)
            : base()
        {
            fromVec7fSpan(a);
        }

        public void fromVec7fSpan(ReadOnlySpan<Vec7f> a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.copyArrayToMat<Vec7f>(a, this, num);
        }

#endif

        public MatOfKeyPoint(params (float x, float y, float size, float angle, float response, float octave, float class_id)[] a)
            : base()
        {
            fromValueTupleArray(a);
        }

        public void fromValueTupleArray(params (float x, float y, float size, float angle, float response, float octave, float class_id)[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec7f> span = this.AsSpan<Vec7f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref Vec7f vec_span = ref span[i];
                    ref readonly var tuple_a = ref a[i];
                    vec_span.Item1 = tuple_a.x;
                    vec_span.Item2 = tuple_a.y;
                    vec_span.Item3 = tuple_a.size;
                    vec_span.Item4 = tuple_a.angle;
                    vec_span.Item5 = tuple_a.response;
                    vec_span.Item6 = tuple_a.octave;
                    vec_span.Item7 = tuple_a.class_id;
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
                        ptr[i * 7] = tuple_a.x;
                        ptr[i * 7 + 1] = tuple_a.y;
                        ptr[i * 7 + 2] = tuple_a.size;
                        ptr[i * 7 + 3] = tuple_a.angle;
                        ptr[i * 7 + 4] = tuple_a.response;
                        ptr[i * 7 + 5] = tuple_a.octave;
                        ptr[i * 7 + 6] = tuple_a.class_id;
                    }
                }

                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 7);

            Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec7f vec_span = ref buffSpan[i];
                ref readonly var tuple_a = ref a[i];
                vec_span.Item1 = tuple_a.x;
                vec_span.Item2 = tuple_a.y;
                vec_span.Item3 = tuple_a.size;
                vec_span.Item4 = tuple_a.angle;
                vec_span.Item5 = tuple_a.response;
                vec_span.Item6 = tuple_a.octave;
                vec_span.Item7 = tuple_a.class_id;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 7);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 7];
            for (int i = 0; i < a.Length; i++)
            {
                ref readonly var tuple_a = ref a[i];
                buff[i * 7] = tuple_a.x;
                buff[i * 7 + 1] = tuple_a.y;
                buff[i * 7 + 2] = tuple_a.size;
                buff[i * 7 + 3] = tuple_a.angle;
                buff[i * 7 + 4] = tuple_a.response;
                buff[i * 7 + 5] = tuple_a.octave;
                buff[i * 7 + 6] = tuple_a.class_id;
            }

            Converters.copyArrayToMat<float>(buff, this, num * 7);
#endif
        }

        public (float x, float y, float size, float angle, float response, float octave, float class_id)[] toValueTupleArrayAsFloat()
        {
            int num = (int)total();
            (float x, float y, float size, float angle, float response, float octave, float class_id)[] ap = new (float x, float y, float size, float angle, float response, float octave, float class_id)[num];
            if (num == 0)
                return ap;

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
            if (this.isContinuous())
            {
                Span<Vec7f> span = this.AsSpan<Vec7f>();
                for (int i = 0; i < span.Length; i++)
                {
                    ref var tuple_ap = ref ap[i];
                    ref readonly Vec7f vec_span = ref span[i];
                    tuple_ap.x = vec_span.Item1;
                    tuple_ap.y = vec_span.Item2;
                    tuple_ap.size = vec_span.Item3;
                    tuple_ap.angle = vec_span.Item4;
                    tuple_ap.response = vec_span.Item5;
                    tuple_ap.octave = vec_span.Item6;
                    tuple_ap.class_id = vec_span.Item7;
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
                        tuple_ap.x = ptr[i * 7];
                        tuple_ap.y = ptr[i * 7 + 1];
                        tuple_ap.size = ptr[i * 7 + 2];
                        tuple_ap.angle = ptr[i * 7 + 3];
                        tuple_ap.response = ptr[i * 7 + 4];
                        tuple_ap.octave = ptr[i * 7 + 5];
                        tuple_ap.class_id = ptr[i * 7 + 6];
                    }
                }

                return ap;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(num * 7);

            Converters.copyMatToArray<float>(this, buff, num * 7);

            Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, num);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                ref readonly Vec7f vec_span = ref buffSpan[i];
                tuple_ap.x = vec_span.Item1;
                tuple_ap.y = vec_span.Item2;
                tuple_ap.size = vec_span.Item3;
                tuple_ap.angle = vec_span.Item4;
                tuple_ap.response = vec_span.Item5;
                tuple_ap.octave = vec_span.Item6;
                tuple_ap.class_id = vec_span.Item7;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[num * 7];

            Converters.copyMatToArray<float>(this, buff, num * 7);
            for (int i = 0; i < ap.Length; i++)
            {
                ref var tuple_ap = ref ap[i];
                tuple_ap.x = buff[i * 7];
                tuple_ap.y = buff[i * 7 + 1];
                tuple_ap.size = buff[i * 7 + 2];
                tuple_ap.angle = buff[i * 7 + 3];
                tuple_ap.response = buff[i * 7 + 4];
                tuple_ap.octave = buff[i * 7 + 5];
                tuple_ap.class_id = buff[i * 7 + 6];
            }
#endif

            return ap;
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        public Span<Vec7f> AsVec7fSpan()
        {
            return AsSpan<Vec7f>();
        }

#endif

    }
}
