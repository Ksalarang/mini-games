using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public WeaponId Id { get; private set; }
        [field: SerializeField] public WeaponTargetingType TargetingType { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int SortingOrder { get; private set; } = 2;
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }

        public void AddComponentsTo(ref EcsEntity weapon)
        {
            weapon.Get<WeaponComponent>() = new WeaponComponent
            {
                Type = Type,
                Id = Id,
                TargetingType = TargetingType,
            };
            weapon.Get<SpriteComponent>().Value = Sprite;
            weapon.Get<RenderOrderComponent>().SortingOrder = SortingOrder;
            weapon.Get<DamageComponent>().Value = Damage;
            weapon.Get<CooldownComponent>().Value = Cooldown;

            OnComponentsAdded(ref weapon);
        }

        protected abstract void OnComponentsAdded(ref EcsEntity weapon);
    }

    public enum WeaponType
    {
        Projectile,
        PayloadProjectile,
    }

    public enum WeaponId
    {
        Knife,
        Fireball,
        Bomb,
    }

    public enum WeaponTargetingType
    {
        PlayerDirection,
        TargetClosestEnemy,
        Random,
    }
}
