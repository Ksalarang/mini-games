using System;
using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    public class Bird : MonoBehaviour
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }

        public event Action OnCollisionEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollisionEnter?.Invoke();
        }
    }
}