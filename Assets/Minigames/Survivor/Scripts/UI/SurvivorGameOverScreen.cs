using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Survivor.Scripts.UI
{
    public class SurvivorGameOverScreen : FadableView
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
    }
}
