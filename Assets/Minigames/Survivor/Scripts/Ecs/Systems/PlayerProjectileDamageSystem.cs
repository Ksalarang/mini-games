using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerProjectileDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<CollisionEvent, OrientedBoxCollisionEvent> filter;

        private readonly EcsEntity[] collisionPair = new EcsEntity[2];

        public void Run()
        {
            foreach (var i in filter)
            {
                var collision = filter.Get1(i);
                collisionPair[0] = collision.Entity1;
                collisionPair[1] = collision.Entity2;

                for (var j = 0; j < collisionPair.Length; j++)
                {
                    var entity1 = collisionPair[j];
                    var entity2 = collisionPair[(j + 1) % collisionPair.Length];

                    if (entity1.Has<ProjectileTag>() && entity2.Has<EnemyTag>())
                    {
                        entity1.Get<ProjectileTag>().Destroy = true;
                        ref var damageEvent = ref entity2.Get<DamageEvent>();
                        damageEvent.Value = entity1.Get<DamageComponent>().Value;
                        damageEvent.Target = entity2;
                    }
                }
            }
        }
    }
}
