using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _keyAction;

    public Action<Vector2> OnMovement;
    public Action OnJump;

    private bool _uiMode = false; // ESC

    private void Awake()
    {
        _keyAction = new PlayerInputAction();
        _keyAction.PlayerInput.Enable(); // 활성화
        _keyAction.PlayerInput.Jump.performed += Jump;
        _keyAction.UI.Submit.performed += UISubmitPressed;

        ChangeKeyInfo();
    }

    private void ChangeKeyInfo()
    {
        _keyAction.PlayerInput.Disable(); // 비활성화 해주고 해야함
        _keyAction.PlayerInput.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<keyboard>/escape")
            .OnComplete(op =>
            {
                Debug.Log($"성공적으로 변경 {op.selectedControl}");
                op.Dispose(); // 할당해제 해줘야 메모리에서 사라짐
                SaveKeyInfo();
                _keyAction.PlayerInput.Enable();
            })
            .OnCancel(op =>
            {
                Debug.Log("취소되었습니다");
                op.Dispose();
                _keyAction.PlayerInput.Enable();
            })
            .Start();
    }

    private void SaveKeyInfo()
    {
        var json = _keyAction.SaveBindingOverridesAsJson();
        Debug.Log(json);

        PlayerPrefs.SetString("keyInfo", json);
    }

    private void LoadKeyInfo()
    {
        var json = PlayerPrefs.GetString("keyInfo", null);
        if (json != null) _keyAction.LoadBindingOverridesFromJson(json);
    }

    private void UISubmitPressed(InputAction.CallbackContext context)
    {
        Debug.Log("UI Submit 눌림");
    }

    private void Update()
    {
        Vector2 inputvector = _keyAction.PlayerInput.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(inputvector);

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadKeyInfo();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _keyAction.Disable(); // 모든 인풋액션이 disable
            if (_uiMode == false)
            {
                _keyAction.UI.Enable();
            }
            else
            {
                _keyAction.PlayerInput.Enable();
            }
            _uiMode = !_uiMode;
        }

        // 현재 프레임에서 클릭이 된 상태인지. 
        // if (Mouse.current.leftButton.wasPressedThisFrame) 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
