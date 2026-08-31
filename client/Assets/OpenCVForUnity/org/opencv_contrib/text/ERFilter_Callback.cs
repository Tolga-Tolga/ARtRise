#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.TextModule
{
    // C++: class Callback
    /// <summary>
    ///  Callback with the classifier is made a class.
    /// </summary>
    /// <remarks>
    ///      By doing it we hide SVM, Boost etc. Developers can provide their own classifiers to the
    ///      ERFilter algorithm.
    /// </remarks>
    public class ERFilter_Callback : DisposableOpenCVObject
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
                        text_ERFilter_1Callback_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal ERFilter_Callback(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static ERFilter_Callback __fromPtr__(IntPtr addr) { return new ERFilter_Callback(addr); }

#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void text_ERFilter_1Callback_delete(IntPtr nativeObj);

    }
}


#endif