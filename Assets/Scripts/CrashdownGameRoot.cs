using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrashdownGameRoot : MonoBehaviour
{
    public Vector3 defaultCameraOffset = new Vector3(0.0f, 5.0f, 0.0f);
    public float defaultCameraAcceleration = 5.0f;
    public LayerMask terrainLayer;
    public bool debugInput = false;
    public bool debugPhysics = false;

    private Controls _controls;
    private Vector2 _curInput = Vector2.zero;
    private bool _inputAttackDownLastFrame = false;
    private bool _inputDodgeDownLastFrame = false;
    private bool _inputCrashdownDownLastFrame = false;
    private bool _inputInteractDownThisFrame = false;
    private Vector3 _currentCameraVelocity = Vector3.zero;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.Move.performed += OnMovementChanged;
        _controls.Player.Move.canceled += OnMovementChanged;
        _controls.Player.Move.Enable();
        _controls.Player.Attack.performed += OnAttackDown;
        _controls.Player.Attack.Enable();
        _controls.Player.Dodge.performed += OnDodgeDown;
        _controls.Player.Dodge.Enable();
        _controls.Player.Crashdown.performed += OnCrashdownDown;
        _controls.Player.Crashdown.Enable();
        _controls.Player.Interact.performed += OnInteractDown;
        _controls.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= OnMovementChanged;
        _controls.Player.Move.canceled -= OnMovementChanged;
        _controls.Player.Move.Disable();
        _controls.Player.Attack.performed -= OnAttackDown;
        _controls.Player.Attack.Disable();
        _controls.Player.Dodge.performed -= OnDodgeDown;
        _controls.Player.Dodge.Disable();
        _controls.Player.Crashdown.performed -= OnCrashdownDown;
        _controls.Player.Crashdown.Disable();
        _controls.Player.Interact.performed -= OnInteractDown;
        _controls.Player.Interact.Disable();
    }

    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnMovementChanged " + context.ToString());
        }
        if (context.performed)
        {
            _curInput = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            _curInput = Vector2.zero;
        }
    }

    private void OnAttackDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnAttackDown " + context.ToString());
        }
        _inputAttackDownLastFrame = true;
    }

    private void OnDodgeDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnDodgeDown " + context.ToString());
        }
        _inputDodgeDownLastFrame = true;
    }

    private void OnCrashdownDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnCrashdownDown " + context.ToString());
        }
        _inputCrashdownDownLastFrame = true;
    }

    private void OnInteractDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnInteractDown " + context.ToString());
        }
        _inputInteractDownThisFrame = true;
    }

    void Update()
    {
        UpdatePlayers();
        UpdateEnemies();
        UpdateGameLogic();
    }

    private void UpdatePlayers()
    {
        Vector3 cameraAveragedTargetPosition = Vector3.zero;
        int numberOfCameraTargets = 0;

        Vector3 inputRight = Camera.main.transform.right;
        inputRight.y = 0.0f;
        inputRight = inputRight.normalized;
        Vector3 inputUp = Camera.main.transform.up;
        inputUp.y = 0.0f;
        inputUp = inputUp.normalized;

        foreach (CrashdownPlayerController player in CrashdownPlayerController.activePlayerInstances)
        {
            if (!player.IsDead())
            {
                bool debugPlayerIsWalkingAround = true;
                if (debugPlayerIsWalkingAround)
                {
                    Vector2 input = _curInput;
                    // Don't make diagonal walking any faster.
                    if (input.sqrMagnitude > 1.0f)
                    {
                        input = input.normalized;
                    }

                    Vector3 worldspaceInput = inputRight * input.x + inputUp * input.y;

                    // Move on the X and Z axes separately so the player can slide along walls.
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 newPosition;
                        switch (i)
                        {
                            case 0:
                                newPosition = player.transform.position + player.GetMaxSpeed() * Time.deltaTime * new Vector3(worldspaceInput.x, 0.0f, 0.0f);
                                break;
                            default:
                                newPosition = player.transform.position + player.GetMaxSpeed() * Time.deltaTime * new Vector3(0.0f, 0.0f, worldspaceInput.z);
                                break;
                        }

                        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit floorHit, player.height, terrainLayer.value))
                        {
                            newPosition = floorHit.point + Vector3.up * (player.height / 2.0f);
                            player.transform.position = newPosition;
                            if (debugPhysics)
                            {
                                Debug.Log("Player " + player.name + " is walking on " + floorHit.collider.gameObject.name + " and moved to " + newPosition, floorHit.collider.gameObject);
                            }
                        }
                        else
                        {
                            // Player tried to walk off an edge, so they should stop and not move there.
                            if (debugPhysics)
                            {
                                Debug.Log("Player " + player.name + " tried to walk off an edge.", player.gameObject);
                            }
                        }
                    }
                }

                cameraAveragedTargetPosition += player.transform.position;
                numberOfCameraTargets++;
            }
        }

        // Update the camera after all the players have moved.
        if (numberOfCameraTargets > 0)
        {
            cameraAveragedTargetPosition /= numberOfCameraTargets;
            Vector3 cameraNewPosition = cameraAveragedTargetPosition + defaultCameraOffset;
            cameraNewPosition = Vector3.SmoothDamp(Camera.main.transform.position, cameraNewPosition, ref _currentCameraVelocity, 1.0f / defaultCameraAcceleration);
            Camera.main.transform.position = cameraNewPosition;
            Camera.main.transform.LookAt(cameraAveragedTargetPosition, Vector3.forward);
        }
    }

    private void UpdateEnemies()
    {
    }

    private void UpdateGameLogic()
    {
    }

}
