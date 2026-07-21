using System;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Minigames/Survivor/WeaponConfig", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
        [field: SerializeField] public ProjectileData[] Projectiles { get; private set; }
    }

    [Serializable]
    public abstract class WeaponData
    {
        public Sprite Sprite;
        public float Damage;
        public float Cooldown;
    }

    [Serializable]
    public class ProjectileData : WeaponData
    {
        public ProjectileDirectionType DirectionType;
        public float Speed;
    }

    public enum ProjectileDirectionType
    {
        Player,
    }
}
