using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class DMatch
    {

        public DMatch(in Vec4f vals)
        {
            queryIdx = (int)vals.Item1;
            trainIdx = (int)vals.Item2;
            imgIdx = (int)vals.Item3;
            distance = vals.Item4;
        }

        public DMatch(in (float queryIdx, float trainId, float imgIdx, float distance) vals)
        {
            queryIdx = (int)vals.Item1;
            trainIdx = (int)vals.Item2;
            imgIdx = (int)vals.Item3;
            distance = vals.Item4;
        }

        public bool lessThan(in Vec4f it)
        {
            return distance < it.Item4;
        }

        public bool lessThan(in (float queryIdx, float trainId, float imgIdx, float distance) it)
        {
            return distance < it.Item4;
        }

        // explicit conversion to Vec4f
        public static explicit operator Vec4f(DMatch dMatch)
        {
            return new Vec4f(dMatch.queryIdx, dMatch.trainIdx, dMatch.imgIdx, dMatch.distance);
        }

        // explicit conversion to ValueTuple<float, float, float, float>
        public static explicit operator (float queryIdx, float trainId, float imgIdx, float distance)(DMatch dMatch)
        {
            return (dMatch.queryIdx, dMatch.trainIdx, dMatch.imgIdx, dMatch.distance);
        }

        // explicit conversion to Vector4
        public static explicit operator UnityEngine.Vector4(DMatch dMatch)
        {
            return new UnityEngine.Vector4(dMatch.queryIdx, dMatch.trainIdx, dMatch.imgIdx, dMatch.distance);
        }

        // explicit conversion from ValueTuple<float, float, float, float>
        public static explicit operator DMatch(in (float queryIdx, float trainId, float imgIdx, float distance) valueTuple)
        {
            return new DMatch(valueTuple);
        }

        public Vec4f ToVec4f() => new Vec4f(queryIdx, trainIdx, imgIdx, distance);

        public (float queryIdx, float trainId, float imgIdx, float distance) ToValueTuple() => (queryIdx, trainIdx, imgIdx, distance);

        public UnityEngine.Vector4 ToVector4() => new UnityEngine.Vector4(queryIdx, trainIdx, imgIdx, distance);

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(queryIdx);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(trainIdx);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(imgIdx);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(distance);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is DMatch))
                return false;
            if ((DMatch)obj == this)
                return true;

            DMatch it = (DMatch)obj;
            return queryIdx == it.queryIdx && trainIdx == it.trainIdx && imgIdx == it.imgIdx && distance == it.distance;
        }


        #region Operators

        // (here D stand for a dmatch ( DMatch ).)

        #region Comparison

        public bool Equals(DMatch a)
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
            return this.queryIdx == a.queryIdx && this.trainIdx == a.trainIdx && this.imgIdx == a.imgIdx && this.distance == a.distance;
        }

        // D == D
        public static bool operator ==(DMatch a, DMatch b)
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
            return a.queryIdx == b.queryIdx && a.trainIdx == b.trainIdx && a.imgIdx == b.imgIdx && a.distance == b.distance;
        }

        // D != D
        public static bool operator !=(DMatch a, DMatch b)
        {
            return !(a == b);
        }

        // D < D
        public static bool operator <(DMatch d1, DMatch d2)
        {
            return d1.distance < d2.distance;
        }

        // D > D
        public static bool operator >(DMatch d1, DMatch d2)
        {
            return d1.distance > d2.distance;
        }
        #endregion

        #endregion

    }
}
