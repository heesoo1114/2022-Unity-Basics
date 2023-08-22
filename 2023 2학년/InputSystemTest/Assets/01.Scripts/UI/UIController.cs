using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private UIDocument _UIdocument;

    private Dictionary<string, InputAction> _inputMap;
    [SerializeField] private PlayerInputAction _inputAction;

    private void Awake()
    {
        _UIdocument = GetComponent<UIDocument>();
        _inputMap.Add("Jump", _inputAction.Player.Jump);
        _inputMap.Add("Movement", _inputAction.Player.Movement);
        _inputMap.Add("Fire", _inputAction.Player.Fire);
    }

    private void OnEnable()
    {
        var menu = _UIdocument.rootVisualElement.Q<VisualElement>("MenuBox");

        menu.RegisterCallback<ClickEvent>(e =>
        {
            var label = e.target as UILabelWithData;

            if (label != null)
            {
                Debug.Log(label.KeyData);
                Debug.Log(label.IndexData);
            }
        });
    }
}
