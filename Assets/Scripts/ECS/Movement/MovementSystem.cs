using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : FixedUpdateSystem {
    private Filter _filter;
    public override void OnAwake()
    {
        _filter = World.Filter.With<Movement>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            MoveEntity(entity);
        }
    }

    private void MoveEntity(Entity entity)
    {
        if (!entity.Has<Body>())
            return;

        var movement = entity.GetComponent<Movement>();
        var body = entity.GetComponent<Body>();

        float straight = movement.relativeDirection.y;
        float sideways = movement.relativeDirection.x;
        float3 force = movement.speed * (straight * body.transform.forward + sideways * body.transform.right);

        body.rigidBody.AddForce(force, ForceMode.VelocityChange);

        float limitRelation = body.rigidBody.velocity.magnitude / movement.speed;
        if(limitRelation > 1)
        {
            float s = 1 / limitRelation;
            body.rigidBody.velocity *= s;
        }
    }
}