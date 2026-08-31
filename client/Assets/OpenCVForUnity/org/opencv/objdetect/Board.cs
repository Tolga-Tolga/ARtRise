

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    // C++: class Board
    /// <summary>
    ///  Board of ArUco markers
    /// </summary>
    /// <remarks>
    ///     A board is a set of markers in the 3D space with a common coordinate system.
    ///     The common form of a board of marker is a planar (2D) board, however any 3D layout can be used.
    ///     A Board object is composed by:
    ///     - The object points of the marker corners, i.e. their coordinates respect to the board system.
    ///     - The dictionary which indicates the type of markers of the board
    ///     - The identifier of all the markers in the board.
    /// </remarks>
    public partial class Board : DisposableOpenCVObject
    {

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
                        objdetect_Board_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Board(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static Board __fromPtr__(IntPtr addr) { return new Board(addr); }

        //
        // C++:   cv::aruco::Board::Board(vector_Mat objPoints, Dictionary dictionary, Mat ids)
        //

        /// <summary>
        ///  Common Board constructor
        /// </summary>
        /// <param name="objPoints">
        /// array of object points of all the marker corners in the board
        /// </param>
        /// <param name="dictionary">
        /// the dictionary of markers employed for this board
        /// </param>
        /// <param name="ids">
        /// vector of the identifiers of the markers in the board
        /// </param>
        public Board(List<Mat> objPoints, Dictionary dictionary, Mat ids)
        {
            if (dictionary != null) dictionary.ThrowIfDisposed();
            if (ids != null) ids.ThrowIfDisposed();
            using Mat objPoints_mat = Converters.vector_Mat_to_Mat(objPoints);
            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_Board_Board_10(objPoints_mat.nativeObj, dictionary.getNativeObjAddr(), ids.nativeObj));


        }


        //
        // C++:  Dictionary cv::aruco::Board::getDictionary()
        //

        /// <summary>
        ///  return the Dictionary of markers employed for this board
        /// </summary>
        public Dictionary getDictionary()
        {
            ThrowIfDisposed();

            return new Dictionary(DisposableObject.ThrowIfNullIntPtr(objdetect_Board_getDictionary_10(nativeObj)));


        }


        //
        // C++:  vector_vector_Point3f cv::aruco::Board::getObjPoints()
        //

        /// <summary>
        ///  return array of object points of all the marker corners in the board.
        /// </summary>
        /// <remarks>
        ///         Each marker include its 4 corners in this order:
        ///         -   objPoints[i][0] - left-top point of i-th marker
        ///         -   objPoints[i][1] - right-top point of i-th marker
        ///         -   objPoints[i][2] - right-bottom point of i-th marker
        ///         -   objPoints[i][3] - left-bottom point of i-th marker
        ///        
        ///         Markers are placed in a certain order - row by row, left to right in every row. For M markers, the size is Mx4.
        /// </remarks>
        public List<MatOfPoint3f> getObjPoints()
        {
            ThrowIfDisposed();
            List<MatOfPoint3f> retVal = new List<MatOfPoint3f>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_Board_getObjPoints_10(nativeObj)));
            Converters.Mat_to_vector_vector_Point3f(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  vector_int cv::aruco::Board::getIds()
        //

        /// <summary>
        ///  vector of the identifiers of the markers in the board (should be the same size as objPoints)
        /// </summary>
        /// <returns>
        ///  vector of the identifiers of the markers
        /// </returns>
        public MatOfInt getIds()
        {
            ThrowIfDisposed();

            return MatOfInt.fromNativeAddr(DisposableObject.ThrowIfNullIntPtr(objdetect_Board_getIds_10(nativeObj)));


        }


        //
        // C++:  Point3f cv::aruco::Board::getRightBottomCorner()
        //

        /// <summary>
        ///  get coordinate of the bottom right corner of the board, is set when calling the function create()
        /// </summary>
        public Point3 getRightBottomCorner()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            objdetect_Board_getRightBottomCorner_10(nativeObj, tmpArray);
            Point3 retVal = new Point3(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::aruco::Board::matchImagePoints(vector_Mat detectedCorners, Mat detectedIds, Mat& objPoints, Mat& imgPoints)
        //

        /// <summary>
        ///  Given a board configuration and a set of detected markers, returns the corresponding
        ///         image points and object points, can be used in solvePnP()
        /// </summary>
        /// <param name="detectedCorners">
        /// List of detected marker corners of the board.
        ///         For cv::Board and cv::GridBoard the method expects std::vector&lt;std::vector<Point2f>&gt; or std::vector<Mat> with Aruco marker corners.
        ///         For cv::CharucoBoard the method expects std::vector<Point2f> or Mat with ChAruco corners (chess board corners matched with Aruco markers).
        /// </param>
        /// <param name="detectedIds">
        /// List of identifiers for each marker or charuco corner.
        ///         For any Board class the method expects std::vector<int> or Mat.
        /// </param>
        /// <param name="objPoints">
        /// Vector of marker points in the board coordinate space.
        ///         For any Board class the method expects std::vector&lt;cv::Point3f&gt; objectPoints or cv::Mat
        /// </param>
        /// <param name="imgPoints">
        /// Vector of marker points in the image coordinate space.
        ///         For any Board class the method expects std::vector&lt;cv::Point2f&gt; objectPoints or cv::Mat
        /// </param>
        /// <remarks>
        ///         @sa solvePnP
        /// </remarks>
        public void matchImagePoints(List<Mat> detectedCorners, Mat detectedIds, Mat objPoints, Mat imgPoints)
        {
            ThrowIfDisposed();
            if (detectedIds != null) detectedIds.ThrowIfDisposed();
            if (objPoints != null) objPoints.ThrowIfDisposed();
            if (imgPoints != null) imgPoints.ThrowIfDisposed();
            using Mat detectedCorners_mat = Converters.vector_Mat_to_Mat(detectedCorners);
            objdetect_Board_matchImagePoints_10(nativeObj, detectedCorners_mat.nativeObj, detectedIds.nativeObj, objPoints.nativeObj, imgPoints.nativeObj);


        }


        //
        // C++:  void cv::aruco::Board::generateImage(Size outSize, Mat& img, int marginSize = 0, int borderBits = 1)
        //

        /// <summary>
        ///  Draw a planar board
        /// </summary>
        /// <param name="outSize">
        /// size of the output image in pixels.
        /// </param>
        /// <param name="img">
        /// output image with the board. The size of this image will be outSize
        ///         and the board will be on the center, keeping the board proportions.
        /// </param>
        /// <param name="marginSize">
        /// minimum margins (in pixels) of the board in the output image
        /// </param>
        /// <param name="borderBits">
        /// width of the marker borders.
        /// </param>
        /// <remarks>
        ///         This function return the image of the board, ready to be printed.
        /// </remarks>
        public void generateImage(Size outSize, Mat img, int marginSize, int borderBits)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            objdetect_Board_generateImage_10(nativeObj, outSize.width, outSize.height, img.nativeObj, marginSize, borderBits);


        }

        /// <summary>
        ///  Draw a planar board
        /// </summary>
        /// <param name="outSize">
        /// size of the output image in pixels.
        /// </param>
        /// <param name="img">
        /// output image with the board. The size of this image will be outSize
        ///         and the board will be on the center, keeping the board proportions.
        /// </param>
        /// <param name="marginSize">
        /// minimum margins (in pixels) of the board in the output image
        /// </param>
        /// <param name="borderBits">
        /// width of the marker borders.
        /// </param>
        /// <remarks>
        ///         This function return the image of the board, ready to be printed.
        /// </remarks>
        public void generateImage(Size outSize, Mat img, int marginSize)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            objdetect_Board_generateImage_11(nativeObj, outSize.width, outSize.height, img.nativeObj, marginSize);


        }

        /// <summary>
        ///  Draw a planar board
        /// </summary>
        /// <param name="outSize">
        /// size of the output image in pixels.
        /// </param>
        /// <param name="img">
        /// output image with the board. The size of this image will be outSize
        ///         and the board will be on the center, keeping the board proportions.
        /// </param>
        /// <param name="marginSize">
        /// minimum margins (in pixels) of the board in the output image
        /// </param>
        /// <param name="borderBits">
        /// width of the marker borders.
        /// </param>
        /// <remarks>
        ///         This function return the image of the board, ready to be printed.
        /// </remarks>
        public void generateImage(Size outSize, Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            objdetect_Board_generateImage_12(nativeObj, outSize.width, outSize.height, img.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::aruco::Board::Board(vector_Mat objPoints, Dictionary dictionary, Mat ids)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_Board_Board_10(IntPtr objPoints_mat_nativeObj, IntPtr dictionary_nativeObj, IntPtr ids_nativeObj);

        // C++:  Dictionary cv::aruco::Board::getDictionary()
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_Board_getDictionary_10(IntPtr nativeObj);

        // C++:  vector_vector_Point3f cv::aruco::Board::getObjPoints()
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_Board_getObjPoints_10(IntPtr nativeObj);

        // C++:  vector_int cv::aruco::Board::getIds()
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_Board_getIds_10(IntPtr nativeObj);

        // C++:  Point3f cv::aruco::Board::getRightBottomCorner()
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_getRightBottomCorner_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::aruco::Board::matchImagePoints(vector_Mat detectedCorners, Mat detectedIds, Mat& objPoints, Mat& imgPoints)
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_matchImagePoints_10(IntPtr nativeObj, IntPtr detectedCorners_mat_nativeObj, IntPtr detectedIds_nativeObj, IntPtr objPoints_nativeObj, IntPtr imgPoints_nativeObj);

        // C++:  void cv::aruco::Board::generateImage(Size outSize, Mat& img, int marginSize = 0, int borderBits = 1)
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_generateImage_10(IntPtr nativeObj, double outSize_width, double outSize_height, IntPtr img_nativeObj, int marginSize, int borderBits);
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_generateImage_11(IntPtr nativeObj, double outSize_width, double outSize_height, IntPtr img_nativeObj, int marginSize);
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_generateImage_12(IntPtr nativeObj, double outSize_width, double outSize_height, IntPtr img_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void objdetect_Board_delete(IntPtr nativeObj);

    }
}
