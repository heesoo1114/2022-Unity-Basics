using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한 명만 성공해라
public class Selector : Composite
{
    public Selector(BehaviorTree t, BT_Node[] children) : base(t, children) { }

    public override Result Execute()
    {
        foreach (var child in children)
        {
            Result result = child.Execute();

            if (result == Result.Success)
            {
                return Result.Success;
            }
            else if (result == Result.Running)
            {
                return Result.Running;
            }
        }
        
        return Result.Fail;
    }
}
