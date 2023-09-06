using BTVisual;
using UnityEngine;

class AttackNode : ActionNode
{
    public float coolTime;
    public float randomRange;
    private float _nextFireTime;

    protected override void OnStart()
    {
        context.agent.isStopped = true; // 네브매시 정지
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (context.agent.isStopped == false)
        {
            context.agent.isStopped = true;
        }

        if (Time.time > _nextFireTime)
        {
            brain.Attack();
            _nextFireTime = Time.time + coolTime + Random.Range(-randomRange, randomRange);
            return State.SUCCESS;
        }
        else
        {
            return State.RUNNING;
        }
    }
}
