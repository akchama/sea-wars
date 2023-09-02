using UnityEngine;

public class SelectableNPC : MonoBehaviour
{
    public GameObject selectionCirclePrefab;
    private GameObject selectionCircle;
    public static GameObject selectedNPC;

    void Start()
    {
        selectionCircle = Instantiate(selectionCirclePrefab, transform.position, Quaternion.identity);
        selectionCircle.transform.SetParent(transform);
        selectionCircle.SetActive(false);
    }

    void OnMouseDown()
    {
        if (selectedNPC != null)
        {
            selectedNPC.GetComponent<SelectableNPC>().Deselect();
        }

        Select();
        selectedNPC = gameObject;
    }

    public void Select()
    {
        selectionCircle.SetActive(true);
    }

    public void Deselect()
    {
        selectionCircle.SetActive(false);
    }
}