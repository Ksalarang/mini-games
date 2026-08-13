using Core.Tools.Structs;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Enemies;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SmallEnemySpawnRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SmallEnemySpawnConfig[] configs;

        private readonly EcsWorld world;
        private readonly EcsFilter<SmallEnemyReplaceRequest, TimerExpiredEvent> replaceRequestFilter;
        private readonly EcsFilter<DifficultyComponent> difficultyFilter;
        private readonly EcsFilter<EnemySpawnRequest, TimerExpiredEvent> enemySpawnRequestFilter;

        private SmallEnemySpawnConfig currentConfig;

        public SmallEnemySpawnRequestSystem(EnemySpawnMasterConfig masterConfig)
        {
            configs = masterConfig.SmallEnemyConfigs;
        }

        public void Init()
        {
            currentConfig = configs[0];
            AddSpawnRequest(currentConfig);
            AddReplaceRequest(currentConfig.ReplacePeriodSeconds);
        }

        public void Run()
        {
            foreach (var i in enemySpawnRequestFilter)
            {
                if (enemySpawnRequestFilter.Get1(i).Config is SmallEnemySpawnConfig)
                {
                    AddSpawnRequest(currentConfig);
                }
            }

            foreach (var i in replaceRequestFilter)
            {
                replaceRequestFilter.GetEntity(0).Del<SmallEnemyReplaceRequest>();
                currentConfig = configs[Random.Range(0, configs.Length)];
                AddReplaceRequest(currentConfig.ReplacePeriodSeconds);
            }
        }

        private void AddSpawnRequest(EnemySpawnConfig config)
        {
            var entity = world.NewEntity();
            entity.Get<TimerComponent>().TimeLeft = 1f / difficultyFilter.Get1(0).TargetEnemySpawnRate;
            entity.Get<EnemySpawnRequest>().Config = config;
        }

        private void AddReplaceRequest(IntRange range)
        {
            var entity = world.NewEntity();
            entity.Get<TimerComponent>().TimeLeft = Random.Range(range.Min, range.Max + 1);
            entity.Get<SmallEnemyReplaceRequest>();
        }
    }
}
