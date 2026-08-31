using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 4-Vector struct of double [CvType.CV_64FC4]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec4d : IComparable, IComparable<Vec4d>, IEquatable<Vec4d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;
        public double Item3;
        public double Item4;

        public Vec4d(double item1 = 0, double item2 = 0, double item3 = 0, double item4 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public readonly Vec4b ToVec4b() => new Vec4b((byte)Item1, (byte)Item2, (byte)Item3, (byte)Item4);
        public readonly Vec4c ToVec4c() => new Vec4c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3, (sbyte)Item4);
        public readonly Vec4w ToVec4w() => new Vec4w((ushort)Item1, (ushort)Item2, (ushort)Item3, (ushort)Item4);
        public readonly Vec4s ToVec4s() => new Vec4s((short)Item1, (short)Item2, (short)Item3, (short)Item4);
        public readonly Vec4i ToVec4i() => new Vec4i((int)Item1, (int)Item2, (int)Item3, (int)Item4);
        public readonly Vec4f ToVec4f() => new Vec4f((float)Item1, (float)Item2, (float)Item3, (float)Item4);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec4d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec4d");
        }

        // IComparable<Vec4d> implementation
        public readonly int CompareTo(Vec4d other)
        {
            int result = Item1.CompareTo(other.Item1);
            if (result != 0)
            {
                return result;
            }
            result = Item2.CompareTo(other.Item2);
            if (result != 0)
            {
                return result;
            }
            result = Item3.CompareTo(other.Item3);
            if (result != 0)
            {
                return result;
            }
            return Item4.CompareTo(other.Item4);
        }

        // IEquatable<Vec4d> implementation
        public readonly bool Equals(Vec4d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec4d other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            return HashCode.Combine(Item1, Item2, Item3, Item4);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Item1.GetHashCode();
                hash = hash * 31 + Item2.GetHashCode();
                hash = hash * 31 + Item3.GetHashCode();
                hash = hash * 31 + Item4.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec4d otherTuple)
            {
                int result = comparer.Compare(Item1, otherTuple.Item1);
                if (result != 0)
                {
                    return result;
                }
                result = comparer.Compare(Item2, otherTuple.Item2);
                if (result != 0)
                {
                    return result;
                }
                result = comparer.Compare(Item3, otherTuple.Item3);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item4, otherTuple.Item4);
            }
            throw new ArgumentException("Object is not a Vec4d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec4d otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2) && comparer.Equals(Item3, otherTuple.Item3) && comparer.Equals(Item4, otherTuple.Item4);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2) ^ comparer.GetHashCode(Item3) ^ comparer.GetHashCode(Item4);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2}, {Item3}, {Item4})";
        }

        public static bool operator ==(Vec4d left, Vec4d right)
        {
            return left.Equals(right);
        }

        // Equality operators
        public static bool operator !=(Vec4d left, Vec4d right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<double, double, double, double>
        public static explicit operator (double, double, double, double)(in Vec4d vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to Rect2d
        public static explicit operator Rect2d(in Vec4d vec)
        {
            return new Rect2d(vec);
        }

        // explicit conversion to Scalar
        public static explicit operator Scalar(in Vec4d vec)
        {
            return new Scalar(vec);
        }

        // explicit conversion from ValueTuple<double, double, double, double>
        public static explicit operator Vec4d(in (double, double, double, double) valueTuple)
        {
            return new Vec4d(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4);
        }

        public readonly (double, double, double, double) ToValueTuple() => (Item1, Item2, Item3, Item4);

        public readonly Rect2d ToRect2d() => new Rect2d(this);

        public readonly Scalar ToScalar() => new Scalar(this);
    }
}
