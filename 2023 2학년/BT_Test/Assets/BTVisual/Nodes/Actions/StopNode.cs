using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopNode : ActionNode
{
    protected override void OnStart()
    {
        context.agent.isStopped = true;
    }

    protected override void OnStop()
    {
        // nothing
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
