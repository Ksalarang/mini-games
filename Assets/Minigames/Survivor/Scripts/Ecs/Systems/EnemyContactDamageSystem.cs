using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemyContactDamageSystem : IEcsRunSystem
    {
        private readonly EnemyDamageConfig config;

        private readonly EcsWorld world;
        private readonly EcsFilter<CollisionEvent> collisionFilter;

        private readonly EcsEntity[] collisionPair = new EcsEntity[2];

        public EnemyContactDamageSystem(EnemyDamageConfig config)
        {
            this.config = config;
        }

        public void Run()
        {
            foreach (var i in collisionFilter)
            {
                var collision = collisionFilter.Get1(i);
                collisionPair[0] = collision.Entity1;
                collisionPair[1] = collision.Entity2;

                for (var j = 0; j < collisionPair.Length; j++)
                {
                    var entity1 = collisionPair[j];
                    var entity2 = collisionPair[(j + 1) % collisionPair.Length];

                    if (entity1.Has<EnemyTag>() && entity2.Has<PlayerTag>())
                    {
                        ref var timer = ref entity1.Get<TimerComponent>();

                        if (timer.TimeLeft <= 0f)
                        {
                            var data = config.Dict[entity1.Get<EnemyTag>().Id];

                            world.NewEntity().Get<DamageEvent>() = new DamageEvent
                            {
                                Value = data.Damage,
                                Target = entity2,
                            };

                            timer.TimeLeft = data.Interval;
                        }
                    }
                }
            }
        }
    }
}
