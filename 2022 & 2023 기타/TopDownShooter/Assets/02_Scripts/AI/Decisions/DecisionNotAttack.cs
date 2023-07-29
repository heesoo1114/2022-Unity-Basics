using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionNotAttack : AIDecision
{
    public override bool MakeADecision()
    {
        return !_aiActionData.isAttack;
    }
}
