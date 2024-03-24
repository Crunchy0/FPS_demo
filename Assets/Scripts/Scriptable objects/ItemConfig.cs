using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Flags]
public enum ItemClass
{
    Melee = 1,
    Firearm = 1 << 1,
    Ammo = 1 << 2,
    Throwable = 1 << 3,
    Consumable = 1 << 4
}

public enum ItemId
{
    Pipe,
    Rifle,
    Pistol,
    RifleAmmo,
    PistolAmmo,
    Grenade,
    Medkit
}

[CreateAssetMenu(menuName = "Scriptable Objects/Item Config", fileName = "New Item Config")]
public class ItemConfig : ScriptableObject
{
    [Header("Logic")]
    [SerializeField] private ItemClass _class;
    [SerializeField] private ItemId _id;
    [SerializeField] private bool _isUnique;

    [Header("UI")]
    [SerializeField] private string _name;
    [SerializeField] private Image _preview;
    [SerializeField] private GameObject _itemPrefab, _equipmentPrefab;

    public ItemClass Class { get => _class; }
    public ItemId Id { get => _id; }
    public bool IsUnique { get => _isUnique; }
    public bool IsEquipment { get => _equipmentPrefab != null; }

    public string Name { get => _name; }
    public Image Preview { get => _preview; }
    public GameObject ItemPrefab { get => _itemPrefab; }
    public GameObject EquipmentPrefab { get => _equipmentPrefab; }

}
