using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using System;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EquipItemsSystem))]
public sealed class EquipItemsSystem : UpdateSystem {
    private Event<ChooseEquipmentEvent> _chooseEquipment;

    [Inject] private Inventory _inventory;

    public override void OnAwake() {
        _chooseEquipment = World.GetEvent<ChooseEquipmentEvent>();

        _inventory.OnEquip += EquipItem;
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var evt in _chooseEquipment.publishedChanges)
        {
            int idx = -1;

            switch(evt.code)
            {
                case "1":
                case "2":
                case "3":
                    idx = Int32.Parse(evt.code) - 1;
                    break;
                case "g":
                    idx = _inventory.ResolveFirstIndex(ItemId.Grenade);
                    break;
                default:
                    break;
            }

            if (idx != -1)
                _inventory.EquipItem(idx);
        }
    }

    private void EquipItem(ItemInstance item, int idx)
    {
        Debug.Log($"Equipping {item.Config.Name} (x{item.Amount})");
    }
}