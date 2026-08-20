using System;
using System.Collections.Generic;
using System.Linq;
using Minigames.Survivor.Scripts.Common;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyDamageConfig", menuName = "Minigames/Survivor/EnemyDamageConfig", order = 0)]
    public class EnemyDamageConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyDamageData[] Data { get; private set; }

        public Dictionary<EnemyId, EnemyDamageData> Dict => dict ??= Data.ToDictionary(d => d.id, d => d);

        private Dictionary<EnemyId, EnemyDamageData> dict;
    }

    [Serializable]
    public class EnemyDamageData
    {
        public EnemyId id;
        public float Damage;
        public float Interval;
    }
}
