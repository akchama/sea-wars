using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public float fireSpeed = 10.0f;
    public float arcHeightFactor = 0.2f;
    private GameManager gameManager;

    private bool isShooting = false;
    private Coroutine shootingCoroutine;

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
        cannonballTween.OnComplete(() =>
        {
            Destroy(cannonball);
            if (target.CompareTag("NPC"))
            {
                Debug.Log("Hit NPC!");
                NPC npc = target.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.TakeDamage(20);
                }
            }
            else if (target.CompareTag("Player"))
            {
                Debug.Log("Hit Player!");
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(20);
                }
            }
        });
    }

    public void StartShooting(GameObject target = null)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Cannot start shooting coroutine on inactive GameObject.");
            return;
        }

        currentTarget = target != null ? target : gameManager.GetComponent<SelectObject>().selectedNPC;

        if (currentTarget == null || !currentTarget.activeInHierarchy)
        {
            Debug.LogWarning("No valid target for shooting.");
            return;
        }

        if (shootingCoroutine == null)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(ShootWithInterval());
        }

    }

    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }

        isShooting = false;
    }

    IEnumerator ShootWithInterval()
    {
        while (isShooting)
        {
            if (currentTarget != null && currentTarget.activeInHierarchy)
            {
                FireAt(currentTarget);
            }
            else
            {
                isShooting = false;
            }

            yield return new WaitForSeconds(shootingInterval);
        }
        
        shootingCoroutine = null;
    }
}
