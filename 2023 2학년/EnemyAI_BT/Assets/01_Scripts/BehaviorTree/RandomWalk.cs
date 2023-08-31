using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : BT_Node
{
    protected Vector3 nextDestination { get; set; }

    public float spd = 3.0f;

    public bool FindNextDestination()
    {
        object o;
        bool found = false;
        found = Tree.blackBoard.TryGetValue("WorldBounds", out o);

        if (found)
        {
            Rect bounds = (Rect)o;
            float x = Random.value * bounds.width;
            float y = Random.value * bounds.height;
            nextDestination = new Vector3(x, y, nextDestination.z);
        }
        return found;
    }

    public RandomWalk(BehaviorTree t) : base(t)
    {
        nextDestination = Vector3.zero;
        FindNextDestination();
    }

    public override Result Execute()
    {
        if (Tree.gameObject.transform.position == nextDestination)
        {
            if (!FindNextDestination())
            {
                return Result.Fail;
            }
            else
            {
                return Result.Success;
            }
        }
        else
        {
            Tree.gameObject.transform.position = Vector3.MoveTowards(
                Tree.gameObject.transform.position,
                nextDestination,
                Time.deltaTime * spd
                );
            return Result.Running;
        }
    }
}
