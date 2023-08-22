using System;
using UnityEngine;
using Core;

public class NormalState : CommonState
{  
    public override void OnEnterState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress += OnMovementHandle; // ���� �� Ű�Է� ����
        _agentInput.OnAttackKeyPress += OnAttackKeyHandle;
        _agentInput.OnRollingKeyPress += OnRollingHandle;
        _agentMovement.IsActiveMove = true;
    }

    public override void OnExitState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress -= OnMovementHandle; // ���� �� Ű�Է� ��������
        _agentInput.OnAttackKeyPress -= OnAttackKeyHandle;
        _agentInput.OnRollingKeyPress -= OnRollingHandle;
        _agentMovement.IsActiveMove = false;
    }

    private void OnAttackKeyHandle()
    {
        // ����Ű��  ó�� ���� ���� ���ݻ��·� ��ȯ
        
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
        return false; // true, false �������
    }
}
