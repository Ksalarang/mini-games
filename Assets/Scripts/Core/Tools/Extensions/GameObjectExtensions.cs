using UnityEngine;

namespace Core.Tools.Extensions
{
    public static class GameObjectExtensions
    {
        public static void Destroy(this GameObject gameObject)
        {
            if (gameObject)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}