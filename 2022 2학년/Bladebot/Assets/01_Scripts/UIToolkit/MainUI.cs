using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        Button popupBtn = root.Q<Button>("MyPopupBtn");

        VisualElement popup = root.Q<VisualElement>("popupWindow");

        popupBtn.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 0;
            popup.AddToClassList("on"); // 클래스에 on을 붙여준다.
        });

        Button closeBtn = root.Q<Button>("closeBtn");
        closeBtn.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 1;
            popup.RemoveFromClassList("on");
        });
    }
}
