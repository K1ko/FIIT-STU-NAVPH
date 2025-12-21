using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour  // Manages the health bar UI with smooth transitions and color changes for player health
{
    [Header("Health Bar Components")]
    public Slider slider;         // The slider UI
    public Image fill;            // The fill image of the slider
    public Gradient gradient;     // Gradient to color the fill from red to green
    private float target = 1f;

    [Header("Lerp Settings")]
    public float lerpSpeed = 5f;  // How quickly to animate the health bar

    private float targetHealth;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        targetHealth = health;

        fill.color = gradient.Evaluate(target); // Full health
    }

    public void SetHealth(float health)
    {
        targetHealth = health;
        StopAllCoroutines();
        StartCoroutine(LerpHealth());
    }

    private IEnumerator LerpHealth()
    {
        while (Mathf.Abs(slider.value - targetHealth) > 0.01f)
        {
            slider.value = Mathf.Lerp(slider.value, targetHealth, Time.deltaTime * lerpSpeed);

            float normalizedHealth = slider.value / slider.maxValue;
            fill.color = gradient.Evaluate(normalizedHealth);

            yield return null;
        }

        // Snap to exact value at the end
        slider.value = targetHealth;
        fill.color = gradient.Evaluate(targetHealth / slider.maxValue);
    }
}
