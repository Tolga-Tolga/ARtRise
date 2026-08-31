using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 3-Vector struct of ushort [CvType.CV_16UC3]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec3w : IComparable, IComparable<Vec3w>, IEquatable<Vec3w>, IStructuralComparable, IStructuralEquatable
    {
        public ushort Item1;
        public ushort Item2;
        public ushort Item3;

        public Vec3w(ushort item1 = 0, ushort item2 = 0, ushort item3 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public readonly Vec3b ToVec3b() => new Vec3b((byte)Item1, (byte)Item2, (byte)Item3);
        public readonly Vec3c ToVec3c() => new Vec3c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3);
        public readonly Vec3s ToVec3s() => new Vec3s((short)Item1, (short)Item2, (short)Item3);
        public readonly Vec3i ToVec3i() => new Vec3i((int)Item1, (int)Item2, (int)Item3);
        public readonly Vec3f ToVec3f() => new Vec3f((float)Item1, (float)Item2, (float)Item3);
        public readonly Vec3d ToVec3d() => new Vec3d((double)Item1, (double)Item2, (double)Item3);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec3w other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec3w");
        }

        // IComparable<Vec3w> implementation
        public readonly int CompareTo(Vec3w other)
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

        // IEquatable<Vec3w> implementation
        public readonly bool Equals(Vec3w other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec3w other)
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
            if (other is Vec3w otherTuple)
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
            throw new ArgumentException("Object is not a Vec3w");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec3w otherTuple)
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
        public static bool operator ==(Vec3w left, Vec3w right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec3w left, Vec3w right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<ushort, ushort, ushort>
        public static explicit operator (ushort, ushort, ushort)(in Vec3w vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3);
        }

        // explicit conversion from ValueTuple<ushort, ushort, ushort>
        public static explicit operator Vec3w(in (ushort, ushort, ushort) valueTuple)
        {
            return new Vec3w(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
        }

        public readonly (ushort, ushort, ushort) ToValueTuple() => (Item1, Item2, Item3);
    }
}
