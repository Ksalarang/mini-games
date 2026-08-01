using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Survivor.Scripts.UI
{
    public class UiContainer : MonoBehaviour
    {
        [field: SerializeField] public Button BackButton { get; private set; }
        [field: SerializeField] public SurvivorGameOverScreen GameOverScreen { get; private set; }
        [field: SerializeField] public ProgressBarView ExpProgressBar { get; private set; }
        [field: SerializeField] public UpgradeCardSelectionView UpgradeCardSelectionView { get; private set; }
    }
}
