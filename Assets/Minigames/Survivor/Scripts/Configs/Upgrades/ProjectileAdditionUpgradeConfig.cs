using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "ProjectileAdditionUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/ProjectileAdditionUpgradeConfig", order = 0)]
    public class ProjectileAdditionUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private ProjectileConfig projectileConfig;

        public override bool IsApplicableTo(EcsEntity player)
        {
            var projectiles = player.Get<WeaponInventory>().Projectiles;
            return projectiles.All(p => p.Type != projectileConfig.Type);
        }

        public override void Apply(EcsEntity player, EcsWorld world)
        {
            var projectileEntity = world.NewEntity();
            projectileConfig.AddWeaponComponentTo(ref projectileEntity);
            var projectileComponent = projectileEntity.Get<ProjectileWeapon>();
            player.Get<WeaponInventory>().Projectiles.Add(projectileComponent);

            var spawnRequest = world.NewEntity();
            spawnRequest.Get<TimerComponent>().TimeLeft = projectileComponent.Cooldown;
            spawnRequest.Get<ProjectileSpawnRequest>().ProjectileType = projectileComponent.Type;
        }
    }
}
