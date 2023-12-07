using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;

    public List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = _baseValue;
        for(int i = 0; i < modifiers.Count; ++i)
        {
            finalValue += modifiers[i];
        }
        return finalValue;
    }

    public void SetDefaultValue(int value)
    {
        _baseValue = value;
    }

    public void AddModifier(int value)
    {
        if(value != 0) 
            modifiers.Add(value);
    }

    public void RemoveModifier(int value) { 
        if(value != 0)
            modifiers.Remove(value);
    }
}
