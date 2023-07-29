using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected float delayBtwAttacks = 2.0f;
    [SerializeField] protected float damage = 2f;
   
    public float Damage { get; set; }
    public float DelayPerShot { get; set; }


    protected float _nextAttackTime;

    protected objectPooler _pooler;

    protected Projectile _currentProjectileLoaded;
    protected Turret _turret;

    private void Start()
    {
        _pooler = GetComponent<objectPooler>();
        _turret = GetComponent<Turret>();

        Damage = damage;
        DelayPerShot = delayBtwAttacks;

        LoadProjectile();
    }

    protected virtual void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if(Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null
            && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }


    protected virtual void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this; //총알 만든 타워를 저장
        _currentProjectileLoaded.ResetProjectile();

        _currentProjectileLoaded.Damage = Damage;

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
