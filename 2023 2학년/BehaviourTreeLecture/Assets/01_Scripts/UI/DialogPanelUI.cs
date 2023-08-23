using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogPanelUI : MonoBehaviour
{
    private Label _label;
    public string Text
    {
        get => _label.text;
        set
        {
            _label.text = value;
        }
    }
    public DialogPanelUI(VisualElement root, string msg)
    {
        _label = root.Q<Label>("MessageLabel");
    }

    public void Show(bool value)
    {
        // value가 true일 경우 보여주고 false일 경우 감추는
    }
}
