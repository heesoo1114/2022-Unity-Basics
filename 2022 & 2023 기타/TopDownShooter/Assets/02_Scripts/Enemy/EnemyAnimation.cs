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
        // ���⼭ �극���� ���� ���ݻ��� ����
        _brain.AIActionData.isAttack = false;
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackHash);
    }

    public override void PlayDeadAnimation()
    {
        base.PlayDeadAnimation();
        _animator.SetBool(_DeadBoolHash, true);
    }

    public void EndofDeadAnimation()
    {
        _brain.Enemy.Die(); // �ִϸ��̼� �������� ������ �׿���..
    }
}
