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
    public ItemConfig ItemConfig { get; }
    public int Capacity { get; }
    public int Amount { get; private set; } = 0;

    public ItemStack(ItemConfig config, int capacity)
    {
        ItemConfig = config;
        Capacity = Mathf.Max(1, capacity);
    }

    public bool AddItem(ItemInstance item)
    {
        if (item.Id != ItemConfig.Id)
            return false;

        int added = Mathf.Min(item.Amount, Capacity - Amount);
        if (added < 1)
            return false;

        Amount += added;
        return true;
    }

    public bool RetrieveItem(int amount, out ItemInstance item)
    {
        item = null;
        int retrieved = Mathf.Min(amount, Amount);
        if (retrieved < 1)
            return false;

        Amount -= retrieved;
        item = new ItemInstance(ItemConfig);
        item.Amount = retrieved;
        return true;
    }
}
