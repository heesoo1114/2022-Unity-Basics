using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Á¶°Ç
public class Decorator : BT_Node
{
    public BT_Node child { get; set; }
    public Decorator(BehaviorTree t, BT_Node c) : base(t)
    {
        child = c;
    }
}
