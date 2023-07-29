using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public static class MouseTransformData
{
    //ui 인벤토리에 마우스가 위치해 있는가?
    public static UIInventory mouseInventory;
    //마우스가 slot UI위에 있는가
    public static GameObject mouseSlot;
    //마우스가 현재 DragDrop 상태인가
    public static GameObject mouseDragging;
}

[RequireComponent(typeof(EventTrigger))]
public abstract class UIInventory : MonoBehaviour
{
    private InventoryObj inventroyObj;

    public Dictionary<GameObject, InventorySlot> uiSlotList = new Dictionary<GameObject, InventorySlot>();

    public abstract void CreateUISlot();

    protected void AddEventAction(GameObject gameObject, EventTriggerType eventTriggerType,
        UnityAction<BaseEventData> BaseEventDataAction)
    {
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (!eventTrigger)
        {
            Debug.Log("이벤트 트리거 없음");
            return;
        }

        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry
        {
            eventID = eventTriggerType
        };

        eventTriggerEntry.callback.AddListener(BaseEventDataAction);
    }

    public void OnEquipUpdate(InventorySlot inventorySlot)
    {
        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().sprite
            = inventorySlot.itemType.item_id < 0 ? null : inventorySlot.itemObj.itemIcon;

        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().color
            = inventorySlot.itemType.item_id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);

        inventorySlot.slotUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
            = inventorySlot.itemType.item_id < 0 ? string.Empty : inventorySlot.itemCnt.ToString("n0");
    }
    private GameObject AddEventDragImage(GameObject gameObject)
    {
        if (uiSlotList[gameObject].itemType.item_id < 0)
            return null;

        GameObject imgDrag = new GameObject();

        RectTransform rectTransform = imgDrag.AddComponent<RectTransform>();

        imgDrag.transform.SetParent(transform.parent);

        Image image = imgDrag.GetComponent<Image>();
        image.sprite = uiSlotList[gameObject].itemObj.itemIcon;

        imgDrag.name = "Drag Image";
        return imgDrag;
    }

    public void OnEnterInventory(GameObject gameObject)
    {
        MouseTransformData.mouseInventory = gameObject.GetComponent<UIInventory>();
    }

    public void OnEnterSlots(GameObject gameObject)
    {
        MouseTransformData.mouseSlot = gameObject;
        MouseTransformData.mouseInventory = gameObject.GetComponentInParent<UIInventory>();
    }

    public void OnStartDrag(GameObject gameObject)
    {
        MouseTransformData.mouseDragging = AddEventDragImage(gameObject);
    }

    public void OnMovingDrag(GameObject gameObject)
    {
        Destroy(MouseTransformData.mouseDragging);

        if (MouseTransformData.mouseInventory == null)
        {
            uiSlotList[gameObject].DestoryItem();
        }
        else
        {
            InventorySlot mouseHoverSlotData = MouseTransformData.mouseInventory.uiSlotList[MouseTransformData.mouseSlot];
            inventroyObj.SwapItems(uiSlotList[gameObject], mouseHoverSlotData);
        }
    }

    public void OnExitSlots(GameObject gameObject)
    {
        MouseTransformData.mouseSlot = null;
    }

    public void OnExitInventory(GameObject gameObject)
    {
        MouseTransformData.mouseInventory = null;
    }
}