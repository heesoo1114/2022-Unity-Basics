using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        // ������ �����ϴ� �κ� �ʿ�
        _brain.Move(Vector2.zero, transform.position);
    }
}
