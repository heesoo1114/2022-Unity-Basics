using System;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;
    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<AIBrain>();
    }

    public abstract void Attack(Action<bool> Callback);
}
