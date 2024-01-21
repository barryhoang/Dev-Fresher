using System;
using System.Collections.Generic;
using Map;
using Obvious.Soap;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonLevel
{
    public class DungeonLevelGrid : MonoBehaviour
    {
        [SerializeField] private MapVariable mapVariable;
        [SerializeField] private ScriptableListUnit scriptableListEnemy;
        [SerializeField] private ScriptableEventInt spawnEnemies;
        [SerializeField] private Vector2Int spawnLimit;
        [SerializeField] private List<Unit> enemies;
        [SerializeField] private GameObject enemySpawner;
        [SerializeField] private int maxEnemyCount;

        private readonly Dictionary<Unit, List<Unit>> _enemyList = new();

        private void Awake()
        {
            SpawnInit();
            spawnEnemies.OnRaised += SpawnEnemy;
        }

        private void SpawnInit()
        {
            foreach (var unit in enemies)
            {
                var enemyList = new List<Unit>();
                if (enemyList == null) throw new ArgumentNullException(nameof(enemyList));
                for (var i = 0; i < maxEnemyCount; i++)
                {
                    var enemy = Instantiate(unit, enemySpawner.transform);
                    enemy.transform.Rotate(0,180,0);
                    enemyList.Add(enemy);
                }
                _enemyList.Add(unit,enemies);
            }
        }
        
        private void SpawnEnemy(int count)
        {
            if (enemies.Count == 0) return;
            for (var i = 0; i < _enemyList[enemies[0]].Count; i++)
            {
                if (count <= 0) return;
                var spawnPos = RandomPos();
                while (mapVariable.Value[spawnPos.x,spawnPos.y] != null)
                {
                    spawnPos = RandomPos();
                }

                var enemy = _enemyList[enemies[0]][i];
                enemy.transform.position = (Vector2)spawnPos;
                mapVariable.Value[spawnPos.x, spawnPos.y] = enemy;
                scriptableListEnemy.Add(enemy);
                count++;
            }
        }

        private Vector2Int RandomPos()
        {
            var randomX = Random.Range(spawnLimit.x, mapVariable.size.x);
            var randomY = Random.Range(spawnLimit.y, mapVariable.size.y);
            return new Vector2Int(randomX, randomY);
        }

        private void OnDisable()
        {
            spawnEnemies.OnRaised -= SpawnEnemy;
        }
    }
}
