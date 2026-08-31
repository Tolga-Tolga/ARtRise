

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    // C++: class GraphicalCodeDetector


    public class GraphicalCodeDetector : DisposableOpenCVObject
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
                        objdetect_GraphicalCodeDetector_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal GraphicalCodeDetector(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static GraphicalCodeDetector __fromPtr__(IntPtr addr) { return new GraphicalCodeDetector(addr); }

        //
        // C++:  bool cv::GraphicalCodeDetector::detect(Mat img, Mat& points)
        //

        /// <summary>
        ///  Detects graphical code in image and returns the quadrangle containing the code.
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing (or not) graphical code.
        /// </param>
        /// <param name="points">
        /// Output vector of vertices of the minimum-area quadrangle containing the code.
        /// </param>
        public bool detect(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            return objdetect_GraphicalCodeDetector_detect_10(nativeObj, img.nativeObj, points.nativeObj);


        }


        //
        // C++:  string cv::GraphicalCodeDetector::decode(Mat img, Mat points, Mat& straight_code = Mat())
        //

        /// <summary>
        ///  Decodes graphical code in image once it's found by the detect() method.
        /// </summary>
        /// <remarks>
        ///       Returns UTF8-encoded output string or empty string if the code cannot be decoded.
        /// </remarks>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical code.
        /// </param>
        /// <param name="points">
        /// Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_code">
        /// The optional output image containing binarized code, will be empty if not found.
        /// </param>
        public string decode(Mat img, Mat points, Mat straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_code != null) straight_code.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_decode_10(nativeObj, img.nativeObj, points.nativeObj, straight_code.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }

        /// <summary>
        ///  Decodes graphical code in image once it's found by the detect() method.
        /// </summary>
        /// <remarks>
        ///       Returns UTF8-encoded output string or empty string if the code cannot be decoded.
        /// </remarks>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical code.
        /// </param>
        /// <param name="points">
        /// Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_code">
        /// The optional output image containing binarized code, will be empty if not found.
        /// </param>
        public string decode(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_decode_11(nativeObj, img.nativeObj, points.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  string cv::GraphicalCodeDetector::detectAndDecode(Mat img, Mat& points = Mat(), Mat& straight_code = Mat())
        //

        /// <summary>
        ///  Both detects and decodes graphical code
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found graphical code quadrangle, will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional output image containing binarized code
        /// </param>
        public string detectAndDecode(Mat img, Mat points, Mat straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_code != null) straight_code.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecode_10(nativeObj, img.nativeObj, points.nativeObj, straight_code.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes graphical code
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found graphical code quadrangle, will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional output image containing binarized code
        /// </param>
        public string detectAndDecode(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecode_11(nativeObj, img.nativeObj, points.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes graphical code
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found graphical code quadrangle, will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional output image containing binarized code
        /// </param>
        public string detectAndDecode(Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecode_12(nativeObj, img.nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  bool cv::GraphicalCodeDetector::detectMulti(Mat img, Mat& points)
        //

        /// <summary>
        ///  Detects graphical codes in image and returns the vector of the quadrangles containing the codes.
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing (or not) graphical codes.
        /// </param>
        /// <param name="points">
        /// Output vector of vector of vertices of the minimum-area quadrangle containing the codes.
        /// </param>
        public bool detectMulti(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            return objdetect_GraphicalCodeDetector_detectMulti_10(nativeObj, img.nativeObj, points.nativeObj);


        }


        //
        // C++:  bool cv::GraphicalCodeDetector::decodeMulti(Mat img, Mat points, vector_string& decoded_info, vector_Mat& straight_code = vector_Mat())
        //

        /// <summary>
        ///  Decodes graphical codes in image once it's found by the detect() method.
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical codes.
        /// </param>
        /// <param name="decoded_info">
        /// UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
        /// </param>
        /// <param name="points">
        /// vector of Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_code">
        /// The optional output vector of images containing binarized codes
        /// </param>
        public bool decodeMulti(Mat img, Mat points, List<string> decoded_info, List<Mat> straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            using Mat straight_code_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_decodeMulti_10(nativeObj, img.nativeObj, points.nativeObj, decoded_info_mat.nativeObj, straight_code_mat.nativeObj);
            Converters.Mat_to_vector_string(decoded_info_mat, decoded_info);
            Converters.Mat_to_vector_Mat(straight_code_mat, straight_code);
            return retVal;
        }

        /// <summary>
        ///  Decodes graphical codes in image once it's found by the detect() method.
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical codes.
        /// </param>
        /// <param name="decoded_info">
        /// UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
        /// </param>
        /// <param name="points">
        /// vector of Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_code">
        /// The optional output vector of images containing binarized codes
        /// </param>
        public bool decodeMulti(Mat img, Mat points, List<string> decoded_info)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_decodeMulti_11(nativeObj, img.nativeObj, points.nativeObj, decoded_info_mat.nativeObj);
            Converters.Mat_to_vector_string(decoded_info_mat, decoded_info);
            return retVal;
        }


        //
        // C++:  bool cv::GraphicalCodeDetector::detectAndDecodeMulti(Mat img, vector_string& decoded_info, Mat& points = Mat(), vector_Mat& straight_code = vector_Mat())
        //

        /// <summary>
        ///  Both detects and decodes graphical codes
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical codes.
        /// </param>
        /// <param name="decoded_info">
        /// UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
        /// </param>
        /// <param name="points">
        /// optional output vector of vertices of the found graphical code quadrangles. Will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional vector of images containing binarized codes
        /// </param>
        /// <remarks>
        ///      - If there are QR codes encoded with a Structured Append mode on the image and all of them detected and decoded correctly,
        ///      method writes a full message to position corresponds to 0-th code in a sequence. The rest of QR codes from the same sequence
        ///      have empty string.
        /// </remarks>
        public bool detectAndDecodeMulti(Mat img, List<string> decoded_info, Mat points, List<Mat> straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            using Mat straight_code_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeMulti_10(nativeObj, img.nativeObj, decoded_info_mat.nativeObj, points.nativeObj, straight_code_mat.nativeObj);
            Converters.Mat_to_vector_string(decoded_info_mat, decoded_info);
            Converters.Mat_to_vector_Mat(straight_code_mat, straight_code);
            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes graphical codes
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical codes.
        /// </param>
        /// <param name="decoded_info">
        /// UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
        /// </param>
        /// <param name="points">
        /// optional output vector of vertices of the found graphical code quadrangles. Will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional vector of images containing binarized codes
        /// </param>
        /// <remarks>
        ///      - If there are QR codes encoded with a Structured Append mode on the image and all of them detected and decoded correctly,
        ///      method writes a full message to position corresponds to 0-th code in a sequence. The rest of QR codes from the same sequence
        ///      have empty string.
        /// </remarks>
        public bool detectAndDecodeMulti(Mat img, List<string> decoded_info, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeMulti_11(nativeObj, img.nativeObj, decoded_info_mat.nativeObj, points.nativeObj);
            Converters.Mat_to_vector_string(decoded_info_mat, decoded_info);
            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes graphical codes
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing graphical codes.
        /// </param>
        /// <param name="decoded_info">
        /// UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
        /// </param>
        /// <param name="points">
        /// optional output vector of vertices of the found graphical code quadrangles. Will be empty if not found.
        /// </param>
        /// <param name="straight_code">
        /// The optional vector of images containing binarized codes
        /// </param>
        /// <remarks>
        ///      - If there are QR codes encoded with a Structured Append mode on the image and all of them detected and decoded correctly,
        ///      method writes a full message to position corresponds to 0-th code in a sequence. The rest of QR codes from the same sequence
        ///      have empty string.
        /// </remarks>
        public bool detectAndDecodeMulti(Mat img, List<string> decoded_info)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeMulti_12(nativeObj, img.nativeObj, decoded_info_mat.nativeObj);
            Converters.Mat_to_vector_string(decoded_info_mat, decoded_info);
            return retVal;
        }


        //
        // C++:  NativeByteArray cv::GraphicalCodeDetector::detectAndDecode(Mat img, Mat& points = Mat(), Mat& straight_code = Mat())
        //

        public byte[] detectAndDecodeBytes(Mat img, Mat points, Mat straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_code != null) straight_code.ThrowIfDisposed();
            byte[] retVal = null;
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecodeBytes_10(nativeObj, img.nativeObj, points.nativeObj, straight_code.nativeObj)));
            Converters.Mat_to_NativeByteArray(retValMat, ref retVal);
            return retVal;
        }

        public byte[] detectAndDecodeBytes(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            byte[] retVal = null;
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecodeBytes_11(nativeObj, img.nativeObj, points.nativeObj)));
            Converters.Mat_to_NativeByteArray(retValMat, ref retVal);
            return retVal;
        }

        public byte[] detectAndDecodeBytes(Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            byte[] retVal = null;
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_detectAndDecodeBytes_12(nativeObj, img.nativeObj)));
            Converters.Mat_to_NativeByteArray(retValMat, ref retVal);
            return retVal;
        }


        //
        // C++:  NativeByteArray cv::GraphicalCodeDetector::decode(Mat img, Mat points, Mat& straight_code = Mat())
        //

        public byte[] decodeBytes(Mat img, Mat points, Mat straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_code != null) straight_code.ThrowIfDisposed();
            byte[] retVal = null;
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_decodeBytes_10(nativeObj, img.nativeObj, points.nativeObj, straight_code.nativeObj)));
            Converters.Mat_to_NativeByteArray(retValMat, ref retVal);
            return retVal;
        }

        public byte[] decodeBytes(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            byte[] retVal = null;
            Mat retValMat = new Mat(DisposableObject.ThrowIfNullIntPtr(objdetect_GraphicalCodeDetector_decodeBytes_11(nativeObj, img.nativeObj, points.nativeObj)));
            Converters.Mat_to_NativeByteArray(retValMat, ref retVal);
            return retVal;
        }


        //
        // C++:  bool cv::GraphicalCodeDetector::decodeMulti(Mat img, Mat points, vector_NativeByteArray& decoded_info, vector_Mat& straight_code = vector_Mat())
        //

        public bool decodeBytesMulti(Mat img, Mat points, List<byte[]> decoded_info, List<Mat> straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            using Mat straight_code_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_decodeBytesMulti_10(nativeObj, img.nativeObj, points.nativeObj, decoded_info_mat.nativeObj, straight_code_mat.nativeObj);
            Converters.Mat_to_vector_NativeByteArray(decoded_info_mat, decoded_info);
            Converters.Mat_to_vector_Mat(straight_code_mat, straight_code);
            return retVal;
        }

        public bool decodeBytesMulti(Mat img, Mat points, List<byte[]> decoded_info)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_decodeBytesMulti_11(nativeObj, img.nativeObj, points.nativeObj, decoded_info_mat.nativeObj);
            Converters.Mat_to_vector_NativeByteArray(decoded_info_mat, decoded_info);
            return retVal;
        }


        //
        // C++:  bool cv::GraphicalCodeDetector::detectAndDecodeMulti(Mat img, vector_NativeByteArray& decoded_info, Mat& points = Mat(), vector_Mat& straight_code = vector_Mat())
        //

        public bool detectAndDecodeBytesMulti(Mat img, List<byte[]> decoded_info, Mat points, List<Mat> straight_code)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            using Mat straight_code_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_10(nativeObj, img.nativeObj, decoded_info_mat.nativeObj, points.nativeObj, straight_code_mat.nativeObj);
            Converters.Mat_to_vector_NativeByteArray(decoded_info_mat, decoded_info);
            Converters.Mat_to_vector_Mat(straight_code_mat, straight_code);
            return retVal;
        }

        public bool detectAndDecodeBytesMulti(Mat img, List<byte[]> decoded_info, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_11(nativeObj, img.nativeObj, decoded_info_mat.nativeObj, points.nativeObj);
            Converters.Mat_to_vector_NativeByteArray(decoded_info_mat, decoded_info);
            return retVal;
        }

        public bool detectAndDecodeBytesMulti(Mat img, List<byte[]> decoded_info)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            using Mat decoded_info_mat = new Mat();
            bool retVal = objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_12(nativeObj, img.nativeObj, decoded_info_mat.nativeObj);
            Converters.Mat_to_vector_NativeByteArray(decoded_info_mat, decoded_info);
            return retVal;
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  bool cv::GraphicalCodeDetector::detect(Mat img, Mat& points)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detect_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);

        // C++:  string cv::GraphicalCodeDetector::decode(Mat img, Mat points, Mat& straight_code = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_decode_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_decode_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);

        // C++:  string cv::GraphicalCodeDetector::detectAndDecode(Mat img, Mat& points = Mat(), Mat& straight_code = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecode_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecode_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecode_12(IntPtr nativeObj, IntPtr img_nativeObj);

        // C++:  bool cv::GraphicalCodeDetector::detectMulti(Mat img, Mat& points)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectMulti_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);

        // C++:  bool cv::GraphicalCodeDetector::decodeMulti(Mat img, Mat points, vector_string& decoded_info, vector_Mat& straight_code = vector_Mat())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_decodeMulti_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr straight_code_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_decodeMulti_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr decoded_info_mat_nativeObj);

        // C++:  bool cv::GraphicalCodeDetector::detectAndDecodeMulti(Mat img, vector_string& decoded_info, Mat& points = Mat(), vector_Mat& straight_code = vector_Mat())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeMulti_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeMulti_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr points_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeMulti_12(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj);

        // C++:  NativeByteArray cv::GraphicalCodeDetector::detectAndDecode(Mat img, Mat& points = Mat(), Mat& straight_code = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecodeBytes_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecodeBytes_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_detectAndDecodeBytes_12(IntPtr nativeObj, IntPtr img_nativeObj);

        // C++:  NativeByteArray cv::GraphicalCodeDetector::decode(Mat img, Mat points, Mat& straight_code = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_decodeBytes_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_GraphicalCodeDetector_decodeBytes_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);

        // C++:  bool cv::GraphicalCodeDetector::decodeMulti(Mat img, Mat points, vector_NativeByteArray& decoded_info, vector_Mat& straight_code = vector_Mat())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_decodeBytesMulti_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr straight_code_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_decodeBytesMulti_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr decoded_info_mat_nativeObj);

        // C++:  bool cv::GraphicalCodeDetector::detectAndDecodeMulti(Mat img, vector_NativeByteArray& decoded_info, Mat& points = Mat(), vector_Mat& straight_code = vector_Mat())
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr points_nativeObj, IntPtr straight_code_mat_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj, IntPtr points_nativeObj);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool objdetect_GraphicalCodeDetector_detectAndDecodeBytesMulti_12(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr decoded_info_mat_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void objdetect_GraphicalCodeDetector_delete(IntPtr nativeObj);

    }
}
