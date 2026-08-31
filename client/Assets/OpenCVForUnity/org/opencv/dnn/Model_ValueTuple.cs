#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    public partial class Model : DisposableOpenCVObject
    {


        //
        // C++:  Model cv::dnn::Model::setInputSize(Size size)
        //

        /// <summary>
        ///  Set input size for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        ///               @note If shape of the new blob less than 0, then frame size not change.
        /// </param>
        public Model setInputSize(in (double width, double height) size)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputSize_10(nativeObj, size.width, size.height)));


        }


        //
        // C++:  Model cv::dnn::Model::setInputMean(Scalar mean)
        //

        /// <summary>
        ///  Set mean value for frame.
        /// </summary>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        public Model setInputMean(in (double v0, double v1, double v2, double v3) mean)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputMean_10(nativeObj, mean.v0, mean.v1, mean.v2, mean.v3)));


        }


        //
        // C++:  Model cv::dnn::Model::setInputScale(Scalar scale)
        //

        /// <summary>
        ///  Set scalefactor value for frame.
        /// </summary>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        public Model setInputScale(in (double v0, double v1, double v2, double v3) scale)
        {
            ThrowIfDisposed();

            return new Model(DisposableObject.ThrowIfNullIntPtr(dnn_Model_setInputScale_10(nativeObj, scale.v0, scale.v1, scale.v2, scale.v3)));


        }


        //
        // C++:  void cv::dnn::Model::setInputParams(double scale = 1.0, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, bool crop = false)
        //

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB, bool crop)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_10(nativeObj, scale, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB, crop);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_11(nativeObj, scale, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_12(nativeObj, scale, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3);


        }

        /// <summary>
        ///  Set preprocessing parameters for frame.
        /// </summary>
        /// <param name="size">
        /// New input size.
        /// </param>
        /// <param name="mean">
        /// Scalar with mean values which are subtracted from channels.
        /// </param>
        /// <param name="scale">
        /// Multiplier for frame values.
        /// </param>
        /// <param name="swapRB">
        /// Flag which indicates that swap first and last channels.
        /// </param>
        /// <param name="crop">
        /// Flag which indicates whether image will be cropped after resize or not.
        ///              blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
        /// </param>
        public void setInputParams(double scale, in (double width, double height) size)
        {
            ThrowIfDisposed();

            dnn_Model_setInputParams_13(nativeObj, scale, size.width, size.height);


        }

    }
}


#endif