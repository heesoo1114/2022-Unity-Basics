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

            // 타겟에 공격이 들어가야하는데 아직 구현 안 함
            Debug.Log("공격");

            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine()); // 쿨타임 돌리고
        }
    }
}
