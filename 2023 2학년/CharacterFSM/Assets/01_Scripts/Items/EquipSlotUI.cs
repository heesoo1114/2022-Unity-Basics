using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotUI : ItemSlotUI
{
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = $"Equip Slot[{slotType.ToString()}]";        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
                
    }

}
