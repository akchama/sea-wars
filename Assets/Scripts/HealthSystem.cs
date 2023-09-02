using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public int currentHealth;

    public event Action<float> OnHealthChanged = delegate { };

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChanged((float)currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}