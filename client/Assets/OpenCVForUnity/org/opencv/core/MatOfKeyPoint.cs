using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing keypoints with 32-bit floating-point attributes (CV_32FC7).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_32FC7, where each element corresponds to a keypoint
    /// defined by 32-bit floating-point values for coordinates (x, y), size, angle, response, octave, and class_id.
    /// The toArray and toList methods allow easy access to the data as an array or list of KeyPoint objects,
    /// making it useful for representing detected feature points and their attributes in OpenCV.
    /// </remarks>
    public partial class MatOfKeyPoint : Mat
    {
        // 32FC7
        private const int _depth = CvType.CV_32F;
        private const int _channels = 7;

        public MatOfKeyPoint()
            : base()
        {

        }

        protected MatOfKeyPoint(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfKeyPoint fromNativeAddr(IntPtr addr)
        {
            return new MatOfKeyPoint(addr);
        }

        public MatOfKeyPoint(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfKeyPoint(params KeyPoint[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params KeyPoint[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_KeyPoint_to_Mat(a, this, num, CvType.CV_32FC(7));
        }

        public KeyPoint[] toArray()
        {
            int num = (int)total();
            KeyPoint[] a = new KeyPoint[num];
            if (num == 0)
                return a;

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new KeyPoint();
            }

            Converters.Mat_to_Array_KeyPoint(this, a, num, CvType.CV_32FC(7));
            return a;
        }

        public void fromList(List<KeyPoint> lkp)
        {
            if (lkp == null || lkp.Count == 0)
                return;
            int num = lkp.Count;
            alloc(num);

            Converters.List_KeyPoint_to_Mat(lkp, this, num, CvType.CV_32FC(7));
        }

        public List<KeyPoint> toList()
        {
            int num = (int)total();
            List<KeyPoint> a = new List<KeyPoint>(num);
            if (num == 0)
                return a;

            for (int i = 0; i < num; i++)
            {
                a.Add(new KeyPoint());
            }

            Converters.Mat_to_List_KeyPoint(this, a, num, CvType.CV_32FC(7));
            return a;
        }
    }
}
