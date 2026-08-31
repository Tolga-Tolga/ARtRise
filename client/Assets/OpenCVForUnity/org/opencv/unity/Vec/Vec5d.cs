using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 5-Vector struct of double [CvType.CV_64FC5, RotatedRect]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec5d : IComparable, IComparable<Vec5d>, IEquatable<Vec5d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;
        public double Item3;
        public double Item4;
        public double Item5;

        public Vec5d(double item1 = 0, double item2 = 0, double item3 = 0, double item4 = 0, double item5 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        //public readonly Vec5b ToVec5b() => new Vec5b((byte)Item1, (byte)Item2, (byte)Item3, (byte)Item4, (byte)Item5);
        //public readonly Vec5c ToVec5c() => new Vec5c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3, (sbyte)Item4, (sbyte)Item5);
        //public readonly Vec5w ToVec5w() => new Vec5w((ushort)Item1, (ushort)Item2, (ushort)Item3, (ushort)Item4, (ushort)Item5);
        //public readonly Vec5s ToVec5s() => new Vec5s((short)Item1, (short)Item2, (short)Item3, (short)Item4, (short)Item5);
        //public readonly Vec5i ToVec5i() => new Vec5i((int)Item1, (int)Item2, (int)Item3, (int)Item4, (int)Item5);
        public readonly Vec5f ToVec5f() => new Vec5f((float)Item1, (float)Item2, (float)Item3, (float)Item4, (float)Item5);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec5d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec5d");
        }

        // IComparable<Vec5d> implementation
        public readonly int CompareTo(Vec5d other)
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
            result = Item4.CompareTo(other.Item4);
            if (result != 0)
            {
                return result;
            }
            return Item5.CompareTo(other.Item5);
        }

        // IEquatable<Vec5d> implementation
        public readonly bool Equals(Vec5d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4 && Item5 == other.Item5;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec5d other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Item1.GetHashCode();
                hash = hash * 31 + Item2.GetHashCode();
                hash = hash * 31 + Item3.GetHashCode();
                hash = hash * 31 + Item4.GetHashCode();
                hash = hash * 31 + Item5.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec5d otherTuple)
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
                result = comparer.Compare(Item4, otherTuple.Item4);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item5, otherTuple.Item5);
            }
            throw new ArgumentException("Object is not a Vec5d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec5d otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2) && comparer.Equals(Item3, otherTuple.Item3) && comparer.Equals(Item4, otherTuple.Item4) && comparer.Equals(Item5, otherTuple.Item5);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2) ^ comparer.GetHashCode(Item3) ^ comparer.GetHashCode(Item4) ^ comparer.GetHashCode(Item5);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2}, {Item3}, {Item4}, {Item5})";
        }

        public static bool operator ==(Vec5d left, Vec5d right)
        {
            return left.Equals(right);
        }

        // Equality operators
        public static bool operator !=(Vec5d left, Vec5d right)
        {
            return !(left == right);
        }


        // explicit conversion to ValueTuple<double, double, double, double, double>
        public static explicit operator (double, double, double, double, double)(in Vec5d vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4, vec.Item5);
        }

        // explicit conversion to RotatedRect
        public static explicit operator RotatedRect(in Vec5d vec)
        {
            return new RotatedRect(vec);
        }

        // explicit conversion from ValueTuple<double, double, double, double, double>
        public static explicit operator Vec5d(in (double, double, double, double, double) valueTuple)
        {
            return new Vec5d(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5);
        }

        public readonly (double, double, double, double, double) ToValueTuple() => (Item1, Item2, Item3, Item4, Item5);

        public readonly RotatedRect ToRotatedRect() => new RotatedRect(this);
    }
}
