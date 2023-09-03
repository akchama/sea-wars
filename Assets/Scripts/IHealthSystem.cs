using System;
using UnityEngine;

public interface IHealthSystem
{
    int maxHealth { get; }
    int currentHealth { get; }

    event Action<float> OnHealthChanged;

    void TakeDamage(int damage);
    void StartRepairing();
    void StopRepairing();
}