using System;
using UnityEditor.Search;
using UnityEngine;

[Serializable]
public class ItemType : MonoBehaviour
{
    public ItemAblility[] ability;

    public int item_id = -1;
    public string item_name = "";

    public ItemType()
    {
        item_id = -1;
        item_name = "";
    }

    public ItemType(ItemObj itemObj)
    {
        item_id = itemObj.itemData.item_id;
        item_name = itemObj.name;

        ability = new ItemAblility[itemObj.itemData.ability.Length];

        for (int i = 0; i < ability.Length; i++)
        {
            ability[i] = new ItemAblility(itemObj.itemData.ability[i].Min, itemObj.itemData.ability[i].Max)
            {
                characterStack = itemObj.itemData.ability[i].characterStack
            };
        }
    }
}
