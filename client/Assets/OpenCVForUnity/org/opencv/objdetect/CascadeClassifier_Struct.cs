

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ObjdetectModule
{
    public partial class CascadeClassifier : DisposableOpenCVObject
    {


        //
        // C++:  bool cv::CascadeClassifier::read(FileNode node)
        //

        // Unknown type 'FileNode' (I), skipping the function


        //
        // C++:  void cv::CascadeClassifier::detectMultiScale(Mat image, vector_Rect& objects, double scaleFactor = 1.1, int minNeighbors = 3, int flags = 0, Size minSize = Size(), Size maxSize = Size())
        //

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="image">
        /// Matrix of the type CV_8U containing an image where objects are detected.
        /// </param>
        /// <param name="objects">
        /// Vector of rectangles where each rectangle contains the detected object, the
        ///      rectangles may be partially outside the original image.
        /// </param>
        /// <param name="scaleFactor">
        /// Parameter specifying how much the image size is reduced at each image scale.
        /// </param>
        /// <param name="minNeighbors">
        /// Parameter specifying how many neighbors each candidate rectangle should have
        ///      to retain it.
        /// </param>
        /// <param name="flags">
        /// Parameter with the same meaning for an old cascade as in the function
        ///      cvHaarDetectObjects. It is not used for a new cascade.
        /// </param>
        /// <param name="minSize">
        /// Minimum possible object size. Objects smaller than that are ignored.
        /// </param>
        /// <param name="maxSize">
        /// Maximum possible object size. Objects larger than that are ignored. If `maxSize == minSize` model is evaluated on single scale.
        /// </param>
        public void detectMultiScale(Mat image, MatOfRect objects, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize, in Vec2d maxSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            Mat objects_mat = objects;
            objdetect_CascadeClassifier_detectMultiScale_10(nativeObj, image.nativeObj, objects_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2, maxSize.Item1, maxSize.Item2);


        }

        /// <summary>
        ///  Detects objects of different sizes in the input image. The detected objects are returned as a list
        ///      of rectangles.
        /// </summary>
        /// <param name="image">
        /// Matrix of the type CV_8U containing an image where objects are detected.
        /// </param>
        /// <param name="objects">
        /// Vector of rectangles where each rectangle contains the detected object, the
        ///      rectangles may be partially outside the original image.
        /// </param>
        /// <param name="scaleFactor">
        /// Parameter specifying how much the image size is reduced at each image scale.
        /// </param>
        /// <param name="minNeighbors">
        /// Parameter specifying how many neighbors each candidate rectangle should have
        ///      to retain it.
        /// </param>
        /// <param name="flags">
        /// Parameter with the same meaning for an old cascade as in the function
        ///      cvHaarDetectObjects. It is not used for a new cascade.
        /// </param>
        /// <param name="minSize">
        /// Minimum possible object size. Objects smaller than that are ignored.
        /// </param>
        /// <param name="maxSize">
        /// Maximum possible object size. Objects larger than that are ignored. If `maxSize == minSize` model is evaluated on single scale.
        /// </param>
        public void detectMultiScale(Mat image, MatOfRect objects, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            Mat objects_mat = objects;
            objdetect_CascadeClassifier_detectMultiScale_11(nativeObj, image.nativeObj, objects_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2);


        }


        //
        // C++:  void cv::CascadeClassifier::detectMultiScale(Mat image, vector_Rect& objects, vector_int& numDetections, double scaleFactor = 1.1, int minNeighbors = 3, int flags = 0, Size minSize = Size(), Size maxSize = Size())
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="image">
        /// Matrix of the type CV_8U containing an image where objects are detected.
        /// </param>
        /// <param name="objects">
        /// Vector of rectangles where each rectangle contains the detected object, the
        ///      rectangles may be partially outside the original image.
        /// </param>
        /// <param name="numDetections">
        /// Vector of detection numbers for the corresponding objects. An object's number
        ///      of detections is the number of neighboring positively classified rectangles that were joined
        ///      together to form the object.
        /// </param>
        /// <param name="scaleFactor">
        /// Parameter specifying how much the image size is reduced at each image scale.
        /// </param>
        /// <param name="minNeighbors">
        /// Parameter specifying how many neighbors each candidate rectangle should have
        ///      to retain it.
        /// </param>
        /// <param name="flags">
        /// Parameter with the same meaning for an old cascade as in the function
        ///      cvHaarDetectObjects. It is not used for a new cascade.
        /// </param>
        /// <param name="minSize">
        /// Minimum possible object size. Objects smaller than that are ignored.
        /// </param>
        /// <param name="maxSize">
        /// Maximum possible object size. Objects larger than that are ignored. If `maxSize == minSize` model is evaluated on single scale.
        /// </param>
        public void detectMultiScale2(Mat image, MatOfRect objects, MatOfInt numDetections, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize, in Vec2d maxSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            if (numDetections != null) numDetections.ThrowIfDisposed();
            Mat objects_mat = objects;
            Mat numDetections_mat = numDetections;
            objdetect_CascadeClassifier_detectMultiScale2_10(nativeObj, image.nativeObj, objects_mat.nativeObj, numDetections_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2, maxSize.Item1, maxSize.Item2);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="image">
        /// Matrix of the type CV_8U containing an image where objects are detected.
        /// </param>
        /// <param name="objects">
        /// Vector of rectangles where each rectangle contains the detected object, the
        ///      rectangles may be partially outside the original image.
        /// </param>
        /// <param name="numDetections">
        /// Vector of detection numbers for the corresponding objects. An object's number
        ///      of detections is the number of neighboring positively classified rectangles that were joined
        ///      together to form the object.
        /// </param>
        /// <param name="scaleFactor">
        /// Parameter specifying how much the image size is reduced at each image scale.
        /// </param>
        /// <param name="minNeighbors">
        /// Parameter specifying how many neighbors each candidate rectangle should have
        ///      to retain it.
        /// </param>
        /// <param name="flags">
        /// Parameter with the same meaning for an old cascade as in the function
        ///      cvHaarDetectObjects. It is not used for a new cascade.
        /// </param>
        /// <param name="minSize">
        /// Minimum possible object size. Objects smaller than that are ignored.
        /// </param>
        /// <param name="maxSize">
        /// Maximum possible object size. Objects larger than that are ignored. If `maxSize == minSize` model is evaluated on single scale.
        /// </param>
        public void detectMultiScale2(Mat image, MatOfRect objects, MatOfInt numDetections, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            if (numDetections != null) numDetections.ThrowIfDisposed();
            Mat objects_mat = objects;
            Mat numDetections_mat = numDetections;
            objdetect_CascadeClassifier_detectMultiScale2_11(nativeObj, image.nativeObj, objects_mat.nativeObj, numDetections_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2);


        }


        //
        // C++:  void cv::CascadeClassifier::detectMultiScale(Mat image, vector_Rect& objects, vector_int& rejectLevels, vector_double& levelWeights, double scaleFactor = 1.1, int minNeighbors = 3, int flags = 0, Size minSize = Size(), Size maxSize = Size(), bool outputRejectLevels = false)
        //

        /// <remarks>
        ///  @overload
        ///      This function allows you to retrieve the final stage decision certainty of classification.
        ///      For this, one needs to set `outputRejectLevels` on true and provide the `rejectLevels` and `levelWeights` parameter.
        ///      For each resulting detection, `levelWeights` will then contain the certainty of classification at the final stage.
        ///      This value can then be used to separate strong from weaker classifications.
        ///  
        ///      A code sample on how to use it efficiently can be found below:
        /// </remarks>
        /// <code language="c++">
        ///      Mat img;
        ///      vector<double> weights;
        ///      vector<int> levels;
        ///      vector<Rect> detections;
        ///      CascadeClassifier model("/path/to/your/model.xml");
        ///      model.detectMultiScale(img, detections, levels, weights, 1.1, 3, 0, Size(), Size(), true);
        ///      cerr << "Detection " << detections[0] << " with weight " << weights[0] << endl;
        /// </code>
        public void detectMultiScale3(Mat image, MatOfRect objects, MatOfInt rejectLevels, MatOfDouble levelWeights, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize, in Vec2d maxSize, bool outputRejectLevels)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            if (rejectLevels != null) rejectLevels.ThrowIfDisposed();
            if (levelWeights != null) levelWeights.ThrowIfDisposed();
            Mat objects_mat = objects;
            Mat rejectLevels_mat = rejectLevels;
            Mat levelWeights_mat = levelWeights;
            objdetect_CascadeClassifier_detectMultiScale3_10(nativeObj, image.nativeObj, objects_mat.nativeObj, rejectLevels_mat.nativeObj, levelWeights_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2, maxSize.Item1, maxSize.Item2, outputRejectLevels);


        }

        /// <remarks>
        ///  @overload
        ///      This function allows you to retrieve the final stage decision certainty of classification.
        ///      For this, one needs to set `outputRejectLevels` on true and provide the `rejectLevels` and `levelWeights` parameter.
        ///      For each resulting detection, `levelWeights` will then contain the certainty of classification at the final stage.
        ///      This value can then be used to separate strong from weaker classifications.
        ///  
        ///      A code sample on how to use it efficiently can be found below:
        /// </remarks>
        /// <code language="c++">
        ///      Mat img;
        ///      vector<double> weights;
        ///      vector<int> levels;
        ///      vector<Rect> detections;
        ///      CascadeClassifier model("/path/to/your/model.xml");
        ///      model.detectMultiScale(img, detections, levels, weights, 1.1, 3, 0, Size(), Size(), true);
        ///      cerr << "Detection " << detections[0] << " with weight " << weights[0] << endl;
        /// </code>
        public void detectMultiScale3(Mat image, MatOfRect objects, MatOfInt rejectLevels, MatOfDouble levelWeights, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize, in Vec2d maxSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            if (rejectLevels != null) rejectLevels.ThrowIfDisposed();
            if (levelWeights != null) levelWeights.ThrowIfDisposed();
            Mat objects_mat = objects;
            Mat rejectLevels_mat = rejectLevels;
            Mat levelWeights_mat = levelWeights;
            objdetect_CascadeClassifier_detectMultiScale3_11(nativeObj, image.nativeObj, objects_mat.nativeObj, rejectLevels_mat.nativeObj, levelWeights_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2, maxSize.Item1, maxSize.Item2);


        }

        /// <remarks>
        ///  @overload
        ///      This function allows you to retrieve the final stage decision certainty of classification.
        ///      For this, one needs to set `outputRejectLevels` on true and provide the `rejectLevels` and `levelWeights` parameter.
        ///      For each resulting detection, `levelWeights` will then contain the certainty of classification at the final stage.
        ///      This value can then be used to separate strong from weaker classifications.
        ///  
        ///      A code sample on how to use it efficiently can be found below:
        /// </remarks>
        /// <code language="c++">
        ///      Mat img;
        ///      vector<double> weights;
        ///      vector<int> levels;
        ///      vector<Rect> detections;
        ///      CascadeClassifier model("/path/to/your/model.xml");
        ///      model.detectMultiScale(img, detections, levels, weights, 1.1, 3, 0, Size(), Size(), true);
        ///      cerr << "Detection " << detections[0] << " with weight " << weights[0] << endl;
        /// </code>
        public void detectMultiScale3(Mat image, MatOfRect objects, MatOfInt rejectLevels, MatOfDouble levelWeights, double scaleFactor, int minNeighbors, int flags, in Vec2d minSize)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (objects != null) objects.ThrowIfDisposed();
            if (rejectLevels != null) rejectLevels.ThrowIfDisposed();
            if (levelWeights != null) levelWeights.ThrowIfDisposed();
            Mat objects_mat = objects;
            Mat rejectLevels_mat = rejectLevels;
            Mat levelWeights_mat = levelWeights;
            objdetect_CascadeClassifier_detectMultiScale3_12(nativeObj, image.nativeObj, objects_mat.nativeObj, rejectLevels_mat.nativeObj, levelWeights_mat.nativeObj, scaleFactor, minNeighbors, flags, minSize.Item1, minSize.Item2);


        }


        //
        // C++:  Size cv::CascadeClassifier::getOriginalWindowSize()
        //

        public Vec2d getOriginalWindowSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            objdetect_CascadeClassifier_getOriginalWindowSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }

    }
}
