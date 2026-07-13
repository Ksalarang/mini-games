using UnityEngine;

namespace Minigames.Survivor.Scripts
{
    public class InfiniteFloor : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float textureWorldSize = 1f;
        [SerializeField] private float margin = 2f;

        private Material material;
        private Transform cameraTransform;

        private void Awake()
        {
            material = meshRenderer.material; // instance, not shared
            cameraTransform = camera.transform;
            Resize();
        }

        private void Resize()
        {
            var height = camera.orthographicSize * 2f + margin;
            var width = height * camera.aspect + margin;

            transform.localScale = new Vector3(width, height, 1f);
            material.mainTextureScale = new Vector2(width, height) / textureWorldSize;
        }

        private void LateUpdate()
        {
            var position = cameraTransform.position;
            material.mainTextureOffset = position / textureWorldSize;
        }
    }
}