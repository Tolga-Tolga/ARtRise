using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Rect2d
    {

        public Rect2d(in Vec4d vals)
            : this()
        {
            set(vals);
        }

        public Rect2d(in (double x, double y, double width, double height) vals)
            : this()
        {
            set(vals);
        }
        public void set(in Vec4d vals)
        {
            x = vals.Item1;
            y = vals.Item2;
            width = vals.Item3;
            height = vals.Item4;
        }

        public void set(in (double x, double y, double width, double height) vals)
        {
            x = vals.x;
            y = vals.y;
            width = vals.width;
            height = vals.height;
        }

        public Vec4d cloneAsVec4d()
        {
            return new Vec4d(x, y, width, height);
        }

        public (double x, double y, double width, double height) cloneAsValueTuple()
        {
            return (x, y, width, height);
        }

        /// <remarks>
        /// the top-left corner
        /// </remarks>
        public Vec2d tlAsVec2d()
        {
            return new Vec2d(x, y);
        }

        /// <remarks>
        /// the top-left corner
        /// </remarks>
        public (double x, double y) tlAsValueTuple()
        {
            return (x, y);
        }

        /// <remarks>
        /// the bottom-right corner
        /// </remarks>
        public Vec2d brAsVec2d()
        {
            return new Vec2d(x + width, y + height);
        }

        /// <remarks>
        /// the bottom-right corner
        /// </remarks>
        public (double x, double y) brAsValueTuple()
        {
            return (x + width, y + height);
        }

        /// <remarks>
        /// size (width, height) of the rectangle
        /// </remarks>
        public Vec2d sizeAsVec2d()
        {
            return new Vec2d(width, height);
        }

        /// <remarks>
        /// size (width, height) of the rectangle
        /// </remarks>
        public (double width, double height) sizeAsValueTuple()
        {
            return (width, height);
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(double x, double y)
        {
            return this.x <= x && x < this.x + this.width && this.y <= y && y < this.y + this.height;
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in Vec2d p)
        {
            return x <= p.Item1 && p.Item1 < x + width && y <= p.Item2 && p.Item2 < y + height;
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in (double x, double y) p)
        {
            return x <= p.x && p.x < x + width && y <= p.y && p.y < y + height;
        }

        // explicit conversion to Vec4d
        public static explicit operator Vec4d(Rect2d rect)
        {
            return new Vec4d(rect.x, rect.y, rect.width, rect.height);
        }

        // explicit conversion to ValueTuple<double, double, double, double>
        public static explicit operator (double x, double y, double width, double height)(Rect2d rect)
        {
            return (rect.x, rect.y, rect.width, rect.height);
        }

        // explicit conversion from ValueTuple<double, double, double, double>
        public static explicit operator Rect2d(in (double x, double y, double width, double height) valueTuple)
        {
            return new Rect2d(valueTuple);
        }

        public Vec4d ToVec4d() => new Vec4d(x, y, width, height);

        public (double x, double y, double width, double height) ToValueTuple() => (x, y, width, height);

        #region Added methods

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(Rect2d rect)
        {
            return x <= rect.x &&
            (rect.x + rect.width) <= (x + width) &&
            y <= rect.y &&
            (rect.y + rect.height) <= (y + height);
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in Vec4d rect)
        {
            return x <= rect.Item1 &&
            (rect.Item1 + rect.Item3) <= (x + width) &&
            y <= rect.Item2 &&
            (rect.Item2 + rect.Item4) <= (y + height);
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in (double x, double y, double width, double height) rect)
        {
            return x <= rect.x &&
            (rect.x + rect.width) <= (x + width) &&
            y <= rect.y &&
            (rect.y + rect.height) <= (y + height);
        }

        /// <summary>
        /// Expands the rectangle by decreasing its position and increasing its size by the specified width and height.
        /// </summary>
        /// <param name="width">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="height">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        public void inflate(double width, double height)
        {
            this.x -= width;
            this.y -= height;
            this.width += (2 * width);
            this.height += (2 * height);
        }

        /// <summary>
        /// Expands the rectangle by decreasing its position and increasing its size by the specified width and height.
        /// </summary>
        /// <param name="width">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="height">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        public void inflate(Size size)
        {
            inflate(size.width, size.height);
        }

        /// <summary>
        /// Expands the rectangle by decreasing its position and increasing its size by the specified width and height.
        /// </summary>
        /// <param name="width">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="height">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        public void inflate(in Vec2d size)
        {
            inflate((int)size.Item1, (int)size.Item2);
        }

        /// <summary>
        /// Expands the rectangle by decreasing its position and increasing its size by the specified width and height.
        /// </summary>
        /// <param name="width">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="height">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        public void inflate(in (double width, double height) size)
        {
            inflate((int)size.width, (int)size.height);
        }

        /// <summary>
        /// Expands the specified rectangle by decreasing its position and increasing its size by the specified amounts.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to be expanded.
        /// </param>
        /// <param name="x">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="y">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        /// <returns>
        /// The expanded rectangle.
        /// </returns>
        public static Rect2d inflate(Rect2d rect, double x, double y)
        {
            rect.inflate(x, y);
            return rect;
        }

        /// <summary>
        /// Expands the specified rectangle by decreasing its position and increasing its size by the specified amounts.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to be expanded.
        /// </param>
        /// <param name="x">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="y">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        /// <returns>
        /// The expanded rectangle.
        /// </returns>
        public static Vec4d inflateAsVec4d(in Vec4d rect, double x, double y)
        {
            Rect2d tmpRect = (Rect2d)rect;
            tmpRect.inflate(x, y);
            return (Vec4d)tmpRect;
        }

        /// <summary>
        /// Expands the specified rectangle by decreasing its position and increasing its size by the specified amounts.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to be expanded.
        /// </param>
        /// <param name="x">
        /// The amount to decrease the x-coordinate and increase the width.
        /// </param>
        /// <param name="y">
        /// The amount to decrease the y-coordinate and increase the height.
        /// </param>
        /// <returns>
        /// The expanded rectangle.
        /// </returns>
        public static (double x, double y, double width, double height) inflateAsValueTuple(in (double x, double y, double width, double height) rect, double x, double y)
        {
            Rect2d tmpRect = (Rect2d)rect;
            tmpRect.inflate(x, y);
            return ((double x, double y, double width, double height))tmpRect;
        }

        /// <summary>
        /// Computes the intersection of two rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle.
        /// </param>
        /// <param name="b">
        /// The second rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public static Rect2d intersect(Rect2d a, Rect2d b)
        {
            double x1 = Math.Max(a.x, b.x);
            double x2 = Math.Min(a.x + a.width, b.x + b.width);
            double y1 = Math.Max(a.y, b.y);
            double y2 = Math.Min(a.y + a.height, b.y + b.height);

            if (x2 >= x1 && y2 >= y1)
                return new Rect2d(x1, y1, x2 - x1, y2 - y1);
            return new Rect2d();
        }

        /// <summary>
        /// Computes the intersection of two rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle.
        /// </param>
        /// <param name="b">
        /// The second rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public static Vec4d intersectAsVec4d(in Vec4d a, in Vec4d b)
        {
            double x1 = Math.Max(a.Item1, b.Item1);
            double x2 = Math.Min(a.Item1 + a.Item3, b.Item1 + b.Item3);
            double y1 = Math.Max(a.Item1, b.Item2);
            double y2 = Math.Min(a.Item2 + a.Item4, b.Item2 + b.Item4);

            if (x2 >= x1 && y2 >= y1)
                return new Vec4d(x1, y1, x2 - x1, y2 - y1);
            return new Vec4d();
        }

        /// <summary>
        /// Computes the intersection of two rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle.
        /// </param>
        /// <param name="b">
        /// The second rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public static (double x, double y, double width, double height) intersectAsValueTuple(in (double x, double y, double width, double height) a, in (double x, double y, double width, double height) b)
        {
            double x1 = Math.Max(a.x, b.x);
            double x2 = Math.Min(a.x + a.width, b.x + b.width);
            double y1 = Math.Max(a.y, b.y);
            double y2 = Math.Min(a.y + a.height, b.y + b.height);

            if (x2 >= x1 && y2 >= y1)
                return (x1, y1, x2 - x1, y2 - y1);
            return (0, 0, 0, 0);
        }

        /// <summary>
        /// Computes the intersection of the current rectangle with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to intersect with the current rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public Rect2d intersect(Rect2d rect)
        {
            return intersect(this, rect);
        }

        /// <summary>
        /// Computes the intersection of the current rectangle with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to intersect with the current rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public Vec4d intersectAsVec4d(in Vec4d rect)
        {
            return intersectAsVec4d((Vec4d)this, rect);
        }

        /// <summary>
        /// Computes the intersection of the current rectangle with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to intersect with the current rectangle.
        /// </param>
        /// <returns>
        /// A rectangle representing the overlapping area of the two rectangles. 
        /// If the rectangles do not overlap, an empty rectangle is returned.
        /// </returns>
        public (double x, double y, double width, double height) intersectAsValueTuple(in (double x, double y, double width, double height) rect)
        {
            return intersectAsValueTuple(((double x, double y, double width, double height))this, rect);
        }

        /// <summary>
        /// Determines whether the current rectangle intersects with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to check for intersection with the current rectangle.
        /// </param>
        /// <returns>
        /// <c>true</c> if the rectangles intersect; otherwise, <c>false</c>.
        /// </returns>
        public bool intersectsWith(Rect2d rect)
        {
            return (
                (x < rect.x + rect.width) &&
                (x + width > rect.x) &&
                (y < rect.y + rect.height) &&
                (y + height > rect.y)
            );
        }

        /// <summary>
        /// Determines whether the current rectangle intersects with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to check for intersection with the current rectangle.
        /// </param>
        /// <returns>
        /// <c>true</c> if the rectangles intersect; otherwise, <c>false</c>.
        /// </returns>
        public bool intersectsWith(in Vec4d rect)
        {
            return (
                (x < rect.Item1 + rect.Item3) &&
                (x + width > rect.Item1) &&
                (y < rect.Item2 + rect.Item4) &&
                (y + height > rect.Item2)
            );
        }

        /// <summary>
        /// Determines whether the current rectangle intersects with the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to check for intersection with the current rectangle.
        /// </param>
        /// <returns>
        /// <c>true</c> if the rectangles intersect; otherwise, <c>false</c>.
        /// </returns>
        public bool intersectsWith(in (double x, double y, double width, double height) rect)
        {
            return (
                (x < rect.x + rect.width) &&
                (x + width > rect.x) &&
                (y < rect.y + rect.height) &&
                (y + height > rect.y)
            );
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of the current rectangle and the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to combine with the current rectangle.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public Rect2d union(Rect2d rect)
        {
            return union(this, rect);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of the current rectangle and the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to combine with the current rectangle.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public Vec4d unionAsVec4d(in Vec4d rect)
        {
            return unionAsVec4d((Vec4d)this, rect);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of the current rectangle and the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to combine with the current rectangle.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public (double x, double y, double width, double height) union(in (double x, double y, double width, double height) rect)
        {
            return unionAsValueTuple(((double x, double y, double width, double height))this, rect);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of two specified rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle to union.
        /// </param>
        /// <param name="b">
        /// The second rectangle to union.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static Rect2d union(Rect2d a, Rect2d b)
        {
            double x1 = Math.Min(a.x, b.x);
            double x2 = Math.Max(a.x + a.width, b.x + b.width);
            double y1 = Math.Min(a.y, b.y);
            double y2 = Math.Max(a.y + a.height, b.y + b.height);

            return new Rect2d(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of two specified rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle to union.
        /// </param>
        /// <param name="b">
        /// The second rectangle to union.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static Vec4d unionAsVec4d(in Vec4d a, in Vec4d b)
        {
            double x1 = Math.Min(a.Item1, b.Item1);
            double x2 = Math.Max(a.Item1 + a.Item3, b.Item1 + b.Item3);
            double y1 = Math.Min(a.Item2, b.Item2);
            double y2 = Math.Max(a.Item2 + a.Item4, b.Item2 + b.Item4);

            return new Vec4d(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of two specified rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle to union.
        /// </param>
        /// <param name="b">
        /// The second rectangle to union.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect2d"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static (double x, double y, double width, double height) unionAsValueTuple(in (double x, double y, double width, double height) a, in (double x, double y, double width, double height) b)
        {
            double x1 = Math.Min(a.x, b.x);
            double x2 = Math.Max(a.x + a.width, b.x + b.width);
            double y1 = Math.Min(a.y, b.y);
            double y2 = Math.Max(a.y + a.height, b.y + b.height);

            return (x1, y1, x2 - x1, y2 - y1);
        }

        #endregion


        #region Operators

        // (here R stand for a rect ( Rect2d ), p for a point ( Point ), S for a size ( Size ).)

        #region Binary

        // R + p, R + S
        public static Rect2d operator +(Rect2d a, Point b)
        {
            return new Rect2d(a.x + b.x, a.y + b.y, a.width, a.height);
        }

        public static Rect2d operator +(Rect2d a, Size b)
        {
            return new Rect2d(a.x, a.y, a.width + b.width, a.height + b.height);
        }

        // R - p, R - S
        public static Rect2d operator -(Rect2d a, Point b)
        {
            return new Rect2d(a.x - b.x, a.y - b.y, a.width, a.height);
        }

        public static Rect2d operator -(Rect2d a, Size b)
        {
            return new Rect2d(a.x, a.y, a.width - b.width, a.height - b.height);
        }

        // R & R
        public static Rect2d operator &(Rect2d a, Rect2d b)
        {
            return intersect(a, b);
        }

        // R | R
        public static Rect2d operator |(Rect2d a, Rect2d b)
        {
            return union(a, b);
        }

        #endregion

        #region Comparison

        public bool Equals(Rect2d a)
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
            return this.x == a.x && this.y == a.y && this.width == a.width && this.height == a.height;
        }

        // R == R
        public static bool operator ==(Rect2d a, Rect2d b)
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
            return a.x == b.x && a.y == b.y && a.width == b.width && a.height == b.height;
        }

        // R != R
        public static bool operator !=(Rect2d a, Rect2d b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
