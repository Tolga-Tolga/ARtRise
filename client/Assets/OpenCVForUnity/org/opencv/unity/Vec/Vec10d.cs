using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.ImgprocModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 10-Vector struct of double [CvType.CV_64FC10, Moments]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec10d : IComparable, IComparable<Vec10d>, IEquatable<Vec10d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;
        public double Item3;
        public double Item4;
        public double Item5;
        public double Item6;
        public double Item7;
        public double Item8;
        public double Item9;
        public double Item10;

        public Vec10d(double item1 = 0, double item2 = 0, double item3 = 0, double item4 = 0, double item5 = 0, double item6 = 0, double item7 = 0, double item8 = 0, double item9 = 0, double item10 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
            Item9 = item9;
            Item10 = item10;
        }

        //public readonly Vec10b ToVec10b() => new Vec10b((byte)Item1, (byte)Item2, (byte)Item3, (byte)Item4, (byte)Item5, (byte)Item6, (byte)Item7, (byte)Item8, (byte)Item9, (byte)Item10);
        //public readonly Vec10c ToVec10c() => new Vec10c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3, (sbyte)Item4, (sbyte)Item5, (sbyte)Item6, (sbyte)Item7, (sbyte)Item8, (sbyte)Item9, (sbyte)Item10);
        //public readonly Vec10w ToVec10w() => new Vec10w((ushort)Item1, (ushort)Item2, (ushort)Item3, (ushort)Item4, (ushort)Item5, (ushort)Item6, (ushort)Item7, (ushort)Item8, (ushort)Item9, (ushort)Item10);
        //public readonly Vec10s ToVec10s() => new Vec10s((short)Item1, (short)Item2, (short)Item3, (short)Item4, (short)Item5, (short)Item6, (short)Item7, (short)Item8, (short)Item9, (short)Item10);
        //public readonly Vec10i ToVec10i() => new Vec10i((int)Item1, (int)Item2, (int)Item3, (int)Item4, (int)Item5, (int)Item6, (int)Item7, (int)Item8, (int)Item9, (int)Item10);
        //public readonly Vec10f ToVec10f() => new Vec10f((float)Item1, (float)Item2, (float)Item3, (float)Item4, (double)Item5, (double)Item6, (double)Item7, (double)Item8, (double)Item9, (double)Item10);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec10d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec10d");
        }

        // IComparable<Vec10d> implementation
        public readonly int CompareTo(Vec10d other)
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
            result = Item7.CompareTo(other.Item7);
            if (result != 0)
            {
                return result;
            }
            result = Item8.CompareTo(other.Item8);
            if (result != 0)
            {
                return result;
            }
            result = Item9.CompareTo(other.Item9);
            if (result != 0)
            {
                return result;
            }
            return Item10.CompareTo(other.Item10);
        }

        // IEquatable<Vec10d> implementation
        public readonly bool Equals(Vec10d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4
                && Item5 == other.Item5 && Item6 == other.Item6 && Item7 == other.Item7 && Item8 == other.Item8 && Item9 == other.Item9 && Item10 == other.Item10;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec10d other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            HashCode hash = new HashCode();
            hash.Add(Item1);
            hash.Add(Item2);
            hash.Add(Item3);
            hash.Add(Item4);
            hash.Add(Item5);
            hash.Add(Item6);
            hash.Add(Item7);
            hash.Add(Item8);
            hash.Add(Item9);
            hash.Add(Item10);
            return hash.ToHashCode();
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
                hash = hash * 31 + Item8.GetHashCode();
                hash = hash * 31 + Item9.GetHashCode();
                hash = hash * 31 + Item10.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec10d otherTuple)
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
                result = comparer.Compare(Item7, otherTuple.Item7);
                if (result != 0)
                {
                    return result;
                }
                result = comparer.Compare(Item8, otherTuple.Item8);
                if (result != 0)
                {
                    return result;
                }
                result = comparer.Compare(Item9, otherTuple.Item9);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item10, otherTuple.Item10);
            }
            throw new ArgumentException("Object is not a Vec10d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec10d otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2) && comparer.Equals(Item3, otherTuple.Item3) && comparer.Equals(Item4, otherTuple.Item4)
                    && comparer.Equals(Item5, otherTuple.Item5) && comparer.Equals(Item6, otherTuple.Item6) && comparer.Equals(Item7, otherTuple.Item7) && comparer.Equals(Item8, otherTuple.Item8)
                    && comparer.Equals(Item9, otherTuple.Item9) && comparer.Equals(Item10, otherTuple.Item10);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2) ^ comparer.GetHashCode(Item3) ^ comparer.GetHashCode(Item4)
                ^ comparer.GetHashCode(Item5) ^ comparer.GetHashCode(Item6) ^ comparer.GetHashCode(Item7) ^ comparer.GetHashCode(Item8) ^ comparer.GetHashCode(Item9) ^ comparer.GetHashCode(Item10);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}, {Item7}, {Item8}, {Item9}, {Item10})";
        }

        public static bool operator ==(Vec10d left, Vec10d right)
        {
            return left.Equals(right);
        }

        // Equality operators
        public static bool operator !=(Vec10d left, Vec10d right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<double, double, double, double, double, double, double, double, double, double>
        public static explicit operator (double, double, double, double, double, double, double, double, double, double)(in Vec10d vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4, vec.Item5, vec.Item6, vec.Item7, vec.Item8, vec.Item9, vec.Item10);
        }

        // explicit conversion to Moments
        public static explicit operator Moments(in Vec10d vec)
        {
            return new Moments(vec);
        }

        // explicit conversion from ValueTuple<double, double, double, double, double, double, double, double, double, double>
        public static explicit operator Vec10d(in (double, double, double, double, double, double, double, double, double, double) valueTuple)
        {
            return new Vec10d(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5,
                valueTuple.Item6, valueTuple.Item7, valueTuple.Item8, valueTuple.Item9, valueTuple.Item10);
        }

        public readonly (double, double, double, double, double, double, double, double, double, double) ToValueTuple() => (Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10);

        public readonly Moments ToMoments() => new Moments(this);
    }
}
