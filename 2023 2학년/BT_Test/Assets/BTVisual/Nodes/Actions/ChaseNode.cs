using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : ActionNode
{
    public float speed = 2;
    public float stoppingDistance = 0.1f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart()
    {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.enemySpotPosition;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (context.agent.pathPending)
        {
            return State.RUNNING;
        }

        if (context.agent.isStopped)
        {
            context.agent.isStopped = false;
        }

        if (context.agent.remainingDistance < tolerance)
        {
            return State.SUCCESS;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.FAILURE;
        }

        float dis = Vector3.Distance(brain.targetTrm.position, context.agent.destination);
        if (dis > tolerance)
        {
            blackboard.enemySpotPosition = brain.targetTrm.position;
            context.agent.destination = blackboard.enemySpotPosition;
        }

        return State.RUNNING;
    }
}
