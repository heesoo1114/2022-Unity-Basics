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

[CreateAssetMenu(menuName ="SO/Stat/Player")]
public class CharacterStat : ScriptableObject
{
    [Header("ÁÖ¿ä ½ºÅÝ")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;


    [Header("¹æ¾î ½ºÅÈ")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicRegistance;

    [Header("°ø°Ý ½ºÅÈ")]
    public Stat damage;
    public Stat criticalDamage;
    public Stat criticalChance;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary;

    protected Player _owner;
    public void SetOwner(Player owner)
    {
        _owner = owner;
    }

    public void IncreaseStatBy(int modifyValue, float duration, StatType statType )
    {
        _owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statType));
    }

    protected IEnumerator StatModifyCoroutine(int modifyValue, float duration, StatType statType)
    {
        Stat target = GetStatByType(statType);
        target.AddModifier(modifyValue);
        yield return new WaitForSeconds(duration);
        target.RemoveModifier(modifyValue);
    }

    protected void OnEnable()
    {
        if(_fieldInfoDictionary == null)
        {
            _fieldInfoDictionary = new Dictionary<StatType, FieldInfo>();   
        }
        _fieldInfoDictionary.Clear();

        Type characterStatType = typeof(CharacterStat); 
        foreach(StatType statType in Enum.GetValues(typeof(StatType)))
        {
            FieldInfo statField = characterStatType.GetField(statType.ToString());

            if(statField == null ) {
                Debug.LogError($"There are no stat! error : {statType.ToString()}");
            }
            else
            {
                _fieldInfoDictionary.Add(statType, statField);
            }
        }
    }

    public Stat GetStatByType(StatType type)
    {
        return _fieldInfoDictionary[type].GetValue(this) as Stat;
    }
}
