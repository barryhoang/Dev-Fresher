using System;
using System.Collections.Generic;
using System.Drawing;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;
using Color = UnityEngine.Color;

namespace Apathfinding
{
    [RequireComponent(typeof(GridMap))]
    public class GridManager : MonoBehaviour
    {
        public static GridManager instance { get; private set; }
        
        [SerializeField] private Tilemap _tilemap;
        public Pathfinding _pathfinding;
        public GridMapVariable gridMap;
        public Vector2 size;
        public List<Vector3> test;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            gridMap.size.x = bounds.size.x;
            gridMap.size.y = bounds.size.y;
            for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
            {
                for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
                {
                    TileBase tile = _tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile != null)
                    {
                        test.Add(new Vector3(x,y,0));
                    }
                    else
                    {
                        if(x > 0 && y>0)
                            gridMap.Value[x,y] = 2;
                    }
                }
            }
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,size);
        }
    }
}
