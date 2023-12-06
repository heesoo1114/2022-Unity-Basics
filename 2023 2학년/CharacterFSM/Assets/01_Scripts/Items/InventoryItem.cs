using System;

[Serializable]
public class InventoryItem 
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData itemData)
    {
        this.itemData = itemData;
        AddStack();
    }

    public void AddStack()
    {
        ++stackSize;
    }

    public void RemoveStack(int count = 1)
    {
        stackSize -= count;
    }
}
