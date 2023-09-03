using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    public float speed = 5.0f;

    private Camera cam;

    private Vector2 movementInput;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
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
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime;

        // Combine and apply movement
        var newPosition = transform.position + movement;

        // Apply constraints
        newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBound, topBound);

        // Update camera position
        transform.position = newPosition;
    }
}