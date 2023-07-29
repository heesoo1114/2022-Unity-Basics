using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Inventory : MonoBehaviour
{
    public InventorySlot[] inventorySlots = new InventorySlot[24];

    public void Clear()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.DestoryItem();
        }
    }

    public bool GetFlagHave(ItemObj itemobj)
    {
        return GetFlagHave(itemobj.itemData.item_id);
    }

    public bool GetFlagHave(int id)
    {
        return inventorySlots.FirstOrDefault(i => i.itemType.item_id == id) != null;
    }
}
