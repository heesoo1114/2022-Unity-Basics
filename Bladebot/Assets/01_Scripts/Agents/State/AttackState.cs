using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class AttackState : CommonState
{
    [SerializeField]
    private float _keyDelay = 0.5f; // 0.5초 내에 마우스를 한 번 눌러줘야 실행

    private int _currentCombo = 0; // 현재 콤보 수치
    private bool _canAttack = true; // 현재 공격이 가능한지
    private float _keyTimer = 0;

    public override void OnEnterState()
    {
        _agentInput.OnAttackKeyPress += OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger += OnAnimationHandle;
        _currentCombo = 0;
        _canAttack = true;
        _agentAnimator.SetAttackState(true);
        OnAttackHandle(); // 수동으로 공격 시작
    }

    public override void OnExitState()
    {
        _agentInput.OnAttackKeyPress -= OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger -= OnAnimationHandle;
        _agentAnimator.SetAttackState(false); // 공격상태로 전환
        _agentAnimator.SetAttackTrigger(false);
    }

    private void OnAnimationHandle()
    {
        _canAttack = true;
        _keyTimer = _keyDelay; // 0.5초 시간 넣어주고 이제부터 카운트 시작
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
