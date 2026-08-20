using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProjectileTag, DamageComponent> projectileFilter;
        private readonly EcsFilter<ProjectileTag, TimerExpiredEvent> timerExpiredFilter;

        public void Run()
        {
            foreach (var i in projectileFilter)
            {
                if (projectileFilter.Get2(i).Value <= 0f)
                {
                    projectileFilter.GetEntity(i).Get<DestroyRequest>();
                }
            }

            foreach (var i in timerExpiredFilter)
            {
                timerExpiredFilter.GetEntity(i).Get<DestroyRequest>();
            }
        }
    }
}
