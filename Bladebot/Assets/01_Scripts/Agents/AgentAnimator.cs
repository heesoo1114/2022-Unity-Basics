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

    public event Action OnAnimationEndTrigger = null; // 애니메이션이 종료될 때마다 트리거 되는 이벤트
    public event Action OnAnimationEventTrigger = null; // 애니메이션 내의 이벤트 트리거

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

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        _animator.SetBool(_isAirboneHash, value);
    }

    public void OnAnimationEnd() // 애니메이션이 종료되면 실행됨
    {
        OnAnimationEndTrigger?.Invoke();
    }

    public void OnAnimationEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }
}