using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using Obvious.Soap.Example;


public class UnitManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _heroCount;
    [SerializeField] private FloatVariable _enemyCount;
    
    public static UnitManager Instance;
    public Transform HeroPrefab;
    public Transform EnemyPrefab;

    private void Awake() => Instance = this;

    public void SpawnHeroes()
    {
        for (var i = 0; i < _heroCount; i++)
        {
            var spawnedHero = Instantiate(HeroPrefab);
            var randomSpawnTile = GridManager.Instance.HeroSpawnTile();
            randomSpawnTile.SetUnit(spawnedHero);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }
    public void SpawnEnemies()
    {
        for (var i = 0; i < _enemyCount; i++)
        {
            var spawnedEnemy = Instantiate(EnemyPrefab);
            var randomSpawnTile = GridManager.Instance.EnemySpawnTile();
            randomSpawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }
}
