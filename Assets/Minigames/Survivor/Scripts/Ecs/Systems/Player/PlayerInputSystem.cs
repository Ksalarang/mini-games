using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
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
            ref var velocity = ref player.Get<Velocity>();
            velocity.X = inputVector.x;
            velocity.Y = inputVector.y;
        }
    }
}
