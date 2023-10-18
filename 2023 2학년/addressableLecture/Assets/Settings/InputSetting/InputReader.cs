using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "SO/Input/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> ArmedEvent;
    public event Action<bool> FireEvent;
    public event Action ReloadEvent;
    public Vector2 AimPosition { get; private set; } //마우스는 이벤트방식이 아니기 때문에
    private Controls _playerInputAction; //싱글톤으로 사용할 녀석

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this); //플레이어 인풋이 발생하면 이 인스턴스를 연결해주고
        }

        _playerInputAction.Player.Enable(); //활성화
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FireEvent?.Invoke(true);
        }else if (context.canceled)
        {
            FireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnArmed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ArmedEvent?.Invoke(true);
        }else if (context.canceled)
        {
            ArmedEvent?.Invoke(false);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed)
            ReloadEvent?.Invoke();
    }
}