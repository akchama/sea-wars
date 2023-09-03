using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public static SelectObject Instance; // Singleton

    public GameObject selectedNPC;
    public GameObject selectionCirclePrefab;  // Set this in the Inspector
    private GameObject selectionCircle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Select(GameObject target)
    {
        if (selectedNPC)
        {
            Deselect();
        }

        selectedNPC = target;

        if (selectionCircle == null)
        {
            selectionCircle = Instantiate(selectionCirclePrefab, target.transform.position, Quaternion.identity);
            selectionCircle.transform.SetParent(target.transform);
        }
        else
        {
            selectionCircle.transform.position = target.transform.position;
            selectionCircle.transform.SetParent(target.transform);
        }
        selectionCircle.SetActive(true);
    }

    public void Deselect()
    {
        if (selectionCircle)
        {
            selectionCircle.SetActive(false);
        }
        selectedNPC = null;
    }
}