using System;
using System.Collections.Generic;
using Animation;
using Entity;
using JetBrains.Annotations;
using MEC;
using Obvious.Soap;
using PrimeTween;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Tung
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityData _entityData;
        [SerializeField] protected Tilemap _tilemap;
        [SerializeField] protected GridMapVariable _gridMap;
        [SerializeField] protected AnimationController _animationController;
        [SerializeField] private FloatVariable _speed;
        [SerializeField] private Image _heatlthBar;
        [SerializeField] private SpriteRenderer _avatar;
        
        private float _currentHealth;
        
        public Entity entityTarget;
        public bool isMoving;
        public bool isReadyAttack;
        public bool isAttacking;
        public bool isAttack;
        public PathNode nodeBefore;
        public List<PathNode> pathNodes;
        public Vector3 posStart;
        public List<Transform> dirAttack;

        protected virtual void Start()
        {
            _currentHealth = _entityData.InitHealth.Value;
            Vector3Int temp = _tilemap.WorldToCell(transform.position);
            posStart = _tilemap.GetCellCenterWorld(temp);
            _gridMap.Value[(int)posStart.x, (int)posStart.y] = true;
            transform.position = posStart;
        }

        public IEnumerator<float> Move()
        {
            _animationController.SetAnimation(AnimationName.Idle, false);
            _gridMap.Value[(int)posStart.x, (int)posStart.y] = false;
            Vector2Int posTarget = DistanceDir(entityTarget);
            Vector2Int currentPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            pathNodes = GridMapManager.instance._pathfinding.FindPath(Mathf.RoundToInt(transform.position.x),
                                                            Mathf.RoundToInt(transform.position.y), posTarget.x, posTarget.y);
            if (DistanceCellEntityTarget(currentPos,
                new Vector2Int(Mathf.RoundToInt(entityTarget.transform.position.x),
                    Mathf.RoundToInt(entityTarget.transform.position.y))))
            {
                var x = entityTarget.transform.position.x - transform.position.x;
                if (x > 0)
                {
                    _avatar.flipX = false;
                }
                else
                {
                    _avatar.flipX = true;
                }
                       
                _animationController.SetAnimation(AnimationName.Move, false);
                isReadyAttack = true;
                _gridMap.Value[nodeBefore.xPos, nodeBefore.yPos] = false;
                _gridMap.Value[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)] = true;
                transform.position = _tilemap.WorldToCell(transform.position);
                yield break;
            }
            
            while (pathNodes != null && pathNodes.Count > 0)
            {
                _animationController.SetAnimation(AnimationName.Move, true);
                posTarget = DistanceDir(entityTarget);
                currentPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
                Vector2Int nextPos = new Vector2Int(pathNodes[0].xPos, pathNodes[0].yPos);
                transform.position = Vector2.MoveTowards(transform.position, nextPos, _speed * Time.deltaTime);

                if (DistanceCell(currentPos, nextPos))
                {
                    Vector2Int check = new Vector2Int(pathNodes[pathNodes.Count - 1].xPos,
                 pathNodes[pathNodes.Count - 1].yPos);
                    if (DistanceCell(currentPos, check))
                    {
                        var x = entityTarget.transform.position.x - transform.position.x;
                        if (x > 0)
                        {
                            _avatar.flipX = false;
                        }
                        else 
                        {
                            _avatar.flipX = true;
                        }
                       
                        _animationController.SetAnimation(AnimationName.Move, false);
                        isReadyAttack = true;
                        _gridMap.Value[nodeBefore.xPos, nodeBefore.yPos] = false;
                        _gridMap.Value[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)] = true;
                        transform.position = _tilemap.WorldToCell(transform.position);
                        break;
                    }
                    _gridMap.Value[nodeBefore.xPos, nodeBefore.yPos] = false;
                    _gridMap.Value[nextPos.x, nextPos.y] = true;
                    nodeBefore = pathNodes[0];
                }
                pathNodes = GridMapManager.instance._pathfinding.FindPath(currentPos.x, currentPos.y,
             posTarget.x, posTarget.y);
                yield return Timing.WaitForOneFrame;
            }
        }
        public IEnumerator<float> Attacking()
        {
            while (isAttack)
            {
                isAttacking = true;
                _animationController.SetAnimation(AnimationName.Attack, true);
                Vector2 dir = (entityTarget.transform.position - transform.position).normalized;
                _animationController.dir = dir;
                entityTarget.TakeDamage(_entityData.InitDamage);
                if (!entityTarget.gameObject.activeInHierarchy)
                {
                    transform.position = _tilemap.WorldToCell(transform.position);
                    isMoving = false;
                    isReadyAttack = false;
                    isAttacking = false;
                    isAttack = false;
                }
                yield return Timing.WaitForSeconds(_entityData.InitAttackSpeed);
            }
        }
        public void TakeDamage(float damage)
        {
            _animationController.SetAnimation(AnimationName.Hit, true);
            _currentHealth -= damage;
            _heatlthBar.fillAmount = _currentHealth / _entityData.InitHealth;
            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private Vector2Int DistanceDir(Entity target)
        {
            Vector2Int posTarget = Vector2Int.zero;
            float minDistance = float.MaxValue;

            foreach (Transform dir in target.dirAttack)
            {
                float distance = Vector2Int.Distance(new Vector2Int(Mathf.RoundToInt(dir.position.x), Mathf.RoundToInt(dir.position.y)), new Vector2Int((int)transform.position.x, (int)transform.position.y));

                if (distance < minDistance && !_gridMap.Value[Mathf.RoundToInt(dir.position.x), Mathf.RoundToInt(dir.position.y)])
                {
                    minDistance = distance;
                    posTarget = new Vector2Int(Mathf.RoundToInt(dir.position.x), Mathf.RoundToInt(dir.position.y));
                }
            }
            return posTarget;
        }
        private bool DistanceCellEntityTarget(Vector2Int pos1, Vector2Int pos2)
        {
            int manhattanDistance = Mathf.Abs(pos2.x - pos1.x) + Mathf.Abs(pos2.y - pos1.y);
            return Math.Abs(manhattanDistance - 1f) <= 0.2f;
        }

        private bool DistanceCell(Vector2Int pos1, Vector2Int pos2)
        {
            int manhattanDistance = Mathf.Abs(pos2.x - pos1.x) + Mathf.Abs(pos2.y - pos1.y);
            return Math.Abs(manhattanDistance) <= 0;
            ;
        }
    }
}