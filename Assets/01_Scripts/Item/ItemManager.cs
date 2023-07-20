using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        CreateNewItem("Health Potion", Resources.Load<Sprite>("HealthPotion"), 10);
        CreateNewItem("Mana Potion", Resources.Load<Sprite>("ManaPotion"), 10);
        CreateNewItem("Atk Potion", Resources.Load<Sprite>("AtkPotion"), 10);
    }
    
    private void CreateNewItem(string name, Sprite icon, int price)
    {
        // ItemData newItem = new ItemData(); // 클래스 인스턴스 생성
        ItemData newItem = ScriptableObject.CreateInstance<ItemData>(); // SO 인스턴스 생성
        newItem.itemName = name;
        newItem.itemIcon = icon;
        newItem.itemPrice = price;

        string assetPath = "Assets/" + name + ".asset";

        // UnityEdigor는 런타임에서는 실행 안 돼
        // + namespace 사용 중인 애들은 런타임에서는 실행 안 돼
        UnityEditor.AssetDatabase.CreateAsset(newItem, assetPath);
        UnityEditor.AssetDatabase.SaveAssets();

        Debug.Log("ScriptableObject 에셋으로 저장되었습니다. 경로: " + assetPath);
    }
}