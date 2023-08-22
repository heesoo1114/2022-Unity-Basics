using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class EnemyAttackData
{
    public EnemyAttack atk;
    public Action<bool> action;
    public float coolTime;
}

public class AIBrain : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementCommand;

    [SerializeField]
    private AIState _currentState;
    private AIStateInfo _stateInfo;
    private AgentMovement _movement;

    public Transform target = null;
    private Dictionary<SkillName, EnemyAttackData> _attackDictionary = new Dictionary<SkillName, EnemyAttackData>();

    public float viewAngle;
    public float viewRange;

    private void Awake()
    {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        _movement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        target = GameManager.Instance.PlayerTrm;
        MakeAttackType();
    }

    private void MakeAttackType()
    {
        Transform atkTrm = transform.Find("AttackType");

        EnemyAttackData rangeAttack = new EnemyAttackData
        {
            atk = atkTrm.GetComponent<RangeAttack>(),
            action = (value) =>
            {
                _stateInfo.IsAttack = false;
                _stateInfo.IsRange = false;
            },
            coolTime = 3f
        },
        meleeAttack = new EnemyAttackData
        {
            atk = atkTrm.GetComponent<MeleeAttack>(),
            action = (value) => {
                _stateInfo.IsAttack = false;
                _stateInfo.IsMelee = false;
            },
            coolTime = 1f
        };

        _attackDictionary.Add(SkillName.Range, rangeAttack);
        _attackDictionary.Add(SkillName.Melee, meleeAttack);
    }

    public void ChangeToState(AIState nextState)
    {
        _currentState = nextState;
    }

    protected virtual void Update()
    {
        _currentState.UpdateState();

        if (_stateInfo.MeleeCool > 0)
        {
            _stateInfo.MeleeCool -= Time.deltaTime;
            if (_stateInfo.MeleeCool < 0) _stateInfo.MeleeCool = 0;
        }
        if (_stateInfo.RangeCool > 0)
        {
            _stateInfo.RangeCool -= Time.deltaTime;
            if (_stateInfo.RangeCool < 0) _stateInfo.RangeCool = 0;
        }
    }

    public virtual void Attack(SkillName skillname)
    {
        if (_stateInfo.IsAttack) return;

        EnemyAttackData atkData = null;

        FieldInfo finfo = typeof(AIStateInfo).GetField($"{ skillname.ToString()}Cool", BindingFlags.Public | BindingFlags.Instance);

        if ((float)finfo.GetValue(_stateInfo) > 0)
        {
            return; // 쿨타임 존재시 공격 X
        }

        FieldInfo finfoBool = typeof(AIStateInfo)
            .GetField($"Is{skillname.ToString()}", BindingFlags.Public | BindingFlags.Instance);
        
        if (_attackDictionary.TryGetValue(skillname, out atkData))
        {
            _movement.StopImmediatley();
            _stateInfo.IsAttack = true;
            finfoBool.SetValue(_stateInfo, true);
            finfo.SetValue(_stateInfo, atkData.coolTime);
            atkData.atk.Attack(atkData.action);
;        }
    }

    public void Move(Vector2 direction, Vector2 targetPos)
    {
        OnMovementCommand?.Invoke(direction);
    }
}
