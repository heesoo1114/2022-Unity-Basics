using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        // ���� ���������� �ʴٸ� �̵� ����

        Vector2 direction = _brain.Target.position - transform.position;

        _brain.Move(direction.normalized, _brain.Target.position);

    }
}
