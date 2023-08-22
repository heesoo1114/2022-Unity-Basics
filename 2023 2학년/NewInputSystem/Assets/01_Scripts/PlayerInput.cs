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
        _keyAction.PlayerInput.Enable(); // Ȱ��ȭ
        _keyAction.PlayerInput.Jump.performed += Jump;
        _keyAction.UI.Submit.performed += UISubmitPressed;

        ChangeKeyInfo();
    }

    private void ChangeKeyInfo()
    {
        _keyAction.PlayerInput.Disable(); // ��Ȱ��ȭ ���ְ� �ؾ���
        _keyAction.PlayerInput.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<keyboard>/escape")
            .OnComplete(op =>
            {
                Debug.Log($"���������� ���� {op.selectedControl}");
                op.Dispose(); // �Ҵ����� ����� �޸𸮿��� �����
                SaveKeyInfo();
                _keyAction.PlayerInput.Enable();
            })
            .OnCancel(op =>
            {
                Debug.Log("��ҵǾ����ϴ�");
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
        Debug.Log("UI Submit ����");
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
            _keyAction.Disable(); // ��� ��ǲ�׼��� disable
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

        // ���� �����ӿ��� Ŭ���� �� ��������. 
        // if (Mouse.current.leftButton.wasPressedThisFrame) 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
