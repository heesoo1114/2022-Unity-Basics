using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(_waitBeforeNextAttack == false)
        {
            _brain.AIActionData.isAttack = true;

            float range = _brain.Enemy.EnemyData.attackRange;
            float distance = Vector2.Distance(_brain.BasePosition.position, _brain.Target.position);

            if(distance < range)
            {
                IHitable hit = _brain.Target.GetComponent<IHitable>();
                hit?.GetHit(damage, gameObject);
            }

            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine()); // 쿨타임 돌리고
        }
    }
}
