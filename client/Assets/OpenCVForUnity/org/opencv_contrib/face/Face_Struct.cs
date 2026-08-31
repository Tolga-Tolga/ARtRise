
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.FaceModule
{
    public partial class Face
    {


        //
        // C++:  void cv::face::drawFacemarks(Mat& image, Mat points, Scalar color = Scalar(255,0,0))
        //

        /// <summary>
        ///  Utility to draw the detected facial landmark points
        /// </summary>
        /// <param name="image">
        /// The input image to be processed.
        /// </param>
        /// <param name="points">
        /// Contains the data of points which will be drawn.
        /// </param>
        /// <param name="color">
        /// The color of points in BGR format represented by cv::Scalar.
        /// </param>
        /// <remarks>
        ///  <B>Example of usage</B>
        /// </remarks>
        /// <code language="c++">
        ///  std::vector<Rect> faces;
        ///  std::vector<std::vector<Point2f> > landmarks;
        ///  facemark->getFaces(img, faces);
        ///  facemark->fit(img, faces, landmarks);
        ///  for(int j=0;j<rects.size();j++){
        ///      face::drawFacemarks(frame, landmarks[j], Scalar(0,0,255));
        ///  }
        /// </code>
        public static void drawFacemarks(Mat image, Mat points, in Vec4d color)
        {
            if (image != null) image.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            face_Face_drawFacemarks_10(image.nativeObj, points.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4);


        }

    }
}
