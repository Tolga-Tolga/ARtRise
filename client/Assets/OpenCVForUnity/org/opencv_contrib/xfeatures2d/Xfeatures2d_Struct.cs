
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.Features2dModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Xfeatures2dModule
{
    public partial class Xfeatures2d
    {

        //
        // C++:  void cv::xfeatures2d::matchGMS(Size size1, Size size2, vector_KeyPoint keypoints1, vector_KeyPoint keypoints2, vector_DMatch matches1to2, vector_DMatch& matchesGMS, bool withRotation = false, bool withScale = false, double thresholdFactor = 6.0)
        //

        /// <summary>
        ///  GMS (Grid-based Motion Statistics) feature matching strategy described in @cite Bian2017gms .
        /// </summary>
        /// <param name="size1">
        /// Input size of image1.
        /// </param>
        /// <param name="size2">
        /// Input size of image2.
        /// </param>
        /// <param name="keypoints1">
        /// Input keypoints of image1.
        /// </param>
        /// <param name="keypoints2">
        /// Input keypoints of image2.
        /// </param>
        /// <param name="matches1to2">
        /// Input 1-nearest neighbor matches.
        /// </param>
        /// <param name="matchesGMS">
        /// Matches returned by the GMS matching strategy.
        /// </param>
        /// <param name="withRotation">
        /// Take rotation transformation into account.
        /// </param>
        /// <param name="withScale">
        /// Take scale transformation into account.
        /// </param>
        /// <param name="thresholdFactor">
        /// The higher, the less matches.
        ///      @note
        ///          Since GMS works well when the number of features is large, we recommend to use the ORB feature and set FastThreshold to 0 to get as many as possible features quickly.
        ///          If matching results are not satisfying, please add more features. (We use 10000 for images with 640 X 480).
        ///          If your images have big rotation and scale changes, please set withRotation or withScale to true.
        /// </param>
        public static void matchGMS(in Vec2d size1, in Vec2d size2, MatOfKeyPoint keypoints1, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, MatOfDMatch matchesGMS, bool withRotation, bool withScale, double thresholdFactor)
        {
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (matchesGMS != null) matchesGMS.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesGMS_mat = matchesGMS;
            xfeatures2d_Xfeatures2d_matchGMS_10(size1.Item1, size1.Item2, size2.Item1, size2.Item2, keypoints1_mat.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, matchesGMS_mat.nativeObj, withRotation, withScale, thresholdFactor);


        }

        /// <summary>
        ///  GMS (Grid-based Motion Statistics) feature matching strategy described in @cite Bian2017gms .
        /// </summary>
        /// <param name="size1">
        /// Input size of image1.
        /// </param>
        /// <param name="size2">
        /// Input size of image2.
        /// </param>
        /// <param name="keypoints1">
        /// Input keypoints of image1.
        /// </param>
        /// <param name="keypoints2">
        /// Input keypoints of image2.
        /// </param>
        /// <param name="matches1to2">
        /// Input 1-nearest neighbor matches.
        /// </param>
        /// <param name="matchesGMS">
        /// Matches returned by the GMS matching strategy.
        /// </param>
        /// <param name="withRotation">
        /// Take rotation transformation into account.
        /// </param>
        /// <param name="withScale">
        /// Take scale transformation into account.
        /// </param>
        /// <param name="thresholdFactor">
        /// The higher, the less matches.
        ///      @note
        ///          Since GMS works well when the number of features is large, we recommend to use the ORB feature and set FastThreshold to 0 to get as many as possible features quickly.
        ///          If matching results are not satisfying, please add more features. (We use 10000 for images with 640 X 480).
        ///          If your images have big rotation and scale changes, please set withRotation or withScale to true.
        /// </param>
        public static void matchGMS(in Vec2d size1, in Vec2d size2, MatOfKeyPoint keypoints1, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, MatOfDMatch matchesGMS, bool withRotation, bool withScale)
        {
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (matchesGMS != null) matchesGMS.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesGMS_mat = matchesGMS;
            xfeatures2d_Xfeatures2d_matchGMS_11(size1.Item1, size1.Item2, size2.Item1, size2.Item2, keypoints1_mat.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, matchesGMS_mat.nativeObj, withRotation, withScale);


        }

        /// <summary>
        ///  GMS (Grid-based Motion Statistics) feature matching strategy described in @cite Bian2017gms .
        /// </summary>
        /// <param name="size1">
        /// Input size of image1.
        /// </param>
        /// <param name="size2">
        /// Input size of image2.
        /// </param>
        /// <param name="keypoints1">
        /// Input keypoints of image1.
        /// </param>
        /// <param name="keypoints2">
        /// Input keypoints of image2.
        /// </param>
        /// <param name="matches1to2">
        /// Input 1-nearest neighbor matches.
        /// </param>
        /// <param name="matchesGMS">
        /// Matches returned by the GMS matching strategy.
        /// </param>
        /// <param name="withRotation">
        /// Take rotation transformation into account.
        /// </param>
        /// <param name="withScale">
        /// Take scale transformation into account.
        /// </param>
        /// <param name="thresholdFactor">
        /// The higher, the less matches.
        ///      @note
        ///          Since GMS works well when the number of features is large, we recommend to use the ORB feature and set FastThreshold to 0 to get as many as possible features quickly.
        ///          If matching results are not satisfying, please add more features. (We use 10000 for images with 640 X 480).
        ///          If your images have big rotation and scale changes, please set withRotation or withScale to true.
        /// </param>
        public static void matchGMS(in Vec2d size1, in Vec2d size2, MatOfKeyPoint keypoints1, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, MatOfDMatch matchesGMS, bool withRotation)
        {
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (matchesGMS != null) matchesGMS.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesGMS_mat = matchesGMS;
            xfeatures2d_Xfeatures2d_matchGMS_12(size1.Item1, size1.Item2, size2.Item1, size2.Item2, keypoints1_mat.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, matchesGMS_mat.nativeObj, withRotation);


        }

        /// <summary>
        ///  GMS (Grid-based Motion Statistics) feature matching strategy described in @cite Bian2017gms .
        /// </summary>
        /// <param name="size1">
        /// Input size of image1.
        /// </param>
        /// <param name="size2">
        /// Input size of image2.
        /// </param>
        /// <param name="keypoints1">
        /// Input keypoints of image1.
        /// </param>
        /// <param name="keypoints2">
        /// Input keypoints of image2.
        /// </param>
        /// <param name="matches1to2">
        /// Input 1-nearest neighbor matches.
        /// </param>
        /// <param name="matchesGMS">
        /// Matches returned by the GMS matching strategy.
        /// </param>
        /// <param name="withRotation">
        /// Take rotation transformation into account.
        /// </param>
        /// <param name="withScale">
        /// Take scale transformation into account.
        /// </param>
        /// <param name="thresholdFactor">
        /// The higher, the less matches.
        ///      @note
        ///          Since GMS works well when the number of features is large, we recommend to use the ORB feature and set FastThreshold to 0 to get as many as possible features quickly.
        ///          If matching results are not satisfying, please add more features. (We use 10000 for images with 640 X 480).
        ///          If your images have big rotation and scale changes, please set withRotation or withScale to true.
        /// </param>
        public static void matchGMS(in Vec2d size1, in Vec2d size2, MatOfKeyPoint keypoints1, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, MatOfDMatch matchesGMS)
        {
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (matchesGMS != null) matchesGMS.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesGMS_mat = matchesGMS;
            xfeatures2d_Xfeatures2d_matchGMS_13(size1.Item1, size1.Item2, size2.Item1, size2.Item2, keypoints1_mat.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, matchesGMS_mat.nativeObj);


        }

    }
}
