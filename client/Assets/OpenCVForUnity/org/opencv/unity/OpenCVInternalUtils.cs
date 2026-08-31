using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// OpenCV Internal utilities.
    /// </summary>
    public static class OpenCVInternalUtils
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static long UnsignedRightShift(long value, int bits)
        {
            return (long)((ulong)value >> bits);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int ArraysHashCode(double[] a)
        {
            if (a == null)
                return 0;

            int result = 1;
            foreach (double e in a)
            {
                long bits = BitConverter.DoubleToInt64Bits(e);
                int elementHash = unchecked((int)(bits ^ (bits >> 32)));
                result = unchecked(31 * result + elementHash);
            }
            return result;
        }

        internal static void FreeStringCopy(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;
            OpenCVForUnity_FreeStringCopy(ptr);
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_FreeStringCopy(IntPtr ptr);

    }
}
