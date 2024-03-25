using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemStackSetup
{
    public ItemId itemId;
    public int capacity;
}

public class ItemStack
{
    public ItemInstance Item { get; }
    public int Capacity { get; }
    public int Amount { get => Item.Amount; }

    public ItemStack(ItemConfig config, int capacity)
    {
        Item = new ItemInstance(config);
        Item.Amount = 0;
        Capacity = Mathf.Max(1, capacity);
    }

    public bool AddItem(ItemInstance item)
    {
        if (item.Config.Id != Item.Config.Id)
            return false;

        int added = Mathf.Min(item.Amount, Capacity - Amount);
        if (added < 1)
            return false;

        Item.Amount += added;
        return true;
    }

    public bool RetrieveItem(int amount, out ItemInstance item)
    {
        item = null;
        int retrieved = Mathf.Min(amount, Amount);
        if (retrieved < 1)
            return false;

        Item.Amount -= retrieved;
        item = new ItemInstance(Item.Config);
        item.Amount = retrieved;
        return true;
    }
}
