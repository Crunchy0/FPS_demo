using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollectItemsSystem))]
public sealed class CollectItemsSystem : UpdateSystem {
    private Request<TryCollectItem> _tryCollect;

    private Stash<Item> _itemStash;

    [Inject] private Inventory _inventory;

    public override void OnAwake()
    {
        _tryCollect = World.GetRequest<TryCollectItem>();

        _itemStash = World.GetStash<Item>();

        _inventory.OnCollect += HandleCollectedItem;
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
        Entity entity;
        if (!World.TryGetEntity(id, out entity) || !_itemStash.Has(entity))
            return;

        if (entity.Has<Body>())
        {
            Destroy(entity.GetComponent<Body>().transform.gameObject);
        }
    }
}