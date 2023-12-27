using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public bool skillEnabled = false; //�رݵǾ��ִ� ��ų�ΰ�?

    [SerializeField] protected float _cooldown;
    [SerializeField] protected LayerMask _whatIsEnemy;
    protected float _cooldownTimer;
    protected Player _player;

    [SerializeField] protected PlayerSkill _skillType;

    //��Ÿ���� ���� ������ ����� �̺�Ʈ�� UI���� �̸� �����Ѵ�.
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
        //�� ��ų���� ��ų�� ���ÿ� �ߵ��ϴ� ȿ������ ����� ���� �� �� ����.
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
