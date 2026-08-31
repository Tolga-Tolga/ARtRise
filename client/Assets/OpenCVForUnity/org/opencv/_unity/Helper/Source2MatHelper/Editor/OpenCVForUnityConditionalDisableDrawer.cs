#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    using GetCondFunc = System.Func<UnityEditor.SerializedProperty, OpenCVForUnityConditionalDisableInInspectorAttribute, bool>;

    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor.OpenCVForUnityConditionalDisableDrawer class instead.")]

    [CustomPropertyDrawer(typeof(OpenCVForUnityConditionalDisableInInspectorAttribute))]
    internal sealed class OpenCVForUnityConditionalDisableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = base.attribute as OpenCVForUnityConditionalDisableInInspectorAttribute;
            var condProp = property.serializedObject.FindProperty(attr.VariableName);
            if (condProp == null)
            {
                Debug.LogError($"Not found '{attr.VariableName}' property");
                EditorGUI.PropertyField(position, property, label, true);
            }

            var isDisable = IsDisable(attr, condProp);
            if (attr.ConditionalInvisible && isDisable)
            {
                return;
            }

            isDisable = (isDisable) ? isDisable : (attr.RuntimeDisable && EditorApplication.isPlayingOrWillChangePlaymode);

            EditorGUI.BeginDisabledGroup(isDisable);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = base.attribute as OpenCVForUnityConditionalDisableInInspectorAttribute;
            var prop = property.serializedObject.FindProperty(attr.VariableName);
            if (attr.ConditionalInvisible && IsDisable(attr, prop))
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private bool IsDisable(OpenCVForUnityConditionalDisableInInspectorAttribute attr, SerializedProperty prop)
        {
            GetCondFunc disableCondFunc;
            if (!DisableCondFuncMap.TryGetValue(attr.VariableType, out disableCondFunc))
            {
                Debug.LogError($"{attr.VariableType} type is not supported");
                return false;
            }
            return disableCondFunc(prop, attr);
        }

        private Dictionary<Type, GetCondFunc> DisableCondFuncMap = new Dictionary<Type, GetCondFunc>() {
        { typeof(bool), (prop, attr) => {return attr.TrueThenDisable ? !prop.boolValue : prop.boolValue;} },
        { typeof(string), (prop, attr) => {return attr.TrueThenDisable ? prop.stringValue == attr.ComparedStr : prop.stringValue != attr.ComparedStr;} },
        { typeof(int), (prop, attr) => {

            if (attr.ComparisonType == OpenCVForUnityConditionalDisableInInspectorComparisonType.FlagMatch)
            {
                int propValue = prop.intValue;
                return attr.TrueThenDisable ? (propValue & attr.ComparedInt) != 0 : (propValue & attr.ComparedInt) == 0;
            }

            return attr.TrueThenDisable ? prop.intValue == attr.ComparedInt : prop.intValue != attr.ComparedInt;

        } },
        { typeof(float), (prop, attr) => {return attr.TrueThenDisable ? prop.floatValue <= attr.ComparedFloat : prop.floatValue > attr.ComparedFloat;} }
    };
    }
}
#endif