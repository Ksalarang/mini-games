using Minigames.Survivor.Scripts.Configs.Weapons;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct WeaponComponent
    {
        public WeaponType Type;
        public WeaponId Id;
        public WeaponTargetingType TargetingType;
    }
}
