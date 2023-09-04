using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IHealthSystem, ICombat
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_currentHealth;
    public int MaxHealth => m_maxHealth;
    public int CurrentHealth => m_currentHealth;

    [SerializeField] public float repairRate = 2.0f;
    [SerializeField] public int repairAmount = 5;

    private Coroutine repairCoroutine;
    private Cannon npcCannon;

    [SerializeField] private List<GameObject> playersInCombatWithMe = new();
    public event Action<float> OnHealthChanged = delegate { };

    private void Awake()
    {
        npcCannon = GetComponent<Cannon>();
        m_currentHealth = MaxHealth;
    }
    
    public void EnterCombat(GameObject player)
    {
        if (!playersInCombatWithMe.Contains(player))
        {
            playersInCombatWithMe.Add(player);
            Retaliate();
        }
    }

    public void ExitCombat(GameObject player)
    {
        if (playersInCombatWithMe.Contains(player))
        {
            playersInCombatWithMe.Remove(player);
            npcCannon.StopShooting();
        }
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

    private void Retaliate()
    {
        if (npcCannon != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            npcCannon.StartShooting(player);
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