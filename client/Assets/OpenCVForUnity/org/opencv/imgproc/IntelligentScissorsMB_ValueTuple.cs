

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class IntelligentScissorsMB : DisposableOpenCVObject
    {


        //
        // C++:  void cv::segmentation::IntelligentScissorsMB::buildMap(Point sourcePt)
        //

        /// <summary>
        ///  Prepares a map of optimal paths for the given source point on the image
        /// </summary>
        /// <remarks>
        ///         @note applyImage() / applyImageFeatures() must be called before this call
        /// </remarks>
        /// <param name="sourcePt">
        /// The source point used to find the paths
        /// </param>
        public void buildMap(in (double x, double y) sourcePt)
        {
            ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_buildMap_10(nativeObj, sourcePt.x, sourcePt.y);


        }


        //
        // C++:  void cv::segmentation::IntelligentScissorsMB::getContour(Point targetPt, Mat& contour, bool backward = false)
        //

        /// <summary>
        ///  Extracts optimal contour for the given target point on the image
        /// </summary>
        /// <remarks>
        ///         @note buildMap() must be called before this call
        /// </remarks>
        /// <param name="targetPt">
        /// The target point
        /// </param>
        /// <param name="contour">
        /// The list of pixels which contains optimal path between the source and the target points of the image. Type is CV_32SC2 (compatible with `std::vector<Point>`)
        /// </param>
        /// <param name="backward">
        /// Flag to indicate reverse order of retrieved pixels (use "true" value to fetch points from the target to the source point)
        /// </param>
        public void getContour(in (double x, double y) targetPt, Mat contour, bool backward)
        {
            ThrowIfDisposed();
            if (contour != null) contour.ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_getContour_10(nativeObj, targetPt.x, targetPt.y, contour.nativeObj, backward);


        }

        /// <summary>
        ///  Extracts optimal contour for the given target point on the image
        /// </summary>
        /// <remarks>
        ///         @note buildMap() must be called before this call
        /// </remarks>
        /// <param name="targetPt">
        /// The target point
        /// </param>
        /// <param name="contour">
        /// The list of pixels which contains optimal path between the source and the target points of the image. Type is CV_32SC2 (compatible with `std::vector<Point>`)
        /// </param>
        /// <param name="backward">
        /// Flag to indicate reverse order of retrieved pixels (use "true" value to fetch points from the target to the source point)
        /// </param>
        public void getContour(in (double x, double y) targetPt, Mat contour)
        {
            ThrowIfDisposed();
            if (contour != null) contour.ThrowIfDisposed();

            imgproc_IntelligentScissorsMB_getContour_11(nativeObj, targetPt.x, targetPt.y, contour.nativeObj);


        }

    }
}
