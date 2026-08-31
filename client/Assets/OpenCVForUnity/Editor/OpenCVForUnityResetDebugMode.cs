#if UNITY_EDITOR
using OpenCVForUnity.UnityIntegration;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.Editor
{
    public class OpenCVForUnityResetDebugMode : MonoBehaviour
    {
        // Private Methods
        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            OpenCVDebug.SetDebugMode(false);
        }
    }
}
#endif
