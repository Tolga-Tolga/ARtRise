using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class specifying a continuous subsequence (slice) of a sequence.
    /// </summary>
    /// <remarks>
    /// The class is used to specify a row or a column span in a matrix ( Mat ) and for many other purposes.
    /// Range(a,b) is basically the same as a:b in Matlab or a..b in Python. As in Python, start is an
    /// inclusive left boundary of the range and end is an exclusive right boundary of the range. Such a
    /// half-opened interval is usually denoted as \f$[start,end)\f$.
    ///
    /// The static method Range::all() returns a special variable that means "the whole sequence" or "the
    /// whole range", just like " : " in Matlab or " ... " in Python. All the methods and functions in
    /// OpenCV that take Range support this special Range::all() value. But, of course, in case of your own
    /// custom processing, you will probably have to check and handle it explicitly:
    /// <example>
    /// <code language="c++">
    ///     void my_function(..., const Range&amp; r, ....)
    ///     {
    ///         if(r == Range::all()) {
    ///             // process all the data
    ///         }
    ///         else {
    ///             // process [r.start, r.end)
    ///         }
    ///     }
    /// </code>
    /// </example>
    ///
    /// <para>
    /// C++: cv::Range Class Reference @see https://docs.opencv.org/4.10.0/da/d35/classcv_1_1Range.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class Range : IEquatable<Range>
    {

        public int start, end;

        public Range(int s, int e)
        {
            this.start = s;
            this.end = e;
        }

        /// <remarks>
        /// default constructor
        /// </remarks>
        public Range()
            : this(0, 0)
        {

        }

        public Range(double[] vals)
        {
            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                start = vals.Length > 0 ? (int)vals[0] : 0;
                end = vals.Length > 1 ? (int)vals[1] : 0;
            }
            else
            {
                start = 0;
                end = 0;
            }

        }

        public int size()
        {
            return empty() ? 0 : end - start;
        }

        public bool empty()
        {
            return end <= start;
        }

        public static Range all()
        {
            return new Range(int.MinValue, int.MaxValue);


        }

        public Range intersection(Range r1)
        {
            Range r = new Range(Math.Max(r1.start, this.start), Math.Min(r1.end, this.end));
            r.end = Math.Max(r.end, r.start);
            return r;
        }

        public Range shift(int delta)
        {
            return new Range(start + delta, end + delta);
        }

        public Range clone()
        {
            return new Range(start, end);
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(start);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(end);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Range))
                return false;
            if ((Range)obj == this)
                return true;

            Range it = (Range)obj;
            return start == it.start && end == it.end;
        }

        //@Override
        public override string ToString()
        {
            return "[" + start + ", " + end + "]";
        }

    }
}
