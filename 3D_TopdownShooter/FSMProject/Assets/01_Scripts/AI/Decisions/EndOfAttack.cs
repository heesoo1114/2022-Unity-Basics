using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EndOfAttack : AIDecision
{
    public SkillName skill;
    private FieldInfo skillInfo;
    protected override void Awake()
    {
        base.Awake();
        skillInfo = typeof(AIStateInfo).GetField($"Is{skill.ToString()}", BindingFlags.Public | BindingFlags.Instance);
    }

    public override bool MakeADecision()
    {
        return (bool)skillInfo.GetValue(_state) == false;
    }
}
