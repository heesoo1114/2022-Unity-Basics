using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingContruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingContruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.Setup(buildingType.constructionTimerMax, buildingType);

        return buildingConstruction;
    }

    private BoxCollider2D boxColllider2d;
    private float constructionTimer;
    private float constructionTimerMax;
    private BuildingTypeSO buildingType;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;


    private void Awake()
    {
        boxColllider2d = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (constructionTimer <= 0f)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Setup(float constructionTimer, BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;
        this.constructionTimer = constructionTimer;
        constructionTimerMax = constructionTimer;

        boxColllider2d.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        boxColllider2d.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;

        spriteRenderer.sprite = buildingType.sprite;
        buildingTypeHolder.buildingType = buildingType;

;    }

    public float GetConstructionTimerNormalized()
    {
        return 1-constructionTimer / constructionTimerMax;
    }
}
