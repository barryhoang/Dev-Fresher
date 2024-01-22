using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
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
        [SerializeField] private FloatVariable damage;
        [SerializeField] private MapVariable mapVariable;
        [SerializeField] private ScriptableEventUnit onDead;
        [SerializeField] private Pathfinding pathFinding;
        
        public bool attacking;
        public bool moving;
        public Unit target;
        public Vector2Int unitPosition;
        
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private List<PathNode> _pathNodes;

        private void Awake()
        {
            unitViewer.ResetDirection();
            unitPosition = transform.position.ToV2Int();
            currentHealth.Value = maxHealth.Value;
            moving = false;
            attacking = false;
            healthViewer.SetMaxHp(currentHealth);
        }
        
        public IEnumerator<float> Move(Unit targetUnit)
        {
            if (!targetUnit) moving = false;
            target = targetUnit;
            if (moving && CheckDistance(transform.position,target.transform.position)) Attack();
            if (!moving) yield break;
            var startNode = transform.position.ToV2Int();
            var endNode = CheckDirection(target.unitPosition);
            if (endNode == Vector2Int.zero)
            {
                moving = false;
                unitViewer.animator.SetBool(Moving, false);
                yield break;
            }
            unitViewer.animator.SetBool(Moving, true);
            _pathNodes = pathFinding.FindPath(startNode.x, startNode.y, 
                endNode.x, endNode.y, this);
            var targetNode = Vector2Int.zero;
            if (_pathNodes != null)
            {
                targetNode = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
                mapVariable.Value[startNode.x, startNode.y] = null;
                mapVariable.Value[targetNode.x, targetNode.y] = this;
                unitPosition = targetNode;
            }
            else moving = false;
            while (_pathNodes !=null && _pathNodes.Count > 0)
            {
                var position = transform.position;
                var direction = target.transform.position.x - position.x;
                unitViewer.Flip(direction);
                transform.position =  Vector2.MoveTowards(position, targetNode,
                    speed.Value * Time.deltaTime);
                if ((Vector2)transform.position == targetNode)
                {
                    if (CheckDistance(targetNode, target.unitPosition))
                    {
                        Attack();
                        yield break;
                    }
                    startNode = transform.position.ToV2Int();
                    endNode = CheckDirection(target.unitPosition); 
                    if (endNode == Vector2Int.zero)
                    {
                        moving = false;
                        yield break;
                    }
                    _pathNodes = pathFinding.FindPath(startNode.x, startNode.y, 
                        endNode.x, endNode.y, this);
                    if (_pathNodes == null)
                    {
                        moving = false;
                        yield break;
                    }
                    targetNode = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
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
            var unitPos = transform.position.ToV2Int();
            var value = Vector2Int.zero;
            var minDistance = float.MaxValue;
            Vector2Int[] listDir = {tempTarget + Vector2Int.down, tempTarget + Vector2Int.left,
                tempTarget + Vector2Int.right, tempTarget + Vector2Int.up};
            foreach (var dir in listDir)
            {
                var distance = Vector2Int.Distance(unitPos, dir);
                if (dir.x < 0 || dir.y < 0 
                              || dir.x >= mapVariable.size.x || dir.y >= mapVariable.size.y) continue;
                if (distance < minDistance && mapVariable.Value[dir.x, dir.y] == null)
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }

        private void TakeDamage(float dmg)
        {
            currentHealth.Value -= dmg;
            healthViewer.UpdateHp(currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth.Value = 0;
                unitViewer.animator.SetBool(Dead, true);
                gameObject.SetActive(false);
                onDead.Raise(this);
            }
        }

        private void Attack()
        {
            unitViewer.animator.SetBool(Moving, false);
            moving = false;
            attacking = true;
            Timing.RunCoroutine(Atk().CancelWith(gameObject));
        }

        private IEnumerator<float> Atk()
        {
            while (!CheckDistance(transform.position,target.transform.position))
            {
                Timing.WaitUntilDone(Timing.RunCoroutine(CheckAttackRange().CancelWith(gameObject)));
                var direction = target.transform.position.x - transform.position.x;
                unitViewer.Flip(direction);
                while (attacking)
                {
                    var atkDir = (target.transform.position - transform.position).normalized;
                    unitViewer.atkDirection = atkDir;
                    unitViewer.StartAtkAnimation(this);
                    yield return Timing.WaitForSeconds(0.5f);
                    target.TakeDamage(damage);
                    unitViewer.EndAtkAnimation(this);
                    yield return Timing.WaitForSeconds(atkSpeed-0.5f);
                }
            }
        }
        
        private IEnumerator<float> CheckAttackRange()
        {
            while (Vector2.Distance(transform.position, target.transform.position) > 1)
            {
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
