using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode 
{
    public int xPos;
    public int yPos;
    public int gValue;
    public int hValue;
    public PathNode parentNode;

    
    public int FValue 
    {
        get {
            return gValue + hValue;
        }
    }

    public PathNode(int xPos, int yPos) 
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }
}

[RequireComponent(typeof(GridMap))]
public class Pathfinding : MonoBehaviour
{
    private GridMap _gridMap;
    private PathNode[,] _pathNodes;

    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_gridMap == null) { _gridMap = GetComponent<GridMap>(); }
        _pathNodes = new PathNode[_gridMap.width, _gridMap.height];
        for (var x = 0; x < _gridMap.width; x++) 
        {
            for (var y = 0; y < _gridMap.height; y++) 
            {
                _pathNodes[x, y] = new PathNode(x,y);
            }
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) 
    {
        var startNode = _pathNodes[startX, startY];
        var endNode = _pathNodes[endX, endY];

        var openList = new List<PathNode>();
        var closedList = new HashSet<PathNode>();

        openList.Add(startNode);

        while (openList.Count > 0) 
        {
            var currentNode = openList[0];

            for (var i = 0; i < openList.Count; i++) 
            {
                if (currentNode.FValue > openList[i].FValue) 
                {
                    currentNode = openList[i];
                }

                if (currentNode.FValue == openList[i].FValue
                    && currentNode.hValue > openList[i].hValue
                    )
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode) 
            {
                return RetracePath(startNode, endNode);
            }

            var neighbourNodes = new List<PathNode>();
            
            for (var x = -1; x < 2; x++) 
            {
                for (var y = -1; y < 2; y++) 
                {
                    if (x == 0 && y == 0) { continue; }
                    if (_gridMap.CheckPos(currentNode.xPos + x, currentNode.yPos + y) == false) 
                    {
                        continue;
                    }

                    neighbourNodes.Add(_pathNodes[currentNode.xPos + x, currentNode.yPos + y]);
                }
            }

            for (var i = 0; i < neighbourNodes.Count; i++) 
            {
                if (closedList.Contains(neighbourNodes[i])) { continue; }
                var movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);
                if (openList.Contains(neighbourNodes[i]) != false && movementCost >= neighbourNodes[i].gValue) continue;
                neighbourNodes[i].gValue = movementCost;
                neighbourNodes[i].hValue = CalculateDistance(neighbourNodes[i], endNode);
                neighbourNodes[i].parentNode = currentNode;

                if (openList.Contains(neighbourNodes[i]) == false) 
                {
                    openList.Add(neighbourNodes[i]);
                }

            }

        }

        return null;
    }

    private static List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        var path = new List<PathNode>();
        var currentNode = endNode;
        while (currentNode != startNode) 
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        return path;
    }

    private static int CalculateDistance(PathNode current, PathNode target)
    {
        var distX = Mathf.Abs(current.xPos - target.xPos);
        var distY = Mathf.Abs(current.yPos - target.yPos);
        if (distX > distY) { return 14 * distY + 10 * (distX - distY);  }
        return 14 * distX + 10 * (distY - distX);

    }
}