using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 7-Vector struct of double [CvType.CV_32FC7]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec7d : IComparable, IComparable<Vec7d>, IEquatable<Vec7d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;
        public double Item3;
        public double Item4;
        public double Item5;
        public double Item6;
        public double Item7;

        public Vec7d(double item1 = 0, double item2 = 0, double item3 = 0, double item4 = 0, double item5 = 0, double item6 = 0, double item7 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        //public readonly Vec7b ToVec7b() => new Vec7b((byte)Item1, (byte)Item2, (byte)Item3, (byte)Item4, (byte) Item5, (byte)Item6, (byte) Item7);
        //public readonly Vec7c ToVec7c() => new Vec7c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3, (sbyte)Item4, (sbyte)Item5, (sbyte)Item6, (sbyte)Item7);
        //public readonly Vec7w ToVec7w() => new Vec7w((ushort)Item1, (ushort)Item2, (ushort)Item3, (ushort)Item4, (ushort)Item5, (ushort)Item6, (ushort)Item7);
        //public readonly Vec7s ToVec7s() => new Vec7s((short)Item1, (short)Item2, (short)Item3, (short)Item4, (short)Item5, (short)Item6, (short)Item7);
        //public readonly Vec7i ToVec7i() => new Vec7i((int)Item1, (int)Item2, (int)Item3, (int)Item4, (int)Item5, (int)Item6, (int)Item7);
        public readonly Vec7f ToVec7f() => new Vec7f((float)Item1, (float)Item2, (float)Item3, (float)Item4, (float)Item5, (float)Item6, (float)Item7);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec7d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec7d");
        }

        // IComparable<Vec7d> implementation
        public readonly int CompareTo(Vec7d other)
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
            result = Item5.CompareTo(other.Item5);
            if (result != 0)
            {
                return result;
            }
            result = Item6.CompareTo(other.Item6);
            if (result != 0)
            {
                return result;
            }
            return Item7.CompareTo(other.Item7);
        }

        // IEquatable<Vec7d> implementation
        public readonly bool Equals(Vec7d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4
                 && Item5 == other.Item5 && Item6 == other.Item6 && Item7 == other.Item7;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec7d other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Item1.GetHashCode();
                hash = hash * 31 + Item2.GetHashCode();
                hash = hash * 31 + Item3.GetHashCode();
                hash = hash * 31 + Item4.GetHashCode();
                hash = hash * 31 + Item5.GetHashCode();
                hash = hash * 31 + Item6.GetHashCode();
                hash = hash * 31 + Item7.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec7d otherTuple)
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
                result = comparer.Compare(Item5, otherTuple.Item5);
                if (result != 0)
                {
                    return result;
                }
                result = comparer.Compare(Item6, otherTuple.Item6);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item7, otherTuple.Item7);
            }
            throw new ArgumentException("Object is not a Vec7d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec7d otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2) && comparer.Equals(Item3, otherTuple.Item3) && comparer.Equals(Item4, otherTuple.Item4)
                     && comparer.Equals(Item5, otherTuple.Item5) && comparer.Equals(Item6, otherTuple.Item6) && comparer.Equals(Item7, otherTuple.Item7);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2) ^ comparer.GetHashCode(Item3) ^ comparer.GetHashCode(Item4)
                 ^ comparer.GetHashCode(Item5) ^ comparer.GetHashCode(Item6) ^ comparer.GetHashCode(Item7);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}, {Item7})";
        }

        // Equality operators
        public static bool operator ==(Vec7d left, Vec7d right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec7d left, Vec7d right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<double, double, double, double, double, double, double>
        public static explicit operator (double, double, double, double, double, double, double)(in Vec7d vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4, vec.Item5, vec.Item6, vec.Item7);
        }

        // explicit conversion from ValueTuple<double, double, double, double, double, double, double>
        public static explicit operator Vec7d(in (double, double, double, double, double, double, double) valueTuple)
        {
            return new Vec7d(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, valueTuple.Item6, valueTuple.Item7);
        }

        public readonly (double, double, double, double, double, double, double) ToValueTuple() => (Item1, Item2, Item3, Item4, Item5, Item6, Item7);

    }
}
