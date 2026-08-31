using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing rectangles with 32-bit integer coordinates (CV_32SC4).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_32SC4, where each element corresponds to a rectangle
    /// defined by 32-bit integer x, y, width, and height values. The toArray and toList methods allow easy access to the data
    /// as an array or list of Rect objects, making it useful for tasks involving bounding boxes or regions of interest in OpenCV.
    /// </remarks>
    public partial class MatOfRect : Mat
    {
        // 32SC4
        private const int _depth = CvType.CV_32S;
        private const int _channels = 4;

        public MatOfRect()
            : base()
        {

        }

        protected MatOfRect(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfRect fromNativeAddr(IntPtr addr)
        {
            return new MatOfRect(addr);
        }

        public MatOfRect(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfRect(params Rect[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params Rect[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_Rect_to_Mat(a, this, num);
        }

        public Rect[] toArray()
        {
            int num = (int)total();
            Rect[] a = new Rect[num];
            if (num == 0)
                return a;

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new Rect();
            }
            Converters.Mat_to_Array_Rect(this, a, num);
            return a;
        }

        public void fromList(List<Rect> lr)
        {
            if (lr == null || lr.Count == 0)
                return;
            int num = lr.Count;
            alloc(num);

            Converters.List_Rect_to_Mat(lr, this, num);
        }

        public List<Rect> toList()
        {
            int num = (int)total();
            List<Rect> a = new List<Rect>(num);
            if (num == 0)
                return a;

            for (int i = 0; i < num; i++)
            {
                a.Add(new Rect());
            }
            Converters.Mat_to_List_Rect(this, a, num);
            return a;
        }
    }
}
