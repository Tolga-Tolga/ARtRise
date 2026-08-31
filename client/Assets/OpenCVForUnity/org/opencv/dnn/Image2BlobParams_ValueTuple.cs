#if !UNITY_WSA_10_0



using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.DnnModule
{
    public partial class Image2BlobParams : DisposableOpenCVObject
    {


        //
        // C++:   cv::dnn::Image2BlobParams::Image2BlobParams(Scalar scalefactor, Size size = Size(), Scalar mean = Scalar(), bool swapRB = false, int ddepth = CV_32F, dnn_DataLayout datalayout = DNN_LAYOUT_NCHW, ImagePaddingMode mode = dnn::DNN_PMODE_NULL, Scalar borderValue = 0.0)
        //

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB, int ddepth, int datalayout, in (double v0, double v1, double v2, double v3) borderValue)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_11(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB, ddepth, datalayout, borderValue.v0, borderValue.v1, borderValue.v2, borderValue.v3));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB, int ddepth, int datalayout)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_12(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB, ddepth, datalayout));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB, int ddepth)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_14(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB, ddepth));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean, bool swapRB)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_15(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3, swapRB));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size, in (double v0, double v1, double v2, double v3) mean)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_16(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height, mean.v0, mean.v1, mean.v2, mean.v3));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor, in (double width, double height) size)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_17(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3, size.width, size.height));


        }

        public Image2BlobParams(in (double v0, double v1, double v2, double v3) scalefactor)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(dnn_Image2BlobParams_Image2BlobParams_18(scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3));


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
        public (int x, int y, int width, int height) blobRectToImageRectAsValueTuple(in (int x, int y, int width, int height) rBlob, in (double width, double height) size)
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_blobRectToImageRect_10(nativeObj, rBlob.x, rBlob.y, rBlob.width, rBlob.height, size.width, size.height, tmpArray);
            (int x, int y, int width, int height) retVal = ((int)tmpArray[0], (int)tmpArray[1], (int)tmpArray[2], (int)tmpArray[3]);

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
        public void blobRectsToImageRects(MatOfRect rBlob, MatOfRect rImg, in (double width, double height) size)
        {
            ThrowIfDisposed();
            if (rBlob != null) rBlob.ThrowIfDisposed();
            if (rImg != null) rImg.ThrowIfDisposed();
            Mat rBlob_mat = rBlob;
            Mat rImg_mat = rImg;
            dnn_Image2BlobParams_blobRectsToImageRects_10(nativeObj, rBlob_mat.nativeObj, rImg_mat.nativeObj, size.width, size.height);


        }


        //
        // C++: Scalar Image2BlobParams::scalefactor
        //

        public (double v0, double v1, double v2, double v3) get_scalefactorAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1scalefactor_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::scalefactor
        //

        public void set_scalefactor(in (double v0, double v1, double v2, double v3) scalefactor)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1scalefactor_10(nativeObj, scalefactor.v0, scalefactor.v1, scalefactor.v2, scalefactor.v3);


        }


        //
        // C++: Size Image2BlobParams::size
        //

        public (double width, double height) get_sizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            dnn_Image2BlobParams_get_1size_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::size
        //

        public void set_size(in (double width, double height) size)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1size_10(nativeObj, size.width, size.height);


        }


        //
        // C++: Scalar Image2BlobParams::mean
        //

        public (double v0, double v1, double v2, double v3) get_meanAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1mean_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::mean
        //

        public void set_mean(in (double v0, double v1, double v2, double v3) mean)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1mean_10(nativeObj, mean.v0, mean.v1, mean.v2, mean.v3);


        }


        //
        // C++: Scalar Image2BlobParams::borderValue
        //

        public (double v0, double v1, double v2, double v3) get_borderValueAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[4];
            dnn_Image2BlobParams_get_1borderValue_10(nativeObj, tmpArray);
            (double v0, double v1, double v2, double v3) retVal = (tmpArray[0], tmpArray[1], tmpArray[2], tmpArray[3]);

            return retVal;
        }


        //
        // C++: void Image2BlobParams::borderValue
        //

        public void set_borderValue(in (double v0, double v1, double v2, double v3) borderValue)
        {
            ThrowIfDisposed();

            dnn_Image2BlobParams_set_1borderValue_10(nativeObj, borderValue.v0, borderValue.v1, borderValue.v2, borderValue.v3);


        }

    }
}


#endif