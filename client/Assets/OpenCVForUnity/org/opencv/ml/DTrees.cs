
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{

    // C++: class DTrees
    /// <summary>
    ///  The class represents a single decision tree or a collection of decision trees.
    /// </summary>
    /// <remarks>
    ///  The current public interface of the class allows user to train only a single decision tree, however
    ///  the class is capable of storing multiple decision trees and using them for prediction (by summing
    ///  responses or using a voting schemes), and the derived from DTrees classes (such as RTrees and Boost)
    ///  use this capability to implement decision tree ensembles.
    ///  
    ///  @sa @ref ml_intro_trees
    /// </remarks>
    public class DTrees : StatModel
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
                        ml_DTrees_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal DTrees(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new DTrees __fromPtr__(IntPtr addr) { return new DTrees(addr); }

        /// <summary>
        /// C++: enum Flags (cv.ml.DTrees.Flags)
        /// </summary>
        public const int PREDICT_AUTO = 0;

        /// <summary>
        /// C++: enum Flags (cv.ml.DTrees.Flags)
        /// </summary>
        public const int PREDICT_SUM = (1 << 8);

        /// <summary>
        /// C++: enum Flags (cv.ml.DTrees.Flags)
        /// </summary>
        public const int PREDICT_MAX_VOTE = (2 << 8);

        /// <summary>
        /// C++: enum Flags (cv.ml.DTrees.Flags)
        /// </summary>
        public const int PREDICT_MASK = (3 << 8);


        //
        // C++:  int cv::ml::DTrees::getMaxCategories()
        //

        /// <remarks>
        ///  @see setMaxCategories
        /// </remarks>
        public int getMaxCategories()
        {
            ThrowIfDisposed();

            return ml_DTrees_getMaxCategories_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setMaxCategories(int val)
        //

        /// <remarks>
        ///  @copybrief getMaxCategories @see getMaxCategories
        /// </remarks>
        public void setMaxCategories(int val)
        {
            ThrowIfDisposed();

            ml_DTrees_setMaxCategories_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::DTrees::getMaxDepth()
        //

        /// <remarks>
        ///  @see setMaxDepth
        /// </remarks>
        public int getMaxDepth()
        {
            ThrowIfDisposed();

            return ml_DTrees_getMaxDepth_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setMaxDepth(int val)
        //

        /// <remarks>
        ///  @copybrief getMaxDepth @see getMaxDepth
        /// </remarks>
        public void setMaxDepth(int val)
        {
            ThrowIfDisposed();

            ml_DTrees_setMaxDepth_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::DTrees::getMinSampleCount()
        //

        /// <remarks>
        ///  @see setMinSampleCount
        /// </remarks>
        public int getMinSampleCount()
        {
            ThrowIfDisposed();

            return ml_DTrees_getMinSampleCount_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setMinSampleCount(int val)
        //

        /// <remarks>
        ///  @copybrief getMinSampleCount @see getMinSampleCount
        /// </remarks>
        public void setMinSampleCount(int val)
        {
            ThrowIfDisposed();

            ml_DTrees_setMinSampleCount_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::DTrees::getCVFolds()
        //

        /// <remarks>
        ///  @see setCVFolds
        /// </remarks>
        public int getCVFolds()
        {
            ThrowIfDisposed();

            return ml_DTrees_getCVFolds_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setCVFolds(int val)
        //

        /// <remarks>
        ///  @copybrief getCVFolds @see getCVFolds
        /// </remarks>
        public void setCVFolds(int val)
        {
            ThrowIfDisposed();

            ml_DTrees_setCVFolds_10(nativeObj, val);


        }


        //
        // C++:  bool cv::ml::DTrees::getUseSurrogates()
        //

        /// <remarks>
        ///  @see setUseSurrogates
        /// </remarks>
        public bool getUseSurrogates()
        {
            ThrowIfDisposed();

            return ml_DTrees_getUseSurrogates_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setUseSurrogates(bool val)
        //

        /// <remarks>
        ///  @copybrief getUseSurrogates @see getUseSurrogates
        /// </remarks>
        public void setUseSurrogates(bool val)
        {
            ThrowIfDisposed();

            ml_DTrees_setUseSurrogates_10(nativeObj, val);


        }


        //
        // C++:  bool cv::ml::DTrees::getUse1SERule()
        //

        /// <remarks>
        ///  @see setUse1SERule
        /// </remarks>
        public bool getUse1SERule()
        {
            ThrowIfDisposed();

            return ml_DTrees_getUse1SERule_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setUse1SERule(bool val)
        //

        /// <remarks>
        ///  @copybrief getUse1SERule @see getUse1SERule
        /// </remarks>
        public void setUse1SERule(bool val)
        {
            ThrowIfDisposed();

            ml_DTrees_setUse1SERule_10(nativeObj, val);


        }


        //
        // C++:  bool cv::ml::DTrees::getTruncatePrunedTree()
        //

        /// <remarks>
        ///  @see setTruncatePrunedTree
        /// </remarks>
        public bool getTruncatePrunedTree()
        {
            ThrowIfDisposed();

            return ml_DTrees_getTruncatePrunedTree_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setTruncatePrunedTree(bool val)
        //

        /// <remarks>
        ///  @copybrief getTruncatePrunedTree @see getTruncatePrunedTree
        /// </remarks>
        public void setTruncatePrunedTree(bool val)
        {
            ThrowIfDisposed();

            ml_DTrees_setTruncatePrunedTree_10(nativeObj, val);


        }


        //
        // C++:  float cv::ml::DTrees::getRegressionAccuracy()
        //

        /// <remarks>
        ///  @see setRegressionAccuracy
        /// </remarks>
        public float getRegressionAccuracy()
        {
            ThrowIfDisposed();

            return ml_DTrees_getRegressionAccuracy_10(nativeObj);


        }


        //
        // C++:  void cv::ml::DTrees::setRegressionAccuracy(float val)
        //

        /// <remarks>
        ///  @copybrief getRegressionAccuracy @see getRegressionAccuracy
        /// </remarks>
        public void setRegressionAccuracy(float val)
        {
            ThrowIfDisposed();

            ml_DTrees_setRegressionAccuracy_10(nativeObj, val);


        }


        //
        // C++:  Mat cv::ml::DTrees::getPriors()
        //

        /// <remarks>
        ///  @see setPriors
        /// </remarks>
        public Mat getPriors()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(ml_DTrees_getPriors_10(nativeObj)));


        }


        //
        // C++:  void cv::ml::DTrees::setPriors(Mat val)
        //

        /// <remarks>
        ///  @copybrief getPriors @see getPriors
        /// </remarks>
        public void setPriors(Mat val)
        {
            ThrowIfDisposed();
            if (val != null) val.ThrowIfDisposed();

            ml_DTrees_setPriors_10(nativeObj, val.nativeObj);


        }


        //
        // C++: static Ptr_DTrees cv::ml::DTrees::create()
        //

        /// <summary>
        ///  Creates the empty model
        /// </summary>
        /// <remarks>
        ///      The static method creates empty decision tree with the specified parameters. It should be then
        ///      trained using train method (see StatModel::train). Alternatively, you can load the model from
        ///      file using Algorithm::load&lt;DTrees&gt;(filename).
        /// </remarks>
        public static DTrees create()
        {


            return DTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_DTrees_create_10()));


        }


        //
        // C++: static Ptr_DTrees cv::ml::DTrees::load(String filepath, String nodeName = String())
        //

        /// <summary>
        ///  Loads and creates a serialized DTrees from a file
        /// </summary>
        /// <remarks>
        ///         Use DTree::save to serialize and store an DTree to disk.
        ///         Load the DTree from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized DTree
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static DTrees load(string filepath, string nodeName)
        {


            return DTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_DTrees_load_10(filepath, nodeName)));


        }

        /// <summary>
        ///  Loads and creates a serialized DTrees from a file
        /// </summary>
        /// <remarks>
        ///         Use DTree::save to serialize and store an DTree to disk.
        ///         Load the DTree from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized DTree
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static DTrees load(string filepath)
        {


            return DTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_DTrees_load_11(filepath)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  int cv::ml::DTrees::getMaxCategories()
        [DllImport(LIBNAME)]
        private static extern int ml_DTrees_getMaxCategories_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setMaxCategories(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setMaxCategories_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::DTrees::getMaxDepth()
        [DllImport(LIBNAME)]
        private static extern int ml_DTrees_getMaxDepth_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setMaxDepth(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setMaxDepth_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::DTrees::getMinSampleCount()
        [DllImport(LIBNAME)]
        private static extern int ml_DTrees_getMinSampleCount_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setMinSampleCount(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setMinSampleCount_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::DTrees::getCVFolds()
        [DllImport(LIBNAME)]
        private static extern int ml_DTrees_getCVFolds_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setCVFolds(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setCVFolds_10(IntPtr nativeObj, int val);

        // C++:  bool cv::ml::DTrees::getUseSurrogates()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_DTrees_getUseSurrogates_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setUseSurrogates(bool val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setUseSurrogates_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool val);

        // C++:  bool cv::ml::DTrees::getUse1SERule()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_DTrees_getUse1SERule_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setUse1SERule(bool val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setUse1SERule_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool val);

        // C++:  bool cv::ml::DTrees::getTruncatePrunedTree()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_DTrees_getTruncatePrunedTree_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setTruncatePrunedTree(bool val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setTruncatePrunedTree_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool val);

        // C++:  float cv::ml::DTrees::getRegressionAccuracy()
        [DllImport(LIBNAME)]
        private static extern float ml_DTrees_getRegressionAccuracy_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setRegressionAccuracy(float val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setRegressionAccuracy_10(IntPtr nativeObj, float val);

        // C++:  Mat cv::ml::DTrees::getPriors()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_DTrees_getPriors_10(IntPtr nativeObj);

        // C++:  void cv::ml::DTrees::setPriors(Mat val)
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_setPriors_10(IntPtr nativeObj, IntPtr val_nativeObj);

        // C++: static Ptr_DTrees cv::ml::DTrees::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_DTrees_create_10();

        // C++: static Ptr_DTrees cv::ml::DTrees::load(String filepath, String nodeName = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_DTrees_load_10(string filepath, string nodeName);
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_DTrees_load_11(string filepath);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_DTrees_delete(IntPtr nativeObj);

    }
}
