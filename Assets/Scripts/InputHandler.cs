using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public static bool npcSelected = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (rayHit.collider)
        {
            // If hit something, check if it's an NPC
            SelectObject selectObject = rayHit.collider.gameObject.GetComponent<SelectObject>();
            if (selectObject)
            {
                npcSelected = true;
            }
            else
            {
                npcSelected = false;
            }

            Debug.Log(rayHit.collider.gameObject.name);
        }
        else
        {
            // If didn't hit anything, deselect NPC if one is selected
            if (SelectObject.selectedNPC)
            {
                SelectObject.selectedNPC.GetComponent<SelectObject>().Deselect();
            }
            npcSelected = false;
        }
    }
}