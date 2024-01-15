using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        public ScriptableEventPathNodes _eventPathNodes;
        public UnitRenderData unitRenderData;
        public GridMapVariable _gripMap;
        public Unit unitTarget;
        public PathNode nodeBefore;
        public float _speed;
        
        private List<PathNode> pathNodes;
        private void OnEnable()
        {
            _eventPathNodes.OnRaised += Move;
            nodeBefore = new PathNode((int)transform.position.x, (int)transform.position.y);
        }

        private void Move(List<PathNode> eventPathNodes)
        {
            var pos = transform.position.ToV2Int();

            if (eventPathNodes == null || eventPathNodes.Count == 0)
            {
                _gripMap.Value[nodeBefore.xPos, nodeBefore.yPos] = null;
                _gripMap.Value[pos.x, pos.y] = this;
                return;
            }
            if (unitTarget == null) return;

            Vector2Int unitTargetPos = unitTarget.transform.position.ToV2Int();
            pathNodes = eventPathNodes;
            Vector2Int pathTarget = new Vector2Int(pathNodes[0].xPos, pathNodes[0].yPos);
            if (CheckDistanceCell(unitTargetPos))
            {
                _gripMap.Value[nodeBefore.xPos, nodeBefore.yPos] = null;
                _gripMap.Value[pos.x, pos.y] = this;
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, pathTarget, _speed * Time.deltaTime);
            if ((Vector2)transform.position == pathTarget)
            {
                _gripMap.Value[nodeBefore.xPos, nodeBefore.yPos] = null;
                _gripMap.Value[pathTarget.x, pathTarget.y] = this;
                nodeBefore = pathNodes[0];
            }
        }
        private bool CheckDistanceCell(Vector2Int posTarget)
        {
            var posStart = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            int distance = Math.Abs(posStart.x - posTarget.x) + Math.Abs(posStart.y - posTarget.y);
            return distance <= 1f;
        }
    }
}
