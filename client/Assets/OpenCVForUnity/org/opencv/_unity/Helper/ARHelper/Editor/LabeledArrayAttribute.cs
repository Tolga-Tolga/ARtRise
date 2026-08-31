#if UNITY_EDITOR
using System;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper.Editor
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.AR.Editor.LabeledArrayAttribute class instead.")]
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
