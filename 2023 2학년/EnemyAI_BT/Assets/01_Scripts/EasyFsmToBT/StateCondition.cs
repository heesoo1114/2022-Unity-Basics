using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCondition : BT_Node
{
    public StateCondition(BehaviorTree t) : base(t) { }

    public override Result Execute()
    {
        if (Tree.nowState == PlayerState.Idle)
        {
            return Result.Success;
        }
        else if (Tree.nowState == PlayerState.Run)
        {
            return Result.Success;
        }
        else if (Tree.nowState == PlayerState.Jump)
        {
            return Result.Success;
        }

        return Result.Fail;
    }
}
