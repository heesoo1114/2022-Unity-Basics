using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAIBrain _brain;

    public UnityEvent AttackFeedback;

    protected float _attackDelay;
    protected bool _waitBeforeNextAttack;
    public bool WaitBeforeNextAttack { get => _waitBeforeNextAttack; }

    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyAIBrain>();
    }

    public abstract void Attack(int damage);

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(_attackDelay);
        _waitBeforeNextAttack = false;
    }

}
