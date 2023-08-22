using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
        HideDemolishBtn();
        HideRepairBtn();
    }
    private void Start()
    {

        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem.SetHealthAmountMax(buildingType.healAmountMax);

        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        ShowRepairBtn();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if (healthSystem.isFull())
        {
            HideRepairBtn();
        }
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowDemolishBtn();
    }
    private void OnMouseExit()
    {
        HideDemolishBtn();
    }

    private void ShowDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }
    private void HideDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }
    private void HideRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
