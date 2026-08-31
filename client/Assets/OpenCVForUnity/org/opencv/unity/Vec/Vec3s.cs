using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 3-Vector struct of short [CvType.CV_16SC3]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec3s : IComparable, IComparable<Vec3s>, IEquatable<Vec3s>, IStructuralComparable, IStructuralEquatable
    {
        public short Item1;
        public short Item2;
        public short Item3;

        public Vec3s(short item1 = 0, short item2 = 0, short item3 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public readonly Vec3b ToVec3b() => new Vec3b((byte)Item1, (byte)Item2, (byte)Item3);
        public readonly Vec3c ToVec3c() => new Vec3c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3);
        public readonly Vec3w ToVec3w() => new Vec3w((ushort)Item1, (ushort)Item2, (ushort)Item3);
        public readonly Vec3i ToVec3i() => new Vec3i((int)Item1, (int)Item2, (int)Item3);
        public readonly Vec3f ToVec3f() => new Vec3f((float)Item1, (float)Item2, (float)Item3);
        public readonly Vec3d ToVec3d() => new Vec3d((double)Item1, (double)Item2, (double)Item3);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec3s other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec3s");
        }

        // IComparable<Vec3s> implementation
        public readonly int CompareTo(Vec3s other)
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
            return Item3.CompareTo(other.Item3);
        }

        // IEquatable<Vec3s> implementation
        public readonly bool Equals(Vec3s other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec3s other)
            {
                return Equals(other);
            }
            return false;
        }

        // GetHashCode override
        public override readonly int GetHashCode()
        {
#if NET_STANDARD_2_1
            return HashCode.Combine(Item1, Item2, Item3);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Item1.GetHashCode();
                hash = hash * 31 + Item2.GetHashCode();
                hash = hash * 31 + Item3.GetHashCode();
                return hash;
            }
#endif
        }

        // IStructuralComparable implementation
        public readonly int CompareTo(object other, IComparer comparer)
        {
            if (other is Vec3s otherTuple)
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
                return comparer.Compare(Item3, otherTuple.Item3);
            }
            throw new ArgumentException("Object is not a Vec3s");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec3s otherTuple)
            {
                return comparer.Equals(Item1, otherTuple.Item1) && comparer.Equals(Item2, otherTuple.Item2) && comparer.Equals(Item3, otherTuple.Item3);
            }
            return false;
        }

        public readonly int GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1) ^ comparer.GetHashCode(Item2) ^ comparer.GetHashCode(Item3);
        }

        // ToString override
        public override readonly string ToString()
        {
            return $"({Item1}, {Item2}, {Item3})";
        }

        // Equality operators
        public static bool operator ==(Vec3s left, Vec3s right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec3s left, Vec3s right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<short, short, short>
        public static explicit operator (short, short, short)(in Vec3s vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3);
        }

        // explicit conversion from ValueTuple<short, short, short>
        public static explicit operator Vec3s(in (short, short, short) valueTuple)
        {
            return new Vec3s(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
        }

        public readonly (short, short, short) ToValueTuple() => (Item1, Item2, Item3);
    }
}
