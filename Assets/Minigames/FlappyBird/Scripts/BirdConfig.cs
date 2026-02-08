using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    [CreateAssetMenu(fileName = "BirdConfig", menuName = "Minigames/FlappyBird/BirdConfig", order = 0)]
    public class BirdConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 GravityAcceleration { get; private set; }
        [field: SerializeField] public float AscendingSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; } 
    }
}