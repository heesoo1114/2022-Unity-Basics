using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public bool hasResourceGenerator;

    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResoureCostArray;
    public int healAmountMax;

    public float constructionTimerMax;

    public string GetReouseCostString()
    {
        string str = "";
        foreach(ResourceAmount resourceAmount in constructionResoureCostArray)
        {
            str += "<color=#"+resourceAmount.resourceType.colorHex+">"
                +resourceAmount.resourceType.nameShort +resourceAmount.amount
                +"</color>";
        }
        return str;
    }
}
