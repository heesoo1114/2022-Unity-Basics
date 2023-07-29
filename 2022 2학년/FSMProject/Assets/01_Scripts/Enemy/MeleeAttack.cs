using System;
using System.Collections;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _playTime = 0.2f;
    private Action<bool> CallBack = null;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = _brain.GetComponent<SpriteRenderer>();
    }

    public override void Attack(Action<bool> Callback)
    {
        _spriteRenderer.color = Color.green;
        this.CallBack = Callback;
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_playTime);
        _spriteRenderer.color = Color.white;
        CallBack(true);
    }
}
