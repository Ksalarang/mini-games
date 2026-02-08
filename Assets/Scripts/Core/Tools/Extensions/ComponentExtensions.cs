using UnityEngine;

namespace Core.Tools.Extensions
{
    public static class ComponentExtensions
    {
        public static void DestroyGameObject(this Component component)
        {
            if (component && component.gameObject)
            {
                Object.Destroy(component.gameObject);
            }
        }
    }
}