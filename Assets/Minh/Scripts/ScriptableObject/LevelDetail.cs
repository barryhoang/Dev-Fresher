using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Minh
{
    [CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObject/Level")]
    public class LevelDetail : ScriptableObject
    {
        [Serializable]
        public class EnemyDetail
        {
            [SerializeField] public GameObject enemyPrefab;
            [SerializeField] public Vector3 spawnPosition;
            
        }

        [SerializeField] public List<EnemyDetail> enemyDetailList;

        
        
    }
    
    
}