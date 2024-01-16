using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private List<GameObject> heroPrefabs;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float tileSize = 1.0f;

    private void Awake()
    {
        mapVariable.Init();
        SpawnUnits();
    }
    private void SpawnUnits()
    {
        foreach (var hero in heroPrefabs)
        {
            var randomX = Random.Range(3, 8);
            var randomY = Random.Range(3, 8);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedHero = Instantiate(hero, snappedPosition, Quaternion.identity, map.transform);
            mapVariable.Value[randomX, randomY] = hero.GetComponent<Hero>();
        }
            
        foreach (var enemy in enemyPrefabs)
        {
            var randomX = Random.Range(10,17);
            var randomY = Random.Range(2, 9);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedEnemy = Instantiate(enemy, snappedPosition, Quaternion.identity, map.transform);
            spawnedEnemy.transform.Rotate(0,180,0);
        }
    }
}