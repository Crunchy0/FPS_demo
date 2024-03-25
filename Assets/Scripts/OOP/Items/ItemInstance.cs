using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemConfig Config { get; }
    public int Amount { get; set; } = 1;

    private IItemState _state;

    public ItemInstance(ItemConfig config, IItemState state = null)
    {
        Config = config;
        _state = state;
    }

    public bool GetState<T>(out T state)
    {
        state = default(T);
        if (_state == null || _state.GetType() != typeof(T))
            return false;

        state = (T)_state;
        return true;
    }
}
