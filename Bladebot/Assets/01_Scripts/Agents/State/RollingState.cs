using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEditor.VersionControl;

public class RollingState : CommonState
{
    [SerializeField]
    private float _rollingSpeed = 0.4f;

    public override void OnEnterState()
    {
        _agentAnimator.OnAnimationEndTrigger += RollingEndHandle;
        _agentMovement.IsActiveMove = false; // 이제 키보드로 조종

        // 여기서 롤링을 마우스 있는 곳으로 롤링할지, 키보드를 누른 방향으로 롤링할지 결정

        _agentMovement.StopImmediately();
        _agentMovement.SetMovementVelocity(transform.forward * _rollingSpeed);
        _agentAnimator.SetRollingState(true);
    }

    public override void OnExitState()
    {
        _agentAnimator.OnAnimationEndTrigger -= RollingEndHandle;
        _agentAnimator.SetRollingState(false);
        _agentMovement.IsActiveMove = true;
    }

    private void RollingEndHandle()
    {
        _agentMovement.StopImmediately();
        _agentController.ChangeState(StateType.Normal);
    }

    public override void UpdateState()
    {

    }   
}
