using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// The class represents rotated (i.e. not up-right) rectangles on a plane.
    /// </summary>
    /// <remarks>
    /// Each rectangle is specified by the center point (mass center), length of each side (represented by
    /// Size2f structure) and the rotation angle in degrees.
    ///
    /// <para>
    /// C++: cv::RotatedRect Class Reference @see https://docs.opencv.org/4.10.0/db/dd6/classcv_1_1RotatedRect.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class RotatedRect : IEquatable<RotatedRect>
    {
        /// <remarks>
        /// returns the rectangle mass center
        /// </remarks>
        public Point center;

        /// <remarks>
        /// returns width and height of the rectangle
        /// </remarks>
        public Size size;

        /// <remarks>
        /// returns the rotation angle. When the angle is 0, 90, 180, 270 etc., the rectangle becomes an up-right rectangle.
        /// </remarks>
        public double angle;

        /// <remarks>
        /// default constructor
        /// </remarks>
        public RotatedRect()
        {
            this.center = new Point();
            this.size = new Size();
            this.angle = 0;
        }

        /// <summary>
        /// full constructor
        /// </summary>
        /// <param name="c">
        /// The rectangle mass center.
        /// </param>
        /// <param name="s">
        /// Width and height of the rectangle.
        /// </param>
        /// <param name="a">
        /// The rotation angle in a clockwise direction. When the angle is 0, 90, 180, 270 etc.,
        /// the rectangle becomes an up-right rectangle.
        /// </param>
        public RotatedRect(Point c, Size s, double a)
        {
            this.center = c.clone();
            this.size = s.clone();
            this.angle = a;
        }

        public RotatedRect(double[] vals)
            : this()
        {

            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                center.x = vals.Length > 0 ? (double)vals[0] : 0;
                center.y = vals.Length > 1 ? (double)vals[1] : 0;
                size.width = vals.Length > 2 ? (double)vals[2] : 0;
                size.height = vals.Length > 3 ? (double)vals[3] : 0;
                angle = vals.Length > 4 ? (double)vals[4] : 0;
            }
            else
            {
                center.x = 0;
                center.y = 0;
                size.width = 0;
                size.height = 0;
                angle = 0;
            }
        }

        /// <summary>
        /// returns 4 vertices of the rotated rectangle
        /// </summary>
        /// <param name="pt">
        /// The points array for storing rectangle vertices. The order is _bottomLeft_, _topLeft_, topRight, bottomRight.
        /// </param>
        /// <remarks>
        /// _Bottom_, _Top_, _Left_ and _Right_ sides refer to the original rectangle (angle is 0),
        /// so after 180 degree rotation _bottomLeft_ point will be located at the top right corner of the
        /// rectangle.
        /// </remarks>
        public void points(Point[] pt)
        {
            double _angle = angle * Math.PI / 180.0;
            double b = (double)Math.Cos(_angle) * 0.5f;
            double a = (double)Math.Sin(_angle) * 0.5f;

            pt[0] = new Point(
                center.x - a * size.height - b * size.width,
                center.y + b * size.height - a * size.width);

            pt[1] = new Point(
                center.x + a * size.height - b * size.width,
                center.y - b * size.height - a * size.width);

            pt[2] = new Point(
                2 * center.x - pt[0].x,
                2 * center.y - pt[0].y);

            pt[3] = new Point(
                2 * center.x - pt[1].x,
                2 * center.y - pt[1].y);
        }

        /// <remarks>
        /// returns the minimal up-right integer rectangle containing the rotated rectangle
        /// </remarks>
        public Rect boundingRect()
        {
            Point[] pt = new Point[4];
            points(pt);
            Rect r = new Rect((int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].x, pt[1].x), pt[2].x), pt[3].x)),//TODO:@check
                         (int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].y, pt[1].y), pt[2].y), pt[3].y)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].x, pt[1].x), pt[2].x), pt[3].x)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].y, pt[1].y), pt[2].y), pt[3].y)));
            r.width -= r.x - 1;
            r.height -= r.y - 1;
            return r;
        }

        public RotatedRect clone()
        {
            return new RotatedRect(center, size, angle);
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(center.x);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(center.y);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(size.width);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(size.height);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(angle);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is RotatedRect))
                return false;
            if ((RotatedRect)obj == this)
                return true;

            RotatedRect it = (RotatedRect)obj;
            return center.Equals(it.center) && size.Equals(it.size) && angle == it.angle;
        }

        //@Override
        public override string ToString()
        {
            return "{ " + center + " " + size + " * " + angle + " }";
        }
    }
}
