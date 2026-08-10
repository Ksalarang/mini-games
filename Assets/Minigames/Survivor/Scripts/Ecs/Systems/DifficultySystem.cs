using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class DifficultySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly DifficultyConfig config;

        private readonly EcsWorld world;
        private readonly EcsFilter<DifficultyComponent> difficultyFilter;
        private readonly EcsFilter<SessionTimeComponent> sessionTimeFilter;
        //todo: do not cache entities and components

        public DifficultySystem(DifficultyConfig config)
        {
            this.config = config;
        }

        public void Init()
        {
            world.NewEntity().Get<DifficultyComponent>().TargetEnemySpawnRate = config.InitialEnemySpanRate;
        }

        public void Run()
        {
            var progress = (float)sessionTimeFilter.Get1(0).Value.TotalSeconds / config.SessionDurationSeconds;
            var targetEnemyCount = progress * config.MaxEnemyCount;
            difficultyFilter.Get1(0).TargetEnemySpawnRate = targetEnemyCount;
        }
    }
}
