using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    //창고 관련 변수
    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    //장비인벤 관련 변수
    public List<InventoryItem> inven;
    public Dictionary<ItemDataEquipment, InventoryItem> invenDictionary;

    //장비칸 관련 변수
    public List<InventoryItem> equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> equipDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform _stashSlotParent;
    [SerializeField] private Transform _invenSlotParent;
    [SerializeField] private Transform _equipSlotParent;

    private ItemSlotUI[] _stashItemSlots;
    private ItemSlotUI[] _invenItemSlots;
    [SerializeField] private EquipSlotUI[] _equipItemSlots;

    private void Awake()
    {
        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        _stashItemSlots = _stashSlotParent.GetComponentsInChildren<ItemSlotUI>();

        inven = new List<InventoryItem>();
        invenDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();
        _invenItemSlots = _invenSlotParent.GetComponentsInChildren<ItemSlotUI>();

        equipment = new List<InventoryItem>();
        equipDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();
        _equipItemSlots = _equipSlotParent.GetComponentsInChildren<EquipSlotUI>();
    }

    private void Start()
    {
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        for(int i = 0; i < _stashItemSlots.Length; ++i)
        {
            _stashItemSlots[i].CleanUpSlot();
        }
        for (int i = 0; i < _invenItemSlots.Length; ++i)
        {
            _invenItemSlots[i].CleanUpSlot();
        }


        for (int i = 0; i < stash.Count; ++i)
        {
            _stashItemSlots[i].UpdateSlot(stash[i]);
        }
        for (int i = 0; i < inven.Count; ++i)
        {
            _invenItemSlots[i].UpdateSlot(inven[i]);
        }

        //여기서 장비 슬롯을 보유한 장비에 따라서 갱신해주면 된다.
        for(int i = 0; i < _equipItemSlots.Length; ++i)
        {
            //equipmentdic 해당 슬롯타입과 일치하는 애가 존재하면 개를 Update를 해주면 되는거
            ItemDataEquipment slotEquip = equipDictionary.Keys.ToList().Find(
                x => x.equipmentType == _equipItemSlots[i].slotType);

            if(slotEquip != null)
            {
                _equipItemSlots[i].UpdateSlot(equipDictionary[slotEquip]);
            }
        }
    }

    public void AddItem(ItemData item)
    {
        if(item.itemType == ItemType.Equipment)
        {
            AddToInventory(item);
        }else if(item.itemType == ItemType.Material)
        {
            AddToStash(item);
        }

        UpdateSlotUI();
    }

    private void AddToInventory(ItemData item)
    {
        ItemDataEquipment equipItem = item as ItemDataEquipment;
        if (invenDictionary.TryGetValue(equipItem, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inven.Add(newItem);
            invenDictionary.Add(equipItem, newItem);
        }
    }

    private void AddToStash(ItemData item)
    {
        if(stashDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            stash.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item, int count = 1)
    {
        //장비인지
        if (invenDictionary.TryGetValue(item as ItemDataEquipment, out InventoryItem value))
        {
            if (value.stackSize <= count)
            {
                inven.Remove(value);
                invenDictionary.Remove(item as ItemDataEquipment);
            }
            else
            {
                value.RemoveStack(count);
            }
            return;
        }

        //재료인지
        if (stashDictionary.TryGetValue(item, out InventoryItem stashValue))
        {
            if(stashValue.stackSize <= count)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(item);
            }
            else
            {
                stashValue.RemoveStack(count);
            }
        }

        UpdateSlotUI();
    }

    public void EquipItem(ItemData item)
    {
        ItemDataEquipment equipItem = item as ItemDataEquipment;
        if(equipItem == null) {
            Debug.Log("Can't equip!");
            return;
        }


        InventoryItem newItem = new InventoryItem(equipItem);
        //여기에 기존에 해당 슬롯에 장착중인템이 있으면 걔를 다시 인벤토리로 돌려보내줘야 해.
        // equipItem의 슬롯에 해당하는 곳이 장착되어 있는지를 체크해서 
        //있다면 UnEquip 시키고 장착
        ItemDataEquipment oldItem = null;
        foreach(var equipPair in equipDictionary)
        {
            if(equipPair.Key.equipmentType == equipItem.equipmentType)
            {
                oldItem = equipPair.Key;
                break;
            }
        }

        if(oldItem != null) { 
            UnEquipItem(oldItem);
        }


        equipment.Add(newItem);
        equipDictionary.Add(equipItem, newItem);

        equipItem.AddModifiers(); //장비 장착으로 인한 스텟이 증가된다.

        RemoveItem(item); //장비칸으로 옮겨갔으면 인벤에서는 빼준다.
        


        UpdateSlotUI();
    }

    public void UnEquipItem(ItemDataEquipment oldEquipItem, bool backToInventory = true)
    {
        if (oldEquipItem == null) return;

        if(equipDictionary.TryGetValue(oldEquipItem, out InventoryItem newItem))
        {
            equipment.Remove(newItem); //장비 해제
            equipDictionary.Remove(oldEquipItem);
            oldEquipItem.RemoveModifiers(); //장착해제된 아이템의 능력치가 사라진다.

            if(backToInventory)
            {
                AddItem(oldEquipItem);
            }
        }
    }
}
