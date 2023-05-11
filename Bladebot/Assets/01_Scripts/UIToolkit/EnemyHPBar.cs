using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHPBar 
{
    private VisualElement _barRect;
    private VisualElement _bar;
    private Label _hpLabel;
    private Label _nameLabel;

    private int _currentHP;
    public int HP
    {
        set
        {
            _currentHP = value;
            UpdateHPText();
        }
    }

    private int _maxHP;
    public int MaxHP
    {
        set
        {
            _currentHP = _maxHP = value;
            UpdateHPText();
        }
    }

    public string EnemyName
    {
        set
        {
            _nameLabel.text = $"{value}";
        }
    }

    private void UpdateHPText()
    {
        _bar.transform.scale = new Vector3((float)_currentHP / _maxHP, 1, 0);
        _hpLabel.text = $"{_currentHP} / {_maxHP}";
    }

    public EnemyHPBar(VisualElement bar)
    {
        _barRect = bar;
        _bar = bar.Q<VisualElement>("Bar");
        _hpLabel = bar.Q<Label>("HPLabel");
        _nameLabel = bar.Q<Label>("NameLabel");
    }

    public void ShowBar(bool value)
    {
        if (value )
        {
            _barRect.AddToClassList("on");
        }
        else
        {
            _barRect.RemoveFromClassList("on");
        }
    }
}
