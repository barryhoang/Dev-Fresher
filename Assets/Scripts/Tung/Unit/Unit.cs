using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private GridMapVariable gridMap;
        [SerializeField] private Pathfinding pathfinding;
        
        public UnitRenderData unitRenderData;
        public List<PathNode> pathNodes;
        public Unit unitTarget;
        public float _speed;
        public Vector2Int posGridUnit;
        public bool isMove;

        private void OnEnable()
        {
            posGridUnit = transform.position.ToV2Int();
        }
        // cho o no bang null 
        // cho o tiep theo bang this
        // nhan unit di chuyen den tinh 4 huong cua no 
        // tinh path 
        // di duoc 1 path cap nhap lai path
        // va o no dung bang null
        // kiem tra o trc mat no co muc tieu chua 
        // neu roi thi out
        // cho o tiep theo bang this
        // khi chay den path cuoi thi dung lai
        public IEnumerator<float> Move(Unit target)
        {
            if(target == null) yield break;
            unitTarget = target;
            var posStart = transform.position.ToV2Int();
            var posTarget = DistanceDir(unitTarget.posGridUnit);
            pathNodes =  pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
            var posPathTarget = new Vector2Int(pathNodes[0].xPos,pathNodes[0].yPos);
            gridMap.Value[posStart.x, posStart.y] = null;
            gridMap.Value[posPathTarget.x,posPathTarget.y] = this;
            posGridUnit = posPathTarget;
            while (pathNodes != null && pathNodes.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, posPathTarget,
                    _speed * Time.deltaTime);
                
                if ((Vector2) transform.position == posPathTarget)
                {
                    if (CheckDistanceCell(posPathTarget,unitTarget.posGridUnit))
                    {
                        yield break;
                    }
                    
                    posStart = transform.position.ToV2Int();
                    posTarget = DistanceDir(unitTarget.posGridUnit);
                    pathNodes =  pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
                    posPathTarget = new Vector2Int(pathNodes[0].xPos,pathNodes[0].yPos);
                    gridMap.Value[posStart.x, posStart.y] = null;
                    gridMap.Value[posPathTarget.x,posPathTarget.y] = this;
                    posGridUnit = posPathTarget;
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        private Vector2Int DistanceDir(Vector2Int unitTarget)
        {
            Vector2Int start =transform.position.ToV2Int();
            Vector2Int value = Vector2Int.zero;
            float minDistance = float.MaxValue;
            Vector2Int[] listDir = {unitTarget + Vector2Int.down,unitTarget + Vector2Int.left,unitTarget+ Vector2Int.right ,unitTarget + Vector2Int.up};
            foreach (var dir in listDir)
            {
                float distance = Vector2Int.Distance(start,dir);
                
                
                if (distance < minDistance && gridMap.Value[dir.x, dir.y] == null)
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }
        
        private bool CheckDistanceCell(Vector2Int pos ,Vector2Int posTarget)
        {
            int distance = Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y);
            Debug.Log(distance);
            return Math.Abs(distance) <= 1f;
        }
    }
}
