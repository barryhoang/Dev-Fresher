using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tung
{
    public class GridMapManager : MonoBehaviour
    {
        public static GridMapManager instance { get; private set; }

        [SerializeField] private GridMapVariable _gridMap;
        public List<GameObject> players;
        public List<Vector3> posOld;
        public Tilemap _tileTest;
        public TileBase highTileBase;
        public Tilemap _tilemap;
        public Pathfinding _pathfinding;
        public List<Vector3> test;
        private void Awake()
        {
            instance = this;
            BoundsInt bounds = _tilemap.cellBounds;
            _gridMap.size.x = bounds.size.x;
            _gridMap.size.y = bounds.size.y;
            _gridMap.Init();
            for (int x = 0; x < bounds.size.x - 1; x++)
                for (int y = 0; y < bounds.size.y - 1; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    TileBase tile = _tilemap.GetTile(tilePosition);
                    if (x >= 0 && y >= 0 && x < _gridMap.size.x && y < _gridMap.size.y)
                    {
                        if (tile == null)
                        {
                            _gridMap.Value[x, y] = true;
                        }
                    }
                }
            _pathfinding.Init();
        }

        public void UpdateGrid()
        {
            for (int i = 0; i < _gridMap.size.x; i++)
            {
                for (int j = 0; j < _gridMap.size.y; j++)
                {
                    if (_gridMap.Value[i, j])
                    {
                        _tileTest.SetTile(new Vector3Int(i, j, 0), highTileBase);
                    }
                    else
                    {
                        _tileTest.SetTile(new Vector3Int(i, j, 0), null);
                    }
                }
            }
        }
    }
}
