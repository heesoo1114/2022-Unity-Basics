using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitState : CommonState
{
    [SerializeField]
    private float _recoverTime = 0.3f;

    private float _hitTimer;

    public override void OnEnterState()
    {
        _agentController.AgentHealthCompo.OnHitTrigger.AddListener(HandleHit);
    }

    public override void OnExitState()
    {
        _agentController.AgentHealthCompo.OnHitTrigger.RemoveListener(HandleHit);
        _agentAnimator.SetIsHit(false);
        _agentAnimator.SetHurtTrigger(false);
    }

    private void HandleHit(int damage, Vector3 point, Vector3 normal)
    {
        normal.y = 0;
        _hitTimer = 0;
        _agentAnimator.SetIsHit(true);
        _agentAnimator.SetHurtTrigger(true);
        _agentController.transform.rotation = Quaternion.LookRotation(normal);
    }

    public override bool UpdateState()
    {
        _hitTimer += Time.deltaTime;
        // 만약 넉백을 구현 -> AgentMovement에서 active 모드로 꺼주고
        // 넉백을 해주고 끝나면 꺼줌

        if (_hitTimer >= _recoverTime)
        {
            _agentController.ChangeState(Core.StateType.Normal); // 다 맞으면 노말로 리커버
        }

        return true;
    }
}
