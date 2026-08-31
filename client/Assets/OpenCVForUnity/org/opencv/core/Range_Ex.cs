using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Range
    {

        public Range(in Vec2i vals)
        {
            set(vals);
        }

        public Range(in (int start, int end) vals)
        {
            set(vals);
        }

        public void set(in Vec2i vals)
        {
            start = vals.Item1;
            end = vals.Item2;
        }

        public void set(in (int start, int end) vals)
        {
            start = vals.start;
            end = vals.end;
        }

        public static Vec2i allAsVec2i()
        {
            return new Vec2i(int.MinValue, int.MaxValue);

        }

        public static (int start, int end) allAsValueTuple()
        {
            return (int.MinValue, int.MaxValue);

        }

        public Vec2i intersectionAsVec2i(in Vec2i r1)
        {
            Vec2i r = new Vec2i(Math.Max(r1.Item1, this.start), Math.Min(r1.Item2, this.end));
            r.Item2 = Math.Max(r.Item2, r.Item1);
            return r;
        }

        public (int start, int end) intersectionAsValueTuple(in (int start, int end) r1)
        {
            (int start, int end) r = (Math.Max(r1.Item1, this.start), Math.Min(r1.Item2, this.end));
            r.Item2 = Math.Max(r.Item2, r.Item1);
            return r;
        }

        public Vec2i shiftAsVec2i(int delta)
        {
            return new Vec2i(start + delta, end + delta);
        }

        public (int start, int end) shiftAsValueTuple(int delta)
        {
            return (start + delta, end + delta);
        }

        public Vec2i cloneAsVec2i()
        {
            return new Vec2i(start, end);
        }

        public (int start, int end) cloneAsValueTuple()
        {
            return (start, end);
        }

        // explicit conversion to Vec2i
        public static explicit operator Vec2i(Range range)
        {
            return new Vec2i(range.start, range.end);
        }

        // explicit conversion to ValueTuple<int, int>
        public static explicit operator (int start, int end)(Range range)
        {
            return (range.start, range.end);
        }

        // explicit conversion from ValueTuple<int, int>
        public static explicit operator Range(in (int start, int end) valueTuple)
        {
            return new Range(valueTuple);
        }

        public Vec2i ToVec2i() => new Vec2i(start, end);

        public (int start, int end) ToValueTuple() => (start, end);

        #region Operators

        // (here r stand for a range ( Range ), alpha for a real-valued scalar ( int ).)

        #region Unary

        // !r
        public static bool operator !(Range r)
        {
            return r.start == r.end;
        }

        #endregion

        #region Binary

        // r + alpha, alpha + r
        public static Range operator +(Range r1, int delta)
        {
            return new Range(r1.start + delta, r1.end + delta);
        }

        public static Range operator +(int delta, Range r1)
        {
            return new Range(r1.start + delta, r1.end + delta);
        }

        // r - alpha
        public static Range operator -(Range r1, int delta)
        {
            return r1 + (-delta);
        }

        // r & r
        public static Range operator &(Range r1, Range r2)
        {
            Range r = new Range(Math.Max(r1.start, r2.start), Math.Min(r1.end, r2.end));
            r.end = Math.Max(r.end, r.start);
            return r;
        }

        #endregion

        #region Comparison

        public bool Equals(Range a)
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
            return this.start == a.start && this.end == a.end;
        }

        // R == R
        public static bool operator ==(Range a, Range b)
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
            return a.start == b.start && a.end == b.end;
        }

        // R != R
        public static bool operator !=(Range a, Range b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
