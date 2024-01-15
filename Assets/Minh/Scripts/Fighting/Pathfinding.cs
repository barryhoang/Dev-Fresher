using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PathNode
    {
        public int xPos;
        public int yPos;
        public int gValue;
        public int hValue;
        public PathNode parentNode;

        public int fValue
        {
            get { return gValue + hValue; }
        }

        public PathNode(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }

    //[RequireComponent(typeof(GridMap))]
    public class Pathfinding : MonoBehaviour
    {
       // [SerializeField] GridMap gridMap;
        [SerializeField] private PathNode[,] pathNodes;
        [SerializeField] private FightingMapVariable _gridMap;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            pathNodes = new PathNode[_gridMap.size.x, _gridMap.size.y];

            for (int x = 0; x < _gridMap.size.x; x++)
            {
                for (int y = 0; y < _gridMap.size.y; y++)
                {
                    pathNodes[x, y] = new PathNode(x, y);
                }
            }
            Debug.Log("PATH NODE" + pathNodes.Length);
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
           
            PathNode startNode = pathNodes[startX, startY];
            PathNode endNode = pathNodes[endX, endY];

            List<PathNode> openList = new List<PathNode>();
            List<PathNode> closedList = new List<PathNode>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[0];

                for (int i = 0; i < openList.Count; i++)
                {
                    if (currentNode.fValue > openList[i].fValue)
                    {
                        currentNode = openList[i];
                    }

                    if (currentNode.fValue == openList[i].fValue
                        && currentNode.hValue >= openList[i].hValue
                    )
                    {
                        currentNode = openList[i];
                    }
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    //we finished searching ours path
                    return RetracePath(startNode, endNode);
                }

                List<PathNode> neighbourNodes = new List<PathNode>();
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        if (x == 0 && y == 0) { continue; }
                        if (x == 1 && y == 1 || y == 1 && x == 1) { continue; }
                        if (x == -1 && y == -1 || y == -1 && x == -1) { continue; }
                        if (x == 1 && y == -1 || y == 1 && x == -1) { continue; }
                        if (_gridMap.CheckPosition(currentNode.xPos + x, currentNode.yPos + y) == false)
                        {
                            continue;
                        }

                        neighbourNodes.Add(pathNodes[currentNode.xPos + x, currentNode.yPos + y]);
                    }
                }

                for (int i = 0; i < neighbourNodes.Count; i++)
                {
                    if (closedList.Contains(neighbourNodes[i]))
                    {
                        continue;
                    }

                    if (new Vector2(neighbourNodes[i].xPos, neighbourNodes[i].yPos) != new Vector2(endX, endY))
                    {
                        if (_gridMap.CheckWalkable(neighbourNodes[i].xPos, neighbourNodes[i].yPos) == false)
                        {
                            continue;
                        }
                    }


                    int movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);

                    if (openList.Contains(neighbourNodes[i]) == false
                        || movementCost < neighbourNodes[i].gValue
                    )
                    {
                        neighbourNodes[i].gValue = movementCost;
                        neighbourNodes[i].hValue = CalculateDistance(neighbourNodes[i], endNode);
                        neighbourNodes[i].parentNode = currentNode;

                        if (openList.Contains(neighbourNodes[i]) == false)
                        {
                            openList.Add(neighbourNodes[i]);
                        }
                    }
                }
            }

            return null;
        }

        private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();

            PathNode currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

             path.Reverse();

            return path;
        }

        private int CalculateDistance(PathNode current, PathNode target)
        {
            int distX = Mathf.Abs(current.xPos - target.xPos);
            int distY = Mathf.Abs(current.yPos - target.yPos);

            if (distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }

            return 14 * distX + 10 * (distY - distX);
        }
    }
}