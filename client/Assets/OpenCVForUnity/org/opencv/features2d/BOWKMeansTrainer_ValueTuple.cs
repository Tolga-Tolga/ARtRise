
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.Features2dModule
{
    public partial class BOWKMeansTrainer : BOWTrainer
    {

        //
        // C++:   cv::BOWKMeansTrainer::BOWKMeansTrainer(int clusterCount, TermCriteria termcrit = TermCriteria(), int attempts = 3, int flags = KMEANS_PP_CENTERS)
        //

        /// <summary>
        ///  The constructor.
        /// </summary>
        /// <remarks>
        ///      @see cv::kmeans
        /// </remarks>
        public BOWKMeansTrainer(int clusterCount, in (double type, double maxCount, double epsilon) termcrit, int attempts, int flags) :
                    base(DisposableObject.ThrowIfNullIntPtr(features2d_BOWKMeansTrainer_BOWKMeansTrainer_10(clusterCount, (int)termcrit.type, (int)termcrit.maxCount, termcrit.epsilon, attempts, flags)))
        {



        }

        /// <summary>
        ///  The constructor.
        /// </summary>
        /// <remarks>
        ///      @see cv::kmeans
        /// </remarks>
        public BOWKMeansTrainer(int clusterCount, in (double type, double maxCount, double epsilon) termcrit, int attempts) :
                    base(DisposableObject.ThrowIfNullIntPtr(features2d_BOWKMeansTrainer_BOWKMeansTrainer_11(clusterCount, (int)termcrit.type, (int)termcrit.maxCount, termcrit.epsilon, attempts)))
        {



        }

        /// <summary>
        ///  The constructor.
        /// </summary>
        /// <remarks>
        ///      @see cv::kmeans
        /// </remarks>
        public BOWKMeansTrainer(int clusterCount, in (double type, double maxCount, double epsilon) termcrit) :
                    base(DisposableObject.ThrowIfNullIntPtr(features2d_BOWKMeansTrainer_BOWKMeansTrainer_12(clusterCount, (int)termcrit.type, (int)termcrit.maxCount, termcrit.epsilon)))
        {



        }

    }
}
