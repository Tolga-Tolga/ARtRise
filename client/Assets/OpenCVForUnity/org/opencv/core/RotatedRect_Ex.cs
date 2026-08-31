using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class RotatedRect
    {

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
        public RotatedRect(in Vec5d vals)
            : this()
        {

            set(vals);
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
        public RotatedRect(in (double x, double y, double width, double height, double angle) vals)
            : this()
        {

            set(vals);
        }

        public void set(in Vec5d vals)
        {

            center.x = vals.Item1;
            center.y = vals.Item2;
            size.width = vals.Item3;
            size.height = vals.Item4;
            angle = vals.Item5;

        }

        public void set(in (double x, double y, double width, double height, double angle) vals)
        {

            center.x = vals.x;
            center.y = vals.y;
            size.width = vals.width;
            size.height = vals.height;
            angle = vals.angle;

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
        public void points(ref Vec2d[] pt)
        {
            double _angle = angle * Math.PI / 180.0;
            double b = (double)Math.Cos(_angle) * 0.5f;
            double a = (double)Math.Sin(_angle) * 0.5f;

            pt[0] = new Vec2d(
                center.x - a * size.height - b * size.width,
                center.y + b * size.height - a * size.width);

            pt[1] = new Vec2d(
                center.x + a * size.height - b * size.width,
                center.y - b * size.height - a * size.width);

            pt[2] = new Vec2d(
                2 * center.x - pt[0].Item1,
                2 * center.y - pt[0].Item2);

            pt[3] = new Vec2d(
                2 * center.x - pt[1].Item1,
                2 * center.y - pt[1].Item2);
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
        public void points(ref (double x, double y)[] pt)
        {
            double _angle = angle * Math.PI / 180.0;
            double b = (double)Math.Cos(_angle) * 0.5f;
            double a = (double)Math.Sin(_angle) * 0.5f;

            pt[0] = (
                center.x - a * size.height - b * size.width,
                center.y + b * size.height - a * size.width);

            pt[1] = (
                center.x + a * size.height - b * size.width,
                center.y - b * size.height - a * size.width);

            pt[2] = (
                2 * center.x - pt[0].Item1,
                2 * center.y - pt[0].Item2);

            pt[3] = (
                2 * center.x - pt[1].Item1,
                2 * center.y - pt[1].Item2);
        }

        /// <remarks>
        /// returns the minimal up-right integer rectangle containing the rotated rectangle
        /// </remarks>
        public Vec4i boundingRectAsVec4i()
        {
            Vec2d[] pt = new Vec2d[4];
            points(ref pt);
            Vec4i r = new Vec4i((int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].Item1, pt[1].Item1), pt[2].Item1), pt[3].Item1)),//TODO:@check
                         (int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].Item2, pt[1].Item2), pt[2].Item2), pt[3].Item2)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].Item1, pt[1].Item1), pt[2].Item1), pt[3].Item1)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].Item2, pt[1].Item2), pt[2].Item2), pt[3].Item2)));
            r.Item3 -= r.Item1 - 1;
            r.Item4 -= r.Item2 - 1;
            return r;
        }

        /// <remarks>
        /// returns the minimal up-right integer rectangle containing the rotated rectangle
        /// </remarks>
        public (int x, int y, int width, int height) boundingRectAsValueTuple()
        {
            (double x, double y)[] pt = new (double x, double y)[4];
            points(ref pt);
            (int x, int y, int width, int height) r = ((int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].Item1, pt[1].Item1), pt[2].Item1), pt[3].Item1)),//TODO:@check
                         (int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].Item2, pt[1].Item2), pt[2].Item2), pt[3].Item2)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].Item1, pt[1].Item1), pt[2].Item1), pt[3].Item1)),
                         (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].Item2, pt[1].Item2), pt[2].Item2), pt[3].Item2)));
            r.Item3 -= r.Item1 - 1;
            r.Item4 -= r.Item2 - 1;
            return r;
        }

        public Vec5d cloneAsVec5d()
        {
            return new Vec5d(center.x, center.y, size.width, size.height, angle);
        }

        public (double x, double y, double width, double height, double angle) cloneAsValueTuple()
        {
            return (center.x, center.y, size.width, size.height, angle);
        }

        // explicit conversion to Vec5d
        public static explicit operator Vec5d(RotatedRect rotatedRect)
        {
            return new Vec5d(rotatedRect.center.x, rotatedRect.center.y, rotatedRect.size.width, rotatedRect.size.height, rotatedRect.angle);
        }

        // explicit conversion to ValueTuple<double, double, double, double, double>
        public static explicit operator (double x, double y, double width, double height, double angle)(RotatedRect rotatedRect)
        {
            return (rotatedRect.center.x, rotatedRect.center.y, rotatedRect.size.width, rotatedRect.size.height, rotatedRect.angle);
        }

        // explicit conversion from ValueTuple<double, double, double, double, double>
        public static explicit operator RotatedRect(in (double x, double y, double width, double height, double angle) valueTuple)
        {
            return new RotatedRect((valueTuple.x, valueTuple.y, valueTuple.width, valueTuple.height, valueTuple.angle));
        }

        public Vec5d ToVec5d() => new Vec5d(center.x, center.y, size.width, size.height, angle);

        public (double x, double y, double width, double height, double angle) ToValueTuple() => (center.x, center.y, size.width, size.height, angle);


        #region Operators

        // (here R stand for a rotatedrect ( RotatedRect ).)

        #region Comparison

        public bool Equals(RotatedRect a)
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
            return this.center.Equals(a.center) && this.size.Equals(a.size) && this.angle == a.angle;
        }

        // T == T
        public static bool operator ==(RotatedRect a, RotatedRect b)
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
            return a.center.Equals(b.center) && a.size.Equals(b.size) && a.angle == b.angle;
        }

        // T != T
        public static bool operator !=(RotatedRect a, RotatedRect b)
        {
            return !(a == b);
        }


        #endregion

        #endregion

    }

}
