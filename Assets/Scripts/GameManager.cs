using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SelectableNPC.selectedNPC != null)
            {
                player.GetComponent<Cannon>().FireAt(SelectableNPC.selectedNPC);
            }
        }
    }
}