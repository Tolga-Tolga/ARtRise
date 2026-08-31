using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.OpenCVDebugMatCanvas class instead.")]
    public class OpenCVDebugMatCanvas : MonoBehaviour
    {

        private void OnDisable()
        {
            DebugMatUtils.clear();
        }
    }
}

