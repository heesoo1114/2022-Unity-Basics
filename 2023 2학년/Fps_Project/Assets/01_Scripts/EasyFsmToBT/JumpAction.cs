using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : BT_Node
{
    public JumpAction(BehaviorTree t) : base(t) { }

    public override Result Execute()
    {
        Debug.Log("Performing Jump action.");
        return Result.Success;
    }
}
