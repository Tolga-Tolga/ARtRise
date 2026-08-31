

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{
    public partial class Tracker : DisposableOpenCVObject
    {

        //
        // C++:  void cv::Tracker::init(Mat image, Rect boundingBox)
        //

        /// <summary>
        ///  Initialize the tracker with a known bounding box that surrounded the target
        /// </summary>
        /// <param name="image">
        /// The initial frame
        /// </param>
        /// <param name="boundingBox">
        /// The initial bounding box
        /// </param>
        public void init(Mat image, in (int x, int y, int width, int height) boundingBox)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();

            video_Tracker_init_10(nativeObj, image.nativeObj, boundingBox.x, boundingBox.y, boundingBox.width, boundingBox.height);


        }


        //
        // C++:  bool cv::Tracker::update(Mat image, Rect& boundingBox)
        //

        /// <summary>
        ///  Update the tracker, find the new most likely bounding box for the target
        /// </summary>
        /// <param name="image">
        /// The current frame
        /// </param>
        /// <param name="boundingBox">
        /// The bounding box that represent the new target location, if true was returned, not
        ///      modified otherwise
        /// </param>
        /// <returns>
        ///  True means that target was located and false means that tracker cannot locate target in
        ///      current frame. Note, that latter *does not* imply that tracker has failed, maybe target is indeed
        ///      missing from the frame (say, out of sight)
        /// </returns>
        public bool update(Mat image, out (int x, int y, int width, int height) boundingBox)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            double[] boundingBox_out = new double[4];
            bool retVal = video_Tracker_update_10(nativeObj, image.nativeObj, boundingBox_out);
            { boundingBox.x = (int)boundingBox_out[0]; boundingBox.y = (int)boundingBox_out[1]; boundingBox.width = (int)boundingBox_out[2]; boundingBox.height = (int)boundingBox_out[3]; }
            return retVal;
        }

    }
}
