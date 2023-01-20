using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    // Public variables
    public event Action JumpEvent;
    public event Action DodgeEvent;

    // Private variables
    private Controls _controls;

    // Unity built-in methods
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);

        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    // Public methods
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        DodgeEvent?.Invoke();
    }
}