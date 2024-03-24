using Scellecs.Morpeh.Providers;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class RegenerationProvider : MonoProvider<Regeneration> {

    [SerializeField] private float _time = 5f;
    [SerializeField] private float _rate = 3f;

    protected override void Initialize()
    {
        ref Regeneration component = ref GetData();
        component.effects = new List<Effect> { new Effect(_time, _rate) };
    }
}