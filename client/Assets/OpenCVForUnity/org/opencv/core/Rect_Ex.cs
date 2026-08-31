using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Rect
    {

        public Rect(in Vec4i vals)
            : this()
        {
            set(vals);
        }

        public Rect(in (int x, int y, int width, int height) vals)
            : this()
        {
            set(vals);
        }
        public void set(in Vec4i vals)
        {
            x = vals.Item1;
            y = vals.Item2;
            width = vals.Item3;
            height = vals.Item4;
        }

        public void set(in (int x, int y, int width, int height) vals)
        {
            x = vals.x;
            y = vals.y;
            width = vals.Item3;
            height = vals.Item4;
        }

        public Vec4i cloneAsVec4i()
        {
            return new Vec4i(x, y, width, height);
        }

        public (int x, int y, int width, int height) cloneAsValueTuple()
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
        public bool contains(int x, int y)
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

        // explicit conversion to Vec4i
        public static explicit operator Vec4i(Rect rect)
        {
            return new Vec4i(rect.x, rect.y, rect.width, rect.height);
        }

        // explicit conversion to RectInt
        public static explicit operator UnityEngine.RectInt(Rect rect)
        {
            return new UnityEngine.RectInt(rect.x, rect.y, rect.width, rect.height);
        }

        // explicit conversion to ValueTuple<int, int, int, int>
        public static explicit operator (int x, int y, int width, int height)(Rect rect)
        {
            return (rect.x, rect.y, rect.width, rect.height);
        }

        // explicit conversion from ValueTuple<int, int, int, int>
        public static explicit operator Rect(in (int x, int y, int width, int height) valueTuple)
        {
            return new Rect(valueTuple);
        }

        public Vec4i ToVec4i() => new Vec4i(x, y, width, height);

        public UnityEngine.RectInt ToRectInt() => new UnityEngine.RectInt(x, y, width, height);

        public (int x, int y, int width, int height) ToValueTuple() => (x, y, width, height);

        #region Added methods

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(Rect rect)
        {
            return x <= rect.x &&
            (rect.x + rect.width) <= (x + width) &&
            y <= rect.y &&
            (rect.y + rect.height) <= (y + height);
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in Vec4i rect)
        {
            return x <= rect.Item1 &&
            (rect.Item1 + rect.Item3) <= (x + width) &&
            y <= rect.Item2 &&
            (rect.Item2 + rect.Item4) <= (y + height);
        }

        /// <remarks>
        /// checks whether the rectangle contains the point
        /// </remarks>
        public bool contains(in (int x, int y, int width, int height) rect)
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
        public void inflate(int width, int height)
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
            inflate((int)size.width, (int)size.height);
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

        public static Rect inflate(Rect rect, int x, int y)
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

        public static Vec4i inflateAsVec4i(in Vec4i rect, int x, int y)
        {
            Rect tmpRect = (Rect)rect;
            tmpRect.inflate(x, y);
            return (Vec4i)tmpRect;
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

        public static (int x, int y, int width, int height) inflateAsValueTuple(in (int x, int y, int width, int height) rect, int x, int y)
        {
            Rect tmpRect = (Rect)rect;
            tmpRect.inflate(x, y);
            return ((int x, int y, int width, int height))tmpRect;
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
        public static Rect intersect(Rect a, Rect b)
        {
            int x1 = Math.Max(a.x, b.x);
            int x2 = Math.Min(a.x + a.width, b.x + b.width);
            int y1 = Math.Max(a.y, b.y);
            int y2 = Math.Min(a.y + a.height, b.y + b.height);

            if (x2 >= x1 && y2 >= y1)
                return new Rect(x1, y1, x2 - x1, y2 - y1);
            return new Rect();
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
        public static Vec4i intersectAsVec4i(in Vec4i a, in Vec4i b)
        {
            int x1 = Math.Max(a.Item1, b.Item1);
            int x2 = Math.Min(a.Item1 + a.Item3, b.Item1 + b.Item3);
            int y1 = Math.Max(a.Item1, b.Item2);
            int y2 = Math.Min(a.Item2 + a.Item4, b.Item2 + b.Item4);

            if (x2 >= x1 && y2 >= y1)
                return new Vec4i(x1, y1, x2 - x1, y2 - y1);
            return new Vec4i();
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
        public static (int x, int y, int width, int height) intersectAsValueTuple(in (int x, int y, int width, int height) a, in (int x, int y, int width, int height) b)
        {
            int x1 = Math.Max(a.x, b.x);
            int x2 = Math.Min(a.x + a.width, b.x + b.width);
            int y1 = Math.Max(a.y, b.y);
            int y2 = Math.Min(a.y + a.height, b.y + b.height);

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
        public Rect intersect(Rect rect)
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
        public Vec4i intersectAsVec4i(in Vec4i rect)
        {
            return intersectAsVec4i((Vec4i)this, rect);
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
        public (int x, int y, int width, int height) intersectAsValueTuple(in (int x, int y, int width, int height) rect)
        {
            return intersectAsValueTuple(((int x, int y, int width, int height))this, rect);
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
        public bool intersectsWith(Rect rect)
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
        public bool intersectsWith(in Vec4i rect)
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
        public bool intersectsWith(in (int x, int y, int width, int height) rect)
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
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public Rect union(Rect rect)
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
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public Vec4i unionAsVec4i(in Vec4i rect)
        {
            return unionAsVec4i((Vec4i)this, rect);
        }

        /// <summary>
        /// Returns a new rectangle that represents the union of the current rectangle and the specified rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle to combine with the current rectangle.
        /// </param>
        /// <returns>
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both the current rectangle and the specified rectangle.
        /// </returns>
        public (int x, int y, int width, int height) union(in (int x, int y, int width, int height) rect)
        {
            return union(((int x, int y, int width, int height))this, rect);
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
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>

        public static Rect union(Rect a, Rect b)
        {
            int x1 = Math.Min(a.x, b.x);
            int x2 = Math.Max(a.x + a.width, b.x + b.width);
            int y1 = Math.Min(a.y, b.y);
            int y2 = Math.Max(a.y + a.height, b.y + b.height);

            return new Rect(x1, y1, x2 - x1, y2 - y1);
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
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>

        public static Vec4i unionAsVec4i(in Vec4i a, in Vec4i b)
        {
            int x1 = Math.Min(a.Item1, b.Item1);
            int x2 = Math.Max(a.Item1 + a.Item3, b.Item1 + b.Item3);
            int y1 = Math.Min(a.Item2, b.Item2);
            int y2 = Math.Max(a.Item2 + a.Item4, b.Item2 + b.Item4);

            return new Vec4i(x1, y1, x2 - x1, y2 - y1);
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
        /// A new <see cref="Rect"/> that represents the smallest rectangle that can contain both <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>

        public static (int x, int y, int width, int height) union(in (int x, int y, int width, int height) a, in (int x, int y, int width, int height) b)
        {
            int x1 = Math.Min(a.x, b.x);
            int x2 = Math.Max(a.x + a.width, b.x + b.width);
            int y1 = Math.Min(a.y, b.y);
            int y2 = Math.Max(a.y + a.height, b.y + b.height);

            return (x1, y1, x2 - x1, y2 - y1);
        }

        #endregion


        #region Operators

        // (here R stand for a rect ( Rect ), p for a point ( Point ), S for a size ( Size ).)

        #region Binary

        // R + p, R + S
        public static Rect operator +(Rect a, Point b)
        {
            return new Rect(a.x + (int)b.x, a.y + (int)b.y, a.width, a.height);
        }

        public static Rect operator +(Rect a, Size b)
        {
            return new Rect(a.x, a.y, a.width + (int)b.width, a.height + (int)b.height);
        }

        // R - p, R - S
        public static Rect operator -(Rect a, Point b)
        {
            return new Rect(a.x - (int)b.x, a.y - (int)b.y, a.width, a.height);
        }

        public static Rect operator -(Rect a, Size b)
        {
            return new Rect(a.x, a.y, a.width - (int)b.width, a.height - (int)b.height);
        }

        // R & R
        public static Rect operator &(Rect a, Rect b)
        {
            return intersect(a, b);
        }

        // R | R
        public static Rect operator |(Rect a, Rect b)
        {
            return union(a, b);
        }

        #endregion

        #region Comparison

        public bool Equals(Rect a)
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
        public static bool operator ==(Rect a, Rect b)
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
        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}
