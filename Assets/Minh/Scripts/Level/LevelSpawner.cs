using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Unity.Mathematics;

namespace Minh
{
    public class LevelSpawner : MonoBehaviour
    {
        public LevelDetail levelDetail;
        public LevelDetail.EnemyDetail enemyDetail;

        private void Start()
        {
            Timing.RunCoroutine(StartLevel());
        }

        public IEnumerator<float> StartLevel()
        {
            for (int i = 0; i < levelDetail.enemyDetailList.Count; i++)
            {
                GameObject enemy;
                enemyDetail = levelDetail.enemyDetailList[i];
                enemy=Instantiate(enemyDetail.enemyPrefab, enemyDetail.spawnPosition,Quaternion.identity);
                EnemyPlacement enemyPlacement = enemy.GetComponent<EnemyPlacement>();
                enemyPlacement.Init();
                yield return Timing.WaitForOneFrame;
            }
        }

    
    }
}