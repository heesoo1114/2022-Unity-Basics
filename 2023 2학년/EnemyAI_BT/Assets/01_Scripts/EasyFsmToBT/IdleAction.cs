using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BT_Node
{
    public IdleAction(BehaviorTree t) : base(t) { }

    public override Result Execute()
    {
        Debug.Log("Performing Idle action.");
        return Result.Success;
    }
}
