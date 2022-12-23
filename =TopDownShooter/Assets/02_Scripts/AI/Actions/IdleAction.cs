using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector2.zero;
        _aiMovementData.pointOfInterest = transform.position;

        _brain.Move(Vector2.zero, transform.position);
    }
}
