using Core.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.FlappyBird.Scripts
{
    public class LoseScreen : FadableView
    {
        [field: SerializeField] public TMP_Text PreviousPoints { get; private set; }
        [field: SerializeField] public TMP_Text Points { get; private set; }
        [field: SerializeField] public Button RetryButton { get; private set; }
    }
}