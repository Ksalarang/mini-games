using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PayloadProjectileDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<PayloadProjectileTag, TimerExpiredEvent> filter;
        private readonly EcsFilter<EnemyTag, Position> enemyFilter;

        public void Run()
        {
            foreach (var i in filter)
            {
                var projectile = filter.GetEntity(i);
                var damage = projectile.Get<DamageComponent>().Value;
                var projectilePosition = projectile.Get<Position>().Value;
                var impactRadius = projectile.Get<ImpactRadiusComponent>().Value;
                var squaredRadius = impactRadius * impactRadius;

                foreach (var j in enemyFilter)
                {
                    var enemyPosition = enemyFilter.Get2(j).Value;

                    if (MathTools.GetSquaredDistance(projectilePosition, enemyPosition) <= squaredRadius)
                    {
                        world.NewEntity().Get<DamageEvent>() = new DamageEvent
                        {
                            Value = damage,
                            Target = enemyFilter.GetEntity(j),
                        };
                    }
                }
            }
        }
    }
}
