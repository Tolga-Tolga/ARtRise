
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoioModule
{
    // C++: class Videoio


    public class Videoio
    {

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_DC1394_OFF = -4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_DC1394_MODE_MANUAL = -3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_DC1394_MODE_AUTO = -2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_DC1394_MODE_ONE_PUSH_AUTO = -1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_DC1394_MAX = 31;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR = 1 << 31;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IMAGE_GENERATOR = 1 << 30;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IR_GENERATOR = 1 << 29;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_GENERATORS_MASK = CAP_OPENNI_DEPTH_GENERATOR + CAP_OPENNI_IMAGE_GENERATOR + CAP_OPENNI_IR_GENERATOR;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_OUTPUT_MODE = 100;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_FRAME_MAX_DEPTH = 101;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_BASELINE = 102;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_FOCAL_LENGTH = 103;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_REGISTRATION = 104;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_REGISTRATION_ON = CAP_PROP_OPENNI_REGISTRATION;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_APPROX_FRAME_SYNC = 105;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_MAX_BUFFER_SIZE = 106;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_CIRCLE_BUFFER = 107;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_MAX_TIME_DURATION = 108;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI_GENERATOR_PRESENT = 109;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI2_SYNC = 110;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_OPENNI2_MIRROR = 111;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IMAGE_GENERATOR_PRESENT = +CAP_OPENNI_IMAGE_GENERATOR + CAP_PROP_OPENNI_GENERATOR_PRESENT;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IMAGE_GENERATOR_OUTPUT_MODE = +CAP_OPENNI_IMAGE_GENERATOR + CAP_PROP_OPENNI_OUTPUT_MODE;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR_PRESENT = +CAP_OPENNI_DEPTH_GENERATOR + CAP_PROP_OPENNI_GENERATOR_PRESENT;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR_BASELINE = +CAP_OPENNI_DEPTH_GENERATOR + CAP_PROP_OPENNI_BASELINE;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR_FOCAL_LENGTH = +CAP_OPENNI_DEPTH_GENERATOR + CAP_PROP_OPENNI_FOCAL_LENGTH;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR_REGISTRATION = +CAP_OPENNI_DEPTH_GENERATOR + CAP_PROP_OPENNI_REGISTRATION;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_GENERATOR_REGISTRATION_ON = CAP_OPENNI_DEPTH_GENERATOR_REGISTRATION;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IR_GENERATOR_PRESENT = +CAP_OPENNI_IR_GENERATOR + CAP_PROP_OPENNI_GENERATOR_PRESENT;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DEPTH_MAP = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_POINT_CLOUD_MAP = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DISPARITY_MAP = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_DISPARITY_MAP_32F = 3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_VALID_DEPTH_MASK = 4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_BGR_IMAGE = 5;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_GRAY_IMAGE = 6;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_IR_IMAGE = 7;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_VGA_30HZ = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_SXGA_15HZ = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_SXGA_30HZ = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_QVGA_30HZ = 3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_OPENNI_QVGA_60HZ = 4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GSTREAMER_QUEUE_LENGTH = 200;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_MULTICASTIP = 300;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_FRAMESTARTTRIGGERMODE = 301;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_DECIMATIONHORIZONTAL = 302;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_DECIMATIONVERTICAL = 303;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_BINNINGX = 304;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_BINNINGY = 305;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_PVAPI_PIXELFORMAT = 306;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_FSTRIGMODE_FREERUN = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_FSTRIGMODE_SYNCIN1 = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_FSTRIGMODE_SYNCIN2 = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_FSTRIGMODE_FIXEDRATE = 3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_FSTRIGMODE_SOFTWARE = 4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_DECIMATION_OFF = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_DECIMATION_2OUTOF4 = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_DECIMATION_2OUTOF8 = 4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_DECIMATION_2OUTOF16 = 8;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_MONO8 = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_MONO16 = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_BAYER8 = 3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_BAYER16 = 4;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_RGB24 = 5;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_BGR24 = 6;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_RGBA32 = 7;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PVAPI_PIXELFORMAT_BGRA32 = 8;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DOWNSAMPLING = 400;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DATA_FORMAT = 401;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_OFFSET_X = 402;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_OFFSET_Y = 403;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TRG_SOURCE = 404;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TRG_SOFTWARE = 405;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GPI_SELECTOR = 406;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GPI_MODE = 407;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GPI_LEVEL = 408;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GPO_SELECTOR = 409;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GPO_MODE = 410;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LED_SELECTOR = 411;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LED_MODE = 412;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_MANUAL_WB = 413;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AUTO_WB = 414;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG = 415;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_EXP_PRIORITY = 416;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AE_MAX_LIMIT = 417;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AG_MAX_LIMIT = 418;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG_LEVEL = 419;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TIMEOUT = 420;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_EXPOSURE = 421;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_EXPOSURE_BURST_COUNT = 422;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GAIN_SELECTOR = 423;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GAIN = 424;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DOWNSAMPLING_TYPE = 426;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BINNING_SELECTOR = 427;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BINNING_VERTICAL = 428;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BINNING_HORIZONTAL = 429;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BINNING_PATTERN = 430;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DECIMATION_SELECTOR = 431;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DECIMATION_VERTICAL = 432;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DECIMATION_HORIZONTAL = 433;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DECIMATION_PATTERN = 434;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TEST_PATTERN_GENERATOR_SELECTOR = 587;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TEST_PATTERN = 588;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_DATA_FORMAT = 435;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SHUTTER_TYPE = 436;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_TAPS = 437;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG_ROI_OFFSET_X = 439;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG_ROI_OFFSET_Y = 440;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG_ROI_WIDTH = 441;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AEAG_ROI_HEIGHT = 442;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BPC = 445;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_WB_KR = 448;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_WB_KG = 449;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_WB_KB = 450;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_WIDTH = 451;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HEIGHT = 452;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_REGION_SELECTOR = 589;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_REGION_MODE = 595;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LIMIT_BANDWIDTH = 459;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_DATA_BIT_DEPTH = 460;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_OUTPUT_DATA_BIT_DEPTH = 461;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_DATA_BIT_DEPTH = 462;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_OUTPUT_DATA_PACKING = 463;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_OUTPUT_DATA_PACKING_TYPE = 464;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IS_COOLED = 465;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_COOLING = 466;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TARGET_TEMP = 467;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CHIP_TEMP = 468;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HOUS_TEMP = 469;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HOUS_BACK_SIDE_TEMP = 590;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_BOARD_TEMP = 596;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CMS = 470;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_APPLY_CMS = 471;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_IS_COLOR = 474;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_COLOR_FILTER_ARRAY = 475;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GAMMAY = 476;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_GAMMAC = 477;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SHARPNESS = 478;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_00 = 479;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_01 = 480;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_02 = 481;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_03 = 482;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_10 = 483;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_11 = 484;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_12 = 485;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_13 = 486;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_20 = 487;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_21 = 488;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_22 = 489;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_23 = 490;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_30 = 491;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_31 = 492;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_32 = 493;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_CC_MATRIX_33 = 494;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEFAULT_CC_MATRIX = 495;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TRG_SELECTOR = 498;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_FRAME_BURST_COUNT = 499;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEBOUNCE_EN = 507;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEBOUNCE_T0 = 508;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEBOUNCE_T1 = 509;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEBOUNCE_POL = 510;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_MODE = 511;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_APERTURE_VALUE = 512;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FOCUS_MOVEMENT_VALUE = 513;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FOCUS_MOVE = 514;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FOCUS_DISTANCE = 515;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FOCAL_LENGTH = 516;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FEATURE_SELECTOR = 517;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LENS_FEATURE = 518;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEVICE_MODEL_ID = 521;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEVICE_SN = 522;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_DATA_FORMAT_RGB32_ALPHA = 529;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_PAYLOAD_SIZE = 530;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TRANSPORT_PIXEL_FORMAT = 531;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_CLOCK_FREQ_HZ = 532;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_CLOCK_FREQ_INDEX = 533;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_OUTPUT_CHANNEL_COUNT = 534;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_FRAMERATE = 535;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_COUNTER_SELECTOR = 536;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_COUNTER_VALUE = 537;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_TIMING_MODE = 538;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AVAILABLE_BANDWIDTH = 539;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BUFFER_POLICY = 540;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LUT_EN = 541;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LUT_INDEX = 542;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_LUT_VALUE = 543;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TRG_DELAY = 544;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TS_RST_MODE = 545;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_TS_RST_SOURCE = 546;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IS_DEVICE_EXIST = 547;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_BUFFER_SIZE = 548;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_BUFFER_SIZE_UNIT = 549;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_TRANSPORT_BUFFER_SIZE = 550;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_BUFFERS_QUEUE_SIZE = 551;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ACQ_TRANSPORT_BUFFER_COMMIT = 552;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_RECENT_FRAME = 553;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEVICE_RESET = 554;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_COLUMN_FPN_CORRECTION = 555;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_ROW_FPN_CORRECTION = 591;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_MODE = 558;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HDR = 559;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HDR_KNEEPOINT_COUNT = 560;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HDR_T1 = 561;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HDR_T2 = 562;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_KNEEPOINT1 = 563;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_KNEEPOINT2 = 564;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_IMAGE_BLACK_LEVEL = 565;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_HW_REVISION = 571;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_DEBUG_LEVEL = 572;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_AUTO_BANDWIDTH_CALCULATION = 573;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_FFS_FILE_ID = 594;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_FFS_FILE_SIZE = 580;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_FREE_FFS_SIZE = 581;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_USED_FFS_SIZE = 582;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_FFS_ACCESS_KEY = 583;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_FEATURE_SELECTOR = 585;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_XI_SENSOR_FEATURE_VALUE = 586;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_ARAVIS_AUTOTRIGGER = 600;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_ANDROID_DEVICE_TORCH = 8001;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IOS_DEVICE_FOCUS = 9001;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IOS_DEVICE_EXPOSURE = 9002;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IOS_DEVICE_FLASH = 9003;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IOS_DEVICE_WHITEBALANCE = 9004;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IOS_DEVICE_TORCH = 9005;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_OFFSET_X = 10001;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_OFFSET_Y = 10002;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_WIDTH_MAX = 10003;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_HEIGH_MAX = 10004;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_SENS_WIDTH = 10005;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GIGA_FRAME_SENS_HEIGH = 10006;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_PROFILE_COUNT = 11001;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_PROFILE_IDX = 11002;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_DEPTH_LOW_CONFIDENCE_VALUE = 11003;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_DEPTH_SATURATION_VALUE = 11004;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_DEPTH_CONFIDENCE_THRESHOLD = 11005;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_DEPTH_FOCAL_LENGTH_HORZ = 11006;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_INTELPERC_DEPTH_FOCAL_LENGTH_VERT = 11007;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_DEPTH_GENERATOR = 1 << 29;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_IMAGE_GENERATOR = 1 << 28;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_IR_GENERATOR = 1 << 27;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_GENERATORS_MASK = CAP_INTELPERC_DEPTH_GENERATOR + CAP_INTELPERC_IMAGE_GENERATOR + CAP_INTELPERC_IR_GENERATOR;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_DEPTH_MAP = 0;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_UVDEPTH_MAP = 1;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_IR_MAP = 2;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_INTELPERC_IMAGE = 3;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_PREVIEW = 17001;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_WIDGET_ENUMERATE = 17002;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_RELOAD_CONFIG = 17003;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_RELOAD_ON_CHANGE = 17004;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_COLLECT_MSGS = 17005;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_GPHOTO2_FLUSH_MSGS = 17006;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_SPEED = 17007;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_APERTURE = 17008;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_EXPOSUREPROGRAM = 17009;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_VIEWFINDER = 17010;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IMAGES_BASE = 18000;

        /// <summary>
        /// C++: enum <unnamed>
        /// </summary>
        public const int CAP_PROP_IMAGES_LAST = 19000;


        /// <summary>
        /// C++: enum VideoAccelerationType (cv.VideoAccelerationType)
        /// </summary>
        public const int VIDEO_ACCELERATION_NONE = 0;

        /// <summary>
        /// C++: enum VideoAccelerationType (cv.VideoAccelerationType)
        /// </summary>
        public const int VIDEO_ACCELERATION_ANY = 1;

        /// <summary>
        /// C++: enum VideoAccelerationType (cv.VideoAccelerationType)
        /// </summary>
        public const int VIDEO_ACCELERATION_D3D11 = 2;

        /// <summary>
        /// C++: enum VideoAccelerationType (cv.VideoAccelerationType)
        /// </summary>
        public const int VIDEO_ACCELERATION_VAAPI = 3;

        /// <summary>
        /// C++: enum VideoAccelerationType (cv.VideoAccelerationType)
        /// </summary>
        public const int VIDEO_ACCELERATION_MFX = 4;


        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_ANY = 0;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_VFW = 200;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_V4L = 200;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_V4L2 = CAP_V4L;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_FIREWIRE = 300;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_FIREWARE = CAP_FIREWIRE;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_IEEE1394 = CAP_FIREWIRE;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_DC1394 = CAP_FIREWIRE;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_CMU1394 = CAP_FIREWIRE;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_QT = 500;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_UNICAP = 600;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_DSHOW = 700;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_PVAPI = 800;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENNI = 900;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENNI_ASUS = 910;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_ANDROID = 1000;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_XIAPI = 1100;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_AVFOUNDATION = 1200;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_GIGANETIX = 1300;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_MSMF = 1400;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_WINRT = 1410;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_INTELPERC = 1500;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_REALSENSE = 1500;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENNI2 = 1600;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENNI2_ASUS = 1610;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENNI2_ASTRA = 1620;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_GPHOTO2 = 1700;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_GSTREAMER = 1800;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_FFMPEG = 1900;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_IMAGES = 2000;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_ARAVIS = 2100;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OPENCV_MJPEG = 2200;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_INTEL_MFX = 2300;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_XINE = 2400;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_UEYE = 2500;

        /// <summary>
        /// C++: enum VideoCaptureAPIs (cv.VideoCaptureAPIs)
        /// </summary>
        public const int CAP_OBSENSOR = 2600;


        /// <summary>
        /// C++: enum VideoCaptureOBSensorDataType (cv.VideoCaptureOBSensorDataType)
        /// </summary>
        public const int CAP_OBSENSOR_DEPTH_MAP = 0;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorDataType (cv.VideoCaptureOBSensorDataType)
        /// </summary>
        public const int CAP_OBSENSOR_BGR_IMAGE = 1;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorDataType (cv.VideoCaptureOBSensorDataType)
        /// </summary>
        public const int CAP_OBSENSOR_IR_IMAGE = 2;


        /// <summary>
        /// C++: enum VideoCaptureOBSensorGenerators (cv.VideoCaptureOBSensorGenerators)
        /// </summary>
        public const int CAP_OBSENSOR_DEPTH_GENERATOR = 1 << 29;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorGenerators (cv.VideoCaptureOBSensorGenerators)
        /// </summary>
        public const int CAP_OBSENSOR_IMAGE_GENERATOR = 1 << 28;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorGenerators (cv.VideoCaptureOBSensorGenerators)
        /// </summary>
        public const int CAP_OBSENSOR_IR_GENERATOR = 1 << 27;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorGenerators (cv.VideoCaptureOBSensorGenerators)
        /// </summary>
        public const int CAP_OBSENSOR_GENERATORS_MASK = CAP_OBSENSOR_DEPTH_GENERATOR + CAP_OBSENSOR_IMAGE_GENERATOR + CAP_OBSENSOR_IR_GENERATOR;


        /// <summary>
        /// C++: enum VideoCaptureOBSensorProperties (cv.VideoCaptureOBSensorProperties)
        /// </summary>
        public const int CAP_PROP_OBSENSOR_INTRINSIC_FX = 26001;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorProperties (cv.VideoCaptureOBSensorProperties)
        /// </summary>
        public const int CAP_PROP_OBSENSOR_INTRINSIC_FY = 26002;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorProperties (cv.VideoCaptureOBSensorProperties)
        /// </summary>
        public const int CAP_PROP_OBSENSOR_INTRINSIC_CX = 26003;

        /// <summary>
        /// C++: enum VideoCaptureOBSensorProperties (cv.VideoCaptureOBSensorProperties)
        /// </summary>
        public const int CAP_PROP_OBSENSOR_INTRINSIC_CY = 26004;


        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_POS_MSEC = 0;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_POS_FRAMES = 1;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_POS_AVI_RATIO = 2;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FRAME_WIDTH = 3;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FRAME_HEIGHT = 4;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FPS = 5;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FOURCC = 6;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FRAME_COUNT = 7;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FORMAT = 8;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_MODE = 9;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_BRIGHTNESS = 10;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_CONTRAST = 11;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_SATURATION = 12;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_HUE = 13;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_GAIN = 14;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_EXPOSURE = 15;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_CONVERT_RGB = 16;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_WHITE_BALANCE_BLUE_U = 17;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_RECTIFICATION = 18;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_MONOCHROME = 19;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_SHARPNESS = 20;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUTO_EXPOSURE = 21;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_GAMMA = 22;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_TEMPERATURE = 23;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_TRIGGER = 24;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_TRIGGER_DELAY = 25;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_WHITE_BALANCE_RED_V = 26;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_ZOOM = 27;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FOCUS = 28;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_GUID = 29;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_ISO_SPEED = 30;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_BACKLIGHT = 32;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_PAN = 33;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_TILT = 34;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_ROLL = 35;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_IRIS = 36;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_SETTINGS = 37;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_BUFFERSIZE = 38;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUTOFOCUS = 39;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_SAR_NUM = 40;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_SAR_DEN = 41;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_BACKEND = 42;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_CHANNEL = 43;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUTO_WB = 44;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_WB_TEMPERATURE = 45;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_CODEC_PIXEL_FORMAT = 46;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_BITRATE = 47;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_ORIENTATION_META = 48;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_ORIENTATION_AUTO = 49;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_HW_ACCELERATION = 50;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_HW_DEVICE = 51;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_HW_ACCELERATION_USE_OPENCL = 52;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_OPEN_TIMEOUT_MSEC = 53;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_READ_TIMEOUT_MSEC = 54;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_STREAM_OPEN_TIME_USEC = 55;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_VIDEO_TOTAL_CHANNELS = 56;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_VIDEO_STREAM = 57;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_STREAM = 58;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_POS = 59;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_SHIFT_NSEC = 60;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_DATA_DEPTH = 61;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_SAMPLES_PER_SECOND = 62;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_BASE_INDEX = 63;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_TOTAL_CHANNELS = 64;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_TOTAL_STREAMS = 65;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_AUDIO_SYNCHRONIZE = 66;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_LRF_HAS_KEY_FRAME = 67;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_CODEC_EXTRADATA_INDEX = 68;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_FRAME_TYPE = 69;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_N_THREADS = 70;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_PTS = 71;

        /// <summary>
        /// C++: enum VideoCaptureProperties (cv.VideoCaptureProperties)
        /// </summary>
        public const int CAP_PROP_DTS_DELAY = 72;


        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_QUALITY = 1;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_FRAMEBYTES = 2;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_NSTRIPES = 3;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_IS_COLOR = 4;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_DEPTH = 5;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_HW_ACCELERATION = 6;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_HW_DEVICE = 7;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_HW_ACCELERATION_USE_OPENCL = 8;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_RAW_VIDEO = 9;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_KEY_INTERVAL = 10;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_KEY_FLAG = 11;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_PTS = 12;

        /// <summary>
        /// C++: enum VideoWriterProperties (cv.VideoWriterProperties)
        /// </summary>
        public const int VIDEOWRITER_PROP_DTS_DELAY = 13;


        //
        // C++:  String cv::videoio_registry::getBackendName(VideoCaptureAPIs api)
        //

        /// <summary>
        ///  Returns backend API name or "UnknownVideoAPI(xxx)"
        /// </summary>
        /// <param name="api">
        /// backend ID (#VideoCaptureAPIs)
        /// </param>
        public static string getBackendName(int api)
        {


            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getBackendName_10(api)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getBackends()
        //

        /// <summary>
        ///  Returns list of all available backends
        /// </summary>
        public static List<int> getBackends()
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getBackends_10()));
            Converters.Mat_to_vector_VideoCaptureAPIs(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getCameraBackends()
        //

        /// <summary>
        ///  Returns list of available backends which works via `cv::VideoCapture(int index)`
        /// </summary>
        public static List<int> getCameraBackends()
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getCameraBackends_10()));
            Converters.Mat_to_vector_VideoCaptureAPIs(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getStreamBackends()
        //

        /// <summary>
        ///  Returns list of available backends which works via `cv::VideoCapture(filename)`
        /// </summary>
        public static List<int> getStreamBackends()
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getStreamBackends_10()));
            Converters.Mat_to_vector_VideoCaptureAPIs(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getStreamBufferedBackends()
        //

        /// <summary>
        ///  Returns list of available backends which works via `cv::VideoCapture(buffer)`
        /// </summary>
        public static List<int> getStreamBufferedBackends()
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getStreamBufferedBackends_10()));
            Converters.Mat_to_vector_VideoCaptureAPIs(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getWriterBackends()
        //

        /// <summary>
        ///  Returns list of available backends which works via `cv::VideoWriter()`
        /// </summary>
        public static List<int> getWriterBackends()
        {

            List<int> retVal = new List<int>();
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getWriterBackends_10()));
            Converters.Mat_to_vector_VideoCaptureAPIs(retValMat, retVal);
            return retVal;
        }


        //
        // C++:  bool cv::videoio_registry::hasBackend(VideoCaptureAPIs api)
        //

        /// <summary>
        ///  Returns true if backend is available
        /// </summary>
        public static bool hasBackend(int api)
        {


            return videoio_Videoio_hasBackend_10(api);


        }


        //
        // C++:  bool cv::videoio_registry::isBackendBuiltIn(VideoCaptureAPIs api)
        //

        /// <summary>
        ///  Returns true if backend is built in (false if backend is used as plugin)
        /// </summary>
        public static bool isBackendBuiltIn(int api)
        {


            return videoio_Videoio_isBackendBuiltIn_10(api);


        }


        //
        // C++:  string cv::videoio_registry::getCameraBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        //

        /// <summary>
        ///  Returns description and ABI/API version of videoio plugin's camera interface
        /// </summary>
        public static string getCameraBackendPluginVersion(int api, int[] version_ABI, int[] version_API)
        {

            double[] version_ABI_out = new double[1];
            double[] version_API_out = new double[1];
            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getCameraBackendPluginVersion_10(api, version_ABI_out, version_API_out)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);
            if (version_ABI != null) version_ABI[0] = (int)version_ABI_out[0];
            if (version_API != null) version_API[0] = (int)version_API_out[0];
            return retVal;
        }


        //
        // C++:  string cv::videoio_registry::getStreamBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        //

        /// <summary>
        ///  Returns description and ABI/API version of videoio plugin's stream capture interface
        /// </summary>
        public static string getStreamBackendPluginVersion(int api, int[] version_ABI, int[] version_API)
        {

            double[] version_ABI_out = new double[1];
            double[] version_API_out = new double[1];
            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getStreamBackendPluginVersion_10(api, version_ABI_out, version_API_out)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);
            if (version_ABI != null) version_ABI[0] = (int)version_ABI_out[0];
            if (version_API != null) version_API[0] = (int)version_API_out[0];
            return retVal;
        }


        //
        // C++:  string cv::videoio_registry::getStreamBufferedBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        //

        /// <summary>
        ///  Returns description and ABI/API version of videoio plugin's buffer capture interface
        /// </summary>
        public static string getStreamBufferedBackendPluginVersion(int api, int[] version_ABI, int[] version_API)
        {

            double[] version_ABI_out = new double[1];
            double[] version_API_out = new double[1];
            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getStreamBufferedBackendPluginVersion_10(api, version_ABI_out, version_API_out)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);
            if (version_ABI != null) version_ABI[0] = (int)version_ABI_out[0];
            if (version_API != null) version_API[0] = (int)version_API_out[0];
            return retVal;
        }


        //
        // C++:  string cv::videoio_registry::getWriterBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        //

        /// <summary>
        ///  Returns description and ABI/API version of videoio plugin's writer interface
        /// </summary>
        public static string getWriterBackendPluginVersion(int api, int[] version_ABI, int[] version_API)
        {

            double[] version_ABI_out = new double[1];
            double[] version_API_out = new double[1];
            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(videoio_Videoio_getWriterBackendPluginVersion_10(api, version_ABI_out, version_API_out)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);
            if (version_ABI != null) version_ABI[0] = (int)version_ABI_out[0];
            if (version_API != null) version_API[0] = (int)version_API_out[0];
            return retVal;
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  String cv::videoio_registry::getBackendName(VideoCaptureAPIs api)
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getBackendName_10(int api);

        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getBackends()
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getBackends_10();

        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getCameraBackends()
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getCameraBackends_10();

        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getStreamBackends()
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getStreamBackends_10();

        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getStreamBufferedBackends()
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getStreamBufferedBackends_10();

        // C++:  vector_VideoCaptureAPIs cv::videoio_registry::getWriterBackends()
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getWriterBackends_10();

        // C++:  bool cv::videoio_registry::hasBackend(VideoCaptureAPIs api)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool videoio_Videoio_hasBackend_10(int api);

        // C++:  bool cv::videoio_registry::isBackendBuiltIn(VideoCaptureAPIs api)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool videoio_Videoio_isBackendBuiltIn_10(int api);

        // C++:  string cv::videoio_registry::getCameraBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getCameraBackendPluginVersion_10(int api, double[] version_ABI_out, double[] version_API_out);

        // C++:  string cv::videoio_registry::getStreamBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getStreamBackendPluginVersion_10(int api, double[] version_ABI_out, double[] version_API_out);

        // C++:  string cv::videoio_registry::getStreamBufferedBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getStreamBufferedBackendPluginVersion_10(int api, double[] version_ABI_out, double[] version_API_out);

        // C++:  string cv::videoio_registry::getWriterBackendPluginVersion(VideoCaptureAPIs api, int& version_ABI, int& version_API)
        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_Videoio_getWriterBackendPluginVersion_10(int api, double[] version_ABI_out, double[] version_API_out);

    }
}
