using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Point
    {

        public Point(in Vec2d vals)
            : this()
        {
            set(vals);
        }

        public Point(in (double x, double y) vals)
            : this()
        {
            set(vals);
        }
        public void set(in Vec2d vals)
        {
            x = vals.Item1;
            y = vals.Item2;
        }

        public void set(in (double x, double y) vals)
        {
            x = vals.x;
            y = vals.y;
        }

        public Vec2d cloneAsVec2d()
        {
            return new Vec2d(x, y);
        }
        public (double x, double y) cloneAsValueTuple()
        {
            return (x, y);
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(in Vec2d p)
        {
            return x * p.Item1 + y * p.Item2;
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(in (double x, double y) p)
        {
            return x * p.x + y * p.y;
        }

        /// <remarks>
        /// checks whether the point is inside the specified rectangle
        /// </remarks>
        public bool inside(in Vec4i r)
        {
            return ((Rect)r).contains(this);
        }

        /// <remarks>
        /// checks whether the point is inside the specified rectangle
        /// </remarks>
        public bool inside(in (int x, int y, int width, int height) r)
        {
            return ((Rect)r).contains(this);
        }

        // explicit conversion to Vec2d
        public static explicit operator Vec2d(Point point)
        {
            return new Vec2d(point.x, point.y);
        }

        // explicit conversion to ValueTuple<double, double>
        public static explicit operator (double x, double y)(Point point)
        {
            return (point.x, point.y);
        }

        // explicit conversion from ValueTuple<double, double>
        public static explicit operator Point(in (double x, double y) valueTuple)
        {
            return new Point(valueTuple);
        }

        public Vec2d ToVec2d() => new Vec2d(x, y);

        public (double x, double y) ToValueTuple() => (x, y);

        #region Operators

        // (here p stand for a point ( Point ), alpha for a real-valued scalar ( double ).)

        #region Unary

        // -p
        public static Point operator -(Point a)
        {
            return new Point(-a.x, -a.y);
        }

        #endregion

        #region Binary

        // p + p
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        // p - p
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        // p * alpha, alpha * p
        public static Point operator *(Point a, double b)
        {
            return new Point(a.x * b, a.y * b);
        }

        public static Point operator *(double a, Point b)
        {
            return new Point(b.x * a, b.y * a);
        }

        // p / alpha
        public static Point operator /(Point a, double b)
        {
            return new Point(a.x / b, a.y / b);
        }

        #endregion

        #region Comparison

        public bool Equals(Point a)
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
            return this.x == a.x && this.y == a.y;
        }

        // p == p
        public static bool operator ==(Point a, Point b)
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
            return a.x == b.x && a.y == b.y;
        }

        // p != p
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
