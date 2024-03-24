using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class InputProvider : MonoProvider<Input> {
    private World _world;

    protected override void Initialize()
    {
        _world = World.Default;

        PlayerControls controls = new PlayerControls();

        ref Input input = ref GetData();
        input.controls = controls;

        controls.Enable();
    }

    private void SetupCallbacks()
    {
        // Buttons callbacks
    }
}