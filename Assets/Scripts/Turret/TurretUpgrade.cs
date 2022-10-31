using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    //업그레이드를 위한 초기 돈
    //업그레이드 할 때마다 필요한 돈 증가
    //업그레이드 할 때 증가되는 데미지

    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    public int UpgradeCost { get; set; }

    private TurretProjectile _turretProjectile;

    private void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTurret();
        }
    }

    private void UpgradeTurret()
    {
        if(MoneySystem.Instance.TotalCoins >= UpgradeCost)
        {
            _turretProjectile.Damage += damageIncremental;
            _turretProjectile.DelayPerShot -= delayReduce;

            UpdateUpgrade();
        }
    }

    private void UpdateUpgrade()
    {
        MoneySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
    }
}
