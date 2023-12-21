using System.Collections.Generic;
using UnityEngine;
using MEC;
using Obvious.Soap;

namespace Minh
{
    public class AllLevelSpawner : MonoBehaviour
    {
        [SerializeField] private AllLevel _allLevel;
        public LevelDetail levelDetail;
        public LevelDetail.EnemyDetail enemyDetail;
        [SerializeField] private IntVariable _currentLevel;

        private void Start()
        {
            Timing.RunCoroutine(StartLevel());
        }

        public void SpawnNewLevel()
        {
            Timing.RunCoroutine(StartLevel());
        }

        private IEnumerator<float> StartLevel()
        {
            for (int i = 0; i < _allLevel._levelDetail[_currentLevel.Value - 1].enemyDetailList.Count; i++)
            {
                GameObject enemy;
                enemyDetail = _allLevel._levelDetail[_currentLevel.Value - 1].enemyDetailList[i];
                enemy = Instantiate(enemyDetail.enemyPrefab, enemyDetail.spawnPosition, Quaternion.identity);
                EnemyPlacement enemyPlacement = enemy.GetComponent<EnemyPlacement>();
                enemyPlacement.Init();
                
            }
            yield break;
        }
    }
}