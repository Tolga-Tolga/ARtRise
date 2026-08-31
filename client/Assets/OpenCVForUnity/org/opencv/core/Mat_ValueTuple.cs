using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace OpenCVForUnity.CoreModule
{
    public partial class Mat : DisposableOpenCVObject
    {

        //
        // C++: Mat::Mat(Size size, int type)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="size">
        /// 2D array size: Size(cols, rows) . In the Size() constructor, the number of rows and the
        /// number of columns go in the reverse order.
        /// </param>
        /// <param name="type">
        /// Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
        /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.
        /// </param>
        public Mat(in (double width, double height) size, int type)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDI(size.width, size.height, type));

        }

        //
        // C++: Mat::Mat(int rows, int cols, int type, Scalar s)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="rows">
        /// Number of rows in a 2D array.
        /// </param>
        /// <param name="cols">
        /// Number of columns in a 2D array.
        /// </param>
        /// <param name="type">
        /// Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
        /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.
        /// </param>
        /// <param name="s">
        /// An optional value to initialize each matrix element with. To set all the matrix elements to
        /// the particular value after the construction, use the assignment operator
        /// Mat::operator=(const Scalar&amp; value) .
        /// </param>
        public Mat(int rows, int cols, int type, in (double v0, double v1, double v2, double v3) s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__IIIDDDD(rows, cols, type, s.v0, s.v1, s.v2, s.v3));

        }

        //
        // C++: Mat::Mat(Size size, int type, Scalar s)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="size">
        /// 2D array size: Size(cols, rows) . In the Size() constructor, the number of rows and the
        /// number of columns go in the reverse order.
        /// </param>
        /// <param name="type">
        /// Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
        /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.
        /// </param>
        /// <param name="s">
        /// An optional value to initialize each matrix element with. To set all the matrix elements to
        /// the particular value after the construction, use the assignment operator
        /// Mat::operator=(const Scalar&amp; value) .
        /// </param>
        public Mat(in (double width, double height) size, int type, in (double v0, double v1, double v2, double v3) s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDIDDDD(size.width, size.height, type, s.v0, s.v1, s.v2, s.v3));

        }

        //
        // C++: Mat::Mat(int ndims, const int* sizes, int type, Scalar s)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="sizes">
        /// Array of integers specifying an n-dimensional array shape.
        /// </param>
        /// <param name="type">
        /// Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
        /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.
        /// </param>
        /// <param name="s">
        /// An optional value to initialize each matrix element with. To set all the matrix elements to
        /// the particular value after the construction, use the assignment operator
        /// Mat::operator=(const Scalar&amp; value) .
        /// </param>
        public Mat(int[] sizes, int type, in (double v0, double v1, double v2, double v3) s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__I_3IIDDDD(sizes.Length, sizes, sizes.Length, type, s.v0, s.v1, s.v2, s.v3));

        }

        //
        // C++: Mat::Mat(Mat m, Range rowRange, Range colRange = Range::all())
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="m">
        /// Array that (as a whole or partly) is assigned to the constructed matrix. No data is copied
        /// by these constructors.Instead, the header pointing to m data or its sub-array is constructed and
        /// associated with it.The reference counter, if any, is incremented.So, when you modify the matrix
        /// formed using such a constructor, you also modify the corresponding elements of m.If you want to
        /// have an independent copy of the sub-array, use Mat::clone() .
        /// </param>
        /// <param name="rowRange">
        /// Range of the m rows to take. As usual, the range start is inclusive and the range
        /// end is exclusive.Use Range::all() to take all the rows.
        /// </param>
        /// <param name="colRange">
        /// colRange Range of the m columns to take. Use Range::all() to take all the columns.
        /// </param>
        public Mat(Mat m, in (int start, int end) rowRange, in (int start, int end) colRange)
        {
            if (m != null)
                m.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__JIIII(m.nativeObj, rowRange.start, rowRange.end, colRange.start, colRange.end));

        }

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="m">
        /// Array that (as a whole or partly) is assigned to the constructed matrix. No data is copied
        /// by these constructors.Instead, the header pointing to m data or its sub-array is constructed and
        /// associated with it.The reference counter, if any, is incremented.So, when you modify the matrix
        /// formed using such a constructor, you also modify the corresponding elements of m.If you want to
        /// have an independent copy of the sub-array, use Mat::clone() .
        /// </param>
        /// <param name="rowRange">
        /// Range of the m rows to take. As usual, the range start is inclusive and the range
        /// end is exclusive.Use Range::all() to take all the rows.
        /// </param>
        public Mat(Mat m, in (int start, int end) rowRange)
        {
            if (m != null)
                m.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__JII(m.nativeObj, rowRange.start, rowRange.end));

        }

        //
        // C++: Mat::Mat(const Mat& m, const std::vector<Range>& ranges)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="m">
        /// Array that (as a whole or partly) is assigned to the constructed matrix. No data is copied
        /// by these constructors.Instead, the header pointing to m data or its sub-array is constructed and
        /// associated with it.The reference counter, if any, is incremented.So, when you modify the matrix
        /// formed using such a constructor, you also modify the corresponding elements of m.If you want to
        /// have an independent copy of the sub-array, use Mat::clone() .
        /// </param>
        /// <param name="ranges">
        /// Array of selected ranges of m along each dimensionality.
        /// </param>
        public Mat(Mat m, in (int start, int end)[] ranges)
        {
            if (m != null)
                m.ThrowIfDisposed();

            int[] rangesArray = new int[ranges.Length * 2];
            for (int i = 0; i < ranges.Length; i++)
            {
                rangesArray[i * 2] = ranges[i].start;
                rangesArray[i * 2 + 1] = ranges[i].end;
            }

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__J_3Lorg_opencv_core_Range_2(m.nativeObj, rangesArray, rangesArray.Length));

        }

        //
        // C++: Mat::Mat(Mat m, Rect roi)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="m">
        /// Array that (as a whole or partly) is assigned to the constructed matrix. No data is copied
        /// by these constructors.Instead, the header pointing to m data or its sub-array is constructed and
        /// associated with it.The reference counter, if any, is incremented.So, when you modify the matrix
        /// formed using such a constructor, you also modify the corresponding elements of m.If you want to
        /// have an independent copy of the sub-array, use Mat::clone() .
        /// </param>
        /// <param name="roi">
        /// Region of interest.
        /// </param>
        public Mat(Mat m, in (int x, int y, int width, int height) roi)
        {
            if (m != null)
                m.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__JIIII(m.nativeObj, roi.y, roi.y + roi.height, roi.x, roi.x + roi.width));

        }

        //
        // C++: Mat::Mat (Size size, int type, void *data, size_t step=AUTO_STEP)
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        /// <param name="size">
        /// 2D array size: Size(cols, rows) . In the Size() constructor, the number of rows and the
        /// number of columns go in the reverse order.
        /// </param>
        /// <param name="type">
        /// Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
        /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.
        /// </param>
        /// <param name="data">
        /// Pointer to the user data. Matrix constructors that take data and step parameters do not
        /// allocate matrix data.Instead, they just initialize the matrix header that points to the specified
        /// data, which means that no data is copied.This operation is very efficient and can be used to
        /// process external data using OpenCV functions.The external data is not automatically deallocated, so
        /// you should take care of it.
        /// </param>
        /// <param name="step">
        /// Number of bytes each matrix row occupies. The value should include the padding bytes at
        /// the end of each row, if any.If the parameter is missing(set to AUTO_STEP ), no padding is assumed
        /// and the actual step is calculated as cols* elemSize(). See Mat::elemSize.
        /// </param>
        public Mat(in (double width, double height) size, int type, IntPtr data, long step = AUTO_STEP)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDIVL(size.width, size.height, type, data, step));

        }

        //
        // C++: Mat Mat::colRange(Range r)
        //

        /// <summary>
        /// Creates a matrix header for the specified column span.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified column span of the matrix. Similarly to Mat::row and
        /// Mat::col , this is an O(1) operation.
        /// </remarks>
        /// <param name="r">
        /// Range structure containing both the start and the end indices.
        /// </param>
        public Mat colRange(in (int start, int end) r)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1colRange(nativeObj, r.start, r.end)));

        }

        //
        // C++: void Mat::create(Size size, int type)
        //

        /// <summary>
        /// Allocates new array data if needed.
        /// </summary>
        /// <remarks>
        /// This is one of the key Mat methods. Most new-style OpenCV functions and methods that produce arrays
        /// call this method for each output array. The method uses the following algorithm:
        /// 
        /// <list type="number">
        /// <item>If the current array shape and the type match the new ones, return immediately. Otherwise,
        /// de-reference the previous data by calling Mat::release.</item>
        /// <item>Initialize the new header.</item>
        /// <item>Allocate the new data of total()*elemSize() bytes.</item>
        /// <item>Allocate the new, associated with the data, reference counter and set it to 1.</item>
        /// </list>
        /// 
        /// Such a scheme makes the memory management robust and efficient at the same time and helps avoid
        /// extra typing for you. This means that usually there is no need to explicitly allocate output arrays.
        /// That is, instead of writing:
        /// <example>
        /// <code language="c++">
        ///     Mat color;
        ///     ...
        ///     Mat gray(color.rows, color.cols, color.depth());
        ///     cvtColor(color, gray, COLOR_BGR2GRAY);
        /// </code>
        /// </example>
        /// you can simply write:
        /// <example>
        /// <code language="c++">
        ///     Mat color;
        ///     ...
        ///     Mat gray;
        ///     cvtColor(color, gray, COLOR_BGR2GRAY);
        /// </code>
        /// </example>
        /// because cvtColor, as well as the most of OpenCV functions, calls Mat::create() for the output array
        /// internally.
        /// </remarks>
        /// <param name="size">
        /// Alternative new matrix size specification: Size(cols, rows)
        /// </param>
        /// <param name="type">
        /// New matrix type.
        /// </param>
        public void create(in (double width, double height) size, int type)
        {
            ThrowIfDisposed();

            core_Mat_n_1create__JDDI(nativeObj, size.width, size.height, type);

        }

        //
        // C++: static Mat Mat::eye(Size size, int type)
        //

        /// <summary>Returns an identity matrix of the specified size and type.</summary>
        /// <remarks>
        /// The method returns a Matlab-style identity matrix initializer, similarly to Mat::zeros. Similarly to
        /// Mat::ones, you can use a scale operation to create a scaled identity matrix efficiently:
        /// <example>
        /// <code language="c++">
        /// // make a 4x4 diagonal matrix with 0.1's on the diagonal.
        /// Mat A = Mat::eye(4, 4, CV_32F)*0.1;
        /// </code>
        /// </example>
        /// @note In case of multi-channels type, identity matrix will be initialized only for the first channel,
        /// the others will be set to 0's
        /// </remarks>
        /// <param name="size">
        /// Alternative matrix size specification as Size(cols, rows) .
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat eye(in (double width, double height) size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1eye__DDI(size.width, size.height, type)));

        }

        //
        // C++: void Mat::locateROI(Size wholeSize, Point ofs)
        //

        /// <summary>Locates the matrix header within a parent matrix.</summary>
        /// <remarks>
        /// After you extracted a submatrix from a matrix using Mat::row, Mat::col, Mat::rowRange,
        /// Mat::colRange, and others, the resultant submatrix points just to the part of the original big
        /// matrix. However, each submatrix contains information (represented by datastart and dataend
        /// fields) that helps reconstruct the original matrix size and the position of the extracted
        /// submatrix within the original matrix. The method locateROI does exactly that.
        /// </remarks>
        /// <param name="wholeSize">
        /// Output parameter that contains the size of the whole matrix containing *this*
        /// as a part.
        /// </param>
        /// <param name="ofs">
        /// Output parameter that contains an offset of *this* inside the whole matrix.
        /// </param>
        public void locateROI(out (double width, double height) wholeSize, out (double x, double y) ofs)
        {
            ThrowIfDisposed();

            double[] wholeSize_out = new double[2];
            double[] ofs_out = new double[2];
            core_Mat_locateROI_10(nativeObj, wholeSize_out, ofs_out);

            {
                wholeSize.width = wholeSize_out[0];
                wholeSize.height = wholeSize_out[1];
            }

            {
                ofs.x = ofs_out[0];
                ofs.y = ofs_out[1];
            }

        }

        //
        // C++: static Mat Mat::ones(Size size, int type)
        //

        /// <summary>Returns an array of all 1's of the specified size and type.</summary>
        /// <remarks>
        /// The method returns a Matlab-style 1's array initializer, similarly to Mat.zeros. Note that using
        /// this method you can initialize an array with an arbitrary value, using the following Matlab idiom:
        /// <example>
        /// <code language="c++">
        ///     Mat A = Mat::ones(100, 100, CV_8U)*3; // make 100x100 matrix filled with 3.
        /// </code>
        /// </example>
        /// The above operation does not form a 100x100 matrix of 1's and then multiply it by 3. Instead, it
        /// just remembers the scale factor (3 in this case) and use it when actually invoking the matrix
        /// initializer.
        /// @note In case of multi-channels type, only the first channel will be initialized with 1's, the
        /// others will be set to 0's.
        /// </remarks>
        /// <param name="size">
        /// Alternative to the matrix size specification Size(cols, rows) .
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat ones(in (double width, double height) size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1ones__DDI(size.width, size.height, type)));

        }

        //
        // C++: Mat Mat::rowRange(Range r)
        //

        /// <summary>
        /// Creates a matrix header for the specified row span.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified row span of the matrix. Similarly to Mat.row and
        /// Mat.col, this is an O(1) operation.
        /// </remarks>
        /// <param name="r">
        /// Range structure containing both the start and the end indices.
        /// </param>
        public Mat rowRange(in (int start, int end) r)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1rowRange(nativeObj, r.start, r.end)));

        }

        //
        // C++: Mat Mat::operator =(Scalar s)
        //

        /// <summary>
        /// Sets all or some of the array elements to the specified value.
        /// </summary>
        /// <remarks>
        /// This is an advanced variant of the Mat.operator=(const Scalar&amp; s) operator.
        /// </remarks>
        /// <param name="s">
        /// Assigned scalar converted to the actual array type.
        /// </param>
        public Mat setTo(in (double v0, double v1, double v2, double v3) s)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JDDDD(nativeObj, s.v0, s.v1, s.v2, s.v3)));

        }

        //
        // C++: Mat Mat::setTo(Scalar value, Mat mask = Mat())
        //

        /// <summary>
        /// Sets all or some of the array elements to the specified value.
        /// </summary>
        /// <remarks>
        /// This is an advanced variant of the Mat.operator=(const Scalar&amp; s) operator.
        /// </remarks>
        /// <param name="value">
        /// Assigned scalar converted to the actual array type.
        /// </param>
        /// <param name="mask">
        /// Operation mask of the same size as *this. Its non-zero elements indicate which matrix
        /// elements need to be copied. The mask has to be of type CV_8U and can have 1 or multiple channels.
        /// </param>
        public Mat setTo(in (double v0, double v1, double v2, double v3) value, Mat mask)
        {
            if (mask != null)
                mask.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JDDDDJ(nativeObj, value.v0, value.v1, value.v2, value.v3, mask.nativeObj)));

        }

        //
        // C++: Size Mat::size()
        //

        /// <remarks>
        /// returns the array of sizes, or NULL if the matrix is not allocated
        /// </remarks>
        public (double width, double height) sizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            core_Mat_n_1size(nativeObj, tmpArray);

            return (tmpArray[0], tmpArray[1]);

        }

        //
        // C++: Mat Mat::operator()(Range rowRange, Range colRange)
        //

        /// <summary>
        /// Extracts a rectangular submatrix.
        /// </summary>
        /// <remarks>
        /// The operators make a new header for the specified sub-array of *this . They are the most
        /// generalized forms of Mat.row, Mat.col, Mat.rowRange, and Mat.colRange . For example,
        /// <example>
        /// <code language="c++">
        ///     A(Range(0, 10), Range::all()) is equivalent to A.rowRange(0, 10).
        /// </code>
        /// </example>
        /// Similarly to all of the above, the operators are O(1) operations, that is, no matrix data is copied.
        /// </remarks>
        /// <param name="rowRange">
        /// Start and end row of the extracted submatrix. The upper boundary is not included. To
        /// select all the rows, use Range.all().
        /// </param>
        /// <param name="colRange">
        /// Start and end column of the extracted submatrix. The upper boundary is not included.
        /// To select all the columns, use Range.all().
        /// </param>
        public Mat submat(in (int start, int end) rowRange, in (int start, int end) colRange)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1submat_1rr(nativeObj, rowRange.start, rowRange.end, colRange.start, colRange.end)));

        }

        //
        // C++: Mat Mat::operator()(const std::vector<Range>& ranges)
        //

        /// <summary>
        /// Extracts a rectangular submatrix.
        /// </summary>
        /// <remarks>
        /// The operators make a new header for the specified sub-array of *this . They are the most
        /// generalized forms of Mat.row, Mat.col, Mat.rowRange, and Mat.colRange . For example,
        /// <example>
        /// <code language="c++">
        ///     A(Range(0, 10), Range::all()) is equivalent to A.rowRange(0, 10).
        /// </code>
        /// </example>
        /// Similarly to all of the above, the operators are O(1) operations, that is, no matrix data is copied.
        /// </remarks>
        /// <param name="ranges">
        /// Array of selected ranges along each array dimension.
        /// </param>
        public Mat submat(in (int start, int end)[] ranges)
        {
            ThrowIfDisposed();

            int[] rangesArray = new int[ranges.Length * 2];
            for (int i = 0; i < ranges.Length; i++)
            {
                rangesArray[i * 2] = ranges[i].start;
                rangesArray[i * 2 + 1] = ranges[i].end;
            }

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1submat_1ranges(nativeObj, rangesArray, rangesArray.Length)));

        }

        //
        // C++: Mat Mat::operator()(Rect roi)
        //

        /// <summary>
        /// Extracts a rectangular submatrix.
        /// </summary>
        /// <remarks>
        /// The operators make a new header for the specified sub-array of *this . They are the most
        /// generalized forms of Mat.row, Mat.col, Mat.rowRange, and Mat.colRange . For example,
        /// <example>
        /// <code language="c++">
        ///     A(Range(0, 10), Range::all()) is equivalent to A.rowRange(0, 10).
        /// </code>
        /// </example>
        /// Similarly to all of the above, the operators are O(1) operations, that is, no matrix data is copied.
        /// </remarks>
        /// <param name="roi">
        /// Extracted submatrix specified as a rectangle.
        /// </param>
        public Mat submat(in (int x, int y, int width, int height) roi)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1submat(nativeObj, roi.x, roi.y, roi.width, roi.height)));

        }

        //
        // C++: static Mat Mat::zeros(Size size, int type)
        //

        /// <summary>
        /// Returns a zero array of the specified size and type.
        /// </summary>
        /// <remarks>
        /// The method returns a Matlab-style zero array initializer. It can be used to quickly form a constant
        /// array as a function parameter, part of a matrix expression, or as a matrix initializer:
        /// <example>
        /// <code language="c++">
        ///     Mat A;
        ///     A = Mat::zeros(3, 3, CV_32F);
        /// </code>
        /// </example>
        /// In the example above, a new matrix is allocated only if A is not a 3x3 floating-point matrix.
        /// Otherwise, the existing matrix A is filled with zeros.
        /// </remarks>
        /// <param name="size">
        /// Alternative to the matrix size specification Size(cols, rows) .
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat zeros(in (double width, double height) size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1zeros__DDI(size.width, size.height, type)));

        }

    }
}
