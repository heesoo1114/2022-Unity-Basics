using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct TabInfo
{
    public VisualElement Tab;
    public int Index;
}

public class TabBar : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _rootUI;

    private List<TabInfo> _tabList;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _tabList = new List<TabInfo>();
        _rootUI = _document.rootVisualElement;

        VisualElement content = _rootUI.Q<VisualElement>("Content");

        int idx = 0;
        _rootUI.Query<VisualElement>(className: "tab").ToList().ForEach(t =>
        {
            int myIdx = idx;
            _tabList.Add(new TabInfo { Tab = t, Index = myIdx });
            idx++;

            t.RegisterCallback<ClickEvent>(evt =>
            {
                content.style.left = new Length(-myIdx * 100, LengthUnit.Percent);
            });
        });
    }
}
