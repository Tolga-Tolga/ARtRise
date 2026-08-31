using System;
using System.Collections.Generic;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.CoreModule
{
    /// <summary>
    /// A specialized Mat class for storing single-channel byte data (CV_8UC1).
    /// </summary>
    /// <remarks>
    /// This class represents a Mat object of type CV_8UC1, managing data as an array of unsigned 8-bit integers (bytes).
    /// It is commonly used storing for single-channel byte values in OpenCV, with toArray and toList methods
    /// allowing for easy extraction of data as byte arrays or lists.
    /// </remarks>
    public partial class MatOfByte : Mat
    {
        // 8UC(x)
        private const int _depth = CvType.CV_8U;
        private const int _channels = 1;

        public MatOfByte()
            : base()
        {

        }

        protected MatOfByte(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfByte fromNativeAddr(IntPtr addr)
        {
            return new MatOfByte(addr);
        }

        public MatOfByte(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfByte(params byte[] a)
            : base()
        {

            fromArray(a);
        }

        public MatOfByte(int offset, int length, params byte[] a)
            : base()
        {

            fromArray(offset, length, a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));

        }

        public void fromArray(params byte[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            Converters.copyArrayToMat<byte>(a, this, num);
        }

        public void fromArray(int offset, int length, params byte[] a)
        {
            if (offset < 0)
                throw new CvException("offset < 0");
            if (a == null)
                throw new CvException();
            if (length < 0 || length + offset > a.Length)
                throw new CvException("invalid 'length' parameter: " + length);
            if (a.Length == 0)
                return;
            int num = length / _channels;
            alloc(num);
            put(0, 0, a, offset, num); //TODO: check ret val!
        }

        public byte[] toArray()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());
            byte[] a = new byte[num * _channels];
            if (num == 0)
                return a;

            Converters.copyMatToArray<byte>(this, a, num);
            return a;
        }

        public void fromList(List<byte> lb)
        {
            if (lb == null || lb.Count == 0)
                return;

            int num = lb.Count / _channels;
            alloc(num);

            Converters.List_uchar_to_Mat(lb, this, num);
        }

        public List<byte> toList()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());

            List<byte> lb = new List<byte>(num);
            for (int i = 0; i < num; i++)
            {
                lb.Add(0);
            }
            Converters.Mat_to_List_uchar(this, lb, num);
            return lb;
        }
    }
}
