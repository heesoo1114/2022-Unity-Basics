using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
using UnityEditor;
using UnityEngine.Rendering;

public class ShootNode : ActionNode
{
    public float coolTime;
    public float randomRange;
    private float _nextFireTime;
    public float rotateSpeed;

    protected override void OnStart()
    {
        context.agent.isStopped = true;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Vector3 dir = brain.targetTrm.position - context.transform.position;
        dir.y = 0;
        context.transform.rotation = Quaternion.LookRotation(dir);

        if (context.agent.isStopped == false)
        {
            context.agent.isStopped = true;
        }

        if (Time.time > _nextFireTime)
        {
            brain.Attack();
            _nextFireTime = Time.time + coolTime + Random.Range(-randomRange, randomRange);
            return State.RUNNING;
        }
        else
        {
            return State.RUNNING;
        }
    }
}
