using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public bool skillEnabled = false; //해금되어있는 스킬인가?

    [SerializeField] protected float _cooldown;
    [SerializeField] protected LayerMask _whatIsEnemy;
    protected float _cooldownTimer;
    protected Player _player;

    [SerializeField] protected PlayerSkill _skillType;

    //쿨타임이 돌고 있을때 발행될 이벤트로 UI등이 이를 구독한다.
    public event Action<float, float> OnCooldown; 

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
    }

    protected virtual void Update()
    {
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if(_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
            }

            OnCooldown?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool AttemptUseSkill()
    {
        if(_cooldownTimer <= 0 && skillEnabled)
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
        //각 스킬마다 스킬을 사용시에 발동하는 효과들을 만들고 싶을 수 도 있지.
        SkillManager.Instance.UseSkillFeedback(_skillType);
    }

    public virtual Transform FindClosestEnemy(Transform checkTrm, float radius)
    {
        Transform target = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkTrm.position, radius, _whatIsEnemy);

        float closestDistance = Mathf.Infinity;

        foreach(Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(checkTrm.position, collider.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                target = collider.transform;
            }
        }

        return target;

    }
}
