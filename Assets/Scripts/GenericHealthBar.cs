using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    public Slider slider;
    [FormerlySerializedAs("healthSystem")] public NPC npc;
    public Image barImage;  // Reference to the Image component of the bar
    private Color initialColor; // To store the initial color set in the editor
    
    private void Start()
    {
        if (!npc) return;

        // Initialize the Image component reference
        barImage = slider.fillRect.GetComponent<Image>();

        // Store initial color from the editor
        initialColor = barImage.color;

        slider.maxValue = npc.maxHealth;
        slider.value = npc.currentHealth;
        npc.OnHealthChanged += UpdateHealthBar;
    }


    private void UpdateHealthBar(float healthPercent)
    {
        slider.value = healthPercent * npc.maxHealth;

        // Update color based on health
        Color newColor = Color.Lerp(Color.red, initialColor, healthPercent);
        barImage.color = newColor;
    }
}