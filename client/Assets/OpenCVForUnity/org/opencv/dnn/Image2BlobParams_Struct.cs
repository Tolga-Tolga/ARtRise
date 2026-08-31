#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    public partial class Image2BlobParams : DisposableOpenCVObject
    {


        //
        // C++:   cv::dnn::Image2BlobParams::Image2BlobParams(Scalar scalefactor, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, int ddepth = CV_32F, dnn_DataLayout datalayout = DNN_LAYOUT_NCHW, ImagePaddingMode mode = dnn::DNN_PMODE_NULL, Scalar borderValue = 0.0)
        //

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size, in Vec4d mean, bool swapRB, int ddepth, int datalayout, in Vec4d borderValue)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_11(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2, mean.Item1, mean.Item2, mean.Item3, mean.Item4, swapRB, ddepth, datalayout, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4));


        }

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size, in Vec4d mean, bool swapRB, int ddepth, int datalayout)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_12(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2, mean.Item1, mean.Item2, mean.Item3, mean.Item4, swapRB, ddepth, datalayout));


        }

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size, in Vec4d mean, bool swapRB, int ddepth)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_14(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2, mean.Item1, mean.Item2, mean.Item3, mean.Item4, swapRB, ddepth));


        }

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size, in Vec4d mean, bool swapRB)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_15(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2, mean.Item1, mean.Item2, mean.Item3, mean.Item4, swapRB));


        }

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size, in Vec4d mean)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_16(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2, mean.Item1, mean.Item2, mean.Item3, mean.Item4));


        }

        public Image2BlobParams(in Vec4d scalefactor, in Vec2d size)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_17(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4, size.Item1, size.Item2));


        }

        public Image2BlobParams(in Vec4d scalefactor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_18(scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4));


        }


        //
        // C++:  Rect cv::dnn::Image2BlobParams::blobRectToImageRect(Rect rBlob, Size size)
        //

        /// <summary>
        ///  Get rectangle coordinates in original image system from rectangle in blob coordinates.
        /// </summary>
        /// <param name="rBlob">
        /// rect in blob coordinates.
        /// </param>
        /// <param name="size">
        /// original input image size.
        /// </param>
        /// <returns>
        ///  rectangle in original image coordinates.
        /// </returns>
        public Vec4i blobRectToImageRectAsVec4i(in Vec4i rBlob, in Vec2d size)
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_blobRectToImageRect_10(nativeObj, rBlob.Item1, rBlob.Item2, rBlob.Item3, rBlob.Item4, size.Item1, size.Item2, tmpArray);
            Vec4i retVal = new Vec4i((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

            return retVal;
        }


        //
        // C++:  void cv::dnn::Image2BlobParams::blobRectsToImageRects(vector_Rect rBlob, vector_Rect& rImg, Size size)
        //

        /// <summary>
        ///  Get rectangle coordinates in original image system from rectangle in blob coordinates.
        /// </summary>
        /// <param name="rBlob">
        /// rect in blob coordinates.
        /// </param>
        /// <param name="rImg">
        /// result rect in image coordinates.
        /// </param>
        /// <param name="size">
        /// original input image size.
        /// </param>
        public void blobRectsToImageRects(MatOfRect rBlob, MatOfRect rImg, in Vec2d size)
        {
            ThrowIfDisposed();
            if (rBlob != null) rBlob.ThrowIfDisposed();
            if (rImg != null) rImg.ThrowIfDisposed();
            Mat rBlob_mat = rBlob;
            Mat rImg_mat = rImg;
            dnn_Image2BlobParams_blobRectsToImageRects_10(nativeObj, rBlob_mat.nativeObj, rImg_mat.nativeObj, size.Item1, size.Item2);


        }


        //
        // C++: Scalar Image2BlobParams::scalefactor
        //

        public Vec4d get_scalefactorAsVec4d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1scalefactor_10(nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::scalefactor
        //

        public void set_scalefactor(in Vec4d scalefactor)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1scalefactor_10(nativeObj, scalefactor.Item1, scalefactor.Item2, scalefactor.Item3, scalefactor.Item4);


        }


        //
        // C++: Size Image2BlobParams::size
        //

        public Vec2d get_sizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            dnn_Image2BlobParams_get_1size_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::size
        //

        public void set_size(in Vec2d size)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1size_10(nativeObj, size.Item1, size.Item2);


        }


        //
        // C++: Scalar Image2BlobParams::mean
        //

        public Vec4d get_meanAsVec4d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1mean_10(nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::mean
        //

        public void set_mean(in Vec4d mean)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1mean_10(nativeObj, mean.Item1, mean.Item2, mean.Item3, mean.Item4);


        }


        //
        // C++: Scalar Image2BlobParams::borderValue
        //

        public Vec4d get_borderValueAsVec4d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1borderValue_10(nativeObj, tmpArray);
            Vec4d retVal = new Vec4d(tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::borderValue
        //

        public void set_borderValue(in Vec4d borderValue)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1borderValue_10(nativeObj, borderValue.Item1, borderValue.Item2, borderValue.Item3, borderValue.Item4);


        }

    }
}


#endif