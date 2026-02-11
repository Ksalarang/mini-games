using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Minigames.FlappyBird.Scripts
{
    public class SceneContainer : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform WorldTransform { get; private set; }
        [field: SerializeField] public Tower ReferenceTower { get; private set; }
        [field: SerializeField] public Bird Bird { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject TowerPrefabReference { get; private set; }

        [field: SerializeField] public Button BackButton { get; private set; }
        [field: SerializeField] public TMP_Text PointLabel { get; private set; }
        [field: Space, SerializeField] public LoseScreen LoseScreen { get; private set; }
        
        [field: Space, SerializeField] public TowerConfig TowerConfig { get; private set; }
        [field: SerializeField] public BirdConfig BirdConfig { get; private set; }
    }
}