using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class InputService : IInitializable, InputActions.IGameplayActions
    {
        private readonly InputActions inputActions = new();

        public event Action OnTapped;

        void IInitializable.Initialize()
        {
            inputActions.Gameplay.AddCallbacks(this);
        }

        void InputActions.IGameplayActions.OnTap(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnTapped?.Invoke();
            }
        }

        public void Enable()
        {
            inputActions.Enable();
        }

        public void Disable()
        {
            inputActions.Disable();
        }
    }
}