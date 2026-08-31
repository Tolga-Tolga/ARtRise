#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor
{
    public class DeviceSimulatorUtils
    {
        public static bool IsDeviceSimulatorActive()
        {
            foreach (EditorWindow window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                string windowType = window.GetType().ToString();
                if (windowType.Contains("DeviceSimulator") || window.titleContent.text.Contains("Simulator"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

#endif
