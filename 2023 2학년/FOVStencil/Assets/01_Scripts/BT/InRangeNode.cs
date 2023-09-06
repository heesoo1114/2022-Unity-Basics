using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class InRangeNode : ActionNode
{
    public float range;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        float distance = Vector3.Distance(brain.targetTrm.position, context.transform.position);
        
        if (distance < range)
        {
            blackboard.enemySpotPosition = brain.targetTrm.position;
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
