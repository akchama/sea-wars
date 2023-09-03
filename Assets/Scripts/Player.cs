using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHealthSystem
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_currentHealth;
    public int MaxHealth => m_maxHealth;
    public int CurrentHealth => m_currentHealth;

    [SerializeField] public float repairRate = 2.0f;
    [SerializeField] public int repairAmount = 5;

    private Coroutine repairCoroutine;

    public event Action<float> OnHealthChanged = delegate { };

    private void Awake()
    {
        m_currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnHealthChanged((float)CurrentHealth / MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (repairCoroutine != null)
        {
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
        }
        Destroy(gameObject);
    }

    public void StartRepairing()
    {
        if (repairCoroutine == null)
        {
            repairCoroutine = StartCoroutine(RepairWithInterval());
        }
    }

    public void StopRepairing()
    {
        if (repairCoroutine != null)
        {
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
        }
    }

    private IEnumerator RepairWithInterval()
    {
        while (true)
        {
            if (CurrentHealth < MaxHealth)
            {
                Repair(repairAmount);
            }
            yield return new WaitForSeconds(repairRate);
        }
    }

    private void Repair(int amount)
    {
        m_currentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        OnHealthChanged((float)CurrentHealth / MaxHealth);
    }
}