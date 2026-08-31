
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.BioinspiredModule
{
    public partial class Retina : Algorithm
    {

        //
        // C++:  Size cv::bioinspired::Retina::getInputSize()
        //

        /// <summary>
        ///  Retreive retina input buffer size
        /// </summary>
        /// <returns>
        ///  the retina input buffer size
        /// </returns>
        public Vec2d getInputSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            bioinspired_Retina_getInputSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++:  Size cv::bioinspired::Retina::getOutputSize()
        //

        /// <summary>
        ///  Retreive retina output buffer size that can be different from the input if a spatial log
        ///      transformation is applied
        /// </summary>
        /// <returns>
        ///  the retina output buffer size
        /// </returns>
        public Vec2d getOutputSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            bioinspired_Retina_getOutputSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static Retina create(in Vec2d inputSize)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_10(inputSize.Item1, inputSize.Item2)));


        }


        //
        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize, bool colorMode, int colorSamplingMethod = RETINA_COLOR_BAYER, bool useRetinaLogSampling = false, float reductionFactor = 1.0f, float samplingStrength = 10.0f)
        //

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(in Vec2d inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling, float reductionFactor, float samplingStrength)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_11(inputSize.Item1, inputSize.Item2, colorMode, colorSamplingMethod, useRetinaLogSampling, reductionFactor, samplingStrength)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(in Vec2d inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling, float reductionFactor)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_12(inputSize.Item1, inputSize.Item2, colorMode, colorSamplingMethod, useRetinaLogSampling, reductionFactor)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(in Vec2d inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_13(inputSize.Item1, inputSize.Item2, colorMode, colorSamplingMethod, useRetinaLogSampling)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(in Vec2d inputSize, bool colorMode, int colorSamplingMethod)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_14(inputSize.Item1, inputSize.Item2, colorMode, colorSamplingMethod)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(in Vec2d inputSize, bool colorMode)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_15(inputSize.Item1, inputSize.Item2, colorMode)));


        }

    }
}
