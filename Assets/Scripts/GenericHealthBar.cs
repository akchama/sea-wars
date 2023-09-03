using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    public Slider slider;
    public IHealthSystem healthSystem;  // Changed from NPC npc;
    public Image barImage;
    private Color initialColor;

    private void Start()
    {
        if (healthSystem == null) return;

        barImage = slider.fillRect.GetComponent<Image>();
        initialColor = barImage.color;

        slider.maxValue = healthSystem.maxHealth;
        slider.value = healthSystem.currentHealth;
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercent)
    {
        slider.value = healthPercent * healthSystem.maxHealth;

        Color newColor = Color.Lerp(Color.red, initialColor, healthPercent);
        barImage.color = newColor;
    }
}