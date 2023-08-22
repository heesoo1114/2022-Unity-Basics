using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;

    private Image constructionImage;
    private void Awake()
    {
        constructionImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        constructionImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
