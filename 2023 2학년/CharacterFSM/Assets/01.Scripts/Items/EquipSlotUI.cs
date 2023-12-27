using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//이건 장비칸 슬롯이야.
public class EquipSlotUI : ItemSlotUI
{
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = $"Equip Slot[{slotType.ToString()}]";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //여기서 장착해제하는걸 해줘야겠지.
        Inventory.Instance.UnEquipItem(item.itemData as ItemDataEquipment);
        CleanUpSlot();
    }
}
