using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;


namespace Minh
{
    public class DungeonLevelGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _spawnEnemy;
        [SerializeField] private FightingMapVariable _fightingMap;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private List<Enemy> _hero;
        [SerializeField] private DungeronLevelManager _dungeronLevelManager;
        private void Start()
        {
            _spawnEnemy.OnRaised += SpawnEnemy;
            _dungeronLevelManager.SpawnEnemy();
        }

        private void OnDestroy()
        {
            _spawnEnemy.OnRaised -= SpawnEnemy;
        }

        private void SpawnEnemy(int numberEnemy)
        {
            for (int i = 0; i < numberEnemy; i++)
            {
                int RandomX = UnityEngine.Random.Range(8, 13);
                int RandomY = UnityEngine.Random.Range(0, 5);
                while (_fightingMap.Value[RandomX, RandomY] != null)
                {
                     RandomX = UnityEngine.Random.Range(8, 13);
                     RandomY = UnityEngine.Random.Range(0, 5);
                }
                int RandomEnemy  = UnityEngine.Random.Range(0, 3);
                Enemy Enemy=Instantiate(_hero[RandomEnemy], new Vector3(RandomX, RandomY, 0), Quaternion.identity);
                Enemy.transform.localScale=new Vector3(-1,1,1);
                _fightingMap.Value[RandomX, RandomY] = Enemy;
                _soapListEnemy.Add(Enemy);
            }
        }
    }
}