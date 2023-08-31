using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Repeater : Decorator
{
    public Repeater(BehaviorTree t, BT_Node c) : base(t, c) { }

    public override Result Execute()
    {
        Debug.Log($"Child returned : {child.Execute()}");
        return base.Execute();
    }
}
