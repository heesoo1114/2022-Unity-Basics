using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    //���׷��̵带 ���� �ʱ� ��
    //���׷��̵� �� ������ �ʿ��� �� ����
    //���׷��̵� �� �� �����Ǵ� ������

    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    [Header("Sell")]
    [Range(0f,1f)]
    [SerializeField] private float sellPert;

    public float SellPert { get; set; }


    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    private TurretProjectile _turretProjectile;

    private void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;
        Level = 1;
        SellPert = sellPert;
    }

    public void UpgradeTurret()
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
        Level++;
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPert);
        return sellValue;
    }
}
