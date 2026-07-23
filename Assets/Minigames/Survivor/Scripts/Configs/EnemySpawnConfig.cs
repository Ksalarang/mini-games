using System;
using System.Collections.Generic;
using System.Linq;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Minigames/Survivor/EnemySpawnConfig", order = 0)]
    public class EnemySpawnConfig : ScriptableObject
    {
        [field: SerializeField] public EnemySpawnData[] Data { get; private set; }

        [field: Space, SerializeField] public GameObject EnemyPrefab { get; private set; }

        public Dictionary<EnemyType, EnemySpawnData> Dict => dict ??= Data.ToDictionary(d => d.EnemyType, d => d);

        private Dictionary<EnemyType, EnemySpawnData> dict;
    }

    [Serializable]
    public class EnemySpawnData
    {
        public EnemyType EnemyType;
        public Sprite[] Sprites;
        public int FramesPerSecond;
        public float SpawnIntervalSeconds;
        public float MoveSpeed;
        public float Health;
    }
}
