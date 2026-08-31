
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using Range = OpenCVForUnity.CoreModule.Range;

namespace OpenCVForUnity.ImgcodecsModule
{
    // C++: class Imgcodecs


    public class Imgcodecs
    {

        /// <summary>
        /// C++: enum ImageMetadataType (cv.ImageMetadataType)
        /// </summary>
        public const int IMAGE_METADATA_UNKNOWN = -1;

        /// <summary>
        /// C++: enum ImageMetadataType (cv.ImageMetadataType)
        /// </summary>
        public const int IMAGE_METADATA_EXIF = 0;

        /// <summary>
        /// C++: enum ImageMetadataType (cv.ImageMetadataType)
        /// </summary>
        public const int IMAGE_METADATA_XMP = 1;

        /// <summary>
        /// C++: enum ImageMetadataType (cv.ImageMetadataType)
        /// </summary>
        public const int IMAGE_METADATA_ICCP = 2;

        /// <summary>
        /// C++: enum ImageMetadataType (cv.ImageMetadataType)
        /// </summary>
        public const int IMAGE_METADATA_MAX = 2;


        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_UNCHANGED = -1;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_GRAYSCALE = 0;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_COLOR_BGR = 1;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_COLOR = 1;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_ANYDEPTH = 2;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_ANYCOLOR = 4;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_LOAD_GDAL = 8;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_GRAYSCALE_2 = 16;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_COLOR_2 = 17;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_GRAYSCALE_4 = 32;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_COLOR_4 = 33;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_GRAYSCALE_8 = 64;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_REDUCED_COLOR_8 = 65;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_IGNORE_ORIENTATION = 128;

        /// <summary>
        /// C++: enum ImreadModes (cv.ImreadModes)
        /// </summary>
        public const int IMREAD_COLOR_RGB = 256;


        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_NO = 0;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_RLE = 1;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_ZIPS = 2;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_ZIP = 3;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_PIZ = 4;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_PXR24 = 5;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_B44 = 6;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_B44A = 7;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_DWAA = 8;

        /// <summary>
        /// C++: enum ImwriteEXRCompressionFlags (cv.ImwriteEXRCompressionFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION_DWAB = 9;


        /// <summary>
        /// C++: enum ImwriteEXRTypeFlags (cv.ImwriteEXRTypeFlags)
        /// </summary>
        public const int IMWRITE_EXR_TYPE_HALF = 1;

        /// <summary>
        /// C++: enum ImwriteEXRTypeFlags (cv.ImwriteEXRTypeFlags)
        /// </summary>
        public const int IMWRITE_EXR_TYPE_FLOAT = 2;


        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_QUALITY = 1;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_PROGRESSIVE = 2;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_OPTIMIZE = 3;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_RST_INTERVAL = 4;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_LUMA_QUALITY = 5;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_CHROMA_QUALITY = 6;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR = 7;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PNG_COMPRESSION = 16;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY = 17;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PNG_BILEVEL = 18;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER = 19;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PXM_BINARY = 32;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_EXR_TYPE = (3 << 4) + 0;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_EXR_COMPRESSION = (3 << 4) + 1;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_EXR_DWA_COMPRESSION_LEVEL = (3 << 4) + 2;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_WEBP_QUALITY = 64;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_HDR_COMPRESSION = (5 << 4) + 0;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_PAM_TUPLETYPE = 128;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_RESUNIT = 256;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_XDPI = 257;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_YDPI = 258;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION = 259;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_ROWSPERSTRIP = 278;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_TIFF_PREDICTOR = 317;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEG2000_COMPRESSION_X1000 = 272;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_AVIF_QUALITY = 512;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_AVIF_DEPTH = 513;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_AVIF_SPEED = 514;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEGXL_QUALITY = 640;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEGXL_EFFORT = 641;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEGXL_DISTANCE = 642;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_JPEGXL_DECODING_SPEED = 643;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_LOOP = 1024;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_SPEED = 1025;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_QUALITY = 1026;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_DITHER = 1027;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_TRANSPARENCY = 1028;

        /// <summary>
        /// C++: enum ImwriteFlags (cv.ImwriteFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE = 1029;


        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_FAST_NO_DITHER = 1;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_FAST_FLOYD_DITHER = 2;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_8 = 3;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_16 = 4;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_32 = 5;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_64 = 6;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_128 = 7;

        /// <summary>
        /// C++: enum ImwriteGIFCompressionFlags (cv.ImwriteGIFCompressionFlags)
        /// </summary>
        public const int IMWRITE_GIF_COLORTABLE_SIZE_256 = 8;


        /// <summary>
        /// C++: enum ImwriteHDRCompressionFlags (cv.ImwriteHDRCompressionFlags)
        /// </summary>
        public const int IMWRITE_HDR_COMPRESSION_NONE = 0;

        /// <summary>
        /// C++: enum ImwriteHDRCompressionFlags (cv.ImwriteHDRCompressionFlags)
        /// </summary>
        public const int IMWRITE_HDR_COMPRESSION_RLE = 1;


        /// <summary>
        /// C++: enum ImwriteJPEGSamplingFactorParams (cv.ImwriteJPEGSamplingFactorParams)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR_411 = 0x411111;

        /// <summary>
        /// C++: enum ImwriteJPEGSamplingFactorParams (cv.ImwriteJPEGSamplingFactorParams)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR_420 = 0x221111;

        /// <summary>
        /// C++: enum ImwriteJPEGSamplingFactorParams (cv.ImwriteJPEGSamplingFactorParams)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR_422 = 0x211111;

        /// <summary>
        /// C++: enum ImwriteJPEGSamplingFactorParams (cv.ImwriteJPEGSamplingFactorParams)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR_440 = 0x121111;

        /// <summary>
        /// C++: enum ImwriteJPEGSamplingFactorParams (cv.ImwriteJPEGSamplingFactorParams)
        /// </summary>
        public const int IMWRITE_JPEG_SAMPLING_FACTOR_444 = 0x111111;


        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_NULL = 0;

        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_BLACKANDWHITE = 1;

        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_GRAYSCALE = 2;

        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_GRAYSCALE_ALPHA = 3;

        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_RGB = 4;

        /// <summary>
        /// C++: enum ImwritePAMFlags (cv.ImwritePAMFlags)
        /// </summary>
        public const int IMWRITE_PAM_FORMAT_RGB_ALPHA = 5;


        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER_NONE = 8;

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER_SUB = 16;

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER_UP = 32;

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER_AVG = 64;

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FILTER_PAETH = 128;

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_FAST_FILTERS = (IMWRITE_PNG_FILTER_NONE | IMWRITE_PNG_FILTER_SUB | IMWRITE_PNG_FILTER_UP);

        /// <summary>
        /// C++: enum ImwritePNGFilterFlags (cv.ImwritePNGFilterFlags)
        /// </summary>
        public const int IMWRITE_PNG_ALL_FILTERS = (IMWRITE_PNG_FAST_FILTERS | IMWRITE_PNG_FILTER_AVG | IMWRITE_PNG_FILTER_PAETH);


        /// <summary>
        /// C++: enum ImwritePNGFlags (cv.ImwritePNGFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY_DEFAULT = 0;

        /// <summary>
        /// C++: enum ImwritePNGFlags (cv.ImwritePNGFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY_FILTERED = 1;

        /// <summary>
        /// C++: enum ImwritePNGFlags (cv.ImwritePNGFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY_HUFFMAN_ONLY = 2;

        /// <summary>
        /// C++: enum ImwritePNGFlags (cv.ImwritePNGFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY_RLE = 3;

        /// <summary>
        /// C++: enum ImwritePNGFlags (cv.ImwritePNGFlags)
        /// </summary>
        public const int IMWRITE_PNG_STRATEGY_FIXED = 4;


        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_NONE = 1;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITTRLE = 2;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITTFAX3 = 3;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITT_T4 = 3;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITTFAX4 = 4;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITT_T6 = 4;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_LZW = 5;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_OJPEG = 6;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_JPEG = 7;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_T85 = 9;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_T43 = 10;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_NEXT = 32766;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_CCITTRLEW = 32771;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_PACKBITS = 32773;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_THUNDERSCAN = 32809;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_IT8CTPAD = 32895;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_IT8LW = 32896;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_IT8MP = 32897;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_IT8BL = 32898;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_PIXARFILM = 32908;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_PIXARLOG = 32909;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_DEFLATE = 32946;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_ADOBE_DEFLATE = 8;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_DCS = 32947;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_JBIG = 34661;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_SGILOG = 34676;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_SGILOG24 = 34677;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_JP2000 = 34712;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_LERC = 34887;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_LZMA = 34925;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_ZSTD = 50000;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_WEBP = 50001;

        /// <summary>
        /// C++: enum ImwriteTiffCompressionFlags (cv.ImwriteTiffCompressionFlags)
        /// </summary>
        public const int IMWRITE_TIFF_COMPRESSION_JXL = 50002;


        /// <summary>
        /// C++: enum ImwriteTiffPredictorFlags (cv.ImwriteTiffPredictorFlags)
        /// </summary>
        public const int IMWRITE_TIFF_PREDICTOR_NONE = 1;

        /// <summary>
        /// C++: enum ImwriteTiffPredictorFlags (cv.ImwriteTiffPredictorFlags)
        /// </summary>
        public const int IMWRITE_TIFF_PREDICTOR_HORIZONTAL = 2;

        /// <summary>
        /// C++: enum ImwriteTiffPredictorFlags (cv.ImwriteTiffPredictorFlags)
        /// </summary>
        public const int IMWRITE_TIFF_PREDICTOR_FLOATINGPOINT = 3;


        //
        // C++:  Mat cv::imread(String filename, int flags = IMREAD_COLOR_BGR)
        //

        /// <summary>
        ///  Loads an image from a file.
        /// </summary>
        /// <remarks>
        ///  @anchor imread
        ///  
        ///  The `imread` function loads an image from the specified file and returns OpenCV matrix. If the image cannot be
        ///  read (because of a missing file, improper permissions, or unsupported/invalid format), the function
        ///  returns an empty matrix.
        ///  
        ///  Currently, the following file formats are supported:
        ///  
        ///  -   Windows bitmaps - \*.bmp, \*.dib (always supported)
        ///  -   GIF files - \*.gif (always supported)
        ///  -   JPEG files - \*.jpeg, \*.jpg, \*.jpe (see the *Note* section)
        ///  -   JPEG 2000 files - \*.jp2 (see the *Note* section)
        ///  -   Portable Network Graphics - \*.png (see the *Note* section)
        ///  -   WebP - \*.webp (see the *Note* section)
        ///  -   AVIF - \*.avif (see the *Note* section)
        ///  -   Portable image format - \*.pbm, \*.pgm, \*.ppm, \*.pxm, \*.pnm (always supported)
        ///  -   PFM files - \*.pfm (see the *Note* section)
        ///  -   Sun rasters - \*.sr, \*.ras (always supported)
        ///  -   TIFF files - \*.tiff, \*.tif (see the *Note* section)
        ///  -   OpenEXR Image files - \*.exr (see the *Note* section)
        ///  -   Radiance HDR - \*.hdr, \*.pic (always supported)
        ///  -   Raster and Vector geospatial data supported by GDAL (see the *Note* section)
        ///  
        ///  @note
        ///  -   The function determines the type of an image by its content, not by the file extension.
        ///  -   In the case of color images, the decoded images will have the channels stored in **B G R** order.
        ///  -   When using IMREAD_GRAYSCALE, the codec's internal grayscale conversion will be used, if available.
        ///      Results may differ from the output of cvtColor().
        ///  -   On Microsoft Windows\* and Mac OS\*, the codecs shipped with OpenCV (libjpeg, libpng, libtiff,
        ///      and libjasper) are used by default. So, OpenCV can always read JPEGs, PNGs, and TIFFs. On Mac OS,
        ///      there is also an option to use native Mac OS image readers. However, beware that currently these
        ///      native image loaders give images with different pixel values because of the color management embedded
        ///      into Mac OS.
        ///  -   On Linux\*, BSD flavors, and other Unix-like open-source operating systems, OpenCV looks for
        ///      codecs supplied with the OS. Ensure the relevant packages are installed (including development
        ///      files, such as "libjpeg-dev" in Debian\* and Ubuntu\*) to get codec support, or turn
        ///      on the OPENCV_BUILD_3RDPARTY_LIBS flag in CMake.
        ///  -   If the *WITH_GDAL* flag is set to true in CMake and @ref IMREAD_LOAD_GDAL is used to load the image,
        ///      the [GDAL](http://www.gdal.org) driver will be used to decode the image, supporting
        ///      [Raster](http://www.gdal.org/formats_list.html) and [Vector](http://www.gdal.org/ogr_formats.html) formats.
        ///  -   If EXIF information is embedded in the image file, the EXIF orientation will be taken into account,
        ///      and thus the image will be rotated accordingly unless the flags @ref IMREAD_IGNORE_ORIENTATION
        ///      or @ref IMREAD_UNCHANGED are passed.
        ///  -   Use the IMREAD_UNCHANGED flag to preserve the floating-point values from PFM images.
        ///  -   By default, the number of pixels must be less than 2^30. This limit can be changed by setting
        ///      the environment variable `OPENCV_IO_MAX_IMAGE_PIXELS`. See @ref tutorial_env_reference.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file to be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of `cv::ImreadModes`.
        /// </param>
        public static Mat imread(string filename, int flags)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imread_10(filename, flags)));


        }

        /// <summary>
        ///  Loads an image from a file.
        /// </summary>
        /// <remarks>
        ///  @anchor imread
        ///  
        ///  The `imread` function loads an image from the specified file and returns OpenCV matrix. If the image cannot be
        ///  read (because of a missing file, improper permissions, or unsupported/invalid format), the function
        ///  returns an empty matrix.
        ///  
        ///  Currently, the following file formats are supported:
        ///  
        ///  -   Windows bitmaps - \*.bmp, \*.dib (always supported)
        ///  -   GIF files - \*.gif (always supported)
        ///  -   JPEG files - \*.jpeg, \*.jpg, \*.jpe (see the *Note* section)
        ///  -   JPEG 2000 files - \*.jp2 (see the *Note* section)
        ///  -   Portable Network Graphics - \*.png (see the *Note* section)
        ///  -   WebP - \*.webp (see the *Note* section)
        ///  -   AVIF - \*.avif (see the *Note* section)
        ///  -   Portable image format - \*.pbm, \*.pgm, \*.ppm, \*.pxm, \*.pnm (always supported)
        ///  -   PFM files - \*.pfm (see the *Note* section)
        ///  -   Sun rasters - \*.sr, \*.ras (always supported)
        ///  -   TIFF files - \*.tiff, \*.tif (see the *Note* section)
        ///  -   OpenEXR Image files - \*.exr (see the *Note* section)
        ///  -   Radiance HDR - \*.hdr, \*.pic (always supported)
        ///  -   Raster and Vector geospatial data supported by GDAL (see the *Note* section)
        ///  
        ///  @note
        ///  -   The function determines the type of an image by its content, not by the file extension.
        ///  -   In the case of color images, the decoded images will have the channels stored in **B G R** order.
        ///  -   When using IMREAD_GRAYSCALE, the codec's internal grayscale conversion will be used, if available.
        ///      Results may differ from the output of cvtColor().
        ///  -   On Microsoft Windows\* and Mac OS\*, the codecs shipped with OpenCV (libjpeg, libpng, libtiff,
        ///      and libjasper) are used by default. So, OpenCV can always read JPEGs, PNGs, and TIFFs. On Mac OS,
        ///      there is also an option to use native Mac OS image readers. However, beware that currently these
        ///      native image loaders give images with different pixel values because of the color management embedded
        ///      into Mac OS.
        ///  -   On Linux\*, BSD flavors, and other Unix-like open-source operating systems, OpenCV looks for
        ///      codecs supplied with the OS. Ensure the relevant packages are installed (including development
        ///      files, such as "libjpeg-dev" in Debian\* and Ubuntu\*) to get codec support, or turn
        ///      on the OPENCV_BUILD_3RDPARTY_LIBS flag in CMake.
        ///  -   If the *WITH_GDAL* flag is set to true in CMake and @ref IMREAD_LOAD_GDAL is used to load the image,
        ///      the [GDAL](http://www.gdal.org) driver will be used to decode the image, supporting
        ///      [Raster](http://www.gdal.org/formats_list.html) and [Vector](http://www.gdal.org/ogr_formats.html) formats.
        ///  -   If EXIF information is embedded in the image file, the EXIF orientation will be taken into account,
        ///      and thus the image will be rotated accordingly unless the flags @ref IMREAD_IGNORE_ORIENTATION
        ///      or @ref IMREAD_UNCHANGED are passed.
        ///  -   Use the IMREAD_UNCHANGED flag to preserve the floating-point values from PFM images.
        ///  -   By default, the number of pixels must be less than 2^30. This limit can be changed by setting
        ///      the environment variable `OPENCV_IO_MAX_IMAGE_PIXELS`. See @ref tutorial_env_reference.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file to be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of `cv::ImreadModes`.
        /// </param>
        public static Mat imread(string filename)
        {


            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imread_11(filename)));


        }


        //
        // C++:  void cv::imread(String filename, Mat& dst, int flags = IMREAD_COLOR_BGR)
        //

        /// <summary>
        ///  Loads an image from a file.
        /// </summary>
        /// <remarks>
        ///  This is an overloaded member function, provided for convenience. It differs from the above function only in what argument(s) it accepts and the return value.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="dst">
        /// object in which the image will be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes
        ///  @note
        ///  The image passing through the img parameter can be pre-allocated. The memory is reused if the shape and the type match with the load image.
        /// </param>
        public static void imread(string filename, Mat dst, int flags)
        {
            if (dst != null) dst.ThrowIfDisposed();

            imgcodecs_Imgcodecs_imread_12(filename, dst.nativeObj, flags);


        }

        /// <summary>
        ///  Loads an image from a file.
        /// </summary>
        /// <remarks>
        ///  This is an overloaded member function, provided for convenience. It differs from the above function only in what argument(s) it accepts and the return value.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="dst">
        /// object in which the image will be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes
        ///  @note
        ///  The image passing through the img parameter can be pre-allocated. The memory is reused if the shape and the type match with the load image.
        /// </param>
        public static void imread(string filename, Mat dst)
        {
            if (dst != null) dst.ThrowIfDisposed();

            imgcodecs_Imgcodecs_imread_13(filename, dst.nativeObj);


        }


        //
        // C++:  Mat cv::imreadWithMetadata(String filename, vector_int& metadataTypes, vector_Mat& metadata, int flags = IMREAD_ANYCOLOR)
        //

        /// <summary>
        ///  Reads an image from a file together with associated metadata.
        /// </summary>
        /// <remarks>
        ///  The function imreadWithMetadata reads image from the specified file. It does the same thing as imread, but additionally reads metadata if the corresponding file contains any.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file to be loaded.
        /// </param>
        /// <param name="metadataTypes">
        /// Output vector with types of metadata chucks returned in metadata, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Output vector of vectors or vector of matrices to store the retrieved metadata
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes
        /// </param>
        public static Mat imreadWithMetadata(string filename, MatOfInt metadataTypes, List<Mat> metadata, int flags)
        {
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = new Mat();
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imreadWithMetadata_10(filename, metadataTypes_mat.nativeObj, metadata_mat.nativeObj, flags)));
            Converters.Mat_to_vector_Mat(metadata_mat, metadata);
            return retVal;
        }

        /// <summary>
        ///  Reads an image from a file together with associated metadata.
        /// </summary>
        /// <remarks>
        ///  The function imreadWithMetadata reads image from the specified file. It does the same thing as imread, but additionally reads metadata if the corresponding file contains any.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file to be loaded.
        /// </param>
        /// <param name="metadataTypes">
        /// Output vector with types of metadata chucks returned in metadata, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Output vector of vectors or vector of matrices to store the retrieved metadata
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes
        /// </param>
        public static Mat imreadWithMetadata(string filename, MatOfInt metadataTypes, List<Mat> metadata)
        {
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = new Mat();
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imreadWithMetadata_11(filename, metadataTypes_mat.nativeObj, metadata_mat.nativeObj)));
            Converters.Mat_to_vector_Mat(metadata_mat, metadata);
            return retVal;
        }


        //
        // C++:  bool cv::imreadmulti(String filename, vector_Mat& mats, int flags = IMREAD_ANYCOLOR)
        //

        /// <summary>
        ///  Loads a multi-page image from a file.
        /// </summary>
        /// <remarks>
        ///  The function imreadmulti loads a multi-page image from the specified file into a vector of Mat objects.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @sa cv::imread
        /// </param>
        public static bool imreadmulti(string filename, List<Mat> mats, int flags)
        {

            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imreadmulti_10(filename, mats_mat.nativeObj, flags);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }

        /// <summary>
        ///  Loads a multi-page image from a file.
        /// </summary>
        /// <remarks>
        ///  The function imreadmulti loads a multi-page image from the specified file into a vector of Mat objects.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @sa cv::imread
        /// </param>
        public static bool imreadmulti(string filename, List<Mat> mats)
        {

            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imreadmulti_11(filename, mats_mat.nativeObj);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }


        //
        // C++:  bool cv::imreadmulti(String filename, vector_Mat& mats, int start, int count, int flags = IMREAD_ANYCOLOR)
        //

        /// <summary>
        ///  Loads images of a multi-page image from a file.
        /// </summary>
        /// <remarks>
        ///  The function imreadmulti loads a specified range from a multi-page image from the specified file into a vector of Mat objects.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page.
        /// </param>
        /// <param name="start">
        /// Start index of the image to load
        /// </param>
        /// <param name="count">
        /// Count number of images to load
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @sa cv::imread
        /// </param>
        public static bool imreadmulti(string filename, List<Mat> mats, int start, int count, int flags)
        {

            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imreadmulti_12(filename, mats_mat.nativeObj, start, count, flags);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }

        /// <summary>
        ///  Loads images of a multi-page image from a file.
        /// </summary>
        /// <remarks>
        ///  The function imreadmulti loads a specified range from a multi-page image from the specified file into a vector of Mat objects.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page.
        /// </param>
        /// <param name="start">
        /// Start index of the image to load
        /// </param>
        /// <param name="count">
        /// Count number of images to load
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @sa cv::imread
        /// </param>
        public static bool imreadmulti(string filename, List<Mat> mats, int start, int count)
        {

            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imreadmulti_13(filename, mats_mat.nativeObj, start, count);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }


        //
        // C++:  bool cv::imreadanimation(String filename, Animation& animation, int start = 0, int count = INT16_MAX)
        //

        /// <summary>
        ///  Loads frames from an animated image file into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imreadanimation loads frames from an animated image file (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="filename">
        /// A string containing the path to the file.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the file was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imreadanimation(string filename, Animation animation, int start, int count)
        {
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imreadanimation_10(filename, animation.getNativeObjAddr(), start, count);


        }

        /// <summary>
        ///  Loads frames from an animated image file into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imreadanimation loads frames from an animated image file (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="filename">
        /// A string containing the path to the file.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the file was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imreadanimation(string filename, Animation animation, int start)
        {
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imreadanimation_11(filename, animation.getNativeObjAddr(), start);


        }

        /// <summary>
        ///  Loads frames from an animated image file into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imreadanimation loads frames from an animated image file (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="filename">
        /// A string containing the path to the file.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the file was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imreadanimation(string filename, Animation animation)
        {
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imreadanimation_12(filename, animation.getNativeObjAddr());


        }


        //
        // C++:  bool cv::imdecodeanimation(Mat buf, Animation& animation, int start = 0, int count = INT16_MAX)
        //

        /// <summary>
        ///  Loads frames from an animated image buffer into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imdecodeanimation loads frames from an animated image buffer (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="buf">
        /// A reference to an InputArray containing the image buffer.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the buffer was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imdecodeanimation(Mat buf, Animation animation, int start, int count)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imdecodeanimation_10(buf.nativeObj, animation.getNativeObjAddr(), start, count);


        }

        /// <summary>
        ///  Loads frames from an animated image buffer into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imdecodeanimation loads frames from an animated image buffer (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="buf">
        /// A reference to an InputArray containing the image buffer.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the buffer was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imdecodeanimation(Mat buf, Animation animation, int start)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imdecodeanimation_11(buf.nativeObj, animation.getNativeObjAddr(), start);


        }

        /// <summary>
        ///  Loads frames from an animated image buffer into an Animation structure.
        /// </summary>
        /// <remarks>
        ///  The function imdecodeanimation loads frames from an animated image buffer (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.
        /// </remarks>
        /// <param name="buf">
        /// A reference to an InputArray containing the image buffer.
        /// </param>
        /// <param name="animation">
        /// A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
        /// </param>
        /// <param name="start">
        /// The index of the first frame to load. This is optional and defaults to 0.
        /// </param>
        /// <param name="count">
        /// The number of frames to load. This is optional and defaults to 32767.
        /// </param>
        /// <returns>
        ///  Returns true if the buffer was successfully loaded and frames were extracted; returns false otherwise.
        /// </returns>
        public static bool imdecodeanimation(Mat buf, Animation animation)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imdecodeanimation_12(buf.nativeObj, animation.getNativeObjAddr());


        }


        //
        // C++:  bool cv::imwriteanimation(String filename, Animation animation, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Saves an Animation to a specified file.
        /// </summary>
        /// <remarks>
        ///  The function imwriteanimation saves the provided Animation data to the specified file in an animated format.
        ///  Supported formats depend on the implementation and may include formats like GIF, AVIF, APNG, or WEBP.
        /// </remarks>
        /// <param name="filename">
        /// The name of the file where the animation will be saved. The file extension determines the format.
        /// </param>
        /// <param name="animation">
        /// A constant reference to an Animation struct containing the frames and metadata to be saved.
        /// </param>
        /// <param name="params">
        /// Optional format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ...).
        ///  These parameters are used to specify additional options for the encoding process. Refer to `cv::ImwriteFlags` for details on possible parameters.
        /// </param>
        /// <returns>
        ///  Returns true if the animation was successfully saved; returns false otherwise.
        /// </returns>
        public static bool imwriteanimation(string filename, Animation animation, MatOfInt _params)
        {
            if (animation != null) animation.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imwriteanimation_10(filename, animation.getNativeObjAddr(), _params_mat.nativeObj);


        }

        /// <summary>
        ///  Saves an Animation to a specified file.
        /// </summary>
        /// <remarks>
        ///  The function imwriteanimation saves the provided Animation data to the specified file in an animated format.
        ///  Supported formats depend on the implementation and may include formats like GIF, AVIF, APNG, or WEBP.
        /// </remarks>
        /// <param name="filename">
        /// The name of the file where the animation will be saved. The file extension determines the format.
        /// </param>
        /// <param name="animation">
        /// A constant reference to an Animation struct containing the frames and metadata to be saved.
        /// </param>
        /// <param name="params">
        /// Optional format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ...).
        ///  These parameters are used to specify additional options for the encoding process. Refer to `cv::ImwriteFlags` for details on possible parameters.
        /// </param>
        /// <returns>
        ///  Returns true if the animation was successfully saved; returns false otherwise.
        /// </returns>
        public static bool imwriteanimation(string filename, Animation animation)
        {
            if (animation != null) animation.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imwriteanimation_11(filename, animation.getNativeObjAddr());


        }


        //
        // C++:  bool cv::imencodeanimation(String ext, Animation animation, vector_uchar& buf, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Encodes an Animation to a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencodeanimation encodes the provided Animation data into a memory
        ///  buffer in an animated format. Supported formats depend on the implementation and
        ///  may include formats like GIF, AVIF, APNG, or WEBP.
        /// </remarks>
        /// <param name="ext">
        /// The file extension that determines the format of the encoded data.
        /// </param>
        /// <param name="animation">
        /// A constant reference to an Animation struct containing the
        ///  frames and metadata to be encoded.
        /// </param>
        /// <param name="buf">
        /// A reference to a vector of unsigned chars where the encoded data will
        ///  be stored.
        /// </param>
        /// <param name="params">
        /// Optional format-specific parameters encoded as pairs (paramId_1,
        ///  paramValue_1, paramId_2, paramValue_2, ...). These parameters are used to
        ///  specify additional options for the encoding process. Refer to `cv::ImwriteFlags`
        ///  for details on possible parameters.
        /// </param>
        /// <returns>
        ///  Returns true if the animation was successfully encoded; returns false otherwise.
        /// </returns>
        public static bool imencodeanimation(string ext, Animation animation, MatOfByte buf, MatOfInt _params)
        {
            if (animation != null) animation.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat buf_mat = buf;
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imencodeanimation_10(ext, animation.getNativeObjAddr(), buf_mat.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Encodes an Animation to a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencodeanimation encodes the provided Animation data into a memory
        ///  buffer in an animated format. Supported formats depend on the implementation and
        ///  may include formats like GIF, AVIF, APNG, or WEBP.
        /// </remarks>
        /// <param name="ext">
        /// The file extension that determines the format of the encoded data.
        /// </param>
        /// <param name="animation">
        /// A constant reference to an Animation struct containing the
        ///  frames and metadata to be encoded.
        /// </param>
        /// <param name="buf">
        /// A reference to a vector of unsigned chars where the encoded data will
        ///  be stored.
        /// </param>
        /// <param name="params">
        /// Optional format-specific parameters encoded as pairs (paramId_1,
        ///  paramValue_1, paramId_2, paramValue_2, ...). These parameters are used to
        ///  specify additional options for the encoding process. Refer to `cv::ImwriteFlags`
        ///  for details on possible parameters.
        /// </param>
        /// <returns>
        ///  Returns true if the animation was successfully encoded; returns false otherwise.
        /// </returns>
        public static bool imencodeanimation(string ext, Animation animation, MatOfByte buf)
        {
            if (animation != null) animation.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            Mat buf_mat = buf;
            return imgcodecs_Imgcodecs_imencodeanimation_11(ext, animation.getNativeObjAddr(), buf_mat.nativeObj);


        }


        //
        // C++:  size_t cv::imcount(String filename, int flags = IMREAD_ANYCOLOR)
        //

        /// <summary>
        ///  Returns the number of images inside the given file
        /// </summary>
        /// <remarks>
        ///  The function imcount returns the number of pages in a multi-page image (e.g. TIFF), the number of frames in an animation (e.g. AVIF), and 1 otherwise.
        ///  If the image cannot be decoded, 0 is returned.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @todo when cv::IMREAD_LOAD_GDAL flag used the return value will be 0 or 1 because OpenCV's GDAL decoder doesn't support multi-page reading yet.
        /// </param>
        public static long imcount(string filename, int flags)
        {


            return imgcodecs_Imgcodecs_imcount_10(filename, flags);


        }

        /// <summary>
        ///  Returns the number of images inside the given file
        /// </summary>
        /// <remarks>
        ///  The function imcount returns the number of pages in a multi-page image (e.g. TIFF), the number of frames in an animation (e.g. AVIF), and 1 otherwise.
        ///  If the image cannot be decoded, 0 is returned.
        /// </remarks>
        /// <param name="filename">
        /// Name of file to be loaded.
        /// </param>
        /// <param name="flags">
        /// Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.
        ///  @todo when cv::IMREAD_LOAD_GDAL flag used the return value will be 0 or 1 because OpenCV's GDAL decoder doesn't support multi-page reading yet.
        /// </param>
        public static long imcount(string filename)
        {


            return imgcodecs_Imgcodecs_imcount_11(filename);


        }


        //
        // C++:  bool cv::imwrite(String filename, Mat img, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Saves an image to a specified file.
        /// </summary>
        /// <remarks>
        ///  The function imwrite saves the image to the specified file. The image format is chosen based on the
        ///  filename extension (see cv::imread for the list of extensions). In general, only 8-bit unsigned (CV_8U)
        ///  single-channel or 3-channel (with 'BGR' channel order) images
        ///  can be saved using this function, with these exceptions:
        ///  
        ///  - With OpenEXR encoder, only 32-bit float (CV_32F) images can be saved.
        ///    - 8-bit unsigned (CV_8U) images are not supported.
        ///  - With Radiance HDR encoder, non 64-bit float (CV_64F) images can be saved.
        ///    - All images will be converted to 32-bit float (CV_32F).
        ///  - With JPEG 2000 encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With JPEG XL encoder, 8-bit unsigned (CV_8U), 16-bit unsigned (CV_16U) and 32-bit float(CV_32F) images can be saved.
        ///    - JPEG XL images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) / 32-bit float 4-channel (CV_32FC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535/1.0.
        ///  - With PAM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With PNG encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///    - PNG images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535(see the code sample below).
        ///  - With PGM/PPM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With TIFF encoder, 8-bit unsigned (CV_8U), 8-bit signed (CV_8S),
        ///                       16-bit unsigned (CV_16U), 16-bit signed (CV_16S),
        ///                       32-bit signed (CV_32S),
        ///                       32-bit float (CV_32F) and 64-bit float (CV_64F) images can be saved.
        ///    - Multiple images (vector of Mat) can be saved in TIFF format (see the code sample below).
        ///    - 32-bit float 3-channel (CV_32FC3) TIFF images will be saved
        ///      using the LogLuv high dynamic range encoding (4 bytes per pixel)
        ///  - With GIF encoder, 8-bit unsigned (CV_8U) images can be saved.
        ///    - GIF images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255.
        ///    - 8-bit single-channel images (CV_8UC1) are not supported due to GIF's limitation to indexed color formats.
        ///  
        ///  If the image format is not supported, the image will be converted to 8-bit unsigned (CV_8U) and saved that way.
        ///  
        ///  If the format, depth or channel order is different, use
        ///  Mat::convertTo and cv::cvtColor to convert it before saving. Or, use the universal FileStorage I/O
        ///  functions to save the image to XML or YAML format.
        ///  
        ///  The sample below shows how to create a BGRA image, how to set custom compression parameters and save it to a PNG file.
        ///  It also demonstrates how to save multiple images in a TIFF file:
        ///  @include snippets/imgcodecs_imwrite.cpp
        /// </remarks>
        /// <param name="filename">
        /// Name of the file.
        /// </param>
        /// <param name="img">
        /// (Mat or vector of Mat) Image or Images to be saved.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see cv::ImwriteFlags
        /// </param>
        public static bool imwrite(string filename, Mat img, MatOfInt _params)
        {
            if (img != null) img.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imwrite_10(filename, img.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Saves an image to a specified file.
        /// </summary>
        /// <remarks>
        ///  The function imwrite saves the image to the specified file. The image format is chosen based on the
        ///  filename extension (see cv::imread for the list of extensions). In general, only 8-bit unsigned (CV_8U)
        ///  single-channel or 3-channel (with 'BGR' channel order) images
        ///  can be saved using this function, with these exceptions:
        ///  
        ///  - With OpenEXR encoder, only 32-bit float (CV_32F) images can be saved.
        ///    - 8-bit unsigned (CV_8U) images are not supported.
        ///  - With Radiance HDR encoder, non 64-bit float (CV_64F) images can be saved.
        ///    - All images will be converted to 32-bit float (CV_32F).
        ///  - With JPEG 2000 encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With JPEG XL encoder, 8-bit unsigned (CV_8U), 16-bit unsigned (CV_16U) and 32-bit float(CV_32F) images can be saved.
        ///    - JPEG XL images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) / 32-bit float 4-channel (CV_32FC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535/1.0.
        ///  - With PAM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With PNG encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///    - PNG images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535(see the code sample below).
        ///  - With PGM/PPM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
        ///  - With TIFF encoder, 8-bit unsigned (CV_8U), 8-bit signed (CV_8S),
        ///                       16-bit unsigned (CV_16U), 16-bit signed (CV_16S),
        ///                       32-bit signed (CV_32S),
        ///                       32-bit float (CV_32F) and 64-bit float (CV_64F) images can be saved.
        ///    - Multiple images (vector of Mat) can be saved in TIFF format (see the code sample below).
        ///    - 32-bit float 3-channel (CV_32FC3) TIFF images will be saved
        ///      using the LogLuv high dynamic range encoding (4 bytes per pixel)
        ///  - With GIF encoder, 8-bit unsigned (CV_8U) images can be saved.
        ///    - GIF images with an alpha channel can be saved using this function.
        ///      To achieve this, create an 8-bit 4-channel (CV_8UC4) BGRA image, ensuring the alpha channel is the last component.
        ///      Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255.
        ///    - 8-bit single-channel images (CV_8UC1) are not supported due to GIF's limitation to indexed color formats.
        ///  
        ///  If the image format is not supported, the image will be converted to 8-bit unsigned (CV_8U) and saved that way.
        ///  
        ///  If the format, depth or channel order is different, use
        ///  Mat::convertTo and cv::cvtColor to convert it before saving. Or, use the universal FileStorage I/O
        ///  functions to save the image to XML or YAML format.
        ///  
        ///  The sample below shows how to create a BGRA image, how to set custom compression parameters and save it to a PNG file.
        ///  It also demonstrates how to save multiple images in a TIFF file:
        ///  @include snippets/imgcodecs_imwrite.cpp
        /// </remarks>
        /// <param name="filename">
        /// Name of the file.
        /// </param>
        /// <param name="img">
        /// (Mat or vector of Mat) Image or Images to be saved.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see cv::ImwriteFlags
        /// </param>
        public static bool imwrite(string filename, Mat img)
        {
            if (img != null) img.ThrowIfDisposed();

            return imgcodecs_Imgcodecs_imwrite_11(filename, img.nativeObj);


        }


        //
        // C++:  bool cv::imwriteWithMetadata(String filename, Mat img, vector_int metadataTypes, vector_Mat metadata, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Saves an image to a specified file with metadata
        /// </summary>
        /// <remarks>
        ///  The function imwriteWithMetadata saves the image to the specified file. It does the same thing as imwrite, but additionally writes metadata if the corresponding format supports it.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file. As with imwrite, image format is determined by the file extension.
        /// </param>
        /// <param name="img">
        /// (Mat or vector of Mat) Image or Images to be saved.
        /// </param>
        /// <param name="metadataTypes">
        /// Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Vector of vectors or vector of matrices with chunks of metadata to store into the file
        /// </param>
        /// <param name="params">
        /// Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see cv::ImwriteFlags
        /// </param>
        public static bool imwriteWithMetadata(string filename, Mat img, MatOfInt metadataTypes, List<Mat> metadata, MatOfInt _params)
        {
            if (img != null) img.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = Converters.vector_Mat_to_Mat(metadata);
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imwriteWithMetadata_10(filename, img.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Saves an image to a specified file with metadata
        /// </summary>
        /// <remarks>
        ///  The function imwriteWithMetadata saves the image to the specified file. It does the same thing as imwrite, but additionally writes metadata if the corresponding format supports it.
        /// </remarks>
        /// <param name="filename">
        /// Name of the file. As with imwrite, image format is determined by the file extension.
        /// </param>
        /// <param name="img">
        /// (Mat or vector of Mat) Image or Images to be saved.
        /// </param>
        /// <param name="metadataTypes">
        /// Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Vector of vectors or vector of matrices with chunks of metadata to store into the file
        /// </param>
        /// <param name="params">
        /// Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see cv::ImwriteFlags
        /// </param>
        public static bool imwriteWithMetadata(string filename, Mat img, MatOfInt metadataTypes, List<Mat> metadata)
        {
            if (img != null) img.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = Converters.vector_Mat_to_Mat(metadata);
            return imgcodecs_Imgcodecs_imwriteWithMetadata_11(filename, img.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj);


        }


        //
        // C++:  bool cv::imwritemulti(String filename, vector_Mat img, vector_int _params = std::vector<int>())
        //

        public static bool imwritemulti(string filename, List<Mat> img, MatOfInt _params)
        {
            if (_params != null) _params.ThrowIfDisposed();
            using Mat img_mat = Converters.vector_Mat_to_Mat(img);
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imwritemulti_10(filename, img_mat.nativeObj, _params_mat.nativeObj);


        }

        public static bool imwritemulti(string filename, List<Mat> img)
        {

            using Mat img_mat = Converters.vector_Mat_to_Mat(img);
            return imgcodecs_Imgcodecs_imwritemulti_11(filename, img_mat.nativeObj);


        }


        //
        // C++:  Mat cv::imdecode(Mat buf, int flags)
        //

        /// <summary>
        ///  Reads an image from a buffer in memory.
        /// </summary>
        /// <remarks>
        ///  The function imdecode reads an image from the specified buffer in the memory. If the buffer is too short or
        ///  contains invalid data, the function returns an empty matrix ( Mat::data==NULL ).
        ///  
        ///  See cv::imread for the list of supported formats and flags description.
        ///  
        ///  @note In the case of color images, the decoded images will have the channels stored in **B G R** order.
        /// </remarks>
        /// <param name="buf">
        /// Input array or vector of bytes.
        /// </param>
        /// <param name="flags">
        /// The same flags as in cv::imread, see cv::ImreadModes.
        /// </param>
        public static Mat imdecode(Mat buf, int flags)
        {
            if (buf != null) buf.ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imdecode_10(buf.nativeObj, flags)));


        }


        //
        // C++:  Mat cv::imdecodeWithMetadata(Mat buf, vector_int& metadataTypes, vector_Mat& metadata, int flags = IMREAD_ANYCOLOR)
        //

        /// <summary>
        ///  Reads an image from a buffer in memory together with associated metadata.
        /// </summary>
        /// <remarks>
        ///  The function imdecode reads an image from the specified buffer in the memory. If the buffer is too short or
        ///  contains invalid data, the function returns an empty matrix ( Mat::data==NULL ).
        ///  
        ///  See cv::imread for the list of supported formats and flags description.
        ///  
        ///  @note In the case of color images, the decoded images will have the channels stored in **B G R** order.
        /// </remarks>
        /// <param name="buf">
        /// Input array or vector of bytes.
        /// </param>
        /// <param name="metadataTypes">
        /// Output vector with types of metadata chucks returned in metadata, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Output vector of vectors or vector of matrices to store the retrieved metadata
        /// </param>
        /// <param name="flags">
        /// The same flags as in cv::imread, see cv::ImreadModes.
        /// </param>
        public static Mat imdecodeWithMetadata(Mat buf, MatOfInt metadataTypes, List<Mat> metadata, int flags)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = new Mat();
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imdecodeWithMetadata_10(buf.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj, flags)));
            Converters.Mat_to_vector_Mat(metadata_mat, metadata);
            return retVal;
        }

        /// <summary>
        ///  Reads an image from a buffer in memory together with associated metadata.
        /// </summary>
        /// <remarks>
        ///  The function imdecode reads an image from the specified buffer in the memory. If the buffer is too short or
        ///  contains invalid data, the function returns an empty matrix ( Mat::data==NULL ).
        ///  
        ///  See cv::imread for the list of supported formats and flags description.
        ///  
        ///  @note In the case of color images, the decoded images will have the channels stored in **B G R** order.
        /// </remarks>
        /// <param name="buf">
        /// Input array or vector of bytes.
        /// </param>
        /// <param name="metadataTypes">
        /// Output vector with types of metadata chucks returned in metadata, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Output vector of vectors or vector of matrices to store the retrieved metadata
        /// </param>
        /// <param name="flags">
        /// The same flags as in cv::imread, see cv::ImreadModes.
        /// </param>
        public static Mat imdecodeWithMetadata(Mat buf, MatOfInt metadataTypes, List<Mat> metadata)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = new Mat();
            Mat retVal = new Mat(DisposableObject.ThrowIfNullIntPtr(imgcodecs_Imgcodecs_imdecodeWithMetadata_11(buf.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj)));
            Converters.Mat_to_vector_Mat(metadata_mat, metadata);
            return retVal;
        }


        //
        // C++:  bool cv::imdecodemulti(Mat buf, int flags, vector_Mat& mats, Range range = Range::all())
        //

        /// <summary>
        ///  Reads a multi-page image from a buffer in memory.
        /// </summary>
        /// <remarks>
        ///  The function imdecodemulti reads a multi-page image from the specified buffer in the memory. If the buffer is too short or
        ///  contains invalid data, the function returns false.
        ///  
        ///  See cv::imreadmulti for the list of supported formats and flags description.
        ///  
        ///  @note In the case of color images, the decoded images will have the channels stored in **B G R** order.
        /// </remarks>
        /// <param name="buf">
        /// Input array or vector of bytes.
        /// </param>
        /// <param name="flags">
        /// The same flags as in cv::imread, see cv::ImreadModes.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page, if more than one.
        /// </param>
        /// <param name="range">
        /// A continuous selection of pages.
        /// </param>
        public static bool imdecodemulti(Mat buf, int flags, List<Mat> mats, Range range)
        {
            if (buf != null) buf.ThrowIfDisposed();
            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imdecodemulti_10(buf.nativeObj, flags, mats_mat.nativeObj, range.start, range.end);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }

        /// <summary>
        ///  Reads a multi-page image from a buffer in memory.
        /// </summary>
        /// <remarks>
        ///  The function imdecodemulti reads a multi-page image from the specified buffer in the memory. If the buffer is too short or
        ///  contains invalid data, the function returns false.
        ///  
        ///  See cv::imreadmulti for the list of supported formats and flags description.
        ///  
        ///  @note In the case of color images, the decoded images will have the channels stored in **B G R** order.
        /// </remarks>
        /// <param name="buf">
        /// Input array or vector of bytes.
        /// </param>
        /// <param name="flags">
        /// The same flags as in cv::imread, see cv::ImreadModes.
        /// </param>
        /// <param name="mats">
        /// A vector of Mat objects holding each page, if more than one.
        /// </param>
        /// <param name="range">
        /// A continuous selection of pages.
        /// </param>
        public static bool imdecodemulti(Mat buf, int flags, List<Mat> mats)
        {
            if (buf != null) buf.ThrowIfDisposed();
            using Mat mats_mat = new Mat();
            bool retVal = imgcodecs_Imgcodecs_imdecodemulti_11(buf.nativeObj, flags, mats_mat.nativeObj);
            Converters.Mat_to_vector_Mat(mats_mat, mats);
            return retVal;
        }


        //
        // C++:  bool cv::imencode(String ext, Mat img, vector_uchar& buf, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Encodes an image into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
        ///  result. See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="img">
        /// Image to be compressed.
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed image.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencode(string ext, Mat img, MatOfByte buf, MatOfInt _params)
        {
            if (img != null) img.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat buf_mat = buf;
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imencode_10(ext, img.nativeObj, buf_mat.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Encodes an image into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
        ///  result. See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="img">
        /// Image to be compressed.
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed image.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencode(string ext, Mat img, MatOfByte buf)
        {
            if (img != null) img.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            Mat buf_mat = buf;
            return imgcodecs_Imgcodecs_imencode_11(ext, img.nativeObj, buf_mat.nativeObj);


        }


        //
        // C++:  bool cv::imencodeWithMetadata(String ext, Mat img, vector_int metadataTypes, vector_Mat metadata, vector_uchar& buf, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Encodes an image into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
        ///  result. See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="img">
        /// Image to be compressed.
        /// </param>
        /// <param name="metadataTypes">
        /// Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Vector of vectors or vector of matrices with chunks of metadata to store into the file
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed image.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencodeWithMetadata(string ext, Mat img, MatOfInt metadataTypes, List<Mat> metadata, MatOfByte buf, MatOfInt _params)
        {
            if (img != null) img.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = Converters.vector_Mat_to_Mat(metadata);
            Mat buf_mat = buf;
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imencodeWithMetadata_10(ext, img.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj, buf_mat.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Encodes an image into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
        ///  result. See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="img">
        /// Image to be compressed.
        /// </param>
        /// <param name="metadataTypes">
        /// Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
        /// </param>
        /// <param name="metadata">
        /// Vector of vectors or vector of matrices with chunks of metadata to store into the file
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed image.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencodeWithMetadata(string ext, Mat img, MatOfInt metadataTypes, List<Mat> metadata, MatOfByte buf)
        {
            if (img != null) img.ThrowIfDisposed();
            if (metadataTypes != null) metadataTypes.ThrowIfDisposed();
            if (buf != null) buf.ThrowIfDisposed();
            Mat metadataTypes_mat = metadataTypes;
            using Mat metadata_mat = Converters.vector_Mat_to_Mat(metadata);
            Mat buf_mat = buf;
            return imgcodecs_Imgcodecs_imencodeWithMetadata_11(ext, img.nativeObj, metadataTypes_mat.nativeObj, metadata_mat.nativeObj, buf_mat.nativeObj);


        }


        //
        // C++:  bool cv::imencodemulti(String ext, vector_Mat imgs, vector_uchar& buf, vector_int _params = std::vector<int>())
        //

        /// <summary>
        ///  Encodes array of images into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function is analog to cv::imencode for in-memory multi-page image compression.
        ///  See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="imgs">
        /// Vector of images to be written.
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed data.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencodemulti(string ext, List<Mat> imgs, MatOfByte buf, MatOfInt _params)
        {
            if (buf != null) buf.ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            using Mat imgs_mat = Converters.vector_Mat_to_Mat(imgs);
            Mat buf_mat = buf;
            Mat _params_mat = _params;
            return imgcodecs_Imgcodecs_imencodemulti_10(ext, imgs_mat.nativeObj, buf_mat.nativeObj, _params_mat.nativeObj);


        }

        /// <summary>
        ///  Encodes array of images into a memory buffer.
        /// </summary>
        /// <remarks>
        ///  The function is analog to cv::imencode for in-memory multi-page image compression.
        ///  See cv::imwrite for the list of supported formats and flags description.
        /// </remarks>
        /// <param name="ext">
        /// File extension that defines the output format. Must include a leading period.
        /// </param>
        /// <param name="imgs">
        /// Vector of images to be written.
        /// </param>
        /// <param name="buf">
        /// Output buffer resized to fit the compressed data.
        /// </param>
        /// <param name="params">
        /// Format-specific parameters. See cv::imwrite and cv::ImwriteFlags.
        /// </param>
        public static bool imencodemulti(string ext, List<Mat> imgs, MatOfByte buf)
        {
            if (buf != null) buf.ThrowIfDisposed();
            using Mat imgs_mat = Converters.vector_Mat_to_Mat(imgs);
            Mat buf_mat = buf;
            return imgcodecs_Imgcodecs_imencodemulti_11(ext, imgs_mat.nativeObj, buf_mat.nativeObj);


        }


        //
        // C++:  bool cv::haveImageReader(String filename)
        //

        /// <summary>
        ///  Checks if the specified image file can be decoded by OpenCV.
        /// </summary>
        /// <remarks>
        ///  The function haveImageReader checks if OpenCV is capable of reading the specified file.
        ///  This can be useful for verifying support for a given image format before attempting to load an image.
        /// </remarks>
        /// <param name="filename">
        /// The name of the file to be checked.
        /// </param>
        /// <returns>
        ///  true if an image reader for the specified file is available and the file can be opened, false otherwise.
        /// </returns>
        /// <remarks>
        ///  @note The function checks the availability of image codecs that are either built into OpenCV or dynamically loaded.
        ///  It does not load the image codec implementation and decode data, but uses signature check.
        ///  If the file cannot be opened or the format is unsupported, the function will return false.
        ///  
        ///  @sa cv::haveImageWriter, cv::imread, cv::imdecode
        /// </remarks>
        public static bool haveImageReader(string filename)
        {


            return imgcodecs_Imgcodecs_haveImageReader_10(filename);


        }


        //
        // C++:  bool cv::haveImageWriter(String filename)
        //

        /// <summary>
        ///  Checks if the specified image file or specified file extension can be encoded by OpenCV.
        /// </summary>
        /// <remarks>
        ///  The function haveImageWriter checks if OpenCV is capable of writing images with the specified file extension.
        ///  This can be useful for verifying support for a given image format before attempting to save an image.
        /// </remarks>
        /// <param name="filename">
        /// The name of the file or the file extension (e.g., ".jpg", ".png").
        ///  It is recommended to provide the file extension rather than the full file name.
        /// </param>
        /// <returns>
        ///  true if an image writer for the specified extension is available, false otherwise.
        /// </returns>
        /// <remarks>
        ///  @note The function checks the availability of image codecs that are either built into OpenCV or dynamically loaded.
        ///  It does not check for the actual existence of the file but rather the ability to write files of the given type.
        ///  
        ///  @sa cv::haveImageReader, cv::imwrite, cv::imencode
        /// </remarks>
        public static bool haveImageWriter(string filename)
        {


            return imgcodecs_Imgcodecs_haveImageWriter_10(filename);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  Mat cv::imread(String filename, int flags = IMREAD_COLOR_BGR)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imread_10(string filename, int flags);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imread_11(string filename);

        // C++:  void cv::imread(String filename, Mat& dst, int flags = IMREAD_COLOR_BGR)
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Imgcodecs_imread_12(string filename, IntPtr dst_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern void imgcodecs_Imgcodecs_imread_13(string filename, IntPtr dst_nativeObj);

        // C++:  Mat cv::imreadWithMetadata(String filename, vector_int& metadataTypes, vector_Mat& metadata, int flags = IMREAD_ANYCOLOR)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imreadWithMetadata_10(string filename, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imreadWithMetadata_11(string filename, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj);

        // C++:  bool cv::imreadmulti(String filename, vector_Mat& mats, int flags = IMREAD_ANYCOLOR)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadmulti_10(string filename, IntPtr mats_mat_nativeObj, int flags);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadmulti_11(string filename, IntPtr mats_mat_nativeObj);

        // C++:  bool cv::imreadmulti(String filename, vector_Mat& mats, int start, int count, int flags = IMREAD_ANYCOLOR)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadmulti_12(string filename, IntPtr mats_mat_nativeObj, int start, int count, int flags);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadmulti_13(string filename, IntPtr mats_mat_nativeObj, int start, int count);

        // C++:  bool cv::imreadanimation(String filename, Animation& animation, int start = 0, int count = INT16_MAX)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadanimation_10(string filename, IntPtr animation_nativeObj, int start, int count);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadanimation_11(string filename, IntPtr animation_nativeObj, int start);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imreadanimation_12(string filename, IntPtr animation_nativeObj);

        // C++:  bool cv::imdecodeanimation(Mat buf, Animation& animation, int start = 0, int count = INT16_MAX)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imdecodeanimation_10(IntPtr buf_nativeObj, IntPtr animation_nativeObj, int start, int count);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imdecodeanimation_11(IntPtr buf_nativeObj, IntPtr animation_nativeObj, int start);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imdecodeanimation_12(IntPtr buf_nativeObj, IntPtr animation_nativeObj);

        // C++:  bool cv::imwriteanimation(String filename, Animation animation, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwriteanimation_10(string filename, IntPtr animation_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwriteanimation_11(string filename, IntPtr animation_nativeObj);

        // C++:  bool cv::imencodeanimation(String ext, Animation animation, vector_uchar& buf, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodeanimation_10(string ext, IntPtr animation_nativeObj, IntPtr buf_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodeanimation_11(string ext, IntPtr animation_nativeObj, IntPtr buf_mat_nativeObj);

        // C++:  size_t cv::imcount(String filename, int flags = IMREAD_ANYCOLOR)
        [DllImport(LIBNAME)]
        private static extern long imgcodecs_Imgcodecs_imcount_10(string filename, int flags);
        [DllImport(LIBNAME)]
        private static extern long imgcodecs_Imgcodecs_imcount_11(string filename);

        // C++:  bool cv::imwrite(String filename, Mat img, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwrite_10(string filename, IntPtr img_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwrite_11(string filename, IntPtr img_nativeObj);

        // C++:  bool cv::imwriteWithMetadata(String filename, Mat img, vector_int metadataTypes, vector_Mat metadata, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwriteWithMetadata_10(string filename, IntPtr img_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwriteWithMetadata_11(string filename, IntPtr img_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj);

        // C++:  bool cv::imwritemulti(String filename, vector_Mat img, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwritemulti_10(string filename, IntPtr img_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imwritemulti_11(string filename, IntPtr img_mat_nativeObj);

        // C++:  Mat cv::imdecode(Mat buf, int flags)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imdecode_10(IntPtr buf_nativeObj, int flags);

        // C++:  Mat cv::imdecodeWithMetadata(Mat buf, vector_int& metadataTypes, vector_Mat& metadata, int flags = IMREAD_ANYCOLOR)
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imdecodeWithMetadata_10(IntPtr buf_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern IntPtr imgcodecs_Imgcodecs_imdecodeWithMetadata_11(IntPtr buf_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj);

        // C++:  bool cv::imdecodemulti(Mat buf, int flags, vector_Mat& mats, Range range = Range::all())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imdecodemulti_10(IntPtr buf_nativeObj, int flags, IntPtr mats_mat_nativeObj, int range_start, int range_end);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imdecodemulti_11(IntPtr buf_nativeObj, int flags, IntPtr mats_mat_nativeObj);

        // C++:  bool cv::imencode(String ext, Mat img, vector_uchar& buf, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencode_10(string ext, IntPtr img_nativeObj, IntPtr buf_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencode_11(string ext, IntPtr img_nativeObj, IntPtr buf_mat_nativeObj);

        // C++:  bool cv::imencodeWithMetadata(String ext, Mat img, vector_int metadataTypes, vector_Mat metadata, vector_uchar& buf, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodeWithMetadata_10(string ext, IntPtr img_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj, IntPtr buf_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodeWithMetadata_11(string ext, IntPtr img_nativeObj, IntPtr metadataTypes_mat_nativeObj, IntPtr metadata_mat_nativeObj, IntPtr buf_mat_nativeObj);

        // C++:  bool cv::imencodemulti(String ext, vector_Mat imgs, vector_uchar& buf, vector_int _params = std::vector<int>())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodemulti_10(string ext, IntPtr imgs_mat_nativeObj, IntPtr buf_mat_nativeObj, IntPtr _params_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_imencodemulti_11(string ext, IntPtr imgs_mat_nativeObj, IntPtr buf_mat_nativeObj);

        // C++:  bool cv::haveImageReader(String filename)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_haveImageReader_10(string filename);

        // C++:  bool cv::haveImageWriter(String filename)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool imgcodecs_Imgcodecs_haveImageWriter_10(string filename);

    }
}
