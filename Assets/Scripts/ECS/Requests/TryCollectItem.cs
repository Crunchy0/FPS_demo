using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scellecs.Morpeh;

public struct TryCollectItem: IRequestData
{
    public EntityId targetId;
    public ItemInstance instance;
}
