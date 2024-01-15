using System;
using System.Collections;
using System.Collections.Generic;
using Minh;
using Obvious.Soap;
using UnityEngine;

public class FightingGrid : MonoBehaviour
{
    private FightingMapVariable _fightingMap;
    public Pathfinding _pathfinding;
    [SerializeField] private int _currentX = 0;
    [SerializeField] private int _currentY = 0;
    
    public List<PathNode>  GetPath(int startX, int startY, int targetX,int targetY)
    {
     
        List<PathNode> path=_pathfinding.FindPath(startX,startY,targetX,targetY);
        return path;
    }
}
