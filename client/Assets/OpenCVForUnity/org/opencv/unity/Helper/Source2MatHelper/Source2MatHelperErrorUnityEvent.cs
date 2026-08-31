using System;
using UnityEngine.Events;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    [Serializable]
    public class Source2MatHelperErrorUnityEvent : UnityEvent<Source2MatHelperErrorCode, string> { }
}
