
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{

    // C++: class StatModel
    /// <summary>
    ///  Base class for statistical models in OpenCV ML.
    /// </summary>
    public class StatModel : Algorithm
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
                        ml_StatModel_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal StatModel(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new StatModel __fromPtr__(IntPtr addr) { return new StatModel(addr); }

        /// <summary>
        /// C++: enum Flags (cv.ml.StatModel.Flags)
        /// </summary>
        public const int UPDATE_MODEL = 1;

        /// <summary>
        /// C++: enum Flags (cv.ml.StatModel.Flags)
        /// </summary>
        public const int RAW_OUTPUT = 1;

        /// <summary>
        /// C++: enum Flags (cv.ml.StatModel.Flags)
        /// </summary>
        public const int COMPRESSED_INPUT = 2;

        /// <summary>
        /// C++: enum Flags (cv.ml.StatModel.Flags)
        /// </summary>
        public const int PREPROCESSED_INPUT = 4;


        //
        // C++:  int cv::ml::StatModel::getVarCount()
        //

        /// <summary>
        ///  Returns the number of variables in training samples
        /// </summary>
        public int getVarCount()
        {
            ThrowIfDisposed();

            return ml_StatModel_getVarCount_10(nativeObj);


        }


        //
        // C++:  bool cv::ml::StatModel::empty()
        //

        public override bool empty()
        {
            ThrowIfDisposed();

            return ml_StatModel_empty_10(nativeObj);


        }


        //
        // C++:  bool cv::ml::StatModel::isTrained()
        //

        /// <summary>
        ///  Returns true if the model is trained
        /// </summary>
        public bool isTrained()
        {
            ThrowIfDisposed();

            return ml_StatModel_isTrained_10(nativeObj);


        }


        //
        // C++:  bool cv::ml::StatModel::isClassifier()
        //

        /// <summary>
        ///  Returns true if the model is classifier
        /// </summary>
        public bool isClassifier()
        {
            ThrowIfDisposed();

            return ml_StatModel_isClassifier_10(nativeObj);


        }


        //
        // C++:  bool cv::ml::StatModel::train(Ptr_TrainData trainData, int flags = 0)
        //

        /// <summary>
        ///  Trains the statistical model
        /// </summary>
        /// <param name="trainData">
        /// training data that can be loaded from file using TrainData::loadFromCSV or
        ///          created with TrainData::create.
        /// </param>
        /// <param name="flags">
        /// optional flags, depending on the model. Some of the models can be updated with the
        ///          new training samples, not completely overwritten (such as NormalBayesClassifier or ANN_MLP).
        /// </param>
        public bool train(TrainData trainData, int flags)
        {
            ThrowIfDisposed();
            if (trainData != null) trainData.ThrowIfDisposed();

            return ml_StatModel_train_10(nativeObj, trainData.getNativeObjAddr(), flags);


        }

        /// <summary>
        ///  Trains the statistical model
        /// </summary>
        /// <param name="trainData">
        /// training data that can be loaded from file using TrainData::loadFromCSV or
        ///          created with TrainData::create.
        /// </param>
        /// <param name="flags">
        /// optional flags, depending on the model. Some of the models can be updated with the
        ///          new training samples, not completely overwritten (such as NormalBayesClassifier or ANN_MLP).
        /// </param>
        public bool train(TrainData trainData)
        {
            ThrowIfDisposed();
            if (trainData != null) trainData.ThrowIfDisposed();

            return ml_StatModel_train_11(nativeObj, trainData.getNativeObjAddr());


        }


        //
        // C++:  bool cv::ml::StatModel::train(Mat samples, int layout, Mat responses)
        //

        /// <summary>
        ///  Trains the statistical model
        /// </summary>
        /// <param name="samples">
        /// training samples
        /// </param>
        /// <param name="layout">
        /// See ml::SampleTypes.
        /// </param>
        /// <param name="responses">
        /// vector of responses associated with the training samples.
        /// </param>
        public bool train(Mat samples, int layout, Mat responses)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (responses != null) responses.ThrowIfDisposed();

            return ml_StatModel_train_12(nativeObj, samples.nativeObj, layout, responses.nativeObj);


        }


        //
        // C++:  float cv::ml::StatModel::calcError(Ptr_TrainData data, bool test, Mat& resp)
        //

        /// <summary>
        ///  Computes error on the training or test dataset
        /// </summary>
        /// <param name="data">
        /// the training data
        /// </param>
        /// <param name="test">
        /// if true, the error is computed over the test subset of the data, otherwise it's
        ///          computed over the training subset of the data. Please note that if you loaded a completely
        ///          different dataset to evaluate already trained classifier, you will probably want not to set
        ///          the test subset at all with TrainData::setTrainTestSplitRatio and specify test=false, so
        ///          that the error is computed for the whole new set. Yes, this sounds a bit confusing.
        /// </param>
        /// <param name="resp">
        /// the optional output responses.
        /// </param>
        /// <remarks>
        ///      The method uses StatModel::predict to compute the error. For regression models the error is
        ///      computed as RMS, for classifiers - as a percent of missclassified samples (0%-100%).
        /// </remarks>
        public float calcError(TrainData data, bool test, Mat resp)
        {
            ThrowIfDisposed();
            if (data != null) data.ThrowIfDisposed();
            if (resp != null) resp.ThrowIfDisposed();

            return ml_StatModel_calcError_10(nativeObj, data.getNativeObjAddr(), test, resp.nativeObj);


        }


        //
        // C++:  float cv::ml::StatModel::predict(Mat samples, Mat& results = Mat(), int flags = 0)
        //

        /// <summary>
        ///  Predicts response(s) for the provided sample(s)
        /// </summary>
        /// <param name="samples">
        /// The input samples, floating-point matrix
        /// </param>
        /// <param name="results">
        /// The optional output matrix of results.
        /// </param>
        /// <param name="flags">
        /// The optional flags, model-dependent. See cv::ml::StatModel::Flags.
        /// </param>
        public virtual float predict(Mat samples, Mat results, int flags)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (results != null) results.ThrowIfDisposed();

            return ml_StatModel_predict_10(nativeObj, samples.nativeObj, results.nativeObj, flags);


        }

        /// <summary>
        ///  Predicts response(s) for the provided sample(s)
        /// </summary>
        /// <param name="samples">
        /// The input samples, floating-point matrix
        /// </param>
        /// <param name="results">
        /// The optional output matrix of results.
        /// </param>
        /// <param name="flags">
        /// The optional flags, model-dependent. See cv::ml::StatModel::Flags.
        /// </param>
        public virtual float predict(Mat samples, Mat results)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (results != null) results.ThrowIfDisposed();

            return ml_StatModel_predict_11(nativeObj, samples.nativeObj, results.nativeObj);


        }

        /// <summary>
        ///  Predicts response(s) for the provided sample(s)
        /// </summary>
        /// <param name="samples">
        /// The input samples, floating-point matrix
        /// </param>
        /// <param name="results">
        /// The optional output matrix of results.
        /// </param>
        /// <param name="flags">
        /// The optional flags, model-dependent. See cv::ml::StatModel::Flags.
        /// </param>
        public virtual float predict(Mat samples)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();

            return ml_StatModel_predict_12(nativeObj, samples.nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  int cv::ml::StatModel::getVarCount()
        [DllImport(LIBNAME)]
        private static extern int ml_StatModel_getVarCount_10(IntPtr nativeObj);

        // C++:  bool cv::ml::StatModel::empty()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_empty_10(IntPtr nativeObj);

        // C++:  bool cv::ml::StatModel::isTrained()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_isTrained_10(IntPtr nativeObj);

        // C++:  bool cv::ml::StatModel::isClassifier()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_isClassifier_10(IntPtr nativeObj);

        // C++:  bool cv::ml::StatModel::train(Ptr_TrainData trainData, int flags = 0)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_train_10(IntPtr nativeObj, IntPtr trainData_nativeObj, int flags);
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_train_11(IntPtr nativeObj, IntPtr trainData_nativeObj);

        // C++:  bool cv::ml::StatModel::train(Mat samples, int layout, Mat responses)
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_StatModel_train_12(IntPtr nativeObj, IntPtr samples_nativeObj, int layout, IntPtr responses_nativeObj);

        // C++:  float cv::ml::StatModel::calcError(Ptr_TrainData data, bool test, Mat& resp)
        [DllImport(LIBNAME)]
        private static extern float ml_StatModel_calcError_10(IntPtr nativeObj, IntPtr data_nativeObj, [MarshalAs(UnmanagedType.U1)] bool test, IntPtr resp_nativeObj);

        // C++:  float cv::ml::StatModel::predict(Mat samples, Mat& results = Mat(), int flags = 0)
        [DllImport(LIBNAME)]
        private static extern float ml_StatModel_predict_10(IntPtr nativeObj, IntPtr samples_nativeObj, IntPtr results_nativeObj, int flags);
        [DllImport(LIBNAME)]
        private static extern float ml_StatModel_predict_11(IntPtr nativeObj, IntPtr samples_nativeObj, IntPtr results_nativeObj);
        [DllImport(LIBNAME)]
        private static extern float ml_StatModel_predict_12(IntPtr nativeObj, IntPtr samples_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_StatModel_delete(IntPtr nativeObj);

    }
}
