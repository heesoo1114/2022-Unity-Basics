using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class AttackState : CommonState
{
    [SerializeField]
    private float _keyDelay = 0.5f; // 0.5�� ���� ���콺�� �� �� ������� ����

    private int _currentCombo = 0; // ���� �޺� ��ġ
    private bool _canAttack = true; // ���� ������ ��������
    private float _keyTimer = 0;

    public override void OnEnterState()
    {
        _agentInput.OnAttackKeyPress += OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger += OnAnimationHandle;
        _currentCombo = 0;
        _canAttack = true;
        _agentAnimator.SetAttackState(true);
        OnAttackHandle(); // �������� ���� ����
    }

    public override void OnExitState()
    {
        _agentInput.OnAttackKeyPress -= OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger -= OnAnimationHandle;
        _agentAnimator.SetAttackState(false); // ���ݻ��·� ��ȯ
        _agentAnimator.SetAttackTrigger(false);
    }

    private void OnAnimationHandle()
    {
        _canAttack = true;
        _keyTimer = _keyDelay; // 0.5�� �ð� �־��ְ� �������� ī��Ʈ ����
    }

    private void OnAttackHandle()
    {
        if (_canAttack && _currentCombo < 3)
        {
            _canAttack = false;
            _agentAnimator.SetAttackTrigger(true);
            _currentCombo++;
        }
    }

    public override void UpdateState()
    {
        if (_canAttack && _keyTimer > 0)
        {
            _keyTimer -= Time.deltaTime;
            if (_keyTimer <= 0)
            {
                _agentController.ChangeState(StateType.Normal);
            }
        }
    }
}
