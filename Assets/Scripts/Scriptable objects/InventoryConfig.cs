using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Inventory Config", fileName ="New Inventory Config")]
public class InventoryConfig : ScriptableObject
{
    [SerializeField] private ItemStackSetup[] _stacks;
    [SerializeField] private ItemClass[] _uniqueSlots;

    public ItemStackSetup[] Stacks { get => _stacks; }
    public ItemClass[] UniqueSlots { get => _uniqueSlots; }
}
