using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Enemy _enemyTarget;

    [SerializeField] private float damage = 2f;
    [SerializeField] private float minDisToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }

    private void Update()
    {
        if(_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjetile();
        }
    }

    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);

        float disToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (disToTarget < minDisToDealDamage) // 적과 총알 거리는 충분히 가까움. 총알 맞음
        {
            _enemyTarget.EnemyHealth.DealDamage(damage);

            TurretOwner.ResetTurretProjectile();
            ObjPooler.ReturnToPool(gameObject);
        }
    }
     
    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

    private void RotateProjetile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void ResetProjectile()
    {
        _enemyTarget = null;
        transform.rotation = Quaternion.identity;
    }
}
