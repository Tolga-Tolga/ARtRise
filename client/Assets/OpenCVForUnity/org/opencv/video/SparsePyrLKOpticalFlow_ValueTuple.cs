
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{
    public partial class SparsePyrLKOpticalFlow : SparseOpticalFlow
    {

        //
        // C++:  Size cv::SparsePyrLKOpticalFlow::getWinSize()
        //

        public (double width, double height) getWinSizeAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            video_SparsePyrLKOpticalFlow_getWinSize_10(nativeObj, tmpArray);
            (double width, double height) retVal = (tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++:  void cv::SparsePyrLKOpticalFlow::setWinSize(Size winSize)
        //

        public void setWinSize(in (double width, double height) winSize)
        {
            ThrowIfDisposed();

            video_SparsePyrLKOpticalFlow_setWinSize_10(nativeObj, winSize.width, winSize.height);


        }


        //
        // C++:  TermCriteria cv::SparsePyrLKOpticalFlow::getTermCriteria()
        //

        public (double type, double maxCount, double epsilon) getTermCriteriaAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            video_SparsePyrLKOpticalFlow_getTermCriteria_10(nativeObj, tmpArray);
            (double type, double maxCount, double epsilon) retVal = (tmpArray[0], tmpArray[1], tmpArray[2]);

            return retVal;
        }


        //
        // C++:  void cv::SparsePyrLKOpticalFlow::setTermCriteria(TermCriteria crit)
        //

        public void setTermCriteria(in (double type, double maxCount, double epsilon) crit)
        {
            ThrowIfDisposed();

            video_SparsePyrLKOpticalFlow_setTermCriteria_10(nativeObj, (int)crit.type, (int)crit.maxCount, crit.epsilon);


        }


        //
        // C++: static Ptr_SparsePyrLKOpticalFlow cv::SparsePyrLKOpticalFlow::create(Size winSize = Size(21, 21), int maxLevel = 3, TermCriteria crit = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 0.01), int flags = 0, double minEigThreshold = 1e-4)
        //

        public static SparsePyrLKOpticalFlow create(in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) crit, int flags, double minEigThreshold)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_10(winSize.width, winSize.height, maxLevel, (int)crit.type, (int)crit.maxCount, crit.epsilon, flags, minEigThreshold)));


        }

        public static SparsePyrLKOpticalFlow create(in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) crit, int flags)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_11(winSize.width, winSize.height, maxLevel, (int)crit.type, (int)crit.maxCount, crit.epsilon, flags)));


        }

        public static SparsePyrLKOpticalFlow create(in (double width, double height) winSize, int maxLevel, in (double type, double maxCount, double epsilon) crit)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_12(winSize.width, winSize.height, maxLevel, (int)crit.type, (int)crit.maxCount, crit.epsilon)));


        }

        public static SparsePyrLKOpticalFlow create(in (double width, double height) winSize, int maxLevel)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_13(winSize.width, winSize.height, maxLevel)));


        }

        public static SparsePyrLKOpticalFlow create(in (double width, double height) winSize)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_14(winSize.width, winSize.height)));


        }

    }
}
