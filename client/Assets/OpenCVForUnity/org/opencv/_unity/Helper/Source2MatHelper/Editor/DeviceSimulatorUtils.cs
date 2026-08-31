#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    public class DeviceSimulatorUtils
    {
        [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor.ImageOptimizationHelper class instead.")]

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