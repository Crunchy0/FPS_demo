using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[RequireComponent(typeof(Rigidbody))]
public sealed class BodyProvider : MonoProvider<Body> {
    protected override void Initialize()
    {
        ref Body component = ref GetData();
        component.transform = transform;
        component.rigidBody = GetComponent<Rigidbody>();
    }
}