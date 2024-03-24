using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InventoryManagerSystem))]
public sealed class InventoryManagerSystem : UpdateSystem {
    private Request<TryCollectItem> _tryCollect;
    private Request<TryEquipItem> _tryEquip;
    private Request<TryDropItem> _tryDrop;

    private Request<CollectItem> _collect;
    private Request<EquipItem> _equip;
    private Request<DropItem> _drop;

    [Inject] private Inventory _inventory;

    public override void OnAwake()
    {
        _tryCollect = World.GetRequest<TryCollectItem>();
        _tryEquip = World.GetRequest<TryEquipItem>();
        _tryDrop = World.GetRequest<TryDropItem>();

        _collect = World.GetRequest<CollectItem>();
        _equip = World.GetRequest<EquipItem>();
        _drop = World.GetRequest<DropItem>();

        _inventory.OnCollect += HandleCollectedItem;
        _inventory.OnEquip += EquipItem;
        _inventory.OnDrop += DropItem;
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(var request in _tryCollect.Consume())
        {
            _inventory.AddItem(request);
        }
    }

    private void HandleCollectedItem(EntityId id)
    {
        _collect.Publish(new CollectItem { targetId = id });
    }

    private void EquipItem(ItemInstance item, int idx)
    {
        _equip.Publish(new EquipItem { inventoryIndex = idx, item = item });
    }

    private void DropItem(ItemInstance item)
    {
        _drop.Publish(new DropItem { item = item });
    }
}