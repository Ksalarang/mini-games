using Minigames.Survivor.Scripts.Configs.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Components.Weapons
{
    public struct ProjectileWeapon
    {
        public Sprite Sprite;
        public float Damage;
        public float Cooldown;
        public ProjectileType Type;
        public ProjectileDirectionType DirectionType;
        public float Speed;
        public float Lifetime;
    }
}
