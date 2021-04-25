using UnityEngine;
using UnityEngine.InputSystem;

public class InputTester : MonoBehaviour, Controls.IPlayerActions
{
    public Controls _controls;

    private Vector2 _movement = Vector2.zero;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }


    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("PEW PEW");
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("DO A BARREL ROLL!");
    }

    public void OnCrashdown(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("CRASHDOWN!");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("PRESS DA BUTTON!");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            _movement = context.ReadValue<Vector2>();
        if (context.canceled)
            _movement = Vector2.zero;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
