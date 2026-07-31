using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ExpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionEvent> collisionFilter;

        private readonly EcsEntity[] collisionPair = new EcsEntity[2];

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

                    if (entity1.Has<PlayerTag>() && entity2.Has<ExpItemComponent>())
                    {
                        ref var playerExp = ref entity1.Get<PlayerExpComponent>();
                        playerExp.CurrentValue += entity2.Get<ExpItemComponent>().Value;

                        entity2.Get<DestroyRequest>();
                        break;
                    }
                }
            }
        }
    }
}
