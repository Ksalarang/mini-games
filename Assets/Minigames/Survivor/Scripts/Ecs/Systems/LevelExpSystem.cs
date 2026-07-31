using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class LevelExpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerExpComponent> filter;

        public void Run()
        {
            ref var playerExp = ref filter.Get1(0);
            var currentValue = playerExp.CurrentValue;
            var nextLevelValue = playerExp.NextLevelValue;

            if (currentValue > nextLevelValue)
            {
                playerExp.CurrentValue = 0;
                playerExp.NextLevelValue = (int)(nextLevelValue * 1.5f);
            }
        }
    }
}
