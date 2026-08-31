

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using Range = OpenCVForUnity.CoreModule.Range;

namespace OpenCVForUnity.ImgcodecsModule
{
    // C++: class Animation
    /// <summary>
    ///  Represents an animation with multiple frames.
    ///  The `Animation` struct is designed to store and manage data for animated sequences such as those from animated formats (e.g., GIF, AVIF, APNG, WebP).
    ///  It provides support for looping, background color settings, frame timing, and frame storage.
    /// </summary>
    public partial class Animation : DisposableOpenCVObject
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        imgcodecs_Animation_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Animation(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static Animation __fromPtr__(IntPtr addr) { return new Animation(addr); }

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
        public Animation(int loopCount, Scalar bgColor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_Animation_10(loopCount, bgColor.val[0], bgColor.val[1], bgColor.val[2], bgColor.val[3]));


        }

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
        public Animation(int loopCount)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_Animation_11(loopCount));


        }

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
        public Animation()
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_Animation_12());


        }


        //
        // C++: int Animation::loop_count
        //

        public int get_loop_count()
        {
            ThrowIfDisposed();

            return imgcodecs_Animation_get_1loop_1count_10(nativeObj);


        }


        //
        // C++: void Animation::loop_count
        //

        public void set_loop_count(int loop_count)
        {
            ThrowIfDisposed();

            imgcodecs_Animation_set_1loop_1count_10(nativeObj, loop_count);


        }


        //
        // C++: Scalar Animation::bgcolor
        //

        public Scalar get_bgcolor()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            imgcodecs_Animation_get_1bgcolor_10(nativeObj, tmpArray);
            Scalar retVal = new Scalar(tmpArray);

            return retVal;
        }


        //
        // C++: void Animation::bgcolor
        //

        public void set_bgcolor(Scalar bgcolor)
        {
            ThrowIfDisposed();

            imgcodecs_Animation_set_1bgcolor_10(nativeObj, bgcolor.val[0], bgcolor.val[1], bgcolor.val[2], bgcolor.val[3]);


        }


        //
        // C++: vector_int Animation::durations
        //

        public MatOfInt get_durations()
        {
            ThrowIfDisposed();

            return MatOfInt.fromNativeAddr(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_get_1durations_10(nativeObj)));


        }


        //
        // C++: void Animation::durations
        //

        public void set_durations(MatOfInt durations)
        {
            ThrowIfDisposed();
            if (durations != null) durations.ThrowIfDisposed();
            Mat durations_mat = durations;
            imgcodecs_Animation_set_1durations_10(nativeObj, durations_mat.nativeObj);


        }


        //
        // C++: vector_Mat Animation::frames
        //

        public List<Mat> get_frames()
        {
            ThrowIfDisposed();
            List<Mat> retVal = new List<Mat>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_get_1frames_10(nativeObj)));
            Converters.Mat_to_vector_Mat(retValMat, retVal);
            return retVal;
        }


        //
        // C++: void Animation::frames
        //

        public void set_frames(List<Mat> frames)
        {
            ThrowIfDisposed();
            using Mat frames_mat = Converters.vector_Mat_to_Mat(frames);
            imgcodecs_Animation_set_1frames_10(nativeObj, frames_mat.nativeObj);


        }


        //
        // C++: Mat Animation::still_image
        //

        public Mat get_still_image()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Animation_get_1still_1image_10(nativeObj)));


        }


        //
        // C++: void Animation::still_image
        //

        public void set_still_image(Mat still_image)
        {
            ThrowIfDisposed();
            if (still_image != null) still_image.ThrowIfDisposed();

            imgcodecs_Animation_set_1still_1image_10(nativeObj, still_image.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::Animation::Animation(int loopCount = 0, Scalar bgColor = Scalar())
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_Animation_10(int loopCount, double bgColor_val0, double bgColor_val1, double bgColor_val2, double bgColor_val3);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_Animation_11(int loopCount);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_Animation_12();

        // C++: int Animation::loop_count
        [DllImport(LIBNAME)]
        private static extern int imgcodecs_Animation_get_1loop_1count_10(IntPtr nativeObj);

        // C++: void Animation::loop_count
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_set_1loop_1count_10(IntPtr nativeObj, int loop_count);

        // C++: Scalar Animation::bgcolor
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_get_1bgcolor_10(IntPtr nativeObj, double[] retVal);

        // C++: void Animation::bgcolor
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_set_1bgcolor_10(IntPtr nativeObj, double bgcolor_val0, double bgcolor_val1, double bgcolor_val2, double bgcolor_val3);

        // C++: vector_int Animation::durations
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_get_1durations_10(IntPtr nativeObj);

        // C++: void Animation::durations
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_set_1durations_10(IntPtr nativeObj, IntPtr durations_mat_nativeObj);

        // C++: vector_Mat Animation::frames
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_get_1frames_10(IntPtr nativeObj);

        // C++: void Animation::frames
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_set_1frames_10(IntPtr nativeObj, IntPtr frames_mat_nativeObj);

        // C++: Mat Animation::still_image
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Animation_get_1still_1image_10(IntPtr nativeObj);

        // C++: void Animation::still_image
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_set_1still_1image_10(IntPtr nativeObj, IntPtr still_image_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Animation_delete(IntPtr nativeObj);

    }
}
