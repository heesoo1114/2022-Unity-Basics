using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        Debug.Log("현재 Idle 액션 실행중");
    }
}
