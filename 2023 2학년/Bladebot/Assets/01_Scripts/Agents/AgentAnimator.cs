using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("speed");
    private readonly int _isAirboneHash = Animator.StringToHash("is_airbone");
    
    private readonly int _attackHash = Animator.StringToHash("attack");
    private readonly int _isAttackHash = Animator.StringToHash("is_attack");

    private readonly int _isRollingHash = Animator.StringToHash("is_rolling");

    private readonly int _isDeadHash = Animator.StringToHash("is_dead");
    private readonly int _deadTriggerHash = Animator.StringToHash("dead");

    private readonly int _hurtTriggerHash = Animator.StringToHash("hurt");
    private readonly int _isHitHash = Animator.StringToHash("is_hit");

    private Animator _animator;
    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    public void SetRollingState(bool state)
    {
        _animator.SetBool(_isRollingHash, state);
    }

    public void SetAttackState(bool state)
    {
        _animator.SetBool(_isAttackHash, state);
    }

    public void SetIsHit(bool value)
    {
        _animator.SetBool(_isHitHash, value);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_attackHash);
        }
        else
        {
            _animator.ResetTrigger(_attackHash);
            // 이전의 트리거 값이 남아있으면 안 되기 때문에 지워줌
        }
    }

    public void SetHurtTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_hurtTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_hurtTriggerHash);
        }
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        _animator.SetBool(_isAirboneHash, value);
    }

    public void StopAnimator(bool value)
    {
        _animator.speed = value ? 0 : 1;
    }

    public void SetDead()
    {
        _animator.SetTrigger(_deadTriggerHash);
        _animator.SetBool(_isDeadHash, true);
    }

    #region 애니메이션 이벤트 영역
    // action 앞에 event가 붙으면 해당 스크립트 안에서만 사용 가능
    // event를 decorator라고 부름
    public event Action OnAnimationEndTrigger = null; // 애니메이션이 종료될 때마다 트리거 되는 이벤트
    public event Action OnAnimationEventTrigger = null; // 애니메이션 내의 이벤트 트리거
    public event Action OnPreAnimationEventTrigger = null; // 전조 애니메이션 트리거

    public void OnAnimationEnd() // 애니메이션이 종료되면 실행됨
    {
        OnAnimationEndTrigger?.Invoke();
    }

    public void OnAnimationEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }

    public void OnPreAnimationEvent()
    {
        OnPreAnimationEventTrigger?.Invoke();
    }
    #endregion
}