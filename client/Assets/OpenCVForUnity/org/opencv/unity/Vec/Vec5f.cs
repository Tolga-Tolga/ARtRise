using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 5-Vector struct of float [CvType.CV_32FC5, MatOfRotatedRect]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec5f : IComparable, IComparable<Vec5f>, IEquatable<Vec5f>, IStructuralComparable, IStructuralEquatable
    {
        public float Item1;
        public float Item2;
        public float Item3;
        public float Item4;
        public float Item5;

        public Vec5f(float item1 = 0, float item2 = 0, float item3 = 0, float item4 = 0, float item5 = 0)
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
        public readonly Vec5d ToVec5d() => new Vec5d((double)Item1, (double)Item2, (double)Item3, (double)Item4, (double)Item5);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec5f other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec5f");
        }

        // IComparable<Vec5f> implementation
        public readonly int CompareTo(Vec5f other)
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

        // IEquatable<Vec5f> implementation
        public readonly bool Equals(Vec5f other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4 && Item5 == other.Item5;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec5f other)
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
            if (other is Vec5f otherTuple)
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
            throw new ArgumentException("Object is not a Vec5f");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec5f otherTuple)
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

        // Equality operators
        public static bool operator ==(Vec5f left, Vec5f right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec5f left, Vec5f right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<float, float, float, float, float>
        public static explicit operator (float, float, float, float, float)(in Vec5f vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4, vec.Item5);
        }

        // explicit conversion from ValueTuple<float, float, float, float, float>
        public static explicit operator Vec5f(in (float, float, float, float, float) valueTuple)
        {
            return new Vec5f(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5);
        }

        public readonly (float, float, float, float, float) ToValueTuple() => (Item1, Item2, Item3, Item4, Item5);
    }
}
