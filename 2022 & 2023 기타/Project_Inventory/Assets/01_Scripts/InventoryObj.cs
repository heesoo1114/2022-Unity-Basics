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

    // ����� ������ �̺�Ʈ �߻�
    public Action<ItemObj> OnUseItemObj;

    // ������ �������� �ִ���
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

    // ������ �߰� �Լ�
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

    // slot ���� ������ ��ü
    public void SwapItems(InventorySlot itemA, InventorySlot itemB)
    {
        // ������ �������̸� ��ü�� �ʿ䰡 ����
        if (itemA == itemB)
        {
            return;
        }

        // ������ B��
    }
}
