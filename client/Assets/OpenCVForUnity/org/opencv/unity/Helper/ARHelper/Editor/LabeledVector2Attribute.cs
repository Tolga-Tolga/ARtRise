#if UNITY_EDITOR

using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.AR.Editor
{
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
