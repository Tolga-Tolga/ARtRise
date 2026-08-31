#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.AR.Editor
{
    [CustomPropertyDrawer(typeof(LabeledArrayAttribute))]
    public class LabeledArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] pathParts = property.propertyPath.Split('[', ']');
            if (pathParts.Length > 1 && int.TryParse(pathParts[1], out int pos) && pos < ((LabeledArrayAttribute)attribute).Labels.Length && ((LabeledArrayAttribute)attribute).Labels[pos] != null)
            {
                EditorGUI.PropertyField(position, property, new GUIContent(((LabeledArrayAttribute)attribute).Labels[pos]));
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}

#endif
