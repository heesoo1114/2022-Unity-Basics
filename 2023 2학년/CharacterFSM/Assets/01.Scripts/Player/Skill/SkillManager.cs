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
        //���۽ÿ� �ڱ⿡�� �޸� ��� �ֵ��� �� Ÿ�Ժ��� ��ųʸ��� �ִ´�.
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
        //���� �������� �� ��ų�� �ߵ��ϴ� ȿ���� �������� �ִٸ� �ߵ��ϵ��� �Ѵ�.
    }
}
