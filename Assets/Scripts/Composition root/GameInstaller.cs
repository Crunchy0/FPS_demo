using TriInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [Required] [SerializeField] private InventoryConfig _inventoryConfig;
    [Required] [SerializeField] private ItemDatabaseConfig _itemDbConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerControls>(Lifetime.Singleton);

        builder.RegisterInstance(_itemDbConfig);
        builder.Register<ItemDatabase>(Lifetime.Singleton);

        builder.RegisterInstance(_inventoryConfig);
        builder.Register<Inventory>(Lifetime.Singleton);

        builder.Register<ItemStateFactory>(Lifetime.Singleton);
    }
}
