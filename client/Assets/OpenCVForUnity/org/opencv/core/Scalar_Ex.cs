using System;
using System.Linq;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Scalar
    {
        public Scalar(in Vec4d vals)
        {
            val = new double[] { vals.Item1, vals.Item2, vals.Item3, vals.Item4 };
        }

        public Scalar(in (double v0, double v1, double v2, double v3) vals)
        {
            val = new double[] { vals.Item1, vals.Item2, vals.Item3, vals.Item4 };
        }
        public void set(in Vec4d vals)
        {
            val[0] = vals.Item1;
            val[1] = vals.Item2;
            val[2] = vals.Item3;
            val[3] = vals.Item4;
        }

        public void set(in (double v0, double v1, double v2, double v3) vals)
        {
            val[0] = vals.v0;
            val[1] = vals.v1;
            val[2] = vals.v2;
            val[3] = vals.v3;
        }

        /// <remarks>
        /// returns a scalar with all elements set to v0
        /// </remarks>
        public static Vec4d allAsVec4d(double v)
        {
            return new Vec4d(v, v, v, v);
        }

        /// <remarks>
        /// returns a scalar with all elements set to v0
        /// </remarks>
        public static (double v0, double v1, double v2, double v3) allAsValueTuple(double v)
        {
            return (v, v, v, v);
        }

        public Vec4d cloneAsVec4d()
        {
            return new Vec4d(val[0], val[1], val[2], val[3]);
        }

        public (double v0, double v1, double v2, double v3) cloneAsValueTuple()
        {
            return (val[0], val[1], val[2], val[3]);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public Vec4d mulAsVec4d(in Vec4d it, double scale)
        {
            return new Vec4d(val[0] * it.Item1 * scale, val[1] * it.Item2 * scale,
                val[2] * it.Item3 * scale, val[3] * it.Item4 * scale);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public (double v0, double v1, double v2, double v3) mulAsValueTuple(in (double v0, double v1, double v2, double v3) it, double scale)
        {
            return (val[0] * it.Item1 * scale, val[1] * it.Item2 * scale,
                val[2] * it.Item3 * scale, val[3] * it.Item4 * scale);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public Vec4d mulAsVec4d(Vec4d it)
        {
            return mulAsVec4d(it, 1);
        }

        /// <remarks>
        /// per-element product
        /// </remarks>
        public (double v0, double v1, double v2, double v3) mulAsValueTuple(in (double v0, double v1, double v2, double v3) it)
        {
            return mulAsValueTuple(it, 1);
        }

        /// <remarks>
        /// returns (v0, -v1, -v2, -v3)
        /// </remarks>
        public Vec4d conjAsVec4d()
        {
            return new Vec4d(val[0], -val[1], -val[2], -val[3]);
        }

        /// <remarks>
        /// returns (v0, -v1, -v2, -v3)
        /// </remarks>
        public (double v0, double v1, double v2, double v3) conjAsValueTuple()
        {
            return (val[0], -val[1], -val[2], -val[3]);
        }

        // explicit conversion to Vec4d
        public static explicit operator Vec4d(Scalar scalar)
        {
            return new Vec4d(scalar.val[0], scalar.val[1], scalar.val[2], scalar.val[3]);
        }

        // explicit conversion to ValueTuple<double, double, double, double>
        public static explicit operator (double v0, double v1, double v2, double v3)(Scalar scalar)
        {
            return (scalar.val[0], scalar.val[1], scalar.val[2], scalar.val[3]);
        }

        // explicit conversion from ValueTuple<double, double, double, double>
        public static explicit operator Scalar(in (double v0, double v1, double v2, double v3) valueTuple)
        {
            return new Scalar(valueTuple);
        }

        public Vec4d ToVec4d() => new Vec4d(val[0], val[1], val[2], val[3]);

        public (double v0, double v1, double v2, double v3) ToValueTuple() => (val[0], val[1], val[2], val[3]);

        #region Operators

        // (here s stand for a scalar ( Scalar ), alpha for a real-valued scalar ( double ).)

        #region Unary

        // -s
        public static Scalar operator -(Scalar a)
        {
            return new Scalar(-a.val[0], -a.val[1], -a.val[2], -a.val[3]);
        }

        #endregion

        #region Binary

        // s + s
        public static Scalar operator +(Scalar a, Scalar b)
        {
            return new Scalar(a.val[0] + b.val[0], a.val[1] + b.val[1], a.val[2] + b.val[2], a.val[3] + b.val[3]);
        }

        // s - s
        public static Scalar operator -(Scalar a, Scalar b)
        {
            return new Scalar(a.val[0] - b.val[0], a.val[1] - b.val[1], a.val[2] - b.val[2], a.val[3] - b.val[3]);
        }

        // s * s, s * alpha, alpha * s
        public static Scalar operator *(Scalar a, Scalar b)
        {
            return new Scalar((a.val[0] * b.val[0] - a.val[1] * b.val[1] - a.val[2] * b.val[2] - a.val[3] * b.val[3]),
                (a.val[0] * b.val[1] + a.val[1] * b.val[0] + a.val[2] * b.val[3] - a.val[3] * b.val[2]),
                (a.val[0] * b.val[2] - a.val[1] * b.val[3] + a.val[2] * b.val[0] + a.val[3] * b.val[1]),
                (a.val[0] * b.val[3] + a.val[1] * b.val[2] - a.val[2] * b.val[1] + a.val[3] * b.val[0]));
        }

        public static Scalar operator *(Scalar a, double alpha)
        {
            return new Scalar(a.val[0] * alpha, a.val[1] * alpha, a.val[2] * alpha, a.val[3] * alpha);
        }

        public static Scalar operator *(double alpha, Scalar a)
        {
            return a * alpha;
        }

        // s / s, s / alpha, alpha / s
        public static Scalar operator /(Scalar a, Scalar b)
        {
            return a * (1 / b);
        }

        public static Scalar operator /(Scalar a, double alpha)
        {
            double s = 1 / alpha;
            return new Scalar(a.val[0] * s, a.val[1] * s, a.val[2] * s, a.val[3] * s);
        }

        public static Scalar operator /(double a, Scalar b)
        {
            double s = a / (b.val[0] * b.val[0] + b.val[1] * b.val[1] + b.val[2] * b.val[2] + b.val[3] * b.val[3]);
            return b.conj() * s;
        }

        #endregion

        #region Comparison

        public bool Equals(Scalar a)
        {
            // If both are same instance, return true.
            if (System.Object.ReferenceEquals(this, a))
            {
                return true;
            }

            // If object is null, return false.
            if ((object)a == null)
            {
                return false;
            }

            // Return true if the fields match:
            if (!this.val.SequenceEqual(a.val))
                return false;
            return true;
        }

        // s == s
        public static bool operator ==(Scalar a, Scalar b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            if (!a.val.SequenceEqual(b.val))
                return false;
            return true;
        }

        // s != s
        public static bool operator !=(Scalar a, Scalar b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
