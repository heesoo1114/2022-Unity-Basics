using BTVisual;
using UnityEngine;

public class ColCheckNode : ActionNode
{
    public float maxDistance = 10f;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        RaycastHit hit;

        Vector3 plus = new Vector3(0, 0.5f, 0);
        Vector3 dir = brain.targetTrm.transform.position - context.transform.position;
        float distance = Vector3.Distance(brain.targetTrm.position, context.transform.position);
        
        Physics.Raycast(context.transform.position + plus, dir.normalized, out hit, distance, brain.whatIsObstacle);

        if (hit.collider != null)
        {
            return State.SUCCESS;
        }
        else
        {
            return State.FAILURE;
        }
    }
}
