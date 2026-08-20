using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemyDestroySystem : IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<EnemyTag> enemyFilter;

        public void Run()
        {
            foreach (var i in enemyFilter)
            {
                var entity = enemyFilter.GetEntity(i);

                if (entity.Get<Health>().Value <= 0f)
                {
                    entity.Get<DestroyRequest>();
                }
            }
        }
    }
}
