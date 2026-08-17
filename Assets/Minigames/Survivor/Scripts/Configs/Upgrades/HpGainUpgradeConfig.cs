using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "HpGainUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/HpGainUpgradeConfig", order = 0)]
    public class HpGainUpgradeConfig : UpgradeConfig
    {
        public override void Apply(EcsEntity player, EcsWorld world)
        {
            ref var health = ref player.Get<Health>();
            health.Value = Mathf.Min(health.Value + Value, health.MaxValue);
        }
    }
}
