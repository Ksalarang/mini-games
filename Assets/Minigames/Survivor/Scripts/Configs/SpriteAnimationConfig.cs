using System;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfig", menuName = "Minigames/Survivor/SpriteAnimationConfig", order = 0)]
    public class SpriteAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteAnimationData[] Data { get; private set; }
    }

    [Serializable]
    public class SpriteAnimationData
    {
        public MoveState State;
        public Sprite[] Sprites;
        public int FramesPerSecond;
    }
}
