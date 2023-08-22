using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CanAttack : AIDecision
{
    public SkillName Skill;

    public override bool MakeADecision()
    {
        FieldInfo fInfo =
            typeof(AIStateInfo).GetField($"{Skill.ToString()}Cool", BindingFlags.Public | BindingFlags.Instance);
        float coolTime = (float)fInfo.GetValue(_state);

        return _state.IsAttack == false && coolTime <= 0;
    }
}
