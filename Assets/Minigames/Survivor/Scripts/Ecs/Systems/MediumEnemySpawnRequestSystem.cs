using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Enemies;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class MediumEnemySpawnRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly MediumEnemySpawnConfig[] configs;

        private readonly EcsWorld world;
        private readonly EcsFilter<OneSecondTimerEvent, TimerExpiredEvent> oneSecondTimerFilter;
        private readonly EcsFilter<DifficultyComponent> difficultyFilter;
        private readonly EcsFilter<PlayerExpComponent> playerEpxFilter;
        private readonly EcsFilter<EnemySpawnRequest, TimerExpiredEvent> enemySpawnRequestFilter;

        private List<MediumEnemySpawnConfig> currentConfigs;

        public MediumEnemySpawnRequestSystem(EnemySpawnMasterConfig masterConfig)
        {
            configs = masterConfig.MediumEnemyConfigs;
        }

        public void Init()
        {
            currentConfigs = configs.ToList();
        }

        public void Run()
        {
            var playerLevel = playerEpxFilter.Get1(0).Level;

            for (var i = currentConfigs.Count - 1; i >= 0; i--)
            {
                var config = currentConfigs[i];

                if (playerLevel >= config.MinPlayerLevel)
                {
                    AddSpawnRequest(config);
                    currentConfigs.Remove(config);
                }
            }

            foreach (var i in enemySpawnRequestFilter)
            {
                var config = enemySpawnRequestFilter.Get1(i).Config;

                if (config is MediumEnemySpawnConfig)
                {
                    AddSpawnRequest(config);
                }
            }
        }

        private void AddSpawnRequest(EnemySpawnConfig config)
        {
            var entity = world.NewEntity();
            entity.Get<TimerComponent>().TimeLeft = 1f / difficultyFilter.Get1(0).TargetEnemySpawnRate;
            entity.Get<EnemySpawnRequest>().Config = config;
        }
    }
}
