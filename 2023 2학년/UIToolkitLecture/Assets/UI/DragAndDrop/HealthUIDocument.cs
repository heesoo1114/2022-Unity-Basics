using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIDocument : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTrm;

    private VisualElement _root;
    private VisualElement _healthBar;
    private VisualElement _bar;
    private UIDocument _document;

    private Camera _mainCam;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _root = _document.rootVisualElement;
        _healthBar = _root.Q<VisualElement>("HealthBar");
        _bar = _healthBar.Q<VisualElement>("Bar"); //??©ö?
    }

    public void OnChangeHealth(float normalHealth)
    {
        _bar.style.width = new Length(normalHealth * 100, LengthUnit.Percent);
    }

    private void LateUpdate()
    {
        Vector3 worldPos = _playerTrm.position;
        Vector2 uiPos = RuntimePanelUtils.CameraTransformWorldToPanel(_root.panel, worldPos, _mainCam);

        float deltaY = -100f;

        _healthBar.style.left = uiPos.x - _healthBar.layout.width * 0.5f;
        _healthBar.style.top = uiPos.y + deltaY;
    }
}

