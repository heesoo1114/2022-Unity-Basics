using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretProjectile : TurretProjectile
{
    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null 
                && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
            {
                FireProjectile(_turret.CurrentEnemyTarget);
                
            }
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile()
    {
    }

    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = _pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPos.position;

        Projectile projectile = instance.GetComponent<Projectile>();

        _currentProjectileLoaded = projectile;
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.SetEnemy(enemy);
        instance.SetActive(true);
    }
}
