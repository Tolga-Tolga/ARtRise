using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing DMatch objects with 32-bit floating-point attributes (CV_32FC4).
    /// </summary>
    /// <remarks>
    /// This class represents a standard Mat object of type CV_32FC4, where each element corresponds to a DMatch
    /// defined by 32-bit floating-point values for the queryIdx, trainIdx, imgIdx, and distance attributes.
    /// The toArray and toList methods allow easy access to the data as an array or list of DMatch objects,
    /// making it useful for representing feature matches and their distances in OpenCV.
    /// </remarks>
    public partial class MatOfDMatch : Mat
    {
        // 32FC4
        private const int _depth = CvType.CV_32F;
        private const int _channels = 4;

        public MatOfDMatch()
            : base()
        {

        }

        protected MatOfDMatch(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat: " + ToString());
            //FIXME: do we need release() here?
        }

        public static MatOfDMatch fromNativeAddr(IntPtr addr)
        {
            return new MatOfDMatch(addr);
        }

        public MatOfDMatch(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat: " + ToString());
            //FIXME: do we need release() here?
        }

        public MatOfDMatch(params DMatch[] ap)
            : base()
        {

            fromArray(ap);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params DMatch[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length;
            alloc(num);

            Converters.Array_DMatch_to_Mat(a, this, num, CvType.CV_32FC4);
        }

        public DMatch[] toArray()
        {
            int num = (int)total();
            DMatch[] a = new DMatch[num];
            if (num == 0)
                return a;

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new DMatch();
            }
            Converters.Mat_to_Array_DMatch(this, a, num, CvType.CV_32FC4);
            return a;
        }

        public void fromList(List<DMatch> ldm)
        {
            if (ldm == null || ldm.Count == 0)
                return;
            int num = ldm.Count;
            alloc(num);

            Converters.List_DMatch_to_Mat(ldm, this, num, CvType.CV_32FC4);
        }

        public List<DMatch> toList()
        {
            int num = (int)total();
            List<DMatch> a = new List<DMatch>(num);
            if (num == 0)
                return a;

            for (int i = 0; i < num; i++)
            {
                a.Add(new DMatch());
            }
            Converters.Mat_to_List_DMatch(this, a, num, CvType.CV_32FC4);
            return a;
        }
    }
}
