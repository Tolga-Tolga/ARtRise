using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 2-Vector struct of float [CvType.CV_32FC2]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec2f : IComparable, IComparable<Vec2f>, IEquatable<Vec2f>, IStructuralComparable, IStructuralEquatable
    {
        public float Item1;
        public float Item2;

        public Vec2f(float item1 = 0, float item2 = 0)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public readonly Vec2b ToVec2b() => new Vec2b((byte)Item1, (byte)Item2);
        public readonly Vec2c ToVec2c() => new Vec2c((sbyte)Item1, (sbyte)Item2);
        public readonly Vec2w ToVec2w() => new Vec2w((ushort)Item1, (ushort)Item2);
        public readonly Vec2s ToVec2s() => new Vec2s((short)Item1, (short)Item2);
        public readonly Vec2i ToVec2i() => new Vec2i((int)Item1, (int)Item2);
        public readonly Vec2d ToVec2d() => new Vec2d((double)Item1, (double)Item2);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec2f other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec2f");
        }

        // IComparable<Vec2f> implementation
        public readonly int CompareTo(Vec2f other)
        {
            int result = Item1.CompareTo(other.Item1);
            if (result != 0)
            {
                return result;
            }
            return Item2.CompareTo(other.Item2);
        }

        // IEquatable<Vec2f> implementation
        public readonly bool Equals(Vec2f other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec2f other)
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
            if (other is Vec2f otherTuple)
            {
                int result = comparer.Compare(Item1, otherTuple.Item1);
                if (result != 0)
                {
                    return result;
                }
                return comparer.Compare(Item2, otherTuple.Item2);
            }
            throw new ArgumentException("Object is not a Vec2f");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec2f otherTuple)
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
        public static bool operator ==(Vec2f left, Vec2f right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec2f left, Vec2f right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<float, float>
        public static explicit operator (float, float)(in Vec2f vec)
        {
            return (vec.Item1, vec.Item2);
        }

        // explicit conversion to Vector2
        public static explicit operator UnityEngine.Vector2(in Vec2f vec)
        {
            return new UnityEngine.Vector2(vec.Item1, vec.Item2);
        }

        // explicit conversion from ValueTuple<float, float>
        public static explicit operator Vec2f(in (float, float) valueTuple)
        {
            return new Vec2f(valueTuple.Item1, valueTuple.Item2);
        }

        public readonly (float, float) ToValueTuple() => (Item1, Item2);

        public readonly UnityEngine.Vector2 ToVector2() => new UnityEngine.Vector2(Item1, Item2);
    }
}
