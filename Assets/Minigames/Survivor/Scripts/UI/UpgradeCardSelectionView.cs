using UnityEngine;

namespace Minigames.Survivor.Scripts.UI
{
    public class UpgradeCardSelectionView : MonoBehaviour
    {
        [field: SerializeField] public UpgradeCardView[] Cards { get; private set; }
    }
}
