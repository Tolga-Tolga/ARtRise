using System;
using System.Buffers;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UtilsModule
{
    public partial class Converters
    {
        /// <summary>
        /// Separator byte for byte array (Unit Separator)
        /// </summary>
        private const byte BYTE_VECTOR_SEPARATOR = 0x1F;

        public static Mat NativeByteArray_to_Mat(byte[] bytes)
        {
            Mat res;

            if (bytes != null && bytes.Length > 0)
            {
                res = new Mat(1, bytes.Length, CvType.CV_8UC1);
                res.put(0, 0, bytes);
            }
            else
            {
                res = new Mat();
            }

            return res;
        }

        public static void Mat_to_NativeByteArray(Mat m, ref byte[] bytes)
        {
            if (m != null)
                m.ThrowIfDisposed();

            int count = m.cols();
            if (CvType.CV_8UC1 != m.type() || m.rows() != 1)
                throw new CvException(
                    "CvType.CV_8UC1 != m.type() ||  m.rows()!=1\n" + m);

            if (count > 0)
            {
                // Initialize bytes if null or insufficient size
                if (bytes == null || bytes.Length < count)
                {
                    bytes = new byte[count];
                }

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE
                if (m.isContinuous())
                {
                    unsafe
                    {
                        Span<byte> buff = new Span<byte>((void*)m.dataAddr(), count);
                        buff.CopyTo(bytes);
                    }
                }
                else
                {
                    m.get(0, 0, bytes, count);
                }
#else
                m.get(0, 0, bytes, count);
#endif
            }
            else
            {
                bytes = null;
            }
        }

        public static Mat vector_NativeByteArray_to_Mat(List<byte[]> bytes)
        {
            Mat res;

            int count = (bytes != null) ? bytes.Count : 0;
            if (count > 0)
            {
                int totalLength = 0;
                for (int i = 0; i < count; i++)
                {
                    // Add only separator byte for null or empty array, otherwise element + separator byte
                    totalLength += (bytes[i] == null || bytes[i].Length == 0) ? 1 : bytes[i].Length + 1;
                }

#if NET_STANDARD_2_1
                byte[] concatBytes = ArrayPool<byte>.Shared.Rent(totalLength);
                Span<byte> concatSpan = concatBytes;

                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (bytes[i] != null && bytes[i].Length > 0)
                    {
                        bytes[i].AsSpan().CopyTo(concatSpan.Slice(index));
                        index += bytes[i].Length;
                    }
                    concatSpan[index] = BYTE_VECTOR_SEPARATOR; // Separate with Unit Separator
                    index++;
                }

                res = new Mat(1, index, CvType.CV_8UC1);
                res.put(0, 0, concatBytes, index);

                ArrayPool<byte>.Shared.Return(concatBytes);
#else
                byte[] concatBytes = new byte[totalLength];

                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (bytes[i] != null && bytes[i].Length > 0)
                    {
                        bytes[i].CopyTo(concatBytes, index);
                        index += bytes[i].Length;
                    }
                    concatBytes[index] = BYTE_VECTOR_SEPARATOR; // Separate with Unit Separator
                    index++;
                }

                res = new Mat(1, index, CvType.CV_8UC1);
                res.put(0, 0, concatBytes, index);
#endif
            }
            else
            {
                res = new Mat();
            }

            return res;
        }

        public static void Mat_to_vector_NativeByteArray(Mat m, List<byte[]> bytes)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (bytes == null)
                throw new CvException("bytes == null");

            int count = m.cols();
            if (CvType.CV_8UC1 != m.type() || m.rows() != 1)
                throw new CvException(
                    "CvType.CV_8UC1 != m.type() ||  m.rows()!=1\n" + m);

            bytes.Clear();

            if (count > 0)
            {
#if NET_STANDARD_2_1
                byte[] matData = ArrayPool<byte>.Shared.Rent(count);

#if !OPENCV_DONT_USE_UNSAFE_CODE
                if (m.isContinuous())
                {
                    unsafe
                    {
                        Span<byte> buff = new Span<byte>((void*)m.dataAddr(), count);
                        buff.CopyTo(matData);
                    }
                }
                else
                {
                    m.get(0, 0, matData, count);
                }
#else
                m.get(0, 0, matData, count);
#endif

                Span<byte> dataSpan = new Span<byte>(matData, 0, count);

                while (true)
                {
                    int index = MemoryExtensions.IndexOf<byte>(dataSpan, BYTE_VECTOR_SEPARATOR);
                    if (index < 0)
                        break;

                    byte[] currentArray = new byte[index];
                    dataSpan.Slice(0, index).CopyTo(currentArray);
                    bytes.Add(currentArray);

                    dataSpan = dataSpan.Slice(index + 1);
                }

                ArrayPool<byte>.Shared.Return(matData);
#else
                byte[] buff = new byte[count];
                m.get(0, 0, buff);

                int startIndex = 0;
                for (int i = 0; i < count; i++)
                {
                    if (buff[i] == BYTE_VECTOR_SEPARATOR) // Unit Separator
                    {
                        // Add elements from startIndex to i (including empty strings)
                        byte[] currentArray = new byte[i - startIndex];
                        Array.Copy(buff, startIndex, currentArray, 0, i - startIndex);
                        bytes.Add(currentArray);
                        startIndex = i + 1;
                    }
                }
#endif
            }
        }
    }
}
