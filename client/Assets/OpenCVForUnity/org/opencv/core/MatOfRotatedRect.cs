using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing rotated rectangles with 32-bit floating-point coordinates (CV_32FC5).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_32FC5, where each element corresponds to a rotated rectangle
    /// defined by 32-bit floating-point values for the center x and y coordinates, width, height, and rotation angle.
    /// The toArray and toList methods allow easy access to the data as an array or list of RotatedRect objects,
    /// making it useful for representing rotated bounding boxes or oriented regions in OpenCV.
    /// </remarks>
    public partial class MatOfRotatedRect : Mat
    {
        // 32FC5
        private const int _depth = CvType.CV_32F;
        private const int _channels = 5;

        public MatOfRotatedRect()
            : base()
        {

        }

        protected MatOfRotatedRect(IntPtr addr)
            : base(addr)
        {
            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfRotatedRect fromNativeAddr(IntPtr addr)
        {
            return new MatOfRotatedRect(addr);
        }

        public MatOfRotatedRect(Mat m)
            : base(m, Range.all())
        {
            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfRotatedRect(params RotatedRect[] a)
            : base()
        {
            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params RotatedRect[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_RotatedRect_to_Mat(a, this, num);
        }

        public RotatedRect[] toArray()
        {
            int num = (int)total();
            RotatedRect[] a = new RotatedRect[num];
            if (num == 0)
                return a;

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new RotatedRect();
            }
            Converters.Mat_to_Array_RotatedRect(this, a, num);
            return a;
        }

        public void fromList(List<RotatedRect> lr)
        {
            if (lr == null || lr.Count == 0)
                return;
            int num = lr.Count;
            alloc(num);

            Converters.List_RotatedRect_to_Mat(lr, this, num);
        }

        public List<RotatedRect> toList()
        {
            int num = (int)total();
            List<RotatedRect> a = new List<RotatedRect>(num);
            if (num == 0)
                return a;

            for (int i = 0; i < num; i++)
            {
                a.Add(new RotatedRect());
            }
            Converters.Mat_to_List_RotatedRect(this, a, num);
            return a;
        }
    }
}
