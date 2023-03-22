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
        _agentMovement.IsActiveMove = false; // ���� Ű����� ����

        Vector3 dir = _agentInput.GetCurrentInputDirection();

        if (dir.magnitude < 0.1f) // Ű���带 ������ �ʾҴٸ� �÷��̾ �ٶ󺸴� �������� ����
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
        if (_timer < _animationTreshold) return; // ������ 1�ʵ� �� ���� -> �� ������

        _agentMovement.StopImmediately();
        _agentController.ChangeState(StateType.Normal);
    }

    public override void UpdateState()
    {
        _timer += Time.deltaTime;
    }   
}
