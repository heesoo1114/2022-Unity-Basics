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

            // Ÿ�ٿ� ������ �����ϴµ� ���� ���� �� ��
            Debug.Log("����");

            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine()); // ��Ÿ�� ������
        }
    }
}
