using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float moveSpeed = 10f;
    protected Enemy _enenyTarget;

    [SerializeField] private float minDisToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }
    public float Damage { get; set; }

    protected virtual void Update()
    {
        if(_enenyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enenyTarget.transform.position, moveSpeed * Time.deltaTime);

        float disToTarget = (_enenyTarget.transform.position - transform.position).magnitude;
        if(disToTarget < minDisToDealDamage) //적과 총알 거리 충분히 가까움. 총알 맞음
        {
            OnEnemyHit?.Invoke(_enenyTarget, Damage);

            _enenyTarget.EnemyHealth.DealDamage(Damage);

            TurretOwner.ResetTurretProjectile();
            objectPooler.ReturnToPool(gameObject); //총알 맞으면 오브젝트 풀러로 되돌림
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        _enenyTarget = enemy;
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = _enenyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void ResetProjectile()
    {
        _enenyTarget = null;
        transform.rotation = Quaternion.identity;
    }
}
