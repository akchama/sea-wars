using System;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour, IHealthSystem
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_currentHealth;
    public int maxHealth => m_maxHealth;
    public int currentHealth => m_currentHealth;

    [SerializeField] public float repairRate = 2.0f;
    [SerializeField] public int repairAmount = 5;

    private Coroutine repairCoroutine;
    private Cannon npcCannon;

    public event Action<float> OnHealthChanged = delegate { };

    private void Awake()
    {
        npcCannon = GetComponent<Cannon>();
        m_currentHealth = maxHealth;
        OnHealthChanged += (float health) => 
        {
            if (health <= 0.0f) 
            {
                Die();
            }
            else 
            {
                Retaliate();
            }
        };
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChanged((float)currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Retaliate()
    {
        if (npcCannon != null)
        {
            // Assuming the player's GameObject is tagged as "Player"
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
            if (currentHealth < maxHealth)
            {
                Repair(repairAmount);
            }
            yield return new WaitForSeconds(repairRate);
        }
    }

    private void Repair(int amount)
    {
        m_currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged((float)currentHealth / maxHealth);
    }
}