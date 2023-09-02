using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5.0f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    private void Update()
    {
        // Get bounds from GraphConstraint Singleton
        var leftBound = GraphConstraint.Instance.leftBound;
        var rightBound = GraphConstraint.Instance.rightBound;
        var topBound = GraphConstraint.Instance.topBound;
        var bottomBound = GraphConstraint.Instance.bottomBound;
        
        // Calculate padding based on camera size
        float horizontalPadding = cam.aspect * cam.orthographicSize;
        float verticalPadding = cam.orthographicSize;
        
        // Adjust the bounds based on padding
        leftBound += horizontalPadding;
        rightBound -= horizontalPadding;
        topBound -= verticalPadding;
        bottomBound += verticalPadding;

        
        // Initialize movement vectors to zero
        Vector3 moveHorizontal = Vector3.zero;
        Vector3 moveVertical = Vector3.zero;

        if (Input.GetKey(KeyCode.A))  // Move Left
        {
            moveHorizontal = Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))  // Move Right
        {
            moveHorizontal = Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))  // Move Down
        {
            moveVertical = Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))  // Move Up
        {
            moveVertical = Vector3.up * speed * Time.deltaTime;
        }

        // Combine and apply movement
        var movement = moveHorizontal + moveVertical;
        var newPosition = transform.position + movement;
        
        // Apply constraints
        newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBound, topBound);

        // Update camera position
        transform.position = newPosition;
    }
}