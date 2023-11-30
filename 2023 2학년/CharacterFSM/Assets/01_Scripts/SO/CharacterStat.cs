using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,
    maxHealth,
    armor,
    evasion,
    magicRegistance,
    damage,
    criticalDamage,
    criticalChance
}

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class CharacterStat : ScriptableObject
{
    [Header("Main Stat")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Protect Stat")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion; // 회피력
    public Stat magicRegistance;

    [Header("Attack Stat")]
    public Stat damage;
    public Stat criticalDamage;
    public Stat criticalChance;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary;

    protected Player _owner;
    public void SetOwner(Player owner)
    {
        _owner = owner;
    }

    private void OnEnable()
    {
        if (_fieldInfoDictionary == null)
        {
            _fieldInfoDictionary = new Dictionary<StatType, FieldInfo>();
        }
        _fieldInfoDictionary.Clear();

        Type characterStatType = typeof(CharacterStat);
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            FieldInfo statField = characterStatType.GetField(statType.ToString());

            if (statField != null)
            {
                _fieldInfoDictionary.Add(statType, statField);
            }
            else
            {
                Debug.LogError($"There are no Stat! error: {statType.ToString()}");
            }
        }
    }

    public Stat GetStateByType(StatType type)
    {
        return _fieldInfoDictionary[type].GetValue(this) as Stat;
    }

    // 일정 시간 버프
    public void IncreaseStatBy(int modifyValue, float duration, StatType statType)
    {
        _owner.StartCoroutine(StatModifyCor(modifyValue, duration, statType));
    }

    protected IEnumerator StatModifyCor(int modifyValue, float duration, StatType statType)
    {
        Stat target = GetStateByType(statType);
        target.AddModifer(modifyValue);
        yield return new WaitForSeconds(duration);
        target.RemoveModifier(modifyValue);
    }

}
