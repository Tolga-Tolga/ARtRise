#if UNITY_EDITOR

using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.Helper.AR.Editor
{
    public class LabeledArrayAttribute : PropertyAttribute
    {
        public string[] Labels { get; }

        public LabeledArrayAttribute(params string[] labels)
        {
            Labels = labels;
        }
    }
}

#endif
