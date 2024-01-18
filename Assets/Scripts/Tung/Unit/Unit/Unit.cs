using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private GridMapVariable gridMap;
        [SerializeField] private Pathfinding _pathfinding;
        [SerializeField] private UnitStat _unitStats;
        [SerializeField] private UnitViewer _unitViewer;
        [SerializeField] private HealthViewer _healthViewer;
        [SerializeField] private VfxUnitViewer _vfxUnitViewer;
        [SerializeField] private ScriptableEventUnit _eventDie;
        private float _currentHealth;
        private List<PathNode> _pathNodes;
        
        public UnitRenderData unitRenderData;
        public Unit unitTarget;
        public Vector2Int posGridUnit;
        public bool isMove;
        public bool isAttacking;

        private void OnEnable()
        {
            posGridUnit = transform.position.ToV2Int();
            _unitViewer.SetAnimation(AniName.IDLE,true);
            _currentHealth = _unitStats.MaxHealth;
            _healthViewer.SetMaxHealth(_currentHealth);
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
            if(target == null) isMove = false;
            
            unitTarget = target;
            if (isMove && CheckDistanceCell(transform.position, unitTarget.transform.position))
            {
                _unitViewer.SetAnimation(AniName.MOVE, false);
                _unitViewer.SetAnimation(AniName.IDLE, true);
                isMove = false;
                isAttacking = true;
                Timing.RunCoroutine(Attack().CancelWith(gameObject));
            }
            if (isMove)
            {
                _unitViewer.SetAnimation(AniName.IDLE,false);
                _unitViewer.SetAnimation(AniName.MOVE, true);
                
                var posStart = transform.position.ToV2Int();
                var posTarget = DistanceDir(unitTarget.posGridUnit);
                _pathNodes =  _pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
                var posPathTarget = Vector2Int.zero;
                if (_pathNodes != null)
                {
                    posPathTarget = new Vector2Int(_pathNodes[0].xPos,_pathNodes[0].yPos);
                    gridMap.Value[posStart.x, posStart.y] = null;
                    gridMap.Value[posPathTarget.x,posPathTarget.y] = this;
                    posGridUnit = posPathTarget;
                }
                else
                {
                    isMove = false;
                }
                while (_pathNodes != null && _pathNodes.Count > 0)
                {
                    var x = unitTarget.transform.position.x - transform.position.x;
                    _unitViewer.SetFlip(x);
                    transform.position = Vector2.MoveTowards(transform.position, posPathTarget,
                        _unitStats.Speed.Value * Time.deltaTime);
                
                    if ((Vector2) transform.position == posPathTarget)
                    {
                        if (CheckDistanceCell(posPathTarget,unitTarget.posGridUnit))
                        {
                            _unitViewer.SetAnimation(AniName.MOVE, false);
                            _unitViewer.SetAnimation(AniName.IDLE, true);
                            isMove = false;
                            isAttacking = true;
                            Timing.RunCoroutine(Attack().CancelWith(gameObject));    
                            break;
                        }
                        posStart = transform.position.ToV2Int();
                        posTarget = DistanceDir(unitTarget.posGridUnit);
                        _pathNodes =  _pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
                        posPathTarget = new Vector2Int(_pathNodes[0].xPos,_pathNodes[0].yPos);
                        gridMap.Value[posStart.x, posStart.y] = null;
                        gridMap.Value[posPathTarget.x,posPathTarget.y] = this;
                        posGridUnit = posPathTarget;
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        private IEnumerator<float> Attack()
        {
            Timing.WaitUntilDone(Timing.RunCoroutine(CheckAttack().CancelWith(gameObject)));
            while (isAttacking)
            {
                if (!unitTarget.gameObject.activeInHierarchy)
                {
                    isAttacking = false;
                    break;
                }
                var dir = (unitTarget.transform.position - transform.position).normalized;
                _unitViewer.dir = dir;
                _unitViewer.SetAttackActive(this);
                yield return Timing.WaitForSeconds(_unitViewer.duration);
                _vfxUnitViewer.SetVfxPunch(dir);
                unitTarget.TakeDamage(_unitStats.Damage);
                _unitViewer.SetAttackEnd(this);
                yield return Timing.WaitForSeconds(_unitStats.AttackSpeed - 0.1f);
            }
        }

        private IEnumerator<float> CheckAttack()
        {
            while (Vector2.Distance(transform.position,unitTarget.transform.position) >= 1f)
            {
                yield return Timing.WaitForOneFrame;
            }
        }

        private void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _healthViewer.UpdateHealthBar(_currentHealth);
            _healthViewer.VfxDamage(damage);
            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
                _eventDie.Raise(this);
            }
        }
        
        #region Check
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
        private bool CheckDistanceCell(Vector2 pos ,Vector2 posTarget)
        {
            
            int distance = (int) (Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y));
            return Math.Abs(distance) <= 1f;
        }
        private bool CheckDistanceCell(Vector2Int pos ,Vector2Int posTarget)
        {
            int distance = Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y);
            return Math.Abs(distance) <= 1f;
        }
        #endregion
        
    }
}
