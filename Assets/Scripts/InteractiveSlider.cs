using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveSlider : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        var slider = GetComponent<Slider>();
        if (slider == null)
            return;
        SetColor(slider);
    }

    public void OnSliderChange(float value)
    {
        var slider = GetComponent<Slider>();
        if (slider == null)
            return;
        if (slider.value < 0.1f)
            slider.value = 0.1f;

        SetColor(slider);
    }

    public void SetColor(Slider slider)
    {
        float alpha = 1 - slider.value - 0.1f;
        target.GetComponent<RawImage>().color = new Color(1, 1, 1, alpha >= 0.8f ? 1 : alpha);
    }
}
