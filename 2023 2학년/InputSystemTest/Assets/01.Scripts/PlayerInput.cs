using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _inputAction;

    public event Action<Vector2> OnMovement;
    public event Action<Vector2> OnAim;
    public Vector2 AimPosition { get; private set; }
    public event Action OnJump;
    public event Action OnFire;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();
        
        _inputAction.Player.Enable();
        _inputAction.Player.Jump.performed += JumpHandle;
        _inputAction.Player.Fire.performed += FireHandle;
    }

    private void FireHandle(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }

    private void JumpHandle(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Update()
    {
        AimPosition = _inputAction.Player.Aim.ReadValue<Vector2>();

        Vector2 targetTr = _inputAction.Player.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(targetTr);
    }
}
