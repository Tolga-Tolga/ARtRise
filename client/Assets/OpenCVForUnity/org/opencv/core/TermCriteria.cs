using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// The class defining termination criteria for iterative algorithms.
    /// </summary>
    /// <remarks>
    /// You can initialize it by default constructor and then override any parameters, or the structure may
    /// be fully initialized using the advanced variant of the constructor.
    ///
    /// <para>
    /// C++: cv::TermCriteria Class Reference @see https://docs.opencv.org/4.10.0/d9/d5d/classcv_1_1TermCriteria.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class TermCriteria : IEquatable<TermCriteria>
    {

        /// <remarks>
        /// The maximum number of iterations or elements to compute
        /// </remarks>
        public const int COUNT = 1;
        /// <remarks>
        /// The maximum number of iterations or elements to compute
        /// </remarks>
        public const int MAX_ITER = COUNT;
        /// <remarks>
        /// The desired accuracy threshold or change in parameters at which the iterative algorithm is terminated.
        /// </remarks>
        public const int EPS = 2;

        /// <remarks>
        /// the type of termination criteria: COUNT, EPS or COUNT + EPS
        /// </remarks>
        public int type;

        /// <remarks>
        /// the maximum number of iterations/elements
        /// </remarks>
        public int maxCount;

        /// <remarks>
        /// the desired accuracy
        /// </remarks>
        public double epsilon;

        /// <remarks>
        /// Termination criteria for iterative algorithms.
        /// </remarks>
        /// <param name="type">
        /// the type of termination criteria: COUNT, EPS or COUNT + EPS.
        /// </param>
        /// <param name="maxCount">
        /// the maximum number of iterations/elements.
        /// </param>
        /// <param name="epsilon">
        /// the desired accuracy.
        /// </param>
        public TermCriteria(int type, int maxCount, double epsilon)
        {
            this.type = type;
            this.maxCount = maxCount;
            this.epsilon = epsilon;
        }

        /// <remarks>
        /// Termination criteria for iterative algorithms.
        /// </remarks>
        public TermCriteria()
                    : this(0, 0, 0.0)
        {

        }

        public TermCriteria(double[] vals)
        {
            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                type = vals.Length > 0 ? (int)vals[0] : 0;
                maxCount = vals.Length > 1 ? (int)vals[1] : 0;
                epsilon = vals.Length > 2 ? (double)vals[2] : 0;
            }
            else
            {
                type = 0;
                maxCount = 0;
                epsilon = 0;
            }
        }

        public TermCriteria clone()
        {
            return new TermCriteria(type, maxCount, epsilon);
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(type);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(maxCount);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(epsilon);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is TermCriteria))
                return false;
            if ((TermCriteria)obj == this)
                return true;

            TermCriteria it = (TermCriteria)obj;
            return type == it.type && maxCount == it.maxCount && epsilon == it.epsilon;
        }

        //@Override
        public override string ToString()
        {

            return "{ type: " + type + ", maxCount: " + maxCount + ", epsilon: " + epsilon + "}";
        }
    }
}
