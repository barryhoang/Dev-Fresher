using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Apathfinding
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager instance { get; private set; }

        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tilemap _wall;

        public Pathfinding _pathfinding;
        public GridMapVariable gridMap;
        public List<Vector3> test;
        public Vector2 size;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            gridMap.size.x = bounds.size.x;
            gridMap.size.y = bounds.size.y;
            _pathfinding.Init();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color =Color.red;
            Gizmos.DrawWireCube(transform.position,size);
        }

        public void UpdateGrid()
        {
            var enties = Physics2D.OverlapBoxAll(transform.position, size, 0);
            foreach (var temp in enties)
            {
                if (temp.gameObject.CompareTag("Character") || temp.gameObject.CompareTag("Enemy"))
                    gridMap.Value[(int) temp.transform.position.x, (int) temp.transform.position.y] = true;
            }
        }
        public void ResetValue()
        {
            _pathfinding.ResetValue();
        }   
    }
}

