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
    private Event<PlayerInteractionEvent> _interact;
    private Event<ChooseEquipmentEvent> _chooseEquipment;

    private Filter _mouseReceivers;
    private Filter _keyboardReceivers;

    [Inject] private PlayerControls _controls;

    public override void OnAwake() {
        _interact = World.GetEvent<PlayerInteractionEvent>();
        _chooseEquipment = World.GetEvent<ChooseEquipmentEvent>();

        _mouseReceivers = World.Filter.With<MouseDelta>().Build();
        _keyboardReceivers = World.Filter.With<RelativeDirection>().Build();

        _controls.Interaction.Interact.started += Interact;

        _controls.Interaction.ChooseEquipment.started += ChooseEquipment;
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
        _interact.NextFrame(new PlayerInteractionEvent { });
    }

    private void ChooseEquipment(InputAction.CallbackContext ctx)
    {
        ChooseEquipmentEvent chooseEvt = new() { code = ctx.control.name };
        _chooseEquipment.NextFrame(chooseEvt);
    }
}