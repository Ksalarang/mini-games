using Core.Tools.Structs;
using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Minigames/FlappyBird/TowerConfig", order = 0)]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public FloatRange GenerationDelay { get; private set; }
        [field: SerializeField] public FloatRange TowerGap { get; private set; }
        [field: SerializeField] public float TowerSpeed { get; private set; }
        [field: SerializeField] public float TowerIncreasePeriodSeconds { get; private set; }
        [field: SerializeField] public float TowerSpeedIncrease { get; private set; }
    }
}