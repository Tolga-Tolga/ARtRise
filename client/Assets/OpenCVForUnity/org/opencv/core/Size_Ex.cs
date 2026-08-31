using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class Size
    {
        public Size(in Vec2d vals)
        {
            set(vals);
        }

        public Size(in (double width, double height) vals)
        {
            set(vals);
        }

        public void set(in Vec2d vals)
        {

            width = vals.Item1;
            height = vals.Item2;

        }

        public void set(in (double width, double height) vals)
        {

            width = vals.width;
            height = vals.height;

        }

        public Vec2d cloneAsVec2d()
        {
            return new Vec2d(width, height);
        }

        public (double width, double height) cloneAsValueTuple()
        {
            return (width, height);
        }

        // explicit conversion to Vec2d
        public static explicit operator Vec2d(Size size)
        {
            return new Vec2d(size.width, size.height);
        }

        // explicit conversion to ValueTuple<double, double>
        public static explicit operator (double width, double height)(Size size)
        {
            return (size.width, size.height);
        }

        // explicit conversion from ValueTuple<double, double>
        public static explicit operator Size(in (double width, double height) valueTuple)
        {
            return new Size(valueTuple);
        }

        public Vec2d ToVec2d() => new Vec2d(width, height);

        public (double width, double height) ToValueTuple() => (width, height);


        #region Operators

        // (here S stand for a size ( Size ), alpha for a real-valued scalar ( double ).)

        #region Binary

        // S + S
        public static Size operator +(Size a, Size b)
        {
            return new Size(a.width + b.width, a.height + b.height);
        }

        // S - S
        public static Size operator -(Size a, Size b)
        {
            return new Size(a.width - b.width, a.height - b.height);
        }

        // S * alpha
        public static Size operator *(Size a, double b)
        {
            return new Size(a.width * b, a.height * b);
        }

        // S / alpha
        public static Size operator /(Size a, double b)
        {
            return new Size(a.width / b, a.height / b);
        }

        #endregion

        #region Comparison

        public bool Equals(Size a)
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
            return this.width == a.width && this.height == a.height;
        }

        // S == S
        public static bool operator ==(Size a, Size b)
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
            return a.width == b.width && a.height == b.height;
        }

        // S != S
        public static bool operator !=(Size a, Size b)
        {
            return !(a == b);
        }

        #endregion

        #endregion

    }
}

