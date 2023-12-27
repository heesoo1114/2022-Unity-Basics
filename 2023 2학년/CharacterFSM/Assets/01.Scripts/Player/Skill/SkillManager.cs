using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkill
{
    Dash = 1, 
    Clone = 2, 
    Crystal = 3,
}

public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills = new Dictionary<Type, Skill>();
    private Dictionary<PlayerSkill, Type> _skillTypes = new Dictionary<PlayerSkill, Type>();
    private void Awake()
    {
        //시작시에 자기에게 달린 모든 애들을 다 타입별로 딕셔너리에 넣는다.
        foreach(PlayerSkill skillType in Enum.GetValues(typeof(PlayerSkill)))
        {
            Skill skillComponent = GetComponent($"{skillType}Skill") as Skill;
            Type t = skillComponent.GetType();
            _skills.Add(t, skillComponent);
            _skillTypes.Add(skillType, t);
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type t = typeof(T);
        if(_skills.TryGetValue(t, out Skill targetSkill))
        {
            return targetSkill as T;
        }
        return null;
    }

    public Skill GetSkill(PlayerSkill skillEnum)
    {
        Type t = _skillTypes[skillEnum];
        if(_skills.TryGetValue(t, out Skill skill))
        {
            return skill;
        }
        return null;
    }


    public void UseSkillFeedback(PlayerSkill skillType)
    {
        //장착 아이템중 이 스킬로 발동하는 효과의 아이템이 있다면 발동하도록 한다.
    }
}
