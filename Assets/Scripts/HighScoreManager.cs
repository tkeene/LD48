using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighScoreManager : MonoBehaviour
{
    public float timeToHoldInteractButton = 1.0f;

    private Controls _controls;

    private bool interactIsHeld = false;
    private float currentHeldTime = 0.0f;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.Interact.performed += OnInteractDown;
        _controls.Player.Interact.canceled += OnInteractUp;
        _controls.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Interact.performed -= OnInteractDown;
        _controls.Player.Interact.canceled -= OnInteractUp;
        _controls.Player.Interact.Disable();
    }

    private void OnInteractDown(InputAction.CallbackContext obj)
    {
        interactIsHeld = true;
    }

    private void OnInteractUp(InputAction.CallbackContext obj)
    {
        interactIsHeld = false;
        currentHeldTime = 0.0f;
    }

    void Update()
    {
        //if (interactIsHeld)
        // END OF JAM, NO TIME FOR A HIGH SCORE SCREEN! KICK THEM BACK TO THE TITLE SCREEN AFTER ONE SECOND!
        {
            if (currentHeldTime > timeToHoldInteractButton)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                currentHeldTime += Time.deltaTime;
            }
        }
    }
}
