
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.FaceModule
{

    // C++: class Facemark
    /// <summary>
    ///  Abstract base class for all facemark models
    /// </summary>
    /// <remarks>
    ///  To utilize this API in your program, please take a look at the @ref tutorial_table_of_content_facemark
    ///  ### Description
    ///  
    ///  Facemark is a base class which provides universal access to any specific facemark algorithm.
    ///  Therefore, the users should declare a desired algorithm before they can use it in their application.
    ///  
    ///  Here is an example on how to declare a facemark algorithm:
    /// </remarks>
    /// <code language="c++">
    ///  // Using Facemark in your code:
    ///  Ptr<Facemark> facemark = createFacemarkLBF();
    /// </code>
    /// <remarks>
    ///  The typical pipeline for facemark detection is as follows:
    ///  - Load the trained model using Facemark::loadModel.
    ///  - Perform the fitting on an image via Facemark::fit.
    /// </remarks>
    public class Facemark : Algorithm
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
                        face_Facemark_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Facemark(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new Facemark __fromPtr__(IntPtr addr) { return new Facemark(addr); }

        //
        // C++:  void cv::face::Facemark::loadModel(String model)
        //

        /// <summary>
        ///  A function to load the trained model before the fitting process.
        /// </summary>
        /// <param name="model">
        /// A string represent the filename of a trained model.
        /// </param>
        /// <remarks>
        ///      <B>Example of usage</B>
        /// </remarks>
        /// <code language="c++">
        ///      facemark->loadModel("../data/lbf.model");
        /// </code>
        public void loadModel(string model)
        {
            ThrowIfDisposed();

            face_Facemark_loadModel_10(nativeObj, model);


        }


        //
        // C++:  bool cv::face::Facemark::fit(Mat image, vector_Rect faces, vector_vector_Point2f& landmarks)
        //

        /// <summary>
        ///  Detect facial landmarks from an image.
        /// </summary>
        /// <param name="image">
        /// Input image.
        /// </param>
        /// <param name="faces">
        /// Output of the function which represent region of interest of the detected faces.
        ///      Each face is stored in cv::Rect container.
        /// </param>
        /// <param name="landmarks">
        /// The detected landmark points for each faces.
        /// </param>
        /// <remarks>
        ///      <B>Example of usage</B>
        /// </remarks>
        /// <code language="c++">
        ///      Mat image = imread("image.jpg");
        ///      std::vector<Rect> faces;
        ///      std::vector<std::vector<Point2f> > landmarks;
        ///      facemark->fit(image, faces, landmarks);
        /// </code>
        public bool fit(Mat image, MatOfRect faces, List<MatOfPoint2f> landmarks)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (faces != null) faces.ThrowIfDisposed();
            Mat faces_mat = faces;
            using Mat landmarks_mat = new Mat();
            bool retVal = face_Facemark_fit_10(nativeObj, image.nativeObj, faces_mat.nativeObj, landmarks_mat.nativeObj);
            Converters.Mat_to_vector_vector_Point2f(landmarks_mat, landmarks);
            return retVal;
        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::face::Facemark::loadModel(String model)
        [DllImport(LIBNAME)]
        private static extern void face_Facemark_loadModel_10(IntPtr nativeObj, string model);

        // C++:  bool cv::face::Facemark::fit(Mat image, vector_Rect faces, vector_vector_Point2f& landmarks)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool face_Facemark_fit_10(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr faces_mat_nativeObj, IntPtr landmarks_mat_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void face_Facemark_delete(IntPtr nativeObj);

    }
}
