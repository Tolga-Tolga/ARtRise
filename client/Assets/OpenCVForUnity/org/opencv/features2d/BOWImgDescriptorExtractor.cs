

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Features2dModule
{
    // C++: class BOWImgDescriptorExtractor
    /// <summary>
    ///  Class to compute an image descriptor using the *bag of visual words*.
    /// </summary>
    /// <remarks>
    ///  Such a computation consists of the following steps:
    ///  
    ///  1.  Compute descriptors for a given image and its keypoints set.
    ///  2.  Find the nearest visual words from the vocabulary for each keypoint descriptor.
    ///  3.  Compute the bag-of-words image descriptor as is a normalized histogram of vocabulary words
    ///  encountered in the image. The i-th bin of the histogram is a frequency of i-th word of the
    ///  vocabulary in the given image.
    /// </remarks>
    public class BOWImgDescriptorExtractor : DisposableOpenCVObject
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
                        features2d_BOWImgDescriptorExtractor_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal BOWImgDescriptorExtractor(IntPtr addr) : base(addr) { }


        public IntPtr getNativeObjAddr() { return nativeObj; }

        // internal usage only
        public static BOWImgDescriptorExtractor __fromPtr__(IntPtr addr) { return new BOWImgDescriptorExtractor(addr); }

        //
        // C++:   cv::BOWImgDescriptorExtractor::BOWImgDescriptorExtractor(Ptr_Feature2D dextractor, Ptr_DescriptorMatcher dmatcher)
        //

        /// <summary>
        ///  The constructor.
        /// </summary>
        /// <param name="dextractor">
        /// Descriptor extractor that is used to compute descriptors for an input image and
        ///      its keypoints.
        /// </param>
        /// <param name="dmatcher">
        /// Descriptor matcher that is used to find the nearest word of the trained vocabulary
        ///      for each keypoint descriptor of the image.
        /// </param>
        public BOWImgDescriptorExtractor(Feature2D dextractor, DescriptorMatcher dmatcher)
        {
            if (dextractor != null) dextractor.ThrowIfDisposed();
            if (dmatcher != null) dmatcher.ThrowIfDisposed();

            nativeObj = DisposableObject.ThrowIfNullIntPtr(features2d_BOWImgDescriptorExtractor_BOWImgDescriptorExtractor_10(dextractor.getNativeObjAddr(), dmatcher.getNativeObjAddr()));


        }


        //
        // C++:  void cv::BOWImgDescriptorExtractor::setVocabulary(Mat vocabulary)
        //

        /// <summary>
        ///  Sets a visual vocabulary.
        /// </summary>
        /// <param name="vocabulary">
        /// Vocabulary (can be trained using the inheritor of BOWTrainer ). Each row of the
        ///      vocabulary is a visual word (cluster center).
        /// </param>
        public void setVocabulary(Mat vocabulary)
        {
            ThrowIfDisposed();
            if (vocabulary != null) vocabulary.ThrowIfDisposed();

            features2d_BOWImgDescriptorExtractor_setVocabulary_10(nativeObj, vocabulary.nativeObj);


        }


        //
        // C++:  Mat cv::BOWImgDescriptorExtractor::getVocabulary()
        //

        /// <summary>
        ///  Returns the set vocabulary.
        /// </summary>
        public Mat getVocabulary()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(features2d_BOWImgDescriptorExtractor_getVocabulary_10(nativeObj)));


        }


        //
        // C++:  void cv::BOWImgDescriptorExtractor::compute2(Mat image, vector_KeyPoint keypoints, Mat& imgDescriptor)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="keypointDescriptors">
        /// Computed descriptors to match with vocabulary.
        /// </param>
        /// <param name="imgDescriptor">
        /// Computed output image descriptor.
        /// </param>
        /// <param name="pointIdxsOfClusters">
        /// Indices of keypoints that belong to the cluster. This means that
        ///      pointIdxsOfClusters[i] are keypoint indices that belong to the i -th cluster (word of vocabulary)
        ///      returned if it is non-zero.
        /// </param>
        public void compute(Mat image, MatOfKeyPoint keypoints, Mat imgDescriptor)
        {
            ThrowIfDisposed();
            if (image != null) image.ThrowIfDisposed();
            if (keypoints != null) keypoints.ThrowIfDisposed();
            if (imgDescriptor != null) imgDescriptor.ThrowIfDisposed();
            Mat keypoints_mat = keypoints;
            features2d_BOWImgDescriptorExtractor_compute_10(nativeObj, image.nativeObj, keypoints_mat.nativeObj, imgDescriptor.nativeObj);


        }


        //
        // C++:  int cv::BOWImgDescriptorExtractor::descriptorSize()
        //

        /// <summary>
        ///  Returns an image descriptor size if the vocabulary is set. Otherwise, it returns 0.
        /// </summary>
        public int descriptorSize()
        {
            ThrowIfDisposed();

            return features2d_BOWImgDescriptorExtractor_descriptorSize_10(nativeObj);


        }


        //
        // C++:  int cv::BOWImgDescriptorExtractor::descriptorType()
        //

        /// <summary>
        ///  Returns an image descriptor type.
        /// </summary>
        public int descriptorType()
        {
            ThrowIfDisposed();

            return features2d_BOWImgDescriptorExtractor_descriptorType_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::BOWImgDescriptorExtractor::BOWImgDescriptorExtractor(Ptr_Feature2D dextractor, Ptr_DescriptorMatcher dmatcher)
        [DllImport(LIBNAME)]
        private static extern IntPtr features2d_BOWImgDescriptorExtractor_BOWImgDescriptorExtractor_10(IntPtr dextractor_nativeObj, IntPtr dmatcher_nativeObj);

        // C++:  void cv::BOWImgDescriptorExtractor::setVocabulary(Mat vocabulary)
        [DllImport(LIBNAME)]
        private static extern void features2d_BOWImgDescriptorExtractor_setVocabulary_10(IntPtr nativeObj, IntPtr vocabulary_nativeObj);

        // C++:  Mat cv::BOWImgDescriptorExtractor::getVocabulary()
        [DllImport(LIBNAME)]
        private static extern IntPtr features2d_BOWImgDescriptorExtractor_getVocabulary_10(IntPtr nativeObj);

        // C++:  void cv::BOWImgDescriptorExtractor::compute2(Mat image, vector_KeyPoint keypoints, Mat& imgDescriptor)
        [DllImport(LIBNAME)]
        private static extern void features2d_BOWImgDescriptorExtractor_compute_10(IntPtr nativeObj, IntPtr image_nativeObj, IntPtr keypoints_mat_nativeObj, IntPtr imgDescriptor_nativeObj);

        // C++:  int cv::BOWImgDescriptorExtractor::descriptorSize()
        [DllImport(LIBNAME)]
        private static extern int features2d_BOWImgDescriptorExtractor_descriptorSize_10(IntPtr nativeObj);

        // C++:  int cv::BOWImgDescriptorExtractor::descriptorType()
        [DllImport(LIBNAME)]
        private static extern int features2d_BOWImgDescriptorExtractor_descriptorType_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void features2d_BOWImgDescriptorExtractor_delete(IntPtr nativeObj);

    }
}
