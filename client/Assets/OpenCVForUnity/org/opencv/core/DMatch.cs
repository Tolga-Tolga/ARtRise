using System;

namespace OpenCVForUnity.CoreModule
{

    //C++: class DMatch

    /// <summary>
    /// Class for matching keypoint descriptors.
    /// </summary>
    /// <remarks>
    /// This class is used for matching keypoint descriptors between images. It stores the query descriptor
    /// index, the train descriptor index, the train image index, and the distance between the descriptors.
    ///
    /// <para>
    /// C++: cv::DMatch Class Reference @see https://docs.opencv.org/4.10.0/d4/de0/classcv_1_1DMatch.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class DMatch : IEquatable<DMatch>
    {

        /// <remarks>
        /// Query descriptor index.
        /// </remarks>
        public int queryIdx;
        /// <remarks>
        /// Train descriptor index.
        /// </remarks>
        public int trainIdx;
        /// <remarks>
        /// Train image index.
        /// </remarks>
        public int imgIdx;
        public float distance;

        public DMatch()
            : this(-1, -1, float.MaxValue)
        {

        }

        public DMatch(int _queryIdx, int _trainIdx, float _distance)
        {
            queryIdx = _queryIdx;
            trainIdx = _trainIdx;
            imgIdx = -1;
            distance = _distance;
        }

        public DMatch(int _queryIdx, int _trainIdx, int _imgIdx, float _distance)
        {
            queryIdx = _queryIdx;
            trainIdx = _trainIdx;
            imgIdx = _imgIdx;
            distance = _distance;
        }



        public bool lessThan(DMatch it)
        {
            return distance < it.distance;
        }


        //@Override
        public override string ToString()
        {
            return "DMatch [queryIdx=" + queryIdx + ", trainIdx=" + trainIdx
                + ", imgIdx=" + imgIdx + ", distance=" + distance + "]";
        }

    }
}
