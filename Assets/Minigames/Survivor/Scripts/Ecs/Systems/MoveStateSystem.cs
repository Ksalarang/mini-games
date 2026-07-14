using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class MoveStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveStateComponent, Velocity> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var state = ref filter.Get1(i);
                var velocity = filter.Get2(i);

                if (velocity.Value.x != 0 || velocity.Value.y != 0)
                {
                    state.PreviousValue = state.CurrentValue;
                    state.CurrentValue = MoveState.Run;
                }
                else
                {
                    state.PreviousValue = state.CurrentValue;
                    state.CurrentValue = MoveState.Idle;
                }
            }
        }
    }
}
