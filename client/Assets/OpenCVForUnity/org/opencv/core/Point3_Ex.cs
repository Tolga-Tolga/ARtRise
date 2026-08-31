using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Point3
    {

        public Point3(in Vec3d vals)
            : this()
        {
            set(vals);
        }

        public Point3(in (double x, double y, double z) vals)
            : this()
        {
            set(vals);
        }
        public void set(in Vec3d vals)
        {
            x = vals.Item1;
            y = vals.Item2;
            z = vals.Item3;
        }

        public void set(in (double x, double y, double z) vals)
        {
            x = vals.x;
            y = vals.y;
            z = vals.z;
        }

        public Vec3d cloneAsVec3d()
        {
            return new Vec3d(x, y, z);
        }
        public (double x, double y, double z) cloneAsValueTuple()
        {
            return (x, y, z);
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(in Vec3d p)
        {
            return x * p.Item1 + y * p.Item2 + z * p.Item3;
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(in (double x, double y, double z) p)
        {
            return x * p.x + y * p.y + z * p.z;
        }

        /// <remarks>
        /// cross product of the 2 3D points
        /// </remarks>
        public Vec3d crossAsVec3d(in Vec3d p)
        {
            return new Vec3d(y * p.Item3 - z * p.Item2, z * p.Item1 - x * p.Item3, x * p.Item2 - y * p.Item1);
        }

        /// <remarks>
        /// cross product of the 2 3D points
        /// </remarks>
        public (double x, double y, double z) crossAsValueTuple(in Vec3d p)
        {
            return (y * p.Item3 - z * p.Item2, z * p.Item1 - x * p.Item3, x * p.Item2 - y * p.Item1);
        }

        /// <remarks>
        /// cross product of the 2 3D points
        /// </remarks>
        public Vec3d crossAsVec3d(in (double x, double y, double z) p)
        {
            return new Vec3d(y * p.z - z * p.y, z * p.x - x * p.z, x * p.y - y * p.x);
        }

        /// <remarks>
        /// cross product of the 2 3D points
        /// </remarks>
        public (double x, double y, double z) crossAsValueTuple(in (double x, double y, double z) p)
        {
            return (y * p.z - z * p.y, z * p.x - x * p.z, x * p.y - y * p.x);
        }

        // explicit conversion to Vec3d
        public static explicit operator Vec3d(Point3 point3)
        {
            return new Vec3d(point3.x, point3.y, point3.z);
        }

        // explicit conversion to ValueTuple<double, double, double>
        public static explicit operator (double x, double y, double z)(Point3 point3)
        {
            return (point3.x, point3.y, point3.z);
        }

        // explicit conversion from ValueTuple<double, double, double>
        public static explicit operator Point3(in (double x, double y, double z) valueTuple)
        {
            return new Point3(valueTuple);
        }

        public Vec3d ToVec3d() => new Vec3d(x, y, z);

        public (double x, double y, double z) ToValueTuple() => (x, y, z);

        #region Operators

        // (here p stand for a point ( Point3 ), alpha for a real-valued scalar ( double ).)

        #region Unary

        // -p
        public static Point3 operator -(Point3 a)
        {
            return new Point3(-a.x, -a.y, -a.z);
        }

        #endregion

        #region Binary

        // p + p
        public static Point3 operator +(Point3 a, Point3 b)
        {
            return new Point3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        // p - p
        public static Point3 operator -(Point3 a, Point3 b)
        {
            return new Point3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        // p * alpha, alpha * p
        public static Point3 operator *(Point3 a, double b)
        {
            return new Point3(a.x * b, a.y * b, a.z * b);
        }

        public static Point3 operator *(double a, Point3 b)
        {
            return new Point3(b.x * a, b.y * a, b.z * a);
        }

        // p / alpha
        public static Point3 operator /(Point3 a, double b)
        {
            return new Point3(a.x / b, a.y / b, a.z / b);
        }

        #endregion

        #region Comparison

        public bool Equals(Point3 a)
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
            return this.x == a.x && this.y == a.y && this.z == a.z;
        }

        // p == p
        public static bool operator ==(Point3 a, Point3 b)
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
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        // p != p
        public static bool operator !=(Point3 a, Point3 b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
