
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class GridBoard : Board
    {

        //
        // C++:   cv::aruco::GridBoard::GridBoard(Size size, float markerLength, float markerSeparation, Dictionary dictionary, Mat ids = Mat())
        //

        /// <summary>
        ///  GridBoard constructor
        /// </summary>
        /// <param name="size">
        /// number of markers in x and y directions
        /// </param>
        /// <param name="markerLength">
        /// marker side length (normally in meters)
        /// </param>
        /// <param name="markerSeparation">
        /// separation between two markers (same unit as markerLength)
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers
        /// </param>
        /// <param name="ids">
        /// set of marker ids in dictionary to use on board.
        /// </param>
        public GridBoard(in Vec2d size, float markerLength, float markerSeparation, Dictionary dictionary, Mat ids) :
                    base(DisposableObject.ThrowIfNullIntPtr(objdetect_GridBoard_GridBoard_10(size.Item1, size.Item2, markerLength, markerSeparation, dictionary.getNativeObjAddr(), ids.nativeObj)))
        {



        }

        /// <summary>
        ///  GridBoard constructor
        /// </summary>
        /// <param name="size">
        /// number of markers in x and y directions
        /// </param>
        /// <param name="markerLength">
        /// marker side length (normally in meters)
        /// </param>
        /// <param name="markerSeparation">
        /// separation between two markers (same unit as markerLength)
        /// </param>
        /// <param name="dictionary">
        /// dictionary of markers indicating the type of markers
        /// </param>
        /// <param name="ids">
        /// set of marker ids in dictionary to use on board.
        /// </param>
        public GridBoard(in Vec2d size, float markerLength, float markerSeparation, Dictionary dictionary) :
                    base(DisposableObject.ThrowIfNullIntPtr(objdetect_GridBoard_GridBoard_11(size.Item1, size.Item2, markerLength, markerSeparation, dictionary.getNativeObjAddr())))
        {



        }


        //
        // C++:  Size cv::aruco::GridBoard::getGridSize()
        //

        public Vec2d getGridSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_GridBoard_getGridSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }

    }
}
