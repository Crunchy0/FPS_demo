using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scellecs.Morpeh;
using VContainer;

public class Inventory
{
    public event Action<EntityId> OnCollect;
    public event Action<ItemInstance, int> OnEquip;
    public event Action<ItemInstance> OnDrop;

    public int OverallSlots { get => _uniqueItems.Count + _collectables.Count; }

    private List<UniqueItemSlot> _uniqueItems = new List<UniqueItemSlot>();
    private List<ItemStack> _collectables = new List<ItemStack>();

    private InventoryConfig _invConfig;
    private ItemDatabase _itemDb;

    public Inventory(InventoryConfig invConfig, ItemDatabase itemDb)
    {
        _invConfig = invConfig;
        _itemDb = itemDb;

        foreach (var setup in _invConfig.Stacks)
        {
            ItemConfig cfg;
            if (!_itemDb.GetItemConfig(setup.itemId, out cfg))
            {
                Debug.LogError($"Unable to find item in the database ({setup.itemId})");
                continue;
            }
            if (cfg.IsUnique)
                Debug.LogWarning("Creating a stack for a unique item type");
            _collectables.Add(new ItemStack(cfg, setup.capacity));
        }
        foreach (var mask in _invConfig.UniqueSlots)
        {
            _uniqueItems.Add(new UniqueItemSlot(mask));
        }
    }

    public void AddItem(TryCollectItem request)
    {
        int idx = -1;
        bool success = false;

        ItemConfig cfg = request.instance.Config;

        if (cfg.IsUnique)
        {
            for(int i = 0; i < _uniqueItems.Count; i++)
            {
                if (!_uniqueItems[i].IsItemSet && _uniqueItems[i].SetItem(request.instance))
                {
                    idx = i;
                    success = true;
                    break;
                }
            }
        }
        else
        {
            for(int i = 0; i < _collectables.Count; i++)
            {
                ItemStack cur = _collectables[i];
                if (cur.Item.Config.Id != cfg.Id)
                    continue;

                if (cur.AddItem(request.instance))
                {
                    idx = _uniqueItems.Count + i;
                    success = true;
                }
                break;
            }
        }

        if(success)
        {
            Debug.Log($"{cfg.Name} (up to {request.instance.Amount}) added to inventory!");
            OnCollect?.Invoke(request.targetId);
        }
    }

    public void RemoveItem(int idx, int amount = 1)
    {
        if (idx < 0 || idx >= OverallSlots || amount < 1)
            return;

        if (idx < _uniqueItems.Count)
            _uniqueItems[idx].SetItem(null);
        else
            _collectables[idx - _uniqueItems.Count].RetrieveItem(amount, out var _);
    }

    public int ResolveFirstIndex(ItemId id)
    {
        for (int i = 0;  i < _uniqueItems.Count; i++)
        {
            if (_uniqueItems[i].IsItemSet && id == _uniqueItems[i].Item.Config.Id)
            {
                return i;
            }
        }

        for (int i = 0; i < _collectables.Count; i++)
        {
            if (id == _collectables[i].Item.Config.Id)
            {
                return _uniqueItems.Count + i;
            }
        }

        return -1;
    }

    public void EquipItem(int idx)
    {
        if (idx < 0 || idx >= OverallSlots)
            return;

        ItemInstance item;
        if (idx < _uniqueItems.Count)
            item = _uniqueItems[idx].Item;
        else
        {
            item = _collectables[idx - _uniqueItems.Count].Item;
        }

        if (item == null || !item.Config.IsEquipment || item.Amount < 1)
            return;

        OnEquip?.Invoke(item, idx);
    }

    public void DropUniqueItem(int idx)
    {
        if (idx < 0 || idx >= _uniqueItems.Count)
            return;

        ItemConfig config = _uniqueItems[idx].Item.Config;
        if (config == null)
            return;

        _uniqueItems[idx].SetItem(null);

        OnDrop?.Invoke(_uniqueItems[idx].Item);
    }
}
