using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(menuName = "SO/Item/Equipment")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;

    //[Header("item effect")]
    [TextArea]
    public string itemEffectDescription;

    [Header("주요 스텟")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;


    [Header("방어 스탯")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicRegistance;

    [Header("공격 스탯")]
    public int damage;
    public int criticalDamage;
    public int criticalChance;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary;

    //이 장비의 고유 효과의 쿨타임.
    // 

    private void OnEnable()
    {
        _fieldInfoDictionary = new Dictionary<StatType, FieldInfo>();

        Type itemType = typeof(ItemDataEquipment);
        foreach(StatType statType in Enum.GetValues(typeof(StatType)))
        {
            FieldInfo itemStatField = itemType.GetField(statType.ToString());

            if(itemStatField == null)
            {
                Debug.LogError($"There are no stat! error : {statType.ToString()} object: {name}");
            }
            else
            {
                _fieldInfoDictionary.Add(statType, itemStatField);
            }
        }
    }

    public void AddModifiers()
    {
        CharacterStat playerStat = GameManager.Instance.Player.Stat;
        if (playerStat == null) return;

        foreach(var fieldPair in _fieldInfoDictionary)
        {
            Stat stat = playerStat.GetStatByType(fieldPair.Key);
            int modifyValue = (int) fieldPair.Value.GetValue(this);
            stat.AddModifier(modifyValue);
        }
    }

    public void RemoveModifiers()
    {
        CharacterStat playerStat = GameManager.Instance.Player.Stat;
        if (playerStat == null) return;

        foreach (var fieldPair in _fieldInfoDictionary)
        {
            Stat stat = playerStat.GetStatByType(fieldPair.Key);
            int modifyValue = (int)fieldPair.Value.GetValue(this);
            stat.RemoveModifier(modifyValue);
        }
    }

    private string GetItemDescription()
    {
        _stringBuilder.Clear();
        foreach (var fieldPair in _fieldInfoDictionary)
        {
            AddItemDescription((int)fieldPair.Value.GetValue(this), fieldPair.Key.ToString());
        }

        return _stringBuilder.ToString();
    }

    private void AddItemDescription(int value, string statName)
    {
        if (value != 0)
        {
            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.AppendLine(); // 한줄 추가
            }

            _stringBuilder.Append($"{statName} : {value.ToString()}");
        }
    }
}
