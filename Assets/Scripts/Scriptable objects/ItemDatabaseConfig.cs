using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Item Db Config", fileName ="New Item Db Config")]
public class ItemDatabaseConfig : ScriptableObject
{
    [SerializeField] private List<ItemConfig> _itemConfigs;

    public List<ItemConfig> ItemConfigs { get => _itemConfigs; }
}
