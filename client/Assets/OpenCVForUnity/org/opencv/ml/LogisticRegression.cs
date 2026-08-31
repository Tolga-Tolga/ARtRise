
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{

    // C++: class LogisticRegression
    /// <summary>
    ///  Implements Logistic Regression classifier.
    /// </summary>
    /// <remarks>
    ///  @sa @ref ml_intro_lr
    /// </remarks>
    public partial class LogisticRegression : StatModel
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
                        ml_LogisticRegression_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal LogisticRegression(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new LogisticRegression __fromPtr__(IntPtr addr) { return new LogisticRegression(addr); }

        /// <summary>
        /// C++: enum Methods (cv.ml.LogisticRegression.Methods)
        /// </summary>
        public const int BATCH = 0;

        /// <summary>
        /// C++: enum Methods (cv.ml.LogisticRegression.Methods)
        /// </summary>
        public const int MINI_BATCH = 1;


        /// <summary>
        /// C++: enum RegKinds (cv.ml.LogisticRegression.RegKinds)
        /// </summary>
        public const int REG_DISABLE = -1;

        /// <summary>
        /// C++: enum RegKinds (cv.ml.LogisticRegression.RegKinds)
        /// </summary>
        public const int REG_L1 = 0;

        /// <summary>
        /// C++: enum RegKinds (cv.ml.LogisticRegression.RegKinds)
        /// </summary>
        public const int REG_L2 = 1;


        //
        // C++:  double cv::ml::LogisticRegression::getLearningRate()
        //

        /// <remarks>
        ///  @see setLearningRate
        /// </remarks>
        public double getLearningRate()
        {
            ThrowIfDisposed();

            return ml_LogisticRegression_getLearningRate_10(nativeObj);


        }


        //
        // C++:  void cv::ml::LogisticRegression::setLearningRate(double val)
        //

        /// <remarks>
        ///  @copybrief getLearningRate @see getLearningRate
        /// </remarks>
        public void setLearningRate(double val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setLearningRate_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::LogisticRegression::getIterations()
        //

        /// <remarks>
        ///  @see setIterations
        /// </remarks>
        public int getIterations()
        {
            ThrowIfDisposed();

            return ml_LogisticRegression_getIterations_10(nativeObj);


        }


        //
        // C++:  void cv::ml::LogisticRegression::setIterations(int val)
        //

        /// <remarks>
        ///  @copybrief getIterations @see getIterations
        /// </remarks>
        public void setIterations(int val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setIterations_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::LogisticRegression::getRegularization()
        //

        /// <remarks>
        ///  @see setRegularization
        /// </remarks>
        public int getRegularization()
        {
            ThrowIfDisposed();

            return ml_LogisticRegression_getRegularization_10(nativeObj);


        }


        //
        // C++:  void cv::ml::LogisticRegression::setRegularization(int val)
        //

        /// <remarks>
        ///  @copybrief getRegularization @see getRegularization
        /// </remarks>
        public void setRegularization(int val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setRegularization_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::LogisticRegression::getTrainMethod()
        //

        /// <remarks>
        ///  @see setTrainMethod
        /// </remarks>
        public int getTrainMethod()
        {
            ThrowIfDisposed();

            return ml_LogisticRegression_getTrainMethod_10(nativeObj);


        }


        //
        // C++:  void cv::ml::LogisticRegression::setTrainMethod(int val)
        //

        /// <remarks>
        ///  @copybrief getTrainMethod @see getTrainMethod
        /// </remarks>
        public void setTrainMethod(int val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setTrainMethod_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::LogisticRegression::getMiniBatchSize()
        //

        /// <remarks>
        ///  @see setMiniBatchSize
        /// </remarks>
        public int getMiniBatchSize()
        {
            ThrowIfDisposed();

            return ml_LogisticRegression_getMiniBatchSize_10(nativeObj);


        }


        //
        // C++:  void cv::ml::LogisticRegression::setMiniBatchSize(int val)
        //

        /// <remarks>
        ///  @copybrief getMiniBatchSize @see getMiniBatchSize
        /// </remarks>
        public void setMiniBatchSize(int val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setMiniBatchSize_10(nativeObj, val);


        }


        //
        // C++:  TermCriteria cv::ml::LogisticRegression::getTermCriteria()
        //

        /// <remarks>
        ///  @see setTermCriteria
        /// </remarks>
        public TermCriteria getTermCriteria()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            ml_LogisticRegression_getTermCriteria_10(nativeObj, tmpArray);
            TermCriteria retVal = new TermCriteria(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::ml::LogisticRegression::setTermCriteria(TermCriteria val)
        //

        /// <remarks>
        ///  @copybrief getTermCriteria @see getTermCriteria
        /// </remarks>
        public void setTermCriteria(TermCriteria val)
        {
            ThrowIfDisposed();

            ml_LogisticRegression_setTermCriteria_10(nativeObj, val.type, val.maxCount, val.epsilon);


        }


        //
        // C++:  float cv::ml::LogisticRegression::predict(Mat samples, Mat& results = Mat(), int flags = 0)
        //

        /// <summary>
        ///  Predicts responses for input samples and returns a float type.
        /// </summary>
        /// <param name="samples">
        /// The input data for the prediction algorithm. Matrix [m x n], where each row
        ///          contains variables (features) of one object being classified. Should have data type CV_32F.
        /// </param>
        /// <param name="results">
        /// Predicted labels as a column matrix of type CV_32S.
        /// </param>
        /// <param name="flags">
        /// Not used.
        /// </param>
        public override float predict(Mat samples, Mat results, int flags)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (results != null) results.ThrowIfDisposed();

            return ml_LogisticRegression_predict_10(nativeObj, samples.nativeObj, results.nativeObj, flags);


        }

        /// <summary>
        ///  Predicts responses for input samples and returns a float type.
        /// </summary>
        /// <param name="samples">
        /// The input data for the prediction algorithm. Matrix [m x n], where each row
        ///          contains variables (features) of one object being classified. Should have data type CV_32F.
        /// </param>
        /// <param name="results">
        /// Predicted labels as a column matrix of type CV_32S.
        /// </param>
        /// <param name="flags">
        /// Not used.
        /// </param>
        public override float predict(Mat samples, Mat results)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (results != null) results.ThrowIfDisposed();

            return ml_LogisticRegression_predict_11(nativeObj, samples.nativeObj, results.nativeObj);


        }

        /// <summary>
        ///  Predicts responses for input samples and returns a float type.
        /// </summary>
        /// <param name="samples">
        /// The input data for the prediction algorithm. Matrix [m x n], where each row
        ///          contains variables (features) of one object being classified. Should have data type CV_32F.
        /// </param>
        /// <param name="results">
        /// Predicted labels as a column matrix of type CV_32S.
        /// </param>
        /// <param name="flags">
        /// Not used.
        /// </param>
        public override float predict(Mat samples)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();

            return ml_LogisticRegression_predict_12(nativeObj, samples.nativeObj);


        }


        //
        // C++:  Mat cv::ml::LogisticRegression::get_learnt_thetas()
        //

        /// <summary>
        ///  This function returns the trained parameters arranged across rows.
        /// </summary>
        /// <remarks>
        ///      For a two class classification problem, it returns a row matrix. It returns learnt parameters of
        ///      the Logistic Regression as a matrix of type CV_32F.
        /// </remarks>
        public Mat get_learnt_thetas()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(ml_LogisticRegression_get_1learnt_1thetas_10(nativeObj)));


        }


        //
        // C++: static Ptr_LogisticRegression cv::ml::LogisticRegression::create()
        //

        /// <summary>
        ///  Creates empty model.
        /// </summary>
        /// <remarks>
        ///      Creates Logistic Regression model with parameters given.
        /// </remarks>
        public static LogisticRegression create()
        {


            return LogisticRegression.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_LogisticRegression_create_10()));


        }


        //
        // C++: static Ptr_LogisticRegression cv::ml::LogisticRegression::load(String filepath, String nodeName = String())
        //

        /// <summary>
        ///  Loads and creates a serialized LogisticRegression from a file
        /// </summary>
        /// <remarks>
        ///         Use LogisticRegression::save to serialize and store an LogisticRegression to disk.
        ///         Load the LogisticRegression from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized LogisticRegression
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static LogisticRegression load(string filepath, string nodeName)
        {


            return LogisticRegression.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_LogisticRegression_load_10(filepath, nodeName)));


        }

        /// <summary>
        ///  Loads and creates a serialized LogisticRegression from a file
        /// </summary>
        /// <remarks>
        ///         Use LogisticRegression::save to serialize and store an LogisticRegression to disk.
        ///         Load the LogisticRegression from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized LogisticRegression
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static LogisticRegression load(string filepath)
        {


            return LogisticRegression.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_LogisticRegression_load_11(filepath)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  double cv::ml::LogisticRegression::getLearningRate()
        [DllImport(LIBNAME)]
        private static extern double ml_LogisticRegression_getLearningRate_10(IntPtr nativeObj);

        // C++:  void cv::ml::LogisticRegression::setLearningRate(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setLearningRate_10(IntPtr nativeObj, double val);

        // C++:  int cv::ml::LogisticRegression::getIterations()
        [DllImport(LIBNAME)]
        private static extern int ml_LogisticRegression_getIterations_10(IntPtr nativeObj);

        // C++:  void cv::ml::LogisticRegression::setIterations(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setIterations_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::LogisticRegression::getRegularization()
        [DllImport(LIBNAME)]
        private static extern int ml_LogisticRegression_getRegularization_10(IntPtr nativeObj);

        // C++:  void cv::ml::LogisticRegression::setRegularization(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setRegularization_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::LogisticRegression::getTrainMethod()
        [DllImport(LIBNAME)]
        private static extern int ml_LogisticRegression_getTrainMethod_10(IntPtr nativeObj);

        // C++:  void cv::ml::LogisticRegression::setTrainMethod(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setTrainMethod_10(IntPtr nativeObj, int val);

        // C++:  int cv::ml::LogisticRegression::getMiniBatchSize()
        [DllImport(LIBNAME)]
        private static extern int ml_LogisticRegression_getMiniBatchSize_10(IntPtr nativeObj);

        // C++:  void cv::ml::LogisticRegression::setMiniBatchSize(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setMiniBatchSize_10(IntPtr nativeObj, int val);

        // C++:  TermCriteria cv::ml::LogisticRegression::getTermCriteria()
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_getTermCriteria_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::ml::LogisticRegression::setTermCriteria(TermCriteria val)
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_setTermCriteria_10(IntPtr nativeObj, int val_type, int val_maxCount, double val_epsilon);

        // C++:  float cv::ml::LogisticRegression::predict(Mat samples, Mat& results = Mat(), int flags = 0)
        [DllImport(LIBNAME)]
        private static extern float ml_LogisticRegression_predict_10(IntPtr nativeObj, IntPtr samples_nativeObj, IntPtr results_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern float ml_LogisticRegression_predict_11(IntPtr nativeObj, IntPtr samples_nativeObj, IntPtr results_nativeObj);
        [DllImport(LIBNAME)]
        private static extern float ml_LogisticRegression_predict_12(IntPtr nativeObj, IntPtr samples_nativeObj);

        // C++:  Mat cv::ml::LogisticRegression::get_learnt_thetas()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_LogisticRegression_get_1learnt_1thetas_10(IntPtr nativeObj);

        // C++: static Ptr_LogisticRegression cv::ml::LogisticRegression::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_LogisticRegression_create_10();

        // C++: static Ptr_LogisticRegression cv::ml::LogisticRegression::load(String filepath, String nodeName = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_LogisticRegression_load_10(string filepath, string nodeName);
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_LogisticRegression_load_11(string filepath);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_LogisticRegression_delete(IntPtr nativeObj);

    }
}
