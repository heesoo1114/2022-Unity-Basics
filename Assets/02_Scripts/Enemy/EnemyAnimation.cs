using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    protected EnemyAIBrain _brain;
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _DeadBoolHash = Animator.StringToHash("isDead");

    protected override void Awake()
    {
        base.Awake();
        _brain = transform.parent.GetComponent<EnemyAIBrain>();
    }

    public void SetEndOfAttackAnimation()
    {
        // 여기서 브레인을 통해 공격상태 해제
        _brain.AIActionData.isAttack = false;
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackHash);
    }

    public void PlayDeadAnimation()
    {
        _animator.SetBool(_DeadBoolHash, true);
        _animator.SetTrigger(_deadHash);
    }

    public void EndofDeadAnimation()
    {
        _brain.Enemy.Die(); // 애니메이션 끝났으면 실제로 죽여라..
    }
}
