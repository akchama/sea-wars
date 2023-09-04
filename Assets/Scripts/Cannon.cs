using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public float fireSpeed = 10.0f;
    public float arcHeightFactor = 0.2f;
    public int damage = 20;

    private bool isShooting = false;
    private Coroutine shootingCoroutine;
    private GameObject _target;

    [SerializeField] public float shootingInterval = 2f;

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
                    npc.TakeDamage(damage);
                }
            }
            else if (target.CompareTag("Player"))
            {
                Debug.Log("Hit Player!");
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        });
    }

    public void StartShooting(GameObject target)
    {
        if (shootingCoroutine != null) return;
        _target = target;
        isShooting = true;
        shootingCoroutine = StartCoroutine(ShootWithInterval(target));
        target.GetComponent<ICombat>().EnterCombat(gameObject);
        
    }

    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            if (_target != null)
                _target.GetComponent<ICombat>().ExitCombat(gameObject);
            _target = null;
        }

        isShooting = false;
    }

    IEnumerator ShootWithInterval(GameObject target)
    {
        while (isShooting)
        {
            if (target != null)
            {
                FireAt(target);
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
