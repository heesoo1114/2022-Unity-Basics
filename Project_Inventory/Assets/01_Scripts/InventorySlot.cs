using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public ItemObjType[] itemObjTypes = new ItemObjType[0];

    [SerializeField]
    public InventoryObj inventoryObj;

    [NonSerialized]
    public GameObject slotUI;

    [NonSerialized]
    public Action<InventorySlot> OnPreuUpload;

    [NonSerialized]
    public Action<InventorySlot> OnPostUpload;

    public ItemType itemType;
    public int itemCnt;

    public ItemObj itemObj
    {
        get
        {
            return itemType.item_id >= 0 ? inventoryObj.itemDBObj.itemObjs[itemType.item_id] : null;
        }
    }

    public void UploadSlot(ItemType itemTypem, int cnt)
    {
        if (cnt <= 0)
        {
            itemType = new ItemType();
        }

        OnPostUpload?.Invoke(this);
        this.itemType = itemTypem;
        this.itemCnt = cnt;
        OnPostUpload?.Invoke(this);
    }

    public InventorySlot() => UploadSlot(new ItemType(), 0);
    public InventorySlot(ItemType itemType, int cnt) => UploadSlot(itemType, cnt);

    public void AddCnt(int value) => UploadSlot(itemType, itemCnt += value);
    public void DestoryItem() => UploadSlot(new ItemType(), 0);
    public bool GetFlagEquipSlot(ItemObj itemObj)
    {
        if (itemObjTypes.Length <= 0 || itemObj == null || itemObj.itemData.item_id < 0)
        {
            return true;
        }

        foreach (ItemObjType itemObjType in itemObjTypes)
        {
            if (itemObj.itemObjType == itemObjType)
            {
                return true;
            }
        }

        return false;
    }
}