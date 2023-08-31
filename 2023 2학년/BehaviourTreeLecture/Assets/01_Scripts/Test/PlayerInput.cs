using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    PlayerInputAction _keyAction;

    public Action<Vector2> OnMovement;
    public Action OnFire;

    private void Awake()
    {
        _keyAction = new PlayerInputAction();
        _keyAction.PlayerInput.Enable();
    }

    private void Update()
    {
        Vector2 inputvector = _keyAction.PlayerInput.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(inputvector);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }
}
