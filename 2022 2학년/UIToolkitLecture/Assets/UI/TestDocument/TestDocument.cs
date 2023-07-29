using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestDocument : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        VisualElement background = root.Q("Background");

        // UIElement¿« Button
        Button btn = root.Q<Button>("MyBtn");
        btn.RegisterCallback<ClickEvent>(e =>
        {
            background.style.backgroundColor = new Color(0.4f, 0.3f, 0.2f, 0.1f);
        });
    }

    private void BtnCallback(ClickEvent evt)
    {
        Debug.Log("click");
    }
}
