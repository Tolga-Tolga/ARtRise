#if !UNITY_WEBGL


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Structured_lightModule
{
    public partial class GrayCodePattern : StructuredLightPattern
    {


        //
        // C++:  bool cv::structured_light::GrayCodePattern::getProjPixel(vector_Mat patternImages, int x, int y, Point& projPix)
        //

        /// <summary>
        ///  For a (x,y) pixel of a camera returns the corresponding projector pixel.
        /// </summary>
        /// <remarks>
        ///        The function decodes each pixel in the pattern images acquired by a camera into their corresponding decimal numbers representing the projector's column and row,
        ///        providing a mapping between camera's and projector's pixel.
        /// </remarks>
        /// <param name="patternImages">
        /// The pattern images acquired by the camera, stored in a grayscale vector &lt; Mat &gt;.
        /// </param>
        /// <param name="x">
        /// x coordinate of the image pixel.
        /// </param>
        /// <param name="y">
        /// y coordinate of the image pixel.
        /// </param>
        /// <param name="projPix">
        /// Projector's pixel corresponding to the camera's pixel: projPix.x and projPix.y are the image coordinates of the projector's pixel corresponding to the pixel being decoded in a camera.
        /// </param>
        public bool getProjPixel(List<Mat> patternImages, int x, int y, out (double x, double y) projPix)
        {
            ThrowIfDisposed();
            using Mat patternImages_mat = Converters.vector_Mat_to_Mat(patternImages);
            double[] projPix_out = new double[2];
            bool retVal = structured_1light_GrayCodePattern_getProjPixel_10(nativeObj, patternImages_mat.nativeObj, x, y, projPix_out);
            { projPix.x = projPix_out[0]; projPix.y = projPix_out[1]; }
            return retVal;
        }

    }
}


#endif