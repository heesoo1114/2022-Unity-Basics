using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAIState : CommonAIState
{
    public override void OnEnterState()
    {
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AnimationEndHandle;
        _enemyController.AgentAnimator.SetHurtTrigger(true);
        _aiActionData.IsHit = true;
    }

    public override void OnExitState()
    {
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AnimationEndHandle;
        _enemyController.AgentAnimator.SetHurtTrigger(false);
    }

    private void AnimationEndHandle()
    {
        _aiActionData.IsHit = false;
    }

    private void FixedUpdate()
    {
        // 넉백을 넣을라고 하면 여기에 넣으면 돼
    }
}
