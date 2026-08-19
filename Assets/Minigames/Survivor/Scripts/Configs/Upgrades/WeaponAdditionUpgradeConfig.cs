using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "WeaponAdditionUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/WeaponAdditionUpgradeConfig")]
    public class WeaponAdditionUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private WeaponConfig config;

        public override bool IsApplicableTo(EcsEntity player)
        {
            var weapons = player.Get<WeaponInventory>().Weapons;
            return weapons.All(w => w.Get<WeaponComponent>().Id != config.Id);
        }

        public override void Apply(EcsEntity player, EcsWorld world)
        {
            var projectile = world.NewEntity();
            config.AddComponentsTo(ref projectile);
            player.Get<WeaponInventory>().Weapons.Add(projectile);

            var spawnRequest = world.NewEntity();
            spawnRequest.Get<TimerComponent>().TimeLeft = config.Cooldown;
            spawnRequest.Get<WeaponSpawnRequest>() = new WeaponSpawnRequest
            {
                WeaponType = config.Type,
                WeaponId = config.Id,
            };
        }
    }
}
