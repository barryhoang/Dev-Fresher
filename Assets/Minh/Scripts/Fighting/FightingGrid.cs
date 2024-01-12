using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class FightingGrid : MonoBehaviour
{
    private FightingMapVariable _fightingMap;
    private Pathfinding _pathfinding;
    private void GetPath(int startX, int startY, int targetX,int targetY)
    {
        _pathfinding.FindPath(startX,startY,targetX,targetY);
    }
}
