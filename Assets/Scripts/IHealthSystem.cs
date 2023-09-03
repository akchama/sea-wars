using System;
using UnityEngine;

public interface IHealthSystem
{
    event Action<float> OnHealthChanged;

    void TakeDamage(int damage);
    void StartRepairing();
    void StopRepairing();
}