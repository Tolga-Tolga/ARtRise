using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class for 3D points specified by its coordinates x, y and z.
    ///
    /// <para>
    /// C++: cv::Point3_&lt;_Tp&gt; Class Template Reference @see https://docs.opencv.org/4.10.0/df/d6c/classcv_1_1Point3__.html
    /// </para>
    /// </summary>
    [Serializable]
    public partial class Point3 : IEquatable<Point3>
    {

        /// <remarks>
        /// x coordinate of the 3D point
        /// </remarks>
        public double x;

        /// <remarks>
        /// y coordinate of the 3D point
        /// </remarks>
        public double y;

        /// <remarks>
        /// z coordinate of the 3D point
        /// </remarks>
        public double z;

        public Point3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <remarks>
        /// default constructor
        /// </remarks>
        public Point3()
            : this(0, 0, 0)
        {

        }

        public Point3(Point p)
        {
            x = p.x;
            y = p.y;
            z = 0;
        }

        public Point3(double[] vals)
            : this()
        {

            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                x = vals.Length > 0 ? vals[0] : 0;
                y = vals.Length > 1 ? vals[1] : 0;
                z = vals.Length > 2 ? vals[2] : 0;
            }
            else
            {
                x = 0;
                y = 0;
                z = 0;
            }
        }

        public Point3 clone()
        {
            return new Point3(x, y, z);
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(Point3 p)
        {
            return x * p.x + y * p.y + z * p.z;
        }

        /// <remarks>
        /// cross product of the 2 3D points
        /// </remarks>
        public Point3 cross(Point3 p)
        {
            return new Point3(y * p.z - z * p.y, z * p.x - x * p.z, x * p.y - y * p.x);
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(x);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(y);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(z);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Point3))
                return false;
            if ((Point3)obj == this)
                return true;

            Point3 it = (Point3)obj;
            return x == it.x && y == it.y && z == it.z;
        }

        //@Override
        public override string ToString()
        {
            return "{" + x + ", " + y + ", " + z + "}";
        }

    }
}
