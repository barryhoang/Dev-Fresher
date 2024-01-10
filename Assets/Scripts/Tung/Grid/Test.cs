using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tung
{
    public class Test : MonoBehaviour
    {
        public Tilemap test;
        public TileBase tileBase;
        public GridMapVariable gridMap;
        public void Start()
        {
            for (int i = 0; i < gridMap.size.x; i++)
            {
                for (int j = 0; j < gridMap.size.y; j++)
                {
                    test.SetTile(new Vector3Int(i, j, 0), tileBase);
                }
            }
        }
    }
}
