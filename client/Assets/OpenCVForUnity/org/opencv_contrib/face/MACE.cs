
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.FaceModule
{

    // C++: class MACE
    /// <summary>
    ///  Minimum Average Correlation Energy Filter
    ///      useful for authentication with (cancellable) biometrical features.
    ///      (does not need many positives to train (10-50), and no negatives at all, also robust to noise/salting)
    /// </summary>
    /// <remarks>
    ///      see also: @cite Savvides04
    ///  
    ///      this implementation is largely based on: https://code.google.com/archive/p/pam-face-authentication (GSOC 2009)
    ///  
    ///      use it like:
    /// </remarks>
    /// <code language="c++">
    ///  
    ///      Ptr<face::MACE> mace = face::MACE::create(64);
    ///  
    ///      vector<Mat> pos_images = ...
    ///      mace->train(pos_images);
    ///  
    ///      Mat query = ...
    ///      bool same = mace->same(query);
    ///  
    /// </code>
    /// <remarks>
    ///      you can also use two-factor authentication, with an additional passphrase:
    /// </remarks>
    /// <code language="c++">
    ///      String owners_passphrase = "ilikehotdogs";
    ///      Ptr<face::MACE> mace = face::MACE::create(64);
    ///      mace->salt(owners_passphrase);
    ///      vector<Mat> pos_images = ...
    ///      mace->train(pos_images);
    ///  
    ///      // now, users have to give a valid passphrase, along with the image:
    ///      Mat query = ...
    ///      cout << "enter passphrase: ";
    ///      string pass;
    ///      getline(cin, pass);
    ///      mace->salt(pass);
    ///      bool same = mace->same(query);
    /// </code>
    /// <remarks>
    ///      save/load your model:
    /// </remarks>
    /// <code language="c++">
    ///      Ptr<face::MACE> mace = face::MACE::create(64);
    ///      mace->train(pos_images);
    ///      mace->save("my_mace.xml");
    ///  
    ///      // later:
    ///      Ptr<MACE> reloaded = MACE::load("my_mace.xml");
    ///      reloaded->same(some_image);
    /// </code>
    public class MACE : Algorithm
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
                        face_MACE_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal MACE(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new MACE __fromPtr__(IntPtr addr) { return new MACE(addr); }

        //
        // C++:  void cv::face::MACE::salt(String passphrase)
        //

        /// <summary>
        ///  optionally encrypt images with random convolution
        /// </summary>
        /// <param name="passphrase">
        /// a crc64 random seed will get generated from this
        /// </param>
        public void salt(string passphrase)
        {
            ThrowIfDisposed();

            face_MACE_salt_10(nativeObj, passphrase);


        }


        //
        // C++:  void cv::face::MACE::train(vector_Mat images)
        //

        /// <summary>
        ///  train it on positive features
        ///         compute the mace filter: `h = D(-1) * X * (X(+) * D(-1) * X)(-1) * C`
        ///         also calculate a minimal threshold for this class, the smallest self-similarity from the train images
        /// </summary>
        /// <param name="images">
        /// a vector<Mat> with the train images
        /// </param>
        public void train(List<Mat> images)
        {
            ThrowIfDisposed();
            using Mat images_mat = Converters.vector_Mat_to_Mat(images);
            face_MACE_train_10(nativeObj, images_mat.nativeObj);


        }


        //
        // C++:  bool cv::face::MACE::same(Mat query)
        //

        /// <summary>
        ///  correlate query img and threshold to min class value
        /// </summary>
        /// <param name="query">
        /// a Mat with query image
        /// </param>
        public bool same(Mat query)
        {
            ThrowIfDisposed();
            if (query != null) query.ThrowIfDisposed();

            return face_MACE_same_10(nativeObj, query.nativeObj);


        }


        //
        // C++: static Ptr_MACE cv::face::MACE::load(String filename, String objname = String())
        //

        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="filename">
        /// build a new MACE instance from a pre-serialized FileStorage
        /// </param>
        /// <param name="objname">
        /// (optional) top-level node in the FileStorage
        /// </param>
        public static MACE load(string filename, string objname)
        {


            return MACE.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(face_MACE_load_10(filename, objname)));


        }

        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="filename">
        /// build a new MACE instance from a pre-serialized FileStorage
        /// </param>
        /// <param name="objname">
        /// (optional) top-level node in the FileStorage
        /// </param>
        public static MACE load(string filename)
        {


            return MACE.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(face_MACE_load_11(filename)));


        }


        //
        // C++: static Ptr_MACE cv::face::MACE::create(int IMGSIZE = 64)
        //

        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="IMGSIZE">
        /// images will get resized to this (should be an even number)
        /// </param>
        public static MACE create(int IMGSIZE)
        {


            return MACE.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(face_MACE_create_10(IMGSIZE)));


        }

        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="IMGSIZE">
        /// images will get resized to this (should be an even number)
        /// </param>
        public static MACE create()
        {


            return MACE.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(face_MACE_create_11()));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::face::MACE::salt(String passphrase)
        [DllImport(LIBNAME)]
        private static extern void face_MACE_salt_10(IntPtr nativeObj, string passphrase);

        // C++:  void cv::face::MACE::train(vector_Mat images)
        [DllImport(LIBNAME)]
        private static extern void face_MACE_train_10(IntPtr nativeObj, IntPtr images_mat_nativeObj);

        // C++:  bool cv::face::MACE::same(Mat query)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool face_MACE_same_10(IntPtr nativeObj, IntPtr query_nativeObj);

        // C++: static Ptr_MACE cv::face::MACE::load(String filename, String objname = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr face_MACE_load_10(string filename, string objname);
        [DllImport(LIBNAME)]
        private static extern IntPtr face_MACE_load_11(string filename);

        // C++: static Ptr_MACE cv::face::MACE::create(int IMGSIZE = 64)
        [DllImport(LIBNAME)]
        private static extern IntPtr face_MACE_create_10(int IMGSIZE);
        [DllImport(LIBNAME)]
        private static extern IntPtr face_MACE_create_11();

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void face_MACE_delete(IntPtr nativeObj);

    }
}
