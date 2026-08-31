

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using Range = OpenCVForUnity.CoreModule.Range;

namespace OpenCVForUnity.ImgcodecsModule
{
    public partial class Animation : DisposableOpenCVObject
    {

        //
        // C++:   cv::Animation::Animation(int loopCount = 0, Scalar bgColor = Scalar())
        //

        /// <summary>
        ///  Constructs an Animation object with optional loop count and background color.
        /// </summary>
        /// <param name="loopCount">
        /// An integer representing the number of times the animation should loop:
        ///      - `0` (default) indicates infinite looping, meaning the animation will replay continuously.
        ///      - Positive values denote finite repeat counts, allowing the animation to play a limited number of times.
        ///      - If a negative value or a value beyond the maximum of `0xffff` (65535) is provided, it is reset to `0`
        ///      (infinite looping) to maintain valid bounds.
        /// </param>
        /// <param name="bgColor">
        /// A `Scalar` object representing the background color in BGR format:
        ///      - Defaults to `Scalar()`, indicating an empty color (usually transparent if supported).
        ///      - This background color provides a solid fill behind frames that have transparency, ensuring a consistent display appearance.
        /// </param>
        public Animation(int loopCount, in (double v0, double v1, double v2, double v3) bgColor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_Animation_10(loopCount, bgColor.v0, bgColor.v1, bgColor.v2, bgColor.v3));


        }


        //
        // C++: Scalar Animation::bgcolor
        //

        public (double v0, double v1, double v2, double v3) get_bgcolorAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            imgcodecs_Animation_get_1bgcolor_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Animation::bgcolor
        //

        public void set_bgcolor(in (double v0, double v1, double v2, double v3) bgcolor)
        {
            ThrowIfDisposed();

            imgcodecs_Animation_set_1bgcolor_10(nativeObj, bgcolor.v0, bgcolor.v1, bgcolor.v2, bgcolor.v3);


        }

    }
}
