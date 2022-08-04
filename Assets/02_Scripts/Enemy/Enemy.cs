using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable, IAgent
{
    [SerializeField]
    protected EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData { get => _enemyData; }

    public bool IsEnemy => true;

    public Vector3 HitPoint { get; private set; }

    public int Health { get; private set; }

    [field: SerializeField] // ������Ƽ�� SerializeField �����ְڴ�.
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    protected bool _isDead = false;

    [SerializeField]
    protected bool _isActive = false; // ������ ��Ƽ�� �����ָ� ������ �����ҰŴ�.
    protected EnemyAIBrain _brain;
    protected EnemyAttack _attack;

    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyAIBrain>();
        _attack = GetComponent<EnemyAttack>();

        SetEnemyData();
    }

    private void SetEnemyData()
    {
        _attack.AttackDelay = _enemyData.attackDelay;  // ���ݵ����̸� ����

        transform.Find("AI/IdleState/TranChase").GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;
        transform.Find("AI/ChaseState/TranIdle").GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;
        transform.Find("AI/ChaseState/TranAttack").GetComponent<DecisionInner>().Distance = _enemyData.attackRange;
        transform.Find("AI/AttackState/TranChase").GetComponent<DecisionOuter>().Distance = _enemyData.attackRange;

        Health = _enemyData.maxHealth;
    }

    public virtual void PerformAttack() // ������ �õ��Ѵ�.
    {
        if(_isDead == false && _isActive == true)
        {
            _attack.Attack(_enemyData.damage);
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead == true) return;

        Health -= damage;
        HitPoint = damageDealer.transform.position; // ���� �� �༮

        OnGetHit?.Invoke(); // �ǰݽ� �ǵ���� ���� �̺�Ʈ Ʈ����

        if (Health <= 0)
            DeadProcess();
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        OnDie?.Invoke();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
