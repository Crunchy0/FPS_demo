using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStateFactory
{
    //private FirearmItemStateFactory _firearmStateFactory;

    public ItemStateFactory(/*FirearmItemStateFactory firearmStateF*/)
    {
        //_firearmStateFactory = firearmStateF;
    }

    public IItemState Create(ItemClass itemClass)
    {
        switch (itemClass)
        {
            case ItemClass.Firearm:
                return new FirearmItemState();
            case ItemClass.Melee:
                break;
            case ItemClass.Ammo:
                break;
            case ItemClass.Throwable:
                break;
            case ItemClass.Consumable:
                break;
        }
        return null;
    }
}
