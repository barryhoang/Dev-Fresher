using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Pathfinding;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        public ScriptableEventPathNodes _eventPathNodes;
        public Seeker ai;
        public UnitRenderData unitRenderData;
        public GridMapVariable _gripMap;
        public Unit unitTarget;
        public PathNode nodeBefore;
        public float _speed;
        public BlockManager blockManager;
        public List<SingleNodeBlocker> obstacles;
        public Path Path;
        public Transform target;
        private List<PathNode> pathNodes;
        BlockManager.TraversalProvider traversalProvider;

        private int currentWaypoint = 0;
        private void OnEnable()
        {
          
            _eventPathNodes.OnRaised += Move;
            // traversalProvider = new BlockManager.TraversalProvider(blockManager, BlockManager.BlockMode.OnlySelector, obstacles);
        }

        public void Update()
        {
            // Path  =  ABPath.Construct(transform.position, target.position, OnPathComplete);
            // ai.StartPath(Path);
            // Path.traversalProvider = traversalProvider;
            // Path.BlockUntilCalculated();
        }

        public void OnPathComplete(Path p)
        {
            p.Claim(this);
            if (!p.error)
            {
                if (Path != null) Path.Release(this);
                Path = p;
                currentWaypoint = 0;
            }
            else
            {
                p.Release(this);
            }
        }
        private void Move(List<PathNode> eventPathNodes)
        {
        
        }
        private bool CheckDistanceCell(Vector2Int posTarget)
        {
            var posStart = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            int distance = Math.Abs(posStart.x - posTarget.x) + Math.Abs(posStart.y - posTarget.y);
            return distance <= 1f;
        }
    }
}
