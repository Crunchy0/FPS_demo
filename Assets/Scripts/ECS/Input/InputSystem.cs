using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem {
    private Event<PlayerInteractionEvent> _interactionEvent;

    private Filter _mouseReceivers;
    private Filter _keyboardReceivers;

    [Inject] private PlayerControls _controls;

    public override void OnAwake() {
        _interactionEvent = World.GetEvent<PlayerInteractionEvent>();

        _mouseReceivers = World.Filter.With<MouseDelta>().Build();
        _keyboardReceivers = World.Filter.With<RelativeDirection>().Build();

        _controls.Interaction.Interact.started += Interact;
        _controls.Enable();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var entity in _mouseReceivers)
        {
            ref var delta = ref entity.GetComponent<MouseDelta>();
            delta.value = _controls.PlayerMovement.TurnCamera.ReadValue<Vector2>();
        }
        foreach (var entity in _keyboardReceivers)
        {
            ref var relDir = ref entity.GetComponent<RelativeDirection>();
            relDir.value = _controls.PlayerMovement.Walk.ReadValue<Vector2>();
        }
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        _interactionEvent.NextFrame(new PlayerInteractionEvent { });
    }
}