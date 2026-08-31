
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.XimgprocModule
{
    public partial class DisparityFilter : Algorithm
    {

        //
        // C++:  void cv::ximgproc::DisparityFilter::filter(Mat disparity_map_left, Mat left_view, Mat& filtered_disparity_map, Mat disparity_map_right = Mat(), Rect ROI = Rect(), Mat right_view = Mat())
        //

        /// <summary>
        ///  Apply filtering to the disparity map.
        /// </summary>
        /// <param name="disparity_map_left">
        /// disparity map of the left view, 1 channel, CV_16S type. Implicitly assumes that disparity
        ///      values are scaled by 16 (one-pixel disparity corresponds to the value of 16 in the disparity map). Disparity map
        ///      can have any resolution, it will be automatically resized to fit left_view resolution.
        /// </param>
        /// <param name="left_view">
        /// left view of the original stereo-pair to guide the filtering process, 8-bit single-channel
        ///      or three-channel image.
        /// </param>
        /// <param name="filtered_disparity_map">
        /// output disparity map.
        /// </param>
        /// <param name="disparity_map_right">
        /// optional argument, some implementations might also use the disparity map
        ///      of the right view to compute confidence maps, for instance.
        /// </param>
        /// <param name="ROI">
        /// region of the disparity map to filter. Optional, usually it should be set automatically.
        /// </param>
        /// <param name="right_view">
        /// optional argument, some implementations might also use the right view of the original
        ///      stereo-pair.
        /// </param>
        public void filter(Mat disparity_map_left, Mat left_view, Mat filtered_disparity_map, Mat disparity_map_right, in (int x, int y, int width, int height) ROI, Mat right_view)
        {
            ThrowIfDisposed();
            if (disparity_map_left != null) disparity_map_left.ThrowIfDisposed();
            if (left_view != null) left_view.ThrowIfDisposed();
            if (filtered_disparity_map != null) filtered_disparity_map.ThrowIfDisposed();
            if (disparity_map_right != null) disparity_map_right.ThrowIfDisposed();
            if (right_view != null) right_view.ThrowIfDisposed();

            ximgproc_DisparityFilter_filter_10(nativeObj, disparity_map_left.nativeObj, left_view.nativeObj, filtered_disparity_map.nativeObj, disparity_map_right.nativeObj, ROI.x, ROI.y, ROI.width, ROI.height, right_view.nativeObj);


        }

        /// <summary>
        ///  Apply filtering to the disparity map.
        /// </summary>
        /// <param name="disparity_map_left">
        /// disparity map of the left view, 1 channel, CV_16S type. Implicitly assumes that disparity
        ///      values are scaled by 16 (one-pixel disparity corresponds to the value of 16 in the disparity map). Disparity map
        ///      can have any resolution, it will be automatically resized to fit left_view resolution.
        /// </param>
        /// <param name="left_view">
        /// left view of the original stereo-pair to guide the filtering process, 8-bit single-channel
        ///      or three-channel image.
        /// </param>
        /// <param name="filtered_disparity_map">
        /// output disparity map.
        /// </param>
        /// <param name="disparity_map_right">
        /// optional argument, some implementations might also use the disparity map
        ///      of the right view to compute confidence maps, for instance.
        /// </param>
        /// <param name="ROI">
        /// region of the disparity map to filter. Optional, usually it should be set automatically.
        /// </param>
        /// <param name="right_view">
        /// optional argument, some implementations might also use the right view of the original
        ///      stereo-pair.
        /// </param>
        public void filter(Mat disparity_map_left, Mat left_view, Mat filtered_disparity_map, Mat disparity_map_right, in (int x, int y, int width, int height) ROI)
        {
            ThrowIfDisposed();
            if (disparity_map_left != null) disparity_map_left.ThrowIfDisposed();
            if (left_view != null) left_view.ThrowIfDisposed();
            if (filtered_disparity_map != null) filtered_disparity_map.ThrowIfDisposed();
            if (disparity_map_right != null) disparity_map_right.ThrowIfDisposed();

            ximgproc_DisparityFilter_filter_11(nativeObj, disparity_map_left.nativeObj, left_view.nativeObj, filtered_disparity_map.nativeObj, disparity_map_right.nativeObj, ROI.x, ROI.y, ROI.width, ROI.height);


        }

    }
}
