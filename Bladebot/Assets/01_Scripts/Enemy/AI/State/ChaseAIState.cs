using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIState : CommonAIState
{
    public override void OnEnterState()
    {

    }

    public override void OnExitState()
    {

    }

    public override void UpdateState()
    {
        base.UpdateState();
        // 여기에 할 일을 써주자

        _enemyController.NavMovement.MoveToTarget(_aiActionData.LastSpotPoint);
        _aiActionData.IsArrived = _enemyController.NavMovement.CheckIsArrived();
    }
}
