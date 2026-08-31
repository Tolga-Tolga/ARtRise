

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class HOGDescriptor : DisposableOpenCVObject
    {


        //
        // C++:   cv::HOGDescriptor::HOGDescriptor(Size _winSize, Size _blockSize, Size _blockStride, Size _cellSize, int _nbins, int _derivAperture = 1, double _winSigma = -1, HOGDescriptor_HistogramNormType _histogramNormType = HOGDescriptor::L2Hys, double _L2HysThreshold = 0.2, bool _gammaCorrection = false, int _nlevels = HOGDescriptor::DEFAULT_NLEVELS, bool _signedGradient = false)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma, int _histogramNormType, double _L2HysThreshold, bool _gammaCorrection, int _nlevels, bool _signedGradient)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_11(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma, _histogramNormType, _L2HysThreshold, _gammaCorrection, _nlevels, _signedGradient));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma, int _histogramNormType, double _L2HysThreshold, bool _gammaCorrection, int _nlevels)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_12(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma, _histogramNormType, _L2HysThreshold, _gammaCorrection, _nlevels));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma, int _histogramNormType, double _L2HysThreshold, bool _gammaCorrection)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_13(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma, _histogramNormType, _L2HysThreshold, _gammaCorrection));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma, int _histogramNormType, double _L2HysThreshold)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_14(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma, _histogramNormType, _L2HysThreshold));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma, int _histogramNormType)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_15(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma, _histogramNormType));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture, double _winSigma)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_16(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture, _winSigma));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins, int _derivAperture)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_17(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins, _derivAperture));


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="_winSize">
        /// sets winSize with given value.
        /// </param>
        /// <param name="_blockSize">
        /// sets blockSize with given value.
        /// </param>
        /// <param name="_blockStride">
        /// sets blockStride with given value.
        /// </param>
        /// <param name="_cellSize">
        /// sets cellSize with given value.
        /// </param>
        /// <param name="_nbins">
        /// sets nbins with given value.
        /// </param>
        /// <param name="_derivAperture">
        /// sets derivAperture with given value.
        /// </param>
        /// <param name="_winSigma">
        /// sets winSigma with given value.
        /// </param>
        /// <param name="_histogramNormType">
        /// sets histogramNormType with given value.
        /// </param>
        /// <param name="_L2HysThreshold">
        /// sets L2HysThreshold with given value.
        /// </param>
        /// <param name="_gammaCorrection">
        /// sets gammaCorrection with given value.
        /// </param>
        /// <param name="_nlevels">
        /// sets nlevels with given value.
        /// </param>
        /// <param name="_signedGradient">
        /// sets signedGradient with given value.
        /// </param>
        public HOGDescriptor(in (double width, double height) _winSize, in (double width, double height) _blockSize, in (double width, double height) _blockStride, in (double width, double height) _cellSize, int _nbins)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(objdetect_HOGDescriptor_HOGDescriptor_18(_winSize.width, _winSize.height, _blockSize.width, _blockSize.height, _blockStride.width, _blockStride.height, _cellSize.width, _cellSize.height, _nbins));


        }


        //
        // C++:  void cv::HOGDescriptor::compute(Mat img, vector_float& descriptors, Size winStride = Size(), Size padding = Size(), vector_Point locations = std::vector<Point>())
        //

        /// <summary>
        ///  Computes HOG descriptors of given image.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U containing an image where HOG features will be calculated.
        /// </param>
        /// <param name="descriptors">
        /// Matrix of the type CV_32F
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="locations">
        /// Vector of Point
        /// </param>
        public void compute(Mat img, MatOfFloat descriptors, in (double width, double height) winStride, in (double width, double height) padding, MatOfPoint locations)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (descriptors != null) descriptors.ThrowIfDisposed();
            if (locations != null) locations.ThrowIfDisposed();
            Mat descriptors_mat = descriptors;
            Mat locations_mat = locations;
            objdetect_HOGDescriptor_compute_10(nativeObj, img.nativeObj, descriptors_mat.nativeObj, winStride.width, winStride.height, padding.width, padding.height, locations_mat.nativeObj);


        }

        /// <summary>
        ///  Computes HOG descriptors of given image.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U containing an image where HOG features will be calculated.
        /// </param>
        /// <param name="descriptors">
        /// Matrix of the type CV_32F
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="locations">
        /// Vector of Point
        /// </param>
        public void compute(Mat img, MatOfFloat descriptors, in (double width, double height) winStride, in (double width, double height) padding)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (descriptors != null) descriptors.ThrowIfDisposed();
            Mat descriptors_mat = descriptors;
            objdetect_HOGDescriptor_compute_11(nativeObj, img.nativeObj, descriptors_mat.nativeObj, winStride.width, winStride.height, padding.width, padding.height);


        }

        /// <summary>
        ///  Computes HOG descriptors of given image.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U containing an image where HOG features will be calculated.
        /// </param>
        /// <param name="descriptors">
        /// Matrix of the type CV_32F
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="locations">
        /// Vector of Point
        /// </param>
        public void compute(Mat img, MatOfFloat descriptors, in (double width, double height) winStride)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (descriptors != null) descriptors.ThrowIfDisposed();
            Mat descriptors_mat = descriptors;
            objdetect_HOGDescriptor_compute_12(nativeObj, img.nativeObj, descriptors_mat.nativeObj, winStride.width, winStride.height);


        }


        //
        // C++:  void cv::HOGDescriptor::detect(Mat img, vector_Point& foundLocations, vector_double& weights, double hitThreshold = 0, Size winStride = Size(), Size padding = Size(), vector_Point searchLocations = std::vector<Point>())
        //

        /// <summary>
        ///  Performs object detection without a multi-scale window.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of point where each point contains left-top corner point of detected object boundaries.
        /// </param>
        /// <param name="weights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="searchLocations">
        /// Vector of Point includes set of requested locations to be evaluated.
        /// </param>
        public void detect(Mat img, MatOfPoint foundLocations, MatOfDouble weights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding, MatOfPoint searchLocations)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (weights != null) weights.ThrowIfDisposed();
            if (searchLocations != null) searchLocations.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat weights_mat = weights;
            Mat searchLocations_mat = searchLocations;
            objdetect_HOGDescriptor_detect_10(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, weights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height, searchLocations_mat.nativeObj);


        }

        /// <summary>
        ///  Performs object detection without a multi-scale window.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of point where each point contains left-top corner point of detected object boundaries.
        /// </param>
        /// <param name="weights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="searchLocations">
        /// Vector of Point includes set of requested locations to be evaluated.
        /// </param>
        public void detect(Mat img, MatOfPoint foundLocations, MatOfDouble weights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (weights != null) weights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat weights_mat = weights;
            objdetect_HOGDescriptor_detect_11(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, weights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height);


        }

        /// <summary>
        ///  Performs object detection without a multi-scale window.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of point where each point contains left-top corner point of detected object boundaries.
        /// </param>
        /// <param name="weights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="searchLocations">
        /// Vector of Point includes set of requested locations to be evaluated.
        /// </param>
        public void detect(Mat img, MatOfPoint foundLocations, MatOfDouble weights, double hitThreshold, in (double width, double height) winStride)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (weights != null) weights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat weights_mat = weights;
            objdetect_HOGDescriptor_detect_12(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, weights_mat.nativeObj, hitThreshold, winStride.width, winStride.height);


        }


        //
        // C++:  void cv::HOGDescriptor::detectMultiScale(Mat img, vector_Rect& foundLocations, vector_double& foundWeights, double hitThreshold = 0, Size winStride = Size(), Size padding = Size(), double scale = 1.05, double groupThreshold = 2.0, bool useMeanshiftGrouping = false)
        //

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of rectangles where each rectangle contains the detected object.
        /// </param>
        /// <param name="foundWeights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="scale">
        /// Coefficient of the detection window increase.
        /// </param>
        /// <param name="groupThreshold">
        /// Coefficient to regulate the similarity threshold. When detected, some objects can be covered
        ///      by many rectangles. 0 means not to perform grouping.
        /// </param>
        /// <param name="useMeanshiftGrouping">
        /// indicates grouping algorithm
        /// </param>
        public void detectMultiScale(Mat img, MatOfRect foundLocations, MatOfDouble foundWeights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding, double scale, double groupThreshold, bool useMeanshiftGrouping)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (foundWeights != null) foundWeights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat foundWeights_mat = foundWeights;
            objdetect_HOGDescriptor_detectMultiScale_10(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, foundWeights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height, scale, groupThreshold, useMeanshiftGrouping);


        }

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of rectangles where each rectangle contains the detected object.
        /// </param>
        /// <param name="foundWeights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="scale">
        /// Coefficient of the detection window increase.
        /// </param>
        /// <param name="groupThreshold">
        /// Coefficient to regulate the similarity threshold. When detected, some objects can be covered
        ///      by many rectangles. 0 means not to perform grouping.
        /// </param>
        /// <param name="useMeanshiftGrouping">
        /// indicates grouping algorithm
        /// </param>
        public void detectMultiScale(Mat img, MatOfRect foundLocations, MatOfDouble foundWeights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding, double scale, double groupThreshold)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (foundWeights != null) foundWeights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat foundWeights_mat = foundWeights;
            objdetect_HOGDescriptor_detectMultiScale_11(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, foundWeights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height, scale, groupThreshold);


        }

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of rectangles where each rectangle contains the detected object.
        /// </param>
        /// <param name="foundWeights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="scale">
        /// Coefficient of the detection window increase.
        /// </param>
        /// <param name="groupThreshold">
        /// Coefficient to regulate the similarity threshold. When detected, some objects can be covered
        ///      by many rectangles. 0 means not to perform grouping.
        /// </param>
        /// <param name="useMeanshiftGrouping">
        /// indicates grouping algorithm
        /// </param>
        public void detectMultiScale(Mat img, MatOfRect foundLocations, MatOfDouble foundWeights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding, double scale)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (foundWeights != null) foundWeights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat foundWeights_mat = foundWeights;
            objdetect_HOGDescriptor_detectMultiScale_12(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, foundWeights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height, scale);


        }

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of rectangles where each rectangle contains the detected object.
        /// </param>
        /// <param name="foundWeights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="scale">
        /// Coefficient of the detection window increase.
        /// </param>
        /// <param name="groupThreshold">
        /// Coefficient to regulate the similarity threshold. When detected, some objects can be covered
        ///      by many rectangles. 0 means not to perform grouping.
        /// </param>
        /// <param name="useMeanshiftGrouping">
        /// indicates grouping algorithm
        /// </param>
        public void detectMultiScale(Mat img, MatOfRect foundLocations, MatOfDouble foundWeights, double hitThreshold, in (double width, double height) winStride, in (double width, double height) padding)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (foundWeights != null) foundWeights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat foundWeights_mat = foundWeights;
            objdetect_HOGDescriptor_detectMultiScale_13(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, foundWeights_mat.nativeObj, hitThreshold, winStride.width, winStride.height, padding.width, padding.height);


        }

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="img">
        /// Matrix of the type CV_8U or CV_8UC3 containing an image where objects are detected.
        /// </param>
        /// <param name="foundLocations">
        /// Vector of rectangles where each rectangle contains the detected object.
        /// </param>
        /// <param name="foundWeights">
        /// Vector that will contain confidence values for each detected object.
        /// </param>
        /// <param name="hitThreshold">
        /// Threshold for the distance between features and SVM classifying plane.
        ///      Usually it is 0 and should be specified in the detector coefficients (as the last free coefficient).
        ///      But if the free coefficient is omitted (which is allowed), you can specify it manually here.
        /// </param>
        /// <param name="winStride">
        /// Window stride. It must be a multiple of block stride.
        /// </param>
        /// <param name="padding">
        /// Padding
        /// </param>
        /// <param name="scale">
        /// Coefficient of the detection window increase.
        /// </param>
        /// <param name="groupThreshold">
        /// Coefficient to regulate the similarity threshold. When detected, some objects can be covered
        ///      by many rectangles. 0 means not to perform grouping.
        /// </param>
        /// <param name="useMeanshiftGrouping">
        /// indicates grouping algorithm
        /// </param>
        public void detectMultiScale(Mat img, MatOfRect foundLocations, MatOfDouble foundWeights, double hitThreshold, in (double width, double height) winStride)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (foundLocations != null) foundLocations.ThrowIfDisposed();
            if (foundWeights != null) foundWeights.ThrowIfDisposed();
            Mat foundLocations_mat = foundLocations;
            Mat foundWeights_mat = foundWeights;
            objdetect_HOGDescriptor_detectMultiScale_14(nativeObj, img.nativeObj, foundLocations_mat.nativeObj, foundWeights_mat.nativeObj, hitThreshold, winStride.width, winStride.height);


        }


        //
        // C++:  void cv::HOGDescriptor::computeGradient(Mat img, Mat& grad, Mat& angleOfs, Size paddingTL = Size(), Size paddingBR = Size())
        //

        /// <summary>
        ///   Computes gradients and quantized gradient orientations.
        /// </summary>
        /// <param name="img">
        /// Matrix contains the image to be computed
        /// </param>
        /// <param name="grad">
        /// Matrix of type CV_32FC2 contains computed gradients
        /// </param>
        /// <param name="angleOfs">
        /// Matrix of type CV_8UC2 contains quantized gradient orientations
        /// </param>
        /// <param name="paddingTL">
        /// Padding from top-left
        /// </param>
        /// <param name="paddingBR">
        /// Padding from bottom-right
        /// </param>
        public void computeGradient(Mat img, Mat grad, Mat angleOfs, in (double width, double height) paddingTL, in (double width, double height) paddingBR)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (grad != null) grad.ThrowIfDisposed();
            if (angleOfs != null) angleOfs.ThrowIfDisposed();

            objdetect_HOGDescriptor_computeGradient_10(nativeObj, img.nativeObj, grad.nativeObj, angleOfs.nativeObj, paddingTL.width, paddingTL.height, paddingBR.width, paddingBR.height);


        }

        /// <summary>
        ///   Computes gradients and quantized gradient orientations.
        /// </summary>
        /// <param name="img">
        /// Matrix contains the image to be computed
        /// </param>
        /// <param name="grad">
        /// Matrix of type CV_32FC2 contains computed gradients
        /// </param>
        /// <param name="angleOfs">
        /// Matrix of type CV_8UC2 contains quantized gradient orientations
        /// </param>
        /// <param name="paddingTL">
        /// Padding from top-left
        /// </param>
        /// <param name="paddingBR">
        /// Padding from bottom-right
        /// </param>
        public void computeGradient(Mat img, Mat grad, Mat angleOfs, in (double width, double height) paddingTL)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (grad != null) grad.ThrowIfDisposed();
            if (angleOfs != null) angleOfs.ThrowIfDisposed();

            objdetect_HOGDescriptor_computeGradient_11(nativeObj, img.nativeObj, grad.nativeObj, angleOfs.nativeObj, paddingTL.width, paddingTL.height);


        }


        //
        // C++: Size HOGDescriptor::winSize
        //

        public (double width, double height) get_winSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_HOGDescriptor_get_1winSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: Size HOGDescriptor::blockSize
        //

        public (double width, double height) get_blockSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_HOGDescriptor_get_1blockSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: Size HOGDescriptor::blockStride
        //

        public (double width, double height) get_blockStrideAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_HOGDescriptor_get_1blockStride_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: Size HOGDescriptor::cellSize
        //

        public (double width, double height) get_cellSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_HOGDescriptor_get_1cellSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }

    }
}
