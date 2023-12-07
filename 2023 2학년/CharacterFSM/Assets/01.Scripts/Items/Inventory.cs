using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    //â�� ���� ����
    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    //����κ� ���� ����
    public List<InventoryItem> inven;
    public Dictionary<ItemDataEquipment, InventoryItem> invenDictionary;

    //���ĭ ���� ����
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

        //���⼭ ��� ������ ������ ��� ���� �������ָ� �ȴ�.
        for(int i = 0; i < _equipItemSlots.Length; ++i)
        {
            //equipmentdic �ش� ����Ÿ�԰� ��ġ�ϴ� �ְ� �����ϸ� ���� Update�� ���ָ� �Ǵ°�
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
        //�������
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

        //�������
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
        //���⿡ ������ �ش� ���Կ� ������������ ������ �¸� �ٽ� �κ��丮�� ����������� ��.
        // equipItem�� ���Կ� �ش��ϴ� ���� �����Ǿ� �ִ����� üũ�ؼ� 
        //�ִٸ� UnEquip ��Ű�� ����
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

        equipItem.AddModifiers(); //��� �������� ���� ������ �����ȴ�.

        RemoveItem(item); //���ĭ���� �Űܰ����� �κ������� ���ش�.
        


        UpdateSlotUI();
    }

    public void UnEquipItem(ItemDataEquipment oldEquipItem, bool backToInventory = true)
    {
        if (oldEquipItem == null) return;

        if(equipDictionary.TryGetValue(oldEquipItem, out InventoryItem newItem))
        {
            equipment.Remove(newItem); //��� ����
            equipDictionary.Remove(oldEquipItem);
            oldEquipItem.RemoveModifiers(); //���������� �������� �ɷ�ġ�� �������.

            if(backToInventory)
            {
                AddItem(oldEquipItem);
            }
        }
    }
}
