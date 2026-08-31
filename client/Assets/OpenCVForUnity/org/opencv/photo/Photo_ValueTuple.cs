
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.PhotoModule
{
    public partial class Photo
    {


        //
        // C++:  void cv::seamlessClone(Mat src, Mat dst, Mat mask, Point p, Mat& blend, int flags)
        //

        /// <summary>
        ///  Performs seamless cloning to blend a region from a source image into a destination image.
        ///  This function is designed for local image editing, allowing changes restricted to a region
        ///  (manually selected as the ROI) to be applied effortlessly and seamlessly. These changes can
        ///  range from slight distortions to complete replacement by novel content @cite PM03.
        /// </summary>
        /// <param name="src">
        /// The source image (8-bit 3-channel), from which a region will be blended into the destination.
        /// </param>
        /// <param name="dst">
        /// The destination image (8-bit 3-channel), where the src image will be blended.
        /// </param>
        /// <param name="mask">
        /// A binary mask (8-bit, 1, 3, or 4-channel) specifying the region in the source image to blend.
        ///  Non-zero pixels indicate the region to be blended. If an empty Mat is provided, a mask with
        ///  all non-zero pixels is created internally.
        /// </param>
        /// <param name="p">
        /// The point where the center of the src image is placed in the dst image.
        /// </param>
        /// <param name="blend">
        /// The output image that stores the result of the seamless cloning. It has the same size and type as `dst`.
        /// </param>
        /// <param name="flags">
        /// Flags that control the type of cloning method, can take values of `cv::SeamlessCloneFlags`.
        /// </param>
        public static void seamlessClone(Mat src, Mat dst, Mat mask, in (double x, double y) p, Mat blend, int flags)
        {
            if (src != null) src.ThrowIfDisposed();
            if (dst != null) dst.ThrowIfDisposed();
            if (mask != null) mask.ThrowIfDisposed();
            if (blend != null) blend.ThrowIfDisposed();

            photo_Photo_seamlessClone_10(src.nativeObj, dst.nativeObj, mask.nativeObj, p.x, p.y, blend.nativeObj, flags);


        }

    }
}
