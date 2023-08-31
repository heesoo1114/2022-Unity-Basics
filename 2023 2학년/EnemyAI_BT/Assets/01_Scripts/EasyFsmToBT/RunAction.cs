using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : BT_Node
{
    public RunAction(BehaviorTree t) : base(t) { }

    public override Result Execute()
    {
        Debug.Log("Performing Run action.");
        return Result.Success;
    }
}
