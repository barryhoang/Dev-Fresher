using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> _units;
    public BaseHero selectedHero;

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;
        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Heroes);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            randomSpawnTile.SetUnit(spawnedHero);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }
    
    public void SpawnEnemies()
    {
        var enemyCount = 1;
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            randomSpawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }
    
    public void SetSelectedHero(BaseHero hero)
    {
        selectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.First(u => u.Faction == faction).UnitPrefab;
    }
}
