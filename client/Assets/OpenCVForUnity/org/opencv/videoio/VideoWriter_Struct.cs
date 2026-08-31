

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoioModule
{
    public partial class VideoWriter : DisposableOpenCVObject
    {


        //
        // C++:   cv::VideoWriter::VideoWriter(String filename, int fourcc, double fps, Size frameSize, bool isColor = true)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="filename">
        /// Name of the output video file.
        /// </param>
        /// <param name="fourcc">
        /// 4-character code of codec used to compress the frames. For example,
        ///      VideoWriter::fourcc('P','I','M','1') is a MPEG-1 codec, VideoWriter::fourcc('M','J','P','G')
        ///      is a motion-jpeg codec etc. List of codes can be obtained at
        ///      [MSDN](https://docs.microsoft.com/en-us/windows/win32/medfound/video-fourccs) page
        ///      or with this [page](https://fourcc.org/codecs.php)
        ///      of the fourcc site for a more complete list). FFMPEG backend with MP4 container natively uses
        ///      other values as fourcc code: see [ObjectType](http://mp4ra.org/#/codecs),
        ///      so you may receive a warning message from OpenCV about fourcc code conversion.
        /// </param>
        /// <param name="fps">
        /// Framerate of the created video stream.
        /// </param>
        /// <param name="frameSize">
        /// Size of the video frames.
        /// </param>
        /// <param name="isColor">
        /// If it is not zero, the encoder will expect and encode color frames, otherwise it
        ///      will work with grayscale frames.
        /// </param>
        /// <remarks>
        ///      @b Tips:
        ///      - With some backends `fourcc=-1` pops up the codec selection dialog from the system.
        ///      - To save image sequence use a proper filename (eg. `img_%02d.jpg`) and `fourcc=0`
        ///        OR `fps=0`. Use uncompressed image format (eg. `img_%02d.BMP`) to save raw frames.
        ///      - Most codecs are lossy. If you want lossless video file you need to use a lossless codecs
        ///        (eg. FFMPEG FFV1, Huffman HFYU, Lagarith LAGS, etc...)
        ///      - If FFMPEG is enabled, using `codec=0; fps=0;` you can create an uncompressed (raw) video file.
        ///      - If FFMPEG is used, we allow frames of odd width or height, but in this case we truncate
        ///        the rightmost column/the bottom row. Probably, this should be handled more elegantly,
        ///        but some internal functions inside FFMPEG swscale require even width/height.
        /// </remarks>
        public VideoWriter(string filename, int fourcc, double fps, in Vec2d frameSize, bool isColor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_11(filename, fourcc, fps, frameSize.Item1, frameSize.Item2, isColor));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="filename">
        /// Name of the output video file.
        /// </param>
        /// <param name="fourcc">
        /// 4-character code of codec used to compress the frames. For example,
        ///      VideoWriter::fourcc('P','I','M','1') is a MPEG-1 codec, VideoWriter::fourcc('M','J','P','G')
        ///      is a motion-jpeg codec etc. List of codes can be obtained at
        ///      [MSDN](https://docs.microsoft.com/en-us/windows/win32/medfound/video-fourccs) page
        ///      or with this [page](https://fourcc.org/codecs.php)
        ///      of the fourcc site for a more complete list). FFMPEG backend with MP4 container natively uses
        ///      other values as fourcc code: see [ObjectType](http://mp4ra.org/#/codecs),
        ///      so you may receive a warning message from OpenCV about fourcc code conversion.
        /// </param>
        /// <param name="fps">
        /// Framerate of the created video stream.
        /// </param>
        /// <param name="frameSize">
        /// Size of the video frames.
        /// </param>
        /// <param name="isColor">
        /// If it is not zero, the encoder will expect and encode color frames, otherwise it
        ///      will work with grayscale frames.
        /// </param>
        /// <remarks>
        ///      @b Tips:
        ///      - With some backends `fourcc=-1` pops up the codec selection dialog from the system.
        ///      - To save image sequence use a proper filename (eg. `img_%02d.jpg`) and `fourcc=0`
        ///        OR `fps=0`. Use uncompressed image format (eg. `img_%02d.BMP`) to save raw frames.
        ///      - Most codecs are lossy. If you want lossless video file you need to use a lossless codecs
        ///        (eg. FFMPEG FFV1, Huffman HFYU, Lagarith LAGS, etc...)
        ///      - If FFMPEG is enabled, using `codec=0; fps=0;` you can create an uncompressed (raw) video file.
        ///      - If FFMPEG is used, we allow frames of odd width or height, but in this case we truncate
        ///        the rightmost column/the bottom row. Probably, this should be handled more elegantly,
        ///        but some internal functions inside FFMPEG swscale require even width/height.
        /// </remarks>
        public VideoWriter(string filename, int fourcc, double fps, in Vec2d frameSize)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_12(filename, fourcc, fps, frameSize.Item1, frameSize.Item2));


        }


        //
        // C++:   cv::VideoWriter::VideoWriter(String filename, int apiPreference, int fourcc, double fps, Size frameSize, bool isColor = true)
        //

        /// <remarks>
        ///  @overload
        ///      The `apiPreference` parameter allows to specify API backends to use. Can be used to enforce a specific reader implementation
        ///      if multiple are available: e.g. cv::CAP_FFMPEG or cv::CAP_GSTREAMER.
        /// </remarks>
        public VideoWriter(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize, bool isColor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_13(filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2, isColor));


        }

        /// <remarks>
        ///  @overload
        ///      The `apiPreference` parameter allows to specify API backends to use. Can be used to enforce a specific reader implementation
        ///      if multiple are available: e.g. cv::CAP_FFMPEG or cv::CAP_GSTREAMER.
        /// </remarks>
        public VideoWriter(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_14(filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2));


        }


        //
        // C++:   cv::VideoWriter::VideoWriter(String filename, int fourcc, double fps, Size frameSize, vector_int _params)
        //

        /// <remarks>
        ///  @overload
        ///         The `params` parameter allows to specify extra encoder parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .)
        ///         see cv::VideoWriterProperties
        /// </remarks>
        public VideoWriter(string filename, int fourcc, double fps, in Vec2d frameSize, MatOfInt _params)
        {
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_15(filename, fourcc, fps, frameSize.Item1, frameSize.Item2, _params_mat.nativeObj));


        }


        //
        // C++:   cv::VideoWriter::VideoWriter(String filename, int apiPreference, int fourcc, double fps, Size frameSize, vector_int _params)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public VideoWriter(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize, MatOfInt _params)
        {
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            nativeObj = DisposableObject.ThrowIfNullIntPtr(videoio_VideoWriter_VideoWriter_16(filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2, _params_mat.nativeObj));


        }


        //
        // C++:  bool cv::VideoWriter::open(String filename, int fourcc, double fps, Size frameSize, bool isColor = true)
        //

        /// <summary>
        ///  Initializes or reinitializes video writer.
        /// </summary>
        /// <remarks>
        ///      The method opens video writer. Parameters are the same as in the constructor
        ///      VideoWriter::VideoWriter.
        /// </remarks>
        /// <returns>
        ///  `true` if video writer has been successfully initialized
        /// </returns>
        /// <remarks>
        ///      The method first calls VideoWriter::release to close the already opened file.
        /// </remarks>
        public bool open(string filename, int fourcc, double fps, in Vec2d frameSize, bool isColor)
        {
            ThrowIfDisposed();

            return videoio_VideoWriter_open_10(nativeObj, filename, fourcc, fps, frameSize.Item1, frameSize.Item2, isColor);


        }

        /// <summary>
        ///  Initializes or reinitializes video writer.
        /// </summary>
        /// <remarks>
        ///      The method opens video writer. Parameters are the same as in the constructor
        ///      VideoWriter::VideoWriter.
        /// </remarks>
        /// <returns>
        ///  `true` if video writer has been successfully initialized
        /// </returns>
        /// <remarks>
        ///      The method first calls VideoWriter::release to close the already opened file.
        /// </remarks>
        public bool open(string filename, int fourcc, double fps, in Vec2d frameSize)
        {
            ThrowIfDisposed();

            return videoio_VideoWriter_open_11(nativeObj, filename, fourcc, fps, frameSize.Item1, frameSize.Item2);


        }


        //
        // C++:  bool cv::VideoWriter::open(String filename, int apiPreference, int fourcc, double fps, Size frameSize, bool isColor = true)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public bool open(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize, bool isColor)
        {
            ThrowIfDisposed();

            return videoio_VideoWriter_open_12(nativeObj, filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2, isColor);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public bool open(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize)
        {
            ThrowIfDisposed();

            return videoio_VideoWriter_open_13(nativeObj, filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2);


        }


        //
        // C++:  bool cv::VideoWriter::open(String filename, int fourcc, double fps, Size frameSize, vector_int _params)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public bool open(string filename, int fourcc, double fps, in Vec2d frameSize, MatOfInt _params)
        {
            ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            return videoio_VideoWriter_open_14(nativeObj, filename, fourcc, fps, frameSize.Item1, frameSize.Item2, _params_mat.nativeObj);


        }


        //
        // C++:  bool cv::VideoWriter::open(String filename, int apiPreference, int fourcc, double fps, Size frameSize, vector_int _params)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public bool open(string filename, int apiPreference, int fourcc, double fps, in Vec2d frameSize, MatOfInt _params)
        {
            ThrowIfDisposed();
            if (_params != null) _params.ThrowIfDisposed();
            Mat _params_mat = _params;
            return videoio_VideoWriter_open_15(nativeObj, filename, apiPreference, fourcc, fps, frameSize.Item1, frameSize.Item2, _params_mat.nativeObj);


        }

    }
}
