#if UNITY_EDITOR
using OpenCVForUnity.UnityIntegration;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.Editor
{
    public class OpenCVForUnityResetDebugMat : MonoBehaviour
    {
        // Private Methods
        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            DebugMat.clear();
        }
    }
}
#endif
