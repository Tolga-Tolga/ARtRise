

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoioModule
{
    // C++: class IStreamReader
    /// <summary>
    ///  Read data stream interface
    /// </summary>
    public class IStreamReader : DisposableOpenCVObject
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
                        videoio_IStreamReader_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                    if (handle.IsAllocated)
                        handle.Free();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected internal IStreamReader(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static IStreamReader __fromPtr__(IntPtr addr) { return new IStreamReader(addr); }


        protected IStreamReader()
        {
            handle = GCHandle.Alloc(this);
            nativeObj = videoio_IStreamReader_IStreamReader_10(
                (IntPtr)handle, Videoio_IStreamReader_Read_Callback, Videoio_IStreamReader_Seek_Callback
            );
        }


        // Callback delegate
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long Videoio_IStreamReader_Read_Delegate(IntPtr userData, IntPtr buffer, long size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long Videoio_IStreamReader_Seek_Delegate(IntPtr userData, long offset, int way);


        // Hold userData with GCHandle
        private GCHandle handle;


        // C# callback called from native
        [AOT.MonoPInvokeCallback(typeof(Videoio_IStreamReader_Read_Delegate))]
        private static long Videoio_IStreamReader_Read_Callback(IntPtr userData, IntPtr buffer, long size)
        {
            var h = GCHandle.FromIntPtr(userData);
            var self = (IStreamReader)h.Target;
            byte[] managedBuffer = new byte[size];
            long bytesRead = self.read(managedBuffer, size);
            Marshal.Copy(managedBuffer, 0, buffer, (int)bytesRead);
            return bytesRead;
        }


        [AOT.MonoPInvokeCallback(typeof(Videoio_IStreamReader_Seek_Delegate))]
        private static long Videoio_IStreamReader_Seek_Callback(IntPtr userData, long offset, int way)
        {
            var h = GCHandle.FromIntPtr(userData);
            var self = (IStreamReader)h.Target;
            return self.seek(offset, way);
        }

        //
        // C++:  long long cv::IStreamReader::read(byte[] buffer, long long size)
        //


        /// <summary>
        ///  Read bytes from stream
        /// </summary>
        /// <param name="buffer">
        /// already allocated buffer of at least @p size bytes
        /// </param>
        /// <param name="size">
        /// maximum number of bytes to read
        /// </param>
        /// <returns>
        ///  actual number of read bytes
        /// </returns>
        public virtual long read(byte[] buffer, long size)
        {
            UnityEngine.Debug.Log("IStreamReader.read() is not implemented. Please implement it in the derived class.");
            return 0;
        }

        //
        // C++:  long long cv::IStreamReader::seek(long long offset, int origin)
        //


        /// <summary>
        ///  Sets the stream position
        /// </summary>
        /// <param name="offset">
        /// Seek offset
        /// </param>
        /// <param name="origin">
        /// SEEK_SET / SEEK_END / SEEK_CUR
        /// </param>
        /// <remarks>
        ///         @see fseek
        /// </remarks>
        public virtual long seek(long offset, int origin)
        {
            UnityEngine.Debug.Log("IStreamReader.seek() is not implemented. Please implement it in the derived class.");
            return 0;
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif




        [DllImport(LIBNAME)]
        private static extern IntPtr videoio_IStreamReader_IStreamReader_10(IntPtr userData, Videoio_IStreamReader_Read_Delegate readCallback, Videoio_IStreamReader_Seek_Delegate seekCallback);


        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void videoio_IStreamReader_delete(IntPtr nativeObj);

    }
}
