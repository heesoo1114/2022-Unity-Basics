using System;
using UnityEngine;
using Core;

public class NormalState : CommonState
{  
    public override void OnEnterState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress += OnMovementHandle; // 들어올 때 키입력 구독
        _agentInput.OnAttackKeyPress += OnAttackKeyHandle;
        _agentInput.OnRollingKeyPress += OnRollingHandle;
        _agentMovement.IsActiveMove = true;
    }

    public override void OnExitState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress -= OnMovementHandle; // 나갈 때 키입력 구독해제
        _agentInput.OnAttackKeyPress -= OnAttackKeyHandle;
        _agentInput.OnRollingKeyPress -= OnRollingHandle;
        _agentMovement.IsActiveMove = false;
    }

    private void OnAttackKeyHandle()
    {
        // 공격키를  처음 누른 순간 공격상태로 전환
        
        _agentMovement.SetRotation(_agentInput.GetMouseWorldPosision());
        _agentController.ChangeState(StateType.Attack);
    }

    private void OnMovementHandle(Vector3 obj)
    {
        _agentMovement?.SetMovementVelocity(obj);
    }

    private void OnRollingHandle()
    {
        _agentController.ChangeState(StateType.Rolling);
    }

    public override bool UpdateState()
    {
        return false; // true, false 상관없음
    }
}
