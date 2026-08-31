using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    public partial class TermCriteria
    {

        /// <remarks>
        /// Termination criteria for iterative algorithms.
        /// </remarks>
        /// <param name="type">
        /// the type of termination criteria: COUNT, EPS or COUNT + EPS.
        /// </param>
        /// <param name="maxCount">
        /// the maximum number of iterations/elements.
        /// </param>
        /// <param name="epsilon">
        /// the desired accuracy.
        /// </param>
        public TermCriteria(in Vec3d vals)
        {
            set(vals);
        }

        /// <remarks>
        /// Termination criteria for iterative algorithms.
        /// </remarks>
        /// <param name="type">
        /// the type of termination criteria: COUNT, EPS or COUNT + EPS.
        /// </param>
        /// <param name="maxCount">
        /// the maximum number of iterations/elements.
        /// </param>
        /// <param name="epsilon">
        /// the desired accuracy.
        /// </param>
        public TermCriteria(in (double type, double maxCount, double epsilon) vals)
        {
            set(vals);
        }

        public void set(in Vec3d vals)
        {

            type = (int)vals.Item1;
            maxCount = (int)vals.Item2;
            epsilon = vals.Item3;

        }

        public void set(in (double type, double maxCount, double epsilon) vals)
        {

            type = (int)vals.Item1;
            maxCount = (int)vals.Item2;
            epsilon = vals.Item3;

        }

        public Vec3d cloneAsVec3d()
        {
            return new Vec3d(type, maxCount, epsilon);
        }

        public (double type, double maxCount, double epsilon) cloneAsValueTuple()
        {
            return (type, maxCount, epsilon);
        }

        // explicit conversion to Vec3d
        public static explicit operator Vec3d(TermCriteria termCriteria)
        {
            return new Vec3d(termCriteria.type, termCriteria.maxCount, termCriteria.epsilon);
        }

        // explicit conversion to ValueTuple<double, double, double>
        public static explicit operator (double type, double maxCount, double epsilon)(TermCriteria termCriteria)
        {
            return (termCriteria.type, termCriteria.maxCount, termCriteria.epsilon);
        }

        // explicit conversion from ValueTuple<double, double, double>
        public static explicit operator TermCriteria(in (double type, double maxCount, double epsilon) valueTuple)
        {
            return new TermCriteria(valueTuple);
        }

        public Vec3d ToVec3d() => new Vec3d(type, maxCount, epsilon);

        public (double type, double maxCount, double epsilon) ToValueTuple() => (type, maxCount, epsilon);


        #region Operators

        // (here T stand for a termcriteria ( TermCriteria ).)

        #region Comparison

        public bool Equals(TermCriteria a)
        {
            // If both are same instance, return true.
            if (System.Object.ReferenceEquals(this, a))
            {
                return true;
            }

            // If object is null, return false.
            if ((object)a == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.type == a.type && this.maxCount == a.maxCount && this.epsilon == a.epsilon;
        }

        // T == T
        public static bool operator ==(TermCriteria a, TermCriteria b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.type == b.type && a.maxCount == b.maxCount && a.epsilon == b.epsilon;
        }

        // T != T
        public static bool operator !=(TermCriteria a, TermCriteria b)
        {
            return !(a == b);
        }


        #endregion

        #endregion

    }
}
