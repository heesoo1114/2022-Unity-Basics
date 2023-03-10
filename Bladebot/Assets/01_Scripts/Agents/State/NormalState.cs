using UnityEngine;

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
        _agnetInput.OnMovementKeyPress += OnMovementHandle; // 들어올 때 키입력 구독
    }

    public override void OnExitState()
    {
        _agentMovement.StopImmediately();
        _agnetInput.OnMovementKeyPress -= OnMovementHandle; // 나갈 때 키입력 구독해제
    }

    private void OnMovementHandle(Vector3 obj)
    {
        _agentMovement?.SetMovementVelocity(obj);
    }

    public override void UpdateState()
    {

    }
}
