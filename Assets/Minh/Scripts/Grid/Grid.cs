using System;
using UnityEngine;
using System.Collections.Generic;
using Obvious.Soap;

namespace Minh
{
    public class Grid : MonoBehaviour
    {
        public Transform StartPosition;
        public LayerMask WallMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        public float Distance;
        Node[,] grid;
        public List<Node> FinalPath;

        private float _nodeDiameter;
         int _gridSizeX;
         int _gridSizeY;

        private void Start()
        {
            nodeRadius = nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
        }

        private void CreateGrid()
        {
            grid=new Node[_gridSizeX,_gridSizeY];
            Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
                                 Vector3.forward * gridWorldSize.y / 2;
            for (int y = 0; y < _gridSizeX;y++)
            {
                for (int x = 0; x < _gridSizeY; x++)
                {
                    Vector3 worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + nodeRadius) +
                                         Vector3.forward * (y * _nodeDiameter + nodeRadius);
                    bool Wall = true;
                    if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                    {
                        Wall = false;
                    }
                    grid[y,x]=new Node(Wall,worldPoint,x,y);
                    
                }
            }
        }

        public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
        {
            float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
            float ypoint = ((a_WorldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

            xpoint = Mathf.Clamp01(xpoint);
            ypoint = Mathf.Clamp01(ypoint);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * xpoint);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * ypoint);

            return grid[x, y];
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
            if (grid != null)
            {
                foreach (Node node in grid)
                {
                    if (node.IsWall)
                    {
                        Gizmos.color=Color.white;
                        
                    }
                    else
                    {
                        Gizmos.color=Color.yellow;
                    }

                    if (FinalPath != null)
                    {
                        Gizmos.color = Color.red;
                    }

                    Gizmos.DrawCube(node.Position, Vector3.one * (_nodeDiameter - Distance));
                }
            }
        }
    }
}