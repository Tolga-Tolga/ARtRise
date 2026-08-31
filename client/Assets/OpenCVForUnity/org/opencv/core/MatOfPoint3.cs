using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing 3D points with 32-bit integer coordinates (CV_32SC3).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_32SC3, where each element corresponds to a 3D point
    /// with 32-bit integer x, y, and z coordinates. The toArray and toList methods allow easy access to the data
    /// as an array or list of Point3 objects, making it useful for representing 3D points or shapes in OpenCV.
    /// </remarks>
    public partial class MatOfPoint3 : Mat
    {
        // 32SC3
        private const int _depth = CvType.CV_32S;
        private const int _channels = 3;

        public MatOfPoint3()
            : base()
        {

        }

        protected MatOfPoint3(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfPoint3 fromNativeAddr(IntPtr addr)
        {
            return new MatOfPoint3(addr);
        }

        public MatOfPoint3(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfPoint3(params Point3[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params Point3[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_Point3_to_Mat(a, this, num, CvType.CV_32SC3);
        }

        public Point3[] toArray()
        {
            int num = (int)total();
            Point3[] ap = new Point3[num];
            if (num == 0)
                return ap;

            for (int i = 0; i < ap.Length; i++)
            {
                ap[i] = new Point3();
            }
            Converters.Mat_to_Array_Point3(this, ap, num, CvType.CV_32SC3);
            return ap;
        }

        public void fromList(List<Point3> lp)
        {
            if (lp == null || lp.Count == 0)
                return;
            int num = lp.Count;
            alloc(num);

            Converters.List_Point3_to_Mat(lp, this, num, CvType.CV_32SC3);
        }

        public List<Point3> toList()
        {
            int num = (int)total();
            List<Point3> ap = new List<Point3>(num);
            if (num == 0)
                return ap;

            for (int i = 0; i < num; i++)
            {
                ap.Add(new Point3());
            }
            Converters.Mat_to_List_Point3(this, ap, num, CvType.CV_32SC3);
            return ap;
        }
    }
}

