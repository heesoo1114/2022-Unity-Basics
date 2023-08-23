using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private UIDocument _UIdocument;

    private Dictionary<string, InputAction> _inputMap;

    [SerializeField] private PlayerInput _playerInput;

    private VisualElement _optionPanel; 

    private void Awake()
    {
        _UIdocument = GetComponent<UIDocument>();
    }

    private void Start()
    {
        _inputMap.Add("Jump", _playerInput.InputAction.Player.Jump);
        _inputMap.Add("Movement", _playerInput.InputAction.Player.Movement);
        _inputMap.Add("Fire", _playerInput.InputAction.Player.Fire);
    }

    private void OnEnable()
    {
        _optionPanel = _UIdocument.rootVisualElement.Q<VisualElement>("MenuBox");

        _UIdocument.rootVisualElement.Q<Button>("BtnCancel").RegisterCallback<ClickEvent>(e => CloseWindow());

        _optionPanel.RegisterCallback<ClickEvent>(e =>
        {
            var label = e.target as UILabelWithData;

            if (label != null)
            {
                if (_inputMap.TryGetValue(label.KeyData, out InputAction target))
                {
                    var oldText = label.text;
                    label.text = "Listening...";
                    var seq = target.PerformInteractiveRebinding();

                    if (label.KeyData != "Fire")
                    {
                        seq = seq.WithControlsExcluding("Mouse");
                    }

                    seq.WithTargetBinding(label.IndexData)
                        .WithCancelingThrough("<keyboard>escape")
                        .OnComplete(op =>
                        {
                            label.text = op.selectedControl.name;
                            op.Dispose();
                        })
                        .OnCancel(op =>
                        {
                            label.text = oldText;
                            op.Dispose();
                        }) 
                        .Start();
                }
            
                Debug.Log(label.KeyData);
                Debug.Log(label.IndexData);
            }
        });
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OpenWindow();
        }
    }

    private void OpenWindow()
    {
        _optionPanel.AddToClassList("open");
        _playerInput.InputAction.Player.Disable();
    }

    private void CloseWindow()
    {
        _optionPanel.RemoveFromClassList("open");
        _playerInput.InputAction.Player.Enable();
    }
}
