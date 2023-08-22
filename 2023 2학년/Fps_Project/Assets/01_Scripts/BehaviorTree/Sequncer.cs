using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequncer : Composite
{
    private int nowNode = 0;
    public Sequncer(BehaviorTree t, BT_Node[] children) : base(t, children) { }

    public override Result Execute()
    {
        if (nowNode < children.Count)
        {
            Result result = children[nowNode].Execute();
            if (result == Result.Running)
            {
                return Result.Running;
            }
            else if (result == Result.Fail)
            {
                nowNode = 0;
                return Result.Success;
            }
            else
            {
                nowNode++;
                if (nowNode < children.Count)
                {
                    return Result.Running;
                }
                else
                {
                    nowNode = 0;
                    return Result.Success;
                }
            }
        }
        return Result.Success;
    }
}
