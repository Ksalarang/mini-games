using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PayloadProjectileDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PayloadProjectileTag, TimerExpiredEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                filter.GetEntity(i).Get<DestroyRequest>();
            }
        }
    }
}
