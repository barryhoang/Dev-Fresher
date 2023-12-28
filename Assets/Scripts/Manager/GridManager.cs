using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    Tilemap tilemap;
    GridMap grid;

    [SerializeField] private TileSet tileSet;
    
    [SerializeField] private Transform Cam;
    

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<GridMap>();
        grid.Init(20, 10);
        Set(1,1,1);
        Set(2,2,2);
        Set(4,3,3);
        Set(10,3,3);
        Set(11,4,4);
        Set(12,3,4);
        Set(7,6,4);
        Set(15,5,5);
        Set(13,10,5);
        Set(2,1,2);
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }

    private void UpdateTile(int x, int y)
    {
        int tileId = grid.Get(x, y);
        if (tileId == -1)
        {
            return;
        }
        
        tilemap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
        
        Cam.transform.position = new Vector3((float)grid.length/2 - 0.5f, (float)grid.height/2 - 0.5f,-10);
    }

    public void Set(int x, int y, int to)
    {
        grid.Set(x,y,to);
        UpdateTile(x,y);
    }
}
