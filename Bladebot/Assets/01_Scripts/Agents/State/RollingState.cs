using System;
using System.Collections;
using UnityEngine;
using Core;

public class RollingState : CommonState
{
    [SerializeField]
    private float _rollingSpeed = 0.4f, _animationTreshold = 0.1f;
    private float _timer = 0;

    public override void OnEnterState()
    {
        _agentAnimator.OnAnimationEndTrigger += RollingEndHandle;
        _agentMovement.IsActiveMove = false; // 이제 키보드로 조종

        Vector3 dir = _agentInput.GetCurrentInputDirection();

        if (dir.magnitude < 0.1f) // 키보드를 누르지 않았다면 플레이어가 바라보는 방향으로 설정
        {
            dir = _agentController.transform.forward;
        }

        _agentMovement.SetRotation(dir + _agentController.transform.position);
        _agentMovement.StopImmediately();
        _agentMovement.SetMovementVelocity(transform.forward * _rollingSpeed);
        _agentAnimator.SetRollingState(true);
        _timer = 0;
    }

    public override void OnExitState()
    {
        _agentAnimator.OnAnimationEndTrigger -= RollingEndHandle;
        _agentAnimator.SetRollingState(false);
        _agentMovement.IsActiveMove = true;
    }

    private void RollingEndHandle()
    {
        if (_timer < _animationTreshold) return; // 들어온지 1초도 안 지남 -> 안 나가게

        _agentMovement.StopImmediately();
        _agentController.ChangeState(StateType.Normal);
    }

    public override void UpdateState()
    {
        _timer += Time.deltaTime;
    }   
}
