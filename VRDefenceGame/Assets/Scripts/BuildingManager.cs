using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Building hqBuilding;

    public static BuildingManager Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs: EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Start()
    {
        hqBuilding.GetComponent<HealthSystem>().OnDied += BuildingManager_OnDied;
    }

    private void BuildingManager_OnDied(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if(CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResoureCostArray))
                    {
                        ResourceManager.Instance.SpendResource(activeBuildingType.constructionResoureCostArray);
                        //Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Can not Afford", new TooltipUI.TooltipTimer { timer = 2f });
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });   
                }
                
            }  
        } 
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset, boxCollider2D.size, 0f);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        } 

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingType == buildingTypeHolder.buildingType)
                {
                    errorMessage = "Too close to another building of ths same type!";
                    return false;
                }
            }
        }

        if (buildingType.hasResourceGenerator)
        {
            ResourceGeneratorData resourceGenerator = buildingType.resourceGeneratorData;
            int nearbyResourAmount = ResourceGenerator.GetNearbyResourceAmount(position, resourceGenerator);
            if (nearbyResourAmount == 0)
            {
                errorMessage = "No ResourceNode";
                return false;
            }
        }


        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "Too far from any other building!";
        return false;
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
