using UnityEngine;
using UnityEngine.UI;

public class MetaLargeSliderBar : MonoBehaviour
{
    public Slider slider;

    // value: 0.0 – 1.0
    public void SetValue(float value)
    {
        slider.SetValueWithoutNotify(Mathf.Clamp01(value));
    }
}
