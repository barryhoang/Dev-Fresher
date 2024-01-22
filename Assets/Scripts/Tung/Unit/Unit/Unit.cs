using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private GridMapVariable gridMap;
        [SerializeField] private Pathfinding _pathfinding;
        [SerializeField] private UnitStat _unitStats;
        [SerializeField] private HealthViewer _healthViewer;
        [SerializeField] private VfxUnitViewer _vfxUnitViewer;
        [SerializeField] private ScriptableEventUnit _eventDie;
        private float _currentHealth;
        private List<PathNode> _pathNodes;
        public UnitViewer _unitViewer;
        public Unit unitTarget;
        public Vector2Int posGridUnit;
        public bool isMove;
        public bool isAttacking;

        private void OnEnable()
        {
            _unitViewer.ResetFlip();
            posGridUnit = transform.position.ToV2Int();
            _unitViewer.transform.position = transform.position;
            _unitViewer.SetAnimation(AniName.IDLE, true);
            _currentHealth = _unitStats.MaxHealth;
            _healthViewer.SetMaxHealth(_currentHealth);
            isMove = false;
            isAttacking = false;
        }

        private void OnDisable()
        {
            _vfxUnitViewer.ResetVfx();
            var pos = transform.position.ToV2Int();
            gridMap.Value[pos.x, pos.y] = null;
        }

        public IEnumerator<float> Move(Unit target)
        {
            if (target == null) isMove = false;

            unitTarget = target;
            if (isMove && CheckDistanceCell(transform.position, unitTarget.transform.position))
            {
                SetAttack();
            }
            if (isMove)
            {
                var posStart = transform.position.ToV2Int();
                var posTarget = DistanceDir(unitTarget.posGridUnit);
                if (posTarget == Vector2Int.zero)
                {
                    _unitViewer.SetAnimation(AniName.IDLE, true);
                    _unitViewer.SetAnimation(AniName.MOVE, false);
                    isMove = false;
                    yield break;
                }
                _unitViewer.SetAnimation(AniName.IDLE, false);
                _unitViewer.SetAnimation(AniName.MOVE, true);
                _pathNodes = _pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
                var posPathTarget = Vector2Int.zero;
                if (_pathNodes != null)
                {
                    posPathTarget = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
                    gridMap.Value[posStart.x, posStart.y] = null;
                    gridMap.Value[posPathTarget.x, posPathTarget.y] = this;
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

                    if ((Vector2)transform.position == posPathTarget)
                    {
                        if (CheckDistanceCell(posPathTarget, unitTarget.posGridUnit))
                        {
                            SetAttack();
                            break;
                        }
                        posStart = transform.position.ToV2Int();
                        posTarget = DistanceDir(unitTarget.posGridUnit);
                        if (posTarget == Vector2Int.zero)
                        {
                            isMove = false;
                            break;
                        }
                        _pathNodes = _pathfinding.FindPath(posStart.x, posStart.y, posTarget.x, posTarget.y, this);
                        if (_pathNodes == null)
                        {
                            isMove = false;
                            break;
                        }
                        posPathTarget = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
                        gridMap.Value[posStart.x, posStart.y] = null;
                        gridMap.Value[posPathTarget.x, posPathTarget.y] = this;
                        posGridUnit = posPathTarget;
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        private void SetAttack()
        {
            _unitViewer.SetAnimation(AniName.MOVE, false);
            _unitViewer.SetAnimation(AniName.IDLE, true);
            isMove = false;
            isAttacking = true;
            Timing.RunCoroutine(Attack().CancelWith(gameObject), "Combat");
        }

        private IEnumerator<float> Attack()
        {
            Timing.WaitUntilDone(Timing.RunCoroutine(CheckAttack().CancelWith(gameObject)));
            var x = unitTarget.transform.position.x - transform.position.x;
            _unitViewer.SetFlip(x);
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
                yield return Timing.WaitForSeconds(_unitStats.AttackSpeed - _unitViewer.duration);
            }
        }

        private IEnumerator<float> CheckAttack()
        {
            while (Vector2.Distance(transform.position, unitTarget.transform.position) > 1)
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
        private Vector2Int DistanceDir(Vector2Int unitTarget)
        {
            Vector2Int start = transform.position.ToV2Int();
            Vector2Int value = Vector2Int.zero;
            float minDistance = float.MaxValue;
            Vector2Int[] listDir = { unitTarget + Vector2Int.down, unitTarget + Vector2Int.left, unitTarget + Vector2Int.right, unitTarget + Vector2Int.up };
            foreach (var dir in listDir)
            {
                float distance = Vector2Int.Distance(start, dir);
                if (dir.x < 0 || dir.y < 0 || dir.x >= gridMap.size.x || dir.y >= gridMap.size.y) continue;
                if (distance < minDistance && gridMap.Value[dir.x, dir.y] == null)
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }

        #region Check
        private bool CheckDistanceCell(Vector2 pos, Vector2 posTarget)
        {
            int distance = (int)(Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y));
            return Math.Abs(distance) <= 1f;
        }
        private bool CheckDistanceCell(Vector2Int pos, Vector2Int posTarget)
        {
            int distance = Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y);
            return Math.Abs(distance) <= 1f;
        }
        #endregion

    }
}
