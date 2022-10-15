using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private float delayBtwAttacks = 2.0f;

    private float _nextAttackTime;

    private ObjPooler _pooler;

    private Projectile _currentProjectileLoaded;
    private Turret _turret;

    private void Start()
    {
        _pooler = GetComponent<ObjPooler>();
        _turret = GetComponent<Turret>();

        LoadProjectile();
    }

    private void Update()
    {
        if(IsTurretEmpty())
        {
            LoadProjectile();
        }

        if(Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHelath > 0f)
            {
            _currentProjectileLoaded.transform.parent = null;
            _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + delayBtwAttacks;
        }

    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this; // 총알 만든 타워를 저장
        _currentProjectileLoaded.ResetProjectile();

        newInstance.SetActive(true);
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }

    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }
}
