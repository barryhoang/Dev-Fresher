using System;
using System.Collections.Generic;
using System.Drawing;
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
        public GridMap gridMap;
        public Vector2 size;
        public List<Vector3> test;
        public LayerMask layerMask;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            gridMap.Init(bounds.size.x,bounds.size.y);
            for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
            {
                for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
                {
                    TileBase tile = _tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile != null)
                    {
                        test.Add(new Vector3(x,y,0));
                        gridMap.Set(x,y,0);
                    }
                    else
                    {
                        test.Add(new Vector3(x,y,2));
                        gridMap.Set(x, y, 2);
                    }
                }
            }

            PhysicCheck();
            _pathfinding.Init();
        }

        public void PhysicCheck()
        {
            Collider2D[] temp =  Physics2D.OverlapBoxAll(transform.position,size, 0f);
            foreach (var entity in temp)
            {
                if (entity.gameObject.CompareTag("Character"))
                {
                    Debug.Log(entity.transform.position.x + " " + entity.transform.position.y);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,size);
        }

        // void UpdateTileMap()
        // {
        //     for (int x = 0; x < _gridMap.length; x++)
        //     {
        //         for (int y = 0; y < _gridMap.height; y++)
        //         {
        //             UpdateTile(x, y);
        //         }
        //     }
        // }
        //
        // private void UpdateTile(int x, int y)
        // {
        //     int tileId = _gridMap.Get(x, y);
        //     if (tileId == -1)
        //     {
        //         return;
        //     }
        //
        //     // _tilemap.SetTile(new Vector3Int(x,y,0),tileSet.tiles[tileId] );
        // }

        // public void Set(int x, int y,int to)
        // {
        //     _gridMap.Set(x,y,to);
        //     UpdateTile(x,y);
        // }
    }
}
