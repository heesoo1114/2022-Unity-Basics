using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Node : MonoBehaviour
{
    public enum Result
    {
        Running, Fail, Success
    }

    public BehaviorTree Tree { get; set; }

    public BT_Node(BehaviorTree t)
    {
        Tree = t;
    }

    public virtual Result Execute()
    {
        return Result.Fail;
    }
}
