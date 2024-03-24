using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInteractionSystem))]
public sealed class PlayerInteractionSystem : UpdateSystem {
    private Request<TryCollectItem> _collectRequest;

    private Event<PlayerInteractionEvent> _interactionEvent;

    private Filter _interactionFilter;
    [Inject] private ItemDatabase _itemDb;

    public override void OnAwake() {
        _collectRequest = World.GetRequest<TryCollectItem>();

        _interactionEvent = World.GetEvent<PlayerInteractionEvent>();

        _interactionFilter = World.Filter.With<PlayerInteraction>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        if (_interactionEvent.publishedChanges.length == 0)
            return;

        RaycastHit hit;
        foreach (var entity in _interactionFilter)
        {
            var interaction = entity.GetComponent<PlayerInteraction>();

            Vector3 origin = interaction.eye.position;
            Vector3 direction = interaction.eye.forward;
            float distance = interaction.reachDistance;

            if (!Physics.Raycast(origin, direction, out hit, distance))
                continue;

            HandleInteraction(hit.transform);
        }
    }

    private void HandleInteraction(Transform transform)
    {
        if (transform.TryGetComponent(out EntityProvider provider))
        {
            Entity entity = provider.Entity;
            if (entity.IsDisposed())
            {
                Debug.LogError($"Entity {entity.ID} is disposed!");
                return;
            }

            if(entity.Has<Item>())
            {
                Item item = entity.GetComponent<Item>();
                _collectRequest.Publish(new TryCollectItem
                {
                    targetId = entity.ID,
                    instance = item.instance
                });
            }
            
            //Debug.Log($"Interacting with item: {item.instance.Id}, amount: {item.instance.Amount}");
        }
    }
}