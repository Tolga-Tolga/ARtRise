using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 2-Vector struct of byte [CvType.CV_8UC2]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec2b : IComparable, IComparable<Vec2b>, IEquatable<Vec2b>, IStructuralComparable, IStructuralEquatable
    {
        public byte Item1;
        public byte Item2;

        public Vec2b(byte item1 = 0, byte item2 = 0)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public readonly Vec2c ToVec2c() => new Vec2c((sbyte)Item1, (sbyte)Item2);
        public readonly Vec2w ToVec2w() => new Vec2w((ushort)Item1, (ushort)Item2);
        public readonly Vec2s ToVec2s() => new Vec2s((short)Item1, (short)Item2);
        public readonly Vec2i ToVec2i() => new Vec2i((int)Item1, (int)Item2);
        public readonly Vec2f ToVec2f() => new Vec2f((float)Item1, (float)Item2);
        public readonly Vec2d ToVec2d() => new Vec2d((double)Item1, (double)Item2);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec2b other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec2b");
        }

        // IComparable<Vec2b> implementation
        public readonly int CompareTo(Vec2b other)
        {
            int result = Item1.CompareTo(other.Item1);
            if (result != 0)
            {
                return result;
            }
            return Item2.CompareTo(other.Item2);
        }

        // IEquatable<Vec2b> implementation
        public readonly bool Equals(Vec2b other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec2b other)
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
            if (other is Vec2b otherTuple)
            {
                int result = comparer.Compare(Item1, otherTuple.Item1);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item2, otherTuple.Item2);
            }
            throw new ArgumentException("Object is not a Vec2b");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec2b otherTuple)
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
        public readonly override string ToString()
        {
            return $"({Item1}, {Item2})";
        }

        // Equality operators
        public static bool operator ==(Vec2b left, Vec2b right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec2b left, Vec2b right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<byte, byte>
        public static explicit operator (byte, byte)(in Vec2b vec)
        {
            return (vec.Item1, vec.Item2);
        }

        // explicit conversion from ValueTuple<byte, byte>
        public static explicit operator Vec2b(in (byte, byte) valueTuple)
        {
            return new Vec2b(valueTuple.Item1, valueTuple.Item2);
        }

        public readonly (byte, byte) ToValueTuple() => (Item1, Item2);
    }
}
