using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurretCard : MonoBehaviour
{
    public static Action<TurretSettings> OnPlaceTurret;

    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public TurretSettings TurretLoaded { get; set; }

    public void SetUpTurretButton(TurretSettings turretSettings)
    {
        TurretLoaded = turretSettings;

        turretImage.sprite = turretSettings.TurretShopSprite;
        turretCost.text = turretSettings.TurretShopCost.ToString();
    }

    public void PlaceTurret()
    {
        if (MoneySystem.Instance.TotalCoins >= TurretLoaded.TurretShopCost)
        {
            MoneySystem.Instance.RemoveCoins(TurretLoaded.TurretShopCost);

            UIManager.Instance.CloseTurretShopPanel();

            OnPlaceTurret?.Invoke(TurretLoaded);
        }
    }
}
