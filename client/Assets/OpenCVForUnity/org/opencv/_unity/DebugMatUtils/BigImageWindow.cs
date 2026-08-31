using System;
using UnityEngine;
using UnityEngine.UI;

namespace OpenCVForUnity.UnityUtils
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.BigImageWindow class instead.")]
    public class BigImageWindow : MonoBehaviour
    {

        public RectTransform bigImageWindow;

        public InputField dumpInputField;

        public void OnPointerClick()
        {
            //Debug.Log("OnPointerClick " + this.name);

            bigImageWindow.gameObject.SetActive(false);
            dumpInputField.gameObject.SetActive(false);
        }
    }
}
