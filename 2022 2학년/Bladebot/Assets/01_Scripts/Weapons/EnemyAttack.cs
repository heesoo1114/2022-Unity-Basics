using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIActionData _actionData;

    protected virtual void Awake()
    {
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public abstract void Attack(int damage, Vector3 targetDir);
    public abstract void PreAttack();
    public abstract void CancelAttack();
}
