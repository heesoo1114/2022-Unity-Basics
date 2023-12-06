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

[CreateAssetMenu(menuName = "SO/Items/Equipment")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;

    [TextArea] public string itemEffectDescription;

    [Header("Main Stat")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Protect Stat")]
    public int maxHealth;
    public int armor;
    public int evasion; // È¸ÇÇ·Â
    public int magicRegistance;

    [Header("Attack Stat")]
    public int damage;
    public int criticalDamage;
    public int criticalChance;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary;

    private void OnEnable()
    {
        _fieldInfoDictionary = new Dictionary<StatType, FieldInfo>();

        Type itemType = typeof(ItemDataEquipment);
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            FieldInfo itemStatField = itemType.GetField(statType.ToString());

            if (itemStatField == null)
            {
                Debug.LogError($"There are no stat! error: {statType.ToString()} object: {name}");
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

        foreach (var fieldPair in _fieldInfoDictionary)
        {
            Stat stat = playerStat.GetStateByType(fieldPair.Key);
            int modifyValue = (int)fieldPair.Value.GetValue(this);
            stat.AddModifer(modifyValue);
        }
    }
}
