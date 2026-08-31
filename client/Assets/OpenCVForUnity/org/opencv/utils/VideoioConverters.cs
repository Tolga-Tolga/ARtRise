using System.Collections.Generic;
using OpenCVForUnity.CoreModule;

namespace OpenCVForUnity.UtilsModule
{
    public partial class Converters
    {

        public static void Mat_to_vector_VideoCaptureAPIs(Mat m, List<int> videoCaptureAPIs)
        {
            if (m != null)
                m.ThrowIfDisposed();

            if (videoCaptureAPIs == null)
                throw new CvException("targets == null");
            int count = m.rows();
            if (CvType.CV_32SC1 != m.type() || m.cols() != 1)
                throw new CvException(
                    "CvType.CV_32SC1 != m.type() ||  m.cols()!=1\n" + m);

            videoCaptureAPIs.Clear();
            for (int i = 0; i < count; i++)
            {
                videoCaptureAPIs.Add(0);
            }
            Converters.Mat_to_List_int(m, videoCaptureAPIs, count, CvType.CV_32SC1);
        }
    }
}
