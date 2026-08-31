using System;
#if NET_STANDARD_2_1
using System.Buffers;
#endif
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.CoreModule
{

    public partial class Mat
    {

        #region put_Array_length_offset

        #region byte

        /// <summary>
        /// Writes a byte array to the matrix at a specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the byte array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified byte array to the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix channel count.
        /// If the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>, an exception will be thrown.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible with byte data.
        /// </exception>
        public int put(int row, int col, byte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutB(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of byte values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <param name="length">The number of bytes to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of byte values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices does not match the matrix's dimensionality,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, byte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region sbyte

        /// <summary>
        /// Writes a sbyte array to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified sbyte array to the matrix at the specified position.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int row, int col, sbyte[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutB(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of sbyte values to the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of sbyte values to the matrix at the provided row and column indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int row, int col, sbyte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutB(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a sbyte array to the matrix at the specified multi-dimensional indices.
        /// </summary>
        /// <param name="idx">The array of indices specifying the position in the matrix where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified array of sbyte array to the matrix at the given multi-dimensional indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of provided indices does not match the matrix's dimensions, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, sbyte[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of sbyte values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of sbyte values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, sbyte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of sbyte values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of sbyte values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int row, int col, sbyte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutBwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of sbyte values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The sbyte array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of sbyte values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, sbyte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nPutBwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region ushort

        /// <summary>
        /// Writes an ushort array to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified ushort array to the matrix at the given row and column.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, ushort[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutS(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of ushort values to the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of ushort values to the matrix at the provided row and column indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, ushort[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutS(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of ushort values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of ushort values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, ushort[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes an ushort array to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified nsigned short array to the matrix at the given indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices does not match the matrix's dimensionality,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, ushort[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of ushort values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of ushort values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, ushort[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of ushort values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The ushort array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of ushort values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, ushort[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region short

        /// <summary>
        /// Writes a specified number of short values to the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of short values to the matrix at the provided row and column indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, short[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutS(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of short values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of short values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, short[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of short values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of short values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, short[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of short values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of short values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_16U</c> or <c>CV_16S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, short[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nPutSwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region int

        /// <summary>
        /// Writes a specified number of int values to the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of int values to the matrix at the provided row and column indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int row, int col, int[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nPutI(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of int values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of int values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int row, int col, int[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nPutIwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of int values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of int values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int[] idx, int[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nPutIIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of int values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of int values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int[] idx, int[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nPutIwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region float

        /// <summary>
        /// Writes a specified number of float values to the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of float values to the matrix at the provided row and column indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32F</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int row, int col, float[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nPutF(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of float values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of float values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32F</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int row, int col, float[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nPutFwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of float values to the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of float values to the matrix at the provided indices.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32F</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int[] idx, float[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nPutFIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a specified number of float values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of float values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_32F</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int[] idx, float[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nPutFwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region double

        /// <summary>
        /// Writes a specified number of double values to the matrix at the given row and column, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of double values to the matrix at the provided row and column indices.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, double[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            return core_Mat_nPutD(nativeObj, row, col, length, data);

        }

        /// <summary>
        /// Writes a specified number of double values to the matrix at the given row and column, starting from the specified offset within the data array, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of double values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, double[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            return core_Mat_nPutDwOffset(nativeObj, row, col, length, offset, data);

        }

        /// <summary>
        /// Writes a specified number of double values to the matrix at the given indices, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">An array specifying the multi-dimensional indices where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of double values to the matrix at the provided indices indices.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, double[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            return core_Mat_nPutDIdx(nativeObj, idx, idx.Length, length, data);

        }

        /// <summary>
        /// Writes a specified number of double values to the matrix at the given indices, starting from the specified offset within the data array, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">An array specifying the multi-dimensional indices where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of double values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, double[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            return core_Mat_nPutDwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);

        }

        #endregion

        #endregion

        #region get_Array_length_offset

        #region byte

        /// <summary>
        /// Reads a specified number of byte values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of byte values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, byte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetB(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of byte values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of byte values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, byte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of byte values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of byte values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, byte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of byte values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of byte values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, byte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region sbyte

        /// <summary>
        /// Reads a sbyte array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified sbyte array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, sbyte[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetB(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of sbyte values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of sbyte values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, sbyte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetB(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of sbyte values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of sbyte values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, sbyte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a sbyte array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified sbyte array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, sbyte[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of sbyte values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of sbyte values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, sbyte[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of sbyte values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The sbyte array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of sbyte values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, sbyte[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                return core_Mat_nGetBwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region ushort

        /// <summary>
        /// Reads an ushort array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified ushort array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, ushort[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetS(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of ushort values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of ushort values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, ushort[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetS(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of ushort values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of ushort values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, ushort[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads an ushort array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified ushort array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, ushort[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of ushort values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of ushort values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, ushort[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of ushort values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The ushort array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of ushort values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, ushort[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region short

        /// <summary>
        /// Reads a specified number of short values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of short values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, short[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetS(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of short values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of short values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, short[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of short values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of short values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, short[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of short values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of short values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, short[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                return core_Mat_nGetSwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region int

        /// <summary>
        /// Reads a specified number of int values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of int values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception
        public int get(int row, int col, int[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nGetI(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of int values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of int values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int get(int row, int col, int[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nGetIwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of int values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of int values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int get(int[] idx, int[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nGetIIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of int values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of int values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int get(int[] idx, int[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                return core_Mat_nGetIwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region float

        /// <summary>
        /// Reads a specified number of float values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of float values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int row, int col, float[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nGetF(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of float values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of float values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int row, int col, float[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nGetFwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of float values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of float values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int[] idx, float[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nGetFIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of float values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of float values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int[] idx, float[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                return core_Mat_nGetFwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region double

        /// <summary>
        /// Reads a specified number of double values from the matrix at the given row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of double values from the matrix at the given row and column.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int row, int col, double[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                return core_Mat_nGetD(nativeObj, row, col, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of double values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of double values from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int row, int col, double[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                return core_Mat_nGetDwOffset(nativeObj, row, col, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of double values from the matrix at the given indices.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of double values from the matrix at the given indices.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int[] idx, double[] data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                return core_Mat_nGetDIdx(nativeObj, idx, idx.Length, length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a specified number of double values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <param name="idx">The array of indices corresponding to the dimensions of the matrix.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <param name="offset">The starting point within the array from which the data will be placed.</param>
        /// <param name="length">The number of elements to read from the matrix.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified number of double values from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The number of elements specified by <paramref name="length"/> must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int[] idx, double[] data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                return core_Mat_nGetDwIdxOffset(nativeObj, idx, idx.Length, length, offset, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #endregion


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region put_Span

        #region byte

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<byte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutB(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<byte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutB(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<byte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutBwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>

        public int put(int[] idx, ReadOnlySpan<byte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>

        public int put(int[] idx, ReadOnlySpan<byte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of byte data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of byte data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of byte data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<byte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nPutBwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region sbyte

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<sbyte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutB(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<sbyte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutB(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<sbyte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutBwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<sbyte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<sbyte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutBIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of sbyte data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of sbyte data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of sbyte data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<sbyte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nPutBwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region ushort

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<ushort> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutS(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<ushort> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutS(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<ushort> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutSwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<ushort> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<ushort> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of ushort data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of ushort data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of ushort data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<ushort> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nPutSwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region short

        /// <summary>
        /// Writes a span of short data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<short> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutS(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of short data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<short> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutS(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of short data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<short> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutSwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of short data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<short> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of short data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<short> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutSIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of short data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of short data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of short data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<short> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nPutSwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region int

        /// <summary>
        /// Writes a span of int data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<int> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutI(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of int data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<int> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutI(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of int data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<int> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutIwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of int data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<int> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutIIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of int data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<int> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutIIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of int data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of int data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of int data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<int> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nPutIwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region float

        /// <summary>
        /// Writes a span of float data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<float> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutF(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of float data to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given row and column.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<float> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutF(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of float data to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<float> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutFwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of float data to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<float> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutFIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of float data to the matrix at the specified indices with a specified length.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given indices.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<float> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutFIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Writes a span of float data to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of float data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of float data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The length of the span written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<float> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nPutFwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region double

        /// <summary>
        /// Writes a span of double data to the matrix at the specified row and column, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given row and column.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<double> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutD(nativeObj, row, col, data.Length, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of double data to the matrix at the specified row and column, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given row and column
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<double> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutD(nativeObj, row, col, length, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of double data to the matrix at the specified row and column, starting from the specified offset within the data array, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, ReadOnlySpan<double> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutDwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of double data to the matrix at the specified indices, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given indices.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<double> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutDIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                }
            }
        }

        /// <summary>
        /// Writes a span of double data to the matrix at the specified indices with a specified length, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given indices.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<double> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutDIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of double data to the matrix at the specified indices, starting from the specified offset within the data span, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of double data to write to the matrix.</param>
        /// <param name="offset">The offset into the span from which to start writing data.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of double data to the matrix at the given indices, starting from the specified offset within the data span.
        /// The values are automatically cast to match the matrix's data type.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, ReadOnlySpan<double> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            unsafe
            {
                fixed (double* ptr = data)
                {
                    return core_Mat_nPutDwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                }
            }

        }

        #endregion

        #endregion

        #region get_Span

        #region byte

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<byte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetB(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<byte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetB(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<byte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetBwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<byte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<byte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of byte data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of byte data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of byte data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<byte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        return core_Mat_nGetBwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region sbyte

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<sbyte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetB(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<sbyte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetB(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<sbyte> data, int offset, int length)
        {
            ThrowIfDisposed();

            if (length < 0)
                length = data.Length;

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetBwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<sbyte> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<sbyte> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetBIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of sbyte data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of sbyte data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of sbyte data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<sbyte> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_8U || CvType.depth(t) == CvType.CV_8S)
            {
                unsafe
                {
                    fixed (sbyte* ptr = data)
                    {
                        return core_Mat_nGetBwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region ushort

        /// <summary>
        /// Reads a span of ushort data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<ushort> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetS(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of ushort data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<ushort> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetS(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of ushort data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<ushort> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetSwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a ushort of byte data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<ushort> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of ushort data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<ushort> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of ushort data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of ushort data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of ushort data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<ushort> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (ushort* ptr = data)
                    {
                        return core_Mat_nGetSwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region short

        /// <summary>
        /// Reads a span of short data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<short> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetS(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of short data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<short> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetS(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of short data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<short> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetSwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of short data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<short> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of short data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<short> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetSIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of short data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of short data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of short data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<short> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_16U || CvType.depth(t) == CvType.CV_16S)
            {
                unsafe
                {
                    fixed (short* ptr = data)
                    {
                        return core_Mat_nGetSwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region int

        /// <summary>
        /// Reads a span of int data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<int> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetI(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of int data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<int> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetI(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of int data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<int> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetIwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of int data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<int> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetIIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of int data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<int> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetIIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of int data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of int data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of int data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<int> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32S)
            {
                unsafe
                {
                    fixed (int* ptr = data)
                    {
                        return core_Mat_nGetIwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region float

        /// <summary>
        /// Reads a span of float data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<float> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetF(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of float data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<float> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetF(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of float data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<float> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetFwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of float data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<float> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetFIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of float data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<float> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetFIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of float data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of float data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of float data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<float> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_32F)
            {
                unsafe
                {
                    fixed (float* ptr = data)
                    {
                        return core_Mat_nGetFwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #region double

        /// <summary>
        /// Reads a span of double data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<double> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetD(nativeObj, row, col, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of double data from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given row and column.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<double> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetD(nativeObj, row, col, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of double data from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int row, int col, Span<double> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetDwOffset(nativeObj, row, col, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of double data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<double> data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : data.Length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetDIdx(nativeObj, idx, idx.Length, data.Length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of double data from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given indices.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<double> data, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetDIdx(nativeObj, idx, idx.Length, length, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        /// <summary>
        /// Reads a span of double data from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of double data to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of double data from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The length of the span read must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the length of the provided span is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not compatible.
        /// </exception>
        public int get(int[] idx, Span<double> data, int offset, int length)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || length % CvType.channels(t) != 0)
                throw new CvException(
                        "Provided data element number (" +
                                (data == null ? 0 : length) +
                                ") should be multiple of the Mat channels count (" +
                                CvType.channels(t) + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            if (CvType.depth(t) == CvType.CV_64F)
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        return core_Mat_nGetDwIdxOffset(nativeObj, idx, idx.Length, length, offset, (IntPtr)ptr);
                    }
                }
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        #endregion

        #endregion

#endif


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SizeofT<T>() where T : unmanaged
        {
#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                return sizeof(T);
            }
#else
            return Marshal.SizeOf<T>();
#endif
        }

        #region put_Array_T

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int, int, int, IntPtr, int> core_Mat_nPutNFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nPutB;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nPutS;
                case CvType.CV_32S:
                    return core_Mat_nPutI;
                case CvType.CV_32F:
                    return core_Mat_nPutF;
                case CvType.CV_64F:
                    return core_Mat_nPutDNoCast;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int, int, int, int, IntPtr, int> core_Mat_nPutNwOffsetFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nPutBwOffset;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nPutSwOffset;
                case CvType.CV_32S:
                    return core_Mat_nPutIwOffset;
                case CvType.CV_32F:
                    return core_Mat_nPutFwOffset;
                case CvType.CV_64F:
                    return core_Mat_nPutDwOffsetNoCast;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int[], int, int, IntPtr, int> core_Mat_nPutNIdxFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nPutBIdx;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nPutSIdx;
                case CvType.CV_32S:
                    return core_Mat_nPutIIdx;
                case CvType.CV_32F:
                    return core_Mat_nPutFIdx;
                case CvType.CV_64F:
                    return core_Mat_nPutDIdxNoCast;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int[], int, int, int, IntPtr, int> core_Mat_nPutNwIdxOffsetFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nPutBwIdxOffset;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nPutSwIdxOffset;
                case CvType.CV_32S:
                    return core_Mat_nPutIwIdxOffset;
                case CvType.CV_32F:
                    return core_Mat_nPutFwIdxOffset;
                case CvType.CV_64F:
                    return core_Mat_nPutDwIdxOffsetNoCast;
                default:
                    break;
            }

            return default;
        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified array of type <typeparamref name="T"/> to the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible
        /// with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, T[] data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNFunc = core_Mat_nPutNFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified array of type <typeparamref name="T"/> to the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, T[] data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNFunc = core_Mat_nPutNFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified array of type <typeparamref name="T"/> to the matrix at the given row and column, starting from the specified offset within the data array.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, T[] data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNwOffsetFunc = core_Mat_nPutNwOffsetFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified array of type <typeparamref name="T"/> to the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible
        /// with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, T[] data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNIdxFunc = core_Mat_nPutNIdxFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified array of type <typeparamref name="T"/> to the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, T[] data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNIdxFunc = core_Mat_nPutNIdxFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Writes an array of type <typeparamref name="T"/> to the matrix at the specified indices, starting from the specified offset within the data array.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified array of type <typeparamref name="T"/> to the matrix at the given indices, starting from the specified offset within the data array.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, T[] data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNwIdxOffsetFunc = core_Mat_nPutNwIdxOffsetFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nPutNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        #endregion

        #region get_Array_T

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int, int, int, IntPtr, int> core_Mat_nGetNFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nGetB;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nGetS;
                case CvType.CV_32S:
                    return core_Mat_nGetI;
                case CvType.CV_32F:
                    return core_Mat_nGetF;
                case CvType.CV_64F:
                    return core_Mat_nGetD;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int, int, int, int, IntPtr, int> core_Mat_nGetNwOffsetFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nGetBwOffset;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nGetSwOffset;
                case CvType.CV_32S:
                    return core_Mat_nGetIwOffset;
                case CvType.CV_32F:
                    return core_Mat_nGetFwOffset;
                case CvType.CV_64F:
                    return core_Mat_nGetDwOffset;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int[], int, int, IntPtr, int> core_Mat_nGetNIdxFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nGetBIdx;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nGetSIdx;
                case CvType.CV_32S:
                    return core_Mat_nGetIIdx;
                case CvType.CV_32F:
                    return core_Mat_nGetFIdx;
                case CvType.CV_64F:
                    return core_Mat_nGetDIdx;
                default:
                    break;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<IntPtr, int[], int, int, int, IntPtr, int> core_Mat_nGetNwIdxOffsetFuncOfMatDepth(int depth)
        {
            switch (depth)
            {
                case CvType.CV_8U:
                case CvType.CV_8S:
                    return core_Mat_nGetBwIdxOffset;
                case CvType.CV_16U:
                case CvType.CV_16S:
                    return core_Mat_nGetSwIdxOffset;
                case CvType.CV_32S:
                    return core_Mat_nGetIwIdxOffset;
                case CvType.CV_32F:
                    return core_Mat_nGetFwIdxOffset;
                case CvType.CV_64F:
                    return core_Mat_nGetDwIdxOffset;
                default:
                    break;
            }

            return default;
        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, T[] data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNFunc = core_Mat_nGetNFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, T[] data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNFunc = core_Mat_nGetNFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified row and column, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="offset">The offset within the array at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given row and column, storing them in the data array starting at the specified offset.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, T[] data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNwOffsetFunc = core_Mat_nGetNwOffsetFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, T[] data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNIdxFunc = core_Mat_nGetNIdxFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, T[] data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNIdxFunc = core_Mat_nGetNIdxFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads an array of type <typeparamref name="T"/> from the matrix at the specified indices, storing them in the data array starting at the specified offset.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The array of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="offset">The offset within the array at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified array of type <typeparamref name="T"/> from the matrix at the given indices, storing them in the data array starting at the specified offset.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, T[] data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNwIdxOffsetFunc = core_Mat_nGetNwIdxOffsetFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            int bytesNum = core_Mat_nGetNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
            return bytesNum;
#endif

        }

        /// <summary>
        /// Reads the element values from the matrix at the specified row and column, returning them as an array of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <returns>
        /// An array of type <typeparamref name="T"/> representing the matrix element(s) at the specified row and column.
        /// The size of the returned array corresponds to the number of matrix element bytes divided by the size of type <typeparamref name="T"/>.
        /// If the read operation fails, returns null.
        /// </returns>
        /// <remarks>
        /// This method reads the element values at the specified row and column in the matrix and returns them as values of type <typeparamref name="T"/>.
        /// Regardless of the matrix's actual data type, the values are cast to the specified type <typeparamref name="T"/>.
        /// The size of the returned array depends on how many values of type <typeparamref name="T"/> can fit into the total byte size of the matrix element.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public T[] get<T>(int row, int col) where T : unmanaged
        {

            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if ((int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    sizeof_T +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            T[] tmpArray = new T[(int)elemSize() / sizeof_T];
            int bytesNum = default;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNFunc = core_Mat_nGetNFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = tmpArray)
                {
                    bytesNum = core_Mat_nGetNFunc(nativeObj, row, col, tmpArray.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(tmpArray, GCHandleType.Pinned);
            bytesNum = core_Mat_nGetNFunc(nativeObj, row, col, tmpArray.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
#endif

            if (bytesNum == 0)
            {
                return null;
            }
            else
            {
                return tmpArray;
            }

        }

        /// <summary>
        /// Reads the element values from the matrix at the specified indices, returning them as an array of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <returns>
        /// An array of type <typeparamref name="T"/> representing the matrix element(s) at the specified indices.
        /// The size of the returned array corresponds to the number of matrix element bytes divided by the size of type <typeparamref name="T"/>.
        /// If the read operation fails, returns null.
        /// </returns>
        /// <remarks>
        /// This method reads the element values at the specified indices in the matrix and returns them as values of type <typeparamref name="T"/>.
        /// Regardless of the matrix's actual data type, the values are cast to the specified type <typeparamref name="T"/>.
        /// The size of the returned array depends on how many values of type <typeparamref name="T"/> can fit into the total byte size of the matrix element.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public T[] get<T>(int[] idx) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if ((int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    sizeof_T +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            T[] tmpArray = new T[(int)elemSize() / sizeof_T];
            int bytesNum = default;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNIdxFunc = core_Mat_nGetNIdxFuncOfMatDepth(CvType.depth(type()));

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = tmpArray)
                {
                    bytesNum = core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, tmpArray.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(tmpArray, GCHandleType.Pinned);
            bytesNum = core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, tmpArray.Length * sizeof_T / sizeof_MatDepth, arrayHandle.AddrOfPinnedObject());
            arrayHandle.Free();
#endif

            if (bytesNum == 0)
            {
                return null;
            }
            else
            {
                return tmpArray;
            }

        }

        #endregion

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region put_Span_T

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of type <typeparamref name="T"/> to the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible
        /// with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, ReadOnlySpan<T> data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNFunc = core_Mat_nPutNFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified span of type <typeparamref name="T"/> to the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, ReadOnlySpan<T> data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNFunc = core_Mat_nPutNFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified row and column, starting from the specified offset within the data span.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="row">The row index in the matrix where the data will be written.</param>
        /// <param name="col">The column index in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="offset">The offset in the data span from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified span of type <typeparamref name="T"/> to the matrix at the given row and column, starting from the specified offset within the data span.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int row, int col, ReadOnlySpan<T> data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNwOffsetFunc = core_Mat_nPutNwOffsetFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified span of type <typeparamref name="T"/> to the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible
        /// with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, ReadOnlySpan<T> data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNIdxFunc = core_Mat_nPutNIdxFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified span of type <typeparamref name="T"/> to the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, ReadOnlySpan<T> data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNIdxFunc = core_Mat_nPutNIdxFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Writes a span of type <typeparamref name="T"/> to the matrix at the specified indices, starting from the specified offset within the data span.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be written.</typeparam>
        /// <param name="idx">The indices in the matrix where the data will be written.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to write to the matrix.</param>
        /// <param name="offset">The offset in the data span from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the span.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes a specified span of type <typeparamref name="T"/> to the matrix at the given indices, starting from the specified offset within the data span.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int put<T>(int[] idx, ReadOnlySpan<T> data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nPutNwIdxOffsetFunc = core_Mat_nPutNwIdxOffsetFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nPutNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        #endregion

        #region get_Span_T

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, Span<T> data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNFunc = core_Mat_nGetNFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNFunc(nativeObj, row, col, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified row and column.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given row and column.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, Span<T> data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            length = (length < 0) ? 0 : (length > data.Length) ? data.Length : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNFunc = core_Mat_nGetNFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified row and column, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="row">The row index in the matrix from which to read data.</param>
        /// <param name="col">The column index in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given row and column, storing them in the data span starting at the specified offset.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int row, int col, Span<T> data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNwOffsetFunc = core_Mat_nGetNwOffsetFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNwOffsetFunc(nativeObj, row, col, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, Span<T> data) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNIdxFunc = core_Mat_nGetNIdxFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, data.Length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified indices.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given indices.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, Span<T> data, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNIdxFunc = core_Mat_nGetNIdxFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNIdxFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        /// <summary>
        /// Reads a span of type <typeparamref name="T"/> from the matrix at the specified indices, storing them in the data span starting at the specified offset.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the data to be read.</typeparam>
        /// <param name="idx">The indices in the matrix from which to read data.</param>
        /// <param name="data">The span of type <typeparamref name="T"/> to store the read values.</param>
        /// <param name="offset">The offset within the span at which to start storing the read data.</param>
        /// <param name="length">The number of elements to read.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified span of type <typeparamref name="T"/> from the matrix at the given indices, storing them in the data span starting at the specified offset.
        /// The size of the type <typeparamref name="T"/> must be compatible with the matrix element size.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if the size of the provided type <typeparamref name="T"/> is not compatible with the matrix element size.
        /// </exception>
        public int get<T>(int[] idx, Span<T> data, int offset, int length) where T : unmanaged
        {
            ThrowIfDisposed();

            int sizeof_T = SizeofT<T>();
            if (data == null || (int)elemSize() % sizeof_T != 0)
                throw new CvException(
                    "Provided data element size (" +
                    (data == null ? 0 : sizeof_T) +
                    ") should be multiple of the Mat element size (" +
                    elemSize() + ")");
            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");

            offset = (offset < 0) ? 0 : (offset > data.Length) ? data.Length : offset;
            length = (length < 0) ? 0 : (offset + length > data.Length) ? data.Length - offset : length;

            int sizeof_MatDepth = (int)elemSize1();
            var core_Mat_nGetNwIdxOffsetFunc = core_Mat_nGetNwIdxOffsetFuncOfMatDepth(CvType.depth(type()));

            unsafe
            {
                fixed (T* ptr = data)
                {
                    return core_Mat_nGetNwIdxOffsetFunc(nativeObj, idx, idx.Length, length * sizeof_T / sizeof_MatDepth, offset * sizeof_T / sizeof_MatDepth, (IntPtr)ptr);
                }
            }

        }

        #endregion

#endif

        #region ptr

        /// <summary>
        /// Returns a pointer to the specified row of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the data at the specified row index in a multi-dimensional OpenCV Mat object.
        /// It performs basic checks to ensure the Mat has not been disposed, but does not perform bounds checking on the specified index.
        ///
        /// The pointer returned points to the first element of the specified row, allowing for direct manipulation of the underlying data.
        ///
        /// <strong>Note:</strong> If the row index is out of bounds, an invalid or out-of-bounds pointer may be returned, leading to undefined behavior.
        /// </remarks>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <returns>
        /// A pointer (IntPtr) to the data at the specified row of the Mat.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr ptr(int i0)
        {
            ThrowIfDisposed();

#if OPENCV_DBGASSERT
            int dims = this.dims();
            if (dims < 2)
                throw new CvException("dims() < 2");

            if (i0 < 0 || i0 >= size(0))
                throw new CvException("i0 < 0 || i0 >= size(0)");
#endif

            return new IntPtr(core_Mat_n_1ptr__JI(nativeObj, i0));
        }

        /// <summary>
        /// Returns a pointer to the specified row and column of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the data at the specified row and column indices in a multi-dimensional OpenCV Mat object.
        /// It ensures that the Mat is valid and not disposed, but does not perform bounds checking on the specified indices.
        ///
        /// The pointer returned points to the first element at the specified row and column, enabling direct manipulation of the underlying data.
        ///
        /// <strong>Note:</strong> If the indices are out of bounds, an invalid or out-of-bounds pointer may be returned, leading to undefined behavior.
        /// </remarks>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <param name="i1">
        /// The column index of the Mat from which the pointer should be returned. Must be within the valid range of columns.
        /// </param>
        /// <returns>
        /// A pointer (IntPtr) to the data at the specified row and column of the Mat.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr ptr(int i0, int i1)
        {
            ThrowIfDisposed();

#if OPENCV_DBGASSERT
            int dims = this.dims();
            if (dims < 2)
                throw new CvException("dims() < 2");

            if (i0 < 0 || i0 >= size(0))
                throw new CvException("i0 < 0 || i0 >= size(0)");

            if (i1 < 0 || i1 >= size(1))
                throw new CvException("i1 < 0 || i1 >= size(1)");
#endif

            return new IntPtr(core_Mat_n_1ptr__JII(nativeObj, i0, i1));
        }

        /// <summary>
        /// Returns a pointer to the specified element of the Mat object based on the given row, column, and third dimension indices.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the data at the specified row, column, and third dimension indices in a multi-dimensional OpenCV Mat object.
        /// It verifies that the Mat is not disposed but does not check the validity of the indices provided.
        ///
        /// The pointer returned allows direct manipulation of the data at the specified coordinates.
        ///
        /// <strong>Note:</strong> If any of the indices are out of bounds, an invalid or out-of-bounds pointer may be returned, resulting in undefined behavior.
        /// </remarks>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <param name="i1">
        /// The column index of the Mat from which the pointer should be returned. Must be within the valid range of columns.
        /// </param>
        /// <param name="i2">
        /// The third dimension index of the Mat from which the pointer should be returned. Must be within the valid range of third dimension.
        /// </param>
        /// <returns>
        /// A pointer (IntPtr) to the data at the specified row, column, and third dimension of the Mat.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr ptr(int i0, int i1, int i2)
        {
            ThrowIfDisposed();

#if OPENCV_DBGASSERT
            int dims = this.dims();
            if (dims < 3)
                throw new CvException("dims() < 3");

            if (i0 < 0 || i0 >= size(0))
                throw new CvException("i0 < 0 || i0 >= size(0)");

            if (i1 < 0 || i1 >= size(1))
                throw new CvException("i1 < 0 || i1 >= size(1)");

            if (i2 < 0 || i2 >= size(2))
                throw new CvException("i2 < 0 || i2 >= size(2)");
#endif

            return new IntPtr(core_Mat_n_1ptr__JIII(nativeObj, i0, i1, i2));
        }

        /// <summary>
        /// Returns a pointer to the specified element of the Mat object based on the provided indices for each dimension.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the data at the specified multi-dimensional indices in an OpenCV Mat object.
        /// It ensures that the Mat is valid and not disposed, but does not perform bounds checking on the provided indices.
        ///
        /// The pointer returned allows direct manipulation of the data at the coordinates specified by the indices.
        ///
        /// <strong>Note:</strong> If any of the indices are out of bounds, an invalid or out-of-bounds pointer may be returned, leading to undefined behavior.
        /// Ensure that the provided indices are within the valid range for each dimension of the Mat.
        /// </remarks>
        /// <param name="idx">
        /// An array of indices specifying the coordinates in the Mat across all dimensions. Each index must be within the valid range of its respective dimension.
        /// </param>
        /// <returns>
        /// A pointer (IntPtr) to the data at the specified coordinates in the Mat.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr ptr(params int[] idx)
        {
            ThrowIfDisposed();

            if (idx == null || idx.Length == 0)
                throw new CvException("idx == null || idx.Length == 0");

            int dims = this.dims();
            int idx_length = idx.Length;

#if OPENCV_DBGASSERT
            if (dims < idx_length)
                throw new CvException("dims < idx.Length");

            for (int i = 0; i < idx_length; ++i)
            {
                if (idx[i] < 0 || idx[i] >= size(i))
                    throw new CvException("idx[" + i + "] < 0 || idx[" + i + "] >= size(" + i + ")");
            }
#endif

#if NET_STANDARD_2_1
            int[] _idx = ArrayPool<int>.Shared.Rent(dims);
            Array.Clear(_idx, 0, dims);
#else
            int[] _idx = new int[dims];
#endif
            for (int i = 0; i < idx_length; ++i)
            {
                _idx[i] = idx[i];
            }

            IntPtr ptr = new IntPtr(core_Mat_n_1ptr__J_3II(nativeObj, _idx, dims));

#if NET_STANDARD_2_1
            ArrayPool<int>.Shared.Return(_idx);
#endif

            return ptr;
        }

        #endregion


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region at

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the first element in the specified row of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element in the specified row (i0) of the Mat object, and uses that pointer to create a Span&lt;T&gt;.
        /// The length of the Span&lt;T&gt; is determined by dividing the size of one element of the Mat by the size of type <typeparamref name="T"/>.
        ///
        /// <strong>Note:</strong> This method does not perform bounds checking on the index i0. If i0 is out of bounds, an invalid pointer may be dereferenced,
        /// which can lead to undefined behavior. Ensure that the index is within the valid range for the Mat object.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the first element of the specified row.
        /// </returns>
        public Span<T> at<T>(int i0) where T : unmanaged
        {
            ThrowIfDisposed();

            var p = ptr(i0);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() / sizeof(T));
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the first element in the specified row and column of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element in the specified row (i0) and column (i1) of the Mat object, and uses that pointer to create a Span&lt;T&gt;.
        /// The length of the Span&lt;T&gt; is determined by dividing the size of one element of the Mat by the size of type <typeparamref name="T"/>.
        ///
        /// <strong>Note:</strong> This method does not perform bounds checking on the indices i0 and i1. If any index is out of bounds, an invalid pointer may be dereferenced,
        /// which can lead to undefined behavior. Ensure that the indices are within the valid range for the Mat object.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <param name="i1">
        /// The column index of the Mat from which the pointer should be returned. Must be within the valid range of columns.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the first element of the specified row and column.
        /// </returns>
        public Span<T> at<T>(int i0, int i1) where T : unmanaged
        {
            ThrowIfDisposed();

            var p = ptr(i0, i1);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() / sizeof(T));
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the first element in the specified row, column, and third dimension of the Mat object
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element in the specified row (i0), column (i1), and third dimension (i2) of the Mat object, and uses that pointer to create a Span&lt;T&gt;.
        /// The length of the Span&lt;T&gt; is determined by dividing the size of one element of the Mat by the size of type <typeparamref name="T"/>.
        ///
        /// <strong>Note:</strong> This method does not perform bounds checking on the indices i0, i1, and i2. If any index is out of bounds, an invalid pointer may be dereferenced,
        /// which can lead to undefined behavior. Ensure that the indices are within the valid range for the Mat object.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="i0">
        /// The row index of the Mat from which the pointer should be returned. Must be within the valid range of rows.
        /// </param>
        /// <param name="i1">
        /// The column index of the Mat from which the pointer should be returned. Must be within the valid range of columns.
        /// </param>
        /// <param name="i2">
        /// The third dimension index of the Mat from which the pointer should be returned. Must be within the valid range of third dimension.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the first element of the specified row, column, and third dimension.
        /// </returns>
        public Span<T> at<T>(int i0, int i1, int i2) where T : unmanaged
        {
            ThrowIfDisposed();

            var p = ptr(i0, i1, i2);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() / sizeof(T));
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the first element in the specified coordinates of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element in the Mat object at the specified coordinates (idx), and uses that pointer to create a Span&lt;T&gt;.
        /// The length of the Span&lt;T&gt; is determined by dividing the size of one element of the Mat by the size of type <typeparamref name="T"/>.
        ///
        /// <strong>Note:</strong> This method does not perform bounds checking on the provided indices. If any index is out of bounds, an invalid pointer may be dereferenced,
        /// which can lead to undefined behavior. Ensure that the indices are within the valid range for the Mat object.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="idx">
        /// The indices of the Mat from which the pointer should be returned. Must be within the valid range for each dimension of the Mat.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the first element at the specified coordinates in the Mat.
        /// </returns>
        public Span<T> at<T>(params int[] idx) where T : unmanaged
        {
            ThrowIfDisposed();

            var p = ptr(idx);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() / sizeof(T));
            }
        }

        #endregion

#endif

#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region AsSpan

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the entire data of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element of the Mat object and uses it to create a Span&lt;T&gt; that spans the entire data stored in the Mat.
        /// The length of the Span&lt;T&gt; is calculated by dividing the total size of the Mat's data (in bytes) by the size of type <typeparamref name="T"/>.
        ///
        /// <para>
        /// <strong>Type Mapping:</strong>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Mat CVType</term>
        ///         <term>Description</term>
        ///         <term>Data Type</term>
        ///         <term>C# Type</term>
        ///         <term>VecStruct</term>
        ///     </listheader>
        ///     <item>
        ///         <description>CV_8U</description>
        ///         <description>Unsigned 8-bit integer</description>
        ///         <description>unsigned char</description>
        ///         <description>byte (System.Byte)</description>
        ///         <description>VecNb</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_8S</description>
        ///         <description>Signed 8-bit integer</description>
        ///         <description>signed char</description>
        ///         <description>sbyte (System.SByte)</description>
        ///         <description>VecNc</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_16U</description>
        ///         <description>Unsigned 16-bit integer</description>
        ///         <description>unsigned short</description>
        ///         <description>ushort (System.UInt16)</description>
        ///         <description>VecNw</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_16S</description>
        ///         <description>Signed 16-bit integer</description>
        ///         <description>short</description>
        ///         <description>short (System.Int16)</description>
        ///         <description>VecNs</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_32S</description>
        ///         <description>Signed 32-bit integer</description>
        ///         <description>int</description>
        ///         <description>int (System.Int32)</description>
        ///         <description>VecNi</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_32F</description>
        ///         <description>32-bit floating-point number</description>
        ///         <description>float</description>
        ///         <description>float (System.Single)</description>
        ///         <description>VecNf</description>
        ///     </item>
        ///     <item>
        ///         <description>CV_64F</description>
        ///         <description>64-bit floating-point number</description>
        ///         <description>double</description>
        ///         <description>double (System.Double)</description>
        ///         <description>VecNd</description>
        ///     </item>
        /// </list>
        /// </para>
        ///
        /// <strong>Note:</strong> This method only works with continuous Mat objects. If <see cref="Mat.isContinuous()"/> is <c>false</c>, an exception will be thrown.
        /// Ensure that the Mat is continuous before calling this method to avoid errors.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged to allow for direct memory access.
        /// </typeparam>
        /// <returns>
        /// A Span&lt;T&gt; that represents the entire data of the Mat object.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when <see cref="Mat.isContinuous()"/> is <c>false</c>, as this method requires a continuous Mat.
        /// </exception>
        public Span<T> AsSpan<T>() where T : unmanaged
        {
            ThrowIfDisposed();

            if (!isContinuous())
                throw new CvException("Mat.isContinuous() must be true.");

            var p = ptr(0);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)(elemSize() * total()) % sizeof(T) != 0)
                    throw new CvException("(int)(elemSize() * total()) % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)(elemSize() * total()) / sizeof(T));
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the data in the specified row or dimension of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element in the specified row (i0) or dimension (i0) of the Mat object, depending on the number of dimensions in the Mat,
        /// and uses that pointer to create a Span&lt;T&gt;.
        /// - For 2D Mats, it returns a pointer to the first element in row i0 and spans the entire row.
        /// - For Mats with more than 2 dimensions, it returns a pointer to the first element in dimension i0, assuming the Mat is continuous. If <see cref="Mat.isContinuous()"/> is <c>false</c>, an exception is thrown.
        ///
        /// The length of the Span&lt;T&gt; is determined by dividing the size of the data in the specified row or dimension by the size of type <typeparamref name="T"/>.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged to allow for direct memory access.
        /// </typeparam>
        /// <param name="i0">
        /// The index of the row or first dimension from which the pointer should be returned. Must be within the valid range for the Mat object.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the data in the specified row or dimension.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when <paramref name="i0"/> is out of the valid range of rows or dimensions.
        /// Thrown when <see cref="Mat.isContinuous()"/> is <c>false</c> for Mats with more than 2 dimensions.
        /// </exception>
        public Span<T> AsSpan<T>(int i0) where T : unmanaged
        {
            ThrowIfDisposed();

            int dims = this.dims();
            if (dims == 2)
            {
                if (i0 < 0 || i0 >= rows())
                    throw new CvException("i0 < 0 || i0 >= rows()");

                var p = ptr(i0);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() * cols() % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() * cols() % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() * cols() / sizeof(T));
                }
            }
            else
            {
                if (!isContinuous())
                    throw new CvException("Mat.isContinuous() must be true.");

                if (i0 < 0 || i0 >= size(0))
                    throw new CvException("i0 < 0 || i0 >= size(0)");

                int size1 = 1;
                for (int i = 1; i < dims; ++i)
                {
                    size1 *= size(i);
                }

                var p = ptr(i0);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() * size1 % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() * size1 % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() * size1 / sizeof(T));
                }
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the data at the specified row and column, or dimensions, of the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the element in the specified row (i0) and column (i1) for 2D Mats, or the first two dimensions (i0, i1) for higher-dimensional Mats,
        /// and uses that pointer to create a Span&lt;T&gt;.
        /// - For 2D Mats, it returns a pointer to the specific element at row i0 and column i1, returning a Span&lt;T&gt; representing that single element.
        /// - For Mats with more than 2 dimensions, it returns a pointer to the element at indices i0 and i1, and spans the rest of the data in the remaining dimensions. If <see cref="Mat.isContinuous()"/> is <c>false</c>, an exception is thrown.
        ///
        /// The length of the Span&lt;T&gt; is determined by dividing the size of the data in the Mat by the size of type <typeparamref name="T"/>. For 2D Mats, it spans just one element.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged to allow for direct memory access.
        /// </typeparam>
        /// <param name="i0">
        /// The row index or first dimension from which the pointer should be returned. Must be within the valid range for the Mat object.
        /// </param>
        /// <param name="i1">
        /// The column index or second dimension from which the pointer should be returned. Must be within the valid range for the Mat object.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the data at the specified row and column, or dimensions.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when <paramref name="i0"/> or <paramref name="i1"/> is out of the valid range of rows, columns, or dimensions.
        /// Thrown when <see cref="Mat.isContinuous()"/> is <c>false</c> for Mats with more than 2 dimensions.
        /// </exception>
        public Span<T> AsSpan<T>(int i0, int i1) where T : unmanaged
        {
            ThrowIfDisposed();

            int dims = this.dims();
            if (dims == 2)
            {
                if (i0 < 0 || i0 >= rows())
                    throw new CvException("i0 < 0 || i0 >= rows()");

                if (i1 < 0 || i1 >= cols())
                    throw new CvException("i1 < 0 || i1 >= cols()");

                var p = ptr(i0, i1);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() / sizeof(T));
                }
            }
            else
            {
                if (!isContinuous())
                    throw new CvException("Mat.isContinuous() must be true.");

                if (i0 < 0 || i0 >= size(0))
                    throw new CvException("i0 < 0 || i0 >= size(0)");

                if (i1 < 0 || i1 >= size(1))
                    throw new CvException("i1 < 0 || i1 >= size(1)");

                int size1 = 1;
                for (int i = 2; i < dims; ++i)
                {
                    size1 *= size(i);
                }

                var p = ptr(i0, i1);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() * size1 % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() * size1 % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() * size1 / sizeof(T));
                }
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the data at the specified element in the Mat object, starting from the given three-dimensional indices.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the element in the specified position (i0, i1, i2) of the Mat object and uses that pointer to create a Span&lt;T&gt;.
        /// The method works for Mats with 3 or more dimensions, spanning the data in the remaining dimensions if applicable.
        ///
        /// - For Mats with exactly 3 dimensions, it returns the pointer to the element at the indices (i0, i1, i2) and returns a Span&lt;T&gt; representing the element at that position.
        /// - For Mats with more than 3 dimensions, it returns the pointer at (i0, i1, i2) and spans the data across the remaining dimensions.
        /// If <see cref="Mat.isContinuous()"/> is <c>false</c>, an exception is thrown since continuous memory is required.
        ///
        /// The length of the Span&lt;T&gt; is determined by dividing the size of the data by the size of type <typeparamref name="T"/>. For 3D Mats, it spans one element, and for higher-dimensional Mats, it spans the remaining elements in the additional dimensions.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged to allow for direct memory access.
        /// </typeparam>
        /// <param name="i0">
        /// The index for the first dimension (typically representing rows). Must be within the valid range of the Mat's first dimension.
        /// </param>
        /// <param name="i1">
        /// The index for the second dimension (typically representing columns). Must be within the valid range of the Mat's second dimension.
        /// </param>
        /// <param name="i2">
        /// The index for the third dimension. Must be within the valid range of the Mat's third dimension.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the data starting at the specified element in the Mat object.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when <paramref name="i0"/>, <paramref name="i1"/>, or <paramref name="i2"/> are out of the valid range of the Mat's respective dimensions.
        /// Thrown when <see cref="Mat.isContinuous()"/> is <c>false</c> for Mats with more than 2 dimensions.
        /// Thrown when the Mat has fewer than 3 dimensions.
        /// </exception>
        public Span<T> AsSpan<T>(int i0, int i1, int i2) where T : unmanaged
        {
            ThrowIfDisposed();

            if (!isContinuous())
                throw new CvException("Mat.isContinuous() must be true.");

            int dims = this.dims();
            if (dims < 3)
                throw new CvException("dims() < 3");

            if (i0 < 0 || i0 >= size(0))
                throw new CvException("i0 < 0 || i0 >= size(0)");

            if (i1 < 0 || i1 >= size(1))
                throw new CvException("i1 < 0 || i1 >= size(1)");

            if (i2 < 0 || i2 >= size(2))
                throw new CvException("i2 < 0 || i2 >= size(2)");

            int size1 = 1;
            for (int i = 3; i < dims; ++i)
            {
                size1 *= size(i);
            }

            var p = ptr(i0, i1, i2);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() * size1 % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() * size1 % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() * size1 / sizeof(T));
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the data at the specified element in the Mat object based on a variable number of indices.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the element in the specified position, determined by the indices provided in the <paramref name="idx"/> array, and uses that pointer to create a Span&lt;T&gt;. The method adapts to Mats with 2 or more dimensions:
        ///
        /// - For a 2D Mat, if one index is provided, it returns a Span&lt;T&gt; for the specified row. If two indices are provided, it returns a Span&lt;T&gt; for the specified element.
        /// - For higher-dimensional Mats, it returns the pointer at the specified indices and spans the remaining dimensions.
        /// If <see cref="Mat.isContinuous()"/> is <c>false</c>, an exception is thrown since continuous memory is required.
        ///
        /// The length of the Span&lt;T&gt; is determined by dividing the size of the data by the size of type <typeparamref name="T"/>. For multi-dimensional Mats, it spans one element or the remaining elements in the additional dimensions.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged to allow for direct memory access.
        /// </typeparam>
        /// <param name="idx">
        /// An array of indices specifying the position of the element in the Mat object. The number of indices must match the number of dimensions in the Mat.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the data starting at the specified element in the Mat object.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when the number of dimensions in the Mat is less than the number of indices provided in <paramref name="idx"/>.
        /// Thrown when any index in <paramref name="idx"/> is out of bounds for the respective dimension.
        /// Thrown when <see cref="Mat.isContinuous()"/> is <c>false</c> for Mats with more than 2 dimensions.
        /// </exception>
        public Span<T> AsSpan<T>(params int[] idx) where T : unmanaged
        {
            ThrowIfDisposed();

            int dims = this.dims();
            int idx_length = idx.Length;
            if (dims == 2)
            {
                if (idx_length == 1)
                {
                    return AsSpan<T>(idx[0]);
                }
                else if (idx_length == 2)
                {
                    return AsSpan<T>(idx[0], idx[1]);
                }
            }

            if (!isContinuous())
            {
                throw new CvException("Mat.isContinuous() must be true.");
            }

            if (dims < idx_length)
                throw new CvException("dims < idx.Length");

            for (int i = 0; i < idx_length; ++i)
            {
                if (idx[i] < 0 || idx[i] >= size(i))
                    throw new CvException("idx[" + i + "] < 0 || idx[" + i + "] >= size(" + i + ")");
            }

            int size1 = 1;
            for (int i = idx_length; i < dims; ++i)
            {
                size1 *= size(i);
            }

            var p = ptr(idx);

            unsafe
            {
#if OPENCV_DBGASSERT
                if ((int)elemSize() * size1 % sizeof(T) != 0)
                    throw new CvException("(int)elemSize() * size1 % sizeof(T) != 0");
#endif

                return new Span<T>(p.ToPointer(), (int)elemSize() * size1 / sizeof(T));
            }
        }

        #endregion

        #region AsSpanRowRange

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing the data in the specified range of rows in the Mat object.
        /// </summary>
        /// <remarks>
        /// This method returns a pointer to the first element of the specified range of rows (from <paramref name="startrow"/> to <paramref name="endrow"/>),
        /// and uses that pointer to create a Span&lt;T&gt; that spans the specified rows.
        ///
        /// - If the difference between <paramref name="startrow"/> and <paramref name="endrow"/> is 1, it returns a Span&lt;T&gt; for that single row.
        /// - For multi-row ranges, the method requires the Mat object to be continuous in memory, as checked by <see cref="Mat.isContinuous()"/>.
        ///
        /// The length of the Span&lt;T&gt; is determined by the number of rows and the size of the elements in the Mat object, divided by the size of type <typeparamref name="T"/>.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="startrow">
        /// The index of the first row in the range to be spanned. Must be within the valid range of rows.
        /// </param>
        /// <param name="endrow">
        /// The index of the row after the last row to be spanned. Must be greater than <paramref name="startrow"/> and within the valid range of rows.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the data in the specified range of rows in the Mat object.
        /// </returns>
        /// <exception cref="CvException">
        /// Thrown when <paramref name="endrow"/> is less than or equal to <paramref name="startrow"/>, when either index is out of bounds, or when <see cref="Mat.isContinuous()"/> is <c>false</c> for multi-row ranges.
        /// </exception>
        public Span<T> AsSpanRowRange<T>(int startrow, int endrow) where T : unmanaged
        {
            ThrowIfDisposed();

            if (endrow - startrow == 1)
                return AsSpan<T>(startrow);

            if (!isContinuous())
            {
                throw new CvException("Mat.isContinuous() must be true.");
            }

            if (endrow < startrow)
                throw new CvException("endrow < startrow");
            if (endrow == startrow)
                throw new CvException("endrow == startrow");

            int dims = this.dims();
            if (dims == 2)
            {
                int rows = this.rows();
                if (startrow < 0 || startrow >= rows)
                    throw new CvException("startrow < 0 || startrow >= rows()");
                if (endrow < 0 || endrow >= rows)
                    throw new CvException("endrow < 0 || endrow >= rows()");

                var p = ptr(startrow);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() * cols() * (endrow - startrow) % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() * cols() * (endrow - startrow) % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() * cols() * (endrow - startrow) / sizeof(T));
                }
            }
            else
            {
                int size0 = size(0);
                if (startrow < 0 || startrow >= size0)
                    throw new CvException("startrow < 0 || startrow >= size(0)");
                if (endrow < 0 || endrow >= size0)
                    throw new CvException("endrow < 0 || endrow >= size(0)");

                int size1 = 1;
                for (int i = 1; i < dims; ++i)
                {
                    size1 *= size(i);
                }

                var p = ptr(startrow);

                unsafe
                {
#if OPENCV_DBGASSERT
                    if ((int)elemSize() * size1 * (endrow - startrow) % sizeof(T) != 0)
                        throw new CvException("(int)elemSize() * size1 * (endrow - startrow) % sizeof(T) != 0");
#endif

                    return new Span<T>(p.ToPointer(), (int)elemSize() * size1 * (endrow - startrow) / sizeof(T));
                }
            }
        }

        /// <summary>
        /// Creates and returns a Span&lt;T&gt; representing a range of rows from the Mat object, defined by the specified Range.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="AsSpanRowRange{T}(int, int)"/> internally, using the start and end indices from the provided Range.
        /// The resulting Span&lt;T&gt; includes the elements from the row specified by <c>r.Start</c> to the row before <c>r.End</c>.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span&lt;T&gt;. Must be unmanaged.
        /// </typeparam>
        /// <param name="r">
        /// The Range representing the start and end row indices.
        /// </param>
        /// <returns>
        /// A Span&lt;T&gt; representing the specified row range in the Mat object.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> AsSpanRowRange<T>(Range r) where T : unmanaged
        {
            return AsSpanRowRange<T>(r.start, r.end);
        }

        #endregion

#endif


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutB(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBIdx(IntPtr self, int[] idx, int lenght, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutB(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBIdx(IntPtr self, int[] idx, int lenght, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutS(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSIdx(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutS(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSIdx(IntPtr self, int[] idx, int length, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutI(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIIdx(IntPtr self, int[] idx, int length, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutF(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFIdx(IntPtr self, int[] idx, int length, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutD(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDIdx(IntPtr self, int[] idx, int length, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDNoCast(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDIdxNoCast(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwOffsetNoCast(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwIdxOffsetNoCast(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDNoCast(IntPtr self, int row, int col, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDIdxNoCast(IntPtr self, int[] idx, int length, int count, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwOffsetNoCast(IntPtr self, int row, int col, int count, int offset, IntPtr data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDwIdxOffsetNoCast(IntPtr self, int[] idx, int length, int count, int offset, IntPtr data);





        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetB(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] sbyte[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetB(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr vals);



        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetS(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ushort[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetS(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr vals);




        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetI(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr vals);




        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetF(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr vals);




        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDwOffset(IntPtr self, int row, int col, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);


        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetD(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDwOffset(IntPtr self, int row, int col, int count, int offset, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, IntPtr vals);





        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGet(IntPtr self, int row, int col, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIdx(IntPtr self, int[] idx, int length, int count, IntPtr vals);

        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1ptr__JI(IntPtr self, int i0);

        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1ptr__JII(IntPtr self, int i0, int i1);

        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1ptr__JIII(IntPtr self, int i0, int i1, int i2);

        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1ptr__J_3II(IntPtr self, int[] idx, int length);



        #region Operators

        // (here A, B stand for matrices ( Mat ), s for a scalar ( Scalar ), alpha for a real-valued scalar ( double ).)

        #region Unary

        #region -
        // Negation.
        // -A
        /// <summary>
        /// Negates all elements of the matrix.
        /// </summary>
        /// <param name="a">The input matrix to be negated.</param>
        /// <returns>A new matrix where each element is the negation of the corresponding element in the input matrix.</returns>
        /// <remarks>
        /// This operator negates all elements in the matrix <paramref name="a"/> by multiplying them by -1.
        /// A new <see cref="Mat"/> object is created and returned.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.multiply(A, Scalar.all(-1), A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator -(Mat a)
        {
            Mat m = new Mat();
            Core.multiply(a, Scalar.all(-1), m);
            return m;
        }
        #endregion

        #region ~
        // Bitwise not.
        // ~A
        /// <summary>
        /// Performs a bitwise NOT operation on the matrix.
        /// </summary>
        /// <param name="a">The input matrix.</param>
        /// <returns>A new matrix with each bit in the input matrix inverted.</returns>
        /// <remarks>
        /// This operator applies a bitwise NOT operation to the matrix <paramref name="a"/> and
        /// returns a new <see cref="Mat"/> object with each bit in the matrix inverted.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.bitwise_not(A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator ~(Mat a)
        {
            Mat m = new Mat();
            Core.bitwise_not(a, m);
            return m;
        }

        #endregion

        #endregion


        #region Binary

        #region +
        // Addition.
        // A + B, A + s, s + A
        // A += A, A += s
        /// <summary>
        /// Adds two matrices element-wise.
        /// </summary>
        /// <param name="a">The first input matrix.</param>
        /// <param name="b">The second input matrix.</param>
        /// <returns>A new matrix that is the sum of the two input matrices.</returns>
        /// <remarks>
        /// This operator performs element-wise addition of the matrices <paramref name="a"/> and <paramref name="b"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.add(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator +(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.add(a, b, m);
            return m;
        }

        /// <summary>
        /// Adds a scalar value to each element of the matrix.
        /// </summary>
        /// <param name="a">The input matrix.</param>
        /// <param name="s">The scalar value to add.</param>
        /// <returns>A new matrix that is the result of adding the scalar to each element of the input matrix.</returns>
        /// <remarks>
        /// This operator performs element-wise addition of the matrix <paramref name="a"/> and the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.add(A, s, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator +(Mat a, Scalar s)
        {
            Mat m = new Mat();
            Core.add(a, s, m);
            return m;
        }

        /// <summary>
        /// Adds a scalar value to each element of the matrix.
        /// </summary>
        /// <param name="s">The scalar value to add.</param>
        /// <param name="a">The input matrix.</param>
        /// <returns>A new matrix that is the result of adding the scalar to each element of the input matrix.</returns>
        /// <remarks>
        /// This operator performs element-wise addition of the scalar <paramref name="s"/> and the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.add(A, s, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator +(Scalar s, Mat a)
        {
            Mat m = new Mat();
            Core.add(a, s, m);
            return m;
        }
        #endregion

        #region -
        // Subtraction.
        // A - B, A - s, s - A
        // A -= A, A -= s
        /// <summary>
        /// Subtracts one matrix from another.
        /// </summary>
        /// <param name="a">The minuend matrix.</param>
        /// <param name="b">The subtrahend matrix.</param>
        /// <returns>A new matrix that is the result of subtracting <paramref name="b"/> from <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise subtraction of the matrix <paramref name="b"/> from the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.subtract(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator -(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.subtract(a, b, m);
            return m;
        }

        /// <summary>
        /// Subtracts a scalar value from each element of the matrix.
        /// </summary>
        /// <param name="a">The matrix from which the scalar will be subtracted.</param>
        /// <param name="s">The scalar value to subtract from each element of the matrix.</param>
        /// <returns>A new matrix that is the result of subtracting <paramref name="s"/> from each element of <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise subtraction of the scalar <paramref name="s"/> from the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.subtract(A, s, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator -(Mat a, Scalar s)
        {
            Mat m = new Mat();
            Core.subtract(a, s, m);
            return m;
        }

        /// <summary>
        /// Subtracts a scalar value from each element of the matrix.
        /// </summary>
        /// <param name="a">The matrix from which the scalar will be subtracted.</param>
        /// <param name="s">The scalar value to subtract from each element of the matrix.</param>
        /// <returns>A new matrix that is the result of subtracting <paramref name="s"/> from each element of <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise subtraction of the scalar <paramref name="s"/> from the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.subtract(B, A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator -(Scalar s, Mat a)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.subtract(b, a, m);
            }
            return m;
        }
        #endregion

        #region *
        // Matrix multiplication.
        // A * A
        /// <summary>
        /// Multiplies two matrices using the General Matrix Multiply (GEMM) operation.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>A new matrix that is the result of multiplying <paramref name="a"/> by <paramref name="b"/>.</returns>
        /// <remarks>
        /// This operator performs matrix multiplication of the matrices <paramref name="a"/> and <paramref name="b"/>
        /// using the GEMM function and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.gemm(A, B, 1, new Mat(), 0, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator *(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.gemm(a, b, 1, new Mat(), 0, m);
            return m;
        }

        // Scaling.
        // A * alpha, alpha * A
        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="a">The matrix to be multiplied.</param>
        /// <param name="s">The scalar value to multiply the matrix by.</param>
        /// <returns>A new matrix that is the result of multiplying <paramref name="a"/> by <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise multiplication of the matrix <paramref name="a"/> by the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.multiply(A, Scalar.all(alpha), A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator *(Mat a, double s)
        {
            Mat m = new Mat();
            Core.multiply(a, Scalar.all(s), m);
            return m;
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="a">The matrix to be multiplied.</param>
        /// <param name="s">The scalar value to multiply the matrix by.</param>
        /// <returns>A new matrix that is the result of multiplying <paramref name="a"/> by <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise multiplication of the matrix <paramref name="a"/> by the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.multiply(A, Scalar.all(alpha), A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator *(double s, Mat a)
        {
            Mat m = new Mat();
            Core.multiply(a, Scalar.all(s), m);
            return m;
        }
        #endregion

        #region /
        // Per-element multiplication and division.
        // A / A, alpha / A
        /// <summary>
        /// Divides one matrix by another.
        /// </summary>
        /// <param name="a">The dividend matrix.</param>
        /// <param name="b">The divisor matrix.</param>
        /// <returns>A new matrix that is the result of dividing <paramref name="a"/> by <paramref name="b"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise division of the matrix <paramref name="a"/> by the matrix <paramref name="b"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.divide(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator /(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.divide(a, b, m);
            return m;
        }

        /// <summary>
        /// Divides a scalar value by a matrix.
        /// </summary>
        /// <param name="s">The scalar value (dividend).</param>
        /// <param name="a">The divisor matrix.</param>
        /// <returns>A new matrix that is the result of dividing the scalar <paramref name="s"/> by the matrix <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator creates a new matrix filled with the scalar value <paramref name="s"/> and performs element-wise
        /// division of this matrix by <paramref name="a"/>. It returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), Scalar.all(alpha)); Core.divide(B, A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator /(double s, Mat a)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), Scalar.all(s)))
            {
                Core.divide(b, a, m);
            }
            return m;
        }

        // Scaling.
        // A / alpha
        /// <summary>
        /// Divides a matrix by a scalar value.
        /// </summary>
        /// <param name="a">The dividend matrix.</param>
        /// <param name="s">The scalar value (divisor).</param>
        /// <returns>A new matrix that is the result of dividing the matrix <paramref name="a"/> by the scalar <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs element-wise division of the matrix <paramref name="a"/> by the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.divide(A, Scalar.all(alpha), A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator /(Mat a, double s)
        {
            Mat m = new Mat();
            Core.divide(a, Scalar.all(s), m);
            return m;
        }
        #endregion

        #region &
        // Bitwise and.
        // A & A, A & s, s & A
        /// <summary>
        /// Performs a bitwise AND operation between two matrices.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise AND operation between <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise AND operation between the matrices <paramref name="a"/> and <paramref name="b"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.bitwise_and(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator &(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.bitwise_and(a, b, m);
            return m;
        }

        /// <summary>
        /// Performs a bitwise AND operation between a matrix and a scalar.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>A new matrix that is the result of the bitwise AND operation between <paramref name="a"/> and <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise AND operation between the matrix <paramref name="a"/> and the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_and(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator &(Mat a, Scalar s)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_and(a, b, m);
            }
            return m;
        }

        /// <summary>
        /// Performs a bitwise AND operation between a scalar and a matrix.
        /// </summary>
        /// <param name="s">The scalar value.</param>
        /// <param name="a">The matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise AND operation between <paramref name="s"/> and <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise AND operation between the scalar <paramref name="s"/> and the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_and(B, A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator &(Scalar s, Mat a)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_and(b, a, m);
            }
            return m;
        }
        #endregion

        #region |
        // Bitwise or.
        // A | A, A | s, s | A
        /// <summary>
        /// Performs a bitwise OR operation between two matrices.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise OR operation between <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise OR operation between the two matrices <paramref name="a"/> and <paramref name="b"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.bitwise_or(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator |(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.bitwise_or(a, b, m);
            return m;
        }

        /// <summary>
        /// Performs a bitwise OR operation between a matrix and a scalar value.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>A new matrix that is the result of the bitwise OR operation between <paramref name="a"/> and the scalar <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise OR operation between the matrix <paramref name="a"/> and a new matrix
        /// created from the scalar <paramref name="s"/>. The new matrix has the same size and type as <paramref name="a"/>.
        /// It returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_or(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator |(Mat a, Scalar s)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_or(a, b, m);
            }
            return m;
        }

        /// <summary>
        /// Performs a bitwise OR operation between a scalar value and a matrix.
        /// </summary>
        /// <param name="s">The scalar value.</param>
        /// <param name="a">The matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise OR operation between the scalar <paramref name="s"/> and the matrix <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise OR operation between a new matrix created from the scalar <paramref name="s"/>
        /// and the matrix <paramref name="a"/>. The new matrix has the same size and type as <paramref name="a"/>.
        /// It returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_or(B, A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator |(Scalar s, Mat a)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_or(b, a, m);
            }
            return m;
        }
        #endregion

        #region ^
        // Bitwise xor.
        // A ^ A, A ^ s, s ^ A
        /// <summary>
        /// Performs a bitwise XOR operation between two matrices.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise XOR operation between <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise XOR operation between the two matrices <paramref name="a"/> and <paramref name="b"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Core.bitwise_xor(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator ^(Mat a, Mat b)
        {
            Mat m = new Mat();
            Core.bitwise_xor(a, b, m);
            return m;
        }

        /// <summary>
        /// Performs a bitwise XOR operation between a matrix and a scalar value.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>A new matrix that is the result of the bitwise XOR operation between <paramref name="a"/> and <paramref name="s"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise XOR operation between the matrix <paramref name="a"/> and the scalar <paramref name="s"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_xor(A, B, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator ^(Mat a, Scalar s)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_xor(a, b, m);
            }
            return m;
        }

        /// <summary>
        /// Performs a bitwise XOR operation between a scalar value and a matrix.
        /// </summary>
        /// <param name="s">The scalar value.</param>
        /// <param name="a">The matrix.</param>
        /// <returns>A new matrix that is the result of the bitwise XOR operation between <paramref name="s"/> and <paramref name="a"/>.</returns>
        /// <remarks>
        /// This operator performs an element-wise bitwise XOR operation between the scalar <paramref name="s"/> and the matrix <paramref name="a"/>
        /// and returns a new <see cref="Mat"/> object containing the result.
        ///
        /// <strong>Note:</strong> In C++, the left-hand operand of compound assignment operators like "<c>A += B</c>" is reused, and operations such as "<c>Core.add(A, B, A)</c>" are performed internally.
        /// However, in C#, it is not possible to explicitly overload compound assignment operators.
        /// Instead, binary operator overloading is used implicitly, which results in a new Mat object being created and assigned to A each time an operator is used.
        /// This behavior leads to different memory management between C++ and C#.
        /// </remarks>
        [Obsolete("This operator creates a new Mat object, which is inefficient. Use 'Mat B = new Mat(A.size(), A.type(), s); Core.bitwise_xor(B, A, A);' instead to modify the existing matrix in-place for better performance.")]
        public static Mat operator ^(Scalar s, Mat a)
        {
            Mat m = new Mat();
            using (Mat b = new Mat(a.size(), a.type(), s))
            {
                Core.bitwise_xor(b, a, m);
            }
            return m;
        }
        #endregion

        #endregion

        #endregion

    }
}
