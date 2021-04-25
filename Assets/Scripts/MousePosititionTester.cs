using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosititionTester : MonoBehaviour
{
    private string _keyboardMouseId = "Keyboard&Mouse";

    private Vector3 _position;
    private string _currentScheme;
    private InputAction _aimAction;
    public PlayerInput playerInput;
    public CrashdownPlayerController playerController;

    private string keyboardId;

    public void OnEnable()
    {
        _aimAction = playerInput.actions["Aim"];

        _currentScheme = playerInput.currentControlScheme;
    }

    public void OnControlsChanged()
    {
        _currentScheme = playerInput.currentControlScheme;
    }

    void Update()
    {
        if (_currentScheme == _keyboardMouseId)
        {
            Vector2 mousePosition = _aimAction.ReadValue<Vector2>();

            Plane plane = new Plane(Vector3.up, playerController.transform.position);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out distance))
            {
                _position = ray.GetPoint(distance);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_position, .5f);
    }
}
