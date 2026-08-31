using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing 6-channel floating-point data (CV_32FC6).
    /// </summary>
    /// <remarks>
    /// This class represents a Mat object of type CV_32FC6, managing data as an array of 6-channel 32-bit floating-point values.
    /// It is commonly used for data where six floating-point attributes are associated with each element, such as specific parametric representations
    /// or multi-dimensional data points in OpenCV. The toArray and toList methods provide easy access to the data as arrays or lists of float arrays.
    /// </remarks>
    public partial class MatOfFloat6 : Mat
    {
        // 32FC6
        private const int _depth = CvType.CV_32F;
        private const int _channels = 6;

        public MatOfFloat6()
            : base()
        {

        }

        protected MatOfFloat6(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfFloat6 fromNativeAddr(IntPtr addr)
        {
            return new MatOfFloat6(addr);
        }

        public MatOfFloat6(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfFloat6(params float[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params float[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<float>(a, this, a.Length);

        }

        public float[] toArray()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());
            float[] a = new float[num * _channels];
            if (num == 0)
                return a;

            Converters.copyMatToArray<float>(this, a, a.Length);

            return a;
        }

        public void fromList(List<float> lb)
        {
            if (lb == null || lb.Count == 0)
                return;

            int num = lb.Count / _channels;
            alloc(num);

            Converters.List_float_to_Mat(lb, this, num, CvType.CV_32FC(6));
        }

        public List<float> toList()
        {

            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());

            List<float> a = new List<float>(num);
            for (int i = 0; i < num * _channels; i++)
            {
                a.Add(0);
            }
            Converters.Mat_to_List_float(this, a, num, CvType.CV_32FC(6));
            return a;
        }
    }
}
