using System;
using System.Runtime.InteropServices;
using AOT;
using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// OpenCV Debug utilities.
    /// </summary>
    public static class OpenCVDebug
    {

#pragma warning disable 0414
        /// <summary>
        /// If true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.
        /// </summary>
        private static bool _openCVDebugMode = false;

        /// <summary>
        /// If true, CvException is thrown instead of calling Debug.LogError (msg).
        /// </summary>
        private static bool _throwOpenCVException = false;

        /// <summary>
        /// Callback called when an OpenCV error occurs on the Native side.
        /// </summary>
        private static Action<string> _openCVSetDebugModeCallback;
#pragma warning restore 0414

        /// <summary>
        /// Gets whether debug mode is enabled.
        /// </summary>
        /// <returns>True if debug mode is enabled, false otherwise.</returns>
        public static bool IsDebugMode()
        {
            return _openCVDebugMode;
        }

        /// <summary>
        /// Gets whether OpenCV exceptions are thrown.
        /// </summary>
        /// <returns>True if exceptions are thrown, false otherwise.</returns>
        public static bool IsThrowException()
        {
            return _throwOpenCVException;
        }

        /// <summary>
        /// Sets the debug mode.
        /// </summary>
        /// <remarks>
        /// If debugMode is true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.However, if throwException is true, CvException is thrown instead of calling Debug.LogError (msg).
        /// </remarks>
        /// <example>
        /// Please use as follows.
        /// <code>
        /// {
        ///     // CVException handling
        ///     // Publish CVException to Debug.LogError.
        ///     OpenCVDebug.SetDebugMode(true, false);
        ///
        ///     Mat m3 = m1 / m2; // element type is different.
        ///
        ///     OpenCVDebug.SetDebugMode(false);
        ///
        ///
        ///     // Throw CVException.
        ///     OpenCVDebug.SetDebugMode(true, true);
        ///
        ///     try
        ///     {
        ///         Mat m4 = m1 / m2; // element type is different.
        ///     }
        ///     catch (Exception e)
        ///     {
        ///         Debug.Log("CVException: " + e);
        ///     }
        ///
        ///     OpenCVDebug.SetDebugMode(false);
        /// }
        /// </code>
        /// </example>
        /// <param name="debugMode">
        /// If true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.
        /// </param>
        /// <param name="throwException">
        /// If true, CvException is thrown instead of calling Debug.LogError (msg).
        /// </param>
        public static void SetDebugMode(bool debugMode, bool throwException = false)
        {
            SetDebugModeInternal(debugMode, throwException, _openCVSetDebugModeCallback);
        }

        /// <summary>
        /// Sets the debug mode with a callback.
        /// </summary>
        /// <remarks>
        /// If debugMode is true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.However, if throwException is true, CvException is thrown instead of calling Debug.LogError (msg).
        /// The callback is maintained even when debug mode is turned off, allowing it to be reused when debug mode is turned on again.
        /// To clear the callback, call this method with null as the callback parameter.
        /// </remarks>
        /// <example>
        /// Please use as follows.
        /// <code>
        /// {
        ///     // CVException handling
        ///     // Publish CVException to Debug.LogError.
        ///     OpenCVDebug.SetDebugMode(true, false);
        ///
        ///     Mat m3 = m1 / m2; // element type is different.
        ///
        ///     OpenCVDebug.SetDebugMode(false);
        ///
        ///
        ///     // Throw CVException.
        ///     OpenCVDebug.SetDebugMode(true, true);
        ///
        ///     try
        ///     {
        ///         Mat m4 = m1 / m2; // element type is different.
        ///     }
        ///     catch (Exception e)
        ///     {
        ///         Debug.Log("CVException: " + e);
        ///     }
        ///
        ///     // Callback string of CVException.
        ///     OpenCVDebug.SetDebugMode(true, true, (str) =>
        ///     {
        ///         Debug.Log("CVException: " + str);
        ///     });
        ///
        ///     try
        ///     {
        ///         Mat m4 = m1 / m2; // element type is different.
        ///     }
        ///     catch (Exception e)
        ///     {
        ///         Debug.Log("CVException: " + e);
        ///     }
        ///
        ///     OpenCVDebug.SetDebugMode(false);
        /// }
        /// </code>
        /// </example>
        /// <param name="debugMode">
        /// If true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.
        /// </param>
        /// <param name="throwException">
        /// If true, CvException is thrown instead of calling Debug.LogError (msg).
        /// </param>
        /// <param name="callback">
        /// Callback called when an OpenCV error occurs on the Native side.
        /// The callback is maintained even when debug mode is turned off.
        /// To clear the callback, pass null as this parameter.
        /// </param>
        public static void SetDebugMode(bool debugMode, bool throwException, Action<string> callback)
        {
            SetDebugModeInternal(debugMode, throwException, callback);
        }

        /// <summary>
        /// Internal implementation of SetDebugMode that handles the common functionality.
        /// </summary>
        private static void SetDebugModeInternal(bool debugMode, bool throwException, Action<string> callback)
        {
            OpenCVForUnity_SetDebugMode(debugMode);

            if (debugMode)
            {
                OpenCVForUnity_SetDebugLogFunc(DebugLogFunc);
                //OpenCVForUnity_DebugLogTest ();

                _throwOpenCVException = throwException;
            }
            else
            {
                OpenCVForUnity_SetDebugLogFunc(null);

                _throwOpenCVException = false;
            }

            _openCVDebugMode = debugMode;
            _openCVSetDebugModeCallback = callback;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void DebugLogDelegate(string str);

        [MonoPInvokeCallback(typeof(DebugLogDelegate))]
        private static void DebugLogFunc(string str)
        {
            if (_openCVSetDebugModeCallback != null) _openCVSetDebugModeCallback.Invoke(str);

            if (_throwOpenCVException)
            {
                throw new CvException(str);
            }
            else
            {
                Debug.LogError(str);
            }
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_SetDebugMode([MarshalAs(UnmanagedType.U1)] bool flag);

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_SetDebugLogFunc(DebugLogDelegate func);

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_DebugLogTest();
    }
}
