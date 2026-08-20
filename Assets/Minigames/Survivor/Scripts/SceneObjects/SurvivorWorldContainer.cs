using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class SurvivorWorldContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Transform Entities { get; private set; }
        [field: SerializeField] public Transform Enemies { get; private set; }
        [field: SerializeField] public Transform Projectiles { get; private set; }
        [field: SerializeField] public Transform ExpItems { get; private set; }

        [field: Space, SerializeField] public SpriteObject SpriteObjectPrefab { get; private set; }
    }
}
