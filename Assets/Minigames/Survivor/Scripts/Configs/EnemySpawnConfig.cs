using System;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Minigames/Survivor/EnemySpawnConfig", order = 0)]
    public class EnemySpawnConfig : ScriptableObject
    {
        [field: SerializeField] public EnemySpawnData[] Data { get; private set; }

        [field: Space, SerializeField] public GameObject EnemyPrefab { get; private set; }

        public EnemySpawnData GetData(EnemyType type)
        {
            foreach (var data in Data)
            {
                if (data.EnemyType == type)
                {
                    return data;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class EnemySpawnData
    {
        public EnemyType EnemyType;
        public Sprite[] Sprites;
        public int FramesPerSecond;
        public float SpawnIntervalSeconds;
        public float MoveSpeed;
    }
}
