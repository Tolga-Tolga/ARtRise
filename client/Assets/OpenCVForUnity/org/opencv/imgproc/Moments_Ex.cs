using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.ImgprocModule
{

    //javadoc:Moments

    public partial class Moments
    {

        public Moments(in Vec10d vals)
        {
            set(vals);
        }

        public Moments(in (double m00, double m10, double m01, double m20, double m11, double m02, double m30, double m21, double m12, double m03) vals)
        {
            set(vals);
        }

        public void set(in Vec10d vals)
        {

            m00 = vals.Item1;
            m10 = vals.Item2;
            m01 = vals.Item3;
            m20 = vals.Item4;
            m11 = vals.Item5;
            m02 = vals.Item6;
            m30 = vals.Item7;
            m21 = vals.Item8;
            m12 = vals.Item9;
            m03 = vals.Item10;
            this.completeState();

        }

        public void set(in (double m00, double m10, double m01, double m20, double m11, double m02, double m30, double m21, double m12, double m03) vals)
        {

            m00 = vals.Item1;
            m10 = vals.Item2;
            m01 = vals.Item3;
            m20 = vals.Item4;
            m11 = vals.Item5;
            m02 = vals.Item6;
            m30 = vals.Item7;
            m21 = vals.Item8;
            m12 = vals.Item9;
            m03 = vals.Item10;
            this.completeState();

        }

        // explicit conversion to Vec10d
        public static explicit operator Vec10d(Moments moments)
        {
            return new Vec10d(moments.m00, moments.m10, moments.m01, moments.m20, moments.m11, moments.m02, moments.m30, moments.m21, moments.m12, moments.m03);
        }

        // explicit conversion to ValueTuple<double, double, double, double, double, double, double, double, double, double>
        public static explicit operator (double m00, double m10, double m01, double m20, double m11, double m02, double m30, double m21, double m12, double m03)(Moments moments)
        {
            return (moments.m00, moments.m10, moments.m01, moments.m20, moments.m11, moments.m02, moments.m30, moments.m21, moments.m12, moments.m03);
        }

        // explicit conversion from ValueTuple<double, double, double, double, double, double, double, double, double, double>
        public static explicit operator Moments(in (double m00, double m10, double m01, double m20, double m11, double m02, double m30, double m21, double m12, double m03) valueTuple)
        {
            return new Moments(valueTuple);
        }

        public (double m00, double m10, double m01, double m20, double m11, double m02, double m30, double m21, double m12, double m03) ToValueTuple() => (m00, m10, m01, m20, m11, m02, m30, m21, m12, m03);

        public Vec10d ToVec10d() => new Vec10d(m00, m10, m01, m20, m11, m02, m30, m21, m12, m03);


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(m00);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m10);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m01);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m20);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m11);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m02);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m30);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m21);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m12);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(m03);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Moments))
                return false;
            if ((Moments)obj == this)
                return true;

            Moments it = (Moments)obj;
            return m00 == it.m00 && m10 == it.m10 && m01 == it.m01 && m20 == it.m20 && m11 == it.m11 && m02 == it.m02 && m30 == it.m30 && m21 == it.m21 && m12 == it.m12 && m03 == it.m03;
        }


        #region Operators

        // (here M stand for a moments ( Moments ).)

        #region Comparison

        public bool Equals(Moments a)
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
            return this.m00 == a.m00 && this.m10 == a.m10 && this.m01 == a.m01 && this.m20 == a.m20 && this.m11 == a.m11 && this.m02 == a.m02 && this.m30 == a.m30 && this.m21 == a.m21 && this.m12 == a.m12 && this.m03 == a.m03;
        }

        // K == K
        public static bool operator ==(Moments a, Moments b)
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
            return a.m00 == b.m00 && a.m10 == b.m10 && a.m01 == b.m01 && a.m20 == b.m20 && a.m11 == b.m11 && a.m02 == b.m02 && a.m30 == b.m30 && a.m21 == b.m21 && a.m12 == b.m12 && a.m03 == b.m03;
        }

        // T != T
        public static bool operator !=(Moments a, Moments b)
        {
            return !(a == b);
        }


        #endregion

        #endregion

    }
}
