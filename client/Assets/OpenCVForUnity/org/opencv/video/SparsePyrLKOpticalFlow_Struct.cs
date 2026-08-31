
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
#if !UNITY_WSA_10_0
using OpenCVForUnity.DnnModule;
#endif
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.VideoModule
{
    public partial class SparsePyrLKOpticalFlow : SparseOpticalFlow
    {

        //
        // C++:  Size cv::SparsePyrLKOpticalFlow::getWinSize()
        //

        public Vec2d getWinSizeAsVec2d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            video_SparsePyrLKOpticalFlow_getWinSize_10(nativeObj, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++:  void cv::SparsePyrLKOpticalFlow::setWinSize(Size winSize)
        //

        public void setWinSize(in Vec2d winSize)
        {
            ThrowIfDisposed();

            video_SparsePyrLKOpticalFlow_setWinSize_10(nativeObj, winSize.Item1, winSize.Item2);


        }


        //
        // C++:  TermCriteria cv::SparsePyrLKOpticalFlow::getTermCriteria()
        //

        public Vec3d getTermCriteriaAsVec3d()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            video_SparsePyrLKOpticalFlow_getTermCriteria_10(nativeObj, tmpArray);
            Vec3d retVal = new Vec3d(tmpArray[0], tmpArray[1], tmpArray[2]);

            return retVal;
        }


        //
        // C++:  void cv::SparsePyrLKOpticalFlow::setTermCriteria(TermCriteria crit)
        //

        public void setTermCriteria(in Vec3d crit)
        {
            ThrowIfDisposed();

            video_SparsePyrLKOpticalFlow_setTermCriteria_10(nativeObj, (int)crit.Item1, (int)crit.Item2, crit.Item3);


        }


        //
        // C++: static Ptr_SparsePyrLKOpticalFlow cv::SparsePyrLKOpticalFlow::create(Size winSize = Size(21, 21), int maxLevel = 3, TermCriteria crit = TermCriteria(TermCriteria::COUNT+TermCriteria::EPS, 30, 0.01), int flags = 0, double minEigThreshold = 1e-4)
        //

        public static SparsePyrLKOpticalFlow create(in Vec2d winSize, int maxLevel, in Vec3d crit, int flags, double minEigThreshold)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_10(winSize.Item1, winSize.Item2, maxLevel, (int)crit.Item1, (int)crit.Item2, crit.Item3, flags, minEigThreshold)));


        }

        public static SparsePyrLKOpticalFlow create(in Vec2d winSize, int maxLevel, in Vec3d crit, int flags)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_11(winSize.Item1, winSize.Item2, maxLevel, (int)crit.Item1, (int)crit.Item2, crit.Item3, flags)));


        }

        public static SparsePyrLKOpticalFlow create(in Vec2d winSize, int maxLevel, in Vec3d crit)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_12(winSize.Item1, winSize.Item2, maxLevel, (int)crit.Item1, (int)crit.Item2, crit.Item3)));


        }

        public static SparsePyrLKOpticalFlow create(in Vec2d winSize, int maxLevel)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_13(winSize.Item1, winSize.Item2, maxLevel)));


        }

        public static SparsePyrLKOpticalFlow create(in Vec2d winSize)
        {


            return SparsePyrLKOpticalFlow.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(video_SparsePyrLKOpticalFlow_create_14(winSize.Item1, winSize.Item2)));


        }

    }
}
