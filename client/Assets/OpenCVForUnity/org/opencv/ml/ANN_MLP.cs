
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{

    // C++: class ANN_MLP
    /// <summary>
    ///  Artificial Neural Networks - Multi-Layer Perceptrons.
    /// </summary>
    /// <remarks>
    ///  Unlike many other models in ML that are constructed and trained at once, in the MLP model these
    ///  steps are separated. First, a network with the specified topology is created using the non-default
    ///  constructor or the method ANN_MLP::create. All the weights are set to zeros. Then, the network is
    ///  trained using a set of input and output vectors. The training procedure can be repeated more than
    ///  once, that is, the weights can be adjusted based on the new training data.
    ///  
    ///  Additional flags for StatModel::train are available: ANN_MLP::TrainFlags.
    ///  
    ///  @sa @ref ml_intro_ann
    /// </remarks>
    public partial class ANN_MLP : StatModel
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
                        ml_ANN_1MLP_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal ANN_MLP(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new ANN_MLP __fromPtr__(IntPtr addr) { return new ANN_MLP(addr); }

        /// <summary>
        /// C++: enum ActivationFunctions (cv.ml.ANN_MLP.ActivationFunctions)
        /// </summary>
        public const int IDENTITY = 0;

        /// <summary>
        /// C++: enum ActivationFunctions (cv.ml.ANN_MLP.ActivationFunctions)
        /// </summary>
        public const int SIGMOID_SYM = 1;

        /// <summary>
        /// C++: enum ActivationFunctions (cv.ml.ANN_MLP.ActivationFunctions)
        /// </summary>
        public const int GAUSSIAN = 2;

        /// <summary>
        /// C++: enum ActivationFunctions (cv.ml.ANN_MLP.ActivationFunctions)
        /// </summary>
        public const int RELU = 3;

        /// <summary>
        /// C++: enum ActivationFunctions (cv.ml.ANN_MLP.ActivationFunctions)
        /// </summary>
        public const int LEAKYRELU = 4;


        /// <summary>
        /// C++: enum TrainFlags (cv.ml.ANN_MLP.TrainFlags)
        /// </summary>
        public const int UPDATE_WEIGHTS = 1;

        /// <summary>
        /// C++: enum TrainFlags (cv.ml.ANN_MLP.TrainFlags)
        /// </summary>
        public const int NO_INPUT_SCALE = 2;

        /// <summary>
        /// C++: enum TrainFlags (cv.ml.ANN_MLP.TrainFlags)
        /// </summary>
        public const int NO_OUTPUT_SCALE = 4;


        /// <summary>
        /// C++: enum TrainingMethods (cv.ml.ANN_MLP.TrainingMethods)
        /// </summary>
        public const int BACKPROP = 0;

        /// <summary>
        /// C++: enum TrainingMethods (cv.ml.ANN_MLP.TrainingMethods)
        /// </summary>
        public const int RPROP = 1;

        /// <summary>
        /// C++: enum TrainingMethods (cv.ml.ANN_MLP.TrainingMethods)
        /// </summary>
        public const int ANNEAL = 2;


        //
        // C++:  void cv::ml::ANN_MLP::setTrainMethod(int method, double param1 = 0, double param2 = 0)
        //

        /// <remarks>
        ///  Sets training method and common parameters.
        /// </remarks>
        /// <param name="method">
        /// Default value is ANN_MLP::RPROP. See ANN_MLP::TrainingMethods.
        /// </param>
        /// <param name="param1">
        /// passed to setRpropDW0 for ANN_MLP::RPROP and to setBackpropWeightScale for ANN_MLP::BACKPROP and to initialT for ANN_MLP::ANNEAL.
        /// </param>
        /// <param name="param2">
        /// passed to setRpropDWMin for ANN_MLP::RPROP and to setBackpropMomentumScale for ANN_MLP::BACKPROP and to finalT for ANN_MLP::ANNEAL.
        /// </param>
        public void setTrainMethod(int method, double param1, double param2)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setTrainMethod_10(nativeObj, method, param1, param2);


        }

        /// <remarks>
        ///  Sets training method and common parameters.
        /// </remarks>
        /// <param name="method">
        /// Default value is ANN_MLP::RPROP. See ANN_MLP::TrainingMethods.
        /// </param>
        /// <param name="param1">
        /// passed to setRpropDW0 for ANN_MLP::RPROP and to setBackpropWeightScale for ANN_MLP::BACKPROP and to initialT for ANN_MLP::ANNEAL.
        /// </param>
        /// <param name="param2">
        /// passed to setRpropDWMin for ANN_MLP::RPROP and to setBackpropMomentumScale for ANN_MLP::BACKPROP and to finalT for ANN_MLP::ANNEAL.
        /// </param>
        public void setTrainMethod(int method, double param1)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setTrainMethod_11(nativeObj, method, param1);


        }

        /// <remarks>
        ///  Sets training method and common parameters.
        /// </remarks>
        /// <param name="method">
        /// Default value is ANN_MLP::RPROP. See ANN_MLP::TrainingMethods.
        /// </param>
        /// <param name="param1">
        /// passed to setRpropDW0 for ANN_MLP::RPROP and to setBackpropWeightScale for ANN_MLP::BACKPROP and to initialT for ANN_MLP::ANNEAL.
        /// </param>
        /// <param name="param2">
        /// passed to setRpropDWMin for ANN_MLP::RPROP and to setBackpropMomentumScale for ANN_MLP::BACKPROP and to finalT for ANN_MLP::ANNEAL.
        /// </param>
        public void setTrainMethod(int method)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setTrainMethod_12(nativeObj, method);


        }


        //
        // C++:  int cv::ml::ANN_MLP::getTrainMethod()
        //

        /// <remarks>
        ///  Returns current training method
        /// </remarks>
        public int getTrainMethod()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getTrainMethod_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setActivationFunction(int type, double param1 = 0, double param2 = 0)
        //

        /// <remarks>
        ///  Initialize the activation function for each neuron.
        ///      Currently the default and the only fully supported activation function is ANN_MLP::SIGMOID_SYM.
        /// </remarks>
        /// <param name="type">
        /// The type of activation function. See ANN_MLP::ActivationFunctions.
        /// </param>
        /// <param name="param1">
        /// The first parameter of the activation function, \f$\alpha\f$. Default value is 0.
        /// </param>
        /// <param name="param2">
        /// The second parameter of the activation function, \f$\beta\f$. Default value is 0.
        /// </param>
        public void setActivationFunction(int type, double param1, double param2)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setActivationFunction_10(nativeObj, type, param1, param2);


        }

        /// <remarks>
        ///  Initialize the activation function for each neuron.
        ///      Currently the default and the only fully supported activation function is ANN_MLP::SIGMOID_SYM.
        /// </remarks>
        /// <param name="type">
        /// The type of activation function. See ANN_MLP::ActivationFunctions.
        /// </param>
        /// <param name="param1">
        /// The first parameter of the activation function, \f$\alpha\f$. Default value is 0.
        /// </param>
        /// <param name="param2">
        /// The second parameter of the activation function, \f$\beta\f$. Default value is 0.
        /// </param>
        public void setActivationFunction(int type, double param1)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setActivationFunction_11(nativeObj, type, param1);


        }

        /// <remarks>
        ///  Initialize the activation function for each neuron.
        ///      Currently the default and the only fully supported activation function is ANN_MLP::SIGMOID_SYM.
        /// </remarks>
        /// <param name="type">
        /// The type of activation function. See ANN_MLP::ActivationFunctions.
        /// </param>
        /// <param name="param1">
        /// The first parameter of the activation function, \f$\alpha\f$. Default value is 0.
        /// </param>
        /// <param name="param2">
        /// The second parameter of the activation function, \f$\beta\f$. Default value is 0.
        /// </param>
        public void setActivationFunction(int type)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setActivationFunction_12(nativeObj, type);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setLayerSizes(Mat _layer_sizes)
        //

        /// <remarks>
        ///  Integer vector specifying the number of neurons in each layer including the input and output layers.
        ///      The very first element specifies the number of elements in the input layer.
        ///      The last element - number of elements in the output layer. Default value is empty Mat.
        ///  @sa getLayerSizes
        /// </remarks>
        public void setLayerSizes(Mat _layer_sizes)
        {
            ThrowIfDisposed();
            if (_layer_sizes != null) _layer_sizes.ThrowIfDisposed();

            ml_ANN_1MLP_setLayerSizes_10(nativeObj, _layer_sizes.nativeObj);


        }


        //
        // C++:  Mat cv::ml::ANN_MLP::getLayerSizes()
        //

        /// <remarks>
        ///  Integer vector specifying the number of neurons in each layer including the input and output layers.
        ///      The very first element specifies the number of elements in the input layer.
        ///      The last element - number of elements in the output layer.
        ///  @sa setLayerSizes
        /// </remarks>
        public Mat getLayerSizes()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(ml_ANN_1MLP_getLayerSizes_10(nativeObj)));


        }


        //
        // C++:  TermCriteria cv::ml::ANN_MLP::getTermCriteria()
        //

        /// <remarks>
        ///  @see setTermCriteria
        /// </remarks>
        public TermCriteria getTermCriteria()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            ml_ANN_1MLP_getTermCriteria_10(nativeObj, tmpArray);
            TermCriteria retVal = new TermCriteria(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::ml::ANN_MLP::setTermCriteria(TermCriteria val)
        //

        /// <remarks>
        ///  @copybrief getTermCriteria @see getTermCriteria
        /// </remarks>
        public void setTermCriteria(TermCriteria val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setTermCriteria_10(nativeObj, val.type, val.maxCount, val.epsilon);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getBackpropWeightScale()
        //

        /// <remarks>
        ///  @see setBackpropWeightScale
        /// </remarks>
        public double getBackpropWeightScale()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getBackpropWeightScale_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setBackpropWeightScale(double val)
        //

        /// <remarks>
        ///  @copybrief getBackpropWeightScale @see getBackpropWeightScale
        /// </remarks>
        public void setBackpropWeightScale(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setBackpropWeightScale_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getBackpropMomentumScale()
        //

        /// <remarks>
        ///  @see setBackpropMomentumScale
        /// </remarks>
        public double getBackpropMomentumScale()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getBackpropMomentumScale_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setBackpropMomentumScale(double val)
        //

        /// <remarks>
        ///  @copybrief getBackpropMomentumScale @see getBackpropMomentumScale
        /// </remarks>
        public void setBackpropMomentumScale(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setBackpropMomentumScale_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getRpropDW0()
        //

        /// <remarks>
        ///  @see setRpropDW0
        /// </remarks>
        public double getRpropDW0()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getRpropDW0_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setRpropDW0(double val)
        //

        /// <remarks>
        ///  @copybrief getRpropDW0 @see getRpropDW0
        /// </remarks>
        public void setRpropDW0(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setRpropDW0_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getRpropDWPlus()
        //

        /// <remarks>
        ///  @see setRpropDWPlus
        /// </remarks>
        public double getRpropDWPlus()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getRpropDWPlus_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setRpropDWPlus(double val)
        //

        /// <remarks>
        ///  @copybrief getRpropDWPlus @see getRpropDWPlus
        /// </remarks>
        public void setRpropDWPlus(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setRpropDWPlus_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getRpropDWMinus()
        //

        /// <remarks>
        ///  @see setRpropDWMinus
        /// </remarks>
        public double getRpropDWMinus()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getRpropDWMinus_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setRpropDWMinus(double val)
        //

        /// <remarks>
        ///  @copybrief getRpropDWMinus @see getRpropDWMinus
        /// </remarks>
        public void setRpropDWMinus(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setRpropDWMinus_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getRpropDWMin()
        //

        /// <remarks>
        ///  @see setRpropDWMin
        /// </remarks>
        public double getRpropDWMin()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getRpropDWMin_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setRpropDWMin(double val)
        //

        /// <remarks>
        ///  @copybrief getRpropDWMin @see getRpropDWMin
        /// </remarks>
        public void setRpropDWMin(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setRpropDWMin_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getRpropDWMax()
        //

        /// <remarks>
        ///  @see setRpropDWMax
        /// </remarks>
        public double getRpropDWMax()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getRpropDWMax_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setRpropDWMax(double val)
        //

        /// <remarks>
        ///  @copybrief getRpropDWMax @see getRpropDWMax
        /// </remarks>
        public void setRpropDWMax(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setRpropDWMax_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getAnnealInitialT()
        //

        /// <remarks>
        ///  @see setAnnealInitialT
        /// </remarks>
        public double getAnnealInitialT()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getAnnealInitialT_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setAnnealInitialT(double val)
        //

        /// <remarks>
        ///  @copybrief getAnnealInitialT @see getAnnealInitialT
        /// </remarks>
        public void setAnnealInitialT(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setAnnealInitialT_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getAnnealFinalT()
        //

        /// <remarks>
        ///  @see setAnnealFinalT
        /// </remarks>
        public double getAnnealFinalT()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getAnnealFinalT_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setAnnealFinalT(double val)
        //

        /// <remarks>
        ///  @copybrief getAnnealFinalT @see getAnnealFinalT
        /// </remarks>
        public void setAnnealFinalT(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setAnnealFinalT_10(nativeObj, val);


        }


        //
        // C++:  double cv::ml::ANN_MLP::getAnnealCoolingRatio()
        //

        /// <remarks>
        ///  @see setAnnealCoolingRatio
        /// </remarks>
        public double getAnnealCoolingRatio()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getAnnealCoolingRatio_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setAnnealCoolingRatio(double val)
        //

        /// <remarks>
        ///  @copybrief getAnnealCoolingRatio @see getAnnealCoolingRatio
        /// </remarks>
        public void setAnnealCoolingRatio(double val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setAnnealCoolingRatio_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::ANN_MLP::getAnnealItePerStep()
        //

        /// <remarks>
        ///  @see setAnnealItePerStep
        /// </remarks>
        public int getAnnealItePerStep()
        {
            ThrowIfDisposed();

            return ml_ANN_1MLP_getAnnealItePerStep_10(nativeObj);


        }


        //
        // C++:  void cv::ml::ANN_MLP::setAnnealItePerStep(int val)
        //

        /// <remarks>
        ///  @copybrief getAnnealItePerStep @see getAnnealItePerStep
        /// </remarks>
        public void setAnnealItePerStep(int val)
        {
            ThrowIfDisposed();

            ml_ANN_1MLP_setAnnealItePerStep_10(nativeObj, val);


        }


        //
        // C++:  Mat cv::ml::ANN_MLP::getWeights(int layerIdx)
        //

        public Mat getWeights(int layerIdx)
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(ml_ANN_1MLP_getWeights_10(nativeObj, layerIdx)));


        }


        //
        // C++: static Ptr_ANN_MLP cv::ml::ANN_MLP::create()
        //

        /// <summary>
        ///  Creates empty model
        /// </summary>
        /// <remarks>
        ///      Use StatModel::train to train the model, Algorithm::load&lt;ANN_MLP&gt;(filename) to load the pre-trained model.
        ///      Note that the train method has optional flags: ANN_MLP::TrainFlags.
        /// </remarks>
        public static ANN_MLP create()
        {


            return ANN_MLP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_ANN_1MLP_create_10()));


        }


        //
        // C++: static Ptr_ANN_MLP cv::ml::ANN_MLP::load(String filepath)
        //

        /// <summary>
        ///  Loads and creates a serialized ANN from a file
        /// </summary>
        /// <remarks>
        ///         Use ANN::save to serialize and store an ANN to disk.
        ///         Load the ANN from this file again, by calling this function with the path to the file.
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized ANN
        /// </param>
        public static ANN_MLP load(string filepath)
        {


            return ANN_MLP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_ANN_1MLP_load_10(filepath)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ml::ANN_MLP::setTrainMethod(int method, double param1 = 0, double param2 = 0)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setTrainMethod_10(IntPtr nativeObj, int method, double param1, double param2);
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setTrainMethod_11(IntPtr nativeObj, int method, double param1);
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setTrainMethod_12(IntPtr nativeObj, int method);

        // C++:  int cv::ml::ANN_MLP::getTrainMethod()
        [DllImport(LIBNAME)]
        private static extern int ml_ANN_1MLP_getTrainMethod_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setActivationFunction(int type, double param1 = 0, double param2 = 0)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setActivationFunction_10(IntPtr nativeObj, int type, double param1, double param2);
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setActivationFunction_11(IntPtr nativeObj, int type, double param1);
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setActivationFunction_12(IntPtr nativeObj, int type);

        // C++:  void cv::ml::ANN_MLP::setLayerSizes(Mat _layer_sizes)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setLayerSizes_10(IntPtr nativeObj, IntPtr _layer_sizes_nativeObj);

        // C++:  Mat cv::ml::ANN_MLP::getLayerSizes()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_ANN_1MLP_getLayerSizes_10(IntPtr nativeObj);

        // C++:  TermCriteria cv::ml::ANN_MLP::getTermCriteria()
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_getTermCriteria_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::ml::ANN_MLP::setTermCriteria(TermCriteria val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setTermCriteria_10(IntPtr nativeObj, int val_type, int val_maxCount, double val_epsilon);

        // C++:  double cv::ml::ANN_MLP::getBackpropWeightScale()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getBackpropWeightScale_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setBackpropWeightScale(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setBackpropWeightScale_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getBackpropMomentumScale()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getBackpropMomentumScale_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setBackpropMomentumScale(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setBackpropMomentumScale_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getRpropDW0()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getRpropDW0_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setRpropDW0(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setRpropDW0_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getRpropDWPlus()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getRpropDWPlus_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setRpropDWPlus(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setRpropDWPlus_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getRpropDWMinus()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getRpropDWMinus_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setRpropDWMinus(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setRpropDWMinus_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getRpropDWMin()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getRpropDWMin_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setRpropDWMin(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setRpropDWMin_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getRpropDWMax()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getRpropDWMax_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setRpropDWMax(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setRpropDWMax_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getAnnealInitialT()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getAnnealInitialT_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setAnnealInitialT(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setAnnealInitialT_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getAnnealFinalT()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getAnnealFinalT_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setAnnealFinalT(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setAnnealFinalT_10(IntPtr nativeObj, double val);

        // C++:  double cv::ml::ANN_MLP::getAnnealCoolingRatio()
        [DllImport(LIBNAME)]
        private static extern double ml_ANN_1MLP_getAnnealCoolingRatio_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setAnnealCoolingRatio(double val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setAnnealCoolingRatio_10(IntPtr nativeObj, double val);

        // C++:  int cv::ml::ANN_MLP::getAnnealItePerStep()
        [DllImport(LIBNAME)]
        private static extern int ml_ANN_1MLP_getAnnealItePerStep_10(IntPtr nativeObj);

        // C++:  void cv::ml::ANN_MLP::setAnnealItePerStep(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_setAnnealItePerStep_10(IntPtr nativeObj, int val);

        // C++:  Mat cv::ml::ANN_MLP::getWeights(int layerIdx)
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_ANN_1MLP_getWeights_10(IntPtr nativeObj, int layerIdx);

        // C++: static Ptr_ANN_MLP cv::ml::ANN_MLP::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_ANN_1MLP_create_10();

        // C++: static Ptr_ANN_MLP cv::ml::ANN_MLP::load(String filepath)
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_ANN_1MLP_load_10(string filepath);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_ANN_1MLP_delete(IntPtr nativeObj);

    }
}
