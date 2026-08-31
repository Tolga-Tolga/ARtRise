#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor.OpenCVForUnityRuntimeDisableAttribute class instead.")]
    public class OpenCVForUnityRuntimeDisableAttribute : PropertyAttribute
    {
    }

    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor.OpenCVForUnityRuntimeDisableDrawer class instead.")]
    [CustomPropertyDrawer(typeof(OpenCVForUnityRuntimeDisableAttribute))]
    public class OpenCVForUnityRuntimeDisableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect aPosition, SerializedProperty aProperty, GUIContent aLabel)
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            EditorGUI.PropertyField(aPosition, aProperty, aLabel, true);
            EditorGUI.EndDisabledGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}
#endif