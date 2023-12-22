/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Obvious.Soap;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;
    
    [SerializeField] private IntVariable _width;
    [SerializeField] private IntVariable _height;

    private static Dictionary<Vector2, Tile> _tiles;
    [SerializeField] private GameObject Grid;
    public static GridManager Instance;

    void Awake() => Instance = this;

    public void _GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
               for (var x = 0; x < _width; x++)
               {
                   for (var y = 0; y < _height; y++)
                   {
                       var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                       spawnedTile.name = $"Tile {x} {y}";
                       _tiles[new Vector2(x, y)] = spawnedTile;
                   }
               }
       
               cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
               GameManager.Instance.ChangeState(GameState.SpawnHeroes);
           }
       
           public Tile HeroSpawnTile()
           {
               return _tiles.Where(t => t.Key.x < _width / 2 ).OrderBy(t => Random.value).First().Value;
           }
           public Tile EnemySpawnTile()
           {
               return _tiles.Where(t => t.Key.x > _width / 2 ).OrderBy(t => Random.value).First().Value;
           }
}*/