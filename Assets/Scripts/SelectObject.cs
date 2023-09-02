using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public static GameObject selectedNPC;
    public GameObject selectionCirclePrefab; // Set this in the Inspector
    private GameObject selectionCircle;

    private void Start()
    {
        // Initialize selection circle but set it to inactive
        selectionCircle = Instantiate(selectionCirclePrefab, transform.position, Quaternion.identity);
        selectionCircle.transform.SetParent(transform);
        selectionCircle.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Deselect any previously selected NPC
        if(selectedNPC != null)
        {
            selectedNPC.GetComponent<SelectObject>().Deselect();
        }

        // Select the current NPC
        Select();
    }

    public void Select()  // made public
    {
        selectedNPC = gameObject;
        selectionCircle.SetActive(true);
    }

    public void Deselect()  // made public
    {
        selectionCircle.SetActive(false);
    }
}