using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDBObj : ScriptableObject
{
    public ItemObj[] itemObjs;
    public void OnValidate()
    {
        for (int i = 0; i < itemObjs.Length; i++)
        {
            itemObjs[i].itemData.item_id = i;
        }
    }
}