using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public bool skillEnabled = false; // �ر� ����

    [SerializeField] protected float _cooldown;
    protected float _cooldownTimer;
    protected Player _player;

    [SerializeField] protected PlayerSkill _skillType;

    // ��Ÿ���� ���� ���� �� ����� �̺�Ʈ�� UI ���� �̸� �����Ѵ�.
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
