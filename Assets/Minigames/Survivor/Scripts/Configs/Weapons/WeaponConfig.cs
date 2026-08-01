using Leopotam.Ecs;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }

        public abstract void CreateWeaponEntity(ref EcsEntity ecsEntity);
    }
}
