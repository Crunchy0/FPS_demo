using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(ItemSetupSystem))]
public sealed class ItemSetupSystem : Initializer {
    [Inject] ItemDatabase _itemDb;
    [Inject] ItemStateFactory _itemStateFactory;

    public override void OnAwake() {
        Filter filter = World.Filter.With<ItemSetup>().Build();

        foreach(var entity in filter)
        {
            ItemConfig config;
            IItemState state = null;
            var setup = entity.GetComponent<ItemSetup>();

            if (!_itemDb.GetItemConfig(setup.itemId, out config))
            {
                entity.Dispose();
                //World.RemoveEntity(entity);
                Debug.LogError($"Unable to get config for {setup.itemId} in the database! Entity disposed");
                continue;
            }

            state = _itemStateFactory.Create(config.Class);

            ItemInstance instance = new ItemInstance(config, state);
            instance.Amount = math.max(1, setup.amount);

            entity.RemoveComponent<ItemSetup>();
            entity.SetComponent(new Item { instance = instance });
        }
    }

    public override void Dispose() {
    }
}