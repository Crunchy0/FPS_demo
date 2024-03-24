using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueItemSlot
{
    public ItemInstance Item { get; private set; } = null;
    public bool IsItemSet { get => Item != null; }

    private ItemClass _allowedClasses;
    
    public UniqueItemSlot(ItemClass allowed)
    {
        _allowedClasses = allowed;
    }

    public bool ClassMatches(ItemClass iClass)
    {
        if ((iClass & _allowedClasses) == 0)
            return false;
        return true;
    }

    public bool SetItem(ItemInstance item)
    {
        if (item == null)
        {
            Item = null;
            return true;
        }

        bool setAllowed = item.Config.IsUnique && ClassMatches(item.Config.Class);
        if (setAllowed)
            Item = item;

        return setAllowed;
    }
}
