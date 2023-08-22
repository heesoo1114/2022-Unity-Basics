using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackAction : AIAction
{
    public override void TakeAction()
    {
        _brain.Attack(SkillName.Range);
    }
}
