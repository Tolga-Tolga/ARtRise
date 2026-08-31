using System;
using System.Collections.Generic;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class KeyPoint
    {

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        /// <param name="_response">
        /// keypoint detector response on the keypoint (that is, strength of the keypoint)
        /// </param>
        /// <param name="_octave">
        /// pyramid octave in which the keypoint has been detected
        /// </param>
        /// <param name="_class_id">
        /// object id
        /// </param>
        public KeyPoint(in Vec7f vals)
        {
            pt = new Point(vals.Item1, vals.Item2);
            size = vals.Item3;
            angle = vals.Item4;
            response = vals.Item5;
            octave = (int)vals.Item6;
            class_id = (int)vals.Item7;
        }

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        /// <param name="_response">
        /// keypoint detector response on the keypoint (that is, strength of the keypoint)
        /// </param>
        /// <param name="_octave">
        /// pyramid octave in which the keypoint has been detected
        /// </param>
        /// <param name="_class_id">
        /// object id
        /// </param>
        public KeyPoint(in (float x, float y, float size, float angle, float response, float octave, float class_id) vals)
        {
            pt = new Point(vals.x, vals.y);
            size = vals.size;
            angle = vals.angle;
            response = vals.response;
            octave = (int)vals.octave;
            class_id = (int)vals.class_id;
        }

        // explicit conversion to Vec7f
        public static explicit operator Vec7f(KeyPoint keypoint)
        {
            return new Vec7f((float)keypoint.pt.x, (float)keypoint.pt.y, keypoint.size, keypoint.angle, keypoint.response, keypoint.octave, keypoint.class_id);
        }

        // explicit conversion to ValueTuple<float, float, float, float, float, float, float>
        public static explicit operator (float x, float y, float size, float angle, float response, float octave, float class_id)(KeyPoint keypoint)
        {
            return ((float)keypoint.pt.x, (float)keypoint.pt.y, keypoint.size, keypoint.angle, keypoint.response, keypoint.octave, keypoint.class_id);
        }

        // explicit conversion from ValueTuple<float, float, float, float, float, float, float>
        public static explicit operator KeyPoint(in (float x, float y, float size, float angle, float response, float octave, float class_id) valueTuple)
        {
            return new KeyPoint((valueTuple.x, valueTuple.y, valueTuple.size, valueTuple.angle, valueTuple.response, valueTuple.octave, valueTuple.class_id));
        }

        public (float x, float y, float size, float angle, float response, float octave, float class_id) ToValueTuple() => ((float)pt.x, (float)pt.y, size, angle, response, octave, class_id);

        public Vec7f ToVec7f() => new Vec7f((float)pt.x, (float)pt.y, size, angle, response, octave, class_id);


        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(pt.x);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(pt.y);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(size);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(angle);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(response);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(octave);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(class_id);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is KeyPoint))
                return false;
            if ((KeyPoint)obj == this)
                return true;

            KeyPoint it = (KeyPoint)obj;
            return pt.x == it.pt.x && pt.y == it.pt.y && size == it.size && angle == it.angle && response == it.response && octave == it.octave && class_id == it.class_id;
        }


        #region Operators

        // (here K stand for a keypoint ( KeyPoint ).)

        #region Comparison

        public bool Equals(KeyPoint a)
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
            return this.pt.x == a.pt.x && this.pt.y == a.pt.y && this.size == a.size && this.angle == a.angle && this.response == a.response && this.octave == a.octave && this.class_id == a.class_id;
        }

        // K == K
        public static bool operator ==(KeyPoint a, KeyPoint b)
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
            return a.pt.x == b.pt.x && a.pt.y == b.pt.y && a.size == b.size && a.angle == b.angle && a.response == b.response && a.octave == b.octave && a.class_id == b.class_id;
        }

        // T != T
        public static bool operator !=(KeyPoint a, KeyPoint b)
        {
            return !(a == b);
        }


        #endregion

        #endregion

    }
}
