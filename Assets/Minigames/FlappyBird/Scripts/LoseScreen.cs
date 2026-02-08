using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.FlappyBird.Scripts
{
    public class LoseScreen : FadableView
    {
        [field: SerializeField] public Button RetryButton { get; private set; }
    }
}