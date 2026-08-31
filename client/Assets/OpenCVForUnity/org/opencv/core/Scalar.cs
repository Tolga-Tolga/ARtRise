using System;
using System.Linq;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class for a 4-element vector derived from Vec.
    /// </summary>
    /// <remarks>
    /// Being derived from Vec&lt;_Tp, 4&gt;, Scalar_ and Scalar can be used just as typical 4-element
    /// vectors. In addition, they can be converted to/from CvScalar. The type Scalar is widely used in
    /// OpenCV to pass pixel values.
    ///
    /// <para>
    /// C++: cv::Scalar_&lt;_Tp&gt; Class Template Reference @see https://docs.opencv.org/4.10.0/d1/da0/classcv_1_1Scalar__.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class Scalar : IEquatable<Scalar>
    {

        public double[] val;

        public Scalar(double v0, double v1, double v2, double v3)
        {
            val = new double[] { v0, v1, v2, v3 };
        }

        public Scalar(double v0, double v1, double v2)
        {
            val = new double[] { v0, v1, v2, 0 };
        }

        public Scalar(double v0, double v1)
        {
            val = new double[] { v0, v1, 0, 0 };
        }

        public Scalar(double v0)
        {
            val = new double[] { v0, 0, 0, 0 };
        }

        public Scalar(double[] vals)
        {
            if (vals != null && vals.Length == 4)
                val = (double[])vals.Clone();
            else
            {
                val = new double[4];
                set(vals);
            }
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                val[0] = vals.Length > 0 ? vals[0] : 0;
                val[1] = vals.Length > 1 ? vals[1] : 0;
                val[2] = vals.Length > 2 ? vals[2] : 0;
                val[3] = vals.Length > 3 ? vals[3] : 0;
            }
            else
                val[0] = val[1] = val[2] = val[3] = 0;
        }

        /// <remarks>
        /// returns a scalar with all elements set to v0
        /// </remarks>
        public static Scalar all(double v)
        {
            return new Scalar(v, v, v, v);
        }

        public Scalar clone()
        {
            return new Scalar(val);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public Scalar mul(Scalar it, double scale)
        {
            return new Scalar(val[0] * it.val[0] * scale, val[1] * it.val[1] * scale,
                val[2] * it.val[2] * scale, val[3] * it.val[3] * scale);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public Scalar mul(Scalar it)
        {
            return mul(it, 1);
        }

        /// <remarks>
        /// returns (v0, -v1, -v2, -v3)
        /// </remarks>
        public Scalar conj()
        {
            return new Scalar(val[0], -val[1], -val[2], -val[3]);
        }

        /// <remarks>
        /// returns true iff v1 == v2 == v3 == 0
        /// </remarks>
        public bool isReal()
        {
            return val[1] == 0 && val[2] == 0 && val[3] == 0;
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            //      result = prime * result + java.util.Arrays.hashCode(val);//TODO: @CHECK
            result = prime * result + OpenCVInternalUtils.ArraysHashCode(val);
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Scalar))
                return false;
            if ((Scalar)obj == this)
                return true;

            Scalar it = (Scalar)obj;
            //      if (!java.util.Arrays.equals(val, it.val)) return false;//TODO: @CHECK
            if (!val.SequenceEqual(it.val))
                return false;
            return true;
        }

        //@Override
        public override string ToString()
        {
            return "[" + val[0] + ", " + val[1] + ", " + val[2] + ", " + val[3] + "]";
        }

    }
}
