
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Features2dModule
{
    public partial class Features2d
    {


        //
        // C++:  void cv::drawKeypoints(Mat image, vector_KeyPoint keypoints, Mat& outImage, Scalar color = Scalar::all(-1), DrawMatchesFlags flags = DrawMatchesFlags::DEFAULT)
        //

        /// <summary>
        ///  Draws keypoints.
        /// </summary>
        /// <param name="image">
        /// Source image.
        /// </param>
        /// <param name="keypoints">
        /// Keypoints from the source image.
        /// </param>
        /// <param name="outImage">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="color">
        /// Color of keypoints.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags. See details above in drawMatches .
        /// </param>
        /// <remarks>
        ///  @note
        ///  For Python API, flags are modified as cv.DRAW_MATCHES_FLAGS_DEFAULT,
        ///  cv.DRAW_MATCHES_FLAGS_DRAW_RICH_KEYPOINTS, cv.DRAW_MATCHES_FLAGS_DRAW_OVER_OUTIMG,
        ///  cv.DRAW_MATCHES_FLAGS_NOT_DRAW_SINGLE_POINTS
        /// </remarks>
        public static void drawKeypoints(Mat image, MatOfKeyPoint keypoints, Mat outImage, in Vec4d color, int flags)
        {
            if (image != null) image.ThrowIfDisposed();
            if (keypoints != null) keypoints.ThrowIfDisposed();
            if (outImage != null) outImage.ThrowIfDisposed();
            Mat keypoints_mat = keypoints;
            features2d_Features2d_drawKeypoints_10(image.nativeObj, keypoints_mat.nativeObj, outImage.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4, flags);


        }

        /// <summary>
        ///  Draws keypoints.
        /// </summary>
        /// <param name="image">
        /// Source image.
        /// </param>
        /// <param name="keypoints">
        /// Keypoints from the source image.
        /// </param>
        /// <param name="outImage">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="color">
        /// Color of keypoints.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags. See details above in drawMatches .
        /// </param>
        /// <remarks>
        ///  @note
        ///  For Python API, flags are modified as cv.DRAW_MATCHES_FLAGS_DEFAULT,
        ///  cv.DRAW_MATCHES_FLAGS_DRAW_RICH_KEYPOINTS, cv.DRAW_MATCHES_FLAGS_DRAW_OVER_OUTIMG,
        ///  cv.DRAW_MATCHES_FLAGS_NOT_DRAW_SINGLE_POINTS
        /// </remarks>
        public static void drawKeypoints(Mat image, MatOfKeyPoint keypoints, Mat outImage, in Vec4d color)
        {
            if (image != null) image.ThrowIfDisposed();
            if (keypoints != null) keypoints.ThrowIfDisposed();
            if (outImage != null) outImage.ThrowIfDisposed();
            Mat keypoints_mat = keypoints;
            features2d_Features2d_drawKeypoints_11(image.nativeObj, keypoints_mat.nativeObj, outImage.nativeObj, color.Item1, color.Item2, color.Item3, color.Item4);


        }


        //
        // C++:  void cv::drawMatches(Mat img1, vector_KeyPoint keypoints1, Mat img2, vector_KeyPoint keypoints2, vector_DMatch matches1to2, Mat& outImg, Scalar matchColor = Scalar::all(-1), Scalar singlePointColor = Scalar::all(-1), vector_char matchesMask = std::vector<char>(), DrawMatchesFlags flags = DrawMatchesFlags::DEFAULT)
        //

        /// <summary>
        ///  Draws the found matches of keypoints from two images.
        /// </summary>
        /// <param name="img1">
        /// First source image.
        /// </param>
        /// <param name="keypoints1">
        /// Keypoints from the first source image.
        /// </param>
        /// <param name="img2">
        /// Second source image.
        /// </param>
        /// <param name="keypoints2">
        /// Keypoints from the second source image.
        /// </param>
        /// <param name="matches1to2">
        /// Matches from the first image to the second one, which means that keypoints1[i]
        ///  has a corresponding point in keypoints2[matches[i]] .
        /// </param>
        /// <param name="outImg">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="matchColor">
        /// Color of matches (lines and connected keypoints). If matchColor==Scalar::all(-1)
        ///  , the color is generated randomly.
        /// </param>
        /// <param name="singlePointColor">
        /// Color of single keypoints (circles), which means that keypoints do not
        ///  have the matches. If singlePointColor==Scalar::all(-1) , the color is generated randomly.
        /// </param>
        /// <param name="matchesMask">
        /// Mask determining which matches are drawn. If the mask is empty, all matches are
        ///  drawn.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags.
        /// </param>
        /// <remarks>
        ///  This function draws matches of keypoints from two images in the output image. Match is a line
        ///  connecting two keypoints (circles). See cv::DrawMatchesFlags.
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor, MatOfByte matchesMask, int flags)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            if (matchesMask != null) matchesMask.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesMask_mat = matchesMask;
            features2d_Features2d_drawMatches_10(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj, flags);


        }

        /// <summary>
        ///  Draws the found matches of keypoints from two images.
        /// </summary>
        /// <param name="img1">
        /// First source image.
        /// </param>
        /// <param name="keypoints1">
        /// Keypoints from the first source image.
        /// </param>
        /// <param name="img2">
        /// Second source image.
        /// </param>
        /// <param name="keypoints2">
        /// Keypoints from the second source image.
        /// </param>
        /// <param name="matches1to2">
        /// Matches from the first image to the second one, which means that keypoints1[i]
        ///  has a corresponding point in keypoints2[matches[i]] .
        /// </param>
        /// <param name="outImg">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="matchColor">
        /// Color of matches (lines and connected keypoints). If matchColor==Scalar::all(-1)
        ///  , the color is generated randomly.
        /// </param>
        /// <param name="singlePointColor">
        /// Color of single keypoints (circles), which means that keypoints do not
        ///  have the matches. If singlePointColor==Scalar::all(-1) , the color is generated randomly.
        /// </param>
        /// <param name="matchesMask">
        /// Mask determining which matches are drawn. If the mask is empty, all matches are
        ///  drawn.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags.
        /// </param>
        /// <remarks>
        ///  This function draws matches of keypoints from two images in the output image. Match is a line
        ///  connecting two keypoints (circles). See cv::DrawMatchesFlags.
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor, MatOfByte matchesMask)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            if (matchesMask != null) matchesMask.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesMask_mat = matchesMask;
            features2d_Features2d_drawMatches_11(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj);


        }

        /// <summary>
        ///  Draws the found matches of keypoints from two images.
        /// </summary>
        /// <param name="img1">
        /// First source image.
        /// </param>
        /// <param name="keypoints1">
        /// Keypoints from the first source image.
        /// </param>
        /// <param name="img2">
        /// Second source image.
        /// </param>
        /// <param name="keypoints2">
        /// Keypoints from the second source image.
        /// </param>
        /// <param name="matches1to2">
        /// Matches from the first image to the second one, which means that keypoints1[i]
        ///  has a corresponding point in keypoints2[matches[i]] .
        /// </param>
        /// <param name="outImg">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="matchColor">
        /// Color of matches (lines and connected keypoints). If matchColor==Scalar::all(-1)
        ///  , the color is generated randomly.
        /// </param>
        /// <param name="singlePointColor">
        /// Color of single keypoints (circles), which means that keypoints do not
        ///  have the matches. If singlePointColor==Scalar::all(-1) , the color is generated randomly.
        /// </param>
        /// <param name="matchesMask">
        /// Mask determining which matches are drawn. If the mask is empty, all matches are
        ///  drawn.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags.
        /// </param>
        /// <remarks>
        ///  This function draws matches of keypoints from two images in the output image. Match is a line
        ///  connecting two keypoints (circles). See cv::DrawMatchesFlags.
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            features2d_Features2d_drawMatches_12(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4);


        }

        /// <summary>
        ///  Draws the found matches of keypoints from two images.
        /// </summary>
        /// <param name="img1">
        /// First source image.
        /// </param>
        /// <param name="keypoints1">
        /// Keypoints from the first source image.
        /// </param>
        /// <param name="img2">
        /// Second source image.
        /// </param>
        /// <param name="keypoints2">
        /// Keypoints from the second source image.
        /// </param>
        /// <param name="matches1to2">
        /// Matches from the first image to the second one, which means that keypoints1[i]
        ///  has a corresponding point in keypoints2[matches[i]] .
        /// </param>
        /// <param name="outImg">
        /// Output image. Its content depends on the flags value defining what is drawn in the
        ///  output image. See possible flags bit values below.
        /// </param>
        /// <param name="matchColor">
        /// Color of matches (lines and connected keypoints). If matchColor==Scalar::all(-1)
        ///  , the color is generated randomly.
        /// </param>
        /// <param name="singlePointColor">
        /// Color of single keypoints (circles), which means that keypoints do not
        ///  have the matches. If singlePointColor==Scalar::all(-1) , the color is generated randomly.
        /// </param>
        /// <param name="matchesMask">
        /// Mask determining which matches are drawn. If the mask is empty, all matches are
        ///  drawn.
        /// </param>
        /// <param name="flags">
        /// Flags setting drawing features. Possible flags bit values are defined by
        ///  DrawMatchesFlags.
        /// </param>
        /// <remarks>
        ///  This function draws matches of keypoints from two images in the output image. Match is a line
        ///  connecting two keypoints (circles). See cv::DrawMatchesFlags.
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, in Vec4d matchColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            features2d_Features2d_drawMatches_13(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4);


        }


        //
        // C++:  void cv::drawMatches(Mat img1, vector_KeyPoint keypoints1, Mat img2, vector_KeyPoint keypoints2, vector_DMatch matches1to2, Mat& outImg, int matchesThickness, Scalar matchColor = Scalar::all(-1), Scalar singlePointColor = Scalar::all(-1), vector_char matchesMask = std::vector<char>(), DrawMatchesFlags flags = DrawMatchesFlags::DEFAULT)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, int matchesThickness, in Vec4d matchColor, in Vec4d singlePointColor, MatOfByte matchesMask, int flags)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            if (matchesMask != null) matchesMask.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesMask_mat = matchesMask;
            features2d_Features2d_drawMatches_15(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchesThickness, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj, flags);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, int matchesThickness, in Vec4d matchColor, in Vec4d singlePointColor, MatOfByte matchesMask)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            if (matchesMask != null) matchesMask.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            Mat matchesMask_mat = matchesMask;
            features2d_Features2d_drawMatches_16(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchesThickness, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, int matchesThickness, in Vec4d matchColor, in Vec4d singlePointColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            features2d_Features2d_drawMatches_17(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchesThickness, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4);


        }

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static void drawMatches(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, MatOfDMatch matches1to2, Mat outImg, int matchesThickness, in Vec4d matchColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (matches1to2 != null) matches1to2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            Mat matches1to2_mat = matches1to2;
            features2d_Features2d_drawMatches_18(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchesThickness, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4);


        }


        //
        // C++:  void cv::drawMatches(Mat img1, vector_KeyPoint keypoints1, Mat img2, vector_KeyPoint keypoints2, vector_vector_DMatch matches1to2, Mat& outImg, Scalar matchColor = Scalar::all(-1), Scalar singlePointColor = Scalar::all(-1), vector_vector_char matchesMask = std::vector<std::vector<char> >(), DrawMatchesFlags flags = DrawMatchesFlags::DEFAULT)
        //

        public static void drawMatchesKnn(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, List<MatOfDMatch> matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor, List<MatOfByte> matchesMask, int flags)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            using Mat matches1to2_mat = Converters.vector_vector_DMatch_to_Mat(matches1to2);
            using Mat matchesMask_mat = Converters.vector_vector_char_to_Mat(matchesMask);
            features2d_Features2d_drawMatchesKnn_10(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj, flags);


        }

        public static void drawMatchesKnn(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, List<MatOfDMatch> matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor, List<MatOfByte> matchesMask)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            using Mat matches1to2_mat = Converters.vector_vector_DMatch_to_Mat(matches1to2);
            using Mat matchesMask_mat = Converters.vector_vector_char_to_Mat(matchesMask);
            features2d_Features2d_drawMatchesKnn_11(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4, matchesMask_mat.nativeObj);


        }

        public static void drawMatchesKnn(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, List<MatOfDMatch> matches1to2, Mat outImg, in Vec4d matchColor, in Vec4d singlePointColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            using Mat matches1to2_mat = Converters.vector_vector_DMatch_to_Mat(matches1to2);
            features2d_Features2d_drawMatchesKnn_12(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4, singlePointColor.Item1, singlePointColor.Item2, singlePointColor.Item3, singlePointColor.Item4);


        }

        public static void drawMatchesKnn(Mat img1, MatOfKeyPoint keypoints1, Mat img2, MatOfKeyPoint keypoints2, List<MatOfDMatch> matches1to2, Mat outImg, in Vec4d matchColor)
        {
            if (img1 != null) img1.ThrowIfDisposed();
            if (keypoints1 != null) keypoints1.ThrowIfDisposed();
            if (img2 != null) img2.ThrowIfDisposed();
            if (keypoints2 != null) keypoints2.ThrowIfDisposed();
            if (outImg != null) outImg.ThrowIfDisposed();
            Mat keypoints1_mat = keypoints1;
            Mat keypoints2_mat = keypoints2;
            using Mat matches1to2_mat = Converters.vector_vector_DMatch_to_Mat(matches1to2);
            features2d_Features2d_drawMatchesKnn_13(img1.nativeObj, keypoints1_mat.nativeObj, img2.nativeObj, keypoints2_mat.nativeObj, matches1to2_mat.nativeObj, outImg.nativeObj, matchColor.Item1, matchColor.Item2, matchColor.Item3, matchColor.Item4);


        }

    }
}
