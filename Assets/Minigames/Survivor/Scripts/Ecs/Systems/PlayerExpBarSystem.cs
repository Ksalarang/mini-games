using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.UI;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerExpBarSystem : IEcsRunSystem
    {
        private readonly ProgressBarView expProgressBar;

        private readonly EcsFilter<PlayerExpComponent> filter;

        private int previousExp = -1;

        public PlayerExpBarSystem(UiContainer uiContainer)
        {
            expProgressBar = uiContainer.ExpProgressBar;
        }

        public void Run()
        {
            var playerExp = filter.Get1(0);
            var current = playerExp.CurrentValue;

            if (current == previousExp)
            {
                return;
            }

            previousExp = current;

            var progress = Mathf.Clamp01((float)playerExp.CurrentValue / playerExp.NextLevelValue);
            expProgressBar.SetProgress(progress);
        }
    }
}
