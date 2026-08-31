using OpenCVForUnity.CoreModule;
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

#if UNITY_6000_0_OR_NEWER
using System.Threading;
using UnityEngine.Rendering;
#endif

#if !OPENCV_DONT_USE_UNSAFE_CODE
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
#endif

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// OpenCV Mat utilities.
    /// </summary>
    public static partial class OpenCVMatUtils
    {

        #region MetToTexture2D

        /// <summary>
        /// Converts an OpenCV Mat to a Unity Texture2D.
        /// </summary>
        /// <remarks>
        /// This method converts an OpenCV Mat to a Unity Texture2D. Conversion is possible even when the number of bytes per pixel differs, such as from Mat(8UC1) to Texture2D(RGBA32).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// Performance is optimal when the per-pixel data size and color order match, such as with Mat(8UC4) and Texture2D(RGBA32).
        /// If the texture format is not RGBA32, BGRA32, RGB24, Alpha8, or R8, the <a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a> and <a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a> methods are used. In such cases, it is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="MatToTexture2DRaw">MatToTexture2DRaw</see>.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D must have the same size as the source Mat.
        /// The destination Texture2D supports the following formats. (<a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a>)
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before conversion.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after conversion.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatToTexture2D(Mat mat, Texture2D texture2D, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            MatToTexture2D(mat, texture2D, null, null, flip, flipCode, updateMipmaps, makeNoLongerReadable);
        }

        /// <summary>
        /// Converts an OpenCV Mat to a Unity Texture2D.
        /// </summary>
        /// <remarks>
        /// This method converts an OpenCV Mat to a Unity Texture2D. Conversion is possible even when the number of bytes per pixel differs, such as from Mat(8UC1) to Texture2D(RGBA32).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// Performance is optimal when the per-pixel data size and color order match, such as with Mat(8UC4) and Texture2D(RGBA32).
        /// If the texture format is not RGBA32, BGRA32, RGB24, Alpha8, or R8, the <a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a> and <a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a> methods are used. In such cases, it is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="MatToTexture2DRaw">MatToTexture2DRaw</see>.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D must have the same size as the source Mat.
        /// The destination Texture2D supports the following formats. (<a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a>)
        /// </param>
        /// <param name="pixels32Buffer">
        /// An optional array for receiving pixel data as Color32. Using this array helps avoid memory allocation each frame.
        /// Ensure the array is initialized to a length matching the texture’s width * height. (<a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a>)
        /// </param>
        /// <param name="rawTextureDataBuffer">
        /// An optional array for receiving raw texture data. This only works when the "OPENCV_DONT_USE_UNSAFE_CODE" symbol is defined and <paramref name="texture2D"/> has no mipmaps (mipmapCount == 1).
        /// Passing a byte array can help avoid memory allocation each frame. Ensure the array length matches the texture's raw data size. (<a href="https://docs.unity3d.com/ScriptReference/Texture2D.GetRawTextureData.html">Texture2D.GetRawTextureData</a>)
        /// The default value is <c>null</c>.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before conversion.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after conversion.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void MatToTexture2D(Mat mat, Texture2D texture2D, Color32[] pixels32Buffer, byte[] rawTextureDataBuffer = null, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat.cols() != texture2D.width || mat.rows() != texture2D.height)
                throw new ArgumentException("The Texture2D must have the same size.");

            int type = mat.type();
            TextureFormat format = texture2D.format;
            if (!(type == CvType.CV_8UC1 || type == CvType.CV_8UC3 || type == CvType.CV_8UC4))
                throw new ArgumentException("The Mat must have the types 'CV_8UC4' (RGBA) , 'CV_8UC3' (RGB) or 'CV_8UC1' (GRAY).");

#if OPENCV_DONT_USE_UNSAFE_CODE
            if (((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))) && texture2D.mipmapCount == 1 && mat.isContinuous())
            {
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }
                texture2D.LoadRawTextureData((IntPtr)mat.dataAddr(), (int)(mat.total() * mat.elemSize()));
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }

                return;
            }
#endif

            if (format == TextureFormat.RGBA32 || format == TextureFormat.BGRA32 || format == TextureFormat.RGB24 || format == TextureFormat.Alpha8 || format == TextureFormat.R8)
            {
#if !OPENCV_DONT_USE_UNSAFE_CODE
                unsafe
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(texture2D.GetRawTextureData<byte>()), (int)format, flip, flipCode);
                }
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);

                return;
#else
                if (texture2D.mipmapCount == 1 && (rawTextureDataBuffer != null || pixels32Buffer == null))
                {
                    GCHandle rawTextureDataHandle;
                    if (rawTextureDataBuffer == null)
                    {
                        byte[] rawTextureData = texture2D.GetRawTextureData();
                        rawTextureDataHandle = GCHandle.Alloc(rawTextureData, GCHandleType.Pinned);
                        try
                        {
                            OpenCVForUnity_MatToTexture(mat.nativeObj, rawTextureDataHandle.AddrOfPinnedObject(), (int)format, flip, flipCode);
                            texture2D.LoadRawTextureData(rawTextureDataHandle.AddrOfPinnedObject(), rawTextureData.Length);
                        }
                        finally
                        {
                            if (rawTextureDataHandle.IsAllocated)
                                rawTextureDataHandle.Free();
                        }
                    }
                    else
                    {
                        int textureChannels = 1;
                        switch (format)
                        {
                            case TextureFormat.RGBA32:
                            case TextureFormat.BGRA32:
                                textureChannels = 4;
                                break;
                            case TextureFormat.RGB24:
                                textureChannels = 3;
                                break;
                            case TextureFormat.Alpha8:
                            case TextureFormat.R8:
                            default:
                                textureChannels = 1;
                                break;
                        }
                        if (rawTextureDataBuffer.Length != texture2D.width * texture2D.height * textureChannels)
                            throw new ArgumentException("The rawTextureDataBuffer array length must match the size of texture2D data.");

                        rawTextureDataHandle = GCHandle.Alloc(rawTextureDataBuffer, GCHandleType.Pinned);
                        try
                        {
                            OpenCVForUnity_MatToTexture(mat.nativeObj, rawTextureDataHandle.AddrOfPinnedObject(), (int)format, flip, flipCode);
                            texture2D.LoadRawTextureData(rawTextureDataHandle.AddrOfPinnedObject(), rawTextureDataBuffer.Length);
                        }
                        finally
                        {
                            if (rawTextureDataHandle.IsAllocated)
                                rawTextureDataHandle.Free();
                        }
                    }
                    texture2D.Apply(updateMipmaps, makeNoLongerReadable);

                    return;
                }
#endif
            }

            //You can use SetPixels32 with the following texture formats:
            //https://docs.unity3d.com/2020.3/Documentation/ScriptReference/Texture2D.SetPixels32.html
#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (pixels32Buffer == null)
            {
                Color32[] pixels32 = texture2D.GetPixels32();
                unsafe
                {
                    fixed (Color32* ptr = pixels32)
                    {
                        OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)ptr, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }

                ConvertToGrayscaleIfFormatMatches(pixels32, texture2D.format);

                texture2D.SetPixels32(pixels32);
            }
            else
            {
                if (pixels32Buffer.Length != mat.total())
                    throw new ArgumentException("The pixels32Buffer array length must match the number of mat elements.");

                unsafe
                {
                    fixed (Color32* ptr = pixels32Buffer)
                    {
                        OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)ptr, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }

                ConvertToGrayscaleIfFormatMatches(pixels32Buffer, texture2D.format);

                texture2D.SetPixels32(pixels32Buffer);
            }
            texture2D.Apply(updateMipmaps, makeNoLongerReadable);
#else
            GCHandle pixels32Handle;
            if (pixels32Buffer == null)
            {
                Color32[] pixels32 = texture2D.GetPixels32();

                pixels32Handle = GCHandle.Alloc(pixels32, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, pixels32Handle.AddrOfPinnedObject(), (int)TextureFormat.RGBA32, flip, flipCode);

                    ConvertToGrayscaleIfFormatMatches(pixels32, texture2D.format);

                    texture2D.SetPixels32(pixels32);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
            else
            {
                if (pixels32Buffer.Length != mat.total())
                    throw new ArgumentException("The pixels32Buffer array length must match the number of mat elements.");

                pixels32Handle = GCHandle.Alloc(pixels32Buffer, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, pixels32Handle.AddrOfPinnedObject(), (int)TextureFormat.RGBA32, flip, flipCode);

                    ConvertToGrayscaleIfFormatMatches(pixels32Buffer, texture2D.format);

                    texture2D.SetPixels32(pixels32Buffer);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
            texture2D.Apply(updateMipmaps, makeNoLongerReadable);
#endif
        }

        /// <summary>
        /// Converts an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level.
        /// </summary>
        /// <remarks>
        /// This method converts an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level. Conversion is possible even when the number of bytes per pixel differs, such as from Mat(8UC1) to Texture2D(RGBA32).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// It is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="MatToTexture2DRaw">MatToTexture2DRaw</see>.
        /// The source Mat must be the same size as the mipmap of the destination Texture2D.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D most be supports mipmaps, as the conversion will target the<paramref name="mipLevel"/>.
        /// The destination Texture2D supports the following formats. (<a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a>)
        /// </param>
        /// <param name="mipLevel">
        /// The mipmap level to which the Mat will be converted. The level must be within the range supported by the destination Texture2D.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before conversion.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after conversion.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatToTexture2D(Mat mat, Texture2D texture2D, int mipLevel, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            MatToTexture2D(mat, texture2D, mipLevel, null, flip, flipCode, updateMipmaps, makeNoLongerReadable);
        }

        /// <summary>
        /// Converts an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level.
        /// </summary>
        /// <remarks>
        /// This method converts an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level. Conversion is possible even when the number of bytes per pixel differs, such as from Mat(8UC1) to Texture2D(RGBA32).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// It is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="MatToTexture2DRaw">MatToTexture2DRaw</see>.
        /// The source Mat must be the same size as the mipmap of the destination Texture2D.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D most be supports mipmaps, as the conversion will target the<paramref name="mipLevel"/>.
        /// The destination Texture2D supports the following formats. (<a href="https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels32.html">Texture2D.SetPixels32</a>)
        /// </param>
        /// <param name="mipLevel">
        /// The mipmap level to which the Mat will be converted. The level must be within the range supported by the destination Texture2D.
        /// </param>
        /// <param name="pixels32Buffer">
        /// An optional array for receiving pixel data as Color32. Using this array helps avoid memory allocation each frame.
        /// Ensure the array is initialized to a length matching the  texture’s mipmap width * height. (<a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a>)
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before conversion.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after conversion.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void MatToTexture2D(Mat mat, Texture2D texture2D, int mipLevel, Color32[] pixels32Buffer, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            int type = mat.type();
            TextureFormat format = texture2D.format;
            if (!(type == CvType.CV_8UC1 || type == CvType.CV_8UC3 || type == CvType.CV_8UC4))
                throw new ArgumentException("The Mat must have the types 'CV_8UC4' (RGBA) , 'CV_8UC3' (RGB) or 'CV_8UC1' (GRAY).");

            //You can use SetPixels32 with the following texture formats:
            //https://docs.unity3d.com/2020.3/Documentation/ScriptReference/Texture2D.SetPixels32.html
#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (pixels32Buffer == null)
            {
                Color32[] pixels32 = texture2D.GetPixels32(mipLevel);

                if (pixels32.Length != mat.total())
                    throw new ArgumentException("The number of mat elements must match the Color32 array length of the specified mipLevel.");

                unsafe
                {
                    fixed (Color32* ptr = pixels32)
                    {
                        OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)ptr, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }

                ConvertToGrayscaleIfFormatMatches(pixels32, texture2D.format);

                texture2D.SetPixels32(pixels32, mipLevel);
            }
            else
            {
                if (pixels32Buffer.Length != mat.total())
                    throw new ArgumentException("The pixels32Buffer array length must match the number of mat elements.");

                unsafe
                {
                    fixed (Color32* ptr = pixels32Buffer)
                    {
                        OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)ptr, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }

                ConvertToGrayscaleIfFormatMatches(pixels32Buffer, texture2D.format);

                texture2D.SetPixels32(pixels32Buffer, mipLevel);
            }
            texture2D.Apply(updateMipmaps, makeNoLongerReadable);
#else
            GCHandle pixels32Handle;
            if (pixels32Buffer == null)
            {
                Color32[] pixels32 = texture2D.GetPixels32(mipLevel);

                if (pixels32.Length != mat.total())
                    throw new ArgumentException("The number of mat elements must match the Color32 array length of the specified mipLevel.");

                pixels32Handle = GCHandle.Alloc(pixels32, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, pixels32Handle.AddrOfPinnedObject(), (int)TextureFormat.RGBA32, flip, flipCode);

                    ConvertToGrayscaleIfFormatMatches(pixels32, texture2D.format);

                    texture2D.SetPixels32(pixels32, mipLevel);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
            else
            {
                if (pixels32Buffer.Length != mat.total())
                    throw new ArgumentException("The pixels32Buffer array length must match the number of mat elements.");

                pixels32Handle = GCHandle.Alloc(pixels32Buffer, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, pixels32Handle.AddrOfPinnedObject(), (int)TextureFormat.RGBA32, flip, flipCode);

                    ConvertToGrayscaleIfFormatMatches(pixels32Buffer, texture2D.format);

                    texture2D.SetPixels32(pixels32Buffer, mipLevel);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
            texture2D.Apply(updateMipmaps, makeNoLongerReadable);
#endif
        }

        private static void ConvertToGrayscaleIfFormatMatches(Color32[] pixels32, TextureFormat format)
        {
            if (format == TextureFormat.Alpha8 || format == TextureFormat.R8 || format == TextureFormat.R16 || format == TextureFormat.RFloat || format == TextureFormat.RHalf)
            {
                for (int i = 0; i < pixels32.Length; i++)
                {
                    ref Color32 color = ref pixels32[i];
                    float grayValue = 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
                    byte gray = (byte)(grayValue + 0.5f); // Round up to the nearest integer (based on OpenCV's cvRound)
                    color.r = gray;
                    color.g = gray;
                    color.b = gray;
                    color.a = gray;
                }
            }
        }

        /// <summary>
        /// Copies raw data from an OpenCV Mat to a Unity Texture2D.
        /// </summary>
        /// <remarks>
        /// This method copies raw data from an OpenCV Mat to a Unity Texture2D.
        /// There are no specific requirements for the size or type of the Mat and Texture2D; data is copied up to the maximum size that fits within the data size of the destination Texture2D, including mipmaps.
        /// A common use case for this method is writing a Mat with BGRA color order to a Texture2D in the BGRA32 format.
        /// If the "OPENCV_DONT_USE_UNSAFE_CODE" symbol is defined, the following operating conditions are added: mat.isContinuous() == true, and the data size of Texture2D is the same as or larger than the data size of Mat.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before copies.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after copies.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <returns>
        /// Returns the number of bytes actually written to the destination Texture2D.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static int MatToTexture2DRaw(Mat mat, Texture2D texture2D, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat.dims() != 2)
                throw new ArgumentException("mat.dims() != 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            int type = mat.type();
            TextureFormat format = texture2D.format;
            var rawTexData = texture2D.GetRawTextureData<byte>();

            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length >= matBytesNum &&
                ((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))))
            {
                unsafe
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(rawTexData), (int)texture2D.format, flip, flipCode);
                }
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);

                return matBytesNum;
            }
            else
            {
                int bytesNum = 0;

                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }
                unsafe
                {
                    bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, rawTexData.Length, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(rawTexData));
                }
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }

                return bytesNum;
            }
#else
            if (!mat.isContinuous())
            {
                Debug.LogError("mat.isContinuous() must be true.");
                return 0;
            }

            var rawTexData = texture2D.GetRawTextureData<byte>();
            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length > matBytesNum)
            {
                Debug.LogError("The size of the mat data must fill the entire texture according to its width, height and mipmapCount, and the data layout must match the texture format.");
                return 0;
            }

            if (flip)
            {
                Core.flip(mat, mat, flipCode);
            }
            texture2D.LoadRawTextureData((IntPtr)mat.dataAddr(), rawTexData.Length);
            texture2D.Apply(updateMipmaps, makeNoLongerReadable);
            if (flip)
            {
                Core.flip(mat, mat, flipCode);
            }

            return rawTexData.Length;
#endif
        }

#if !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Copies raw data from an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level.
        /// </summary>
        /// <remarks>
        /// This method copies raw data from an OpenCV Mat to a Unity Texture2D, targeting the specified mipmap level.
        /// There are no specific requirements for the size or type of the Mat and Texture2D; data is copied up to the maximum size that fits within the data size of the mipmap of the destination Texture2D.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat must be 2-dimensional.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D.
        /// </param>
        /// <param name="mipLevel">
        /// The mipmap level to which the Mat will be converted. The level must be within the range supported by the destination Texture2D.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the Mat is flipped before copies.
        /// The default is <c>true</c>, as the Mat must be flipped to align with the coordinate system of the destination Texture2D image.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Mat: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="updateMipmaps">
        /// If <c>true</c>, mipmaps are recalculated after copies.
        /// The default is <c>false</c>.
        /// </param>
        /// <param name="makeNoLongerReadable">
        /// If <c>true</c>, system memory copy of a texture is released.
        /// The default is <c>false</c>.
        /// </param>
        /// <returns>
        /// Returns the number of bytes actually written to the destination Texture2D.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static int MatToTexture2DRaw(Mat mat, Texture2D texture2D, int mipLevel, bool flip = true, int flipCode = 0, bool updateMipmaps = false, bool makeNoLongerReadable = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat.dims() != 2)
                throw new ArgumentException("mat.dims() != 2");

            int type = mat.type();
            TextureFormat format = texture2D.format;
            var rawTexData = texture2D.GetPixelData<byte>(mipLevel);

            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length >= matBytesNum &&
                ((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))))
            {
                unsafe
                {
                    OpenCVForUnity_MatToTexture(mat.nativeObj, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(rawTexData), (int)texture2D.format, flip, flipCode);
                }
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);

                return matBytesNum;
            }
            else
            {
                int bytesNum = 0;

                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }
                unsafe
                {
                    bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, rawTexData.Length, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(rawTexData));
                }
                texture2D.Apply(updateMipmaps, makeNoLongerReadable);
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }

                return bytesNum;
            }
        }
#endif

        #endregion

        #region Texture2DToMat

        /// <summary>
        /// Converts a Unity Texture2D to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method converts a Unity Texture2D to an OpenCV Mat. Conversion is possible even when the number of bytes per pixel differs, such as from Texture2D(RGBA32) to Mat(8UC1).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// Performance is optimal when the per-pixel data size and color order match, such as with Texture2D(RGBA32) and Mat(8UC4).
        /// If the texture format is not RGBA32, BGRA32, RGB24, Alpha8, or R8, the <a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a> methods are used.
        /// </remarks>
        /// <param name="texture2D">
        /// The source Texture2D must have the same size as the destination Mat.
        /// The source Texture2D supports the following formats. (<a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a>)
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="Texture2DToMatRaw">Texture2DToMatRaw</see>.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the Texture2D is flipped before conversion.
        /// The default is <c>true</c>, as the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Textrue2D image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="texture2D"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void Texture2DToMat(Texture2D texture2D, Mat mat, bool flip = true, int flipCode = 0)
        {
            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (mat.cols() != texture2D.width || mat.rows() != texture2D.height)
                throw new ArgumentException("The Mat must have the same size.");

            int type = mat.type();
            TextureFormat format = texture2D.format;
            if (!(type == CvType.CV_8UC1 || type == CvType.CV_8UC3 || type == CvType.CV_8UC4))
                throw new ArgumentException("The Mat must have the types 'CV_8UC4' (RGBA) , 'CV_8UC3' (RGB) or 'CV_8UC1' (GRAY).");

            if (format == TextureFormat.RGBA32 || format == TextureFormat.BGRA32 || format == TextureFormat.RGB24 || format == TextureFormat.Alpha8 || format == TextureFormat.R8)
            {
#if !OPENCV_DONT_USE_UNSAFE_CODE
                unsafe
                {
                    OpenCVForUnity_TextureToMat((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(texture2D.GetRawTextureData<byte>()), mat.nativeObj, (int)format, flip, flipCode);
                }
#else
                GCHandle RawTextureDataHandle = GCHandle.Alloc(texture2D.GetRawTextureData(), GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_TextureToMat(RawTextureDataHandle.AddrOfPinnedObject(), mat.nativeObj, (int)format, flip, flipCode);
                }
                finally
                {
                    if (RawTextureDataHandle.IsAllocated)
                        RawTextureDataHandle.Free();
                }
#endif

                return;
            }

            Color32[] pixels32 = texture2D.GetPixels32();

            ConvertToRGBAIfFormatMatches(pixels32, texture2D.format);

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (Color32* ptr = pixels32)
                {
                    OpenCVForUnity_TextureToMat((IntPtr)ptr, mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                }
            }
#else
            GCHandle pixels32Handle = GCHandle.Alloc(pixels32, GCHandleType.Pinned);
            try
            {
                OpenCVForUnity_TextureToMat(pixels32Handle.AddrOfPinnedObject(), mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
            }
            finally
            {
                if (pixels32Handle.IsAllocated)
                    pixels32Handle.Free();
            }
#endif
        }

        /// <summary>
        /// Converts a Unity Texture2D to an OpenCV Mat, targeting the specified mipmap level.
        /// </summary>
        /// <remarks>
        /// This method converts a Unity Texture2D to an OpenCV Mat, targeting the specified mipmap level. Conversion is possible even when the number of bytes per pixel differs, such as from Texture2D(RGBA32) to Mat(8UC1).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// </remarks>
        /// <param name="texture2D">
        /// The source Texture2D supports the following formats. (<a href="http://docs.unity3d.com/ScriptReference/Texture2D.GetPixels32.html">Texture2D.GetPixels32</a>)
        /// The source Texture2D most be supports mipmaps, as the conversion will target the<paramref name="mipLevel"/>.
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE). For other CvTypes or color orders, use <see cref="Texture2DToMatRaw">Texture2DToMatRaw</see>.
        /// The destination Mat must be the same size as the mipmap of the destination Texture2D.
        /// </param>
        /// <param name="mipLevel">
        /// The mipmap level to which the Mat will be converted. The level must be within the range supported by the source Texture2D.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the Texture2D is flipped before conversion.
        /// The default is <c>true</c>, as the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Textrue2D image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="texture2D"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void Texture2DToMat(Texture2D texture2D, Mat mat, int mipLevel, bool flip = true, int flipCode = 0)
        {
            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            int type = mat.type();
            TextureFormat format = texture2D.format;
            if (!(type == CvType.CV_8UC1 || type == CvType.CV_8UC3 || type == CvType.CV_8UC4))
                throw new ArgumentException("The Mat object must have the types 'CV_8UC4' (RGBA) , 'CV_8UC3' (RGB) or 'CV_8UC1' (GRAY).");

            Color32[] pixels32 = texture2D.GetPixels32(mipLevel);
            if (pixels32.Length != mat.total())
                throw new ArgumentException("The number of mat elements must match the Color32 array length of the specified mipLevel.");

            ConvertToRGBAIfFormatMatches(pixels32, texture2D.format);

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (Color32* ptr = pixels32)
                {
                    OpenCVForUnity_TextureToMat((IntPtr)ptr, mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                }
            }
#else
            GCHandle pixels32Handle = GCHandle.Alloc(pixels32, GCHandleType.Pinned);
            try
            {
                OpenCVForUnity_TextureToMat(pixels32Handle.AddrOfPinnedObject(), mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
            }
            finally
            {
                if (pixels32Handle.IsAllocated)
                    pixels32Handle.Free();
            }
#endif
        }

        private static void ConvertToRGBAIfFormatMatches(Color32[] pixels32, TextureFormat format)
        {
            if (format == TextureFormat.Alpha8)
            {
                for (int i = 0; i < pixels32.Length; i++)
                {
                    ref Color32 color = ref pixels32[i];
                    color.r = color.a;
                    color.g = color.a;
                    color.b = color.a;
                }
            }
            else if (format == TextureFormat.R8 || format == TextureFormat.R16 || format == TextureFormat.RFloat || format == TextureFormat.RHalf)
            {
                for (int i = 0; i < pixels32.Length; i++)
                {
                    ref Color32 color = ref pixels32[i];
                    color.g = color.r;
                    color.b = color.r;
                    color.a = color.r;
                }
            }
        }

        /// <summary>
        /// Copies raw data from a Unity Texture2D to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method copies raw data from a Unity Texture2D to an OpenCV Mat.
        /// There are no specific requirements for the size or type of the Texture2D and Mat; data is copied up to the maximum size that fits within the data size of the destination Mat.
        /// A common use case for this method is writing a Texture2D in the BGRA32 format to a Mat with BGRA color order.
        /// </remarks>
        /// <param name="texture2D">
        /// The source Texture2D.
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the Texture2D is flipped before copies.
        /// The default is <c>true</c>, as  the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Textrue2D image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <returns>
        /// Returns the number of bytes actually written to the destination Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="texture2D"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static int Texture2DToMatRaw(Texture2D texture2D, Mat mat, bool flip = true, int flipCode = 0)
        {
            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (mat.dims() != 2)
                throw new ArgumentException("mat.dims() != 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            int type = mat.type();
            TextureFormat format = texture2D.format;
            var rawTexData = texture2D.GetRawTextureData<byte>();

            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length >= matBytesNum &&
                ((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))))
            {
                unsafe
                {
                    OpenCVForUnity_TextureToMat((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(rawTexData), mat.nativeObj, (int)texture2D.format, flip, flipCode);
                }

                return matBytesNum;
            }
            else
            {
                int bytesNum = 0;

                unsafe
                {
                    bytesNum = OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(rawTexData), rawTexData.Length, mat.nativeObj);
                }
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }

                return bytesNum;
            }
#else
            int type = mat.type();
            TextureFormat format = texture2D.format;
            var rawTexData = texture2D.GetRawTextureData();

            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length >= matBytesNum &&
                ((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))))
            {
                GCHandle RawTextureDataHandle = GCHandle.Alloc(rawTexData, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_TextureToMat(RawTextureDataHandle.AddrOfPinnedObject(), mat.nativeObj, (int)texture2D.format, flip, flipCode);
                }
                finally
                {
                    if (RawTextureDataHandle.IsAllocated)
                        RawTextureDataHandle.Free();
                }

                return matBytesNum;
            }
            else
            {
                GCHandle RawTextureDataHandle = GCHandle.Alloc(rawTexData, GCHandleType.Pinned);
                try
                {
                    int bytesNum = OpenCVForUnity_ByteArrayToMatData(RawTextureDataHandle.AddrOfPinnedObject(), rawTexData.Length, mat.nativeObj);
                    if (flip)
                    {
                        Core.flip(mat, mat, flipCode);
                    }

                    return bytesNum;
                }
                finally
                {
                    if (RawTextureDataHandle.IsAllocated)
                        RawTextureDataHandle.Free();
                }
            }
#endif
        }

#if !OPENCV_DONT_USE_UNSAFE_CODE
        /// <summary>
        /// Copies raw data from a Unity Texture2D to an OpenCV Mat, targeting the specified mipmap level.
        /// </summary>
        /// <remarks>
        /// This method copies raw data from a Unity Texture2D to an OpenCV Mat, targeting the specified mipmap level.
        /// There are no specific requirements for the size or type of the Texture2D and Mat; data is copied up to the maximum size that fits within the data size of the mipmap of the destination Mat.
        /// </remarks>
        /// <param name="texture2D">
        /// The source Texture2D.
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional.
        /// </param>
        /// <param name="mipLevel">
        /// The mipmap level to which the Mat will be converted. The level must be within the range supported by the source Texture2D.
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the Texture2D is flipped before copies.
        /// The default is <c>true</c>, as  the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Textrue2D image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <returns>
        /// Returns the number of bytes actually written to the destination Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="texture2D"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static int Texture2DToMatRaw(Texture2D texture2D, Mat mat, int mipLevel, bool flip = true, int flipCode = 0)
        {
            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (mat.dims() != 2)
                throw new ArgumentException("mat.dims() != 2");

            int type = mat.type();
            TextureFormat format = texture2D.format;
            var rawTexData = texture2D.GetPixelData<byte>(mipLevel);

            int matBytesNum = (int)(mat.total() * mat.elemSize());

            if (rawTexData.Length >= matBytesNum &&
                ((type == CvType.CV_8UC4 && format == TextureFormat.RGBA32) || (type == CvType.CV_8UC3 && format == TextureFormat.RGB24) || (type == CvType.CV_8UC1 && (format == TextureFormat.Alpha8 || format == TextureFormat.R8))))
            {
                unsafe
                {
                    OpenCVForUnity_TextureToMat((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(rawTexData), mat.nativeObj, (int)texture2D.format, flip, flipCode);
                }

                return matBytesNum;
            }
            else
            {
                int bytesNum = 0;

                unsafe
                {
                    bytesNum = OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(rawTexData), rawTexData.Length, mat.nativeObj);
                }
                if (flip)
                {
                    Core.flip(mat, mat, flipCode);
                }

                return bytesNum;
            }
        }
#endif

        #endregion

        #region WebCamTextureToMat

#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API

        /// <summary>
        /// Converts a Unity WebCamTexture to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method converts a Unity WebCamTexture image to an OpenCV Mat. Conversion is possible even when the number of bytes per pixel differs, such as from WebCamTexture(RGBA32) to Mat(8UC1).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// Performance is optimal when the per-pixel data size and color order match, such as with Texture2D(RGBA32) and Mat(8UC4).
        /// It is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="webCamTexture">
        /// The source WebCamTexture must have the same size as the destination Mat.
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE).
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the WebCamTexture is flipped before conversion.
        /// The default is <c>true</c>, as the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the WebCamTexture image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode > 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode < 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="webCamTexture"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WebCamTextureToMat(WebCamTexture webCamTexture, Mat mat, bool flip = true, int flipCode = 0)
        {
            WebCamTextureToMat(webCamTexture, mat, null, flip, flipCode);
        }

        /// <summary>
        /// Converts a Unity WebCamTexture to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method converts a Unity WebCamTexture image to an OpenCV Mat. Conversion is possible even when the number of bytes per pixel differs, such as from WebCamTexture(RGBA32) to Mat(8UC1).
        /// In the case of multi-channel color to 1-channel, it is converted to grayscale.
        /// Performance is optimal when the per-pixel data size and color order match, such as with Texture2D(RGBA32) and Mat(8UC4).
        /// It is recommended to use the <paramref name="pixels32Buffer"/> argument to avoid repeated memory allocations.
        /// </remarks>
        /// <param name="webCamTexture">
        /// The source WebCamTexture must have the same size as the destination Mat.
        /// </param>
        /// <param name="mat">
        /// The destination Mat must be 2-dimensional, with a CvType of 'CV_8UC4' (RGBA), 'CV_8UC3' (RGB), or 'CV_8UC1' (GRAYSCALE).
        /// </param>
        /// <param name="pixels32Buffer">
        /// An optional array for receiving pixel data as Color32. Using this array helps avoid memory allocation each frame.
        /// Ensure the array is initialized to a length matching the  texture’s width * height. (<a href="http://docs.unity3d.com/ScriptReference/WebCamTexture.GetPixels32.html">WebCamTexture.GetPixels32</a>)
        /// </param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the WebCamTexture is flipped before conversion.
        /// The default is <c>true</c>, as the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the WebCamTexture image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode > 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode < 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="webCamTexture"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void WebCamTextureToMat(WebCamTexture webCamTexture, Mat mat, Color32[] pixels32Buffer, bool flip = true, int flipCode = 0)
        {
            if (webCamTexture == null)
                throw new ArgumentNullException(nameof(webCamTexture));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (mat.cols() != webCamTexture.width || mat.rows() != webCamTexture.height)
                throw new ArgumentException("The Mat must have the same size.");

            int type = mat.type();
            if (!(type == CvType.CV_8UC1 || type == CvType.CV_8UC3 || type == CvType.CV_8UC4))
                throw new ArgumentException("The Mat must have the types 'CV_8UC4' (RGBA) , 'CV_8UC3' (RGB) or 'CV_8UC1' (GRAY).");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            if (pixels32Buffer == null)
            {
                unsafe
                {
                    fixed (Color32* ptr = webCamTexture.GetPixels32())
                    {
                        OpenCVForUnity_TextureToMat((IntPtr)ptr, mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }
            }
            else
            {
                webCamTexture.GetPixels32(pixels32Buffer);
                unsafe
                {
                    fixed (Color32* ptr = pixels32Buffer)
                    {
                        OpenCVForUnity_TextureToMat((IntPtr)ptr, mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                    }
                }
            }
#else
            GCHandle pixels32Handle;
            if (pixels32Buffer == null)
            {

                Color32[] colors = webCamTexture.GetPixels32();

                pixels32Handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_TextureToMat(pixels32Handle.AddrOfPinnedObject(), mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
            else
            {
                webCamTexture.GetPixels32(pixels32Buffer);

                pixels32Handle = GCHandle.Alloc(pixels32Buffer, GCHandleType.Pinned);
                try
                {
                    OpenCVForUnity_TextureToMat(pixels32Handle.AddrOfPinnedObject(), mat.nativeObj, (int)TextureFormat.RGBA32, flip, flipCode);
                }
                finally
                {
                    if (pixels32Handle.IsAllocated)
                        pixels32Handle.Free();
                }
            }
#endif
        }

#endif //!OPENCV_DONT_USE_WEBCAMTEXTURE_API

        #endregion

        #region TextureToTexture2D

        /// <summary>
        /// Converts a Texture to a Texture2D.
        /// </summary>
        /// <remarks>
        /// This method converts a Texture to a Texture2D.
        /// The Texture and the Texture2D must be the same size.
        /// </remarks>
        /// <param name="texture">
        /// The source Texture.
        /// </param>
        /// <param name="texture2D">
        /// The destination Texture2D. It must have a TextureFormat of RGBA32, ARGB32, RGB24, RGBAFloat, or RGBAHalf.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="texture"/> or <paramref name="texture2D"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void TextureToTexture2D(Texture texture, Texture2D texture2D)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (texture2D == null)
                throw new ArgumentNullException(nameof(texture2D));

            if (texture.width != texture2D.width || texture.height != texture2D.height)
                throw new ArgumentException("The Texture and the Texture2D must be the same size.");

            RenderTexture prevRT = RenderTexture.active;

            if (texture is RenderTexture)
            {
                RenderTexture.active = (RenderTexture)texture;
                texture2D.ReadPixels(new UnityEngine.Rect(0f, 0f, texture.width, texture.height), 0, 0, false);
                texture2D.Apply(false, false);
            }
            else
            {
                RenderTexture tempRT = RenderTexture.GetTemporary(texture.width, texture.height, 0, (texture.isDataSRGB) ? GraphicsFormat.B8G8R8A8_SRGB : GraphicsFormat.B8G8R8A8_UNorm);
                Graphics.Blit(texture, tempRT);

                RenderTexture.active = tempRT;
                texture2D.ReadPixels(new UnityEngine.Rect(0f, 0f, texture.width, texture.height), 0, 0, false);
                texture2D.Apply(false, false);
                RenderTexture.ReleaseTemporary(tempRT);
            }

            RenderTexture.active = prevRT;
        }

        #endregion

        #region TextureToMatAsync

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Converts a Texture to an OpenCV Mat asynchronously.
        /// </summary>
        /// <param name="texture">The source Texture to be converted.</param>
        /// <param name="mat">The destination Mat that will store the converted texture data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="texture"/> or <paramref name="mat"/> is null.</exception>
        /// <exception cref="NotSupportedException">Thrown when the graphics format of the texture is not supported for reading using AsyncGPUReadback.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via the <paramref name="cancellationToken"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when AsyncGPUReadback completes with an error.</exception>
        public static async Awaitable TextureToMatAsync(Texture texture, Mat mat, CancellationToken cancellationToken = default)
        {
            await TextureToMatAsync(texture, mat, true, 0, 0, 0, -1, 0, -1, 0, 1, null, cancellationToken);

            return;
        }

        /// <summary>
        /// Converts a Texture to an OpenCV Mat asynchronously.
        /// </summary>
        /// <param name="texture">The source Texture to be converted.</param>
        /// <param name="mat">The destination Mat that will store the converted texture data.</param>
        /// <param name="flip">
        /// If <c>true</c>, the pixel data retrieved from the Texture2D is flipped before copies.
        /// The default is <c>true</c>, as the pixel data must be flipped to align with the coordinate system of the destination Mat.
        /// </param>
        /// <param name="flipCode">
        /// Specifies how to flip the Textrue2D image: Vertical flipping of the image (flipCode == 0) to flip around the x-axis,
        /// horizontal flipping of the image (flipCode &gt; 0, e.g., 1) to flip around the y-axis, and simultaneous horizontal and vertical flipping (flipCode &lt; 0, e.g., -1) to flip around both axes.
        /// The default is <c>0</c>.
        /// </param>
        /// <param name="mipIndex">Index of the mipmap to be fetched.</param>
        /// <param name="x">Starting X coordinate in pixels of the Texture data to be fetched.</param>
        /// <param name="width">Width in pixels of the Texture data to be fetched.</param>
        /// <param name="y">Starting Y coordinate in pixels of the Texture data to be fetched.</param>
        /// <param name="height">Height in pixels of the Texture data to be fetched.</param>
        /// <param name="z">Depth in pixels for Texture3D being fetched. Number of layers for TextureCube, TextureArray and TextureCubeArray.</param>
        /// <param name="depth">Depth in pixels for Texture3D being fetched. Number of layers for TextureCube, TextureArray and TextureCubeArray.</param>
        /// <param name="dstFormat">Target GraphicsFormat of the data. If the target format is different from the format stored on the GPU, the conversion is automatic.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="texture"/> or <paramref name="mat"/> is null.</exception>
        /// <exception cref="NotSupportedException">Thrown when the graphics format of the texture is not supported for reading using AsyncGPUReadback.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via the <paramref name="cancellationToken"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when AsyncGPUReadback completes with an error.</exception>
        public static async Awaitable TextureToMatAsync(Texture texture, Mat mat, bool flip = true, int flipCode = 0, int mipIndex = 0, int x = 0, int width = -1, int y = 0, int height = -1, int z = 0, int depth = 1, GraphicsFormat? dstFormat = null, CancellationToken cancellationToken = default)
        {
            // Validate arguments
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            // Check if the texture format supports AsyncGPUReadback
            if (!SystemInfo.IsFormatSupported(texture.graphicsFormat, GraphicsFormatUsage.ReadPixels))
                throw new NotSupportedException($"The graphics format '{texture.graphicsFormat}' is not supported for AsyncGPUReadback.RequestAsync method.");


            cancellationToken.ThrowIfCancellationRequested();

            // Set default width and height if unspecified
            width = (width == -1) ? mat.width() : width;
            height = (height == -1) ? mat.height() : height;

            // Use provided format or default to texture's current format
            GraphicsFormat formatToUse = dstFormat ?? texture.graphicsFormat;


            cancellationToken.ThrowIfCancellationRequested();

            // Perform asynchronous GPU readback
            AsyncGPUReadbackRequest asyncGPUReadbackRequest = await AsyncGPUReadback.RequestAsync(
                texture, mipIndex, x, width, y, height, z, depth, formatToUse);


            cancellationToken.ThrowIfCancellationRequested();

            // Handle GPU readback error
            if (asyncGPUReadbackRequest.hasError)
                throw new InvalidOperationException("AsyncGPUReadback.RequestAsync failed: GPU readback encountered an error.");

            // Get the data as a NativeArray
            NativeArray<byte> textureDataNativeArray = asyncGPUReadbackRequest.GetData<byte>();

            // Calculate the number of bytes that the destination Mat can hold
            long matByteSize = mat.total() * mat.elemSize();

            // Ensure the destination Mat has enough space for the data
            if (textureDataNativeArray.Length > matByteSize)
            {
                Debug.LogWarning($"[TextureToMatAsync] Mat is too small. Texture data size is {textureDataNativeArray.Length} bytes but Mat can only hold {matByteSize} bytes.");
                return;
            }

            // Copy the data from GPU to Mat
            if (mat.isContinuous() || mat.dims() <= 2)
            {
                OpenCVMatUtils.CopyToMat<byte>(textureDataNativeArray, mat);
            }
            else
            {
#if NET_STANDARD_2_1
                mat.put<byte>(0, 0, textureDataNativeArray);
#else
                // Fallback for environments without NativeArray overload
                byte[] data = new byte[textureDataNativeArray.Length];
                textureDataNativeArray.CopyTo(data);
                mat.put<byte>(0, 0, data);
#endif
            }

            // Flip the image if requested
            if (flip)
                Core.flip(mat, mat, flipCode);
        }

#endif

        #endregion


        #region CopyFromMat_IntPtr

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// The entire range of data managed by the Mat will be copied to the IntPtr.
        /// If you want to specify the exact range of data to copy, please use the method that accepts a <paramref name="length"/> argument.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat(Mat mat, IntPtr intPtr)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            int length = (int)(mat.total() * mat.elemSize());

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size of the memory region pointed to by the IntPtr is smaller than the data size managed by the Mat, data will be copied up to the size of the IntPtr.
        /// If the data size of the memory region pointed to by the IntPtr is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="length">
        /// The number of elements, in terms of byte size, to copy from the source Mat to the destination IntPtr.
        /// The <paramref name="length"/> represents the size in bytes, regardless of the actual type of data in the Mat or the destination.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>

        public static int CopyFromMat(Mat mat, IntPtr intPtr, int length)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to an IntPtr.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into an IntPtr. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size of the memory region pointed to by the IntPtr is smaller than the data size managed by the Mat, data will be copied up to the size of the IntPtr.
        /// If the data size of the memory region pointed to by the IntPtr is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements represented by the IntPtr. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="intPtr">
        /// The destination IntPtr where the data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination IntPtr.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the IntPtr.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, IntPtr intPtr, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;
            int sizeof_T = default;
#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                sizeof_T = sizeof(T);
            }
#else
            sizeof_T =  Marshal.SizeOf<T>();
#endif
            length = length * sizeof_T;

            return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length, intPtr);
        }

        #endregion

        #region CopyToMat_IntPtr

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// Data of the same size in bytes as the data managed by the Mat is copied from the IntPtr to the Mat.
        /// If you want to specify the exact range of data to copy, please use the method that accepts a <paramref name="length"/> argument.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>

        public static int CopyToMat(IntPtr intPtr, Mat mat)
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            int length = (int)(mat.total() * mat.elemSize());

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size of the memory region pointed to by the IntPtr, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size of the memory region pointed to by the IntPtr.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements, in terms of byte size, to copy from the source IntPtr to the destination Mat.
        /// The <paramref name="length"/> represents the size in bytes, regardless of the actual type of data in the IntPtr or the destination.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat(IntPtr intPtr, Mat mat, int length)
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        /// <summary>
        /// Copies the data from an IntPtr to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an IntPtr to an OpenCV Mat. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size of the memory region pointed to by the IntPtr, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size of the memory region pointed to by the IntPtr.
        ///
        /// <strong>Note:</strong> This method operates on unmanaged memory, and incorrect usage can lead to unpredictable behavior,
        /// crashes, or data corruption. Ensure that the source pointer points to a valid memory region  and that the specified length does not exceed the allocated memory size for that region.
        /// Additionally, as this method involves unmanaged memory, be cautious about potential security risks.
        /// Accessing memory beyond the intended range can lead to buffer overflows and may compromise application stability and security.
        /// It is highly recommended to perform validation checks on the pointer and the specified length.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements represented by the IntPtr. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="intPtr">
        /// The source IntPtr from which data will be copied. This pointer must reference a valid memory region.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source IntPtr to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the IntPtr to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>intPtr == IntPtr.Zero</c>.
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(IntPtr intPtr, Mat mat, int length) where T : unmanaged
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : length;
            int sizeof_T = default;
#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                sizeof_T = sizeof(T);
            }
#else
            sizeof_T =  Marshal.SizeOf<T>();
#endif
            length = length * sizeof_T;

            return OpenCVForUnity_ByteArrayToMatData(intPtr, length, mat.nativeObj);
        }

        #endregion


        #region CopyFromMat_Array

        /// <summary>
        /// Copies the data from an OpenCV Mat to a managed array.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a managed array. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination managed array where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the managed array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, T[] array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                int bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * Marshal.SizeOf<T>(), arrayHandle.AddrOfPinnedObject());
                return bytesNum;
            }
            finally
            {
                if (arrayHandle.IsAllocated)
                    arrayHandle.Free();
            }
#endif
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a managed array.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a managed array. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination managed array where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination managed array.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the managed array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, T[] array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)ptr);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                int bytesNum = OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * Marshal.SizeOf<T>(), arrayHandle.AddrOfPinnedObject());
                return bytesNum;
            }
            finally
            {
                if (arrayHandle.IsAllocated)
                    arrayHandle.Free();
            }
#endif
        }

        #endregion

        #region CopyToMat_Array

        /// <summary>
        /// Copies the data from a managed array to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a managed array into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source managed array from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the managed array to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(T[] array, Mat mat) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, array.Length * sizeof(T), mat.nativeObj);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                int bytesNum = OpenCVForUnity_ByteArrayToMatData(arrayHandle.AddrOfPinnedObject(), array.Length * Marshal.SizeOf<T>(), mat.nativeObj);
                return bytesNum;
            }
            finally
            {
                if (arrayHandle.IsAllocated)
                    arrayHandle.Free();
            }
#endif
        }

        /// <summary>
        /// Copies the data from a managed array to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a managed array into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the managed array. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source managed array from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source managed array to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the managed array to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(T[] array, Mat mat, int length) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, length * sizeof(T), mat.nativeObj);
                }
            }
#else
            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                int bytesNum = OpenCVForUnity_ByteArrayToMatData(arrayHandle.AddrOfPinnedObject(), length * Marshal.SizeOf<T>(), mat.nativeObj);
                return bytesNum;
            }
            finally
            {
                if (arrayHandle.IsAllocated)
                    arrayHandle.Free();
            }
#endif
        }

        #endregion


#if !OPENCV_DONT_USE_UNSAFE_CODE

        #region CopyFromMat_NativeArray

        /// <summary>
        /// Copies the data from an OpenCV Mat to a NativeArray.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a NativeArray. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination NativeArray where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the NativeArray.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, NativeArray<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(array));
            }
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a NativeArray.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a NativeArray. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination NativeArray where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination managed array.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the NativeArray.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, NativeArray<T> array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(array));
            }
        }

        #endregion

        #region CopyToMat_NativeArray

        /// <summary>
        /// Copies the data from a NativeArray to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a NativeArray into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source NativeArray from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the NativeArray to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(NativeArray<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                return OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length * sizeof(T), mat.nativeObj);
            }
        }

        /// <summary>
        /// Copies the data from a NativeArray to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a NativeArray into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the NativeArray. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source NativeArray from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source NativeArray to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the NativeArray to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(NativeArray<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                return OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), length * sizeof(T), mat.nativeObj);
            }
        }

        #endregion

#endif


#if NET_STANDARD_2_1 && !OPENCV_DONT_USE_UNSAFE_CODE

        #region CopyFromMat_Span

        /// <summary>
        /// Copies the data from an OpenCV Mat to a Span.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a Span. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination Span where the data will be copied.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the Span.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, Span<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, array.Length * sizeof(T), (IntPtr)ptr);
                }
            }
        }

        /// <summary>
        /// Copies the data from an OpenCV Mat to a Span.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in an OpenCV Mat object into a Span. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data from Mats with any number of dimensions.
        ///
        /// If the data size managed by the array is smaller than the data size managed by the Mat, data will be copied up to the size of the array.
        /// If the data size managed by the array is larger, data will be copied up to the size managed by the Mat.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the Mat and the array.
        /// Although its functionality is more limited compared to the <c>Mat.get</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="mat">
        /// The source Mat from which data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="array">
        /// The destination Span where the data will be copied.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Mat to the destination Span.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Mat to the Span.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mat"/> or <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyFromMat<T>(Mat mat, Span<T> array, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_MatDataToByteArray(mat.nativeObj, length * sizeof(T), (IntPtr)ptr);
                }
            }
        }

        #endregion

        #region CopyToMat_Span

        /// <summary>
        /// Copies the data from a Span to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a Span into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source Span from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Span to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(ReadOnlySpan<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, array.Length * sizeof(T), mat.nativeObj);
                }
            }
        }

        /// <summary>
        /// Copies the data from a Span to an OpenCV Mat.
        /// </summary>
        /// <remarks>
        /// This method transfers the data stored in a Span into an OpenCV Mat object. It supports both continuous and non-continuous Mat objects:
        /// - If <c>mat.isContinuous()</c> is <c>false</c> and <c>mat.dims()</c> exceeds 2, the method will throw an exception, as non-continuous Mats with more than 2 dimensions are unsupported.
        /// - If <c>mat.isContinuous()</c> is <c>true</c>, the method can copy data where Mats with any number of dimensions.
        ///
        /// If the data size managed by the Mat is smaller than the data size managed by the array, data will be copied up to the size of the mat.
        /// If the data size managed by the Mat is larger, data will be copied up to the size managed by the array.
        ///
        /// <strong>Note:</strong> This method is specialized for copying a continuous range of data regardless of differences in types between the array and the Mat.
        /// Although its functionality is more limited compared to the <c>Mat.put</c> method, it offers improved performance for such operations.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the elements in the Span. This must be an unmanaged type (i.e., a type without references to managed objects) to allow for direct memory access.
        /// </typeparam>
        /// <param name="array">
        /// The source Span from which data will be copied.
        /// </param>
        /// <param name="mat">
        /// The destination Mat where the data will be copied. The Mat must not be disposed of or null.
        /// </param>
        /// <param name="length">
        /// The number of elements of type <typeparamref name="T"/> to copy from the source Span to the destination Mat.
        /// </param>
        /// <returns>
        /// The number of bytes copied from the Span to the Mat.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <c>!mat.isContinuous() &amp;&amp; mat.dims() > 2</c>.
        /// </exception>
        public static int CopyToMat<T>(ReadOnlySpan<T> array, Mat mat, int length) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (!mat.isContinuous() && mat.dims() > 2)
                throw new ArgumentException("!mat.isContinuous() && mat.dims() > 2");

            length = (length < 0) ? 0 : (length > array.Length) ? array.Length : length;

            unsafe
            {
                fixed (T* ptr = array)
                {
                    return OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, length * sizeof(T), mat.nativeObj);
                }
            }
        }

        #endregion

#endif


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_MatToTexture(IntPtr mat, IntPtr textureColors, int textureFormat, [MarshalAs(UnmanagedType.U1)] bool flip, int flipCode);

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_TextureToMat(IntPtr textureColors, IntPtr Mat, int textureFormat, [MarshalAs(UnmanagedType.U1)] bool flip, int flipCode);

        [DllImport(LIBNAME)]
        private static extern int OpenCVForUnity_MatDataToByteArray(IntPtr mat, int length, IntPtr byteArray);

        [DllImport(LIBNAME)]
        private static extern int OpenCVForUnity_ByteArrayToMatData(IntPtr byteArray, int length, IntPtr Mat);
    }
}
