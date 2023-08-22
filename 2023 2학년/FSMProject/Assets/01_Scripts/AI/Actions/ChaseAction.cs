using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 direction = _brain.target.position - transform.position;

        _brain.Move(direction.normalized, _brain.target.position);
    }
}
