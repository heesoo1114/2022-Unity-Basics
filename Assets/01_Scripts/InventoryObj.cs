using System;
using System.Linq;
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

    // 사용한 아이템 이벤트 발생
    public Action<ItemObj> OnUseItemObj;

    // 동일한 아이템이 있는지
    public InventorySlot SearchItemInInven(ItemType itemType)
    {
        return inventorySlots.FirstOrDefault(i => i.itemType.item_id == itemType.item_id);
    }

    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.FirstOrDefault(i => i.itemType.item_id <= -1);
    }

    public bool IsCotainItem(ItemObj itemObj)
    {
        return inventorySlots.FirstOrDefault(i => i.itemType.item_id == itemObj.itemData.item_id) != null;
    }

    // 아이템 추가 함수
    public bool AddItem(ItemType itemType, int amount)
    {
        InventorySlot inventorySlot = SearchItemInInven(itemType);

        if (itemDBObj.itemObjs[itemType.item_id].getFlagStackable || inventorySlot == null)
        {
            if (GetEmptySlotCnt <= 0)
                return false;

            GetEmptySlot().UploadSlot(itemType, amount);
        }
        else
        {
            inventorySlot.AddCnt(amount);
        }
        return true;
    }

    // slot 간에 아이템 교체
    public void SwapItems(InventorySlot itemA, InventorySlot itemB)
    {
        // 동일한 아이템이면 교체할 필요가 없다
        if (itemA == itemB)
        {
            return;
        }

        // 아이템 B와
    }
}
