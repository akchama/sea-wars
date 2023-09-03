using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var target = gameObject.GetComponent<SelectObject>().selectedNPC;
            player.GetComponent<Cannon>().StartShooting(target);
        }
    }
}