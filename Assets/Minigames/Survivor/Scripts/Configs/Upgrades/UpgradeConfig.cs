using Leopotam.Ecs;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public float Value { get; private set; } = 1f;

        protected float NormalizedValue => Value / 100f;

        public virtual bool IsApplicableTo(EcsEntity player) => true;

        public abstract void Apply(EcsEntity player, EcsWorld world);
    }
}
