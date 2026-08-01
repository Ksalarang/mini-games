using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "KnifeFireRateUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/KnifeFireRateUpgradeConfig", order = 0)]
    public class KnifeFireRateUpgradeConfig : UpgradeConfig
    {
        [field: SerializeField] public float MinCooldown { get; private set; }

        public override void Apply(EcsEntity player)
        {
            var projectiles = player.Get<WeaponInventory>().Projectiles;

            for (var i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];

                if (projectile.Type is ProjectileType.Knife)
                {
                    var value = projectile.Cooldown * (1f - Value / 100f);
                    projectile.Cooldown = Mathf.Max(MinCooldown, value);
                    projectiles[i] = projectile;
                    break;
                }
            }
        }
    }
}
