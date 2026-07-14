using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class VelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Direction, Speed> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                var direction = filter.Get1(i);
                var speed = filter.Get2(i);
                ref var velocity = ref filter.GetEntity(i).Get<Velocity>();

                var value = direction.Value * (Time.deltaTime * speed.Value);
                velocity.Value = value;
            }
        }
    }
}
