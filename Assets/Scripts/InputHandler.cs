using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public ClickToMove clickToMove;

    private void Awake()
    {
        _mainCamera = Camera.main;
        clickToMove = FindObjectOfType<ClickToMove>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (rayHit.collider)
        {
            if (rayHit.collider.gameObject.CompareTag("NPC"))
            {
                SelectObject.Instance.Select(rayHit.collider.gameObject);
            }
        }
        else
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = -_mainCamera.transform.position.z;
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            clickToMove.OnClick(worldPosition);
        }
    }
}