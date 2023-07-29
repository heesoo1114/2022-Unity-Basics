using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEndDecision : AIDecision
{
    public override bool MakeDecision()
    {
        return _aiActionData.IsAttacking == false;
    }
}
