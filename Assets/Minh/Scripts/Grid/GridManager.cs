using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Minh
{
    [RequireComponent((typeof(GridMap)))]
    [RequireComponent(typeof(Tilemap))]
    public class GridManager : MonoBehaviour
    {
        private Tilemap _tilemap;
        private GridMap _gridMap;
        [SerializeField]private GridMapVariable _gridMap1;

        [SerializeField] private TileSet _tileSet;
        private void Start()
        {
            _tilemap = GetComponent<Tilemap>();
            _gridMap = GetComponent<GridMap>();
            _gridMap.Init(_gridMap1.size.x, _gridMap1.size.y);
          //  Set(0,0,3);
         //    Set(5,5,3);
            // Set(1,1,3);
            // Set(1,0,3);
            UpdateTileMap();
        }

        private void UpdateTileMap()
        {
            for (int x = 0;  x< _gridMap._length; x++)
            {
                for (int y = 0; y < _gridMap._height; y++)
                {
                    UpdateTile(x, y);
                }
            }
        }

        private void UpdateTile(int x, int y)
        {
            int tileId = _gridMap.Get(x, y);
            if (tileId == -1)
            {
                return;
            }
            _tilemap.SetTile(new Vector3Int(x,y,0), _tileSet.tiles[tileId]);
      
        }

        public void Set(int x, int y, int to)
        {
            _gridMap.Set(x,y,to);
            UpdateTile(x,y);
        }
    }
}