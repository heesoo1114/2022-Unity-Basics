using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveDecision : AIDecision
{
    public override bool MakeDecision()
    {
        return _aiActionData.IsArrived;
    }
}
