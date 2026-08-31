#if UNITY_EDITOR

using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.AR.Editor.LabeledVector2Attribute class instead.")]
    public class LabeledVector2Attribute : PropertyAttribute
    {
        public string[] Labels { get; }

        public LabeledVector2Attribute(string xLabel, string yLabel)
        {
            Labels = new string[] { xLabel, yLabel };
        }
    }
}

#endif
