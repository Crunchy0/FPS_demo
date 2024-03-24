using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    private Dictionary<ItemId, ItemConfig> _db = new Dictionary<ItemId, ItemConfig>();

    public ItemDatabase(ItemDatabaseConfig config)
    {
        foreach (var itemConfig in config.ItemConfigs)
        {
            AddEntry(itemConfig);
        }
    }

    public bool GetItemConfig(ItemId id, out ItemConfig config)
    {
        return _db.TryGetValue(id, out config);
    }

    private void AddEntry(ItemConfig config)
    {
        if (_db.TryGetValue(config.Id, out var _))
            return;
        _db.Add(config.Id, config);
    }

    private void RemoveEntry(ItemId id)
    {
        _db.Remove(id);
    }
}
