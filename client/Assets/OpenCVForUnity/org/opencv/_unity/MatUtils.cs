using OpenCVForUnity.CoreModule;
using System;
using System.Runtime.InteropServices;

#if !OPENCV_DONT_USE_UNSAFE_CODE
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
#endif

namespace OpenCVForUnity.UnityUtils
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.OpenCVMatUtils class instead.")]
    public static class MatUtils
    {
        #region copyFromMat_IntPtr

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// The entire range of data managed by the Mat will be copied to the IntPtr.
        /// If you want to specify the exact range of data to copy, please use the method that accepts a <paramref name="length"/> argument.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat(Mat mat, IntPtr intPtr)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            int length = (int)(mat.total() * mat.elemSize());

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size of the memory region pointed to by the IntPtr is smaller than the data size managed by the Mat, data will be copied up to the size of the IntPtr.
        /// If the data size of the memory region pointed to by the IntPtr is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="length">
        /// The number of elements, in terms of byte size, to copy from the source Mat to the destination IntPtr.
        /// The <paramref name="length"/> represents the size in bytes, regardless of the actual type of data in the Mat or the destination.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>

        public static int copyFromMat(Mat mat, IntPtr intPtr, int length)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size of the memory region pointed to by the IntPtr is smaller than the data size managed by the Mat, data will be copied up to the size of the IntPtr.
        /// If the data size of the memory region pointed to by the IntPtr is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements represented by the IntPtr. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination IntPtr.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, IntPtr intPtr, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;
            int sizeof_T = default;
#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                sizeof_T = sizeof(T);
            }
#else
            sizeof_T =  Marshal.SizeOf<T>();
#endif
            length = length * sizeof_T;

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        #endregion

        #region copyToMat_IntPtr

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// Data of the same size in bytes as the data managed by the Mat is copied from the IntPtr to the Mat.
        /// If you want to specify the exact range of data to copy, please use the method that accepts a <paramref name="length"/> argument.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>

        public static int copyToMat(IntPtr intPtr, Mat mat)
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            int length = (int)(mat.total() * mat.elemSize());

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size of the memory region pointed to by the IntPtr, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size of the memory region pointed to by the IntPtr.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements, in terms of byte size, to copy from the source IntPtr to the destination Mat.
        /// The <paramref name="length"/> represents the size in bytes, regardless of the actual type of data in the IntPtr or the destination.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat(IntPtr intPtr, Mat mat, int length)
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size of the memory region pointed to by the IntPtr, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size of the memory region pointed to by the IntPtr.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements represented by the IntPtr. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source IntPtr to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(IntPtr intPtr, Mat mat, int length) where T : unmanaged
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;
            int sizeof_T = default;
#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                sizeof_T = sizeof(T);
            }
#else
            sizeof_T =  Marshal.SizeOf<T>();
#endif
            length = length * sizeof_T;

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        #endregion


        #region copyFromMat_Array

        /// <summary>
        /// Copies the data from an OpenCV Mat to a managed array.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a managed array. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination managed array where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the managed array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, T[] array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            int bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * Marshal.SizeOf<T>(), arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a managed array.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a managed array. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination managed array where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination managed array.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the managed array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, T[] array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            int bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * Marshal.SizeOf<T>(), arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif
        }

        #endregion

        #region copyToMat_Array

        /// <summary>
        /// Copies the data from a managed array to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a managed array into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source managed array from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the managed array to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(T[] array, Mat mat) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, array.Length * sizeof(T), mat.nativeObj);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            int bytesNum = OpenCVForUnity_ByteArrayToMatData(arrayHandle.AddrOfPinnedObject(), array.Length * Marshal.SizeOf<T>(), mat.nativeObj);
            arrayHandle.Free();
            return bytesNum;
#endif
        }

        /// <summary>
        /// Copies the data from a managed array to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a managed array into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source managed array from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source managed array to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the managed array to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(T[] array, Mat mat, int length) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, length * sizeof(T), mat.nativeObj);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            int bytesNum = OpenCVForUnity_ByteArrayToMatData(arrayHandle.AddrOfPinnedObject(), length * Marshal.SizeOf<T>(), mat.nativeObj);
            arrayHandle.Free();
            return bytesNum;
#endif
        }

        #endregion


#if !OPENCV_DONT_USE_UNSAFE_CODE

        #region copyFromMat_NativeArray

        /// <summary>
        /// Copies the data from an OpenCV Mat to a NativeArray.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a NativeArray. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination NativeArray where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the NativeArray.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, NativeArray<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(array));
            }
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a NativeArray.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a NativeArray. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination NativeArray where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination managed array.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the NativeArray.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, NativeArray<T> array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(array));
            }
        }

        #endregion

        #region copyToMat_NativeArray

        /// <summary>
        /// Copies the data from a NativeArray to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a NativeArray into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source NativeArray from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the NativeArray to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(NativeArray<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                return OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length * sizeof(T), mat.nativeObj);
            }
        }

        /// <summary>
        /// Copies the data from a NativeArray to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a NativeArray into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source NativeArray from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source NativeArray to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the NativeArray to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(NativeArray<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                return OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), length * sizeof(T), mat.nativeObj);
            }
        }

        #endregion

#endif


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region copyFromMat_Span

        /// <summary>
        /// Copies the data from an OpenCV Mat to a Span.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a Span. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination Span where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the Span.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, Span<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)ptr);
                }
            }
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a Span.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a Span. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination Span where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination Span.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the Span.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyFromMat<T>(Mat mat, Span<T> array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)ptr);
                }
            }
        }

        #endregion

        #region copyToMat_Span

        /// <summary>
        /// Copies the data from a Span to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a Span into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source Span from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Span to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(Span<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, array.Length * sizeof(T), mat.nativeObj);
                }
            }
        }

        /// <summary>
        /// Copies the data from a Span to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a Span into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source Span from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Span to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Span to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int copyToMat<T>(Span<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, length * sizeof(T), mat.nativeObj);
                }
            }
        }

        #endregion

#endif



#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif

        [DllImport(LIBNAME)]
        private static extern int OpenCVForUnity_MatDataToByteArray(IntPtr mat, int length, IntPtr byteArray);

        [DllImport(LIBNAME)]
        private static extern int OpenCVForUnity_ByteArrayToMatData(IntPtr byteArray, int length, IntPtr Mat);
    }
}
