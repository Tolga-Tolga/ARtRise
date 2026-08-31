
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.MlModule
{
    public partial class EM : StatModel
    {


        //
        // C++:  TermCriteria cv::ml::EM::getTermCriteria()
        //

        /// <remarks>
        ///  @see setTermCriteria
        /// </remarks>
        public (double type, double maxCount, double epsilon) getTermCriteriaAsValueTuple()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            ml_EM_getTermCriteria_10(nativeObj, tmpArray);
            (double type, double maxCount, double epsilon) retVal = (tmpArray[0], tmpArray[1], tmpArray[2]);

            return retVal;
        }


        //
        // C++:  void cv::ml::EM::setTermCriteria(TermCriteria val)
        //

        /// <remarks>
        ///  @copybrief getTermCriteria @see getTermCriteria
        /// </remarks>
        public void setTermCriteria(in (double type, double maxCount, double epsilon) val)
        {
            ThrowIfDisposed();

            ml_EM_setTermCriteria_10(nativeObj, (int)val.type, (int)val.maxCount, val.epsilon);


        }

    }
}
