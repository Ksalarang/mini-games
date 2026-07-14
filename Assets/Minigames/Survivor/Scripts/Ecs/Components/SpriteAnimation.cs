using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct SpriteAnimation
    {
        public Sprite[] Sprites;
        public int FramesPerSecond;
        public int CurrentIndex;
        public float CurrentTimeSeconds;
    }
}
