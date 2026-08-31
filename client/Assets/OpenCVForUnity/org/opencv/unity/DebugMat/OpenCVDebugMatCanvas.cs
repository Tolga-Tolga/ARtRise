using UnityEngine;

namespace OpenCVForUnity.UnityIntegration
{
    public class OpenCVDebugMatCanvas : MonoBehaviour
    {
        private void OnDisable()
        {
            DebugMat.clear();
        }
    }
}
