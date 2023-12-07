using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public float XInput { get; private set; }
    public float YInput { get; private set; }

    public event Action JumpEvent;
    public event Action DashEvent;
    public event Action PrimaryAttackEvent;

    private Controls _controls;
    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DashEvent?.Invoke();    
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnXMovement(InputAction.CallbackContext context)
    {
        XInput = context.ReadValue<float>();
    }

    public void OnYMovement(InputAction.CallbackContext context)
    {
        YInput = context.ReadValue<float>();
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PrimaryAttackEvent?.Invoke();
        }
    }
}
