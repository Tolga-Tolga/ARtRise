using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// 3-Vector struct of double [CvType.CV_64FC3, Point3]
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec3d : IComparable, IComparable<Vec3d>, IEquatable<Vec3d>, IStructuralComparable, IStructuralEquatable
    {
        public double Item1;
        public double Item2;
        public double Item3;

        public Vec3d(double item1 = 0, double item2 = 0, double item3 = 0)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public readonly Vec3b ToVec3b() => new Vec3b((byte)Item1, (byte)Item2, (byte)Item3);
        public readonly Vec3c ToVec3c() => new Vec3c((sbyte)Item1, (sbyte)Item2, (sbyte)Item3);
        public readonly Vec3w ToVec3w() => new Vec3w((ushort)Item1, (ushort)Item2, (ushort)Item3);
        public readonly Vec3s ToVec3s() => new Vec3s((short)Item1, (short)Item2, (short)Item3);
        public readonly Vec3i ToVec3i() => new Vec3i((int)Item1, (int)Item2, (int)Item3);
        public readonly Vec3f ToVec3f() => new Vec3f((float)Item1, (float)Item2, (float)Item3);

        // IComparable implementation
        public readonly int CompareTo(object obj)
        {
            if (obj is Vec3d other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Vec3d");
        }

        // IComparable<Vec2b> implementation
        public readonly int CompareTo(Vec3d other)
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

        // IEquatable<Vec3d> implementation
        public readonly bool Equals(Vec3d other)
        {
            return Item1 == other.Item1 && Item2 == other.Item2 && Item3 == other.Item3;
        }

        // Equals(object) override
        public override readonly bool Equals(object obj)
        {
            if (obj is Vec3d other)
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
            if (other is Vec3d otherTuple)
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
            throw new ArgumentException("Object is not a Vec3d");
        }

        // IStructuralEquatable implementation
        public readonly bool Equals(object other, IEqualityComparer comparer)
        {
            if (other is Vec3d otherTuple)
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
        public static bool operator ==(Vec3d left, Vec3d right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec3d left, Vec3d right)
        {
            return !(left == right);
        }

        // explicit conversion to ValueTuple<double, double, double>
        public static explicit operator (double, double, double)(in Vec3d vec)
        {
            return (vec.Item1, vec.Item2, vec.Item3);
        }

        // explicit conversion to Point3
        public static explicit operator Point3(in Vec3d vec)
        {
            return new Point3(vec);
        }

        // explicit conversion to TermCriteria
        public static explicit operator TermCriteria(in Vec3d vec)
        {
            return new TermCriteria(vec);
        }

        // explicit conversion from ValueTuple<double, double, double>
        public static explicit operator Vec3d(in (double, double, double) valueTuple)
        {
            return new Vec3d(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
        }

        public readonly (double, double, double) ToValueTuple() => (Item1, Item2, Item3);

        public readonly Point3 ToPoint3() => new Point3(Item1, Item2, Item3);

        public readonly TermCriteria ToTermCriteria() => new TermCriteria(this);
    }
}
