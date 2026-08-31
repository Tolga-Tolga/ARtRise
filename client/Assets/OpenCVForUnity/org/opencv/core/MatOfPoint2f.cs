using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing 2D points with floating-point coordinates (CV_32FC2).
    /// </summary>
    /// <remarks>
    /// This class represents a Mat object of type CV_32FC2, managing data as an array of 2D points with 32-bit floating-point coordinates.
    /// It is commonly used in OpenCV for storing sets of 2D points, such as keypoints, contours, or other coordinate-based data.
    /// The toArray and toList methods provide convenient access to the data as arrays or lists of Point2f objects.
    /// </remarks>
    public partial class MatOfPoint2f : Mat
    {
        // 32FC2
        private const int _depth = CvType.CV_32F;
        private const int _channels = 2;

        public MatOfPoint2f()
            : base()
        {

        }

        protected MatOfPoint2f(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfPoint2f fromNativeAddr(IntPtr addr)
        {
            return new MatOfPoint2f(addr);
        }

        public MatOfPoint2f(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfPoint2f(params Point[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params Point[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_Point_to_Mat(a, this, num, CvType.CV_32FC2);
        }

        public Point[] toArray()
        {
            int num = (int)total();
            Point[] ap = new Point[num];
            if (num == 0)
                return ap;

            for (int i = 0; i < ap.Length; i++)
            {
                ap[i] = new Point();
            }
            Converters.Mat_to_Array_Point(this, ap, num, CvType.CV_32FC2);
            return ap;
        }

        public void fromList(List<Point> lp)
        {
            if (lp == null || lp.Count == 0)
                return;
            int num = lp.Count;
            alloc(num);

            Converters.List_Point_to_Mat(lp, this, num, CvType.CV_32FC2);
        }

        public List<Point> toList()
        {
            int num = (int)total();
            List<Point> ap = new List<Point>(num);
            if (num == 0)
                return ap;

            for (int i = 0; i < num; i++)
            {
                ap.Add(new Point());
            }
            Converters.Mat_to_List_Point(this, ap, num, CvType.CV_32FC2);
            return ap;
        }
    }
}