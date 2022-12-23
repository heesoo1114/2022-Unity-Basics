using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAction : AIAction
{
    public override void TakeAction()
    {
        _brain.Attack(SkillName.Melee);
    }
}
