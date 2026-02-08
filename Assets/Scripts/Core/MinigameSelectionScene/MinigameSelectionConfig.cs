using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.MinigameSelectionScene
{
    [CreateAssetMenu(fileName = "MinigameSelectionConfig", menuName = "MinigameSelection/MinigameSelectionConfig", order = 0)]
    public class MinigameSelectionConfig : ScriptableObject
    {
        [field: SerializeField] public List<MinigameConfig> Minigames { get; private set; }
        [field: SerializeField] public AssetReference MinigameButtonViewReference { get; private set; }
    }
}