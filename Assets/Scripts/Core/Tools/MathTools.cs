using UnityEngine;

namespace Core.Tools
{
    public static class MathTools
    {
        public static Vector2 GetRandomPointOnCircle(Vector2 center, float radius)
        {
            var angle = Random.Range(0f, Mathf.PI * 2f);
            var x = center.x + Mathf.Cos(angle) * radius;
            var y = center.y + Mathf.Sin(angle) * radius;

            return new Vector2(x, y);
        }
    }
}
