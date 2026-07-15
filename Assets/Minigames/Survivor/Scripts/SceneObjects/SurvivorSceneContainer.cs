using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class SurvivorSceneContainer : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }

        [field: SerializeField] public InfiniteFloor InfiniteFloor { get; private set; }
        [field: SerializeField] public PlayerContainer PlayerContainer { get; private set; }
    }
}
