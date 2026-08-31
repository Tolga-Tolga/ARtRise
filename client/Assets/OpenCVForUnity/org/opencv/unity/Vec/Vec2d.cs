using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 2-Vector struct of double [CvType.CV_64FC2, Point]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec2d : IComparable, IComparable<Vec2d>, IEquatable<Vec2d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;

        public Vec2d(double item1 = 0, double item2 = 0)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public readonly Vec2b ToVec2b() => new Vec2b((byte)Item1, (byte)Item2);
        public readonly Vec2c ToVec2c() => new Vec2c((sbyte)Item1, (sbyte)Item2);
        public readonly Vec2w ToVec2w() => new Vec2w((ushort)Item1, (ushort)Item2);
        public readonly Vec2s ToVec2s() => new Vec2s((short)Item1, (short)Item2);
        public readonly Vec2i ToVec2i() => new Vec2i((int)Item1, (int)Item2);
        public readonly Vec2f ToVec2f() => new Vec2f((float)Item1, (float)Item2);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec2d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec2d");
        }

        // IComparable<Vec2d> implementation
        public readonly int CompareTo(Vec2d other)
        {
            int result = Item1.CompareTo(other.Item1);
            if (result != 0)
            {
                return result;
            }
            return Item2.CompareTo(other.Item2);
        }

        // IEquatable<Vec2d> implementation
        public readonly bool Equals(Vec2d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec2d other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            return HashCode.Combine(Item1, Item2);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Item1.GetHashCode();
                hash = hash * 31 + Item2.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec2d otherTuple)
            {
                int result = comparer.Compare(Item1, otherTuple.Item1);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item2, otherTuple.Item2);
            }
            throw new ArgumentException("Object is not a Vec2d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec2d otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2})";
        }

        // Equality operators
        public static bool operator ==(Vec2d left, Vec2d right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec2d left, Vec2d right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<double, double>
        public static explicit operator (double, double)(in Vec2d vec)
        {
            return (vec.Item1, vec.Item2);
        }

        // explicit conversion to Point
        public static explicit operator Point(in Vec2d vec)
        {
            return new Point(vec);
        }

        // explicit conversion to Size
        public static explicit operator Size(in Vec2d vec)
        {
            return new Size(vec);
        }

        // explicit conversion from ValueTuple<double, double>
        public static explicit operator Vec2d(in (double, double) valueTuple)
        {
            return new Vec2d(valueTuple.Item1, valueTuple.Item2);
        }

        public readonly (double, double) ToValueTuple() => (Item1, Item2);

        public readonly Point ToPoint() => new Point(this);
        public readonly Size ToSize() => new Size(this);

    }
}
