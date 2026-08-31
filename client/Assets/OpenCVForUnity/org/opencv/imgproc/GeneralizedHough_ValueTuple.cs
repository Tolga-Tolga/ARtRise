
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class GeneralizedHough : Algorithm
    {

        //
        // C++:  void cv::GeneralizedHough::setTemplate(Mat templ, Point templCenter = Point(-1, -1))
        //

        public void setTemplate(Mat templ, in (double x, double y) templCenter)
        {
            ThrowIfDisposed();
            if (templ != null) templ.ThrowIfDisposed();

            imgproc_GeneralizedHough_setTemplate_10(nativeObj, templ.nativeObj, templCenter.x, templCenter.y);


        }


        //
        // C++:  void cv::GeneralizedHough::setTemplate(Mat edges, Mat dx, Mat dy, Point templCenter = Point(-1, -1))
        //

        public void setTemplate(Mat edges, Mat dx, Mat dy, in (double x, double y) templCenter)
        {
            ThrowIfDisposed();
            if (edges != null) edges.ThrowIfDisposed();
            if (dx != null) dx.ThrowIfDisposed();
            if (dy != null) dy.ThrowIfDisposed();

            imgproc_GeneralizedHough_setTemplate_12(nativeObj, edges.nativeObj, dx.nativeObj, dy.nativeObj, templCenter.x, templCenter.y);


        }

    }
}
