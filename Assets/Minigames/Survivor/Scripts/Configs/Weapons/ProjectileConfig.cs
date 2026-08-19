using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Minigames/Survivor/Weapons/ProjectileConfig", order = 0)]
    public class ProjectileConfig : WeaponConfig
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }

        protected override void OnComponentsAdded(ref EcsEntity weapon)
        {
            weapon.Get<SpeedComponent>().Value = Speed;
            weapon.Get<LifetimeComponent>().Value = Lifetime;
        }
    }
}
