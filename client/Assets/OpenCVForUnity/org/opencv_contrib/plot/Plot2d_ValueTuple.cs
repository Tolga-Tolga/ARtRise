
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.PlotModule
{
    public partial class Plot2d : Algorithm
    {


        //
        // C++:  void cv::plot::Plot2d::setPlotLineColor(Scalar _plotLineColor)
        //

        public void setPlotLineColor(in (double v0, double v1, double v2, double v3) _plotLineColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotLineColor_10(nativeObj, _plotLineColor.v0, _plotLineColor.v1, _plotLineColor.v2, _plotLineColor.v3);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotBackgroundColor(Scalar _plotBackgroundColor)
        //

        public void setPlotBackgroundColor(in (double v0, double v1, double v2, double v3) _plotBackgroundColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotBackgroundColor_10(nativeObj, _plotBackgroundColor.v0, _plotBackgroundColor.v1, _plotBackgroundColor.v2, _plotBackgroundColor.v3);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotAxisColor(Scalar _plotAxisColor)
        //

        public void setPlotAxisColor(in (double v0, double v1, double v2, double v3) _plotAxisColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotAxisColor_10(nativeObj, _plotAxisColor.v0, _plotAxisColor.v1, _plotAxisColor.v2, _plotAxisColor.v3);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotGridColor(Scalar _plotGridColor)
        //

        public void setPlotGridColor(in (double v0, double v1, double v2, double v3) _plotGridColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotGridColor_10(nativeObj, _plotGridColor.v0, _plotGridColor.v1, _plotGridColor.v2, _plotGridColor.v3);


        }


        //
        // C++:  void cv::plot::Plot2d::setPlotTextColor(Scalar _plotTextColor)
        //

        public void setPlotTextColor(in (double v0, double v1, double v2, double v3) _plotTextColor)
        {
            ThrowIfDisposed();

            plot_Plot2d_setPlotTextColor_10(nativeObj, _plotTextColor.v0, _plotTextColor.v1, _plotTextColor.v2, _plotTextColor.v3);


        }

    }
}
