using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 4-Vector struct of float [CvType.CV_32FC4, DMatch]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec4f : IComparable, IComparable<Vec4f>, IEquatable<Vec4f>, IStructuralComparable, IStructuralEquatable
    {
        public float Item1;
        public float Item2;
        public float Item3;
        public float Item4;

        public Vec4f(float item1 = 0, float item2 = 0, float item3 = 0, float item4 = 0)
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
        public readonly Vec4d ToVec4d() => new Vec4d((double)Item1, (double)Item2, (double)Item3, (double)Item4);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec4f other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec4f");
        }

        // IComparable<Vec4f> implementation
        public readonly int CompareTo(Vec4f other)
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

        // IEquatable<Vec4f> implementation
        public readonly bool Equals(Vec4f other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3 && Item4 == other.Item4;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec4f other)
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
            if (other is Vec4f otherTuple)
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
            throw new ArgumentException("Object is not a Vec4f");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec4f otherTuple)
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

        // Equality operators
        public static bool operator ==(Vec4f left, Vec4f right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec4f left, Vec4f right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<float, float, float, float>
        public static explicit operator (float, float, float, float)(in Vec4f vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to Vector4
        public static explicit operator UnityEngine.Vector4(in Vec4f vec)
        {
            return new UnityEngine.Vector4(vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to Quaternion
        public static explicit operator UnityEngine.Quaternion(in Vec4f vec)
        {
            return new UnityEngine.Quaternion(vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to Color
        public static explicit operator UnityEngine.Color(in Vec4f vec)
        {
            return new UnityEngine.Color(vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to Rect
        public static explicit operator UnityEngine.Rect(in Vec4f vec)
        {
            return new UnityEngine.Rect(vec.Item1, vec.Item2, vec.Item3, vec.Item4);
        }

        // explicit conversion to DMatch
        public static explicit operator DMatch(in Vec4f vec)
        {
            return new DMatch(vec);
        }

        // explicit conversion from ValueTuple<float, float, float, float>
        public static explicit operator Vec4f(in (float, float, float, float) valueTuple)
        {
            return new Vec4f(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4);
        }

        public readonly (float, float, float, float) ToValueTuple() => (Item1, Item2, Item3, Item4);

        public readonly UnityEngine.Vector4 ToVector4() => new UnityEngine.Vector4(Item1, Item2, Item3, Item4);

        public readonly UnityEngine.Quaternion ToQuaternion() => new UnityEngine.Quaternion(Item1, Item2, Item3, Item4);

        public readonly UnityEngine.Color ToColor() => new UnityEngine.Color(Item1, Item2, Item3, Item4);

        public readonly UnityEngine.Rect ToRect() => new UnityEngine.Rect(Item1, Item2, Item3, Item4);

        public readonly DMatch ToDMatch() => new DMatch(this);
    }
}
