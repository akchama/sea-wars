using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public float fireSpeed = 10.0f;
    public float arcHeightFactor = 0.2f;

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
            Destroy(cannonball);  // Destroys the cannonball itself
            Destroy(target);  // Destroys the NPC
        });
    }
}