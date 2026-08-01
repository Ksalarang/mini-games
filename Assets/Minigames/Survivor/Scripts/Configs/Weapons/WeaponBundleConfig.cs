using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Weapons
{
    [CreateAssetMenu(fileName = "WeaponBundleConfig", menuName = "Minigames/Survivor/Weapons/WeaponBundleConfig", order = 0)]
    public class WeaponBundleConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject WeaponPrefab { get; private set; }

        [field: Space, SerializeField] public WeaponConfig StartingWeapon { get; private set; }
        [field: SerializeField] public ProjectileConfig[] Projectiles { get; private set; }
    }
}
