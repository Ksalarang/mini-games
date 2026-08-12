using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemySpawnRequestDeleteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemySpawnRequest, TimerExpiredEvent> enemySpawnRequestFilter;

        public void Run()
        {
            foreach (var i in enemySpawnRequestFilter)
            {
                enemySpawnRequestFilter.GetEntity(i).Del<EnemySpawnRequest>();
            }
        }
    }
}
