#if UNITY_EDITOR

using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    public enum OpenCVForUnityConditionalDisableInInspectorComparisonType
    {
        ExactMatch,
        FlagMatch
    }

    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.Source2Mat.Editor.OpenCVForUnityConditionalDisableInInspectorAttribute class instead.")]

    public partial class OpenCVForUnityConditionalDisableInInspectorAttribute : PropertyAttribute
    {

        public readonly string VariableName;
        public readonly Type VariableType;
        public readonly bool TrueThenDisable;
        public readonly bool ConditionalInvisible;
        public readonly bool RuntimeDisable;
        public OpenCVForUnityConditionalDisableInInspectorComparisonType ComparisonType;

        public readonly string ComparedStr;
        public readonly int ComparedInt;
        public readonly float ComparedFloat;

        private OpenCVForUnityConditionalDisableInInspectorAttribute(string variableName, Type variableType, bool trueThenDisable = false, bool conditionalInvisible = false, bool runtimeDisable = false, OpenCVForUnityConditionalDisableInInspectorComparisonType comparisonType = OpenCVForUnityConditionalDisableInInspectorComparisonType.ExactMatch)
        {
            this.VariableName = variableName;
            this.VariableType = variableType;
            this.TrueThenDisable = trueThenDisable;
            this.ConditionalInvisible = conditionalInvisible;
            this.RuntimeDisable = runtimeDisable;
            this.ComparisonType = comparisonType;
        }

        public OpenCVForUnityConditionalDisableInInspectorAttribute(string boolVariableName, bool trueThenDisable = false, bool conditionalInvisible = false, bool runtimeDisable = false)
        : this(boolVariableName, typeof(bool), trueThenDisable, conditionalInvisible, runtimeDisable) { }

        public OpenCVForUnityConditionalDisableInInspectorAttribute(string strVariableName, string comparedStr, bool notEqualThenEnable = false, bool conditionalInvisible = false, bool runtimeDisable = false)
        : this(strVariableName, comparedStr.GetType(), notEqualThenEnable, conditionalInvisible, runtimeDisable)
        {
            this.ComparedStr = comparedStr;
        }

        public OpenCVForUnityConditionalDisableInInspectorAttribute(string intVariableName, int comparedInt, bool notEqualThenEnable = false, bool conditionalInvisible = false, bool runtimeDisable = false, OpenCVForUnityConditionalDisableInInspectorComparisonType comparisonType = OpenCVForUnityConditionalDisableInInspectorComparisonType.ExactMatch)
        : this(intVariableName, comparedInt.GetType(), notEqualThenEnable, conditionalInvisible, runtimeDisable, comparisonType)
        {
            this.ComparedInt = comparedInt;
        }

        public OpenCVForUnityConditionalDisableInInspectorAttribute(string floatVariableName, float comparedFloat, bool greaterThanComparedThenEnable = true, bool conditionalInvisible = false, bool runtimeDisable = false)
        : this(floatVariableName, comparedFloat.GetType(), greaterThanComparedThenEnable, conditionalInvisible, runtimeDisable)
        {
            this.ComparedFloat = comparedFloat;
        }
    }
}
#endif