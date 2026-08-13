using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.UI;
using TMPro;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SessionTimeLabelUpdateSystem : IEcsRunSystem
    {
        private readonly TMP_Text label;

        private readonly EcsFilter<OneSecondTimerEvent, TimerExpiredEvent> oneSecondTimerFilter;
        private readonly EcsFilter<SessionTimeComponent> sessionTimeFilter;

        public SessionTimeLabelUpdateSystem(UiContainer uiContainer)
        {
            label = uiContainer.RunClockLabel;
        }

        public void Run()
        {
            foreach (var i in oneSecondTimerFilter)
            {
                label.text = sessionTimeFilter.Get1(0).Value.ToString(@"mm\:ss");
            }
        }
    }
}
