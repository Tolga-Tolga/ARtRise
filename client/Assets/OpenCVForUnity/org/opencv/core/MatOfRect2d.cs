using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing rectangles with 64-bit floating-point coordinates (CV_64FC4).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_64FC4, where each element corresponds to a rectangle
    /// defined by 64-bit floating-point x, y, width, and height values. The toArray and toList methods allow easy access to the data
    /// as an array or list of Rect2d objects, making it suitable for representing precise bounding boxes or regions of interest in OpenCV.
    /// </remarks>
    public partial class MatOfRect2d : Mat
    {
        // 64FC4
        private const int _depth = CvType.CV_64F;
        private const int _channels = 4;

        public MatOfRect2d()
            : base()
        {

        }

        protected MatOfRect2d(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfRect2d fromNativeAddr(IntPtr addr)
        {
            return new MatOfRect2d(addr);
        }

        public MatOfRect2d(Mat m)
            : base(m, Range.all())
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfRect2d(params Rect2d[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params Rect2d[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_Rect2d_to_Mat(a, this, num);
        }

        public Rect2d[] toArray()
        {
            int num = (int)total();
            Rect2d[] a = new Rect2d[num];
            if (num == 0)
                return a;

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new Rect2d();
            }
            Converters.Mat_to_Array_Rect2d(this, a, num);
            return a;
        }

        public void fromList(List<Rect2d> lr)
        {
            if (lr == null || lr.Count == 0)
                return;
            int num = lr.Count;
            alloc(num);

            Converters.List_Rect2d_to_Mat(lr, this, num);
        }

        public List<Rect2d> toList()
        {
            int num = (int)total();
            List<Rect2d> a = new List<Rect2d>(num);
            if (num == 0)
                return a;

            for (int i = 0; i < num; i++)
            {
                a.Add(new Rect2d());
            }
            Converters.Mat_to_List_Rect2d(this, a, num);
            return a;
        }
    }
}
