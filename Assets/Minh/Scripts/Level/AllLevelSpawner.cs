using System.Collections.Generic;
using UnityEngine;
using MEC;
using Obvious.Soap;

namespace Minh
{
    public class AllLevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private AllLevel _allLevel;
        public LevelDetail levelDetail;
        public LevelDetail.EnemyDetail enemyDetail;
        [SerializeField] private IntVariable _currentLevel;
        private int offset;

        private void Start()
        {
            SpawnPlayer();
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
                Enemy _enemy = enemy.GetComponent<Enemy>();
                EnemyPlacement enemyPlacement = enemy.GetComponent<EnemyPlacement>();
                enemyPlacement.Init();
                _enemy.AddToList();
            }
            
            yield break;
        }

        private void SpawnPlayer()
        {
            for (int i = 0; i < 2; i++)
            {
                
                GameObject player;
                player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                PlayerPlacement playerPlacement;
                Player _playerScript;
                _playerScript = player.GetComponent<Player>();
                playerPlacement = player.GetComponent<PlayerPlacement>();
                playerPlacement.Init(new Vector3(0,i+2,0));
                offset = offset + 2;
                _playerScript.AddToList();


            }
        }
    }
}