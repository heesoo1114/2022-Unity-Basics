using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public bool skillEnabled = false; // 해금 유무

    [SerializeField] protected float _cooldown;
    protected float _cooldownTimer;
    protected Player _player;

    [SerializeField] protected PlayerSkill _skillType;

    // 쿨타임이 돌고 있을 때 발행될 이벤트로 UI 등이 이를 구독한다.
    public event Action<float, float> OnCooldown;

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
            }

            OnCooldown?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool AttemptUseSkill()
    {
        if (_cooldownTimer <= 0 && skillEnabled)
        {
            _cooldownTimer = _cooldown;
            UseSkill();
            return true;
        }
        Debug.Log("Skill Cooldown or locked");
        return false;
    }

    public virtual void UseSkill()
    {
        SkillManager.Instance.UseSkillFeedback(_skillType);
    }
}
