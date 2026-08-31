
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{

    // C++: class Boost
    /// <summary>
    ///  Boosted tree classifier derived from DTrees
    /// </summary>
    /// <remarks>
    ///  @sa @ref ml_intro_boost
    /// </remarks>
    public class Boost : DTrees
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
                        ml_Boost_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Boost(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new Boost __fromPtr__(IntPtr addr) { return new Boost(addr); }

        /// <summary>
        /// C++: enum Types (cv.ml.Boost.Types)
        /// </summary>
        public const int DISCRETE = 0;

        /// <summary>
        /// C++: enum Types (cv.ml.Boost.Types)
        /// </summary>
        public const int REAL = 1;

        /// <summary>
        /// C++: enum Types (cv.ml.Boost.Types)
        /// </summary>
        public const int LOGIT = 2;

        /// <summary>
        /// C++: enum Types (cv.ml.Boost.Types)
        /// </summary>
        public const int GENTLE = 3;


        //
        // C++:  int cv::ml::Boost::getBoostType()
        //

        /// <remarks>
        ///  @see setBoostType
        /// </remarks>
        public int getBoostType()
        {
            ThrowIfDisposed();

            return ml_Boost_getBoostType_10(nativeObj);


        }


        //
        // C++:  void cv::ml::Boost::setBoostType(int val)
        //

        /// <remarks>
        ///  @copybrief getBoostType @see getBoostType
        /// </remarks>
        public void setBoostType(int val)
        {
            ThrowIfDisposed();

            ml_Boost_setBoostType_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::Boost::getWeakCount()
        //

        /// <remarks>
        ///  @see setWeakCount
        /// </remarks>
        public int getWeakCount()
        {
            ThrowIfDisposed();

            return ml_Boost_getWeakCount_10(nativeObj);


        }


        //
        // C++:  void cv::ml::Boost::setWeakCount(int val)
        //

        /// <remarks>
        ///  @copybrief getWeakCount @see getWeakCount
        /// </remarks>
        public void setWeakCount(int val)
        {
            ThrowIfDisposed();

            ml_Boost_setWeakCount_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::Boost::getWeightTrimRate()
        //

        /// <remarks>
        ///  @see setWeightTrimRate
        /// </remarks>
        public double getWeightTrimRate()
        {
            ThrowIfDisposed();

            return ml_Boost_getWeightTrimRate_10(nativeObj);


        }


        //
        // C++:  void cv::ml::Boost::setWeightTrimRate(double val)
        //

        /// <remarks>
        ///  @copybrief getWeightTrimRate @see getWeightTrimRate
        /// </remarks>
        public void setWeightTrimRate(double val)
        {
            ThrowIfDisposed();

            ml_Boost_setWeightTrimRate_10(nativeObj, val);


        }


        //
        // C++: static Ptr_Boost cv::ml::Boost::create()
        //

        /// <remarks>
        ///  Creates the empty model.
        ///  Use StatModel::train to train the model, Algorithm::load&lt;Boost&gt;(filename) to load the pre-trained model.
        /// </remarks>
        public static new Boost create()
        {


            return Boost.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_Boost_create_10()));


        }


        //
        // C++: static Ptr_Boost cv::ml::Boost::load(String filepath, String nodeName = String())
        //

        /// <summary>
        ///  Loads and creates a serialized Boost from a file
        /// </summary>
        /// <remarks>
        ///         Use Boost::save to serialize and store an RTree to disk.
        ///         Load the Boost from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized Boost
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static new Boost load(string filepath, string nodeName)
        {


            return Boost.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_Boost_load_10(filepath, nodeName)));


        }

        /// <summary>
        ///  Loads and creates a serialized Boost from a file
        /// </summary>
        /// <remarks>
        ///         Use Boost::save to serialize and store an RTree to disk.
        ///         Load the Boost from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized Boost
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static new Boost load(string filepath)
        {


            return Boost.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_Boost_load_11(filepath)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  int cv::ml::Boost::getBoostType()
        [DllImport(LIBNAME)]
        private static extern int ml_Boost_getBoostType_10(IntPtr nativeObj);

        // C++:  void cv::ml::Boost::setBoostType(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_Boost_setBoostType_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::Boost::getWeakCount()
        [DllImport(LIBNAME)]
        private static extern int ml_Boost_getWeakCount_10(IntPtr nativeObj);

        // C++:  void cv::ml::Boost::setWeakCount(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_Boost_setWeakCount_10(IntPtr nativeObj, int val);

        // C++:  double cv::ml::Boost::getWeightTrimRate()
        [DllImport(LIBNAME)]
        private static extern double ml_Boost_getWeightTrimRate_10(IntPtr nativeObj);

        // C++:  void cv::ml::Boost::setWeightTrimRate(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_Boost_setWeightTrimRate_10(IntPtr nativeObj, double val);

        // C++: static Ptr_Boost cv::ml::Boost::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_Boost_create_10();

        // C++: static Ptr_Boost cv::ml::Boost::load(String filepath, String nodeName = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_Boost_load_10(string filepath, string nodeName);
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_Boost_load_11(string filepath);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_Boost_delete(IntPtr nativeObj);

    }
}
