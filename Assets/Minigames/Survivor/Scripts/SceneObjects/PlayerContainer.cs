using Minigames.Survivor.Scripts.UI;
using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class PlayerContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public ProgressBar HealthBar { get; private set; }
    }
}
