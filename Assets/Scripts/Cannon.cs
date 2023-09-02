using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public float fireSpeed = 20.0f;

    public void FireAt(GameObject target)
    {
        GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (target.transform.position - transform.position).normalized;
        cannonball.GetComponent<Rigidbody2D>().velocity = direction * fireSpeed;
    }
}