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
        private Dictionary<Unit, List<Unit>> _dicEnemies;

        private void Start()
        {
            InitSpawn();
            _numberEnemy.OnRaised += SpawnEnemies;
        }

        private void OnDisable()
        {
            _numberEnemy.OnRaised -= SpawnEnemies;
        }
        public void OnLose()
        {
            for (int i = 0; i < _enemyManager.transform.childCount; i++)
            {
                _enemyManager.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void InitSpawn()
        {
            _dicEnemies = new Dictionary<Unit, List<Unit>>();
            foreach (var unit in _enemiesPrefab)
            {
                List<Unit> enemies = new List<Unit>();
                for (int j = 0; j < _maxEnemies; j++)
                {
                    var enemy = Instantiate(unit, _enemyManager.transform);
                    enemy.gameObject.SetActive(false);
                    enemies.Add(enemy);
                }
                _dicEnemies.Add(unit, enemies);
            }
        }
        private void SpawnEnemies(int numberEnemy)
        {
            _listSoapEnemies.ResetToInitialValue();
            for (int i = 0; i < numberEnemy; i++)
            {
                var pos = RandomPosition();
                while (_gridMap.Value[pos.x, pos.y] != null)
                {
                    pos = RandomPosition();
                }
                int index = Random.Range(0, _enemiesPrefab.Count);
                var enemy = _dicEnemies[_enemiesPrefab[index]][i];
                enemy.transform.position = (Vector2)pos;
                _gridMap.Value[pos.x, pos.y] = enemy;
                _listSoapEnemies.Add(enemy);
                _listSoapEnemies[i].gameObject.SetActive(true);
            }
        }
        private Vector2Int RandomPosition()
        {
            int randomX = Random.Range(_sizeSpawn.x, _gridMap.size.x);
            int randomY = Random.Range(_sizeSpawn.y, _gridMap.size.y);
            return new Vector2Int(randomX, randomY);
        }
    }
}