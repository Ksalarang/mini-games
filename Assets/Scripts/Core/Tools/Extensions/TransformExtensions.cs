using UnityEngine;

namespace Core.Tools.Extensions
{
    public static class TransformExtensions
    {
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            var localScale = transform.localScale;
            localScale.x = x;
            transform.localScale = localScale;
        }
    }
}
