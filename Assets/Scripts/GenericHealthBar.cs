using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    public Slider slider;
    public HealthSystem healthSystem;
    public Image barImage;  // Reference to the Image component of the bar
    private Color initialColor; // To store the initial color set in the editor
    
    private void Start()
    {
        if (!healthSystem) return;

        // Initialize the Image component reference
        barImage = slider.fillRect.GetComponent<Image>();

        // Store initial color from the editor
        initialColor = barImage.color;

        slider.maxValue = healthSystem.maxHealth;
        slider.value = healthSystem.currentHealth;
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }


    private void UpdateHealthBar(float healthPercent)
    {
        slider.value = healthPercent * healthSystem.maxHealth;

        // Update color based on health
        Color newColor = Color.Lerp(Color.red, initialColor, healthPercent);
        barImage.color = newColor;
    }
}