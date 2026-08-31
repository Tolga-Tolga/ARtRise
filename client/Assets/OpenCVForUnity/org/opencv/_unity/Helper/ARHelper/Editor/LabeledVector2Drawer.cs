#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    /// <summary>
    /// Custom property drawer for LabeledVector2Attribute.
    /// </summary>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.AR.Editor.LabeledVector2Drawer class instead.")]
    [CustomPropertyDrawer(typeof(LabeledVector2Attribute))]
    public class LabeledVector2Drawer : PropertyDrawer
    {
        /// <summary>
        /// Draws the property in the Unity Editor.
        /// </summary>
        /// <param name="position">The position to draw the property.</param>
        /// <param name="property">The property being drawn.</param>
        /// <param name="label">The label of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Ensure that the property type is Vector2.
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.LabelField(position, label.text, "Use with Vector2 only");
                return;
            }

            // Get LabeledVector2Attribute instance.
            LabeledVector2Attribute labeledVector2 = (LabeledVector2Attribute)attribute;
            string[] labels = labeledVector2.Labels;

            // Ensure that there are at least two labels; otherwise, use default "X" and "Y".
            string xLabel = labels.Length > 0 ? labels[0] : "X";
            string yLabel = labels.Length > 1 ? labels[1] : "Y";

            Vector2 value = property.vector2Value;

            // Draw the main label.
            position = EditorGUI.PrefixLabel(position, label);

            // Calculate the width of each element.
            float labelWidth = 30f; // Width for "X" and "Y" labels.
            float fieldWidth = (position.width - labelWidth * 2) / 2;

            // Define the label and field areas for the X component.
            Rect xLabelRect = new Rect(position.x, position.y, labelWidth, position.height);
            Rect xFieldRect = new Rect(position.x + labelWidth, position.y, fieldWidth, position.height);

            // Define the label and field areas for the Y component.
            Rect yLabelRect = new Rect(position.x + labelWidth + fieldWidth, position.y, labelWidth, position.height);
            Rect yFieldRect = new Rect(position.x + labelWidth * 2 + fieldWidth, position.y, fieldWidth, position.height);

            // Draw GUI elements.
            EditorGUI.LabelField(xLabelRect, xLabel);
            value.x = EditorGUI.FloatField(xFieldRect, value.x);

            EditorGUI.LabelField(yLabelRect, yLabel);
            value.y = EditorGUI.FloatField(yFieldRect, value.y);

            // Apply the modified value.
            property.vector2Value = value;
        }
    }
}

#endif
