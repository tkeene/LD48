using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthbarTest : MonoBehaviour, Controls.ITestActions
{
    public Controls _controls;

    public int startingMaxHealth;

    public HealthbarFill healthBar;

    private int _curMax;
    private int _curHealth;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Test.SetCallbacks(this);
        }

        _curMax = startingMaxHealth;
        _curHealth = _curMax;

        healthBar.SetMaxHealth(_curMax);

        _controls.Test.Enable();
    }

    private void OnDisable()
    {
        _controls.Test.Disable();
    }

    public void OnAddMaxHealth(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _curMax += 20;
            _curHealth += 20;

            healthBar.SetMaxHealth(_curMax);
            healthBar.SetHealth(_curHealth);
        }
    }

    public void OnDamagePlayer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _curHealth -= 10;
            if (_curHealth < 0)
            {
                _curHealth = 0;
            }

            healthBar.SetHealth(_curHealth);
        }
    }
}
