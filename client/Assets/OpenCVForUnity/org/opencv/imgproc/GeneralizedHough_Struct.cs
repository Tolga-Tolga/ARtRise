
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class GeneralizedHough : Algorithm
    {

        //
        // C++:  void cv::GeneralizedHough::setTemplate(Mat templ, Point templCenter = Point(-1, -1))
        //

        public void setTemplate(Mat templ, in Vec2d templCenter)
        {
            ThrowIfDisposed();
            if (templ != null) templ.ThrowIfDisposed();

            imgproc_GeneralizedHough_setTemplate_10(nativeObj, templ.nativeObj, templCenter.Item1, templCenter.Item2);


        }


        //
        // C++:  void cv::GeneralizedHough::setTemplate(Mat edges, Mat dx, Mat dy, Point templCenter = Point(-1, -1))
        //

        public void setTemplate(Mat edges, Mat dx, Mat dy, in Vec2d templCenter)
        {
            ThrowIfDisposed();
            if (edges != null) edges.ThrowIfDisposed();
            if (dx != null) dx.ThrowIfDisposed();
            if (dy != null) dy.ThrowIfDisposed();

            imgproc_GeneralizedHough_setTemplate_12(nativeObj, edges.nativeObj, dx.nativeObj, dy.nativeObj, templCenter.Item1, templCenter.Item2);


        }

    }
}
