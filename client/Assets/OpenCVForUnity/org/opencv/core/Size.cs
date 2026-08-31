using System;
using OpenCVForUnity.UnityIntegration;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Template class for specifying the size of an image or rectangle.
    ///
    /// <para>
    /// C++: cv::Size_&lt;_Tp&gt; Class Template Reference @see https://docs.opencv.org/4.10.0/d6/d50/classcv_1_1Size__.html
    /// </para>
    /// </summary>
    [Serializable]
    public partial class Size : IEquatable<Size>
    {

        /// <remarks>
        /// the width
        /// </remarks>
        public double width;

        /// <remarks>
        /// the height
        /// </remarks>
        public double height;

        public Size(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        /// <remarks>
        /// default constructor
        /// </remarks>
        public Size()
            : this(0, 0)
        {

        }

        public Size(Point p)
        {
            width = p.x;
            height = p.y;
        }

        public Size(double[] vals)
        {
            set(vals);
        }

        public void set(double[] vals)
        {
            if (vals != null)
            {
                width = vals.Length > 0 ? vals[0] : 0;
                height = vals.Length > 1 ? vals[1] : 0;
            }
            else
            {
                width = 0;
                height = 0;
            }
        }

        /// <remarks>
        /// the area (width*height)
        /// </remarks>
        public double area()
        {
            return width * height;
        }

        /// <remarks>
        /// true if empty
        /// </remarks>
        public bool empty()
        {
            return width <= 0 || height <= 0;
        }

        public Size clone()
        {
            return new Size(width, height);
        }

        //@Override
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(height);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            temp = BitConverter.DoubleToInt64Bits(width);
            result = prime * result + (int)(temp ^ (OpenCVInternalUtils.UnsignedRightShift(temp, 32)));
            return result;
        }

        //@Override
        public override bool Equals(Object obj)
        {
            if (!(obj is Size))
                return false;
            if ((Size)obj == this)
                return true;

            Size it = (Size)obj;
            return width == it.width && height == it.height;
        }

        //@Override
        public override string ToString()
        {
            return (int)width + "x" + (int)height;
        }

    }
}
