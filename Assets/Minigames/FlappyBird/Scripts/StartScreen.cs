using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.FlappyBird.Scripts
{
    public class StartScreen : FadableView
    {
        [field: SerializeField] public Button StartButton { get; private set; }
    }
}
