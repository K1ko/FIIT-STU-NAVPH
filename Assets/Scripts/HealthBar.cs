using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider slider;

    // Sets the maximum health value of the health bar
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        Debug.Log("Health bar max health set to: " + health);
    }

    // Sets the current health value of the health bar
    public void SetHealth(float health)
    {
        slider.value = health;
        Debug.Log("Health bar current health set to1: " + health);
    }
}