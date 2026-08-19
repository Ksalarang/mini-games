using Core.Tools.Structs;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    [CreateAssetMenu(fileName = "PayloadProjectileConfig", menuName = "Minigames/Survivor/Weapons/PayloadProjectileConfig")]
    public class PayloadProjectileConfig : WeaponConfig
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public FloatRange LifetimeRange { get; private set; }
        [field: SerializeField] public float ImpactRadius { get; private set; }

        protected override void OnComponentsAdded(ref EcsEntity weapon)
        {
            weapon.Get<SpeedComponent>().Value = Speed;
            weapon.Get<LifetimeRangeComponent>().Value = LifetimeRange;
            weapon.Get<ImpactRadiusComponent>().Value = ImpactRadius;
        }
    }
}
