using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject targetObject; // Assign this in the editor
    private IHealthSystem healthSystem;
    public Image barImage;
    private Color initialColor;

    private void Start()
    {
        // Find the IHealthSystem component on the target object
        healthSystem = targetObject.GetComponent<IHealthSystem>();
        
        if (healthSystem == null)
        {
            Debug.LogError("No IHealthSystem component found on target object.");
            return;
        }

        // Initialize the Image component reference
        barImage = slider.fillRect.GetComponent<Image>();
        
        // Store initial color from the editor
        initialColor = barImage.color;

        slider.maxValue = healthSystem.MaxHealth;  // Note: property names are case-sensitive
        slider.value = healthSystem.CurrentHealth; // Note: property names are case-sensitive
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercent)
    {
        slider.value = healthPercent * healthSystem.MaxHealth; // Note: property names are case-sensitive

        // Update color based on health
        Color newColor = Color.Lerp(Color.red, initialColor, healthPercent);
        barImage.color = newColor;
    }
}