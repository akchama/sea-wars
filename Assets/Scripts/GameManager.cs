using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    [SerializeField] public float shootingInterval = 2f;
    
    private bool isShooting = false;
    private bool isShootingCoroutineRunning = false;  // New flag to track if coroutine is running

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = !isShooting;

            if (isShooting && !isShootingCoroutineRunning)  // Check if coroutine is already running
            {
                StartCoroutine(ShootWithInterval());
            }
        }
    }

    IEnumerator ShootWithInterval()
    {
        if (isShootingCoroutineRunning)  // Exit if already running
        {
            yield break;
        }

        isShootingCoroutineRunning = true;  // Set flag to true as coroutine is now running

        while (isShooting)
        {
            if (SelectObject.selectedNPC != null)
            {
                player.GetComponent<Cannon>().FireAt(SelectObject.selectedNPC);
            }

            yield return new WaitForSeconds(shootingInterval);
        }

        isShootingCoroutineRunning = false;  // Reset flag as coroutine has finished
    }
}