using System;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    public class Source2MatHelperUtils
    {
        /// <summary>
        /// Gets the number of channels for the specified color format.
        /// </summary>
        /// <param name="type">The color format</param>
        /// <returns>Number of channels. Returns 1 for GRAY, 3 for RGB/BGR, 4 for RGBA/BGRA. Returns 4 for unknown formats.</returns>
        /// <remarks>
        /// Channel counts for each color format:
        /// - GRAY: 1 channel
        /// - RGB/BGR: 3 channels
        /// - RGBA/BGRA: 4 channels
        /// </remarks>
        static public int Channels(Source2MatHelperColorFormat type)
        {
            switch (type)
            {
                case Source2MatHelperColorFormat.GRAY:
                    return 1;
                case Source2MatHelperColorFormat.RGB:
                case Source2MatHelperColorFormat.BGR:
                    return 3;
                case Source2MatHelperColorFormat.RGBA:
                case Source2MatHelperColorFormat.BGRA:
                    return 4;
                default:
                    return 4;
            }
        }

        /// <summary>
        /// Gets the OpenCV color conversion code needed for converting between two color formats.
        /// </summary>
        /// <param name="srcType">The source color format</param>
        /// <param name="dstType">The destination color format</param>
        /// <returns>OpenCV color conversion code. Returns -1 if conversion is not possible.</returns>
        /// <remarks>
        /// Supported color format conversions:
        /// - GRAY ⇔ RGB/BGR/RGBA/BGRA
        /// - RGB ⇔ GRAY/BGR/RGBA/BGRA
        /// - BGR ⇔ GRAY/RGB/RGBA/BGRA
        /// - RGBA ⇔ GRAY/RGB/BGR/BGRA
        /// - BGRA ⇔ GRAY/RGB/BGR/RGBA
        /// </remarks>
        static public int ColorConversionCodes(Source2MatHelperColorFormat srcType, Source2MatHelperColorFormat dstType)
        {
            if (srcType == Source2MatHelperColorFormat.GRAY)
            {
                if (dstType == Source2MatHelperColorFormat.RGB) return Imgproc.COLOR_GRAY2RGB;
                else if (dstType == Source2MatHelperColorFormat.BGR) return Imgproc.COLOR_GRAY2BGR;
                else if (dstType == Source2MatHelperColorFormat.RGBA) return Imgproc.COLOR_GRAY2RGBA;
                else if (dstType == Source2MatHelperColorFormat.BGRA) return Imgproc.COLOR_GRAY2BGRA;
            }
            else if (srcType == Source2MatHelperColorFormat.RGB)
            {
                if (dstType == Source2MatHelperColorFormat.GRAY) return Imgproc.COLOR_RGB2GRAY;
                else if (dstType == Source2MatHelperColorFormat.BGR) return Imgproc.COLOR_RGB2BGR;
                else if (dstType == Source2MatHelperColorFormat.RGBA) return Imgproc.COLOR_RGB2RGBA;
                else if (dstType == Source2MatHelperColorFormat.BGRA) return Imgproc.COLOR_RGB2BGRA;
            }
            else if (srcType == Source2MatHelperColorFormat.BGR)
            {
                if (dstType == Source2MatHelperColorFormat.GRAY) return Imgproc.COLOR_BGR2GRAY;
                else if (dstType == Source2MatHelperColorFormat.RGB) return Imgproc.COLOR_BGR2RGB;
                else if (dstType == Source2MatHelperColorFormat.RGBA) return Imgproc.COLOR_BGR2RGBA;
                else if (dstType == Source2MatHelperColorFormat.BGRA) return Imgproc.COLOR_BGR2BGRA;
            }
            else if (srcType == Source2MatHelperColorFormat.RGBA)
            {
                if (dstType == Source2MatHelperColorFormat.GRAY) return Imgproc.COLOR_RGBA2GRAY;
                else if (dstType == Source2MatHelperColorFormat.RGB) return Imgproc.COLOR_RGBA2RGB;
                else if (dstType == Source2MatHelperColorFormat.BGR) return Imgproc.COLOR_RGBA2BGR;
                else if (dstType == Source2MatHelperColorFormat.BGRA) return Imgproc.COLOR_RGBA2BGRA;
            }
            else if (srcType == Source2MatHelperColorFormat.BGRA)
            {
                if (dstType == Source2MatHelperColorFormat.GRAY) return Imgproc.COLOR_BGRA2GRAY;
                else if (dstType == Source2MatHelperColorFormat.RGB) return Imgproc.COLOR_BGRA2RGB;
                else if (dstType == Source2MatHelperColorFormat.BGR) return Imgproc.COLOR_BGRA2BGR;
                else if (dstType == Source2MatHelperColorFormat.RGBA) return Imgproc.COLOR_BGRA2RGBA;
            }

            return -1;
        }

        /// <summary>
        /// Apply Mat transformations based on IMatTransformationProvider settings.
        /// </summary>
        /// <param name="inputMat">Input mat to transform.</param>
        /// <param name="rotated90Mat">The rotated Mat output. If null, no rotation will be performed. If not null, must have the same CVType as inputMat and dimensions swapped (width=height, height=width).</param>
        /// <param name="flipVertical">Whether to flip vertically.</param>
        /// <param name="flipHorizontal">Whether to flip horizontally.</param>
        /// <returns>Transformed mat.</returns>
        /// <remarks>
        /// The rotated90Mat parameter must be pre-allocated with the correct dimensions and CVType
        /// to prevent any memory allocation during rotation. If rotated90Mat is null, the method will return inputMat without rotation.
        /// </remarks>
        static public Mat ApplyMatTransformations(Mat inputMat, Mat rotated90Mat, bool flipVertical, bool flipHorizontal)
        {
            if (inputMat == null)
                throw new ArgumentNullException(nameof(inputMat), "Input mat cannot be null.");
            inputMat.ThrowIfDisposed();

            // Apply flip transformations
            if (flipVertical || flipHorizontal)
            {
                int flipCode = -1;
                if (flipVertical && flipHorizontal)
                    flipCode = 0;
                else if (flipVertical)
                    flipCode = 0;
                else if (flipHorizontal)
                    flipCode = 1;
                Core.flip(inputMat, inputMat, flipCode);
            }

            // Apply rotation transformation
            if (rotated90Mat != null)
            {
                rotated90Mat.ThrowIfDisposed();

                // Check if rotated90Mat has the correct size and CVType for 90-degree rotation
                // When rotating 90 degrees clockwise, width and height are swapped
                if (rotated90Mat.width() == inputMat.height() &&
                    rotated90Mat.height() == inputMat.width() &&
                    rotated90Mat.type() == inputMat.type())
                {
                    // No memory allocation occurs here as we're using pre-allocated rotated90Mat
                    Core.rotate(inputMat, rotated90Mat, Core.ROTATE_90_CLOCKWISE);
                    return rotated90Mat;
                }
            }

            return inputMat;
        }
    }
}
