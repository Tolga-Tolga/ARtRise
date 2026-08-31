
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.PlotModule
{
    public partial class Plot2d : Algorithm
    {


        //
        // C++:  void cv::plot::Plot2d::setPlotLineColor(Scalar _plotLineColor)
        //

        public void setPlotLineColor(in Vec4d _plotLineColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotLineColor_10(nativeObj, _plotLineColor.Item1, _plotLineColor.Item2, _plotLineColor.Item3, _plotLineColor.Item4);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotBackgroundColor(Scalar _plotBackgroundColor)
        //

        public void setPlotBackgroundColor(in Vec4d _plotBackgroundColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotBackgroundColor_10(nativeObj, _plotBackgroundColor.Item1, _plotBackgroundColor.Item2, _plotBackgroundColor.Item3, _plotBackgroundColor.Item4);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotAxisColor(Scalar _plotAxisColor)
        //

        public void setPlotAxisColor(in Vec4d _plotAxisColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotAxisColor_10(nativeObj, _plotAxisColor.Item1, _plotAxisColor.Item2, _plotAxisColor.Item3, _plotAxisColor.Item4);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotGridColor(Scalar _plotGridColor)
        //

        public void setPlotGridColor(in Vec4d _plotGridColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotGridColor_10(nativeObj, _plotGridColor.Item1, _plotGridColor.Item2, _plotGridColor.Item3, _plotGridColor.Item4);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotTextColor(Scalar _plotTextColor)
        //

        public void setPlotTextColor(in Vec4d _plotTextColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotTextColor_10(nativeObj, _plotTextColor.Item1, _plotTextColor.Item2, _plotTextColor.Item3, _plotTextColor.Item4);


        }

    }
}
