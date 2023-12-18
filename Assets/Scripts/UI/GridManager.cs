using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _width, _height;
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private Transform _cam;

        private Dictionary<Vector2, Tile> _tiles;

        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            _tiles = new Dictionary<Vector2, Tile>();
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var spawnedTile = Instantiate(_tilePrefab, new Vector3(i,j), Quaternion.identity);
                    spawnedTile.name = $"Tile {i} {j}";
                    var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0); 
                    spawnedTile.Init(isOffset);
                    _tiles[new Vector2(i, j)] = spawnedTile;
                }
            }
            _cam.transform.position = new  Vector3((float)_width/2-0.5f,(float)_height/2-0.5f,-10);
        }

        public Tile GetTileAtPosition(Vector2 pos)
        {
            return _tiles.TryGetValue(pos,out  var tile) ? tile : null;
        }
    }
}
