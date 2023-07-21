using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InterfaceType
{
    Inventory,
    Equipment,
    QuickSlot,
    Box
}

[CreateAssetMenu(fileName = "New InventoryObject", menuName = "Inventory/InventoryObject")]
public class InventoryObj : MonoBehaviour
{
    public ItemDBObj itemDBObj;
    public InterfaceType type;

    [SerializeField]
    private Inventory inventory = new Inventory();
    public InventorySlot[] inventorySlots => inventory.inventorySlots;

    public int GetEmptySlotCnt
    {
        get
        {
            int cnt = 0;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemType.item_id <= -1)
                {
                    cnt++;
                }
            }
            return cnt;
        }
    }
}
