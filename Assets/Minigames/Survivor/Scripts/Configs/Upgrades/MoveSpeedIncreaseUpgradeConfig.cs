using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "MoveSpeedIncreaseUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/MoveSpeedIncreaseUpgradeConfig", order = 0)]
    public class MoveSpeedIncreaseUpgradeConfig : UpgradeConfig
    {
        public override void Apply(EcsEntity player)
        {
            ref var speed = ref player.Get<Speed>();
            speed.Value *= 1f + NormalizedValue;
        }
    }
}
