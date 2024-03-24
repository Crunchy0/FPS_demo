using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(RegenerationSystem))]
public sealed class RegenerationSystem : FixedUpdateSystem {
    private Filter _filter;

    public override void OnAwake() {
        _filter = World.Filter.With<Health>().With<Regeneration>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in _filter)
        {
            ref Health health = ref entity.GetComponent<Health>();

            float regenSum = RegenProcessing(entity, deltaTime);

            if (regenSum > 0 && health.value < health.max)
                health.value = math.min(health.max, health.value + regenSum);
        }
    }

    private float RegenProcessing(Entity entity, float deltaTime)
    {
        ref Regeneration regen = ref entity.GetComponent<Regeneration>();

        float sum = 0f;
        int idx = 0;
        while (idx < regen.effects.Count)
        {
            if (regen.effects[idx].Timer.IsExpired)
            {
                regen.effects.RemoveAt(idx);
                continue;
            }

            float prevTimeLeft = regen.effects[idx].Timer.TimeLeft;
            regen.effects[idx].Timer.Update(deltaTime);
            float scale = prevTimeLeft - regen.effects[idx].Timer.TimeLeft;

            sum += regen.effects[idx].Value * scale;
            idx++;
        }

        if (regen.effects.Count == 0)
        {
            entity.RemoveComponent<Regeneration>();
        }

        return sum;
    }
}