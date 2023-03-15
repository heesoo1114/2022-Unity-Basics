using System;
using UnityEngine;
using Core;

public class NormalState : CommonState
{
    private AgentMovement _agentMovement;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _agentMovement = agentRoot.GetComponent<AgentMovement>(); 
    }

    public override void OnEnterState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress += OnMovementHandle; // ���� �� Ű�Է� ����
        _agentInput.OnAttackKeyPress += OnAttackKeyHandle;
    }

    private void OnAttackKeyHandle()
    {
        _agentController.ChangeState(StateType.Attack);
    }

    public override void OnExitState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress -= OnMovementHandle; // ���� �� Ű�Է� ��������
        _agentInput.OnAttackKeyPress -= OnAttackKeyHandle;
    }

    private void OnMovementHandle(Vector3 obj)
    {
        _agentMovement?.SetMovementVelocity(obj);
    }

    public override void UpdateState()
    {

    }
}
