using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "WeaponCooldownUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/WeaponCooldownUpgradeConfig", order = 0)]
    public class WeaponCooldownUpgradeConfig : WeaponUpgradeConfig
    {
        [field: SerializeField] public float MinCooldown { get; private set; } = 0.1f;

        public override void Apply(EcsEntity player, EcsWorld world)
        {
            var weapons = player.Get<WeaponInventory>().Weapons;

            for (var i = 0; i < weapons.Count; i++)
            {
                var weapon = weapons[i];
                var weaponComponent = weapon.Get<WeaponComponent>();

                if (weaponComponent.Id == WeaponId)
                {
                    ref var cooldown = ref weapon.Get<CooldownComponent>();
                    cooldown.Value = Mathf.Max(MinCooldown, cooldown.Value * (1 - NormalizedValue));
                    weapons[i] = weapon;
                    return;
                }
            }
        }
    }
}
