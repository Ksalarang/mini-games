using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemyDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyTag> enemyFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        public void Run()
        {
            var playerPosition = playerFilter.GetEntity(0).Get<Position>();

            foreach (var i in enemyFilter)
            {
                var entity = enemyFilter.GetEntity(i);
                ref var direction = ref entity.Get<Direction>();
                var position = entity.Get<Position>();
                direction.Value = (playerPosition.Value - position.Value).normalized;
            }
        }
    }
}
