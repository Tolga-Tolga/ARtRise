using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing single-channel integer data (CV_32SC1).
    /// </summary>
    /// <remarks>
    /// This class represents a Mat object of type CV_32SC1, managing data as an array of 32-bit integers.
    /// It is commonly used for storing single-channel integer values in OpenCV, with toArray and toList methods
    /// providing easy access to the data as int arrays or lists.
    /// </remarks>
    public partial class MatOfInt : Mat
    {
        // 32SC1
        private const int _depth = CvType.CV_32S;
        private const int _channels = 1;

        public MatOfInt()
            : base()
        {

        }

        protected MatOfInt(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfInt fromNativeAddr(IntPtr addr)
        {
            return new MatOfInt(addr);
        }

        public MatOfInt(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfInt(params int[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params int[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<int>(a, this, num);

        }

        public int[] toArray()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());
            int[] a = new int[num * _channels];
            if (num == 0)
                return a;

            Converters.copyMatToArray<int>(this, a, num);

            return a;
        }

        public void fromList(List<int> lb)
        {
            if (lb == null || lb.Count == 0)
                return;

            int num = lb.Count / _channels;
            alloc(num);

            Converters.List_int_to_Mat(lb, this, num, CvType.CV_32SC1);
        }

        public List<int> toList()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());

            List<int> a = new List<int>(num);
            for (int i = 0; i < num; i++)
            {
                a.Add(0);
            }
            Converters.Mat_to_List_int(this, a, num, CvType.CV_32SC1);
            return a;
        }
    }
}
