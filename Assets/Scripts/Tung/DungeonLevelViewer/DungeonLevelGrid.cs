using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tung
{
    public class DungeonLevelGrid : MonoBehaviour
    {
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableEventInt _numberEnemy;
        [SerializeField] private List<Unit> _enemiesPrefab;
        [SerializeField] private GameObject _enemyManager;
        [SerializeField] private int _maxEnemies;
        [SerializeField] private Vector2Int _sizeSpawn;
        
        private Dictionary<Unit, List<Unit>> _dicEnemies =  new Dictionary<Unit, List<Unit>>();
        private void Start()
        {
            InitSpawn();
            _numberEnemy.OnRaised += SpawnEnemies;
        }

        private void OnDisable()
        {
            _numberEnemy.OnRaised -= SpawnEnemies;
        }

        private void InitSpawn()
        {
            foreach (var unit in _enemiesPrefab)
            {
                List<Unit> enemies = new List<Unit>();
                for (int j = 0; j < _maxEnemies; j++)
                {
                    var enemy = Instantiate(unit,_enemyManager.transform);
                    enemy.gameObject.SetActive(false);
                    enemies.Add(enemy);
                }
                _dicEnemies.Add(unit,enemies);
            }
        }
        private void SpawnEnemies(int numberEnemy)
        {
            if(_enemiesPrefab.Count == 0) return;
            var temp = numberEnemy;
            for (int i = 0; i < _dicEnemies[_enemiesPrefab[0]].Count; i++)
            {
                if(temp <= 0) return;
                var pos = RandomPosition();
                while (_gridMap.Value[pos.x,pos.y] != null)
                {
                    pos = RandomPosition();
                }
                var enemy = _dicEnemies[_enemiesPrefab[0]][i];
                enemy.transform.position = (Vector2) pos;
                _gridMap.Value[pos.x, pos.y] = enemy;
                _listSoapEnemies.Add(enemy);
                enemy.gameObject.SetActive(true);
                temp--;
            }
        }
        private Vector2Int RandomPosition()
        {
            int randomX = Random.Range(_sizeSpawn.x, _gridMap.size.x);
            int randomY = Random.Range(_sizeSpawn.y, _gridMap.size.y);
            return new Vector2Int(randomX,randomY);
        }
    }
}