
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class CharucoBoard : Board
    {

        //
        // C++:   cv::aruco::CharucoBoard::CharucoBoard(Size size, float squareLength, float markerLength, Dictionary dictionary, Mat ids = Mat())
        //

        /// <summary>
        ///  CharucoBoard constructor
        /// </summary>
        /// <param name="size">
        /// number of chessboard squares in x and y directions
        /// </param>
        /// <param name="squareLength">
        /// squareLength chessboard square side length (normally in meters)
        /// </param>
        /// <param name="markerLength">
        /// marker side length (same unit than squareLength)
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers
        /// </param>
        /// <param name="ids">
        /// array of id used markers
        ///         The first markers in the dictionary are used to fill the white chessboard squares.
        /// </param>
        public CharucoBoard(in (double width, double height) size, float squareLength, float markerLength, Dictionary dictionary, Mat ids) :
                    base(DisposableObject.ThrowIfNullIntPtr(objdetect_CharucoBoard_CharucoBoard_10(size.width, size.height, squareLength, markerLength, dictionary.getNativeObjAddr(), ids.nativeObj)))
        {



        }

        /// <summary>
        ///  CharucoBoard constructor
        /// </summary>
        /// <param name="size">
        /// number of chessboard squares in x and y directions
        /// </param>
        /// <param name="squareLength">
        /// squareLength chessboard square side length (normally in meters)
        /// </param>
        /// <param name="markerLength">
        /// marker side length (same unit than squareLength)
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers
        /// </param>
        /// <param name="ids">
        /// array of id used markers
        ///         The first markers in the dictionary are used to fill the white chessboard squares.
        /// </param>
        public CharucoBoard(in (double width, double height) size, float squareLength, float markerLength, Dictionary dictionary) :
                    base(DisposableObject.ThrowIfNullIntPtr(objdetect_CharucoBoard_CharucoBoard_11(size.width, size.height, squareLength, markerLength, dictionary.getNativeObjAddr())))
        {



        }


        //
        // C++:  Size cv::aruco::CharucoBoard::getChessboardSize()
        //

        public (double width, double height) getChessboardSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_CharucoBoard_getChessboardSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }

    }
}
