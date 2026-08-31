using System;
using System.Buffers;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_6000_0_OR_NEWER
using System.Threading;
#endif

#if OPENCV_DONT_USE_UNSAFE_CODE
using System.Runtime.InteropServices;
#endif

namespace OpenCVForUnity.UnityUtils
{
    public static partial class Utils
    {

        #region Public Methods

        /// <summary>
        /// Enumeration to specify the data copy method.
        /// </summary>
        public enum CopyMode
        {
            /// <summary>
            /// Copies data on a per-pixel basis.
            /// If the input is 4 bytes per pixel and the output is 8 bytes per pixel, 4 bytes from the input are copied, and the remaining 4 bytes are filled with zeros.
            /// If the input is 8 bytes per pixel and the output is 4 bytes per pixel, only the first 4 bytes from the input are copied.
            /// </summary>
            PerPixel,

            /// <summary>
            /// Copies data as a contiguous block of memory.
            /// </summary>
            Contiguous
        }

        /// <summary>
        /// Copies data from a OpenCv Mat to a Unity GraphicsBuffer.
        /// </summary>
        /// <param name="mat">The source OpenCV Mat.</param>
        /// <param name="graphicsBuffer">The destination Unity GraphicsBuffer.</param>
        /// <param name="copyMode">The CopyMode enumeration specifying the method of copying data.</param>
        /// <exception cref="ArgumentNullException">Thrown if mat or graphicsBuffer is null.</exception>
        /// <exception cref="ArgumentException">Thrown if data size or stride does not match.</exception>
        public static void matToGraphicsBuffer(Mat mat, GraphicsBuffer graphicsBuffer, CopyMode copyMode)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (graphicsBuffer == null)
                throw new ArgumentNullException(nameof(graphicsBuffer));

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();
            int graphicsBufferCount = graphicsBuffer.count;
            int graphicsBufferStride = graphicsBuffer.stride;

            if (copyMode == CopyMode.Contiguous)
            {
                if (matCount * matStride > graphicsBufferCount * graphicsBufferStride)
                    throw new ArgumentException("The data size of the graphicsBuffer must be greater than or equal to the data size of the mat.");
            }
            else
            {
                if (matCount != graphicsBufferCount)
                    throw new ArgumentException("mat.total() and graphicsBuffer.count are not equal.");
                if (graphicsBufferStride % (int)mat.elemSize1() != 0)
                    throw new ArgumentException("graphicsBuffer stride is not a multiple of the mat.elemSize1().");
            }

            CopyFromMatToGraphicsBuffer(mat, graphicsBuffer, copyMode, matCount, matStride, graphicsBufferCount, graphicsBufferStride);
        }

#if UNITY_6000_0_OR_NEWER

        /// <summary>
        /// Copies data from a OpenCv Mat to a Unity GraphicsBuffer asynchronously.
        /// </summary>
        /// <param name="mat">The source OpenCV Mat.</param>
        /// <param name="graphicsBuffer">The destination Unity GraphicsBuffer.</param>
        /// <param name="copyMode">The CopyMode enumeration specifying the method of copying data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if mat or graphicsBuffer is null.</exception>
        /// <exception cref="ArgumentException">Thrown if data size or stride does not match.</exception>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via the <paramref name="cancellationToken"/>.</exception>
        public static async Awaitable<AsyncGPUReadbackRequest> matToGraphicsBufferAsync(Mat mat, GraphicsBuffer graphicsBuffer, CopyMode copyMode, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            matToGraphicsBuffer(mat, graphicsBuffer, copyMode);

            cancellationToken.ThrowIfCancellationRequested();

            AsyncGPUReadbackRequest asyncGPUReadbackRequest = await AsyncGPUReadback.RequestAsync(graphicsBuffer);

            cancellationToken.ThrowIfCancellationRequested();

            return asyncGPUReadbackRequest;
        }

#endif

        /// <summary>
        /// Copies data from a Unity GraphicsBuffer to a OpenCV Mat.
        /// </summary>
        /// <param name="graphicsBuffer">The source Unity GraphicsBuffer.</param>
        /// <param name="mat">The destination OpenCV Mat.</param>
        /// <param name="copyMode">The CopyMode enumeration specifying the method of copying data.</param>
        /// <exception cref="ArgumentNullException">Thrown if mat or graphicsBuffer is null.</exception>
        /// <exception cref="ArgumentException">Thrown if data size or stride does not match.</exception>
        public static void graphicsBufferToMat(GraphicsBuffer graphicsBuffer, Mat mat, CopyMode copyMode)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (mat != null)
                mat.ThrowIfDisposed();

            if (graphicsBuffer == null)
                throw new ArgumentNullException(nameof(graphicsBuffer));

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();
            int graphicsBufferCount = graphicsBuffer.count;
            int graphicsBufferStride = graphicsBuffer.stride;

            if (copyMode == CopyMode.Contiguous)
            {
                if (graphicsBufferCount * graphicsBufferStride > matCount * matStride)
                    throw new ArgumentException("The data size of the graphicsBuffer must be greater than or equal to the data size of the mat.");
            }
            else
            {
                if (graphicsBufferCount != matCount)
                    throw new ArgumentException("graphicsBuffer.count and mat.total() are not equal.");
                if (graphicsBufferStride % (int)mat.elemSize1() != 0)
                    throw new ArgumentException("graphicsBuffer stride is not a multiple of the mat.elemSize1().");
            }

            CopyFromGraphicsBufferToMat(graphicsBuffer, mat, copyMode, graphicsBufferCount, graphicsBufferStride, matCount, matStride);
        }

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Copies data from a Unity GraphicsBuffer to an OpenCV Mat asynchronously.
        /// </summary>
        /// <param name="graphicsBuffer">The source Unity GraphicsBuffer.</param>
        /// <param name="mat">The destination OpenCV Mat.</param>
        /// <param name="copyMode">The CopyMode enumeration specifying the method of copying data.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="mat"/> or <paramref name="graphicsBuffer"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if data size or stride does not match.</exception>
        /// <exception cref="NotSupportedException">Thrown if compute shaders are not supported on the current platform.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public static async Awaitable graphicsBufferToMatAsync(GraphicsBuffer graphicsBuffer, Mat mat, CopyMode copyMode, CancellationToken cancellationToken = default)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (graphicsBuffer == null)
                throw new ArgumentNullException(nameof(graphicsBuffer));


            cancellationToken.ThrowIfCancellationRequested();

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();
            int graphicsBufferCount = graphicsBuffer.count;
            int graphicsBufferStride = graphicsBuffer.stride;

            if (copyMode == CopyMode.Contiguous)
            {
                if (graphicsBufferCount * graphicsBufferStride > matCount * matStride)
                    throw new ArgumentException("The data size of the graphicsBuffer must be greater than or equal to the data size of the mat.");
            }
            else
            {
                if (graphicsBufferCount != matCount)
                    throw new ArgumentException("graphicsBuffer.count and mat.total() are not equal.");
                if (graphicsBufferStride % (int)mat.elemSize1() != 0)
                    throw new ArgumentException("graphicsBuffer stride is not a multiple of the mat.elemSize1().");
            }

            await CopyFromGraphicsBufferToMatAsync(graphicsBuffer, mat, copyMode, graphicsBufferCount, graphicsBufferStride, matCount, matStride, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
        }

#endif

        /// <summary>
        /// Converts a Unity RenderTexture to an OpenCV Mat.
        /// </summary>
        /// <param name="renderTexture">The source Unity RenderTexture. Must have <c>enableRandomWrite</c> set to true.</param>
        /// <param name="mat">The destination OpenCV Mat. The depth must be 'CV_8U'. The Mat object must not have more than 4 channels.</param>
        /// <param name="graphicsBuffer">A temporary GraphicsBuffer. If null, a new one will be created. The stride of the graphics buffer must be exactly 4 bytes.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="renderTexture"/> or <paramref name="mat"/> is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="renderTexture"/> has <c>enableRandomWrite</c> set to false, or if the size or format of the RenderTexture is invalid.
        /// </exception>
        public static void RenderTextureToMat(RenderTexture renderTexture, Mat mat, GraphicsBuffer graphicsBuffer, bool flip = true, int flipCode = 0)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (!renderTexture.enableRandomWrite)
                throw new ArgumentException("renderTexture must have enableRandomWrite set to true.", nameof(renderTexture));

            if (mat.depth() != CvType.CV_8U)
                throw new ArgumentException("The Mat object must have a depth of 'CV_8U'.");

            if (mat.channels() > 4)
                throw new ArgumentException("The Mat object must not have more than 4 channels.");

            if ((renderTexture.width & 0x7) != 0)
                throw new ArgumentException("renderTexture.width must be a multiple of 8.");
            if ((renderTexture.height & 0x7) != 0)
                throw new ArgumentException("renderTexture.height must be a multiple of 8.");

            if (mat.cols() != renderTexture.width || mat.rows() != renderTexture.height)
                throw new ArgumentException("The Mat object must have the same size.");

            if (graphicsBuffer != null && graphicsBuffer.stride != 4)
                throw new ArgumentException("The stride of the graphics buffer must be exactly 4 bytes.");

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();

            if (graphicsBuffer == null)
            {
                GraphicsBuffer newGraphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, matCount, 4);

                int graphicsBufferCount = newGraphicsBuffer.count;
                int graphicsBufferStride = newGraphicsBuffer.stride;

                DispatchRenderTextureToMatComputeShader(renderTexture, newGraphicsBuffer, flip, flipCode);
                CopyFromGraphicsBufferToMat(newGraphicsBuffer, mat, CopyMode.PerPixel, graphicsBufferCount, graphicsBufferStride, matCount, matStride);

                newGraphicsBuffer.Dispose();
            }
            else
            {
                int graphicsBufferCount = graphicsBuffer.count;
                int graphicsBufferStride = graphicsBuffer.stride;

                DispatchRenderTextureToMatComputeShader(renderTexture, graphicsBuffer, flip, flipCode);
                CopyFromGraphicsBufferToMat(graphicsBuffer, mat, CopyMode.PerPixel, graphicsBufferCount, graphicsBufferStride, matCount, matStride);
            }
        }

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Converts a Unity RenderTexture to an OpenCV Mat asynchronously.
        /// </summary>
        /// <param name="renderTexture">The source Unity RenderTexture. Must have <c>enableRandomWrite</c> set to true.</param>
        /// <param name="mat">The destination OpenCV Mat. The depth must be 'CV_8U'. The Mat object must not have more than 4 channels.</param>
        /// <param name="graphicsBuffer">A temporary GraphicsBuffer. If null, a new one will be created. The stride of the graphics buffer must be exactly 4 bytes.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="renderTexture"/> or <paramref name="mat"/> is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="renderTexture"/> has <c>enableRandomWrite</c> set to false, or if the size or format of the RenderTexture is invalid.
        /// </exception>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public static async Awaitable renderTextureToMatAsync(RenderTexture renderTexture, Mat mat, GraphicsBuffer graphicsBuffer, bool flip = true, int flipCode = 0, CancellationToken cancellationToken = default)
        {

            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (!renderTexture.enableRandomWrite)
                throw new ArgumentException("renderTexture must have enableRandomWrite set to true.", nameof(renderTexture));

            if (mat.depth() != CvType.CV_8U)
                throw new ArgumentException("The Mat object must have a depth of 'CV_8U'.");

            if (mat.channels() > 4)
                throw new ArgumentException("The Mat object must not have more than 4 channels.");

            if ((renderTexture.width & 0x7) != 0)
                throw new ArgumentException("renderTexture.width must be a multiple of 8.");
            if ((renderTexture.height & 0x7) != 0)
                throw new ArgumentException("renderTexture.height must be a multiple of 8.");

            if (mat.cols() != renderTexture.width || mat.rows() != renderTexture.height)
                throw new ArgumentException("The Mat object must have the same size.");

            if (graphicsBuffer != null && graphicsBuffer.stride != 4)
                throw new ArgumentException("The stride of the graphics buffer must be exactly 4 bytes.");

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();


            cancellationToken.ThrowIfCancellationRequested();

            if (graphicsBuffer == null)
            {
                GraphicsBuffer newGraphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, matCount, 4);

                try
                {
                    int graphicsBufferCount = newGraphicsBuffer.count;
                    int graphicsBufferStride = newGraphicsBuffer.stride;

                    DispatchRenderTextureToMatComputeShader(renderTexture, newGraphicsBuffer, flip, flipCode);

                    cancellationToken.ThrowIfCancellationRequested();

                    await CopyFromGraphicsBufferToMatAsync(newGraphicsBuffer, mat, CopyMode.PerPixel, graphicsBufferCount, graphicsBufferStride, matCount, matStride, cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();
                }
                finally
                {
                    newGraphicsBuffer.Dispose();
                }
            }
            else
            {
                int graphicsBufferCount = graphicsBuffer.count;
                int graphicsBufferStride = graphicsBuffer.stride;

                DispatchRenderTextureToMatComputeShader(renderTexture, graphicsBuffer, flip, flipCode);

                cancellationToken.ThrowIfCancellationRequested();

                await CopyFromGraphicsBufferToMatAsync(graphicsBuffer, mat, CopyMode.PerPixel, graphicsBufferCount, graphicsBufferStride, matCount, matStride, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }

#endif

        /// <summary>
        /// Converts an OpenCV Mat to a Unity RenderTexture.
        /// </summary>
        /// <param name="mat">The source OpenCV Mat. The depth must be 'CV_8U'. The Mat object must not have more than 4 channels.</param>
        /// <param name="renderTexture">
        /// The destination Unity RenderTexture. Must have <c>enableRandomWrite</c> set to true.
        /// </param>
        /// <param name="graphicsBuffer">A temporary GraphicsBuffer. If null, a new one will be created. The stride of the graphics buffer must be exactly 4 bytes.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="renderTexture"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="renderTexture"/> has <c>enableRandomWrite</c> set to false,
        /// or if the size or format of the RenderTexture is invalid.
        /// </exception>
        public static void matToRenderTexture(Mat mat, RenderTexture renderTexture, GraphicsBuffer graphicsBuffer, bool flip = true, int flipCode = 0)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (!renderTexture.enableRandomWrite)
                throw new ArgumentException("renderTexture must have enableRandomWrite set to true.", nameof(renderTexture));

            if ((renderTexture.width & 0x7) != 0)
                throw new ArgumentException("renderTexture.width must be a multiple of 8.");
            if ((renderTexture.height & 0x7) != 0)
                throw new ArgumentException("renderTexture.height must be a multiple of 8.");

            if (mat.depth() != CvType.CV_8U)
                throw new ArgumentException("The Mat object must have a depth of 'CV_8U'.");

            if (mat.channels() > 4)
                throw new ArgumentException("The Mat object must not have more than 4 channels.");

            if (mat.cols() != renderTexture.width || mat.rows() != renderTexture.height)
                throw new ArgumentException("The Mat object must have the same size.");

            if (graphicsBuffer != null && graphicsBuffer.stride != 4)
                throw new ArgumentException("The stride of the graphics buffer must be exactly 4 bytes.");

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();

            if (graphicsBuffer == null)
            {
                GraphicsBuffer newGraphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, matCount, 4);

                int graphicsBufferCount = newGraphicsBuffer.count;
                int graphicsBufferStride = newGraphicsBuffer.stride;

                CopyFromMatToGraphicsBuffer(mat, newGraphicsBuffer, CopyMode.PerPixel, matCount, matStride, graphicsBufferCount, graphicsBufferStride);
                DispatchMatToRenderTextureComputeShader(newGraphicsBuffer, renderTexture, flip, flipCode);

                newGraphicsBuffer.Dispose();
            }
            else
            {
                int graphicsBufferCount = graphicsBuffer.count;
                int graphicsBufferStride = graphicsBuffer.stride;

                CopyFromMatToGraphicsBuffer(mat, graphicsBuffer, CopyMode.PerPixel, matCount, matStride, graphicsBufferCount, graphicsBufferStride);
                DispatchMatToRenderTextureComputeShader(graphicsBuffer, renderTexture, flip, flipCode);
            }
        }

#if UNITY_6000_0_OR_NEWER

        /// <summary>
        /// Converts an OpenCV Mat to a Unity RenderTexture asynchronously.
        /// </summary>
        /// <param name="mat">The source OpenCV Mat. The depth must be 'CV_8U'. The Mat object must not have more than 4 channels.</param>
        /// <param name="renderTexture">
        /// The destination Unity RenderTexture. Must have <c>enableRandomWrite</c> set to true.
        /// </param>
        /// <param name="graphicsBuffer">A temporary GraphicsBuffer. If null, a new one will be created. The stride of the graphics buffer must be exactly 4 bytes.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="renderTexture"/> or <paramref name="mat"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="renderTexture"/> has <c>enableRandomWrite</c> set to false,
        /// or if the size or format of the RenderTexture is invalid.
        /// </exception>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public static async Awaitable<AsyncGPUReadbackRequest> matToRenderTextureAsync(Mat mat, RenderTexture renderTexture, GraphicsBuffer graphicsBuffer, bool flip = true, int flipCode = 0, CancellationToken cancellationToken = default)
        {
            if (!SystemInfo.supportsComputeShaders)
                throw new NotSupportedException("Compute Shaders are not supported on this platform. This feature requires a platform and hardware that supports compute shader operations.");

            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            mat.ThrowIfDisposed();

            if (!SystemInfo.IsFormatSupported(renderTexture.graphicsFormat, UnityEngine.Experimental.Rendering.GraphicsFormatUsage.ReadPixels))
                throw new NotSupportedException($"The graphics format '{renderTexture.graphicsFormat}' is not supported for AsyncGPUReadback.RequestAsync method.");

            if (!renderTexture.enableRandomWrite)
                throw new ArgumentException("renderTexture must have enableRandomWrite set to true.", nameof(renderTexture));

            if ((renderTexture.width & 0x7) != 0)
                throw new ArgumentException("renderTexture.width must be a multiple of 8.");
            if ((renderTexture.height & 0x7) != 0)
                throw new ArgumentException("renderTexture.height must be a multiple of 8.");

            if (mat.depth() != CvType.CV_8U)
                throw new ArgumentException("The Mat object must have a depth of 'CV_8U'.");

            if (mat.channels() > 4)
                throw new ArgumentException("The Mat object must not have more than 4 channels.");

            if (mat.cols() != renderTexture.width || mat.rows() != renderTexture.height)
                throw new ArgumentException("The Mat object must have the same size.");

            if (graphicsBuffer != null && graphicsBuffer.stride != 4)
                throw new ArgumentException("The stride of the graphics buffer must be exactly 4 bytes.");


            cancellationToken.ThrowIfCancellationRequested();

            int matCount = (int)mat.total();
            int matStride = (int)mat.elemSize();

            if (graphicsBuffer == null)
            {
                GraphicsBuffer newGraphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, matCount, 4);

                try
                {
                    int graphicsBufferCount = newGraphicsBuffer.count;
                    int graphicsBufferStride = newGraphicsBuffer.stride;

                    CopyFromMatToGraphicsBuffer(mat, newGraphicsBuffer, CopyMode.PerPixel, matCount, matStride, graphicsBufferCount, graphicsBufferStride);
                    DispatchMatToRenderTextureComputeShader(newGraphicsBuffer, renderTexture, flip, flipCode);

                    cancellationToken.ThrowIfCancellationRequested();

                    AsyncGPUReadbackRequest result = await AsyncGPUReadback.RequestAsync(renderTexture);

                    cancellationToken.ThrowIfCancellationRequested();

                    return result;
                }
                finally
                {
                    newGraphicsBuffer.Dispose();
                }
            }
            else
            {
                int graphicsBufferCount = graphicsBuffer.count;
                int graphicsBufferStride = graphicsBuffer.stride;

                CopyFromMatToGraphicsBuffer(mat, graphicsBuffer, CopyMode.PerPixel, matCount, matStride, graphicsBufferCount, graphicsBufferStride);
                DispatchMatToRenderTextureComputeShader(graphicsBuffer, renderTexture, flip, flipCode);

                cancellationToken.ThrowIfCancellationRequested();

                AsyncGPUReadbackRequest result = await AsyncGPUReadback.RequestAsync(renderTexture);

                cancellationToken.ThrowIfCancellationRequested();

                return result;
            }
        }

#endif

        /// <summary>
        /// Gets the kernel index to use for converting from RenderTexture to Mat based on the current platform and graphics API.
        /// This kernel index is intended to be used when dispatching the ComputeShader obtained from the createRenderTextureToMatComputeShader method.
        /// </summary>
        /// <param name="renderTexture">The source RenderTexture.</param>
        /// <param name="graphicsBuffer">The destination GraphicsBuffer.</param>
        /// <returns>The kernel index to use for the conversion.</returns>
        public static int getKernelIndexForRenderTextureToMatComputeShader(RenderTexture renderTexture, GraphicsBuffer graphicsBuffer)
        {
            // Select the kernel based on color space
            int kernelIndex = 0;
            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                kernelIndex = 1;
                if (!renderTexture.isDataSRGB) kernelIndex = 0;
            }

            return kernelIndex;
        }

        /// <summary>
        /// Creates a ComputeShader for converting from RenderTexture to Mat based on the current platform and graphics API.
        /// This ComputeShader is intended to be used with the kernel index obtained from the getKernelIndexForRenderTextureToMatComputeShader method when dispatching.
        /// </summary>
        /// <param name="renderTexture">The source RenderTexture.</param>
        /// <param name="graphicsBuffer">The destination GraphicsBuffer.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <returns>The created ComputeShader.</returns>
        public static ComputeShader createRenderTextureToMatComputeShader(RenderTexture renderTexture, GraphicsBuffer graphicsBuffer, bool flip = true, int flipCode = 0)
        {
            ComputeShader renderTextureToMatComputeShader = Resources.Load<ComputeShader>("RenderTextureToMat");

            int kernelIndex = getKernelIndexForRenderTextureToMatComputeShader(renderTexture, graphicsBuffer);

            SetupRenderTextureToMatComputeShader(renderTextureToMatComputeShader, renderTexture, graphicsBuffer, flip, flipCode, kernelIndex);

            return renderTextureToMatComputeShader;
        }

        /// <summary>
        /// Gets the kernel index to use for converting from Mat to RenderTexture based on the current platform and graphics API.
        /// This kernel index is intended to be used when dispatching the ComputeShader obtained from the createMatToRenderTextureComputeShader method.
        /// </summary>
        /// <param name="graphicsBuffer">The source GraphicsBuffer.</param>
        /// <param name="renderTexture">The destination RenderTexture.</param>
        /// <returns>The kernel index to use for the conversion.</returns>
        public static int getKernelIndexForMatToRenderTextureComputeShader(GraphicsBuffer graphicsBuffer, RenderTexture renderTexture)
        {
            // Select the kernel based on color space and graphics device
            int kernelIndex = 0;
            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal) kernelIndex = 1;
#if UNITY_6000_0_OR_NEWER
                if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.WebGPU) kernelIndex = 1;
#endif
                if (!renderTexture.isDataSRGB) kernelIndex = 0;
            }

            return kernelIndex;
        }

        /// <summary>
        /// Creates a ComputeShader for converting from Mat to RenderTexture based on the current platform and graphics API.
        /// This ComputeShader is intended to be used with the kernel index obtained from the getKernelIndexForMatToRenderTextureComputeShader method when dispatching.
        /// </summary>
        /// <param name="graphicsBuffer">The source GraphicsBuffer.</param>
        /// <param name="renderTexture">The destination RenderTexture.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <returns>The created ComputeShader.</returns>
        public static ComputeShader createMatToRenderTextureComputeShader(GraphicsBuffer graphicsBuffer, RenderTexture renderTexture, bool flip = true, int flipCode = 0)
        {

            ComputeShader matToRenderTextureComputeShader = Resources.Load<ComputeShader>("MatToRenderTexture");

            int kernelIndex = getKernelIndexForMatToRenderTextureComputeShader(graphicsBuffer, renderTexture);

            SetupMatToRenderTextureComputeShader(matToRenderTextureComputeShader, graphicsBuffer, renderTexture, flip, flipCode, kernelIndex);

            return matToRenderTextureComputeShader;
        }

        #endregion Public Methods

        #region Private Methods

        private static ComputeShader renderTextureToMatComputeShader;
        private static ComputeShader matToRenderTextureComputeShader;

        private const int ARRAYPOOL_MAXARRAYLENGTH = 512 * 512 * 4;

        /// <summary>
        /// Sets up the parameters for copying data from the specified GraphicsBuffer to the specified RenderTexture using the specified ComputeShader.
        /// </summary>
        /// <param name="computeShader">The ComputeShader to use for copying data.</param>
        /// <param name="graphicsBuffer">The source GraphicsBuffer.</param>
        /// <param name="renderTexture">The destination RenderTexture.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <param name="kernelIndex">The kernel index to use for the ComputeShader.</param>
        private static void SetupMatToRenderTextureComputeShader(ComputeShader computeShader, GraphicsBuffer graphicsBuffer, RenderTexture renderTexture, bool flip, int flipCode, int kernelIndex)
        {

            // Set flips only if enabled
            if (flip)
            {
                computeShader.SetBool("HFlip", GetHFlip(flipCode));
                computeShader.SetBool("VFlip", GetVFlip(flipCode));
            }
            else
            {
                computeShader.SetBool("HFlip", false);
                computeShader.SetBool("VFlip", false);
            }

            // Set the GraphicsBuffer and RenderTexture to the shader
            computeShader.SetBuffer(kernelIndex, "Source", graphicsBuffer);
            computeShader.SetTexture(kernelIndex, "Destination", renderTexture);
        }

        /// <summary>
        /// Sets up the parameters for copying data from the specified RenderTexture to the specified GraphicsBuffer using the specified ComputeShader.
        /// </summary>
        /// <param name="computeShader">The ComputeShader to use for copying data.</param>
        /// <param name="renderTexture">The source RenderTexture.</param>
        /// <param name="graphicsBuffer">The destination GraphicsBuffer.</param>
        /// <param name="flip">Whether to flip the image vertically.</param>
        /// <param name="flipCode">The flipCode for the Core.flip() method.</param>
        /// <param name="kernelIndex">The kernel index to use for the ComputeShader.</param>
        private static void SetupRenderTextureToMatComputeShader(ComputeShader computeShader, RenderTexture renderTexture, GraphicsBuffer graphicsBuffer, bool flip, int flipCode, int kernelIndex)
        {

            // Set flips only if enabled
            if (flip)
            {
                computeShader.SetBool("HFlip", GetHFlip(flipCode));
                computeShader.SetBool("VFlip", GetVFlip(flipCode));
            }
            else
            {
                computeShader.SetBool("HFlip", false);
                computeShader.SetBool("VFlip", false);
            }

            // Set the RenderTexture and GraphicsBuffer to the shader
            computeShader.SetTexture(kernelIndex, "Source", renderTexture);
            computeShader.SetBuffer(kernelIndex, "Destination", graphicsBuffer);
        }

        /// <summary>
        /// Uses a Compute Shader to copy data from the specified Mat object to a RenderTexture.
        /// </summary>
        /// <param name="graphicsBuffer">The GraphicsBuffer that stores the Mat data.</param>
        /// <param name="renderTexture">The RenderTexture to which the data will be copied.</param>
        /// <param name="flip">A flag indicating whether to flip the image.</param>
        /// <param name="flipCode">A code specifying how to flip the image. Corresponds to Core.flip()'s flipCode.</param>
        /// <remarks>
        /// This method converts the specified Mat object to a RenderTexture using a Compute Shader.
        /// The shader selects the appropriate kernel based on the color space and graphics device type,
        /// and flips the image if necessary.
        /// </remarks>
        private static void DispatchMatToRenderTextureComputeShader(GraphicsBuffer graphicsBuffer, RenderTexture renderTexture, bool flip, int flipCode)
        {
            // Load the shader only if it has not been loaded yet
            if (matToRenderTextureComputeShader == null)
                matToRenderTextureComputeShader = Resources.Load<ComputeShader>("MatToRenderTexture");

            int kernelIndex = getKernelIndexForMatToRenderTextureComputeShader(graphicsBuffer, renderTexture);

            SetupMatToRenderTextureComputeShader(matToRenderTextureComputeShader, graphicsBuffer, renderTexture, flip, flipCode, kernelIndex);

            // Execute the shader
            matToRenderTextureComputeShader.Dispatch(kernelIndex, renderTexture.width / 8, renderTexture.height / 8, 1);
        }

        /// <summary>
        /// Copies data from the specified Mat object to a GraphicsBuffer.
        /// </summary>
        /// <param name="mat">The Mat object from which to retrieve data.</param>
        /// <param name="graphicsBuffer">The GraphicsBuffer to which the data will be copied.</param>
        /// <param name="copyMode">Specifies the mode for copying data, allowing either per-pixel or continuous copying.</param>
        /// <param name="matCount">The number of elements to copy from the Mat.</param>
        /// <param name="matStride">The stride size of the Mat in bytes.</param>
        /// <param name="graphicsBufferCount">The number of elements in the destination GraphicsBuffer.</param>
        /// <param name="graphicsBufferStride">The stride size of the GraphicsBuffer in bytes.</param>
        /// <remarks>
        /// This method copies the data from the specified Mat object to the GraphicsBuffer.
        /// Depending on the mode, it performs either per-pixel copying or continuous copying.
        /// If the Mat is continuous, it sets the data directly to the GraphicsBuffer.
        /// If the Mat is not continuous, it divides the data into chunks for copying.
        /// </remarks>
        private static void CopyFromMatToGraphicsBuffer(Mat mat, GraphicsBuffer graphicsBuffer, CopyMode copyMode, int matCount, int matStride, int graphicsBufferCount, int graphicsBufferStride)
        {
            if (copyMode == CopyMode.PerPixel && !(matStride == graphicsBufferStride))
            {
                switch (CvType.depth(mat.type()))
                {
                    case CvType.CV_8U:
                        SetDataPerPixelChunks<byte>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride, 1);
                        break;
                    case CvType.CV_8S:
                        SetDataPerPixelChunks<sbyte>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride, 1);
                        break;
                    case CvType.CV_16U:
                        SetDataPerPixelChunks<ushort>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride / 2, 2);
                        break;
                    case CvType.CV_16S:
                        SetDataPerPixelChunks<short>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride / 2, 2);
                        break;
                    //case CvType.CV_16F:
                    case CvType.CV_32S:
                        SetDataPerPixelChunks<int>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride / 4, 4);
                        break;
                    case CvType.CV_32F:
                        SetDataPerPixelChunks<float>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride / 4, 4);
                        break;
                    case CvType.CV_64F:
                        SetDataPerPixelChunks<double>(mat, graphicsBuffer, matCount, mat.channels(), graphicsBufferStride / 8, 8);
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                }
            }
            else
            {
                if (mat.isContinuous())
                {
                    SetData(graphicsBuffer, (IntPtr)mat.dataAddr(), matCount * matStride);
                }
                else
                {
                    switch (CvType.depth(mat.type()))
                    {
                        case CvType.CV_8U:
                            SetDataContinuousChunks<byte>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        case CvType.CV_8S:
                            SetDataContinuousChunks<sbyte>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        case CvType.CV_16U:
                            SetDataContinuousChunks<ushort>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        case CvType.CV_16S:
                            SetDataContinuousChunks<short>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        //case CvType.CV_16F:
                        case CvType.CV_32S:
                            SetDataContinuousChunks<int>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        case CvType.CV_32F:
                            SetDataContinuousChunks<float>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        case CvType.CV_64F:
                            SetDataContinuousChunks<double>(mat, graphicsBuffer, matCount, mat.channels());
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                    }
                }
            }
        }

        /// <summary>
        /// Copies data from the specified Mat object to the GraphicsBuffer.
        /// The data is copied in chunks on a per-pixel basis.
        /// </summary>
        /// <typeparam name="T">The type of the data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="mat">The Mat object from which to retrieve the data.</param>
        /// <param name="graphicsBuffer">The GraphicsBuffer to which the data will be copied.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <param name="matChannels">The number of channels in the Mat object.</param>
        /// <param name="graphicsBufferChannels">The number of channels in the GraphicsBuffer.</param>
        /// <param name="elementSize">The size of each element in bytes.</param>
        /// <remarks>
        /// This method copies data from the specified Mat object to the GraphicsBuffer.
        /// The data is processed in chunks on a per-pixel basis, and channel mapping is performed as needed.
        /// Additionally, the data copy is split based on buffer sizes to optimize performance.
        /// </remarks>
        private static void SetDataPerPixelChunks<T>(Mat mat, GraphicsBuffer graphicsBuffer, int count, int matChannels, int graphicsBufferChannels, int elementSize) where T : unmanaged
        {

            // Determine the number of channels to copy
            int copyChannels = Math.Min(matChannels, graphicsBufferChannels);
            int maxChannels = Math.Max(matChannels, graphicsBufferChannels);

            // Number of elements to copy at once
            int chunkCount = ARRAYPOOL_MAXARRAYLENGTH / maxChannels;
            int matChunkArrayLength = chunkCount * matChannels;
            int graphicsBufferChunkArrayLength = chunkCount * graphicsBufferChannels;

            // Total number of elements in Mat and GraphicsBuffer arrays
            int matTotalArrayLength = count * matChannels;
            int graphicsBufferTotalArrayLength = count * graphicsBufferChannels;

            // CvType
            int matType = mat.type();
            int graphicsBufferType = CvType.makeType(CvType.depth(matType), graphicsBufferChannels);

            MatOfInt fromToMat = null;
            if (matChannels != graphicsBufferChannels)
            {
                // Array to store the channel indices of the source
                int[] fromTo = new int[copyChannels * 2];

                for (int i = 0; i < copyChannels; ++i)
                {
                    fromTo[i * 2] = i;
                    fromTo[i * 2 + 1] = i;
                }

                fromToMat = new MatOfInt(fromTo);
            }

            // Number of chunks to split into
            int numChunks = (count + chunkCount - 1) / chunkCount;

#if NET_STANDARD_2_1
            T[] matData = ArrayPool<T>.Shared.Rent(matChunkArrayLength);
            T[] graphicsBufferData = ArrayPool<T>.Shared.Rent(graphicsBufferChunkArrayLength);
            Array.Fill(graphicsBufferData, default); // Initialize
#else
            T[] matData = new T[matChunkArrayLength];
            T[] graphicsBufferData = new T[graphicsBufferChunkArrayLength];
#endif

            for (int i = 0; i < numChunks; i++)
            {
                // Calculate the number of elements to copy in each chunk
                int matCopyArrayLength = Math.Min(matChunkArrayLength, matTotalArrayLength - (i * matChunkArrayLength));
                int graphicsBufferCopyArrayLength = Math.Min(graphicsBufferChunkArrayLength, graphicsBufferTotalArrayLength - (i * graphicsBufferChunkArrayLength));

                // Ensure that matCopyArrayLength and graphicsBufferCopyArrayLength are greater than zero
                if (matCopyArrayLength > 0 && graphicsBufferCopyArrayLength > 0)
                {
                    mat.get<T>(calcIndices(mat, i * chunkCount), matData, matCopyArrayLength);

#if !OPENCV_DONT_USE_UNSAFE_CODE
                    unsafe
                    {
                        fixed (T* matDataPtr = matData)
                        fixed (T* graphicsBufferDataPtr = graphicsBufferData)
                        {
                            Mat matMat = new Mat(1, matCopyArrayLength / matChannels, matType, (IntPtr)matDataPtr);
                            Mat graphicsBufferMat = new Mat(1, graphicsBufferCopyArrayLength / graphicsBufferChannels, graphicsBufferType, (IntPtr)graphicsBufferDataPtr);

                            if (fromToMat == null)
                            {
                                matMat.copyTo(graphicsBufferMat);
                            }
                            else
                            {
                                partialCopyUsingMixChannels(matMat, graphicsBufferMat, fromToMat);
                            }

                            matMat.Dispose();
                            graphicsBufferMat.Dispose();
                        }
                    }
#else
                    GCHandle matDataHandle = GCHandle.Alloc(matData, GCHandleType.Pinned);
                    GCHandle graphicsBufferDataHandle = GCHandle.Alloc(graphicsBufferData, GCHandleType.Pinned);
                    Mat matMat = new Mat(1, matCopyArrayLength / matChannels, matType, matDataHandle.AddrOfPinnedObject());
                    Mat graphicsBufferMat = new Mat(1, graphicsBufferCopyArrayLength / graphicsBufferChannels, graphicsBufferType, graphicsBufferDataHandle.AddrOfPinnedObject());

                    if (fromToMat == null)
                    {
                        matMat.copyTo(graphicsBufferMat);
                    }
                    else
                    {
                        partialCopyUsingMixChannels(matMat, graphicsBufferMat, fromToMat);
                    }

                    matMat.Dispose();
                    graphicsBufferMat.Dispose();

                    matDataHandle.Free();
                    graphicsBufferDataHandle.Free();
#endif

                    graphicsBuffer.SetData(graphicsBufferData, 0, i * graphicsBufferChunkArrayLength, graphicsBufferCopyArrayLength);
                }
            }

#if NET_STANDARD_2_1
            ArrayPool<T>.Shared.Return(graphicsBufferData);
            ArrayPool<T>.Shared.Return(matData);
#endif

            fromToMat?.Dispose();
        }

        /// <summary>
        /// Copies data from the specified Mat object to the GraphicsBuffer. The data is processed as continuous chunks.
        /// </summary>
        /// <typeparam name="T">The type of the data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="mat">The Mat object from which to retrieve data.</param>
        /// <param name="graphicsBuffer">The GraphicsBuffer to which the data will be copied.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <param name="matChannels">The number of channels in the Mat object.</param>
        /// <remarks>
        /// This method efficiently copies data from the specified Mat object to the GraphicsBuffer.
        /// The data is divided into chunks based on the specified number of elements and channels and is copied continuously.
        /// </remarks>
        private static void SetDataContinuousChunks<T>(Mat mat, GraphicsBuffer graphicsBuffer, int count, int matChannels) where T : unmanaged
        {

            // The number of elements in the Mat to copy at once
            int chunkCount = ARRAYPOOL_MAXARRAYLENGTH / matChannels;

            // The number of elements in the array rented from ArrayPool at once
            int matChunkArrayLength = chunkCount * matChannels;

            // The total number of elements to copy from the Mat
            int matTotalArrayLength = count * matChannels;

            // The number of chunks to divide the data into
            int numChunks = (count + chunkCount - 1) / chunkCount;

#if NET_STANDARD_2_1
            T[] data = ArrayPool<T>.Shared.Rent(matChunkArrayLength);
#else
            T[] data = new T[matChunkArrayLength];
#endif

            for (int i = 0; i < numChunks; i++)
            {
                // Calculate the number of elements to copy in each chunk
                int copyArrayLength = Math.Min(matChunkArrayLength, matTotalArrayLength - (i * matChunkArrayLength));

                mat.get<T>(calcIndices(mat, i * chunkCount), data, copyArrayLength);

                graphicsBuffer.SetData(data, 0, i * chunkCount * matChannels, copyArrayLength);
            }

#if NET_STANDARD_2_1
            ArrayPool<T>.Shared.Return(data);
#endif

        }

        /// <summary>
        /// Uses a Compute Shader to copy data from the specified RenderTexture to a Mat object.
        /// </summary>
        /// <param name="renderTexture">The RenderTexture from which to retrieve data.</param>
        /// <param name="graphicsBuffer">The GraphicsBuffer that stores the data from the RenderTexture.</param>
        /// <param name="flip">A flag indicating whether to flip the image.</param>
        /// <param name="flipCode">A code specifying how to flip the image. Corresponds to Core.flip()'s flipCode.</param>
        /// <remarks>
        /// This method converts the specified RenderTexture to a Mat object using a Compute Shader.
        /// The shader selects the appropriate kernel based on the color space and flips the image if necessary.
        /// </remarks>
        private static void DispatchRenderTextureToMatComputeShader(RenderTexture renderTexture, GraphicsBuffer graphicsBuffer, bool flip, int flipCode)
        {
            // Load the shader only if it has not been loaded yet
            if (renderTextureToMatComputeShader == null)
                renderTextureToMatComputeShader = Resources.Load<ComputeShader>("RenderTextureToMat");

            int kernelIndex = getKernelIndexForRenderTextureToMatComputeShader(renderTexture, graphicsBuffer);

            SetupRenderTextureToMatComputeShader(renderTextureToMatComputeShader, renderTexture, graphicsBuffer, flip, flipCode, kernelIndex);

            // Dispatch the shader
            renderTextureToMatComputeShader.Dispatch(kernelIndex, renderTexture.width / 8, renderTexture.height / 8, 1);
        }

        /// <summary>
        /// Copies data from the GraphicsBuffer to the specified Mat object.
        /// The copy method is determined by either per-pixel copying or continuous chunk copying.
        /// </summary>
        /// <param name="graphicsBuffer">The GraphicsBuffer from which to retrieve data.</param>
        /// <param name="mat">The Mat object to which the data will be copied.</param>
        /// <param name="copyMode">The method of copying (per pixel or continuous chunk).</param>
        /// <param name="graphicsBufferCount">The number of elements in the GraphicsBuffer.</param>
        /// <param name="graphicsBufferStride">The stride of the GraphicsBuffer.</param>
        /// <param name="matCount">The number of elements in the Mat.</param>
        /// <param name="matStride">The stride of the Mat.</param>
        /// <exception cref="NotSupportedException">Thrown when an unsupported Mat type is specified.</exception>
        private static void CopyFromGraphicsBufferToMat(GraphicsBuffer graphicsBuffer, Mat mat, CopyMode copyMode, int graphicsBufferCount, int graphicsBufferStride, int matCount, int matStride)
        {
            if (copyMode == CopyMode.PerPixel && !(matStride == graphicsBufferStride))
            {
                switch (CvType.depth(mat.type()))
                {
                    case CvType.CV_8U:
                        GetDataPerPixelChunks<byte>(graphicsBuffer, mat, matCount, graphicsBufferStride, mat.channels(), 1);
                        break;
                    case CvType.CV_8S:
                        GetDataPerPixelChunks<sbyte>(graphicsBuffer, mat, matCount, graphicsBufferStride, mat.channels(), 1);
                        break;
                    case CvType.CV_16U:
                        GetDataPerPixelChunks<ushort>(graphicsBuffer, mat, matCount, graphicsBufferStride / 2, mat.channels(), 2);
                        break;
                    case CvType.CV_16S:
                        GetDataPerPixelChunks<short>(graphicsBuffer, mat, matCount, graphicsBufferStride / 2, mat.channels(), 2);
                        break;
                    //case CvType.CV_16F:
                    case CvType.CV_32S:
                        GetDataPerPixelChunks<int>(graphicsBuffer, mat, matCount, graphicsBufferStride / 4, mat.channels(), 4);
                        break;
                    case CvType.CV_32F:
                        GetDataPerPixelChunks<float>(graphicsBuffer, mat, matCount, graphicsBufferStride / 4, mat.channels(), 4);
                        break;
                    case CvType.CV_64F:
                        GetDataPerPixelChunks<double>(graphicsBuffer, mat, matCount, graphicsBufferStride / 8, mat.channels(), 8);
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                }
            }
            else
            {

                switch (CvType.depth(mat.type()))
                {
                    case CvType.CV_8U:
                        GetDataContinuousChunks<byte>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride, mat.channels());
                        break;
                    case CvType.CV_8S:
                        GetDataContinuousChunks<sbyte>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride, mat.channels());
                        break;
                    case CvType.CV_16U:
                        GetDataContinuousChunks<ushort>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride / 2, mat.channels());
                        break;
                    case CvType.CV_16S:
                        GetDataContinuousChunks<short>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride / 2, mat.channels());
                        break;
                    //case CvType.CV_16F:
                    case CvType.CV_32S:
                        GetDataContinuousChunks<int>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride / 4, mat.channels());
                        break;
                    case CvType.CV_32F:
                        GetDataContinuousChunks<float>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride / 4, mat.channels());
                        break;
                    case CvType.CV_64F:
                        GetDataContinuousChunks<double>(graphicsBuffer, mat, graphicsBufferCount, graphicsBufferStride / 8, mat.channels());
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                }
            }
        }

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Copies data from the GraphicsBuffer to the specified Mat object asynchronously.
        /// The copy method is determined by either per-pixel copying or continuous chunk copying.
        /// </summary>
        /// <param name="graphicsBuffer">The GraphicsBuffer from which to retrieve data.</param>
        /// <param name="mat">The Mat object to which the data will be copied.</param>
        /// <param name="copyMode">The method of copying (per pixel or continuous chunk).</param>
        /// <param name="graphicsBufferCount">The number of elements in the GraphicsBuffer.</param>
        /// <param name="graphicsBufferStride">The stride of the GraphicsBuffer.</param>
        /// <param name="matCount">The number of elements in the Mat.</param>
        /// <param name="matStride">The stride of the Mat.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <exception cref="NotSupportedException">Thrown when an unsupported Mat type is specified.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        private static async Awaitable CopyFromGraphicsBufferToMatAsync(GraphicsBuffer graphicsBuffer, Mat mat, CopyMode copyMode, int graphicsBufferCount, int graphicsBufferStride, int matCount, int matStride, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (copyMode == CopyMode.PerPixel && !(matStride == graphicsBufferStride))
            {
                switch (CvType.depth(mat.type()))
                {
                    case CvType.CV_8U:
                        await GetDataPerPixelChunksAsync<byte>(graphicsBuffer, mat, matCount, graphicsBufferStride, mat.channels(), 1, cancellationToken);
                        break;
                    case CvType.CV_8S:
                        await GetDataPerPixelChunksAsync<sbyte>(graphicsBuffer, mat, matCount, graphicsBufferStride, mat.channels(), 1, cancellationToken);
                        break;
                    case CvType.CV_16U:
                        await GetDataPerPixelChunksAsync<ushort>(graphicsBuffer, mat, matCount, graphicsBufferStride / 2, mat.channels(), 2, cancellationToken);
                        break;
                    case CvType.CV_16S:
                        await GetDataPerPixelChunksAsync<short>(graphicsBuffer, mat, matCount, graphicsBufferStride / 2, mat.channels(), 2, cancellationToken);
                        break;
                    //case CvType.CV_16F:
                    case CvType.CV_32S:
                        await GetDataPerPixelChunksAsync<int>(graphicsBuffer, mat, matCount, graphicsBufferStride / 4, mat.channels(), 4, cancellationToken);
                        break;
                    case CvType.CV_32F:
                        await GetDataPerPixelChunksAsync<float>(graphicsBuffer, mat, matCount, graphicsBufferStride / 4, mat.channels(), 4, cancellationToken);
                        break;
                    case CvType.CV_64F:
                        await GetDataPerPixelChunksAsync<double>(graphicsBuffer, mat, matCount, graphicsBufferStride / 8, mat.channels(), 8, cancellationToken);
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                }
            }
            else
            {
                switch (CvType.depth(mat.type()))
                {
                    case CvType.CV_8U:
                        await GetDataContinuousAsync<byte>(graphicsBuffer, mat, cancellationToken);
                        break;
                    case CvType.CV_8S:
                        await GetDataContinuousAsync<sbyte>(graphicsBuffer, mat, cancellationToken);
                        break;
                    case CvType.CV_16U:
                        await GetDataContinuousAsync<ushort>(graphicsBuffer, mat, cancellationToken);
                        break;
                    case CvType.CV_16S:
                        await GetDataContinuousAsync<short>(graphicsBuffer, mat, cancellationToken);
                        break;
                    //case CvType.CV_16F:
                    case CvType.CV_32S:
                        await GetDataContinuousAsync<int>(graphicsBuffer, mat, cancellationToken);
                        break;
                    case CvType.CV_32F:
                        await GetDataContinuousAsync<float>(graphicsBuffer, mat, cancellationToken);
                        break;
                    case CvType.CV_64F:
                        await GetDataContinuousAsync<double>(graphicsBuffer, mat, cancellationToken);
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported Mat type: {mat.type()}");
                }
            }
        }

#endif

        /// <summary>
        /// Sets data to the GraphicsBuffer from the specified pointer.
        /// </summary>
        /// <param name="buffer">The GraphicsBuffer to which the data will be set.</param>
        /// <param name="pointer">The pointer to the data.</param>
        /// <param name="matByteDataCount">The number of bytes of data to set.</param>
        /// <remarks>
        /// This method converts an unmanaged memory block to a NativeArray and sets the data in the GraphicsBuffer.
        /// When Unity collection checks are enabled, an atomic safety handle is used to ensure thread safety.
        /// </remarks>
        private unsafe static void SetData(GraphicsBuffer buffer, IntPtr pointer, int matByteDataCount)
        {
            //Debug.Log("bufferByteDataCount "+(buffer.count * buffer.stride) + " matByteDataCount " + matByteDataCount);

            // NativeArray view for the unmanaged memory block
            var view =
              NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>
                ((void*)pointer, matByteDataCount, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            var safety = AtomicSafetyHandle.Create();
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref view, safety);
#endif

            buffer.SetData(view, 0, 0, matByteDataCount);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.Release(safety);
#endif
        }

        /// <summary>
        /// Retrieves pixel data from the specified GraphicsBuffer in chunks and copies it to the Mat object as continuous chunks.
        /// </summary>
        /// <typeparam name="T">The type of the pixel data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="graphicsBuffer">The graphics buffer from which to retrieve the pixel data.</param>
        /// <param name="mat">The Mat object to which the pixel data will be copied.</param>
        /// <param name="count">The number of elements to copy from the graphics buffer.</param>
        /// <param name="graphicsBufferChannels">The number of channels in the graphics buffer.</param>
        /// <param name="matChannels">The number of channels in the Mat object.</param>
        /// <param name="elementSize">The size of each element in bytes.</param>
        /// <remarks>
        /// This method splits the data into chunks based on the number of channels in the Mat object.
        /// Each chunk is retrieved from the graphics buffer and copied to the Mat as a continuous block of pixel data.
        /// The total number of elements to be copied is calculated, and memory management is optimized using the ArrayPool
        /// when <c>NET_STANDARD_2_1</c> is defined.
        /// If the number of channels differs between the graphics buffer and the Mat, appropriate channel mapping is applied.
        /// </remarks>
        private static void GetDataPerPixelChunks<T>(GraphicsBuffer graphicsBuffer, Mat mat, int count, int graphicsBufferChannels, int matChannels, int elementSize) where T : unmanaged
        {
            // Determine the number of channels to copy
            int copyCount = Math.Min(matChannels, graphicsBufferChannels);
            int maxChannels = Math.Max(matChannels, graphicsBufferChannels);

            // Number of elements to copy at once
            int chunkCount = ARRAYPOOL_MAXARRAYLENGTH / maxChannels;
            int matChunkArrayLength = chunkCount * matChannels;
            int graphicsBufferChunkArrayLength = chunkCount * graphicsBufferChannels;

            // Total number of elements in Mat and GraphicsBuffer
            int matTotalArrayLength = count * matChannels;
            int graphicsBufferTotalArrayLength = count * graphicsBufferChannels;

            // CvType
            int matType = mat.type();
            int graphicsBufferType = CvType.makeType(CvType.depth(matType), graphicsBufferChannels);

            MatOfInt fromToMat = null;
            if (matChannels != graphicsBufferChannels)
            {
                // Array to store the source channel indices
                int[] fromTo = new int[copyCount * 2];
                for (int i = 0; i < copyCount; ++i)
                {
                    fromTo[i * 2] = i;
                    fromTo[i * 2 + 1] = i;
                }
                fromToMat = new MatOfInt(fromTo);
            }

            // Number of chunks to divide the data into
            int numChunks = (count + chunkCount - 1) / chunkCount;

#if NET_STANDARD_2_1
            T[] matData = ArrayPool<T>.Shared.Rent(matChunkArrayLength);
            matData.AsSpan<T>().Fill(default(T));
            T[] graphicsBufferData = ArrayPool<T>.Shared.Rent(graphicsBufferChunkArrayLength);
#else
            T[] matData = new T[matChunkArrayLength];
            T[] graphicsBufferData = new T[graphicsBufferChunkArrayLength];
#endif

            for (int i = 0; i < numChunks; i++)
            {
                // Calculate the number of elements to copy in each chunk
                int matCopyArrayLength = Math.Min(matChunkArrayLength, matTotalArrayLength - (i * matChunkArrayLength));
                int graphicsBufferCopyArrayLength = Math.Min(graphicsBufferChunkArrayLength, graphicsBufferTotalArrayLength - (i * graphicsBufferChunkArrayLength));

                // Ensure the copy sizes are greater than zero
                if (matCopyArrayLength > 0 && graphicsBufferCopyArrayLength > 0)
                {
                    graphicsBuffer.GetData(graphicsBufferData, 0, i * graphicsBufferChunkArrayLength, graphicsBufferCopyArrayLength);

#if !OPENCV_DONT_USE_UNSAFE_CODE
                    unsafe
                    {
                        fixed (T* matDataPtr = matData)
                        fixed (T* graphicsBufferDataPtr = graphicsBufferData)
                        {
                            Mat matMat = new Mat(1, matCopyArrayLength / matChannels, matType, (IntPtr)matDataPtr);
                            Mat graphicsBufferMat = new Mat(1, graphicsBufferCopyArrayLength / graphicsBufferChannels, graphicsBufferType, (IntPtr)graphicsBufferDataPtr);

                            if (fromToMat == null)
                            {
                                graphicsBufferMat.copyTo(matMat);
                            }
                            else
                            {
                                partialCopyUsingMixChannels(graphicsBufferMat, matMat, fromToMat);
                            }

                            matMat.Dispose();
                            graphicsBufferMat.Dispose();
                        }
                    }
#else
                    GCHandle matDataHandle = GCHandle.Alloc(matData, GCHandleType.Pinned);
                    GCHandle graphicsBufferDataHandle = GCHandle.Alloc(graphicsBufferData, GCHandleType.Pinned);
                    Mat matMat = new Mat(1, matCopyArrayLength / matChannels, matType, matDataHandle.AddrOfPinnedObject());
                    Mat graphicsBufferMat = new Mat(1, graphicsBufferCopyArrayLength / graphicsBufferChannels, graphicsBufferType, graphicsBufferDataHandle.AddrOfPinnedObject());

                    if (fromToMat == null)
                    {
                        graphicsBufferMat.copyTo(matMat);
                    }
                    else
                    {
                        partialCopyUsingMixChannels(graphicsBufferMat, matMat, fromToMat);
                    }

                    matMat.Dispose();
                    graphicsBufferMat.Dispose();

                    matDataHandle.Free();
                    graphicsBufferDataHandle.Free();
#endif

                    mat.put<T>(calcIndices(mat, i * chunkCount), matData, matCopyArrayLength);
                }
            }

#if NET_STANDARD_2_1
            ArrayPool<T>.Shared.Return(graphicsBufferData);
            ArrayPool<T>.Shared.Return(matData);
#endif

            fromToMat?.Dispose();
        }

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Retrieves pixel data from the specified GraphicsBuffer in chunks and copies it to the Mat object as continuous chunks asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the pixel data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="graphicsBuffer">The graphics buffer from which to retrieve the pixel data.</param>
        /// <param name="mat">The Mat object to which the pixel data will be copied.</param>
        /// <param name="count">The number of elements to copy from the graphics buffer.</param>
        /// <param name="graphicsBufferChannels">The number of channels in the graphics buffer.</param>
        /// <param name="matChannels">The number of channels in the Mat object.</param>
        /// <param name="elementSize">The size of each element in bytes.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <remarks>
        /// This method splits the data into chunks based on the number of channels in the Mat object.
        /// Each chunk is retrieved from the graphics buffer and copied to the Mat as a continuous block of pixel data.
        /// The total number of elements to be copied is calculated, and memory management is optimized using the ArrayPool
        /// when <c>NET_STANDARD_2_1</c> is defined.
        /// If the number of channels differs between the graphics buffer and the Mat, appropriate channel mapping is applied.
        /// </remarks>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        private static async Awaitable GetDataPerPixelChunksAsync<T>(GraphicsBuffer graphicsBuffer, Mat mat, int count, int graphicsBufferChannels, int matChannels, int elementSize, CancellationToken cancellationToken) where T : unmanaged
        {
            // Determine the number of channels to copy
            int copyCount = Math.Min(matChannels, graphicsBufferChannels);
            int maxChannels = Math.Max(matChannels, graphicsBufferChannels);

            // Number of elements to copy at once
            int chunkCount = 0;
            // AsyncGPUReadback behaves differently in WebGPUs, and if data is acquired in small batches by specifying offsets, it will fail, so data should be acquired in batches.
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.WebGPU)
            {
                chunkCount = count;

                //// As there is information that offset must be a multiple of 256 in WebGPU, calculate chunkCount so that offset is a multiple of 256.
                //int baseChunkCount = ARRAYPOOL_MAXARRAYLENGTH / maxChannels;
                //int alignmentFactor = 256 / (maxChannels * elementSize);
                //if (alignmentFactor > 0)
                //{
                //    baseChunkCount = (baseChunkCount / alignmentFactor) * alignmentFactor;
                //}
                //int chunkCount = Math.Max(alignmentFactor, baseChunkCount);
            }
            else
            {
                chunkCount = ARRAYPOOL_MAXARRAYLENGTH / maxChannels;
            }

            int matChunkArrayLength = chunkCount * matChannels;
            int graphicsBufferChunkArrayLength = chunkCount * graphicsBufferChannels;

            // Total number of elements in Mat and GraphicsBuffer
            int matTotalArrayLength = count * matChannels;
            int graphicsBufferTotalArrayLength = count * graphicsBufferChannels;

            // CvType
            int matType = mat.type();
            int graphicsBufferType = CvType.makeType(CvType.depth(matType), graphicsBufferChannels);

            MatOfInt fromToMat = null;
            if (matChannels != graphicsBufferChannels)
            {
                // Array to store the source channel indices
                int[] fromTo = new int[copyCount * 2];
                for (int i = 0; i < copyCount; ++i)
                {
                    fromTo[i * 2] = i;
                    fromTo[i * 2 + 1] = i;
                }
                fromToMat = new MatOfInt(fromTo);
            }

            // Number of chunks to divide the data into
            int numChunks = (count + chunkCount - 1) / chunkCount;

#if NET_STANDARD_2_1
            T[] matData = ArrayPool<T>.Shared.Rent(matChunkArrayLength);
            matData.AsSpan<T>().Fill(default(T));
#else
            T[] matData = new T[matChunkArrayLength];
            T[] graphicsBufferData = new T[graphicsBufferChunkArrayLength];
#endif

            try
            {
                for (int i = 0; i < numChunks; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Calculate the number of elements to copy in each chunk
                    int matCopyArrayLength = Math.Min(matChunkArrayLength, matTotalArrayLength - (i * matChunkArrayLength));
                    int graphicsBufferCopyArrayLength = Math.Min(graphicsBufferChunkArrayLength, graphicsBufferTotalArrayLength - (i * graphicsBufferChunkArrayLength));

                    // Ensure the copy sizes are greater than zero
                    if (matCopyArrayLength > 0 && graphicsBufferCopyArrayLength > 0)
                    {
                        AsyncGPUReadbackRequest asyncGPUReadbackRequest = await AsyncGPUReadback.RequestAsync(
                            graphicsBuffer,
                            graphicsBufferCopyArrayLength * elementSize,
                            (i * graphicsBufferChunkArrayLength) * elementSize);

                        cancellationToken.ThrowIfCancellationRequested();

                        NativeArray<T> graphicsBufferDataNativeArray = asyncGPUReadbackRequest.GetData<T>();

                        unsafe
                        {
#if NET_STANDARD_2_1
                            fixed (T* matDataPtr = matData)
                            {
                                T* graphicsBufferDataPtr = (T*)graphicsBufferDataNativeArray.GetUnsafeReadOnlyPtr();
#else
                            graphicsBufferDataNativeArray.AsSpan().CopyTo(graphicsBufferData.AsSpan(0, graphicsBufferDataNativeArray.Length));

                            fixed (T* matDataPtr = matData)
                            fixed (T* graphicsBufferDataPtr = graphicsBufferData)
                            {
#endif
                                Mat matMat = new Mat(1, matCopyArrayLength / matChannels, matType, (IntPtr)matDataPtr);
                                Mat graphicsBufferMat = new Mat(1, graphicsBufferCopyArrayLength / graphicsBufferChannels, graphicsBufferType, (IntPtr)graphicsBufferDataPtr);

                                if (fromToMat == null)
                                {
                                    graphicsBufferMat.copyTo(matMat);
                                }
                                else
                                {
                                    partialCopyUsingMixChannels(graphicsBufferMat, matMat, fromToMat);
                                }

                                matMat.Dispose();
                                graphicsBufferMat.Dispose();
                            }
                        }

                        mat.put<T>(calcIndices(mat, i * chunkCount), matData, matCopyArrayLength);

                        graphicsBufferDataNativeArray.Dispose();
                    }
                }
            }
            finally
            {
#if NET_STANDARD_2_1
                ArrayPool<T>.Shared.Return(matData);
#endif
                fromToMat?.Dispose();
            }
        }

#endif

        /// <summary>
        /// Retrieves data from the graphics buffer in chunks and copies it to the specified Mat object as continuous chunks.
        /// </summary>
        /// <typeparam name="T">The type of the data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="graphicsBuffer">The graphics buffer from which to retrieve the data.</param>
        /// <param name="mat">The Mat object to which the data will be copied.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <param name="graphicsBufferChannels">The number of channels in the graphics buffer.</param>
        /// <param name="matChannels">The number of channels in the Mat object.</param>
        private static void GetDataContinuousChunks<T>(GraphicsBuffer graphicsBuffer, Mat mat, int count, int graphicsBufferChannels, int matChannels) where T : unmanaged
        {
            // The number of elements in the Mat to copy at once
            int chunkCount = ARRAYPOOL_MAXARRAYLENGTH / matChannels;

            // The number of elements in the array rented from ArrayPool at once
            int matChunkArrayLength = chunkCount * matChannels;

            // The total number of elements to copy from the graphics buffer
            int graphicsBufferTotalArrayLength = count * graphicsBufferChannels;

            // The number of chunks to divide the data into
            int numChunks = (count + chunkCount - 1) / chunkCount;

#if NET_STANDARD_2_1
            T[] data = ArrayPool<T>.Shared.Rent(matChunkArrayLength);
#else
            T[] data = new T[matChunkArrayLength];
#endif

            for (int i = 0; i < numChunks; i++)
            {
                // Calculate the number of elements to copy in each chunk
                int copyArrayLength = Math.Min(matChunkArrayLength, graphicsBufferTotalArrayLength - (i * matChunkArrayLength));

                graphicsBuffer.GetData(data, 0, i * matChunkArrayLength, copyArrayLength);

                mat.put<T>(calcIndices(mat, i * chunkCount), data, copyArrayLength);
            }

#if NET_STANDARD_2_1
            ArrayPool<T>.Shared.Return(data);
#endif
        }

#if UNITY_6000_0_OR_NEWER && !OPENCV_DONT_USE_UNSAFE_CODE

        /// <summary>
        /// Retrieves data from the graphics buffer and copies it to the specified Mat object as continuous asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the data to be copied. Constrained to <c>unmanaged</c> types.</typeparam>
        /// <param name="graphicsBuffer">The graphics buffer from which to retrieve the data.</param>
        /// <param name="mat">The Mat object to which the data will be copied.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        private static async Awaitable GetDataContinuousAsync<T>(GraphicsBuffer graphicsBuffer, Mat mat, CancellationToken cancellationToken) where T : unmanaged
        {
            cancellationToken.ThrowIfCancellationRequested();

            AsyncGPUReadbackRequest asyncGPUReadbackRequest = await AsyncGPUReadback.RequestAsync(graphicsBuffer);

            cancellationToken.ThrowIfCancellationRequested();

            NativeArray<T> graphicsBufferDataNativeArray = asyncGPUReadbackRequest.GetData<T>();

            if (mat.isContinuous() || mat.dims() <= 2)
            {
                MatUtils.copyToMat<T>(graphicsBufferDataNativeArray, mat);
            }
            else
            {
#if NET_STANDARD_2_1
                mat.put<T>(0, 0, graphicsBufferDataNativeArray);
#else
                T[] data = new T[graphicsBufferDataNativeArray.Length];
                graphicsBufferDataNativeArray.CopyTo(data);

                mat.put<T>(0, 0, data);
#endif
            }

        }

#endif

        /// <summary>
        /// Calculates the indices for each dimension of the matrix based on the given one-dimensional index.
        /// </summary>
        /// <param name="mat">The matrix for which to calculate the indices.</param>
        /// <param name="index">The one-dimensional index.</param>
        /// <returns>An array containing the indices for each dimension.</returns>
        private static int[] calcIndices(Mat mat, int index)
        {
            int dimensions = mat.dims();
            int[] indices = new int[dimensions];

            // Cache the size of each dimension of the matrix
            int[] sizes = new int[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                sizes[i] = mat.size(i);
            }

            // Calculate the indices
            for (int i = dimensions - 1; i >= 0; i--)
            {
                indices[i] = index % sizes[i];
                index /= sizes[i];
            }

            return indices;
        }

        /// <summary>
        /// Copies part of the channels from the source matrix to the destination matrix.
        /// </summary>
        /// <param name="src">The source matrix from which to copy.</param>
        /// <param name="dst">The destination matrix to which to copy.</param>
        /// <param name="fromToMat">A MatOfInt specifying the mapping between source and destination channels.</param>
        private static void partialCopyUsingMixChannels(Mat src, Mat dst, MatOfInt fromToMat)
        {
            List<Mat> srcMats = new List<Mat>();
            srcMats.Add(src);
            List<Mat> dstMats = new List<Mat>();
            dstMats.Add(dst);

            Core.mixChannels(srcMats, dstMats, fromToMat);
        }

        /// <summary>
        /// Determines whether a horizontal flip should be applied based on the flip code.
        /// </summary>
        /// <param name="flipCode">The flip code. A value of 1 or -1 indicates a horizontal flip.</param>
        /// <returns>True if a horizontal flip should be applied; otherwise, false.</returns>
        private static bool GetHFlip(int flipCode)
        {
            return flipCode == 1 || flipCode == -1;
        }

        /// <summary>
        /// Determines whether a vertical flip should be applied based on the flip code.
        /// </summary>
        /// <param name="flipCode">The flip code. A value of 0 or -1 indicates a vertical flip.</param>
        /// <returns>True if a vertical flip should be applied; otherwise, false.</returns>
        private static bool GetVFlip(int flipCode)
        {
            return flipCode == 0 || flipCode == -1;
        }

        #endregion Private Methods
    }
}
