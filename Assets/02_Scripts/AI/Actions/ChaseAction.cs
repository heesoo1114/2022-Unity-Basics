using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        // 현재 공격중이지 않다면 이동 가능

        Vector2 direction = _brain.Target.position - transform.position;

        _brain.Move(direction.normalized, _brain.Target.position);

    }
}
