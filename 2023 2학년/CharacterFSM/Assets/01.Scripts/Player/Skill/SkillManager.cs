using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerSkill
{
    Dash = 1,
    // Clone = 2,
    // Crystal = 5,
}

public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills = new Dictionary<Type, Skill>();

    private void Awake()
    {
        foreach (PlayerSkill skillType in Enum.GetValues(typeof(PlayerSkill)))
        {
            Skill skillComponent = GetComponent($"{skillType}Skill") as Skill;
            Type t = skillComponent.GetType();
            _skills.Add(t, skillComponent);
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type t = typeof(T);
        if (_skills.TryGetValue(t, out Skill targetSkill))
        {
            return targetSkill as T;
        }
        return null;
    }

    public void UseSkillFeedback(PlayerSkill skillType)
    {
        // 장착 아이템 중 이 스킬로 발동하는 효과의 아이템
    }
}
