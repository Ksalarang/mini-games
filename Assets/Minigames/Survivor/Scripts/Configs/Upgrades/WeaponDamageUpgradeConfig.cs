using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "WeaponDamageUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/WeaponDamageUpgradeConfig")]
    public class WeaponDamageUpgradeConfig : WeaponUpgradeConfig
    {
        public override void Apply(EcsEntity player, EcsWorld world)
        {
            var weapons = player.Get<WeaponInventory>().Weapons;

            for (var i = 0; i < weapons.Count; i++)
            {
                var weapon = weapons[i];

                if (weapon.Get<WeaponComponent>().Id == WeaponId)
                {
                    weapon.Get<DamageComponent>().Value *= 1f + NormalizedValue;
                    weapons[i] = weapon;
                    return;
                }
            }
        }
    }
}
