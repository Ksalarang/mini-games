using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "KnifeDamageUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/KnifeDamageUpgradeConfig", order = 0)]
    public class KnifeDamageUpgradeConfig : UpgradeConfig
    {
        public override void Apply(EcsEntity player)
        {
            var projectiles = player.Get<WeaponInventory>().Projectiles;

            for (var i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];

                if (projectile.Type == ProjectileType.Knife)
                {
                    projectile.Damage *= 1f + NormalizedValue;
                    projectiles[i] = projectile;
                    break;
                }
            }
        }
    }
}
