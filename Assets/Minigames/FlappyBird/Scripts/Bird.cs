using System;
using UnityEngine;

namespace Minigames.FlappyBird.Scripts
{
    public class Bird : MonoBehaviour
    {
        public event Action OnCollisionEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollisionEnter?.Invoke();
        }
    }
}