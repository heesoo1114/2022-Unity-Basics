using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//¿Ã∞« ¿Â∫Òƒ≠ ΩΩ∑‘¿Ãæﬂ.
public class EquipSlotUI : ItemSlotUI
{
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = $"Equip Slot[{slotType.ToString()}]";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.Instance.UnEquipItem(item.itemData as ItemDataEquipment);
        CleanUpSlot();
    }
}
