using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    private Node _currentNodeSelected;

    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    public void CloseTurretShopPanel()
    {
        turretShopPanel.SetActive(false);
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.SetActive(true);
        }
        else
        {
            ShowNodeUI();
        }
    }

    private void ShowNodeUI()
    {
        nodeUIPanel.SetActive(true);
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();

        UpdateUpgradeText();
        UpdateTurretLevel();
        
    }

    public void UpgradeTurret()
    {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();

        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }

    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }

    public void SellTurret()
    {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }
}
