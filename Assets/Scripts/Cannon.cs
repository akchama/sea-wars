using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public float fireSpeed = 10.0f;
    public float arcHeightFactor = 0.2f;
    private GameManager gameManager;
    
    private bool isShooting = false;
    private bool isShootingCoroutineRunning = false;  // New flag to track if coroutine is running
    
    private GameObject currentTarget = null;
    
    [SerializeField] public float shootingInterval = 2f;
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void FireAt(GameObject target)
    {
        GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity);

        Vector3 startPos = transform.position;
        Vector3 targetPos = target.transform.position;
        float distance = Vector3.Distance(startPos, targetPos);
        float dynamicArcHeight = distance * arcHeightFactor;
        Vector3 midPoint = (startPos + targetPos) / 2;
        midPoint.y += dynamicArcHeight;
        Vector3[] path = new Vector3[] { midPoint, targetPos };
        float tweenDuration = distance / fireSpeed;

        Tween cannonballTween = cannonball.transform.DOPath(path, tweenDuration, PathType.CatmullRom).SetEase(Ease.Linear);

        // Destroy both the cannonball and the NPC when the tween completes
        cannonballTween.OnComplete(() => 
        {
            Destroy(cannonball);
            HealthSystem healthSystem = target.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(20);  // Deal 20 damage
            }
        });
    }

    public void StartShooting()
    {
        var newTarget = gameManager.GetComponent<SelectObject>().selectedNPC;

        if (isShooting && newTarget == currentTarget)
        {
            isShooting = false;
            return;
        }
        
        currentTarget = newTarget;

        isShooting = true;
        
        if (!isShootingCoroutineRunning)
        {
            StartCoroutine(ShootWithInterval());
        }
    }
    
    public void StopShooting()
    {
        isShooting = false;
    }
    
    IEnumerator ShootWithInterval()
    {
        if (isShootingCoroutineRunning)
        {
            yield break;
        }

        isShootingCoroutineRunning = true;

        while (isShooting)
        {
            if (currentTarget != null)
            {
                FireAt(currentTarget);
            }
            else
            {
                isShooting = false;
            }

            yield return new WaitForSeconds(shootingInterval);
        }

        isShootingCoroutineRunning = false;
    }
}