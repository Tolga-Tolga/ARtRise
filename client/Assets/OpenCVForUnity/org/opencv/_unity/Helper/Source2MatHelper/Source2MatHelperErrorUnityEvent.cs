using System;
using UnityEngine.Events;

namespace OpenCVForUnity.UnityUtils.Helper
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Source2MatHelperErrorUnityEvent class instead.")]
    [Serializable]
    public class Source2MatHelperErrorUnityEvent : UnityEvent<Source2MatHelperErrorCode, string>
    {

    }
}
