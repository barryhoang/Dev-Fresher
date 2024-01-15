using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] private TileSet tileSet;
    
    private Tilemap _tileMap;
    private GridMap _gridMap;
    private SaveLoadMap _saveLoadMap;
    private MapData _mapData;

    private void Awake()
    {
        _tileMap = GetComponent<Tilemap>();
        _gridMap = GetComponent<GridMap>();
    }

    private void Start()
    {
        _tileMap.ClearAllTiles();
        _saveLoadMap = GetComponent<SaveLoadMap>();
        _saveLoadMap.LoadMap(_gridMap);
        UpdateTileMap();
    }

    private void UpdateTileMap()
    {
        for (var x = 0; x < _gridMap.width; x++)
        {
            for (var y = 0; y < _gridMap.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }

    private void UpdateTile(int x, int y)
    {
        var tileId = _gridMap.GetTile(x, y);
        if (tileId == -1)
        {
            return;
        }
        
        _tileMap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public int[,] ReadTileMap()
    {
        if (_tileMap == null)
        {
            _tileMap = GetComponent<Tilemap>();
        }
        var sizeX = _tileMap.size.x;
        var sizeY = _tileMap.size.y;
        var tileMapData = new int[sizeX,sizeY];

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var tileBase = _tileMap.GetTile(new Vector3Int(x, y, 0));
                var indexTile = tileSet.tiles.FindIndex(x=> x == tileBase);
                tileMapData[x, y] = indexTile;
            }
        }
        return tileMapData;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetTile(int x, int y,int tileId)
    {
        if (_tileMap == null)
        {
            _tileMap = GetComponent<Tilemap>();
        }
        _tileMap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
        _tileMap = null;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Clear()
    {
        if (_tileMap == null)
        {
            _tileMap = GetComponent<Tilemap>();
        }
        _tileMap.ClearAllTiles();
        _tileMap = null;
    }
}