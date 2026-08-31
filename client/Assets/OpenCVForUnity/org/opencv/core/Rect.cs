using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class for 2D rectangles.
    /// </summary>
    /// <remarks>
    /// Described by the following parameters:
    /// - Coordinates of the top-left corner. This is a default interpretation of Rect_::x and Rect_::y
    ///   in OpenCV. Though, in your algorithms you may count x and y from the bottom-left corner.
    /// - Rectangle width and height.
    ///
    /// OpenCV typically assumes that the top and left boundary of the rectangle are inclusive, while the
    /// right and bottom boundaries are not.
    ///
    /// <para>
    /// C++: cv::Rect_&lt;_Tp&gt; Class Template Reference @see https://docs.opencv.org/4.10.0/d2/d44/classcv_1_1Rect__.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class Rect : IEquatable<Rect>
    {

        /// <remarks>
        /// x coordinate of the top-left corner
        /// </remarks>
        public int x;

        /// <remarks>
        /// y coordinate of the top-left corner
        /// </remarks>
        public int y;

        /// <remarks>
        /// width of the rectangle
        /// </remarks>
        public int width;

        /// <remarks>
        /// height of the rectangle
        /// </remarks>
        public int height;

        public Rect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <remarks>
        /// default constructor
        /// </remarks>
        public Rect()
            : this(0, 0, 0, 0)
        {

        }

        public Rect(Point p1, Point p2)
        {
            x = (int)(p1.x < p2.x ? p1.x : p2.x);
            y = (int)(p1.y < p2.y ? p1.y : p2.y);
            width = (int)(p1.x > p2.x ? p1.x : p2.x) - x;
            height = (int)(p1.y > p2.y ? p1.y : p2.y) - y;
        }

        public Rect(Point p, Size s)
            : this((int)p.x, (int)p.y, (int)s.width, (int)s.height)
        {

        }

        public Rect(double[] vals)
        {
            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                x = vals.Length > 0 ? (int)vals[0] : 0;
                y = vals.Length > 1 ? (int)vals[1] : 0;
                width = vals.Length > 2 ? (int)vals[2] : 0;
                height = vals.Length > 3 ? (int)vals[3] : 0;
            }
            else
            {
                x = 0;
                y = 0;
                width = 0;
                height = 0;
            }
        }

        public Rect clone()
        {
            return new Rect(x, y, width, height);
        }

        /// <remarks>
        /// the top-left corner
        /// </remarks>
        public Point tl()
        {
            return new Point(x, y);
        }

        /// <remarks>
        /// the bottom-right corner
        /// </remarks>
        public Point br()
        {
            return new Point(x + width, y + height);
        }

        /// <remarks>
        /// size (width, height) of the rectangle
        /// </remarks>
        public Size size()
        {
            return new Size(width, height);
        }

        /// <remarks>
        /// area (width*height) of the rectangle
        /// </remarks>
        public double area()
        {
            return width * height;
        }

        /// <remarks>
        /// true if empty
        /// </remarks>
        public bool empty()
        {
            return width <= 0 || height <= 0;
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(Point p)
        {
            return x <= p.x && p.x < x + width && y <= p.y && p.y < y + height;
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(height);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(width);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(x);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(y);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Rect))
                return false;
            if ((Rect)obj == this)
                return true;

            Rect it = (Rect)obj;
            return x == it.x && y == it.y && width == it.width && height == it.height;
        }

        //@Override
        public override string ToString()
        {
            return "{" + x + ", " + y + ", " + width + "x" + height + "}";
        }

    }
}
