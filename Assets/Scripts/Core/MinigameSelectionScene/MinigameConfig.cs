using UnityEngine;

namespace Core.MinigameSelectionScene
{
    [CreateAssetMenu(fileName = "MinigameConfig", menuName = "MinigameSelection/vb ghMinigameConfig", order = 0)]
    public class MinigameConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string SceneKey { get; private set; }
    }
}