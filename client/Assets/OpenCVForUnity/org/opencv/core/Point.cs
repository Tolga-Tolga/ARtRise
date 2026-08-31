using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class for 2D points specified by its coordinates x and y.
    ///
    /// <para>
    /// C++: cv::Point_&lt;_Tp&gt; Class Template Reference @see https://docs.opencv.org/4.10.0/db/d4e/classcv_1_1Point__.html
    /// </para>
    /// </summary>
    [Serializable]
    public partial class Point : IEquatable<Point>
    {

        /// <remarks>
        /// x coordinate of the point
        /// </remarks>
        public double x;

        /// <remarks>
        /// y coordinate of the point
        /// </remarks>
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <remarks>
        /// default constructor
        /// </remarks>
        public Point()
            : this(0, 0)
        {

        }

        public Point(double[] vals)
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
            }
            else
            {
                x = 0;
                y = 0;
            }
        }

        public Point clone()
        {
            return new Point(x, y);
        }

        /// <remarks>
        /// dot product
        /// </remarks>
        public double dot(Point p)
        {
            return x * p.x + y * p.y;
        }

        //  @Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(x);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(y);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Point))
                return false;
            if ((Point)obj == this)
                return true;

            Point it = (Point)obj;
            return x == it.x && y == it.y;
        }

        /// <remarks>
        /// checks whether the point is inside the specified rectangle
        /// </remarks>
        public bool inside(Rect r)
        {
            return r.contains(this);
        }

        //@Override
        public override string ToString()
        {
            return "{" + x + ", " + y + "}";
        }

    }
}
