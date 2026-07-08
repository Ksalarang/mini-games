using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Minigames/FlappyBird/AudioConfig", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public AudioClip Flap { get; private set; }
        [field: SerializeField] public AudioClip Point { get; private set; }
        [field: SerializeField] public AudioClip Hit { get; private set; }
    }
}
