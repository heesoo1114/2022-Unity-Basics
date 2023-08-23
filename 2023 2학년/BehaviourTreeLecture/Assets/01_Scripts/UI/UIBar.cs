using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBar : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _panelElement;
    private DialogPanelUI _panelUI;
    private BulletUI _bulletUI;

    private bool _isOn = false;
    public bool IsOn
    {
        get => _isOn;
        set
        {
            // 여기에 패널 코드 추가
            _isOn = value;
        }
    }

    private Camera _mainCam;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainCam = Camera.main;
    }

    public void SetText(string text)
    {
        _panelUI.Text = text;
    }

    private void OnEnable()
    {
        var panelRoot = _uiDocument.rootVisualElement.Q("DialogPanel");
        _panelUI = new DialogPanelUI(panelRoot, "..");

        var bulletRoot = _uiDocument.rootVisualElement.Q("BulletContiner");
        _bulletUI = new BulletUI(bulletRoot, 7);
    }

    public void SetBullet(int cnt)
    {
        _bulletUI.BulletCount = cnt;
    }

    public void LookToCamera()
    {
        transform.rotation = _mainCam.transform.rotation;
    }
}
