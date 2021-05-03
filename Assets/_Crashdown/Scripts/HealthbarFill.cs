using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarFill : MonoBehaviour
{
    public RectTransform rectTransform;
    public float sizeScale;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int maxValue)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * sizeScale);

        slider.maxValue = maxValue;
        slider.value = maxValue;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetColor(Color color)
    {
        fill.color = color;
    }
}
