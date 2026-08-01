using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Minigames/Survivor/Weapons/ProjectileConfig", order = 0)]
    public class ProjectileConfig : WeaponConfig
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }
        [field: SerializeField] public ProjectileDirectionType DirectionType { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }

        public override void CreateWeaponEntity(EcsWorld world, out EcsEntity entity)
        {
            entity = world.NewEntity();
            entity.Get<ProjectileWeapon>() = new ProjectileWeapon
            {
                Sprite = Sprite,
                Damage = Damage,
                Cooldown = Cooldown,
                Type = Type,
                DirectionType = DirectionType,
                Speed = Speed,
                Lifetime = Lifetime,
            };
        }
    }

    public enum ProjectileType
    {
        Knife,
    }

    public enum ProjectileDirectionType
    {
        Player,
    }
}
