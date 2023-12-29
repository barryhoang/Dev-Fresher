using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    Tilemap tileMap;
    GridMap gridMap;
    SaveLoadMap saveLoadMap;
    MapData mapData;

    [SerializeField] private TileSet tileSet;
    

    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        gridMap = GetComponent<GridMap>();
        tileMap.ClearAllTiles();
        saveLoadMap = GetComponent<SaveLoadMap>();
        saveLoadMap.mapLoad(gridMap);
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        for (int x = 0; x < gridMap.width; x++)
        {
            for (int y = 0; y < gridMap.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }

    private void UpdateTile(int x, int y)
    {
        int tileId = gridMap.Get(x, y);
        if (tileId == -1)
        {
            return;
        }
        
        tileMap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
    }

    public int[,] ReadTileMap()
    {
        if (tileMap == null)
        {
            tileMap = GetComponent<Tilemap>();
        }
        int sizeX = tileMap.size.x;
        int sizeY = tileMap.size.y;
        int[,] tileMapData = new int[sizeX,sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                TileBase tileBase = tileMap.GetTile(new Vector3Int(x, y, 0));
                int indexTile = tileSet.tiles.FindIndex(x=> x == tileBase);
                tileMapData[x, y] = indexTile;
            }
        }
        return tileMapData;
    }

    public void SetTile(int x, int y,int tileId)
    {
        if (tileMap == null)
        {
            tileMap = GetComponent<Tilemap>();
        }
        tileMap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
        tileMap = null;
    }

    public void Clear()
    {
        if (tileMap == null)
        {
            tileMap = GetComponent<Tilemap>();
        }
        tileMap.ClearAllTiles();
        tileMap = null;
    }
}
/*public void Set(int x, int y, int to)
 {
     gridMap.Set(x,y,to);
     UpdateTile(x,y);
 }*/