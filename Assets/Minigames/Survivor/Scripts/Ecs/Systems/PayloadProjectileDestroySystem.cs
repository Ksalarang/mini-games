using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PayloadProjectileDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PayloadProjectileTag, TimerExpiredEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                var entity = filter.GetEntity(i);
                Object.Destroy(entity.Get<GameObjectComponent>().Value);
                entity.Destroy();
            }
        }
    }
}
