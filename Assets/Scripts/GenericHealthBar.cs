using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    public Slider slider;
    public HealthSystem healthSystem;

    private void Start()
    {
        if (!healthSystem) return;
        slider.maxValue = healthSystem.maxHealth;
        Debug.Log("Max Health: " + healthSystem.maxHealth);
        slider.value = healthSystem.currentHealth;
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercent)
    {
        slider.value = healthPercent * healthSystem.maxHealth;
        Debug.Log("Slider value set to: " + slider.value);
    }
}