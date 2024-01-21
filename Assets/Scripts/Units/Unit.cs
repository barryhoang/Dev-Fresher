using System;
using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using PrimeTween;
using Ultilities;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private UnitViewer unitViewer;
        [SerializeField] private HealthViewer healthViewer;
        [SerializeField] private FloatVariable currentHealth;
        [SerializeField] private FloatVariable maxHealth;
        [SerializeField] private FloatVariable atkSpeed;
        [SerializeField] private FloatVariable speed;
        [SerializeField] private MapVariable mapVariable;
        [SerializeField] private ScriptableEventUnit onDead;
        [SerializeField] private Pathfinding pathfinding;
        
        public bool attacking;
        public bool moving;
        public Unit target;
        public Vector2Int unitPosition;

        [SerializeField] private List<PathNode> pathNodes;

        private void OnEnable()
        {
            unitViewer.ResetDirection();
            unitPosition = transform.position.ToV2Int();
            currentHealth.Value = maxHealth.Value;
            moving = false;
            attacking = false;
            healthViewer.SetMaxHp(currentHealth);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator<float> Move(Unit targetUnit)
        {
            if (!targetUnit) moving = false;
            target = targetUnit;
            if (moving && CheckDistance(transform.position,target.transform.position))
            {
                Attack();
            }

            if (!moving) yield break;
            var startNode = transform.position.ToV2Int();
            var endNode = CheckDirection(target.unitPosition);
            Debug.Log(endNode.x+" + "+endNode.y);
            if (endNode == Vector2Int.zero)
            {
                moving = false;
                yield break;
            }
                
            pathNodes = pathfinding.FindPath(startNode.x, startNode.y, 
                endNode.x, endNode.y, this);
            var targetNode = Vector2Int.zero;
            if (pathNodes != null)
            {
                targetNode = new Vector2Int(pathNodes[0].xPos, pathNodes[0].yPos);
                mapVariable.Value[startNode.x, startNode.y] = null;
                mapVariable.Value[targetNode.x, targetNode.y] = this;
                unitPosition = targetNode;
            }
            else moving = false;

            while (pathNodes is { Count: > 0 })
            {
                var position = transform.position;
                var direction = target.transform.position.x - position.x;
                unitViewer.Flip(direction);
                Vector2.MoveTowards(position, targetNode,
                    speed.Value * Time.deltaTime);
                if ((Vector2)transform.position == targetNode)
                {
                    if (CheckDistance(targetNode,target.unitPosition))
                    {
                        Attack();
                        break;
                    }

                    startNode = transform.position.ToV2Int();
                    endNode = CheckDirection(target.unitPosition);
                    if (endNode == Vector2Int.zero)
                    {
                        moving = false;
                        break;
                    }
                    pathNodes = pathfinding.FindPath(startNode.x, startNode.y, 
                        endNode.x, endNode.y, this);
                    if (pathNodes == null)
                    {
                        moving = false;
                        break;
                    }

                    targetNode = new Vector2Int(pathNodes[0].xPos, pathNodes[0].yPos);
                    mapVariable.Value[startNode.x, startNode.y] = null;
                    mapVariable.Value[targetNode.x, targetNode.y] = this;
                    unitPosition = targetNode;
                }

                yield return Timing.WaitForOneFrame;
            }
        }
        
        private static bool CheckDistance(Vector2 unitPos ,Vector2 targetPos)
        {                                   
            var distance = (int) (Mathf.Abs(unitPos.x - targetPos.x)
                                  + Mathf.Abs(unitPos.y - targetPos.y));
            return Mathf.Abs(distance) <= 1f;
        }

        private Vector2Int CheckDirection(Vector2Int tempTarget)
        {
            var start = transform.position.ToV2Int();
            var value = Vector2Int.zero;
            var minDistance = float.MaxValue;
            Vector2Int[] listDir = {tempTarget + Vector2Int.down,tempTarget + Vector2Int.left,
                tempTarget+ Vector2Int.right ,tempTarget + Vector2Int.up};
            foreach (var dir in listDir)
            {
                var distance = Vector2Int.Distance(start, dir);
                if (dir.x < 0 || dir.y < 0 
                              || dir.x >= mapVariable.size.x || dir.y >= mapVariable.size.y) continue;
                if (distance < minDistance && !mapVariable.Value[dir.x, dir.y])
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }

        private void Attack()
        {
            moving = false;
            attacking = true;
            Timing.RunCoroutine(Atk().CancelWith(gameObject));
        }

        private void TakeDamage(float damage)
        {
            currentHealth.Value -= damage;
            healthViewer.UpdateHp(currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth.Value = 0;
                Tween.StopAll();
                var diePos = transform.position.ToV2Int();
                mapVariable.Value[diePos.x, diePos.y] = null;
                gameObject.SetActive(false);
                onDead.Raise(this);
            }
        }

        private IEnumerator<float> Atk()
        {
            while (!CheckDistance(transform.position,target.transform.position))
            {
                yield return Timing.WaitForOneFrame;
                var direction = target.transform.position.x - transform.position.x;
                unitViewer.Flip(direction);
                while (attacking)
                {
                    if (!target.gameObject.activeInHierarchy)
                    {
                        attacking = false;
                        break;
                    }
                    var atkDir = (target.transform.position - transform.position).normalized;
                    unitViewer.atkDirection = atkDir;
                    unitViewer.StartAtkAnimation(this);
                    yield return Timing.WaitForSeconds(0.5f);
                    unitViewer.EndAtkAnimation(this);
                    yield return Timing.WaitForSeconds(atkSpeed);
                }
            }
        } 
    }
}
