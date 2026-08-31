using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;

#if UNITY_6000_0_OR_NEWER
using Unity.Collections;
#endif

#if NET_STANDARD_2_1
using System.Buffers;
#endif

namespace OpenCVForUnity.UtilsModule
{
    public partial class Converters
    {

        /// <summary>
        /// String vector separator character (Unit Separator)
        /// </summary>
        private const char STRING_VECTOR_SEPARATOR = (char)0x1F;

        internal static void copyMatToArray<T>(Mat mat, T[] array, int length) where T : unmanaged
        {
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyFromMat<T>(mat, array, length);
            }
            else
            {
                mat.get<T>(0, 0, array, length);
            }
        }

        internal static void copyArrayToMat<T>(T[] array, Mat mat, int length) where T : unmanaged
        {
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyToMat<T>(array, mat, length);
            }
            else
            {
                mat.put<T>(0, 0, array, length);
            }
        }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        internal static void copyArrayToMat<T>(ReadOnlySpan<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyToMat<T>(array, mat, length);
            }
            else
            {
                mat.put<T>(0, 0, array, length);
            }
        }

#endif

#if UNITY_6000_0_OR_NEWER && NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        internal static void copyMatToNativeArray<T>(Mat mat, NativeArray<T> array, int length) where T : unmanaged
        {
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyFromMat<T>(mat, array, length);
            }
            else
            {
                mat.get<T>(0, 0, array, length);
            }
        }

        internal static void copyNativeArrayToMat<T>(NativeArray<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyToMat<T>(array, mat, length);
            }
            else
            {
                mat.put<T>(0, 0, array, length);
            }
        }

#endif

        public static Mat vector_Point_to_Mat(List<Point> pts)
        {
            return vector_Point_to_Mat(pts, CvType.CV_32SC2);
        }

        public static Mat vector_Point2f_to_Mat(List<Point> pts)
        {
            return vector_Point_to_Mat(pts, CvType.CV_32FC2);
        }

        public static Mat vector_Point2d_to_Mat(List<Point> pts)
        {
            return vector_Point_to_Mat(pts, CvType.CV_64FC2);
        }

        public static Mat vector_Point_to_Mat(List<Point> pts, int type)
        {
            Mat res;
            int count = (pts != null) ? pts.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, type);
                List_Point_to_Mat(pts, res, count, type);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static Mat vector_Point3_to_Mat(List<Point3> pts)
        {
            return vector_Point3_to_Mat(pts, CvType.CV_32SC3);
        }

        public static Mat vector_Point3i_to_Mat(List<Point3> pts)
        {
            return vector_Point3_to_Mat(pts, CvType.CV_32SC3);
        }

        public static Mat vector_Point3f_to_Mat(List<Point3> pts)
        {
            return vector_Point3_to_Mat(pts, CvType.CV_32FC3);
        }

        public static Mat vector_Point3d_to_Mat(List<Point3> pts)
        {
            return vector_Point3_to_Mat(pts, CvType.CV_64FC3);
        }

        public static Mat vector_Point3_to_Mat(List<Point3> pts, int type)
        {
            Mat res;
            int count = (pts != null) ? pts.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, type);
                List_Point3_to_Mat(pts, res, count, type);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_Point(Mat m, List<Point> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point(m, pts, m.type());
        }
        public static void Mat_to_vector_Point2f(Mat m, List<Point> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point(m, pts, m.type());
        }

        public static void Mat_to_vector_Point2d(Mat m, List<Point> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point(m, pts, m.type());
        }

        public static void Mat_to_vector_Point(Mat m, List<Point> pts, int type)
        {

            if (pts == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (m.cols() != 1)
                throw new CvException("Input Mat should have one column\n" + m);

            pts.Clear();
            for (int i = 0; i < count; i++)
            {
                pts.Add(new Point());
            }
            Mat_to_List_Point(m, pts, count, type);
        }

        public static void Mat_to_vector_Point3(Mat m, List<Point3> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point3(m, pts, m.type());
        }
        public static void Mat_to_vector_Point3i(Mat m, List<Point3> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point3(m, pts, m.type());
        }

        public static void Mat_to_vector_Point3f(Mat m, List<Point3> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point3(m, pts, m.type());
        }

        public static void Mat_to_vector_Point3d(Mat m, List<Point3> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            Mat_to_vector_Point3(m, pts, m.type());
        }

        public static void Mat_to_vector_Point3(Mat m, List<Point3> pts, int type)
        {

            if (pts == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (m.cols() != 1)
                throw new CvException("Input Mat should have one column\n" + m);

            pts.Clear();
            for (int i = 0; i < count; i++)
            {
                pts.Add(new Point3());
            }
            Mat_to_List_Point3(m, pts, count, type);
        }

        public static Mat vector_Mat_to_Mat(List<Mat> mats)
        {
            Mat res;
            int count = (mats != null) ? mats.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_32SC2);
                Converters.List_Mat_to_Mat<Mat>(mats, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_Mat(Mat m, List<Mat> mats)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (mats == null)
                throw new CvException("mats == null");
            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            mats.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        mats.Add(new Mat(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                mats.Add(new Mat(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                mats.Add(new Mat(new IntPtr(addr)));
            }
#endif
        }

        public static Mat vector_float_to_Mat(List<float> fs)
        {
            Mat res;
            int count = (fs != null) ? fs.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_32FC1);
                List_float_to_Mat(fs, res, count, CvType.CV_32FC1);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_float(Mat m, List<float> fs)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (fs == null)
                throw new CvException("fs == null");
            int count = m.rows();
            if (CvType.CV_32FC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32FC1 != m.type() ||  m.cols()!=1\n" + m);

            fs.Clear();
            for (int i = 0; i < count; i++)
            {
                fs.Add(0);
            }
            Converters.Mat_to_List_float(m, fs, count, CvType.CV_32FC1);
        }

        public static Mat vector_uchar_to_Mat(List<byte> bs)
        {
            Mat res;
            int count = (bs != null) ? bs.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_8UC1);
                List_uchar_to_Mat(bs, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_uchar(Mat m, List<byte> us)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (us == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (CvType.CV_8UC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_8UC1 != m.type() ||  m.cols()!=1\n" + m);

            us.Clear();
            for (int i = 0; i < count; i++)
            {
                us.Add(0);
            }
            Converters.Mat_to_List_uchar(m, us, count);
        }

        public static Mat vector_char_to_Mat(List<sbyte> bs)
        {
            Mat res;
            int count = (bs != null) ? bs.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_8SC1);
                List_char_to_Mat(bs, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static Mat vector_int_to_Mat(List<int> _is)
        {
            Mat res;
            int count = (_is != null) ? _is.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_32SC1);
                List_int_to_Mat(_is, res, count, CvType.CV_32SC1);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_int(Mat m, List<int> _is)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (_is == null)
                throw new CvException("is == null");
            int count = m.rows();
            if (CvType.CV_32SC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC1 != m.type() ||  m.cols()!=1\n" + m);

            _is.Clear();
            for (int i = 0; i < count; i++)
            {
                _is.Add(0);
            }
            Converters.Mat_to_List_int(m, _is, count, CvType.CV_32SC1);
        }

        public static void Mat_to_vector_char(Mat m, List<sbyte> bs)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (bs == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (CvType.CV_8SC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_8SC1 != m.type() ||  m.cols()!=1\n" + m);

            bs.Clear();
            for (int i = 0; i < count; i++)
            {
                bs.Add(0);
            }
            Converters.Mat_to_List_char(m, bs, count);
        }

        public static Mat vector_Rect_to_Mat(List<Rect> rs)
        {
            Mat res;
            int count = (rs != null) ? rs.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_32SC4);
                List_Rect_to_Mat(rs, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_Rect(Mat m, List<Rect> rs)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (rs == null)
                throw new CvException("rs == null");
            int count = m.rows();
            if (CvType.CV_32SC4 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC4 != m.type() ||  m.rows()!=1\n" + m);

            rs.Clear();
            for (int i = 0; i < count; i++)
            {
                rs.Add(new Rect());
            }
            Converters.Mat_to_List_Rect(m, rs, count);
        }

        public static Mat vector_KeyPoint_to_Mat(List<KeyPoint> kps)
        {
            Mat res;
            int count = (kps != null) ? kps.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_64FC(7));
                List_KeyPoint_to_Mat(kps, res, count, CvType.CV_64FC(7));
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_KeyPoint(Mat m, List<KeyPoint> kps)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (kps == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (CvType.CV_64FC(7) != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_64FC(7) != m.type() ||  m.cols()!=1\n" + m);

            kps.Clear();
            for (int i = 0; i < count; i++)
            {
                kps.Add(new KeyPoint());
            }
            Converters.Mat_to_List_KeyPoint(m, kps, count, CvType.CV_64FC(7));
        }

        public static Mat vector_vector_Point_to_Mat(List<MatOfPoint> pts)
        {
            Mat res;
            int lCount = (pts != null) ? pts.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfPoint>(pts, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_vector_Point(Mat m, List<MatOfPoint> pts)
        {

            if (m != null)
                m.ThrowIfDisposed();

            if (pts == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            pts.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        pts.Add(MatOfPoint.fromNativeAddr(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                pts.Add(MatOfPoint.fromNativeAddr(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                pts.Add(MatOfPoint.fromNativeAddr(new IntPtr(addr)));
            }
#endif

        }

        // vector_vector_Point2f
        public static void Mat_to_vector_vector_Point2f(Mat m, List<MatOfPoint2f> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (pts == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            pts.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        pts.Add(MatOfPoint2f.fromNativeAddr(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                pts.Add(MatOfPoint2f.fromNativeAddr(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                pts.Add(MatOfPoint2f.fromNativeAddr(new IntPtr(addr)));
            }
#endif
        }

        // vector_vector_Point2f
        public static Mat vector_vector_Point2f_to_Mat(List<MatOfPoint2f> pts)
        {
            Mat res;
            int lCount = (pts != null) ? pts.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfPoint2f>(pts, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        // vector_vector_Point3f
        public static void Mat_to_vector_vector_Point3f(Mat m, List<MatOfPoint3f> pts)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (pts == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            pts.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        pts.Add(MatOfPoint3f.fromNativeAddr(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                pts.Add(MatOfPoint3f.fromNativeAddr(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                pts.Add(MatOfPoint3f.fromNativeAddr(new IntPtr(addr)));
            }
#endif
        }

        // vector_vector_Point3f
        public static Mat vector_vector_Point3f_to_Mat(List<MatOfPoint3f> pts)
        {
            Mat res;
            int lCount = (pts != null) ? pts.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfPoint3f>(pts, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        // vector_vector_KeyPoint
        public static Mat vector_vector_KeyPoint_to_Mat(List<MatOfKeyPoint> kps)
        {
            Mat res;
            int lCount = (kps != null) ? kps.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfKeyPoint>(kps, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_vector_KeyPoint(Mat m, List<MatOfKeyPoint> kps)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (kps == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            kps.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        kps.Add(MatOfKeyPoint.fromNativeAddr(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                kps.Add(MatOfKeyPoint.fromNativeAddr(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                kps.Add(MatOfKeyPoint.fromNativeAddr(new IntPtr(addr)));
            }
#endif
        }

        public static Mat vector_double_to_Mat(List<double> ds)
        {
            Mat res;
            int count = (ds != null) ? ds.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_64FC1);
                List_double_to_Mat(ds, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_double(Mat m, List<double> ds)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (ds == null)
                throw new CvException("ds == null");
            int count = m.rows();
            if (CvType.CV_64FC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_64FC1 != m.type() ||  m.cols()!=1\n" + m);

            ds.Clear();
            for (int i = 0; i < count; i++)
            {
                ds.Add(0);
            }
            Converters.Mat_to_List_double(m, ds, count);
        }

        public static Mat vector_DMatch_to_Mat(List<DMatch> matches)
        {
            Mat res;
            int count = (matches != null) ? matches.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_64FC4);
                List_DMatch_to_Mat(matches, res, count, CvType.CV_64FC4);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_DMatch(Mat m, List<DMatch> matches)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (matches == null)
                throw new CvException("Output List can't be null");
            int count = m.rows();
            if (CvType.CV_64FC4 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_64FC4 != m.type() ||  m.cols()!=1\n" + m);

            matches.Clear();
            for (int i = 0; i < count; i++)
            {
                matches.Add(new DMatch());
            }
            Converters.Mat_to_List_DMatch(m, matches, count, CvType.CV_64FC4);
        }

        // vector_vector_DMatch
        public static Mat vector_vector_DMatch_to_Mat(List<MatOfDMatch> lvdm)
        {
            Mat res;
            int lCount = (lvdm != null) ? lvdm.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfDMatch>(lvdm, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_vector_DMatch(Mat m, List<MatOfDMatch> lvdm)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (lvdm == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            int count = m.rows();
            if (CvType.CV_32SC2 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC2 != m.type() ||  m.cols()!=1\n" + m);

            lvdm.Clear();

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (m.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)m.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (((long)ptr[i * 2]) << 32) | (((long)ptr[i * 2 + 1]) & 0xffffffffL);
                        lvdm.Add(MatOfDMatch.fromNativeAddr(new IntPtr(addr)));
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);
            copyMatToArray<int>(m, buff, count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref readonly Vec2i vec = ref buffSpan[i];
                long addr = (((long)vec.Item1) << 32) | (((long)vec.Item2) & 0xffffffffL);
                lvdm.Add(MatOfDMatch.fromNativeAddr(new IntPtr(addr)));
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            copyMatToArray<int>(m, buff, count * 2);

            for (int i = 0; i < count; i++)
            {
                long addr = (((long)buff[i * 2]) << 32) | (((long)buff[i * 2 + 1]) & 0xffffffffL);
                lvdm.Add(MatOfDMatch.fromNativeAddr(new IntPtr(addr)));
            }
#endif
        }

        // vector_vector_char
        public static Mat vector_vector_char_to_Mat(List<MatOfByte> lvb)
        {
            Mat res;
            int lCount = (lvb != null) ? lvb.Count : 0;
            if (lCount > 0)
            {
                res = new Mat(lCount, 1, CvType.CV_32SC2);
                List_Mat_to_Mat<MatOfByte>(lvb, res, lCount);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_vector_char(Mat m, List<List<byte>> llb)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (llb == null)
                throw new CvException("Output List can't be null");

            if (m == null)
                throw new CvException("Input Mat can't be null");

            List<Mat> mats = new List<Mat>(m.rows());
            Mat_to_vector_Mat(m, mats);
            foreach (Mat mi in mats)
            {
                List<byte> lb = new List<byte>();
                Mat_to_vector_uchar(mi, lb);
                llb.Add(lb);
                mi.release();
            }
            mats.Clear();
        }

        public static Mat vector_RotatedRect_to_Mat(List<RotatedRect> rs)
        {
            Mat res;
            int count = (rs != null) ? rs.Count : 0;
            if (count > 0)
            {
                res = new Mat(count, 1, CvType.CV_32FC(5));
                List_RotatedRect_to_Mat(rs, res, count);
            }
            else
            {
                res = new Mat();
            }
            return res;
        }

        public static void Mat_to_vector_RotatedRect(Mat m, List<RotatedRect> rs)
        {
            if (rs == null)
                throw new CvException("rs == null");
            int count = m.rows();
            if (CvType.CV_32FC(5) != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32FC5 != m.type() ||  m.rows()!=1\n" + m);

            rs.Clear();
            for (int i = 0; i < count; i++)
            {
                rs.Add(new RotatedRect());
            }
            Converters.Mat_to_List_RotatedRect(m, rs, count);
        }

        public static Mat vector_String_to_Mat(List<string> strings)
        {
            Mat res;

            int count = (strings != null) ? strings.Count : 0;
            if (count > 0)
            {
#if NET_STANDARD_2_1
                int[] strLen = ArrayPool<int>.Shared.Rent(count);

                int concatStrLen = 0;
                for (int i = 0; i < count; i++)
                {
                    strLen[i] = strings[i]?.Length ?? 0; // Use 0 if null
                    concatStrLen += strLen[i] + 1; // Add separator at the end of each element
                }

                char[] concatStr = ArrayPool<char>.Shared.Rent(concatStrLen);

                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    string str = strings[i];
                    if (str != null)
                    {
                        str.CopyTo(0, concatStr, index, strLen[i]);
                    }
                    index += strLen[i];
                    concatStr[index] = STRING_VECTOR_SEPARATOR; // Separate with separator character
                    index++;
                }

                byte[] buff = ArrayPool<byte>.Shared.Rent(concatStrLen * 3); // UTF-8 maximum 3 bytes per character

                int length = System.Text.Encoding.UTF8.GetBytes(
                    new ReadOnlySpan<char>(concatStr, 0, concatStrLen),
                    buff
                );

                res = new Mat(1, length, CvType.CV_8UC1);
                res.put(0, 0, buff, length);

                ArrayPool<byte>.Shared.Return(buff);
                ArrayPool<char>.Shared.Return(concatStr);
                ArrayPool<int>.Shared.Return(strLen);
#else
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    string str = strings[i];
                    if (str != null)
                    {
                        sb.Append(str);
                    }
                    sb.Append(STRING_VECTOR_SEPARATOR);
                }

                byte[] buff = System.Text.Encoding.UTF8.GetBytes(sb.ToString());

                res = new Mat(1, buff.Length, CvType.CV_8UC1);
                res.put(0, 0, buff);
#endif
            }
            else
            {
                res = new Mat();
            }

            return res;
        }

        public static void Mat_to_vector_String(Mat m, List<string> strings)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (strings == null)
                throw new CvException("strings == null");

            int count = m.cols();
            if (CvType.CV_8UC1 != m.type() || m.rows() != 1)
                throw new CvException(
                    "CvType.CV_8UC1 != m.type() ||  m.rows()!=1\n" + m);

            strings.Clear();

            if (count > 0)
            {
#if NET_STANDARD_2_1
                char[] chars = ArrayPool<char>.Shared.Rent(count);
                int length = 0;

#if !OPENCV_DONT_USE_UNSAFE_CODE
                if (m.isContinuous())
                {
                    unsafe
                    {
                        Span<byte> buff = new Span<byte>((void*)m.dataAddr(), count);
                        length = System.Text.Encoding.UTF8.GetChars(buff, chars);
                    }
                }
                else
                {
                    byte[] buff = ArrayPool<byte>.Shared.Rent(count);
                    m.get(0, 0, buff, count);
                    length = System.Text.Encoding.UTF8.GetChars(new Span<byte>(buff, 0, count), chars);
                    ArrayPool<byte>.Shared.Return(buff);
                }
#else
                byte[] buff = ArrayPool<byte>.Shared.Rent(count);
                m.get(0, 0, buff, count);
                length = System.Text.Encoding.UTF8.GetChars(new Span<byte>(buff, 0, count), chars);
                ArrayPool<byte>.Shared.Return(buff);
#endif

                Span<char> charsSpan = new Span<char>(chars, 0, length);

                while (true)
                {
                    int index = MemoryExtensions.IndexOf<char>(charsSpan, STRING_VECTOR_SEPARATOR);
                    if (index < 0)
                        break;

                    strings.Add(charsSpan.Slice(0, index).ToString());

                    charsSpan = charsSpan.Slice(index + 1);
                }

                ArrayPool<char>.Shared.Return(chars);
#else
                byte[] buff = new byte[count];
                m.get(0, 0, buff);

                string str = System.Text.Encoding.UTF8.GetString(buff);

                // Split by separator character and exclude the last empty string
                string[] splitResult = str.Split(STRING_VECTOR_SEPARATOR);
                for (int i = 0; i < splitResult.Length - 1; i++)
                {
                    strings.Add(splitResult[i]);
                }
#endif
            }
        }

        public static Mat vector_string_to_Mat(List<string> strings)
        {
            return vector_String_to_Mat(strings);
        }

        public static void Mat_to_vector_string(Mat m, List<string> strings)
        {
            Mat_to_vector_String(m, strings);
        }


        internal static void List_Point_to_Mat(List<Point> lp, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lp == null || mat == null || count < 0 || count > lp.Count || mat.total() * mat.channels() < count * 2)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC2 && type != CvType.CV_32FC2 && type != CvType.CV_64FC2)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC2)
                    {
                        int* ptr = (int*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            ptr[i * 2 + 0] = (int)p.x;
                            ptr[i * 2 + 1] = (int)p.y;
                        }
                    }
                    else if (type == CvType.CV_32FC2)
                    {
                        float* ptr = (float*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            ptr[i * 2 + 0] = (float)p.x;
                            ptr[i * 2 + 1] = (float)p.y;
                        }
                    }
                    else if (type == CvType.CV_64FC2)
                    {
                        double* ptr = (double*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            ptr[i * 2 + 0] = p.x;
                            ptr[i * 2 + 1] = p.y;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC2, CV_32F or CV_64FC2");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC2)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 2);

                Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2i vec = ref buffSpan[i];
                    Point p = lp[i];
                    vec.Item1 = (int)p.x;
                    vec.Item2 = (int)p.y;
                }

                copyArrayToMat<int>(buff, mat, count * 2);

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 2);

                Span<Vec2f> buffSpan = MemoryMarshal.Cast<float, Vec2f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2f vec = ref buffSpan[i];
                    Point p = lp[i];
                    vec.Item1 = (float)p.x;
                    vec.Item2 = (float)p.y;
                }

                copyArrayToMat<float>(buff, mat, count * 2);

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 2);

                Span<Vec2d> buffSpan = MemoryMarshal.Cast<double, Vec2d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2d vec = ref buffSpan[i];
                    Point p = lp[i];
                    vec.Item1 = p.x;
                    vec.Item2 = p.y;
                }

                copyArrayToMat<double>(buff, mat, count * 2);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC2, CV_32FC2 or CV_64FC2");
            }
#else
            if (type == CvType.CV_32SC2)
            {

                int[] buff = new int[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    buff[i * 2 + 0] = (int)p.x;
                    buff[i * 2 + 1] = (int)p.y;
                }
                copyArrayToMat<int>(buff, mat, count * 2);

            }
            else if (type == CvType.CV_32FC2)
            {


                float[] buff = new float[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    buff[i * 2 + 0] = (float)p.x;
                    buff[i * 2 + 1] = (float)p.y;
                }
                copyArrayToMat<float>(buff, mat, count * 2);

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = new double[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    buff[i * 2 + 0] = p.x;
                    buff[i * 2 + 1] = p.y;
                }
                copyArrayToMat<double>(buff, mat, count * 2);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC2, CV_32FC2 or CV_64FC2");
            }
#endif
        }

        internal static void Array_Point_to_Mat(Point[] ap, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ap == null || mat == null || count < 0 || count > ap.Length || mat.total() * mat.channels() < count * 2)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC2 && type != CvType.CV_32FC2 && type != CvType.CV_64FC2)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC2)
                    {
                        int* ptr = (int*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            ptr[i * 2 + 0] = (int)p.x;
                            ptr[i * 2 + 1] = (int)p.y;
                        }
                    }
                    else if (type == CvType.CV_32FC2)
                    {
                        float* ptr = (float*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            ptr[i * 2 + 0] = (float)p.x;
                            ptr[i * 2 + 1] = (float)p.y;
                        }
                    }
                    else if (type == CvType.CV_64FC2)
                    {
                        double* ptr = (double*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            ptr[i * 2 + 0] = p.x;
                            ptr[i * 2 + 1] = p.y;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC2, CV_32F or CV_64FC2");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC2)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 2);

                Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2i vec = ref buffSpan[i];
                    Point p = ap[i];
                    vec.Item1 = (int)p.x;
                    vec.Item2 = (int)p.y;
                }

                copyArrayToMat<int>(buff, mat, count * 2);

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 2);

                Span<Vec2f> buffSpan = MemoryMarshal.Cast<float, Vec2f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2f vec = ref buffSpan[i];
                    Point p = ap[i];
                    vec.Item1 = (float)p.x;
                    vec.Item2 = (float)p.y;
                }

                copyArrayToMat<float>(buff, mat, count * 2);

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 2);

                Span<Vec2d> buffSpan = MemoryMarshal.Cast<double, Vec2d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec2d vec = ref buffSpan[i];
                    Point p = ap[i];
                    vec.Item1 = p.x;
                    vec.Item2 = p.y;
                }

                copyArrayToMat<double>(buff, mat, count * 2);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC2, CV_32FC2 or CV_64FC2");
            }
#else
            if (type == CvType.CV_32SC2)
            {

                int[] buff = new int[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    buff[i * 2 + 0] = (int)p.x;
                    buff[i * 2 + 1] = (int)p.y;
                }
                copyArrayToMat<int>(buff, mat, count * 2);

            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = new float[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    buff[i * 2 + 0] = (float)p.x;
                    buff[i * 2 + 1] = (float)p.y;
                }
                copyArrayToMat<float>(buff, mat, count * 2);

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = new double[count * 2];

                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    buff[i * 2 + 0] = p.x;
                    buff[i * 2 + 1] = p.y;
                }
                copyArrayToMat<double>(buff, mat, count * 2);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC2, CV_32FC2 or CV_64FC2");
            }
#endif
        }

        internal static void Mat_to_List_Point(Mat mat, List<Point> lp, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lp == null || mat == null || count < 0 || count > lp.Count || mat.total() * mat.channels() < count * 2)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC2 && type != CvType.CV_32FC2 && type != CvType.CV_64FC2)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC2)
                    {
                        int* ptr = (int*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }

                    }
                    else if (type == CvType.CV_32FC2)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }
                    }
                    else if (type == CvType.CV_64FC2)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = lp[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC2)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count * 2);

                copyMatToArray<int>(mat, buff, count * 2);

                Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = lp[i];
                    ref readonly Vec2i vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<int>.Shared.Return(buff);

            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 2);

                copyMatToArray<float>(mat, buff, count * 2);

                Span<Vec2f> buffSpan = MemoryMarshal.Cast<float, Vec2f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = lp[i];
                    ref readonly Vec2f vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 2);

                copyMatToArray<double>(mat, buff, count * 2);

                Span<Vec2d> buffSpan = MemoryMarshal.Cast<double, Vec2d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = lp[i];
                    ref readonly Vec2d vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
            }
#else
            if (type == CvType.CV_32SC2)
            {

                int[] buff = new int[count * 2];

                copyMatToArray<int>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = new float[count * 2];

                copyMatToArray<float>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = new double[count * 2];

                copyMatToArray<double>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = lp[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
            }
#endif
        }

        internal static void Mat_to_Array_Point(Mat mat, Point[] ap, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ap == null || mat == null || count < 0 || count > ap.Length || mat.total() * mat.channels() < count * 2)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC2 && type != CvType.CV_32FC2 && type != CvType.CV_64FC2)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC2)
                    {
                        int* ptr = (int*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }

                    }
                    else if (type == CvType.CV_32FC2)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }
                    }
                    else if (type == CvType.CV_64FC2)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point p = ap[i];
                            p.x = ptr[i * 2];
                            p.y = ptr[i * 2 + 1];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC2)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count * 2);

                copyMatToArray<int>(mat, buff, count * 2);

                Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = ap[i];
                    ref readonly Vec2i vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<int>.Shared.Return(buff);

            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 2);

                copyMatToArray<float>(mat, buff, count * 2);

                Span<Vec2f> buffSpan = MemoryMarshal.Cast<float, Vec2f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = ap[i];
                    ref readonly Vec2f vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 2);

                copyMatToArray<double>(mat, buff, count * 2);

                Span<Vec2d> buffSpan = MemoryMarshal.Cast<double, Vec2d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point p = ap[i];
                    ref readonly Vec2d vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
            }
#else
            if (type == CvType.CV_32SC2)
            {

                int[] buff = new int[count * 2];

                copyMatToArray<int>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else if (type == CvType.CV_32FC2)
            {

                float[] buff = new float[count * 2];

                copyMatToArray<float>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else if (type == CvType.CV_64FC2)
            {

                double[] buff = new double[count * 2];

                copyMatToArray<double>(mat, buff, count * 2);
                for (int i = 0; i < count; i++)
                {
                    Point p = ap[i];
                    p.x = buff[i * 2];
                    p.y = buff[i * 2 + 1];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC2, CV_32FC2 or CV_64FC2 type\n" + mat);
            }
#endif
        }

        internal static void List_Point3_to_Mat(List<Point3> lp3, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lp3 == null || mat == null || count < 0 || count > lp3.Count || mat.total() * mat.channels() < count * 3)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC3 && type != CvType.CV_32FC3 && type != CvType.CV_64FC3)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC3)
                    {
                        int* ptr = (int*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            ptr[i * 3] = (int)p.x;
                            ptr[i * 3 + 1] = (int)p.y;
                            ptr[i * 3 + 2] = (int)p.z;
                        }
                    }
                    else if (type == CvType.CV_32FC3)
                    {
                        float* ptr = (float*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            ptr[i * 3] = (float)p.x;
                            ptr[i * 3 + 1] = (float)p.y;
                            ptr[i * 3 + 2] = (float)p.z;
                        }
                    }
                    else if (type == CvType.CV_64FC3)
                    {
                        double* ptr = (double*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            ptr[i * 3] = p.x;
                            ptr[i * 3 + 1] = p.y;
                            ptr[i * 3 + 2] = p.z;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
                    }

                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC3)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 3);

                Span<Vec3i> buffSpan = MemoryMarshal.Cast<int, Vec3i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3i vec = ref buffSpan[i];
                    Point3 p = lp3[i];
                    vec.Item1 = (int)p.x;
                    vec.Item2 = (int)p.y;
                    vec.Item3 = (int)p.z;
                }

                copyArrayToMat<int>(buff, mat, count * 3);

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 3);

                Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3f vec = ref buffSpan[i];
                    Point3 p = lp3[i];
                    vec.Item1 = (float)p.x;
                    vec.Item2 = (float)p.y;
                    vec.Item3 = (float)p.z;
                }

                copyArrayToMat<float>(buff, mat, count * 3);

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 3);

                Span<Vec3d> buffSpan = MemoryMarshal.Cast<double, Vec3d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3d vec = ref buffSpan[i];
                    Point3 p = lp3[i];
                    vec.Item1 = p.x;
                    vec.Item2 = p.y;
                    vec.Item3 = p.z;
                }

                copyArrayToMat<double>(buff, mat, count * 3);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
            }
#else
            if (type == CvType.CV_32SC3)
            {

                int[] buff = new int[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    buff[i * 3 + 0] = (int)p.x;
                    buff[i * 3 + 1] = (int)p.y;
                    buff[i * 3 + 2] = (int)p.z;
                }
                copyArrayToMat<int>(buff, mat, count * 3);

            }
            else if (type == CvType.CV_32FC3)
            {


                float[] buff = new float[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    buff[i * 3 + 0] = (float)p.x;
                    buff[i * 3 + 1] = (float)p.y;
                    buff[i * 3 + 2] = (float)p.z;
                }
                copyArrayToMat<float>(buff, mat, count * 3);

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = new double[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    buff[i * 3 + 0] = p.x;
                    buff[i * 3 + 1] = p.y;
                    buff[i * 3 + 2] = p.z;
                }
                copyArrayToMat<double>(buff, mat, count * 3);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
            }
#endif
        }

        internal static void Array_Point3_to_Mat(Point3[] ap3, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ap3 == null || mat == null || count < 0 || count > ap3.Length || mat.total() * mat.channels() < count * 3)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC3 && type != CvType.CV_32FC3 && type != CvType.CV_64FC3)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC3)
                    {
                        int* ptr = (int*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            ptr[i * 3] = (int)p.x;
                            ptr[i * 3 + 1] = (int)p.y;
                            ptr[i * 3 + 2] = (int)p.z;
                        }
                    }
                    else if (type == CvType.CV_32FC3)
                    {
                        float* ptr = (float*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            ptr[i * 3] = (float)p.x;
                            ptr[i * 3 + 1] = (float)p.y;
                            ptr[i * 3 + 2] = (float)p.z;
                        }
                    }
                    else if (type == CvType.CV_64FC3)
                    {
                        double* ptr = (double*)mat.dataAddr();
                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            ptr[i * 3] = p.x;
                            ptr[i * 3 + 1] = p.y;
                            ptr[i * 3 + 2] = p.z;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
                    }

                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC3)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 3);

                Span<Vec3i> buffSpan = MemoryMarshal.Cast<int, Vec3i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3i vec = ref buffSpan[i];
                    Point3 p = ap3[i];
                    vec.Item1 = (int)p.x;
                    vec.Item2 = (int)p.y;
                    vec.Item3 = (int)p.z;
                }

                copyArrayToMat<int>(buff, mat, count * 3);

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 3);

                Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3f vec = ref buffSpan[i];
                    Point3 p = ap3[i];
                    vec.Item1 = (float)p.x;
                    vec.Item2 = (float)p.y;
                    vec.Item3 = (float)p.z;
                }

                copyArrayToMat<float>(buff, mat, count * 3);

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 3);

                Span<Vec3d> buffSpan = MemoryMarshal.Cast<double, Vec3d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec3d vec = ref buffSpan[i];
                    Point3 p = ap3[i];
                    vec.Item1 = p.x;
                    vec.Item2 = p.y;
                    vec.Item3 = p.z;
                }

                copyArrayToMat<double>(buff, mat, count * 3);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
            }
#else
            if (type == CvType.CV_32SC3)
            {

                int[] buff = new int[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    buff[i * 3 + 0] = (int)p.x;
                    buff[i * 3 + 1] = (int)p.y;
                    buff[i * 3 + 2] = (int)p.z;
                }
                copyArrayToMat<int>(buff, mat, count * 3);

            }
            else if (type == CvType.CV_32FC3)
            {


                float[] buff = new float[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    buff[i * 3 + 0] = (float)p.x;
                    buff[i * 3 + 1] = (float)p.y;
                    buff[i * 3 + 2] = (float)p.z;
                }
                copyArrayToMat<float>(buff, mat, count * 3);

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = new double[count * 3];

                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    buff[i * 3 + 0] = p.x;
                    buff[i * 3 + 1] = p.y;
                    buff[i * 3 + 2] = p.z;
                }
                copyArrayToMat<double>(buff, mat, count * 3);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC3, CV_32FC3 or CV_64FC3");
            }
#endif
        }

        internal static void Mat_to_List_Point3(Mat mat, List<Point3> lp3, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lp3 == null || mat == null || count < 0 || count > lp3.Count || mat.total() * mat.channels() < count * 3)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC3 && type != CvType.CV_32FC3 && type != CvType.CV_64FC3)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC3)
                    {
                        int* ptr = (int*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }

                    }
                    else if (type == CvType.CV_32FC3)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }
                    }
                    else if (type == CvType.CV_64FC3)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = lp3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC3)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 3);

                copyMatToArray<int>(mat, buff, count * 3);
                Span<Vec3i> buffSpan = MemoryMarshal.Cast<int, Vec3i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point3 p = lp3[i];
                    ref readonly Vec3i vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<int>.Shared.Return(buff);

            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 3);

                copyMatToArray<float>(mat, buff, count * 3);
                Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point3 p = lp3[i];
                    ref readonly Vec3f vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 3);

                copyMatToArray<double>(mat, buff, count * 3);
                Span<Vec3d> buffSpan = MemoryMarshal.Cast<double, Vec3d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref readonly Vec3d vec = ref buffSpan[i];
                    Point3 p = lp3[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
            }
#else
            if (type == CvType.CV_32SC3)
            {

                int[] buff = new int[count * 3];

                copyMatToArray<int>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = new float[count * 3];

                copyMatToArray<float>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = new double[count * 3];

                copyMatToArray<double>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = lp3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
            }
#endif
        }

        internal static void Mat_to_Array_Point3(Mat mat, Point3[] ap3, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ap3 == null || mat == null || count < 0 || count > ap3.Length || mat.total() * mat.channels() < count * 3)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32SC3 && type != CvType.CV_32FC3 && type != CvType.CV_64FC3)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32SC3)
                    {
                        int* ptr = (int*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }

                    }
                    else if (type == CvType.CV_32FC3)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }
                    }
                    else if (type == CvType.CV_64FC3)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            Point3 p = ap3[i];
                            p.x = ptr[i * 3];
                            p.y = ptr[i * 3 + 1];
                            p.z = ptr[i * 3 + 2];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32SC3)
            {

                int[] buff = ArrayPool<int>.Shared.Rent(count * 3);

                copyMatToArray<int>(mat, buff, count * 3);
                Span<Vec3i> buffSpan = MemoryMarshal.Cast<int, Vec3i>(buff.AsSpan<int>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref readonly Vec3i vec = ref buffSpan[i];
                    Point3 p = ap3[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<int>.Shared.Return(buff);

            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 3);

                copyMatToArray<float>(mat, buff, count * 3);
                Span<Vec3f> buffSpan = MemoryMarshal.Cast<float, Vec3f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref readonly Vec3f vec = ref buffSpan[i];
                    Point3 p = ap3[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = ArrayPool<double>.Shared.Rent(count * 3);

                copyMatToArray<double>(mat, buff, count * 3);
                Span<Vec3d> buffSpan = MemoryMarshal.Cast<double, Vec3d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    Point3 p = ap3[i];
                    ref readonly Vec3d vec = ref buffSpan[i];
                    p.x = vec.Item1;
                    p.y = vec.Item2;
                    p.z = vec.Item3;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
            }
#else
            if (type == CvType.CV_32SC3)
            {

                int[] buff = new int[count * 3];

                copyMatToArray<int>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else if (type == CvType.CV_32FC3)
            {

                float[] buff = new float[count * 3];

                copyMatToArray<float>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else if (type == CvType.CV_64FC3)
            {

                double[] buff = new double[count * 3];

                copyMatToArray<double>(mat, buff, count * 3);
                for (int i = 0; i < count; i++)
                {
                    Point3 p = ap3[i];
                    p.x = buff[i * 3];
                    p.y = buff[i * 3 + 1];
                    p.z = buff[i * 3 + 2];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC3, CV_32FC3 or CV_64FC3 type\n" + mat);
            }
#endif
        }

        internal static void List_DMatch_to_Mat(List<DMatch> ldm, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ldm == null || mat == null || count < 0 || count > ldm.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC4 && type != CvType.CV_64FC4)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC4)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = ldm[i];
                            ptr[i * 4] = m.queryIdx;
                            ptr[i * 4 + 1] = m.trainIdx;
                            ptr[i * 4 + 2] = m.imgIdx;
                            ptr[i * 4 + 3] = m.distance;
                        }

                    }
                    else if (type == CvType.CV_64FC4)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = ldm[i];
                            ptr[i * 4] = m.queryIdx;
                            ptr[i * 4 + 1] = m.trainIdx;
                            ptr[i * 4 + 2] = m.imgIdx;
                            ptr[i * 4 + 3] = m.distance;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC4)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);

                Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec4f vec = ref buffSpan[i];
                    DMatch m = ldm[i];
                    vec.Item1 = m.queryIdx;
                    vec.Item2 = m.trainIdx;
                    vec.Item3 = m.imgIdx;
                    vec.Item4 = m.distance;
                }

                copyArrayToMat<float>(buff, mat, count * 4);

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

                Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec4d vec = ref buffSpan[i];
                    DMatch m = ldm[i];
                    vec.Item1 = m.queryIdx;
                    vec.Item2 = m.trainIdx;
                    vec.Item3 = m.imgIdx;
                    vec.Item4 = m.distance;
                }

                copyArrayToMat<double>(buff, mat, count * 4);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
            }
#else
            if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[count * 4];
                for (int i = 0; i < count; i++)
                {
                    DMatch m = ldm[i];
                    buff[i * 4] = m.queryIdx;
                    buff[i * 4 + 1] = m.trainIdx;
                    buff[i * 4 + 2] = m.imgIdx;
                    buff[i * 4 + 3] = m.distance;
                }
                copyArrayToMat<float>(buff, mat, count * 4);
            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = new double[count * 4];

                for (int i = 0; i < count; i++)
                {
                    DMatch m = ldm[i];
                    buff[i * 4] = m.queryIdx;
                    buff[i * 4 + 1] = m.trainIdx;
                    buff[i * 4 + 2] = m.imgIdx;
                    buff[i * 4 + 3] = m.distance;
                }
                copyArrayToMat<double>(buff, mat, count * 4);
            }
            else
            {
                throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
            }
#endif
        }

        internal static void Array_DMatch_to_Mat(DMatch[] adm, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (adm == null || mat == null || count < 0 || count > adm.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC4 && type != CvType.CV_64FC4)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC4)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = adm[i];
                            ptr[i * 4] = m.queryIdx;
                            ptr[i * 4 + 1] = m.trainIdx;
                            ptr[i * 4 + 2] = m.imgIdx;
                            ptr[i * 4 + 3] = m.distance;
                        }

                    }
                    else if (type == CvType.CV_64FC4)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = adm[i];
                            ptr[i * 4] = m.queryIdx;
                            ptr[i * 4 + 1] = m.trainIdx;
                            ptr[i * 4 + 2] = m.imgIdx;
                            ptr[i * 4 + 3] = m.distance;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC4)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);

                Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec4f vec = ref buffSpan[i];
                    DMatch m = adm[i];
                    vec.Item1 = m.queryIdx;
                    vec.Item2 = m.trainIdx;
                    vec.Item3 = m.imgIdx;
                    vec.Item4 = m.distance;
                }

                copyArrayToMat<float>(buff, mat, count * 4);

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

                Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec4d vec = ref buffSpan[i];
                    DMatch m = adm[i];
                    vec.Item1 = m.queryIdx;
                    vec.Item2 = m.trainIdx;
                    vec.Item3 = m.imgIdx;
                    vec.Item4 = m.distance;
                }

                copyArrayToMat<double>(buff, mat, count * 4);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
            }
#else
            if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[count * 4];
                for (int i = 0; i < count; i++)
                {
                    DMatch m = adm[i];
                    buff[i * 4] = m.queryIdx;
                    buff[i * 4 + 1] = m.trainIdx;
                    buff[i * 4 + 2] = m.imgIdx;
                    buff[i * 4 + 3] = m.distance;
                }
                copyArrayToMat<float>(buff, mat, count * 4);
            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = new double[count * 4];

                for (int i = 0; i < count; i++)
                {
                    DMatch m = adm[i];
                    buff[i * 4] = m.queryIdx;
                    buff[i * 4 + 1] = m.trainIdx;
                    buff[i * 4 + 2] = m.imgIdx;
                    buff[i * 4 + 3] = m.distance;
                }
                copyArrayToMat<double>(buff, mat, count * 4);
            }
            else
            {
                throw new CvException("'type' can be CV_32SC4 or CV_64FC4");
            }
#endif
        }

        internal static void Mat_to_List_DMatch(Mat mat, List<DMatch> ldm, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (ldm == null || mat == null || count < 0 || count > ldm.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC4 && type != CvType.CV_64FC4)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC4)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = ldm[i];
                            m.queryIdx = (int)ptr[i * 4];
                            m.trainIdx = (int)ptr[i * 4 + 1];
                            m.imgIdx = (int)ptr[i * 4 + 2];
                            m.distance = (float)ptr[i * 4 + 3];
                        }

                    }
                    else if (type == CvType.CV_64FC4)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = ldm[i];
                            m.queryIdx = (int)ptr[i * 4];
                            m.trainIdx = (int)ptr[i * 4 + 1];
                            m.imgIdx = (int)ptr[i * 4 + 2];
                            m.distance = (float)ptr[i * 4 + 3];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC4)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);


                copyMatToArray<float>(mat, buff, count * 4);

                Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    DMatch m = ldm[i];
                    ref readonly Vec4f vec = ref buffSpan[i];
                    m.queryIdx = (int)vec.Item1;
                    m.trainIdx = (int)vec.Item2;
                    m.imgIdx = (int)vec.Item3;
                    m.distance = (float)vec.Item4;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

                copyMatToArray<double>(mat, buff, count * 4);

                Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    DMatch m = ldm[i];
                    ref readonly Vec4d vec = ref buffSpan[i];
                    m.queryIdx = (int)vec.Item1;
                    m.trainIdx = (int)vec.Item2;
                    m.imgIdx = (int)vec.Item3;
                    m.distance = (float)vec.Item4;
                }

                ArrayPool<double>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
            }
#else
            if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[4 * count];

                copyMatToArray<float>(mat, buff, count * 4);

                for (int i = 0; i < count; i++)
                {
                    DMatch m = ldm[i];
                    m.queryIdx = (int)buff[i * 4];
                    m.trainIdx = (int)buff[i * 4 + 1];
                    m.imgIdx = (int)buff[i * 4 + 2];
                    m.distance = (float)buff[i * 4 + 3];
                }

            }
            else if (type == CvType.CV_64FC4)
            {

                double[] buff = new double[4 * count];

                copyMatToArray<double>(mat, buff, count * 4);

                for (int i = 0; i < count; i++)
                {
                    DMatch m = ldm[i];
                    m.queryIdx = (int)buff[i * 4];
                    m.trainIdx = (int)buff[i * 4 + 1];
                    m.imgIdx = (int)buff[i * 4 + 2];
                    m.distance = (float)buff[i * 4 + 3];
                }
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
            }
#endif
        }

        internal static void Mat_to_Array_DMatch(Mat mat, DMatch[] adm, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (adm == null || mat == null || count < 0 || count > adm.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC4 && type != CvType.CV_64FC4)
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC4)
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = adm[i];
                            m.queryIdx = (int)ptr[i * 4];
                            m.trainIdx = (int)ptr[i * 4 + 1];
                            m.imgIdx = (int)ptr[i * 4 + 2];
                            m.distance = (float)ptr[i * 4 + 3];
                        }

                    }
                    else if (type == CvType.CV_64FC4)
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            DMatch m = adm[i];
                            m.queryIdx = (int)ptr[i * 4];
                            m.trainIdx = (int)ptr[i * 4 + 1];
                            m.imgIdx = (int)ptr[i * 4 + 2];
                            m.distance = (float)ptr[i * 4 + 3];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC4)
            {

                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);


                copyMatToArray<float>(mat, buff, count * 4);

                Span<Vec4f> buffSpan = MemoryMarshal.Cast<float, Vec4f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    DMatch m = adm[i];
                    ref readonly Vec4f vec = ref buffSpan[i];
                    m.queryIdx = (int)vec.Item1;
                    m.trainIdx = (int)vec.Item2;
                    m.imgIdx = (int)vec.Item3;
                    m.distance = (float)vec.Item4;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC4)
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

                copyMatToArray<double>(mat, buff, count * 4);

                Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    DMatch m = adm[i];
                    ref readonly Vec4d vec = ref buffSpan[i];
                    m.queryIdx = (int)vec.Item1;
                    m.trainIdx = (int)vec.Item2;
                    m.imgIdx = (int)vec.Item3;
                    m.distance = (float)vec.Item4;
                }

                ArrayPool<double>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
            }
#else
            if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[4 * count];

                copyMatToArray<float>(mat, buff, count * 4);

                for (int i = 0; i < count; i++)
                {
                    DMatch m = adm[i];
                    m.queryIdx = (int)buff[i * 4];
                    m.trainIdx = (int)buff[i * 4 + 1];
                    m.imgIdx = (int)buff[i * 4 + 2];
                    m.distance = (float)buff[i * 4 + 3];
                }

            }
            else if (type == CvType.CV_64FC4)
            {

                double[] buff = new double[4 * count];

                copyMatToArray<double>(mat, buff, count * 4);

                for (int i = 0; i < count; i++)
                {
                    DMatch m = adm[i];
                    m.queryIdx = (int)buff[i * 4];
                    m.trainIdx = (int)buff[i * 4 + 1];
                    m.imgIdx = (int)buff[i * 4 + 2];
                    m.distance = (float)buff[i * 4 + 3];
                }
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC4 or CV_64FC4 type\n" + mat);
            }
#endif
        }

        internal static void List_KeyPoint_to_Mat(List<KeyPoint> lkp, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lkp == null || mat == null || count < 0 || count > lkp.Count || mat.total() * mat.channels() < count * 7)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC(7) && type != CvType.CV_64FC(7))
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC(7))
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = lkp[i];
                            ptr[i * 7] = (float)kp.pt.x;
                            ptr[i * 7 + 1] = (float)kp.pt.y;
                            ptr[i * 7 + 2] = kp.size;
                            ptr[i * 7 + 3] = kp.angle;
                            ptr[i * 7 + 4] = kp.response;
                            ptr[i * 7 + 5] = kp.octave;
                            ptr[i * 7 + 6] = kp.class_id;
                        }

                    }
                    else if (type == CvType.CV_64FC(7))
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = lkp[i];
                            ptr[i * 7] = kp.pt.x;
                            ptr[i * 7 + 1] = kp.pt.y;
                            ptr[i * 7 + 2] = kp.size;
                            ptr[i * 7 + 3] = kp.angle;
                            ptr[i * 7 + 4] = kp.response;
                            ptr[i * 7 + 5] = kp.octave;
                            ptr[i * 7 + 6] = kp.class_id;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32FC7 or CV_64FC7.");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC(7))
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 7);

                Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec7f vec = ref buffSpan[i];
                    KeyPoint kp = lkp[i];
                    vec.Item1 = (float)kp.pt.x;
                    vec.Item2 = (float)kp.pt.y;
                    vec.Item3 = kp.size;
                    vec.Item4 = kp.angle;
                    vec.Item5 = kp.response;
                    vec.Item6 = kp.octave;
                    vec.Item7 = kp.class_id;
                }

                copyArrayToMat<float>(buff, mat, count * 7);

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC(7))
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 7);

                Span<Vec7d> buffSpan = MemoryMarshal.Cast<double, Vec7d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec7d vec = ref buffSpan[i];
                    KeyPoint kp = lkp[i];
                    vec.Item1 = kp.pt.x;
                    vec.Item2 = kp.pt.y;
                    vec.Item3 = kp.size;
                    vec.Item4 = kp.angle;
                    vec.Item5 = kp.response;
                    vec.Item6 = kp.octave;
                    vec.Item7 = kp.class_id;
                }

                copyArrayToMat<double>(buff, mat, count * 7);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32FC7 or CV_64FC7");
            }
#else
            if (type == CvType.CV_32FC(7))
            {
                float[] buff = new float[count * 7];

                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = lkp[i];
                    buff[i * 7] = (float)kp.pt.x;
                    buff[i * 7 + 1] = (float)kp.pt.y;
                    buff[i * 7 + 2] = kp.size;
                    buff[i * 7 + 3] = kp.angle;
                    buff[i * 7 + 4] = kp.response;
                    buff[i * 7 + 5] = kp.octave;
                    buff[i * 7 + 6] = kp.class_id;
                }
                copyArrayToMat<float>(buff, mat, count * 7);

            }
            else if (type == CvType.CV_64FC(7))
            {
                double[] buff = new double[count * 7];

                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = lkp[i];
                    buff[i * 7] = kp.pt.x;
                    buff[i * 7 + 1] = kp.pt.y;
                    buff[i * 7 + 2] = kp.size;
                    buff[i * 7 + 3] = kp.angle;
                    buff[i * 7 + 4] = kp.response;
                    buff[i * 7 + 5] = kp.octave;
                    buff[i * 7 + 6] = kp.class_id;
                }
                copyArrayToMat<double>(buff, mat, count * 7);

            }
            else
            {
                throw new CvException("'type' can be CV_32FC7 or CV_64FC7.");
            }
#endif
        }

        internal static void Array_KeyPoint_to_Mat(KeyPoint[] akp, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (akp == null || mat == null || count < 0 || count > akp.Length || mat.total() * mat.channels() < count * 7)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC(7) && type != CvType.CV_64FC(7))
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC(7))
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = akp[i];
                            ptr[i * 7] = (float)kp.pt.x;
                            ptr[i * 7 + 1] = (float)kp.pt.y;
                            ptr[i * 7 + 2] = kp.size;
                            ptr[i * 7 + 3] = kp.angle;
                            ptr[i * 7 + 4] = kp.response;
                            ptr[i * 7 + 5] = kp.octave;
                            ptr[i * 7 + 6] = kp.class_id;
                        }

                    }
                    else if (type == CvType.CV_64FC(7))
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = akp[i];
                            ptr[i * 7] = kp.pt.x;
                            ptr[i * 7 + 1] = kp.pt.y;
                            ptr[i * 7 + 2] = kp.size;
                            ptr[i * 7 + 3] = kp.angle;
                            ptr[i * 7 + 4] = kp.response;
                            ptr[i * 7 + 5] = kp.octave;
                            ptr[i * 7 + 6] = kp.class_id;
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32FC7 or CV_64FC7.");
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC(7))
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 7);

                Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec7f vec = ref buffSpan[i];
                    KeyPoint kp = akp[i];
                    vec.Item1 = (float)kp.pt.x;
                    vec.Item2 = (float)kp.pt.y;
                    vec.Item3 = kp.size;
                    vec.Item4 = kp.angle;
                    vec.Item5 = kp.response;
                    vec.Item6 = kp.octave;
                    vec.Item7 = kp.class_id;
                }

                copyArrayToMat<float>(buff, mat, count * 7);

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC(7))
            {
                double[] buff = ArrayPool<double>.Shared.Rent(count * 7);

                Span<Vec7d> buffSpan = MemoryMarshal.Cast<double, Vec7d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    ref Vec7d vec = ref buffSpan[i];
                    KeyPoint kp = akp[i];
                    vec.Item1 = kp.pt.x;
                    vec.Item2 = kp.pt.y;
                    vec.Item3 = kp.size;
                    vec.Item4 = kp.angle;
                    vec.Item5 = kp.response;
                    vec.Item6 = kp.octave;
                    vec.Item7 = kp.class_id;
                }

                copyArrayToMat<double>(buff, mat, count * 7);

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("'type' can be CV_32FC7 or CV_64FC7.");
            }
#else
            if (type == CvType.CV_32FC(7))
            {
                float[] buff = new float[count * 7];

                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = akp[i];
                    buff[i * 7] = (float)kp.pt.x;
                    buff[i * 7 + 1] = (float)kp.pt.y;
                    buff[i * 7 + 2] = kp.size;
                    buff[i * 7 + 3] = kp.angle;
                    buff[i * 7 + 4] = kp.response;
                    buff[i * 7 + 5] = kp.octave;
                    buff[i * 7 + 6] = kp.class_id;
                }
                copyArrayToMat<float>(buff, mat, count * 7);

            }
            else if (type == CvType.CV_64FC(7))
            {
                double[] buff = new double[count * 7];

                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = akp[i];
                    buff[i * 7] = kp.pt.x;
                    buff[i * 7 + 1] = kp.pt.y;
                    buff[i * 7 + 2] = kp.size;
                    buff[i * 7 + 3] = kp.angle;
                    buff[i * 7 + 4] = kp.response;
                    buff[i * 7 + 5] = kp.octave;
                    buff[i * 7 + 6] = kp.class_id;
                }
                copyArrayToMat<double>(buff, mat, count * 7);

            }
            else
            {
                throw new CvException("'type' can be CV_32FC7 or CV_64FC7.");
            }
#endif
        }

        internal static void Mat_to_List_KeyPoint(Mat mat, List<KeyPoint> lkp, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lkp == null || mat == null || count < 0 || count > lkp.Count || mat.total() * mat.channels() < count * 7)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC(7) && type != CvType.CV_64FC(7))
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC(7))
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = lkp[i];
                            kp.pt.x = ptr[i * 7];
                            kp.pt.y = ptr[i * 7 + 1];
                            kp.size = ptr[i * 7 + 2];
                            kp.angle = ptr[i * 7 + 3];
                            kp.response = ptr[i * 7 + 4];
                            kp.octave = (int)ptr[i * 7 + 5];
                            kp.class_id = (int)ptr[i * 7 + 6];
                        }
                    }
                    else if (type == CvType.CV_64FC(7))
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = lkp[i];
                            kp.pt.x = (float)ptr[i * 7];
                            kp.pt.y = (float)ptr[i * 7 + 1];
                            kp.size = (float)ptr[i * 7 + 2];
                            kp.angle = (float)ptr[i * 7 + 3];
                            kp.response = (float)ptr[i * 7 + 4];
                            kp.octave = (int)ptr[i * 7 + 5];
                            kp.class_id = (int)ptr[i * 7 + 6];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC(7))
            {

                float[] buff = ArrayPool<float>.Shared.Rent(7 * count);

                copyMatToArray<float>(mat, buff, count * 7);

                Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    KeyPoint kp = lkp[i];
                    ref readonly Vec7f vec = ref buffSpan[i];
                    kp.pt.x = vec.Item1;
                    kp.pt.y = vec.Item2;
                    kp.size = vec.Item3;
                    kp.angle = vec.Item4;
                    kp.response = vec.Item5;
                    kp.octave = (int)vec.Item6;
                    kp.class_id = (int)vec.Item7;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC(7))
            {

                double[] buff = ArrayPool<double>.Shared.Rent(7 * count);

                copyMatToArray<double>(mat, buff, count * 7);

                Span<Vec7d> buffSpan = MemoryMarshal.Cast<double, Vec7d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    KeyPoint kp = lkp[i];
                    ref readonly Vec7d vec = ref buffSpan[i];
                    kp.pt.x = (float)vec.Item1;
                    kp.pt.y = (float)vec.Item2;
                    kp.size = (float)vec.Item3;
                    kp.angle = (float)vec.Item4;
                    kp.response = (float)vec.Item5;
                    kp.octave = (int)vec.Item6;
                    kp.class_id = (int)vec.Item7;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
            }
#else
            if (type == CvType.CV_32FC(7))
            {

                float[] buff = new float[7 * count];

                copyMatToArray<float>(mat, buff, count * 7);
                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = lkp[i];
                    kp.pt.x = buff[i * 7];
                    kp.pt.y = buff[i * 7 + 1];
                    kp.size = buff[i * 7 + 2];
                    kp.angle = buff[i * 7 + 3];
                    kp.response = buff[i * 7 + 4];
                    kp.octave = (int)buff[i * 7 + 5];
                    kp.class_id = (int)buff[i * 7 + 6];
                }

            }
            else if (type == CvType.CV_64FC(7))
            {

                double[] buff = new double[7 * count];

                copyMatToArray<double>(mat, buff, count * 7);
                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = lkp[i];
                    kp.pt.x = (float)buff[i * 7];
                    kp.pt.y = (float)buff[i * 7 + 1];
                    kp.size = (float)buff[i * 7 + 2];
                    kp.angle = (float)buff[i * 7 + 3];
                    kp.response = (float)buff[i * 7 + 4];
                    kp.octave = (int)buff[i * 7 + 5];
                    kp.class_id = (int)buff[i * 7 + 6];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
            }
#endif
        }

        internal static void Mat_to_Array_KeyPoint(Mat mat, KeyPoint[] akp, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (akp == null || mat == null || count < 0 || count > akp.Length || mat.total() * mat.channels() < count * 7)
                throw new CvException("Invalid parameters");

            if (type != CvType.CV_32FC(7) && type != CvType.CV_64FC(7))
                throw new CvException("Unsupported type");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    if (type == CvType.CV_32FC(7))
                    {
                        float* ptr = (float*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = akp[i];
                            kp.pt.x = ptr[i * 7];
                            kp.pt.y = ptr[i * 7 + 1];
                            kp.size = ptr[i * 7 + 2];
                            kp.angle = ptr[i * 7 + 3];
                            kp.response = ptr[i * 7 + 4];
                            kp.octave = (int)ptr[i * 7 + 5];
                            kp.class_id = (int)ptr[i * 7 + 6];
                        }
                    }
                    else if (type == CvType.CV_64FC(7))
                    {
                        double* ptr = (double*)mat.dataAddr();

                        for (int i = 0; i < count; i++)
                        {
                            KeyPoint kp = akp[i];
                            kp.pt.x = (float)ptr[i * 7];
                            kp.pt.y = (float)ptr[i * 7 + 1];
                            kp.size = (float)ptr[i * 7 + 2];
                            kp.angle = (float)ptr[i * 7 + 3];
                            kp.response = (float)ptr[i * 7 + 4];
                            kp.octave = (int)ptr[i * 7 + 5];
                            kp.class_id = (int)ptr[i * 7 + 6];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            if (type == CvType.CV_32FC(7))
            {
                float[] buff = ArrayPool<float>.Shared.Rent(7 * count);

                copyMatToArray<float>(mat, buff, count * 7);

                Span<Vec7f> buffSpan = MemoryMarshal.Cast<float, Vec7f>(buff.AsSpan<float>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    KeyPoint kp = akp[i];
                    ref readonly Vec7f vec = ref buffSpan[i];
                    kp.pt.x = vec.Item1;
                    kp.pt.y = vec.Item2;
                    kp.size = vec.Item3;
                    kp.angle = vec.Item4;
                    kp.response = vec.Item5;
                    kp.octave = (int)vec.Item6;
                    kp.class_id = (int)vec.Item7;
                }

                ArrayPool<float>.Shared.Return(buff);

            }
            else if (type == CvType.CV_64FC(7))
            {

                double[] buff = ArrayPool<double>.Shared.Rent(7 * count);

                copyMatToArray<double>(mat, buff, count * 7);

                Span<Vec7d> buffSpan = MemoryMarshal.Cast<double, Vec7d>(buff.AsSpan<double>()).Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    KeyPoint kp = akp[i];
                    ref readonly Vec7d vec = ref buffSpan[i];
                    kp.pt.x = (float)vec.Item1;
                    kp.pt.y = (float)vec.Item2;
                    kp.size = (float)vec.Item3;
                    kp.angle = (float)vec.Item4;
                    kp.response = (float)vec.Item5;
                    kp.octave = (int)vec.Item6;
                    kp.class_id = (int)vec.Item7;
                }

                ArrayPool<double>.Shared.Return(buff);

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
            }
#else
            if (type == CvType.CV_32FC(7))
            {

                float[] buff = new float[7 * count];

                copyMatToArray<float>(mat, buff, count * 7);
                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = akp[i];
                    kp.pt.x = buff[i * 7];
                    kp.pt.y = buff[i * 7 + 1];
                    kp.size = buff[i * 7 + 2];
                    kp.angle = buff[i * 7 + 3];
                    kp.response = buff[i * 7 + 4];
                    kp.octave = (int)buff[i * 7 + 5];
                    kp.class_id = (int)buff[i * 7 + 6];
                }

            }
            else if (type == CvType.CV_64FC(7))
            {

                double[] buff = new double[7 * count];

                copyMatToArray<double>(mat, buff, count * 7);
                for (int i = 0; i < count; i++)
                {
                    KeyPoint kp = akp[i];
                    kp.pt.x = (float)buff[i * 7];
                    kp.pt.y = (float)buff[i * 7 + 1];
                    kp.size = (float)buff[i * 7 + 2];
                    kp.angle = (float)buff[i * 7 + 3];
                    kp.response = (float)buff[i * 7 + 4];
                    kp.octave = (int)buff[i * 7 + 5];
                    kp.class_id = (int)buff[i * 7 + 6];
                }

            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC7 or CV_64FC7 type\n" + mat);
            }
#endif
        }

        internal static void List_Rect_to_Mat(List<Rect> lr, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (lr == null || mat == null || count < 0 || count > lr.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect r = lr[i];
                        ptr[i * 4] = (int)r.x;
                        ptr[i * 4 + 1] = (int)r.y;
                        ptr[i * 4 + 2] = (int)r.width;
                        ptr[i * 4 + 3] = (int)r.height;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1

            int[] buff = ArrayPool<int>.Shared.Rent(4 * count);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4i vec = ref buffSpan[i];
                Rect r = lr[i];
                vec.Item1 = (int)r.x;
                vec.Item2 = (int)r.y;
                vec.Item3 = (int)r.width;
                vec.Item4 = (int)r.height;
            }

            copyArrayToMat<int>(buff, mat, count * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[4 * count];

            for (int i = 0; i < count; i++)
            {
                Rect r = lr[i];
                buff[i * 4] = (int)r.x;
                buff[i * 4 + 1] = (int)r.y;
                buff[i * 4 + 2] = (int)r.width;
                buff[i * 4 + 3] = (int)r.height;
            }
            copyArrayToMat<int>(buff, mat, count * 4);
#endif
        }

        internal static void Array_Rect_to_Mat(Rect[] ar, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (ar == null || mat == null || count < 0 || count > ar.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect r = ar[i];
                        ptr[i * 4] = (int)r.x;
                        ptr[i * 4 + 1] = (int)r.y;
                        ptr[i * 4 + 2] = (int)r.width;
                        ptr[i * 4 + 3] = (int)r.height;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1

            int[] buff = ArrayPool<int>.Shared.Rent(4 * count);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4i vec = ref buffSpan[i];
                Rect r = ar[i];
                vec.Item1 = (int)r.x;
                vec.Item2 = (int)r.y;
                vec.Item3 = (int)r.width;
                vec.Item4 = (int)r.height;
            }

            copyArrayToMat<int>(buff, mat, count * 4);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[4 * count];

            for (int i = 0; i < count; i++)
            {
                Rect r = ar[i];
                buff[i * 4] = (int)r.x;
                buff[i * 4 + 1] = (int)r.y;
                buff[i * 4 + 2] = (int)r.width;
                buff[i * 4 + 3] = (int)r.height;
            }
            copyArrayToMat<int>(buff, mat, count * 4);
#endif
        }

        internal static void Mat_to_List_Rect(Mat mat, List<Rect> lr, int count)
        {
#if OPENCV_DBGASSERT
            if (lr == null || mat == null || count < 0 || count > lr.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect r = lr[i];
                        r.x = ptr[i * 4];
                        r.y = ptr[i * 4 + 1];
                        r.width = ptr[i * 4 + 2];
                        r.height = ptr[i * 4 + 3];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(4 * count);

            copyMatToArray<int>(mat, buff, count * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                Rect r = lr[i];
                ref readonly Vec4i vec = ref buffSpan[i];
                r.x = vec.Item1;
                r.y = vec.Item2;
                r.width = vec.Item3;
                r.height = vec.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[4 * count];

            copyMatToArray<int>(mat, buff, count * 4);

            for (int i = 0; i < count; i++)
            {
                Rect r = lr[i];
                r.x = buff[i * 4];
                r.y = buff[i * 4 + 1];
                r.width = buff[i * 4 + 2];
                r.height = buff[i * 4 + 3];
            }
#endif
        }

        internal static void Mat_to_Array_Rect(Mat mat, Rect[] ar, int count)
        {
#if OPENCV_DBGASSERT
            if (ar == null || mat == null || count < 0 || count > ar.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect r = ar[i];
                        r.x = ptr[i * 4];
                        r.y = ptr[i * 4 + 1];
                        r.width = ptr[i * 4 + 2];
                        r.height = ptr[i * 4 + 3];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(4 * count);

            copyMatToArray<int>(mat, buff, count * 4);

            Span<Vec4i> buffSpan = MemoryMarshal.Cast<int, Vec4i>(buff.AsSpan<int>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                Rect r = ar[i];
                ref readonly Vec4i vec = ref buffSpan[i];
                r.x = vec.Item1;
                r.y = vec.Item2;
                r.width = vec.Item3;
                r.height = vec.Item4;
            }

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[4 * count];

            copyMatToArray<int>(mat, buff, count * 4);

            for (int i = 0; i < count; i++)
            {
                Rect r = ar[i];
                r.x = buff[i * 4];
                r.y = buff[i * 4 + 1];
                r.width = buff[i * 4 + 2];
                r.height = buff[i * 4 + 3];
            }
#endif
        }

        internal static void List_Rect2d_to_Mat(List<Rect2d> lr2d, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (lr2d == null || mat == null || count < 0 || count > lr2d.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();
                    for (int i = 0; i < count; i++)
                    {
                        Rect2d r = lr2d[i];
                        ptr[i * 4] = (double)r.x;
                        ptr[i * 4 + 1] = (double)r.y;
                        ptr[i * 4 + 2] = (double)r.width;
                        ptr[i * 4 + 3] = (double)r.height;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

            Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4d vec = ref buffSpan[i];
                Rect2d r = lr2d[i];
                vec.Item1 = (double)r.x;
                vec.Item2 = (double)r.y;
                vec.Item3 = (double)r.width;
                vec.Item4 = (double)r.height;
            }

            copyArrayToMat<double>(buff, mat, count * 4);

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count * 4];

            for (int i = 0; i < count; i++)
            {
                Rect2d r = lr2d[i];
                buff[i * 4] = (double)r.x;
                buff[i * 4 + 1] = (double)r.y;
                buff[i * 4 + 2] = (double)r.width;
                buff[i * 4 + 3] = (double)r.height;
            }
            copyArrayToMat<double>(buff, mat, count * 4);
#endif
        }

        internal static void Array_Rect2d_to_Mat(Rect2d[] ar2d, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (ar2d == null || mat == null || count < 0 || count > ar2d.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();
                    for (int i = 0; i < count; i++)
                    {
                        Rect2d r = ar2d[i];
                        ptr[i * 4] = (double)r.x;
                        ptr[i * 4 + 1] = (double)r.y;
                        ptr[i * 4 + 2] = (double)r.width;
                        ptr[i * 4 + 3] = (double)r.height;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

            Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec4d vec = ref buffSpan[i];
                Rect2d r = ar2d[i];
                vec.Item1 = (double)r.x;
                vec.Item2 = (double)r.y;
                vec.Item3 = (double)r.width;
                vec.Item4 = (double)r.height;
            }

            copyArrayToMat<double>(buff, mat, count * 4);

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count * 4];

            for (int i = 0; i < count; i++)
            {
                Rect2d r = ar2d[i];
                buff[i * 4] = (double)r.x;
                buff[i * 4 + 1] = (double)r.y;
                buff[i * 4 + 2] = (double)r.width;
                buff[i * 4 + 3] = (double)r.height;
            }
            copyArrayToMat<double>(buff, mat, count * 4);
#endif
        }

        internal static void Mat_to_List_Rect2d(Mat mat, List<Rect2d> lr2d, int count)
        {
#if OPENCV_DBGASSERT
            if (lr2d == null || mat == null || count < 0 || count > lr2d.Count || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect2d r = lr2d[i];
                        r.x = ptr[i * 4];
                        r.y = ptr[i * 4 + 1];
                        r.width = ptr[i * 4 + 2];
                        r.height = ptr[i * 4 + 3];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

            copyMatToArray<double>(mat, buff, count * 4);

            Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                Rect2d r = lr2d[i];
                ref readonly Vec4d vec = ref buffSpan[i];
                r.x = vec.Item1;
                r.y = vec.Item2;
                r.width = vec.Item3;
                r.height = vec.Item4;
            }

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count * 4];

            copyMatToArray<double>(mat, buff, count * 4);
            for (int i = 0; i < count; i++)
            {
                Rect2d r = lr2d[i];
                r.x = buff[i * 4];
                r.y = buff[i * 4 + 1];
                r.width = buff[i * 4 + 2];
                r.height = buff[i * 4 + 3];
            }
#endif
        }

        internal static void Mat_to_Array_Rect2d(Mat mat, Rect2d[] ar2d, int count)
        {
#if OPENCV_DBGASSERT
            if (ar2d == null || mat == null || count < 0 || count > ar2d.Length || mat.total() * mat.channels() < count * 4)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        Rect2d r = ar2d[i];
                        r.x = ptr[i * 4];
                        r.y = ptr[i * 4 + 1];
                        r.width = ptr[i * 4 + 2];
                        r.height = ptr[i * 4 + 3];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count * 4);

            copyMatToArray<double>(mat, buff, count * 4);

            Span<Vec4d> buffSpan = MemoryMarshal.Cast<double, Vec4d>(buff.AsSpan<double>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                Rect2d r = ar2d[i];
                ref readonly Vec4d vec = ref buffSpan[i];
                r.x = vec.Item1;
                r.y = vec.Item2;
                r.width = vec.Item3;
                r.height = vec.Item4;
            }

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count * 4];

            copyMatToArray<double>(mat, buff, count * 4);
            for (int i = 0; i < count; i++)
            {
                Rect2d r = ar2d[i];
                r.x = buff[i * 4];
                r.y = buff[i * 4 + 1];
                r.width = buff[i * 4 + 2];
                r.height = buff[i * 4 + 3];
            }
#endif
        }

        internal static void List_RotatedRect_to_Mat(List<RotatedRect> lrr, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (lrr == null || mat == null || count < 0 || count > lrr.Count || mat.total() * mat.channels() < count * 5)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        RotatedRect r = lrr[i];
                        ptr[i * 5] = (float)r.center.x;
                        ptr[i * 5 + 1] = (float)r.center.y;
                        ptr[i * 5 + 2] = (float)r.size.width;
                        ptr[i * 5 + 3] = (float)r.size.height;
                        ptr[i * 5 + 4] = (float)r.angle;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(5 * count);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec = ref buffSpan[i];
                RotatedRect r = lrr[i];
                vec.Item1 = (float)r.center.x;
                vec.Item2 = (float)r.center.y;
                vec.Item3 = (float)r.size.width;
                vec.Item4 = (float)r.size.height;
                vec.Item5 = (float)r.angle;
            }

            copyArrayToMat<float>(buff, mat, count * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[5 * count];

            for (int i = 0; i < count; i++)
            {
                RotatedRect r = lrr[i];
                buff[i * 5] = (float)r.center.x;
                buff[i * 5 + 1] = (float)r.center.y;
                buff[i * 5 + 2] = (float)r.size.width;
                buff[i * 5 + 3] = (float)r.size.height;
                buff[i * 5 + 4] = (float)r.angle;
            }
            copyArrayToMat<float>(buff, mat, count * 5);
#endif
        }

        internal static void Array_RotatedRect_to_Mat(RotatedRect[] arr, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (arr == null || mat == null || count < 0 || count > arr.Length || mat.total() * mat.channels() < count * 5)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        RotatedRect r = arr[i];
                        ptr[i * 5] = (float)r.center.x;
                        ptr[i * 5 + 1] = (float)r.center.y;
                        ptr[i * 5 + 2] = (float)r.size.width;
                        ptr[i * 5 + 3] = (float)r.size.height;
                        ptr[i * 5 + 4] = (float)r.angle;
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(5 * count);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec5f vec = ref buffSpan[i];
                RotatedRect r = arr[i];
                vec.Item1 = (float)r.center.x;
                vec.Item2 = (float)r.center.y;
                vec.Item3 = (float)r.size.width;
                vec.Item4 = (float)r.size.height;
                vec.Item5 = (float)r.angle;
            }

            copyArrayToMat<float>(buff, mat, count * 5);

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[5 * count];

            for (int i = 0; i < count; i++)
            {
                RotatedRect r = arr[i];
                buff[i * 5] = (float)r.center.x;
                buff[i * 5 + 1] = (float)r.center.y;
                buff[i * 5 + 2] = (float)r.size.width;
                buff[i * 5 + 3] = (float)r.size.height;
                buff[i * 5 + 4] = (float)r.angle;
            }
            copyArrayToMat<float>(buff, mat, count * 5);
#endif
        }

        internal static void Mat_to_List_RotatedRect(Mat mat, List<RotatedRect> lrr, int count)
        {
#if OPENCV_DBGASSERT
            if (lrr == null || mat == null || count < 0 || count > lrr.Count || mat.total() * mat.channels() < count * 5)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        RotatedRect r = lrr[i];
                        r.center.x = ptr[i * 5];
                        r.center.y = ptr[i * 5 + 1];
                        r.size.width = ptr[i * 5 + 2];
                        r.size.height = ptr[i * 5 + 3];
                        r.angle = ptr[i * 5 + 4];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(5 * count);

            copyMatToArray<float>(mat, buff, count * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                RotatedRect r = lrr[i];
                ref readonly Vec5f vec = ref buffSpan[i];
                r.center.x = vec.Item1;
                r.center.y = vec.Item2;
                r.size.width = vec.Item3;
                r.size.height = vec.Item4;
                r.angle = vec.Item5;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[5 * count];

            copyMatToArray<float>(mat, buff, count * 5);

            for (int i = 0; i < count; i++)
            {
                RotatedRect r = lrr[i];
                r.center.x = buff[i * 5];
                r.center.y = buff[i * 5 + 1];
                r.size.width = buff[i * 5 + 2];
                r.size.height = buff[i * 5 + 3];
                r.angle = buff[i * 5 + 4];
            }
#endif
        }

        internal static void Mat_to_Array_RotatedRect(Mat mat, RotatedRect[] arr, int count)
        {
#if OPENCV_DBGASSERT
            if (arr == null || mat == null || count < 0 || count > arr.Length || mat.total() * mat.channels() < count * 5)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        RotatedRect r = arr[i];
                        r.center.x = ptr[i * 5];
                        r.center.y = ptr[i * 5 + 1];
                        r.size.width = ptr[i * 5 + 2];
                        r.size.height = ptr[i * 5 + 3];
                        r.angle = ptr[i * 5 + 4];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            float[] buff = ArrayPool<float>.Shared.Rent(5 * count);

            copyMatToArray<float>(mat, buff, count * 5);

            Span<Vec5f> buffSpan = MemoryMarshal.Cast<float, Vec5f>(buff.AsSpan<float>()).Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                RotatedRect r = arr[i];
                ref readonly Vec5f vec = ref buffSpan[i];
                r.center.x = vec.Item1;
                r.center.y = vec.Item2;
                r.size.width = vec.Item3;
                r.size.height = vec.Item4;
                r.angle = vec.Item5;
            }

            ArrayPool<float>.Shared.Return(buff);
#else
            float[] buff = new float[5 * count];

            copyMatToArray<float>(mat, buff, count * 5);

            for (int i = 0; i < count; i++)
            {
                RotatedRect r = arr[i];
                r.center.x = buff[i * 5];
                r.center.y = buff[i * 5 + 1];
                r.size.width = buff[i * 5 + 2];
                r.size.height = buff[i * 5 + 3];
                r.angle = buff[i * 5 + 4];
            }
#endif
        }

        internal static void List_Mat_to_Mat<T>(List<T> lm, Mat mat, int count) where T : Mat
        {
#if OPENCV_DBGASSERT
            if (lm == null || mat == null || count < 0 || count > lm.Count || mat.total() * mat.channels() < count * 2)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        long addr = (long)lm[i].nativeObj;//TODO:@check
                        ptr[i * 2] = (int)(addr >> 32);
                        ptr[i * 2 + 1] = (int)(addr & 0xffffffff);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            int[] buff = ArrayPool<int>.Shared.Rent(count * 2);

            Span<Vec2i> buffSpan = MemoryMarshal.Cast<int, Vec2i>(buff.AsSpan<int>()).Slice(0, count);

            for (int i = 0; i < buffSpan.Length; i++)
            {
                ref Vec2i vec = ref buffSpan[i];
                long addr = (long)lm[i].nativeObj;//TODO:@check
                vec.Item1 = (int)(addr >> 32);
                vec.Item2 = (int)(addr & 0xffffffff);
            }

            copyArrayToMat<int>(buff, mat, count * 2);

            ArrayPool<int>.Shared.Return(buff);
#else
            int[] buff = new int[count * 2];
            for (int i = 0; i < count; i++)
            {
                long addr = (long)lm[i].nativeObj;
                buff[i * 2] = (int)(addr >> 32);
                buff[i * 2 + 1] = (int)(addr & 0xffffffff);

            }
            copyArrayToMat<int>(buff, mat, count * 2);
#endif
        }

        internal static void List_uchar_to_Mat(List<byte> lb, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (lb == null || mat == null || count < 0 || count > lb.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    byte* ptr = (byte*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        ptr[i] = lb[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            byte[] buff = ArrayPool<byte>.Shared.Rent(count);

            Span<byte> buffSpan = buff.AsSpan<byte>().Slice(0, count);

            for (int i = 0; i < buffSpan.Length; i++)
            {
                buffSpan[i] = lb[i];
            }
            copyArrayToMat<byte>(buff, mat, count);

            ArrayPool<byte>.Shared.Return(buff);
#else
            byte[] buff = new byte[count];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = lb[i];
            }
            copyArrayToMat<byte>(buff, mat, count);
#endif
        }

        internal static void Mat_to_List_uchar(Mat mat, List<byte> lb, int count)
        {
#if OPENCV_DBGASSERT
            if (lb == null || mat == null || count < 0 || count > lb.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    byte* ptr = (byte*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        lb[i] = ptr[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            byte[] buff = ArrayPool<byte>.Shared.Rent(count);

            copyMatToArray<byte>(mat, buff, count);

            Span<byte> buffSpan = buff.AsSpan<byte>().Slice(0, count);
            for (int i = 0; i < buffSpan.Length; i++)
            {
                lb[i] = buffSpan[i];
            }
            ArrayPool<byte>.Shared.Return(buff);
#else
            byte[] buff = new byte[count];
            copyMatToArray<byte>(mat, buff, count);
            for (int i = 0; i < buff.Length; i++)
            {
                lb[i] = buff[i];
            }
#endif
        }

        internal static void List_char_to_Mat(List<sbyte> lb, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (lb == null || mat == null || count < 0 || count > lb.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    sbyte* ptr = (sbyte*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        ptr[i] = lb[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            sbyte[] buff = ArrayPool<sbyte>.Shared.Rent(count);
#else
            sbyte[] buff = new sbyte[count];
#endif
            for (int i = 0; i < count; i++)
            {
                buff[i] = lb[i];
            }
            copyArrayToMat<sbyte>(buff, mat, count);
#if NET_STANDARD_2_1
            ArrayPool<sbyte>.Shared.Return(buff);
#endif
        }

        internal static void Mat_to_List_char(Mat mat, List<sbyte> lb, int count)
        {
#if OPENCV_DBGASSERT
            if (lb == null || mat == null || count < 0 || count > lb.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    sbyte* ptr = (sbyte*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        lb[i] = ptr[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            sbyte[] buff = ArrayPool<sbyte>.Shared.Rent(count);
#else
            sbyte[] buff = new sbyte[count];
#endif
            copyMatToArray<sbyte>(mat, buff, count);
            for (int i = 0; i < count; i++)
            {
                lb[i] = buff[i];
            }
#if NET_STANDARD_2_1
            ArrayPool<sbyte>.Shared.Return(buff);
#endif
        }

        internal static void List_int_to_Mat(List<int> li, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (li == null || mat == null || count < 0)
                throw new CvException("Invalid parameters");

            if (type == CvType.CV_32SC1)
            {
                if (count > li.Count || mat.total() * mat.channels() < count)
                    throw new CvException("Invalid parameters for CV_32SC1");
            }
            else if (type == CvType.CV_32SC4)
            {
                if (count * 4 > li.Count || mat.total() * mat.channels() < count * 4)
                    throw new CvException("Invalid parameters for CV_32SC4");
            }
            else
            {
                throw new CvException("Unsupported type");
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    if (type == CvType.CV_32SC1)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ptr[i] = li[i];
                        }
                    }
                    else if (type == CvType.CV_32SC4)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ptr[i * 4 + 0] = li[i * 4 + 0];
                            ptr[i * 4 + 1] = li[i * 4 + 1];
                            ptr[i * 4 + 2] = li[i * 4 + 2];
                            ptr[i * 4 + 3] = li[i * 4 + 3];
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32SC1 or CV_32SC4");
                    }
                    return;
                }
            }
#endif

#if NET_STANDARD_2_1

            if (type == CvType.CV_32SC1)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count);

                Span<int> buffSpan = buff.AsSpan<int>().Slice(0, count);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    buffSpan[i] = li[i];
                }

                copyArrayToMat<int>(buff, mat, count);

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32SC4)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count * 4);

                Span<int> buffSpan = buff.AsSpan<int>().Slice(0, count * 4);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    buffSpan[i] = li[i];
                }

                copyArrayToMat<int>(buff, mat, count * 4);

                ArrayPool<int>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("'type' can be CV_32SC1 or CV_32SC4");
            }

#else
            if (type == CvType.CV_32SC1)
            {
                int[] buff = new int[count];

                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = li[i];
                }

                copyArrayToMat<int>(buff, mat, count);
            }
            else if (type == CvType.CV_32SC4)
            {
                int[] buff = new int[count * 4];

                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = li[i];
                }

                copyArrayToMat<int>(buff, mat, count * 4);
            }
            else
            {
                throw new CvException("'type' can be CV_32SC1 or CV_32SC4");
            }
#endif
        }

        internal static void Mat_to_List_int(Mat mat, List<int> li, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (li == null || mat == null || count < 0)
                throw new CvException("Invalid parameters");

            if (type == CvType.CV_32SC1)
            {
                if (count > li.Count || mat.total() * mat.channels() < count)
                    throw new CvException("Invalid parameters for CV_32SC1");
            }
            else if (type == CvType.CV_32SC4)
            {
                if (count * 4 > li.Count || mat.total() * mat.channels() < count * 4)
                    throw new CvException("Invalid parameters for CV_32SC4");
            }
            else
            {
                throw new CvException("Unsupported type");
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    int* ptr = (int*)mat.dataAddr();

                    if (type == CvType.CV_32SC1)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            li[i] = ptr[i];
                        }
                    }
                    else if (type == CvType.CV_32SC4)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            li[i * 4 + 0] = ptr[i * 4 + 0];
                            li[i * 4 + 1] = ptr[i * 4 + 1];
                            li[i * 4 + 2] = ptr[i * 4 + 2];
                            li[i * 4 + 3] = ptr[i * 4 + 3];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32SC1 or CV_32SC4 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1

            if (type == CvType.CV_32SC1)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count);

                copyMatToArray<int>(mat, buff, count);

                Span<int> buffSpan = buff.AsSpan<int>().Slice(0, count);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    li[i] = buffSpan[i];
                }

                ArrayPool<int>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32SC4)
            {
                int[] buff = ArrayPool<int>.Shared.Rent(count * 4);

                copyMatToArray<int>(mat, buff, count * 4);

                Span<int> buffSpan = buff.AsSpan<int>().Slice(0, count * 4);
                for (int i = 0; i < buffSpan.Length; i++)
                {
                    li[i] = buffSpan[i];
                }

                ArrayPool<int>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC1 or CV_32SC4 type\n" + mat);
            }

#else
            if (type == CvType.CV_32SC1)
            {
                int[] buff = new int[count];

                copyMatToArray<int>(mat, buff, count);

                for (int i = 0; i < buff.Length; i++)
                {
                    li[i] = buff[i];
                }
            }
            else if (type == CvType.CV_32SC4)
            {
                int[] buff = new int[count * 4];

                copyMatToArray<int>(mat, buff, count * 4);

                for (int i = 0; i < buff.Length; i++)
                {
                    li[i] = buff[i];
                }
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32SC1 or CV_32SC4 type\n" + mat);
            }
#endif
        }

        internal static void List_float_to_Mat(List<float> lf, Mat mat, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lf == null || mat == null || count < 0)
                throw new CvException("Invalid parameters");

            if (type == CvType.CV_32FC1)
            {
                if (count > lf.Count || mat.total() * mat.channels() < count)
                    throw new CvException("Invalid parameters for CV_32FC1");
            }
            else if (type == CvType.CV_32FC4)
            {
                if (count * 4 > lf.Count || mat.total() * mat.channels() < count * 4)
                    throw new CvException("Invalid parameters for CV_32FC4");
            }
            else if (type == CvType.CV_32FC(6))
            {
                if (count * 6 > lf.Count || mat.total() * mat.channels() < count * 6)
                    throw new CvException("Invalid parameters for CV_32FC6");
            }
            else
            {
                throw new CvException("Unsupported type");
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    if (type == CvType.CV_32FC1)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ptr[i] = lf[i];
                        }
                    }
                    else if (type == CvType.CV_32FC4)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ptr[i * 4 + 0] = lf[i * 4 + 0];
                            ptr[i * 4 + 1] = lf[i * 4 + 1];
                            ptr[i * 4 + 2] = lf[i * 4 + 2];
                            ptr[i * 4 + 3] = lf[i * 4 + 3];
                        }
                    }
                    else if (type == CvType.CV_32FC(6))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ptr[i * 6 + 0] = lf[i * 6 + 0];
                            ptr[i * 6 + 1] = lf[i * 6 + 1];
                            ptr[i * 6 + 2] = lf[i * 6 + 2];
                            ptr[i * 6 + 3] = lf[i * 6 + 3];
                            ptr[i * 6 + 4] = lf[i * 6 + 4];
                            ptr[i * 6 + 5] = lf[i * 6 + 5];
                        }
                    }
                    else
                    {
                        throw new CvException("'type' can be CV_32FC1, CV_32FC4 or CV_32FC6");
                    }
                    return;
                }
            }
#endif

#if NET_STANDARD_2_1

            if (type == CvType.CV_32FC1)
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    buffSpan[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count);
                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC4)
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count * 4);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    buffSpan[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count * 4);
                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC(6))
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 6);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count * 6);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    buffSpan[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count * 6);
                ArrayPool<float>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("'type' can be CV_32FC1, CV_32FC4 or CV_32FC6");
            }

#else
            if (type == CvType.CV_32FC1)
            {
                float[] buff = new float[count];

                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count);
            }
            else if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[count * 4];

                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count * 4);
            }
            else if (type == CvType.CV_32FC(6))
            {
                float[] buff = new float[count * 6];

                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = lf[i];
                }

                copyArrayToMat<float>(buff, mat, count * 6);
            }
            else
            {
                throw new CvException("'type' can be CV_32FC1, CV_32FC4 or CV_32FC6");
            }
#endif
        }

        internal static void Mat_to_List_float(Mat mat, List<float> lf, int count, int type)
        {
#if OPENCV_DBGASSERT
            if (lf == null || mat == null || count < 0)
                throw new CvException("Invalid parameters");

            if (type == CvType.CV_32FC1)
            {
                if (count > lf.Count || mat.total() * mat.channels() < count)
                    throw new CvException("Invalid parameters for CV_32FC1");
            }
            else if (type == CvType.CV_32FC4)
            {
                if (count * 4 > lf.Count || mat.total() * mat.channels() < count * 4)
                    throw new CvException("Invalid parameters for CV_32FC4");
            }
            else if (type == CvType.CV_32FC(6))
            {
                if (count * 6 > lf.Count || mat.total() * mat.channels() < count * 6)
                    throw new CvException("Invalid parameters for CV_32FC6");
            }
            else
            {
                throw new CvException("Unsupported type");
            }
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    float* ptr = (float*)mat.dataAddr();

                    if (type == CvType.CV_32FC1)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            lf[i] = ptr[i];
                        }
                    }
                    else if (type == CvType.CV_32FC4)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            lf[i * 4 + 0] = ptr[i * 4 + 0];
                            lf[i * 4 + 1] = ptr[i * 4 + 1];
                            lf[i * 4 + 2] = ptr[i * 4 + 2];
                            lf[i * 4 + 3] = ptr[i * 4 + 3];
                        }
                    }
                    else if (type == CvType.CV_32FC(6))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            lf[i * 6 + 0] = ptr[i * 6 + 0];
                            lf[i * 6 + 1] = ptr[i * 6 + 1];
                            lf[i * 6 + 2] = ptr[i * 6 + 2];
                            lf[i * 6 + 3] = ptr[i * 6 + 3];
                            lf[i * 6 + 4] = ptr[i * 6 + 4];
                            lf[i * 6 + 5] = ptr[i * 6 + 5];
                        }
                    }
                    else
                    {
                        throw new CvException("Input Mat should be of CV_32FC1, CV_32FC4 or CV_32FC6 type\n" + mat);
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1

            if (type == CvType.CV_32FC1)
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count);

                copyMatToArray<float>(mat, buff, count);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    lf[i] = buffSpan[i];
                }

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC4)
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 4);

                copyMatToArray<float>(mat, buff, count * 4);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count * 4);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    lf[i] = buffSpan[i];
                }

                ArrayPool<float>.Shared.Return(buff);
            }
            else if (type == CvType.CV_32FC(6))
            {
                float[] buff = ArrayPool<float>.Shared.Rent(count * 6);

                copyMatToArray<float>(mat, buff, count * 6);

                Span<float> buffSpan = buff.AsSpan<float>().Slice(0, count * 6);

                for (int i = 0; i < buffSpan.Length; i++)
                {
                    lf[i] = buffSpan[i];
                }

                ArrayPool<float>.Shared.Return(buff);
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC1, CV_32FC4 or CV_32FC6 type\n" + mat);
            }
#else
            if (type == CvType.CV_32FC1)
            {
                float[] buff = new float[count];

                copyMatToArray<float>(mat, buff, count);

                for (int i = 0; i < buff.Length; i++)
                {
                    lf[i] = buff[i];
                }
            }
            else if (type == CvType.CV_32FC4)
            {
                float[] buff = new float[count * 4];

                copyMatToArray<float>(mat, buff, count * 4);

                for (int i = 0; i < buff.Length; i++)
                {
                    lf[i] = buff[i];
                }
            }
            else if (type == CvType.CV_32FC(6))
            {
                float[] buff = new float[count * 6];

                copyMatToArray<float>(mat, buff, count * 6);

                for (int i = 0; i < buff.Length; i++)
                {
                    lf[i] = buff[i];
                }
            }
            else
            {
                throw new CvException("Input Mat should be of CV_32FC1, CV_32FC4 or CV_32FC6 type\n" + mat);
            }
#endif
        }

        internal static void List_double_to_Mat(List<double> ld, Mat mat, int count)
        {
#if OPENCV_DBGASSERT
            if (ld == null || mat == null || count < 0 || count > ld.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        ptr[i] = ld[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count);

            Span<double> buffSpan = buff.AsSpan<double>().Slice(0, count);

            for (int i = 0; i < buffSpan.Length; i++)
            {
                buffSpan[i] = ld[i];
            }

            copyArrayToMat<double>(buff, mat, count);

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = ld[i];
            }
            copyArrayToMat<double>(buff, mat, count);
#endif
        }

        internal static void Mat_to_List_double(Mat mat, List<double> ld, int count)
        {
#if OPENCV_DBGASSERT
            if (ld == null || mat == null || count < 0 || count > ld.Count || mat.total() * mat.channels() < count)
                throw new CvException("Invalid parameters");
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (mat.isContinuous())
            {
                unsafe
                {
                    double* ptr = (double*)mat.dataAddr();

                    for (int i = 0; i < count; i++)
                    {
                        ld[i] = ptr[i];
                    }
                }
                return;
            }
#endif

#if NET_STANDARD_2_1
            double[] buff = ArrayPool<double>.Shared.Rent(count);

            copyMatToArray<double>(mat, buff, count);

            Span<double> buffSpan = buff.AsSpan<double>().Slice(0, count);

            for (int i = 0; i < buffSpan.Length; i++)
            {
                ld[i] = buffSpan[i];
            }

            ArrayPool<double>.Shared.Return(buff);
#else
            double[] buff = new double[count];

            copyMatToArray<double>(mat, buff, count);

            for (int i = 0; i < buff.Length; i++)
            {
                ld[i] = buff[i];
            }
#endif
        }
    }
}
