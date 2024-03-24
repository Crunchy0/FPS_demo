using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HandleCollectedItemsSystem))]
public sealed class HandleCollectedItemsSystem : UpdateSystem {
    private Request<CollectItem> _handleRequest;

    private Stash<Item> _itemStash;

    public override void OnAwake() {
        _handleRequest = World.GetRequest<CollectItem>();

        _itemStash = World.GetStash<Item>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var request in _handleRequest.Consume())
        {
            EntityId id = request.targetId;
            Entity entity;
            if (!World.TryGetEntity(id, out entity) || !_itemStash.Has(entity))
                continue;

            if(entity.Has<Body>())
            {
                Destroy(entity.GetComponent<Body>().transform.gameObject);
            }
        }
    }
}