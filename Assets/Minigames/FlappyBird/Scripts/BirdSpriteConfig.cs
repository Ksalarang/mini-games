using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    [CreateAssetMenu(fileName = "BirdSpriteConfig", menuName = "Minigames/FlappyBird/BirdSpriteConfig", order = 0)]
    public class BirdSpriteConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite[] FlappingSprites { get; private set; }

        [field: SerializeField] public int FlapUpdatePeriodMillis { get; private set; }
    }
}
