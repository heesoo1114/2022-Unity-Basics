using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;
        _itemImage.color = Color.white;

        if(item != null )
        {
            _itemImage.sprite = item.itemData.itemIcon;

            if(item.stackSize > 1 )
            {
                _itemAmountText.text = item.stackSize.ToString();
            }
            else
            {
                _itemAmountText.text = string.Empty;
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        _itemImage.sprite = null;
        _itemImage.color = Color.clear;
        _itemAmountText.text = string.Empty;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null) return;

        if(item.itemData.itemType == ItemType.Equipment)
        {
            Inventory.Instance.EquipItem(item.itemData);
            return;
        }

        Inventory.Instance.RemoveItem(item.itemData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;
        string desc = item.itemData.GetDescription();
        if( !string.IsNullOrEmpty( desc) )
        {
            Debug.Log(desc);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
