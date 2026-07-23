using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem, InputActions.IPlayerActions
    {
        private readonly InputActions inputActions = new();

        private readonly EcsFilter<PlayerTag> playerFilter;

        private Vector2 inputVector;

        public void Init()
        {
            inputActions.Player.AddCallbacks(this);
            inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }

        public void Run()
        {
            var player = playerFilter.GetEntity(0);
            ref var input = ref player.Get<PlayerMoveInput>();

            input.X = inputVector.x;
            input.Y = inputVector.y;
        }
    }
}
