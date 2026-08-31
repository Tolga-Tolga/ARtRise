using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace OpenCVForUnity.CoreModule
{

    // C++: class Mat

    /// <summary>
    /// n-dimensional dense array class
    /// </summary>
    /// <remarks>
    /// <para>
    /// C++: cv::Mat Class Reference @see https://docs.opencv.org/4.10.0/d3/d63/classcv_1_1Mat.html
    /// </para>
    /// </remarks>

    public partial class Mat : DisposableOpenCVObject
    {

        // C++: enum
        public const int AUTO_STEP = 0;

        /// <summary>
        /// Deallocates the matrix data and releases resources.
        /// </summary>
        /// <remarks>
        /// This method decrements the reference counter associated with the matrix data. If the reference
        /// counter reaches zero, the matrix data is deallocated, and the data and reference counter pointers
        /// are set to null. It is generally not necessary to call this method manually, as it is automatically
        /// invoked in the destructor or by any other method that changes the data pointer. The reference
        /// counter decrement and check for zero is an atomic operation on supported platforms, ensuring
        /// thread safety when operating on the same matrices asynchronously.
        /// </remarks>
        protected override void Dispose(bool disposing)
        {

            try
            {

                if (disposing)
                {
                }

                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        core_Mat_n_1delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }

            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        public Mat(IntPtr addr)
        {

            if (addr == IntPtr.Zero)
                throw new CvException("Native object address is NULL");
            nativeObj = addr;

        }

        //
        // C++: Mat::Mat()
        //

        /// <remarks>
        /// These are various constructors that form a matrix.As noted in the AutomaticAllocation, often
        /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
        /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
        /// allocated with Mat::create.In the former case, the old content is de-referenced.
        /// </remarks>
        public Mat()
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__());

        }

        //
        // C++: Mat::Mat(int rows, int cols, int type)
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
        public Mat(int rows, int cols, int type)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__III(rows, cols, type));

        }

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
        public Mat(Size size, int type)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDI(size.width, size.height, type));

        }

        //
        // C++: Mat::Mat(int ndims, const int* sizes, int type)
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
        public Mat(int[] sizes, int type)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__I_3II(sizes.Length, sizes, sizes.Length, type));

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
        public Mat(int rows, int cols, int type, Scalar s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__IIIDDDD(rows, cols, type, s.val[0], s.val[1], s.val[2], s.val[3]));

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
        public Mat(Size size, int type, Scalar s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDIDDDD(size.width, size.height, type, s.val[0], s.val[1], s.val[2], s.val[3]));

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
        public Mat(int[] sizes, int type, Scalar s)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__I_3IIDDDD(sizes.Length, sizes, sizes.Length, type, s.val[0], s.val[1], s.val[2], s.val[3]));

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
        public Mat(Mat m, Range rowRange, Range colRange)
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
        public Mat(Mat m, Range rowRange)
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
        public Mat(Mat m, Range[] ranges)
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
        public Mat(Mat m, Rect roi)
        {
            if (m != null)
                m.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__JIIII(m.nativeObj, roi.y, roi.y + roi.height, roi.x, roi.x + roi.width));

        }

        //
        // C++: Mat::Mat (int rows, int cols, int type, void *data, size_t step=AUTO_STEP)
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
        public Mat(int rows, int cols, int type, IntPtr data, long step = AUTO_STEP)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__IIIVL(rows, cols, type, data, step));

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
        public Mat(Size size, int type, IntPtr data, long step = AUTO_STEP)
        {

            nativeObj = DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1Mat__DDIVL(size.width, size.height, type, data, step));

        }

        //
        // C++: Mat Mat::adjustROI(int dtop, int dbottom, int dleft, int dright)
        //

        /// <summary>
        /// Adjusts a submatrix size and position within the parent matrix.
        /// </summary>
        /// <remarks>
        /// The method is complimentary to Mat::locateROI.The typical use of these functions is to determine
        /// the submatrix position within the parent matrix and then shift the position somehow.Typically, it
        /// can be required for filtering operations when pixels outside of the ROI should be taken into
        /// account.When all the method parameters are positive, the ROI needs to grow in all directions by the
        /// specified amount, for example:
        /// <example>
        /// <code language="c++">
        ///     A.adjustROI(2, 2, 2, 2);
        /// </code>
        /// </example>
        /// In this example, the matrix size is increased by 4 elements in each direction. The matrix is shifted
        /// by 2 elements to the left and 2 elements up, which brings in all the necessary pixels for the
        /// filtering with the 5x5 kernel.
        /// 
        /// adjustROI forces the adjusted ROI to be inside of the parent matrix that is boundaries of the
        /// adjusted ROI are constrained by boundaries of the parent matrix.For example, if the submatrix A is
        /// located in the first row of a parent matrix and you called A.adjustROI(2, 2, 2, 2) then A will not
        /// be increased in the upward direction.
        /// 
        /// The function is used internally by the OpenCV filtering functions, like filter2D , morphological
        /// operations, and so on.
        /// </remarks>
        /// <param name="dtop">
        /// Shift of the top submatrix boundary upwards.
        /// </param>
        /// <param name="dbottom">
        /// Shift of the bottom submatrix boundary downwards.
        /// </param>
        /// <param name="dleft">
        /// Shift of the left submatrix boundary to the left.
        /// </param>
        /// <param name="dright">
        /// Shift of the right submatrix boundary to the right.
        /// </param>
        /// <remarks>
        /// @sa copyMakeBorder
        /// </remarks>
        public Mat adjustROI(int dtop, int dbottom, int dleft, int dright)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1adjustROI(nativeObj, dtop, dbottom, dleft, dright)));

        }

        //
        // C++: void Mat::assignTo(Mat m, int type = -1)
        //

        /// <summary>
        /// Provides a functional form of convertTo.
        /// </summary>
        /// <remarks>
        /// This is an internally used method called by the @ref MatrixExpressions engine.
        /// </remarks>
        /// <param name="m">
        /// Destination array.
        /// </param>
        /// <param name="type">
        /// Desired destination array depth (or -1 if it should be the same as the source type).
        /// </param>
        public void assignTo(Mat m, int type)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();


            core_Mat_n_1assignTo__JJI(nativeObj, m.nativeObj, type);

        }

        /// <summary>
        /// Provides a functional form of convertTo.
        /// </summary>
        /// <remarks>
        /// This is an internally used method called by the @ref MatrixExpressions engine.
        /// </remarks>
        /// <param name="m">
        /// Destination array.
        /// </param>
        public void assignTo(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1assignTo__JJ(nativeObj, m.nativeObj);

        }

        //
        // C++: int Mat::channels()
        //

        /// <summary>
        /// Returns the number of matrix channels.
        /// </summary>
        /// <remarks>
        /// returns the number of matrix channels.
        /// </remarks>
        public int channels()
        {
            ThrowIfDisposed();

            return core_Mat_n_1channels(nativeObj);

        }

        //
        // C++: int Mat::checkVector(int elemChannels, int depth = -1, bool
        // requireContinuous = true)
        //

        /// <param name="elemChannels">
        /// Number of channels or number of columns the matrix should have.
        /// For a 2-D matrix, when the matrix has only 1 column, then it should have
        /// elemChannels channels; When the matrix has only 1 channel,
        /// then it should have elemChannels columns.
        /// For a 3-D matrix, it should have only one channel. Furthermore,
        /// if the number of planes is not one, then the number of rows
        /// within every plane has to be 1; if the number of rows within
        /// every plane is not 1, then the number of planes has to be 1.
        /// </param>
        /// <param name="depth">
        /// The depth the matrix should have. Set it to -1 when any depth is fine.
        /// </param>
        /// <param name="requireContinuous">
        /// Set it to true to require the matrix to be continuous
        /// </param>
        /// <returns>
        /// -1 if the requirement is not satisfied.
        /// Otherwise, it returns the number of elements in the matrix.Note
        /// that an element may have multiple channels.
        /// </returns>
        /// <remarks>
        /// <example>
        /// The following code demonstrates its usage for a 2-d matrix:
        /// <code language="c++">
        ///  cv::Mat mat(20, 1, CV_32FC2);
        ///  int n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // mat has 20 elements
        ///  
        ///  mat.create(20, 2, CV_32FC1);
        ///  n = mat.checkVector(1);
        ///  CV_Assert(n == -1); // mat is neither a column nor a row vector
        ///  
        ///  n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // the 2 columns are considered as 1 element
        /// </code>
        /// </example>
        /// <example>
        /// The following code demonstrates its usage for a 3-d matrix:
        /// <code language="c++">
        ///  int dims[] = { 1, 3, 5 }; // 1 plane, every plane has 3 rows and 5 columns
        ///  mat.create(3, dims, CV_32FC1); // for 3-d mat, it MUST have only 1 channel
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        ///  
        ///  int dims2[] = { 3, 1, 5 }; // 3 planes, every plane has 1 row and 5 columns
        ///  mat.create(3, dims2, CV_32FC1);
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        /// </code>
        /// </example>
        /// </remarks>
        public int checkVector(int elemChannels, int depth, bool requireContinuous)
        {
            ThrowIfDisposed();

            return core_Mat_n_1checkVector__JIIZ(nativeObj, elemChannels, depth, requireContinuous);

        }

        /// <param name="elemChannels">
        /// Number of channels or number of columns the matrix should have.
        /// For a 2-D matrix, when the matrix has only 1 column, then it should have
        /// elemChannels channels; When the matrix has only 1 channel,
        /// then it should have elemChannels columns.
        /// For a 3-D matrix, it should have only one channel. Furthermore,
        /// if the number of planes is not one, then the number of rows
        /// within every plane has to be 1; if the number of rows within
        /// every plane is not 1, then the number of planes has to be 1.
        /// </param>
        /// <param name="depth">
        /// The depth the matrix should have. Set it to -1 when any depth is fine.
        /// </param>
        /// <returns>
        /// -1 if the requirement is not satisfied.
        /// Otherwise, it returns the number of elements in the matrix.Note
        /// that an element may have multiple channels.
        /// </returns>
        /// <remarks>
        /// <example>
        /// The following code demonstrates its usage for a 2-d matrix:
        /// <code language="c++">
        ///  cv::Mat mat(20, 1, CV_32FC2);
        ///  int n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // mat has 20 elements
        ///  
        ///  mat.create(20, 2, CV_32FC1);
        ///  n = mat.checkVector(1);
        ///  CV_Assert(n == -1); // mat is neither a column nor a row vector
        ///  
        ///  n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // the 2 columns are considered as 1 element
        /// </code>
        /// </example>
        /// <example>
        /// The following code demonstrates its usage for a 3-d matrix:
        /// <code language="c++">
        ///  int dims[] = { 1, 3, 5 }; // 1 plane, every plane has 3 rows and 5 columns
        ///  mat.create(3, dims, CV_32FC1); // for 3-d mat, it MUST have only 1 channel
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        ///  
        ///  int dims2[] = { 3, 1, 5 }; // 3 planes, every plane has 1 row and 5 columns
        ///  mat.create(3, dims2, CV_32FC1);
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        /// </code>
        /// </example>
        /// </remarks>
        public int checkVector(int elemChannels, int depth)
        {
            ThrowIfDisposed();

            return core_Mat_n_1checkVector__JII(nativeObj, elemChannels, depth);

        }

        /// <param name="elemChannels">
        /// Number of channels or number of columns the matrix should have.
        /// For a 2-D matrix, when the matrix has only 1 column, then it should have
        /// elemChannels channels; When the matrix has only 1 channel,
        /// then it should have elemChannels columns.
        /// For a 3-D matrix, it should have only one channel. Furthermore,
        /// if the number of planes is not one, then the number of rows
        /// within every plane has to be 1; if the number of rows within
        /// every plane is not 1, then the number of planes has to be 1.
        /// </param>
        /// <returns>
        /// -1 if the requirement is not satisfied.
        /// Otherwise, it returns the number of elements in the matrix.Note
        /// that an element may have multiple channels.
        /// </returns>
        /// <remarks>
        /// <example>
        /// The following code demonstrates its usage for a 2-d matrix:
        /// <code language="c++">
        ///  cv::Mat mat(20, 1, CV_32FC2);
        ///  int n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // mat has 20 elements
        ///  
        ///  mat.create(20, 2, CV_32FC1);
        ///  n = mat.checkVector(1);
        ///  CV_Assert(n == -1); // mat is neither a column nor a row vector
        ///  
        ///  n = mat.checkVector(2);
        ///  CV_Assert(n == 20); // the 2 columns are considered as 1 element
        /// </code>
        /// </example>
        /// <example>
        /// The following code demonstrates its usage for a 3-d matrix:
        /// <code language="c++">
        ///  int dims[] = { 1, 3, 5 }; // 1 plane, every plane has 3 rows and 5 columns
        ///  mat.create(3, dims, CV_32FC1); // for 3-d mat, it MUST have only 1 channel
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        ///  
        ///  int dims2[] = { 3, 1, 5 }; // 3 planes, every plane has 1 row and 5 columns
        ///  mat.create(3, dims2, CV_32FC1);
        ///  n = mat.checkVector(5); // the 5 columns are considered as 1 element
        ///  CV_Assert(n == 3);
        /// </code>
        /// </example>
        /// </remarks>
        public int checkVector(int elemChannels)
        {
            ThrowIfDisposed();

            return core_Mat_n_1checkVector__JI(nativeObj, elemChannels);

        }

        //
        // C++: Mat Mat::clone()
        //

        /// <summary>
        /// Creates a full copy of the array and the underlying data.
        /// </summary>
        /// <remarks>
        /// The method creates a full copy of the array. The original step[] is not taken into account. So, the
        /// array copy is a continuous array occupying total()*elemSize() bytes.
        /// </remarks>
        public Mat clone()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1clone(nativeObj)));

        }

        //
        // C++: Mat Mat::col(int x)
        //

        /// <summary>
        /// Creates a matrix header for the specified matrix column.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified matrix column and returns it. This is an O(1)
        /// operation, regardless of the matrix size.The underlying data of the new matrix is shared with the
        /// original matrix.See also the Mat::row description.
        /// </remarks>
        /// <param name="x">
        /// A 0-based column index.
        /// </param>
        public Mat col(int x)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1col(nativeObj, x)));

        }

        //
        // C++: Mat Mat::colRange(int startcol, int endcol)
        //

        /// <summary>
        /// Creates a matrix header for the specified column span.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified column span of the matrix. Similarly to Mat::row and
        /// Mat::col , this is an O(1) operation.
        /// </remarks>
        /// <param name="startcol">
        /// An inclusive 0-based start index of the column span.
        /// </param>
        /// <param name="endcol">
        /// An exclusive 0-based ending index of the column span.
        /// </param>
        public Mat colRange(int startcol, int endcol)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1colRange(nativeObj, startcol, endcol)));

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
        public Mat colRange(Range r)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1colRange(nativeObj, r.start, r.end)));

        }

        //
        // C++: int Mat::dims()
        //

        /// <remarks>
        /// returns the matrix dimensionality
        /// </remarks>
        public int dims()
        {
            ThrowIfDisposed();

            return core_Mat_n_1dims(nativeObj);

        }

        //
        // C++: int Mat::cols()
        //

        /// <remarks>
        /// number of columns in the matrix; -1 when the matrix has more than 2 dimensions
        /// </remarks>
        public int cols()
        {
            ThrowIfDisposed();

            return core_Mat_n_1cols(nativeObj);

        }

        //
        // C++: void Mat::convertTo(Mat& m, int rtype, double alpha = 1, double beta
        // = 0)
        //

        /// <summary>
        /// Converts an array to another data type with optional scaling.
        /// </summary>
        /// <remarks>
        /// The method converts source pixel values to the target data type. saturate_cast&lt;&gt; is applied at
        /// the end to avoid possible overflows:
        /// 
        /// \f[m(x,y) = saturate \_ cast&lt;rType&gt;( \alpha (*this)(x,y) +  \beta )\f]
        /// </remarks>
        /// <param name="m">
        /// Output matrix; if it does not have a proper size or type before the operation, it is reallocated.
        /// </param>
        /// <param name="rtype">
        /// Desired output matrix type or, rather, the depth since the number of channels are the
        /// same as the input has; if rtype is negative, the output matrix will have the same type as the input.
        /// </param>
        /// <param name="alpha">
        /// Optional scale factor.
        /// </param>
        /// <param name="beta">
        /// Optional delta added to the scaled values.
        /// </param>
        public void convertTo(Mat m, int rtype, double alpha, double beta)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1convertTo__JJIDD(nativeObj, m.nativeObj, rtype, alpha, beta);

        }

        /// <summary>
        /// Converts an array to another data type with optional scaling.
        /// </summary>
        /// <remarks>
        /// The method converts source pixel values to the target data type. saturate_cast&lt;&gt; is applied at
        /// the end to avoid possible overflows:
        /// 
        /// \f[m(x,y) = saturate \_ cast&lt;rType&gt;( \alpha (*this)(x,y) +  \beta )\f]
        /// </remarks>
        /// <param name="m">
        /// Output matrix; if it does not have a proper size or type before the operation, it is reallocated.
        /// </param>
        /// <param name="rtype">
        /// Desired output matrix type or, rather, the depth since the number of channels are the
        /// same as the input has; if rtype is negative, the output matrix will have the same type as the input.
        /// </param>
        /// <param name="alpha">
        /// Optional scale factor.
        /// </param>
        public void convertTo(Mat m, int rtype, double alpha)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1convertTo__JJID(nativeObj, m.nativeObj, rtype, alpha);

        }

        /// <summary>
        /// Converts an array to another data type with optional scaling.
        /// </summary>
        /// <remarks>
        /// The method converts source pixel values to the target data type. saturate_cast&lt;&gt; is applied at
        /// the end to avoid possible overflows:
        /// <para>
        /// 
        /// \f[m(x,y) = saturate \_ cast&lt;rType&gt;( \alpha (*this)(x,y) +  \beta )\f]
        /// </para>
        /// </remarks>
        /// <param name="m">
        /// Output matrix; if it does not have a proper size or type before the operation, it is reallocated.
        /// </param>
        /// <param name="rtype">
        /// Desired output matrix type or, rather, the depth since the number of channels are the
        /// same as the input has; if rtype is negative, the output matrix will have the same type as the input.
        /// </param>
        public void convertTo(Mat m, int rtype)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1convertTo__JJI(nativeObj, m.nativeObj, rtype);

        }

        //
        // C++: void Mat::copyTo(Mat& m)
        //

        /// <summary>
        /// Copies the matrix to another one.
        /// </summary>
        /// <remarks>
        /// The method copies the matrix data to another matrix. Before copying the data, the method invokes :
        /// <example>
        /// <code language="c++">
        ///     m.create(this->size(), this->type());
        /// </code>
        /// </example>
        /// so that the destination matrix is reallocated if needed. While m.copyTo(m); works flawlessly, the
        /// function does not handle the case of a partial overlap between the source and the destination
        /// matrices.
        /// 
        /// When the operation mask is specified, if the Mat::create call shown above reallocates the matrix,
        /// the newly allocated matrix is initialized with all zeros before copying the data.
        /// </remarks>
        /// <param name="m">
        /// Destination matrix. If it does not have a proper size or type before the operation, it is
        /// reallocated.
        /// </param>
        public void copyTo(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1copyTo__JJ(nativeObj, m.nativeObj);

        }

        //
        // C++: void Mat::copyTo(Mat& m, Mat mask)
        //

        /// <summary>
        /// Copies the matrix to another one.
        /// </summary>
        /// <remarks>
        /// The method copies the matrix data to another matrix. Before copying the data, the method invokes :
        /// <example>
        /// <code language="c++">
        ///     m.create(this->size(), this->type());
        /// </code>
        /// </example>
        /// so that the destination matrix is reallocated if needed. While m.copyTo(m); works flawlessly, the
        /// function does not handle the case of a partial overlap between the source and the destination
        /// matrices.
        /// 
        /// When the operation mask is specified, if the Mat::create call shown above reallocates the matrix,
        /// the newly allocated matrix is initialized with all zeros before copying the data.
        /// </remarks>
        /// <param name="m">
        /// Destination matrix. If it does not have a proper size or type before the operation, it is
        /// reallocated.
        /// </param>
        /// <param name="mask">
        /// Operation mask of the same size as \*this. Its non-zero elements indicate which matrix
        /// elements need to be copied.The mask has to be of type CV_8U and can have 1 or multiple channels.
        /// </param>
        public void copyTo(Mat m, Mat mask)
        {
            if (m != null)
                m.ThrowIfDisposed();
            if (mask != null)
                mask.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1copyTo__JJJ(nativeObj, m.nativeObj, mask.nativeObj);

        }

        //
        // C++: void Mat::create(int rows, int cols, int type)
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
        /// <param name="rows">
        /// New number of rows.
        /// </param>
        /// <param name="cols">
        /// New number of columns.
        /// </param>
        /// <param name="type">
        /// New matrix type.
        /// </param>
        public void create(int rows, int cols, int type)
        {
            ThrowIfDisposed();

            core_Mat_n_1create__JIII(nativeObj, rows, cols, type);

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
        public void create(Size size, int type)
        {
            ThrowIfDisposed();

            core_Mat_n_1create__JDDI(nativeObj, size.width, size.height, type);

        }

        //
        // C++: void Mat::create(int ndims, const int* sizes, int type)
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
        /// <param name="sizes">
        /// Array of integers specifying a new array shape.
        /// </param>
        /// <param name="type">
        /// New matrix type.
        /// </param>
        public void create(int[] sizes, int type)
        {
            ThrowIfDisposed();

            core_Mat_n_1create__JI_3II(nativeObj, sizes.Length, sizes, sizes.Length, type);

        }

        //
        // C++: void Mat::copySize(const Mat& m);
        //

        /// <remarks>
        /// internal use function; properly re-allocates _size, _step arrays
        /// </remarks>
        public void copySize(Mat m)
        {
            ThrowIfDisposed();

            core_Mat_n_1copySize(nativeObj, m.nativeObj);

        }

        //
        // C++: Mat Mat::cross(Mat m)
        //

        /// <summary>
        /// Computes a cross-product of two 3-element vectors.
        /// </summary>
        /// <remarks>
        /// The method computes a cross-product of two 3-element vectors. The vectors must be 3-element
        /// floating-point vectors of the same shape and size. The result is another 3-element vector of the
        /// same shape and type as operands.
        /// </remarks>
        /// <param name="m">
        /// Another cross-product operand.
        /// </param>
        public Mat cross(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1cross(nativeObj, m.nativeObj)));

        }

        //
        // C++: long Mat::dataAddr()
        //

        /// <summary>
        /// Returns the first pointer address of Mat data.
        /// </summary>
        /// <returns>
        /// Returns the first pointer address of Mat data.
        /// </returns>
        public long dataAddr()
        {
            ThrowIfDisposed();

            return core_Mat_n_1dataAddr(nativeObj);

        }

        //
        // C++: int Mat::depth()
        //

        /// <summary>
        /// Returns the depth of a matrix element.
        /// </summary>
        /// <remarks>
        /// The method returns the identifier of the matrix element depth (the type of each individual channel).
        /// For example, for a 16-bit signed element array, the method returns CV_16S. A complete list of
        /// matrix types contains the following values:
        /// <list type="bullet">
        /// <item>CV_8U - 8-bit unsigned integers ( 0..255 )</item>
        /// <item>CV_8S - 8-bit signed integers ( -128..127 )</item>
        /// <item>CV_16U - 16-bit unsigned integers ( 0..65535 )</item>
        /// <item>CV_16S - 16-bit signed integers ( -32768..32767 )</item>
        /// <item>CV_32S - 32-bit signed integers ( -2147483648..2147483647 )</item>
        /// <item>CV_32F - 32-bit floating-point numbers ( -FLT_MAX..FLT_MAX, INF, NAN )</item>
        /// <item>CV_64F - 64-bit floating-point numbers ( -DBL_MAX..DBL_MAX, INF, NAN )</item>
        /// </list>
        /// </remarks>
        public int depth()
        {
            ThrowIfDisposed();

            return core_Mat_n_1depth(nativeObj);

        }

        //
        // C++: Mat Mat::diag(int d = 0)
        //

        /// <summary>
        /// Extracts a diagonal from a matrix.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified matrix diagonal. The new matrix is represented as a
        /// single-column matrix. Similarly to Mat::row and Mat::col, this is an O(1) operation.
        /// </remarks>
        /// <param name="d">
        /// Index of the diagonal, with the following values:
        /// <list type="bullet">
        /// <item><description><c>d=0</c> is the main diagonal.</description></item>
        /// <item><description><c>d&lt;0</c> is a diagonal from the lower half. For example, <c>d=-1</c> means the diagonal is set
        /// immediately below the main one.</description></item>
        /// <item><description><c>d&gt;0</c> is a diagonal from the upper half. For example, <c>d=1</c> means the diagonal is set
        /// immediately above the main one.</description></item>
        /// </list>
        /// </param>
        /// <remarks>
        /// <example>
        /// For example:
        /// <code language="c++">
        /// Mat m = (Mat_&lt;int&gt;(3,3) &lt;&lt;
        ///             1,2,3,
        ///             4,5,6,
        ///             7,8,9);
        /// Mat d0 = m.diag(0);
        /// Mat d1 = m.diag(1);
        /// Mat d_1 = m.diag(-1);
        /// </code>
        /// The resulting matrices are:
        /// <code language="c++">
        ///  d0 =
        ///    [1;
        ///     5;
        ///     9]
        ///  d1 =
        ///    [2;
        ///     6]
        ///  d_1 =
        ///    [4;
        ///     8]
        /// </code>
        /// </example>
        /// </remarks>

        public Mat diag(int d)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1diag__JI(nativeObj, d)));

        }

        /// <summary>
        /// Extracts a diagonal from a matrix.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified matrix diagonal. The new matrix is represented as a
        /// single-column matrix. Similarly to Mat::row and Mat::col, this is an O(1) operation.
        /// <example>
        /// For example:
        /// <code language="c++">
        /// Mat m = (Mat_&lt;int&gt;(3,3) &lt;&lt;
        ///             1,2,3,
        ///             4,5,6,
        ///             7,8,9);
        /// Mat d0 = m.diag(0);
        /// Mat d1 = m.diag(1);
        /// Mat d_1 = m.diag(-1);
        /// </code>
        /// The resulting matrices are:
        /// <code language="c++">
        ///  d0 =
        ///    [1;
        ///     5;
        ///     9]
        ///  d1 =
        ///    [2;
        ///     6]
        ///  d_1 =
        ///    [4;
        ///     8]
        /// </code>
        /// </example>
        /// </remarks>
        public Mat diag()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1diag__JI(nativeObj, 0)));

        }

        //
        // C++: static Mat Mat::diag(Mat d)
        //

        /// <summary>
        /// creates a diagonal matrix
        /// </summary>
        /// <remarks>
        /// The method creates a square diagonal matrix from specified main diagonal.
        /// </remarks>
        /// <param name="d">
        /// One-dimensional matrix that represents the main diagonal.
        /// </param>
        public static Mat diag(Mat d)
        {
            if (d != null)
                d.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1diag__J(d.nativeObj)));

        }

        //
        // C++: double Mat::dot(Mat m)
        //

        /// <summary>
        /// Computes a dot-product of two vectors.
        /// </summary>
        /// <remarks>
        /// The method computes a dot-product of two matrices. If the matrices are not single-column or
        /// single-row vectors, the top-to-bottom left-to-right scan ordering is used to treat them as 1D
        /// vectors. The vectors must have the same size and type. If the matrices have more than one channel,
        /// the dot products from all the channels are summed together.
        /// </remarks>
        /// <param name="m">
        /// Another dot-product operand.
        /// </param>
        public double dot(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            return core_Mat_n_1dot(nativeObj, m.nativeObj);

        }

        //
        // C++: size_t Mat::elemSize()
        //

        /// <summary>
        /// Returns the matrix element size in bytes.
        /// </summary>
        /// <remarks>
        /// The method returns the matrix element size in bytes. For example, if the matrix type is CV_16SC3,
        /// the method returns 3 * sizeof(short) or 6.
        /// </remarks>
        public long elemSize()
        {
            ThrowIfDisposed();

            return core_Mat_n_1elemSize(nativeObj);//TODO: @size_t long long

        }

        //
        // C++: size_t Mat::elemSize1()
        //

        /// <summary>
        /// Returns the size of each matrix element channel in bytes.
        /// </summary>
        /// <remarks>
        /// The method returns the matrix element channel size in bytes, that is, it ignores the number of
        /// channels. For example, if the matrix type is CV_16SC3, the method returns sizeof(short) or 2.
        /// </remarks>
        public long elemSize1()
        {
            ThrowIfDisposed();

            return core_Mat_n_1elemSize1(nativeObj);//TODO: @size_t long long

        }

        //
        // C++: bool Mat::empty()
        //

        /// <summary>
        /// Returns true if the array has no elements.
        /// </summary>
        /// <remarks>
        /// The method returns true if <c>Mat::total() == 0</c> or if <c>Mat::data == null</c>. Because of 
        /// <c>pop_back()</c> and <c>resize()</c> methods, <c>M.total() == 0</c> does not imply that 
        /// <c>M.data == null</c>.
        /// </remarks>
        public bool empty()
        {
            ThrowIfDisposed();

            return core_Mat_n_1empty(nativeObj);

        }

        //
        // C++: static Mat Mat::eye(int rows, int cols, int type)
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
        /// <param name="rows">
        /// Number of rows.
        /// </param>
        /// <param name="cols">
        /// Number of columns.
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat eye(int rows, int cols, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1eye__III(rows, cols, type)));

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
        public static Mat eye(Size size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1eye__DDI(size.width, size.height, type)));

        }

        //
        // C++: Mat Mat::inv(int method = DECOMP_LU)
        //

        /// <summary>Inverses a matrix.</summary>
        /// <remarks>
        /// The method performs a matrix inversion by means of matrix expressions. This means that a temporary
        /// matrix inversion object is returned by the method and can be used further as a part of more complex
        /// matrix expressions or can be assigned to a matrix.
        /// </remarks>
        /// <param name="method">
        /// Matrix inversion method. One of cv::DecompTypes
        /// </param>
        public Mat inv(int method)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1inv__JI(nativeObj, method)));

        }

        /// <summary>Inverses a matrix.</summary>
        /// <remarks>
        /// The method performs a matrix inversion by means of matrix expressions. This means that a temporary
        /// matrix inversion object is returned by the method and can be used further as a part of more complex
        /// matrix expressions or can be assigned to a matrix.
        /// </remarks>
        public Mat inv()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1inv__J(nativeObj)));

        }

        //
        // C++: bool Mat::isContinuous()
        //

        /// <summary>Reports whether the matrix is continuous or not.</summary>
        /// <remarks>
        /// The method returns true if the matrix elements are stored continuously without gaps at the end of
        /// each row. Otherwise, it returns false. Obviously, 1x1 or 1xN matrices are always continuous.
        /// Matrices created with Mat::create are always continuous. But if you extract a part of the matrix
        /// using Mat::col, Mat::diag, and so on, or constructed a matrix header for externally allocated data,
        /// such matrices may no longer have this property.
        ///
        /// The continuity flag is stored as a bit in the Mat::flags field and is computed automatically when
        /// you construct a matrix header. Thus, the continuity check is a very fast operation, though
        /// theoretically it could be done as follows:
        /// <example>
        /// <code language="c++">
        ///     // alternative implementation of Mat::isContinuous()
        ///     bool myCheckMatContinuity(const Mat&amp; m)
        ///     {
        ///         //return (m.flags &amp; Mat::CONTINUOUS_FLAG) != 0;
        ///         return m.rows == 1 || m.step == m.cols*m.elemSize();
        ///     }
        /// </code>
        /// </example>
        /// The method is used in quite a few of OpenCV functions. The point is that element-wise operations
        /// (such as arithmetic and logical operations, math functions, alpha blending, color space
        /// transformations, and others) do not depend on the image geometry. Thus, if all the input and output
        /// arrays are continuous, the functions can process them as very long single-row vectors. The example
        /// below illustrates how an alpha-blending function can be implemented:
        /// <example>
        /// <code language="c++">
        ///     template&lt;typename T&gt;
        ///     void alphaBlendRGBA(const Mat&amp; src1, const Mat&amp; src2, Mat&amp; dst)
        ///     {
        ///         const float alpha_scale = (float)std::numeric_limits&lt;T&gt;::max(),
        ///                     inv_scale = 1.f/alpha_scale;
        ///
        ///         CV_Assert( src1.type() == src2.type() &amp;&amp;
        ///                    src1.type() == CV_MAKETYPE(traits::Depth&lt;T&gt;::value, 4) &amp;&amp;
        ///                    src1.size() == src2.size());
        ///         Size size = src1.size();
        ///         dst.create(size, src1.type());
        ///
        ///         // here is the idiom: check the arrays for continuity and,
        ///         // if this is the case,
        ///         // treat the arrays as 1D vectors
        ///         if( src1.isContinuous() &amp;&amp; src2.isContinuous() &amp;&amp; dst.isContinuous() )
        ///         {
        ///             size.width *= size.height;
        ///             size.height = 1;
        ///         }
        ///         size.width *= 4;
        ///
        ///         for( int i = 0; i &lt; size.height; i++ )
        ///         {
        ///             // when the arrays are continuous,
        ///             // the outer loop is executed only once
        ///             const T* ptr1 = src1.ptr&lt;T&gt;(i);
        ///             const T* ptr2 = src2.ptr&lt;T&gt;(i);
        ///             T* dptr = dst.ptr&lt;T&gt;(i);
        ///
        ///             for( int j = 0; j &lt; size.width; j += 4 )
        ///             {
        ///                 float alpha = ptr1[j+3]*inv_scale, beta = ptr2[j+3]*inv_scale;
        ///                 dptr[j] = saturate_cast&lt;T&gt;(ptr1[j]*alpha + ptr2[j]*beta);
        ///                 dptr[j+1] = saturate_cast&lt;T&gt;(ptr1[j+1]*alpha + ptr2[j+1]*beta);
        ///                 dptr[j+2] = saturate_cast&lt;T&gt;(ptr1[j+2]*alpha + ptr2[j+2]*beta);
        ///                 dptr[j+3] = saturate_cast&lt;T&gt;((1 - (1-alpha)*(1-beta))*alpha_scale);
        ///             }
        ///         }
        ///     }
        /// </code>
        /// </example>
        /// This approach, while being very simple, can boost the performance of a simple element-operation by
        /// 10-20 percents, especially if the image is rather small and the operation is quite simple.
        ///
        /// Another OpenCV idiom in this function, a call of Mat::create for the destination array, that
        /// allocates the destination array unless it already has the proper size and type. And while the newly
        /// allocated arrays are always continuous, you still need to check the destination array because
        /// Mat::create does not always allocate a new matrix.
        /// </remarks>

        public bool isContinuous()
        {
            ThrowIfDisposed();

            return core_Mat_n_1isContinuous(nativeObj);

        }

        //
        // C++: bool Mat::isSubmatrix()
        //

        /// <remarks>
        /// returns true if the matrix is a submatrix of another matrix
        /// </remarks>
        public bool isSubmatrix()
        {
            ThrowIfDisposed();

            return core_Mat_n_1isSubmatrix(nativeObj);

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
        public void locateROI(Size wholeSize, Point ofs)
        {
            ThrowIfDisposed();

            double[] wholeSize_out = new double[2];
            double[] ofs_out = new double[2];
            core_Mat_locateROI_10(nativeObj, wholeSize_out, ofs_out);
            if (wholeSize != null)
            {
                wholeSize.width = wholeSize_out[0];
                wholeSize.height = wholeSize_out[1];
            }
            if (ofs != null)
            {
                ofs.x = ofs_out[0];
                ofs.y = ofs_out[1];
            }

        }

        //
        // C++: Mat Mat::mul(Mat m, double scale = 1)
        //

        /// <summary>Performs an element-wise multiplication or division of the two matrices.</summary>
        /// <remarks>
        /// The method returns a temporary object encoding per-element array multiplication, with optional
        /// scale. Note that this is not a matrix multiplication that corresponds to a simpler "*" operator.
        /// </remarks>
        /// <example>
        /// Example:
        /// <code language="c++">
        ///     Mat C = A.mul(5/B); // equivalent to divide(A, B, C, 5)
        /// </code>
        /// </example>
        /// <param name="m">
        /// Another array of the same type and the same size as *this, or a matrix expression.
        /// </param>
        /// <param name="scale">
        /// Optional scale factor.
        /// </param>
        public Mat mul(Mat m, double scale)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1mul__JJD(nativeObj, m.nativeObj, scale)));

        }

        /// <summary>
        /// Element-wise multiplication
        /// </summary>
        /// <param name="m">
        /// operand with with which to perform element-wise multiplication
        /// </param>
        /// <returns>
        /// reference to a new Mat object
        /// </returns>
        public Mat mul(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1mul__JJ(nativeObj, m.nativeObj)));

        }

        /// <remarks>
        /// Matrix multiplication
        /// </remarks>
        /// <param name="m">
        /// operand with with which to perform matrix multiplication
        /// </param>
        /// <returns>
        /// reference to a new Mat object
        /// </returns>
        public Mat matMul(Mat m)
        {
            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1matMul__JJ(nativeObj, m.nativeObj)));
        }

        //
        // C++: static Mat Mat::ones(int rows, int cols, int type)
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
        /// <param name="rows">
        /// Number of rows.
        /// </param>
        /// <param name="cols">
        /// Number of columns.
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat ones(int rows, int cols, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1ones__III(rows, cols, type)));

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
        public static Mat ones(Size size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1ones__DDI(size.width, size.height, type)));

        }

        //
        // C++: static Mat Mat::ones(int ndims, const int* sizes, int type)
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
        /// <param name="sizes">
        /// Array of integers specifying an n-dimensional array shape.
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat ones(int[] sizes, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1ones__I_3II(sizes.Length, sizes, sizes.Length, type)));

        }

        //
        // C++: void Mat::push_back(Mat m)
        //

        /// <summary>
        /// Adds elements to the bottom of the matrix.
        /// </summary>
        /// <remarks>
        /// The methods add one or more elements to the bottom of the matrix. They emulate the corresponding
        /// method of the STL vector class. When elem is Mat, its type and the number of columns must be the
        /// same as in the container matrix.
        /// </remarks>
        /// <param name="m">
        /// Added line(s).
        /// </param>
        public void push_back(Mat m)
        {
            if (m != null)
                m.ThrowIfDisposed();
            ThrowIfDisposed();

            core_Mat_n_1push_1back(nativeObj, m.nativeObj);

        }

        //
        // C++: void Mat::release()
        //

        /// <summary>
        /// Decrements the reference counter and deallocates the matrix if needed.
        /// </summary>
        /// <remarks>
        /// The method decrements the reference counter associated with the matrix data. When the reference
        /// counter reaches 0, the matrix data is deallocated and the data and the reference counter pointers
        /// are set to NULL's. If the matrix header points to an external data set (see Mat::Mat), the
        /// reference counter is NULL, and the method has no effect in this case.
        /// 
        /// This method can be called manually to force the matrix data deallocation. But since this method is
        /// automatically called in the destructor, or by any other method that changes the data pointer, it is
        /// usually not needed. The reference counter decrement and check for 0 is an atomic operation on the
        /// platforms that support it. Thus, it is safe to operate on the same matrices asynchronously in
        /// different threads.
        /// </remarks>
        public void release()
        {
            ThrowIfDisposed();

            core_Mat_n_1release(nativeObj);

        }

        //
        // C++: Mat Mat::reshape(int cn, int rows = 0)
        //

        /// <summary>
        /// Changes the shape and/or the number of channels of a 2D matrix without copying the data.
        /// </summary>
        /// <remarks>
        /// The method makes a new matrix header for *this elements. The new matrix may have a different size
        /// and/or different number of channels. Any combination is possible if:
        /// -   No extra elements are included into the new matrix and no elements are excluded. Consequently,
        ///     the product rows*cols*channels() must stay the same after the transformation.
        /// -   No data is copied. That is, this is an O(1) operation. Consequently, if you change the number of
        ///     rows, or the operation changes the indices of elements row in some other way, the matrix must be
        ///     continuous. See Mat.isContinuous.
        ///
        /// For example, if there is a set of 3D points stored as an STL vector, and you want to represent the
        /// points as a 3xN matrix, do the following:
        /// <example>
        /// <code language="c++">
        ///     std::vector&lt;Point3f&gt; vec;
        ///     ...
        ///     Mat pointMat = Mat(vec). // convert vector to Mat, O(1) operation
        ///                       reshape(1). // make Nx3 1-channel matrix out of Nx1 3-channel.
        ///                                   // Also, an O(1) operation
        ///                          t(); // finally, transpose the Nx3 matrix.
        ///                                   // This involves copying all the elements
        /// </code>
        /// </example>
        /// 3-channel 2x2 matrix reshaped to 1-channel 4x3 matrix, each column has values from one of original channels:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// vector&lt;int&gt; new_shape {4, 3};
        /// m = m.reshape(1, new_shape);
        /// </code>
        /// </example>
        /// or:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// const int new_shape[] = {4, 3};
        /// m = m.reshape(1, 2, new_shape);
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="cn">
        /// New number of channels. If the parameter is 0, the number of channels remains the same.
        /// </param>
        /// <param name="rows">
        /// New number of rows. If the parameter is 0, the number of rows remains the same.
        /// </param>

        public Mat reshape(int cn, int rows)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1reshape__JII(nativeObj, cn, rows)));

        }

        /// <summary>
        /// Changes the shape and/or the number of channels of a 2D matrix without copying the data.
        /// </summary>
        /// <remarks>
        /// The method makes a new matrix header for *this elements. The new matrix may have a different size
        /// and/or different number of channels. Any combination is possible if:
        /// -   No extra elements are included into the new matrix and no elements are excluded. Consequently,
        ///     the product rows*cols*channels() must stay the same after the transformation.
        /// -   No data is copied. That is, this is an O(1) operation. Consequently, if you change the number of
        ///     rows, or the operation changes the indices of elements row in some other way, the matrix must be
        ///     continuous. See Mat.isContinuous.
        ///
        /// For example, if there is a set of 3D points stored as an STL vector, and you want to represent the
        /// points as a 3xN matrix, do the following:
        /// <example>
        /// <code language="c++">
        ///     std::vector&lt;Point3f&gt; vec;
        ///     ...
        ///     Mat pointMat = Mat(vec). // convert vector to Mat, O(1) operation
        ///                       reshape(1). // make Nx3 1-channel matrix out of Nx1 3-channel.
        ///                                   // Also, an O(1) operation
        ///                          t(); // finally, transpose the Nx3 matrix.
        ///                                   // This involves copying all the elements
        /// </code>
        /// </example>
        /// 3-channel 2x2 matrix reshaped to 1-channel 4x3 matrix, each column has values from one of original channels:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// vector&lt;int&gt; new_shape {4, 3};
        /// m = m.reshape(1, new_shape);
        /// </code>
        /// </example>
        /// or:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// const int new_shape[] = {4, 3};
        /// m = m.reshape(1, 2, new_shape);
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="cn">
        /// New number of channels. If the parameter is 0, the number of channels remains the same.
        /// </param>
        public Mat reshape(int cn)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1reshape__JI(nativeObj, cn)));

        }

        //
        // C++: Mat Mat::reshape(int cn, int newndims, const int* newsz)
        //

        /// <summary>
        /// Changes the shape and/or the number of channels of a 2D matrix without copying the data.
        /// </summary>
        /// <remarks>
        /// The method makes a new matrix header for *this elements. The new matrix may have a different size
        /// and/or different number of channels. Any combination is possible if:
        /// -   No extra elements are included into the new matrix and no elements are excluded. Consequently,
        ///     the product rows*cols*channels() must stay the same after the transformation.
        /// -   No data is copied. That is, this is an O(1) operation. Consequently, if you change the number of
        ///     rows, or the operation changes the indices of elements row in some other way, the matrix must be
        ///     continuous. See Mat.isContinuous.
        ///
        /// For example, if there is a set of 3D points stored as an STL vector, and you want to represent the
        /// points as a 3xN matrix, do the following:
        /// <example>
        /// <code language="c++">
        ///     std::vector&lt;Point3f&gt; vec;
        ///     ...
        ///     Mat pointMat = Mat(vec). // convert vector to Mat, O(1) operation
        ///                       reshape(1). // make Nx3 1-channel matrix out of Nx1 3-channel.
        ///                                   // Also, an O(1) operation
        ///                          t(); // finally, transpose the Nx3 matrix.
        ///                                   // This involves copying all the elements
        /// </code>
        /// </example>
        /// 3-channel 2x2 matrix reshaped to 1-channel 4x3 matrix, each column has values from one of original channels:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// vector&lt;int&gt; new_shape {4, 3};
        /// m = m.reshape(1, new_shape);
        /// </code>
        /// </example>
        /// or:
        /// <example>
        /// <code language="c++">
        /// Mat m(Size(2, 2), CV_8UC3, Scalar(1, 2, 3));
        /// const int new_shape[] = {4, 3};
        /// m = m.reshape(1, 2, new_shape);
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="cn">
        /// New number of channels. If the parameter is 0, the number of channels remains the same.
        /// </param>
        /// <param name="newshape">
        /// Vector with new matrix size by all dimentions. If some sizes are zero,
        /// the original sizes in those dimensions are presumed.
        /// </param>
        public Mat reshape(int cn, int[] newshape)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1reshape_11(nativeObj, cn, newshape.Length, newshape, newshape.Length)));

        }

        //
        // C++: Mat Mat::row(int y)
        //

        /// <summary>
        /// Creates a matrix header for the specified matrix row.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified matrix row and returns it. This is an O(1)
        /// operation, regardless of the matrix size. The underlying data of the new matrix is shared with the
        /// original matrix. Here is the example of one of the classical basic matrix processing operations,
        /// axpy, used by LU and many other algorithms:
        /// <example>
        /// <code language="c++">
        ///     inline void matrix_axpy(Mat&amp; A, int i, int j, double alpha)
        ///     {
        ///         A.row(i) += A.row(j)*alpha;
        ///     }
        /// </code>
        /// </example>
        /// In the current implementation, the following code does not work as expected:
        /// <example>
        /// <code language="c++">
        ///     Mat A;
        ///     ...
        ///     A.row(i) = A.row(j); // will not work
        /// </code>
        /// </example>
        /// This happens because A.row(i) forms a temporary header that is further assigned to another header.
        /// Remember that each of these operations is O(1), that is, no data is copied. Thus, the above
        /// assignment is not true if you may have expected the j-th row to be copied to the i-th row. To
        /// achieve that, you should either turn this simple assignment into an expression or use the
        /// Mat.copyTo method:
        /// <example>
        /// <code language="c++">
        ///     Mat A;
        ///     ...
        ///     // works, but looks a bit obscure.
        ///     A.row(i) = A.row(j) + 0;
        ///     // this is a bit longer, but the recommended method.
        ///     A.row(j).copyTo(A.row(i));
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="y">
        /// A 0-based row index.
        /// </param>

        public Mat row(int y)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1row(nativeObj, y)));

        }

        //
        // C++: Mat Mat::rowRange(int startrow, int endrow)
        //

        /// <summary>
        /// Creates a matrix header for the specified row span.
        /// </summary>
        /// <remarks>
        /// The method makes a new header for the specified row span of the matrix. Similarly to Mat.row and
        /// Mat.col, this is an O(1) operation.
        /// </remarks>
        /// <param name="startrow">
        /// An inclusive 0-based start index of the row span.
        /// </param>
        /// <param name="endrow">
        /// An exclusive 0-based ending index of the row span.
        /// </param>
        public Mat rowRange(int startrow, int endrow)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1rowRange(nativeObj, startrow, endrow)));

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
        public Mat rowRange(Range r)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1rowRange(nativeObj, r.start, r.end)));

        }

        //
        // C++: int Mat::rows()
        //

        /// <remarks>
        /// number of rows in the matrix; -1 when the matrix has more than 2 dimensions
        /// </remarks>
        public int rows()
        {
            ThrowIfDisposed();

            return core_Mat_n_1rows(nativeObj);

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
        public Mat setTo(Scalar s)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JDDDD(nativeObj, s.val[0], s.val[1], s.val[2], s.val[3])));

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
        public Mat setTo(Scalar value, Mat mask)
        {
            if (mask != null)
                mask.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JDDDDJ(nativeObj, value.val[0], value.val[1], value.val[2], value.val[3], mask.nativeObj)));

        }

        //
        // C++: Mat Mat::setTo(Mat value, Mat mask = Mat())
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
        public Mat setTo(Mat value, Mat mask)
        {
            if (value != null)
                value.ThrowIfDisposed();
            if (mask != null)
                mask.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JJJ(nativeObj, value.nativeObj, mask.nativeObj)));

        }

        /// <summary>
        /// Sets all or some of the array elements to the specified value.
        /// </summary>
        /// <remarks>
        /// This is an advanced variant of the Mat.operator=(const Scalar&amp; s) operator.
        /// </remarks>
        /// <param name="value">
        /// Assigned scalar converted to the actual array type.
        /// </param>
        public Mat setTo(Mat value)
        {
            if (value != null)
                value.ThrowIfDisposed();
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1setTo__JJ(nativeObj, value.nativeObj)));

        }

        //
        // C++: Size Mat::size()
        //

        /// <remarks>
        /// returns the array of sizes, or NULL if the matrix is not allocated
        /// </remarks>
        public Size size()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            core_Mat_n_1size(nativeObj, tmpArray);

            return new Size(tmpArray);

        }

        //
        // C++: int Mat::size(int i)
        //

        /// <remarks>
        /// returns the size of i-th matrix dimension (or 0)
        /// </remarks>
        /// <param name="i"></param>
        public int size(int i)
        {
            ThrowIfDisposed();

            return core_Mat_n_1size_1i__JI(nativeObj, i);

        }

        //
        // C++: size_t Mat::step1(int i = 0)
        //

        /// <summary>
        /// Returns a normalized step.
        /// </summary>
        /// <remarks>
        /// The method returns a matrix step divided by Mat::elemSize1(). It can be useful to quickly access an
        /// arbitrary matrix element.
        /// </remarks>
        public long step1(int i)
        {
            ThrowIfDisposed();

            return core_Mat_n_1step1__JI(nativeObj, i);

        }

        /// <summary>
        /// Returns a normalized step.
        /// </summary>
        /// <remarks>
        /// The method returns a matrix step divided by Mat::elemSize1(). It can be useful to quickly access an
        /// arbitrary matrix element.
        /// </remarks>
        public long step1()
        {
            ThrowIfDisposed();

            return core_Mat_n_1step1__J(nativeObj);

        }

        //
        // C++: Mat Mat::operator()(int rowStart, int rowEnd, int colStart, int
        // colEnd)
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
        /// <param name="rowStart">
        /// Start row of the extracted submatrix.
        /// </param>
        /// <param name="rowEnd">
        /// End row of the extracted submatrix.
        /// </param>
        /// <param name="colStart">
        /// Start column of the extracted submatrix.
        /// </param>
        /// <param name="colEnd">
        /// End column of the extracted submatrix.
        /// </param>
        public Mat submat(int rowStart, int rowEnd, int colStart, int colEnd)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1submat_1rr(nativeObj, rowStart, rowEnd, colStart, colEnd)));

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
        public Mat submat(Range rowRange, Range colRange)
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
        public Mat submat(Range[] ranges)
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
        public Mat submat(Rect roi)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1submat(nativeObj, roi.x, roi.y, roi.width, roi.height)));

        }

        //
        // C++: Mat Mat::t()
        //

        /// <summary>
        /// Transposes a matrix.
        /// </summary>
        /// <remarks>
        /// The method performs matrix transposition by means of matrix expressions. It does not perform the
        /// actual transposition but returns a temporary matrix transposition object that can be further used as
        /// a part of more complex matrix expressions or can be assigned to a matrix:
        /// <example>
        /// <code language="c++">
        ///     Mat A1 = A + Mat.eye(A.size(), A.type())*lambda;
        ///     Mat C = A1.t()*A1; // compute (A + lambda*I)^t * (A + lamda*I)
        /// </code>
        /// </example>
        /// </remarks>
        public Mat t()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1t(nativeObj)));

        }

        //
        // C++: size_t Mat::total()
        //

        /// <summary>
        /// Returns the total number of array elements.
        /// </summary>
        /// <remarks>
        /// The method returns the number of array elements (a number of pixels if the array represents an
        /// image).
        /// </remarks>
        public long total()
        {
            ThrowIfDisposed();

            return core_Mat_n_1total(nativeObj);

        }

        //
        // C++: int Mat::type()
        //

        /// <summary>
        /// Returns the type of a matrix element.
        /// </summary>
        /// <remarks>
        /// The method returns a matrix element type. This is an identifier compatible with the CvMat type
        /// system, like CV_16SC3 or 16-bit signed 3-channel array, and so on.
        /// </remarks>
        public int type()
        {
            ThrowIfDisposed();

            return core_Mat_n_1type(nativeObj);

        }

        //
        // C++: static Mat Mat::zeros(int rows, int cols, int type)
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
        /// <param name="rows">
        /// Number of rows.
        /// </param>
        /// <param name="cols">
        /// Number of columns.
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat zeros(int rows, int cols, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1zeros__III(rows, cols, type)));

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
        public static Mat zeros(Size size, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1zeros__DDI(size.width, size.height, type)));

        }

        //
        // C++: static Mat Mat::zeros(int ndims, const int* sizes, int type)
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
        /// <param name="sizes">
        /// Array of integers specifying an n-dimensional array shape.
        /// </param>
        /// <param name="type">
        /// Created matrix type.
        /// </param>
        public static Mat zeros(int[] sizes, int type)
        {

            return new Mat(DisposableObject.ThrowIfNullIntPtr(core_Mat_n_1zeros__I_3II(sizes.Length, sizes, sizes.Length, type)));

        }

        //@Override
        public override string ToString()
        {

            String _dims = (dims() > 0) ? "" : "-1*-1*";
            for (int i = 0; i < dims(); i++)
            {
                _dims += size(i) + "*";
            }
            return "Mat [ " + _dims + CvType.typeToString(type()) +
                    ", isCont=" + isContinuous() + ", isSubmat=" + isSubmatrix() +
                    ", nativeObj=0x" + Convert.ToString(nativeObj) +
                    ", dataAddr=0x" + Convert.ToString(dataAddr()) +
                    " ]";

        }

        /// <summary>
        /// Returns the contents of the visualized matrix in String format.
        /// </summary>
        /// <remarks>
        /// This method returns the matrix elements in String format, formatted in a human-readable format. This is useful for debugging to quickly visualize the matrix contents.
        /// Only works with two-dimensional Mat.
        /// </remarks>
        /// <returns>
        /// Returns the contents of the visualized matrix in String format.
        /// </returns>
        public string dump()
        {
            ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(core_Mat_nDump(nativeObj));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);
            return retVal;

        }

        // javadoc:Mat::put(row,col,data)
        /// <summary>
        /// Writes a double array to the matrix at the given row and column, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified double array to the matrix at the provided row and column indices. 
        /// The values are automatically cast to match the matrix's data type. 
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int row, int col, params double[] data)
        {
            ThrowIfDisposed();

            int t = type();
            if (data == null || data.Length % CvType.channels(t) != 0)
                throw new CvException(
                    "Provided data element number (" +
                    (data == null ? 0 : data.Length) +
                    ") should be multiple of the Mat channels count (" +
                    CvType.channels(t) + ")");
            return core_Mat_nPutD(nativeObj, row, col, data.Length, data);

        }

        // javadoc:Mat::put(idx,data)
        /// <summary>
        /// Writes a double array to the matrix at the given indices, casting them to match the matrix's data type.
        /// </summary>
        /// <param name="idx">An array specifying the multi-dimensional indices where the data will be written.</param>
        /// <param name="data">The double array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified double array to the matrix at the provided indices. 
        /// The values are automatically cast to match the matrix's data type. 
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, or if the number of elements is not a multiple of the matrix's channel count.
        /// </exception>
        public int put(int[] idx, params double[] data)
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
            return core_Mat_nPutDIdx(nativeObj, idx, idx.Length, data.Length, data);

        }

        // javadoc:Mat::put(row,col,data)
        /// <summary>
        /// Writes a float array to the matrix at a specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified float array to the matrix at the given row and column.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int row, int col, float[] data)
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
                return core_Mat_nPutF(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::put(idx,data)
        /// <summary>
        /// Writes a float array to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array specifying the multi-dimensional indices where the data will be written.</param>
        /// <param name="data">The float array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified float array to the matrix at the given multi-dimensional indices.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// if the number of indices in <paramref name="idx"/> does not match the matrix's dimensionality, 
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int put(int[] idx, float[] data)
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
                return core_Mat_nPutFIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::put(row,col,data)
        /// <summary>
        /// Writes an int array to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified int array to the matrix at the given row and column indices.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int row, int col, int[] data)
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
                return core_Mat_nPutI(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::put(idx,data)
        /// <summary>
        /// Writes an int array to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices where the data will be written.</param>
        /// <param name="data">The int array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified int array to the matrix at the given indices.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// The number of indices provided must match the number of dimensions of the matrix.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// if the number of indices does not match the matrix's dimensions, 
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int put(int[] idx, int[] data)
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
                return core_Mat_nPutIIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::put(row,col,data)
        /// <summary>
        /// Writes a short array to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified short array to the matrix at the given row and column.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int row, int col, short[] data)
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

        // javadoc:Mat::put(idx,data)
        /// <summary>
        /// Writes a short array to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices where the data will be written.</param>
        /// <param name="data">The short array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified short array to the matrix at the given indices.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// The number of indices provided must match the matrix's dimensions.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// if the number of indices does not match the matrix's dimensions, 
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int put(int[] idx, short[] data)
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

        // javadoc:Mat::put(row,col,data)
        /// <summary>
        /// Writes a byte array to the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified byte array to the matrix at the given row and column.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int row, int col, byte[] data)
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

        // javadoc:Mat::put(idx,data)
        /// <summary>
        /// Writes a byte array to the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified byte array to the matrix at the given indices.
        /// The number of elements in the <paramref name="data"/> array must be a multiple of the matrix's channel count.
        /// The number of indices provided in <paramref name="idx"/> must match the number of dimensions of the matrix.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count, 
        /// if the number of indices does not match the matrix's dimensions, 
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, byte[] data)
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

        // javadoc:Mat::put(row,col,data,offset,length)
        /// <summary>
        /// Writes a specified number of byte values to the matrix at the given row and column, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="row">The row index where the data will be written.</param>
        /// <param name="col">The column index where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of byte values to the matrix at the provided row and column indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int row, int col, byte[] data, int offset, int length)
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

        // javadoc:Mat::put(idx,data,offset,length)
        /// <summary>
        /// Writes a specified number of byte values to the matrix at the given indices, starting from the specified offset within the data array.
        /// </summary>
        /// <param name="idx">The array of indices where the data will be written.</param>
        /// <param name="data">The byte array containing the data elements to write.</param>
        /// <param name="offset">The offset in the data array from which to start writing elements.</param>
        /// <param name="length">The number of elements to write from the data array.</param>
        /// <returns>The number of elements successfully written to the matrix.</returns>
        /// <remarks>
        /// This method writes the specified number of byte values to the matrix at the provided indices, starting from the specified offset within the data array.
        /// The number of elements written must be a multiple of the matrix's channel count.
        /// The matrix must be of type <c>CV_8U</c> or <c>CV_8S</c> for compatibility.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the number of elements is not a multiple of the matrix's channel count,
        /// or if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int put(int[] idx, byte[] data, int offset, int length)
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

        // javadoc:Mat::get(row,col,data)
        /// <summary>
        /// Reads a byte array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified byte array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int row, int col, byte[] data)
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

        // javadoc:Mat::get(idx,data)
        /// <summary>
        /// Reads a byte array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The byte array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified byte array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_8U</c> or <c>CV_8S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_8U</c> or <c>CV_8S</c>.
        /// </exception>
        public int get(int[] idx, byte[] data)
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

        // javadoc:Mat::get(row,col,data)
        /// <summary>
        /// Reads a short array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified short array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int row, int col, short[] data)
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

        // javadoc:Mat::get(idx,data)
        /// <summary>
        /// Reads a short array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The short array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified short array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_16U</c> or <c>CV_16S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_16U</c> or <c>CV_16S</c>.
        /// </exception>
        public int get(int[] idx, short[] data)
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

        // javadoc:Mat::get(row,col,data)
        /// <summary>
        /// Reads an int array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified int array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int get(int row, int col, int[] data)
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
                return core_Mat_nGetI(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(idx,data)
        /// <summary>
        /// Reads an int array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The int array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified int array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_32S</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32S</c>.
        /// </exception>
        public int get(int[] idx, int[] data)
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
                return core_Mat_nGetIIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(row,col,data)
        /// <summary>
        /// Reads a float array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified float array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int row, int col, float[] data)
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
                return core_Mat_nGetF(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(idx,data)
        /// <summary>
        /// Reads a float array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The float array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified float array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_32F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_32F</c>.
        /// </exception>
        public int get(int[] idx, float[] data)
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
                return core_Mat_nGetFIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(row,col,data)
        /// <summary>
        /// Reads a double array from the matrix at the specified row and column.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified double array from the matrix at the given row and column.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int row, int col, double[] data)
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
                return core_Mat_nGetD(nativeObj, row, col, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(idx,data)
        /// <summary>
        /// Reads a double array from the matrix at the specified indices.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <param name="data">The double array where the read data elements will be stored.</param>
        /// <returns>The number of elements successfully read from the matrix.</returns>
        /// <remarks>
        /// This method reads the specified double array from the matrix at the given indices.
        /// The length of the <paramref name="data"/> array must be sufficient to hold the data,
        /// and it must be a multiple of the matrix's channel count.
        /// The number of indices in <paramref name="idx"/> must match the matrix's dimension count.
        /// Only matrices with a data type of <c>CV_64F</c> are compatible with this method.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the data array is null, if the length of the data array is not a multiple of the matrix's channel count,
        /// if the number of indices is incorrect, or if the matrix's data type is not <c>CV_64F</c>.
        /// </exception>
        public int get(int[] idx, double[] data)
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
                return core_Mat_nGetDIdx(nativeObj, idx, idx.Length, data.Length, data);
            }
            throw new CvException("Mat data type is not compatible: " + t);

        }

        // javadoc:Mat::get(row,col)
        /// <summary>
        /// Reads the element values from the matrix at the specified row and column, returning them as an array of double values.
        /// </summary>
        /// <param name="row">The row index from which the data will be read.</param>
        /// <param name="col">The column index from which the data will be read.</param>
        /// <returns>
        /// An array of double values representing the matrix element(s) at the specified row and column. 
        /// The length of the array corresponds to the number of channels in the matrix at the specified location. 
        /// If the read operation fails, returns null.
        /// </returns>
        /// <remarks>
        /// This method reads the element values at the specified row and column in the matrix and returns them as double values. 
        /// Regardless of the matrix's actual data type, the values are cast to double. 
        /// The size of the returned array depends on the number of channels in the matrix.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if an error occurs while retrieving the data.
        /// </exception>
        public double[] get(int row, int col)
        {

            ThrowIfDisposed();

            double[] tmpArray = new double[channels()];
            int result = core_Mat_nGet(nativeObj, row, col, tmpArray.Length, tmpArray);

            if (result == 0)
            {
                return null;
            }
            else
            {
                return tmpArray;
            }

        }

        // javadoc:Mat::get(idx)
        /// <summary>
        /// Reads the element values from the matrix at the specified indices, returning them as an array of double values.
        /// </summary>
        /// <param name="idx">An array of indices specifying the location from which the data will be read.</param>
        /// <returns>
        /// An array of double values representing the matrix element(s) at the specified indices. 
        /// The length of the array corresponds to the number of channels in the matrix at the specified location. 
        /// If the read operation fails, returns null.
        /// </returns>
        /// <remarks>
        /// This method reads the element values at the specified indices in the matrix and returns them as double values. 
        /// Regardless of the matrix's actual data type, the values are cast to double. 
        /// The size of the returned array depends on the number of channels in the matrix.
        /// </remarks>
        /// <exception cref="CvException">
        /// Thrown if the matrix is disposed or if an error occurs while retrieving the data, if the number of indices is incorrect.
        /// </exception>
        public double[] get(int[] idx)
        {
            ThrowIfDisposed();

            if (idx.Length != dims())
                throw new CvException("Incorrect number of indices");
            double[] tmpArray = new double[channels()];
            int result = core_Mat_nGetIdx(nativeObj, idx, idx.Length, tmpArray.Length, tmpArray);

            if (result == 0)
            {
                return null;
            }
            else
            {
                return tmpArray;
            }

        }

        /// <summary>
        /// Returns the number of rows in the matrix.
        /// </summary>
        /// <remarks>
        /// The method returns the number of rows in the matrix, which corresponds to the height
        /// of the image represented by the matrix. If the matrix is empty (i.e., it has no elements),
        /// the method will return 0.
        /// </remarks>
        /// <returns>
        /// The number of rows in the matrix.
        /// </returns>
        public int height()
        {

            return rows();

        }

        /// <summary>
        /// Returns the number of columns in the matrix.
        /// </summary>
        /// <remarks>
        /// The method returns the number of columns in the matrix, which corresponds to the width
        /// of the image represented by the matrix. If the matrix is empty (i.e., it has no elements),
        /// the method will return 0.
        /// </remarks>
        /// <returns>
        /// The number of columns in the matrix.
        /// </returns>
        public int width()
        {

            return cols();

        }

        public IntPtr getNativeObjAddr()
        {

            return nativeObj;

        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";

#else
        const string LIBNAME = "opencvforunity";
#endif

        // C++: Mat::Mat()
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__();

        // C++: Mat::Mat(int rows, int cols, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__III(int rows, int cols, int type);

        // C++: Mat::Mat(int ndims, const int* sizes, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__I_3II(int ndims, int[] sizes, int length, int type);

        // C++: Mat::Mat(Size size, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__DDI(double size_width, double size_height, int type);

        // C++: Mat::Mat(int rows, int cols, int type, Scalar s)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__IIIDDDD(int rows, int cols, int type, double s_val0, double s_val1, double s_val2, double s_val3);

        // C++: Mat::Mat(Size size, int type, Scalar s)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__DDIDDDD(double size_width, double size_height, int type, double s_val0, double s_val1, double s_val2, double s_val3);

        // C++: Mat::Mat(int ndims, const int* sizes, int type, Scalar s)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__I_3IIDDDD(int ndims, int[] sizes, int length, int type, double s_val0, double s_val1, double s_val2, double s_val3);

        // C++: Mat::Mat(Mat m, Range rowRange, Range colRange = Range::all())
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__JIIII(IntPtr m_nativeObj, int rowRange_start, int rowRange_end, int colRange_start, int colRange_end);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__JII(IntPtr m_nativeObj, int rowRange_start, int rowRange_end);

        // C++: Mat::Mat(const Mat& m, const std::vector<Range>& ranges)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__J_3Lorg_opencv_core_Range_2(IntPtr m_nativeObj, int[] rangesArray, int length);

        // C++: Mat::Mat (int rows, int cols, int type, void *data, size_t step=AUTO_STEP)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__IIIVL(int rows, int cols, int type, IntPtr data, long step);

        // C++: Mat::Mat (Size size, int type, void *data, size_t step=AUTO_STEP)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1Mat__DDIVL(double size_width, double size_height, int type, IntPtr data, long step);

        // C++: Mat Mat::adjustROI(int dtop, int dbottom, int dleft, int dright)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1adjustROI(IntPtr nativeObj, int dtop, int dbottom, int dleft, int dright);

        // C++: void Mat::assignTo(Mat m, int type = -1)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1assignTo__JJI(IntPtr nativeObj, IntPtr m_nativeObj, int type);

        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1assignTo__JJ(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: int Mat::channels()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1channels(IntPtr nativeObj);

        // C++: int Mat::checkVector(int elemChannels, int depth = -1, bool
        // requireContinuous = true)
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1checkVector__JIIZ(IntPtr nativeObj, int elemChannels, int depth, [MarshalAs(UnmanagedType.U1)] bool requireContinuous);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1checkVector__JII(IntPtr nativeObj, int elemChannels, int depth);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1checkVector__JI(IntPtr nativeObj, int elemChannels);

        // C++: Mat Mat::clone()
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1clone(IntPtr nativeObj);

        // C++: Mat Mat::col(int x)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1col(IntPtr nativeObj, int x);

        // C++: Mat Mat::colRange(int startcol, int endcol)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1colRange(IntPtr nativeObj, int startcol, int endcol);

        // C++: int Mat::dims()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1dims(IntPtr nativeObj);

        // C++: int Mat::cols()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1cols(IntPtr nativeObj);

        // C++: void Mat::convertTo(Mat& m, int rtype, double alpha = 1, double beta
        // = 0)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1convertTo__JJIDD(IntPtr nativeObj, IntPtr m_nativeObj, int rtype, double alpha, double beta);

        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1convertTo__JJID(IntPtr nativeObj, IntPtr m_nativeObj, int rtype, double alpha);

        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1convertTo__JJI(IntPtr nativeObj, IntPtr m_nativeObj, int rtype);

        // C++: void Mat::copyTo(Mat& m)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1copyTo__JJ(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: void Mat::copyTo(Mat& m, Mat mask)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1copyTo__JJJ(IntPtr nativeObj, IntPtr m_nativeObj, IntPtr mask_nativeObj);

        // C++: void Mat::create(int rows, int cols, int type)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1create__JIII(IntPtr nativeObj, int rows, int cols, int type);

        // C++: void Mat::create(Size size, int type)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1create__JDDI(IntPtr nativeObj, double size_width, double size_height, int type);

        // C++: void Mat::create(int ndims, const int* sizes, int type)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1create__JI_3II(IntPtr nativeObj, int ndims, int[] sizes, int length, int type);

        // C++: void Mat::copySize(const Mat& m)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1copySize(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: Mat Mat::cross(Mat m)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1cross(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: long Mat::dataAddr()
        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1dataAddr(IntPtr nativeObj);

        // C++: int Mat::depth()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1depth(IntPtr nativeObj);

        // C++: Mat Mat::diag(int d = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1diag__JI(IntPtr nativeObj, int d);

        // C++: static Mat Mat::diag(Mat d)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1diag__J(IntPtr d_nativeObj);

        // C++: double Mat::dot(Mat m)
        [DllImport(LIBNAME)]
        private static extern double core_Mat_n_1dot(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: size_t Mat::elemSize()
        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1elemSize(IntPtr nativeObj);

        // C++: size_t Mat::elemSize1()
        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1elemSize1(IntPtr nativeObj);

        // C++: bool Mat::empty()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool core_Mat_n_1empty(IntPtr nativeObj);

        // C++: static Mat Mat::eye(int rows, int cols, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1eye__III(int rows, int cols, int type);

        // C++: static Mat Mat::eye(Size size, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1eye__DDI(double size_width, double size_height, int type);

        // C++: Mat Mat::inv(int method = DECOMP_LU)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1inv__JI(IntPtr nativeObj, int method);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1inv__J(IntPtr nativeObj);

        // C++: bool Mat::isContinuous()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool core_Mat_n_1isContinuous(IntPtr nativeObj);

        // C++: bool Mat::isSubmatrix()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool core_Mat_n_1isSubmatrix(IntPtr nativeObj);

        // C++: void Mat::locateROI(Size wholeSize, Point ofs)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_locateROI_10(IntPtr nativeObj, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] double[] wholeSize_out, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] double[] ofs_out);

        // C++: Mat Mat::mul(Mat m, double scale = 1)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1mul__JJD(IntPtr nativeObj, IntPtr m_nativeObj, double scale);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1mul__JJ(IntPtr nativeObj, IntPtr m_nativeObj);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1matMul__JJ(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: static Mat Mat::ones(int rows, int cols, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1ones__III(int rows, int cols, int type);

        // C++: static Mat Mat::ones(Size size, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1ones__DDI(double size_width, double size_height, int type);

        // C++: static Mat Mat::ones(int ndims, const int* sizes, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1ones__I_3II(int ndims, int[] sizes, int length, int type);

        // C++: void Mat::push_back(Mat m)
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1push_1back(IntPtr nativeObj, IntPtr m_nativeObj);

        // C++: void Mat::release()
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1release(IntPtr nativeObj);

        // C++: Mat Mat::reshape(int cn, int rows = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1reshape__JII(IntPtr nativeObj, int cn, int rows);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1reshape__JI(IntPtr nativeObj, int cn);

        // C++: Mat Mat::reshape(int cn, int newndims, const int* newsz)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1reshape_11(IntPtr nativeObj, int cn, int newndims, int[] newsz, int length);

        // C++: Mat Mat::row(int y)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1row(IntPtr nativeObj, int y);

        // C++: Mat Mat::rowRange(int startrow, int endrow)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1rowRange(IntPtr nativeObj, int startrow, int endrow);

        // C++: int Mat::rows()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1rows(IntPtr nativeObj);

        // C++: Mat Mat::operator =(Scalar s)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1setTo__JDDDD(IntPtr nativeObj, double s_val0, double s_val1, double s_val2, double s_val3);

        // C++: Mat Mat::setTo(Scalar value, Mat mask = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1setTo__JDDDDJ(IntPtr nativeObj, double s_val0, double s_val1, double s_val2, double s_val3, IntPtr mask_nativeObj);

        // C++: Mat Mat::setTo(Mat value, Mat mask = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1setTo__JJJ(IntPtr nativeObj, IntPtr value_nativeObj, IntPtr mask_nativeObj);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1setTo__JJ(IntPtr nativeObj, IntPtr value_nativeObj);

        // C++: Size Mat::size()
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1size(IntPtr nativeObj,
                                                     [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] double[] vals);

        // C++: int Mat::size(int i)
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1size_1i__JI(IntPtr nativeObj, int i);

        // C++: size_t Mat::step1(int i = 0)
        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1step1__JI(IntPtr nativeObj, int i);

        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1step1__J(IntPtr nativeObj);

        // C++: Mat Mat::operator()(Range rowRange, Range colRange)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1submat_1rr(IntPtr nativeObj, int rowRange_start, int rowRange_end, int colRange_start, int colRange_end);

        // C++: Mat Mat::operator()(const std::vector<Range>& ranges)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1submat_1ranges(IntPtr nativeObj, int[] rangesArray, int length);

        // C++: Mat Mat::operator()(Rect roi)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1submat(IntPtr nativeObj, int roi_x, int roi_y, int roi_width, int roi_height);

        // C++: Mat Mat::t()
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1t(IntPtr nativeObj);

        // C++: size_t Mat::total()
        [DllImport(LIBNAME)]
        private static extern long core_Mat_n_1total(IntPtr nativeObj);

        // C++: int Mat::type()
        [DllImport(LIBNAME)]
        private static extern int core_Mat_n_1type(IntPtr nativeObj);

        // C++: static Mat Mat::zeros(int rows, int cols, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1zeros__III(int rows, int cols, int type);

        // C++: static Mat Mat::zeros(Size size, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1zeros__DDI(double size_width, double size_height, int type);

        // C++: static Mat Mat::zeros(int ndims, const int* sizes, int type)
        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_n_1zeros__I_3II(int ndims, int[] sizes, int length, int type);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void core_Mat_n_1delete(IntPtr nativeObj);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutD(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutDIdx(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutF(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutFIdx(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutI(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutIIdx(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutS(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutSIdx(IntPtr self, int[] idx, int length, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutB(IntPtr self, int row, int col, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBIdx(IntPtr self, int[] idx, int lenght, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwOffset(IntPtr self, int row, int col, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nPutBwIdxOffset(IntPtr self, int[] idx, int length, int count, int offset, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetB(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetBIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetS(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetSIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] short[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetI(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetF(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetFIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetD(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetDIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGet(IntPtr self, int row, int col, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);

        [DllImport(LIBNAME)]
        private static extern int core_Mat_nGetIdx(IntPtr self, int[] idx, int length, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] double[] vals);

        [DllImport(LIBNAME)]
        private static extern IntPtr core_Mat_nDump(IntPtr self);

    }
}
