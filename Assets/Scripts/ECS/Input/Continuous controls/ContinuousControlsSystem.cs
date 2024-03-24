using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ContinuousControlsSystem))]
public sealed class ContinuousControlsSystem : UpdateSystem {
    private Filter _moveContr;
    private Filter _camContr;

    public override void OnAwake() {
        _moveContr = World.Filter.With<Movement>().With<RelativeDirection>().Build();
        _camContr = World.Filter.With<ActiveCamera>().With<MouseDelta>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var entity in _moveContr)
        {
            var relDir = entity.GetComponent<RelativeDirection>();
            ref var movement = ref entity.GetComponent<Movement>();

            movement.relativeDirection = relDir.value;
        }
        foreach(var entity in _camContr)
        {
            var delta = entity.GetComponent<MouseDelta>();
            ref var camera = ref entity.GetComponent<ActiveCamera>();

            camera.rot.x += delta.value.x;
            camera.rot.y -= delta.value.y;
        }
    }
}