
using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using Pathfinding;
using Pathfinding.Util;
using Test;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public Transform target;
    public bool isMove = true;
    public FightingGridVariable map;
    public Vector3 dir;
    private GraphNode node;
    
    void Update ()
    {
        node = AstarPath.active.GetNearest(target.transform.position).node;

        if (!node.Walkable)
        {
            if (isMove)
            {
                var posTarget = target.transform.position + dir;
                Debug.Log(posTarget);
                var traversalProvider = new BlockTest();
                ABPath path = ABPath.Construct(transform.position, (Vector2) posTarget);
           
                path.traversalProvider = traversalProvider;
                // Start calculating the path and put the path at the front of the queue
                AstarPath.StartPath(path, true);

                // Calculate the path immediately
                path.BlockUntilCalculated();
                if(path.vectorPath.Count == 1) return;
                if ( path.vectorPath.Count >= 1)
                {
                    transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[1], 2 * Time.deltaTime);
                }

                if ( transform.position == posTarget)
                {
                    isMove = false;
                    var a = transform.position.ToV2Int();
                    map.Value[a.x, a.y] = new Unit(); ;
                }
            
                // Make sure the remaining paths do not use the same nodes as this one
                foreach (var node in path.path) {
                    traversalProvider.blockedNodes.Add(node);
                }
                // Draw the path in the scene view
                for (int i = 0; i < path.vectorPath.Count - 1; i++) {
                    Debug.DrawLine(path.vectorPath[i], path.vectorPath[i+1], Color.red);
                }
            }
        }
    }

    public Vector2Int tinhtoandir()
    {
        var shortestDistance = float.MaxValue;
        Vector2Int[] targets = new Vector2Int[2];
        var position = target.transform.position;
        targets[0] = (position + Vector3.right).ToV2Int();
        targets[1] = (position + Vector3.left).ToV2Int();
        Vector2Int value = Vector2Int.zero;
        foreach (var dir in targets)
        {
            float distance = Vector2.Distance(transform.position, dir);
            if(map.Value[dir.x,dir.y] != null) continue;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                value = dir;
            }
        }

        return value;
    }
}
