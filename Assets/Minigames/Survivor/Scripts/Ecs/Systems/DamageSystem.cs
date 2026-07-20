using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DamageEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                var damage = filter.Get1(i);
                ref var health = ref damage.Target.Get<Health>();
                health.Value = Mathf.Max(0f, health.Value - damage.Value);
            }
        }
    }
}
